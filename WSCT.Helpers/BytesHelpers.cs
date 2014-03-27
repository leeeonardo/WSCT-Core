using System;
using System.Globalization;
using System.Text;

namespace WSCT.Helpers
{
    /// <summary>
    /// Helper class allowing simple manipulation and conversion between <see cref="byte"/>[] and <see cref="string"/> types data.
    /// </summary>
    public static class BytesHelpers
    {
        /// <summary>
        /// Default separator to be used for <c>toHexa(...)</c> methods.
        /// </summary>
        public static Char DefaultSeparator = ' ';

        /// <summary>
        /// Converts a <c>UInt32</c> into a <c>byte[]</c>.
        /// </summary>
        /// <param name="value">Source data to convert.</param>
        /// <returns>A new <c>byte[]</c>.</returns>
        /// <example>
        ///     <code>
        ///     byte value = 0x12;
        ///     byte[] data = value.toByteArray();
        ///     // now data = { 0x12 }
        ///     </code>
        /// </example>
        public static byte[] ToByteArray(this byte value)
        {
            var byteArray = new byte[1];

            byteArray[0] = value;

            return byteArray;
        }

        /// <summary>
        /// Converts a <c>UInt32</c> into a <c>byte[]</c>, which length is given.
        /// </summary>
        /// <param name="value">Source data to convert.</param>
        /// <param name="length">Length of the array to create.</param>
        /// <returns>A new <c>byte[]</c></returns>
        /// <example>
        ///     <code>
        ///     UInt32 value = 0x12345678;
        ///     byte[] data = value.toByteArray(4);
        ///     // now data = { 0x12, 0x34, 0x56, 0x78}
        ///     </code>
        ///     <code>
        ///     UInt32 value = 0x12;
        ///     byte[] data = value.toByteArray(2);
        ///     // now data = { 0x00, 0x12 }
        ///     </code>
        /// </example>
        public static byte[] ToByteArray(this UInt32 value, int length)
        {
            var byteArray = new byte[length];

            for (var i = length - 1; i >= 0; i--)
            {
                byteArray[i] = (byte)(value%0x100);
                value /= 0x100;
            }

            return byteArray;
        }

        /// <summary>
        /// Converts a String representing hexadecimal values into a <c>byte[]</c>.
        /// </summary>
        /// <param name="hexa">Source data to convert.</param>
        /// <returns>A new <c>byte[]</c>.</returns>
        /// <example>
        ///     <code>
        ///     string str = "12 34 56";
        ///     byte[] data = str.fromHexa();
        ///     // Now data = { 0x12, 0x34, 0x56 }
        ///     </code>
        ///     Example with odd number of digits
        ///     <code>
        ///     string str = "23456";
        ///     byte[] data = str.fromHexa();
        ///     // Now data = { 0x02, 0x34, 0x56 }
        ///     </code>
        /// </example>
        public static byte[] FromHexa(this String hexa)
        {
            byte[] bytes;

            hexa = hexa.Replace(" ", "");
            hexa = hexa.Replace("-", "");

            if (hexa.Length%2 == 0)
            {
                bytes = new byte[hexa.Length/2];
                for (var index = 0; index < hexa.Length/2; index++)
                {
                    bytes[index] = byte.Parse(hexa.Substring(2*index, 2), NumberStyles.HexNumber);
                }
            }
            else
            {
                bytes = new byte[hexa.Length/2 + 1];
                bytes[0] = byte.Parse(hexa.Substring(0, 1), NumberStyles.HexNumber);
                for (var index = 1; index < hexa.Length/2 + 1; index++)
                {
                    bytes[index] = byte.Parse(hexa.Substring(2*index - 1, 2), NumberStyles.HexNumber);
                }
            }

            return bytes;
        }

        /// <summary>
        /// Converts a String into a <c>byte[]</c>.
        /// </summary>
        /// <param name="buffer">Source data to convert.</param>
        /// <returns>A new <c>byte[]</c> where each character of <paramref name="buffer"/> is encoded as its ascii value.</returns>
        /// <example>
        ///     <code>
        ///     string str = "123";
        ///     byte[] data = str.ToAsciiString();
        ///     // Now data = { 0x31, 0x32, 0x33 }
        ///     </code>
        /// </example>
        public static byte[] FromString(this String buffer)
        {
            var byteBuffer = new byte[buffer.Length];

            for (var i = 0; i < buffer.Length; i++)
            {
                byteBuffer[i] = Convert.ToByte(buffer[i]);
            }

            return byteBuffer;
        }

