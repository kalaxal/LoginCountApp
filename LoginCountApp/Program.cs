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
            GetLastMonth();

            if (IsValid())
            {
                if ()
                {
                }
            }

            Console.ReadKey();
        }

        private static DateTime GetLastMonth()
        {
            DateTime currentDate = DateTime.Now;
            return currentDate.AddMonths(-1);
        }

        private static bool IsValid()
        {
            

            RecentlyModified()
        }

        private static string[] GetFileList()
        {
            string filePath = GetPath();
            string[] files = Directory.GetFiles(filePath, "*.txt");
            return files;
        }

        private static string GetPath()
        {
            string[] config = File.ReadAllLines(@"D:\Trey\Documents\School 2017-2018\WorkStudy\CountAppConfig.txt");
            return config[0];
        }

        private static string[,] GetComputerList()
        {
            string[] config = File.ReadAllLines(@"D:\Trey\Documents\School 2017-2018\WorkStudy\CountAppConfig.txt");

            string[,] computerConfig = new string[config.Length, 2];
            for (int i = 1; i < config.Length; i++)
            {
                string[] temp = config[i].Split(' ');
                computerConfig[i,0] = temp[0];
                computerConfig[i,1] = temp[1];

            }
            return computerConfig;
        }

        private static List<string> RecentlyModified()
        {
            string[] files = GetFileList();
            List<string> currentFiles = new List<string>();
            for (int i = 0; i < files.Length; i++)
            {
                if (File.GetLastWriteTime(files[i]) < GetLastMonth()) {
                    currentFiles.Add(files[i]);
                }
            }
            return currentFiles;
        }

        private static void SearchFiles()
        {
            List<string> files = RecentlyModified();
            for (int i = 0; i < files.Count; i++)
            {
                string[] lines = File.ReadAllLines(files[i]);
                Sanitize(lines);
            }


        }

        private static void Sanitize(string[] lines, string user)
        {
            //string[,,] log = new string[];
            int line = 0;
            int loginIndex = 0;
            int logoutIndex = 0;
            bool computer = false;
            bool date = false;
            string[,] computers = GetComputerList();
            while (line < lines.Length)
            {
                int start = lines[line].IndexOf("Login", logoutIndex); //get index of word login
                int end = lines[line].IndexOf("Logout", loginIndex);
                for (int i = start; i < end; i++)
                {
                    if (computers[i,0].Contains(lines[line]))
                    {
                        // If the computer is on list record it if not ignore it
                        computer = true;
                    }
                    string foundDate = lines[line].Substring(lines[line].IndexOf(' ') + 1); //find space grab all after

                    if (ValidDate(foundDate))
                    {
                        date = true;
                    }
                }
                if (computer && date)
                {
                    // take note
                }
                ++line;
            }
        }

        private static bool ValidDate(string input)
        {
            if (IsDate(input))
            {
                DateTime date = Convert.ToDateTime(input); //then convert to datetime
                if (date > GetLastMonth())
                {
                    return true;
                }
                //Console.WriteLine($"User: {userLogName} NextLine: {nextLine} Year: {date.Year} Month: {date.Month} ");
            }
            return false;
        }

        private static bool IsDate(string input)
        {
            try
            {
                Convert.ToDateTime(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /*
        string userLog;
            string userLogName;
            for (int i = 0; i < files.Length; ++i)
            {
                int count = 0;
                userLog = Path.GetFileName(files[i]);
                
                userLogName = Path.GetFileNameWithoutExtension(files[i]);


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
                }
                Console.WriteLine(userLog + count);
            }

        }*/


    }
}
