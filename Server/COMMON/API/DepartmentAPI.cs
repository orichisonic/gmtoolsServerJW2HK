using System;
using System.Text;
using System.Data;
using Common.Logic;
using Common.DataInfo;


namespace Common.API
{
	/// <summary>
	/// DepartmentAPI 的摘要说明。
	/// </summary>
	public class DepartmentAPI
	{
		Message msg = null;
		public DepartmentAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
		}
		#region 得到部门信息
		/// <summary>
		/// 得到部门信息
		/// </summary>
		/// <returns></returns>
		public Message GM_QueryDepartInfo()
		{
			System.Data.DataSet ds = null;
			GMLogAPI logAPI = new GMLogAPI();
			try
			{
				ds = GMDepartmentInfo.getDepartInfo();
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
				
					logAPI.writeTitle(LanguageAPI.API_Display+LanguageAPI.API_DepartmentAPI_DepInfo,LanguageAPI.API_Display+LanguageAPI.API_DepartmentAPI_DepInfo);
					logAPI.writeContent(LanguageAPI.API_DepartmentAPI_DepID,LanguageAPI.API_DepartmentAPI_DepTitle,LanguageAPI.API_DepartmentAPI_DepDesp);	
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						strut.AddTagKey(TagName.DepartID,TagFormat.TLV_INTEGER,4,TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]));
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.DepartName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.DepartRemark,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						structList[i]=strut;
						logAPI.writeData(ds.Tables[0].Rows[i].ItemArray[0].ToString(),ds.Tables[0].Rows[i].ItemArray[1].ToString(),ds.Tables[0].Rows[i].ItemArray[2].ToString());
					}
					Console.Write(logAPI.Buffer.ToString());
					return Message.COMMON_MES_RESP(structList,Msg_Category.USER_ADMIN,ServiceKey.DEPART_QUERY_RESP,3);

				}
				else
				{
					return Message.COMMON_MES_RESP(LanguageAPI.API_DepartmentAPI_NoDepInfo,Msg_Category.USER_ADMIN,ServiceKey.DEPART_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}

			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);	
				return null;
				
			}
		}
		#endregion
		#region 得到关联部门信息
		/// <summary>
		/// 得到关联部门信息
		/// </summary>
		/// <returns></returns>
		public Message GM_QueryDepartRelateInfo()
		{
			int deptID = 0;
			System.Data.DataSet ds = null;
			GMLogAPI logAPI = new GMLogAPI();
			try
			{
				TLV_Structure tlvStrut = new TLV_Structure(TagName.DepartID,4,msg.m_packet.m_Body.getTLVByTag(TagName.DepartID).m_bValueBuffer);
				deptID = (int)tlvStrut.toInteger();
				SqlHelper.log.WriteLog(LanguageAPI.API_DepartmentAPI_GameList);
				Console.WriteLine(DateTime.Now + " - " + LanguageAPI.API_DepartmentAPI_GameList);
				ds = GMDepartmentInfo.getDepartRelateInfo(deptID);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						strut.AddTagKey(TagName.GameID,TagFormat.TLV_INTEGER,4,TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]));
						structList[i]=strut;
					}
					Console.Write(logAPI.Buffer.ToString());
					return Message.COMMON_MES_RESP(structList,Msg_Category.USER_ADMIN,ServiceKey.DEPARTMENT_RELATE_QUERY_RESP,1);
				}
				else
				{
					return Message.COMMON_MES_RESP(LanguageAPI.API_DepartmentAPI_NoDepInfo,Msg_Category.USER_ADMIN,ServiceKey.DEPARTMENT_RELATE_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}

			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);	
				return Message.COMMON_MES_RESP(LanguageAPI.API_DepartmentAPI_NoDepInfo,Msg_Category.USER_ADMIN,ServiceKey.DEPARTMENT_RELATE_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				
			}
		}
		#endregion
		#region 得到该用户所在部门的所拥有的游戏
		/// <summary>
		/// 得到该用户所在部门的所拥有的游戏
		/// </summary>
		/// <returns></returns>
		public Message GM_QueryDepartRelateGameInfo()
		{
			int userID = 0;
			System.Data.DataSet ds = null;
			GMLogAPI logAPI = new GMLogAPI();
			try
			{
				TLV_Structure tlvStrut = new TLV_Structure(TagName.User_ID,4,msg.m_packet.m_Body.getTLVByTag(TagName.User_ID).m_bValueBuffer);
				userID = (int)tlvStrut.toInteger();
				SqlHelper.log.WriteLog(LanguageAPI.API_DepartmentAPI_HoldGame);
				Console.WriteLine(DateTime.Now + " - " + LanguageAPI.API_DepartmentAPI_HoldGame);
				ds = GMDepartmentInfo.getDepartRelateGameInfo(userID);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						strut.AddTagKey(TagName.GameID,TagFormat.TLV_INTEGER,4,TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]));
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.GameName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);						
						structList[i]=strut;
					}
					Console.Write(logAPI.Buffer.ToString());
					return Message.COMMON_MES_RESP(structList,Msg_Category.USER_ADMIN,ServiceKey.DEPART_RELATE_GAME_QUERY_RESP,2);
				}
				else
				{
					return Message.COMMON_MES_RESP(LanguageAPI.API_DepartmentAPI_NoDepInfo,Msg_Category.USER_ADMIN,ServiceKey.DEPART_RELATE_GAME_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}

			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);	
				return Message.COMMON_MES_RESP(LanguageAPI.API_DepartmentAPI_NoDepInfo,Msg_Category.USER_ADMIN,ServiceKey.DEPART_RELATE_GAME_QUERY_RESP,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				
			}
		}
		#endregion
		/// <summary>
		/// 插入一个新的部门
		/// </summary>
		public Message GM_InsertDepartInfo()
		{

			int result = -1;
			int operateUserID = 0;
			string deptName = null;
			string deptRemark = null;
			string gameList = null;
			GMLogAPI logAPI = new GMLogAPI();
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID = (int)strut.toInteger();
				deptName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.DepartName).m_bValueBuffer);
				deptRemark = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.DepartRemark).m_bValueBuffer);
				gameList = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.GameContent).m_bValueBuffer);
				result = GMDepartmentInfo.insertRow(operateUserID,deptName,deptRemark,gameList);
				if(result ==1)
				{
					logAPI.writeTitle(LanguageAPI.API_Add + LanguageAPI.API_DepartmentAPI_DepInfo,LanguageAPI.API_Add + LanguageAPI.API_DepartmentAPI_DepInfo + LanguageAPI.API_Success + "！");
					logAPI.writeContent(LanguageAPI.API_DepartmentAPI_OperatorID,LanguageAPI.API_DepartmentAPI_DepTitle,LanguageAPI.API_DepartmentAPI_DepDesp);
					logAPI.writeContent(Convert.ToString(operateUserID),deptName,deptRemark);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.USER_ADMIN,ServiceKey.DEPARTMENT_CREATE_RESP,TagName.Status,TagFormat.TLV_STRING);
				}
				else
				{
					logAPI.writeTitle(LanguageAPI.API_Add + LanguageAPI.API_DepartmentAPI_DepInfo,LanguageAPI.API_Add + LanguageAPI.API_DepartmentAPI_DepInfo + LanguageAPI.API_Failure + "！");
					logAPI.writeContent(LanguageAPI.API_DepartmentAPI_OperatorID,LanguageAPI.API_DepartmentAPI_DepTitle,LanguageAPI.API_DepartmentAPI_DepDesp);
					logAPI.writeContent(Convert.ToString(operateUserID),deptName,deptRemark);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.USER_ADMIN,ServiceKey.DEPARTMENT_CREATE_RESP,TagName.Status,TagFormat.TLV_STRING);
				}

			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP("FAILURE",Msg_Category.USER_ADMIN,ServiceKey.DEPARTMENT_CREATE_RESP,TagName.Status,TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		///  删除一个模块信息
		/// </summary>
		public Message GM_DelDepartInfo()
		{
			GMLogAPI logAPI = new GMLogAPI();
			int result = -1;
			int deptID = 0;
			int userByID = 0;
			try
			{
				TLV_Structure strut1 = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userByID =(int)strut1.toInteger();
				TLV_Structure strut2 = new TLV_Structure(TagName.DepartID,4,msg.m_packet.m_Body.getTLVByTag(TagName.DepartID).m_bValueBuffer);
				deptID  =(int)strut2.toInteger();
				result = GMDepartmentInfo.deleteRow(userByID,deptID);
				if(result==1)
				{
					logAPI.writeTitle(LanguageAPI.API_Delete + LanguageAPI.API_DepartmentAPI_DepInfo,LanguageAPI.API_Delete + LanguageAPI.API_DepartmentAPI_DepInfo + LanguageAPI.API_Success + "！");
					logAPI.writeContent(LanguageAPI.API_DepartmentAPI_OperatorID,LanguageAPI.API_DepartmentAPI_DepTitle,LanguageAPI.API_DepartmentAPI_DepDesp);
					logAPI.writeContent(Convert.ToString(userByID),Convert.ToString(deptID),LanguageAPI.API_Delete + LanguageAPI.API_DepartmentAPI_DepInfo);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.USER_ADMIN,ServiceKey.DEPARTMENT_DELETE_RESP,TagName.Status,TagFormat.TLV_STRING);

				}
				else
				{
					logAPI.writeTitle(LanguageAPI.API_Delete + LanguageAPI.API_DepartmentAPI_DepInfo,LanguageAPI.API_Delete + LanguageAPI.API_DepartmentAPI_DepInfo + LanguageAPI.API_Failure + "！");
					logAPI.writeContent(LanguageAPI.API_DepartmentAPI_OperatorID,LanguageAPI.API_DepartmentAPI_DepTitle,LanguageAPI.API_DepartmentAPI_DepDesp);
					logAPI.writeContent(Convert.ToString(userByID),Convert.ToString(deptID),LanguageAPI.API_Delete + LanguageAPI.API_DepartmentAPI_DepInfo);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.USER_ADMIN,ServiceKey.DEPARTMENT_DELETE_RESP,TagName.Status,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP("FAILURE",Msg_Category.USER_ADMIN,ServiceKey.DEPARTMENT_DELETE_RESP,TagName.Status,TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 更新一个模块信息
		/// </summary>
		public Message GM_UpdateDepartInfo()
		{
			int result = -1;
			int deptID = 0;
			int operateUserID = 0;
			string deptName = null;
			string deptRemark = null;
			string gameID = null;
			GMLogAPI logAPI = new GMLogAPI();
			try
			{
				TLV_Structure tlvStrut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)tlvStrut.toInteger();
				tlvStrut = new TLV_Structure(TagName.DepartID,4,msg.m_packet.m_Body.getTLVByTag(TagName.DepartID).m_bValueBuffer);
				deptID  =(int)tlvStrut.toInteger();
				deptName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.DepartName).m_bValueBuffer);
				deptRemark = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.DepartRemark).m_bValueBuffer);
				gameID = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.GameContent).m_bValueBuffer);
				result = GMDepartmentInfo.updateRow(operateUserID,deptID,deptName,deptRemark,gameID);
				if(result==1)
				{
					logAPI.writeTitle(LanguageAPI.API_Update + LanguageAPI.API_DepartmentAPI_DepInfo,LanguageAPI.API_Update + LanguageAPI.API_DepartmentAPI_DepInfo + LanguageAPI.API_Success + "！");
					logAPI.writeContent(LanguageAPI.API_DepartmentAPI_DepID,LanguageAPI.API_DepartmentAPI_DepTitle,LanguageAPI.API_DepartmentAPI_DepDesp);
					logAPI.writeContent(Convert.ToString(deptID),deptName,deptRemark);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.USER_ADMIN,ServiceKey.DEPARTMENT_UPDATE_RESP,TagName.Status,TagFormat.TLV_STRING);
				}
				else
				{
					logAPI.writeTitle(LanguageAPI.API_Update + LanguageAPI.API_DepartmentAPI_DepInfo,LanguageAPI.API_Update + LanguageAPI.API_DepartmentAPI_DepInfo + LanguageAPI.API_Failure + "！");
					logAPI.writeContent(LanguageAPI.API_DepartmentAPI_DepID,LanguageAPI.API_DepartmentAPI_DepTitle,LanguageAPI.API_DepartmentAPI_DepDesp);
					logAPI.writeContent(Convert.ToString(deptID),deptName,deptRemark);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.USER_ADMIN,ServiceKey.DEPARTMENT_UPDATE_RESP,TagName.Status,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP("FAILURE",Msg_Category.USER_ADMIN,ServiceKey.DEPARTMENT_UPDATE_RESP,TagName.Status,TagFormat.TLV_STRING);
			}

		}
	}
}
