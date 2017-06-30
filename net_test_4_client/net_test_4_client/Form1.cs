using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;

namespace ChatClient
{
    public partial class FClient : Form
    {
        public FClient()
        {
            InitializeComponent();
            //关闭对文本框的非法线程操作检查
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }
        //创建 1个客户端套接字 和1个负责监听服务端请求的线程  
        Socket socketClient = null;
        Thread threadClient = null;
        Socket socketFIClient = null;
        Thread threadFIClient = null;
        Socket socketFRClient = null;
        Thread threadFRClient = null;

        public const int SendBufferSize = 2 * 1024;
        public const int ReceiveBufferSize = 8 * 1024;

        private void TCP_Connect(ref Socket sclient, ref Thread tclient, string ip, string port, ThreadStart func)
        {
            //定义一个套字节监听  包含3个参数(IP4寻址协议,流式连接,TCP协议)
            sclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //获取文本框中的IP地址
            IPAddress ipaddress = IPAddress.Parse(ip);
            //将获取的ip地址和端口号绑定到网络节点endpoint上
            IPEndPoint endpoint = new IPEndPoint(ipaddress, int.Parse(port));
            //向指定的ip和端口号的服务端发送连接请求 用的方法是Connect 不是Bind
            sclient.Connect(endpoint);
            //创建一个新线程 用于监听服务端发来的信息
            tclient = new Thread(func);
            //将窗体线程设置为与后台同步
            tclient.IsBackground = true;
            //启动线程
            tclient.Start();
            txtMsg.AppendText("已与服务端:" + ip + "建立连接,可以开始通信...\r\n");
        }

        private void btnBeginListen_Click(object sender, EventArgs e)
        {
            TCP_Connect(ref socketClient, ref threadClient, txtIP.Text, txtPort.Text, RecMsg);
            fileServerStart();
            fileRecService();
        }

        /// <summary>
        /// 接受服务端发来信息的方法
        /// </summary>
        private void RecMsg()
        {
            while (true) //持续监听服务端发来的消息
            {
                string strRecMsg = null;
                int length = 0;
                byte[] buffer = new byte[SendBufferSize];
                try
                {
                    length = socketClient.Receive(buffer);
                }
                catch (Exception ex)
                {
                    txtMsg.AppendText("RecMsg异常消息:" + ex.Message);
                    break;
                }
                //将套接字获取到的字节数组转换为人可以看懂的字符串
                strRecMsg = Encoding.UTF8.GetString(buffer, 0, length);
                if (strRecMsg.Substring(0, 2).Equals("IP"))
                {
                    string t = strRecMsg.Substring(2);
                    TCP_Connect(ref socketFIClient, ref threadFIClient, t, "7701", ACKRecvMsg);
                    //byte[] msg = Encoding.UTF8.GetBytes("HELLO");
                    byte[] msg = Encoding.UTF8.GetBytes(txtFileName.Text);
                    socketFIClient.Send(msg);
                }
                //将文本框输入的信息附加到txtMsg中
                txtMsg.AppendText("服务器:" + GetCurrentTime() + "\r\n" + strRecMsg + "\r\n");
            }
        }

        private void ACKRecvMsg()
        {
            while (true) //持续监听服务端发来的消息
            {
                string strRecMsg = null;
                int length = 0;
                byte[] buffer = new byte[SendBufferSize];
                try
                {
                    length = socketFIClient.Receive(buffer);
                }
                catch (Exception ex)
                {
                    txtMsg.AppendText("ACKRecvMsg异常消息:" + ex.Message);
                    break;
                }
                //将套接字获取到的字节数组转换为人可以看懂的字符串
                strRecMsg = Encoding.UTF8.GetString(buffer, 0, length);
                //if (strRecMsg.Equals("ACCEPT")) {
                //将文本框输入的信息附加到txtMsg中                    
                txtMsg.AppendText("请求连接成功\r\n");
                byte[] msg = Encoding.UTF8.GetBytes(txtFileName.Text);
                socketFIClient.Send(msg);
                txtMsg.AppendText("请求文件成功即将发送\r\n");
                //}
            }
        }

        private void clientFRRecvMsg()
        {
            while (true) //持续监听服务端发来的消息
            {
                string strRecMsg = null;
                int length = 0;
                byte[] buffer = new byte[SendBufferSize];
                try
                {
                    length = socketFRClient.Receive(buffer);
                }
                catch (Exception ex)
                {
                    txtMsg.AppendText("ClientFR异常消息:" + ex.Message);
                    break;
                }
                //将套接字获取到的字节数组转换为人可以看懂的字符串
                strRecMsg = Encoding.UTF8.GetString(buffer, 0, length);
                MessageBox.Show("frclient" + strRecMsg);
            }
        }

