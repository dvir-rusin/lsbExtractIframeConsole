using Emgu.CV.Reg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace lsbExtractIFramesProject
{
    internal class Extraction
    {
        // Example usage of the ExtractIFrames function
        //string inputFilePath = "C:/Users/user 2017/Videos/WireShark/ArcVideo_clip.ts";
        //string outputDirectory = "C:/Users/user 2017/Videos/WireShark/LSBextractedFrames";
        public static async Task ExtractIFrames(string videoFilePath, string outputDirectory)
        {
            // Set the FFmpeg path if not already set
            FFmpeg.SetExecutablesPath("C:/ffmpeg/bin"); // Change to your FFmpeg path  

            // Define the output pattern for frame images
            string outputPattern = Path.Combine(outputDirectory, "frame_%04d.png");

            // Create the conversion with input and output
            var conversion = FFmpeg.Conversions.New()
               .AddParameter($"-i \"{videoFilePath}\"")           // Input file path
               .AddParameter("-vf select='eq(pict_type,I)'")      // Filter for only I-frames
               .AddParameter("-vsync vfr")                        // Avoid duplicate frames
               .SetOutput(outputPattern);

            Console.WriteLine("Extracting I-frames...");

            // Execute the conversion
            await conversion.Start();

            Console.WriteLine("All I-frames have been extracted and saved to the output directory.");
        }

        public static void ExtractMessageFromFrames(string frameDirectory)
        {
            Console.WriteLine("");
            Console.WriteLine("Extracting message from frames...");
            // Get the first image to read the message length
            string firstFramePath = Directory.GetFiles(frameDirectory, "*.png").FirstOrDefault();
            if (firstFramePath == null)
            {
                Console.WriteLine("No frames found in the directory.");
                Console.WriteLine("-exiting-");
                return;
            }
           

            foreach (string filePath in Directory.GetFiles(frameDirectory, "*.png"))
            {
                Bitmap frameBitmap = new Bitmap(filePath);
                int msglen = GetMessageLength(frameBitmap);
                
            }

            // Initialize to extract the message based on length
            
            foreach (string filePath in Directory.GetFiles(frameDirectory, "*.png"))
            {
                Bitmap frameBitmap = new Bitmap(filePath);
                using (frameBitmap)
                {
                    int messageLength = GetMessageLength(frameBitmap);
                    Console.WriteLine("msglen:" + messageLength);
                    string message =  GetHiddenMessage(frameBitmap, messageLength);
                    Console.WriteLine("Hidden Message: " + message);
                }
            }

         
        }


        private static int GetMessageLength(Bitmap bitmap)
        {
            StringBuilder binaryLength = new StringBuilder();
            int lengthBitsExtracted = 0;
            Color pixelColor;
            for (int i = 0;i<3;i++)  
            {
                pixelColor = bitmap.GetPixel(i, 0);
                binaryLength.Append((pixelColor.R & 1) == 1 ? "1" : "0");
                binaryLength.Append((pixelColor.G & 1) == 1 ? "1" : "0");
                binaryLength.Append((pixelColor.B & 1) == 1 ? "1" : "0");

                lengthBitsExtracted += 3;
            }
            return Convert.ToInt32(binaryLength.ToString(), 2);
        }

        private static string GetHiddenMessage(Bitmap frameBitmap, int messageLength)
        {
            StringBuilder binaryMessage = new StringBuilder();
            int messageBitsExtracted = 0;
            Color pixelColor;
            string HiddenMsg;
            // Start reading from the 5th pixel (index 4) for the actual message
            for (int i = 0; i < frameBitmap.Width * frameBitmap.Height && messageBitsExtracted < messageLength; i++)
            {
                int x = i % frameBitmap.Width;
                int y = i / frameBitmap.Width;
                pixelColor = frameBitmap.GetPixel(x, y);
                // Extract the LSB from each color channel
                binaryMessage.Append((pixelColor.R & 1) == 1 ? "1" : "0");
                messageBitsExtracted++;
                if(messageBitsExtracted%8==0)
                {
                    HiddenMsg = lsbExtractIFramesProject.HelperFunctions.BinaryToString(binaryMessage.ToString());
                    if (NullCheck(HiddenMsg, messageBitsExtracted) == true)
                        break;
                }
                    


                binaryMessage.Append((pixelColor.G & 1) == 1 ? "1" : "0");
                messageBitsExtracted++;
                if (messageBitsExtracted % 8 == 0)
                {
                    HiddenMsg = lsbExtractIFramesProject.HelperFunctions.BinaryToString(binaryMessage.ToString());
                    if (NullCheck(HiddenMsg, messageBitsExtracted) == true)
                        break;
                }

                binaryMessage.Append((pixelColor.B & 1) == 1 ? "1" : "0");
                messageBitsExtracted++;
                if (messageBitsExtracted % 8 == 0)
                {
                    HiddenMsg = lsbExtractIFramesProject.HelperFunctions.BinaryToString(binaryMessage.ToString());
                    if (NullCheck(HiddenMsg, messageBitsExtracted) == true)
                        break;
                }

            }
            Console.WriteLine("Binary Message: " + binaryMessage);
            HiddenMsg = lsbExtractIFramesProject.HelperFunctions.BinaryToString(binaryMessage.ToString());
            return HiddenMsg;
            //string extractedMessage = lsbExtractIFramesProject.HelperFunctions.BinaryToString(binaryMessage.ToString());
            //Console.WriteLine($"Extracted Message: {extractedMessage}");
            
        }

        private static bool NullCheck(string HiddenMsg, int messageBitsExtracted)
        {
            
            
            if (HiddenMsg[HiddenMsg.Length - 1] == char.MinValue)
            {
                return true;
            }
            return false;
            
            
        }
    }
}
