using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lsbExtractIFramesProject
{
    internal class HelperFunctions
    {

        public static string StringToBinary(string data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in data)
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }

        public static string BinaryToString(string binaryData)
        {
            var sb = new StringBuilder();

            // Process each 8 bits (1 byte) at a time
            for (int i = 0; i < binaryData.Length; i += 8)
            {
                // Take 8 bits and convert them to a character
                string byteString = binaryData.Substring(i, 8);

                //add a decryption method for the extracted bits later on 

                int asciiValue = Convert.ToInt32(byteString, 2);
                sb.Append((char)asciiValue);
            }

            return sb.ToString();
        }
    }
}
