using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AddressGenerator.Extension
{
    public static class StringExtension
    {
        public static double ExtracttLatitude(this string value)
        {
            var temp = value.Substring(value.IndexOf('@') + 1, 30).Split(',');

            return double.Parse(temp[0].Trim().Replace(".", ","));
        }

        public static double ExtracttLongitude(this string value)
        {
            var temp = value.Substring(value.IndexOf('@') + 1, 30).Split(',');

            return double.Parse(temp[1].Trim().Replace(".",","));
        }

        public static string CleanUrl(this string value)
        {
            Regex regEx = new Regex("\\+");
            return regEx.Replace(Uri.UnescapeDataString(value), " ");
        }

        public static string ExtractNeighborhood(this string value)
        {
            var temp = value.Split(',');

            return temp[1].Substring(temp[1].IndexOf('-') + 1).Trim();
        }

        public static string ExtractCity(this string value)
        {
            var temp = value.Split(',');

            if (temp.Length < 3)
                return "São Paulo";

            return temp[2].Substring(0, temp[2].IndexOf('-')).Trim();
        }

        public static string ExtractState(this string value)
        {
            var temp = value.Split(',');

            if (temp.Length < 3)
                return "SP";

            return temp[2].Substring(temp[2].IndexOf('-')+ 1, 3).Trim();
        }
    }
}