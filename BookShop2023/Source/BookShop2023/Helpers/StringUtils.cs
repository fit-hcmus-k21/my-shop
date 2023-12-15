using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingOneObject
{
    internal class StringUtils
    {
        public static string RemoveAccents(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Chuỗi chứa các ký tự có dấu
            string[] accents = { "á", "à", "ả", "ã", "ạ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ",
                                "đ", "Đ",
                              "é", "è", "ẻ", "ẽ", "ẹ", "ê", "ế", "ề", "ể", "ễ", "ệ",
                              "í", "ì", "ỉ", "ĩ", "ị",
                              "ó", "ò", "ỏ", "õ", "ọ", "ô", "ố", "ồ", "ổ", "ỗ", "ộ", "ơ", "ớ", "ờ", "ở", "ỡ", "ợ",
                              "ú", "ù", "ủ", "ũ", "ụ", "ư", "ứ", "ừ", "ử", "ữ", "ự",
                              "ý", "ỳ", "ỷ", "ỹ", "ỵ" };

            // Chuỗi chứa các ký tự không dấu tương ứng
            string[] unaccented = { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                                "d", "D",
                                "e", "e", "e", "e", "e", "e", "e", "e", "e", "e", "e",
                                "i", "i", "i", "i", "i",
                                "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o",
                                "u", "u", "u", "u", "u", "u", "u", "u", "u", "u", "u",
                                "y", "y", "y", "y", "y" };

            StringBuilder result = new StringBuilder();

            foreach (char c in input)
            {
                int index = Array.IndexOf(accents, c.ToString());
                if (index != -1)
                    result.Append(unaccented[index]);
                else
                    result.Append(c);
            }

            return result.ToString();
        }
    }
}