        /// <summary>
        /// Converts an array of bytes to the equivalent String in Hexa.
        /// </summary>
        /// <param name="buffer">Source data to convert.</param>
        /// <returns>A String representation of the array where each byte is represented as its hexadecimal value.</returns>
        /// <example>
        ///     <code>
        ///     byte[] value = new byte[] { 0x31, 0x32, 0x33 };
        ///     string data = value.toHexa();
        ///     // Now data = "31 32 33"
        ///     </code>
        /// </example>
        public static String ToHexa(this byte[] buffer)
        {
            return (buffer == null ? "" : buffer.ToHexa(buffer.Length, DefaultSeparator));
        }

        /// <summary>
        /// Converts an array of bytes to the equivalent String in Hexa.
        /// </summary>
        /// <param name="buffer">Source data to convert.</param>
        /// <param name="separator">Separator to be used between each group of 2 hexadecimal digits.</param>
        /// <returns>A String representation of the array where each byte is represented as its hexadecimal value. <c>0</c> means no separator.</returns>
        /// <example>
        ///     <code>
        ///     byte[] value = new byte[] { 0x31, 0x32, 0x33 };
        ///     string data = value.toHexa('-');
        ///     // Now data = "31-32-33"
        ///     </code>
        /// </example>
        public static String ToHexa(this byte[] buffer, Char separator)
        {
            return (buffer == null ? "" : buffer.ToHexa(buffer.Length, separator));
        }

        /// <summary>
        /// Converts first bytes of a byte Array to the equivalent String in Hexa.
        /// </summary>
        /// <param name="buffer">Source data to convert.</param>
        /// <param name="size">Maximum number of bytes in the array to convert.</param>
        /// <returns>A String representation of the array where each byte is represented as its hexadecimal value.</returns>
        /// <example>
        ///     <code>
        ///     byte[] value = new byte[] { 0x31, 0x32, 0x33 };
        ///     string data = value.toHexa(2);
        ///     // Now data = "31 32"
        ///     </code>
        /// </example>
        public static String ToHexa(this byte[] buffer, int size)
        {
            return (buffer == null ? "" : buffer.ToHexa(size, DefaultSeparator));
        }

        /// <summary>
        /// Converts first bytes of a byte Array to the equivalent String in Hexa.
        /// </summary>
        /// <param name="buffer">Source data to convert.</param>
        /// <param name="size">Maximum number of bytes in the array to convert.</param>
        /// <param name="separator">Separator to be used between each group of 2 hexadecimal digits.</param>
        /// <returns>A String representation of the array where each byte is represented as its hexadecimal value.</returns>
        /// <example>
        ///     <code>
        ///     byte[] value = new byte[] { 0x31, 0x32, 0x33 };
        ///     string data = value.toHexa(2, '-');
        ///     // Now data = "31-32"
        ///     </code>
        /// </example>
        public static String ToHexa(this byte[] buffer, int size, Char separator)
        {
            var s = new StringBuilder();

            if (size > buffer.Length)
            {
                size = buffer.Length;
            }

            if (size > 0)
            {
                s.AppendFormat("{0:X2}", buffer[0]);
            }

            if (separator == 0)
            {
                for (var i = 1; i < size; i++)
                {
                    s.AppendFormat("{0:X2}", buffer[i]);
                }
            }
            else
            {
                for (var i = 1; i < size; i++)
                {
                    s.AppendFormat("{1}{0:X2}", buffer[i], separator);
                }
            }

            return s.ToString();
        }

        /// <summary>
        /// Converts a <c>byte[]</c> to the equivalent String (byte > char).
        /// </summary>
        /// <param name="buffer">Source data to convert.</param>
        /// <returns>A String representation of the array where each byte is represented as a character (global default encoding).</returns>
        /// <example>
        ///     <code>
        ///     byte[] value = new byte[] { 0x31, 0x32, 0x33 };
        ///     string data = value.ToAsciiString();
        ///     // Now data = "123"
        ///     </code>
        /// </example>
        public static String ToAsciiString(this byte[] buffer)
        {
            if (buffer == null)
            {
                return String.Empty;
            }

            var s = new StringBuilder();

            foreach (var b in buffer)
            {
                s.Append(Convert.ToChar(b));
            }

            return s.ToString();
        }

        /// <summary>
        /// Converts an array of bytes to an array of bytes "BCD coded".
        /// </summary>
        /// <param name="bytes">Source array of bytes.</param>
        /// <returns>A new array of bytes "BCD coded".</returns>
        /// <example>
        ///     <code>
        ///     byte[] value = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
        ///     byte[] data = value.toBcd();
        ///     // Now data = { 0x12, 0x34, 0x56, 0x78, 0x90 }
        ///     </code>
        /// </example>
        public static byte[] ToBcd(this byte[] bytes)
        {
            return bytes.ToBcd(0x0);
        }

