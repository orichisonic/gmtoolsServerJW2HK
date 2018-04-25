using System;
using System.Data;
using System.Text;
using SDO.SDODataInfo;
using Common.Logic;
using Common.DataInfo;
using lg = Common.API.LanguageAPI;
namespace SDO.SDOAPI
{
	/// <summary>
	/// SDOCharacterInfoAPI 的摘要说明。
	/// </summary>
	public class SDOCharacterInfoAPI
	{
		Message msg = null;
		public SDOCharacterInfoAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public SDOCharacterInfoAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
			
		}
		/// <summary>
		/// 玩家人物资料信息
		/// </summary>
		/// <returns></returns>
		public Message SDOCharInfo_Query()
		{
			string serverIP = null;
			string account = null;
			string userNick = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
				userNick = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_NickName).m_bValueBuffer);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+" "+account + lg.SDOAPI_SDOCharacterInfoAPI_AccountInfo + "!");
				ds = CharacterInfo.characterInfo_Query(serverIP,account,userNick);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.SDO_UserIndexID,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.SDO_Account,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[15]);
						strut.AddTagKey(TagName.SDO_NickName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.SDO_Level,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.SDO_Exp,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[4]);
						strut.AddTagKey(TagName.SDO_GameTotal,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[5]);
						strut.AddTagKey(TagName.SDO_GameWin,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[6]);
						strut.AddTagKey(TagName.SDO_DogFall,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[7]);
						strut.AddTagKey(TagName.SDO_GameFall,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[8]);
						strut.AddTagKey(TagName.SDO_Reputation,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[9]);
						strut.AddTagKey(TagName.SDO_GCash,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[10]);
						strut.AddTagKey(TagName.SDO_MCash,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						object address;
						if(ds.Tables[0].Rows[i].IsNull(11)==false)
							address = ds.Tables[0].Rows[i].ItemArray[11];
						else
							address = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,address);
						strut.AddTagKey(TagName.SDO_Address,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						object age;
						if(ds.Tables[0].Rows[i].IsNull(12)==false)
							age = ds.Tables[0].Rows[i].ItemArray[12];
						else
							age = 0;
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,age);
						strut.AddTagKey(TagName.SDO_Age,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						object city;
						if(ds.Tables[0].Rows[i].IsNull(13)==false)
							city = ds.Tables[0].Rows[i].ItemArray[13];
						else
							city = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,city);
						strut.AddTagKey(TagName.SDO_City,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_BOOLEAN,ds.Tables[0].Rows[i].ItemArray[14]);
						strut.AddTagKey(TagName.SDO_SEX,TagFormat.TLV_BOOLEAN,(uint)bytes.Length,bytes);

						object Main_Ch;
						if(ds.Tables[0].Rows[i].IsNull(16)==false)
							Main_Ch = ds.Tables[0].Rows[i].ItemArray[16].ToString();
						else
							Main_Ch = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,Main_Ch);
						strut.AddTagKey(TagName.SDO_MAIN_CH,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);

						object Sub_Ch;
						if(ds.Tables[0].Rows[i].IsNull(17)==false)
							Sub_Ch = ds.Tables[0].Rows[i].ItemArray[17].ToString();
						else
							Sub_Ch = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,Sub_Ch);
						strut.AddTagKey(TagName.SDO_SUB_CH,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[18]);
						strut.AddTagKey(TagName.SDO_Lock_Status, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						structList[i]=strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHARACTERINFO_QUERY_RESP,19);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDOCharacterInfoAPI_NoRelativeInfo,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHARACTERINFO_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.SDOAPI_SDOCharacterInfoAPI_NoRelativeInfo, Msg_Category.SDO_ADMIN, ServiceKey.SDO_CHARACTERINFO_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 玩家资料信息修改
		/// </summary>
		/// <returns></returns>
		public Message SDOCharacterInfo_Update()
		{
			int result = -1;
			string serverIP = null;
			string account = null;
			int level = 0;
			int experience =0; 
			int operateUserID = 0;
			int battle = 0;
			int win = 0;
			int draw = 0;
			int lose = 0;
			int MCash = 0;
			int GCash = 0;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
//				strut = new TLV_Structure(TagName.SDO_Level,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Level).m_bValueBuffer);
//				level  =(int)strut.toInteger();
			    strut = new TLV_Structure(TagName.SDO_Exp,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Exp).m_bValueBuffer);
				experience  =(int)strut.toInteger();
