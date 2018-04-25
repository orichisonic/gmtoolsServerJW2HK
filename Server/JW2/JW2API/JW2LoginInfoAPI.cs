using System;
using System.Data;
using System.Text;
using GM_Server.JW2DataInfo;
using Common.Logic;
using Common.DataInfo;
using Domino;
using Common.NotesDataInfo;
using lg = Common.API.LanguageAPI;
using System.Configuration;
namespace GM_Server.JW2API
{
	/// <summary>
	/// JW2LoginInfoAPI 的摘要说明。
	/// </summary>
	public class JW2LoginInfoAPI
	{
		Message msg = null;
		public JW2LoginInfoAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region 构造函数
		public JW2LoginInfoAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
		}
		#endregion

		#region 剔除用户下线
		/// <summary>
		/// 剔除用户下线
		/// </summary>
		/// <returns></returns>
		public Message JW2_BANISHPLAYER()
		{
			string strDesc = "";
			string serverIP = "";
			int result = -1;
			string userid = "";
			string Reason = "";
			int userSN = 0;
			string UserName = "";
			string UserNick = "";
			string charkey = "";
			int userbyid = 0;
			try
			{
				//ip
//				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				//操作员id
				TLV_Structure strut = new TLV_Structure(TagName.Magic_PetID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userbyid =(int)strut.toInteger();
				//用户名
//				UserName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserID).m_bValueBuffer);
				//角色id
				UserName = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserID).m_bValueBuffer);

				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_BANISHPLAYER+UserName);
				result = JW2DataInfo.JW2LoginDataInfo.BANISHPLAYER(serverIP,UserName,userbyid,ref strDesc);
				if(result ==0)
				{
					Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_BANISHPLAYER+UserName+lg.JW2API_Success);
					return Message.COMMON_MES_RESP(strDesc,Msg_Category.JW2_ADMIN,ServiceKey.JW2_BANISHPLAYER_RESP);
				}
				else
				{
					Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_BANISHPLAYER+UserName+lg.JW2API_Failure);
					return Message.COMMON_MES_RESP(strDesc,Msg_Category.JW2_ADMIN,ServiceKey.JW2_BANISHPLAYER_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryBANISHPLAYER+serverIP+lg.JW2API_PlayerAccount+UserName+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoBANISHPLAYER, Msg_Category.JW2_ADMIN, ServiceKey.JW2_BANISHPLAYER_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion		

		#region 解封用户
		/// <summary>
		/// 解封用户
		/// </summary>
		/// <returns></returns>
		public Message JW2_ACCOUNT_OPEN()
		{
			string strDesc = "";
			string serverIP = "";
			int result = -1;
			string userid = "";
			string Reason = "";
			int userSN = 0;
			string UserName = "";
			string UserNick = "";
			string charkey = "";
			int userbyid = 0;
			try
			{
				//ip
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				//操作员id
				TLV_Structure strut = new TLV_Structure(TagName.Magic_PetID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userbyid =(int)strut.toInteger();
				//昵称
				UserNick = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserNick).m_bValueBuffer);
				//用户名
				UserName = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserID).m_bValueBuffer);
				//角色id
				strut = new TLV_Structure(TagName.Magic_PetID,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserSN).m_bValueBuffer);
				userSN =(int)strut.toInteger();
				//解封原因
				Reason = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_Reason).m_bValueBuffer);

				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_ACCOUNTOPEN+UserNick);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_ACCOUNTOPEN+UserNick);
				result = JW2DataInfo.JW2LoginDataInfo.ACCOUNT_OPEN(serverIP,userSN,UserNick,UserName,userbyid,Reason,ref strDesc);
				if(result ==1)
				{
					Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_ACCOUNTOPEN+UserNick+lg.JW2API_Success);
					return Message.COMMON_MES_RESP(strDesc,Msg_Category.JW2_ADMIN,ServiceKey.JW2_ACCOUNT_OPEN_RESP);
				}
				else
				{
					Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_ACCOUNTOPEN+UserNick+lg.JW2API_Failure);
					return Message.COMMON_MES_RESP(strDesc,Msg_Category.JW2_ADMIN,ServiceKey.JW2_ACCOUNT_OPEN_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryACCOUNTOPEN+serverIP+lg.JW2API_PlayerAccount+UserName+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_ACCOUNTOPEN + UserNick +lg.JW2API_Failure, Msg_Category.JW2_ADMIN, ServiceKey.JW2_ACCOUNT_OPEN_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion		

		#region 封停用户
		/// <summary>
		/// 封停用户
		/// </summary>
		/// <returns></returns>
		public Message JW2_ACCOUNT_CLOSE()
		{
			string  strDesc = "";
			string serverIP = "";
			int result = -1;
			string userid = "";
			string Reason = "";
			int userSN = 0;
			string UserName = "";
			string UserNick = "";
			string charkey = "";
			int userbyid = 0;
			try
			{
				//ip
//				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				//操作员id
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userbyid =(int)strut.toInteger();
				//昵称
//				UserNick = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserNick).m_bValueBuffer);
//				//用户名
//				UserName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserID).m_bValueBuffer);
				//昵称
				UserNick = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserNick).m_bValueBuffer);
				//用户名
				UserName = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserID).m_bValueBuffer);
				//角色id
				strut = new TLV_Structure(TagName.Magic_PetID,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserSN).m_bValueBuffer);
				userSN =(int)strut.toInteger();
				//封停原因