        /// <summary>
        /// Converts an array of bytes to an array of bytes "BCD coded".
        /// </summary>
        /// <param name="bytes">Source array of bytes.</param>
        /// <param name="filler">Value to be used a a filler is length is odd.</param>
        /// <returns>A new array of bytes "BCD coded".</returns>
        /// <example>
        ///     <code>
        ///     byte[] value = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        ///     byte[] data = value.toBcd(0xF);
        ///     // Now data = { 0x12, 0x34, 0x56, 0x78, 0x9F }
        ///     </code>
        /// </example>
        public static byte[] ToBcd(this byte[] bytes, byte filler)
        {
            var bcd = new byte[bytes.Length/2 + bytes.Length%2];

            int index;
            for (index = 0; index + 1 < bytes.Length; index += 2)
            {
                bcd[index/2] = (byte)((bytes[index] << 4) + bytes[index + 1]);
            }

            if (bytes.Length%2 == 1)
            {
                bcd[index/2] = (byte)((bytes[index] << 4) + filler);
            }

            return bcd;
        }

        /// <summary>
        /// Converts an array of bytes to a string representation "BCD coded"
        /// </summary>
        /// <param name="bytes">Source array of bytes</param>
        /// <param name="filler">Value to be used a a filler is length is odd</param>
        /// <returns>A new array of bytes "BCD coded"</returns>
        /// <example>
        ///     <code>
        ///     byte[] value = new byte[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        ///     string data = value.toBcd('F');
        ///     // Now data = "123456789F"
        ///     </code>
        /// </example>
        public static String ToBcdString(this byte[] bytes, Char filler)
        {
            var bcd = new StringBuilder(bytes.Length);

            foreach (var b in bytes)
            {
                bcd.Append(b);
            }

            if (bytes.Length%2 == 1)
            {
                bcd.Append(filler);
            }

            return bcd.ToString();
        }

        /// <summary>
        /// Converts an array of bytes "BCD coded" to an array of bytes where each byte is a digit
        /// </summary>
        /// <param name="bcd">Source BCD coded array of bytes</param>
        /// <returns>A new array of bytes where each byte is a digit</returns>
        /// <example>
        ///     <code>
        ///     byte[] source = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90 };
        ///     byte[] data = source.fromBcd(9);
        ///     // Now data = { 1, 2, 3, 4, 5, 6, 7, 8, 9 }
        ///     </code>
        /// </example>
        public static byte[] FromBcd(this byte[] bcd)
        {
            return bcd.FromBcd((UInt32)bcd.Length*2);
        }

        /// <summary>
        /// Converts an array of bytes "BCD coded" to an array of bytes where each byte is a digit
        /// </summary>
        /// <param name="bcd">Source BCD coded array of bytes</param>
        /// <param name="length">Length of BCD data</param>
        /// <returns>A new array of bytes where each byte is a digit</returns>
        /// <example>
        ///     <code>
        ///     byte[] value = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90 };
        ///     byte[] data = value.fromBcd(7);
        ///     // Now data = { 1, 2, 3, 4, 5, 6, 7 }
        ///     </code>
        /// </example>
        public static byte[] FromBcd(this byte[] bcd, UInt32 length)
        {
            var bytes = new byte[length];

            int i;
            for (i = 0; i + 1 < length; i += 2)
            {
                bytes[i] = (byte)((bcd[i/2] & 0xF0) >> 4);
                bytes[i + 1] = (byte)(bcd[i/2] & 0x0F);
            }

            if (length%2 == 1)
            {
                bytes[i] = (byte)((bcd[i/2] & 0xF0) >> 4);
            }

            return bytes;
        }

        /// <summary>
        /// Converts a string "BCD coded" to an array of bytes where each byte is a digit
        /// </summary>
        /// <param name="bcd">Source BCD coded array of bytes</param>
        /// <returns>A new array of bytes where each byte is a digit</returns>
        /// <example>
        ///     <code>
        ///     string value = "1234567890";
        ///     byte[] data = value.fromBcd();
        ///     // Now data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }
        ///     </code>
        /// </example>
        public static byte[] FromBcd(this String bcd)
        {
            return bcd.FromBcd((UInt32)bcd.Length);
        }

        /// <summary>
        /// Converts a string "BCD coded" to an array of bytes where each byte is a digit
        /// </summary>
        /// <param name="bcd">Source BCD coded array of bytes</param>
        /// <param name="length">Length of BCD data</param>
        /// <returns>A new array of bytes where each byte is a digit</returns>
        /// <example>
        ///     <code>
        ///     string value = "1234567890";
        ///     byte[] data = value.fromBcd(7);
        ///     // Now data = { 1, 2, 3, 4, 5, 6, 7 }
        ///     </code>
        /// </example>
        public static byte[] FromBcd(this String bcd, UInt32 length)
        {
            var bytes = new byte[length];

            for (var i = 0; i < length; i++)
            {
                bytes[i] = byte.Parse(bcd[i].ToString(CultureInfo.InvariantCulture));
            }

            return bytes;
        }
    }
}