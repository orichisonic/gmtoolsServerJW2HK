using System;
using System.Threading;
using System.Collections;
namespace GMSERVER.ServerSocket
{
	/// <summary>
	/// HandlerManager 的摘要说明。
	/// </summary>
	public class HandlerManager
	{
		Hashtable handlers_ = new Hashtable();
		private Mutex mut = new Mutex();
		public HandlerManager()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public byte registerHandler(Handler handler)
		{
			if(handler==null)
				return 0;
			mut.WaitOne();
			int i=0;
			byte[] v_handler = new byte[255];

			foreach (DictionaryEntry d in handlers_)
			{
				v_handler[ i++ ] =(byte)d.Key;
			}

			for ( int j = 0; j < i; j++ ) 
			{
				for ( int k = j + 1; k < i; k++ ) 
				{
					if ( v_handler[ j ] > v_handler[ k ] ) 
					{
						byte swap = v_handler[ k ];
						v_handler[ k ] = v_handler[ j ];
						v_handler[ j ] = swap;
					}
				}
			}
			byte handler_id =1;
			if ( ( i == 0 ) || ( ( i != 0 ) && ( v_handler[ 0 ] != 1 ) ) ) 
			{
				handler_id = 1;
			} 
			else 
			{
				for ( int j = 0; j < i; j++ ) 
				{
					if ( j == i - 1 ) 
					{
						handler_id = Convert.ToByte(v_handler[ i - 1 ] + 1);
						break;
					}
					if ( v_handler[ j + 1 ] != v_handler[ j ] + 1 ) 
					{
						handler_id = Convert.ToByte(v_handler[ j ] + 1);
						break; 
					}
				}
			}
			mut.ReleaseMutex();
			handlers_.Add( handler_id, handler );
			return handler_id;


		}
		public Handler unregisterHandler(byte handler_id ) 
		{

			foreach (DictionaryEntry d in handlers_)
			{
				if( handler_id!=(byte)d.Key)
				{
					Handler unregistered = (Handler)d.Value;
					handlers_.Remove(handler_id);
					return unregistered;
				}					
			}
			return null;
		}
		public Handler getHandler( byte handler_id ) 
		{
			foreach (DictionaryEntry d in handlers_)
			{
				if( handler_id==(byte)d.Key)
				{
					return (Handler)d.Value;
				}
				else
				{
					continue;
				}					
			}
			return null;
		}
		public bool isFull() 
		{
			return  handlers_.Count > 0x10 ;
		}


	}
}
