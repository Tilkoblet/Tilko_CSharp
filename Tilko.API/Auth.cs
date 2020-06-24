using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Tilko.API.Models;

namespace Tilko.API
{
	/// <summary>
	/// 제  목: 틸코 API 인증 클래스
	/// 작성자: 손정민(help@tilko.net)
	/// 작성일: 2020-06-24 15:33
	/// 설  명: 틸코 API 인증을 담당하는 클래스입니다.
	/// </summary>
    public class Auth : Base
    {
		Random _rnd				= new Random();
		string _pubUrl			= "http://beta.api.tilko.net/api/Auth/GetPublicKey?APIkey=";
		string _authUrl			= "http://beta.api.tilko.net/api/v1.0/Nhis/Auth";

		#region Properties
		/// <summary>
		/// 인증서 공개키 파일 경로
		/// </summary>
		public string CertFilePath { get; set; }

		/// <summary>
		/// 인증서 개인키 파일 경로
		/// </summary>
		public string KeyFilePath { get; set; }

		/// <summary>
		/// 주민등록번호
		/// </summary>
		public string IdentityNumber { get; set; }

		/// <summary>
		/// 인증서 비밀번호
		/// </summary>
		public string CertPassword { get; set; }
		#endregion

		// Public Methods
		#region Authenticate : 인증처리 후 결과 반환
		/// <summary>
		/// 인증처리 후 결과 반환
		/// </summary>
		/// <returns></returns>
		public AuthResponse Authenticate()
		{
			try
			{
				if (!File.Exists(this.CertFilePath))
				{
					throw new Exception("인증서 경로에 인증서 공개키 파일이 존재하지 않습니다.");
				}
				else if (!File.Exists(this.KeyFilePath))
				{
					throw new Exception("인증서 경로에 인증서 개인키 파일이 존재하지 않습니다.");
				}
				if (string.IsNullOrEmpty(IdentityNumber))
				{
					throw new Exception("주민등록번호를 입력하세요.");
				}
				else if (string.IsNullOrEmpty(CertPassword))
				{
					throw new Exception("인증서 비밀번호를 입력하세요.");
				}

				/*
				 * API 키에 해당하는 RSA 공개키 가져오기
				 */
				RsaPublicKey _pubKey;
				using (HttpClient _httpClient = new HttpClient())
				{
					_httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
					
					// 틸코 인증 서버에 RSA 공개키 요청
					var _response				= _httpClient.GetAsync(_pubUrl + this.ApiKey).Result;
					var _resContent				= _response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
					_pubKey						= JsonConvert.DeserializeObject<RsaPublicKey>(_resContent.ToString());
				}


				/*
				 * 서버로 암호화 하여 전송할 데이터들
				 */
				byte[] _certPlainBytes			= File.ReadAllBytes(this.CertFilePath);
				byte[] _keyPlainBytes			= File.ReadAllBytes(this.KeyFilePath);
				byte[] _identityPlainBytes		= Encoding.ASCII.GetBytes(this.IdentityNumber.Replace("-", ""));
				byte[] _passwordPlainBytes		= Encoding.ASCII.GetBytes(this.CertPassword);


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
				byte[] _rsaPublicKey			= Convert.FromBase64String(_pubKey.PublicKey);
				using (var _rsa = new RSACryptoServiceProvider())
				{
					_rsa.ImportCspBlob(_rsaPublicKey);
					_aesCipherKey		= _rsa.Encrypt(_aesPlainKey, false);	// PKCS #1.5 처리
				}


				AuthResponse _authResponse;

				#region 인증 처리
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
					_httpClient.DefaultRequestHeaders.Add("API-Key", this.ApiKey);
					_httpClient.DefaultRequestHeaders.Add("ENC-Key", Convert.ToBase64String(_aesCipherKey));



					/*
					 * 본문 설정
					 * 암호화된 공인인증서 정보를 BASE64 포맷으로 전달합니다.
					 */
					var _dic					= new Dictionary<string, string>();
					_dic.Add("CertFile", Convert.ToBase64String(_certCipherBytes));				// 인증서 공개키
					_dic.Add("KeyFile", Convert.ToBase64String(_keyCipherBytes));				// 인증서 개인키
					_dic.Add("IdentityNumber", Convert.ToBase64String(_identityCipherBytes));	// 주민등록번호
					_dic.Add("CertPassword", Convert.ToBase64String(_passwordCipherBytes));		// 인증서 암호



					// 틸코 인증 서버에 건강보험공단 인증 요청
					var _reqContent				= new StringContent(JsonConvert.SerializeObject(_dic), Encoding.UTF8, "application/json");
					var _response				= _httpClient.PostAsync(_authUrl, _reqContent).Result;
					var _resContent				= _response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
					_authResponse				= JsonConvert.DeserializeObject<AuthResponse>(_resContent.ToString());
				}
				#endregion

				return _authResponse;
			}
			catch
			{
				throw;
			}
		}
		#endregion

		// Private Methods
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
}
