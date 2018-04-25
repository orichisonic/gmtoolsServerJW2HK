using System;

using System.Text;
using SDO.SDODataInfo;
using Common.Logic;
using Common.DataInfo;
using lg = Common.API.LanguageAPI;

namespace SDO.SDOAPI
{
    public class SDOItemLogInfoAPI
    {
        Message msg = null;
        public SDOItemLogInfoAPI(byte[] packet)
        {
            msg = new Message(packet, (uint)packet.Length);
        }
        /// <summary>
        /// 玩家的充值明细查询
        /// </summary>
        /// <returns></returns>
        public Message userChargeDetail_Query(int index,int pageSize)
        {
            string serverIP = null;
            string account = null;
            DateTime beginDate ;
            DateTime endDate;
            System.Data.DataSet ds = null;
            try
			{
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
                account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
                TLV_Structure tlvStrut = new TLV_Structure(TagName.SDO_BeginTime,3,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_BeginTime).m_bValueBuffer);
				beginDate =tlvStrut.toDate();
                tlvStrut = new TLV_Structure(TagName.SDO_EndTime,3,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_EndTime).m_bValueBuffer);
				endDate =tlvStrut.toDate();
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDOItemLogInfoAPI_Account + account + lg.SDOAPI_SDOItemLogInfoAPI_FillDetail + "!");
                //请求玩家身上的道具
                ds = ItemLogInfo.userChargeDetail_Query(serverIP,account,beginDate,endDate);
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
                    for (int i = 0; i < index + pageSize; i++)
                    {
                        Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length+1);
                        //用户ID
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[0]);
                        strut.AddTagKey(TagName.SDO_Account, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //充值日期
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, ds.Tables[0].Rows[i].ItemArray[1]);
                        strut.AddTagKey(TagName.SDO_ShopTime, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        //充值金额
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.SDO_MCash, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                        structList[i - index] = strut;
                    }
                    return Message.COMMON_MES_RESP(structList, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERMCASH_QUERY_RESP, 3);
                }
                else
                    return Message.COMMON_MES_RESP(lg.SDOAPI_SDOItemLogInfoAPI_NoChargeRecord, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERMCASH_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

            }
            catch (Common.Logic.Exception ex)
            {
                return Message.COMMON_MES_RESP(lg.SDOAPI_SDOItemLogInfoAPI_NoChargeRecord, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERMCASH_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }
        }
        /// <summary>
        /// 玩家的充值明细合计
        /// </summary>
        /// <returns></returns>
        public Message userChargeSum_Query()
        {
            System.Data.DataSet result = null;
            string serverIP = null;
            string account = null;
            DateTime beginDate;
            DateTime endDate;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
                account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
                TLV_Structure tlvStrut = new TLV_Structure(TagName.SDO_BeginTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_BeginTime).m_bValueBuffer);
                beginDate = tlvStrut.toDate();
                tlvStrut = new TLV_Structure(TagName.SDO_EndTime, 3, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_EndTime).m_bValueBuffer);
                endDate = tlvStrut.toDate();
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDOItemLogInfoAPI_Account + account + lg.SDOAPI_SDOItemLogInfoAPI_FillDetail + lg.SDOAPI_SDOItemLogInfoAPI_Sum + "!");
                result = ItemLogInfo.userChargeSum_Query(serverIP, account, beginDate, endDate);
                if (result != null && result.Tables[0].Rows.Count>0)
                {

                    return Message.COMMON_MES_RESP(Convert.ToInt32(result.Tables[0].Rows[0].ItemArray[1]), Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERCHARAGESUM_QUERY_RESP, TagName.SDO_ChargeSum, TagFormat.TLV_INTEGER);
                }
                else
                {
                    return Message.COMMON_MES_RESP(lg.SDOAPI_SDOItemLogInfoAPI_NoTotalValue, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERCHARAGESUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }

            }
            catch (System.Exception ex)
            {
                return Message.COMMON_MES_RESP(lg.SDOAPI_SDOItemLogInfoAPI_NoTotalValue, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERCHARAGESUM_QUERY_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }

        }
        /// <summary>
        /// 玩家的Ｇ币查询
        /// </summary>
        /// <returns></returns>
        public Message userGCash_Query()
        {
            System.Data.DataSet result = null;
            string serverIP = null;
            string account = null;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
                account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
                Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDOItemLogInfoAPI_Account + account + lg.SDOAPI_SDOItemLogInfoAPI_GCash + lg.SDOAPI_SDOItemLogInfoAPI_Sum + "!");
                result = ItemLogInfo.userGCash_Query(serverIP, account);
                if (result != null && result.Tables[0].Rows.Count > 0)
                {

                    return Message.COMMON_MES_RESP(Convert.ToInt32(result.Tables[0].Rows[0].ItemArray[2]), Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERGCASH_QUERY_RESP, TagName.SDO_GCash, TagFormat.TLV_INTEGER);
                }
                else
                {
                    return Message.COMMON_MES_RESP(0, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERGCASH_QUERY_RESP, TagName.SDO_GCash, TagFormat.TLV_INTEGER);
                }

            }
            catch (System.Exception ex)
            {
                return Message.COMMON_MES_RESP(0, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERGCASH_QUERY_RESP, TagName.SDO_GCash, TagFormat.TLV_INTEGER);
            }

        }
        /// <summary>
        /// 玩家的G币补发
        /// </summary>
        /// <returns></returns>
        public Message SDO_GCash_Update()
        {
            int result = -1;
            int operateUserID = 0;
            string account = null;
            string serverIP = null;
            int GCash = 0;
            try
            {
                serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
                account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_Account).m_bValueBuffer);
                TLV_Structure strut = new TLV_Structure(TagName.UserByID, 4, msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
                operateUserID = (int)strut.toInteger();
                strut = new TLV_Structure(TagName.SDO_GCash, 4, msg.m_packet.m_Body.getTLVByTag(TagName.SDO_GCash).m_bValueBuffer);
                GCash = (int)strut.toInteger();
                result = ItemLogInfo.SDO_UserMcash_addG(operateUserID, serverIP, account, GCash);
                if (result == -1)
                {
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + " " + account + lg.SDOAPI_SDOCharacterInfoAPI_NoAccount + "!");
                    return Message.COMMON_MES_RESP( lg.SDOAPI_SDOCharacterInfoAPI_NoAccount, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERGCASH_UPDATE_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

                }
                else if (result == 1)
                {
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) + lg.SDOAPI_SDOItemLogInfoAPI_Account + account + lg.SDOAPI_SDOItemLogInfoAPI_Compensate + GCash + lg.SDOAPI_SDOItemLogInfoAPI_GCash + lg.API_Success + "!");
                    return Message.COMMON_MES_RESP("SUCESS", Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERGCASH_UPDATE_RESP);
                }
                else
                {
                    Console.WriteLine(DateTime.Now + " - " + lg.API_Display + lg.SDOAPI_SDO + "+>" + lg.API_CommonAPI_ServerIP + CommonInfo.serverIP_Query(serverIP) +  lg.SDOAPI_SDOItemLogInfoAPI_Account + account + lg.SDOAPI_SDOItemLogInfoAPI_Compensate + GCash + lg.SDOAPI_SDOItemLogInfoAPI_GCash + lg.API_Failure + "!");
                    return Message.COMMON_MES_RESP("FAILURE", Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERGCASH_UPDATE_RESP);
                }
            }
            catch (Common.Logic.Exception ex)
            {
                return Message.COMMON_MES_RESP(ex.Message, Msg_Category.SDO_ADMIN, ServiceKey.SDO_USERGCASH_UPDATE_RESP);
            }

        }

		/// <summary>
		/// 查询玩家宠物信息
		/// </summary>
		/// <returns></returns>
		public Message SDO_PetInfo_Query()
		{
			string serverIP = null;
			int userIndexID = 0;
			System.Data.DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.SDO_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.SDO_UserIndexID,4,msg.m_packet.m_Body.getTLVByTag(TagName.SDO_UserIndexID).m_bValueBuffer);
				userIndexID =(int)strut.toInteger();	
				SqlHelper.log.WriteLog("流覽超級舞者+>"+CommonInfo.serverIP_Query(serverIP)+"玩家"+userIndexID+"寵物資訊!");
				Console.WriteLine(DateTime.Now+" - 流覽超級舞者+>"+CommonInfo.serverIP_Query(serverIP)+"玩家"+userIndexID+"寵物資訊!");
				ds = ItemShopInfo.SDOPetInfo_Query(serverIP,userIndexID);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut1 = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[0]);
						strut1.AddTagKey(TagName.SDO_NickName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
						strut1.AddTagKey(TagName.SDO_ItemName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP,ds.Tables[0].Rows[i].ItemArray[2]);
						strut1.AddTagKey(TagName.SDO_DateLimit,TagFormat.TLV_TIMESTAMP,(uint)bytes.Length,bytes);	

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[3]);
						strut1.AddTagKey(TagName.SDO_State,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[4]);
						strut1.AddTagKey(TagName.SDO_Level,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[5]);
						strut1.AddTagKey(TagName.SDO_Exp,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[6]);
						strut1.AddTagKey(TagName.SDO_Food,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[7]);
						strut1.AddTagKey(TagName.SDO_mood,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	

						structList[i]=strut1;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.SDO_ADMIN,ServiceKey.SDO_PetInfo_Query_RESP,8);
				}
				else
				{
					return Message.COMMON_MES_RESP("沒有相關寵物資訊",Msg_Category.SDO_ADMIN,ServiceKey.SDO_PetInfo_Query_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP("沒有相關寵物資訊", Msg_Category.SDO_ADMIN, ServiceKey.SDO_PetInfo_Query_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}
    }
}
