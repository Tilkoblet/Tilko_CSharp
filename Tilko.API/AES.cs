using System;
using System.IO;
using System.Security.Cryptography;

namespace Tilko.API
{
	public class AES
	{
		public byte[] Key { get; set; }

		public byte[] Iv { get; set; }

		public byte[] Encrypt(byte[] PlainText)
		{
			byte[] _ret		= new byte[0];

			try
			{
				using (RijndaelManaged _aes = new RijndaelManaged())
				{
					_aes.Key				= this.Key;
					_aes.IV					= this.Iv;
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
	}
}
