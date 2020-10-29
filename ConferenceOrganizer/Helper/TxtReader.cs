using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ConferenceOrganizer.Helper
{
    public  class TxtReader
    {


        public static List<string> ReadTxtFile(string Path)
        {
            List<string> FileContents = new List<string>();
            try
            {
              

            using StreamReader sr = new StreamReader(Path);
            string line;
            /*Read and display lines from the file until 
             the end of the file is reached. */

            while ((line = sr.ReadLine()) != null  )
            {
                    if (line.Length > 1)
                    { FileContents.Add(line); }
               

            }

            return FileContents;

            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                return FileContents;

            }
        }


      public static int FindDuration(string line )
        {

           int duration = 0;
           
            Regex re = new Regex(@"(\d+)");
            Match result = re.Match(line);


            if (Regex.IsMatch(result.Value, @"(\d+)"))
            {

                duration = int.Parse(result.Value);

            }
            else if (line.Contains("lightning"))
            {
                duration = 5;

            }

            return duration;

        }
        

    }
}
