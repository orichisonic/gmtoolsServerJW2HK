using System;
using System.Threading;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.InteropServices;

using Common.Logic;
using Common.API;

using Common.DataInfo;


namespace GM_Server.JW2API
{
    /// <summary>
    /// Task 的摘要说明。
    /// </summary>
    public class TaskBulletion
    {

        #region DLL加载
        /// <summary>
        ///创建DSNShell（返回值为DSNShell的句柄）
        /// </summary>
        [DllImport("DSNShell.dll")]
        public static extern int CreateDSNShell();
        /// <summary>
        ///销毁DSNShell（句柄）
        /// </summary>
        [DllImport("DSNShell.dll")]
        public static extern void DestroyDSNShell(int handle);
        /// <summary>
        ///更新DSNShell（句柄，收到消息的标志）
        /// </summary>
        [DllImport("DSNShell.dll")]
        public static extern int DSNShellUpdate(int handle, byte[] result);
        /// <summary>
        ///连接GW（句柄，IP，端口）---Send
        /// </summary>
        [DllImport("DSNShell.dll", CharSet = CharSet.Unicode)]
        public static extern int DSNShellConnectGW(int handle, string Ip, int port);
        /// <summary>
        ///连接GW（句柄，IP，端口）---Rev
        /// </summary>
        [DllImport("DSNShell.dll")]
        public static extern int DSNShellIsGWConnected(int handle);

        /// <summary>
        ///连接GW（句柄，IP，端口）---Rev
        /// </summary>
        [DllImport("DSNShell.dll", CharSet = CharSet.Unicode)]
        public static extern int DSNShellSendBulletin(int handle,string szContent);
        #endregion

//        ConnectionPool ConnectionPool = null;
        private object this_lock = new object();
//        TaskManager ClientTask = null;
        public TcpClient mTCPClient = null;
        public NetworkStream mStream = null;	//网络传输
        public byte[] sendbuff = null;
        public int status = 0;
        public int command = 0;
        public int taskID = 0;
        public int interval = 0;
        public int type = -1;
        public int timeOut = 0;
        public int timeStart = 0;
        public int startTime = 0;
        public string gsServerIP_ = null;
        public string context = null;
        public int atonce = 0;
        System.Collections.ArrayList ClientSockets;
        private static bool ContinueReclaim = true;
//        private PacketEncryption pe_;
        private uint packetcount = 0;
        public TaskBulletion(int i)
        {

        }
        public TaskBulletion()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        ~TaskBulletion()
        { 
            
        }
        public bool Connect(string strAddress, int iPort)
        {
            bool bConnect = false;

            try
            {
                mTCPClient = new TcpClient();
                mTCPClient.Connect(strAddress, iPort);
                mTCPClient.ReceiveBufferSize = 8192 * 50;
                mStream = mTCPClient.GetStream();

                bConnect = true;
            }
            catch (SystemException e)
            {
                Console.WriteLine("服务器无法连接!");
                bConnect = false;
            }

            return bConnect;
        }

