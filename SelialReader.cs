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
        string[] portnames;
        string pathtof;
        string Filename = "READYTORECORD";
        string data;
        bool WTF=false;
        bool createNewfile = true;
        bool writeEvent = false;
        public SelialReader()
        {
            InitializeComponent();
            pathtof=Directory.GetCurrentDirectory();
            WriteLabel.Text = "Ready";
            pathtof += "\\Files";
        }
     

        private void SelialReader_Load(object sender, EventArgs e)
        {
            SetupControls();
        }
        private void SetupControls()
        {
            ClosePort.Enabled = false;
            port= new SerialPort[SerialPort.GetPortNames().Length];
            portnames = new string[SerialPort.GetPortNames().Length];
            for (int i = 0; i < SerialPort.GetPortNames().Length; i++)
            {
                port[i] = new SerialPort();

            }
            portnames = SerialPort.GetPortNames();
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
            this.Invoke(new EventHandler(
                delegate
                {
                    rtbDisplay.SelectionColor = Color.Blue;
                    for (int i = 1; i < port.Length; i++)
                    {
                        if (port[i].IsOpen)
                        {
                            //rtbDisplay.AppendText(port[i].PortName + "  ");
                            //rtbDisplay.AppendText(port[i].ReadExisting());
                            data = port[i].ReadExisting();
                            rtbDisplay.AppendText(data);
                            if (WTF)
                            {
                                
                                if (!File.Exists(pathtof+ "\\" + Filename + ".txt")&& createNewfile==true)
                                {
                                    Filename = DateTime.Now.ToLongDateString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                                    File.Create(pathtof + "\\" + Filename + ".txt").Dispose();

                                    File.AppendAllText(pathtof + "\\" + Filename + ".txt", "The very first line!");
                                    createNewfile = false;
                                    writeEvent = false;
                                    WriteLabel.Text = "Writing";
                                }
                                else if (File.Exists(pathtof + "\\" + Filename + ".txt"))
                                {
                                    if (data=="Split\r\n"&& writeEvent)
                                    {
                                        Filename = "READYTORECORD";
                                        WriteLabel.Text = "Ready";
                                        writeEvent = false;
                                        createNewfile = true;
                                    }
                                    else if (data != "" && createNewfile == false && data!= "Split\r\n")
                                    {
                                        File.AppendAllText(pathtof + "\\" + Filename + ".txt", data + "\r\n");
                                        writeEvent = true;
                                    }
                                    
                                   
                                     
                                    
                                    //File.AppendAllText(pathtof + "\\" + Filename + ".txt", port[i].PortName+"\r\n");
                                    /*  using (TextWriter tw = new StreamWriter(pathtof + "\\" + Filename + ".txt",true))
                                      {
                                          tw.WriteLine(port[i].PortName+" ");
                                          tw.WriteLine(data);
                                          tw.WriteLine(" Pass \n");
                                      }*/
                                }
                            }
                        }
                    }
                    
                   
                        
                   
                    rtbDisplay.ScrollToCaret();

                }));
          
                
        }

        private void ClosePort_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < port.Length; i++)
            {
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
                rtbDisplay.AppendText("Error: Not Connected to Port! Establish connection first.\n");
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
            else {
                WTF = true;
                WriteLabel.Text = "Writing";
            }
        }
    }
}
