using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.IO;

namespace GMSERVER.ServerSocket
{
	/// <summary>
	/// Constant 的摘要说明。
	/// </summary>
	public class Constant
	{	

		public Constant()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//			
		}
		
		public const string	XML_FILE_NAME = "ipaddrss.xml";
		public const string STRLOGINSTANCE = "LOG";
		public const string STRPROINSTANCE = "properties";
		public const string DEFAULTLOGFILE = @"e:\log.txt";
		public const string LOG_FILE = @"e:\log.txt";
		public const string SERVERIP_XML = @"ServerIP.xml";

		public const string ELEMENT_USER_INFO = "database";
		public const string ELEMENT_USER_INFO_NAME = "username";
		public const string ELEMENT_USER_INFO_PASSWORD = "password";
		public const string ELEMENT_USER_INFO_DATA_NAME = "dataname";
		public const string ELEMENT_USER_INFO_DATABASE_IP = "DataBaseIP";

		public const string ELEMENT_SERVER_INFO = "server";
		public const string ELEMENT_SERVER_INFO_NAME = "server-name";
		public const string ELEMENT_SERVER_INFO_IP = "server-ip";
		public const string ELEMENT_SERVER_INFO_PORT = "server-port";
		public const string ELEMENT_SERVER_CNT = "server-cnt";

	}

}
