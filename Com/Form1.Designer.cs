namespace Com
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sendBtn = new System.Windows.Forms.Button();
            this.messageTB = new System.Windows.Forms.TextBox();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.portNameTB = new System.Windows.Forms.TextBox();
            this.baudRateTB = new System.Windows.Forms.TextBox();
            this.openClosePortBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(484, 12);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(75, 23);
            this.sendBtn.TabIndex = 0;
            this.sendBtn.Text = "Send";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // messageTB
            // 
            this.messageTB.Location = new System.Drawing.Point(12, 12);
            this.messageTB.Name = "messageTB";
            this.messageTB.PlaceholderText = "Message";
            this.messageTB.Size = new System.Drawing.Size(466, 23);
            this.messageTB.TabIndex = 1;
            // 
            // logTextBox
            // 
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.logTextBox.Location = new System.Drawing.Point(0, 123);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new System.Drawing.Size(571, 327);
            this.logTextBox.TabIndex = 1;
            // 
            // portNameTB
            // 
            this.portNameTB.Location = new System.Drawing.Point(12, 41);
            this.portNameTB.Name = "portNameTB";
            this.portNameTB.PlaceholderText = "PortName ";
            this.portNameTB.Size = new System.Drawing.Size(466, 23);
            this.portNameTB.TabIndex = 2;
            this.portNameTB.Text = "COM6";
            this.portNameTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.portNameTB_KeyDown);
            // 
            // baudRateTB
            // 
            this.baudRateTB.Location = new System.Drawing.Point(12, 70);
            this.baudRateTB.Name = "baudRateTB";
            this.baudRateTB.PlaceholderText = "Baud Rate ";
            this.baudRateTB.Size = new System.Drawing.Size(466, 23);
            this.baudRateTB.TabIndex = 3;
            this.baudRateTB.Text = "9600";
            this.baudRateTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.baudRateTB_KeyDown);
            // 
            // openClosePortBtn
            // 
            this.openClosePortBtn.Location = new System.Drawing.Point(484, 41);
            this.openClosePortBtn.Name = "openClosePortBtn";
            this.openClosePortBtn.Size = new System.Drawing.Size(75, 52);
            this.openClosePortBtn.TabIndex = 4;
            this.openClosePortBtn.Text = "Open Port";
            this.openClosePortBtn.UseVisualStyleBackColor = true;
            this.openClosePortBtn.Click += new System.EventHandler(this.openClosePortBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 450);
            this.Controls.Add(this.openClosePortBtn);
            this.Controls.Add(this.baudRateTB);
            this.Controls.Add(this.portNameTB);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.messageTB);
            this.Controls.Add(this.sendBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.TextBox messageTB;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.TextBox portNameTB;
        private System.Windows.Forms.TextBox baudRateTB;
        private System.Windows.Forms.Button openClosePortBtn;
    }
}

