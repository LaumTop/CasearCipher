using System;
using System.Text;
using System.Threading;

namespace Cipher
{
    class MyCipher
    {
        public static int trys;
        public static string cipher = "";
        public static int procents;
        public static bool decryptionCompleted = false;

        static void ProgressBar()
        {
            var symbol = "#";
            Console.ForegroundColor = ConsoleColor.Green;

            for (int a = 0; a <= 100; a++)
            {
                procents = a;
                Console.Write($"Progress: [{procents}%] ");

                int completedBlocks = procents / 10;
                int remainingBlocks = 10 - completedBlocks;

                Console.Write("[");
                Console.Write(new string(symbol[0], completedBlocks));
                Console.Write(new string('.', remainingBlocks));
                Console.Write("]");

                Thread.Sleep(100);
                Console.SetCursorPosition(0, Console.CursorTop);
            }

            Console.WriteLine();
            Console.ResetColor();

            decryptionCompleted = true;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please enter the cipher: ");
            cipher = Console.ReadLine();
            Console.WriteLine("Also, enter the number of attempts for the brute force:");
            trys = Convert.ToInt32(Console.ReadLine());
            Console.ResetColor();
            Console.Clear();

            Thread progressBarThread = new Thread(ProgressBar);
            progressBarThread.Start();

            progressBarThread.Join();

            if (decryptionCompleted)
            {
                Console.WriteLine("Decrypted text:");
                Cipher();
            }
        }

        static void Cipher()
        {
            string encryptedText = cipher;
            for (int key = 0; key <= trys; key++)
            {
                string decryptedText = DecryptText(encryptedText, key);

                Console.WriteLine("Key: " + key + ", Decrypted text: " + decryptedText);
            }
        }

        public static string DecryptText(string encryptedText, int key)
        {
            string decryptedText = "";

            foreach (char c in encryptedText)
            {
                if (char.IsLetter(c))
                {
                    char decryptedChar = (char)(c - key);
                    if (decryptedChar < 'A')
                    {
                        decryptedChar = (char)(decryptedChar + ('Z' - 'A' + 1));
                    }
                    decryptedText += decryptedChar;
                }
                else
                {
                    decryptedText += c;
                }
            }

            return decryptedText;
        }
    }
}