//				Reason = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_Reason).m_bValueBuffer);

				Reason = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_Reason).m_bValueBuffer);

				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_ACCOUNTCLOSE+UserNick);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_ACCOUNTCLOSE+UserNick);
//				if(JW2DataInfo.JW2LoginDataInfo.BANISHPLAYER(serverIP,UserName,userbyid,ref strDesc)==0)
//				{
					result = JW2DataInfo.JW2LoginDataInfo.ACCOUNT_CLOSE(serverIP,userSN,UserNick,UserName,userbyid,Reason,ref strDesc);
					if(result ==1)
					{
						Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_ACCOUNTCLOSE+UserNick+lg.JW2API_Success);
						return Message.COMMON_MES_RESP(strDesc,Msg_Category.JW2_ADMIN,ServiceKey.JW2_ACCOUNT_CLOSE_RESP);
					}
					else if(result ==2)
					{
						Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+UserNick+lg.JW2API_Success);
						return Message.COMMON_MES_RESP("this user has been AccountClose",Msg_Category.JW2_ADMIN,ServiceKey.JW2_ACCOUNT_CLOSE_RESP);
					}
					else
					{
						Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_ACCOUNTCLOSE+UserNick+lg.JW2API_Failure);
						return Message.COMMON_MES_RESP(strDesc,Msg_Category.JW2_ADMIN,ServiceKey.JW2_ACCOUNT_CLOSE_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
					}
