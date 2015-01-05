using System;

namespace NowPlayingLib.Helpers
{
    internal static class TryParseHelper
    {
        public static Boolean ToBoolean(this string s)
        {
            Boolean value;
            return Boolean.TryParse(s, out value) ? value : default(Boolean);
        }

        public static Byte ToByte(this string s)
        {
            Byte value;
            return Byte.TryParse(s, out value) ? value : default(Byte);
        }

        public static SByte ToSByte(this string s)
        {
            SByte value;
            return SByte.TryParse(s, out value) ? value : default(SByte);
        }

        public static Char ToChar(this string s)
        {
            Char value;
            return Char.TryParse(s, out value) ? value : default(Char);
        }

        public static Decimal ToDecimal(this string s)
        {
            Decimal value;
            return Decimal.TryParse(s, out value) ? value : default(Decimal);
        }

        public static Int16 ToInt16(this string s)
        {
            Int16 value;
            return short.TryParse(s, out value) ? value : default(Int16);
        }

        public static UInt16 ToUInt16(this string s)
        {
            UInt16 value;
            return UInt16.TryParse(s, out value) ? value : default(UInt16);
        }

        public static Int32 ToInt32(this string s)
        {
            Int32 value;
            return Int32.TryParse(s, out value) ? value : default(Int32);
        }

        public static UInt32 ToUInt32(this string s)
        {
            UInt32 value;
            return UInt32.TryParse(s, out value) ? value : default(UInt32);
        }

        public static Int64 ToInt64(this string s)
        {
            Int64 value;
            return Int64.TryParse(s, out value) ? value : default(Int64);
        }

        public static UInt64 ToUInt64(this string s)
        {
            UInt64 value;
            return UInt64.TryParse(s, out value) ? value : default(UInt64);
        }

        public static Single ToSingle(this string s)
        {
            Single value;
            return Single.TryParse(s, out value) ? value : default(Single);
        }

        public static Double ToDouble(this string s)
        {
            Double value;
            return Double.TryParse(s, out value) ? value : default(Double);
        }

        public static DateTime ToDateTime(this string s)
        {
            DateTime value;
            return DateTime.TryParse(s, out value) ? value : default(DateTime);
        }

        public static DateTimeOffset ToDateTimeOffset(this string s)
        {
            DateTimeOffset value;
            return DateTimeOffset.TryParse(s, out value) ? value : default(DateTimeOffset);
        }

        public static TimeSpan ToTimeSpan(this string s)
        {
            TimeSpan value;
            return TimeSpan.TryParse(s, out value) ? value : default(TimeSpan);
        }
    }
}
