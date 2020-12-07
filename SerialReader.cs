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
    public partial class SerialReader : Form
    {
        SerialPort[] port = new SerialPort[1];
        string pathtof;
        string Filename = "READYTORECORD";
        string data;
        bool WTF = false;
        int fileNum = 0;
        string prevFileName = "";
        string fullData = ";COM3;\n;COM4;";
        int timeEllapsed = 0;

        public SerialReader()
        {
            InitializeComponent();
            pathtof = Directory.GetCurrentDirectory();
            WriteLabel.Text = "Ready";
            pathtof += "\\Files\\" + DateTime.Now.ToString("dd.MM.yy HH-mm-ss");
            Directory.CreateDirectory(pathtof);
        }


        private void SerialReader_Load(object sender, EventArgs e)
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
        }


        void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Invoke(new EventHandler(
                delegate
                {
                    timeEllapsed++;
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
                            data = data.Replace("\r\n", "Ellapsed:" + timeEllapsed + " " + port[i].PortName + "\r\n");
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
                        timeEllapsed = 0;
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
                timeEllapsed = 0;
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
                if (curContent == ";COM3;\n;COM4;" || !curContent.Contains("\n"))
                {
                    File.Delete(pathToCheck);
                }
                else
                {
                    curContent = curContent.Replace("\r\n;COM3;", "");
                    curContent = curContent.Replace("\r\n;COM4;", "");
                    curContent = curContent.Replace("\r\n\n", "\r\n");
                    curContent = curContent.Trim();
                    File.WriteAllText(pathToCheck, curContent);
                }
            }
        }


        public class dataPoint
        {
            public double[] data;


            public dataPoint(string dataLine) 
            {
                string[] a = dataLine.Split('|');
                data = new double[a.Length];
                for (int i = 0; i < a.Length; i++)
                {
                        a[i]=a[i].Replace(",", ".");
                    data[i]= double.Parse(a[i],NumberStyles.Any, CultureInfo.InvariantCulture);
                }
                // data point contains values in order: aX, aY ,aZ, vX, vY, vZ, pX, pY, pZ, wQ, xQ, yQ, zQ, time, comPort;
                
                }
          

        }

        public string MidPoint(dataPoint first, dataPoint second, int magnitude)
        {
            StringBuilder outString = new StringBuilder();
            outString.Append(Math.Round((second.data[0] - first.data[0]) / magnitude, 5).ToString() + "|");
            outString.Append(Math.Round((second.data[1] - first.data[1]) / magnitude,5).ToString() + "|");
            outString.Append(Math.Round((second.data[2] - first.data[2]) / magnitude, 5).ToString() + "|");

            outString.Append(Math.Round((second.data[3] - first.data[3]) / magnitude, 5).ToString() + "|");
            outString.Append(Math.Round((second.data[4] - first.data[4]) / magnitude, 5).ToString() + "|");
            outString.Append(Math.Round((second.data[5] - first.data[5]) / magnitude, 5).ToString() + "|");

            outString.Append(Math.Round((second.data[6] - first.data[6]) / magnitude, 5).ToString() + "|");
            outString.Append(Math.Round((second.data[7] - first.data[7]) / magnitude, 5).ToString() + "|");
            outString.Append(Math.Round((second.data[8] - first.data[8]) / magnitude, 5).ToString() + "|");

            outString.Append(Math.Round((second.data[9] - first.data[9]) / magnitude, 5).ToString() + "|");
            outString.Append(Math.Round((second.data[10] - first.data[10]) / magnitude, 5).ToString() + "|");
            outString.Append(Math.Round((second.data[11] - first.data[11]) / magnitude, 5).ToString() + "|");
            outString.Append(Math.Round((second.data[12] - first.data[12]) / magnitude, 5).ToString() + "|");

            outString.Append(first.data[13]+"|");
            outString.Append(first.data[14]);
            return outString.ToString();
        }
        public string dataSumm(string first, string second) 
        {
            StringBuilder outString = new StringBuilder();
            dataPoint f = new dataPoint(first);
            dataPoint s = new dataPoint(second);
            for (int i = 0; i < f.data.Length - 2; i++)
            {
                outString.Append(f.data[i] + s.data[i]+"|");
            }
            outString.Append(f.data[13] + "|");
            outString.Append(f.data[14]);

            return outString.ToString();
        }

        private void ResizeDataButton_Click(object sender, EventArgs e)
        {
            string pathToData = Environment.CurrentDirectory + "\\Files\\Правая рука вверх с поворотом сидя 23-24-45";
            string[] files = Directory.GetFiles(pathToData, "*");
            resizeLabel.Text = "";
            foreach (string pathToParse in files)
            {
                List<dataPoint> fileDataPoints = new List<dataPoint>();
                List<dataPoint> COM3DataPoints = new List<dataPoint>();
                List<dataPoint> COM4DataPoints = new List<dataPoint>();
                string[] curFileToResize = File.ReadAllLines(pathToParse);
                double[,] ResizedData = new double[2000, 14];
                int PointsLength = 2000 / curFileToResize.GetLength(0);
                double[,] ParsedString = new double[curFileToResize.Length, 14];
                for (int i = 0; i < curFileToResize.Length; i++)
                {
                    if (curFileToResize[i] == "")
                    {
                        continue;
                    }
                    curFileToResize[i] = curFileToResize[i].Replace("COM", "1 ");
                    curFileToResize[i] = curFileToResize[i].Replace(" ", ",");
                    curFileToResize[i] = curFileToResize[i].Replace(",", "|");
                    
                    string[] a = curFileToResize[i].Split('|');
                    if (a.Length < ParsedString.GetLength(1)+1 || a.Length > ParsedString.GetLength(1)+1)
                    {
                        continue;
                    }
                    fileDataPoints.Add(new dataPoint(curFileToResize[i])); 
                }
                //its before after this line
                for (int i = 0; i < fileDataPoints.Count-1; i++)
                {
                    if (fileDataPoints[i].data[14] == 3)
                    {
                        COM3DataPoints.Add(fileDataPoints[i]);
                    }
                    if (fileDataPoints[i].data[14] == 3 && fileDataPoints[i + 1].data[14] == 3)
                    {
                        string step = MidPoint(fileDataPoints[i], fileDataPoints[i + 1], PointsLength);
                        string CurData = dataSumm(MidPoint(fileDataPoints[i], fileDataPoints[i], 1), step);
                        for (int k = 0; k < PointsLength - 1; k++)
                        {
                            CurData = dataSumm(MidPoint(new dataPoint(CurData), new dataPoint(step), PointsLength), step);
                            COM3DataPoints.Add(new dataPoint(dataSumm(CurData, step)));
                        }
                    }
                    if (fileDataPoints[i].data[14] == 4)
                    {
                        COM4DataPoints.Add(fileDataPoints[i]);
                    }
                    if (fileDataPoints[i].data[14] == 4 && fileDataPoints[i + 1].data[14] == 4)
                    {
                        string step = MidPoint(fileDataPoints[i], fileDataPoints[i + 1], PointsLength);
                        string CurData = dataSumm(MidPoint(fileDataPoints[i], fileDataPoints[i],1), step);
                        for (int k = 0; k < PointsLength - 1; k++)
                        {
                            CurData = dataSumm(MidPoint(new dataPoint(CurData), new dataPoint(step), PointsLength), step);
                            COM4DataPoints.Add(new dataPoint(dataSumm(CurData, step)));
                        }
                    }
                }
                //its fine after this line
                int leftoverData = 2000 % (ParsedString.GetLength(0) - 1);
                for (int i = ResizedData.GetLength(0) - leftoverData; i < 2000; i++)
                {
                    for (int j = 0; j < ResizedData.GetLength(1); j++)
                    {
                        ResizedData[i, j] = ParsedString[ParsedString.GetLength(0) - 1, j];
                    }
                }
                string dir = pathToData + " Parsed";
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
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
            resizeLabel.Text = "Resizing done!";
        }
    }
}
