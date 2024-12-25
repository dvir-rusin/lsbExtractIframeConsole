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

            try
            {
                

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during testing: {ex.Message}");
            }

            Console.WriteLine("FunctionTesting completed.");
        }

        public static async Task Main2()
        {
            await RunTests();
        }
    }
}
