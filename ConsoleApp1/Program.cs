using System;
using System.IO;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            /*Declaring our directory to search need to make this in your C drive, I am too lazy to create a dir for you*/
            const string filePath = "C:/batchFiles/";
            int loopcount = 0;
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            /*inserting each file from the directory into our array to loop through*/
            /*For ease of use saying that any file in that directory well loop through so as long as you create the directory you dont need to worry about
             switching file type.*/
            string[] _batches = Directory.GetFiles(@filePath, "*.*");

            /*looping through each file in the directory*/
            
            foreach (string file in _batches)
            {
                /*Reading our original file and showing output in console*/

                Console.WriteLine("\n" + "Beginning Reading File {0}", file + "\n");

                using (StreamReader sr = File.OpenText(file))
                {
                    string pre_replace = "";
                    while ((pre_replace = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(pre_replace);
                    }
                    sr.Close(); 
                }
                                
                Console.WriteLine("\n" + "Finished Reading - Beginning Replacing");

                string text = "";
                using (StreamReader sr = new StreamReader(file))
                {                   
                   /*Forcing one interation I like my DO WHILE loops*/
                    do
                    { 
                   /*reading each line != null if we find our value, replace with blank then write each new line*/
                        string line = sr.ReadLine();
                        if (line != "")
                        {
                            line = line.Replace("K3*CERNFIN", "");
                            text = text + line + Environment.NewLine;
                        }
                    } while (sr.EndOfStream == false);
                }

                /*re writing the file after we replace our lines*/
                File.WriteAllText(file, text);

                Console.WriteLine("Finished Replacing" + "\n" + "Post Replace file" + "\n");

                /*reading our post replaced file and showing output in console*/
                using (StreamReader sr = File.OpenText(file))
                {
                    string post_replace = "";
                    while ((post_replace = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(post_replace);
                    }
                    sr.Close();
                }
                loopcount++;
            }
            watch.Stop();
            Console.WriteLine("\n" + "**************************");
            Console.WriteLine("*" + "Total files processed {0}", loopcount + "*");
            Console.WriteLine("*" + "Execution time: {0}", watch.ElapsedMilliseconds + "*");
            Console.WriteLine("**************************");
            Console.Read();
        }
    }
}

