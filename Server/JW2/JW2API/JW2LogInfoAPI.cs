using System;
using System.Data;
using System.Text;
using GM_Server.JW2DataInfo;
using Common.Logic;
using Common.DataInfo;
using System.Web;
using System.Net;
using Domino;
using Common.NotesDataInfo;
using lg = Common.API.LanguageAPI;
namespace GM_Server.JW2API
{
	/// <summary>
	/// JW2LogInfoAPI 的摘要说明。
	/// </summary>
	public class JW2LogInfoAPI
	{
		Message msg = null;
		public JW2LogInfoAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 构造函数
		public JW2LogInfoAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
		}
		#endregion

		#region 查看购物，送礼
		/// <summary>
		/// 查看购物，送礼
		/// </summary>
		/// <returns></returns>
		public Message JW2_SMALL_PRESENT_QUERY(int index,int pageSize)
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
				strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_GOODSTYPE).m_bValueBuffer);
				type =(int)strut.toInteger();
				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userSN+lg.JW2API_ShoppingPresent);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userSN+lg.JW2API_ShoppingPresent);
				ds = JW2DataInfo.JW2LogDataInfo.SMALL_PRESENT_QUERY(serverIP,userSN,type);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_SMALL_PRESENT_QUERY_RESP,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.JW2API_NoShppingPresent,Msg_Category.JW2_ADMIN,ServiceKey.JW2_SMALL_PRESENT_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryShoppingPresent+serverIP+lg.JW2API_PlayerAccount+userSN+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoShppingPresent, Msg_Category.JW2_ADMIN, ServiceKey.JW2_SMALL_PRESENT_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 消费日志查询
		/// <summary>
		/// 消费日志查询
		/// </summary>
		/// <returns></returns>
		public Message JW2_CashMoney_Log(int index,int pageSize)
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			string BeginTime = "";
			string EndTime = "";
			string userID = "";
			int type =-1;
			int userSN = -1;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				userID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserID).m_bValueBuffer);
				BeginTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.BeginTime).m_bValueBuffer);
				EndTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.EndTime).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserSN).m_bValueBuffer);
				userSN =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_GOODSTYPE).m_bValueBuffer);
				type =(int)strut.toInteger();
				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userID+lg.JW2API_ConsumerLog);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userID+lg.JW2API_ConsumerLog);
				ds = JW2DataInfo.JW2LogDataInfo.CashMoney_Log(serverIP,userSN,type,BeginTime,EndTime);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList;
					if(type==6)
						structList = Message.JW2_buildTLV(ds,index,pageSize,false,userID);
					else
						structList = Message.JW2_buildTLV(ds,index,pageSize,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_CashMoney_Log_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.JW2API_NoConsumerLog,Msg_Category.JW2_ADMIN,ServiceKey.JW2_CashMoney_Log_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryConsumerLog+serverIP+lg.JW2API_PlayerAccount+userID+lg.JW2API_BeginTime+BeginTime+lg.JW2API_EndTime+EndTime+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoConsumerLog, Msg_Category.JW2_ADMIN, ServiceKey.JW2_CashMoney_Log_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 购买日志
		/// <summary>
		/// 购买日志
		/// </summary>
		/// <returns></returns>
		public Message JW2_MoneyLog_Query(int index,int pageSize)
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			string BeginTime = "";
			string EndTime = "";
			int GoodsType = -1;
			int type =-1;
			string itemName = "";
			int userSN = -1;
			try
			{
				itemName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ItemName).m_bValueBuffer);
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				BeginTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.BeginTime).m_bValueBuffer);
				EndTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.EndTime).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserSN).m_bValueBuffer);
				userSN =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_GOODSTYPE).m_bValueBuffer);
				GoodsType =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_Type).m_bValueBuffer);
				type =(int)strut.toInteger();
				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userSN+lg.JW2API_PurchaseLog);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userSN+lg.JW2API_PurchaseLog);
				ds = JW2DataInfo.JW2LogDataInfo.MoneyLog_Query(serverIP,userSN,GoodsType,BeginTime,EndTime,type,itemName);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_MoneyLog_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.JW2API_NoPurchaseLog,Msg_Category.JW2_ADMIN,ServiceKey.JW2_MoneyLog_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryPurchaseLog+serverIP+lg.JW2API_PlayerAccount+userSN+lg.JW2API_BeginTime+BeginTime+lg.JW2API_EndTime+EndTime+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoPurchaseLog, Msg_Category.JW2_ADMIN, ServiceKey.JW2_MoneyLog_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 中间件背包道具
		/// <summary>
		/// 中间件背包道具
		/// </summary>
		/// <returns></returns>
		public Message JW2_CenterAvAtarItem_Bag_Query(int index,int pageSize)
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			string userID = "";
			int userSN = -1;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				userID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserID).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserSN).m_bValueBuffer);
				userSN =(int)strut.toInteger();
				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userSN+lg.JW2API_CenterAvAtarItemBag);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userSN+lg.JW2API_CenterAvAtarItemBag);
				ds = JW2DataInfo.JW2LogDataInfo.CenterAvAtarItem_Bag_Query(serverIP,userSN);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,userID);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_CenterAvAtarItem_Bag_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.JW2API_NoCenterAvAtarItemBag,Msg_Category.JW2_ADMIN,ServiceKey.JW2_CenterAvAtarItem_Bag_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryCenterAvAtarItemBag+serverIP+lg.JW2API_PlayerAccount+userSN+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoCenterAvAtarItemBag, Msg_Category.JW2_ADMIN, ServiceKey.JW2_CenterAvAtarItem_Bag_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 中间件身上道具
		/// <summary>
		/// 中间件身上道具
		/// </summary>
		/// <returns></returns>
		public Message JW2_CenterAvAtarItem_Equip_Query(int index,int pageSize)
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			string userID = "";
			int userSN = -1;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				userID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserID).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserSN).m_bValueBuffer);
				userSN =(int)strut.toInteger();			
				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userSN+lg.JW2API_CenterAvAtarItemEquip);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userSN+lg.JW2API_CenterAvAtarItemEquip);
				ds = JW2DataInfo.JW2LogDataInfo.CenterAvAtarItem_Equip_Query(serverIP,userSN);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,userID);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_CenterAvAtarItem_Equip_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.JW2API_NoCenterAvAtarItemEquip,Msg_Category.JW2_ADMIN,ServiceKey.JW2_CenterAvAtarItem_Equip_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryCenterAvAtarItemEquip+serverIP+lg.JW2API_PlayerAccount+userSN+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoCenterAvAtarItemEquip, Msg_Category.JW2_ADMIN, ServiceKey.JW2_CenterAvAtarItem_Equip_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion
		
		#region 举报信息查詢
		/// <summary>
		/// 举报信息查詢
		/// </summary>
		/// <param name="index"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		public Message JW2_JB_Query(int index,int pageSize)
		{
			string serverIP = "";
			string BeginTime = "";
			string EndTime = "";
			string typeName = "";
			int type = -1;

			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);

				BeginTime  = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.BeginTime).m_bValueBuffer);
				EndTime  = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.EndTime).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_Type).m_bValueBuffer);
				type =(int)strut.toInteger();
							
				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_JB+typeName+"!");
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_JB+typeName+"!");
				string ret = "";
				switch(type)
				{
					case 1:
					{
						typeName = "昵称有问题";
						ret = "2,4,8,16,32,64,128,256,512,1024";
						break;
					}
					case 2:
					{
						typeName = "房间名有问题";
						ret = "1,4,8,16,32,64,128,256,512,1024";
						break;
					}
					case 4:
					{
						typeName = "大小喇叭有问题";
						ret = "1,2,8,16,32,64,128,256,512,1024";
						break;
					}
					case 8:
					{
						typeName = "横幅内容有问题";
						ret = "1,2,4,16,32,64,128,256,512,1024";
						break;
					}
					case 16:
					{
						typeName = "宠物名字有问题";
						ret = "1,2,4,8,32,64,128,256,512,1024";
						break;
					}
					case 32:
					{
						typeName = "家族名有问题";
						ret = "1,2,4,8,16,64,128,256,512,1024";
						break;
					}
					case 64:
					{
						typeName = "聊天内容有问题";
						ret = "1,2,4,8,16,32,128,256,512,1024";
						break;
					}
					case 128:
					{
						typeName = "名片信息有问题";
						ret = "1,2,4,8,16,32,64,256,512,1024";
						break;
					}
					case 256:
					{
						typeName = "小屋留言板有问题";
						ret = "1,2,4,8,16,32,64,128,512,1024";
						break;
					}
					case 512:
					{
						typeName = "家族论坛有问题";
						ret = "1,2,4,8,16,32,64,128,256,1024";
						break;
					}
					case 1024:
					{
						typeName = "外挂";
						ret = "1,2,4,8,16,32,64,128,256,512";
						break;
					}
				}
				DataSet ds = JW2DataInfo.JW2LogDataInfo.JB_Query(serverIP,type,ret,BeginTime,EndTime);

				if (ds != null && ds.Tables[0].Rows.Count > 0)
				{
					//总页数
					index = index*pageSize;
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
						Query_Structure strut1 = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length+1);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,int.Parse(ds.Tables[0].Rows[i].ItemArray[0].ToString()));
						strut1.AddTagKey(TagName.JW2_ReportSn,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	

						string ReporterNick = CommonInfo.JW2_UserSn_UserNick(serverIP,int.Parse(ds.Tables[0].Rows[i].ItemArray[1].ToString()));
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ReporterNick);
						strut1.AddTagKey(TagName.JW2_ReporterNick,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2].ToString());
						strut1.AddTagKey(TagName.JW2_ReportedNick,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, int.Parse(ds.Tables[0].Rows[i].ItemArray[3].ToString()));
						strut1.AddTagKey(TagName.JW2_ReportType,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[4].ToString());
						strut1.AddTagKey(TagName.JW2_Memo,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);

						strut1.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 5, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i-index]=strut1;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.JW2_ADMIN, ServiceKey.JW2_JB_Query_Resp, 5);
					
				}

				else
				{
					return Message.COMMON_MES_RESP(lg.JW2API_NoJB, Msg_Category.JW2_ADMIN, ServiceKey.JW2_JB_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryJB+serverIP+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoJB, Msg_Category.JW2_ADMIN, ServiceKey.JW2_JB_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);				
			}
		}
		#endregion

		#region 中間件踢人
		/// <summary>
		/// 中間件踢人
		/// </summary>
		/// <returns></returns>
		public Message JW2_Center_Kick_Query()
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
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				//操作员id
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userbyid =(int)strut.toInteger();
				//昵称
				UserNick = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserNick).m_bValueBuffer);
				//用户名
				UserName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserID).m_bValueBuffer);
				//角色id
				strut = new TLV_Structure(TagName.Magic_PetID,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserSN).m_bValueBuffer);
				userSN =(int)strut.toInteger();

				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_CenterKick+UserNick);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_CenterKick+UserNick);
				if(CommonInfo.JW2_KickUser(userSN,"get_userid",userbyid,serverIP,UserName,ref strDesc)==1)
				{
					result = CommonInfo.JW2_KickUser(userSN,"del_userid",userbyid,serverIP,UserName,ref strDesc);
					if(result ==1)
					{
						return Message.COMMON_MES_RESP(strDesc,Msg_Category.JW2_ADMIN,ServiceKey.JW2_Center_Kick_Query_Resp);
					}
					else
					{
						return Message.COMMON_MES_RESP(strDesc,Msg_Category.JW2_ADMIN,ServiceKey.JW2_Center_Kick_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);

						//return Message.COMMON_MES_RESP("删除卡在中间件中的用户",Msg_Category.JW2_ADMIN,ServiceKey.JW2_Center_Kick_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
					}
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.JW2API_NoCenterKick,Msg_Category.JW2_ADMIN,ServiceKey.JW2_Center_Kick_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryCenterKick+serverIP+lg.JW2API_PlayerAccount+UserName+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_CenterKick+UserNick+lg.JW2API_Failure, Msg_Category.JW2_ADMIN, ServiceKey.JW2_Center_Kick_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion		

		#region 玩家任务日志查询
		/// <summary>
		/// 玩家任务日志查询
		/// </summary>
		/// <returns></returns>
		public Message JW2_MissionInfoLog_Query(int index,int pageSize)
		{
			string serverIP = "";
			//int uid = 0;
			DataSet ds = null;
			string BeginTime = "";
			string EndTime = "";
			string userID = "";
			int type =-1;
			int userSN = -1;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				userID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserID).m_bValueBuffer);
				BeginTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.BeginTime).m_bValueBuffer);
				EndTime = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.EndTime).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserSN).m_bValueBuffer);
				userSN =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.JW2_UserSN,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_Type).m_bValueBuffer);
				type =(int)strut.toInteger();
				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userID+lg.JW2API_MissionInfoLog);
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+userID+lg.JW2API_MissionInfoLog);
				ds = JW2DataInfo.JW2LogDataInfo.MissionInfoLog_Query(serverIP,userSN,type,BeginTime,EndTime);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.JW2_buildTLV(ds,index,pageSize,false,serverIP);
					return Message.COMMON_MES_RESP(structList,Msg_Category.JW2_ADMIN,ServiceKey.JW2_MissionInfoLog_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.JW2API_NoMissionInfoLog,Msg_Category.JW2_ADMIN,ServiceKey.JW2_MissionInfoLog_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryMissionInfoLog+serverIP+lg.JW2API_PlayerAccount+userID+lg.JW2API_BeginTime+BeginTime+lg.JW2API_EndTime+EndTime+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_NoMissionInfoLog, Msg_Category.JW2_ADMIN, ServiceKey.JW2_MissionInfoLog_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion

		#region 重复购买退款
		/// <summary>
		/// 重复购买退款
		/// </summary>
		/// <returns></returns>
		public Message JW2_AgainBuy_Query()
		{
			string  strDesc = "";
			string serverIP = "";
			int result = -1;

			string userid = "";
			string Reason = "";
			int userSN = 0;
			string UserName = "";
			string UserNick = "";
			string itemName = "";
			int cash = 0;
			int userbyid = 0;
			int buySN = 0;
			try
			{
				//ip
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ServerIP).m_bValueBuffer);
				//操作员id
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userbyid =(int)strut.toInteger();
				//用户名
				UserName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserID).m_bValueBuffer);
				//角色id
				strut = new TLV_Structure(TagName.Magic_PetID,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_UserSN).m_bValueBuffer);
				userSN =(int)strut.toInteger();
				//钱
				strut = new TLV_Structure(TagName.Magic_PetID,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_Cash).m_bValueBuffer);
				cash =(int)strut.toInteger();
				//LOGsn
				strut = new TLV_Structure(TagName.Magic_PetID,4,msg.m_packet.m_Body.getTLVByTag(TagName.JW2_BUYSN).m_bValueBuffer);
				buySN =(int)strut.toInteger();

				//道具名
				itemName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.JW2_ItemName).m_bValueBuffer);

				SqlHelper.log.WriteLog(lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+UserName+lg.JW2API_RepeatPurchaseItem+itemName+lg.JW2API_ReFund+cash.ToString());
				Console.WriteLine(DateTime.Now+lg.JW2API_BrowseServerAddress+CommonInfo.serverIP_Query(serverIP)+lg.JW2API_PlayerAccount+UserName+lg.JW2API_RepeatPurchaseItem+itemName+lg.JW2API_ReFund+cash.ToString());
				if(JW2DataInfo.JW2LoginDataInfo.BANISHPLAYER(serverIP,UserName,userbyid,ref strDesc)==0)
				{
					result = JW2DataInfo.JW2LogDataInfo.AgainBuy_Query(serverIP,buySN,userSN,userbyid,cash,UserName,itemName);
					if(result ==1)
					{
						return Message.COMMON_MES_RESP("SCUESS",Msg_Category.JW2_ADMIN,ServiceKey.JW2_AgainBuy_Query_Resp);
					}
					else
					{
						return Message.COMMON_MES_RESP(lg.JW2API_NoRepeatPurchaseItem,Msg_Category.JW2_ADMIN,ServiceKey.JW2_AgainBuy_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
					}
				}
				else
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.JW2_ADMIN,ServiceKey.JW2_AgainBuy_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.JW2API_QueryRepeatPurchaseItem+serverIP+lg.JW2API_PlayerAccount+UserName+"->"+ex.Message);
				return Message.COMMON_MES_RESP(lg.JW2API_Failure, Msg_Category.JW2_ADMIN, ServiceKey.JW2_AgainBuy_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		#endregion		

		

	}
}
