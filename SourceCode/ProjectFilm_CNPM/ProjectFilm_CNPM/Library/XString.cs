using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ProjectFilm_CNPM.Library
{
    public static class XString
    {
        public static string Str_Slug(string s)
        {
            string[][] symbols ={
                new String[]{"[áàảãạâấầẩẫậăắằẳẵặ]","a"},
                new String[]{"[đ]","d"},
                new String[]{"[éèẻẽẹêếềểễệ]","e"},
                new String[]{"[íìỉĩị]","i"},
                new String[]{"[óòỏõọôốồổỗộơớờởỡợ]","o"},
                new String[]{"[úùủũụưứừửữự]","u"},
                new String[]{"[ýỳỷỹỵ]","y"}
    };
            s = s.ToLower();
            foreach (var ss in symbols)
            {
                s = Regex.Replace(s, ss[0], ss[1]);
            }
            // Loại bỏ các ký tự không hợp lệ khác trong tên file
            s = Regex.Replace(s, "[^a-zA-Z0-9_.]+", "-", RegexOptions.Compiled);
            // Loại bỏ các dấu gạch ngang liền nhau
            s = Regex.Replace(s, "-{2,}", "-", RegexOptions.Compiled);
            return s;
        }
        public static string ToMD5(this string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sbHash = new StringBuilder();
            foreach (byte b in bHash)
            {
                sbHash.Append(String.Format("{0:x2}", b));
            }
            return sbHash.ToString();
        }
        public static string ToShortString(this string str, int? length)
        {
            int lengt = (length ?? 20);
            if (str.Length > lengt)
            {
                str = str.Substring(0, lengt) + "...";
            }
            return str;
        }
    }
}