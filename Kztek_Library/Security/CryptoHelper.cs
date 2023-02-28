using Kztek.Security;

namespace Kztek_Library.Security
{
    public class CryptoHelper
    {
        /// <summary>
        /// Mã hóa mật khẩu người dùng
        /// </summary>
        /// <param name="pass">mk gốc</param>
        /// <param name="passsalat">mk salat của mỗi tk</param>
        /// <returns>mk mã hóa</returns>
        public static string EncryptPass_User(string pass, string passsalat)
        {   
            var privatekey = SecurityModel.System_Key;

            //Mã hóa lần 1
            pass = CryptoProvider.SimpleEncryptWithPassword(pass, passsalat);

            //Mã hóa lần 2
            pass = CryptoProvider.SimpleEncryptWithPassword(pass, privatekey);

            return pass;
        }

        /// <summary>
        /// Giải mã mật khẩu
        /// </summary>
        /// <param name="pass">mk mã hóa</param>
        /// <param name="passsalat">mk salat của mỗi tk</param>
        /// <returns>mk gốc</returns>
        public static string DecryptPass_User(string pass, string passsalat)
        {   
            var privatekey = SecurityModel.System_Key;

            //Giải mã lần 1
            pass = CryptoProvider.SimpleDecryptWithPassword(pass, privatekey);

            //Giải mã lần 2
            pass = CryptoProvider.SimpleDecryptWithPassword(pass, passsalat);

            return pass;
        }

        /// <summary>
        /// Mã hóa thông tin session người dùng đăng nhập
        /// </summary>
        /// <param name="userjson">Serialize model gốc</param>
        /// <returns>thông tin mã hóa để lưu</returns>
        public static string EncryptSessionCookie_User(string userjson)
        {   
            var privatekey = SecurityModel.Session_Key;

            //Mã hóa lần 1
            userjson = CryptoProvider.SimpleEncryptWithPassword(userjson, privatekey);

            //Mã hóa lần 2
            return userjson;
        }

        /// <summary>
        /// Giải mã thông tin session người dùng đăng nhập
        /// </summary>
        /// <param name="userjson">model đã mã hóa</param>
        /// <returns>Serialize model gốc</returns>
        public static string DecryptSessionCookie_User(string userjson)
        {   
            var privatekey = SecurityModel.Session_Key;

            //Mã hóa lần 1
            userjson = CryptoProvider.SimpleDecryptWithPassword(userjson, privatekey);

            //Mã hóa lần 2
            return userjson;
        }
    }
}