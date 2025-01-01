using System;
using System.Threading.Tasks;

namespace lsbExtractIFramesProject
{
    internal class FunctionTesting
    {
        public static async Task RunTests()
        {
            string inputFilePath = "C:/Users/user 2017/Videos/WireShark/ArcVideo_clip.ts";
            string LSBallFrames = "C:/Users/user 2017/Videos/WireShark/LSBallFrames";

            string input7secfile = "C:/Users/user 2017/Videos/WireShark/output.mp4";
            string outputAll7secFrames = "C:/Users/user 2017/Videos/WireShark/LSB7secAllFrames";
            string outputAll7secFramesWithMessage = "C:/Users/user 2017/Videos/WireShark/LSB7secAllFramesWIthMessage";
            string outputAll7secFramesWithMessageRedTest = "C:/Users/user 2017/Videos/WireShark/LSB7secOutputAllFramesWIthMessageRedTest";
            string test7SecframeWithMessage = "C:/Users/user 2017/Videos/WireShark/LSB7secTestwithIframe";

            string SunFlower = "C:/Users/user 2017/Videos/WireShark/bbb_sunflower_1080p_60fps_normal.ts";
            string outputDirectory = "C:/Users/user 2017/Videos/WireShark/LSBextractedFrames";
            string message = "hello gg ro everyone"; // Message to hide
            string LSBextractedFramesOutputDirectory = "C:/Users/user 2017/Videos/WireShark/LSBextractedFramesOutput";
            string LSBallExtractedFramesOutput = "C:/Users/user 2017/Videos/WireShark/LSBallextractedFrames";
            string LSBallExtractedFramesOutputTest = "C:/Users/user 2017/Videos/WireShark/LSBallextractedFramesTest";

            string reconstructed_video_7sec_withMessage = "C:/Users/user 2017/Videos/WireShark/reconstructed_video_7sec_withMessageLossless7.mp4";
            string reconstructed_video_7sec_Iframes_directory = "C:/Users/user 2017/Videos/WireShark/reconstructed_video_7sec_Iframes";
            string reconstructed_video_7sec_Iframes_directory4 = "C:/Users/user 2017/Videos/WireShark/reconstructed_video_7sec_Iframes4";
            string reconstructed_video_7sec_Iframes_directory7 = "C:/Users/user 2017/Videos/WireShark/reconstructed_video_7sec_Iframes7";

            string metaData7secfileReconstructed = "C:/Users/user 2017/Videos/WireShark/7secfile_ReconstructedWithMessage_metadata.csv";
            string metadataFile = "C:/Users/user 2017/Videos/WireShark/ArcVideoframe_metadata.csv";
            string metaData7secfile = "C:/Users/user 2017/Videos/WireShark/7secfile_metadata.csv";

            Console.WriteLine("Starting FunctionTesting...");

            //extract i frames from a video
            //await HelperFunctions.ExtractIFrames(inputFilePath, LSBallFrames);

            //test if i frame folder has the message
            //await HelperFunctions.ExtractMessageFromIFrames(test7SecframeWithMessage, outputAll7secFramesWithMessageRedTest, message);

            //extract all frames from a video
            //await HelperFunctions.ExtractAllFrames(input7secfile, outputAll7secFrames);



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

        
    }
}
