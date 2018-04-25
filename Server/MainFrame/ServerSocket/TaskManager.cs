using System;
using System.Threading;
using System.Collections;
namespace GMSERVER.ServerSocket
{
	public class PointlessTask
	{
		private int m_TaskId;

		public PointlessTask(int TaskId)
		{
			m_TaskId=TaskId;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="NotUsed">Only here to support the WaitCallback Interface</param>
		public void DoWork(object NotUsed)
		{
			//Thread.Sleep(1000);
			Console.WriteLine(m_TaskId);
			for(int i=0;i<10;i++)
			{
				Console.WriteLine(i);
				Thread.Sleep(1000);
			}
		}
	}
	/// <summary>
	/// TaskManager 的摘要说明。
	/// </summary>
	public class TaskManager
	{
		private ConnectionPool Pool  ;
		private bool ContinueProcess = false ;
		private Hashtable taskset_map_ = new Hashtable();
		private Thread [] ThreadTask  ;
		private Mutex mut = new Mutex();

		public TaskManager()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}     
		public TaskManager(ConnectionPool pool) 
		{
			this.Pool = pool ;
		}  
        /// <summary>
        /// 为每一个连接过来的客户端创建一个线程
        /// </summary>
		public void Start() 
		{
			ReadXMLFile xmlFiles = new ReadXMLFile();
			ThreadTask = new Thread[xmlFiles.ServerCnt] ;
			ContinueProcess = true ;
			// Start threads to handle Client Task
			for ( int i = 0 ; i < ThreadTask.Length ; i++) 
			{
				ThreadTask[i] = new Thread( new ThreadStart(this.Process) );
				ThreadTask[i].Start() ;
			}
		}

        /// <summary>
        /// 单个线程处理事务
        /// </summary>
		private  void Process()  
		{
			while ( ContinueProcess ) 
			{	

				Handler clientHandler  = null ;
				lock( Pool.SyncRoot ) 
				{
					if  ( Pool.Count > 0 )
						clientHandler = Pool.Dequeue() ;
				}		                         
				if ( clientHandler != null ) 
				{
					clientHandler.Process() ; // Provoke client
					// if client still connect, schedufor later processingle it 
					if ( clientHandler.Alive ) 
						Pool.Enqueue(clientHandler) ;
				}
                        
				Thread.Sleep(100) ;
			}         
		}
		public void initialize(Task task)
		{
			mut.WaitOne();
			int i=0;
			byte[] v_task = new byte[255];

			foreach (DictionaryEntry d in taskset_map_)
			{
				v_task[ i++ ] =(byte)d.Key;
			}

			for ( int j = 0; j < i; j++ ) 
			{
				for ( int k = j + 1; k < i; k++ ) 
				{
					if ( v_task[ j ] > v_task[ k ] ) 
					{
						byte swap = v_task[ k ];
						v_task[ k ] = v_task[ j ];
						v_task[ j ] = swap;
					}
				}
			}
			byte task_id =1;
			if ( ( i == 0 ) || ( ( i != 0 ) && ( v_task[ 0 ] != 1 ) ) ) 
			{
				task_id = 1;
			} 
			else 
			{
				for ( int j = 0; j < i; j++ ) 
				{
					if ( j == i - 1 ) 
					{
						task_id = Convert.ToByte(v_task[ i - 1 ] + 1);
						break;
					}
					if ( v_task[ j + 1 ] != v_task[ j ] + 1 ) 
					{
						task_id = Convert.ToByte(v_task[ j ] + 1);
						break; 
					}
				}
			}
			mut.ReleaseMutex();
			taskset_map_.Add( task_id, task );

		}

		public  int add(string tskset_name, Task tskset )
		{
			if ( tskset == null )
				return -1;
			if ( this.find( tskset_name ) != null )
				return -1;
			taskset_map_.Add(tskset_name, tskset);
			return 0;

		}
		Task remove(string tskset_name ) 
		{
			Task removed = this.find( tskset_name );
			if ( removed == null )
				return null;
			taskset_map_.Remove(tskset_name );
			return removed;
		}

		private Task find( string tskset_name ) 
		{
			
			foreach (DictionaryEntry d in taskset_map_)
			{
				if( tskset_name.Equals(d.Key.ToString()))
				{
					return (Task)d.Value;
				}					
			}
			return null;
		}

		/// <summary>
		/// 终止一个线程
		/// </summary>
		public  void Stop() 
		{
			ContinueProcess = false ;        
			for ( int i = 0 ; i < ThreadTask.Length ; i++) 
			{
				if ( ThreadTask[i] != null &&  ThreadTask[i].IsAlive )  
					ThreadTask[i].Join() ;
			}
                        
			// Close all client connections
			while ( Pool.Count > 0 ) 
			{
				Handler client = Pool.Dequeue() ;
				client.Close() ; 
				Console.WriteLine("Client connection is closed!") ;
			}
		}
        
	}
}
