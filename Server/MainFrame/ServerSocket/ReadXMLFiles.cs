using System;
using System.Xml;
using Common.DataInfo;
using System.Runtime.Serialization;
using System.IO;
namespace GMSERVER.ServerSocket
{

	/// <summary>
	/// ReadServerInfo 的摘要说明。
	/// </summary>
	public class ReadXMLFile
	{
		private string userName;
		private string passWord;
		private string dbName;
		private string databaseIP;
		private string serverName;
		private string serverIP;
		private int serverPort;
		private int serverCnt;
		/// <summary>
		/// 构造类
		/// </summary>
		public ReadXMLFile()
		{
			init();
		}
		/// <summary>
		/// 初始化数据
		/// </summary>
		public void init()
		{
			XmlDocument doc = new XmlDocument();
			
			try
			{
				string path= AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
				doc.Load(path+Constant.SERVERIP_XML);
				XmlNodeList userInfo = doc.GetElementsByTagName(Constant.ELEMENT_USER_INFO);
				XmlNodeList serverInfo = doc.GetElementsByTagName(Constant.ELEMENT_SERVER_INFO);
				setUserinfo(userInfo);
				setServerinfo(serverInfo);

				//	Console.Write(userName+passWord+userText);
				//	Console.Write("end");

			}
			catch(Exception ex)
			{
				Console.Write(ex.Message);
			}
		}

		/// <summary>
		/// 大区数据库用户名
		/// </summary>
		public string UserName
		{
			get
			{
				return userName;
			}
		}
		/// <summary>
		/// 大区数据库密码
		/// </summary>
		public string PassWord
		{
			get
			{
				return passWord;
			}
		}
		public string DatabaseIP
		{
			get
			{
				return databaseIP;
			}

		}
		public string DBName
		{
			get
			{
				return dbName;
			}
		}
		public string ServerName
		{
			get
			{
				return ServerName;
			}
		}
		public string ServerIP
		{
			get
			{
				return serverIP;
			}

		}
		public int ServerPort
		{
			get
			{
				return serverPort;
			}
			set
			{
				this.serverPort = value;
			}
		}
		public int ServerCnt
		{
			get
			{
				return serverCnt;
			}
		}
		#region 序列化和反序列化

		/// <summary>
		/// 从二进制数组反序列化得到对象
		/// </summary>
		/// <param name="buf">字节数组</param>
		/// <returns>得到的对象</returns>
		public static object DeserializeBinary(byte[] buf) 
		{
			try
			{
				System.IO.MemoryStream memStream = new MemoryStream(buf);
				memStream.Position=0;
				System.Runtime.Serialization.Formatters.Binary.BinaryFormatter deserializer = 
					new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				object newobj = deserializer.Deserialize(memStream);
				memStream.Close();
				return newobj;
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// 序列化为二进制字节数组
		/// </summary>
		/// <param name="request">要序列化的对象</param>
		/// <returns>字节数组</returns>
		public static byte[] SerializeBinary(object request) 
		{
			try
			{
				System.Runtime.Serialization.Formatters.Binary.BinaryFormatter serializer = 
					new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				System.IO.MemoryStream memStream = new System.IO.MemoryStream();
				serializer.Serialize(memStream, request);
				return memStream.GetBuffer();
			}
			catch
			{
				return null;
			}
		}

		#endregion


		/// <summary>
		/// 设置数据库连接用户的相关信息
		/// </summary>
		/// <param name="_nodelist">数据库用户的信息：user-info</param>
		public void setUserinfo(XmlNodeList _nodelist)
		{
			XmlNodeList userNode = _nodelist;
			for(int i = 0; i < userNode.Count; i++)
			{
				XmlNodeList tmp = userNode.Item(i).ChildNodes;
				if(tmp.Count > 0)
				{
					for(int j = 0; j < tmp.Count; j++)
					{
						switch(tmp.Item(j).Name)
						{
							case Constant.ELEMENT_USER_INFO_NAME:
							{
								userName = tmp.Item(j).ChildNodes.Item(0).Value;
								break;
							}
							case Constant.ELEMENT_USER_INFO_PASSWORD:
							{
								passWord = tmp.Item(j).ChildNodes.Item(0).Value;
								break;
							}
							case Constant.ELEMENT_USER_INFO_DATA_NAME:
							{
								dbName = tmp.Item(j).ChildNodes.Item(0).Value;
								break;
							}	
							case Constant.ELEMENT_USER_INFO_DATABASE_IP:
							{
								databaseIP = tmp.Item(j).ChildNodes.Item(0).Value;
								break;
							}
						}						
					}
				}
			}
		}
		


		/// <summary>
		/// 设置服务器端配置
		/// </summary>
		/// <param name="_nodelist">The server list</param>
		private void setServerinfo(XmlNodeList _nodelist)
		{
			XmlNodeList serverNode = _nodelist;
			for(int i = 0; i < serverNode.Count; i++)
			{
				XmlNodeList tmp = serverNode.Item(i).ChildNodes;
				if(tmp.Count > 0)
				{
					for(int j = 0; j < tmp.Count; j++)
					{
						switch(tmp.Item(j).Name)
						{
							case Constant.ELEMENT_SERVER_INFO_NAME:
							{
								serverName = tmp.Item(j).ChildNodes.Item(0).Value;
								break;
							}
							case Constant.ELEMENT_SERVER_INFO_IP:
							{
								serverIP = tmp.Item(j).ChildNodes.Item(0).Value;
								break;
							}
							case Constant.ELEMENT_SERVER_INFO_PORT:
							{
								serverPort =  Convert.ToInt32(tmp.Item(j).ChildNodes.Item(0).Value);
								break;
							}
							case Constant.ELEMENT_SERVER_CNT:
							{
								serverCnt = Convert.ToInt32(tmp.Item(j).ChildNodes.Item(0).Value);
								break;
							}	
						}						
					}
				}
			}

		}
	}
}
