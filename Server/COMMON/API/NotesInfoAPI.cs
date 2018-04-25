using System;
using Common.Logic;
using Common.DataInfo;
namespace Common.API
{
	/// <summary>
	/// NotesInfoAPI 的摘要说明。
	/// </summary>
	public class NotesInfoAPI
	{
		Message msg;
		public NotesInfoAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public NotesInfoAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
		}
		public Message Notes_TransferInfo_Resp(int index,int pageSize)
		{
            GMLogAPI logAPI = new GMLogAPI();
            try
            {
                System.Data.DataSet ds;
                logAPI.writeTitle(LanguageAPI.API_NotesInfoAPI_NotesEmailList,LanguageAPI.API_Display + LanguageAPI.API_NotesInfoAPI_NotesEmailList);
                logAPI.writeContent(LanguageAPI.API_NotesInfoAPI_EmailID,LanguageAPI.API_NotesInfoAPI_EmailSubject,LanguageAPI.API_NotesInfoAPI_EmailContent,LanguageAPI.API_NotesInfoAPI_EmailSender);
                ds = GMNotesInfo.SelectAll();
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
                        Query_Structure strut = new Query_Structure(7);
                        //邮件ID
                        strut.AddTagKey(TagName.Letter_ID, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[0]));
                        //邮件发件人
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[1]);
                        strut.AddTagKey(TagName.Letter_Sender, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //邮件收件人
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.Letter_Receiver, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //邮件主题
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[3]);
                        strut.AddTagKey(TagName.Letter_Subject, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //邮件内容
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[4]);
                        strut.AddTagKey(TagName.Letter_Text, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //发送日期
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, ds.Tables[0].Rows[i].ItemArray[5]);
                        strut.AddTagKey(TagName.Send_Date, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                        structList[i - index] = strut;
                        logAPI.writeData(Convert.ToString(ds.Tables[0].Rows[i].ItemArray[0]), ds.Tables[0].Rows[i].ItemArray[3].ToString(), ds.Tables[0].Rows[i].ItemArray[4].ToString(), ds.Tables[0].Rows[i].ItemArray[1].ToString());
                    }
                    Console.Write(logAPI.Buffer.ToString());
                    return Message.COMMON_MES_RESP(structList, Msg_Category.NOTES_ADMIN, ServiceKey.NOTES_LETTER_TRANSFER_RESP, 7);
                }
                else
                {
                    return Message.COMMON_MES_RESP(LanguageAPI.API_NotesInfoAPI_NoDealWithEmail, Msg_Category.COMMON, ServiceKey.NOTES_LETTER_TRANSFER_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
                }
            }
            catch (System.Exception ex)
            {
                return Message.COMMON_MES_RESP(ex.Message, Msg_Category.COMMON, ServiceKey.NOTES_LETTER_TRANSFER_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

            }
		}
		/// <summary>
		/// 对NOTE邮件的处理
		/// </summary>
		/// <returns>邮件处理响应消息</returns>
		public Message Notes_LetterProcess_Resp()
		{
			int result = -1;
			int userByID = 0;
			int letterID = 0;
			int processMan  =0;
			DateTime processDate;
			int isProcess= 0 ;
			int transmitMan = 0;
			string reason = null;
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userByID = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.Letter_ID,4,msg.m_packet.m_Body.getTLVByTag(TagName.Letter_ID).m_bValueBuffer);
				letterID = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.Process_Man,4,msg.m_packet.m_Body.getTLVByTag(TagName.Process_Man).m_bValueBuffer);
				processMan = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.Process_Date,4,msg.m_packet.m_Body.getTLVByTag(TagName.Process_Date).m_bValueBuffer);
				processDate = strut.toDate();			
				strut = new TLV_Structure(TagName.Transmit_Man,4,msg.m_packet.m_Body.getTLVByTag(TagName.Transmit_Man).m_bValueBuffer);
				transmitMan = (int)strut.toInteger();
				reason = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.Process_Reason).m_bValueBuffer);
				isProcess  = 1;
				result = GMNotesInfo.updateRow(userByID,letterID,processMan,processDate,transmitMan,isProcess,reason);
				if(result==1)
				  return Message.COMMON_MES_RESP("SUCESS",Msg_Category.NOTES_ADMIN,ServiceKey.NOTES_LETTER_PROCESS_RESP,TagName.Status,TagFormat.TLV_STRING);
				else
				 return Message.COMMON_MES_RESP("FAILURE",Msg_Category.NOTES_ADMIN,ServiceKey.NOTES_LETTER_PROCESS_RESP,TagName.Status,TagFormat.TLV_STRING);
			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP(LanguageAPI.API_NotesInfoAPI_DealWithEmailFailure,Msg_Category.NOTES_ADMIN,ServiceKey.NOTES_LETTER_PROCESS_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}

		}
        public Message Notes_TransmitInfo_Resp(int userByID, int index, int pageSize)
        {
            GMLogAPI logAPI = new GMLogAPI();
            System.Data.DataSet ds;
            try
            {
                logAPI.writeTitle(LanguageAPI.API_Display + LanguageAPI.API_NotesInfoAPI_NotesTransEmailList, LanguageAPI.API_Display + LanguageAPI.API_NotesInfoAPI_NotesTransEmailList);
                logAPI.writeContent(LanguageAPI.API_NotesInfoAPI_EmailID,LanguageAPI.API_NotesInfoAPI_EmailSubject,LanguageAPI.API_NotesInfoAPI_EmailContent,LanguageAPI.API_NotesInfoAPI_EmailSender);
                ds = GMNotesInfo.SelectTransmitLetter(userByID);
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
                        //邮件ID
                        strut.AddTagKey(TagName.Letter_ID, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[0]));
                        //邮件发件人
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[1]);
                        strut.AddTagKey(TagName.Letter_Sender, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //邮件收件人
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.Letter_Receiver, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //邮件主题
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[3]));
                        strut.AddTagKey(TagName.Letter_Subject, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //邮件内容
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[4]));
                        strut.AddTagKey(TagName.Letter_Text, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //发送日期
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, ds.Tables[0].Rows[i].ItemArray[5]);
                        strut.AddTagKey(TagName.Send_Date, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        //处理日期
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP, ds.Tables[0].Rows[i].ItemArray[7]);
                        strut.AddTagKey(TagName.Process_Date, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
                        //处理标志
                        strut.AddTagKey(TagName.Is_Process, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[9]));
                        //处理人姓名
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[11]);
                        strut.AddTagKey(TagName.RealName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						//处理理由
						object reason;
						if (ds.Tables[0].Rows[i].IsNull(10) == false)
							reason = ds.Tables[0].Rows[i].ItemArray[10];
						else
							reason = "";
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, reason);
						strut.AddTagKey(TagName.Process_Reason, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));

                        structList[i - index] = strut;
                        logAPI.writeData(Convert.ToString(ds.Tables[0].Rows[i].ItemArray[0]), ds.Tables[0].Rows[i].ItemArray[3].ToString(), ds.Tables[0].Rows[i].ItemArray[4].ToString(), ds.Tables[0].Rows[i].ItemArray[1].ToString());
                    }
                    Console.Write(logAPI.Buffer.ToString());
                    return Message.COMMON_MES_RESP(structList, Msg_Category.NOTES_ADMIN, ServiceKey.NOTES_LETTER_TRANSMIT_RESP, 11);
                }
                else
                {
                    return Message.COMMON_MES_RESP(LanguageAPI.API_NotesInfoAPI_NoTransDealWithEmail, Msg_Category.NOTES_ADMIN, ServiceKey.NOTES_LETTER_TRANSMIT_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);

                }
            }
            catch (System.Exception ex)
            {
                return Message.COMMON_MES_RESP(LanguageAPI.API_NotesInfoAPI_NoTransDealWithEmail, Msg_Category.NOTES_ADMIN, ServiceKey.NOTES_LETTER_TRANSMIT_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
            }
        }
	}
}