//				strut = new TLV_Structure(TagName.SDO_GameTotal,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_GameTotal).m_bValueBuffer);
//				battle  =(int)strut.toInteger();
    			strut = new TLV_Structure(TagName.SDO_GameWin,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_GameWin).m_bValueBuffer);
				win  =(int)strut.toInteger();
//				strut = new TLV_Structure(TagName.SDO_DogFall,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_DogFall).m_bValueBuffer);
//				draw  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_GameFall,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_GameFall).m_bValueBuffer);
				lose  =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.SDO_MCash, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_MCash).m_bValueBuffer);
				MCash = (int)strut.toInteger();
//				strut = new TLV_Structure(TagName.SDO_GCash, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_GCash).m_bValueBuffer);
//				GCash = (int)strut.toInteger();
				result = CharacterInfo.characterInfo_Update(operateUserID,serverIP,account,level,experience,battle,win,draw,lose,MCash,GCash);
				if (result == -1)
				{
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) +" "+ account + lg.SDOAPI_SDOCharacterInfoAPI_NoAccount);
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDOCharacterInfoAPI_NoAccount, Msg_Category.SDO_ADMIN, ServiceKey.SDO_CHARACTERINFO_UPDATE_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
				else if(result==1)
				{
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+" "+account + lg.API_Update + lg.SDOAPI_SDOCharacterInfoAPI_AccountInfo + lg.API_Success + "!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHARACTERINFO_UPDATE_RESP);
				}
				else
				{
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+" "+account + lg.API_Update + lg.SDOAPI_SDOCharacterInfoAPI_AccountInfo + lg.API_Failure + "!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHARACTERINFO_UPDATE_RESP);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_CHARACTERINFO_UPDATE_RESP);
			}

		}
		/// <summary>
		/// 玩家人物资料信息
		/// </summary>
		/// <returns></returns>
		public Message friendsnick_Query()
		{
			string serverIP = null;
			string account = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);				
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP)+" "+account + lg.SDOAPI_SDOCharacterInfoAPI_FriendList + "!");
				ds = CharacterInfo.friendsnick_Query(serverIP,account);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.SDO_NickName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);							
						structList[i]=strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_FRIENDS_QUERY_RESP,1);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDOCharacterInfoAPI_NoFriendList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_FRIENDS_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.SDOAPI_SDOCharacterInfoAPI_NoFriendList, Msg_Category.SDO_ADMIN, ServiceKey.SDO_FRIENDS_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 剔除在线游戏玩家
		/// </summary>
		/// <returns></returns>
		public Message SDOUserOnline_out()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string account = null;
			//string passWD = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
				//passWD = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Passwd).m_bValueBuffer);
				result = CharacterInfo.userOnline_out(operateUserID,serverIP,account);
				if (result == -1)
				{
					SqlHelper.log.WriteLog(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) +" "+ account + lg.SDOAPI_SDOCharacterInfoAPI_NotOnline);
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) +" "+ account + lg.SDOAPI_SDOCharacterInfoAPI_NotOnline);
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDOCharacterInfoAPI_NotOnline, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERLOGIN_DEL_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
				else if(result==1)
				{
					SqlHelper.log.WriteLog(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) +" "+ account + lg.SDOAPI_SDOCharacterInfoAPI_KickSuccess);
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) +" "+ account + lg.SDOAPI_SDOCharacterInfoAPI_KickSuccess);
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_USERLOGIN_DEL_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) +" "+ account + lg.SDOAPI_SDOCharacterInfoAPI_KickFailure);
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) +" "+ account +  lg.SDOAPI_SDOCharacterInfoAPI_KickFailure);
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_USERLOGIN_DEL_RESP);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.SDOAPI_SDOCharacterInfoAPI_NotOnline,Msg_Category.SDO_ADMIN,ServiceKey.SDO_USERLOGIN_DEL_RESP);
			}

		}
		/// <summary>
		/// 剔除在线游戏玩家
		/// </summary>
		/// <returns></returns>
		public Message SDOGateWay_out()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string addrIP = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				addrIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Address).m_bValueBuffer);
				//passWD = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Passwd).m_bValueBuffer);
				result = CharacterInfo.gateWayOnlineUser_out(operateUserID,serverIP,addrIP);
				if (result == -1)
				{
					SqlHelper.log.WriteLog(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) +" " + lg.SDOAPI_SDOCharacterInfoAPI_GateWay + addrIP + "Not Exist");
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) +" " + lg.SDOAPI_SDOCharacterInfoAPI_GateWay + addrIP + "Not Exist!");
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDOCharacterInfoAPI_NoAccount, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERLOGIN_DEL_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
				else if(result==1)
				{
					SqlHelper.log.WriteLog(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) +" "+lg.SDOAPI_SDOCharacterInfoAPI_GateWay+addrIP+ lg.SDOAPI_SDOCharacterInfoAPI_KickSuccess);
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) +" "+lg.SDOAPI_SDOCharacterInfoAPI_GateWay+addrIP+lg.SDOAPI_SDOCharacterInfoAPI_KickSuccess);
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.SDO_ADMIN,ServiceKey.SDO_USERLOGIN_DEL_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) +" "+lg.SDOAPI_SDOCharacterInfoAPI_GateWay+addrIP+ lg.SDOAPI_SDOCharacterInfoAPI_KickFailure);
					Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) +" "+lg.SDOAPI_SDOCharacterInfoAPI_GateWay+addrIP+ lg.SDOAPI_SDOCharacterInfoAPI_KickFailure);
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SDO_ADMIN,ServiceKey.SDO_USERLOGIN_DEL_RESP);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.SDO_ADMIN,ServiceKey.SDO_USERLOGIN_DEL_RESP);
			}

		}
       
		/// <summary>
		/// 网关查询
		/// </summary>
		/// <returns></returns>
		public Message SDO_GateWay_Query(int index,int pageSize)
		{
			string serverIP = null;
			string gatewayIP = null;
			System.Data.DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				gatewayIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Address).m_bValueBuffer);
				SqlHelper.log.WriteLog(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDOCharacterInfoAPI_GateWay);
				Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDOCharacterInfoAPI_GateWay);
				//查询GATEWAY IP
				ds = CharacterInfo.ServerGateWay_Query(serverIP,gatewayIP);
				if (ds!=null && ds.Tables[0].Rows.Count > 0)
				{
					//总页数
					int pageCount = 0;
					pageCount = ds.Tables[0].Rows.Count % pageSize;
					if (pageCount > 0)
					{
						pageCount = ds.Tables[0].Rows.Count / pageSize + 1;
					}
					else
						pageCount = ds.Tables[0].Rows.Count / pageSize;
					if (index + pageSize > ds.Tables[0].Rows.Count)
					{
						pageSize = ds.Tables[0].Rows.Count - index;
					}
					Query_Structure[] structList = new Query_Structure[pageSize];
					for (int i = index; i < index + pageSize; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length+1);
						//ADDR IP
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.SDO_Address, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						//总页数
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i - index] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.SDO_ADMIN, ServiceKey.SDO_GATEWAY_QUERY_RESP, 2);
				}
				else
					return Message.COMMON_MES_RESP(lg.SDOAPI_SDOCharacterInfoAPI_NotOnline, Msg_Category.SDO_ADMIN, ServiceKey.SDO_GATEWAY_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

			}
			catch (System.Exception ex)
			{
				return Message.COMMON_MES_RESP(lg.SDOAPI_SDOCharacterInfoAPI_NotOnline, Msg_Category.SDO_ADMIN, ServiceKey.SDO_GATEWAY_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}

	}
}
