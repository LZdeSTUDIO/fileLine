using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileLine
{
    public partial class Form1 : Form
    {
        //UI 
        #region
        //创建一个委托，是为访问TextBox控件服务的。
        public delegate void UpdateTxt(string msg);
        //修改TextBox值的方法。
        public void UpdateTxtMethod(string msg)
        {
            label1.Text = msg;
        }
        //定义一个委托变量
        public UpdateTxt updateTxt;
        #endregion

        //server&client
        private static int myProt = 8885;   //端口

        //server
        private static byte[] result = new byte[1024];
        static Socket serverSocket;

        //client


        //test
        int i = 0;
        FileRead test;


        public Form1()
        {
            InitializeComponent();

            test = new FileRead();
            socketStart();
        }

        //服务器方法
        #region
        /// <summary>
        /// 开启服务器监听
        /// </summary>
        private void socketStart()
        {
            var ipAd =  new tools().GetAddressIP();
            //服务器IP地址
            IPAddress ip = IPAddress.Parse(ipAd);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myProt));  //绑定IP地址：端口
            serverSocket.Listen(10);    //设定最多10个排队连接请求
            Debug.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());
            //通过Clientsoket发送数据
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
        }
        /// <summary>
        /// 监听客户端连接
        /// </summary>
        private  void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                clientSocket.Send(Encoding.ASCII.GetBytes("U just connect me!"));
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientSocket);
            }
        }
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="clientSocket"></param>
        private void ReceiveMessage(Object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    //通过clientSocket接收数据
                    int receiveNumber = myClientSocket.Receive(result);

                    if (i < 10)
                    {
                        test.write(result, receiveNumber);
                        i++;
                    }
                    else
                    {
                        test.dispose();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    myClientSocket.Shutdown(SocketShutdown.Both);
                    myClientSocket.Close();
                    break;
                }
            }
        }
        #endregion
    }
}
