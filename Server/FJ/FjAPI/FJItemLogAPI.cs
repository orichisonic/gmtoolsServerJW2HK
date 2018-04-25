using System;
using System.Data;
using System.Text;
using Common.Logic;
using Common.DataInfo;
using GM_Server.FJDataInfo;
using System.Collections;
namespace GM_Server.FjAPI
{
	/// <summary>
	/// FJItemLogAPI 的摘要说明。
	/// </summary>
	public class FJItemLogAPI
	{
		Message msg = null;
		public FJItemLogAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public FJItemLogAPI(byte[] packet)
		{
			msg = new Message(packet, (uint)packet.Length);
		}
		/// <summary>
		/// 查看服务器所有停封帐号
		/// </summary>
		/// <returns></returns>
		public Message FJ_Gamesbanishment_Query(int index, int pageSize)
		{
			string serverIP = null;
			string city = null;
			string account = null;
			System.Data.DataSet ds = null;
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				city = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserID).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址" + serverIP + "游戏里面停封帐号信息");
				Console.WriteLine(DateTime.Now + " - 浏览风火之旅+>服务器地址" + serverIP + "游戏里面停封帐号信息");
				ds = FJItemLogInfo.GamesUserBan_Query(serverIP, city, account);
				if (null != ds && ds.Tables[0].Rows.Count > 0)
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
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length + 1);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.FJ_UserID, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.FJ_BanDate, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.FJ_Reason, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.FJ_Style, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						//总页数
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));


						structList[i - index] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_GamesUserBan_Query_RESP, 5);
				}
				else
				{
					return Message.COMMON_MES_RESP("游戏里面没有被停封的帐号", Msg_Category.FJ_ADMIN, ServiceKey.FJ_GamesUserBan_Query_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

				}
			}
			catch (System.Exception e)
			{
				return Message.COMMON_MES_RESP("游戏里面没有被停封的帐号", Msg_Category.FJ_ADMIN, ServiceKey.FJ_GamesUserBan_Query_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 游戏里面玩家帐号解封
		/// </summary>
		/// <returns></returns>
		public Message FJ_GamesAccountOpen_Update()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string city = null;
			string account = null;
			string reason = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				city = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserID).m_bValueBuffer);
				reason = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Reason).m_bValueBuffer);
				result = FJItemLogInfo.GamesUserBan_Open(operateUserID, serverIP, city, account, reason);
				if (result == -1)
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "游戏里面玩家" + account + "的帐号已被解封");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "游戏里面玩家" + account + "帐号不存在!");
					return Message.COMMON_MES_RESP("该玩家帐号已被解封!", Msg_Category.FJ_ADMIN, ServiceKey.FJ_GamesUserBan_Open_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
				else if (result == 1)
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "游戏里面玩家" + account + "帐号解封成功!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "游戏里面玩家" + account + "帐号解封成功!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.FJ_ADMIN, ServiceKey.FJ_GamesUserBan_Open_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "游戏里面玩家" + account + "帐号解封失败!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "游戏里面玩家" + account + "帐号解封失败!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.FJ_ADMIN, ServiceKey.FJ_GamesUserBan_Open_RESP);
				}
			}
			catch (System.Exception e)
			{
				return Message.COMMON_MES_RESP("解封失败", Msg_Category.FJ_ADMIN, ServiceKey.FJ_GamesUserBan_Open_RESP,TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 查看服务器所有停封帐号
		/// </summary>
		/// <returns></returns>
		public Message FJ_banishment_Query(int index, int pageSize)
		{
			string serverIP = null;
			string city = null;
			string account = null;
			System.Data.DataSet ds = null;
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				city = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserID).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址" + serverIP + "停封帐号信息");
				Console.WriteLine(DateTime.Now + " - 浏览风火之旅+>服务器地址" + serverIP + "停封帐号信息");
				ds = FJItemLogInfo.UserBan_Query(serverIP, city, account);
				if (null != ds && ds.Tables[0].Rows.Count > 0)
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
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length + 1);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.FJ_UserID, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.FJ_BanDate, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.FJ_Reason, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						//判断是否被在线GM封停,通过IP，大区名，帐号
//						DataSet fjDS = FJItemLogInfo.FJGMBan_Query(serverIP,city,ds.Tables[0].Rows[i].ItemArray[0].ToString());
//						if(fjDS!=null && fjDS.Tables[0].Rows.Count>0)
//						{
//							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, "在线GM封停");
//						}
//						else
//						{
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[3]);
						//}
						strut.AddTagKey(TagName.FJ_Style, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						//总页数
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));


						structList[i - index] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserBan_Query_Resp, 5);
				}
				else
				{
					return Message.COMMON_MES_RESP("当前没有被停封的帐号", Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserBan_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);

				}
			}
			catch (System.Exception e)
			{
				return Message.COMMON_MES_RESP("当前没有被停封的帐号", Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserBan_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 服务器玩家帐号解封
		/// </summary>
		/// <returns></returns>
		public Message FJ_AccountOpen_Update()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string city = null;
			string account = null;
			string reason = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				city = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserID).m_bValueBuffer);
				reason = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Reason).m_bValueBuffer);
				result = FJItemLogInfo.UserBan_Open(operateUserID, serverIP, city, account, reason);
				if (result == -1)
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + account + "的帐号已被解封");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + account + "帐号不存在!");
					return Message.COMMON_MES_RESP("该玩家帐号已被解封!", Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserBan_Open_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

				else if (result == 1)
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + account + "帐号解封成功!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + account + "帐号解封成功!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserBan_Open_Resp);
				}
				else
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + account + "帐号解封失败!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + account + "帐号解封失败!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserBan_Open_Resp);
				}
			}
			catch (System.Exception e)
			{
				return Message.COMMON_MES_RESP("解封失败", Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserBan_Open_Resp,TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}

		/// <summary>
		/// 服务器玩家帐号解封 扣除300积分
		/// </summary>
		/// <returns></returns>
		public Message FJ_Account300_Update()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string city = null;
			string account = null;
			string reason = null;
			string desc = "";
			int Decresult = 0;
			ArrayList arrUserinfo;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				city = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserID).m_bValueBuffer);
				reason = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Reason).m_bValueBuffer);
				//调用接口请求玩家合计充值金额
				arrUserinfo = CharacterInfo.FJUserCharge_Query(CommonInfo.FJdbid_Query(city),account,ref desc);
				//Decresult = FJItemLogInfo.FJ_UserChargeConsume(account,city, Convert.ToInt32(arrUserinfo[1])*10);
				
				//充值金额为 RMB
				//1RMB = 1000休闲币
				//1000休闲币 = 10 虚拟金 
				//解封需要扣除300虚拟金
				//首先判断玩家的金额是否300虚拟金 不足300则直接返回 无法解封帐号
				if(arrUserinfo!= null && arrUserinfo[1].ToString() !="" && Convert.ToInt32(arrUserinfo[1])*10 >= 300 )
				{	//虚拟金>=300 则调用存储过程 记录当前扣除的金额 玩家帐号 大区
					Decresult = FJItemLogInfo.FJ_UserChargeConsume(city,account, Convert.ToInt32(arrUserinfo[1])*10);
					//记录成功则解封玩家
					if(Decresult == 1)
					{
						result = FJItemLogInfo.UserBan_Open(operateUserID, serverIP, city, account, reason);
						if (result == -1)
						{
							SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + account + "的帐号已被解封");
							Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + account + "帐号不存在!");
							return Message.COMMON_MES_RESP("该玩家帐号已被解封!", Msg_Category.FJ_ADMIN, ServiceKey.FJ_AccountGold_Update_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
						}

						else if (result == 1)
						{	
						
							SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + account + "帐号解封成功!");
							Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + account + "帐号解封成功!");
							return Message.COMMON_MES_RESP("SUCESS", Msg_Category.FJ_ADMIN, ServiceKey.FJ_AccountGold_Update_RESP);

						}
						else
						{
							SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + account + "帐号解封失败!");
							Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + account + "帐号解封失败!");
							return Message.COMMON_MES_RESP("FAILURE", Msg_Category.FJ_ADMIN, ServiceKey.FJ_AccountGold_Update_RESP);
						}
					}
					else
					{
						return Message.COMMON_MES_RESP("该玩家帐号内虚拟资金不足,不予于解封!", Msg_Category.FJ_ADMIN, ServiceKey.FJ_AccountGold_Update_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
					}
				}
				else
				{
					return Message.COMMON_MES_RESP("该玩家帐号内虚拟资金不足,不予于解封!", Msg_Category.FJ_ADMIN, ServiceKey.FJ_AccountGold_Update_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
			}
			catch
			{
				return Message.COMMON_MES_RESP("该玩家帐号内虚拟资金不足,不予于解封", Msg_Category.FJ_ADMIN,ServiceKey.FJ_AccountGold_Update_RESP,TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 有条件停封服务器玩家帐号
		/// </summary>
		/// <returns></returns>
		public Message FJ_AccountTimeClose_Update()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string city = null;
			string account = null;
			string accountState = null;
			string reason = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				city = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserID).m_bValueBuffer);
				accountState = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Style).m_bValueBuffer);
				reason = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Reason).m_bValueBuffer);
				result = FJItemLogInfo.UserBanTime_Close(operateUserID, serverIP, city, account,Convert.ToInt32(accountState),reason);
				if (result == -1)
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + account + "的帐号已被停封!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + account + "的帐号不存在!");
					return Message.COMMON_MES_RESP("该帐号已被停封！", Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserBanTime_Create_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
				else if (result == 1)
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + account + "帐号停封成功!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + account + "帐号停封成功!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserBanTime_Create_RESP);
				}
				else
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + account + "帐号停封失败!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + account + "帐号停封失败!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserBanTime_Create_RESP);
				}
			}
			catch (System.Exception e)
			{
				return Message.COMMON_MES_RESP("停封失败", Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserBanTime_Create_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 停封服务器玩家帐号
		/// </summary>
		/// <returns></returns>
		public Message FJ_AccountClose_Update()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string city = null;
			string account = null;
			string reason = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				city = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserID).m_bValueBuffer);
				reason = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Reason).m_bValueBuffer);
				result = FJItemLogInfo.UserBan_Close(operateUserID, serverIP, city, account, reason);
				if (result == -1)
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + account + "的帐号已被停封!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + account + "的帐号不存在!");
					return Message.COMMON_MES_RESP("该帐号已被停封！", Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserBan_Close_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
				else if (result == 1)
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + account + "帐号停封成功!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + account + "帐号停封成功!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserBan_Close_Resp);
				}
				else
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + account + "帐号停封失败!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + account + "帐号停封失败!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserBan_Close_Resp);
				}
			}
			catch (System.Exception e)
			{
				return Message.COMMON_MES_RESP("停封失败", Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserBan_Close_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 查询GS服务器
		/// </summary>
		/// <returns></returns>
		public Message FJ_GSName_Query()
		{
			DataSet ds = null;
			string serverIP = null;
			string city = null;
			try
			{
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				city = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "GS服务器信息!");
				Console.WriteLine(DateTime.Now + " - 浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "GS服务器信息");
				ds = FJItemLogInfo.FJ_GS_Query(serverIP, city);
				Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
				if (ds != null && ds.Tables[0].Rows.Count > 0)
				{
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.FJ_GSName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_GS_Query_Resp, 1);

				}
				else
				{
					return Message.COMMON_MES_RESP("没有GS服务器信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_GS_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch (System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP" + serverIP + ex.Message);
				return Message.COMMON_MES_RESP("没有GS服务器信息!", Msg_Category.FJ_ADMIN, ServiceKey.FJ_GS_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 公告信息查询
		/// </summary>
		/// <returns></returns>
		public Message FJ_BoardList_Query()
		{
			string serverIP = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "公告信息!");
				Console.WriteLine(DateTime.Now + " - 浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "公告信息!");
				ds = FJItemLogInfo.BoardList_Query(serverIP);
				if (ds != null && ds.Tables[0].Rows.Count > 0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.FJ_MsgID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][1].ToString());
						strut.AddTagKey(TagName.FJ_GSName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][2].ToString());
						strut.AddTagKey(TagName.FJ_MsgContent, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]));
						strut.AddTagKey(TagName.FJ_BoardFlag, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, Convert.ToString(ds.Tables[0].Rows[i].ItemArray[4]));
						strut.AddTagKey(TagName.FJ_StartTime, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[5]));
						strut.AddTagKey(TagName.FJ_Interval, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, Convert.ToString(ds.Tables[0].Rows[i].ItemArray[6]));
						strut.AddTagKey(TagName.FJ_EndTime, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_Task_Query_Resp, 7);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有公告信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_Task_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch (System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP" + serverIP + ex.Message);
				return Message.COMMON_MES_RESP("没有公告信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_Task_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 添加公告内容
		/// </summary>
		/// <returns></returns>
		public Message FJ_BoardList_Insert()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string city = null;
			string gsName = null;
			string gsDesc = null;
			int interval = 0;
			string msgContent = null;
			string startDate = "";
			string endDate = "";
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				city = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
				gsName = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_GSName).m_bValueBuffer);
				gsDesc = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Message).m_bValueBuffer);
				startDate = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_StartTime).m_bValueBuffer);
				endDate = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_EndTime).m_bValueBuffer);
				strut = new TLV_Structure(TagName.FJ_Interval, 4, msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Interval).m_bValueBuffer);
				interval = (int)strut.toInteger();
				msgContent = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_MsgContent).m_bValueBuffer);
				result = FJItemLogInfo.FJBoardList_Insert(operateUserID, serverIP, city, gsName, gsDesc, msgContent, startDate, endDate, interval);
				if (result == 1)
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "公告添加成功!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "公告添加成功!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.FJ_ADMIN, ServiceKey.FJ_BoardList_Insert_Resp);
				}
				else
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "公告失败!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "公告添加失败!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.FJ_ADMIN, ServiceKey.FJ_BoardList_Insert_Resp);
				}
			}
			catch (System.Exception e)
			{
				return Message.COMMON_MES_RESP("公告添加失败", Msg_Category.FJ_ADMIN, ServiceKey.FJ_BoardList_Insert_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 删除公告内容
		/// </summary>
		/// <returns></returns>
		public Message FJ_BoardList_Delete()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string city = null;
			int taskID = 0;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				city = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
				strut = new TLV_Structure(TagName.FJ_MsgID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.FJ_MsgID).m_bValueBuffer);
				taskID = (int)strut.toInteger();
				result = FJItemLogInfo.FJBoardList_Delete(operateUserID, serverIP, city, taskID);
				if (result == 1)
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "公告删除成功!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "公告删除成功!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.FJ_ADMIN, ServiceKey.FJ_BoardList_Delete_Resp);
				}
				else
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "公告删除失败!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "公告删除失败!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.FJ_ADMIN, ServiceKey.FJ_BoardList_Delete_Resp);
				}
			}
			catch (System.Exception e)
			{
				return Message.COMMON_MES_RESP("公告删除失败", Msg_Category.FJ_ADMIN, ServiceKey.FJ_BoardList_Delete_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 玩家任务查询
		/// </summary>
		/// <returns></returns>
		public Message Task_Query()
		{
			string serverIP = null;
			string charName = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				charName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserNick).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "玩家" + charName + "任务信息!");
				Console.WriteLine(DateTime.Now + " - 浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "玩家" + charName + "任务信息!");

				ds = FJItemLogInfo.Task_Query(serverIP, charName);
				if (ds != null && ds.Tables[0].Rows.Count > 0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.FJ_UserNick, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][1].ToString());
						strut.AddTagKey(TagName.FJ_Quest_id, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][2].ToString());
						strut.AddTagKey(TagName.FJ_Quest_state, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.FJ_Content, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_Task_Query_Resp, 4);
				}
				else
				{
					return Message.COMMON_MES_RESP("该玩家没有任务信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_Task_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch (System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP" + serverIP + ex.Message);
				return Message.COMMON_MES_RESP("该玩家没有任务信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_Task_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 任务列表查询
		/// </summary>
		/// <returns></returns>
		public Message Quest_Query()
		{
			int level = 0;
			DataSet ds = null;
			try
			{
				TLV_Structure struts = new TLV_Structure(TagName.FJ_Level, 4, msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Level).m_bValueBuffer);
				level = (int)struts.toInteger();
				SqlHelper.log.WriteLog("浏览风火之旅+>任务列表信息!");
				Console.WriteLine(DateTime.Now + " - 浏览风火之旅+>任务列表信息!");

				ds = FJItemLogInfo.Quest_Query(level);
				if (ds != null && ds.Tables[0].Rows.Count > 0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.FJ_GuidID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][1].ToString());
						strut.AddTagKey(TagName.FJ_GuildName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_QuestTable_Query_Resp, 2);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有任务列表信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_QuestTable_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP("没有任务列表信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_QuestTable_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}

		/// <summary>
		/// 玩家任务更新
		/// </summary>
		/// <returns></returns>
		public Message FJ_Task_Update()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string nickName = null;
			int taskID = 0;
			int taskState = 0;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				nickName = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserNick).m_bValueBuffer);
				taskID = int.Parse(System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Quest_id).m_bValueBuffer));
				taskState = int.Parse(System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Quest_state).m_bValueBuffer));
				result = FJItemLogInfo.Task_Update(operateUserID, serverIP, nickName, taskID, taskState);
				if (result == 1)
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + nickName + "任务更新成功!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + nickName + "任务更新成功!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.FJ_ADMIN, ServiceKey.FJ_Task_Update_Resp);
				}
				else
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + nickName + "任务更新失败!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + nickName + "任务更新失败!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.FJ_ADMIN, ServiceKey.FJ_Task_Update_Resp);
				}
			}
			catch (System.Exception e)
			{
				return Message.COMMON_MES_RESP("任务更新失败", Msg_Category.FJ_ADMIN, ServiceKey.FJ_Task_Update_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}

		/// <summary>
		/// 玩家任务添加
		/// </summary>
		/// <returns></returns>
		public Message FJ_Task_Insert()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string nickName = null;
			int taskID = 0;
			int taskState = 0;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				nickName = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserNick).m_bValueBuffer);
				taskID = int.Parse(System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Quest_id).m_bValueBuffer));
				taskState = int.Parse(System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Quest_state).m_bValueBuffer));
				result = FJItemLogInfo.Task_Insert(operateUserID, serverIP, nickName, taskID, taskState);
				if (result == 1)
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + nickName + "任务添加成功!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + nickName + "任务添加成功!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.FJ_ADMIN, ServiceKey.FJ_Task_Insert_Resp);
				}
				else
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + nickName + "任务添加失败!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + nickName + "任务添加失败!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.FJ_ADMIN, ServiceKey.FJ_Task_Insert_Resp);
				}
			}
			catch (System.Exception e)
			{
				return Message.COMMON_MES_RESP("任务添加失败", Msg_Category.FJ_ADMIN, ServiceKey.FJ_Task_Insert_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 玩家任务删除
		/// </summary>
		/// <returns></returns>
		public Message FJ_Task_Delete()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string nickName = null;
			int taskID = 0;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				nickName = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserNick).m_bValueBuffer);
				taskID = int.Parse(System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Quest_id).m_bValueBuffer));
				result = FJItemLogInfo.Task_Delete(operateUserID, serverIP, nickName, taskID);
				if (result == 1)
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + nickName + "任务删除成功!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + nickName + "任务删除成功!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.FJ_ADMIN, ServiceKey.FJ_Task_Delete_Resp);
				}
				else
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + nickName + "任务删除失败!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + nickName + "任务删除失败!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.FJ_ADMIN, ServiceKey.FJ_Task_Delete_Resp);
				}
			}
			catch (System.Exception e)
			{
				return Message.COMMON_MES_RESP("任务删除失败", Msg_Category.FJ_ADMIN, ServiceKey.FJ_Task_Delete_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 一个服务器内，同一个IP下，登陆的账号数≥10
		/// </summary>
		/// <returns></returns>
		public Message UserLoginCount_Query(int index,int pageSize)
		{
			string city = null;
			DateTime beginDate;
			DateTime endDate;
			DataSet ds = null;
			try
			{
				city = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
				TLV_Structure struts = new TLV_Structure(TagName.FJ_StartTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.FJ_StartTime).m_bValueBuffer);
				beginDate = struts.toDate();
				struts = new TLV_Structure(TagName.FJ_EndTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.FJ_EndTime).m_bValueBuffer);
				endDate = struts.toDate();
				SqlHelper.log.WriteLog("浏览风火之旅+>一个服务器内，同一个IP下，登陆的账号数≥10!");
				Console.WriteLine(DateTime.Now + " - 一个服务器内，同一个IP下，登陆的账号数≥10!");

				ds = FJItemLogInfo.UserLoginCount_Query(city,beginDate,endDate);
				if (ds != null && ds.Tables[0].Rows.Count > 0)
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
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, Convert.ToString(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.FJ_UserID, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][1].ToString());
						strut.AddTagKey(TagName.ServerInfo_City, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][2].ToString());
						strut.AddTagKey(TagName.FJ_LoginIP, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][3].ToString());
						strut.AddTagKey(TagName.FJ_LoginTime, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						//总页数
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i-index] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserLoginCount_Query_RESP,5);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有相关帐号信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserLoginCount_Query_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP("没有相关帐号信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserLoginCount_Query_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 查询玩家日志类型
		/// </summary>
		/// <returns></returns>
		public Message ItemLogType_Query()
		{
			DataSet ds = null;
			SqlHelper.log.WriteLog("浏览风火之旅+>日志类型!");
			Console.WriteLine(DateTime.Now + " - 浏览风火之旅+>日志类型!");
			try
			{
				ds = FJItemLogInfo.ItemLogType_Query();
				if (ds != null && ds.Tables[0].Rows.Count > 0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.FJ_Type, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][1].ToString());
						strut.AddTagKey(TagName.FJ_Content, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_ItemLogType_Query_Resp, 2);
				}
				else
				{
					return Message.COMMON_MES_RESP("该玩家没有日志类型", Msg_Category.FJ_ADMIN, ServiceKey.FJ_ItemLogType_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch (System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP("该玩家没有日志类型", Msg_Category.FJ_ADMIN, ServiceKey.FJ_ItemLogType_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}


		}
		/// <summary>
		/// 查询玩家日志
		/// </summary>
		/// <returns></returns>
		public Message ItemLog_Query(int index, int pageSize)
		{
			string serverIP = null;
			string city = null;
			string charName = null;
			string itemName = null;
			int actionType = 0;
			int fjtype = 0;
			string tableName = null;
			DateTime beginDate = new DateTime();
			DateTime endDate = new DateTime();
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				city = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
				charName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserNick).m_bValueBuffer);
				itemName = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ItemName).m_bValueBuffer);
				tableName = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Relate).m_bValueBuffer);
				TLV_Structure tlvstrut = new TLV_Structure(TagName.FJ_ActionType, 4, msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ActionType).m_bValueBuffer);
				actionType = (int)tlvstrut.toInteger();
				tlvstrut = new TLV_Structure(TagName.FJ_StartTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.FJ_StartTime).m_bValueBuffer);
				beginDate = tlvstrut.toDate();
				tlvstrut = new TLV_Structure(TagName.FJ_EndTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.FJ_EndTime).m_bValueBuffer);
				endDate = tlvstrut.toDate();
				tlvstrut = new TLV_Structure(TagName.FJ_Type, 4, msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Type).m_bValueBuffer);
				fjtype = (int)tlvstrut.toInteger();
				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "玩家" + charName + "详细日志!");
				Console.WriteLine(DateTime.Now + " - 浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "玩家" + charName + "详细日志!");
				ds = FJItemLogInfo.ItemLog_Query(serverIP, city, charName, tableName, itemName, actionType, fjtype, beginDate, endDate);
				if (ds != null && ds.Tables[0].Rows.Count > 0)
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
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length + 6);
						if (fjtype == 100)
						{
							byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
							strut.AddTagKey(TagName.FJ_GuidID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i][1].ToString()));
							strut.AddTagKey(TagName.FJ_SendTime, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][2].ToString());
							strut.AddTagKey(TagName.FJ_Sender, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][3].ToString());
							strut.AddTagKey(TagName.FJ_Receiver, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][4].ToString());
							strut.AddTagKey(TagName.FJ_Title, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[5]);
							strut.AddTagKey(TagName.FJ_Content, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							DataSet nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[6]));
							string name = "";
							string color = "";
							if (nameDS != null && nameDS.Tables[0].Rows.Count > 0)
							{
								name = nameDS.Tables[0].Rows[0][0].ToString();
								color = nameDS.Tables[0].Rows[0][1].ToString();
							}
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, name);
							strut.AddTagKey(TagName.FJ_item_0_guid, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, color);
							strut.AddTagKey(TagName.FJ_Color0, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[7]);
							strut.AddTagKey(TagName.FJ_item_0_Num, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
							nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[8]));
							name = "";
							color = "";
							if (nameDS != null && nameDS.Tables[0].Rows.Count > 0)
							{
								name = nameDS.Tables[0].Rows[0][0].ToString();
								color = nameDS.Tables[0].Rows[0][1].ToString();
							}
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, name);
							strut.AddTagKey(TagName.FJ_item_1_guid, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, color);
							strut.AddTagKey(TagName.FJ_Color1, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[9]);
							strut.AddTagKey(TagName.FJ_item_1_Num, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
							nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[10]));
							name = "";
							color = "";
							if (nameDS != null && nameDS.Tables[0].Rows.Count > 0)
							{
								name = nameDS.Tables[0].Rows[0][0].ToString();
								color = nameDS.Tables[0].Rows[0][1].ToString();
							}
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, name);
							strut.AddTagKey(TagName.FJ_item_2_guid, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, color);
							strut.AddTagKey(TagName.FJ_Color2, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[11]);
							strut.AddTagKey(TagName.FJ_item_2_Num, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
							nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[12]));
							name = "";
							color = "";
							if (nameDS != null && nameDS.Tables[0].Rows.Count > 0)
							{
								name = nameDS.Tables[0].Rows[0][0].ToString();
								color = nameDS.Tables[0].Rows[0][1].ToString();
							}
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, name);
							strut.AddTagKey(TagName.FJ_item_3_guid, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, color);
							strut.AddTagKey(TagName.FJ_Color3, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[13]);
							strut.AddTagKey(TagName.FJ_item_3_Num, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
							nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[14]));
							name = "";
							color = "";
							if (nameDS != null && nameDS.Tables[0].Rows.Count > 0)
							{
								name = nameDS.Tables[0].Rows[0][0].ToString();
								color = nameDS.Tables[0].Rows[0][1].ToString();
							}
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, name);
							strut.AddTagKey(TagName.FJ_item_4_guid, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, color);
							strut.AddTagKey(TagName.FJ_Color4, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[15]);
							strut.AddTagKey(TagName.FJ_item_4_Num, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[16]);
							strut.AddTagKey(TagName.FJ_iMoney, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
							//总页数
							strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
							structList[i - index] = strut;

						}
						else
						{

							byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
							strut.AddTagKey(TagName.FJ_GSName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][1].ToString());
							strut.AddTagKey(TagName.FJ_UserNick, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][2].ToString());
							strut.AddTagKey(TagName.FJ_ItemName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i][3].ToString()));
							strut.AddTagKey(TagName.FJ_ItemCount, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
							int color = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]);
							string colorDesc = "";
							switch (color)
							{
								case 1:
									colorDesc = "白色";
									break;
								case 2:
									colorDesc = "绿色";
									break;
								case 3:
									colorDesc = "蓝色";
									break;
								case 4:
									colorDesc = "紫色";
									break;
								case 5:
									colorDesc = "金色";
									break;
							}
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, colorDesc);
							strut.AddTagKey(TagName.FJ_Color, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i][5]));
							strut.AddTagKey(TagName.FJ_Act_Time, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[6]);
							strut.AddTagKey(TagName.FJ_RelateCHarName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, Convert.ToString(ds.Tables[0].Rows[i].ItemArray[7]));
							strut.AddTagKey(TagName.FJ_iMoney, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, Convert.ToString(ds.Tables[0].Rows[i].ItemArray[8]));
							strut.AddTagKey(TagName.FJ_LeftMoney, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, Convert.ToString(ds.Tables[0].Rows[i].ItemArray[9]));
							strut.AddTagKey(TagName.FJ_FactoryMark, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, Convert.ToString(ds.Tables[0].Rows[i].ItemArray[10]));
							strut.AddTagKey(TagName.FJ_ConsumeCredit, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, Convert.ToString(ds.Tables[0].Rows[i].ItemArray[11]));
							strut.AddTagKey(TagName.FJ_LeftCredit, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
							//总页数
							strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
							structList[i - index] = strut;
						}
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_ItemLog_Query_Resp, ds.Tables[0].Rows[0].ItemArray.Length + 1);
				}
				else
				{
					return Message.COMMON_MES_RESP("该玩家没有日志信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_ItemLog_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch (System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP" + serverIP + ex.Message);
				return Message.COMMON_MES_RESP("该玩家没有日志信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_ItemLog_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// GM命令封停帐号信息
		/// </summary>
		/// <returns></returns>
		public Message FJ_GMUserBan_Query(int index, int pageSize)
		{
			string serverIP = null;
			string city = null;
			string account = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				city = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserID).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "玩家" + account + "GM命令封停帐号信息!");
				Console.WriteLine(DateTime.Now + " - 浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "玩家" + account + "GM命令封停帐号信息!");
				//请求所有分类道具列表
				ds = FJItemLogInfo.GMUserBanLog_Query(serverIP, city, account);
				if (ds != null && ds.Tables[0].Rows.Count > 0)
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
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length + 1);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.FJ_GuidID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][1].ToString());
						strut.AddTagKey(TagName.FJ_UserID, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i][2].ToString()));
						strut.AddTagKey(TagName.FJ_BanDate, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][3]);
						strut.AddTagKey(TagName.FJ_GMAccountName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						//总页数
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));

						structList[i - index] = strut;

					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_GMUserBan_Query_RESP, 5);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有GM命令封停的帐号信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_GMUserBan_Query_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
			}
			catch (System.Exception ex)
			{
				return Message.COMMON_MES_RESP("没有GM命令封停的帐号信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_GMUserBan_Query_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// GM帐号的log记录查询
		/// </summary>
		/// <returns></returns>
		public Message FJ_GMUserLog_Query(int index, int pageSize)
		{
			string serverIP = null;
			string city = null;
			string charName = null;
			DateTime beginDate = new DateTime();
			DateTime endDate = new DateTime();
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				city = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
				charName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserNick).m_bValueBuffer);
				TLV_Structure tlvstrut = new TLV_Structure(TagName.FJ_StartTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.FJ_StartTime).m_bValueBuffer);
				beginDate = tlvstrut.toDate();
				tlvstrut = new TLV_Structure(TagName.FJ_EndTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.FJ_EndTime).m_bValueBuffer);
				endDate = tlvstrut.toDate();
				tlvstrut = new TLV_Structure(TagName.FJ_ActionType, 4, msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ActionType).m_bValueBuffer);
				int actionType = (int)tlvstrut.toInteger();
				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "玩家" + charName + "GM帐号操作的日志!");
				Console.WriteLine(DateTime.Now + " - 浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "玩家" + charName + "GM帐号操作的日志!");
				//请求所有分类道具列表
				ds = FJItemLogInfo.GMLog_Query(serverIP, city,charName,actionType, beginDate, endDate);
				if (ds != null && ds.Tables[0].Rows.Count > 0)
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
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length + 1);
						//byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
						//strut.AddTagKey(TagName.FJ_GuidID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][0].ToString());
						strut.AddTagKey(TagName.FJ_Server_Name, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][1].ToString());
						strut.AddTagKey(TagName.FJ_UserNick, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i][2]));
						strut.AddTagKey(TagName.FJ_Act_Time, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, Convert.ToString(ds.Tables[0].Rows[i].ItemArray[3]));
						strut.AddTagKey(TagName.FJ_XPosition, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, Convert.ToString(ds.Tables[0].Rows[i].ItemArray[4]));
						strut.AddTagKey(TagName.FJ_YPosition, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, Convert.ToString(ds.Tables[0].Rows[i].ItemArray[5]));
						strut.AddTagKey(TagName.FJ_ZPosition, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[6]);
						strut.AddTagKey(TagName.FJ_RelateCHarName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[7]);
						strut.AddTagKey(TagName.FJ_Command_Content, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						//总页数
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));

						structList[i - index] = strut;

					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_GMUser_Qyery, 9);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有该GM帐号的LOG日志", Msg_Category.FJ_ADMIN, ServiceKey.FJ_GMUser_Qyery, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
			}
			catch (System.Exception ex)
			{
				return Message.COMMON_MES_RESP("没有该GM帐号的LOG日志", Msg_Category.FJ_ADMIN, ServiceKey.FJ_GMUser_Qyery, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// GM帐号的添加删除、权限等级设定
		/// </summary>
		/// <returns></returns>
		public Message FJ_GMUser_Update()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string city = null;
			string account = null;
			int power = 0;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				city = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserID).m_bValueBuffer);
				power = int.Parse(System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Quest_id).m_bValueBuffer));
				result = FJItemLogInfo.GMAccount_Update(operateUserID, serverIP, city, account, power);
				if (result == 1)
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + account + "添加GM帐号成功!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + account + "添加GM帐号成功!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.FJ_ADMIN, ServiceKey.FJ_GMUser_Update_Resp);
				}
				else
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + account + "添加GM帐号失败!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + account + "添加GM帐号失败!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.FJ_ADMIN, ServiceKey.FJ_GMUser_Update_Resp);
				}
			}
			catch (System.Exception e)
			{
				return Message.COMMON_MES_RESP("GM帐号添加失败", Msg_Category.FJ_ADMIN, ServiceKey.FJ_GMUser_Update_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 玩家使用外挂信息查询
		/// </summary>
		/// <returns></returns>
		public Message KillUser_Query(int index, int pageSize)
		{
			string serverIP = null;
			string city = null;
			string account = null;
			string tableName = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserID).m_bValueBuffer);
				tableName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Title).m_bValueBuffer);
				city = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "玩家" + account + "使用外挂信息!");
				Console.WriteLine(DateTime.Now + " - 浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "玩家" + account + "使用外挂信息!");

				ds = FJItemLogInfo.UserKill_Query(serverIP, account, tableName, city);
				if (ds != null && ds.Tables[0].Rows.Count > 0)
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
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length + 1);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.FJ_LoginTime, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][1].ToString());
						strut.AddTagKey(TagName.FJ_UserID, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i][2].ToString()));
						strut.AddTagKey(TagName.FJ_Level, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]));
						strut.AddTagKey(TagName.FJ_dwProcessID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[4]));
						strut.AddTagKey(TagName.FJ_BanDate, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[5]));
						strut.AddTagKey(TagName.FJ_dwErrorID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[6]));
						strut.AddTagKey(TagName.FJ_dwHandle, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, Convert.ToString(ds.Tables[0].Rows[i].ItemArray[7]));
						strut.AddTagKey(TagName.FJ_GSName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						//总页数
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i - index] = strut;

					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserKill_Query_Resp, 9);
				}
				else
				{
					return Message.COMMON_MES_RESP("该玩家没有使用外挂", Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserKill_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch (System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP" + serverIP + ex.Message);
				return Message.COMMON_MES_RESP("该玩家没有使用外挂", Msg_Category.FJ_ADMIN, ServiceKey.FJ_UserKill_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 查询风火活动卡使用情况
		/// </summary>
		/// <returns></returns>
		public Message FJ_AccountReward_Query()
		{
			string serverIP = null;
			string account = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserID).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "风火英雄卡信息!");
				Console.WriteLine(DateTime.Now + " - 浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "风火英雄卡信息!");
				ds = FJItemLogInfo.FJ_AccountReward__Query(serverIP, account);
				if (ds != null && ds.Tables[0].Rows.Count > 0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.FJ_UserID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][1].ToString());
						strut.AddTagKey(TagName.FJ_UserNick, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][2].ToString());
						strut.AddTagKey(TagName.FJ_GSName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]));
						strut.AddTagKey(TagName.FJ_Level, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, Convert.ToString(ds.Tables[0].Rows[i].ItemArray[4]));
						strut.AddTagKey(TagName.FJ_StartTime, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[5]));
						strut.AddTagKey(TagName.FJ_Interval, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, Convert.ToString(ds.Tables[0].Rows[i].ItemArray[6]));
						strut.AddTagKey(TagName.FJ_EndTime, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_AccountReward_Query_Resp, 7);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有英雄卡信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_AccountReward_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch (System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP" + serverIP + ex.Message);
				return Message.COMMON_MES_RESP("没有英雄卡信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_AccountReward_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 查询风火活动卡使用情况
		/// </summary>
		/// <returns></returns>
		public Message FJ_RewardInfo_Query()
		{
			string serverIP = null;
			string city = null;
			string account = null;
			string style = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				city = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserID).m_bValueBuffer);
				style = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Style).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "风火详细英雄卡道具领取情况!");

				Console.WriteLine(DateTime.Now + " - 浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "风火详细英雄卡道具领取情况!");
				ds = FJItemLogInfo.FJ_RewardInfo_Query(serverIP, city, account, style);
				if (ds != null && ds.Tables[0].Rows.Count > 0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.FJ_UserID, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						string userNick = "";
						if (ds.Tables[0].Rows[i].IsNull(1) == false)
							userNick = ds.Tables[0].Rows[i][1].ToString();
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, userNick);
						strut.AddTagKey(TagName.FJ_UserNick, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						string gs_name = "";
						if (ds.Tables[0].Rows[i].IsNull(2) == false)
							gs_name = ds.Tables[0].Rows[i][2].ToString();
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, gs_name);
						strut.AddTagKey(TagName.FJ_GSName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						int lvl = 0;
						if (ds.Tables[0].Rows[i].IsNull(3) == false)
							lvl = Convert.ToInt32(ds.Tables[0].Rows[i][3]);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, lvl);
						strut.AddTagKey(TagName.FJ_Level, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						DateTime getTime = new DateTime();
						if (ds.Tables[0].Rows[i].IsNull(4) == false)
							getTime = Convert.ToDateTime(ds.Tables[0].Rows[i][4]);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, getTime);
						strut.AddTagKey(TagName.FJ_reward_get_time, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[5]);
						strut.AddTagKey(TagName.FJ_activity_name, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i][6].ToString()));
						strut.AddTagKey(TagName.FJ_select_num, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i][7].ToString()));
						strut.AddTagKey(TagName.FJ_MinLevel, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[8]));
						strut.AddTagKey(TagName.FJ_Max_lvl, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						DataSet nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[9]));
						string name = "";
						if (nameDS != null && nameDS.Tables[0].Rows.Count > 0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, name);
						strut.AddTagKey(TagName.FJ_item_0_guid, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[10]));
						strut.AddTagKey(TagName.FJ_item_0_Num, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[11]));
						name = "";
						if (nameDS != null && nameDS.Tables[0].Rows.Count > 0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, name);
						strut.AddTagKey(TagName.FJ_item_1_guid, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[12]));
						strut.AddTagKey(TagName.FJ_item_1_Num, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[13]));
						name = "";
						if (nameDS != null && nameDS.Tables[0].Rows.Count > 0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, name);
						strut.AddTagKey(TagName.FJ_item_2_guid, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[14]));
						strut.AddTagKey(TagName.FJ_item_2_Num, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[15]));
						name = "";
						if (nameDS != null && nameDS.Tables[0].Rows.Count > 0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, name);
						strut.AddTagKey(TagName.FJ_item_3_guid, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[16]));
						strut.AddTagKey(TagName.FJ_item_3_Num, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[17]));
						name = "";
						if (nameDS != null && nameDS.Tables[0].Rows.Count > 0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, name);
						strut.AddTagKey(TagName.FJ_item_4_guid, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[18]));
						strut.AddTagKey(TagName.FJ_item_4_Num, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[19]));
						name = "";
						if (nameDS != null && nameDS.Tables[0].Rows.Count > 0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, name);
						strut.AddTagKey(TagName.FJ_item_5_guid, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[20]));
						strut.AddTagKey(TagName.FJ_item_5_Num, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[21]));
						name = "";
						if (nameDS != null && nameDS.Tables[0].Rows.Count > 0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, name);
						strut.AddTagKey(TagName.FJ_item_6_guid, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[22]));
						strut.AddTagKey(TagName.FJ_item_6_Num, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[23]));
						name = "";
						if (nameDS != null && nameDS.Tables[0].Rows.Count > 0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, name);
						strut.AddTagKey(TagName.FJ_item_7_guid, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[24]));
						strut.AddTagKey(TagName.FJ_item_7_Num, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_RewardDetail_Query_Resp, 25);
				}
				else
				{
					return Message.COMMON_MES_RESP("没有详细英雄卡道具领取情况", Msg_Category.FJ_ADMIN, ServiceKey.FJ_RewardDetail_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch (System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP" + serverIP + ex.Message);
				return Message.COMMON_MES_RESP("没有详细英雄卡道具领取情况", Msg_Category.FJ_ADMIN, ServiceKey.FJ_RewardDetail_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 查询玩家充值信息
		/// </summary>
		/// <returns></returns>
		public Message FJ_AccountCharage_Query(int index, int pageSize)
		{
			string serverIP = null;
			string city = null;
			string account = null;
			string tableName = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserID).m_bValueBuffer);
				city = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "玩家" + account + "充值信息!");
				Console.WriteLine(DateTime.Now + " - 浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "玩家" + account + "充值信息!");

				ds = FJItemLogInfo.FJ_AccountDeposit_Query(serverIP, account, city);
				if (ds != null && ds.Tables[0].Rows.Count > 0)
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
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length + 1);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.FJ_PaySN, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][1].ToString());
						strut.AddTagKey(TagName.FJ_UserID, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i][2].ToString()));
						strut.AddTagKey(TagName.FJ_Option_Time, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, Convert.ToString(ds.Tables[0].Rows[i].ItemArray[3]));
						strut.AddTagKey(TagName.FJ_Credit, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[4]));
						strut.AddTagKey(TagName.FJ_Add_Time, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[5]));
						strut.AddTagKey(TagName.FJ_Add_Ok, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						//总页数
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i - index] = strut;

					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_AccountDeposit_Query_Resp, 7);
				}
				else
				{
					return Message.COMMON_MES_RESP("该玩家没有充值信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_AccountDeposit_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch (System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP" + serverIP + ex.Message);
				return Message.COMMON_MES_RESP("该玩家没有充值信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_AccountDeposit_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 重置玩家的充值信息
		/// </summary>
		/// <returns></returns>
		public Message FJ_AccountCharage_Update()
		{
			int result = -1;
			int operateUserID = 0;
			int paySN = 0;
			string serverIP = null;
			string city = null;
			string account = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.FJ_UserIndexID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserIndexID).m_bValueBuffer);
				paySN = (int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				city = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);
				account = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserID).m_bValueBuffer);
				result = FJItemLogInfo.FJ_AccountDeposit_Update(operateUserID, serverIP, city, account, paySN);
				if (result == 1)
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + account + "重置玩家充值信息成功!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + account + "重置玩家充值信息成功!");
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.FJ_ADMIN, ServiceKey.FJ_AccountDeposit_Update_Resp);
				}
				else if (result == -1)
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + account + "重置玩家充值信息失败!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + account + "重置玩家充值信息失败!");
					return Message.COMMON_MES_RESP("对不起，你没有权限重置这项服务", Msg_Category.FJ_ADMIN, ServiceKey.FJ_AccountDeposit_Update_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
				else
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址" + serverIP + "玩家" + account + "重置玩家充值信息失败!");
					Console.WriteLine(DateTime.Now + " - 风火之旅+>服务器地址" + serverIP + "玩家" + account + "重置玩家充值信息失败!");
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.FJ_ADMIN, ServiceKey.FJ_AccountDeposit_Update_Resp);
				}
			}
			catch (System.Exception e)
			{
				return Message.COMMON_MES_RESP("重置失败", Msg_Category.FJ_ADMIN, ServiceKey.FJ_AccountDeposit_Update_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}

		/// <summary>
		/// 查询玩家回梦Log信息
		/// </summary>
		/// <returns></returns>
		public Message FJ_MakeDream_Query(int index, int pageSize)
		{
			string serverIP = null;
			string city = null;
			string account = null;

			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserNick).m_bValueBuffer);
				string querytime=Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.BeginTime).m_bValueBuffer);
				string logtype=Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Log_Type).m_bValueBuffer);
