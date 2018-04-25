using System;
using System.Text;
using System.Data;
using Common.Logic;
using Common.API;
using Common.DataInfo;
using RayCity.RayCityDataInfo;
using lg = Common.API.LanguageAPI;
namespace RayCity.RayCityAPI
{
	/// <summary>
	/// RayCityItemShopAPI 的摘要说明。
	/// </summary>
	public class RayCityItemShopAPI
	{
		Message msg = null;
		public RayCityItemShopAPI(byte[] packets)
		{
			msg = new Message(packets,(uint)packets.Length);
		}
		public RayCityItemShopAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		/// <summary>
		/// 查询所有道具分类資訊
		/// </summary>
		/// <returns></returns>
		public Message RayCity_ItemShopType_Query()
		{
			DataSet ds = null;
			try
			{
				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_ItemShopType);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_ItemShopType);
				ds = ItemShopInfo.RayCity_ItemShopType_Query();	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,40,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_ItemType_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.RayCityAPI_NoItemInfo,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_ItemType_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.RayCityAPI_NoItemInfo, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_ItemType_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 查询所有道具資訊
		/// </summary>
		/// <returns></returns>
		public Message RayCity_ItemShop_Query(int index,int pageSize)
		{
			string itemName = null;
			int itemCode = 0;
			string serverIP = null;
			DataSet ds = null;
			try
			{

				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				itemName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ItemName).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.RayCity_ItemID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ItemID).m_bValueBuffer);
				itemCode =(int)strut.toInteger();	
				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+lg.RayCityAPI_ItemInfo);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+lg.RayCityAPI_ItemInfo);
				ds = ItemShopInfo.RayCity_ItemShop_Query(itemCode,itemName,serverIP);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_ItemShop_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.RayCityAPI_NoItemInfo,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_ItemShop_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(lg.RayCityAPI_NoItemInfo, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_ItemShop_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 添加玩家道具
		/// </summary>
		/// <returns></returns>
		public Message RayCity_ItemShop_Insert()
		{
			string serverIP = null;
            string account = null;
			int operateUserID  = 0;
			int characterID = 0;
			int itemID =  0;
			string giftMessage = null;
			int result = -1;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_NyUserID).m_bValueBuffer);
				strut = new TLV_Structure(TagName.RayCity_ItemID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ItemID).m_bValueBuffer);
				itemID =(int)strut.toInteger();	
				strut = new TLV_Structure(TagName.RayCity_CharacterID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_CharacterID).m_bValueBuffer);
				characterID =(int)strut.toInteger();			
				giftMessage = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_Message).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.API_Add+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_ItemInfo);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.API_Add+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_ItemInfo);
				result = ItemShopInfo.RayCity_ItemShop_Insert(operateUserID,serverIP,account,characterID,itemID,giftMessage);	
				if(result==1)
				{
					return Message.COMMON_MES_RESP("SUCCESS",Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_ItemShop_Insert_Resp,TagName.Status,TagFormat.TLV_STRING);
				}
				else
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_ItemShop_Insert_Resp,TagName.Status,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.API_Error, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_ItemShop_Insert_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 添加玩家金钱
		/// </summary>
		/// <returns></returns>
		public Message RayCity_AddMoney()
		{
			int operateUserID = 0;
			string serverIP = null;
			string account = null;
			int characterID = 0;
			int money =  0;
			string giftTitle = null;
			string giftMessage = null;
			int result = -1;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_NyUserID).m_bValueBuffer);
				strut = new TLV_Structure(TagName.RayCity_CharacterMoney,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_CharacterMoney).m_bValueBuffer);
				money =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.RayCity_CharacterID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_CharacterID).m_bValueBuffer);
				characterID =(int)strut.toInteger();		
				giftTitle = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_Title).m_bValueBuffer);
				giftMessage = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_Message).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.API_Add+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_ItemInfo);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.API_Add+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_ItemInfo);
				result = ItemShopInfo.RayCity_AddMoney(operateUserID,serverIP,account,characterID,money,giftTitle,giftMessage);	
				if(result==1)
				{
					return Message.COMMON_MES_RESP("SUCCESS",Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_AddMoney_Resp,TagName.Status,TagFormat.TLV_STRING);
				}
				else
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_AddMoney_Resp,TagName.Status,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.API_Error, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_AddMoney_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 查询所有技能資訊
		/// </summary>
		/// <returns></returns>
		public Message RayCity_Skill_Query()
		{
			string serverIP = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_SkillInfo);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_SkillInfo);
				ds = ItemShopInfo.Skill_Query(serverIP);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,40,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_Skill_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.RayCityAPI_NoSkillInfo,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_Skill_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.RayCityAPI_NoSkillInfo, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_Skill_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 查询玩家技能資訊
		/// </summary>
		/// <returns></returns>
		public Message RayCity_PlayerSkill_Query()
		{
			string serverIP = null;
			int characterID = 0;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.RayCity_CharacterID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_CharacterID).m_bValueBuffer);
				characterID =(int)strut.toInteger();
				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_SkillInfo);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_SkillInfo);
				ds = ItemShopInfo.PlayerSkill_Query(serverIP,characterID);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,40,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_PlayerSkill_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.RayCityAPI_NoSkillInfo,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_PlayerSkill_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.RayCityAPI_NoSkillInfo, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_PlayerSkill_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 添加玩家技能
		/// </summary>
		/// <returns></returns>
		public Message RayCity_PlayerSkill_Insert()
		{
			int operateUserID = 0;
			string serverIP = null;
			string account = null;
			int characterID = 0;
			int skillID =  0;
			int skilllvl = 0;
			int result = -1;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_NyUserID).m_bValueBuffer);
				strut = new TLV_Structure(TagName.RayCity_SkillID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_SkillID).m_bValueBuffer);
				skillID =(int)strut.toInteger();	
				strut = new TLV_Structure(TagName.RayCity_CharacterID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_CharacterID).m_bValueBuffer);
				characterID =(int)strut.toInteger();	
				strut = new TLV_Structure(TagName.RayCity_SkillLevel,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_SkillLevel).m_bValueBuffer);
				skilllvl =(int)strut.toInteger();
				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.API_Add+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_SkillInfo);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.API_Add+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_SkillInfo);
				result = ItemShopInfo.PlayerSkill_Insert(operateUserID,serverIP,account,characterID,skillID,skilllvl);	
				if(result==1)
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_PlayerSkill_Insert_Resp,TagName.Status,TagFormat.TLV_STRING);
				}
				else
				{
                    return Message.COMMON_MES_RESP("SUCCESS",Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_PlayerSkill_Insert_Resp,TagName.Status,TagFormat.TLV_STRING);

				}

			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.API_Error, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_PlayerSkill_Insert_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 删除玩家技能
		/// </summary>
		/// <returns></returns>
		public Message RayCity_PlayerSkill_Del()
		{
			int operateUserID = 0;
			string serverIP = null;
			string account = null;
			int characterID = 0;
			int skillID =  0;
			int result = -1;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_NyUserID).m_bValueBuffer);
				strut = new TLV_Structure(TagName.RayCity_SkillID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_SkillID).m_bValueBuffer);
				skillID =(int)strut.toInteger();	
				strut = new TLV_Structure(TagName.RayCity_CharacterID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_CharacterID).m_bValueBuffer);
				characterID =(int)strut.toInteger();			
				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.API_Delete+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_SkillInfo);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.API_Delete+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_SkillInfo);
				result = ItemShopInfo.PlayerSkill_Del(operateUserID,serverIP,account,characterID,skillID);	
				if(result==1)
				{
					
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_PlayerSkill_Delete_Resp,TagName.Status,TagFormat.TLV_STRING);
				}
				else
				{
	                return Message.COMMON_MES_RESP("SUCCESS",Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_PlayerSkill_Delete_Resp,TagName.Status,TagFormat.TLV_STRING);

				}

			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.API_Error, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_PlayerSkill_Delete_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
	}
}
