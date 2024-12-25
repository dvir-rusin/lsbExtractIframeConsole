﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static int extractNumberFromFilePath(string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            // Regular expression to extract the number
            string pattern = @"frame_(\d{4})";
            Match match = Regex.Match(fileName, pattern);
            int number;
            string numberString;
            if (match.Success)
            {
                // Extract the number and parse it as an integer
                numberString = match.Groups[1].Value;
                number = int.Parse(numberString);

                Console.WriteLine($"Extracted number: {number}");
            }
            else
            {
                Console.WriteLine("No number found in the file name.");
            }
             numberString = match.Groups[1].Value;
             number = int.Parse(numberString);
            return number;
        }


        /// <summary>
        /// Reconstructs a video from frames stored in a folder and compresses it using H.264.
        /// </summary>
        /// <param name="framesPath">Path to the folder containing the video frames.</param>
        /// <param name="outputFilePath">Path to save the reconstructed video file.</param>
        /// <param name="frameRate">Frame rate for the output video (e.g., 30).</param>
        public static void ReconstructVideo(string framesPath, string outputFilePath, int frameRate)
        {
            // Check for FFmpeg availability
            string ffmpegPath = "ffmpeg"; // Assumes ffmpeg is in system PATH
            string inputPattern = $"{framesPath}\\frame_%04d.png"; // Adjust for the frame naming format (e.g., frame_0001.png)

            // FFmpeg command to reconstruct and compress video
            string arguments = $"-framerate {frameRate} -i \"{inputPattern}\" -c:v libx264  -qp 0 -preset ultrafast -an \"{outputFilePath}\"";



            try
            {
                // Configure the process
                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // Start the process
                using (Process process = new Process { StartInfo = processInfo })
                {
                    process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
                    process.ErrorDataReceived += (sender, args) => Console.WriteLine(args.Data);

                    Console.WriteLine("Starting video reconstruction...");
                    process.Start();

                    // Begin async reading of output/error
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    process.WaitForExit();

                    Console.WriteLine("Video reconstruction completed successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reconstructing the video: {ex.Message}");
            }
        }

    }
}
