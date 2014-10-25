using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WVC.Framework {
	public static class DataTypeHelper {

		public static int GetQuarter(this DateTime dateTime) {
			if (dateTime.Month <= 3)
				return 1;

			if (dateTime.Month <= 6)
				return 2;

			if (dateTime.Month <= 9)
				return 3;

			return 4;
		}

		public static float SafeDivision(float f1, float f2) {
			if (f2 == 0)
				//throw new System.DivideByZeroException();
				return 0;
			else
				return f1 / f2;
		}

		public static double SafeDivision(double d1, double d2) {
			if (d2 == 0)
				//throw new System.DivideByZeroException();
				return 0;
			else
				return d1 / d2;
		}

		public static decimal SafeDivision(decimal d1, decimal d2) {
			if (d2 == 0)
				//throw new System.DivideByZeroException();
				return 0;
			else
				return d1 / d2;
		}

		private static string RemoveSymbols(string value) {
			if (string.IsNullOrEmpty(value) == false) {
				value = value.Replace("$", "").Replace("%", "").Replace(",", "").Replace("(", "-").Replace(")", "");
			}
			return value;
		}

		public static float ToFloat(string value) {
			value = RemoveSymbols(value);
			float returnValue;
			float.TryParse(value, out returnValue);
			return returnValue;
		}

		public static decimal ToDecimal(string value) {
			value = RemoveSymbols(value);
			decimal returnValue;
			decimal.TryParse(value, out returnValue);
			return returnValue;
		}

		public static Int32 ToInt32(string value) {
			value = RemoveSymbols(value);
			int returnValue;
			Int32.TryParse(value, out returnValue);
			return returnValue;
		}

		public static Int16 ToInt16(string value) {
			value = RemoveSymbols(value);
			Int16 returnValue;
			Int16.TryParse(value, out returnValue);
			return returnValue;
		}

		public static DateTime ToDateTime(string value) {
			DateTime returnValue;
			DateTime.TryParse(value, out returnValue);
			return returnValue.Year <= 1900 ? new DateTime(1900, 1, 1) : returnValue;
		}

		public static DateTime ToFromOADate(string value) {
			DateTime returnValue;
			double dateValue = 0;
			double.TryParse(value, out dateValue);
			returnValue = DateTime.FromOADate(dateValue);
			return returnValue.Year <= 1900 ? new DateTime(1900, 1, 1) : returnValue;
		}

		public static bool CheckBoolean(string value) {
			bool calcValue = false;
			if (string.IsNullOrEmpty(value) == false)
				value = value.ToLower().Trim();
			else
				value = string.Empty;

			if (value.Contains("true"))
				calcValue = true;
			if (value.Contains("yes"))
				calcValue = true;
			if (value == "1")
				calcValue = true;

			return calcValue;
		}

	}
}
