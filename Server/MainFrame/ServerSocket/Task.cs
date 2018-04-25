using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using lg = Common.API.LanguageAPI;
namespace GMSERVER.ServerSocket
{
	
	/// <summary>
	/// task 的摘要说明。
	/// </summary>
	public class Task
	{
		ConnectionPool ConnectionPool = null;
		TaskManager ClientTask  = null;
		TcpListener listener = null;
		public Task(int i)
		{
			
		}
		public Task()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		/// <summary>
		/// 初始化线程的启动
		/// </summary>
		public int initialize()
		{
			IPAddress ipAddress = Dns.Resolve(Dns.GetHostName()).AddressList[0];
			IPEndPoint ipLocalEndPoint = new IPEndPoint(ipAddress, 10116);
        
			//线程池
			ConnectionPool = new ConnectionPool()  ;    
            //从线程池创建一个线程
			ClientTask = new TaskManager(ConnectionPool) ;
			//启一个线程
			ClientTask.Start() ;
    
			listener = new TcpListener(ipLocalEndPoint);
			try 
			{
				//为每个连接过来客户端启一个连接通道
				listener.Start();
				Console.WriteLine("Waiting for a sender client connection...");
			}
			catch (Exception e) 
			{
				Console.WriteLine(e.ToString());
			}
        
			Console.WriteLine("\n" + lg.ServerSocket_Task_Continue);
			Console.Read();
			return 1;
		}
		/// <summary>
		/// 线程创建以后,进入该线程的处理事务
		/// </summary>
		public void process()
		{
			int TestingCycle = 5 ; // Number of testing cycles
			int ClientNbr = 0 ;
			try
			{
				while ( TestingCycle > 0 ) 
				{
                    //接受客户端连接请求
					TcpClient handler = listener.AcceptTcpClient();
                    //得到客户端连接句柄
					if (  handler != null)  
					{
						Console.WriteLine(lg.ServerSocket_Task_Query+"#{0}!", ++ClientNbr) ;
                                
						// 一个新连接请求的处理
						ConnectionPool.Enqueue( new Handler(handler) ) ;
                                
						--TestingCycle ;
					}
					else 
						break;                
				}
			}
			catch(SocketException ex)
			{
				Console.WriteLine(ex.Message);
			}
			listener.Stop();
              
			// Stop client requests handling
			ClientTask.Stop() ;
		}
	}
}
