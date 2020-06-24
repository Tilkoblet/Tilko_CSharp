using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
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
		Random _rnd				= new Random();
		string _authUrl			= "http://beta.api.tilko.net/api/v1.0/Nhis/Auth";
		string _pubKeyUrl		= "http://beta.api.tilko.net/api/v1.0/Auth/GetPublicKey";

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
			_dt.Rows.Add("", "건강보험공단(약국 이용 이력)");
			_dt.Rows.Add("", "건강보험공단(병원 내원 이력)");

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
				if (string.IsNullOrEmpty(_txtCertPath.Text))
				{
					MessageBox.Show(this, "인증서 경로를 설정해 주세요.", "WARNING");
					return;
				}
				else if (string.IsNullOrEmpty(_txtIdentityNumber.Text))
				{
					MessageBox.Show(this, "주민등록번호를 입력하세요.", "WARNING");
					return;
				}
				else if (string.IsNullOrEmpty(_txtCertPassword.Text))
				{
					MessageBox.Show(this, "인증서 비밀번호를 입력하세요.", "WARNING");
					return;
				}
				else
				{
					string _certFilePath		= string.Format(@"{0}\signCert.der", _txtCertPath.Text);
					string _keyFilePath			= string.Format(@"{0}\signPri.key", _txtCertPath.Text);

					if (!File.Exists(_certFilePath))
					{
						MessageBox.Show(this, "인증서 경로에 인증서 공개키 파일이 존재하지 않습니다.", "WARNING");
						return;
					}
					else if (!File.Exists(_keyFilePath))
					{
						MessageBox.Show(this, "인증서 경로에 인증서 개인키 파일이 존재하지 않습니다.", "WARNING");
						return;
					}


					/*
					 * 서버로 암호화 하여 전송할 데이터들
					 */
					byte[] _certPlainBytes			= File.ReadAllBytes(_certFilePath);
					byte[] _keyPlainBytes			= File.ReadAllBytes(_keyFilePath);
					byte[] _identityPlainBytes		= Encoding.ASCII.GetBytes(_txtIdentityNumber.Text.Replace("-", ""));
					byte[] _passwordPlainBytes		= Encoding.ASCII.GetBytes(_txtCertPassword.Text);


					/*
					 * 공인인증서 정보를 암호화할 AES 키를 생성하고, AES-CBC-128로 암호화 합니다.
					 */
					byte[] _aesPlainKey				= new byte[16];
					_rnd.NextBytes(_aesPlainKey);	// AES 키는 랜덤하게 생성해도 되고, 고정 값을 사용하셔도 됩니다.
					// AES의 IV 값은 고정입니다.
					byte[] _aesIv				= new byte[16] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };


					/*
					 * AES 키를 이용하여 암호화합니다.
					 */
					byte[] _certCipherBytes			= this._AesEncrypt(_aesPlainKey, _aesIv, _certPlainBytes);
					byte[] _keyCipherBytes			= this._AesEncrypt(_aesPlainKey, _aesIv, _keyPlainBytes);
					byte[] _identityCipherBytes		= this._AesEncrypt(_aesPlainKey, _aesIv, _identityPlainBytes);
					byte[] _passwordCipherBytes		= this._AesEncrypt(_aesPlainKey, _aesIv, _passwordPlainBytes);


					/*
					 * AES 암호화에 사용된 키는 서버로부터 전달 받은 RSA를 이용하여 암호화 해 주세요.
					 * RSA 공개키로 암호화된 값은 서버에 있는 RSA 개인키로만 복호화가 가능합니다.
					 */
					byte[] _aesCipherKey			= new byte[0];
					byte[] _rsaPublicKey			= Convert.FromBase64String(_txtRSA_PUBLIC_KEY.Text);
					using (var _rsa = new RSACryptoServiceProvider())
					{
						_rsa.ImportCspBlob(_rsaPublicKey);
						_aesCipherKey		= _rsa.Encrypt(_aesPlainKey, false);	// PKCS #1.5 처리
					}


					/*
					 * 틸코의 인증 서버로부터 건강보험공단에 공인인증서 로그인 인증 번호를 부여 받습니다.
					 */
					using (HttpClient _httpClient = new HttpClient())
					{
						_httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
						
						/*
						 * 헤더 설정
						 * 헤더에는 API키와 암호화된 AES의 키 값이 들어가야 합니다.
						 * 여기서 암호화된 AES 키 값은 BASE64 포맷으로 전달합니다.
						 */
						_httpClient.DefaultRequestHeaders.Add("API-Key", _txtAPI_KEY.Text);
						_httpClient.DefaultRequestHeaders.Add("ENC-Key", Convert.ToBase64String(_aesCipherKey));

						/*
						 * 본문 설정
						 * 암호화된 공인인증서 정보를 BASE64 포맷으로 전달합니다.
						 */
						var _dic				= new Dictionary<string, string>();
						_dic.Add("CertFile", Convert.ToBase64String(_certCipherBytes));				// 인증서 공개키
						_dic.Add("KeyFile", Convert.ToBase64String(_keyCipherBytes));				// 인증서 개인키
						_dic.Add("IdentityNumber", Convert.ToBase64String(_identityCipherBytes));	// 주민등록번호
						_dic.Add("CertPassword", Convert.ToBase64String(_passwordCipherBytes));		// 인증서 암호



						// JSON 전송 방법
						var _reqContent			= new StringContent(JsonConvert.SerializeObject(_dic), Encoding.UTF8, "application/json");
						var _response			= _httpClient.PostAsync(_authUrl, _reqContent).Result;
						var _resContent			= _response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
						var _authResponse		= JsonConvert.DeserializeObject<AuthResponse>(_resContent.ToString());
						_txtResult.Text			= _resContent.ToString();
					}
				}
			}
			catch (Exception err)
			{
				MessageBox.Show(this, err.Message, "ERROR");
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

		#region _AesEncrypt : AES로 암호화된 값 반환
		/// <summary>
		/// AES로 암호화된 값 반환
		/// </summary>
		/// <returns></returns>
		public byte[] _AesEncrypt(byte[] Key, byte[] IV, byte[] PlainText)
		{
			byte[] _ret		= new byte[0];

			try
			{
				using (RijndaelManaged _aes = new RijndaelManaged())
				{
					_aes.Key				= Key;
					_aes.IV					= IV;
					_aes.Mode				= CipherMode.CBC;
					_aes.Padding			= PaddingMode.PKCS7;

					using (ICryptoTransform _enc = _aes.CreateEncryptor(_aes.Key, _aes.IV))
					{
						using (MemoryStream _ms = new MemoryStream())
						{
							using (CryptoStream _cs = new CryptoStream(_ms, _enc, CryptoStreamMode.Write))
							{
								_cs.Write(PlainText, 0, PlainText.Length);
								_cs.FlushFinalBlock();
								_ret = _ms.ToArray();
							}
						}
					}
					_aes.Clear();
				}
			}
			catch
			{
				throw;
			}

			return _ret;
		}
		#endregion
	}

	#region class AuthResponse : 건강보험공단 인증결과 데이터 모델
	/// <summary>
	/// 건강보험공단 인증결과 데이터 모델
	/// </summary>
	internal class AuthResponse
	{
		public string Status { get; set; }

		public string Message { get; set; }

		public long AuthCode { get; set; }
	}
	#endregion
}
