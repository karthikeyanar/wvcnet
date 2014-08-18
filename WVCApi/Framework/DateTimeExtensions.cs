using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;


namespace System {
    public static class DateTimeExtensions {
        public static string ToNeutralShortDate(this DateTime source, string prefix = " on ") {
            return prefix + source.ToString("d MMM yyyy");
        }

        public static string ToNeutralShortDate(this DateTime? source, string prefix = " on ") {
            if (source.HasValue) {
                return source.Value.ToNeutralShortDate(prefix);
            }
            return string.Empty;
        }
    }
}