using System;
using System.Data;
using System.Data.SqlClient;
using Common.DataInfo;
using Common.Logic;

namespace SDO.SDODataInfo
{
	public enum protocol:short
	{
		RET_OK = 0,
		RET_FAIL = 1,
		MONEY_GCASH = 0,
		MONEY_MCASH = 1,
		GW_BASE = 1000,
		GW_LOGIN_REQ = 1000+0,
		GW_LOGIN_ACK = 1000+1,
		GW_CHANNELLIST_REQ = 1000+2,
		GW_CHANNELLIST_ACK = 1000+3,
		EH_BASE = 10000,
		EH_ALIVE_REQ = 10000+0,
		EH_ALIVE_ACK = 10000+1,
		GM_BASE = 20000,
		GM_CHANNELLIST_REQ = 20000 + 0,
		GM_CHANNELLIST_ACK = 20000 + 1,
		GM_NOTICE_REQ = 20000 + 2,
		GM_NOTICE_ACK = 20000 + 3
	}
	/// <summary>
	/// NoticeDataInfo 的摘要说明。
	/// </summary>

	public class NoticeDataInfo
	{
		//发公告协议

		public NoticeDataInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		/**
		 * 发送公告任务查询
		 * */
		public static DataSet BoardTask_Query()
		{
			DataSet result = null;
			try
			{
				result = SqlHelper.ExecuteDataset("SDO_BoardTask_Query");
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		/**
		 * 发送公告任务单个查询
		 * */
		public static DataSet BoardTask_OwnerQuery(int taskID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@SDO_TaskID",SqlDbType.Int)
											   };
				paramCode[0].Value = taskID;

				result = SqlHelper.ExecSPDataSet("SDO_BoardTask_OwnerQuery",paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		/**
		 * 发送公告任务更新
		 * */
		public static int BoardTask_Insert(int userByID,string serverIP,string boardMessage,DateTime begintime,DateTime endTime,int interval)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[7]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@SDO_ServerIP",SqlDbType.VarChar,300),
												   new SqlParameter("@SDO_BoardMessage",SqlDbType.VarChar,500),
												   new SqlParameter("@SDO_begintime",SqlDbType.DateTime),
												   new SqlParameter("@SDO_endTime",SqlDbType.DateTime),
												   new SqlParameter("@SDO_interval",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = boardMessage;
				paramCode[3].Value = begintime;
				paramCode[4].Value = endTime;
				paramCode[5].Value = interval;
				paramCode[6].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TaskList_Insert", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		/**
		 * 发送公告任务更新
		 * */
		public static int BoardTask_Update(string serverIP,int userByID,int taskID,DateTime beginTime,DateTime endTime,int interval,int status,string boardMessage)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[9]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,300),
												   new SqlParameter("@SDO_TaskID",SqlDbType.Int),
												   new SqlParameter("@SDO_BoardMessage",SqlDbType.VarChar,500),
												   new SqlParameter("@SDO_begintime",SqlDbType.DateTime),
												   new SqlParameter("@SDO_endTime",SqlDbType.DateTime),
												   new SqlParameter("@SDO_interval",SqlDbType.Int),
												   new SqlParameter("@SDO_status",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = taskID;
				paramCode[3].Value = boardMessage;
				paramCode[4].Value = beginTime;
				paramCode[5].Value = endTime;
				paramCode[6].Value = interval;
				paramCode[7].Value = status;
				paramCode[8].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TaskList_Update", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}

		/**
		* 发送公告任务删除
		* */
		public static int BoardTask_delete(int userByID,int taskID)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@SDO_taskID",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = taskID;
				paramCode[2].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TaskList_Del", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		public static int BoardMessage_Req(int userByID,string serverIP, string channelList,string boardMessage)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_ChannelList",SqlDbType.VarChar,500),
												   new SqlParameter("@SDO_BoardMessage",SqlDbType.VarChar,500),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = channelList;
				paramCode[3].Value = boardMessage;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_BoardMessage_Insert", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}

		public static int InsertChannel_Req(int wPlanetID,int wChannelID,int iLimitUser,int iCurrentUser,string ipaddr)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@wPlanetID",SqlDbType.TinyInt),
												   new SqlParameter("@wChannelID",SqlDbType.TinyInt),
												   new SqlParameter("@iLimitUser",SqlDbType.TinyInt),
												   new SqlParameter("@iCurrentUser",SqlDbType.TinyInt),
												   new SqlParameter("@ipaddr",SqlDbType.VarChar,30),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = wPlanetID;
				paramCode[1].Value = wChannelID;
				paramCode[2].Value = iLimitUser;
				paramCode[3].Value = iCurrentUser;
				paramCode[4].Value = ipaddr;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_ChannelList_Insert", paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		
		}
		public static int TruncateTable_Req()
		{
			int result = -1;
			try
			{
				result = SqlHelper.ExecCommand("truncate table ChannelList");
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		public static DataSet ChannelList_Req()
		{
			DataSet result = null;
			try
			{
				result = SqlHelper.ExecuteDataset("select * from ChannelList");
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
	}
	public class CMsg
	{
		public const int NETWORK_BUF_SIZE		=	8192;
		public const int NETWORK_MSG_SIZE		=	4096;
		public const int NETWORK_MSG_HEADER	=   4;
		int m_readSize = 0;
		int m_pSize = 0;
		short m_pID;
		byte[] m_pRead = new byte[NETWORK_BUF_SIZE];
		byte[] m_pWrite = new byte[NETWORK_BUF_SIZE];
		byte[] m_pBuf = new byte[NETWORK_BUF_SIZE];
		public CMsg()
		{

		}
		public CMsg(short wID)
		{
			m_pID = wID;
			m_pSize = NETWORK_MSG_HEADER;

			byte[] ret = new byte[]{
									   (byte)wID,(byte)(wID>>8)
								   };
			System.Array.Copy(ret,0,m_pBuf,2,ret.Length);
			//m_pBuf[2] = (byte)wID;
			System.Array.Copy(m_pBuf,4,m_pWrite,0,NETWORK_BUF_SIZE-NETWORK_MSG_HEADER);
			System.Array.Copy(m_pBuf,4,m_pRead,0,NETWORK_BUF_SIZE-NETWORK_MSG_HEADER);
			//m_pRead = m_pWrite = m_pBuf[NETWORK_MSG_HEADER];
		}
		public void writeLength(short msglength)
		{
			byte[] ret = new byte[]{
									   (byte)msglength,(byte)(msglength>>8)
								   };
			System.Array.Copy(ret,0,m_pBuf,0,2);

		}
		public short GetSize()  
		{
			return (short)m_pSize;
		}
		public protocol	
			ID() {return (protocol)m_pID;}
		public void Clear()
		{
			m_pBuf = new byte[NETWORK_BUF_SIZE];
			m_pWrite = new byte[NETWORK_BUF_SIZE];
			m_pRead = new byte[NETWORK_BUF_SIZE];
			m_pID = 0;
			m_readSize = 0;
		}

		public void Replicate()
		{
			m_pBuf.Initialize();
			int ret = 0;
			for ( int i =0 ;i < ( int ) 2;i++ )
				ret += (int)( m_pBuf[ i ] << ( i * 8 ));
			m_pSize = ret;
			uint uret = 0;
			for ( int i =0 ;i < ( int ) 2;i++ )
				uret += (uint)( m_pBuf[ i+2 ] << ( i * 8 ));
			m_pID	= (short)uret;
			System.Array.Copy(m_pBuf,4,m_pWrite,0,NETWORK_BUF_SIZE-NETWORK_MSG_HEADER);
			System.Array.Copy(m_pBuf,4,m_pRead,0,NETWORK_BUF_SIZE-NETWORK_MSG_HEADER);
		}
		public void WriteData(byte[] pData,int n)
		{
			if( m_pWrite.Length + n >= m_pBuf.Length + NETWORK_BUF_SIZE )
				Console.WriteLine("CMsg::ReadData > "+NETWORK_BUF_SIZE);
	
			System.Array.Copy(pData,0,m_pWrite,m_pSize,n);

			System.Array.Copy(pData,0,m_pBuf,m_pSize,n);
			m_pSize +=1;
			m_pSize += n;
		}
		public object ReadData(object pData, int n)
		{
			byte[] m_uiValue = new byte[n];
			if( m_pRead.Length + n > m_pBuf.Length + NETWORK_BUF_SIZE)
				Console.WriteLine("CMsg::ReadData >" + NETWORK_BUF_SIZE);
			if( m_pRead.Length + n > m_pBuf.Length + GetSize())
				Console.WriteLine("CMsg::ReadData > "+ GetSize());

			System.Array.Copy(m_pRead,m_readSize,m_uiValue,0,n);
			if(pData.GetType()== typeof(System.Byte))
			{
				int ret = 0;
				for ( int i =0;i < n;i++ )
					ret += (int)( m_uiValue[ i ] << ( i * 8 ) );
				pData = ret;
				//pData= BitConverter.ToChar(m_uiValue,0);
			}
			else if(pData.GetType() == typeof(System.Int16))
			{
				pData= BitConverter.ToInt16(m_uiValue,0);
			}
			else if (pData.GetType() == typeof(System.Int32))
			{
				pData = BitConverter.ToInt32(m_uiValue,0);
			}
			else if(pData.GetType() == typeof(System.String))
			{
				int index = 0;
				for(int i=0;i<m_uiValue.Length;i++)
				{
					if(m_uiValue[i]=='\0')
					{
						break;
					}
					else
						index++;
				}
				pData = System.Text.Encoding.Default.GetString(m_uiValue,0,index);
				n = index;
				m_readSize++;
			}
			m_readSize += n;
			return pData;
		}
		public byte[] GetBuf()  {return m_pBuf;}
		public static short toInteger(byte[] m_uiValue,int m_uiValueLen) 
		{
			if ( m_uiValueLen > 4 )
				return 0xfff;
			short ret = 0;
			for ( int i = 0;i < ( int ) m_uiValueLen;i++ )
				ret += (short)( m_uiValue[ i ] << ( i * 8 ) );
			return ret;
		}

	}
	
}
