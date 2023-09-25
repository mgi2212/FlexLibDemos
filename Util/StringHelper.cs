﻿// ****************************************************************************
///*!	\file StringHelper.cs
// *	\brief Helps handle strings in various localizations
// *
// *	\copyright	Copyright 2012-2017 FlexRadio Systems.  All Rights Reserved.
// *				Unauthorized use, duplication or distribution of this software is
// *				strictly prohibited by law.
// *
// *	\date 2013-06-18
// *	\author Eric Wachsmann, KE5DTO
// */
// ****************************************************************************

using System.Globalization;
using System.Text.RegularExpressions;

namespace Util
{
    public class StringHelper
    {
        #region Integer

        /// <summary>
        /// Converts a string into a uint taking Hex values into account if necessary
        /// </summary>
        /// <param name="s">The string to convert</param>
        /// <param name="result">The resulting number</param>
        /// <returns>Whether it was parsed correctly</returns>
        public static bool TryParseInteger(string s, out uint result)
        {
            // handle Hex values
            if (s.ToLower().StartsWith("0x"))
                return uint.TryParse(s.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result);
            else
                return uint.TryParse(s, out result);
        }

        /// <summary>
        /// Converts a string into a uint taking Hex values into account if necessary
        /// </summary>
        /// <param name="s">The string to convert</param>
        /// <param name="result">The resulting number</param>
        /// <returns>Whether it was parsed correctly</returns>
        public static bool TryParseInteger(string s, out int result)
        {
            // handle Hex values
            if (s.ToLower().StartsWith("0x"))
                return int.TryParse(s.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result);

            else return int.TryParse(s, out result);
        }

        #endregion

        #region Double

        public static bool TryParseDouble(string s, out double result)
        {
            return double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
        }

        public static string DoubleToString(double val)
        {
            return val.ToString(CultureInfo.InvariantCulture);
        }

        public static string DoubleToString(double val, string format)
        {
            return val.ToString(format, CultureInfo.InvariantCulture);
        }

        #endregion

        #region Float

        public static bool TryParseFloat(string s, out float result)
        {
            return float.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
        }

        public static string FloatToString(float val)
        {
            return val.ToString(CultureInfo.InvariantCulture);
        }

        public static string FloatToString(float val, string format)
        {
            return val.ToString(format, CultureInfo.InvariantCulture);
        }

        #endregion

        #region Misc

        public static string HandleNonPrintableChars(string data)
        {
            if (data == null) return null;

            string result = "";
            for (int i = 0; i < data.Length; i++)
            {
                byte b = (byte)data[i];
                if (b < 0x20 || b >= 0x7F)
                    result += ("<" + b.ToString("X2") + ">");
                else result += data[i];
            }

            return result;
        }        

        public static string Sanitize(string input)
        {
            // The PSoC font cannot handle some special characters on the front display.
            // This regex will allow special characters to show up in the client, but any special
            // characters will be taken out when in the display.

            // ^ matches characters that are NOT in the set
            // alphanumeric, periods, commas, forwards shashes, dash
            return Regex.Replace(input, @"[^a-zA-Z0-9\.,/-]", string.Empty);
        }

        public static string SanitizeInvalidRadioChars(string input)
        {
            return Regex.Replace(input, "[\\*#@!%^&.,;:?\")(+=`'~<>|\\[{}]+", string.Empty);
        }

        #endregion
    }
}
