namespace Motion_Project
{
    partial class SelialReader
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
            this.SuspendLayout();
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(601, 17);
            this.Send.Margin = new System.Windows.Forms.Padding(2);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(83, 36);
            this.Send.TabIndex = 0;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // SendText
            // 
            this.SendText.Location = new System.Drawing.Point(26, 17);
            this.SendText.Margin = new System.Windows.Forms.Padding(2);
            this.SendText.Multiline = true;
            this.SendText.Name = "SendText";
            this.SendText.Size = new System.Drawing.Size(573, 38);
            this.SendText.TabIndex = 1;
            // 
            // PortBox
            // 
            this.PortBox.FormattingEnabled = true;
            this.PortBox.Location = new System.Drawing.Point(77, 358);
            this.PortBox.Margin = new System.Windows.Forms.Padding(2);
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(133, 21);
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
            this.BaudBox.Location = new System.Drawing.Point(76, 385);
            this.BaudBox.Margin = new System.Windows.Forms.Padding(2);
            this.BaudBox.Name = "BaudBox";
            this.BaudBox.Size = new System.Drawing.Size(133, 21);
            this.BaudBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 358);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 385);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Baud";
            // 
            // OpenPort
            // 
            this.OpenPort.Location = new System.Drawing.Point(601, 358);
            this.OpenPort.Margin = new System.Windows.Forms.Padding(2);
            this.OpenPort.Name = "OpenPort";
            this.OpenPort.Size = new System.Drawing.Size(83, 19);
            this.OpenPort.TabIndex = 8;
            this.OpenPort.Text = "Open Port";
            this.OpenPort.UseVisualStyleBackColor = true;
            this.OpenPort.Click += new System.EventHandler(this.OpenPort_Click);
            // 
            // ClosePort
            // 
            this.ClosePort.Location = new System.Drawing.Point(601, 385);
            this.ClosePort.Margin = new System.Windows.Forms.Padding(2);
            this.ClosePort.Name = "ClosePort";
            this.ClosePort.Size = new System.Drawing.Size(83, 19);
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
            this.DataBits.Location = new System.Drawing.Point(265, 359);
            this.DataBits.Margin = new System.Windows.Forms.Padding(2);
            this.DataBits.Name = "DataBits";
            this.DataBits.Size = new System.Drawing.Size(145, 21);
            this.DataBits.TabIndex = 10;
            // 
            // StopBits
            // 
            this.StopBits.FormattingEnabled = true;
            this.StopBits.Items.AddRange(new object[] {
            "1",
            "3",
            "2"});
            this.StopBits.Location = new System.Drawing.Point(265, 386);
            this.StopBits.Margin = new System.Windows.Forms.Padding(2);
            this.StopBits.Name = "StopBits";
            this.StopBits.Size = new System.Drawing.Size(145, 21);
            this.StopBits.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 353);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Data bits";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(212, 381);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Stop Bits";
            // 
            // Paritybox
            // 
            this.Paritybox.FormattingEnabled = true;
            this.Paritybox.Location = new System.Drawing.Point(479, 358);
            this.Paritybox.Margin = new System.Windows.Forms.Padding(2);
            this.Paritybox.Name = "Paritybox";
            this.Paritybox.Size = new System.Drawing.Size(121, 21);
            this.Paritybox.TabIndex = 14;
            // 
            // TerminationBox
            // 
            this.TerminationBox.FormattingEnabled = true;
            this.TerminationBox.Location = new System.Drawing.Point(479, 385);
            this.TerminationBox.Margin = new System.Windows.Forms.Padding(2);
            this.TerminationBox.Name = "TerminationBox";
            this.TerminationBox.Size = new System.Drawing.Size(121, 21);
            this.TerminationBox.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(436, 358);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Patity";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(413, 385);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Termination";
            // 
            // WristPort
            // 
            this.WristPort.FormattingEnabled = true;
            this.WristPort.Location = new System.Drawing.Point(17, 184);
            this.WristPort.Margin = new System.Windows.Forms.Padding(2);
            this.WristPort.Name = "WristPort";
            this.WristPort.Size = new System.Drawing.Size(68, 21);
            this.WristPort.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 168);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Wrist";
            // 
            // ElbowPort
            // 
            this.ElbowPort.FormattingEnabled = true;
            this.ElbowPort.Location = new System.Drawing.Point(32, 150);
            this.ElbowPort.Margin = new System.Windows.Forms.Padding(2);
            this.ElbowPort.Name = "ElbowPort";
            this.ElbowPort.Size = new System.Drawing.Size(68, 21);
            this.ElbowPort.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(29, 134);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Elbow";
            // 
            // ShoulderPort
            // 
            this.ShoulderPort.FormattingEnabled = true;
            this.ShoulderPort.Location = new System.Drawing.Point(43, 115);
            this.ShoulderPort.Margin = new System.Windows.Forms.Padding(2);
            this.ShoulderPort.Name = "ShoulderPort";
            this.ShoulderPort.Size = new System.Drawing.Size(68, 21);
            this.ShoulderPort.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(34, 100);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Shoulder";
            // 
            // rtbDisplay
            // 
            this.rtbDisplay.Location = new System.Drawing.Point(309, 61);
            this.rtbDisplay.Margin = new System.Windows.Forms.Padding(2);
            this.rtbDisplay.Name = "rtbDisplay";
            this.rtbDisplay.Size = new System.Drawing.Size(377, 295);
            this.rtbDisplay.TabIndex = 24;
            this.rtbDisplay.Text = "";
            // 
            // WriteToFile
            // 
            this.WriteToFile.Location = new System.Drawing.Point(601, 408);
            this.WriteToFile.Margin = new System.Windows.Forms.Padding(2);
            this.WriteToFile.Name = "WriteToFile";
            this.WriteToFile.Size = new System.Drawing.Size(83, 20);
            this.WriteToFile.TabIndex = 25;
            this.WriteToFile.Text = "Write to file";
            this.WriteToFile.UseVisualStyleBackColor = true;
            this.WriteToFile.Click += new System.EventHandler(this.WriteToFile_Click);
            // 
            // WriteLabel
            // 
            this.WriteLabel.AutoSize = true;
            this.WriteLabel.Location = new System.Drawing.Point(601, 432);
            this.WriteLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.WriteLabel.Name = "WriteLabel";
            this.WriteLabel.Size = new System.Drawing.Size(41, 13);
            this.WriteLabel.TabIndex = 26;
            this.WriteLabel.Text = "label10";
            // 
            // buttonFixTrash
            // 
            this.buttonFixTrash.Location = new System.Drawing.Point(604, 447);
            this.buttonFixTrash.Margin = new System.Windows.Forms.Padding(2);
            this.buttonFixTrash.Name = "buttonFixTrash";
            this.buttonFixTrash.Size = new System.Drawing.Size(83, 20);
            this.buttonFixTrash.TabIndex = 27;
            this.buttonFixTrash.Text = "Fix files";
            this.buttonFixTrash.UseVisualStyleBackColor = true;
            this.buttonFixTrash.Click += new System.EventHandler(this.buttonFixTrash_Click);
            // 
            // tBoxTrashFolder
            // 
            this.tBoxTrashFolder.Location = new System.Drawing.Point(359, 447);
            this.tBoxTrashFolder.Margin = new System.Windows.Forms.Padding(2);
            this.tBoxTrashFolder.Multiline = true;
            this.tBoxTrashFolder.Name = "tBoxTrashFolder";
            this.tBoxTrashFolder.Size = new System.Drawing.Size(241, 20);
            this.tBoxTrashFolder.TabIndex = 28;
            // 
            // labelWritingDone
            // 
            this.labelWritingDone.AutoSize = true;
            this.labelWritingDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 47F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelWritingDone.Location = new System.Drawing.Point(14, 236);
            this.labelWritingDone.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelWritingDone.Name = "labelWritingDone";
            this.labelWritingDone.Size = new System.Drawing.Size(0, 72);
            this.labelWritingDone.TabIndex = 29;
            // 
            // ResizeDataButton
            // 
            this.ResizeDataButton.Location = new System.Drawing.Point(244, 446);
            this.ResizeDataButton.Margin = new System.Windows.Forms.Padding(2);
            this.ResizeDataButton.Name = "ResizeDataButton";
            this.ResizeDataButton.Size = new System.Drawing.Size(83, 20);
            this.ResizeDataButton.TabIndex = 30;
            this.ResizeDataButton.Text = "Resize data";
            this.ResizeDataButton.UseVisualStyleBackColor = true;
            this.ResizeDataButton.Click += new System.EventHandler(this.ResizeDataButton_Click);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 23);
            this.label10.TabIndex = 0;
            // 
            // resizeLabel
            // 
            this.resizeLabel.AutoSize = true;
            this.resizeLabel.Location = new System.Drawing.Point(149, 450);
            this.resizeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.resizeLabel.Name = "resizeLabel";
            this.resizeLabel.Size = new System.Drawing.Size(0, 13);
            this.resizeLabel.TabIndex = 31;
            // 
            // SelialReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 484);
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
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SelialReader";
            this.Text = "Serial Reader";
            this.Load += new System.EventHandler(this.SelialReader_Load);
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
    }
}

