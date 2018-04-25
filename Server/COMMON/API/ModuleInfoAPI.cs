using System;
using System.Data;
using System.Data.SqlClient;
using Common.DataInfo;
using Common.Logic;
using System.Text;
namespace Common.API
{
	/// <summary>
	/// ModuleInfoAPI 的摘要说明。
	/// </summary>
	public class ModuleInfoAPI
	{
		Message msg;
		public ModuleInfoAPI(byte[] packet)
		{
			msg= new Message(packet,(uint)packet.Length); 
		}
		public void switchResult(byte[] packet)
		{
			msg= new Message(packet,(uint)packet.Length); 
			switch(msg.GetMessageID())
			{
				case Message_Tag_ID.MODULE_CREATE://module_create
					GM_InsertModuleInfo();
					break;
				case Message_Tag_ID.MODULE_UPDATE://module_update
					GM_UpdateModuleInfo();
					break;
				case Message_Tag_ID.MODULE_DELETE://module_delete
					GM_DelModuleInfo();
					break;
			}
		}
		/// <summary>
		/// 得到所有模块信息
		/// </summary>
		/// <returns>模块数据集</returns>
		public Message GM_QueryAll(int index,int pageSize)
		{
            GMLogAPI logAPI = new GMLogAPI();
            DataSet ds = null;
            try
            {
                logAPI.writeTitle(LanguageAPI.API_Display + LanguageAPI.API_ModuleInfoAPI_ModuleList, LanguageAPI.API_Display + LanguageAPI.API_ModuleInfoAPI_ModuleList);
                logAPI.writeContent(LanguageAPI.API_GameInfoAPI_ModuleID,LanguageAPI.API_GameInfoAPI_ModuleTitle,LanguageAPI.API_GameInfoAPI_ModuleClass,LanguageAPI.API_GameInfoAPI_ModuleDesp);
                ds = GMModuleInfo.SelectAll();
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    return Message.COMMON_MES_RESP(LanguageAPI.API_ModuleInfoAPI_NoModuleList, Msg_Category.MODULE_ADMIN, ServiceKey.MODULE_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
                }
                else
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
						strut.AddTagKey(TagName.GameID, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[0]));
                        strut.AddTagKey(TagName.Module_ID, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, ds.Tables[0].Rows[i].ItemArray[1]));
                        byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[2]);
                        strut.AddTagKey(TagName.GameName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[3]);
                        strut.AddTagKey(TagName.ModuleName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[4]);
                        strut.AddTagKey(TagName.ModuleClass, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, ds.Tables[0].Rows[i].ItemArray[5]);
                        strut.AddTagKey(TagName.ModuleContent, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
                        //总页数
                        strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
                        structList[i - index] = strut;
                        logAPI.writeData(ds.Tables[0].Rows[i].ItemArray[1].ToString(), ds.Tables[0].Rows[i].ItemArray[2].ToString(), ds.Tables[0].Rows[i].ItemArray[3].ToString(), ds.Tables[0].Rows[i].ItemArray[4].ToString());
                    }
                    Console.Write(logAPI.Buffer.ToString());
                    return Message.COMMON_MES_RESP(structList, Msg_Category.MODULE_ADMIN, ServiceKey.MODULE_QUERY_RESP, 7);

                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return Message.COMMON_MES_RESP(ex.Message, Msg_Category.MODULE_ADMIN, ServiceKey.MODULE_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);

            }
		}
		/// <summary>
		/// 得到模块信息类
		/// </summary>
		/// <param name="moduleID">模块ID</param>
		/// <returns>模块类</returns>
		public GMModuleInfo GM_QueryModuleInfo(int moduleID)
		{
			System.Data.DataSet ds = null;
			GMModuleInfo ModuleInfo = null;

			try
			{
				//将模块信息存入DATASET
				ds = GMModuleInfo.QueryModuleInfo(moduleID);
				//构造一个模块信息类
				Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
				for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
					Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
					strut.AddTagKey(TagName.Module_ID,TagFormat.TLV_INTEGER,4,TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]));	
					byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
					strut.AddTagKey(TagName.GameName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[2]);
					strut.AddTagKey(TagName.ModuleName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[3]);
					strut.AddTagKey(TagName.ModuleClass,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[4]);
					strut.AddTagKey(TagName.ModuleContent,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	
					structList[i]=strut;
				}
			}

			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);		
				
			}

			return ModuleInfo;
			
		}
		/// <summary>
		/// 插入一个新的模块
		/// </summary>
		public Message GM_InsertModuleInfo()
		{

			int result = -1;
			int operateUserID = 0;
			int gameID=0;
			string moduleName = null;
			string moduleClass = null;
			string moduleContent= null;
			GMLogAPI logAPI = new GMLogAPI();
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.GameID,4,msg.m_packet.m_Body.getTLVByTag(TagName.GameID).m_bValueBuffer);
				gameID = (int)strut.toInteger();
				moduleName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ModuleName).m_bValueBuffer);
				moduleClass = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ModuleClass).m_bValueBuffer);
				moduleContent = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ModuleContent).m_bValueBuffer);
				result = GMModuleInfo.insertRow(operateUserID,gameID,moduleName,moduleClass,moduleContent);
				if(result ==1)
				{
					logAPI.writeTitle(LanguageAPI.API_Add + LanguageAPI.API_ModuleInfoAPI_ModuleInfo,LanguageAPI.API_Add + LanguageAPI.API_ModuleInfoAPI_ModuleInfo + LanguageAPI.API_Success + "！");
					logAPI.writeContent(LanguageAPI.API_GameInfoAPI_GameID,LanguageAPI.API_GameInfoAPI_ModuleTitle,LanguageAPI.API_GameInfoAPI_ModuleClass,LanguageAPI.API_GameInfoAPI_ModuleDesp);
					logAPI.writeContent(Convert.ToString(gameID),moduleName,moduleClass,moduleContent);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.Common_MODULE_CREATE_RESP("SUCESS");
				}
				else
				{
					logAPI.writeTitle(LanguageAPI.API_Add + LanguageAPI.API_ModuleInfoAPI_ModuleInfo,LanguageAPI.API_Add + LanguageAPI.API_ModuleInfoAPI_ModuleInfo + LanguageAPI.API_Failure + "！");
					logAPI.writeContent(LanguageAPI.API_GameInfoAPI_GameID,LanguageAPI.API_GameInfoAPI_ModuleTitle,LanguageAPI.API_GameInfoAPI_ModuleClass,LanguageAPI.API_GameInfoAPI_ModuleDesp);
					logAPI.writeContent(Convert.ToString(gameID),moduleName,moduleClass,moduleContent);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.Common_MODULE_CREATE_RESP("FAILURE");
				}

			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
				return Message.Common_MODULE_CREATE_RESP("FAILURE");
			}

		}
		/// <summary>
		///  删除一个模块信息
		/// </summary>
		public Message GM_DelModuleInfo()
		{
			GMLogAPI logAPI = new GMLogAPI();
			int result = -1;
			int moduleID = 0;
			int userByID = 0;
			try
			{
				TLV_Structure strut1 = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userByID =(int)strut1.toInteger();
				TLV_Structure strut2 = new TLV_Structure(TagName.Module_ID,4,msg.m_packet.m_Body.getTLVByTag(TagName.Module_ID).m_bValueBuffer);
				moduleID  =(int)strut2.toInteger();
				result = GMModuleInfo.deleteRow(userByID,moduleID);
				if(result==1)
				{
					logAPI.writeTitle(LanguageAPI.API_Update + LanguageAPI.API_ModuleInfoAPI_ModuleInfo,LanguageAPI.API_Update + LanguageAPI.API_ModuleInfoAPI_ModuleInfo + LanguageAPI.API_Success + "！");
					logAPI.writeContent(LanguageAPI.API_DepartmentAPI_OperatorID,LanguageAPI.API_GameInfoAPI_ModuleID,LanguageAPI.API_GameInfoAPI_ModuleDesp);
					logAPI.writeContent(Convert.ToString(userByID),Convert.ToString(moduleID),LanguageAPI.API_Update + LanguageAPI.API_ModuleInfoAPI_ModuleInfo);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.Common_MODULE_DELETE_RESP(moduleID,"SUCESS");
				}
				else
				{
					logAPI.writeTitle(LanguageAPI.API_Update + LanguageAPI.API_ModuleInfoAPI_ModuleInfo,LanguageAPI.API_Update + LanguageAPI.API_ModuleInfoAPI_ModuleInfo + LanguageAPI.API_Failure + "！");
					logAPI.writeContent(LanguageAPI.API_DepartmentAPI_OperatorID,LanguageAPI.API_GameInfoAPI_ModuleID,LanguageAPI.API_GameInfoAPI_ModuleDesp);
					logAPI.writeContent(Convert.ToString(userByID),Convert.ToString(moduleID),LanguageAPI.API_Update + LanguageAPI.API_ModuleInfoAPI_ModuleInfo);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.Common_MODULE_DELETE_RESP(moduleID,"FAILURE");
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
				return Message.Common_MODULE_DELETE_RESP(moduleID,"FAILURE");
			}

		}
		/// <summary>
		/// 更新一个模块信息
		/// </summary>
		public Message GM_UpdateModuleInfo()
		{
			int result = -1;
			int moduleID = 0;
			int gameID =0; 
			int operateUserID = 0;
			string moduleName = null;
			string moduleClass = null;
			string moduleContent= null;
			GMModuleInfo moduleInfo = null;
			GMLogAPI logAPI = new GMLogAPI();
			try
			{
				TLV_Structure tlvStrut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)tlvStrut.toInteger();
				tlvStrut = new TLV_Structure(TagName.GameID,4,msg.m_packet.m_Body.getTLVByTag(TagName.GameID).m_bValueBuffer);
				gameID  =(int)tlvStrut.toInteger();
				tlvStrut = new TLV_Structure(TagName.Module_ID,4,msg.m_packet.m_Body.getTLVByTag(TagName.Module_ID).m_bValueBuffer);
				moduleID  =(int)tlvStrut.toInteger();
				moduleName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ModuleName).m_bValueBuffer);
				moduleClass = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ModuleClass).m_bValueBuffer);
				moduleContent = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ModuleContent).m_bValueBuffer);
				moduleInfo = new GMModuleInfo(gameID,moduleID,moduleName,moduleClass,moduleContent);
                result = moduleInfo.updateRow(operateUserID,moduleID);
				if(result==1)
				{
					logAPI.writeTitle(LanguageAPI.API_Delete + LanguageAPI.API_ModuleInfoAPI_ModuleInfo,LanguageAPI.API_Delete + LanguageAPI.API_ModuleInfoAPI_ModuleInfo + LanguageAPI.API_Success + "！");
					logAPI.writeContent(LanguageAPI.API_GameInfoAPI_GameID,LanguageAPI.API_GameInfoAPI_ModuleTitle,LanguageAPI.API_GameInfoAPI_ModuleClass,LanguageAPI.API_GameInfoAPI_ModuleDesp);
					logAPI.writeContent(Convert.ToString(gameID),moduleName,moduleClass,moduleContent);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.Common_MODULE_UPDATE_RESP("SUCESS");
				}
				else
				{
					logAPI.writeTitle(LanguageAPI.API_Delete + LanguageAPI.API_ModuleInfoAPI_ModuleInfo,LanguageAPI.API_Delete + LanguageAPI.API_ModuleInfoAPI_ModuleInfo + LanguageAPI.API_Failure + "！");
					logAPI.writeContent(LanguageAPI.API_GameInfoAPI_GameID,LanguageAPI.API_GameInfoAPI_ModuleTitle,LanguageAPI.API_GameInfoAPI_ModuleClass,LanguageAPI.API_GameInfoAPI_ModuleDesp);
					logAPI.writeContent(Convert.ToString(gameID),moduleName,moduleClass,moduleContent);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.Common_MODULE_UPDATE_RESP("FAILURE");
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
				return Message.Common_MODULE_UPDATE_RESP("FAILURE");
			}

		}
	}
}
