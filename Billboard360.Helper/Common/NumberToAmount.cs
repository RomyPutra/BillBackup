using System.Collections.Generic;
using System.Text;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Billboard360.Helper.Common
{
    public static class NumberToAmount
    {
        public static string ToRupiah(this int angka)
        {
            return String.Format(CultureInfo.CreateSpecificCulture("id-id"), "Rp. {0:N}", angka);
        }

        public static string ToRupiah(this string angka)
        {
            return String.Format(CultureInfo.CreateSpecificCulture("id-id"), "Rp. {0:N}", angka);
        }

        public static string ToRupiah(this double angka)
        {
            return String.Format(CultureInfo.CreateSpecificCulture("id-id"), "Rp. {0:N}", angka);
        }
    }
}
