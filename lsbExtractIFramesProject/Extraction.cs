using Emgu.CV.Reg;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xabe.FFmpeg;
using CsvHelper;
using CsvHelper.Configuration;

namespace lsbExtractIFramesProject
{
    public class CsvRow
    {
        public string Frame { get; set; }
        public string Type { get; set; }
    }
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


        public static async Task ExtractFrameMetadata(string videoFilePath, string metadataFile)
        {
            // Set the FFmpeg executables path
            FFmpeg.SetExecutablesPath("C:/ffmpeg/bin"); // Adjust this path as necessary

            // Use ffprobe to extract frame metadata
            var ffprobeCommand = $"ffprobe -i \"{videoFilePath}\" -select_streams v:0 -show_entries frame=pict_type -of csv=p=0 > \"{metadataFile}\"";

            Console.WriteLine("Extracting frame metadata...");
            await RunCommand(ffprobeCommand);
            Console.WriteLine($"Frame metadata saved to {metadataFile}");
        }




        private static async Task RunCommand(string command)
        {
            var processStartInfo = new ProcessStartInfo("cmd", "/C " + command)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(processStartInfo);
            await process.WaitForExitAsync();

            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();

            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine("Error: " + error);
            }
            else
            {
                Console.WriteLine(output);
            }
        }

        public static async Task ProcessAndSaveIFrames(string inputVideoPath, string outputVideoPath, string message)
        {
            // Set FFmpeg path
            FFmpeg.SetExecutablesPath("C:/ffmpeg/bin");
            int count = 0;

            Console.WriteLine("Processing frames...");

            // FFmpeg command to extract frames as a pipe
            var ffmpegInputArgs = $"-i \"{inputVideoPath}\" -vf \"showinfo\" -f image2pipe -vcodec png -";
            var ffmpegOutputArgs = $"-y -f image2pipe -vcodec png -i pipe: -c:v libx264 \"{outputVideoPath}\"";

            // Start FFmpeg process
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $"/C ffmpeg {ffmpegInputArgs} | ffmpeg {ffmpegOutputArgs}",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(processStartInfo);
            using var inputPipe = process.StandardOutput.BaseStream;
            using var outputPipe = process.StandardInput.BaseStream;
            

            // Process each frame
            while (true)
            {
                try
                {
                    // Read the current frame as a Bitmap
                    Bitmap frame = new Bitmap(inputPipe);
                    count++;
                    Console.WriteLine("Frame: " + count);
                    // Check if it's an I-frame (you may use metadata or FFmpeg filters)
                    if (IsIFrame(frame)) // Implement your own logic to identify I-frames
                    {
                        // Modify the frame
                        Bitmap modifiedFrame = Embedding.EmbedMessageInFrameTest(frame, message);

                        // Write the modified frame back to FFmpeg
                        modifiedFrame.Save(outputPipe, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    else
                    {
                        // Write the original frame back to FFmpeg
                        frame.Save(outputPipe, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Frame processing completed or encountered an error: " + ex.Message);
                    break;
                }
            }

            await process.WaitForExitAsync();
            Console.WriteLine("Video processing complete.");
        }
        private static bool IsIFrame(Bitmap frame)
        {
            // Use metadata from FFmpeg or pixel properties to detect if the frame is an I-frame
            // For now, this is a placeholder returning `true` for demonstration
            return true; // Replace with actual detection logic
        }


        public static async Task<int[]> GetIFrameLocations(string metadataFile)
        {
            List<int> iFrameLocations = new List<int>();
            int retryCount = 5; // Number of retries
            int delay = 500;    // Delay in milliseconds between retries

            for (int attempt = 1; attempt <= retryCount; attempt++)
            {
                try
                {
                    // Read the metadata file line by line
                    string[] lines = await File.ReadAllLinesAsync(metadataFile);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        // Each line should correspond to a frame's pict_type
                        string frameType = lines[i].Trim();

                        // Check if the frame type is "I"
                        if (frameType == "I,")
                        {
                            // Add the frame number to the array (1-based indexing)
                            iFrameLocations.Add(i + 1);
                        }
                    }

                    // Output the locations for debugging purposes
                    Console.WriteLine($"I-Frame Locations: {string.Join(", ", iFrameLocations)}");
                    break; // Exit the loop if successful
                }
                catch (IOException ex) when (ex.Message.Contains("being used by another process"))
                {
                    Console.WriteLine($"Attempt {attempt}/{retryCount}: File is in use. Retrying in {delay}ms...");
                    await Task.Delay(delay);

                    if (attempt == retryCount)
                    {
                        Console.WriteLine($"Error reading metadata file after {retryCount} attempts: {ex.Message}");
                        return Array.Empty<int>();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error reading metadata file: {ex.Message}");
                    return Array.Empty<int>();
                }
            }

            return iFrameLocations.ToArray();
        }









    }
}
