using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Tilko.API;

namespace Tilko.Sample
{
	/// <summary>
	/// 제  목: 틸코 API 샘플 프로그램
	/// 작성자: 손정민(help@tilko.net)
	/// 작성일: 2020-06-24 15:33
	/// 설  명: 틸코 API를 활용할 수 있는 C# 샘플 프로그램입니다.
	/// </summary>
	public partial class frmMain : Form
	{
		Tilko.API.Auth _auth	= new Tilko.API.Auth();
		Tilko.API.Data _data	= new Tilko.API.Data();
		AES _aes				= new AES();

		#region frmMain : 생성자
		/// <summary>
		/// 생성자
		/// </summary>
		public frmMain()
		{
			InitializeComponent();
		}
		#endregion

		#region frmMain_Load : 폼 로드
		/// <summary>
		/// 폼 로드
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void frmMain_Load(object sender, EventArgs e)
		{
			string _folderPath					= Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\LocalLow";
			if (Directory.Exists(_folderPath))
			{
				folderBrowserDialog1.SelectedPath	= _folderPath;
			}

			/*
			 * 각 데이터의 EndPoint를 정의합니다.
			 */
			var _dt								= new DataTable();
			_dt.Columns.Add("TOTAL_CODE", typeof(string));
			_dt.Columns.Add("CODE_NAME", typeof(string));
			_dt.Rows.Add("https://api.tilko.net/api/v1.0/nhis/jpaca00101/geongangboheom", "건강보험공단(건강보험료납부내역)");
			_dt.Rows.Add("https://api.tilko.net/api/v1.0/hira/hiraa050300000100", "내가 먹는 약");
			//_dt.Rows.Add("", "건강보험공단(병원 내원 이력)");

			_cmbEndPoint.ValueMember			= "TOTAL_CODE";
			_cmbEndPoint.DisplayMember			= "CODE_NAME";
			_cmbEndPoint.DataSource				= _dt;
		}
		#endregion

		#region _txtCertPath_DoubleClick : 인증서 경로 입력창 더블클릭
		/// <summary>
		/// 인증서 경로 입력창 더블클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _txtCertPath_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				this._SetFolderPath();
			}
			catch (Exception err)
			{
				MessageBox.Show(this, err.Message, "ERROR");
			}
		}
		#endregion

		#region _btnFind_Click : 찾기 버튼 클릭
		/// <summary>
		/// 찾기 버튼 클릭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _btnFind_Click(object sender, EventArgs e)
		{
			try
			{
				this._SetFolderPath();
			}
			catch (Exception err)
			{
				MessageBox.Show(this, err.Message, "ERROR");
			}
		}
		#endregion

		#region _SetFolderPath : 인증서 폴더 설정
		/// <summary>
		/// 인증서 폴더 설정
		/// </summary>
		void _SetFolderPath()
		{
			try
			{
				DialogResult _res	= folderBrowserDialog1.ShowDialog(this);
				if (_res == DialogResult.OK)
				{
					_txtCertPath.Text		= folderBrowserDialog1.SelectedPath;
				}
			}
			catch
			{
				throw;
			}
		}
		#endregion

		#region _btnOK_Click : 확인 버튼
		/// <summary>
		/// 확인 버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				_btnOK.Enabled				= false;
				_btnCancel.Enabled			= false;
				_txtResult.Text				= "";

				string _certFilePath		= string.Format(@"{0}\signCert.der", _txtCertPath.Text);
				string _keyFilePath			= string.Format(@"{0}\signPri.key", _txtCertPath.Text);

				// AES 키 생성 프로세스
				_auth.ApiKey				= _txtAPI_KEY.Text;
				_auth.SetAesKey();

				// AES KEY, IV 세팅
				_aes.Key					= _auth.PlainEncKey;
				// IV 값은 고정입니다.
				_aes.Iv						= new byte[16] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

				// 인증서 정보 암호화 처리
				byte[] _certFile			= _aes.Encrypt(File.ReadAllBytes(_certFilePath));
				byte[] _keyFile				= _aes.Encrypt(File.ReadAllBytes(_keyFilePath));
				byte[] _certPassword		= _aes.Encrypt(Encoding.ASCII.GetBytes(_txtCertPassword.Text));
				byte[] _identityNumber		= _aes.Encrypt(Encoding.ASCII.GetBytes(_txtIdentityNumber.Text));

				_data.Body.Clear();
				// API 별 파라미터 설정
				if (_cmbEndPoint.SelectedValue.ToString() == "https://api.tilko.net/api/v1.0/nhis/jpaca00101/geongangboheom")
				{
					// 검색 파라미터
					_data.Body.Add("Year", "2019");
					_data.Body.Add("StartMonth", "01");
					_data.Body.Add("EndMonth", "12");
				}
				else
				{
					// 암호화 처리
					byte[] _cellphoneNumber		= _aes.Encrypt(Encoding.ASCII.GetBytes("01012345678"));
					_data.Body.Add("CellphoneNumber", Convert.ToBase64String(_cellphoneNumber));
					_data.Body.Add("TelecomCompany", "0");
				}

				// 데이터 가져오기
				_data.ApiKey				= _txtAPI_KEY.Text;		// API 키 설정
				_data.PlainEncKey			= _auth.PlainEncKey;	// API 키 설정
				_data.CipherEncKey			= _auth.CipherEncKey;

				#region 데이터 조회
				_data.Body.Add("CertFile", Convert.ToBase64String(_certFile));
				_data.Body.Add("KeyFile", Convert.ToBase64String(_keyFile));
				_data.Body.Add("CertPassword", Convert.ToBase64String(_certPassword));
				_data.Body.Add("IdentityNumber", Convert.ToBase64String(_identityNumber));
				#endregion

				var _dataRes				= _data.GetData(new Uri(_cmbEndPoint.SelectedValue.ToString()));
				_txtResult.Text				= _dataRes;
			}
			catch (Exception err)
			{
				MessageBox.Show(this, err.Message, "ERROR");
			}
			finally
			{
				_btnOK.Enabled				= true;
				_btnCancel.Enabled			= true;
			}
		}
		#endregion

		#region _btnCancel_Click : 취소 버튼
		/// <summary>
		/// 취소 버튼
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void _btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		#endregion
	}
}
