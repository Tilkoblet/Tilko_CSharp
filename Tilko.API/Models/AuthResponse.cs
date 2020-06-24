using System;

namespace Tilko.API.Models
{
	/// <summary>
	/// 건강보험공단 인증결과 데이터 모델
	/// </summary>
	public class AuthResponse : BaseModel
	{
		public long AuthCode { get; set; }
	}
}
