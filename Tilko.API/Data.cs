using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Tilko.API
{
	public class Data : Base
	{
		#region Properties
		/// <summary>
		/// 검색 조건들
		/// </summary>
		public Dictionary<string, object> Body { get; set; }
		#endregion

		#region Data : 생성자
		/// <summary>
		/// 생성자
		/// </summary>
		public Data()
		{
			this.Body		= new Dictionary<string, object>();
		}
		#endregion

		#region GetData : 데이터 가져오기
		/// <summary>
		/// 데이터 가져오기
		/// </summary>
		/// <param name="EndPoint"></param>
		/// <returns></returns>
		public string GetData(Uri EndPoint)
		{
			try
			{
				#region 데이터 처리
				using (HttpClient _httpClient = new HttpClient())
				{
					_httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
						
					/*
					 * 헤더 설정
					 */
					_httpClient.DefaultRequestHeaders.Add("API-Key", this.ApiKey);
					_httpClient.DefaultRequestHeaders.Add("ENC-Key", Convert.ToBase64String(this.CipherEncKey));

					// 틸코 데이터 서버에 데이터 요청
					string _dataUrl				= EndPoint.AbsoluteUri;
					var _reqContent				= new StringContent(JsonConvert.SerializeObject(this.Body), Encoding.UTF8, "application/json");
					var _response				= _httpClient.PostAsync(_dataUrl, _reqContent).Result;
					var _resContent				= _response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
					return _resContent.ToString();
				}
				#endregion
			}
			catch
			{
				throw;
			}
		}
		#endregion
	}
}
