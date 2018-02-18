using Persia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yeast.Utilities.Helpers
{
	public static class PersianHelper
	{
		#region DateAndTime
		public static DateTime GetDateTime()
		{
			return DateTime.Now;
		}

		public static string ConvertToPersian(DateTime dateTime, string mod = "")
		{
			SolarDate solar = Calendar.ConvertToPersian(dateTime);
			return string.IsNullOrEmpty(mod) ? solar.ToString() : solar.ToString(mod);
		}

		public static string ConvertToPersian()
		{
			return PersianHelper.ConvertToPersian(DateTime.Now);
		}

		public static string ToPersianDate(this DateTime datetime, string mod = "")
		{
			return PersianHelper.ConvertToPersian(datetime, mod);
		}

		public static string ConvertToIslamic(DateTime dateTime, string mod = "")
		{
			LunarDate lunar = Calendar.ConvertToIslamic(dateTime);
			return string.IsNullOrEmpty(mod) ? lunar.ToString() : lunar.ToString(mod);
		}

		public static string ToIslamicDate(this DateTime datetime, string mod = "")
		{
			return PersianHelper.ConvertToIslamic(datetime, mod);
		}

		public static DateTime ConvertToGregorian(int year, int month, int day, int hour, int minute, int second, DateType dateType)
		{
			DateTime datetime = Calendar.ConvertToGregorian(year, month, day, hour, minute, second, dateType);
			return datetime;
		}

		public static DateTime ConvertToGregorian(int year, int month, int day, DateType dateType = DateType.Persian)
		{
			return PersianHelper.ConvertToGregorian(year, month, day,0 ,0,0, dateType);
		}
		#endregion

		#region PersianNumber
		public static string ConvertToPersianString(object digit)
		{
			return PersianWord.ToPersianString(digit);
		}
		public static string ConvertToPersianString(this int digit)
		{
			return PersianWord.ToPersianString(digit);
		}
		public static string ConvertToPersianString(this decimal digit)
		{
			return PersianWord.ToPersianString(digit);
		}

		public static string ConvertToPersianString(this string digit)
		{
			return PersianWord.ToPersianString(digit);
		}
		#endregion

		#region LatinNumber
		public static string ConvertToLatinNumberString(object digit)
		{
			return PersianWord.ConvertToLatinNumber(digit.ToString());
		}
		public static string ConvertToLatinNumberString(this int digit)
		{
			return PersianWord.ConvertToLatinNumber(digit.ToString());
		}
		public static string ConvertToLatinNumberString(this decimal digit)
		{
			return PersianWord.ConvertToLatinNumber(digit.ToString());
		}

		public static string ConvertToLatinNumberString(this string digit)
		{
			return PersianWord.ConvertToLatinNumber(digit);
		}
		#endregion

		#region Ye Ke
		public static string ApplyCorrectYeKe(string input)
		{
			var newVal = input.ToString().Replace("ی", "ی").Replace("ک", "ک");
			return newVal;
		}
		public static string ApplyCorrectYeKe1(this string input)
		{
			return PersianHelper.ApplyCorrectYeKe(input);
		}
		#endregion

	}
}
