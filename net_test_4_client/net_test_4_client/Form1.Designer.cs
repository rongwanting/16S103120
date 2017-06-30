namespace ChatClient
{
    partial class FClient
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCMsg = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.btnBeginListen = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.remove = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(428, 273);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFile.TabIndex = 45;
            this.btnSelectFile.Text = "选择";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(95, 273);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(309, 21);
            this.txtFileName.TabIndex = 44;
            // 
            // btnSendFile
            // 
            this.btnSendFile.Location = new System.Drawing.Point(139, 328);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(75, 23);
            this.btnSendFile.TabIndex = 43;
            this.btnSendFile.Text = "分享";
            this.btnSendFile.UseVisualStyleBackColor = true;
            this.btnSendFile.Click += new System.EventHandler(this.btnSendFile_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 42;
            this.label4.Text = "输入信息：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 41;
            this.label1.Text = "聊天内容：";
            // 
            // txtCMsg
            // 
            this.txtCMsg.Location = new System.Drawing.Point(95, 229);
            this.txtCMsg.Name = "txtCMsg";
            this.txtCMsg.Size = new System.Drawing.Size(309, 21);
            this.txtCMsg.TabIndex = 40;
            this.txtCMsg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCMsg_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 39;
            this.label3.Text = "通讯地址：";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(428, 229);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 38;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(95, 62);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMsg.Size = new System.Drawing.Size(408, 144);
            this.txtMsg.TabIndex = 37;
            // 
            // btnBeginListen
            // 
            this.btnBeginListen.Location = new System.Drawing.Point(428, 19);
            this.btnBeginListen.Name = "btnBeginListen";
            this.btnBeginListen.Size = new System.Drawing.Size(77, 26);
            this.btnBeginListen.TabIndex = 36;
            this.btnBeginListen.Text = "连接到服务端";
            this.btnBeginListen.UseVisualStyleBackColor = true;
            this.btnBeginListen.Click += new System.EventHandler(this.btnBeginListen_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(312, 23);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(92, 21);
            this.txtPort.TabIndex = 35;
            this.txtPort.Text = "7777";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(230, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 34;
            this.label2.Text = "端口PORT：";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(95, 23);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(115, 21);
            this.txtIP.TabIndex = 33;
            this.txtIP.Text = "127.0.0.1";
            // 
            // remove
            // 
            this.remove.Location = new System.Drawing.Point(276, 328);
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(75, 23);
            this.remove.TabIndex = 53;
            this.remove.Text = "取消";
            this.remove.UseVisualStyleBackColor = true;
            this.remove.Click += new System.EventHandler(this.btnRemoveFile_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 276);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 42;
            this.label5.Text = "选择文件：";
            // 
            // FClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 377);
            this.Controls.Add(this.remove);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnSendFile);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCMsg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.btnBeginListen);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIP);
            this.Name = "FClient";
            this.Text = "客户端";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnSendFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCMsg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.Button btnBeginListen;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button remove;
        private System.Windows.Forms.Label label5;
    }
}

