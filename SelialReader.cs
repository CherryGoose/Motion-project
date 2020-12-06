using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Text;

namespace Motion_Project
{
    public partial class SelialReader : Form
    {
        SerialPort[] port = new SerialPort[1];
        string pathtof;
        string Filename = "READYTORECORD";
        string data;
        bool WTF = false;
        int fileNum = 0;
        string prevFileName = "";
        string fullData = ";COM3;\n;COM4;";

        public SelialReader()
        {
            InitializeComponent();
            pathtof = Directory.GetCurrentDirectory();
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
            port = new SerialPort[SerialPort.GetPortNames().Length];
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
                else if (port[i].IsOpen == true)
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

        private void ResizeDataButton_Click(object sender, EventArgs e)
        {
            string pathToData = Environment.CurrentDirectory + "\\Files\\Правая рука вверх с поворотом сидя 23-24-45";
            string[] files = Directory.GetFiles(pathToData, "*");
            foreach (string pathToParse in files)
            {
                string[] curFileToResize = File.ReadAllLines(pathToParse);
                double[,] ParsedString = new double[curFileToResize.Length, 14];
                for (int i = 0; i < curFileToResize.Length; i++)
                {
                    if (curFileToResize[i] == "")
                    {
                        continue;
                    }
                    curFileToResize[i] = curFileToResize[i].Replace("COM", "");
                    curFileToResize[i] = curFileToResize[i].Replace(" ", ",");
                    string[] a = curFileToResize[i].Split(',');
                    if (a.Length < ParsedString.GetLength(1) || a.Length > ParsedString.GetLength(1))
                    {
                        continue;
                    }
                    for (int j = 0; j < a.Length; j++)
                    {
                        ParsedString[i, j] = double.Parse(a[j], CultureInfo.InvariantCulture);
                    }
                }
                double[,] ResizedData = new double[2000, 14];
                int PointsLength = ParsedString.GetLength(0);
                PointsLength = ResizedData.GetLength(0) / PointsLength;
                for (int i = 0; i < ResizedData.GetLength(0); i++)
                {
                    for (int j = 0; j < ResizedData.GetLength(1); j++)
                    {
                        ResizedData[i, j] = 777;
                    }
                }
                for (int j = 0; j < ResizedData.GetLength(1); j++)
                {
                    int parsedSTRCounter = 0;
                    double nextValue = 0;

                    for (int i = 0; i < ResizedData.GetLength(0); i++)
                    {
                        if (i > parsedSTRCounter * PointsLength || parsedSTRCounter + 1 >= ParsedString.GetLength(0))
                        {
                            ResizedData[i, j] = ParsedString[parsedSTRCounter, j];
                            continue;
                        }
                        double Val = Math.Abs(ParsedString[parsedSTRCounter, j] > ParsedString[(parsedSTRCounter + 1), j] ?
                            ParsedString[parsedSTRCounter, j] - ParsedString[(parsedSTRCounter + 1), j]
                            : ParsedString[(parsedSTRCounter + 1), j] - ParsedString[parsedSTRCounter, j]);
                        int signVal;
                        if (ParsedString[parsedSTRCounter, j] < ParsedString[(parsedSTRCounter + 1), j])
                            signVal = 1;
                        else signVal = -1;
                        nextValue += signVal * Val / PointsLength;
                        ResizedData[i, j] = Math.Round(nextValue, 4);
                        if (parsedSTRCounter * PointsLength == i && parsedSTRCounter < ParsedString.GetLength(0))
                        {
                            ResizedData[i, j] = ParsedString[parsedSTRCounter, j];
                            nextValue = ParsedString[parsedSTRCounter, j];
                            parsedSTRCounter++;
                        }
                    }
                }
                string dir = pathToData + " Parsed";
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                else
                {
                    StringBuilder builder = new StringBuilder();
                    for (int j = 0; j < ResizedData.GetLength(0); j++)
                    {
                        for (int i = 0; i < ResizedData.GetLength(1); i++)
                        {
                            builder.Append(ResizedData[j, i]);
                            builder.Append(" ");
                        }
                        builder.Append("\r\n");
                    }
                    File.WriteAllText(dir + "\\" + Path.GetFileName(pathToParse), builder.ToString());
                }
            }
        }
    }
}
