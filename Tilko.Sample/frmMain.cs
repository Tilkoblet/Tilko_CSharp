using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

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
			_dt.Rows.Add("http://beta.api.tilko.net/api/v1.0/Nhis/PaymentList", "건강보험공단(건강보험료납부내역)");
			//_dt.Rows.Add("", "건강보험공단(약국 이용 이력)");
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
				
				// 인증 처리
				_auth.ApiKey				= _txtAPI_KEY.Text;
				_auth.CertFilePath			= _certFilePath;
				_auth.KeyFilePath			= _keyFilePath;
				_auth.IdentityNumber		= _txtIdentityNumber.Text;
				_auth.CertPassword			= _txtCertPassword.Text;
				var _authRes				= _auth.Authenticate();

				// 데이터 가져오기
				_data.ApiKey				= _txtAPI_KEY.Text;
				_data.Body.Clear();
				_data.Body.Add("AuthCode", _authRes.AuthCode);	// 건강보험공단 틸코 서버의 인증코드
				_data.Body.Add("Year", "2019");					// 인증서 개인키
				_data.Body.Add("StartMonth", "01");				// 주민등록번호
				_data.Body.Add("EndMonth", "12");				// 인증서 암호
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
