﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lsbExtractIFramesProject
{
    internal class Embedding
    {

        

        public static void EmbedMessageInFramesRedTest(string frameDirectory, string message)
        {
            string LSBextractedFramesOutputDirectory = "C:/Users/user 2017/Videos/WireShark/reconstructed_video_7sec_Iframes4";

            if (!Directory.Exists(LSBextractedFramesOutputDirectory))
            {
                Console.WriteLine("EmbedMessageInFrames function message : the directory of i frames does not exist ");
                return;
            }
            message += char.MinValue;
            Console.WriteLine(message);
            string binaryMessage = HelperFunctions.StringToBinary(message);
            Console.WriteLine("ORIGINAL binaryMessage: " + binaryMessage);
            Console.WriteLine("Message : " + HelperFunctions.BinaryToString(binaryMessage));

            string binaryLength = Convert.ToString(binaryMessage.Length, 2).PadLeft(12, '0');
            Console.WriteLine("Binary Length: " + binaryLength);

            bool messageComplete = false;
            int messageIndex = 0;

            foreach (string filePath in Directory.GetFiles(frameDirectory, "*.png"))
            {
                using (Bitmap bitmap = new Bitmap(filePath))
                {
                    for (int y = 0; y < bitmap.Height && !messageComplete; y++)
                    {
                        for (int x = 0; x < bitmap.Width && !messageComplete; x++)
                        {
                            Color pixelColor = bitmap.GetPixel(x, y);

                            int r = pixelColor.R, g = pixelColor.G, b = pixelColor.B;

                            r = EmbedBitInColorChannel(r, binaryMessage, ref messageIndex);
                            g = EmbedBitInColorChannel(g, binaryMessage, ref messageIndex);
                            b = EmbedBitInColorChannel(b, binaryMessage, ref messageIndex);

                            if (messageIndex >= binaryMessage.Length)
                            {
                                messageComplete = true;
                            }

                            bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                        }
                    }
                    bitmap.SetPixel(1000, 1000, Color.FromArgb(255, 0, 0));
                    bitmap.SetPixel(1001, 1000, Color.FromArgb(255, 0, 0));
                    bitmap.SetPixel(1002, 1000, Color.FromArgb(255, 0, 0));
                    bitmap.SetPixel(1003, 1000, Color.FromArgb(255, 0, 0));
                    bitmap.SetPixel(1004, 1000, Color.FromArgb(255, 0, 0));
                    bitmap.SetPixel(1005, 1000, Color.FromArgb(255, 0, 0));
                    bitmap.SetPixel(1006, 1000, Color.FromArgb(255, 0, 0));
                    bitmap.SetPixel(1007, 1000, Color.FromArgb(255, 0, 0));
                    bitmap.SetPixel(bitmap.Width, bitmap.Height, Color.FromArgb(255, 0, 0));
                    bitmap.SetPixel(bitmap.Width - 1, bitmap.Height - 1, Color.FromArgb(255, 0, 0));
                    bitmap.SetPixel(bitmap.Width - 2, bitmap.Height - 2, Color.FromArgb(255, 0, 0));
                    bitmap.SetPixel(bitmap.Width - 3, bitmap.Height - 3, Color.FromArgb(255, 0, 0));

                    string outputFilePath = Path.Combine(LSBextractedFramesOutputDirectory, Path.GetFileName(filePath));
                    bitmap.Save(outputFilePath, System.Drawing.Imaging.ImageFormat.Png);

                    if (messageComplete) break; // Stop for only checking the first frame break will be removed later on
                }
            }

            Console.WriteLine("Message embedded in all frames.");
        }

        public static void EmbedMessageInFramesTestInVideo
            (string inputframesDirectory,
            string outputframesDirectrory,
            int[] IframesLocation)
        {
            Console.WriteLine("Enter text to encrypt:");
            string UserInputMessage = Console.ReadLine(); // Read the plaintext input from the user

            string UserPassword = EncryptionAes.GetUserCustomKey(); // Get the user-provided custom key

            string EncryptedMessage = EncryptionAes.Encrypt(UserInputMessage, UserPassword); // Encrypt the plaintext using the custom key
            Console.WriteLine($"Encrypted Text: {EncryptedMessage}");

            if (!Directory.Exists(inputframesDirectory))
            {
                Console.WriteLine("EmbedMessageInFramesTestInVideo function message : inputframesDirectory does not exist ");
                return;
            }
            EncryptedMessage += char.MinValue;
            Console.WriteLine(EncryptedMessage);
            string binaryEncryptedMessage = HelperFunctions.StringToBinary(EncryptedMessage);
            Console.WriteLine("ORIGINAL binaryMessage: " + binaryEncryptedMessage);
            Console.WriteLine("Message : " + HelperFunctions.BinaryToString(binaryEncryptedMessage));

            string binaryLength = Convert.ToString(binaryEncryptedMessage.Length, 2).PadLeft(12, '0');
            Console.WriteLine("Binary Length: " + binaryLength);
            int indexer = 0;



            foreach (string filePath in Directory.GetFiles(inputframesDirectory, "*.png"))
            {
                bool isEmpty = false;
                bool messageComplete = false;
                int messageIndex = 0;
                bool isNumberInFile = false;
                
                int numberInFile = HelperFunctions.extractNumberFromFilePath(filePath);
                using (Bitmap bitmap = new Bitmap(filePath))
                {
                    for (int i = indexer; isNumberInFile != true && i < IframesLocation.Length; i++)
                    {
                        if (numberInFile == IframesLocation.ElementAt(i))
                        {
                            isNumberInFile = true;
                            indexer++;

                        }
                            

                    }
                    if (isNumberInFile == true)
                    {
                        for (int y = 0; y < bitmap.Height && !messageComplete; y++)
                        {
                            for (int x = 0; x < bitmap.Width && !messageComplete; x++)
                            {
                                Color pixelColor = bitmap.GetPixel(x, y);

                                int r = pixelColor.R, g = pixelColor.G, b = pixelColor.B;

                                r = EmbedBitInColorChannel(r, binaryEncryptedMessage, ref messageIndex);
                                g = EmbedBitInColorChannel(g, binaryEncryptedMessage, ref messageIndex);
                                b = EmbedBitInColorChannel(b, binaryEncryptedMessage, ref messageIndex);

                                if (messageIndex >= binaryEncryptedMessage.Length)
                                {
                                    messageComplete = true;

                                    bitmap.SetPixel(1000, 1000, Color.FromArgb(254, 0, 0));
                                    bitmap.SetPixel(1001, 1000, Color.FromArgb(254, 0, 0));
                                    bitmap.SetPixel(1002, 1000, Color.FromArgb(254, 0, 0));
                                    bitmap.SetPixel(1003, 1000, Color.FromArgb(254, 0, 0));
                                    bitmap.SetPixel(1004, 1000, Color.FromArgb(254, 0, 0));
                                    bitmap.SetPixel(1005, 1000, Color.FromArgb(254, 0, 0));
                                    bitmap.SetPixel(1006, 1000, Color.FromArgb(254, 0, 0));
                                    bitmap.SetPixel(1007, 1000, Color.FromArgb(254, 0, 0));
                                    
                                    bitmap.SetPixel(bitmap.Width - 1, bitmap.Height - 1, Color.FromArgb(254, 1, 1));
                                    bitmap.SetPixel(bitmap.Width - 2, bitmap.Height - 2, Color.FromArgb(254, 1, 1));
                                    bitmap.SetPixel(bitmap.Width - 3, bitmap.Height - 3, Color.FromArgb(254, 1, 1));
                                    bitmap.SetPixel(bitmap.Width-4, bitmap.Height-4, Color.FromArgb(254, 1, 1));
                                }

                                bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                                Console.WriteLine("bitmap has been saved with the number: " + numberInFile);
                            }
                        }
                    }
                    

                    string outputFilePath = Path.Combine(outputframesDirectrory, Path.GetFileName(filePath));
                    bitmap.Save(outputFilePath, System.Drawing.Imaging.ImageFormat.Png);

                    //if (messageComplete) break; // Stop for only checking the first frame break will be removed later on
                }
            }

            Console.WriteLine("Message embedded in all frames.");
        }

        public static int EmbedBitInColorChannel(int colorChannelValue, string binaryMessage, ref int messageIndex)
        {
            // Only embed if there are bits left to embed
            if (messageIndex < binaryMessage.Length)
            {
                // Get the bit to embed (0 or 1)
                int bit = binaryMessage[messageIndex] == '1' ? 1 : 0;


                // Embed the bit in the LSB
                colorChannelValue = (colorChannelValue & 0xFE) | bit;

                // Move to the next bit in the message
                messageIndex++;
            }

            return colorChannelValue;
        }

        public static Bitmap EmbedMessageInFrameTest(Bitmap frame,string message)
        {

            message += char.MinValue;
            Console.WriteLine(message);
            string binaryMessage = HelperFunctions.StringToBinary(message);
            Console.WriteLine("ORIGINAL binaryMessage: " + binaryMessage);
            Console.WriteLine("Message : " + HelperFunctions.BinaryToString(binaryMessage));

            string binaryLength = Convert.ToString(binaryMessage.Length, 2).PadLeft(12, '0');
            Console.WriteLine("Binary Length: " + binaryLength);

            bool messageComplete = false;
            int messageIndex = 0;



            using (Bitmap bitmap = new Bitmap(frame))
            {
                for (int y = 0; y < bitmap.Height && !messageComplete; y++)
                {
                    for (int x = 0; x < bitmap.Width && !messageComplete; x++)
                    {
                        Color pixelColor = bitmap.GetPixel(x, y);

                        int r = pixelColor.R, g = pixelColor.G, b = pixelColor.B;

                        r = EmbedBitInColorChannel(r, binaryMessage, ref messageIndex);
                        g = EmbedBitInColorChannel(g, binaryMessage, ref messageIndex);
                        b = EmbedBitInColorChannel(b, binaryMessage, ref messageIndex);

                        if (messageIndex >= binaryMessage.Length)
                        {
                            messageComplete = true;
                        }

                        bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }



                if (messageComplete)
                {
                    Console.WriteLine("Message embedded in frame.");
                    return frame;

                }


            }
            return frame;
        }
    }
}
