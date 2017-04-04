
//
// Juhta.NET, Copyright (c) 2017 Juha Lähteenmäki
//
// This source code may be used, modified and distributed under the terms of
// the MIT license. Please refer to the LICENSE.txt file for details.
//

using System;

namespace Juhta.Net.Extensions
{
    /// <summary>
    /// Defines a static class containing extension methods for the <see cref="DateTime"/> structure.
    /// </summary>
    public static class DateTimeExtensions
    {
        #region Public Methods

        /// <summary>
        /// Converts the value of the current DateTime instance to an equivalent 21-digit timestamp.
        /// </summary>
        /// <param name="dateTime">Specifies the current DateTime instance.</param>
        /// <returns>Returns the value of the current DateTime instance as a 21-digit timestamp.</returns>
        /// <remarks>Digit timestamps contain no separators between the timestamp parts. The number of digits
        /// expressing fractional seconds is 7.</remarks>
        public static string ToDigitTimestamp(this DateTime dateTime)
        {
            return(String.Format("{0:0000}{1:00}{2:00}{3:00}{4:00}{5:00}{6:0000000}", dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Ticks % 10000000));
        }

        /// <summary>
        /// Converts the value of the current DateTime instance to an equivalent timestamp string.
        /// </summary>
        /// <param name="dateTime">Specifies the current DateTime instance.</param>
        /// <returns>Returns the value of the current DateTime instance as a timestamp string.</returns>
        public static string ToTimestamp(this DateTime dateTime)
        {
            return(dateTime.ToTimestamp(' ', true, false));
        }

        /// <summary>
        /// Converts the value of the current DateTime instance to an equivalent timestamp string.
        /// </summary>
        /// <param name="dateTime">Specifies the current DateTime instance.</param>
        /// <param name="dateTimeSeparator">Specifies a character that is used to separate the date and time part in
        /// the timestamp.</param>
        /// <returns>Returns the value of the current DateTime instance as a timestamp string.</returns>
        public static string ToTimestamp(this DateTime dateTime, char dateTimeSeparator)
        {
            return(dateTime.ToTimestamp(dateTimeSeparator, true, false));
        }

        /// <summary>
        /// Converts the value of the current DateTime instance to an equivalent timestamp string.
        /// </summary>
        /// <param name="dateTime">Specifies the current DateTime instance.</param>
        /// <param name="dateTimeSeparator">Specifies a character that is used to separate the date and time part in
        /// the timestamp.</param>
        /// <param name="addFractionalSeconds">If true, adds fractional seconds to the timestamp.</param>
        /// <returns>Returns the value of the current DateTime instance as a timestamp string.</returns>
        public static string ToTimestamp(this DateTime dateTime, char dateTimeSeparator, bool addFractionalSeconds)
        {
            return(dateTime.ToTimestamp(dateTimeSeparator, addFractionalSeconds, false));
        }

        /// <summary>
        /// Converts the value of the current DateTime instance to an equivalent timestamp string.
        /// </summary>
        /// <param name="dateTime">Specifies the current DateTime instance.</param>
        /// <param name="dateTimeSeparator">Specifies a character that is used to separate the date and time part in
        /// the timestamp.</param>
        /// <param name="addFractionalSeconds">If true, adds fractional seconds to the timestamp.</param>
        /// <param name="addUtcOffset">If true, adds the offset to Coordinated Universal Time to the timestamp.</param>
        /// <returns>Returns the value of the current DateTime instance as a timestamp string.</returns>
        public static string ToTimestamp(this DateTime dateTime, char dateTimeSeparator, bool addFractionalSeconds, bool addUtcOffset)
        {
            string timestamp = String.Format("{0:0000}-{1:00}-{2:00}{3}{4:00}:{5:00}:{6:00}", dateTime.Year, dateTime.Month, dateTime.Day, dateTimeSeparator, dateTime.Hour, dateTime.Minute, dateTime.Second);
            TimeSpan utcOffset;

            if (addFractionalSeconds)
                timestamp += String.Format(".{0:0000000}", dateTime.Ticks % 10000000);

            if (addUtcOffset)
            {
                utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(dateTime);

                timestamp += String.Format("{0:+00;-00}:{1:00;00}", utcOffset.Hours, utcOffset.Minutes);
            }

            return(timestamp);
        }

        #endregion
    }
}
