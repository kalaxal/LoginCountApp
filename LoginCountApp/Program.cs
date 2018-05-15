using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LoginCountApp
{
    class Program
    {
        static void Main(string[] args)
        {

            SearchFiles();


            Console.ReadKey();
        }

        private static void SearchFiles()
        {
            DateTime currentDate = DateTime.Now;
            DateTime previousMonth = currentDate.AddMonths(-1);

            DateTime fileModified = File.GetLastWriteTime("current file being scanned");
            string filePath = @"D:\Trey\Documents\School 2017-2018\WorkStudy\users\";
            string[] files = Directory.GetFiles(filePath, "*.txt");
            string userLog;
            string userLogName;
            for (int i = 0; i < files.Length; ++i)
            {
                int count = 0;
                userLog = Path.GetFileName(files[i]);
                if (File.GetLastWriteTime(filePath + userLog) < previousMonth)
                {
                    //Only scans files that have been modified in the last month
                }
                
                userLogName = Path.GetFileNameWithoutExtension(files[i]);
                string[] lines = File.ReadAllLines (filePath + userLog);
                // process text
                Sanitize(lines);


                /*
                for(int line = 0; line < lines.Length - 1; ++line)
                {
                    string newLine = lines[line].ToLower().Trim();
                    userLogName = userLogName.ToLower().Trim();
                     
                    if (newLine.Equals(userLogName))
                    {
                        string nextLine = lines[line + 1];
                        string foundDate = nextLine.Substring(nextLine.IndexOf(' ') + 1); //find space grab all after

                        // Need to create a check to make sure the next line is a date
                        if(IsDate(foundDate))
                        {
                            DateTime date = Convert.ToDateTime(foundDate); //then convert to datetime
                            if (date.Year == year && date.Month == month)
                            {
                                ++count;
                            }
                            //Console.WriteLine($"User: {userLogName} NextLine: {nextLine} Year: {date.Year} Month: {date.Month} ");
                        }
                        
                        
                    }
                }*/
                Console.WriteLine(userLog + count);
            }

        }

        private static void Sanitize(string[] lines)
        {
            int line = 0;
            while (line < lines.Length)
            {
                if(lines[line].Equals("Login"))
                {

                }
                ++line;
            }
            //Console.WriteLine(lines);
            //int lineLength = lines.Length;
            //lines.Split(string["Logout"]);
            //string block = lines.Substring(lines.IndexOf("Login"), lines.IndexOf("Logoff"));
            //Console.WriteLine(block);
        }

        private static bool IsDate(string input)
        {
            DateTime results;
            if (DateTime.TryParse(input, out results))
            {
                return true;
            }
            return false;
        }
    }
}
