using System;
using System.Text;
using System.Data;
using Common.Logic;
using Common.DataInfo;
using GM_Server.SDOnlineDataInfo;
using lg = Common.API.LanguageAPI;

namespace GM_Server.SDOnlineAPI
{
	/// <summary>
	/// AccountInfoAPI 的摘要说明。
	/// </summary>
	public class SDAccountInfoAPI
	{
		Message msg = null;
		public SDAccountInfoAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
			
		}
		public SDAccountInfoAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemLogInfoAPI_Sumlg.SDAPI_SDItemShopAPI_Integral+
		/// <summary>
		/// lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemLogInfoAPI_Sumlg.SDAPI_SDItemShopAPI_Integral+
		/// </summary>
		/// <returns>Messagebody</returns>
		public Message SDUserActiveCode_Query()
		{
			string activeCode = null;
			string strDesc = null;
			System.Collections.ArrayList ds = new System.Collections.ArrayList();
			try
			{
				activeCode = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ActiceCode).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemLogInfoAPI_Sum+activeCode+lg.SDAPI_SDItemShopAPI_NoIntegral+lg.SDAPI_SDChallengeDataAPI_ProbabilityList+"!");
				Console.WriteLine(DateTime.Now+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemLogInfoAPI_Sum+activeCode+lg.SDAPI_SDItemShopAPI_NoIntegral+lg.SDAPI_SDChallengeDataAPI_ProbabilityList+"!");
				ds=SDAccountInfo.ActiveCode_Query(activeCode,1,ref strDesc);
				if(ds!=null && ds.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[1];

					Query_Structure strut = new Query_Structure((uint)ds.Count);
					byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds[0]);
					strut.AddTagKey(TagName.SD_ActiceCode,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds[1]);
					strut.AddTagKey(TagName.SD_GoldAccount,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds[2]);
					strut.AddTagKey(TagName.SD_IsUsed, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds[3]);
					strut.AddTagKey(TagName.SD_UserName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,Convert.ToString(ds[4]));
					strut.AddTagKey(TagName.SD_UseDate, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					structList[0] = strut;
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_ActiveCode_Query_Resp,5);
				}
				else
				{
					return Message.COMMON_MES_RESP(strDesc,Msg_Category.SD_ADMIN,ServiceKey.SD_ActiveCode_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception)
			{
				return Message.COMMON_MES_RESP(strDesc,Msg_Category.SD_ADMIN,ServiceKey.SD_ActiveCode_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);                
			}
            
		}
		#endregion

		#region 用户lg.SDAPI_SDItemShopAPI_Integral+lg.SDAPI_SDItemLogInfoAPI_Sum
		/// <summary>
		/// 用户lg.SDAPI_SDItemShopAPI_Integral+lg.SDAPI_SDItemLogInfoAPI_Sum
		/// </summary>
		/// <returns>Messagebody</returns>
		public Message SDUserAccount_Active_Query()
		{
			string account = null;
			string strDesc = null;
			System.Collections.ArrayList ds = new System.Collections.ArrayList();
			try
			{
				
				account = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemLogInfoAPI_Account+account+lg.SDAPI_SDItemLogInfoAPI_Sum+"!");
				Console.WriteLine(DateTime.Now+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemLogInfoAPI_Account+account+lg.SDAPI_SDItemLogInfoAPI_Sum+"!");
				ds=SDAccountInfo.Account_Active_Query(account,ref strDesc);
				if(ds!=null && ds.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[1];

					Query_Structure strut = new Query_Structure((uint)ds.Count);
					byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds[0]);
					strut.AddTagKey(TagName.Magic_UserName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds[1]);
					strut.AddTagKey(TagName.Magic_Date,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,Convert.ToString(ds[2]));
					strut.AddTagKey(TagName.Magic_ServerName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,Convert.ToString(ds[3]));
					strut.AddTagKey(TagName.Magic_Money, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					structList[0] = strut;
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_Account_Active_Query_Resp,4);
				}
				else
				{
					return Message.COMMON_MES_RESP(strDesc,Msg_Category.SD_ADMIN,ServiceKey.SD_Account_Active_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception)
			{
				return Message.COMMON_MES_RESP(strDesc,Msg_Category.SD_ADMIN,ServiceKey.SD_Account_Active_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);                
			}
            
		}
		#endregion	

		#region lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemShopAPI_ConsumeRecord
		/// <summary>
		/// lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemShopAPI_ConsumeRecord
		/// </summary>
		/// <returns>Messagebody</returns>
		public Message SD_Card_Info_Query()
		{
			string activeCode = null;
			string userName = null;
			string typeName = null;
			string strDesc = null;
			int type = -1;
			System.Collections.ArrayList ds = new System.Collections.ArrayList();
			try
			{
				activeCode = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ActiceCode).m_bValueBuffer);
				//用户名
				userName = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);

				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemShopAPI_ConsumeRecord+activeCode+lg.Logic_Exception_ExpectedValue+lg.SDAPI_SDItemShopAPI_NoIntegral+lg.SDAPI_SDChallengeDataAPI_ProbabilityList+"!");
				Console.WriteLine(DateTime.Now+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemShopAPI_ConsumeRecord+activeCode+lg.SDAPI_SDItemShopAPI_NoIntegral+lg.SDAPI_SDChallengeDataAPI_ProbabilityList+"!");
				ds=SDAccountInfo.Card_Info_Query(userName,activeCode,ref strDesc);
				if(ds!=null && ds.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[1];

					Query_Structure strut = new Query_Structure((uint)ds.Count);
					byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds[0]);
					strut.AddTagKey(TagName.SD_ActiceCode,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds[1]);
					strut.AddTagKey(TagName.SD_passWd,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds[2]);
					strut.AddTagKey(TagName.SD_UserName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds[3]);
					strut.AddTagKey(TagName.SD_ServerName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,Convert.ToString(ds[4]));
					strut.AddTagKey(TagName.SD_Time, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,Convert.ToString(ds[5]));
					strut.AddTagKey(TagName.SD_ServerIP, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					if(ds[6].ToString()=="1")
						typeName = "新手卡";
					else
						typeName = "鑽石卡";
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,typeName);
					strut.AddTagKey(TagName.Magic_TypeName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					structList[0] = strut;
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_ActiveCode_Query_Resp,7);
				}
				else
				{
					return Message.COMMON_MES_RESP(strDesc,Msg_Category.SD_ADMIN,ServiceKey.SD_ActiveCode_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception)
			{
				return Message.COMMON_MES_RESP(strDesc,Msg_Category.SD_ADMIN,ServiceKey.SD_ActiveCode_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);                
			}
            
		}
		#endregion

		#region 查询用户角色lg.SDAPI_SDChallengeDataAPI_ProbabilityList
		/// <summary>
		/// 查询用户角色lg.SDAPI_SDChallengeDataAPI_ProbabilityList
		/// </summary>
		/// <returns></returns>
		public Message SD_Account_Query()
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			string strnick = null;
			string strusername = null;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				strnick = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.f_pilot).m_bValueBuffer);
				strusername = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);

				if(strnick=="")
				{	
					SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+strusername+lg.SDAPI_SDCharacterInfoAPI_AccountInfo+"!");
					Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+strusername+lg.SDAPI_SDCharacterInfoAPI_AccountInfo+"!");
					ds = SDAccountInfo.Account_Query(serverIP,strusername);	
				}
				else
				{	
					SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+strusername+lg.SDAPI_SDCharacterInfoAPI_AccountInfo+"!");
					Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+strusername+lg.SDAPI_SDCharacterInfoAPI_AccountInfo+"!");
					ds = SDAccountInfo.Nick_Query(serverIP,strnick);	
				}	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,ds.Tables[0].Rows.Count,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_Account_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDCharacterInfoAPI_AccountInfo,Msg_Category.SD_ADMIN,ServiceKey.SD_Account_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDCharacterInfoAPI_AccountInfo, Msg_Category.SD_ADMIN, ServiceKey.SD_Account_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		#endregion

		#region lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG8
		/// <summary>
		/// lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG8
		/// </summary>
		/// <returns></returns>
		public Message SD_Item_MixTree_Query(int index,int pageSize)
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			int userid = 0;
			string username = null;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();

				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDItemMsG8+"!");
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDItemMsG8+"!");
				ds = SDAccountInfo.Item_MixTree_Query(serverIP,userid);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_Item_MixTree_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG8,Msg_Category.SD_ADMIN,ServiceKey.SD_Item_MixTree_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG8, Msg_Category.SD_ADMIN, ServiceKey.SD_Item_MixTree_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		#endregion

		#region lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDChallengeDataAPI_SceneProbability+lg.SDAPI_SDChallengeDataAPI_ProbabilityList
		/// <summary>
		/// lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDChallengeDataAPI_SceneProbability+lg.SDAPI_SDChallengeDataAPI_ProbabilityList
		/// </summary>
		/// <returns></returns>
		public Message SD_Item_UserUnits_Query(int index,int pageSize)
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			int userid = 0;
			string username = null;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();

				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDChallengeDataAPI_SceneProbability+lg.SDAPI_SDChallengeDataAPI_ProbabilityList+"!");
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDChallengeDataAPI_SceneProbability+lg.SDAPI_SDChallengeDataAPI_ProbabilityList+"!");
				ds = SDAccountInfo.Item_UserUnits_Query(serverIP,userid);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_Item_UserUnits_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDChallengeDataAPI_SceneProbability+lg.SDAPI_SDChallengeDataAPI_ProbabilityList,Msg_Category.SD_ADMIN,ServiceKey.SD_Item_UserUnits_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDChallengeDataAPI_SceneProbability+lg.SDAPI_SDChallengeDataAPI_ProbabilityList, Msg_Category.SD_ADMIN, ServiceKey.SD_Item_UserUnits_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		#endregion

		#region lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDChallengeDataAPI_SceneProbability+lg.SDAPI_SDChallengeDataAPI_GameMusicList+lg.SDAPI_SDChallengeDataAPI_ProbabilityList
		/// <summary>
		/// lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDChallengeDataAPI_SceneProbability+lg.SDAPI_SDChallengeDataAPI_GameMusicList+lg.SDAPI_SDChallengeDataAPI_ProbabilityList
		/// </summary>
		/// <returns></returns>
		public Message SD_UnitsItem_Query()
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			int userid = 0;
			int unitId = 0;
			string username = null;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();

				strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_ItemID).m_bValueBuffer);
				unitId =(int)strut.toInteger();


				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDChallengeDataAPI_SceneProbability+lg.SDAPI_SDChallengeDataAPI_GameMusicList+lg.SDAPI_SDChallengeDataAPI_ProbabilityList+"!");
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDChallengeDataAPI_SceneProbability+lg.SDAPI_SDChallengeDataAPI_GameMusicList+lg.SDAPI_SDChallengeDataAPI_ProbabilityList+"!");
				ds = SDAccountInfo.UnitsItem_Query(serverIP,userid,unitId);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,ds.Tables[0].Rows.Count,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_UnitsItem_Query,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDChallengeDataAPI_SceneProbability+lg.SDAPI_SDChallengeDataAPI_GameMusicList+lg.SDAPI_SDChallengeDataAPI_ProbabilityList,Msg_Category.SD_ADMIN,ServiceKey.SD_Item_UserUnits_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDChallengeDataAPI_SceneProbability+lg.SDAPI_SDChallengeDataAPI_GameMusicList+lg.SDAPI_SDChallengeDataAPI_ProbabilityList, Msg_Category.SD_ADMIN, ServiceKey.SD_Item_UserUnits_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		#endregion

		#region lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDChallengeDataAPI_NoGameMusicList+lg.SDAPI_SDChallengeDataAPI_GameMusicList+
		/// <summary>
		/// lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDChallengeDataAPI_NoGameMusicList+lg.SDAPI_SDChallengeDataAPI_GameMusicList+
		/// </summary>
		/// <returns></returns>
		public Message SD_Item_UserCombatitems_Query(int index,int pageSize)
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			int userid = 0;
			string username = null;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();

				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDChallengeDataAPI_NoGameMusicList+lg.SDAPI_SDChallengeDataAPI_GameMusicList+"!");
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDChallengeDataAPI_NoGameMusicList+lg.SDAPI_SDChallengeDataAPI_GameMusicList+"!");
				ds = SDAccountInfo.Item_UserCombatitems_Query(serverIP,userid);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_Item_UserCombatitems_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDChallengeDataAPI_NoGameMusicList+lg.SDAPI_SDChallengeDataAPI_GameMusicList,Msg_Category.SD_ADMIN,ServiceKey.SD_Item_UserCombatitems_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDChallengeDataAPI_NoGameMusicList+lg.SDAPI_SDChallengeDataAPI_GameMusicList, Msg_Category.SD_ADMIN, ServiceKey.SD_Item_UserCombatitems_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		#endregion

		#region lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDChallengeDataAPI_NoSceneList+lg.SDAPI_SDChallengeDataAPI_GameMusicList+
		/// <summary>
		/// lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDChallengeDataAPI_NoSceneList+lg.SDAPI_SDChallengeDataAPI_GameMusicList+
		/// </summary>
		/// <returns></returns>
		public Message SD_Item_Operator_Query(int index,int pageSize)
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			int userid = 0;
			string username = null;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();

				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDChallengeDataAPI_NoSceneList+lg.SDAPI_SDChallengeDataAPI_GameMusicList+"!");
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDChallengeDataAPI_NoSceneList+lg.SDAPI_SDChallengeDataAPI_GameMusicList+"!");
				ds = SDAccountInfo.Item_Operator_Query(serverIP,userid);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_Item_Operator_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDChallengeDataAPI_NoSceneList+lg.SDAPI_SDChallengeDataAPI_GameMusicList,Msg_Category.SD_ADMIN,ServiceKey.SD_Item_Operator_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDChallengeDataAPI_NoSceneList+lg.SDAPI_SDChallengeDataAPI_GameMusicList, Msg_Category.SD_ADMIN, ServiceKey.SD_Item_Operator_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		#endregion

		#region lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG10+lg.SDAPI_SDChallengeDataAPI_GameMusicList+
		/// <summary>
		/// lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG10+lg.SDAPI_SDChallengeDataAPI_GameMusicList+
		/// </summary>
		/// <returns></returns>
		public Message SD_Item_Paint_Query(int index,int pageSize)
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			int userid = 0;
			string username = null;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();

				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDItemMsG10+lg.SDAPI_SDChallengeDataAPI_GameMusicList+"!");
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDItemMsG10+lg.SDAPI_SDChallengeDataAPI_GameMusicList+"!");
				ds = SDAccountInfo.Item_Paint_Query(serverIP,userid);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_Item_Paint_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG10+lg.SDAPI_SDChallengeDataAPI_GameMusicList,Msg_Category.SD_ADMIN,ServiceKey.SD_Item_Paint_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG10+lg.SDAPI_SDChallengeDataAPI_GameMusicList, Msg_Category.SD_ADMIN, ServiceKey.SD_Item_Paint_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		#endregion

		#region lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemLogInfoAPI_Compensate+lg.SDAPI_SDChallengeDataAPI_GameMusicList+
		/// <summary>
		/// lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemLogInfoAPI_Compensate+lg.SDAPI_SDChallengeDataAPI_GameMusicList+
		/// </summary>
		/// <returns></returns>
		public Message SD_Item_Skill_Query(int index,int pageSize)
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			int userid = 0;
			string username = null;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();

				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDItemLogInfoAPI_Compensate+lg.SDAPI_SDChallengeDataAPI_GameMusicList+"!");
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDItemLogInfoAPI_Compensate+lg.SDAPI_SDChallengeDataAPI_GameMusicList+"!");
				ds = SDAccountInfo.Item_Skill_Query(serverIP,userid);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_Item_Skill_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemLogInfoAPI_Compensate+lg.SDAPI_SDChallengeDataAPI_GameMusicList,Msg_Category.SD_ADMIN,ServiceKey.SD_Item_Skill_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemLogInfoAPI_Compensate+lg.SDAPI_SDChallengeDataAPI_GameMusicList, Msg_Category.SD_ADMIN, ServiceKey.SD_Item_Skill_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		#endregion

		#region lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG2+lg.SDAPI_SDChallengeDataAPI_GameMusicList+
		/// <summary>
		/// lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG2+lg.SDAPI_SDChallengeDataAPI_GameMusicList+
		/// </summary>
		/// <returns></returns>
		public Message SD_Item_Sticker_Query(int index,int pageSize)
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			int userid = 0;
			string username = null;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();

				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDItemMsG2+lg.SDAPI_SDChallengeDataAPI_GameMusicList+"!");
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDItemMsG2+lg.SDAPI_SDChallengeDataAPI_GameMusicList+"!");
				ds = SDAccountInfo.Item_Sticker_Query(serverIP,userid);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_Item_Sticker_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG2+lg.SDAPI_SDChallengeDataAPI_GameMusicList,Msg_Category.SD_ADMIN,ServiceKey.SD_Item_Sticker_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG2+lg.SDAPI_SDChallengeDataAPI_GameMusicList, Msg_Category.SD_ADMIN, ServiceKey.SD_Item_Skill_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		#endregion

		#region lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG3+lg.SDAPI_SDChallengeDataAPI_ProbabilityList
		/// <summary>
		/// lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG3+lg.SDAPI_SDChallengeDataAPI_ProbabilityList
		/// </summary>
		/// <returns></returns>
		public Message SD_Firend_Query(int index,int pageSize)
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			int userid = 0;
			string username = null;
			try
			{
				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				username = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_UserName).m_bValueBuffer);
				//用户
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.f_idx).m_bValueBuffer);
				userid =(int)strut.toInteger();

				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDItemMsG3+lg.SDAPI_SDChallengeDataAPI_ProbabilityList+"!");
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDItemMsG3+lg.SDAPI_SDChallengeDataAPI_ProbabilityList+"!");
				ds = SDAccountInfo.Firend_Query(serverIP,userid);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_Firend_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG3+lg.SDAPI_SDChallengeDataAPI_ProbabilityList,Msg_Category.SD_ADMIN,ServiceKey.SD_Firend_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG3+lg.SDAPI_SDChallengeDataAPI_ProbabilityList, Msg_Category.SD_ADMIN, ServiceKey.SD_Firend_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		#endregion

		#region lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG4+lg.SDAPI_SDItemShopAPI_Integral+
		/// <summary>
		/// lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG4+lg.SDAPI_SDItemShopAPI_Integral+
		/// </summary>
		/// <returns></returns>
		public Message SD_UserRank_query(int index,int pageSize)
		{
			string serverIP = null;
			//int uid = 0;
			DataSet ds = null;
			int userid = 0;
			int type=-1;
			string username = null;
			try
			{
				//lg.SDAPI_SDItemShopAPI_Integral+类型
				TLV_Structure strut = new TLV_Structure(TagName.f_idx,4,msg.m_packet.m_Body.getTLVByTag(TagName.SD_Type).m_bValueBuffer);
				type =(int)strut.toInteger();

				serverIP = Encoding.Unicode.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SD_ServerIP).m_bValueBuffer);
				
				SqlHelper.log.WriteLog(lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDItemMsG4+lg.SDAPI_SDItemShopAPI_Integral+"!");
				Console.WriteLine(DateTime.Now+" - "+lg.SDAPI_SDItemMsG+lg.SDAPI_SD+"+>"+lg.SDAPI_SDItemMsG1+CommonInfo.serverIP_Query(serverIP)+lg.SDAPI_SDItemLogInfoAPI_Account+username+lg.SDAPI_SDItemMsG4+lg.SDAPI_SDItemShopAPI_Integral+"!");
				ds = SDAccountInfo.UserRank_Query(serverIP,type);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.SD_ADMIN,ServiceKey.SD_Firend_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG4+lg.SDAPI_SDItemShopAPI_Integral,Msg_Category.SD_ADMIN,ServiceKey.SD_Firend_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog(lg.SDAPI_SDItemMsG1+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(lg.SDAPI_SDItemMsG5+lg.SDAPI_SDChallengeDataAPI_NoChallengeScene+lg.SDAPI_SDItemLogInfoAPI_Account+lg.SDAPI_SDItemMsG4+lg.SDAPI_SDItemShopAPI_Integral, Msg_Category.SD_ADMIN, ServiceKey.SD_Firend_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
		#endregion

	}
}