//				}
//				else
//				{
//					Console.WriteLine(DateTime.Now+" - 浏览劲舞团2+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"封停玩家"+UserNick+"失败");
//					return Message.COMMON_MES_RESP(strDesc,Msg_Category.JW2_ADMIN,ServiceKey.JW2_ACCOUNT_CLOSE_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
//				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryACCOUNTCLOSE+serverIP+lg.JW2API_PlayerAccount+UserName+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoACCOUNTCLOSE, Msg_Category.JW2_ADMIN, ServiceKey.JW2_ACCOUNT_CLOSE_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion		

		#region 玩家封停帐号信息查询
		/// <summary>
		/// 玩家封停帐号信息查询
		/// </summary>
		/// <returns></returns>
		public Message JW2_ACCOUNT_BANISHMENT_QUERY(int index,int pageSize)
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			string  userID = "";
			int type = 0;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				userID = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserID).m_bValueBuffer);
				
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_GOODSTYPE).m_bValueBuffer);
				type =(int)strut.toInteger();

				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_BANISHMENT);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userID+lg.JW2API_BANISHMENT);
				ds = JW2DataInfo.JW2LoginDataInfo.ACCOUNT_BANISHMENT_QUERY(serverIP,userID,type);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_ACCOUNT_BANISHMENT_QUERY_RESP,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.JW2API_NoBANISHMENT,Msg_Category.JW2_ADMIN,ServiceKey.JW2_ACCOUNT_BANISHMENT_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryBANISHMENT+serverIP+lg.JW2API_PlayerAccount+userID+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoBANISHMENT, Msg_Category.JW2_ADMIN, ServiceKey.JW2_ACCOUNT_BANISHMENT_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 玩家登陆登出信息
		/// <summary>
		/// 玩家登陆登出信息
		/// </summary>
		/// <returns></returns>
		public Message JW2_LOGINOUT_QUERY(int index,int pageSize)
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			int userSN = 0;
			string BeginTime = "";
			string login_IP = "";
			string EndTime = "";
			int type = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				login_IP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_IP).m_bValueBuffer);
				BeginTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.BeginTime).m_bValueBuffer);
				EndTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.EndTime).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserSN).m_bValueBuffer);

				userSN =(int)strut.toInteger();
				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_LOGINOUT);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userSN+lg.JW2API_LOGINOUT);
				ds = JW2DataInfo.JW2LoginDataInfo.LOGINOUT_QUERY(serverIP,userSN,login_IP,BeginTime,EndTime);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_LOGINOUT_QUERY_RESP,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.JW2API_NoLOGINOUT,Msg_Category.JW2_ADMIN,ServiceKey.JW2_LOGINOUT_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryLOGINOUT+serverIP+lg.JW2API_BeginTime+BeginTime+lg.JW2API_EndTime+EndTime+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoLOGINOUT, Msg_Category.JW2_ADMIN, ServiceKey.JW2_LOGINOUT_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 公告更新
		/// <summary>
		/// 公告更新
		/// </summary>
		/// <returns></returns>
		public Message JW2_BOARDTASK_UPDATE()
		{
			string serverIP = "";
			int result = -1;
			int Taskid = 0;
			string BoardMessage = "";
			string BeginTime = "";
			string EndTime = "";
			int Interval = 0;
			int userbyid = 0;
			int Status = 0;
			try
			{
				//ip
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				//操作员id
				TLV_Structure strut = new TLV_Structure(TagName.Magic_PetID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userbyid =(int)strut.toInteger();
				//Taskid
				strut = new TLV_Structure(TagName.Magic_PetID,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_TaskID).m_bValueBuffer);
				Taskid =(int)strut.toInteger();
				//公告内容
				BoardMessage = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_BoardMessage).m_bValueBuffer);
				//开始时间
				BeginTime = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_BeginTime).m_bValueBuffer);
				//结束时间
				EndTime = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_EndTime).m_bValueBuffer);
				//间隔
				strut = new TLV_Structure(TagName.Magic_PetID,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_Interval).m_bValueBuffer);
				Interval =(int)strut.toInteger();
				//封停状态
				strut = new TLV_Structure(TagName.Magic_PetID,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_Status).m_bValueBuffer);
				Status =(int)strut.toInteger();

				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_BOARDTASKUPDATE);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_BOARDTASKUPDATE);
				result = JW2DataInfo.JW2LoginDataInfo.BOARDTASK_UPDATE(serverIP,Taskid,BoardMessage,BeginTime,EndTime,userbyid,Interval,Status);
				if(result ==1)
				{
					Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_BOARDTASKUPDATE+lg.JW2API_Success);
					return Message.COMMON_MES_RESP("SCUESS",Msg_Category.JW2_ADMIN,ServiceKey.JW2_BOARDTASK_UPDATE_RESP);
				}
				else
				{
					Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_BOARDTASKUPDATE+lg.JW2API_Failure);
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.JW2_ADMIN,ServiceKey.JW2_BOARDTASK_UPDATE_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryBOARDTASKUPDATE+serverIP+lg.JW2API_BeginTime+BeginTime+lg.JW2API_EndTime+EndTime+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_BOARDTASKUPDATE, Msg_Category.JW2_ADMIN, ServiceKey.JW2_BOARDTASK_UPDATE_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion		

		#region 公告查询
		/// <summary>
		/// 公告查询
		/// </summary>
		/// <returns></returns>
		public Message JW2_BOARDTASK_QUERY(int index,int pageSize)
		{
			DataSet ds = null;
			try
			{

				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress);
				ds = JW2DataInfo.JW2LoginDataInfo.BOARDTASK_QUERY();


				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_BOARDTASK_QUERY_RESP,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.JW2API_NoBOARDTASK,Msg_Category.JW2_ADMIN,ServiceKey.JW2_BOARDTASK_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryBOARDTASKUPDATE+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoBOARDTASK, Msg_Category.JW2_ADMIN, ServiceKey.JW2_BOARDTASK_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 公告添加
		/// <summary>
		/// 公告添加
		/// </summary>
		/// <returns></returns>
		public Message JW2_BOARDTASK_INSERT()
		{
			string serverIP = "";
			string GSserverIP = "";
			int result = -1;
			string BoardMessage = "";
			string BeginTime = "";
			string EndTime = "";
			int Interval = 0;
			int userbyid = 0;
			try
			{
				//ip
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
//				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				//操作员id
				TLV_Structure strut = new TLV_Structure(TagName.Magic_PetID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userbyid =(int)strut.toInteger();
				//GSserverIP
				GSserverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_GSServerIP).m_bValueBuffer);
				//公告内容
				BoardMessage = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_BoardMessage).m_bValueBuffer);
				//开始时间
				BeginTime = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_BeginTime).m_bValueBuffer);
				//结束时间
				EndTime = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_EndTime).m_bValueBuffer);
