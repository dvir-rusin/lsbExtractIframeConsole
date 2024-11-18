using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lsbExtractIFramesProject
{
    internal class Embedding
    {

        //string outputDirectory = "C:/Users/user 2017/Videos/WireShark/LSBextractedFrames";
        //string message = "hello g"; // Message to hide


        public static void EmbedMessageInFrames(string frameDirectory, string message)
        {
            string LSBextractedFramesOutputDirectory = "C:/Users/user 2017/Videos/WireShark/LSBextractedFramesOutput";

            if (!Directory.Exists(LSBextractedFramesOutputDirectory))
            {
                Directory.CreateDirectory(LSBextractedFramesOutputDirectory);
            }
            message += char.MinValue;
            Console.WriteLine(message);
            string binaryMessage = lsbExtractIFramesProject.HelperFunctions.StringToBinary(message);
            Console.WriteLine("ORIGINAL binaryMessage: " + binaryMessage);
            Console.WriteLine("Message : " + lsbExtractIFramesProject.HelperFunctions.BinaryToString(binaryMessage));

            string binaryLength = Convert.ToString(binaryMessage.Length, 2).PadLeft(12, '0');
            Console.WriteLine("Binary Length: " + binaryLength);

            bool messageComplete = false;
            int messageIndex = 0;

            foreach (string filePath in Directory.GetFiles(frameDirectory, "*.png"))
            {
                using (Bitmap bitmap = new Bitmap(filePath))
                {
                    for (int y = 0; y < bitmap.Height && !messageComplete; y++)
                    {
                        for (int x = 0; x < bitmap.Width && !messageComplete; x++)
                        {
                            Color pixelColor = bitmap.GetPixel(x, y);

                            int r = pixelColor.R, g = pixelColor.G, b = pixelColor.B;

                                r = EmbedBitInColorChannel(r, binaryMessage, ref messageIndex);
                                g = EmbedBitInColorChannel(g, binaryMessage, ref messageIndex);
                                b = EmbedBitInColorChannel(b, binaryMessage, ref messageIndex);

                                if (messageIndex >= binaryMessage.Length)
                                {
                                    messageComplete = true;
                                }
                            
                            bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                        }
                    }

                    string outputFilePath = Path.Combine(LSBextractedFramesOutputDirectory, Path.GetFileName(filePath));
                    bitmap.Save(outputFilePath, System.Drawing.Imaging.ImageFormat.Png);

                    if (messageComplete) break; // Stop if the message is complete
                }
            }

            Console.WriteLine("Message embedded in all frames.");
        }


        public static int EmbedBitInColorChannel(int colorChannelValue, string binaryMessage, ref int messageIndex)
        {
            // Only embed if there are bits left to embed
            if (messageIndex < binaryMessage.Length)
            {
                // Get the bit to embed (0 or 1)
                int bit = binaryMessage[messageIndex] == '1' ? 1 : 0;
                

                // Embed the bit in the LSB
                colorChannelValue = (colorChannelValue & 0xFE) | bit;

                // Move to the next bit in the message
                messageIndex++;
            }

            return colorChannelValue;
        }
    }
}
