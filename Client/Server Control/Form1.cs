using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Haha
{
    public partial class Form1 : Form
    {
        TcpClient tcpClient;
        NetworkStream myns;
        StreamWriter mysw;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendCommand("shutdown");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendCommand("monke");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SendCommand("minecraft");
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            SendCommand(link.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SendCommand("stop");
        }

        void SendCommand(string command)
        {
            try
            {
                if (tcpClient != null)
                {
                    myns = tcpClient.GetStream();
                    mysw = new StreamWriter(myns);

                    Thread.Sleep(50);

                    mysw.WriteLine(command);

                    mysw.Flush();

                    responses.Text = "Sent command!";
                }
                else
                {
                    MessageBox.Show("TCP Client hasn't been started yet.");
                }
            }
            catch(Exception ex)
            {
                responses.Text = "Failed to send command.";
                MessageBox.Show("Failed to send command:\n\n" + ex.ToString());

                mysw.Flush();
            }
        }

        bool Connect(string IP, int port)
        {
            try
            {
                tcpClient = new TcpClient(IP, port);

                int size = Marshal.SizeOf((uint)0);

                byte[] keepAlive = new byte[size * 3];

                Buffer.BlockCopy(BitConverter.GetBytes((uint)1), 0, keepAlive, 0, size);
                Buffer.BlockCopy(BitConverter.GetBytes((uint)5000), 0, keepAlive, size, size);
                Buffer.BlockCopy(BitConverter.GetBytes((uint)5000), 0, keepAlive, size * 2, size);

                tcpClient.Client.IOControl(IOControlCode.KeepAliveValues, keepAlive, null);

                return true;
            }
            catch (SocketException ex) {
                responses.Text = "Failed to connect! Please try again.";
                MessageBox.Show("Failed to connect! The connection either refused or timed out.\n\n" + ex.ToString());

                return false;
            }
            catch (Exception ex)
            {
                responses.Text = "An unknown error occured.";
                MessageBox.Show("An unknown error occured:\n\n" + ex.ToString());

                return false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string IP = ipBox.Text;

            responses.Text = "Starting...";

            bool status = Connect(IP, 5020);

            if (status == true)
            {
                responses.Text = "Started TCP client!";
            }
            else
            {
                responses.Text = "Failed to start TCP client!";
            }
            Thread.Sleep(1000);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tcpClient != null)
            {
                myns.Close();
                mysw.Close();
                tcpClient.Close();
            }
        }
    }
}