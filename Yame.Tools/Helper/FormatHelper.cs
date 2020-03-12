using System;
using System.Collections.Generic;
using System.Text;

namespace Yame.Tools.Helper
{
    public static class FormatHelper
    {
        public static string DateFormatWithoutYear { get; } = "MM/dd";
        public static string DateFormat { get; } = "yyyy/MM/dd";
        public static string DateFormatWithoutSlash { get; } = "yyyyMMdd";
        public static string DateFormatForSmarteraspDb { get; } = "M_d_yyyy";
        public static string DateTimeFormat { get; } = "yyyy/MM/dd HH:mm";
        public static string DateTimeFileName { get; } = "yyyy_MM_dd_HH_mm_ss";
        public static string TimeFormat { get; } = "HH:mm";

    }
}
