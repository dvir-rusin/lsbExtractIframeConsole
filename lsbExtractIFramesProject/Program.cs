using Emgu.CV.Reg;
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
        string outputDirectory = "C:/Users/user 2017/Videos/WireShark/LSBextractedFrames";
        string message ="hello g"; // Message to hide
        string LSBextractedFramesOutputDirectory = "C:/Users/user 2017/Videos/WireShark/LSBextractedFramesOutput";

        Console.WriteLine("Starting I-frame extraction and embedding...");

        try
        {
            await lsbExtractIFramesProject.Extraction.ExtractIFrames(inputFilePath, outputDirectory);
            lsbExtractIFramesProject.Embedding.EmbedMessageInFrames(outputDirectory, message);//the output of ExtractIFrames becomes the input 
            Console.WriteLine("I-frame extraction and embedding completed successfully.");
            lsbExtractIFramesProject.Extraction.ExtractMessageFromFrames(LSBextractedFramesOutputDirectory);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

       

    }


    
}