//				GSserverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_GSServerIP).m_bValueBuffer);
//				//公告内容
//				BoardMessage = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_BoardMessage).m_bValueBuffer);
//				//开始时间
//				BeginTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_BeginTime).m_bValueBuffer);
//				//结束时间
//				EndTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_EndTime).m_bValueBuffer);
				//间隔
				strut = new TLV_Structure(TagName.Magic_PetID,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_Interval).m_bValueBuffer);
				Interval =(int)strut.toInteger();

				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_BOARDTASKINSERT);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_BOARDTASKINSERT);
				result = JW2DataInfo.JW2LoginDataInfo.BOARDTASK_INSERT(serverIP,GSserverIP,BoardMessage,BeginTime,EndTime,userbyid,Interval);

				
				if(result ==1)
				{

					SqlHelper helper = new SqlHelper();
					int iiii = 0;

					string DatabaseIP = ConfigurationSettings.AppSettings["DatabaseIP"];
					string DatabaseName = ConfigurationSettings.AppSettings["DatabaseName"];
					string DatabaseUser = ConfigurationSettings.AppSettings["DatabaseUser"];
					string DatabasePass= ConfigurationSettings.AppSettings["DatabasePass"];
					helper.init(DatabaseIP,DatabaseName,DatabaseUser, DatabasePass);

				
					string SqlStr = "select interval*1000*60,taskID,datediff(mi,getdate(),SendEndTime) as expire,datediff(mi,getdate(),SendBeginTime) as start,status,command,boardMessage,datediff(mi,getdate(),SendBeginTime),atonce from JW2_boardTasker where (status=0 or status=2)";
					System.Data.DataSet ds = SqlHelper.ExecuteDataset(SqlStr);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						for(int  i=0;i<ds.Tables[0].Rows.Count;i++)
						{
							int interval = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]);
							int taskID = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[1]);
							int timeOut = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[2]);
							int timeStart = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]);
							int status = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]);
							int command = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[5].ToString());
							string context = ds.Tables[0].Rows[i].ItemArray[6].ToString();
							int startTime = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[7].ToString());
							int atonce = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[8].ToString());
							TaskBulletion task = new TaskBulletion(i);
							task.status =status;
							task.taskID = taskID;
							task.timeOut = timeOut;
							task.timeStart = timeStart;
							task.command = command;
							task.interval = interval;
							task.context = context;
							task.atonce = atonce;

							task.process(task.taskID );
							}
						}
					string SqlStr_U = "update JW2_boardTasker set status=1 where status=0 or status=2";//即时公告发送后结束
					SqlHelper.ExecCommand(SqlStr_U);
					Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_BOARDTASKINSERT+lg.JW2API_Success);
					return Message.COMMON_MES_RESP("SCUESS",Msg_Category.JW2_ADMIN,ServiceKey.JW2_BOARDTASK_INSERT_RESP);
				}
				else
				{
					Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_BOARDTASKINSERT+lg.JW2API_Failure);
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.JW2_ADMIN,ServiceKey.JW2_BOARDTASK_INSERT_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryBOARDTASKINSERT+serverIP+lg.JW2API_BeginTime+BeginTime+lg.JW2API_EndTime+EndTime+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_BOARDTASKINSERT, Msg_Category.JW2_ADMIN, ServiceKey.JW2_BOARDTASK_INSERT_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion		

		#region 查看玩家婚姻信息
		/// <summary>
		/// 查看玩家婚姻信息
		/// </summary>
		/// <returns></returns>
		public Message JW2_WEDDINGINFO_QUERY()
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
				
				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userSN+lg.JW2API_WEDDINGINFO);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userSN+lg.JW2API_WEDDINGINFO);
				ds = JW2DataInfo.JW2LoginDataInfo.WEDDINGINFO_QUERY(serverIP,userSN);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,0,ds.Tables[0].Rows.Count,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_WEDDINGINFO_QUERY_RESP,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.JW2API_NoWEDDINGINFO,Msg_Category.JW2_ADMIN,ServiceKey.JW2_WEDDINGINFO_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryWEDDINGINFO+serverIP+lg.JW2API_PlayerAccount+userSN+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoWEDDINGINFO, Msg_Category.JW2_ADMIN, ServiceKey.JW2_WEDDINGINFO_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion
		
		#region 查看玩家婚姻历史
		/// <summary>
		/// 查看玩家婚姻历史
		/// </summary>
		/// <returns></returns>
		public Message JW2_WEDDINGLOG_QUERY(int index,int pageSize)
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			int userSN = -1;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserSN).m_bValueBuffer);
				userSN =(int)strut.toInteger();
				
				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userSN+lg.JW2API_WEDDINGLOG);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userSN+lg.JW2API_WEDDINGLOG);
				ds = JW2DataInfo.JW2LoginDataInfo.WEDDINGLOG_QUERY(serverIP,userSN);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_WEDDINGLOG_QUERY_RESP,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.JW2API_NoWEDDINGLOG,Msg_Category.JW2_ADMIN,ServiceKey.JW2_WEDDINGLOG_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryWEDDINGLOG+serverIP+lg.JW2API_PlayerAccount+userSN+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoWEDDINGLOG, Msg_Category.JW2_ADMIN, ServiceKey.JW2_WEDDINGLOG_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 查看玩家结婚信息
		/// <summary>
		/// 查看玩家结婚信息
		/// </summary>
		/// <returns></returns>
		public Message JW2_WEDDINGGROUND_QUERY()
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
				
				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userSN+lg.JW2API_WEDDINGINFO);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userSN+lg.JW2API_WEDDINGINFO);
				ds = JW2DataInfo.JW2LoginDataInfo.WEDDINGGROUND_QUERY(serverIP,userSN);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,0,ds.Tables[0].Rows.Count,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_WEDDINGGROUND_QUERY_RESP,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.JW2API_NoWEDDINGINFO,Msg_Category.JW2_ADMIN,ServiceKey.JW2_WEDDINGGROUND_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryWEDDINGINFO+serverIP+lg.JW2API_PlayerAccount+userSN+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoWEDDINGINFO, Msg_Category.JW2_ADMIN, ServiceKey.JW2_WEDDINGGROUND_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 查看玩家情侣信息
		/// <summary>
		/// 查看玩家情侣信息
		/// </summary>
		/// <returns></returns>
		public Message JW2_COUPLEINFO_QUERY()
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
				
				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userSN+lg.JW2API_COUPLEINFO);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userSN+lg.JW2API_COUPLEINFO);
				ds = JW2DataInfo.JW2LoginDataInfo.COUPLEINFO_QUERY(serverIP,userSN);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,0,ds.Tables[0].Rows.Count,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_COUPLEINFO_QUERY_RESP,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.JW2API_NoCOUPLEINFO,Msg_Category.JW2_ADMIN,ServiceKey.JW2_COUPLEINFO_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryCOUPLEINFO+serverIP+lg.JW2API_PlayerAccount+userSN+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoCOUPLEINFO, Msg_Category.JW2_ADMIN, ServiceKey.JW2_COUPLEINFO_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 查看玩家情侣历史
		/// <summary>
		/// 查看玩家情侣历史
		/// </summary>
		/// <returns></returns>
		public Message JW2_COUPLELOG_QUERY(int index,int pageSize)
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
				
				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userSN+lg.JW2API_COUPLELOG);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userSN+lg.JW2API_COUPLELOG);
				ds = JW2DataInfo.JW2LoginDataInfo.COUPLELOG_QUERY(serverIP,userSN);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_COUPLELOG_QUERY_RESP,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.JW2API_NoQueryCOUPLELOG,Msg_Category.JW2_ADMIN,ServiceKey.JW2_COUPLELOG_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryCOUPLELOG+serverIP+lg.JW2API_PlayerAccount+userSN+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoQueryCOUPLELOG, Msg_Category.JW2_ADMIN, ServiceKey.JW2_COUPLELOG_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 修改临时密码
		/// <summary>
		/// 修改临时密码
		/// </summary>
		/// <returns></returns>
		public Message JW2_MODIFY_PWD()
		{
			string strDesc = "";
			string serverIP = "";
			//int uid = 0;
			int result = 0;
			string strnick = "";
			string username = "";
			string content = "";
			string serverName = "";
			string TmpPWD = "";
			int UserByID = 0;
			DateTime EndTime;
			int usersn = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				serverName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerName).m_bValueBuffer);
				username = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserID).m_bValueBuffer);
				TmpPWD = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_TmpPWD).m_bValueBuffer);

				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserSN).m_bValueBuffer);
				usersn =(int)strut.toInteger();
				//GM 操作员
				strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				UserByID =(int)strut.toInteger();

				result = JW2DataInfo.JW2LoginDataInfo.TmpPassWord_Query(serverIP,serverName,UserByID,usersn,username,TmpPWD,ref strDesc);
				if(result ==1)
				{
					SqlHelper.log.WriteLog(lg.JW2API_QueryMODIFYPWD+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_ModiUser+username+lg.JW2API_MODIFYPWD);
					Console.WriteLine(DateTime.Now+lg.JW2API_QueryMODIFYPWD+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_ModiUser+username+lg.JW2API_MODIFYPWD);
					return Message.COMMON_MES_RESP(strDesc,Msg_Category.JW2_ADMIN,ServiceKey.JW2_MODIFY_PWD_RESP);
				}
				else if(result == 2)
				{
					return Message.COMMON_MES_RESP(lg.JW2API_NoMODIFYPWD,Msg_Category.JW2_ADMIN,ServiceKey.JW2_MODIFY_PWD_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
				else
				{
					return Message.COMMON_MES_RESP(strDesc,Msg_Category.JW2_ADMIN,ServiceKey.JW2_MODIFY_PWD_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
				
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryMODIFYPWD+serverIP+lg.JW2API_PlayerAccount+username+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoExistMODIFYPWD, Msg_Category.JW2_ADMIN, ServiceKey.JW2_MODIFY_PWD_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 恢复临时密码
		/// <summary>
		/// 恢复临时密码
		/// </summary>
		/// <returns></returns>
		public Message JW2_RECALL_PWD()
		{
			string strDesc = "";
			string serverIP = "";
			//int uid = 0;
			int result = 0;
			string strnick = "";
			string username = "";
			string content = "";
			string serverName = "";
			string TmpPWD = "";
			int UserByID = 0;
			DateTime EndTime;
			int usersn = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				serverName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerName).m_bValueBuffer);
				username = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserID).m_bValueBuffer);

				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserSN).m_bValueBuffer);
				usersn =(int)strut.toInteger();
				//GM 操作员
				strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				UserByID =(int)strut.toInteger();

				result = JW2DataInfo.JW2LoginDataInfo.ReTmpPassWord_Query(serverIP,serverName,UserByID,usersn,username,ref strDesc);
				if(result ==1)
				{
					SqlHelper.log.WriteLog(lg.JW2API_QueryRECALLPWD+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_RECALLUser+username+lg.JW2API_RECALLPWD);
					Console.WriteLine(DateTime.Now+lg.JW2API_QueryRECALLPWD+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_RECALLUser+username+lg.JW2API_RECALLPWD);
					return Message.COMMON_MES_RESP(strDesc,Msg_Category.JW2_ADMIN,ServiceKey.JW2_RECALL_PWD_RESP);
				}
				else if(result ==2)
				{
					return Message.COMMON_MES_RESP(lg.JW2API_NoRECALLPWD,Msg_Category.JW2_ADMIN,ServiceKey.JW2_RECALL_PWD_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
				else
				{
					return Message.COMMON_MES_RESP(strDesc,Msg_Category.JW2_ADMIN,ServiceKey.JW2_RECALL_PWD_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
				
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryRECALLPWD+serverIP+lg.JW2API_PlayerAccount+username+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoExistRECALLPWD, Msg_Category.JW2_ADMIN, ServiceKey.JW2_RECALL_PWD_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 查询最后一次临时密码
		/// <summary>
		/// 查询最后一次临时密码
		/// </summary>
		/// <returns></returns>
		public Message JW2_SearchPassWord_Query()
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			string username = "";
			string serverName = "";
			int usersn = 0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				serverName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerName).m_bValueBuffer);
				username = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserID).m_bValueBuffer);
				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserSN).m_bValueBuffer);
				usersn =(int)strut.toInteger();

				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_SearchPassWord);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_SearchPassWord);
				ds = JW2DataInfo.JW2LoginDataInfo.SearchPassWord_Query(serverIP,serverName,usersn,username);		
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,ds.Tables[0].Rows.Count,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_SearchPassWord_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.JW2API_NoSearchPassWord,Msg_Category.JW2_ADMIN,ServiceKey.JW2_SearchPassWord_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QuerySearchPassWord+serverIP+lg.JW2API_PlayerAccount+username+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoSearchPassWord, Msg_Category.JW2_ADMIN, ServiceKey.JW2_SearchPassWord_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion


		
		
		
	}
}
