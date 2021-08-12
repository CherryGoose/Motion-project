namespace Motion_Project
{
    partial class SerialReader
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Send = new System.Windows.Forms.Button();
            this.SendText = new System.Windows.Forms.TextBox();
            this.PortBox = new System.Windows.Forms.ComboBox();
            this.BaudBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OpenPort = new System.Windows.Forms.Button();
            this.ClosePort = new System.Windows.Forms.Button();
            this.DataBits = new System.Windows.Forms.ComboBox();
            this.StopBits = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Paritybox = new System.Windows.Forms.ComboBox();
            this.TerminationBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.WristPort = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ElbowPort = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ShoulderPort = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.rtbDisplay = new System.Windows.Forms.RichTextBox();
            this.WriteToFile = new System.Windows.Forms.Button();
            this.WriteLabel = new System.Windows.Forms.Label();
            this.buttonFixTrash = new System.Windows.Forms.Button();
            this.tBoxTrashFolder = new System.Windows.Forms.TextBox();
            this.labelWritingDone = new System.Windows.Forms.Label();
            this.ResizeDataButton = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.resizeLabel = new System.Windows.Forms.Label();
            this.CorrelButton = new System.Windows.Forms.Button();
            this.createXmlfiles_button = new System.Windows.Forms.Button();
            this.GenerateNew = new System.Windows.Forms.Button();
            this.CombiteXml = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(902, 26);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(124, 55);
            this.Send.TabIndex = 0;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // SendText
            // 
            this.SendText.Location = new System.Drawing.Point(39, 26);
            this.SendText.Multiline = true;
            this.SendText.Name = "SendText";
            this.SendText.Size = new System.Drawing.Size(858, 56);
            this.SendText.TabIndex = 1;
            // 
            // PortBox
            // 
            this.PortBox.FormattingEnabled = true;
            this.PortBox.Location = new System.Drawing.Point(116, 551);
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(198, 28);
            this.PortBox.TabIndex = 3;
            // 
            // BaudBox
            // 
            this.BaudBox.FormattingEnabled = true;
            this.BaudBox.Items.AddRange(new object[] {
            "300",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.BaudBox.Location = new System.Drawing.Point(114, 592);
            this.BaudBox.Name = "BaudBox";
            this.BaudBox.Size = new System.Drawing.Size(198, 28);
            this.BaudBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 551);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 592);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Baud";
            // 
            // OpenPort
            // 
            this.OpenPort.Location = new System.Drawing.Point(902, 551);
            this.OpenPort.Name = "OpenPort";
            this.OpenPort.Size = new System.Drawing.Size(124, 29);
            this.OpenPort.TabIndex = 8;
            this.OpenPort.Text = "Open Port";
            this.OpenPort.UseVisualStyleBackColor = true;
            this.OpenPort.Click += new System.EventHandler(this.OpenPort_Click);
            // 
            // ClosePort
            // 
            this.ClosePort.Location = new System.Drawing.Point(902, 592);
            this.ClosePort.Name = "ClosePort";
            this.ClosePort.Size = new System.Drawing.Size(124, 29);
            this.ClosePort.TabIndex = 9;
            this.ClosePort.Text = "Close Port";
            this.ClosePort.UseVisualStyleBackColor = true;
            this.ClosePort.Click += new System.EventHandler(this.ClosePort_Click);
            // 
            // DataBits
            // 
            this.DataBits.FormattingEnabled = true;
            this.DataBits.Items.AddRange(new object[] {
            "8",
            "5",
            "6",
            "7"});
            this.DataBits.Location = new System.Drawing.Point(398, 552);
            this.DataBits.Name = "DataBits";
            this.DataBits.Size = new System.Drawing.Size(216, 28);
            this.DataBits.TabIndex = 10;
            // 
            // StopBits
            // 
            this.StopBits.FormattingEnabled = true;
            this.StopBits.Items.AddRange(new object[] {
            "1",
            "3",
            "2"});
            this.StopBits.Location = new System.Drawing.Point(398, 594);
            this.StopBits.Name = "StopBits";
            this.StopBits.Size = new System.Drawing.Size(216, 28);
            this.StopBits.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(318, 543);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Data bits";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(318, 586);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Stop Bits";
            // 
            // Paritybox
            // 
            this.Paritybox.FormattingEnabled = true;
            this.Paritybox.Location = new System.Drawing.Point(718, 551);
            this.Paritybox.Name = "Paritybox";
            this.Paritybox.Size = new System.Drawing.Size(180, 28);
            this.Paritybox.TabIndex = 14;
            // 
            // TerminationBox
            // 
            this.TerminationBox.FormattingEnabled = true;
            this.TerminationBox.Location = new System.Drawing.Point(718, 592);
            this.TerminationBox.Name = "TerminationBox";
            this.TerminationBox.Size = new System.Drawing.Size(180, 28);
            this.TerminationBox.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(654, 551);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Patity";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(620, 592);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Termination";
            // 
            // WristPort
            // 
            this.WristPort.FormattingEnabled = true;
            this.WristPort.Location = new System.Drawing.Point(26, 283);
            this.WristPort.Name = "WristPort";
            this.WristPort.Size = new System.Drawing.Size(100, 28);
            this.WristPort.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 258);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 20);
            this.label7.TabIndex = 19;
            this.label7.Text = "Wrist";
            // 
            // ElbowPort
            // 
            this.ElbowPort.FormattingEnabled = true;
            this.ElbowPort.Location = new System.Drawing.Point(48, 231);
            this.ElbowPort.Name = "ElbowPort";
            this.ElbowPort.Size = new System.Drawing.Size(100, 28);
            this.ElbowPort.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(44, 206);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 20);
            this.label8.TabIndex = 21;
            this.label8.Text = "Elbow";
            // 
            // ShoulderPort
            // 
            this.ShoulderPort.FormattingEnabled = true;
            this.ShoulderPort.Location = new System.Drawing.Point(64, 177);
            this.ShoulderPort.Name = "ShoulderPort";
            this.ShoulderPort.Size = new System.Drawing.Size(100, 28);
            this.ShoulderPort.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(51, 154);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 20);
            this.label9.TabIndex = 23;
            this.label9.Text = "Shoulder";
            // 
            // rtbDisplay
            // 
            this.rtbDisplay.Location = new System.Drawing.Point(464, 94);
            this.rtbDisplay.Name = "rtbDisplay";
            this.rtbDisplay.Size = new System.Drawing.Size(564, 452);
            this.rtbDisplay.TabIndex = 24;
            this.rtbDisplay.Text = "";
            // 
            // WriteToFile
            // 
            this.WriteToFile.Location = new System.Drawing.Point(902, 628);
            this.WriteToFile.Name = "WriteToFile";
            this.WriteToFile.Size = new System.Drawing.Size(124, 31);
            this.WriteToFile.TabIndex = 25;
            this.WriteToFile.Text = "Write to file";
            this.WriteToFile.UseVisualStyleBackColor = true;
            this.WriteToFile.Click += new System.EventHandler(this.WriteToFile_Click);
            // 
            // WriteLabel
            // 
            this.WriteLabel.AutoSize = true;
            this.WriteLabel.Location = new System.Drawing.Point(902, 665);
            this.WriteLabel.Name = "WriteLabel";
            this.WriteLabel.Size = new System.Drawing.Size(60, 20);
            this.WriteLabel.TabIndex = 26;
            this.WriteLabel.Text = "label10";
            // 
            // buttonFixTrash
            // 
            this.buttonFixTrash.Location = new System.Drawing.Point(906, 688);
            this.buttonFixTrash.Name = "buttonFixTrash";
            this.buttonFixTrash.Size = new System.Drawing.Size(124, 31);
            this.buttonFixTrash.TabIndex = 27;
            this.buttonFixTrash.Text = "Fix files";
            this.buttonFixTrash.UseVisualStyleBackColor = true;
            this.buttonFixTrash.Click += new System.EventHandler(this.buttonFixTrash_Click);
            // 
            // tBoxTrashFolder
            // 
            this.tBoxTrashFolder.Location = new System.Drawing.Point(538, 688);
            this.tBoxTrashFolder.Multiline = true;
            this.tBoxTrashFolder.Name = "tBoxTrashFolder";
            this.tBoxTrashFolder.Size = new System.Drawing.Size(360, 29);
            this.tBoxTrashFolder.TabIndex = 28;
            this.tBoxTrashFolder.TextChanged += new System.EventHandler(this.tBoxTrashFolder_TextChanged);
            // 
            // labelWritingDone
            // 
            this.labelWritingDone.AutoSize = true;
            this.labelWritingDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 47F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWritingDone.Location = new System.Drawing.Point(21, 363);
            this.labelWritingDone.Name = "labelWritingDone";
            this.labelWritingDone.Size = new System.Drawing.Size(0, 107);
            this.labelWritingDone.TabIndex = 29;
            // 
            // ResizeDataButton
            // 
            this.ResizeDataButton.Location = new System.Drawing.Point(366, 686);
            this.ResizeDataButton.Name = "ResizeDataButton";
            this.ResizeDataButton.Size = new System.Drawing.Size(124, 31);
            this.ResizeDataButton.TabIndex = 30;
            this.ResizeDataButton.Text = "Resize data";
            this.ResizeDataButton.UseVisualStyleBackColor = true;
            this.ResizeDataButton.Click += new System.EventHandler(this.ResizeDataButton_Click);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(150, 35);
            this.label10.TabIndex = 0;
            // 
            // resizeLabel
            // 
            this.resizeLabel.AutoSize = true;
            this.resizeLabel.Location = new System.Drawing.Point(224, 692);
            this.resizeLabel.Name = "resizeLabel";
            this.resizeLabel.Size = new System.Drawing.Size(0, 20);
            this.resizeLabel.TabIndex = 31;
            // 
            // CorrelButton
            // 
            this.CorrelButton.Location = new System.Drawing.Point(310, 499);
            this.CorrelButton.Name = "CorrelButton";
            this.CorrelButton.Size = new System.Drawing.Size(148, 31);
            this.CorrelButton.TabIndex = 32;
            this.CorrelButton.Text = "Compute Correl";
            this.CorrelButton.UseVisualStyleBackColor = true;
            this.CorrelButton.Click += new System.EventHandler(this.CorrelButton_Click);
            // 
            // createXmlfiles_button
            // 
            this.createXmlfiles_button.Location = new System.Drawing.Point(718, 635);
            this.createXmlfiles_button.Name = "createXmlfiles_button";
            this.createXmlfiles_button.Size = new System.Drawing.Size(166, 31);
            this.createXmlfiles_button.TabIndex = 33;
            this.createXmlfiles_button.Text = "createXmlfiles_button";
            this.createXmlfiles_button.UseVisualStyleBackColor = true;
            this.createXmlfiles_button.Click += new System.EventHandler(this.createXmlfiles_button_Click);
            // 
            // GenerateNew
            // 
            this.GenerateNew.Location = new System.Drawing.Point(526, 635);
            this.GenerateNew.Name = "GenerateNew";
            this.GenerateNew.Size = new System.Drawing.Size(176, 31);
            this.GenerateNew.TabIndex = 34;
            this.GenerateNew.Text = "GenerateNew";
            this.GenerateNew.UseVisualStyleBackColor = true;
            this.GenerateNew.Click += new System.EventHandler(this.GenerateNew_Click);
            // 
            // CombiteXml
            // 
            this.CombiteXml.Location = new System.Drawing.Point(366, 635);
            this.CombiteXml.Name = "CombiteXml";
            this.CombiteXml.Size = new System.Drawing.Size(143, 31);
            this.CombiteXml.TabIndex = 35;
            this.CombiteXml.Text = "Combine Xml";
            this.CombiteXml.UseVisualStyleBackColor = true;
            this.CombiteXml.Click += new System.EventHandler(this.CombiteXml_Click);
            // 
            // SerialReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 745);
            this.Controls.Add(this.CombiteXml);
            this.Controls.Add(this.GenerateNew);
            this.Controls.Add(this.createXmlfiles_button);
            this.Controls.Add(this.CorrelButton);
            this.Controls.Add(this.resizeLabel);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ResizeDataButton);
            this.Controls.Add(this.labelWritingDone);
            this.Controls.Add(this.tBoxTrashFolder);
            this.Controls.Add(this.buttonFixTrash);
            this.Controls.Add(this.WriteLabel);
            this.Controls.Add(this.WriteToFile);
            this.Controls.Add(this.rtbDisplay);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ShoulderPort);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ElbowPort);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.WristPort);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TerminationBox);
            this.Controls.Add(this.Paritybox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.StopBits);
            this.Controls.Add(this.DataBits);
            this.Controls.Add(this.ClosePort);
            this.Controls.Add(this.OpenPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BaudBox);
            this.Controls.Add(this.PortBox);
            this.Controls.Add(this.SendText);
            this.Controls.Add(this.Send);
            this.Name = "SerialReader";
            this.Text = "Serial Reader";
            this.Load += new System.EventHandler(this.SerialReader_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.TextBox SendText;
        private System.Windows.Forms.ComboBox PortBox;
        private System.Windows.Forms.ComboBox BaudBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OpenPort;
        private System.Windows.Forms.Button ClosePort;
        private System.Windows.Forms.ComboBox DataBits;
        private System.Windows.Forms.ComboBox StopBits;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Paritybox;
        private System.Windows.Forms.ComboBox TerminationBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox WristPort;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox ElbowPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox ShoulderPort;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox rtbDisplay;
        private System.Windows.Forms.Button WriteToFile;
        private System.Windows.Forms.Label WriteLabel;
        private System.Windows.Forms.Button buttonFixTrash;
        private System.Windows.Forms.TextBox tBoxTrashFolder;
        private System.Windows.Forms.Label labelWritingDone;
        private System.Windows.Forms.Button ResizeDataButton;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label resizeLabel;
        private System.Windows.Forms.Button CorrelButton;
        private System.Windows.Forms.Button createXmlfiles_button;
        private System.Windows.Forms.Button GenerateNew;
        private System.Windows.Forms.Button CombiteXml;
    }
}

