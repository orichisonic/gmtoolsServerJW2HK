 using System;
using Common.Logic;
using System.Text;
using System.Data.SqlClient;
using Common.DataInfo;
namespace Common.API
{
	/// <summary>
	/// UserModuleAPI 的摘要说明。
	/// </summary>
	public class UserModuleAPI
	{
		Message msg;
		public UserModuleAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length); 
		}
		public void switchResult(byte[] packet)
		{
			msg= new Message(packet,(uint)packet.Length); 
			switch(msg.GetMessageID())
			{
				case Message_Tag_ID.MODULE_CREATE ://user_create
					GM_InsertUserModuleInfo();
					break;

			}
		} 
		/// <summary>
		/// 根据得到模块信息类
		/// </summary>
		/// <param name="userID">用户ID</param>
		/// <returns>模块类</returns>
		public Message GM_getModuleInfo()
		{
			GMLogAPI logAPI = new GMLogAPI();
			System.Data.DataSet ds = null;

			try
			{
				//将模块信息存入DATASET
				ds = GMModuleInfo.SelectAll();
				//构造一个模块信息类
				Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
				for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
					Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
					strut.AddTagKey(TagName.GameID,TagFormat.TLV_INTEGER,4,TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]));	
					strut.AddTagKey(TagName.Module_ID,TagFormat.TLV_INTEGER,4,TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[1]));	
					byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[2]);
					strut.AddTagKey(TagName.GameName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[3]);
					strut.AddTagKey(TagName.ModuleName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[4]);
					strut.AddTagKey(TagName.ModuleClass,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[5]);
					strut.AddTagKey(TagName.ModuleContent,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	
					structList[i]=strut;
					//打印具体信息
					//logAPI.writeData(ds.Tables[0].Rows[i].ItemArray[1].ToString(),ds.Tables[0].Rows[i].ItemArray[2].ToString(),ds.Tables[0].Rows[i].ItemArray[3].ToString(),ds.Tables[0].Rows[i].ItemArray[4].ToString());
				}
				//Console.Write(logAPI.Buffer.ToString());
				return Message.COMMON_MES_RESP(structList,Msg_Category.USER_MODULE_ADMIN,ServiceKey.USER_MODULE_QUERY_RESP,6);
			}

			catch(System.Exception)
			{
				return Message.COMMON_MES_RESP(LanguageAPI.API_UserModuleAPI_NoRecord,Msg_Category.USER_MODULE_ADMIN,ServiceKey.USER_MODULE_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				
			}
			
		}
		/// <summary>
		/// 根据用户ID得到模块信息类
		/// </summary>
		/// <param name="userID">用户ID</param>
		/// <returns>模块类</returns>
		public Message GM_getModuleInfo(int userID)
		{
			GMLogAPI logAPI = new GMLogAPI();
			System.Data.DataSet ds = null;

			try
			{
				//打印标题
				logAPI.writeTitle(LanguageAPI.API_UserModuleAPI_UserAuth,LanguageAPI.API_Display + userID + LanguageAPI.API_ModuleInfoAPI_ModuleInfo);
				//打印名称
				logAPI.writeContent(LanguageAPI.API_GameInfoAPI_ModuleID,LanguageAPI.API_GameInfoAPI_GameTitle,LanguageAPI.API_GameInfoAPI_ModuleTitle,LanguageAPI.API_GameInfoAPI_ModuleClass);
				//将模块信息存入DATASET
				ds = GMModuleInfo.getModuleInfo(userID);
				//构造一个模块信息类
				Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
				for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
					Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
					strut.AddTagKey(TagName.GameID,TagFormat.TLV_INTEGER,4,TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]));
					strut.AddTagKey(TagName.Module_ID,TagFormat.TLV_INTEGER,4,TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[1]));		
					byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[2]);
					strut.AddTagKey(TagName.GameName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[3]);
					strut.AddTagKey(TagName.ModuleName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[4]);
					strut.AddTagKey(TagName.ModuleClass,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[5]);
					strut.AddTagKey(TagName.ModuleContent,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	
					structList[i]=strut;
					//打印具体信息
					logAPI.writeData(ds.Tables[0].Rows[i].ItemArray[1].ToString(),ds.Tables[0].Rows[i].ItemArray[2].ToString(),ds.Tables[0].Rows[i].ItemArray[3].ToString(),ds.Tables[0].Rows[i].ItemArray[4].ToString());
				}
				Console.Write(logAPI.Buffer.ToString());
				return Message.COMMON_MES_RESP(structList,Msg_Category.USER_MODULE_ADMIN,ServiceKey.USER_MODULE_QUERY_RESP,6);
			}

			catch(System.Exception)
			{
				return Message.COMMON_MES_RESP(LanguageAPI.API_UserModuleAPI_NoRecord,Msg_Category.USER_MODULE_ADMIN,ServiceKey.USER_MODULE_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				
			}
			
		}
		/// <summary>
		/// 给用户添加模块
		/// </summary>
		/// <returns></returns>
		public Message GM_UserModuleAdmin()
		{
			GMLogAPI logAPI = new GMLogAPI();
			int result = -1;
			int userByID=0;
			int userID = 0;
			string moduleList = null;		
			TLV_Structure tlv1 = new TLV_Structure(TagName.User_ID,3,msg.m_packet.m_Body.getTLVByTag(TagName.User_ID).m_bValueBuffer);
			userID =(int)tlv1.toInteger();
			TLV_Structure tlv2 = new TLV_Structure(TagName.UserByID,3,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
			userByID = (int)tlv2.toInteger();
			moduleList = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.ModuleList).m_bValueBuffer);
			result = GMUserModule.UserModuleAdmin(userID,moduleList);
			//打印标题
			logAPI.writeTitle(LanguageAPI.API_UserModuleAPI_UserAuth,userID + LanguageAPI.API_UserModuleAPI_UserModule);
			//打印名称
			logAPI.writeContent(LanguageAPI.API_DepartmentAPI_OperatorID,LanguageAPI.API_UserInfoAPI_UserID,LanguageAPI.API_ModuleInfoAPI_ModuleList);
			logAPI.writeData(Convert.ToString(userByID),Convert.ToString(userID),moduleList);
			Console.Write(logAPI.Buffer.ToString());
			   return Message.COMMON_MES_RESP("SUCESS",Msg_Category.USER_MODULE_ADMIN,ServiceKey.USER_MODULE_CREATE_RESP);

		}
		/// <summary>
		/// 插入一个新的用户与模块关系数据
		/// </summary>
		public void GM_InsertUserModuleInfo()
		{
			int userID = 0;
			int moduleID = 0;
			try
			{	
				TLV_Structure tlv1 = new TLV_Structure(TagName.User_ID,3,msg.m_packet.m_Body.getTLVByTag(TagName.User_ID).m_bValueBuffer);
				userID =(int)tlv1.toInteger();

				TLV_Structure tlv2 = new TLV_Structure(TagName.Module_ID,3,msg.m_packet.m_Body.getTLVByTag(TagName.Module_ID).m_bValueBuffer);
				moduleID = (int)tlv2.toInteger();

				TLV_Structure tlv = new TLV_Structure(TagName.Limit,3,msg.m_packet.m_Body.getTLVByTag(TagName.Limit).m_bValueBuffer);
				DateTime limit =tlv.toDate();

				//GMUserInfo.insertRow(userName,passWd,mac,limit);
			}
			catch(Common.Logic.Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

		}
		/// <summary>
		/// 删除一个用户与模块关系数据
		/// </summary>
		public void GM_DelUserModuleInfo()
		{
			int userID = 0;
			try
			{
				userID = Convert.ToInt32(Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.User_ID).m_bValueBuffer));
				//GMUserInfo.deleteRow(userID);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}

		}
	}
}
