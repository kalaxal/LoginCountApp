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
            Console.WriteLine("Done");
            Console.ReadKey();
        }

        private static DateTime GetLastMonth()
        {
            DateTime currentDate = DateTime.Now;
            return currentDate.AddMonths(-1);
        }

        private static string[] GetFileList()
        {
            string filePath = GetPath();
            string[] files = Directory.GetFiles(@filePath, "*.txt");
            return files;
        }

        private static string GetPath()
        {
            string[] config = File.ReadAllLines(@"CountAppConfig.txt");
            return config[0];
        }

        private static string[,] GetComputerList()
        {
            string[] config = File.ReadAllLines(@"CountAppConfig.txt");

            string[,] computerConfig = new string[config.Length, 2];
            for (int i = 0; i < config.Length; i++)
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
                if (File.GetLastWriteTime(files[i]) > GetLastMonth()) {
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
                //ProcessLog(lines, files[i].Substring(files[i].LastIndexOf('\\'), files[i].IndexOf('.')));
                ProcessLog(lines, Path.GetFileNameWithoutExtension(files[i]));
            }
        }

        private static void ProcessLog(string[] lines, string user)
        {
            //int line = 0;
            int loginIndex = 0;
            int logoutIndex = 0;
            string[,] computers = GetComputerList();

            while (/*line*/logoutIndex > -1)
            {
                //Console.Write(".");
                loginIndex = Array.IndexOf(lines, "Login ", logoutIndex); //get index of word login
                logoutIndex = Array.IndexOf(lines, "Logout ", loginIndex);

                GetCurrLog(user, computers, lines, loginIndex, logoutIndex);

                //line = Array.IndexOf(lines, "Login ", logoutIndex);
            }
        }

        private static bool GetCurrLog(string user, string[,] computers, string[] lines, int loginIndex, int logoutIndex)
        {
            bool compIsValid = false;
            bool dateIsValid = false;
            string computerName = "";
            string computerLocation = "";
            DateTime logIn = DateTime.Today;

            for (int currLine = loginIndex; currLine < logoutIndex; currLine++)
            {
                for (int comp = 0; comp < computers.GetLength(0); comp++)
                {
                    //if lines[currLine].Trim() matches patter add to object


                    if (computers[comp, 0].Equals(lines[currLine].Trim()) && compIsValid == false)
                    {
                        // If the computer is on list record it if not ignore it
                        //TODO find a way to break out if it is found
                        computerName = computers[comp, 0];
                        computerLocation = computers[comp, 1];
                        compIsValid = true;
                    }
                }

                //DateTime foundDate = Convert.ToDateTime(lines[currLine].Substring(lines[currLine].IndexOf(' ') + 1)); //find space grab all after

                if (ValidDate(lines[currLine].Substring(lines[currLine].IndexOf(' ') + 1)))
                {
                    //add
                    logIn = Convert.ToDateTime(lines[currLine].Substring(lines[currLine].IndexOf(' ') + 1) + lines[currLine + 1]);
                    dateIsValid = true;
                }
            }
            if (compIsValid && dateIsValid)
            {

                //TODO write data to db or csv
                string LogLine = user + ", " + computerName + ", " + computerLocation + ", " + logIn;
                Console.WriteLine(LogLine);
                return true;
            }
            return false;
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
    }
}
