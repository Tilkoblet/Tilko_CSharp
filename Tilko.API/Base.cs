using System;
using System.Collections.Generic;

namespace Tilko.API
{
	public class Base
	{
		/// <summary>
		/// API 키
		/// </summary>
		public string ApiKey { get; set; }

		/// <summary>
		/// 암호화 키
		/// </summary>
		public byte[] EncKey { get; set; }
	}
}
