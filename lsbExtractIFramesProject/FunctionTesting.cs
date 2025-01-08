using System;
using System.Threading.Tasks;

namespace lsbExtractIFramesProject
{
    internal class FunctionTesting
    {
        public static async Task RunTests()
        {
            


        }
        public static void EncryptionAesTesting()
        {
            Console.WriteLine("Enter text to encrypt:");
            string text = Console.ReadLine(); // Read the plaintext input from the user

            string customKey = EncryptionAes.GetUserCustomKey(); // Get the user-provided custom key

            string encrypted = EncryptionAes.Encrypt(text, customKey); // Encrypt the plaintext using the custom key
            Console.WriteLine($"Encrypted Text: {encrypted}");

            Console.WriteLine("\nEnter the custom key to decrypt:");
            string inputKey = Console.ReadLine(); // Read the custom key from the user for decryption

            try
            {
                // Attempt to decrypt the ciphertext with the user-provided key
                string decrypted = EncryptionAes.Decrypt(encrypted, inputKey);
                Console.WriteLine($"Decrypted Text: {decrypted}");
            }
            catch
            {
                // Handle decryption failure (e.g., incorrect custom key)
                Console.WriteLine("Decryption failed. Check your custom key.");
            }
        }

        public static void testing_red_pixels_after_compression()
        {
            //testing red pixels after compression
            //int[] iFrameLocations = await Extraction.GetIFrameLocations(metaData7secfile);
            //Embedding.EmbedMessageInFramesTestInVideo(outputAll7secFrames, outputAll7secFramesWithMessageRedTest, message, iFrameLocations);
            //HelperFunctions.ReconstructVideo(outputAll7secFramesWithMessage, reconstructed_video_7sec_withMessageMKV, 30);
            //await Extraction.ExtractIFrames(reconstructed_video_outputTsTest_ts, reconstructed_video_outputTsTestIframes_ts);

            //Extraction.GetPixelColor(reconstructed_video_7sec_IframeWithMessageRed);
        }

        public static void testing_EmbedMessageInFrames()
        {
            //public static void EmbedMessageInFrames(string frameDirectory, string message)
            //{
            //    string LSBextractedFramesOutputDirectory = "C:/Users/user 2017/Videos/WireShark/reconstructed_video_7sec_Iframes4";

            //    if (!Directory.Exists(LSBextractedFramesOutputDirectory))
            //    {
            //        Console.WriteLine("EmbedMessageInFrames function message : the directory of i frames does not exist ");
            //        return;
            //    }
            //    message += char.MinValue;
            //    Console.WriteLine(message);
            //    string binaryMessage = HelperFunctions.StringToBinary(message);
            //    Console.WriteLine("ORIGINAL binaryMessage: " + binaryMessage);
            //    Console.WriteLine("Message : " + HelperFunctions.BinaryToString(binaryMessage));

            //    string binaryLength = Convert.ToString(binaryMessage.Length, 2).PadLeft(12, '0');
            //    Console.WriteLine("Binary Length: " + binaryLength);

            //    bool messageComplete = false;
            //    int messageIndex = 0;

            //    foreach (string filePath in Directory.GetFiles(frameDirectory, "*.png"))
            //    {
            //        using (Bitmap bitmap = new Bitmap(filePath))
            //        {
            //            for (int y = 0; y < bitmap.Height && !messageComplete; y++)
            //            {
            //                for (int x = 0; x < bitmap.Width && !messageComplete; x++)
            //                {
            //                    Color pixelColor = bitmap.GetPixel(x, y);

            //                    int r = pixelColor.R, g = pixelColor.G, b = pixelColor.B;

            //                    r = EmbedBitInColorChannel(r, binaryMessage, ref messageIndex);
            //                    g = EmbedBitInColorChannel(g, binaryMessage, ref messageIndex);
            //                    b = EmbedBitInColorChannel(b, binaryMessage, ref messageIndex);

            //                    if (messageIndex >= binaryMessage.Length)
            //                    {
            //                        messageComplete = true;
            //                    }

            //                    bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
            //                }
            //            }


            //            string outputFilePath = Path.Combine(LSBextractedFramesOutputDirectory, Path.GetFileName(filePath));
            //            bitmap.Save(outputFilePath, System.Drawing.Imaging.ImageFormat.Png);

            //            if (messageComplete) break; // Stop for only checking the first frame break will be removed later on
            //        }
            //    }

            //    Console.WriteLine("Message embedded in all frames.");
            //}
        }

