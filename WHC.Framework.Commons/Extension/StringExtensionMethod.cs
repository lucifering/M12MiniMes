﻿using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace WHC.Framework.Commons
{
    /// <summary>
    /// 用于字符串转换其他类型的扩展函数
    /// </summary>
    public static class StringExtensionMethod
    {
        #region 字符串转换其他格式

        /// <summary>
        /// 通用的转换数据类型，支持可空类型
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="convertibleValue"></param>
        /// <example>
        /// <![CDATA[例如："123".ConvertTo<int?>();]]>
        /// </example>
        /// <returns></returns>
        public static T ConvertTo<T>(this IConvertible convertibleValue)
        {
            if (null == convertibleValue)
            {
                return default(T);
            }

            if (!typeof(T).IsGenericType)
            {
                return (T)Convert.ChangeType(convertibleValue, typeof(T));
            }
            else
            {
                Type genericTypeDefinition = typeof(T).GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    return (T)Convert.ChangeType(convertibleValue, Nullable.GetUnderlyingType(typeof(T)));
                }
            }
            throw new InvalidCastException(string.Format("Invalid cast from type \"{0}\" to type \"{1}\".", convertibleValue.GetType().FullName, typeof(T).FullName));
        }

        /// <summary>
        /// 转换字符串为日期类型
        /// </summary>
        /// <param name="str">字符串内容</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str)
        {
            str = ConvertHelper.ConvertToDBC(str);//先转换为半角字符串
            DateTime defaultValue = Convert.ToDateTime("1900-1-1");
            bool converted = DateTime.TryParse(str, out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 转换字符串为Int类型，可以指定默认值
        /// </summary>
        /// <param name="str">字符串内容</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int ToInt32(this string str, int defaultValue = 0)
        {
            str = ConvertHelper.ConvertToDBC(str);//先转换为半角字符串
            bool converted = int.TryParse(str, out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 转换字符串为Int16类型，可以指定默认值
        /// </summary>
        /// <param name="str">字符串内容</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int ToInt16(this string str, Int16 defaultValue = 0)
        {
            str = ConvertHelper.ConvertToDBC(str);//先转换为半角字符串
            bool converted = Int16.TryParse(str, out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 转换字符串为Int64类型，可以指定默认值
        /// </summary>
        /// <param name="str">字符串内容</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static Int64 ToInt64(this string str, Int64 defaultValue = 0)
        {
            str = ConvertHelper.ConvertToDBC(str);//先转换为半角字符串
            bool converted = Int64.TryParse(str, out defaultValue);
            return defaultValue;
        }


        /// <summary>
        /// 转换字符串为UInt64类型，可以指定默认值
        /// </summary>
        /// <param name="str">字符串内容</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static UInt64 ToUInt64(this string str, UInt64 defaultValue = 0)
        {
            str = ConvertHelper.ConvertToDBC(str);//先转换为半角字符串
            bool converted = UInt64.TryParse(str, out defaultValue);
            return defaultValue;
        }
        /// <summary>
        /// 转换字符串为uint类型，可以指定默认值
        /// </summary>
        /// <param name="str">字符串内容</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static uint ToUInt32(this string str, uint defaultValue = 0)
        {
            str = ConvertHelper.ConvertToDBC(str);//先转换为半角字符串
            bool converted = uint.TryParse(str, out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 转换字符串为UInt16类型，可以指定默认值
        /// </summary>
        /// <param name="str">字符串内容</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static UInt16 ToUInt16(this string str, UInt16 defaultValue = 0)
        {
            str = ConvertHelper.ConvertToDBC(str);//先转换为半角字符串
            bool converted = UInt16.TryParse(str, out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 转换字符串为byte类型，可以指定默认值
        /// </summary>
        /// <param name="str">字符串内容</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static byte ToByte(this string str, byte defaultValue = 0)
        {
            str = ConvertHelper.ConvertToDBC(str);//先转换为半角字符串
            bool converted = byte.TryParse(str, out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 转换字符串为double类型，可以指定默认值
        /// </summary>
        /// <param name="str">字符串内容</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static double ToDouble(this string str, double defaultValue = 0)
        {
            str = ConvertHelper.ConvertToDBC(str);//先转换为半角字符串
            bool converted = double.TryParse(str, out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 转换字符串为decimal类型，可以指定默认值
        /// </summary>
        /// <param name="str">字符串内容</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static decimal ToDecimal(this string str, decimal defaultValue = 0M)
        {
            str = ConvertHelper.ConvertToDBC(str);//先转换为半角字符串
            bool converted = decimal.TryParse(str, out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 转换字符串为float类型，可以指定默认值
        /// </summary>
        /// <param name="str">字符串内容</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static float ToFloat(this string str, float defaultValue = 0)
        {
            str = ConvertHelper.ConvertToDBC(str);//先转换为半角字符串
            bool converted = float.TryParse(str, out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 转换字符串为float类型，可以指定默认值
        /// </summary>
        /// <param name="str">字符串内容</param>
        /// <returns></returns>
        public static bool ToBoolean(this string str)
        {
            str = ConvertHelper.ConvertToDBC(str);//先转换为半角字符串

            //bool defaultValue = false;
            //bool converted = bool.TryParse(str, out defaultValue);
            //return defaultValue;

            bool ret = false;
            switch (str.ToLower())
            {
                case "true":
                case "t":
                case "yes":
                case "y":
                case "1":
                case "-1":
                    ret = true;
                    break;

                case "false":
                case "f":
                case "no":
                case "n":
                case "0":
                    ret = false;
                    break;
                default:
                    ret = false;
                    break;
            }
            return ret;
        }

        /// <summary>
        /// 字符串转换为指定格式的列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">字符串内容</param>
        /// <param name="delimiter">分隔符号</param>
        /// <returns></returns>
        public static List<T> ToDelimitedList<T>(this string value, string delimiter)
        {
            if (value == null)
            {
                return new List<T>();
            }

            var output = value.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
            return output.Select(x => (T)Convert.ChangeType(x.Trim(), typeof(T))).ToList();
        }

        /// <summary>
        /// 字符串转换为指定格式的列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">字符串内容</param>
        /// <param name="delimiter">分隔符号</param>
        /// <param name="converter">提供的转换操作</param>
        /// <returns></returns>
        public static List<T> ToDelimitedList<T>(this string value, string delimiter, Func<string, T> converter)
        {
            if (value == null)
            {
                return new List<T>();
            }

            var output = value.Split(new string[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
            return output.Select(converter).ToList();
        }

        /// <summary>
        /// 根据长度分割不同的字符串到列表里面
        /// </summary>
        /// <param name="value">字符串内容</param>
        /// <param name="length">分割的长度</param>
        /// <returns></returns>
        public static IEnumerable<string> SplitEvery(this string value, int length)
        {
            int index = 0;
            while (index + length < value.Length)
            {
                yield return value.Substring(index, length);
                index += length;
            }

            if (index < value.Length)
            {
                yield return value.Substring(index, value.Length - index);
            }
        }

        /// <summary>
        /// 把两个字符串组合为URL的路径形式
        /// </summary>
        /// <param name="val">URL1</param>
        /// <param name="append">增加的URL2</param>
        /// <returns>返回两个URL路径的组合</returns>
        public static string UriCombine(this string val, string append)
        {
            if (string.IsNullOrEmpty(val)) return append;
            if (string.IsNullOrEmpty(append)) return val;

            return val.TrimEnd('/') + "/" + append.TrimStart('/');
        }

        /// <summary>
        /// 将两个路径组合一个合适的路径
        /// </summary>
        /// <param name="val">路径</param>
        /// <param name="path2">追加的路径</param>
        /// <returns></returns>
        public static string PathCombine(this string val, string path2)
        {
            if (Path.IsPathRooted(path2))
            {
                path2 = path2.TrimStart(Path.DirectorySeparatorChar);
                path2 = path2.TrimStart(Path.AltDirectorySeparatorChar);
            }

            return Path.Combine(val, path2);
        }

        #endregion

        #region 其他辅助方法

        /// <summary>
        /// 如果是有效的Email地址，则返回True
        /// </summary>
        /// <param name="email">邮件地址</param>
        /// <returns>如果是有效的Email地址，则返回True</returns>
        public static bool IsValidEmailAddress(this string email)
        {
            if (string.IsNullOrEmpty(email)) return false;

            return new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,6}$").IsMatch(email);

            //const string expresion = @"^(?:[a-zA-Z0-9_'^&/+-])+(?:\.(?:[a-zA-Z0-9_'^&/+-])+)*@(?:(?:\[?(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?))\.){3}(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\]?)|(?:[a-zA-Z0-9-]+\.)+(?:[a-zA-Z]){2,}\.?)$";
            //Regex regex = new Regex(expresion, RegexOptions.IgnoreCase);
            //return regex.IsMatch(email);      
        }

        /// <summary>
        /// 检查URL是否有效
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <returns></returns>
        public static bool IsValidUrl(this string url)
        {
            string strRegex = "^(https?://)"
        + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //user@
        + @"(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP- 199.194.52.184
        + "|" // allows either IP or domain
        + @"([0-9a-z_!~*'()-]+\.)*" // tertiary domain(s)- www.
        + @"([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]" // second level domain
        + @"(\.[a-z]{2,6})?)" // first level domain- .com or .museum is optional
        + "(:[0-9]{1,5})?" // port number- :80
        + "((/?)|" // a slash isn't required if there is no file name
        + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";
            return new Regex(strRegex).IsMatch(url);
        }

        /// <summary>
        /// 检查字符串是否是有效的URI
        /// </summary>
        /// <param name="val">输入字符串</param>
        /// <returns>如果它是有效的URI，则返回true</returns>
        public static bool IsValidURI(this string val)
        {
            if (string.IsNullOrEmpty(val)) return false;

            return Uri.IsWellFormedUriString(val, UriKind.Absolute);
        }

        /// <summary>
        /// 检查URL(http)是否可用.
        /// </summary>
        /// <param name="httpUri">URL地址</param>
        /// <example>
        /// string url = "www.codeproject.com;
        /// if( !url.UrlAvailable())
        ///     ...codeproject is not available
        /// </example>
        /// <returns>true if available</returns>
        public static bool UrlAvailable(this string httpUrl)
        {
            if (!httpUrl.StartsWith("http://") || !httpUrl.StartsWith("https://"))
                httpUrl = "http://" + httpUrl;
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(httpUrl);
                myRequest.Method = "GET";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myRequest.GetResponse();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 反转字符串内容的顺序
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns></returns>
        public static string Reverse(this string input)
        {
            char[] charArray = null;
            string ret = string.Empty;
            if (input != null)
            {
                charArray = input.ToCharArray();
                Array.Reverse(charArray);
                ret = new string(charArray);
            }
            return ret;
        }
        
        /// <summary>
        /// 减少字符串到更短的预览，可选地由某些字符串（...）结束。
        /// </summary>
        /// <param name="s">待减少的字符串</param>
        /// <param name="count">返回字符串的长度，包括结尾。</param>
        /// <param name="endings">缩减文本的可选结尾</param>
        /// <example>
        /// string description = "This is very long description of something";
        /// string preview = description.Reduce(20,"...");
        /// produce -> "This is very long..."
        /// </example>
        /// <returns></returns>
        public static string Reduce(this string s, int count, string endings)
        {
            if (count < endings.Length)
                throw new Exception("Failed to reduce to less then endings length.");
            int sLength = s.Length;
            int len = sLength;
            if (endings != null)
                len += endings.Length;
            if (count > sLength)
                return s; //it's too short to reduce
            s = s.Substring(0, sLength - len + count);
            if (endings != null)
                s += endings;
            return s;
        }

        /// <summary>
        /// 从字符串中替换指定的字符，可以指定是否大小写敏感。
        /// </summary>
        /// <param name="str">输入的字符串</param>
        /// <param name="find">查找字符串</param>
        /// <param name="replacement">替换字符串</param>
        /// <param name="caseSensitive">是否大小写敏感</param>
        /// <returns></returns>
        public static String Replace(this String str, String find, String replacement, bool caseSensitive)
        {
            if (caseSensitive)
                return Microsoft.VisualBasic.Strings.Replace(str, find, replacement, 1, -1, CompareMethod.Binary);
            else
                return Microsoft.VisualBasic.Strings.Replace(str, find, replacement, 1, -1, CompareMethod.Text);
        }

        /// <summary>
        /// 在解析用户输入如电话，价格的时候，删除不是行末端的空白
        /// int.Parse("1 000 000".RemoveSpaces(),.....
        /// </summary>
        /// <param name="s"></param>
        /// <param name="value">没有空白的字符串</param>
        public static string RemoveSpaces(this string s)
        {
            return s.Replace(" ", "");
        }

        /// <summary>
        /// 如果指定为浮点数，数值能够转换为Double类型，返回True；否则Int32也认为是数值（中间空格会被忽略）
        /// </summary>
        /// <param name="s">输入字符串</param>
        /// <param name="floatpoint">如果指定为浮点数，则为True，否则为Int32类型</param>
        /// <returns>如果字符串只包含数字或浮点数，则返回True</returns>
        public static bool IsNumber(this string s, bool floatpoint)
        {
            int i;
            double d;
            string withoutWhiteSpace = s.RemoveSpaces();
            if (floatpoint)
            {
                return double.TryParse(withoutWhiteSpace, NumberStyles.Any,
                    Thread.CurrentThread.CurrentUICulture, out d);
            }
            else
            {
                return int.TryParse(withoutWhiteSpace, out i);
            }
        }

        /// <summary>
        /// 如果字符串只包含数字或浮点数则为True，不考虑空格
        /// </summary>
        /// <param name="s">输入字符串</param>
        /// <param name="floatpoint">如果指定为浮点数，则为True</param>
        /// <returns>如果字符串只包含数字或浮点数则为True</returns>
        public static bool IsNumberOnly(this string s, bool floatpoint)
        {
            s = s.Trim();
            if (s.Length == 0)
                return false;
            foreach (char c in s)
            {
                if (!char.IsDigit(c))
                {
                    if (floatpoint && (c == '.' || c == ','))
                        continue;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 从字符串中删除重音
        /// </summary>
        /// <example>
        ///  input:  "Příliš žluťoučký kůň úpěl ďábelské ódy."
        ///  result: "Prilis zlutoucky kun upel dabelske ody."
        /// </example>
        /// <param name="s"></param>
        /// <remarks>founded at http://stackoverflow.com/questions/249087/
        /// how-do-i-remove-diacritics-accents-from-a-string-in-net</remarks>
        /// <returns>string without accents</returns>
        public static string RemoveDiacritics(this string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }
            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        /// <summary>
        /// 使用<br/>替换 \r\n 或者 \n
        /// </summary>
        /// <param name="s">输入字符串</param>
        /// <returns></returns>
        public static string Nl2Br(this string s)
        {
            return s.Replace("\r\n", "<br />").Replace("\n", "<br />");
        }


        static MD5CryptoServiceProvider s_md5 = null;
        /// <summary>
        /// 使用MD5加密字符串
        /// </summary>
        /// <param name="s">输入字符串</param>
        /// <returns></returns>
        public static string MD5(this string s)
        {
            if (s_md5 == null) //creating only when needed
                s_md5 = new MD5CryptoServiceProvider();
            Byte[] newdata = Encoding.Default.GetBytes(s);
            Byte[] encrypted = s_md5.ComputeHash(newdata);
            return BitConverter.ToString(encrypted).Replace("-", "").ToLower();
        }


        /// <summary>
        /// 用字符串中的另一个字符代替给定的字符。 
        /// 替换不区分大小写
        /// </summary>
        /// <param name="val"></param>
        /// <param name="charToReplace">The character to replace</param>
        /// <param name="replacement">The character by which to be replaced</param>
        /// <returns>Copy of string with the characters replaced</returns>
        public static string CaseInsenstiveReplace(this string val, char charToReplace, char replacement)
        {
            Regex regEx = new Regex(charToReplace.ToString(), RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regEx.Replace(val, replacement.ToString());
        }
        /// <summary>
        /// 用字符串中的另一个字符串代替给定的字符串。 
        /// 替换不区分大小写
        /// </summary>
        /// <param name="val"></param>
        /// <param name="stringToReplace">The string to replace</param>
        /// <param name="replacement">The string by which to be replaced</param>
        /// <returns>Copy of string with the string replaced</returns>
        public static string CaseInsenstiveReplace(this string val, string stringToReplace, string replacement)
        {
            Regex regEx = new Regex(stringToReplace, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regEx.Replace(val, replacement);
        }


        /// <summary>
        /// 删除字符串中出现的单词，匹配区分大小写。
        /// </summary>
        /// <param name="val"></param>
        /// <param name="filterWords">Array of words to be removed from the string</param>
        /// <returns>Copy of the string with the words removed</returns>
        public static string RemoveWords(this string val, params string[] filterWords)
        {
            return MaskWords(val, char.MinValue, filterWords);
        }

        /// <summary>
        /// 用给定字符掩码字符串中的单词的出现
        /// </summary>
        /// <param name="val"></param>
        /// <param name="mask">The character mask to apply</param>
        /// <param name="filterWords">The words to be replaced</param>
        /// <returns>The copy of string with the mask applied</returns>
        public static string MaskWords(this string val, char mask, params string[] filterWords)
        {
            string stringMask = mask == char.MinValue ?
               string.Empty : mask.ToString();
            string totalMask = stringMask;

            foreach (string s in filterWords)
            {
                Regex regEx = new Regex(s, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                if (stringMask.Length > 0)
                {
                    for (int i = 1; i < s.Length; i++)
                        totalMask += stringMask;
                }

                val = regEx.Replace(val, totalMask);
                totalMask = stringMask;
            }
            return val;
        }

        /// <summary>
        /// 在传递的字符总数中,将传递的字符串换行
        /// </summary>
        /// <param name="val"></param>
        /// <param name="charCount">The number of characters after which it should wrap the text</param>
        /// <returns>The copy of the string after applying the Wrap</returns>
        public static string WordWrap(this string val, int charCount)
        {
            return WordWrap(val, charCount, false, Environment.NewLine);
        }

        /// <summary>
        /// Wraps the passed string at the passed total number of characters (if cuttOff is true)
        /// or at the next whitespace (if cutOff is false). 
        /// Uses the environment new line symbol for the break text
        /// </summary>
        /// <param name="val"></param>
        /// <param name="charCount">The number of characters after which to break</param>
        /// <param name="cutOff">true to break at specific</param>
        /// <returns></returns>
        public static string WordWrap(this string val, int charCount, bool cutOff)
        {
            return WordWrap(val, charCount, cutOff, Environment.NewLine);
        }

        private static string WordWrap(this string val, int charCount, bool cutOff, string breakText)
        {
            StringBuilder sb = new StringBuilder(val.Length + 100);
            int counter = 0;

            if (cutOff)
            {
                while (counter < val.Length)
                {
                    if (val.Length > counter + charCount)
                    {
                        sb.Append(val.Substring(counter, charCount));
                        sb.Append(breakText);
                    }
                    else
                    {
                        sb.Append(val.Substring(counter));
                    }
                    counter += charCount;
                }
            }
            else
            {
                string[] strings = val.Split(' ');
                for (int i = 0; i < strings.Length; i++)
                {
                    // added one to represent the space.
                    counter += strings[i].Length + 1;
                    if (i != 0 && counter > charCount)
                    {
                        sb.Append(breakText);
                        counter = 0;
                    }

                    sb.Append(strings[i] + ' ');
                }
            }
            // to get rid of the extra space at the end.
            return sb.ToString().TrimEnd();
        }


        /// <summary>
        /// Converts an list of string to CSV string representation.
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="insertSpaces">True to add spaces after each comma</param>
        /// <returns>CSV representation of the data</returns>
        public static string ToCSV(this IEnumerable<string> val, bool insertSpaces)
        {
            if (insertSpaces)
                return String.Join(", ", val.ToArray());
            else
                return String.Join(",", val.ToArray());
        }
        /// <summary>
        /// Converts an list of characters to CSV string representation.
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="insertSpaces">True to add spaces after each comma</param>
        /// <returns>CSV representation of the data</returns>
        public static string ToCSV(this IEnumerable<char> val, bool insertSpaces)
        {
            List<string> casted = new List<string>();
            foreach (var item in val)
                casted.Add(item.ToString());

            if (insertSpaces)
                return String.Join(", ", casted.ToArray());
            else
                return String.Join(",", casted.ToArray());
        }
        /// <summary>
        /// Converts CSV to list of string.
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns>IEnumerable collection of string</returns>
        public static IEnumerable<string> ListFromCSV(this string val)
        {
            string[] split = val.Split(',');
            foreach (string item in split)
            {
                item.Trim();
            }
            return new List<string>(split);
        }

        /// <summary>
        /// 二进制序列化到一个文件
        /// </summary>
        /// <param name="val"></param>
        /// <param name="filePath">存储序列化数据的文件路径</param>
        public static void Serialize(this string val, string filePath)
        {
            try
            {
                Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, val);
                stream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
