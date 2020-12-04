using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace Motion_Project
{
    public partial class SelialReader : Form
    {
        SerialPort[] port= new SerialPort[1];
        string pathtof;
        string Filename = "READYTORECORD";
        string data;
        bool WTF=false;
        int fileNum = 0;
        string prevFileName = "";
        string fullData = ";COM3;\n;COM4;";

        public SelialReader()
        {
            InitializeComponent();
            pathtof=Directory.GetCurrentDirectory();
            WriteLabel.Text = "Ready";
            pathtof += "\\Files\\" + DateTime.Now.ToString("dd.MM.yy HH-mm-ss");
            Directory.CreateDirectory(pathtof);
        }
     

        private void SelialReader_Load(object sender, EventArgs e)
        {
            SetupControls();
        }
        private void SetupControls()
        {
            ClosePort.Enabled = false;
            port= new SerialPort[SerialPort.GetPortNames().Length];
            for (int i = 0; i < SerialPort.GetPortNames().Length; i++)
            {
                port[i] = new SerialPort();

            }
            foreach (string str in SerialPort.GetPortNames())
            {
                PortBox.Items.Add(str);
            }
            foreach (string str in Enum.GetNames(typeof(Parity)))
                Paritybox.Items.Add(str);

            if (PortBox.Items.Contains("COM1"))
                PortBox.SelectedItem = "COM1";
            else PortBox.SelectedIndex = 0;

            Paritybox.SelectedIndex = 0;
            StopBits.SelectedIndex = 0;
            BaudBox.SelectedIndex = 9;
            DataBits.SelectedIndex = 0;
           // TerminationBox.SelectedIndex = 0;
        }
     
     
        void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Invoke(new EventHandler(
                delegate
                {
                    bool allSensorsIdle = true;
                    for (int i = 0; i < port.Length; i++)
                    {
                        if (!port[i].IsOpen)
                        {
                            continue;
                        }
                        if (i == 1)
                        {
                            rtbDisplay.SelectionColor = Color.Blue;
                        }
                        else
                        {
                            rtbDisplay.SelectionColor = Color.Red;
                        }
                        data = port[i].ReadExisting();
                        if (!data.Contains("Split"))
                        {
                            rtbDisplay.AppendText(data);
                        }
                        else
                        {
                            rtbDisplay.ScrollToCaret();
                        }
                        if (!WTF)
                        {
                            continue;
                        }
                        if (!data.Contains("Split"))
                        {
                            allSensorsIdle = false;
                            data = data.Replace("\r\n", " " + port[i].PortName + "\r\n");
                            string signature = ";" + port[i].PortName + ";";
                            data += signature;
                            fullData = fullData.Replace(signature, data);
                            if (!fullData.Equals(";COM3;\n;COM4;"))
                            {
                                labelWritingDone.Text = "WRITING";
                                File.WriteAllText(pathtof + "\\" + Filename + ".txt", fullData);
                                if (!prevFileName.Equals(Filename))
                                {
                                    fileNum++;
                                    prevFileName = Filename;
                                }
                            }
                        }
                    }
                    if (allSensorsIdle)
                    {
                        labelWritingDone.Text = "STANDBY";
                        fullData = ";COM3;\n;COM4;";
                        Filename = fileNum.ToString();
                    }
                }));
        }

        private void ClosePort_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < port.Length; i++)
            {
                if (port[i].PortName == "COM1")
                    continue;
                port[i].Close();
            }

            ClosePort.Enabled = false;
            OpenPort.Text = "Open Port";
            rtbDisplay.SelectionColor = Color.Red;
            rtbDisplay.AppendText("closed!\n");
            rtbDisplay.ScrollToCaret();
        }

        private void Send_Click(object sender, EventArgs e)
        {
          /*  string lineTermination = string.Empty;

            if (!port.IsOpen)
            {
                rtbDisplay.SelectionColor = Color.Red;
                rtbDisplay.AppendText("Error: Not connected to Port! Establish connection first.\n");
                  rtbDisplay.ScrollToCaret();
                return;
            }

            rtbDisplay.SelectionColor = Color.Green;
            switch (TerminationBox.SelectedIndex)
            {
                case 0:
                    lineTermination = string.Empty;
                    break;
                case 1:
                    lineTermination = "\n";
                    break;
                case 2:
                    lineTermination = "\r";
                    break;
                case 3:
                    lineTermination = "\n\r";
                    break;
                default:
                    break;
            }
            rtbDisplay.AppendText("TX: " + SendText.Text + "\n");
            port.Write(SendText.Text + lineTermination);
            rtbDisplay.ScrollToCaret();*/
        }

        private void OpenPort_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < port.Length; i++)
            {
                if (port[i].IsOpen == false)
                {
                    port[i].PortName = SerialPort.GetPortNames()[i];
                    port[i].BaudRate = int.Parse(BaudBox.SelectedItem.ToString());

                    port[i].DataBits = int.Parse(DataBits.SelectedItem.ToString());

                    port[i].StopBits = (StopBits)Enum.Parse(typeof(StopBits),
                                                StopBits.SelectedItem.ToString());
                    port[i].Parity = (Parity)Enum.Parse(typeof(Parity),
                                                Paritybox.SelectedItem.ToString());

                    if (port[i].PortName != "COM1")
                    {
                         port[i].Open();
                            port[i].DataReceived +=
                             new SerialDataReceivedEventHandler(port_DataReceived);
                            ClosePort.Enabled = true;
                        
                    }
                    rtbDisplay.ScrollToCaret();
                }
                else                if (port[i].IsOpen == true)
                {
                    OpenPort.Text = "Refresh Port";
                    ClosePort.Enabled = true;
                    rtbDisplay.SelectionColor = Color.Red;
                    rtbDisplay.AppendText(port[i].PortName + " opened!\n");
                    rtbDisplay.ScrollToCaret();
                }
            }

        }

        private void WriteToFile_Click(object sender, EventArgs e)
        {
            if (WTF)
            {
                WTF = false;
                Filename = "READYTORECORD";
                WriteLabel.Text = "Ready";                
            }
            else 
            {
                WTF = true;
                WriteLabel.Text = "Writing";
            }
        }

        private void buttonFixTrash_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(tBoxTrashFolder.Text, "*");
            foreach (string pathToCheck in files)
            {
                string curContent = File.ReadAllText(pathToCheck);
                if (curContent == ";COM3;\n;COM4;")
                {
                    File.Delete(pathToCheck);
                }
                else
                {
                    curContent = curContent.Replace(";COM3;", "");
                    curContent = curContent.Replace(";COM4;", "");
                    File.WriteAllText(pathToCheck, curContent);
                }
            }
        }
    }
}
