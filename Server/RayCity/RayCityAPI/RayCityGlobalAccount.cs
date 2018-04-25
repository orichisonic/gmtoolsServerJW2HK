using System;
using System.Text;
using System.Data;
using Common.Logic;
using Common.API;
using Common.DataInfo;
using System.Web.Mail;
using RayCity.RayCityDataInfo;
using lg = Common.API.LanguageAPI;
namespace RayCity.RayCityAPI
{
	/// <summary>
	/// RayCityGlobalAccount 的摘要说明。
	/// </summary>
	public class RayCityGlobalAccount
	{
		Message msg = null;
		public RayCityGlobalAccount(byte[] packets)
		{
			msg = new Message(packets,(uint)packets.Length);
		}
		/// <summary>
		/// 基本帳號資訊
		/// </summary>
		/// <returns></returns>
		public Message BasicAccount_Query(int index,int pageSize)
		{
			string serverIP = null;
			string account = null;
			string userNick = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_NyUserID).m_bValueBuffer);
				userNick = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_NyNickName).m_bValueBuffer);

				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+account+lg.RayCityAPI_AccountInfo);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+account+lg.RayCityAPI_AccountInfo);
				ds = GlobalAccount.BasicAccount_Query(serverIP,account,userNick);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_BasicAccount_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.RayCityAPI_NoAccountInfo,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_BasicAccount_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.RayCityAPI_NoAccountInfo, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_BasicAccount_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 基本玩家人物資料資訊
		/// </summary>
		/// <returns></returns>
		public Message BasicCharacter_Query()
		{
			string serverIP = null;
			int accountID = 0;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.RayCity_AccountID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_AccountID).m_bValueBuffer);
				accountID =(int)strut.toInteger();				
				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+accountID+lg.RayCityAPI_AccountInfo);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Account+accountID+lg.RayCityAPI_NoAccountInfo);
				ds = GlobalAccount.BasicCharacter_Query(serverIP,accountID);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,1,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_BasicCharacter_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.RayCityAPI_NoAccountInfo,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_BasicCharacter_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.RayCityAPI_NoAccountInfo, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_BasicCharacter_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		public Message CharacterName_Query()
		{
			string name = "";
			int characterID = 0;
			string serverIP="";
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.RayCity_CharacterID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_CharacterID).m_bValueBuffer);
				characterID =(int)strut.toInteger();	
				name= GlobalAccount.CharacterName_Query(serverIP,characterID);	
				return Message.COMMON_MES_RESP(name,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_CashItemDetailLog_Query_Resp,TagName.RayCity_CharacterName,TagFormat.TLV_STRING);
			}
			catch(System.Exception)
			{
				return Message.COMMON_MES_RESP(lg.API_Error, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_CashItemDetailLog_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);

			}

		}
		/// <summary>
		/// 查询所有帮会的資訊
		/// </summary>
		/// <returns></returns>
		public Message Guild_Query(int index ,int pageSize)
		{
			string serverIP = null;
			string guildName = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);				
				guildName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_GuildName).m_bValueBuffer);				

				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Guild);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Guild);
				ds = GlobalAccount.Guild_Query(serverIP,guildName);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_Guild_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.RayCityAPI_NoGuild,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_Guild_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.RayCityAPI_NoGuild, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_Guild_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 查询所有帮会成员的資訊
		/// </summary>
		/// <returns></returns>
		public Message Guildmember_Query()
		{
			string serverIP = null;
			int guildID =  0;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.RayCity_GuildIDX,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_GuildIDX).m_bValueBuffer);
				guildID =(int)strut.toInteger();				
				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_GuildMember);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_GuildMember);
				ds = GlobalAccount.GuildMember_Query(serverIP,guildID);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,100,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_GuildMember_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.RayCityAPI_NoGuildMember,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_GuildMember_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.RayCityAPI_NoGuildMember, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_GuildMember_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}

		/// <summary>
		/// 9you用户邮件地址查询
		/// </summary>
		/// <returns></returns>
		public Message SDOEmailQuery()
		{
			string  result = null;
			string account = null;
			try
			{
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
				result = GlobalAccount.SDOEmail_Query(account.Trim());
				if (result != null)
				{
					SqlHelper.log.WriteLog("久游用户中心+>玩家" + account + "邮件地址!");
					Console.WriteLine(DateTime.Now + " - 久游用户中心+>玩家" + account + "邮件地址!");
					return Message.COMMON_MES_RESP(result, Msg_Category.SDO_ADMIN, ServiceKey.SDO_EMAIL_QUERY_RESP, TagName.SDO_Email, TagFormat.TLV_STRING);
				}
				else
				{
					SqlHelper.log.WriteLog("久游用户中心+>玩家" + account + "邮件地址!");
					Console.WriteLine(DateTime.Now + " - 久游用户中心+>玩家" + account + "邮件地址!");
					return Message.COMMON_MES_RESP("該玩家EMAI為空!", Msg_Category.SDO_ADMIN, ServiceKey.SDO_EMAIL_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch (System.Exception ex)
			{
				string str = ex.Message;
				SqlHelper.log.WriteLog("久游用户中心+>玩家" + account + "邮件地址!");
				Console.WriteLine(DateTime.Now + " - 久游用户中心+>玩家" + account + "邮件地址!");
				return Message.COMMON_MES_RESP("該玩家EMAI為空!",Msg_Category.SDO_ADMIN, ServiceKey.SDO_EMAIL_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}

		#region 发送邮件函数--光线飞车仓库保护密码
		public static int sendRayCityMail(string sender, string receiver, string subject, string body)
		{
			try
			{
				try
				{
					MailMessage Message = new MailMessage();
					Message.To = receiver;
					Message.From = sender;
					string message= "亲爱的"+subject+"<br>";
					message += "&nbsp;&nbsp;&nbsp;&nbsp;欢迎您使用久游网的密码保护功能,您的光线飞车仓库保护密码是"+body+"   请牢记您的密码.<br>";
					message += "如果您仍然存在问题,请联系久游客户服务中心<br>";
					message += "网址：kefu.9you.com<br>";
					message += "客服热线: 021-63601518<br>";
					message += "电邮信箱: account_9you@staff.9you.com<br>";
					message +="如果您还有其他问题,可以登陆kefu.9you.com,我们将竭诚为您服务!<br>";
					string headers = "From: 久游网 <password@staff.9you.com>\r\n";
					Message.Priority = MailPriority.High;
					Message.BodyFormat = MailFormat.Html;
					Message.Subject = headers;
					Message.Body = message;
					try
					{
						SmtpMail.SmtpServer = "localhost";
						SmtpMail.Send(Message);
						return 1;
					}
					catch (System.Web.HttpException ehttp)
					{
						Console.WriteLine("{0}", ehttp.Message);
						Console.WriteLine("Here is the full error message output");
						Console.Write("{0}", ehttp.ToString());
						return 0;
					}
				}
				catch (IndexOutOfRangeException)
				{
					return 0;
                   
				}
			}
			catch (System.Exception e)
			{
				Console.WriteLine("Unknown Exception occurred {0}", e.Message);
				Console.WriteLine("Here is the Full Message output");
				Console.WriteLine("{0}", e.ToString());
				return 0;
			}

		}
		#endregion

		/// <summary>
		///  取回玩家光線飛車仓库密码
		/// </summary>
		/// <returns></returns>
		public Message Get_WarehousePassword()
		{
			int operateUserID = 0;
			string serverIP = null;
			string account  =null;
			string passwd = null;
			string email = null;
			int characterID = 0;
			int result = -1;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_NyUserID).m_bValueBuffer);
				email = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Email).m_bValueBuffer);
				string signpasswd = MD5EncryptAPI.nextID(6);
				MD5Encrypt st = new MD5Encrypt();
				passwd = st.getMD5ofStr(signpasswd).ToLower();
				strut = new TLV_Structure(TagName.RayCity_CharacterID,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_CharacterID).m_bValueBuffer);
				characterID =(int)strut.toInteger();
				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Set+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_WarehousePassword);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_Set+lg.RayCityAPI_Account+characterID+lg.RayCityAPI_WarehousePassword);
				result = GlobalAccount.Get_WarehousePassword(operateUserID,serverIP,characterID,account,signpasswd,passwd);	
				if(result==1)
				{
					if(sendRayCityMail("password@9youstaff.com",email,account,signpasswd)==1)
					  return Message.COMMON_MES_RESP("SCUESS",Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_WareHousePwd_Update_Resp);
					else
					  return Message.COMMON_MES_RESP("FAILURE",Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_WareHousePwd_Update_Resp);

				}
				else
				{
					return Message.COMMON_MES_RESP(lg.API_Error,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_WareHousePwd_Update_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.API_Error, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_WareHousePwd_Update_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		///  GM帳號查询，已封停和未封停
		/// </summary>
		/// <returns></returns>
		public Message GMAccount_Query(int index,int pageSize)
		{
			string serverIP = null;
			string account = null;
			int accountState = 0;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_NyUserID).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.RayCity_AccountState,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_AccountState).m_bValueBuffer);
				accountState =(int)strut.toInteger();	
				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_GMAccount+account+lg.RayCityAPI_BanAccount);
				Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.RayCityAPI_GMAccount+account+lg.RayCityAPI_BanAccount);
				ds = GlobalAccount.GMAccount_Query(serverIP,account,accountState);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_GMUser_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.API_Error,Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_GMUser_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.API_Error, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_GMUser_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 添加GM帳號
		/// </summary>
		/// <returns></returns>
		public Message GMAccount_Update()
		{
			int operateUserID = 0;
			string serverIP = null;
			string account  = null;
			int accountState =  0;
			int result = -1;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_NyUserID).m_bValueBuffer);
				strut = new TLV_Structure(TagName.RayCity_AccountState,4,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_AccountState).m_bValueBuffer);
				accountState =(int)strut.toInteger();			
				result = GlobalAccount.GMAccount_Update(operateUserID,serverIP,account,accountState);	
				if(result==1)
				{
					SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.API_Add+lg.RayCityAPI_GMAccount+account+lg.API_Success);
					Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.API_Add+lg.RayCityAPI_GMAccount+account+lg.API_Success);
					return Message.COMMON_MES_RESP("SUCCESS",Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_GMUser_Update_Resp,TagName.Status,TagFormat.TLV_STRING);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.API_Add+lg.RayCityAPI_GMAccount+account+lg.API_Failure);
					Console.WriteLine(DateTime.Now+lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address+CommonInfo.serverIP_Query(serverIP)+lg.API_Add+lg.RayCityAPI_GMAccount+account+lg.API_Failure);
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.RAYCITY_ADMIN,ServiceKey.RayCity_GMUser_Update_Resp,TagName.Status,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.API_Error, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_GMUser_Update_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 公告資訊查询
		/// </summary>
		/// <returns></returns>
		public Message RayCity_BoardList_Query()
		{
			string serverIP = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address + CommonInfo.serverIP_Query(serverIP) + lg.RayCityAPI_Notices);
				Console.WriteLine(DateTime.Now + lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address + CommonInfo.serverIP_Query(serverIP) + lg.RayCityAPI_Notices);
				ds = GlobalAccount.BoardList_Query(serverIP);
				if (ds != null && ds.Tables[0].Rows.Count > 0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.RayCity_NoticeID, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.RayCity_StartDate, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, Convert.ToDateTime(ds.Tables[0].Rows[i][2].ToString()));
						strut.AddTagKey(TagName.RayCity_EndDate, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i][3].ToString()));
						strut.AddTagKey(TagName.RayCity_Interval, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]));
						strut.AddTagKey(TagName.RayCity_Repeat, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, Convert.ToString(ds.Tables[0].Rows[i].ItemArray[5]));
						strut.AddTagKey(TagName.RayCity_Message, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, Convert.ToString(ds.Tables[0].Rows[i].ItemArray[6]));
						strut.AddTagKey(TagName.RayCity_use_state, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_BoardList_Query_Resp,7);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.RayCityAPI_NoNotices, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_BoardList_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}

			}
			catch (System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("伺服器IP" + serverIP + ex.Message);
				return Message.COMMON_MES_RESP(lg.RayCityAPI_NoNotices, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_BoardList_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 添加公告内容
		/// </summary>
		/// <returns></returns>
		public Message RayCity_BoardList_Insert()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			string city = null;
			int interval = 0;
			string msgContent = null;
			DateTime startDate = new DateTime();
			DateTime endDate = new DateTime();
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				strut = new TLV_Structure(TagName.RayCity_BeginDate,6,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_BeginDate).m_bValueBuffer);
				startDate =strut.toTimeStamp();
				strut = new TLV_Structure(TagName.RayCity_EndDate,6,msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_EndDate).m_bValueBuffer);
				endDate =strut.toTimeStamp();
				strut = new TLV_Structure(TagName.FJ_Interval, 4, msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_Interval).m_bValueBuffer);
				interval = (int)strut.toInteger();
				msgContent = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_Message).m_bValueBuffer);
				result = GlobalAccount.RayCityBoardList_Insert(operateUserID, serverIP, city, msgContent, startDate, endDate, interval);
				if (result == 1)
				{
					SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address + serverIP + lg.RayCityAPI_Notices+lg.API_Add+lg.API_Success);
					Console.WriteLine(DateTime.Now + lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address + serverIP +  lg.RayCityAPI_Notices+lg.API_Add+lg.API_Success);
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_BoardList_Insert_Resp);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address + serverIP +  lg.RayCityAPI_Notices+lg.API_Add+lg.API_Failure);
					Console.WriteLine(DateTime.Now + lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address + serverIP +  lg.RayCityAPI_Notices+lg.API_Add+lg.API_Failure);
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_BoardList_Insert_Resp);
				}
			}
			catch (System.Exception e)
			{
				return Message.COMMON_MES_RESP(lg.RayCityAPI_Notices+lg.API_Add+lg.API_Failure, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_BoardList_Insert_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 删除公告内容
		/// </summary>
		/// <returns></returns>
		public Message RayCity_BoardList_Delete()
		{
			int result = -1;
			int operateUserID = 0;
			string serverIP = null;
			int taskID = 0;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				serverIP = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_ServerIP).m_bValueBuffer);
				strut = new TLV_Structure(TagName.RayCity_NoticeID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.RayCity_NoticeID).m_bValueBuffer);
				taskID = (int)strut.toInteger();
				result = GlobalAccount.RayCityBoardList_Delete(operateUserID, serverIP, taskID);
				if (result == 1)
				{
					SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address + serverIP + lg.RayCityAPI_Notices+lg.API_Delete+lg.API_Success);
					Console.WriteLine(DateTime.Now + lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address + serverIP + lg.RayCityAPI_Notices+lg.API_Delete+lg.API_Success);
					return Message.COMMON_MES_RESP("SUCESS", Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_BoardList_Delete_Resp);
				}
				else
				{
					SqlHelper.log.WriteLog(lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address + serverIP + lg.RayCityAPI_Notices+lg.API_Delete+lg.API_Failure);
					Console.WriteLine(DateTime.Now + lg.API_Look+lg.RayCityAPI_Name+"+>"+lg.RayCityAPI_Address + serverIP + lg.RayCityAPI_Notices+lg.API_Delete+lg.API_Failure);
					return Message.COMMON_MES_RESP("FAILURE", Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_BoardList_Delete_Resp);
				}
			}
			catch (System.Exception e)
			{
				return Message.COMMON_MES_RESP(lg.RayCityAPI_Notices+lg.API_Delete+lg.API_Failure, Msg_Category.RAYCITY_ADMIN, ServiceKey.RayCity_BoardList_Delete_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
	}
}
