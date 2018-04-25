using System;
//using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Data.SqlClient;
using System.Data;

using System.Runtime.InteropServices;

using Common.Logic;
using Common.API;
//using MySql.Data.MySqlClient;
using Common.DataInfo;

namespace GM_Server.JW2API
{
    class ClientHandler
    {
        [DllImport("OperTool.dll", CharSet = CharSet.Ansi)]
        public static extern int BulletinTool(StringBuilder IP, int port, StringBuilder content);
        [DllImport("DSNShell.dll")]
        public static extern void CreateDSNShell();
        [DllImport("DSNShell.dll")]
        public static extern void DestroyDSNShell();
        [DllImport("DSNShell.dll")]
        public static extern int DSNShellUpdate(byte[] result);
        [DllImport("DSNShell.dll", CharSet = CharSet.Unicode)]
        public static extern int DSNShellConnectGW(StringBuilder Ip, int port);
        [DllImport("DSNShell.dll")]
        public static extern int DSNShellIsGWConnected();
        [DllImport("DSNShell.dll", CharSet = CharSet.Unicode)]
        public static extern int DSNShellSendBulletin(StringBuilder msg);
        [DllImport("DSNShell.dll")]
        public static extern int DSNShellKickUser(StringBuilder userID);
        [DllImport("DSNShell.dll", CharSet = CharSet.Unicode)]
        public static extern int DSNShellLoginGW(StringBuilder szAccount, StringBuilder szPassword, StringBuilder szVersion);
        [DllImport("DSNShell.dll")]
        public static extern int DSNShellLoginGWRet(byte[] result);
        public NetworkStream mStream = null;	//网络传输
        System.Threading.Thread ClientThread;
        public TcpClient mTCPClient = null;
        bool ContinueProcess = false;
        public byte[] sendbuff = new byte[10116];
        string serverIP = null;
        int type = -1;
        string contxt = null;
//        private PacketEncryption pe_;
        private Socket clientSock;
        private uint packetcount = 0;
        private bool bConnected;
        private object this_lock = new object();
        private bool loginFlag;
        string au_gsip = null;
        public ClientHandler(TaskBulletion task)
        {
            sendbuff = task.sendbuff;
            serverIP = task.gsServerIP_;
            type = task.type;
            contxt = task.context;

        }
        public void Start() 
		{
			ContinueProcess = true ;
            ClientThread = new System.Threading.Thread(new System.Threading.ThreadStart(Process));
			ClientThread.Start() ;
		}
        public void Stop()
        {
            try
            {
                ContinueProcess = false;
                if (ClientThread != null && ClientThread.IsAlive)
                    ClientThread.Join();
            }
            catch (System.Exception)
            {

            }
        }
        #region 查询serverIP
        /// <summary>
        /// 查询serverIP
        /// </summary>
        /// <param name="serverIP"></param>
        /// <param name="dbid"></param>
        /// <returns></returns>
        public static string JW2_FindDBIP(string serverIP, int dbid)
        {
            string serverName = "";
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[2]{
												   new SqlParameter("@serverIp",SqlDbType.VarChar,50),
												   new SqlParameter("@dbid",SqlDbType.Int) };
                paramCode[0].Value = serverIP;
                paramCode[1].Value = dbid;
                DataSet ds = SqlHelper.ExecSPDataSet("JW2_FindDBIP", paramCode);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    serverName = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return serverName;
        }
        #endregion
        #region 發送公告
        private int Bulletion(string ip,string msg)
        {
            int state = -1;
            int int_Result = -1;
            try
            {
                System.Threading.Thread.Sleep(3000);
                lock (this.this_lock)
                {
                    
                    ip = JW2_FindDBIP(ip, 5);
                    byte[] result = new byte[255];
                    StringBuilder sb_IP = new StringBuilder(ip);

                    if (2 == DSNShellIsGWConnected())
                    {
                        if (0 == DSNShellConnectGW(sb_IP, 58118))  //发送端口
                        {
                            Console.WriteLine("Begin Connect");
                        }
                    }
                    int x = 0;
                    while (true)
                    {
                        System.Threading.Thread.Sleep(500);
                        DSNShellUpdate(result);
                        state = DSNShellIsGWConnected();
                        Console.WriteLine("Now Connection Status：->" + state);
                        if (0 == state)
                        {
                            Console.WriteLine(ip + "：58118-->" + "Connection Success");
                            break;
                        }
                        else
                        {
                            x++;
                            if (x > 10)
                            {
                                Console.WriteLine(ip + "：58118-->" + "Connection OverTime");
                                break;
                            }
                        }
                    }
                    if (0 == DSNShellIsGWConnected())
                    {
                        DSNShellUpdate(result);
                        StringBuilder sb_Msg = new StringBuilder(msg);
                        int_Result = DSNShellSendBulletin(sb_Msg);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return int_Result;
        }
        #endregion
        private void Process()
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                lock (this.this_lock)
                {
                    int result = Bulletion(serverIP,contxt);
                    if (result!=-1)
                        Console.WriteLine(System.DateTime.Now.ToString() + "服务器" + JW2_FindDBIP(serverIP, 5) + "发送公告--->" + contxt + "，成功");
                    else
                        Console.WriteLine(System.DateTime.Now.ToString() + "服务器" + JW2_FindDBIP(serverIP, 5) + "发送公告--->" + contxt + "，失败");
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public byte[] getPacketBuff(byte[] _buff)
        {
            try
            {
                int size = 0;
                for (int i = 1; i < _buff.Length; i++)
                {
                    if (_buff[i] == '\0'
                        && _buff[i + 1] == '\0'
                        && _buff[i + 2] == '\0'
                        && _buff[i + 3] == '\0')
                    {
                        size = i;
                        break;
                    }
                }
                byte[] buff = new byte[size];
                Array.Copy(_buff, buff, size);
                return buff;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        public byte[] CreatePacket(string strMsg, int type)
        {
            try
            {
                uint index = 0;
                byte[] buff = new byte[3];
                buff[0] = (byte)0;
                buff[1] = (byte)4;
                if (type == 1)
                {
                    buff[2] = (byte)1;
                }
                else
                {
                    buff[2] = (byte)0;
                }
                byte[] msgBuf = System.Text.Encoding.Default.GetBytes(strMsg);
                byte[] packet = new byte[msgBuf.Length + 5];
                index += 2;
                Array.Copy(buff, 0, packet, 2, 3);
                index += 3;
                Array.Copy(msgBuf, 0, packet, 5, msgBuf.Length);
                index += Convert.ToUInt16(msgBuf.Length);
                Array.Copy(BitConverter.GetBytes(index), 0, packet, 0, 2);
                return packet;
            }
            catch (System.Exception)
            {
                return null;
            }

        }
        /// <summary>
        /// 初始化线程的启动
        /// </summary>
        public int send(byte[] buffer, int length)
        {
            try
            {
                mStream.Write(buffer, 0, length);
            }
            catch (SocketException ex)
            {

            }
            return 1;
        }
        public bool Alive
        {
            get
            {
                return (ClientThread != null && ClientThread.IsAlive);
            }
        }
    }
}
