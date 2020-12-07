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
            public double aX;
            public double aY;
            public double aZ;
            public double vX;
            public double vY;
            public double vZ;
            public double pX;
            public double pY;
            public double pZ;
            public double wQ;
            public double xQ;
            public double yQ;
            public double zQ;
            public int time;
            public int comPort;
            public dataPoint(string dataLine) 
            {
                string[] a = dataLine.Split('|');
                aX= double.Parse(a[0], CultureInfo.InvariantCulture);
                aY = double.Parse(a[1], CultureInfo.InvariantCulture);
                aZ = double.Parse(a[2], CultureInfo.InvariantCulture);
                vX = double.Parse(a[3], CultureInfo.InvariantCulture);
                vY = double.Parse(a[4], CultureInfo.InvariantCulture);
                vZ = double.Parse(a[5], CultureInfo.InvariantCulture);
                pX = double.Parse(a[6], CultureInfo.InvariantCulture);
                pY = double.Parse(a[7], CultureInfo.InvariantCulture);
                pZ = double.Parse(a[8], CultureInfo.InvariantCulture);
                wQ = double.Parse(a[9], CultureInfo.InvariantCulture);
                xQ = double.Parse(a[10], CultureInfo.InvariantCulture);
                yQ = double.Parse(a[11], CultureInfo.InvariantCulture);
                zQ = double.Parse(a[12], CultureInfo.InvariantCulture);
                time = int.Parse(a[13], CultureInfo.InvariantCulture);
                comPort = int.Parse(a[14], CultureInfo.InvariantCulture);
        }
          
        }

        public dataPoint MidPoint(dataPoint first, dataPoint second, int magnitude)
        {
            string outString = "";
            outString  += (first.aX - second.aX) / magnitude +"|";
            outString += (first.aY - second.aY) / magnitude + "|";
            outString += (first.aZ - second.aZ) / magnitude + "|";

            outString += (first.vX - second.vX) / magnitude + "|";
            outString += (first.vY - second.vY) / magnitude + "|";
            outString += (first.vZ - second.vZ) / magnitude + "|";

            outString += (first.pX - second.pX) / magnitude + "|";
            outString += (first.pY - second.pY) / magnitude + "|";
            outString += (first.pZ - second.pZ) / magnitude + "|";

            outString += (first.wQ - second.wQ) / magnitude + "|";
            outString += (first.xQ - second.xQ) / magnitude + "|";
            outString += (first.yQ - second.yQ) / magnitude + "|";
            outString += (first.zQ - second.zQ) / magnitude + "|";
            dataPoint result = new dataPoint(outString);
            return result;
        }

        private void ResizeDataButton_Click(object sender, EventArgs e)
        {
            string pathToData = Environment.CurrentDirectory + "\\Files\\Правая рука вверх с поворотом сидя 23-24-45";
            string[] files = Directory.GetFiles(pathToData, "*");
            resizeLabel.Text = "";
            foreach (string pathToParse in files)
            {
                List<dataPoint> fileDataPoints = new List<dataPoint>();
                List<dataPoint> resizedDataPoints = new List<dataPoint>();
                string[] curFileToResize = File.ReadAllLines(pathToParse);
                double[,] ResizedData = new double[2000, 14];
                int PointsLength = ResizedData.GetLength(0) / curFileToResize.GetLength(0);
                double[,] ParsedString = new double[curFileToResize.Length, 14];
                for (int i = 0; i < curFileToResize.Length; i++)
                {
                    if (curFileToResize[i] == "")
                    {
                        continue;
                    }
                    curFileToResize[i] = curFileToResize[i].Replace("COM", "");
                    curFileToResize[i] = curFileToResize[i].Replace(" ", ",");
                    curFileToResize[i] = curFileToResize[i].Replace(",", "|");
                    string[] a = curFileToResize[i].Split('|');
                    if (a.Length < ParsedString.GetLength(1) || a.Length > ParsedString.GetLength(1))
                    {
                        continue;
                    }
                    fileDataPoints.Add(new dataPoint(curFileToResize[i])); 
                }

                for (int i = 0; i < fileDataPoints.Count- 1; i++)
                {

                    for (int j = 0; j < ParsedString.GetLength(1) - 1; j++)
                    {
                        resizedDataPoints.Add(fileDataPoints[i]);
                        for (int k = 0; k < PointsLength; k++)
                        {
                            resizedDataPoints.Add(MidPoint(fileDataPoints[i+k], fileDataPoints[i + 1+k],PointsLength));
                        }
                    }
                }
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