        public static async Task testing_EmbeddingAndExtractionFully()
        {
            // Example usage of the ExtractIFrames function
            string inputFilePath = "C:/Users/user 2017/Videos/WireShark/ArcVideo_clip.ts";
            string LSBallFrames = "C:/Users/user 2017/Videos/WireShark/LSBallFrames";

            string input7secfile = "C:/Users/user 2017/Videos/WireShark/output.mp4";
            string outputAll7secFrames = "C:/Users/user 2017/Videos/WireShark/LSB7secAllFrames";
            string outputAll7secFramesbmp = "C:/Users/user 2017/Videos/WireShark/LSB7secAllFramesbmp";

            string outputAll7secFramesWithMessage = "C:/Users/user 2017/Videos/WireShark/LSB7secAllFramesWIthMessage";
            string outputAll7secFramesWithMessageRedTest = "C:/Users/user 2017/Videos/WireShark/LSB7secOutputAllFramesWIthMessageRedTest";
            string outputAll7secFramesWithEncryptedMessage = "C:/Users/user 2017/Videos/WireShark/LSB7secAllFramesWIthEncryptedMessage";
            string test7SecframeWithMessage = "C:/Users/user 2017/Videos/WireShark/LSB7secTestwithIframe";

            string SunFlower = "C:/Users/user 2017/Videos/WireShark/bbb_sunflower_1080p_60fps_normal.ts";
            string outputDirectory = "C:/Users/user 2017/Videos/WireShark/LSBextractedFrames";
            string message = "hello gg ro everyone"; // Message to hide
            string LSBextractedFramesOutputDirectory = "C:/Users/user 2017/Videos/WireShark/LSBextractedFramesOutput";
            string LSBallExtractedFramesOutput = "C:/Users/user 2017/Videos/WireShark/LSBallextractedFrames";
            string LSBallExtractedFramesOutputTest = "C:/Users/user 2017/Videos/WireShark/LSBallextractedFramesTest";

            string reconstructed_video_7sec_withMessageMKV = "C:/Users/user 2017/Videos/WireShark/reconstructed_video_7sec_withMessageLossless11.mkv";
            string mkvIframes = "C:/Users/user 2017/Videos/WireShark/MKViFrames";

            string reconstructed_video_7sec_withMessageAvi = "C:/Users/user 2017/Videos/WireShark/reconstructed_video_7sec_withMessageLosslessAvi1.avi";

            string reconstructed_video_7sec_withMessageRed = "C:/Users/user 2017/Videos/WireShark/reconstructed_video_7sec_withMessageLosslessRed3.ts";
            string reconstructed_video_7sec_Iframes_directory = "C:/Users/user 2017/Videos/WireShark/reconstructed_video_7sec_Iframes";
            string reconstructed_video_7sec_Iframes_directory4 = "C:/Users/user 2017/Videos/WireShark/reconstructed_video_7sec_Iframes4";
            string reconstructed_video_7sec_Iframes_directory9 = "C:/Users/user 2017/Videos/WireShark/reconstructed_video_7sec_Iframes9";
            string test123 = "C:/Users/user 2017/Videos/WireShark/test123";

            string reconstructed_video_7sec_IframeWithMessageRed = "C:/Users/user 2017/Videos/WireShark/reconstructed_video_7sec_IframeWithMessageRed";

            string reconstructed_video_outputTsTest_ts = "C:/Users/user 2017/Videos/WireShark/outputTsTest.ts";
            string reconstructed_video_outputTsTestIframes_ts = "C:/Users/user 2017/Videos/WireShark/reconstructed_video_outputTsTestIframes_ts";

            string metaData7secfileReconstructed = "C:/Users/user 2017/Videos/WireShark/7secfile_ReconstructedWithMessage_metadata.csv";
            string metadataFile = "C:/Users/user 2017/Videos/WireShark/ArcVideoframe_metadata.csv";
            string metaData7secfile = "C:/Users/user 2017/Videos/WireShark/7secfile_metadata.csv";
            try
            {

                //extrcat all frames from a video
                await Extraction.ExtractAllFrames(input7secfile, outputAll7secFrames);

                //extract frames meta data for message embedding
    
                Extraction.ExtractFrameMetadata(input7secfile, metaData7secfile);

                //embed message in i frames based on the extracted meta data
    
                int[] iFrameLocations = await Extraction.GetIFrameLocations(metaData7secfile);
                //Embedding.EmbedMessageInFramesTestInVideo(outputAll7secFrames, outputAll7secFramesWithMessage, iFrameLocations);
                Embedding.EmbedMessageInFramesTestInVideo(outputAll7secFrames, outputAll7secFramesWithEncryptedMessage, iFrameLocations);

                //reconstruct video from frames with message
                HelperFunctions.ReconstructVideo(outputAll7secFramesWithEncryptedMessage, reconstructed_video_7sec_withMessageAvi, 30);

                //extract i frames from the reconstructed video
                await Extraction.ExtractIFrames(reconstructed_video_7sec_withMessageMKV, mkvIframes);


                //extract message from all i frames from reconstructed video
    
                Extraction.ExtractMessageFromIFrames(mkvIframes);




            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


    }
}
