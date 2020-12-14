using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MathNet.Numerics.Statistics;

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
                    a[i] = a[i].Replace(",", ".");
                    data[i] = double.Parse(a[i],NumberStyles.Any, CultureInfo.InvariantCulture);
                }
                // data point contains values in order: aX, aY ,aZ, vX, vY, vZ, pX, pY, pZ, wQ, xQ, yQ, zQ, time, comPort;                
            }

            public dataPoint(double[] data)
            {
                this.data = data;
            }
        }

        public string MidPoint(dataPoint first, dataPoint second, int magnitude)
        {
            StringBuilder outString = new StringBuilder();
            for (int i = 0; i < first.data.Length - 2; i++)
            {
                outString.Append(Math.Round((second.data[i] - first.data[i]) / magnitude, 5).ToString() + "|");
            }
            outString.Append(first.data[13]+"|");
            outString.Append(first.data[14]);
            return outString.ToString();
        }

        public string dataSumm(dataPoint first, dataPoint second) 
        {
            StringBuilder outString = new StringBuilder();
            for (int i = 0; i < first.data.Length - 2; i++)
            {
                outString.Append(first.data[i] + second.data[i]+"|");
            }
            outString.Append(first.data[13] + "|");
            outString.Append(first.data[14]);

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
                    if (a.Length < ParsedString.GetLength(1) + 1 || a.Length > ParsedString.GetLength(1) + 1)
                    {
                        continue;
                    }
                    fileDataPoints.Add(new dataPoint(curFileToResize[i]));
                }

                //clear all but com3 data
                for (int i = 0; i < fileDataPoints.Count; i++)
                {
                    int index;
                    if (fileDataPoints.Exists(x => x.data[14] == 4))
                    {
                        index = fileDataPoints.FindIndex(x => x.data[14] == 4);
                        fileDataPoints.RemoveAt(index);
                    }
                }

                int PointsLength = 1000 / fileDataPoints.Count;

                for (int i = 0; i < fileDataPoints.Count - 1; i++)
                {
                    if (fileDataPoints[i].data[14] == 3)
                    {
                        COM3DataPoints.Add(new dataPoint(fileDataPoints[i].data));
                    }
                    if (fileDataPoints[i].data[14] == 3 && fileDataPoints[i + 1].data[14] == 3)
                    {
                        dataPoint st = new dataPoint(MidPoint(fileDataPoints[i], fileDataPoints[i + 1], PointsLength));
                        dataPoint CurData = new dataPoint(fileDataPoints[i].data);
                        for (int k = 0; k < PointsLength - 1; k++)
                        {
                            dataPoint temp = new dataPoint(dataSumm(CurData, st));
                            COM3DataPoints.Add(temp);
                            CurData = new dataPoint(temp.data);
                        }
                    }
               
                }
                if (COM3DataPoints.Count < 1000) 
                { int border = COM3DataPoints.Count;
                    for (int i = 0; i < 1000 - border; i++)
                    {
                        dataPoint extraFill = new dataPoint(COM3DataPoints[COM3DataPoints.Count - 1].data);
                        COM3DataPoints.Add(extraFill);
                    }
                }
              
                string dir = pathToData + " Parsed";
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < COM3DataPoints.Count; i++)
                {
                    for (int j = 0; j < COM3DataPoints[i].data.Length-2; j++)
                    {
                        builder.Append(COM3DataPoints[i].data[j].ToString());
                        builder.Append("|");
                    }
                    builder.Append("\r\n");
                }
                File.WriteAllText(dir + "\\" + Path.GetFileName(pathToParse), builder.ToString());

                string HorPath = pathToData + " Horrizontal";
                if (!Directory.Exists(HorPath))
                    Directory.CreateDirectory(HorPath);
                StringBuilder HorBuilder = new StringBuilder();
                for (int j = 0; j< COM3DataPoints[j].data.Length - 2; j++)
                {
                    for (int i = 0; i < COM3DataPoints.Count; i++)
                    {
                        HorBuilder.Append(COM3DataPoints[i].data[j].ToString());
                        HorBuilder.Append("|");
                    }
                    HorBuilder.Append("\r\n");
                }
                File.WriteAllText(HorPath + "\\" + Path.GetFileName(pathToParse), HorBuilder.ToString());

            }
            resizeLabel.Text = "Resizing done!";
        }

        private void CorrelButton_Click(object sender, EventArgs e)
        {
            string pathToData = Environment.CurrentDirectory + "\\Files\\Правая рука вверх с поворотом сидя 23-24-45 Horrizontal";
            string[] files = Directory.GetFiles(pathToData, "*");
            List<dataPoint> Ax = new List<dataPoint>();
            List<dataPoint> Ay = new List<dataPoint>();
            List<dataPoint> Az = new List<dataPoint>();
            List<dataPoint> Vx = new List<dataPoint>();
            List<dataPoint> Vy = new List<dataPoint>();
            List<dataPoint> Vz = new List<dataPoint>();
            List<dataPoint> Px = new List<dataPoint>();
            List<dataPoint> Py = new List<dataPoint>();
            List<dataPoint> Pz = new List<dataPoint>();
            List<dataPoint> W = new List<dataPoint>();
            List<dataPoint> Rx = new List<dataPoint>();
            List<dataPoint> Ry = new List<dataPoint>();
            List<dataPoint> Rz = new List<dataPoint>();

            foreach (string pathToFile in files)
            {
                string[] CurFileToAccess = File.ReadAllLines(pathToFile);
                for (int i = 0; i < CurFileToAccess.Length; i++)
                {
                    CurFileToAccess[i] = CurFileToAccess[i].Remove(CurFileToAccess[i].Length - 1);
                }
                        Ax.Add(new dataPoint(CurFileToAccess[0]));
                        Ay.Add(new dataPoint(CurFileToAccess[1])); 
                        Az.Add(new dataPoint(CurFileToAccess[2]));
                        Vx.Add(new dataPoint(CurFileToAccess[3]));
                        Vy.Add(new dataPoint(CurFileToAccess[4]));
                        Vz.Add(new dataPoint(CurFileToAccess[5]));
                        Px.Add(new dataPoint(CurFileToAccess[6]));
                        Py.Add(new dataPoint(CurFileToAccess[7]));
                        Pz.Add(new dataPoint(CurFileToAccess[8]));
                        W.Add(new dataPoint(CurFileToAccess[9]));
                        Rx.Add(new dataPoint(CurFileToAccess[10]));
                        Ry.Add(new dataPoint(CurFileToAccess[11]));
                        Rz.Add(new dataPoint(CurFileToAccess[12]));
            }
            List<double> avarageCorrel= new List<double>();
            for (int j = 0; j < Px.Count - 1; j++)
            {
                for (int i = 0; i < Px.Count - 1; i++)
                {
                    double correl = Correlation.Pearson(W[j].data, W[i].data);
                    avarageCorrel.Add(correl);
                }
            }
             double result = System.Linq.Enumerable.Average(avarageCorrel);
            rtbDisplay.AppendText(result.ToString());
                //to do: correl calculation
            }
    }
}
