using Emgu.CV.Reg;
using lsbExtractIFramesProject;
using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Xabe.FFmpeg;

public class Program
{
    public static async Task Main(string[] args)
    {

        // Example usage of the ExtractIFrames function
        string inputFilePath = "C:/Users/user 2017/Videos/WireShark/ArcVideo_clip.ts";
        string LSBallFrames = "C:/Users/user 2017/Videos/WireShark/LSBallFrames";

        string input7secfile = "C:/Users/user 2017/Videos/WireShark/output.mp4";
        string outputAll7secFrames = "C:/Users/user 2017/Videos/WireShark/LSB7secAllFrames";
        string outputAll7secFramesWithMessage = "C:/Users/user 2017/Videos/WireShark/LSB7secAllFramesWIthMessage";
        string test7SecframeWithMessage = "C:/Users/user 2017/Videos/WireShark/LSB7secTestwithIframe";

        string SunFlower = "C:/Users/user 2017/Videos/WireShark/bbb_sunflower_1080p_60fps_normal.ts";
        string outputDirectory = "C:/Users/user 2017/Videos/WireShark/LSBextractedFrames";
        string message ="hello gg ro everyone"; // Message to hide
        string LSBextractedFramesOutputDirectory = "C:/Users/user 2017/Videos/WireShark/LSBextractedFramesOutput";
        string LSBallExtractedFramesOutput = "C:/Users/user 2017/Videos/WireShark/LSBallextractedFrames";
        string LSBallExtractedFramesOutputTest = "C:/Users/user 2017/Videos/WireShark/LSBallextractedFramesTest";

        string reconstructed_video_7sec_withMessage = "C:/Users/user 2017/Videos/WireShark/reconstructed_video_7sec_withMessageLossless.mp4";
        string reconstructed_video_7sec_Iframes_directory = "C:/Users/user 2017/Videos/WireShark/reconstructed_video_7sec_Iframes";

        string metaData7secfileReconstructed = "C:/Users/user 2017/Videos/WireShark/7secfile_ReconstructedWithMessage_metadata.csv";
        string metadataFile = "C:/Users/user 2017/Videos/WireShark/ArcVideoframe_metadata.csv";
        string metaData7secfile = "C:/Users/user 2017/Videos/WireShark/7secfile_metadata.csv";
        Console.WriteLine("Starting I-frame extraction and embedding...");

        try
        {
            //await Extraction.ExtractAllFrames(input7secfile, outputAll7secFrames);

            //Extraction.ExtractFrameMetadata(input7secfile, metaData7secfile);
            //int[] iFrameLocations = await Extraction.GetIFrameLocations(metaData7secfile);
            //Embedding.EmbedMessageInFramesTestInVideo(outputAll7secFrames, outputAll7secFramesWithMessage, message, iFrameLocations);
            HelperFunctions.ReconstructVideo(outputAll7secFramesWithMessage, reconstructed_video_7sec_withMessage, 30);
            //await Extraction.ExtractIFrames(reconstructed_video_7sec_withMessage, reconstructed_video_7sec_Iframes);
            //Embedding.EmbedMessageInFrames(outputDirectory, message);//the output of ExtractIFrames becomes the input

            //Console.WriteLine("I-frame extraction and embedding completed successfully.");
            //Extraction.ExtractMessageFromIFrames(reconstructed_video_7sec_Iframes_directory);
            //await Extraction.ExtractFrameMetadata(reconstructed_video_7sec_withMessage, metaData7secfileReconstructed);



        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
       

        

        //Console.WriteLine($"Array of I-Frame locations: {string.Join(", ", iFrameLocations)}");

    }

}
