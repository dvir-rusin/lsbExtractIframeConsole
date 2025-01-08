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
        string outputAll7secFramesbmp = "C:/Users/user 2017/Videos/WireShark/LSB7secAllFramesbmp";

        string outputAll7secFramesWithMessage = "C:/Users/user 2017/Videos/WireShark/LSB7secAllFramesWIthMessage";
        string outputAll7secFramesWithEncryptedMessage = "C:/Users/user 2017/Videos/WireShark/LSB7secAllFramesWIthEncryptedMessage";
        string outputAll7secFramesWithMessageRedTest = "C:/Users/user 2017/Videos/WireShark/LSB7secOutputAllFramesWIthMessageRedTest";
        string test7SecframeWithMessage = "C:/Users/user 2017/Videos/WireShark/LSB7secTestwithIframe";

        string SunFlower = "C:/Users/user 2017/Videos/WireShark/bbb_sunflower_1080p_60fps_normal.ts";
        string outputDirectory = "C:/Users/user 2017/Videos/WireShark/LSBextractedFrames";
        string message ="hello gg ro everyone"; // Message to hide
        string LSBextractedFramesOutputDirectory = "C:/Users/user 2017/Videos/WireShark/LSBextractedFramesOutput";
        string LSBallExtractedFramesOutput = "C:/Users/user 2017/Videos/WireShark/LSBallextractedFrames";
        string LSBallExtractedFramesOutputTest = "C:/Users/user 2017/Videos/WireShark/LSBallextractedFramesTest";

        string reconstructed_video_7sec_withMessageMKV = "C:/Users/user 2017/Videos/WireShark/reconstructed_video_7sec_withMessageLossless11.mkv";
        string reconstructed_video_7sec_withMessageRawMp4 = "C:/Users/user 2017/Videos/WireShark/reconstructed_video_7sec_withMessageRawMp4.mp4";
        string reconstructed_video_7sec_withMessageAvi = "C:/Users/user 2017/Videos/WireShark/reconstructed_video_7sec_withMessageLosslessAviVeryFast.avi";
        string aviIframes = "C:/Users/user 2017/Videos/WireShark/AviiFrames";
        string mkvIframes = "C:/Users/user 2017/Videos/WireShark/MKViFrames";

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
        //Console.WriteLine("Starting I-frame extraction and embedding...");




        //try
        //{

        //extrcat all frames from a video
        //await Extraction.ExtractAllFrames(input7secfile, outputAll7secFrames);

        //extract frames meta data for message embedding 
        //Extraction.ExtractFrameMetadata(input7secfile, metaData7secfile);

        //embed message in i frames based on the extracted meta data
        //int[] iFrameLocations = await Extraction.GetIFrameLocations(metaData7secfile);
        //Embedding.EmbedMessageInFramesTestInVideo(outputAll7secFrames, outputAll7secFramesWithEncryptedMessage, iFrameLocations);
        //Embedding.EmbedMessageInFramesTestInVideo(outputAll7secFrames, outputAll7secFramesWithEncryptedMessage, iFrameLocations);
        //reconstruct video from frames with message
        //HelperFunctions.ReconstructVideo(outputAll7secFramesWithEncryptedMessage, reconstructed_video_7sec_withMessageAvi, 30);

        //extract i frames from the reconstructed video
        //await Extraction.ExtractIFrames(reconstructed_video_7sec_withMessageAvi, aviIframes);


        //extract message from all i frames from reconstructed video
        Extraction.ExtractMessageFromIFrames(aviIframes);

        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"An error occurred: {ex.Message}");
        //}




        //Console.WriteLine($"Array of I-Frame locations: {string.Join(", ", iFrameLocations)}");


        // encryption testing 
        //FunctionTesting.EncryptionAesTesting();

        //testing everything together
        //FunctionTesting.testing_EmbeddingAndExtractionFully();

    }


}