        /// <summary>
        /// 发送字符串信息到服务端的方法
        /// </summary>
        private void ClientSendMsg(string sendMsg, byte symbol)
        {
            byte[] arrClientMsg = Encoding.UTF8.GetBytes(sendMsg);
            //实际发送的字节数组比实际输入的长度多1 用于存取标识符
            byte[] arrClientSendMsg = new byte[arrClientMsg.Length + 1];
            arrClientSendMsg[0] = symbol;  //在索引为0的位置上添加一个标识符
            Buffer.BlockCopy(arrClientMsg, 0, arrClientSendMsg, 1, arrClientMsg.Length);

            socketClient.Send(arrClientMsg);
            txtMsg.AppendText("荣婉婷:" + GetCurrentTime() + "\r\n" + sendMsg + "\r\n");
        }

        //向服务端发送信息
        private void btnSend_Click(object sender, EventArgs e)
        {
            ClientSendMsg(txtCMsg.Text, 0);
        }

        //快捷键 Enter 发送信息
        private void txtCMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ClientSendMsg(txtCMsg.Text, 0);
            }
        }

        string filePath = null;   //文件的全路径
        string fileName = null;   //文件名称(不包含路径) 
        //选择要发送的文件
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            if (ofDialog.ShowDialog(this) == DialogResult.OK)
            {
                fileName = ofDialog.SafeFileName; //获取选取文件的文件名
                filePath = @ofDialog.FileName;     //获取包含文件名的全路径
                txtFileName.Text = filePath;      //将文件名显示在文本框上 
            }
        }

        /// <summary>
        /// 获取当前系统时间
        /// </summary>
        public DateTime GetCurrentTime()
        {
            DateTime currentTime = new DateTime();
            currentTime = DateTime.Now;
            return currentTime;
        }

        //点击共享按钮 共享文件
        private void btnSendFile_Click(object sender, EventArgs e)
        {
            ClientSendMsg("ADD " + filePath, 0);
        }

        //点击取消共享按钮 取消共享文件
        private void btnRemoveFile_Click(object sender, EventArgs e)
        {
            ClientSendMsg("DELETE " + filePath, 0);
        }









        //分别创建一个监听客户端的线程和套接字
        Thread threadFIWatch = null;
        Thread threadFRWatch = null;
        Socket socketFIWatch = null;
        Socket socketFRWatch = null;

        private void TCP_ServerStart(ref Socket sWatch, ref Thread tWatch, string ip, int port, ThreadStart func)
        {
            //定义一个套接字用于监听客户端发来的信息  包含3个参数(IP4寻址协议,流式连接,TCP协议)
            sWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //发送信息 需要1个IP地址和端口号
            IPAddress ipaddress = IPAddress.Parse(ip); //获取文本框输入的IP地址
            //将IP地址和端口号绑定到网络节点endpoint上 
            IPEndPoint endpoint = new IPEndPoint(ipaddress, port); //获取文本框上输入的端口号
            //套接字点听绑定网络端点
            sWatch.Bind(endpoint);
            //将套接字的监听队列长度设置为20
            sWatch.Listen(20);
            //创建一个负责监听客户端的线程 
            tWatch = new Thread(func);
            //将窗体线程设置为与后台同步
            tWatch.IsBackground = true;
            //启动线程
            tWatch.Start();
            txtMsg.AppendText("文件服务:" + port.ToString() + "已经启动,开始监听peer传来的信息!" + "\r\n");
        }
        private void fileServerStart()
        {
            TCP_ServerStart(ref socketFIWatch, ref threadFIWatch, txtIP.Text, 7701, WatchFIConnecting);
        }

        private void fileRecService()
        {
            TCP_ServerStart(ref socketFRWatch, ref threadFRWatch, txtIP.Text, 7702, WatchFRConnecting);
        }

        //创建与客户端建立连接的套接字
        Socket socFIConnection = null;

        Socket socFRConnection = null;

        /// <summary>
        /// 持续不断监听客户端发来的请求, 用于不断获取客户端发送过来的连续数据信息
        /// </summary>
        private void WatchFIConnecting()
        {
            while (true)
            {
                try
                {
                    socFIConnection = socketFIWatch.Accept();
                }
                catch (Exception ex)
                {
                    txtMsg.AppendText(ex.Message); //提示套接字监听异常
                    break;
                }
                txtMsg.AppendText("Peer端连接成功,两端可以开始通信了！\r\n");
                //创建通信线程 
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ServerFIRecMsg);
                Thread thr = new Thread(pts);
                thr.IsBackground = true;
                //启动线程 
                thr.Start(socFIConnection);
            }
        }

        private void WatchFRConnecting()
        {
            while (true)
            {
                try
                {
                    socFRConnection = socketFRWatch.Accept();
                }
                catch (Exception ex)
                {
                    txtMsg.AppendText(ex.Message); //提示套接字监听异常
                    break;
                }
                txtMsg.AppendText("Peer端连接成功,两端可以开始通信了！.\r\n");
                //创建通信线程 
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ServerFRRecMsg);
                Thread thr = new Thread(pts);
                thr.IsBackground = true;
                //启动线程 
                thr.Start(socFRConnection);
            }
        }

        /// <summary>
        /// 发送信息到客户端的方法
        /// </summary>
        /// <param name="sendMsg">发送的字符串信息</param>
        private void ServerFISendMsg(string sendMsg)
        {
            MessageBox.Show(sendMsg);
            byte[] buffer = Encoding.UTF8.GetBytes(sendMsg);

            socFIConnection.Send(buffer);
            txtMsg.AppendText("荣婉婷" + GetCurrentTime() + "\r\n" + sendMsg + "\r\n");
        }

        private void FISendMsg(string sendMsg, byte symbol)
        {
            byte[] arrClientMsg = Encoding.UTF8.GetBytes(sendMsg);
            //实际发送的字节数组比实际输入的长度多1 用于存取标识符
            byte[] arrClientSendMsg = new byte[arrClientMsg.Length + 1];
            arrClientSendMsg[0] = symbol;  //在索引为0的位置上添加一个标识符
            Buffer.BlockCopy(arrClientMsg, 0, arrClientSendMsg, 1, arrClientMsg.Length);

            socketFRClient.Send(arrClientSendMsg);
            txtMsg.AppendText("FRCLIENT:" + GetCurrentTime() + "\r\n" + sendMsg + "\r\n");
        }

        /// <summary>
        /// 发送文件的方法
        /// </summary>
        /// <param name="fileFullPath">文件全路径(包含文件名称)</param>
        private void ClientFRSendMsg(string fileFullPath)
        {
            if (string.IsNullOrEmpty(fileFullPath))
            {
                MessageBox.Show(@"请求文件不存在!");
                return;
            }

            //发送文件之前 将文件名字和长度发送过去
            long fileLength = new FileInfo(fileFullPath).Length;
            string totalMsg = string.Format("{0}-{1}", fileName, fileLength);
            FISendMsg("D:\\share\\" + totalMsg, 2);


            //发送文件
            byte[] buffer = new byte[SendBufferSize];

            using (FileStream fs = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read))
            {
                int readLength = 0;
                bool firstRead = true;
                long sentFileLength = 0;
                while ((readLength = fs.Read(buffer, 0, buffer.Length)) > 0 && sentFileLength < fileLength)
                {
                    sentFileLength += readLength;
                    //在第一次发送的字节流上加个前缀1
                    if (firstRead)
                    {
                        byte[] firstBuffer = new byte[readLength + 1];
                        firstBuffer[0] = 1; //告诉机器该发送的字节数组为文件
                        Buffer.BlockCopy(buffer, 0, firstBuffer, 1, readLength);

                        socketFRClient.Send(firstBuffer, 0, readLength + 1, SocketFlags.None);

                        firstRead = false;
                        continue;
                    }
                    //之后发送的均为直接读取的字节流
                    socketFRClient.Send(buffer, 0, readLength, SocketFlags.None);
                }
                fs.Close();
            }
            txtMsg.AppendText("socketFRClient:" + GetCurrentTime() + "\r\n您发送了文件:" + fileName + "\r\n");
        }

        /// <summary>
        /// 接收客户端发来的信息
        /// </summary>
        private void ServerFIRecMsg(object socketClientPara)
        {
            Socket socketServer = socketClientPara as Socket;
            while (true)
            {
                int firstReceived = 0;
                byte[] buffer = new byte[ReceiveBufferSize];
                try
                {
                    string strSRecMsg = null;
                    //获取接收的数据,并存入内存缓冲区  返回一个字节数组的长度
                    if (socketServer != null) firstReceived = socketServer.Receive(buffer);

                    if (firstReceived > 0) //接受到的长度大于0 说明有信息或文件传来
                    {
                        strSRecMsg = Encoding.UTF8.GetString(buffer, 0, firstReceived);
                        MessageBox.Show(strSRecMsg);
                        //if (strSRecMsg == "HELLO") {
                        TCP_Connect(ref socketFRClient, ref threadFRClient, txtIP.Text, "7702", clientFRRecvMsg);
                        //    ServerFISendMsg("ACCEPT");
                        txtMsg.AppendText("ServerFi:" + GetCurrentTime() + "\r\n" + strSRecMsg + "\r\n");
                        ClientFRSendMsg(strSRecMsg);
                    }
                }
                catch (Exception ex)
                {
                    txtMsg.AppendText("ServerFI异常消息:" + ex.Message);
                    break;
                }
            }
        }

        string strSRecMsg1 = null;
        /// <summary>
        /// 接收客户端发来的信息
        /// </summary>
        private void ServerFRRecMsg(object socketClientPara)
        {
            Socket socketServer = socketClientPara as Socket;

            long fileLength = 0;
            while (true)
            {
                int firstReceived = 0;
                byte[] buffer = new byte[ReceiveBufferSize];
                try
                {
                    //获取接收的数据,并存入内存缓冲区  返回一个字节数组的长度
                    if (socketServer != null) firstReceived = socketServer.Receive(buffer);

                    if (firstReceived > 0) //接受到的长度大于0 说明有信息或文件传来
                    {
                        if (buffer[0] == 0) //0为文字信息
                        {
                            strSRecMsg1 = Encoding.UTF8.GetString(buffer, 1, firstReceived - 1);//真实有用的文本信息要比接收到的少1(标识符)
                            txtMsg.AppendText("SoFlash:" + GetCurrentTime() + "\r\n" + strSRecMsg1 + "\r\n");
                        }
                        if (buffer[0] == 2)//2为文件名字和长度
                        {
                            string fileNameWithLength = Encoding.UTF8.GetString(buffer, 1, firstReceived - 1);
                            strSRecMsg1 = fileNameWithLength.Split('-').First(); //文件名
                            string l = fileNameWithLength.Split('-').Last();
                            //fileLength = Convert.ToInt64(l);//文件长度
                            fileLength = long.Parse(l.Split(' ').First());
                            int a = 1;
                            int b = 1;
                        }
                        if (buffer[0] == 1)//1为文件
                        {
                            string fileNameSuffix = strSRecMsg1.Substring(strSRecMsg1.LastIndexOf('.')); //文件后缀
                            SaveFileDialog sfDialog = new SaveFileDialog()
                            {
                                Filter = "(*" + fileNameSuffix + ")|*" + fileNameSuffix + "", //文件类型
                                FileName = strSRecMsg1
                            };

                            //如果点击了对话框中的保存文件按钮 
                            if (sfDialog.ShowDialog(this) == DialogResult.OK)
                            {
                                string savePath = sfDialog.FileName; //获取文件的全路径
                                //保存文件
                                int received = 0;
                                long receivedTotalFilelength = 0;
                                bool firstWrite = true;
                                using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                                {
                                    while (receivedTotalFilelength < fileLength) //之后收到的文件字节数组
                                    {
                                        if (firstWrite)
                                        {
                                            fs.Write(buffer, 1, firstReceived - 1); //第一次收到的文件字节数组 需要移除标识符1 后写入文件
                                            fs.Flush();

                                            receivedTotalFilelength += firstReceived - 1;

                                            firstWrite = false;
                                            continue;
                                        }
                                        received = socketServer.Receive(buffer); //之后每次收到的文件字节数组 可以直接写入文件
                                        fs.Write(buffer, 0, received);
                                        fs.Flush();

                                        receivedTotalFilelength += received;
                                    }
                                    fs.Close();
                                }

                                string fName = savePath.Substring(savePath.LastIndexOf("\\") + 1); //文件名 不带路径
                                string fPath = savePath.Substring(0, savePath.LastIndexOf("\\")); //文件路径 不带文件名
                                txtMsg.AppendText("ServerFRRecMsg:" + GetCurrentTime() + "\r\n您成功接收了文件" + fName + "\r\n保存路径为:" + fPath + "\r\n");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    txtMsg.AppendText("ServerFRRecMsg异常消息:" + ex.Message);
                    break;
                }
            }
        }

        private void FClient_Load(object sender, EventArgs e)
        {

        }


    }
}
