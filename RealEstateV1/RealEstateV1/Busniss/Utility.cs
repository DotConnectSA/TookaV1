using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Web;

namespace RealEstateV1.Busniss
{
    public static partial class Utility
    {
        public static Object __lock = new Object();
        public static void CompressFile(String filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            using (FileStream inFile = fi.OpenRead())
            {
                if ((File.GetAttributes(fi.FullName)
                    & FileAttributes.Hidden)
                    != FileAttributes.Hidden & fi.Extension != ".gz")
                {
                    using (FileStream outFile =
                                File.Create(fi.FullName + ".gz"))
                    {
                        using (GZipStream Compress =
                            new GZipStream(outFile,
                            CompressionMode.Compress))
                        {
                            inFile.CopyTo(Compress);
                        }
                    }
                }
            }
        }

        public static void DecompressFile(String filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            using (FileStream inFile = fi.OpenRead())
            {
                string curFile = fi.FullName;
                string origName = curFile.Remove(curFile.Length -
                        fi.Extension.Length);
                using (FileStream outFile = File.Create(origName))
                {
                    using (GZipStream Decompress = new GZipStream(inFile,
                            CompressionMode.Decompress))
                    {
                        Decompress.CopyTo(outFile);
                    }
                }
            }
        }

        public static Byte[] Compress(Byte[] uncompressed)
        {
            try
            {
                if (uncompressed == null)
                    throw new ArgumentNullException("uncompressed",
                                                    "The given array is null!");
                if (uncompressed.LongLength > (long)int.MaxValue)
                    throw new ArgumentException("The given array is to large!");

                using (MemoryStream ms = new MemoryStream())
                using (GZipStream gzs = new GZipStream(ms, CompressionMode.Compress))
                {
                    // Write the data to the stream to compress it 
                    gzs.Write(uncompressed, 0, uncompressed.Length);
                    gzs.Close();
                    // Get the compressed byte array back 
                    return ms.ToArray();
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static Byte[] Decompress(Byte[] compressed)
        {
            try
            {
                if (compressed == null)
                    throw new ArgumentNullException("compressed",
                                                    "Data to decompress cant be null!");
                using (MemoryStream ms = new MemoryStream(compressed))
                using (GZipStream gzs = new GZipStream(ms, CompressionMode.Decompress))
                {
                    MemoryStream msOut = new MemoryStream();
                    byte[] buff = new byte[8];
                    for (; ; )
                    {
                        int read = gzs.Read(buff, 0, buff.Length);
                        if (read == 0) break;
                        msOut.Write(buff, 0, read);
                    }
                    return msOut.ToArray();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static Int32 ConvertToInt32(Object value)
        {
            Int32 result = 0;
            if (value != null)
            {
                Int32.TryParse(value.ToString(), out result);
            }
            return result;

        }
        public static Boolean ConvertToBoolean(Object value)
        {
            Boolean result = false;
            if (value != null)
            {
                Boolean.TryParse(value.ToString(), out result);
            }
            return result;
        }
        public static double ConvertToDouble(Object value)
        {
            double result = 0;
            if (value != null)
            {
                double.TryParse(value.ToString(), out result);
            }
            return result;

        }

        public static long ConvertToLong(Object value)
        {
            long result = 0;
            if (value != null)
            {
                long.TryParse(value.ToString(), out result);
            }
            return result;

        }



        public static int? ConvertToNullableInt32(Object value)
        {
            Int32? result = null;
            if (value != null)
            {
                int val = 0;
                if (Int32.TryParse(value.ToString(), out val))
                {
                    result = val;
                }
            }
            return result;

        }

        public static Color ConvertColorFromString(String value)
        {
            ColorConverter colorConvert = new ColorConverter();
            return (Color)colorConvert.ConvertFromString(value);
        }

        public static DateTime? ConvertToNullableDateTime(Object value)
        {
            DateTime result = DateTime.Today;
            if (value == null)
            {
                return null;
            }
            DateTime.TryParse(value.ToString(), out result);
            return result;
        }

        public static Decimal ConvertToDecimal(Object value)
        {
            Decimal result = 0;
            if (value != null)
            {
                Decimal.TryParse(value.ToString(), out result);
            }
            return result;
        }

        public static DateTime ConvertToDateTime(Object value)
        {
            DateTime result = DateTime.MinValue;
            if (value != null)
            {
                DateTime.TryParse(value.ToString(), out result);
            }
            return result;
        }

        public static String ConvertToString(Object value)
        {
            String result = String.Empty;
            if (value != null)
            {
                result = value.ToString();
            }
            return result;
        }

        public static Byte[] ConvertToBuffer(Object value)
        {
            if (value != null)
            {
                return value as Byte[];
            }
            return null;
        }

        public static String ConvertToUrlString(Object value)
        {
            String result = String.Empty;
            if (value != null)
            {
                result = value.ToString();
                if (!String.IsNullOrEmpty(result))
                {
                    result = HttpContext.Current.Server.MapPath(result);
                }
            }
            return result;
        }




        public static DataRow ConvertObjectToDataRow(Object value, DataTable targetTable)
        {
            if (targetTable != null && value != null)
            {
                DataRow row = targetTable.NewRow();
                DataColumn targetColumn = null;
                if (value is DataRowView)
                {
                    foreach (DataColumn col in targetTable.Columns)
                    {
                        try
                        {
                            row[col.ColumnName] = (value as DataRowView).Row[col.ColumnName];
                        }
                        catch
                        {
                            row[col.ColumnName] = (value as DataRowView).Row[col.Caption];
                        }
                    }
                }
                else if (value is DbDataRecord)
                {
                    DbDataRecord record = (value as DbDataRecord);
                    for (Int32 index = 0; index < record.FieldCount; index++)
                    {
                        try
                        {
                            row[record.GetName(index)] = record.GetValue(index);
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    foreach (PropertyInfo property in value.GetType().GetProperties())
                    {
                        targetColumn = targetTable.Columns.Cast<DataColumn>().FirstOrDefault(col => col.ColumnName == property.Name);
                        if (targetColumn != null)
                        {
                            row[targetColumn.ColumnName] = property.GetValue(value, null);
                        }
                    }
                }
                return row;
            }
            return null;
        }
        static public string GetDateInHijri(DateTime dt)
        {
            string tempDate = string.Empty;
            CultureInfo Culture = new CultureInfo("en-US");
            Culture.DateTimeFormat.Calendar = new GregorianCalendar();

            CultureInfo Culture1 = new CultureInfo("ar-SA");
            Culture1.DateTimeFormat.Calendar = new HijriCalendar();

            DateTime dateTime = new DateTime();

            if (DateTime.TryParseExact(dt.ToString("dd-MM-yyyy"), "dd-MM-yyyy", Culture1.DateTimeFormat, DateTimeStyles.None, out dateTime))
            {
                tempDate = dateTime.ToString("yyyy-MM-dd", Culture.DateTimeFormat);

            }


            return tempDate;
        }
        public static string GetDateInHijriYear(string DATE_INS)
        {
            string ConvertDateString = "";
            Int32 CurrentDay;
            Int32 CurrentMonth;
            Int32 CurrentYear;
            DateTime DateFrom;
            //
            DateFrom = Convert.ToDateTime(DATE_INS.Trim());
            CurrentDay = DateFrom.Day;
            CurrentMonth = DateFrom.Month;
            CurrentYear = DateFrom.Year;

            CultureInfo higri_format = new CultureInfo("ar-SA");
            higri_format.DateTimeFormat.Calendar = new HijriCalendar();
            DateTime CurrentDate;
            DateTime x;
            if (!DateTime.TryParse(DATE_INS, out x))
            {
                ConvertDateString = "";
                return "";
            }
            CurrentDate = Convert.ToDateTime(DATE_INS);
            ConvertDateString = CurrentDate.Date.ToString("yyyy", higri_format);
            return ConvertDateString;
        }
        public static string GetDateInHijriNew(string DATE_INS)
        {
            string ConvertDateString = "";
            Int32 CurrentDay;
            Int32 CurrentMonth;
            Int32 CurrentYear;
            DateTime DateFrom;
            //
            DateFrom = Convert.ToDateTime(DATE_INS.Trim());
            CurrentDay = DateFrom.Day;
            CurrentMonth = DateFrom.Month;
            CurrentYear = DateFrom.Year;

            CultureInfo higri_format = new CultureInfo("ar-SA");
            higri_format.DateTimeFormat.Calendar = new HijriCalendar();
            DateTime CurrentDate;
            DateTime x;
            if (!DateTime.TryParse(DATE_INS, out x))
            {
                ConvertDateString = "";
                return "";
            }
            CurrentDate = Convert.ToDateTime(DATE_INS);
            ConvertDateString = CurrentDate.Date.ToString("yyyy-MM-dd", higri_format);
            return ConvertDateString;
        }
        static public void LogException(Exception xpn)
        {

        }
        public static DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable dt = new DataTable();

            foreach (System.Reflection.PropertyInfo info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
            }
            foreach (T t in list)
            {
                DataRow row = dt.NewRow();
                foreach (System.Reflection.PropertyInfo info in typeof(T).GetProperties())
                {
                    row[info.Name] = info.GetValue(t, null);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        public static String IntToWords(long number)
        {
            String words = "";
            lock (__lock)
            {
                if (number == 0)
                    return "صفر";

                if (number < 0)
                    return "سالب " + IntToWords(Math.Abs(number));



                if ((number / 1000000000000000) > 0)
                {
                    words += IntToWords(number / 1000000000000000) + " QUADRILLION ";
                    number %= 1000000000000000;
                }

                if ((number / 1000000000000) > 0)
                {
                    words += IntToWords(number / 1000000000000) + " TRILLION ";
                    number %= 1000000000000;
                }

                if ((number / 1000000000) > 0)
                {
                    words += IntToWords(number / 1000000000) + " BILLION ";
                    number %= 1000000000;
                }

                if ((number / 1000000) > 0)
                {
                    words += IntToWords(number / 1000000) + " MILLION ";
                    number %= 1000000;
                }

                if ((number / 1000) > 0)
                {
                    words += IntToWords(number / 1000) + " THOUSAND ";
                    number %= 1000;
                }

                if ((number / 100) > 0)
                {
                    words += IntToWords(number / 100) + " HUNDRED ";
                    number %= 100;
                }

                if (number > 0)
                {
                    if (words != String.Empty)
                        words += "و ";

                    var unitsMap = new[] { "صفر", "واحد", "إثنان", "ثلاثة", "أربعة", "خمسة", "ستة", "سبعة", "ثمانية", "تسعة", "عشرة", "أحد عشر", "أثنا عشر", "ثلاثة عشر", "أربعة عشر", "خمسة عشر", "ستة عشر", "سبعة عشر", "ثمانية عشر", "تسعة عشر" };
                    var unitsMap1 = new[] { "صفر", "واحد", "إثنان", "ثلاث", "أربع", "خمس", "ست", "سبع", "ثمان", "تسع" };
                    var tensMap = new[] { "صفر", "عشرة", "عشرون", "ثلاثون", "أربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون" };

                    if (number < 20)
                        words += unitsMap[number];
                    else
                    {
                        words += tensMap[number / 10];
                        if ((number % 10) > 0)
                            words = String.Format("{0} و {1}", unitsMap1[number % 10], words);
                    }
                }
            }

            return words.Trim();
        }

        public static String IntToWords(Decimal number)
        {
            String words = "";
            lock (__lock)
            {
                if (number == 0)
                    return "صفر";

                if (number < 0)
                    return "سالب " + IntToWords(Math.Abs(number));



                if ((number / 1000000000000000) > 0)
                {
                    words += IntToWords(number / 1000000000000000) + " QUADRILLION ";
                    number %= 1000000000000000;
                }

                if ((number / 1000000000000) > 0)
                {
                    words += IntToWords(number / 1000000000000) + " TRILLION ";
                    number %= 1000000000000;
                }

                if ((number / 1000000000) > 0)
                {
                    words += IntToWords(number / 1000000000) + " BILLION ";
                    number %= 1000000000;
                }

                if ((number / 1000000) > 0)
                {
                    words += IntToWords(number / 1000000) + " MILLION ";
                    number %= 1000000;
                }

                if ((number / 1000) > 0)
                {
                    words += IntToWords(number / 1000) + " THOUSAND ";
                    number %= 1000;
                }

                if ((number / 100) > 0)
                {
                    words += IntToWords(number / 100) + " HUNDRED ";
                    number %= 100;
                }

                if (number > 0)
                {
                    if (words != String.Empty)
                        words += "و ";

                    var unitsMap = new[] { "صفر", "واحد", "إثنان", "ثلاثة", "أربعة", "خمسة", "ستة", "سبعة", "ثمانية", "تسعة", "عشرة", "أحد عشر", "أثنا عشر", "ثلاثة عشر", "أربعة عشر", "خمسة عشر", "ستة عشر", "سبعة عشر", "ثمانية عشر", "تسعة عشر" };
                    var unitsMap1 = new[] { "صفر", "واحد", "إثنان", "ثلاث", "أربع", "خمس", "ست", "سبع", "ثمان", "تسع" };
                    var tensMap = new[] { "صفر", "عشرة", "عشرون", "ثلاثون", "أربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون" };

                    if (number < 20)
                        words += unitsMap[(Int32)number];
                    else
                    {
                        words += tensMap[(Int32)number / 10];
                        if ((number % 10) > 0)
                            words = String.Format("{0} و {1}", unitsMap1[(Int32)number % 10], words);
                    }
                }
            }
            return words.Trim();
        }

        public static String IntToWords(Double number)
        {
            String words = "";
            lock (__lock)
            {
                if (number == 0)
                    return "صفر";

                if (number < 0)
                    return "سالب " + IntToWords(Math.Abs(number));

                if (number > 0)
                {
                    if (words != String.Empty)
                        words += "و ";

                    var unitsMap = new[] { "صفر", "واحد", "إثنان", "ثلاثة", "أربعة", "خمسة", "ستة", "سبعة", "ثمانية", "تسعة", "عشرة", "أحد عشر", "أثنا عشر", "ثلاثة عشر", "أربعة عشر", "خمسة عشر", "ستة عشر", "سبعة عشر", "ثمانية عشر", "تسعة عشر" };
                    var unitsMap1 = new[] { "صفر", "واحد", "إثنان", "ثلاث", "أربع", "خمس", "ست", "سبع", "ثمان", "تسع" };
                    var tensMap = new[] { "صفر", "عشرة", "عشرون", "ثلاثون", "أربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون" };

                    if (number < 20)
                        words += unitsMap[(Int32)number];
                    else
                    {
                        words += tensMap[(Int32)number / 10];
                        if ((number % 10) > 0)
                            words = String.Format("{0} و {1}", unitsMap1[(Int32)number % 10], words);
                    }
                    double decPart = 0;

                    string[] splitter = number.ToString().Contains(".") ? number.ToString().Split('.') : null;
                    if (splitter != null)
                        decPart = double.Parse(splitter[1]);

                    if (decPart > 0)
                    {
                        if (words != "")
                            words += " و ";

                        if (decPart == 25)
                            words += " ربع";
                        else if (decPart == 5)
                            words += " نصف";
                        else if (decPart == 75)
                            words += " ثلاثة أرباع";
                        else
                        {
                            int counter = decPart.ToString().Length;
                            switch (counter)
                            {
                                case 1: words += IntToWords(decPart) + " أعشار"; break;
                                case 2: words += IntToWords(decPart) + " عشراً"; break;
                            }
                        }
                    }
                }
            }
            return words.Trim();
        }

        public static String NumWordsWrapper(double n)
        {
            string words = "";
            double intPart;
            double decPart = 0;
            if (n == 0)
                return "صفر";
            try
            {
                string[] splitter = n.ToString().Split('.');
                intPart = double.Parse(splitter[0]);
                decPart = double.Parse(splitter[1]);
            }
            catch
            {
                intPart = n;
            }

            words = NumWords(intPart);

            if (decPart > 0)
            {
                if (words != "")
                    words += " و ";
                int counter = decPart.ToString().Length;
                switch (counter)
                {
                    case 1: words += NumWords(decPart) + " أعشار"; break;
                    case 2: words += NumWords(decPart) + " مئات"; break;
                    case 3: words += NumWords(decPart) + " ألوف"; break;
                    case 4: words += NumWords(decPart) + " ten-thousandths"; break;
                    case 5: words += NumWords(decPart) + " hundred-thousandths"; break;
                    case 6: words += NumWords(decPart) + " millionths"; break;
                    case 7: words += NumWords(decPart) + " ten-millionths"; break;
                }
            }
            return words;
        }

        static String NumWords(double n) //converts double to words
        {
            string[] numbersArr = new string[] { "واحد", "إثنان", "ثلاثة", "أربعة", "خمسة", "ستة", "سبعة", "ثمانية", "تسعة", "عشرة", "أحد عشر", "أثنا عشر", "ثلاثة عشر", "أربعة عشر", "خمسة عشر", "ستة عشر", "سبعة عشر", "ثمانية عشر", "تسعة عشر" };
            string[] tensArr = new string[] { "عشرون", "ثلاثون", "أربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون" };
            string[] suffixesArr = new string[] { "ألف", "million", "billion", "trillion", "quadrillion", "quintillion", "sextillion", "septillion", "octillion", "nonillion", "decillion", "undecillion", "duodecillion", "tredecillion", "Quattuordecillion", "Quindecillion", "Sexdecillion", "Septdecillion", "Octodecillion", "Novemdecillion", "Vigintillion" };
            string words = "";

            bool tens = false;

            if (n < 0)
            {
                words += "سالب ";
                n *= -1;
            }

            int power = (suffixesArr.Length + 1) * 3;

            while (power > 3)
            {
                double pow = Math.Pow(10, power);
                if (n > pow)
                {
                    if (n % Math.Pow(10, power) > 0)
                    {
                        words += NumWords(Math.Floor(n / pow)) + " " + suffixesArr[(power / 3) - 1] + ", ";
                    }
                    else if (n % pow > 0)
                    {
                        words += NumWords(Math.Floor(n / pow)) + " " + suffixesArr[(power / 3) - 1];
                    }
                    n %= pow;
                }
                power -= 3;
            }
            if (n >= 1000)
            {
                if (n % 1000 > 0) words += NumWords(Math.Floor(n / 1000)) + " ألف, ";
                else words += NumWords(Math.Floor(n / 1000)) + " ألف";
                n %= 1000;
            }
            if (0 <= n && n <= 999)
            {
                if ((int)n / 100 > 0)
                {
                    words += NumWords(Math.Floor(n / 100)) + " مائة";
                    n %= 100;
                }
                if ((int)n / 10 > 1)
                {
                    if (words != "")
                        words += " ";
                    words += tensArr[(int)n / 10 - 2];
                    tens = true;
                    n %= 10;
                }

                if (n < 20 && n > 0)
                {
                    if (words != "" && tens == false)
                        words += " ";
                    words += (tens ? " و " + numbersArr[(int)n - 1] : numbersArr[(int)n - 1]);
                    n -= Math.Floor(n);
                }
            }

            return words;

        }


        public static List<T> GetRandomListFromList<T>(List<T> List, int CountRaturn)
        {
            List<T> randomizedList = List.OrderBy(x => Guid.NewGuid()).Take(CountRaturn).ToList();
            return randomizedList;
        }

        public static List<T> SortListRandomly<T>(List<T> List)
        {
            List<T> randomizedList = List.OrderBy(x => Guid.NewGuid()).ToList();
            return randomizedList;
        }



        public static class PresentationHelper
        {


            public static Byte[] ConvertImageToBinary(String imagePath)
            {
                Byte[] buffer = null;
                try
                {
                    FileInfo info = new FileInfo(imagePath);
                    using (BinaryReader reader = new BinaryReader(File.OpenRead(imagePath)))
                    {
                        buffer = reader.ReadBytes((Int32)info.Length);
                        reader.Close();
                    }
                }
                catch (Exception)
                {
                }
                return buffer;
            }




        }
    }
}