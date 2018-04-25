using System;
using System.Text;
using System.Data;
using Common.Logic;
using STONE.HU.HELPER.UTIL;
using Common.DataInfo;
using lg = Common.API.LanguageAPI;
using SDGOManageBrokerLib;
using System.Runtime.InteropServices;

using GM_Server.SDOnlineDataInfo;

namespace GM_Server.SDOnlineAPI
{
	/// <summary>
	/// SDLogInfoAPI 的摘要说明。
	/// </summary>
	
	public class SDLogInfoAPI
	{
		[DllImport("SDOManager.dll")]
		private static extern int ManageKickOut(int ServerGroup, int Account,byte[] NickName);

		Message msg = null;
		public static Log log = null;
		public SDLogInfoAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
			
		}
		public SDLogInfoAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}	
		#region lg.SDAPI_SDItemShopAPI_Integral+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG9+信息
		/// <summary>
		/// lg.SDAPI_SDItemShopAPI_Integral+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG9+信息
		/// </summary>
		/// <returns></returns>
		public Message SD_UserLoginfo_Query(int index,int pageSize)
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			string strnick = null;
			string username = null;
			DateTime BeginTime ;
			DateTime EndTime ;
			int userid = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();

				strut = new TLV_Structure(TagName.SD_StartTime, 6, msg.m_packet.m_Body.getTLVByTag(TagName.SD_StartTime).m_bValueBuffer);
				BeginTime  =strut.toTimeStamp();

				strut = new TLV_Structure(TagName.SD_EndTime, 6, msg.m_packet.m_Body.getTLVByTag(TagName.SD_EndTime).m_bValueBuffer);
				EndTime  =strut.toTimeStamp();

				
				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDItemMsG9+lg.SDAPI_SDChallengeDataAPI_ProbabilityList);
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDItemMsG9+lg.SDAPI_SDChallengeDataAPI_ProbabilityList);
				ds = SDLogDataInfo.UserLoginfo_Query(serverIP,userid,BeginTime,EndTime);		
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_UserLoginfo_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG9+lg.SDAPI_SDChallengeDataAPI_ProbabilityList,Msg_Category.SD_ADMIN,ServiceKey.SD_UserLoginfo_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG9+lg.SDAPI_SDChallengeDataAPI_ProbabilityList, Msg_Category.SD_ADMIN, ServiceKey.SD_UserLoginfo_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		#endregion

		#region lg.SDAPI_SDItemLogInfoAPI_Account+礼物\邮件lg.SDAPI_SDItemShopAPI_Integral+
		/// <summary>
		/// lg.SDAPI_SDItemLogInfoAPI_Account+礼物\邮件lg.SDAPI_SDItemShopAPI_Integral+
		/// </summary>
		/// <returns></returns>
		public Message SD_UserGrift_Query(int index,int pageSize)
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			int type =-1;
			string strnick = null;
			string username = null;
			int userid = 0;
			DateTime BeginTime;
			DateTime EndTime;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();

				strut = new TLV_Structure(TagName.SD_StartTime, 6, msg.m_packet.m_Body.getTLVByTag(TagName.SD_StartTime).m_bValueBuffer);
				BeginTime  =strut.toTimeStamp();

				strut = new TLV_Structure(TagName.SD_EndTime, 6, msg.m_packet.m_Body.getTLVByTag(TagName.SD_EndTime).m_bValueBuffer);
				EndTime  =strut.toTimeStamp();

				strut = new TLV_Structure(TagName.SD_Type, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SD_Type).m_bValueBuffer);
				type  =(byte)strut.toInteger();
				
				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDItemShopAPI_NoIntegral+lg.SDAPI_SDChallengeDataAPI_ProbabilityList);
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDItemShopAPI_NoIntegral+lg.SDAPI_SDChallengeDataAPI_ProbabilityList);
				ds = SDLogDataInfo.UserGrift_Query(serverIP,userid,BeginTime,EndTime,type);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_UserGrift_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemShopAPI_NoIntegral+lg.SDAPI_SDChallengeDataAPI_ProbabilityList,Msg_Category.SD_ADMIN,ServiceKey.SD_UserGrift_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemShopAPI_NoIntegral+lg.SDAPI_SDChallengeDataAPI_ProbabilityList, Msg_Category.SD_ADMIN, ServiceKey.SD_UserGrift_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		#endregion

		#region 用户踢下线
		/// <summary>
		/// 用户踢下线
		/// </summary>
		/// <returns></returns>
		public Message SD_KickUser_Query()
		{
			string serverIP = null;
			//int uid = 0;
			int result = -1;
			string strnick = null;
			string username = null;
			int UserByID = 0;
			int userid = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();
				//GM 操作员
				strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				UserByID =(int)strut.toInteger();

				strnick = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.f_pilot).m_bValueBuffer);
				byte[] bytNick = System.Text.Encoding.Unicode.GetBytes(strnick);
				int servergroup = int.Parse(CommonInfo.SD_GameDBInfo_Query(serverIP)[0].ToString());
				result = ManageKickOut(servergroup,10,bytNick);
				if(result ==0)
				{
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_KickUser_Query","用户"+username.ToString()+"，踢下线成功");
					Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+"踢用户"+username+"下线成功!");
					return Message.COMMON_MES_RESP("SCUESS",Msg_Category.SD_ADMIN,ServiceKey.SD_KickUser_Query_Resp);
				}
				else
				{
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_KickUser_Query","用户"+username.ToString()+"，踢下线失败");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SD_ADMIN,ServiceKey.SD_KickUser_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("操作失败", Msg_Category.SD_ADMIN, ServiceKey.SD_KickUser_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region lg.SDAPI_SDMemberInfoAPI_AccountLock+
		/// <summary>
		/// lg.SDAPI_SDMemberInfoAPI_AccountLock+
		/// </summary>
		/// <returns></returns>
		public Message SD_BanUser_Ban()
		{
			string serverIP = null;
			//int uid = 0;
			int result = -1;
			string strnick = null;
			string username = null;
			string content = null;
			string serverName = null;
			int UserByID = 0;
			DateTime EndTime;
			int userid = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				serverName = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerName).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				content = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_Content).m_bValueBuffer);

				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();
				//GM 操作员
				strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				UserByID =(int)strut.toInteger();

				strut = new TLV_Structure(TagName.SD_EndTime, 6, msg.m_packet.m_Body.getTLVByTag(TagName.SD_EndTime).m_bValueBuffer);
				EndTime  =strut.toTimeStamp();

				strnick = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.f_pilot).m_bValueBuffer);
				byte[] bytNick = System.Text.Encoding.Unicode.GetBytes(strnick);

				int servergroup = int.Parse(CommonInfo.SD_GameDBInfo_Query(serverIP)[0].ToString());
				result = ManageKickOut(servergroup,10,bytNick);
				if(result ==0)
				{
					result = SDLogDataInfo.BanUser_Ban(serverIP,serverName,UserByID,userid,username,content,EndTime);
					if(result ==1)
					{
						SqlHelper.log.WriteLog("封停--SD高达+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"封停lg.SDAPI_SDItemLogInfoAPI_Account+"+username+"到"+EndTime.ToString()+"，成功!");
						Console.WriteLine(DateTime.Now+" - 封停--SD高达+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"封停lg.SDAPI_SDItemLogInfoAPI_Account+"+username+"到"+EndTime.ToString()+"，成功!");
						return Message.COMMON_MES_RESP("SCUESS",Msg_Category.SD_ADMIN,ServiceKey.SD_BanUser_Ban_Resp);
					}
					else
					{
						return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SD_ADMIN,ServiceKey.SD_BanUser_Ban_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
					}
				}
				else
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SD_ADMIN,ServiceKey.SD_BanUser_Ban_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("操作失败", Msg_Category.SD_ADMIN, ServiceKey.SD_BanUser_Ban_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 解封用户
		/// <summary>
		/// 解封用户
		/// </summary>
		/// <returns></returns>
		public Message SD_BanUser_UnBan()
		{
			string serverIP = null;
			//int uid = 0;
			int result = 0;
			string strnick = null;
			string username = null;
			string content = null;
			string serverName = null;
			int UserByID = 0;
			DateTime EndTime;
			int userid = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				serverName = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerName).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				content = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_Content).m_bValueBuffer);

				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();
				//GM 操作员
				strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				UserByID =(int)strut.toInteger();

				result = SDLogDataInfo.BanUser_UnBan(serverIP,serverName,UserByID,userid,username);
				if(result ==1)
				{
					SqlHelper.log.WriteLog("解封--SD高达+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"解封用户"+username+"成功!");
					Console.WriteLine(DateTime.Now+" - 解封--SD高达+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"解封用户"+username+"成功!");
					return Message.COMMON_MES_RESP("SCUESS",Msg_Category.SD_ADMIN,ServiceKey.SD_BanUser_UnBan_Resp);
				}
				else
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SD_ADMIN,ServiceKey.SD_BanUser_UnBan_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
				
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("操作失败", Msg_Category.SD_ADMIN, ServiceKey.SD_BanUser_UnBan_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region lg.SDAPI_SDMemberInfoAPI_AccountLock+lg.SDAPI_SDItemShopAPI_Integral+
		/// <summary>
		/// lg.SDAPI_SDMemberInfoAPI_AccountLock+lg.SDAPI_SDItemShopAPI_Integral+
		/// </summary>
		/// <returns></returns>
		public Message SD_BanUser_Query(int index,int pageSize)
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			DateTime EndTime;
			string serverName = null;
			string userID = null;
			int type = -1;
			int userid = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				serverName = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerName).m_bValueBuffer);

				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_Type).m_bValueBuffer);
				type =(int)strut.toInteger();
				if(type==1)
					userID = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				else
					userID = "";
				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDMemberInfoAPI_AccountLock+lg.SDAPI_SDChallengeDataAPI_ProbabilityList);
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDMemberInfoAPI_AccountLock+lg.SDAPI_SDChallengeDataAPI_ProbabilityList);
				ds = SDLogDataInfo.BanUser_Query(serverIP,serverName,type,userID);		
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_BanUser_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDMemberInfoAPI_AccountLock+lg.SDAPI_SDChallengeDataAPI_ProbabilityList,Msg_Category.SD_ADMIN,ServiceKey.SD_BanUser_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDMemberInfoAPI_AccountLock+lg.SDAPI_SDChallengeDataAPI_ProbabilityList, Msg_Category.SD_ADMIN, ServiceKey.SD_BanUser_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion
	
		#region 修改lg.SDAPI_SDItemMsG6+
		/// <summary>
		/// 修改lg.SDAPI_SDItemMsG6+
		/// </summary>
		/// <returns></returns>
		public Message SD_TmpPassWord_Query()
		{
			string serverIP = null;
			//int uid = 0;
			int result = 0;
			string strnick = null;
			string username = null;
			string content = null;
			string serverName = null;
			string TmpPWD = null;
			int UserByID = 0;
			DateTime EndTime;
			int userid = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				serverName = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerName).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				TmpPWD = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_TmpPWD).m_bValueBuffer);

				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();
				//GM 操作员
				strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				UserByID =(int)strut.toInteger();

				result = SDLogDataInfo.TmpPassWord_Query(serverIP,serverName,UserByID,userid,username,TmpPWD);
				if(result ==1)
				{
					SqlHelper.log.WriteLog("修改密码--SD高达+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"修改用户"+username+"lg.SDAPI_SDItemMsG6+成功!");
					Console.WriteLine(DateTime.Now+" - 修改密码-SD高达+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"修改用户"+username+"lg.SDAPI_SDItemMsG6+成功!");
					return Message.COMMON_MES_RESP("SCUESS",Msg_Category.SD_ADMIN,ServiceKey.SD_TmpPassWord_Query_Resp);
				}
				else if(result == 2)
				{
					return Message.COMMON_MES_RESP("已经修改过lg.SDAPI_SDItemMsG6+，请恢复再修改！",Msg_Category.SD_ADMIN,ServiceKey.SD_TmpPassWord_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
				else
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SD_ADMIN,ServiceKey.SD_TmpPassWord_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
				
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("操作失败", Msg_Category.SD_ADMIN, ServiceKey.SD_TmpPassWord_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 恢复lg.SDAPI_SDItemMsG6+
		/// <summary>
		/// 恢复lg.SDAPI_SDItemMsG6+
		/// </summary>
		/// <returns></returns>
		public Message SD_ReTmpPassWord_Query()
		{
			string serverIP = null;
			//int uid = 0;
			int result = 0;
			string strnick = null;
			string username = null;
			string content = null;
			string serverName = null;
			string TmpPWD = null;
			int UserByID = 0;
			DateTime EndTime;
			int userid = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				serverName = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerName).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);

				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();
				//GM 操作员
				strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				UserByID =(int)strut.toInteger();

				result = SDLogDataInfo.ReTmpPassWord_Query(serverIP,serverName,UserByID,userid,username);
				if(result ==1)
				{
					SqlHelper.log.WriteLog("恢复密码--SD高达+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"恢复用户"+username+"lg.SDAPI_SDItemMsG6+成功!");
					Console.WriteLine(DateTime.Now+" - 恢复密码--SD高达+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"恢复用户"+username+"lg.SDAPI_SDItemMsG6+成功!");
					return Message.COMMON_MES_RESP("SCUESS",Msg_Category.SD_ADMIN,ServiceKey.SD_ReTmpPassWord_Query_Resp);
				}
				else if(result ==2)
				{
					return Message.COMMON_MES_RESP("用户没有设置lg.SDAPI_SDItemMsG6+",Msg_Category.SD_ADMIN,ServiceKey.SD_ReTmpPassWord_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
				else
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SD_ADMIN,ServiceKey.SD_ReTmpPassWord_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
				
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("操作失败", Msg_Category.SD_ADMIN, ServiceKey.SD_ReTmpPassWord_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region lg.SDAPI_SDItemShopAPI_Integral+最后一次lg.SDAPI_SDItemMsG6+
		/// <summary>
		/// lg.SDAPI_SDItemShopAPI_Integral+最后一次lg.SDAPI_SDItemMsG6+
		/// </summary>
		/// <returns></returns>
		public Message SD_SearchPassWord_Query()
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			string username = null;
			string serverName = null;
			int userid = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				serverName = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerName).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();

				SqlHelper.log.WriteLog("浏览SD高达+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"lg.SDAPI_SDItemShopAPI_Integral+最后一次lg.SDAPI_SDItemMsG6+!");
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+"lg.SDAPI_SDItemShopAPI_Integral+最后一次lg.SDAPI_SDItemMsG6+!");
				ds = SDLogDataInfo.SearchPassWord_Query(serverIP,serverName,userid,username);		
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,ds.Tables[0].Rows.Count,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_SearchPassWord_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDItemMsG6,Msg_Category.SD_ADMIN,ServiceKey.SD_SearchPassWord_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDItemMsG6, Msg_Category.SD_ADMIN, ServiceKey.SD_SearchPassWord_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 修改lg.SDAPI_SDItemLogInfoAPI_Account+等级
		/// <summary>
		/// 修改lg.SDAPI_SDItemLogInfoAPI_Account+等级
		/// </summary>
		/// <returns></returns>
		public Message SD_UpdateExp_Query()
		{
			string serverIP = null;
			//int uid = 0;
			int result = 0;
			string strnick = null;
			string username = null;
			string content = null;
			string serverName = null;
			string TmpPWD = null;
			int UserByID = 0;
			int level = 0;
			DateTime EndTime;
			int userid = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);

				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();
				//GM 操作员
				strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				UserByID =(int)strut.toInteger();

				//LEVEL
				strut = new TLV_Structure(TagName.f_level,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_level).m_bValueBuffer);
				level =(int)strut.toInteger();


				result = SDLogDataInfo.UpdateExp_Query(serverIP,UserByID,userid,username,level);
				if(result ==1)
				{
					SqlHelper.log.WriteLog("修改等级--SD高达+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"修改用户"+username+"等级为"+level.ToString()+"，成功!");
					Console.WriteLine(DateTime.Now+" - 修改等级--SD高达+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"修改用户"+username+"等级为"+level.ToString()+"，成功!");
					return Message.COMMON_MES_RESP("SCUESS",Msg_Category.SD_ADMIN,ServiceKey.SD_UpdateExp_Query_Resp);
				}
				else
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SD_ADMIN,ServiceKey.SD_UpdateExp_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
				
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("操作失败", Msg_Category.SD_ADMIN, ServiceKey.SD_UpdateExp_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 修改lg.SDAPI_SDItemLogInfoAPI_Account+机体等级
		/// <summary>
		/// 修改lg.SDAPI_SDItemLogInfoAPI_Account+机体等级
		/// </summary>
		/// <returns></returns>
		public Message SD_UpdateUnitsExp_Query()
		{
			string serverIP = null;
			//int uid = 0;
			int result = 0;
			string strnick = null;
			string username = null;
			string content = null;
			string serverName = null;
			string TmpPWD = null;
			int UserByID = 0;
			int level = 0;
			int CustomLvMax = 0;
			int UnitNumber = 0;
			DateTime EndTime;
			string str = null;
			int userid = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);

				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();
				//GM 操作员
				strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				UserByID =(int)strut.toInteger();

				//LEVEL
				strut = new TLV_Structure(TagName.f_level,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_ItemID).m_bValueBuffer);
				UnitNumber =(int)strut.toInteger();


				//机体等级
				strut = new TLV_Structure(TagName.f_level,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_UnitLevelNumber).m_bValueBuffer);
				level =(int)strut.toInteger();

				//强化等级
				strut = new TLV_Structure(TagName.f_level,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_CustomLvMax).m_bValueBuffer);
				CustomLvMax =(int)strut.toInteger();

				str = SDLogDataInfo.UpdateUnitsExp_Query(serverIP,UserByID,userid,username,level,CustomLvMax,UnitNumber);
					SqlHelper.log.WriteLog("修改机体等级--SD高达+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"修改用户"+username+"机体等级为"+level.ToString()+"，成功!");
					Console.WriteLine(DateTime.Now+" - 修改机体等级--SD高达+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"修改用户"+username+"机体等级为"+level.ToString()+"，成功!");
					return Message.COMMON_MES_RESP(str,Msg_Category.SD_ADMIN,ServiceKey.SD_UpdateUnitsExp_Query_Resp);
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("操作失败", Msg_Category.SD_ADMIN, ServiceKey.SD_UpdateUnitsExp_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 添加道具
		/// <summary>
		/// 添加道具
		/// </summary>
		/// <returns></returns>
		public Message SD_UserAdditem_Add()
		{
			string serverIP = null;
			//int uid = 0;
			int result = 0;
			string strnick = null;
			string username = null;
			string content = null;
			string serverName = null;
			string ItemName = null;
			string sendUser = null;
			int UserByID = 0;
			DateTime EndTime;
			int userid = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);

				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();
				//GM 操作员
				strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				UserByID =(int)strut.toInteger();
				//itemname
				ItemName = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ItemName).m_bValueBuffer);

				sendUser = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_Title).m_bValueBuffer);
				content  = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_Content).m_bValueBuffer);
				strnick = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.f_pilot).m_bValueBuffer);
				result = SDLogDataInfo.UserAdditem_Add(serverIP,UserByID,userid,strnick,ItemName,sendUser,content);
				if(result !=-1)
				{
					SqlHelper.log.WriteLog("添加道具--SD高达+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"添加道具："+ItemName.ToString()+"，成功!");
					Console.WriteLine(DateTime.Now+" - 添加道具--SD高达+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"添加道具："+ItemName.ToString()+"，成功!");
					return Message.COMMON_MES_RESP("SCUESS",Msg_Category.SD_ADMIN,ServiceKey.SD_UserAdditem_Add_Resp);
				}
				else
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SD_ADMIN,ServiceKey.SD_UserAdditem_Add_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("操作失败", Msg_Category.SD_ADMIN, ServiceKey.SD_UserAdditem_Add_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 添加道具（批量）
		/// <summary>
		/// 添加道具（批量）
		/// </summary>
		/// <returns></returns>
		public Message SD_UserAdditem_Add_All()
		{
			string serverIP = null;
			//int uid = 0;
			string result = null;
			string strnick = null;
			string username = null;
			string content = null;
			string serverName = null;
			string ItemName = null;
			int UserByID = 0;
			DateTime EndTime;
			int userid = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);

				//GM 操作员
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				UserByID =(int)strut.toInteger();
				//itemname
				ItemName = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ItemName).m_bValueBuffer);

				result = SDLogDataInfo.UserAdditem_Add_All(serverIP,UserByID,ItemName);
				SqlHelper.log.WriteLog("添加道具--SD高达+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"添加道具!");
				Console.WriteLine(DateTime.Now+" - 添加道具--SD高达+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"添加道具!");
				Console.WriteLine(result);
				return Message.COMMON_MES_RESP("SCUESS",Msg_Category.SD_ADMIN,ServiceKey.SD_UserAdditem_Add_All_Resp);
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("操作失败", Msg_Category.SD_ADMIN, ServiceKey.SD_UserAdditem_Add_All_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion
		
		#region 获得lg.SDAPI_SDItemMsG7+
		/// <summary>
		/// 获得lg.SDAPI_SDItemMsG7+
		/// </summary>
		/// <returns></returns>
		public Message SD_GetItemList_Query()
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			int type = 0;
			string username = null;
			string serverName = null;
			string ItemName = "";
			int userid = 0;
			try
			{
				TLV_Structure tlvStrut = new TLV_Structure(TagName.UserByID,3,msg.m_packet.m_Body.getTLVByTag(TagName.SD_Type).m_bValueBuffer);
				type =(int)tlvStrut.toInteger();

				if(type==1)
				{
					ItemName = "";
				}
				else
				{
					ItemName = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ItemName).m_bValueBuffer);
				}
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);

				SqlHelper.log.WriteLog("浏览SD高达+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"获得lg.SDAPI_SDItemMsG7+!");
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+"获得lg.SDAPI_SDItemMsG7+!");
				ds = SDLogDataInfo.GetItemList_Query(serverIP,type,ItemName);		
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,ds.Tables[0].Rows.Count,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_ItemList_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDItemMsG7,Msg_Category.SD_ADMIN,ServiceKey.SD_ItemList_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDItemMsG7, Msg_Category.SD_ADMIN, ServiceKey.SD_ItemList_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 发送人礼物信息lg.SDAPI_SDItemShopAPI_Integral+
		/// <summary>
		/// 发送人礼物信息lg.SDAPI_SDItemShopAPI_Integral+
		/// </summary>
		/// <returns></returns>
		public Message SD_Grift_FromUser_Query(int index,int pageSize)
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			string  username = null;
			int  fromidx = 0;
			int  toidx = 0;
			DateTime Time;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);

				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_FromUser).m_bValueBuffer);
				//用户
				TLV_Structure strut = new TLV_Structure(TagName.SD_FromIdx,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_FromIdx).m_bValueBuffer);
				fromidx =(int)strut.toInteger();

				//用户
				strut = new TLV_Structure(TagName.SD_ToIdx,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_ToIdx).m_bValueBuffer);
				toidx =(int)strut.toInteger();

				strut = new TLV_Structure(TagName.SD_SendTime, 6, msg.m_packet.m_Body.getTLVByTag(TagName.SD_SendTime).m_bValueBuffer);
				Time  =strut.toTimeStamp();
				
				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+"发送人"+username+"礼物信息lg.SDAPI_SDItemShopAPI_Integral+!");
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+"发送人"+username+"礼物信息lg.SDAPI_SDItemShopAPI_Integral+!");
				ds = SDLogDataInfo.Grift_FromUser_Query(serverIP,toidx,fromidx,Time);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_Grift_FromUser_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemShopAPI_Integral,Msg_Category.SD_ADMIN,ServiceKey.SD_Grift_FromUser_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemShopAPI_Integral, Msg_Category.SD_ADMIN, ServiceKey.SD_Grift_FromUser_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		#endregion

		#region 接收人礼物信息lg.SDAPI_SDItemShopAPI_Integral+
		/// <summary>
		/// 接收人礼物信息lg.SDAPI_SDItemShopAPI_Integral+
		/// </summary>
		/// <returns></returns>
		public Message SD_Grift_ToUser_Query(int index,int pageSize)
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			int s_id = -1;
			string  username = null;
			string  item = null;
			int  toidx = 0;
			DateTime Time;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ToUser).m_bValueBuffer);
				//用户
				item = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ItemName).m_bValueBuffer);

				//用户
				TLV_Structure strut = new TLV_Structure(TagName.SD_ToIdx,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_ToIdx).m_bValueBuffer);
				toidx =(int)strut.toInteger();

				//数据ID
				strut = new TLV_Structure(TagName.SD_ToIdx,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_ID).m_bValueBuffer);
				s_id =(int)strut.toInteger();

				strut = new TLV_Structure(TagName.SD_SendTime, 6, msg.m_packet.m_Body.getTLVByTag(TagName.SD_SendTime).m_bValueBuffer);
				Time  =strut.toTimeStamp();
				
				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+"接受人"+username+"礼物信息lg.SDAPI_SDItemShopAPI_Integral+!");
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+"接受人"+username+"礼物信息lg.SDAPI_SDItemShopAPI_Integral+!");
				ds = SDLogDataInfo.Grift_ToUser_Query(serverIP,toidx,item,Time,s_id);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_Grift_ToUser_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemShopAPI_Integral,Msg_Category.SD_ADMIN,ServiceKey.SD_Grift_ToUser_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemShopAPI_Integral, Msg_Category.SD_ADMIN, ServiceKey.SD_Grift_ToUser_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		#endregion
		
		#region 删除道具
		/// <summary>
		/// 删除道具
		/// </summary>
		/// <returns></returns>
		public Message SD_UserAdditem_Del()
		{
			string serverIP = null;
			//int uid = 0;
			int result = -1;
			int type=0;
			string strnick = null;
			string username = null;
			string content = null;
			string serverName = null;
			string ItemName = null;
			int UserByID = 0;
			int itemID = 0;
			DateTime EndTime;
			int userid = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);

				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();
				//GM 操作员
				strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				UserByID =(int)strut.toInteger();

				//itemid
				strut = new TLV_Structure(TagName.SD_ItemID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_ID).m_bValueBuffer);
				itemID =(int)strut.toLong();

				//itemid
				strut = new TLV_Structure(TagName.SD_ItemID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_Type).m_bValueBuffer);
				type =(int)strut.toInteger();

				//itemname
				ItemName = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ItemName).m_bValueBuffer);

				result = SDLogDataInfo.UserAdditem_Del(serverIP,UserByID,userid,username,itemID,ItemName,type);
				if(result ==0)
				{
					SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+"删除道具："+ItemName.ToString()+"，成功!");
					Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+"删除道具："+ItemName.ToString()+"，成功!");
					return Message.COMMON_MES_RESP("SCUESS",Msg_Category.SD_ADMIN,ServiceKey.SD_UserAdditem_Del_Resp);
				}
				else
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SD_ADMIN,ServiceKey.SD_UserAdditem_Del_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("操作失败", Msg_Category.SD_ADMIN, ServiceKey.SD_UserAdditem_Del_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 发送公告
		public Message SD_SendNotes_Query()
		{
			string serverIP = null;
			string gsseverIP = null;
			string boardMessage = null;
			DateTime beginTime;
			DateTime endTime;
			int userbyID = 0;
			int interval = 0;
			int noticeType = 0;
			int Type = 0;
			int result = -1;
			try
			{
				serverIP = System.Text.Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				//操作员ID
				TLV_Structure tlvStrut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userbyID =(int)tlvStrut.toInteger();
				tlvStrut = new TLV_Structure(TagName.AU_BeginTime,6,msg.m_packet.m_Body.getTLVByTag(TagName.SD_StartTime).m_bValueBuffer);
				beginTime =tlvStrut.toTimeStamp();
				tlvStrut = new TLV_Structure(TagName.AU_EndTime,6,msg.m_packet.m_Body.getTLVByTag(TagName.SD_EndTime).m_bValueBuffer);
				endTime =tlvStrut.toTimeStamp();
				boardMessage = System.Text.Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_Content).m_bValueBuffer);
				//发送间隔
				tlvStrut = new TLV_Structure(TagName.AU_Interval,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_Limit).m_bValueBuffer);
				interval =(int)tlvStrut.toInteger();

				//公告类型
				tlvStrut = new TLV_Structure(TagName.AU_Interval,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_Type).m_bValueBuffer);
				noticeType =(int)tlvStrut.toInteger();

				//发送类型
				tlvStrut = new TLV_Structure(TagName.AU_Interval,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_SendType).m_bValueBuffer);
				Type =(int)tlvStrut.toInteger();
				
				result = SDLogDataInfo.BoardTask_Insert(userbyID,serverIP,boardMessage,beginTime,endTime,interval,noticeType,Type);
				if(result ==1)
				{
					SqlHelper.log.WriteLog("添加SD敢达+>内容为"+boardMessage.ToString()+"的公告成功!");
					Console.WriteLine(DateTime.Now+" - 添加SD敢达+>内容为"+boardMessage.ToString()+"的公告成功!");
					return Message.COMMON_MES_RESP("SCUESS",Msg_Category.SD_ADMIN,ServiceKey.SD_SendNotes_Query_Resp);
				}
				else
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SD_ADMIN,ServiceKey.SD_SendNotes_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("添加失敗",Msg_Category.SD_ADMIN,ServiceKey.SD_SendNotes_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
            
		}
		#endregion

		#region lg.SDAPI_SDItemShopAPI_Integral+公告
		public Message SD_SeacrhNotes_Query()
		{
			System.Data.DataSet ds = null;
			try
			{
				SqlHelper.log.WriteLog("添加SD敢达+>lg.SDAPI_SDItemShopAPI_Integral+公告列表!");
				Console.WriteLine(DateTime.Now+" - 添加SD敢达+>lg.SDAPI_SDItemShopAPI_Integral+公告列表");
				//从数据库里面将频道列表读出来
				ds = SDLogDataInfo.BoardTask_Query();
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,ds.Tables[0].Rows.Count,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_BanUser_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_ProbabilityList,Msg_Category.SD_ADMIN,ServiceKey.SD_BanUser_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+"SD_SeacrhNotes_Query"+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_ProbabilityList, Msg_Category.SD_ADMIN, ServiceKey.SD_BanUser_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
            
		}
		#endregion 

		#region 修改公告
		public Message SD_TaskList_Update()
		{
			string serverIP = "";
			int userbyID = 0;
			int taskID = 0;
			DateTime beginTime = DateTime.Now;
			DateTime endTime = DateTime.Now;
			int interval = 0;
			int status = 0;
			int noticeType = 0;
			string boardMessage = "";
			int result = -1;
			try
			{
				//操作员ID
				TLV_Structure tlvStrut = new TLV_Structure(TagName.UserByID,3,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userbyID =(int)tlvStrut.toInteger();
				//发送状态
				tlvStrut = new TLV_Structure(TagName.AU_Status,3,msg.m_packet.m_Body.getTLVByTag(TagName.SD_Status).m_bValueBuffer);
				status =(int)tlvStrut.toInteger();
				//任务ID
				tlvStrut = new TLV_Structure(TagName.AU_TaskID,3,msg.m_packet.m_Body.getTLVByTag(TagName.SD_ID).m_bValueBuffer);
				taskID =(int)tlvStrut.toInteger();

				boardMessage  = System.Text.Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_Content).m_bValueBuffer);
				if(status==0)
				{
					tlvStrut = new TLV_Structure(TagName.AU_BeginTime,6,msg.m_packet.m_Body.getTLVByTag(TagName.SD_StartTime).m_bValueBuffer);
					beginTime =tlvStrut.toTimeStamp();

					tlvStrut = new TLV_Structure(TagName.AU_EndTime,6,msg.m_packet.m_Body.getTLVByTag(TagName.SD_EndTime).m_bValueBuffer);
					endTime =tlvStrut.toTimeStamp();

					//发送间隔
					tlvStrut = new TLV_Structure(TagName.AU_Interval,3,msg.m_packet.m_Body.getTLVByTag(TagName.SD_Limit).m_bValueBuffer);
					interval =(int)tlvStrut.toInteger();

					boardMessage  = System.Text.Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_Content).m_bValueBuffer);

					//公告类型
					tlvStrut = new TLV_Structure(TagName.AU_TaskID,3,msg.m_packet.m_Body.getTLVByTag(TagName.SD_Type).m_bValueBuffer);
					noticeType =(int)tlvStrut.toInteger();
				}
				

				result = SDLogDataInfo.BoardTask_Update(serverIP,userbyID,taskID,beginTime,endTime,interval,noticeType,status,boardMessage);
				if(result ==1)
				{
					SqlHelper.log.WriteLog("更新SD敢达+>ID为"+taskID+"的公告成功!");
					Console.WriteLine(DateTime.Now+" - 更新SD敢达+>ID为"+taskID+"的公告成功!");
					return Message.COMMON_MES_RESP("SCUESS",Msg_Category.SD_ADMIN,ServiceKey.SD_TaskList_Update_Resp);
				}
				else
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SD_ADMIN,ServiceKey.SD_TaskList_Update_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("更新失敗",Msg_Category.SD_ADMIN,ServiceKey.SD_TaskList_Update_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region lg.SDAPI_SDItemShopAPI_Integral+lg.SDAPI_SDItemLogInfoAPI_Account+购买记录
		/// <summary>
		/// lg.SDAPI_SDItemShopAPI_Integral+lg.SDAPI_SDItemLogInfoAPI_Account+购买记录
		/// </summary>
		/// <returns></returns>
		public Message SD_BuyLog_Query(int index,int pageSize)
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			string strnick = null;
			string username = null;
			DateTime BeginTime ;
			DateTime EndTime ;
			int userid = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();

				strut = new TLV_Structure(TagName.SD_StartTime, 6, msg.m_packet.m_Body.getTLVByTag(TagName.SD_StartTime).m_bValueBuffer);
				BeginTime  =strut.toTimeStamp();

				strut = new TLV_Structure(TagName.SD_EndTime, 6, msg.m_packet.m_Body.getTLVByTag(TagName.SD_EndTime).m_bValueBuffer);
				EndTime  =strut.toTimeStamp();


				
				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+"购买记录lg.SDAPI_SDItemShopAPI_Integral+!");
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+"购买记录lg.SDAPI_SDItemShopAPI_Integral+!");
				ds = SDLogDataInfo.BuyLog_Query(serverIP,userid,BeginTime,EndTime);		
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_BuyLog_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemShopAPI_Integral,Msg_Category.SD_ADMIN,ServiceKey.SD_BuyLog_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemShopAPI_Integral, Msg_Category.SD_ADMIN, ServiceKey.SD_BuyLog_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		#endregion

		#region lg.SDAPI_SDItemShopAPI_Integral+道具删除记录
		/// <summary>
		/// lg.SDAPI_SDItemShopAPI_Integral+道具删除记录
		/// </summary>
		/// <returns></returns>
		public Message SD_Delete_ItemLog_Query(int index,int pageSize)
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			string strnick = null;
			string username = null;
			DateTime BeginTime ;
			DateTime EndTime ;
			int type = 0;
			int userid = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();
				//道具类型
				strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_Type).m_bValueBuffer);
				type =(int)strut.toInteger();

				strut = new TLV_Structure(TagName.SD_StartTime, 6, msg.m_packet.m_Body.getTLVByTag(TagName.SD_StartTime).m_bValueBuffer);
				BeginTime  =strut.toTimeStamp();

				strut = new TLV_Structure(TagName.SD_EndTime, 6, msg.m_packet.m_Body.getTLVByTag(TagName.SD_EndTime).m_bValueBuffer);
				EndTime  =strut.toTimeStamp();

				
				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+"道具删除记录!");
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+"道具删除记录!");
				ds = SDLogDataInfo.Delete_ItemLog_Query(serverIP,userid,type,BeginTime,EndTime);		
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_Delete_ItemLog_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+"道具删除记录",Msg_Category.SD_ADMIN,ServiceKey.SD_Delete_ItemLog_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+"道具删除记录", Msg_Category.SD_ADMIN, ServiceKey.SD_Delete_ItemLog_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		#endregion

		#region lg.SDAPI_SDItemShopAPI_Integral+lg.SDAPI_SDItemLogInfoAPI_Account+日志信息
		/// <summary>
		/// lg.SDAPI_SDItemShopAPI_Integral+lg.SDAPI_SDItemLogInfoAPI_Account+日志信息
		/// </summary>
		/// <returns></returns>
		public Message SD_LogInfo_Query(int index,int pageSize)
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			string strnick = null;
			string username = null;
			DateTime BeginTime ;
			DateTime EndTime ;
			int type = 0;
			int userid = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();
				//道具类型
				strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_Type).m_bValueBuffer);
				type =(int)strut.toInteger();

				strut = new TLV_Structure(TagName.SD_StartTime, 6, msg.m_packet.m_Body.getTLVByTag(TagName.SD_StartTime).m_bValueBuffer);
				BeginTime  =strut.toTimeStamp();

				strut = new TLV_Structure(TagName.SD_EndTime, 6, msg.m_packet.m_Body.getTLVByTag(TagName.SD_EndTime).m_bValueBuffer);
				EndTime  =strut.toTimeStamp();

				
				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+"日志记录lg.SDAPI_SDItemShopAPI_Integral+!");
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+"日志记录lg.SDAPI_SDItemShopAPI_Integral+!");
				ds = SDLogDataInfo.LogInfo_Query(serverIP,userid,type,BeginTime,EndTime);		
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_LogInfo_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemShopAPI_Integral,Msg_Category.SD_ADMIN,ServiceKey.SD_LogInfo_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemShopAPI_Integral, Msg_Category.SD_ADMIN, ServiceKey.SD_LogInfo_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		#endregion

		#region 获得GM账号
		/// <summary>
		/// 获得GM账号
		/// </summary>
		/// <returns></returns>
		public Message SD_GetGmAccount_Query()
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			int type = 0;
			string username = null;
			string serverName = null;
			string userName = "";
			int userid = 0;
			try
			{
				TLV_Structure tlvStrut = new TLV_Structure(TagName.UserByID,3,msg.m_packet.m_Body.getTLVByTag(TagName.SD_Type).m_bValueBuffer);
				type =(int)tlvStrut.toInteger();

				if(type==1)
				{
					userName = "";
				}
				else
				{
					userName = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				}
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);

				SqlHelper.log.WriteLog("浏览SD高达+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"获得GM账号列表!");
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+"获得GM账号列表!");
				ds = SDLogDataInfo.GetGmAccount_Query(serverIP,type,userName);		
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,ds.Tables[0].Rows.Count,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_GetGmAccount_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+"GM账号列表",Msg_Category.SD_ADMIN,ServiceKey.SD_GetGmAccount_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+"GM账号列表", Msg_Category.SD_ADMIN, ServiceKey.SD_GetGmAccount_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 修改GM账号
		/// <summary>
		/// 修改GM账号
		/// </summary>
		/// <returns></returns>
		public Message SD_UpdateGmAccount_Query()
		{
			string serverIP = null;
			//int uid = 0;
			int result = -1;
			int type=0;
			string strnick = null;
			string username = null;
			string oldUserName = null;
			string content = null;
			string serverName = null;
			string passWd = null;
			int UserByID = 0;
			int itemID = 0;
			string pilotName = null;
			DateTime EndTime;
			int userid = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				oldUserName = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName_Old).m_bValueBuffer);
				pilotName = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.f_pilot).m_bValueBuffer);

				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();

				//GM 操作员
				strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				UserByID =(int)strut.toInteger();

				//itemname
				passWd = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_passWd).m_bValueBuffer);

				result = SDLogDataInfo.UpdateGmAccount_Query(serverIP,UserByID,userid,username,passWd,oldUserName,pilotName);
				if(result ==1)
				{
					SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+"修改GM账号，成功!");
					Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+"修改GM账号，成功!");
					return Message.COMMON_MES_RESP("SCUESS",Msg_Category.SD_ADMIN,ServiceKey.SD_UpdateGmAccount_Query_Resp);
				}
				
				else
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SD_ADMIN,ServiceKey.SD_UpdateGmAccount_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("操作失败", Msg_Category.SD_ADMIN, ServiceKey.SD_UpdateGmAccount_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 修改金钱
		/// <summary>
		/// 修改金钱
		/// </summary>
		/// <returns></returns>
		public Message SD_UpdateMoney_Query()
		{
			string serverIP = null;
			//int uid = 0;
			int result = -1;
			int type=0;
			string strnick = null;
			string username = null;
			int Money = 0;
			int oldMoney = 0;
			string content = null;
			string serverName = null;
			string passWd = null;
			int UserByID = 0;
			int itemID = 0;
			DateTime EndTime;
			int userid = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				

				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();

				//GM 操作员
				strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				UserByID =(int)strut.toInteger();

				//钱（修改之前）
				strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_GC).m_bValueBuffer);
				Money =(int)strut.toInteger();

				//钱（修改之后）
				strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_Money_Old).m_bValueBuffer);
				oldMoney =(int)strut.toInteger();


				result = SDLogDataInfo.UpdateMoney_Query(serverIP,UserByID,userid,username,Money,oldMoney);
				if(result ==1)
				{
					SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+"修改账号金钱，成功!");
					Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+"修改账号金钱，成功!");
					return Message.COMMON_MES_RESP("SCUESS",Msg_Category.SD_ADMIN,ServiceKey.SD_UpdateMoney_Query_Resp);
				}
				else
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.SD_ADMIN,ServiceKey.SD_UpdateMoney_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("操作失败", Msg_Category.SD_ADMIN, ServiceKey.SD_UpdateMoney_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion
		
		#region 恢复机体
		/// <summary>
		/// 恢复机体
		/// </summary>
		/// <returns></returns>
		public Message SD_ReGetUnits_Query()
		{
			string serverIP = null;
			//int uid = 0;
			string result = null;
			string username = null;
			string beginDate = null;
			int UserByID = 0;
			int SDID = 0;
			int itemID = 0;
			string itemName = null;
			int userid = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				itemName = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ItemName).m_bValueBuffer);
				beginDate = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.BeginTime).m_bValueBuffer);

				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();

				//GM 操作员
				strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				UserByID =(int)strut.toInteger();

				//序列号
				strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_ID).m_bValueBuffer);
				SDID =(int)strut.toInteger();

				//SD 机体ID
				strut = new TLV_Structure(TagName.SD_ID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_ItemID).m_bValueBuffer);
				itemID =(int)strut.toInteger();
				
				result = SDLogDataInfo.ReGetUnits_Query(serverIP,UserByID,userid,username,SDID,itemID,itemName,beginDate);
				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+result);
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+result);
				return Message.COMMON_MES_RESP(result,Msg_Category.SD_ADMIN,ServiceKey.SD_ReGetUnits_Query_Resp);
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("操作失败", Msg_Category.SD_ADMIN, ServiceKey.SD_ReGetUnits_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

	}
}
