using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Common.Logic;
using Common.DataInfo;
using SDO.SDODataInfo;
using lg = Common.API.LanguageAPI;
namespace SDO.SDOAPI
{
	/// <summary>
	/// SDONoticeInfoAPI 的摘要说明。
	/// </summary>
	public class SDONoticeInfoAPI
	{
		Message msg;
		CMsg msgStruct;
		Socket client = null;
		int listCnt = 1;
		public SDONoticeInfoAPI()
		{

		}

		public SDONoticeInfoAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
		}
		//发送公告消息
		public Message SendBoardInfo()
		{
			string serverIP = null;
			string channelList = null;
			string boardMessage = null;
			int userbyID = 0;
			int result = -1;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				//频道列表
				channelList = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ChannelList).m_bValueBuffer);
				//公告信息
				boardMessage = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_BoardMessage).m_bValueBuffer);
				//操作员ID
				TLV_Structure tlvStrut = new TLV_Structure(TagName.UserByID,3,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userbyID =(int)tlvStrut.toInteger();
				connect_Req(serverIP);
				//发公告消息
				msgStruct = new CMsg((short)protocol.GM_NOTICE_REQ);
				msgStruct.WriteData(System.Text.Encoding.Default.GetBytes(boardMessage),boardMessage.Length);
				string[] chanList = channelList.Split(';'); 
				int nChan = chanList.Length;
				byte[] ret = new byte[4];
				ret = BitConverter.GetBytes(nChan);
				for(int i=0;i<nChan;i++)
				{
					string[] wPlanetList = chanList[i].Split('/'); 
					short wPlanetID = Convert.ToInt16(wPlanetList[0]);
					short wChannelID = Convert.ToInt16(wPlanetList[1]);
					ret = new byte[]{
										(byte)wPlanetID,(byte)(wPlanetID>>8)
									};
					msgStruct.WriteData(ret,2);
					ret = new byte[]{
										(byte)wChannelID,(byte)(wChannelID>>8)
									};
					msgStruct.WriteData(ret,2);
				}
				msgStruct.writeLength(msgStruct.GetSize());
				client.Send(msgStruct.GetBuf(),0,msgStruct.GetSize(),0);
				client.Close();
				result = NoticeDataInfo.BoardMessage_Req(userbyID,serverIP,channelList,boardMessage);
				if(result == 1)
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDONoticeInfoAPI_SendNoticeInfo + lg.API_Success + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDONoticeInfoAPI_SendNoticeInfo + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_BOARDMESSAGE_REQ_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDONoticeInfoAPI_SendNoticeInfo + lg.API_Failure + "!");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDONoticeInfoAPI_SendNoticeInfo + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_BOARDMESSAGE_REQ_RESP);
				}

			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				SqlHelper.log.WriteLog(lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDONoticeInfoAPI_SendNoticeInfo + lg.API_Failure + "!");
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDONoticeInfoAPI_SendNoticeInfo + lg.API_Failure + "!");
				return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_BOARDMESSAGE_REQ_RESP);

			}

		}
		//登陆请求
		void connect_Req(string serverIP)
		{
			string userName = "sdohuodong07";
			string password = "9YouGongGao";
			short version = 1;
			short version1 = 6;
			int port = 15010;
			try
			{
				IPAddress ipAdress = IPAddress.Parse(serverIP);
				client = new Socket(AddressFamily.InterNetwork,
					SocketType.Stream,
					ProtocolType.Tcp);
 
				//Console.WriteLine("Establishing Connection to {0}", 
				//serverIP);
				IPEndPoint ipReomtePoint = new IPEndPoint(ipAdress,port);

				client.Connect(ipReomtePoint);
				if(client.Connected==true)
				{
					//Console.WriteLine("Connection established");
					msgStruct = new CMsg((short)protocol.GW_LOGIN_REQ);
					msgStruct.WriteData(System.Text.Encoding.Default.GetBytes(userName),userName.Length);
					msgStruct.WriteData(System.Text.Encoding.Default.GetBytes(password),password.Length);
					byte[] ret = new byte[]{
											   (byte)version,(byte)(version>>8)
										   };
					msgStruct.WriteData(ret,1);
					ret = new byte[]{
										(byte)version1,(byte)(version1>>8)
									};
					msgStruct.WriteData(ret,1);
					msgStruct.writeLength(msgStruct.GetSize());
					client.Send(msgStruct.GetBuf(),0,msgStruct.GetSize(),0);
				}
			}
			catch(SocketException ex)
			{
				Console.WriteLine(ex.Message);
			}

		}
		//发送任务信息查询
		public Message TaskList_Query()
		{
			System.Data.DataSet ds = null;
			try
			{
				SqlHelper.log.WriteLog(lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.SDOAPI_SDONoticeInfoAPI_SendNoticeList + "!");
				Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.SDOAPI_SDONoticeInfoAPI_SendNoticeList + "!");
				//从数据库里面将频道列表读出来
				ds = NoticeDataInfo.BoardTask_Query();
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut = new Query_Structure(6);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.SDO_TaskID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.SDO_BeginTime, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.SDO_EndTime, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.SDO_Interval, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[4]);
						strut.AddTagKey(TagName.SDO_Status, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						object boardMessage;
						if(ds.Tables[0].Rows[i].IsNull(5)==false)
							boardMessage = ds.Tables[0].Rows[i].ItemArray[5];
						else
							boardMessage = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,boardMessage);
						strut.AddTagKey(TagName.SDO_BoardMessage, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_BOARDTASK_QUERY_RESP,6);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDONoticeInfoAPI_NoNoticeList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_BOARDTASK_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.SDOAPI_SDONoticeInfoAPI_NoNoticeList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_BOARDTASK_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
            
		}
		//发送任务信息查询
		public Message TaskList_OnwerQuery()
		{
			int taskID = 0;
			string city = null;
			System.Data.DataSet ds = null;
			try
			{
				TLV_Structure tlvStrut = new TLV_Structure(TagName.SDO_TaskID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_TaskID).m_bValueBuffer);
				taskID =(int)tlvStrut.toInteger();
				SqlHelper.log.WriteLog(lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.SDOAPI_SDONoticeInfoAPI_SendNoticeList + ":" + taskID);
				Console.WriteLine(DateTime.Now+" - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.SDOAPI_SDONoticeInfoAPI_SendNoticeList + ":" + taskID);
				//从数据库里面将频道列表读出来
				ds = NoticeDataInfo.BoardTask_OwnerQuery(taskID);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						city+=ds.Tables[0].Rows[i].ItemArray[0].ToString()+",";
					}
					return Message.COMMON_MES_RESP(city,Msg_Category.SDO_ADMIN,ServiceKey.SDO_BOARDMESSAGE_REQ_RESP,TagName.SDO_ServerIP,TagFormat.TLV_STRING);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDONoticeInfoAPI_SendNoticeList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_BOARDMESSAGE_REQ_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.SDOAPI_SDONoticeInfoAPI_SendNoticeList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_BOARDTASK_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
            
		}
		//发送任务信息添加
		public Message TaskList_Insert()
		{
			string serverIP = null;
			string boardMessage = null;
			DateTime beginTime;
			DateTime endTime;
			int userbyID = 0;
			int interval = 0;
			int result = -1;
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				//操作员ID
				TLV_Structure tlvStrut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userbyID =(int)tlvStrut.toInteger();
				tlvStrut = new TLV_Structure(TagName.SDO_BeginTime,6,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_BeginTime).m_bValueBuffer);
				beginTime =tlvStrut.toTimeStamp();
				tlvStrut = new TLV_Structure(TagName.SDO_EndTime,6,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_EndTime).m_bValueBuffer);
				endTime =tlvStrut.toTimeStamp();
				boardMessage = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_BoardMessage).m_bValueBuffer);
				//发送间隔
				tlvStrut = new TLV_Structure(TagName.SDO_Interval,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Interval).m_bValueBuffer);
				interval =(int)tlvStrut.toInteger();
				SqlHelper.log.WriteLog(lg.API_Add + lg.SDOAPI_SDO + "+>" + lg.SDOAPI_SDONoticeInfoAPI_SendNoticeList + "!");
				Console.WriteLine(DateTime.Now+" - " + lg.API_Add + lg.SDOAPI_SDO + "+>" + lg.SDOAPI_SDONoticeInfoAPI_SendNoticeList + "!");

				result = NoticeDataInfo.BoardTask_Insert(userbyID,serverIP,boardMessage,beginTime,endTime,interval);
				if(result ==1)
				{
					return Message.COMMON_MES_RESP("SCUESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_BOARDTASK_INSERT_RESP);
				}
				else
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_BOARDTASK_INSERT_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.API_Update + lg.API_Failure,Msg_Category.SDO_ADMIN,ServiceKey.SDO_BOARDTASK_INSERT_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
            
		}
		//发送任务信息更新
		public Message TaskList_Update()
		{
			string serverIP = "";
			int userbyID = 0;
			int taskID = 0;
			DateTime beginTime = DateTime.Now;
			DateTime endTime = DateTime.Now;
			int interval = 0;
			int status = 0;
			string boardMessage = "";
			int result = -1;
			try
			{
				//操作员ID
				TLV_Structure tlvStrut = new TLV_Structure(TagName.UserByID,3,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userbyID =(int)tlvStrut.toInteger();
				//发送状态
				tlvStrut = new TLV_Structure(TagName.SDO_Status,3,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Status).m_bValueBuffer);
				status =(int)tlvStrut.toInteger();
				//任务ID
				tlvStrut = new TLV_Structure(TagName.SDO_TaskID,3,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_TaskID).m_bValueBuffer);
				taskID =(int)tlvStrut.toInteger();
				if(status==0)
				{
					serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);

					tlvStrut = new TLV_Structure(TagName.SDO_BeginTime,6,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_BeginTime).m_bValueBuffer);
					beginTime =tlvStrut.toTimeStamp();

					tlvStrut = new TLV_Structure(TagName.SDO_EndTime,6,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_EndTime).m_bValueBuffer);
					endTime =tlvStrut.toTimeStamp();
					//发送间隔
					tlvStrut = new TLV_Structure(TagName.SDO_Interval,3,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Interval).m_bValueBuffer);
					interval =(int)tlvStrut.toInteger();

					boardMessage  = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_BoardMessage).m_bValueBuffer);
				}
				SqlHelper.log.WriteLog(lg.API_Update + lg.SDOAPI_SDO + "+>" + lg.SDOAPI_SDONoticeInfoAPI_SendNoticeList + "!");
				Console.WriteLine(DateTime.Now+" - " + lg.API_Update + lg.SDOAPI_SDO + "+>" + lg.SDOAPI_SDONoticeInfoAPI_SendNoticeList + "!");

				result = NoticeDataInfo.BoardTask_Update(serverIP,userbyID,taskID,beginTime,endTime,interval,status,boardMessage);
				if(result ==1)
				{
					return Message.COMMON_MES_RESP("SCUESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_BOARDTASK_UPDATE_RESP);
				}
				else
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_BOARDTASK_UPDATE_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.API_Update + lg.API_Failure,Msg_Category.SDO_ADMIN,ServiceKey.SDO_BOARDTASK_UPDATE_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
            
		}
		//发送任务信息删除
		public Message TaskList_Delete()
		{
			int result = -1;
			int userbyID = 0;
			int taskID = 0;
			try
			{
				//操作员ID
				TLV_Structure tlvStrut = new TLV_Structure(TagName.UserByID,3,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userbyID =(int)tlvStrut.toInteger();

				//任务ID
				tlvStrut = new TLV_Structure(TagName.SDO_TaskID,3,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_TaskID).m_bValueBuffer);
				taskID =(int)tlvStrut.toInteger();
				SqlHelper.log.WriteLog(lg.API_Delete + lg.SDOAPI_SDO + "+>" + lg.SDOAPI_SDONoticeInfoAPI_SendNoticeList + "!");
				Console.WriteLine(DateTime.Now+" - " + lg.API_Delete + lg.SDOAPI_SDO + "+>" + lg.SDOAPI_SDONoticeInfoAPI_SendNoticeList + "!");

				result = NoticeDataInfo.BoardTask_delete(userbyID,taskID);
				if(result ==1)
				{
			
					return Message.COMMON_MES_RESP("SCUESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_BOARDTASK_UPDATE_RESP);
				}
				else
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_BOARDTASK_UPDATE_RESP);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.API_Update + lg.API_Failure,Msg_Category.SDO_ADMIN,ServiceKey.SDO_ACCOUNT_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
            
		}
		//频道列表请求
		public Message ChannelList_Query()
		{
			System.Data.DataSet ds = null;
			string serverIP = null;
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				//发送登陆请求消息
				connect_Req(serverIP);
				NoticeDataInfo.TruncateTable_Req();

				while(listCnt<=7)
				{
					//接受消息
					if(receiveMsg()>0)
					{
						//解析消息
						parseMsg();
					}
					//Thread.Sleep(1000);
				}
				client.Close();
				SqlHelper.log.WriteLog(lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.SDOAPI_SDONoticeInfoAPI_ChannelList);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+lg.SDOAPI_SDONoticeInfoAPI_ChannelList);
				//从数据库里面将频道列表读出来
				ds = NoticeDataInfo.ChannelList_Req();
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut = new Query_Structure(5);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.SDO_wPlanetID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.SDO_wChannelID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.SDO_iLimitUser, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.SDO_iCurrentUser, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[4]);
						strut.AddTagKey(TagName.SDO_ipaddr, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHANNELLIST_QUERY_RESP,5);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDONoticeInfoAPI_NoChannelInfo,Msg_Category.SDO_ADMIN,ServiceKey.SDO_ACCOUNT_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.SDOAPI_SDONoticeInfoAPI_NoChannelInfo,Msg_Category.SDO_ADMIN,ServiceKey.SDO_ACCOUNT_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
		}
		/*接收消息
		 * */
		int receiveMsg()
		{
			byte[] recvBuf  = new byte[4096];
			short size = 0;
			try
			{
				if(client.Receive(recvBuf,0,2,0)!= 2)
					return 0;
				//size= System.BitConverter.ToInt16(recvSize,0);
				size = (short)CMsg.toInteger(recvBuf,2);
				if(size > 0)
				{
					if(size > CMsg.NETWORK_MSG_SIZE)
					{
						//Console.WriteLine("Message Too Large");
						return 0;
					}
					if(client.Receive(recvBuf,2,size-2,0) == size-2)
					{
						msgStruct.Clear();
						System.Array.Copy(recvBuf,msgStruct.GetBuf(),size);
						msgStruct.Replicate();
						return size;
					}
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return 0;
			}
			return 0;
		}
		/*消息解析
		 * */
		void parseMsg()
		{
			switch(msgStruct.ID())
			{

				case protocol.GW_LOGIN_ACK:
				{
					int res = 0;
					//m_msg >> res;
					if(res == (int)protocol.RET_OK)
					{
						//Console.WriteLine("登录成功");
						//发送频道列表请求消息
						msgStruct = new CMsg((short)protocol.GM_CHANNELLIST_REQ);
						msgStruct.writeLength(msgStruct.GetSize());
						client.Send(msgStruct.GetBuf(),0,msgStruct.GetSize(),0);
					}
					else
					{
						Console.WriteLine(lg.SDOAPI_SDONoticeInfoAPI_LoginFailure);

					}
				}
					break;
				case protocol.GM_CHANNELLIST_ACK:
				{
					int nChan = 0;
					byte bHasMore =0;
					short wPlanetID = 0, wChannelID = 0;
					int iLimitUser = 0, iCurrentUser = 0;
					string ipaddr = "";
					nChan = (int)msgStruct.ReadData(nChan,4);
					bHasMore = Convert.ToByte(msgStruct.ReadData(bHasMore,1));
					//Console.WriteLine("收到频道信息"+nChan+"个");

					for(int i=0; i<nChan; i++)
					{
						try
						{
							//星球ID
							wPlanetID=Convert.ToInt16(msgStruct.ReadData(wPlanetID,2));
							//频道ID
							wChannelID=Convert.ToInt16(msgStruct.ReadData(wChannelID,2));
							//最大限制人数
							iLimitUser=(int)msgStruct.ReadData(iLimitUser,4);
							//当前人数
							iCurrentUser=(int)msgStruct.ReadData(iCurrentUser,4);
							//IP地址
							ipaddr = Convert.ToString(msgStruct.ReadData(ipaddr,15));
							//Console.WriteLine(wPlanetID+"/"+wChannelID+"/"+iLimitUser+"/"+iCurrentUser+"/"+ipaddr+"\n");
							NoticeDataInfo.InsertChannel_Req(wPlanetID,wChannelID,iLimitUser,iCurrentUser,ipaddr);                        

						}
						catch(System.Exception ex)
						{
							Console.WriteLine(ex.Message);
						}

					}
					listCnt ++;
				}
					break;
				case protocol.EH_ALIVE_REQ:
				{
					msgStruct = new CMsg((short)protocol.EH_ALIVE_ACK);
					send_msg(msgStruct);

				}
					break;
				default:
					//Console.WriteLine("Unknown Message Received:"+msgStruct.ID());
					break;
			}
		}
		//发送心跳消息
		public Message sendAlive_Req()
		{
			msgStruct = new CMsg((short)protocol.EH_ALIVE_ACK);
			send_msg(msgStruct);
			return Message.COMMON_MES_RESP(1,Msg_Category.SDO_ADMIN,ServiceKey.SDO_ALIVE_REQ_RESP,TagName.Status,TagFormat.TLV_INTEGER);
		}
		int send_msg(CMsg msg)
		{
			return client.Send(msg.GetBuf(),msg.GetSize(),0);
		}
		public void sendBoardMsg_Req()
		{
			int nChan = 40;
			string strNotice = null;
			msgStruct = new CMsg((short)protocol.GM_NOTICE_REQ);
			msgStruct.WriteData(System.Text.Encoding.Default.GetBytes(strNotice),strNotice.Length);
			byte[] ret = new byte[]{
									   (byte)nChan,(byte)(nChan>>8)
								   };
			msgStruct.WriteData(ret,4);
			client.Send(msgStruct.GetBuf(),0,msgStruct.GetSize(),0);   
			for(int i=0;i<nChan;i++)
			{
				ulong itemdata = 0;
				ret = new byte[]{
									(byte)LOWORD(itemdata),(byte)(LOWORD(itemdata)>>8)
								};
				msgStruct.WriteData(ret,2);
				ret = new byte[]{
									(byte)HIWORD(itemdata),(byte)(HIWORD(itemdata)>>8)
								};
				msgStruct.WriteData(ret,2);
			}
			msgStruct.writeLength(msgStruct.GetSize());
			client.Send(msgStruct.GetBuf(),0,msgStruct.GetSize(),0);
		}
		public int _send(byte[] buf,int len,int flags)
		{
			int sended=0;
			while(sended<len)
			{
				int now=client.Send(buf,sended,len-sended,0);
				if(now==-1) break;
				sended+=now;
			}
			return sended;
		}
		public int _recv(byte[] buf,int len,int flags)
		{
			int recved=0;
			while(recved<len)
			{
				int now=client.Receive(buf,recved,len-recved,0);
				if(now==-1||now==0) break;
				recved+=now;
			}
			return recved;
		}
		short LOWORD(ulong itemData)
		{
			return ((short)((ulong)(itemData) & 0xffff));
		}
		short HIWORD(ulong itemData)
		{
			return ((short)((ulong)(itemData) >> 16));
		}
	}
}
