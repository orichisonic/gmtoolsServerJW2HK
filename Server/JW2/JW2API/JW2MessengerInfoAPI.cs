using System;
using System.Data;
using System.Text;
using GM_Server.JW2DataInfo;
using Common.Logic;
using Common.DataInfo;
using Domino;
using Common.NotesDataInfo;

namespace GM_Server.JW2API
{
	/// <summary>
	/// JW2MessengerInfoAPI 的摘要说明。
	/// </summary>
	public class JW2MessengerInfoAPI
	{
		Message msg = null;
		public JW2MessengerInfoAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 构造函数
		public JW2MessengerInfoAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
		}
		#endregion

		#region 查看玩家家族信息
		/// <summary>
		/// 查看玩家家族信息
		/// </summary>
		/// <returns></returns>
		public Message JW2_User_Family_Query()
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			int type =-1;
			int userSN = -1;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserSN).m_bValueBuffer);
				userSN =(int)strut.toInteger();
				
				SqlHelper.log.WriteLog("浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+userSN+"家族信息!");
				Console.WriteLine(DateTime.Now+" - 浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+userSN+"家族信息!");
				ds = JW2DataInfo.JW2MessengerDataInfo.User_Family_Query(serverIP,userSN);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,0,ds.Tables[0].Rows.Count,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_User_Family_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有该玩家家族信息",Msg_Category.JW2_ADMIN,ServiceKey.JW2_User_Family_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("查看玩家家族信息->JW2_User_Family_Query->服务器IP->"+serverIP+"->用户->"+userSN+"->"+ex.Message);
				return Message.COMMON_MES_RESP("没有该玩家家族信息", Msg_Category.JW2_ADMIN, ServiceKey.JW2_User_Family_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 查看家族信息
		/// <summary>
		/// 查看家族信息
		/// </summary>
		/// <returns></returns>
		public Message JW2_FAMILYINFO_QUERY(int index,int pageSize)
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			int type =-1;
			string FamilyName = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				FamilyName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_FAMILYNAME).m_bValueBuffer);
				
				SqlHelper.log.WriteLog("浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"家族："+FamilyName+"信息!");
				Console.WriteLine(DateTime.Now+" - 浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"家族："+FamilyName+"信息!");
				ds = JW2DataInfo.JW2MessengerDataInfo.FAMILYINFO_QUERY(serverIP,FamilyName);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_FAMILYINFO_QUERY_RESP,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有该家族信息",Msg_Category.JW2_ADMIN,ServiceKey.JW2_FAMILYINFO_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("查看家族信息->JW2_FAMILYINFO_QUERY->服务器IP->"+serverIP+"->家族->"+FamilyName+"->"+ex.Message);
				return Message.COMMON_MES_RESP("没有该家族信息", Msg_Category.JW2_ADMIN, ServiceKey.JW2_FAMILYINFO_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 查看家族宠物信息
		/// <summary>
		/// 查看家族宠物信息
		/// </summary>
		/// <returns></returns>
		public Message JW2_FamilyPet_Query()
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			int type =-1;
			int FamilyID = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_FAMILYID,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_FAMILYID).m_bValueBuffer);
				FamilyID =(int)strut.toInteger();
				
				SqlHelper.log.WriteLog("浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家家族"+FamilyID+"宠物信息!");
				Console.WriteLine(DateTime.Now+" - 浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家家族"+FamilyID+"宠物信息!");
				ds = JW2DataInfo.JW2MessengerDataInfo.FamilyPet_Query(serverIP,FamilyID);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,0,ds.Tables[0].Rows.Count,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_FamilyPet_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有家族宠物信息",Msg_Category.JW2_ADMIN,ServiceKey.JW2_FamilyPet_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("查看家族宠物信息->JW2_User_Family_Query->服务器IP->"+serverIP+"->家族ID->"+FamilyID+"->"+ex.Message);
				return Message.COMMON_MES_RESP("没有该家族宠物信息", Msg_Category.JW2_ADMIN, ServiceKey.JW2_FamilyPet_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 查看基地信息
		/// <summary>
		/// 查看基地信息
		/// </summary>
		/// <returns></returns>
		public Message JW2_BasicInfo_Query(int index,int pageSize)
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			int type =-1;
			int FamilyNameID = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_FAMILYID,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_FAMILYID).m_bValueBuffer);
				FamilyNameID =(int)strut.toInteger();
				
				
				SqlHelper.log.WriteLog("浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"基地ID："+FamilyNameID+"信息!");
				Console.WriteLine(DateTime.Now+" - 浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"基地ID："+FamilyNameID+"信息!");
				ds = JW2DataInfo.JW2MessengerDataInfo.BasicInfo_Query(serverIP,FamilyNameID);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_BasicInfo_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有该家族信息",Msg_Category.JW2_ADMIN,ServiceKey.JW2_BasicInfo_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("查看基地信息->JW2_BasicInfo_Query->服务器IP->"+serverIP+"->家族->"+FamilyNameID+"->"+ex.Message);
				return Message.COMMON_MES_RESP("没有该基地信息", Msg_Category.JW2_ADMIN, ServiceKey.JW2_BasicInfo_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 查看家族成员信息
		/// <summary>
		/// 查看家族成员信息
		/// </summary>
		/// <returns></returns>
		public Message JW2_FAMILYMEMBER_QUERY(int index,int pageSize)
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			int FamilyID = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_FAMILYID).m_bValueBuffer);
				FamilyID =(int)strut.toInteger();
				
				SqlHelper.log.WriteLog("浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"家族ID："+FamilyID+"成员信息!");
				Console.WriteLine(DateTime.Now+" - 浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"家族ID："+FamilyID+"成员信息!");
				ds = JW2DataInfo.JW2MessengerDataInfo.FAMILYMEMBER_QUERY(serverIP,FamilyID);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_FAMILYMEMBER_QUERY_RESP,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有该家族成员信息",Msg_Category.JW2_ADMIN,ServiceKey.JW2_FAMILYMEMBER_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("查看家族成员信息->JW2_FAMILYMEMBER_QUERY->服务器IP->"+serverIP+"->家族->"+FamilyID+"->"+ex.Message);
				return Message.COMMON_MES_RESP("没有该家族成员信息", Msg_Category.JW2_ADMIN, ServiceKey.JW2_FAMILYMEMBER_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 查看家族道具信息
		/// <summary>
		/// 查看家族道具信息
		/// </summary>
		/// <returns></returns>
		public Message JW2_FamilyItemInfo_Query(int index,int pageSize)
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			int FamilyID = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_FAMILYID).m_bValueBuffer);
				FamilyID =(int)strut.toInteger();
				
				SqlHelper.log.WriteLog("浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"家族ID："+FamilyID+"道具信息!");
				Console.WriteLine(DateTime.Now+" - 浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"家族ID："+FamilyID+"道具信息!");
				ds = JW2DataInfo.JW2MessengerDataInfo.FamilyItemInfo_Query(serverIP,FamilyID);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_FamilyItemInfo_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有该家族道具信息",Msg_Category.JW2_ADMIN,ServiceKey.JW2_FamilyItemInfo_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("查看家族道具信息->JW2_FamilyItemInfo_Query->服务器IP->"+serverIP+"->家族->"+FamilyID+"->"+ex.Message);
				return Message.COMMON_MES_RESP("没有该家族道具信息", Msg_Category.JW2_ADMIN, ServiceKey.JW2_FamilyItemInfo_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion
		
		#region 查看家族申请中成员信息
		/// <summary>
		/// 查看家族申请中成员信息
		/// </summary>
		/// <returns></returns>
		public Message JW2_FamilyMember_Applying(int index,int pageSize)
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			int FamilyID = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_FAMILYID).m_bValueBuffer);
				FamilyID =(int)strut.toInteger();
				
				SqlHelper.log.WriteLog("浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"家族ID："+FamilyID+"申请中成员信息!");
				Console.WriteLine(DateTime.Now+" - 浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"家族ID："+FamilyID+"申请中成员信息!");
				ds = JW2DataInfo.JW2MessengerDataInfo.FamilyMember_Applying(serverIP,FamilyID);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_FamilyMember_Applying_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有该家族申请中成员信息",Msg_Category.JW2_ADMIN,ServiceKey.JW2_FamilyMember_Applying_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("查看家族申请中成员信息->JW2_FamilyMember_Applying->服务器IP->"+serverIP+"->家族->"+FamilyID+"->"+ex.Message);
				return Message.COMMON_MES_RESP("没有该家族申请中成员信息", Msg_Category.JW2_ADMIN, ServiceKey.JW2_FamilyMember_Applying_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 查看家族排名信息
		/// <summary>
		/// 查看家族排名信息
		/// </summary>
		/// <returns></returns>
		public Message JW2_BasicBank_Query(int index,int pageSize)
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			int FamilyID = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_FAMILYID).m_bValueBuffer);
				FamilyID =(int)strut.toInteger();
				
				SqlHelper.log.WriteLog("浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"家族ID："+FamilyID+"排名信息!");
				Console.WriteLine(DateTime.Now+" - 浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"家族ID："+FamilyID+"排名信息!");
				ds = JW2DataInfo.JW2MessengerDataInfo.BasicRank_Query(serverIP,FamilyID);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_BasicBank_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有该家族排名信息",Msg_Category.JW2_ADMIN,ServiceKey.JW2_BasicBank_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("查看家族排名信息->JW2_BasicBank_Query->服务器IP->"+serverIP+"->家族->"+FamilyID+"->"+ex.Message);
				return Message.COMMON_MES_RESP("没有该家族排名信息", Msg_Category.JW2_ADMIN, ServiceKey.JW2_BasicBank_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 查看玩家Messenger称号信息
		/// <summary>
		/// 查看玩家Messenger称号信息
		/// </summary>
		/// <returns></returns>
		public Message JW2_Messenger_Query(int index,int pageSize)
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			int userSN = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserSN).m_bValueBuffer);
				userSN =(int)strut.toInteger();
				
				SqlHelper.log.WriteLog("浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家："+userSN+"Messenger称号信息!");
				Console.WriteLine(DateTime.Now+" - 浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家："+userSN+"Messenger称号信息!");
				ds = JW2DataInfo.JW2MessengerDataInfo.Messenger_Query(serverIP,userSN);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_Messenger_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有该玩家Messenger称号信息",Msg_Category.JW2_ADMIN,ServiceKey.JW2_Messenger_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("查看玩家Messenger称号信息->JW2_Messenger_Query->服务器IP->"+serverIP+"->用户->"+userSN+"->"+ex.Message);
				return Message.COMMON_MES_RESP("没有该玩家Messenger称号信息", Msg_Category.JW2_ADMIN, ServiceKey.JW2_Messenger_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 查看家族道具购买日志
		/// <summary>
		/// 查看家族道具购买日志
		/// </summary>
		/// <returns></returns>
		public Message JW2_FamilyBuyLog_Query(int index,int pageSize)
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			string BeginTime = "";
			string EndTime = "";
			int FamilyID = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_FAMILYID).m_bValueBuffer);
				FamilyID =(int)strut.toInteger();
				BeginTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.BeginTime).m_bValueBuffer);
				EndTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.EndTime).m_bValueBuffer);
				
				SqlHelper.log.WriteLog("浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"家族ID："+FamilyID+"道具购买日志!");
				Console.WriteLine(DateTime.Now+" - 浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"家族ID："+FamilyID+"道具购买日志!");
				ds = JW2DataInfo.JW2MessengerDataInfo.FamilyBuyLog_Query(serverIP,FamilyID,BeginTime,EndTime);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_FamilyBuyLog_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有该家族道具购买日志",Msg_Category.JW2_ADMIN,ServiceKey.JW2_FamilyBuyLog_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("查看家族道具购买日志->JW2_FamilyBuyLog_Query->服务器IP->"+serverIP+"->家族->"+FamilyID+"->开始时间->"+BeginTime+"->结束时间->"+EndTime+"->"+ex.Message);
				return Message.COMMON_MES_RESP("没有该家族道具购买日志", Msg_Category.JW2_ADMIN, ServiceKey.JW2_FamilyBuyLog_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 基金日志(0,捐赠,1,消费)
		/// <summary>
		/// 基金日志(0,捐赠,1,消费)
		/// </summary>
		/// <returns></returns>
		public Message JW2_FamilyFund_Log(int index,int pageSize)
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			string BeginTime = "";
			string EndTime = "";
			int type = -1;
			int FamilyID = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_FAMILYID).m_bValueBuffer);
				FamilyID =(int)strut.toInteger();
				BeginTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.BeginTime).m_bValueBuffer);
				EndTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.EndTime).m_bValueBuffer);
				strut = new TLV_Structure(TagName.JW2_GOODSTYPE,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_GOODSTYPE).m_bValueBuffer);
				type =(int)strut.toInteger();
				
				SqlHelper.log.WriteLog("浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"家族ID："+FamilyID+"基金购买日志!");
				Console.WriteLine(DateTime.Now+" - 浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"家族ID："+FamilyID+"基金购买日志!");
				ds = JW2DataInfo.JW2MessengerDataInfo.FamilyFund_Log(serverIP,FamilyID,BeginTime,EndTime,type);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_FamilyFund_Log_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有该家族基金购买日志",Msg_Category.JW2_ADMIN,ServiceKey.JW2_FamilyFund_Log_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("基金日志(0,捐赠,1,消费)->JW2_FamilyFund_Log->服务器IP->"+serverIP+"->家族->"+FamilyID+"->开始时间->"+BeginTime+"->结束时间->"+EndTime+"->"+ex.Message);
				return Message.COMMON_MES_RESP("没有该家族基金购买日志", Msg_Category.JW2_ADMIN, ServiceKey.JW2_FamilyFund_Log_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 家族成员领取小宠物蛋信息查询
		/// <summary>
		/// 家族成员领取小宠物蛋信息查询
		/// </summary>
		/// <returns></returns>
		public Message JW2_SmallPetAgg_Query()
		{
			string serverIP = "";
			DataSet ds = null;
			string BeginTime = "";
			string EndTime = "";
			int userSN = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserSN).m_bValueBuffer);
				userSN =(int)strut.toInteger();
				BeginTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.BeginTime).m_bValueBuffer);
				EndTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.EndTime).m_bValueBuffer);
				
				SqlHelper.log.WriteLog("浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"家族成员："+userSN+"领取小宠物蛋信息查询!");
				Console.WriteLine(DateTime.Now+" - 浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"家族成员："+userSN+"领取小宠物蛋信息查询!");
				ds = JW2DataInfo.JW2MessengerDataInfo.SmallPetAgg_Query(serverIP,userSN,BeginTime,EndTime);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,0,ds.Tables[0].Rows.Count,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_SmallPetAgg_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有该家族基金购买日志",Msg_Category.JW2_ADMIN,ServiceKey.JW2_SmallPetAgg_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("家族成员领取小宠物蛋信息查询->JW2_SmallPetAgg_Query->服务器IP->"+serverIP+"->家族成员->"+userSN+"->开始时间->"+BeginTime+"->结束时间->"+EndTime+"->"+ex.Message);
				return Message.COMMON_MES_RESP("家族成员领取小宠物蛋信息查询", Msg_Category.JW2_ADMIN, ServiceKey.JW2_SmallPetAgg_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion
		
		#region 道具日志
		/// <summary>
		/// 道具日志
		/// </summary>
		/// <returns></returns>
		public Message JW2_Item_Log(int index,int pageSize)
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			string BeginTime = "";
			string EndTime = "";
			int userSN = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserSN).m_bValueBuffer);
				userSN =(int)strut.toInteger();
				BeginTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.BeginTime).m_bValueBuffer);
				EndTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.EndTime).m_bValueBuffer);
				
				SqlHelper.log.WriteLog("浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"用戶ID："+userSN+"道具日志!");
				Console.WriteLine(DateTime.Now+" - 浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"用戶ID："+userSN+"道具日志!");
				ds = JW2DataInfo.JW2MessengerDataInfo.Item_Log(serverIP,userSN,BeginTime,EndTime);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_Item_Log_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有该用户道具日志",Msg_Category.JW2_ADMIN,ServiceKey.JW2_Item_Log_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("道具日志->JW2_Item_Log->服务器IP->"+serverIP+"->用户->"+userSN+"->开始时间->"+BeginTime+"->结束时间->"+EndTime+"->"+ex.Message);
				return Message.COMMON_MES_RESP("没有该用户道具日志", Msg_Category.JW2_ADMIN, ServiceKey.JW2_Item_Log_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 查看玩家纸条箱信息
		/// <summary>
		/// 查看玩家纸条箱信息
		/// </summary>
		/// <returns></returns>
		public Message JW2_MailInfo_Query(int index,int pageSize)
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			string EndTime = "";
			string BeginTime = "";
			int userSN = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				BeginTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.BeginTime).m_bValueBuffer);
				EndTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.EndTime).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserSN).m_bValueBuffer);
				userSN =(int)strut.toInteger();
				
				SqlHelper.log.WriteLog("浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家："+userSN+"纸条箱信息!");
				Console.WriteLine(DateTime.Now+" - 浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家："+userSN+"纸条箱信息!");
				ds = JW2DataInfo.JW2MessengerDataInfo.MailInfo_Query(serverIP,userSN,BeginTime,EndTime);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_MailInfo_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有该玩家纸条箱信息",Msg_Category.JW2_ADMIN,ServiceKey.JW2_MailInfo_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("查看玩家纸条箱信息->JW2_MailInfo_Query->服务器IP->"+serverIP+"->用户->"+userSN+"->开始时间->"+BeginTime+"->结束时间->"+EndTime+"->"+ex.Message);
				return Message.COMMON_MES_RESP("没有该玩家纸条箱信息", Msg_Category.JW2_ADMIN, ServiceKey.JW2_MailInfo_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 修改家族名
		/// <summary>
		/// 修改家族名
		/// </summary>
		/// <returns></returns>
		public Message JW2_UpdateFamilyName_Query()
		{
			string serverIP = "";
			int result = -1;
			int itemID = 0;
			int itemNo = 0;
			int type = -1;
			string itemName = "";
			string UserName = "";
			string OLD_FamilyName = "";
			string FamilyName = "";
			int familyID = 0;
			int userbyid = 0;
			try
			{
				//ip
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				//老家族
				OLD_FamilyName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_OLD_FAMILYNAME).m_bValueBuffer);
				//家族名
				FamilyName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_FAMILYNAME).m_bValueBuffer);
				//操作员id
				TLV_Structure strut = new TLV_Structure(TagName.Magic_PetID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userbyid =(int)strut.toInteger();
				//家族id
				strut = new TLV_Structure(TagName.Magic_PetID,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_FAMILYID).m_bValueBuffer);
				familyID =(int)strut.toInteger();

				SqlHelper.log.WriteLog("浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"修改家族名"+OLD_FamilyName+",为"+FamilyName);
				result = JW2DataInfo.JW2MessengerDataInfo.UpdateFamilyName_Query(serverIP,OLD_FamilyName,FamilyName,userbyid,familyID);
				if(result ==1)
				{
					Console.WriteLine(DateTime.Now+" - 浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"修改家族名"+OLD_FamilyName+",为"+FamilyName+"成功");
					return Message.COMMON_MES_RESP("SCUESS",Msg_Category.JW2_ADMIN,ServiceKey.JW2_UpDateFamilyName_Query_Resp);
				}
				else
				{
					Console.WriteLine(DateTime.Now+" - 浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"修改家族名"+OLD_FamilyName+",为"+FamilyName+"失败");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.JW2_ADMIN,ServiceKey.JW2_UpDateFamilyName_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("修改家族名->JW2_UpdateFamilyName_Query->服务器IP->"+serverIP+"->帐号->"+UserName+"->旧家族名"+OLD_FamilyName+"->家族名->"+FamilyName+"->"+ex.Message);
				return Message.COMMON_MES_RESP("修改家族名【" + OLD_FamilyName +"】为【" + FamilyName +"】失败，请确认该家族是否存在！", Msg_Category.JW2_ADMIN, ServiceKey.JW2_UpDateFamilyName_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion		

	}
}
