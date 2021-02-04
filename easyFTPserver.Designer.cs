
namespace easyFTPserver
{
    partial class FTPform
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTPform));
            this.start = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.功能ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.开启ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭软件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.什么是FTPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于作者ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.如何使用ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.select = new System.Windows.Forms.Button();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.pathtext = new System.Windows.Forms.TextBox();
            this.porttext = new System.Windows.Forms.TextBox();
            this.IP = new System.Windows.Forms.Label();
            this.Port = new System.Windows.Forms.Label();
            this.exit = new System.Windows.Forms.Button();
            this.selectIP = new System.Windows.Forms.ComboBox();
            this.client = new System.Windows.Forms.Label();
            this.clientcount = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.start.Location = new System.Drawing.Point(502, 133);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(125, 36);
            this.start.TabIndex = 0;
            this.start.Text = "开启";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // close
            // 
            this.close.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.close.Location = new System.Drawing.Point(502, 179);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(125, 36);
            this.close.TabIndex = 1;
            this.close.Text = "关闭";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // 功能ToolStripMenuItem
            // 
            this.功能ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开启ToolStripMenuItem,
            this.关闭ToolStripMenuItem,
            this.关闭软件ToolStripMenuItem});
            this.功能ToolStripMenuItem.Name = "功能ToolStripMenuItem";
            this.功能ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.功能ToolStripMenuItem.Text = "功能";
            // 
            // 开启ToolStripMenuItem
            // 
            this.开启ToolStripMenuItem.Name = "开启ToolStripMenuItem";
            this.开启ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.开启ToolStripMenuItem.Text = "开启";
            // 
            // 关闭ToolStripMenuItem
            // 
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.关闭ToolStripMenuItem.Text = "关闭";
            // 
            // 关闭软件ToolStripMenuItem
            // 
            this.关闭软件ToolStripMenuItem.Name = "关闭软件ToolStripMenuItem";
            this.关闭软件ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.关闭软件ToolStripMenuItem.Text = "关闭软件";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.什么是FTPToolStripMenuItem,
            this.关于作者ToolStripMenuItem,
            this.如何使用ToolStripMenuItem});
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.关于ToolStripMenuItem.Text = "帮助与关于";
            // 
            // 什么是FTPToolStripMenuItem
            // 
            this.什么是FTPToolStripMenuItem.Name = "什么是FTPToolStripMenuItem";
            this.什么是FTPToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.什么是FTPToolStripMenuItem.Text = "什么是FTP？";
            this.什么是FTPToolStripMenuItem.Click += new System.EventHandler(this.什么是FTPToolStripMenuItem_Click);
            // 
            // 关于作者ToolStripMenuItem
            // 
            this.关于作者ToolStripMenuItem.Name = "关于作者ToolStripMenuItem";
            this.关于作者ToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.关于作者ToolStripMenuItem.Text = "关于作者？";
            this.关于作者ToolStripMenuItem.Click += new System.EventHandler(this.关于作者ToolStripMenuItem_Click);
            // 
            // 如何使用ToolStripMenuItem
            // 
            this.如何使用ToolStripMenuItem.Name = "如何使用ToolStripMenuItem";
            this.如何使用ToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.如何使用ToolStripMenuItem.Text = "如何使用？";
            this.如何使用ToolStripMenuItem.Click += new System.EventHandler(this.如何使用ToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.功能ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(633, 25);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // select
            // 
            this.select.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.select.Location = new System.Drawing.Point(505, 28);
            this.select.Name = "select";
            this.select.Size = new System.Drawing.Size(122, 35);
            this.select.TabIndex = 5;
            this.select.Text = "选择文件夹";
            this.select.UseVisualStyleBackColor = true;
            this.select.Click += new System.EventHandler(this.select_Click);
            // 
            // folderBrowser
            // 
            this.folderBrowser.Description = "选择要开启FTP的文件夹";
            this.folderBrowser.SelectedPath = "C:\\Users\\ZKJ_Jaker\\Desktop";
            this.folderBrowser.ShowNewFolderButton = false;
            // 
            // pathtext
            // 
            this.pathtext.BackColor = System.Drawing.SystemColors.MenuText;
            this.pathtext.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pathtext.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pathtext.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pathtext.ForeColor = System.Drawing.SystemColors.Window;
            this.pathtext.Location = new System.Drawing.Point(3, 28);
            this.pathtext.Multiline = true;
            this.pathtext.Name = "pathtext";
            this.pathtext.ReadOnly = true;
            this.pathtext.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.pathtext.Size = new System.Drawing.Size(496, 232);
            this.pathtext.TabIndex = 6;
            this.pathtext.Text = "1、选择要开启ＦＴＰ的文件夹\r\n2、选择合适的ip地址\r\n3、选择合适的端口号（建议使用默认的21）\r\n注意防火墙规则\r\n本软件是为了方便，只建议在局域网下使用，" +
    "用户密码随便输入没有认证\r\n只是为了以后有需要留下接口\r\n";
            this.pathtext.TextChanged += new System.EventHandler(this.pathtext_TextChanged);
            // 
            // porttext
            // 
            this.porttext.Location = new System.Drawing.Point(562, 106);
            this.porttext.Name = "porttext";
            this.porttext.Size = new System.Drawing.Size(65, 21);
            this.porttext.TabIndex = 6;
            this.porttext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // IP
            // 
            this.IP.AutoSize = true;
            this.IP.Location = new System.Drawing.Point(503, 72);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(29, 12);
            this.IP.TabIndex = 7;
            this.IP.Text = "IP：";
            // 
            // Port
            // 
            this.Port.AutoSize = true;
            this.Port.Location = new System.Drawing.Point(503, 109);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(41, 12);
            this.Port.TabIndex = 7;
            this.Port.Text = "端口：";
            // 
            // exit
            // 
            this.exit.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exit.Location = new System.Drawing.Point(502, 224);
            this.exit.Name = "exit";
            this.exit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.exit.Size = new System.Drawing.Size(125, 36);
            this.exit.TabIndex = 1;
            this.exit.Text = "关闭并退出";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // selectIP
            // 
            this.selectIP.FormattingEnabled = true;
            this.selectIP.Location = new System.Drawing.Point(522, 69);
            this.selectIP.Name = "selectIP";
            this.selectIP.Size = new System.Drawing.Size(105, 20);
            this.selectIP.TabIndex = 9;
            this.selectIP.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // client
            // 
            this.client.AutoSize = true;
            this.client.BackColor = System.Drawing.SystemColors.HighlightText;
            this.client.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.client.Location = new System.Drawing.Point(468, 7);
            this.client.Name = "client";
            this.client.Size = new System.Drawing.Size(119, 15);
            this.client.TabIndex = 10;
            this.client.Text = "客户端连接数：";
            // 
            // clientcount
            // 
            this.clientcount.AutoSize = true;
            this.clientcount.BackColor = System.Drawing.SystemColors.HighlightText;
            this.clientcount.Location = new System.Drawing.Point(595, 8);
            this.clientcount.Name = "clientcount";
            this.clientcount.Size = new System.Drawing.Size(11, 12);
            this.clientcount.TabIndex = 11;
            this.clientcount.Text = "0";
            // 
            // FTPform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 263);
            this.Controls.Add(this.clientcount);
            this.Controls.Add(this.client);
            this.Controls.Add(this.selectIP);
            this.Controls.Add(this.Port);
            this.Controls.Add(this.IP);
            this.Controls.Add(this.porttext);
            this.Controls.Add(this.pathtext);
            this.Controls.Add(this.select);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.close);
            this.Controls.Add(this.start);
            this.Controls.Add(this.menuStrip1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FTPform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "简单的FTP服务器";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.ToolStripMenuItem 功能ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开启ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关闭软件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button select;
        public System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.TextBox pathtext;
        private System.Windows.Forms.ToolStripMenuItem 什么是FTPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于作者ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 如何使用ToolStripMenuItem;
        private System.Windows.Forms.TextBox porttext;
        private System.Windows.Forms.Label IP;
        private System.Windows.Forms.Label Port;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.ComboBox selectIP;
        private System.Windows.Forms.Label client;
        private System.Windows.Forms.Label clientcount;
    }
}

