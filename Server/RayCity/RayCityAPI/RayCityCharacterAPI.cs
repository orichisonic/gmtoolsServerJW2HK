using System;
using System.Data;
using System.Text;
using Common.Logic;
using Common.API;
using Common.DataInfo;
using RayCity.RayCityDataInfo;
using lg = Common.API.LanguageAPI;
namespace RayCity.RayCityAPI
{
	/// <summary>
	/// RayCityCharacterAPI 的摘要说明。
	/// </summary>
	public class RayCityCharacterAPI
	{
		public RayCityCharacterAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		Message msg = null;
		public RayCityCharacterAPI(byte[] packets)
		{
			msg = new Message(packets,(uint)packets.Length);
		}
		/// <summary>
		/// 玩家人物資料資訊
		/// </summary>
		/// <returns></returns>
		public Message RCCharInfo_Query()
		{
			string serverIP = null;
			int accountID = 0;
			int characterID = 0;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.RayCity_AccountID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_AccountID).m_bValueBuffer);
				accountID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.RayCity_CharacterID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_CharacterID).m_bValueBuffer);
				characterID =(int)strut.toInteger();

				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_AccountInfo);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_AccountInfo);
				ds = CharacterInfo.characterInfo_Query(serverIP,characterID,accountID);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,10,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_Character_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.RayCityAPI_NoAccountInfo,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_Character_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.RayCityAPI_NoAccountInfo, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_Character_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 玩家排行榜
		/// </summary>
		/// <returns></returns>
		public Message PlayerSkill_Query()
		{
			string serverIP = null;
			int accountID = 0;
			int characterID = 0;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.RayCity_AccountID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_AccountID).m_bValueBuffer);
				accountID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.RayCity_CharacterID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_CharacterID).m_bValueBuffer);
				characterID =(int)strut.toInteger();

				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_AccountInfo);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_AccountInfo);
				ds = CharacterInfo.characterInfo_Query(serverIP,characterID,accountID);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,1,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_Character_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.RayCityAPI_NoAccountInfo,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_Character_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.RayCityAPI_NoAccountInfo, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_Character_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 玩家人物狀態資訊
		/// </summary>
		/// <returns></returns>
		public Message RCCharStateInfo_Query()
		{
			string serverIP = null;
			int accountID = 0;
			int characterID = 0;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.RayCity_AccountID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_AccountID).m_bValueBuffer);
				accountID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.RayCity_CharacterID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_CharacterID).m_bValueBuffer);
				characterID =(int)strut.toInteger();
				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_CurrentState);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_CurrentState);
				ds = CharacterInfo.characterStateInfo_Query(serverIP,characterID,accountID);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,1,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_CharacterState_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.RayCityAPI_NoCurrentState,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_CharacterState_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.RayCityAPI_NoCurrentState, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_CharacterState_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 玩家車資料資訊
		/// </summary>
		/// <returns></returns>
		public Message RCCarInfo_Query()
		{
			string serverIP = null;
			int characterID = 0;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.RayCity_CharacterID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_CharacterID).m_bValueBuffer);
				characterID =(int)strut.toInteger();
				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_Car_CurrentState);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_Car_CurrentState);
				ds = CharacterInfo.CarInfo_Query(serverIP,characterID);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,20,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_CarList_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.RayCityAPI_Car_NoCurrentState,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_CarList_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.RayCityAPI_Car_NoCurrentState, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_CarList_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 玩家車装备資料資訊
		/// </summary>
		/// <returns></returns>
		public Message RCCarInventoryInfo_Query()
		{
			string serverIP = null;
			int garageID = 0;
			int characterID = 0;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.RayCity_CharacterID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_CharacterID).m_bValueBuffer);
				characterID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.RayCity_CarIDX,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_CarIDX).m_bValueBuffer);
				garageID =(int)strut.toInteger();
				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_Car_itemState);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_Car_itemState);
				ds = CharacterInfo.CarInventoryInfo_Query(serverIP,characterID,garageID);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,30,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_CarInventory_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.RayCityAPI_Car_NoitemState,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_CarInventory_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.RayCityAPI_Car_NoitemState, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_CarInventory_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 玩家連接狀態
		/// </summary>
		/// <returns></returns>
		public Message RCConnectionState_Query()
		{
			string serverIP = null;
			int characterID = 0;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.RayCity_CharacterID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_CharacterID).m_bValueBuffer);
				characterID =(int)strut.toInteger();
				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_Car_ConnectState);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_Car_ConnectState);
				ds = CharacterInfo.ConnectionStateInfo_Query(serverIP,characterID);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,20,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_ConnectionState_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.RayCityAPI_Car_NoConnectState,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_ConnectionState_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.RayCityAPI_Car_NoConnectState, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_ConnectionState_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 得到玩家好友列表
		/// </summary>
		/// <returns></returns>
		public Message RCFriendList_Query()
		{
			string serverIP = null;
			int characterID = 0;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.RayCity_CharacterID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_CharacterID).m_bValueBuffer);
				characterID =(int)strut.toInteger();
				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_FirendState);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_FirendState);
				ds = CharacterInfo.FriendList_Query(serverIP,characterID);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,20,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_FriendList_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.RayCityAPI_NoFirendState,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_FriendList_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.RayCityAPI_NoFirendState, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_FriendList_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 得到速度資訊
		/// </summary>
		/// <returns></returns>
		public Message RCRaceState_Query()
		{
			string serverIP = null;
			int characterID = 0;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.RayCity_CharacterID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_CharacterID).m_bValueBuffer);
				characterID =(int)strut.toInteger();
				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_SpeedState);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_SpeedState);
				ds = CharacterInfo.RaceState_Query(serverIP,characterID);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,1,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_RaceState_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.RayCityAPI_NoSpeedState,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_RaceState_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.RayCityAPI_NoSpeedState, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_RaceState_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 重置玩家初始位置
		/// </summary>
		/// <returns></returns>
		public Message SetResetPos()
		{
			string serverIP = null;
			string account = null;
			int operateUserID = 0;
			int characterID = 0;
			int result = -1;
			try
			{
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_NyUserID).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				strut = new TLV_Structure(TagName.RayCity_CharacterID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_CharacterID).m_bValueBuffer);
				characterID =(int)strut.toInteger();
				result = CharacterInfo.SetResetPos(operateUserID,serverIP,account,characterID);	
				if(result==1)
				{
					SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Again+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_firstAddress+lg.API_Success);
					Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_firstAddress+lg.API_Success);
					return Message.COMMON_MES_RESP("SUCCESS",Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_SetPos_Update_Resp);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Again+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_firstAddress+lg.API_Failure);
					Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_firstAddress+lg.API_Failure);
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_SetPos_Update_Resp);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.API_Error, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_ConnectionState_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 批量創建帳號
		/// </summary>
		/// <returns></returns>
		public Message PlayerAccount_Create()
		{
			string serverIP = null;
			string account = null;
			int operateUserID = 0;
			string NyUserID = null;
			string NyPassWD = null;
			int startNum  = 0;
			int endNum  = 0;
			int result = -1;
			try
			{
				
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				NyUserID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_NyUserID).m_bValueBuffer);
				NyPassWD = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_NyPassword).m_bValueBuffer);
				strut = new TLV_Structure(TagName.RayCity_StartNum,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_StartNum).m_bValueBuffer);
				startNum =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.RayCity_EndNum,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_EndNum).m_bValueBuffer);
				endNum =(int)strut.toInteger();
				result = CharacterInfo.CreatePlayerAccount(operateUserID,serverIP,NyUserID,NyPassWD,startNum,endNum);	
				if(result==1)
				{
					SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.API_Create+lg.RayCityAPI_Account+lg.RayCityAPI_Char+NyUserID+lg.API_From+startNum+lg.API_To+endNum+lg.API_Success);
					Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.API_Create+lg.RayCityAPI_Account+lg.RayCityAPI_Char+NyUserID+lg.API_From+startNum+lg.API_To+endNum+lg.API_Success);
					return Message.COMMON_MES_RESP("SUCCESS",Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_PlayerAccount_Create_Resp);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.API_Create+lg.RayCityAPI_Account+lg.RayCityAPI_Char+NyUserID+lg.API_From+startNum+lg.API_To+endNum+lg.API_Failure);
					Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.API_Create+lg.RayCityAPI_Account+lg.RayCityAPI_Char+NyUserID+lg.API_From+startNum+lg.API_To+endNum+lg.API_Failure);
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_PlayerAccount_Create_Resp);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.API_Error, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_PlayerAccount_Create_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}

		#region 玩家活动卡查询
		/// <summary>
		/// 玩家活动卡查询
		/// </summary>
		/// <returns></returns>
		public Message RcUserActiveCard_Query()
		{
			string activeCode = null;
			string strDesc = null;
			int atype = 0;
			System.Collections.ArrayList ds = new System.Collections.ArrayList();
			try
			{
				activeCode = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_activename).m_bValueBuffer);
				TLV_Structure strut1 = new TLV_Structure(TagName.RayCity_ActionType,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ActionType).m_bValueBuffer);
				atype =(int)strut1.toInteger();

				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Account+activeCode+lg.RayCityAPI_ActiveCard_Query);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Account+activeCode+lg.RayCityAPI_ActiveCard_Query);
				if(atype == 1)
				{
					ds=CharacterInfo.ActiveCard_Query(activeCode,1,ref strDesc);
				}
				else if(atype == 2)
				{
					ds=CharacterInfo.ActiveCard_Query(activeCode,2,ref strDesc);
				}
				
				if(ds!=null && ds.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[1];

					Query_Structure strut = new Query_Structure((uint)ds.Count);

					byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds[0]);
					strut.AddTagKey(TagName.RayCity_rccdkey,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);

					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds[1]);
					strut.AddTagKey(TagName.RayCity_getuser,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);

					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds[2]);
					strut.AddTagKey(TagName.RayCity_gettime, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds[3]);
					strut.AddTagKey(TagName.RayCity_use_state,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);

					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds[4]);
					strut.AddTagKey(TagName.RayCity_activename, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

					structList[0] = strut;

					return Message.COMMON_MES_RESP(structList,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_ActiveCard_Query_Resp,5);
				}
				else
				{
					return Message.COMMON_MES_RESP(strDesc,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_ActiveCard_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception)
			{
				return Message.COMMON_MES_RESP(strDesc,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_ActiveCard_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);                
			}
            
		}
		#endregion
	}
}
