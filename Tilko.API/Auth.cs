using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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
		AES _aes				= new AES();

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

		#region SetAesKey : AES 키 설정
		/// <summary>
		/// 서버로 전송할 데이터를 암호화할 AES의 키 값을 생성하고, 해당 키 값은 다시 서버 공개키로 RSA 암호화여 서버로 전달합니다.
		/// </summary>
		public void SetAesKey()
		{
			try
			{
				/*
				 * 공인인증서 정보를 암호화할 AES 키를 생성하고, AES-CBC-128로 암호화 합니다.
				 */
				byte[] _aesPlainKey				= new byte[16];
				_rnd.NextBytes(_aesPlainKey);	// AES 키는 랜덤하게 생성해도 되고, 고정 값을 사용하셔도 됩니다.
				// AES의 IV 값은 고정입니다.
				byte[] _aesIv					= new byte[16] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
				_aes.Key						= _aesPlainKey;
				_aes.Iv							= _aesIv;


				/*
				 * API 키에 해당하는 RSA 공개키 가져오기
				 */
				RsaPublicKey _pubKey;
				using (HttpClient _httpClient = new HttpClient())
				{
					_httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
					
					// 틸코 인증 서버에 RSA 공개키 요청
					var _response				= _httpClient.GetAsync("https://api.tilko.net/api/Auth/GetPublicKey?APIkey=" + this.ApiKey).Result;
					var _resContent				= _response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
					_pubKey						= JsonConvert.DeserializeObject<RsaPublicKey>(_resContent.ToString());
				}

				/*
				 * AES 암호화에 사용된 키는 서버로부터 전달 받은 RSA를 이용하여 암호화 해 주세요.
				 * RSA 공개키로 암호화된 값은 서버에 있는 RSA 개인키로만 복호화가 가능합니다.
				 */
				byte[] _aesCipherKey			= new byte[0];
				byte[] _rsaPublicKey			= Convert.FromBase64String(_pubKey.PublicKey);
				using (var _x509 = new X509Certificate2(_rsaPublicKey))
				{
					using (var _rsa = _x509.GetRSAPublicKey())
					{
						_aesCipherKey = _rsa.Encrypt(_aesPlainKey, RSAEncryptionPadding.Pkcs1);
					}
				}
				this.PlainEncKey				= _aesPlainKey;
				this.CipherEncKey				= _aesCipherKey;
			}
			catch
			{
				throw;
			}
		}
		#endregion
    }
}