//				TLV_Structure strut1 = new TLV_Structure(TagName.FJ_ActionType, 4, msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ActionType).m_bValueBuffer);
//				int searchtype= (int)strut1.toInteger();
//				string EndTime=Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.EndTime).m_bValueBuffer);

				//city = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);

				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "玩家" + account + "查询玩家回梦Log信息!");
				Console.WriteLine(DateTime.Now + " - 浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "玩家" + account + "查询玩家回梦Log信息!");

				ds = FJItemLogInfo.FJ_MakeDream_Query(serverIP, account, querytime,logtype,"",0);

				if (ds != null && ds.Tables[0].Rows.Count > 0)
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
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length + 1);

						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0].ToString());
						strut.AddTagKey(TagName.ServerInfo_City, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][1].ToString());
						strut.AddTagKey(TagName.FJ_UserNick, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[2]));
						strut.AddTagKey(TagName.FJ_value1, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, Convert.ToString(ds.Tables[0].Rows[i].ItemArray[3]));
						strut.AddTagKey(TagName.FJ_Type, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[4]));
						strut.AddTagKey(TagName.MF_record_time, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						//总页数
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i - index] = strut;

					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_MakeDream_Query_RESP, 6);
				}
				else
				{
					return Message.COMMON_MES_RESP("该玩家没有充值信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_MakeDream_Query_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch (System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP" + serverIP + ex.Message);
				return Message.COMMON_MES_RESP("该玩家没有充值信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_MakeDream_Query_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}


		/// <summary>
		/// 查询玩家元宝划分时间信息
		/// </summary>
		/// <returns></returns>
		public Message FJ_CreditTime_Query(int index, int pageSize)
		{
			string serverIP = null;
			string city = null;
			string account = null;
			
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserID).m_bValueBuffer);
				string querytime=Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.BeginTime).m_bValueBuffer);
