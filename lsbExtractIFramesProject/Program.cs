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
        string input7secfile = "C:/Users/user 2017/Videos/WireShark/output.mp4";
        string SunFlower = "C:/Users/user 2017/Videos/WireShark/bbb_sunflower_1080p_60fps_normal.ts";
        string outputDirectory = "C:/Users/user 2017/Videos/WireShark/LSBextractedFrames";
        string message ="hello gg"; // Message to hide
        string LSBextractedFramesOutputDirectory = "C:/Users/user 2017/Videos/WireShark/LSBextractedFramesOutput";
        string LSBallExtractedFramesOutput = "C:/Users/user 2017/Videos/WireShark/LSBallextractedFrames";
        string LSBallExtractedFramesOutputTest = "C:/Users/user 2017/Videos/WireShark/LSBallextractedFramesTest";
        string metadataFile = "C:/Users/user 2017/Videos/WireShark/ArcVideoframe_metadata.csv";
        string metaData7secfile = "C:/Users/user 2017/Videos/WireShark/7secfile_metadata.csv";
        Console.WriteLine("Starting I-frame extraction and embedding...");

        //try
        //{
        //    await Extraction.ExtractIFrames(inputFilePath, outputDirectory);
        //    Embedding.EmbedMessageInFrames(outputDirectory, message);//the output of ExtractIFrames becomes the input 
        //    Console.WriteLine("I-frame extraction and embedding completed successfully.");
        //    Extraction.ExtractMessageFromFrames(LSBextractedFramesOutputDirectory);
        //    await Extraction.ExtractFrameMetadata(inputFilePath, metadataFile);

        //    //var iFrameLocations = await Extraction.ExtractIFrameLocations(inputFilePath);

        //    //// Output the absolute locations of I-frames
        //    //Console.WriteLine("I-Frame Locations:");
        //    //foreach (var location in iFrameLocations)
        //    //{
        //    //    Console.WriteLine(location);
        //    //}

        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"An error occurred: {ex.Message}");
        //}
        //try
        //{
        //    await Extraction.ProcessAndSaveIFrames
        //        (inputFilePath,
        //        "C:/Users/user 2017/Videos/WireShark",
        //        message
        //        ); // Pass the modification logic here


        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine($"An error occurred: {ex.Message}");
        //}
        
        
        //await Extraction.ExtractAllFramesWithType(input7secfile, LSBallExtractedFramesOutputTest);
        Extraction.ExtractFrameMetadata(input7secfile, metaData7secfile);
        int[] iFrameLocations = await Extraction.GetIFrameLocations(metaData7secfile);

        Console.WriteLine($"Array of I-Frame locations: {string.Join(", ", iFrameLocations)}");











    }



}
