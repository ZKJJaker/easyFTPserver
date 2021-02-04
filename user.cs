using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace easyFTPserver
{
    /// <summary>
    /// 保存连接设置信息
    /// </summary>
    public class UserSeesion
    {
        //网络流
        private NetworkStream networkStream;
        //向网络流里写入或者读取内容
        public readonly StreamReader streamReader;
        public readonly StreamWriter streamWriter;
        // TcpClient对象代表一个客户端对象
        public readonly TcpClient tcpClient;
        //将数据转换为二进制 来写入或者读取
        public readonly BinaryReader binaryReader;
        public readonly BinaryWriter binaryWriter;
        //保存客户端的会话
        public UserSeesion(TcpClient client)
        {
            this.tcpClient = client;
            networkStream = tcpClient.GetStream();
            streamReader = new StreamReader(networkStream, Encoding.Default);
            streamWriter = new StreamWriter(networkStream, Encoding.Default);
            streamWriter.AutoFlush = true;
            binaryReader = new BinaryReader(networkStream, Encoding.UTF8);
            binaryWriter = new BinaryWriter(networkStream, Encoding.UTF8);
        }

        public void Close()
        {
            tcpClient.Client.Shutdown(SocketShutdown.Both);
            tcpClient.Client.Close();
            tcpClient.Close();
        }
    }
    /// <summary>
    /// 保存与客户端通信需要的信息
    /// </summary>
    class User
    {
        //用户的数量
         public int userCount { get; set; }
        //命令会话
        public UserSeesion commandSession { get; set; }
        //数据会话
        public UserSeesion dataSession { get; set; }
        //数据监听 用于被动的时候
        public TcpListener dataListener { get; set; }

        // 主动模式下使用的客户端监听的IPEndPoint
        public IPEndPoint remoteEndPoint { get; set; }
        // 用户名
        public string userName { get; set; }

        // 工作主目录
        public string subjectDir { get; set; }

        // 当前工作目录
        public string currentDir { get; set; }

        // 初始状态为等待输入用户名
        public int loginStatus { get; set; }

        // 是否使用二进制数据传输方式
        public bool isBinary { get; set; }

        // 数据连接使用的是否被动连接
        public bool isPassive { get; set; }
        public User() {
            userCount += 1;
        }
        ~User() {
            userCount -= 1;
            Console.WriteLine("现在的用户数量："+userCount);
        }
        public void showCount()
        {
            Console.WriteLine("现在的用户数量："+userCount);
        }
    }
}