//				TLV_Structure strut1 = new TLV_Structure(TagName.FJ_ActionType, 4, msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ActionType).m_bValueBuffer);
//				int searchtype= (int)strut1.toInteger();
//				string EndTime=Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.EndTime).m_bValueBuffer);
				//city = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ServerInfo_City).m_bValueBuffer);

				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "玩家" + account + "元宝划分时间信息!");
				Console.WriteLine(DateTime.Now + " - 浏览风火之旅+>服务器地址" + CommonInfo.serverIP_Query(serverIP) + "玩家" + account + "元宝划分时间信息!");

				ds = FJItemLogInfo.FJ_CreditTime_Query(serverIP, account, querytime,"",0);

				if (ds != null && ds.Tables[0].Rows.Count > 0)
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
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length + 1);

						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0].ToString());
						strut.AddTagKey(TagName.ServerInfo_City, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i][1].ToString());
						strut.AddTagKey(TagName.FJ_UserID, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[2]));
						strut.AddTagKey(TagName.FJ_pre_credit, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]));
						strut.AddTagKey(TagName.FJ_post_credit, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]));
						strut.AddTagKey(TagName.FJ_pre_time, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[5]));
						strut.AddTagKey(TagName.FJ_post_time, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[6]));
						strut.AddTagKey(TagName.FJ_Option_Time, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						//总页数
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i - index] = strut;

					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_Credit_time_Query_RESP, 8);
				}
				else
				{
					return Message.COMMON_MES_RESP("该玩家没有充值信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_Credit_time_Query_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch (System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP" + serverIP + ex.Message);
				return Message.COMMON_MES_RESP("该玩家没有充值信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_Credit_time_Query_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
	}
}