        public byte[] getPacketBuff(byte[] _buff)
        {
            int size = 0;
            for (int i = 1; i < _buff.Length; i++)
            {
                if (_buff[i] == '\0')
                {
                    size = i;
                    break;
                }
            }
            byte[] buff = new byte[size];
            Array.Copy(_buff, buff, size);
            return buff;
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
                byte[] packet = new byte[msgBuf.Length + 6];
                index += 2;
                Array.Copy(buff, 0, packet, 2, 3);
                index += 3;
                index += 1;
                Array.Copy(BitConverter.GetBytes(msgBuf.Length), 0, packet, 5, 1);
                Array.Copy(msgBuf, 0, packet, 6, msgBuf.Length);
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
        #region 开辟一个客户端单独线程
        private void Reclaim()
        {
            try
            {
                while (ContinueReclaim)
                {
                    lock (ClientSockets.SyncRoot)
                    {
                        for (int x = ClientSockets.Count - 1; x >= 0; x--)
                        {
                            Object Client = ClientSockets[x];
                            if (!((ClientHandler)Client).Alive)
                            {
                                ClientSockets.Remove(Client);
                                ((ClientHandler)Client).Stop();
                            }
                        }
                    }
                    Thread.Sleep(200);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                //SqlHelper.log.WriteLog(ex.Message);

            }
        }
        #endregion



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
        private int Bulletion(string ip, string msg)
        {
            int handle = -1;
            Console.WriteLine("Sending Bulletion...");
            int state = -1;
            int int_Result = -1;
            try
            {
                lock (this.this_lock)
                {
                    System.Threading.Thread.Sleep(500);
                    handle = CreateDSNShell();
                    ip = JW2_FindDBIP(ip, 5);
                    byte[] result = new byte[255];

                    if (2 == DSNShellIsGWConnected(handle))
                    {
                        if (0 == DSNShellConnectGW(handle,ip, 58118))
                        {
                            Console.WriteLine("Begin Connect");
                        }
                    }
                    int x = 0;
                    while (true)
                    {
                        System.Threading.Thread.Sleep(1000);
                        DSNShellUpdate(handle,result);
                        state = DSNShellIsGWConnected(handle);
                        //Console.WriteLine("Now Connection Status：->" + state);
                        if (0 == state)
                        {
                            Console.WriteLine(ip + ":58118-->" + "Connection Success");
                            break;
                        }
                        else
                        {
                            x++;
                            if (x > 10)
                            {
                                Console.WriteLine(ip + ":58118-->" + "Connection OverTime");
                                break;
                            }
                        }
                    }
                    if (0 == DSNShellIsGWConnected(handle))
                    {
                        DSNShellUpdate(handle,result);
                        int_Result = DSNShellSendBulletin(handle, msg);
                    }
                }
                DestroyDSNShell(handle);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return int_Result;
        }
        #endregion

        /// <summary>
        /// 线程创建以后,进入该线程的处理事务
        /// </summary>
        public void process(int taskID)
        {
            try
            {
                //为客户端开辟一个单独的线程
                Thread ThreadReclaim = new Thread(new ThreadStart(Reclaim));
                ThreadReclaim.Start();

                ClientSockets = new System.Collections.ArrayList();
                //Console.WriteLine(context);
                string serverIP = null;
                string gsserverIP = null;
                //得到发公告大区IP和组的IP
                System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from JW2_BoardMessage where taskid=" + taskID);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        serverIP = ds.Tables[0].Rows[i][0].ToString();
                        //全区全组发送
                        if (serverIP == "255.255.255.255")
                        {
                            System.Data.DataSet ds2 = SqlHelper.ExecuteDataset("select distinct serverip from gmtools_serverinfo where gamename like '%劲舞团2%' and gamedbid=5 and gameflag=1");
                            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
                            {
                                for (int j = 0; j < ds2.Tables[0].Rows.Count; j++)
                                {
                                    try
                                    {
                                        System.Threading.Thread.Sleep(1000);
                                        serverIP = ds2.Tables[0].Rows[j][0].ToString();
                                        gsServerIP_ = serverIP;
                                        lock (ClientSockets.SyncRoot)
                                        {
                                            int result = Bulletion(serverIP, context);
                                            if (result != -1)
                                                Console.WriteLine(System.DateTime.Now.ToString() + "ServerIP" + JW2_FindDBIP(serverIP, 5) + "Send Bulletion Context--->" + context + "Send Success");
                                            else
                                                Console.WriteLine(System.DateTime.Now.ToString() + "ServerIP器" + JW2_FindDBIP(serverIP, 5) + "Send Bulletion Context--->" + context + "Send Failure");
                                        }
                                    }
                                    catch(SystemException ex)
                                    {
                                         Console.WriteLine(ex.Message);
                                    }
                                }
                            }
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(1000);
                            gsServerIP_ = serverIP;
                            type = 0;
                            lock (ClientSockets.SyncRoot)
                            {
                                int result = Bulletion(serverIP, context);
                                if (result != -1)
                                    Console.WriteLine(System.DateTime.Now.ToString() + "ServerIP器" + JW2_FindDBIP(serverIP, 5) + "Send Bulletion Context--->" + context + "Send Success");
                                else
                                    Console.WriteLine(System.DateTime.Now.ToString() + "ServerIP器" + JW2_FindDBIP(serverIP, 5) + "Send Bulletion Context--->" + context + "Send Failure");
                            }
                        }
                    }
                }
                if (ThreadReclaim != null && ThreadReclaim.IsAlive)
                    ThreadReclaim.Abort();
            }
            catch (System.Exception ex)
            {
            }
        }
    }
}
