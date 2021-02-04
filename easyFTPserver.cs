using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace easyFTPserver
{
    public partial class FTPform : Form
    {
        private string ftppath = @"C:\Users\ZKJ_Jaker\Desktop\数据库系统工程师真题";
        private int port;
        private IPAddress IPad;
        TcpListener myTcpListener = null;
        private Thread listenThread;

        public FTPform()
        {
            //ComboBox.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            // 设置默认的Ip和端口
            selectIP.Items.AddRange(Dns.GetHostAddresses(""));
            selectIP.SelectedIndex = selectIP.Items.Count - 1;
            porttext.Text = "21";
        }
        private void select_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (folderBrowser.SelectedPath != "")
                {
                    ftppath = folderBrowser.SelectedPath;
                    pathtext.AppendText("已选择" + ftppath + "为主目录\r\n");
                }
                else
                {
                    pathtext.AppendText("选择的路径有误，请重新选择！\r\n");
                }
            }
        }
        //开启服务器
        private void start_Click(object sender, EventArgs e)
        {
            if (int.Parse(porttext.Text) > 65535)
            {
                pathtext.AppendText("端口不要超过65535\r\n");
                pathtext.AppendText("建议使用默认端口21\r\n");
            }
            else if (myTcpListener != null)
            {
                pathtext.AppendText("服务已经开启\r\n");
            }
            //else if (selectIP.Text != "" && porttext.Text != ""&& ftppath!="")
            else if (selectIP.Text != "" && porttext.Text != "")
            {
                IPad = IPAddress.Parse(selectIP.Text);
                port = int.Parse(porttext.Text);
                pathtext.AppendText("正在" + IPad + ":" + port + "上\r\n主目录为[" + ftppath + "]下开启服务\r\n");
                listenThread = new Thread(ListenClientConnect);
                listenThread.IsBackground = true;
                listenThread.Start();
            }
            else
            {
                pathtext.AppendText("请检查IP、端口和文件主目录\r\n");
            }

        }
        // 向屏幕输出显示状态信息（这里使用了委托机制）
        private delegate void ShowInfoDelegate(string str);
        // 监听端口，处理客户端连接
        private void ListenClientConnect()
        {
            myTcpListener = new TcpListener(IPad, port);
            // 开始监听入站的请求
            myTcpListener.Start();
            ShowInfo("正在" + IPad + ":" + port + "上成功开启服务\r\n");
            while (true)
            {
                try
                {

                    //创建一个tcp连接，接收连接请求
                    TcpClient tcpClient = myTcpListener.AcceptTcpClient();
                    ShowInfo(string.Format("客户端（{0}）与本机（{1}）建立Ftp连接\r\n", tcpClient.Client.RemoteEndPoint, myTcpListener.LocalEndpoint));

                    User user = new User();
                    user.commandSession = new UserSeesion(tcpClient);
                    user.currentDir = ftppath;
                    user.subjectDir = ftppath;
                    user.remoteEndPoint = (IPEndPoint)myTcpListener.LocalEndpoint;
                    Thread th = new Thread(userClient);
                    th.IsBackground = true;
                    th.Start(user);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("建立连接失败" + ex);
                    break;
                }
            }
        }
        private void ShowInfo(string str)
        {
            // 如果调用ShowInfo()方法的线程与创建ListView控件的线程不在一个线程时
            // 此时利用委托在创建ListView的线程上调用
            if (pathtext.InvokeRequired == true)
            {
                ShowInfoDelegate d = new ShowInfoDelegate(ShowInfo);
                this.Invoke(d, str);
            }
            else
            {
                pathtext.AppendText(str);
            }
        }
        //回应客户端
        private void reply_user(User user, string sendstr)
        {
            try
            {
                user.commandSession.streamWriter.WriteLine(sendstr);
                ShowInfo(string.Format("向客户端（{0}）发送[{1}]\r\n", user.commandSession.tcpClient.Client.RemoteEndPoint, sendstr));
            }
            catch
            {
                ShowInfo(string.Format("向客户端（{0}）发送信息失败\r\n", user.commandSession.tcpClient.Client.RemoteEndPoint));
            }
        }
        //处理客户端的请求
        private void userClient(object obj)
        {
            User user = (User)obj;
            user.showCount();
            string sendString = "220 FTP Server v0.1";
            reply_user(user, sendString);
            while (true)
            {
                string receive_str = null;
                try
                {
                    // 读取客户端发来的请求信息
                    receive_str = user.commandSession.streamReader.ReadLine();
                    ShowInfo(user.commandSession.tcpClient.Client.RemoteEndPoint + "客户端>>>" + receive_str + "\r\n");
                }
                catch (Exception ex)
                {
                    //判断客户端的连接是否存在
                    if (!user.commandSession.tcpClient.Connected)
                    {
                        ShowInfo(string.Format("客户端({0}断开连接！)", user.commandSession.tcpClient.Client.RemoteEndPoint));
                    }
                    else
                    {
                        ShowInfo("客户端未连接！\r\n" + ex.Message);
                    }
                    break;
                }

                if (receive_str == null)
                {
                    ShowInfo("接收的字符串为null，线程结束！\r\n");
                    break;
                }
                //分解客户端的请求命令和参数
                string[] rec_str = receive_str.Split(' ');
                string command = rec_str[0].ToUpper();
                string param = rec_str.Length > 1 ? rec_str[1] : string.Empty;
                //优先判断是不是退出
                if (command == "QUIT")
                {
                    user.commandSession.Close();
                    return;
                }
                if (command == "OPTS")
                {
                    //OPTS UTF8 ON
                    string param2 = rec_str.Length > 1 ? rec_str[2] : string.Empty;
                    if (param == "UTF8")
                    {
                        if (param2 == "ON")
                        {
                            reply_user(user, "200 OPTS UTF8 command successful - UTF8 encoding now ON");
                            ShowInfo("使用UTF8解码成功，默认编码也是UTF8");
                        }
                        else if (param2 == "OFF")
                        {
                            reply_user(user, "200 OPTS UTF8 command successful - UTF8 encoding now ON");
                            ShowInfo("使用UTF8解码失败，默认编码也是UTF8");
                        }
                        else
                        {
                            reply_user(user, "502 command is not implemented.");
                        }
                    }
                }
                else if (user.loginStatus == 0 && command == "USER")
                {
                    reply_user(user, "331 USER command OK, password required.");
                    user.userName = param;
                    user.loginStatus = 1;
                }
                else if (user.loginStatus == 1 && command == "PASS")
                {
                    //可以调用commandPASS 进行身份验证 这里为了方便都匿名
                    reply_user(user, "230 User logged in success");
                    user.loginStatus = 2;
                    user.dataSession = user.commandSession;
                }
                else if (user.loginStatus == 2)
                {
                    switch (command)
                    {
                        case "SIZE":
                            reply_user(user, "213 4096");
                            break;
                        case "PWD":
                            commandPWD(user);
                            break;
                        case "TYPE":
                            commandTYPE(user, param);
                            break;
                        case "SYST":
                            reply_user(user, "213 FTPserver" + user.currentDir);
                            break;
                        case "STAT":
                            reply_user(user, "214 aa bb cc dd");
                            break;
                        case "OPTS":
                            reply_user(user, "200 OKOKOK");
                            break;
                        case "CWD":
                            if (param != "")
                            {
                                param = receive_str.Substring(4);
                            }
                            commandCWD(user, param);
                            break;
                        case "PASV":
                            commandPASV(user);
                            break;
                        case "PORT":
                            commandPORT(user, param);
                            break;
                        case "LIST":
                            if (param!=string.Empty)
                            {
                                param = receive_str.Substring(4);
                            }
                            commandLIST(user, param);
                            break;
                        case "NLIST":
                            commandLIST(user, param);
                            break;
                        // 处理下载文件命令
                        case "RETR":
                            commandRETR(user, param);
                            break;
                        // 处理上传文件命令
                        case "STOR":
                            commandSTOR(user, param);
                            break;
                        // 处理删除命令
                        case "DELE":
                            commandDELE(user, param);
                            break;
                        default:
                            sendString = "500 command is not implemented.";
                            reply_user(user, sendString);
                            break;
                    }
                }
                else
                {
                    sendString = "502 command is not implemented.";
                    reply_user(user, sendString);
                }
            }
        }

        //PASS 进行身份验证
        private void commandPASS(User user)
        {
            //扩展方案 增加身份验证 可以自己创建用户和密码保存到文本
            //if (名字一样，密码一样) { 
            //    send_string = "230 User logged in success";
            //    user.loginStatus = 2;
            //}
            //else
            //{
            //    send_string =  "530 Password incorrect.";
            //}
            string sendString = "230 User logged in success";
            user.loginStatus = 2;
        }
        //互相确认传输方式
        private void commandTYPE(User user, string param)
        {
            string send_string;
            if (param == "I")
            {
                // 二进制
                send_string = "220 Type set to I(Binary)";
            }
            else
            {
                // ASCII方式
                send_string = "330 Type set to A(ASCII)";
            }
            reply_user(user, send_string);
        }
        //切换目录
        private void commandCWD(User user, string str)
        {
            string sendString = string.Empty;
            try
            {
                //把要去的目录清洗无用字符
                //str = str.Replace(" ", "");
                str = str.Replace("/", "\\");
                user.currentDir += "\\";
                str = str.EndsWith("\\") ? str : str+"\\";
                //如果要切换的目录在主目录下 才可以切换
                Console.WriteLine(str.Contains(user.subjectDir));
                if (str.Contains(user.subjectDir))
                {
                    
                    // 是否为当前目录的子目录，且不包含父目录名称
                    if (Directory.Exists(str))
                    {
                        user.currentDir = str;
                        sendString = "250 Directory changed to " + str + " successfully";
                    }
                    else
                    {
                        sendString = "550 Directory '" + str + "' does not exist";
                    }
                }
                else if (Directory.Exists(user.currentDir + str))
                {
                    user.currentDir = user.currentDir + str;
                    sendString = "250 Directory changed successfully";
                }
                else
                {
                    sendString = "250 Directory changed to " + user.currentDir + " successfullys";
                }
            }
            catch
            {
                sendString = "502 Directory changed unsuccessfully";
            }

            reply_user(user, sendString);

        }
        // 处理PWD命令，显示工作目录
        private void commandPWD(User user)
        {
            string sendString = string.Empty;
            sendString = "257 " + user.currentDir;
            reply_user(user, sendString);
        }
        //确认是主动还是被动连接
        private void commandPASV(User user)
        {
            string send_string;
            IPAddress localip = Dns.GetHostEntry("").AddressList[Dns.GetHostEntry("").AddressList.Length - 1];

            // 被动模式，即服务器接收客户端的连接请求
            // 被动模式下FTP服务器使用随机生成的端口进行传输数据
            // 而主动模式下FTP服务器使用端口20进行数据传输
            Random random = new Random();
            int random1, random2;
            int port;
            while (true)
            {
                // 随机生成一个端口进行数据传输
                random1 = random.Next(5, 200);
                random2 = random.Next(0, 200);
                // 生成的端口号控制>1024的随机端口
                // 下面这个运算算法只是为了得到一个大于1024的端口值 <<8 左移8位相当于乘以256
                port = random1 << 8 | random2;
                try
                {
                    user.dataListener = new TcpListener(localip, port);
                    ShowInfo(localip.ToString() + "：" + port + "—已准备好服务（被动模式）");
                    user.isPassive = true;
                }
                catch
                {
                    continue;
                }
                string temp = localip.ToString().Replace('.', ',');
                send_string = "227 Entering Passive Mode(" + temp + "," + random1 + "," + random2 + ")";

                reply_user(user, send_string);
                // 必须把端口号IP地址告诉客户端，客户端接收到响应命令后，
                // 再通过新的端口连接服务器的端口，然后进行文件数据传输
                user.dataListener.Start();
                break;

            }
        }
        // 处理PORT命令，使用主动模式进行传输
        private void commandPORT(User user, string portstring)
        {
            // 主动模式时，客户端必须告知服务器接收数据的端口号，PORT 命令格式为：PORT address
            // address参数的格式为i1、i2、i3、i4、p1、p2,其中i1、i2、i3、i4表示IP地址
            // 下面通过.字符串来组合这四个参数得到IP地址
            // p1、p2表示端口号，下面通过int.Parse(temp[4]) << 8) | int.Parse(temp[5]
            // 这个算法来获得一个大于1024的端口来发送给服务器
            string sendString = string.Empty;
            string[] temp = portstring.Split(',');
            string ipString = "" + temp[0] + "." + temp[1] + "." + temp[2] + "." + temp[3];

            // 客户端发出PORT命令把客户端的IP地址和随机的端口告诉服务器
            int portNum = (int.Parse(temp[4]) << 8) | int.Parse(temp[5]);
            user.remoteEndPoint = new IPEndPoint(IPAddress.Parse(ipString), portNum);
            sendString = "200 PORT command successful.";

            // 服务器以接受到的客户端IP地址和端口为目标发起主动连接请求
            // 服务器根据客户端发送过来的IP地址和端口主动发起与客户端建立连接
            reply_user(user, sendString);
        }
        //显示文件列表
        private void commandLIST(User user, string param)
        {
            string sendString = string.Empty;
            //提供时间信息
            DateTimeFormatInfo dateTimeFormat = new CultureInfo("en-US", true).DateTimeFormat;
            // 得到目录列表

            string[] dir, file;
            //如果目录+参数是目录 返回目录下的文件夹和文件
            user.currentDir += '\\';
            if (param == "-l")
            {
                dir = Directory.GetDirectories(user.currentDir);
                file = Directory.GetFiles(user.currentDir);
            }
            else if (Directory.Exists(user.currentDir + param) && !string.IsNullOrEmpty(param))
            {
                //string s = user.currentDir.TrimEnd('/');
                //user.currentDir = s.Substring(0, s.LastIndexOf("/") + 1);
                user.currentDir += param + "\\";
                dir = Directory.GetDirectories(user.currentDir);
                file = Directory.GetFiles(user.currentDir);
            }
            else
            {
                dir = Directory.GetDirectories(user.currentDir);
                file = Directory.GetFiles(user.currentDir);
            }
            for (int i = 0; i < dir.Length; i++)
            {
                //如果是目录返回目录名如果是文件返回文件名
                string folderName = Path.GetFileName(dir[i]);
                DirectoryInfo d = new DirectoryInfo(dir[i]);
                //DateTime d = new DateTime();
                // 按下面的格式输出目录列表
                //Unix格式
                sendString += "drw------- " + "1 user user 0" + d.CreationTime.ToString(" MMM dd yyyy") + "\t" + folderName  + Environment.NewLine;
                //windows格式
                //sendString += d.CreationTimeUtc.ToString("MM-dd-yy  HH:mmtt") + "  <DIR> " + folderName + Environment.NewLine;
            }

            for (int i = 0; i < file.Length; i++)
            {
                FileInfo f = new FileInfo(file[i]);
                if (f.Extension == ".ini") { continue; }
                string fileName = Path.GetFileName(file[i]);
                // 按下面的格式输出文件列表
                //Unix格式
                sendString += "-rw------- " + "1 user user " + f.Length + f.CreationTime.ToString(" MMM dd yyyy ") + f.Name + Environment.NewLine;
                //windows格式
                //sendString += f.CreationTimeUtc.ToString("MM-dd-yy HH:mmtt") + "  " + f.Length + "               " + f.Name + Environment.NewLine;
            }
            // List命令指示获得FTP服务器上的文件列表字符串信息
            // 所以调用List命令过程，客户端接受的指示一些字符串
            // 所以isBinary是false,代表传输的是ASCII数据

            // 但是为了防止isBinary因为 设置user.isBinary = false而改变
            // 所以事先保存user.IsBinary的引用（此时为true）,方便后面下载文件
            //bool isBinary = user.isBinary;
            //user.isBinary = false;
            reply_user(user, "125 connection already open; Transfer starting.");
            InitDataSession(user);
            SendByUserSession(user, sendString);
            reply_user(user, "226 Transfer complete");
            //user.isBinary = isBinary;
        }
        //下载文件
        private void commandRETR(User user, string filename)
        {
            string sendString = "";

            // 下载的文件全名

            string path = filename;
            if (!File.Exists(path))
            {
                sendString = "450 文件不存在";
                reply_user(user, sendString);
                return;
            }
            FileStream filestream = new FileStream(path, FileMode.Open, FileAccess.Read);

            // 发送150到用户，表示服务器文件状态良好，将要打开数据连接传输文件
            if (user.isBinary)
            {
                sendString = "150 Opening BINARY mode data connection for download";
            }
            else
            {
                sendString = "150 Opening ASCII mode data connection for download";
            }

            reply_user(user, sendString);
            InitDataSession(user);
            SendFileByUserSession(user, filestream);
            reply_user(user, "226 Transfer complete");
        }
        //上传文件
        private void commandSTOR(User user, string filename)
        {
            string sendString = "";
            // 上传的文件全名
            string path = user.currentDir + filename;
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);

            // 发送150到用户，表示服务器状态良好
            if (user.isBinary)
            {
                sendString = "150 Opening BINARY mode data connection for upload";
            }
            else
            {
                sendString = "150 Opeing ASCII mode data connection for upload";
            }

            reply_user(user, sendString);
            InitDataSession(user);
            ReadFileByUserSession(user, fs);
            reply_user(user, "226 Transfer complete");
        }
        //删除文件
        private void commandDELE(User user, string filename)
        {
            string sendString = "";
            // 删除的文件全名
            string path = user.currentDir + filename;
            ShowInfo("正在删除文件" + filename + "...");
            File.Delete(path);
            ShowInfo("删除成功");
            sendString = "250 File " + filename + " has been deleted.";
            reply_user(user, sendString);
        }
        // 初始化数据连接
        private void InitDataSession(User user)
        {
            TcpClient client = null;
            if (user.isPassive)
            {
                ShowInfo("采用被动模式返回LIST目录和文件列表");
                client = user.dataListener.AcceptTcpClient();
            }
            else
            {
                ShowInfo("采用主动模式向用户发送LIST目录和文件列表");
                client = new TcpClient();
                client.Connect(user.remoteEndPoint);
            }

            user.dataSession = new UserSeesion(client);
        }
        // 使用数据连接发送字符串
        private void SendByUserSession(User user, string sendString)
        {
            ShowInfo("\r\n向用户发送(字符串信息)>>>>>" + sendString + "\r\n");
            try
            {
                user.dataSession.streamWriter.WriteLine(sendString);
                ShowInfo("发送完毕\r\n");
            }
            finally
            {
                user.dataSession.Close();
            }
        }

        // 使用数据连接发送文件流（客户端下载文件命令）
        private void SendFileByUserSession(User user, FileStream fs)
        {
            ShowInfo("向用户发送(文件流)：[...");
            try
            {
                if (user.isBinary)
                {
                    byte[] bytes = new byte[1024];
                    BinaryReader binaryReader = new BinaryReader(fs);
                    int count = binaryReader.Read(bytes, 0, bytes.Length);
                    while (count > 0)
                    {
                        user.dataSession.binaryWriter.Write(bytes, 0, count);
                        user.dataSession.binaryWriter.Flush();
                        count = binaryReader.Read(bytes, 0, bytes.Length);
                    }
                }
                else
                {
                    StreamReader streamReader = new StreamReader(fs);
                    while (streamReader.Peek() > -1)
                    {
                        user.dataSession.streamWriter.WriteLine(streamReader.ReadLine());
                    }
                }

                ShowInfo("...]发送完毕！");
            }
            finally
            {
                user.dataSession.Close();
                fs.Close();
            }
        }

        // 使用数据连接接收文件流(客户端上传文件功能)
        private void ReadFileByUserSession(User user, FileStream fs)
        {
            ShowInfo("接收用户上传数据（文件流）：[...");
            try
            {
                if (user.isBinary)
                {
                    byte[] bytes = new byte[1024];
                    BinaryWriter binaryWriter = new BinaryWriter(fs);
                    int count = user.dataSession.binaryReader.Read(bytes, 0, bytes.Length);
                    while (count > 0)
                    {
                        binaryWriter.Write(bytes, 0, count);
                        binaryWriter.Flush();
                        count = user.dataSession.binaryReader.Read(bytes, 0, bytes.Length);
                    }
                }
                else
                {
                    StreamWriter streamWriter = new StreamWriter(fs);
                    while (user.dataSession.streamReader.Peek() > -1)
                    {
                        streamWriter.Write(user.dataSession.streamReader.ReadLine());
                        streamWriter.Flush();
                    }
                }

                ShowInfo("...]接收完毕");
            }
            finally
            {
                user.dataSession.Close();
                fs.Close();
            }
        }
        //关闭按钮
        private void close_Click(object sender, EventArgs e)
        {
            if (myTcpListener != null)
            {
                pathtext.AppendText("正在停止服务……\r\n");
                myTcpListener.Stop();
                myTcpListener = null;
                pathtext.AppendText("服务停止成功\r\n");
            }
            else
            {
                pathtext.AppendText("服务还未开始，不要心急！\r\n");
            }

        }
        //关闭并退出按钮
        private void exit_Click(object sender, EventArgs e)
        {
            if (myTcpListener != null)
            {
                myTcpListener.Stop();
            }
            Close();
        }
        private void 如何使用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1、点击选择文件夹，选择要开启FTP的文件夹，输入端口\n2、点击开启，启动FTP服务。\n用完了不想用了就点关闭/微笑\n主要要给端口开放权限", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 关于作者ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("作者：Jaker\n联系方式：QQ2205909051\n本软件受法律保护！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 什么是FTPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FTP（File Transfer Protocol，文件传输协议） 是 TCP/IP 协议组中的协议之一。主要是用来共享文件的/peace", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pathtext.AppendText("已选择" + selectIP.Text + "为IP\r\n");
        }

        private void pathtext_TextChanged(object sender, EventArgs e)
        {
        }

    }
}
