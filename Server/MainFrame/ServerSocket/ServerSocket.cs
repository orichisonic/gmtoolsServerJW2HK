  using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using Common.DataInfo;
using Common.Logic;
using Common.API;
using lg = Common.API.LanguageAPI;
namespace GMSERVER.ServerSocket
{
	/// <summary>
	/// ServerSocket 的摘要说明。
	/// </summary>
	public class ServerSocket
	{
		Queue[] queues_ = new Queue[2];
		public ServerSocket()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		~ServerSocket()
		{
			//设置所有在线用户为离线状态
			UserInfoAPI userAPI = new UserInfoAPI();
			userAPI.GM_UpdateActiveUser(0);
		}
		private void initialize()
		{
			Console.WriteLine( "QueueMgr initializing...\n" );
			int[] num = new int[2];
			num[0] = 10;
			num[1] = 10;
			for(int k=0;k<2;k++)
			{
				for(int i=0;i<num[k];i++)
				{
					Queue SyncdQ = new Queue();
					if(SyncdQ.initized()==0)
						this.queues_[0].Enqueue(SyncdQ);
					else
						SyncdQ.Clear();
				}
			}
		}
		//监听客户端的连接，并创建一个发消息实体
		public  static  void StartListening1() 
		{
			Console.Write("****************************************************\n");
			Console.Write("**                "+lg.ServerSocket_ServerSocket_GMTools_Title+"                      **\n");
			Console.Write("****************************************************\n");
			//Thread thread = new Thread(new ThreadStart(receiverThread));
			//thread.Start();
			//初始化XML文件
			ReadXMLFile xmlFiles = new ReadXMLFile();
			//IPAddress ipAddress = Dns.Resolve(xmlFiles.ServerIP).AddressList[0];
			IPAddress ipAddress = IPAddress.Parse(xmlFiles.ServerIP);
			//初始化数据库连接参数值
			SqlHelper helper = new SqlHelper();
			helper.init(xmlFiles.DatabaseIP,xmlFiles.DBName,xmlFiles.UserName,xmlFiles.PassWord);
			IPEndPoint ipLocalEndPoint = new IPEndPoint(ipAddress, xmlFiles.ServerPort);
			TaskManager ClientTask  ;
        
			// Client Connections Pool
			ConnectionPool ConnectionPool = new ConnectionPool()  ;          
        
			// Client Task to handle client requests
			ClientTask = new TaskManager(ConnectionPool) ;
        
			ClientTask.Start();

			TcpListener listener = new TcpListener(ipLocalEndPoint);
			try 
			{     
				listener.Start();
				int clientCycle = xmlFiles.ServerCnt;
				int ClientNbr = 0 ;
        
				// Start listening for connections.
				Console.WriteLine(lg.ServerSocket_ServerSocket_GMTools_Port + xmlFiles.ServerPort);
				HandlerManager handlers = new HandlerManager();
				while ( clientCycle > 0 ) 
				{
                      
					TcpClient handler = listener.AcceptTcpClient(); 
					if (handler!=null)  
					{
						Console.WriteLine(lg.ServerSocket_ServerSocket_GMTools_Accept+lg.ServerSocket_ServerSocket_GMTools_Client+"#{0}!", ++ClientNbr) ;
						Console.WriteLine(lg.ServerSocket_ServerSocket_GMTools_Validate);
						handlers.registerHandler(new Handler(handler));
						// An incoming connection needs to be processed.
						ConnectionPool.Enqueue( new Handler(handler) ) ;
						//--clientCycle ;
					}
					else 
					{
						Console.WriteLine(lg.ServerSocket_ServerSocket_GMTools_Client+"#{0}"+lg.ServerSocket_Handler_UserLeft,ClientNbr);
						break;  
					}
				}
				listener.Stop();
				ClientTask.Stop() ;
				UserInfoAPI userAPI = new UserInfoAPI();
				userAPI.GM_UpdateActiveUser(1,0);
			} 
			catch (IOException e) 
			{
				Console.WriteLine(e.ToString());
			}
			catch(SocketException ex)
			{
				Console.WriteLine(ex.Message);
			}
        
			Console.WriteLine("\nHit enter to continue...");
			Console.Read();
		}
		public  static  int Main(String[] args) 
		{
			ServerSocket.StartListening1();
			//FileStream fs = new FileStream(FILE_NAME, FileMode.CreateNew);
			//BinaryReader r = new BinaryReader(fs);
			//fs.Read
			return 0;
		}

	}
}
