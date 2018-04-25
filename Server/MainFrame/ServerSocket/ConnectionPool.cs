using System;

namespace GMSERVER.ServerSocket
{
	/// <summary>
	/// ConnectionPool 的摘要说明。
	/// </summary>
	public class ConnectionPool
	{
		// 创建一个同步的Queue队列
		private  System.Collections.Queue SyncdQ = System.Collections.Queue.Synchronized( new Queue() );
        /// <summary>
        /// 将客户端连接句柄添加到Queue的结尾。
        /// </summary>
        /// <param name="client">客户端连接句柄</param>
		public  void Enqueue(Handler client) 
		{
			SyncdQ.Enqueue(client) ;
		}
        /// <summary>
        /// 将客户端连接句柄移除并返回位于 Queue 开始处的。
        /// </summary>
        /// <returns>连接句柄</returns>
		public Handler Dequeue() 
		{
			return (Handler) ( SyncdQ.Dequeue() ) ;
		}
        
		public  int Count 
		{
			get { return SyncdQ.Count ; }
		}

		public object SyncRoot 
		{
			get { return SyncdQ.SyncRoot ; }
		}
	}
        

}
