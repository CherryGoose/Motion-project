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
using System.Xml;
using System.Xml.Linq;

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
        string fullData = ";COM6;\n;";
        int timeEllapsed = 0;
        string DirectoryName = "";


        public SerialReader()
        {
            InitializeComponent();
            WriteLabel.Text = "Ready";
            pathtof = Directory.GetCurrentDirectory()+"\\Files\\" + DateTime.Now.ToString("dd.MM.yy HH-mm-ss");
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
                            //data = data.Replace("\r\n", "Ellapsed:" + timeEllapsed + " " + port[i].PortName + "\r\n");
                            string signature = ";" + port[i].PortName + ";";
                            data += signature;
                            fullData = fullData.Replace(signature, data);
                            if (!fullData.Equals(";COM6;\n;"))
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
                        fullData = ";COM6;\n;";
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
                pathtof = Directory.GetCurrentDirectory()+"\\Files\\" + DateTime.Now.ToString("dd.MM.yy HH-mm-ss");
                Directory.CreateDirectory(pathtof);
            }
            else
            {
                timeEllapsed = 0;
                WTF = true;
                WriteLabel.Text = "Writing";
            }
        }
        private void tBoxTrashFolder_TextChanged(object sender, EventArgs e)
        {
            DirectoryName = Path.GetFileName(tBoxTrashFolder.Text);

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
                    
                    curContent = curContent.Replace("\r\n;COM6;", "");
                    curContent = curContent.Replace("\r\n;COM4;", "");
                    curContent = curContent.Replace("\r\n\n", "\r\n");
                    curContent = curContent.Replace(";", "");
                    curContent = curContent.Trim();
                    File.WriteAllText(pathToCheck, curContent);
                    string [] sizeFile = File.ReadAllLines(pathToCheck);
                    if (sizeFile.Length < 20)
                    {
                        File.Delete(pathToCheck);
                    }
                }
            }
            resizeLabel.Text = "Fixtrash Done!";
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
            for (int i = 0; i < first.data.Length; i++)
            {

                if (i == first.data.Length - 1)
                {
                    outString.Append(Math.Round((second.data[i] - first.data[i]) / magnitude, 5).ToString());
                }
                else { outString.Append(Math.Round((second.data[i] - first.data[i]) / magnitude, 5).ToString() + "|"); }
            }
            return outString.ToString();
        }

        public string dataSumm(dataPoint first, dataPoint second) 
        {
            StringBuilder outString = new StringBuilder();
            for (int i = 0; i < first.data.Length; i++)
            {
                if (i == first.data.Length - 1)
                {
                    outString.Append(first.data[i] + second.data[i]);
                }
                else
                {
                    outString.Append(first.data[i] + second.data[i] + "|");

                }
            }
                return outString.ToString();
        }

        private void CombiteXml_Click(object sender, EventArgs e)
        {
            string pathToData = Environment.CurrentDirectory + "\\Files\\XMLFiles";
            string[] files = Directory.GetFiles(pathToData, "*.xml"); 
            if (!Directory.Exists(Environment.CurrentDirectory + "\\Files\\All Classes data.xml"))
            {
                XmlWriterSettings wSettings = new XmlWriterSettings();
                wSettings.Indent = true;
                wSettings.ConformanceLevel = ConformanceLevel.Document;
                XmlWriter writer = XmlWriter.Create(Environment.CurrentDirectory + "\\Files\\All Classes data.xml", wSettings);
                writer.WriteProcessingInstruction("xml", "version='1.0' encoding='utf-8'");
                writer.WriteStartElement("Classes");
                writer.WriteAttributeString("lang", "ru");
                writer.WriteStartElement("Specification");
                writer.WriteAttributeString("description", "Распознание");
                writer.WriteStartElement("Feature");
                writer.WriteAttributeString("id", "1");
                writer.WriteAttributeString("description", "Ax");
                writer.WriteEndElement();
                writer.WriteStartElement("Feature");
                writer.WriteAttributeString("id", "2");
                writer.WriteAttributeString("description", "Ay");
                writer.WriteEndElement();
                writer.WriteStartElement("Feature");
                writer.WriteAttributeString("id", "3");
                writer.WriteAttributeString("description", "Az");
                writer.WriteEndElement();
                writer.WriteStartElement("Feature");
                writer.WriteAttributeString("id", "4");
                writer.WriteAttributeString("description", "q");
                writer.WriteEndElement();
                writer.WriteStartElement("Feature");
                writer.WriteAttributeString("id", "5");
                writer.WriteAttributeString("description", "w");
                writer.WriteEndElement();
                writer.WriteStartElement("Feature");
                writer.WriteAttributeString("id", "6");
                writer.WriteAttributeString("description", "s");
                writer.WriteEndElement();
                writer.WriteStartElement("Feature");
                writer.WriteAttributeString("id", "7");
                writer.WriteAttributeString("description", "n");
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteStartElement("Features");
                writer.WriteStartElement("Class");
                writer.WriteAttributeString("name", "Empty");
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.Flush();
                writer.Close();
            }
            var xml1 = XDocument.Load(Environment.CurrentDirectory + "\\Files\\All Classes data.xml");

            for (int i = 0; i < files.Length; i++)
            {
                if (i == 0)
                {
                    xml1.Descendants("Class").LastOrDefault().ReplaceWith(XDocument.Load(files[i]).Descendants("Class"));
                }
                else
                {
                    xml1.Descendants("Class").LastOrDefault().AddAfterSelf(XDocument.Load(files[i]).Descendants("Class"));
                }
            }

            xml1.Save(Environment.CurrentDirectory + "\\Files\\All Classes data.xml");

        }
        private void createXmlfiles_button_Click(object sender, EventArgs e)
        {
            XmlWriterSettings wSettings = new XmlWriterSettings();
            wSettings.Indent = true;
           ///
           wSettings.ConformanceLevel = ConformanceLevel.Document;
            //wSettings.OmitXmlDeclaration = true;
            //wSettings.Encoding = Encoding.UTF8;
            string nameOfFolder = DirectoryName;

            string pathToData = Environment.CurrentDirectory + "\\Files\\"+nameOfFolder+" resized generated";
            string[] files = Directory.GetFiles(pathToData, "*");
            XmlWriter writer = XmlWriter.Create(Environment.CurrentDirectory + "\\Files\\XMLFiles\\" + nameOfFolder + "Data.xml", wSettings);
            writer.WriteProcessingInstruction("xml", "version='1.0' encoding='utf-8'");

            writer.WriteStartElement("Classes");
            writer.WriteAttributeString("lang", "ru");
            writer.WriteStartElement("Specification");
            writer.WriteAttributeString("description", "Распознание");
            writer.WriteStartElement("Feature");
            writer.WriteAttributeString("id", "1");
            writer.WriteAttributeString("description", "Ax");
            writer.WriteEndElement();

            writer.WriteStartElement("Feature");
            writer.WriteAttributeString("id", "2");
            writer.WriteAttributeString("description", "Ay");
            writer.WriteEndElement();

            writer.WriteStartElement("Feature");
            writer.WriteAttributeString("id", "3");
            writer.WriteAttributeString("description", "Az");
            writer.WriteEndElement();

            writer.WriteStartElement("Feature");
            writer.WriteAttributeString("id", "4");
            writer.WriteAttributeString("description", "q");
            writer.WriteEndElement();

            writer.WriteStartElement("Feature");
            writer.WriteAttributeString("id", "5");
            writer.WriteAttributeString("description", "w");
            writer.WriteEndElement();

            writer.WriteStartElement("Feature");
            writer.WriteAttributeString("id", "6");
            writer.WriteAttributeString("description", "s");
            writer.WriteEndElement();

            writer.WriteStartElement("Feature");
            writer.WriteAttributeString("id", "7");
            writer.WriteAttributeString("description", "n");
            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.WriteStartElement("Features");
            writer.WriteStartElement("Class");
            writer.WriteAttributeString("name", nameOfFolder);
            
            foreach (string pathToParse in files)
            {
                string[] curFileToResize = File.ReadAllLines(pathToParse);
                string ax = curFileToResize[0];
                string ay = curFileToResize[1];
                string az = curFileToResize[2];
                string q = curFileToResize[3];
                string w = curFileToResize[4];
                string s = curFileToResize[5];
                string n = curFileToResize[6];

                //fileDataPoints.Add(new dataPoint(curFileToResize[i]));
                string[] axSplit = ax.Split('|');
                string[] aySplit = ay.Split('|');
                string[] azSplit = az.Split('|');
                string[] qSplit = q.Split('|');
                string[] wSplit = w.Split('|');
                string[] sSplit = s.Split('|');
                string[] nSplit = n.Split('|');

                {
                   
                    writer.WriteStartElement("Realization");
                    for (int i = 0; i < axSplit.Length; i++)
                    {
                        writer.WriteStartElement("Feature");
                        writer.WriteAttributeString("id", "1");
                        writer.WriteAttributeString("value", axSplit[i]);
                        writer.WriteEndElement();
                        writer.WriteStartElement("Feature");
                        writer.WriteAttributeString("id", "2");
                        writer.WriteAttributeString("value", aySplit[i]);
                        writer.WriteEndElement();
                        writer.WriteStartElement("Feature");
                        writer.WriteAttributeString("id", "3");
                        writer.WriteAttributeString("value", azSplit[i]);
                        writer.WriteEndElement();
                        writer.WriteStartElement("Feature");
                        writer.WriteAttributeString("id", "4");
                        writer.WriteAttributeString("value", qSplit[i]);
                        writer.WriteEndElement();
                        writer.WriteStartElement("Feature");
                        writer.WriteAttributeString("id", "5");
                        writer.WriteAttributeString("value", wSplit[i]);
                        writer.WriteEndElement();
                        writer.WriteStartElement("Feature");
                        writer.WriteAttributeString("id", "6");
                        writer.WriteAttributeString("value", sSplit[i]);
                        writer.WriteEndElement();
                        writer.WriteStartElement("Feature");
                        writer.WriteAttributeString("id", "7");
                        writer.WriteAttributeString("value", nSplit[i]);
                        writer.WriteEndElement();
                    }
                    //writer.WriteAttributeString("value", ax.ToString());
                    writer.WriteEndElement();
                   
                 }

            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Flush();
            writer.Close();
            resizeLabel.Text = "Xml Creation done!";
        }
        private void ResizeDataButton_Click(object sender, EventArgs e)
        {
            string pathToData = Environment.CurrentDirectory + "\\Files\\"+ DirectoryName;
            string[] files = Directory.GetFiles(pathToData, "*");
            int finalsize = 1000;
            resizeLabel.Text = "";
            foreach (string pathToParse in files)
            {
                List<dataPoint> fileDataPoints = new List<dataPoint>();
                List<dataPoint> COM3DataPoints = new List<dataPoint>();
                string[] curFileToResize = File.ReadAllLines(pathToParse);
                double[,] ParsedString = new double[curFileToResize.Length, 7];
                for (int i = 0; i < curFileToResize.Length; i++)
                {
                   
                    curFileToResize[i] = curFileToResize[i].Replace("COM", "");
                    curFileToResize[i] = curFileToResize[i].Replace(";", "");
                    curFileToResize[i] = curFileToResize[i].Replace(",", "|");
                    if (curFileToResize[i] == "")
                    {
                        continue;
                    }
                    string[] a = curFileToResize[i].Split('|');
                   
                    fileDataPoints.Add(new dataPoint(curFileToResize[i]));
                }

                int PointsLength = finalsize / fileDataPoints.Count;

                for (int i = 0; i < fileDataPoints.Count - 1; i++)
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
                if (COM3DataPoints.Count < finalsize)  // if not all data is as big as it needs to be
                { int border = COM3DataPoints.Count;
                    for (int i = 0; i < finalsize - border; i++)
                    {
                        dataPoint extraFill = new dataPoint(COM3DataPoints[COM3DataPoints.Count - 1].data);
                        COM3DataPoints.Add(extraFill);
                    }
                }
              
                string dir = pathToData + " resized";
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < COM3DataPoints.Count; i++)
                {
                    for (int j = 0; j < COM3DataPoints[i].data.Length; j++)
                    {
                        builder.Append(COM3DataPoints[i].data[j].ToString());
                        if (j != COM3DataPoints[i].data.Length - 1)
                        {
                            builder.Append("|");
                        }
                    }
                    builder.Append("\r\n");
                }
                File.WriteAllText(dir + "\\" + Path.GetFileName(pathToParse), builder.ToString());

                //string HorPath = pathToData + " Horrizontal";
                //if (!Directory.Exists(HorPath))
                //    Directory.CreateDirectory(HorPath);
                //StringBuilder HorBuilder = new StringBuilder();
                //for (int j = 0; j< COM3DataPoints[j].data.Length; j++)
                //{
                //    for (int i = 0; i < COM3DataPoints.Count; i++)
                //    {
                //        HorBuilder.Append(COM3DataPoints[i].data[j].ToString());
                //        HorBuilder.Append("|");
                //    }
                //    HorBuilder.Remove(HorBuilder.Length-1, 1);
                //    HorBuilder.Append("\r\n");
                //}
                //File.WriteAllText(HorPath + "\\" + Path.GetFileName(pathToParse), HorBuilder.ToString());

            }
            resizeLabel.Text = "Resizing done!";
        }



        private void GenerateNew_Click(object sender, EventArgs e)
        {
            string pathToData = Environment.CurrentDirectory + "\\Files\\"+ DirectoryName + " resized";
            string[] files = Directory.GetFiles(pathToData, "*");
            double[,,] dataFromAllFiles = new double[files.Length, 7, 1000];

            int stringcount = 0;

            foreach (string pathToParse in files)
            {
                string[] curFileToResize = File.ReadAllLines(pathToParse);
                for (int i = 0; i < curFileToResize.Length; i++)
                {
                    string[] a = curFileToResize[i].Split('|');
                    for (int j = 0; j < a.Length; j++)
                    {
                        dataFromAllFiles[stringcount, j, i] = Double.Parse(a[j]);
                    }
                }
                stringcount++;
            }

            double[,] AvarageData = new double[7, 1000];
            double[,] Maxdiviation = new double[7, 1000];
            double[,] Mindiviation = new double[7, 1000];
            for (int i = 0; i < dataFromAllFiles.GetLength(0); i++)
            {
                for (int j = 0; j < dataFromAllFiles.GetLength(1); j++)
                {
                    for (int k = 0; k < dataFromAllFiles.GetLength(2); k++)
                    {
                        AvarageData[j, k] += dataFromAllFiles[i, j, k];

                    }
                }
            }

            for (int j = 0; j < dataFromAllFiles.GetLength(1); j++)
            {
                for (int k = 0; k < dataFromAllFiles.GetLength(2); k++)
                {
                    AvarageData[j, k] = AvarageData[j, k] / dataFromAllFiles.GetLength(0);

                }
            }
            for (int i = 0; i < dataFromAllFiles.GetLength(0); i++)
            {
                for (int j = 0; j < dataFromAllFiles.GetLength(1); j++)
                {
                    for (int k = 0; k < dataFromAllFiles.GetLength(2); k++)
                    {
                        if (AvarageData[j, k] > 0 && AvarageData[j, k] < dataFromAllFiles[i, j, k])
                        {
                            Maxdiviation[j, k] = dataFromAllFiles[i, j, k];
                        }
                        else
                        {
                            if (AvarageData[j, k] > 0 && AvarageData[j, k] < dataFromAllFiles[i, j, k])
                            {
                                Mindiviation[j, k] = dataFromAllFiles[i, j, k];
                            }
                        }
                        if (AvarageData[j, k] < 0 && AvarageData[j, k] > dataFromAllFiles[i, j, k])
                        {
                            Maxdiviation[j, k] = dataFromAllFiles[i, j, k];
                        }
                        else
                        {
                            if (AvarageData[j, k] < 0 && AvarageData[j, k] < dataFromAllFiles[i, j, k])
                            {
                                Mindiviation[j, k] = dataFromAllFiles[i, j, k];
                            }
                        }
                    }
                }
            }
            int numberOfnewSamples = 200;
            double[,] GeteratedValues = new double[7, 1000];
            Random rand = new Random();
            string dir = pathToData + " generated";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);


            for (int i = 0; i < numberOfnewSamples; i++)
            {
                StringBuilder builder = new StringBuilder();
                for (int j = 0; j < dataFromAllFiles.GetLength(1); j++)
                {
                    for (int k = 0; k < dataFromAllFiles.GetLength(2); k++)
                    {
                        GeteratedValues[j, k] = Math.Round(rand.NextDouble() * (Maxdiviation[j, k] - Mindiviation[j, k]) + Mindiviation[j, k],4);
                        builder.Append(GeteratedValues[j, k].ToString());
                        builder.Append("|");
                    }
                builder.Remove(builder.Length-1,1);
                builder.Append("\r\n");

                }
                File.WriteAllText(dir + "\\" + i+".txt", builder.ToString());

            }
            resizeLabel.Text = "Generation done!";
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

            foreach (string pathToFile in files)            //Horizontal Data
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
            int sameShit = 0; // Spearman correlation
            for (int j = 0; j < Px.Count - 1; j++)
            {
                for (int i = 0; i < Px.Count - 1; i++)
                {
                    double correlx = Correlation.Pearson(Ax[j].data, Ax[i].data);
                    double correly = Correlation.Pearson(Ay[j].data, Ay[i].data);
                    double correlz = Correlation.Pearson(Az[j].data, Az[i].data);
                    if (correlx > 0.75 && correly > 0.75 && correlz > 0.75)
                    {
                        sameShit++;
                    }
                }
                rtbDisplay.AppendText("i=" + j + " " + sameShit.ToString() + "\r\n");
                sameShit = 0;
            }

            //List<double> AvX = new List<double>(); // мат ожидание 
            //List<double> AvY = new List<double>();
            //List<double> AvZ = new List<double>();

            //for (int j = 0; j < Px.Count; j++)
            //{
            //        AvX.Add( Enumerable.Average(Ax[j].data));
            //        AvY.Add(Enumerable.Average(Ay[j].data));
            //        AvZ.Add(Enumerable.Average(Az[j].data));
            //}

            //List<double> EvX = new List<double>();
            //List<double> EvY = new List<double>();
            //List<double> EvZ = new List<double>();
            //for (int i = 0; i < Px.Count; i++)
            //{
            //    for (int j = 0; j < Px.Count; j++)
            //    {
            //        if (i != j)
            //        {
            //            EvX.Add(Math.Abs(AvX[i] - AvX[j]));
            //            EvY.Add(Math.Abs(AvY[i] - AvY[j]));
            //            EvZ.Add(Math.Abs(AvZ[i] - AvZ[j]));
            //        }
            //    }
            //}

            //double EuclidFinalX = Enumerable.Average(EvX);
            //double EuclidFinalY = Enumerable.Average(EvY);
            //double EuclidFinalZ = Enumerable.Average(EvZ);

            //rtbDisplay.AppendText(EuclidFinalX.ToString()+"\r\n");
            //rtbDisplay.AppendText(EuclidFinalY.ToString() + "\r\n");
            //rtbDisplay.AppendText(EuclidFinalZ.ToString() + "\r\n");

            //dataPoint EuclidX = new dataPoint(new double[1000]);
            //dataPoint EuclidY = new dataPoint(new double[1000]);
            //dataPoint EuclidZ = new dataPoint(new double[1000]);

            //for (int i = 0; i < 1000; i++)
            //{

            //    for (int j = 0; j < Px.Count; j++)
            //    {
            //        EuclidX.data[i] += Math.Abs( AvarageX.data[i] - Ax[j].data[i]);
            //        EuclidY.data[i] += Math.Abs( AvarageY.data[i] - Ay[j].data[i]);
            //        EuclidZ.data[i] += Math.Abs( AvarageY.data[i] - Az[j].data[i]);
            //    }
            //    EuclidX.data[i] = EuclidX.data[i] / Px.Count;
            //    EuclidY.data[i] = EuclidY.data[i] / Px.Count;
            //    EuclidZ.data[i] = EuclidZ.data[i] / Px.Count;
            //}


            //string HorPath = pathToData + " Avarage";
            //if (!Directory.Exists(HorPath))
            //    Directory.CreateDirectory(HorPath);
            //StringBuilder HorBuilder = new StringBuilder();
            //for (int j = 0; j < EuclidX.data.Length; j++)
            //{
            //        HorBuilder.Append(EuclidX.data[j].ToString());
            //        HorBuilder.Append("|");
            //        HorBuilder.Append(EuclidY.data[j].ToString());
            //        HorBuilder.Append("|");
            //        HorBuilder.Append(EuclidZ.data[j].ToString());
            //        HorBuilder.Append("\r\n");
            //}
            //File.WriteAllText(HorPath + "\\" + Path.GetFileName(pathToData), HorBuilder.ToString());


            //to do: correl calculation
        }

        
    }
}
