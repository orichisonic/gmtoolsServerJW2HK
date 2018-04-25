using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
using Common.API;
namespace GM_Server.FJDataInfo
{
	/// <summary>
	/// FJItemLogInfo 的摘要说明。
	/// </summary>
	public class FJItemLogInfo
	{
		public FJItemLogInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region GS查询
		/// <summary>
		/// GS查询
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet FJ_GS_Query(string serverIP,string city)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,30)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = city;
				result = SqlHelper.ExecSPDataSet("FJ_GS_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 公告查询
		/// <summary>
		/// 公告查询
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet BoardList_Query(string serverIP)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30)};
				paramCode[0].Value = serverIP;
				result = SqlHelper.ExecSPDataSet("FJ_BoardList_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 添加公告
		/// <summary>
		/// 添加公告
		/// </summary>
		/// <param name="userByID"></param>
		/// <param name="serverIP"></param>
		/// <param name="charname"></param>
		/// <param name="taskID"></param>
		/// <param name="taskState"></param>
		/// <returns></returns>
		public static int FJBoardList_Insert(int userByID,string serverIP,string city,string gsname,string gsDesc,string Message,string startTime,string endTime,int interval)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[10]{
													new SqlParameter("@Gm_UserID",SqlDbType.Int),
													new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
													new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
													new SqlParameter("@FJ_GSName",SqlDbType.VarChar,400),
													new SqlParameter("@FJ_GSDesc",SqlDbType.VarChar,400),
													new SqlParameter("@FJ_Message",SqlDbType.VarChar,980),
													new SqlParameter("@FJ_StartTime",SqlDbType.VarChar,30),
													new SqlParameter("@FJ_End_time",SqlDbType.VarChar,30),
													new SqlParameter("@FJ_Interval",SqlDbType.Int),
													new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = city;
				paramCode[3].Value = gsname;
				paramCode[4].Value = gsDesc;
				paramCode[5].Value = Message;
				paramCode[6].Value = startTime;
				paramCode[7].Value = endTime;
				paramCode[8].Value = interval;
				paramCode[9].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_BoardMsg_Create",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 删除公告
		/// <summary>
		/// 删除公告
		/// </summary>
		/// <param name="userByID"></param>
		/// <param name="serverIP"></param>
		/// <param name="charname"></param>
		/// <param name="taskID"></param>
		/// <param name="taskState"></param>
		/// <returns></returns>
		public static int FJBoardList_Delete(int userByID,string serverIP,string city,int boardID)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
												   new SqlParameter("@msg_Guild",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = city;
				paramCode[3].Value = boardID;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_BoardMsg_Delete",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 任务系统查询
		/// <summary>
		/// 任务系统查询
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet Task_Query(string serverIP,string charName)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Char_Name",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = charName;
				result = SqlHelper.ExecSPDataSet("FJ_Task_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 任务列表查询
		/// <summary>
		/// 任务系统查询
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet Quest_Query(int level)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@FJ_level",SqlDbType.Int)};

				paramCode[0].Value = level;
				result = SqlHelper.ExecSPDataSet("FJ_Questtable_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 添加任务
		/// <summary>
		/// 添加任务
		/// </summary>
		/// <param name="userByID"></param>
		/// <param name="serverIP"></param>
		/// <param name="charname"></param>
		/// <param name="taskID"></param>
		/// <param name="taskState"></param>
		/// <returns></returns>
		public static int Task_Insert(int userByID,string serverIP,string charname,int taskID,int taskState)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_CharName",SqlDbType.VarChar,50),
												   new SqlParameter("@FJ_TaskID",SqlDbType.Int),
												   new SqlParameter("@FJ_TaskState",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = charname;
				paramCode[3].Value = taskID;
				paramCode[4].Value = taskState;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_Task_Insert",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 删除任务
		/// <summary>
		/// 删除任务
		/// </summary>
		/// <param name="userByID"></param>
		/// <param name="serverIP"></param>
		/// <param name="charname"></param>
		/// <param name="taskID"></param>
		/// <param name="taskState"></param>
		/// <returns></returns>
		public static int Task_Delete(int userByID,string serverIP,string charname,int taskID)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_CharName",SqlDbType.VarChar,50),
												   new SqlParameter("@FJ_TaskID",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = charname;
				paramCode[3].Value = taskID;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_Task_Delete",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 更新任务
		/// <summary>
		/// 更新任务
		/// </summary>
		/// <param name="userByID"></param>
		/// <param name="serverIP"></param>
		/// <param name="charname"></param>
		/// <param name="taskID"></param>
		/// <param name="taskState"></param>
		/// <returns></returns>
		public static int Task_Update(int userByID,string serverIP,string charname,int taskID,int taskState)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_CharName",SqlDbType.VarChar,50),
												   new SqlParameter("@FJ_TaskID",SqlDbType.Int),
												   new SqlParameter("@FJ_TaskState",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = charname;
				paramCode[3].Value = taskID;
				paramCode[4].Value = taskState;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_Task_Update",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查询玩家封停信息
		/// <summary>
		/// 查询玩家封停信息
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet UserBan_Query(string serverIP,string city,string account)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_city",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = city;
				paramCode[2].Value = account;
				result = SqlHelper.ExecSPDataSet("FJ_UserBan_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查询游戏里面玩家封停信息
		/// <summary>
		/// 查询游戏里面玩家封停信息
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet GamesUserBan_Query(string serverIP,string city,string account)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_city",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = city;
				paramCode[2].Value = account;
				result = SqlHelper.ExecSPDataSet("FJ_GamesUserBan_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 游戏里面解封玩家帐号
		/// <summary>
		/// 游戏里面解封玩家帐号
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static int GamesUserBan_Open(int userByID,string serverIP,string city,string account,string reason)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_city",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_UnBanReason",SqlDbType.VarChar,500),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = city;
				paramCode[3].Value = account;
				paramCode[4].Value = reason;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_GamesUserBan_Open",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 解封玩家帐号
		/// <summary>
		/// 解封玩家帐号
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static int UserBan_Open(int userByID,string serverIP,string city,string account,string reason)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_city",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_UnBanReason",SqlDbType.VarChar,500),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = city;
				paramCode[3].Value = account;
				paramCode[4].Value = reason;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_UserBan_Open",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 有条件封停玩家帐号
		/// <summary>
		/// 有条件封停玩家帐号
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static int UserBanTime_Close(int userByID,string serverIP,string city,string account,int accountState,string reason)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[7]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_city",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_AccountState",SqlDbType.Int),
												   new SqlParameter("@FJ_BanReason",SqlDbType.VarChar,500),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = city;
				paramCode[3].Value = account;
				paramCode[4].Value = accountState;
				paramCode[5].Value = reason;
				paramCode[6].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_UserBanTime_Close",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 一个服务器内，同一个IP下，登陆的账号数≥10
		/// <summary>
		/// 一个服务器内，同一个IP下，登陆的账号数≥10
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet UserLoginCount_Query(string city,DateTime beginDate,DateTime endDate)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@FJ_city",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_BeginDate",SqlDbType.DateTime),
												   new SqlParameter("@FJ_EndDate",SqlDbType.DateTime)};
				paramCode[0].Value = city;
				paramCode[1].Value = beginDate;
				paramCode[2].Value = endDate;
				result = SqlHelper.ExecSPDataSet("FJ_UserLoginCount_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+ex.Message);
			}
			return result;
		}
		#endregion
		#region 封停玩家帐号
		/// <summary>
		/// 封停玩家帐号
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static int UserBan_Close(int userByID,string serverIP,string city,string account,string reason)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_city",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_BanReason",SqlDbType.VarChar,500),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = city;
				paramCode[3].Value = account;
				paramCode[4].Value = reason;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_UserBan_Close",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查询是否是在线GM封停的
		/// <summary>
		/// 查询是否是在线GM封停的
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet FJGMBan_Query(string serverIP,string city,string account)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = city;
				paramCode[2].Value = account;
				result = SqlHelper.ExecSPDataSet("FJ_GMBan_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 玩家日志类型查询
		/// <summary>
		/// 玩家日志查询
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet ItemLogType_Query()
		{
			DataSet result = null;
			try
			{
				result = SqlHelper.ExecSPDataSet("FJ_ItemLogType_Query");
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog(ex.Message);
			}
			return result;
		}
		#endregion
		#region 玩家日志查询
		/// <summary>
		/// 玩家日志查询
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet ItemLog_Query(string ServerIP,string city,string char_name,string tableName,string itemCode,int actionType,int type,DateTime beginDate,DateTime endDate)
		{
			DataSet result = new DataSet();
			DataSet dv = null;

			string strSQL = null;

			SqlParameter[] paramCode;
			try
			{
				System.TimeSpan interval = endDate - beginDate;
				int dayofyears = beginDate.DayOfYear;
				int endofyears = endDate.DayOfYear;
				int nowsofyears = DateTime.Now.DayOfYear;
				int days = interval.Days;
				if(type==100)
				{
					
					paramCode = new SqlParameter[9]{
													   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
													   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
													   new SqlParameter("@FJ_Char_Name",SqlDbType.VarChar,20),
													   new SqlParameter("@FJ_ItemName",SqlDbType.VarChar,50),
													   new SqlParameter("@FJ_ActionType",SqlDbType.Int),
													   new SqlParameter("@FJ_Type",SqlDbType.Int),
													   new SqlParameter("@FJ_BeginDate",SqlDbType.DateTime),
													   new SqlParameter("@FJ_EndDate",SqlDbType.DateTime),
													   new SqlParameter("@FJ_TableName",SqlDbType.VarChar,20)
												   };
					paramCode[0].Value = ServerIP;
					paramCode[1].Value = city;
					paramCode[2].Value = char_name;
					paramCode[3].Value = itemCode;
					paramCode[4].Value = actionType;
					paramCode[5].Value = type;
					paramCode[6].Value = beginDate;
					paramCode[7].Value = endDate;
					paramCode[8].Value = tableName;
					result = SqlHelper.ExecSPDataSet("FJ_ITEMLOG_Query",paramCode);
				}
				else
				{
					if(actionType==2)
					{
						if((dayofyears % 2)==0)
						{
//							System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=10");
//							if(ds!=null && ds.Tables[0].Rows.Count>0)
//							{
//								string serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
//							}
							strSQL="select server_name,actor_name,c.name,item_count,color,act_time,target_name,imoney,left_money,factory_mark,credit,left_credit from FJLogin.dbo.item_log a,FJLogin.dbo.FJ_ItemData c where  act_time >=convert(varchar(10),'"+beginDate.ToString("yyyy-MM-dd")+"',120)  and  act_time <=convert(varchar(10),'"+endDate.ToString("yyyy-MM-dd")+"',120)  and  a.log_type =convert(varchar,'"+type+"')   and a.actor_name='"+char_name+"' and  server_name='"+city.Substring(city.IndexOf('(')+1,city.Length-city.IndexOf('(')-2)+"' and  a.item_id=c.guid  order by act_time desc";
							result = SqlHelper.ExecSQL("60.206.14.44",strSQL);

						}
						else
						{
							strSQL="select server_name,actor_name,c.name,item_count,color,act_time,target_name,imoney,left_money,factory_mark,credit,left_credit from FJLogin.dbo.item_log a,FJLogin.dbo.FJ_ItemData c where a.item_id=c.guid and act_time >=convert(varchar(10),'"+beginDate.ToString("yyyy-MM-dd")+"',120)  and  act_time <=convert(varchar(10),'"+endDate.ToString("yyyy-MM-dd")+"',120)  and a.log_type =convert(varchar,'"+type+"')   and a.actor_name='"+char_name+"'  and server_name='"+city.Substring(city.IndexOf('(')+1,city.Length-city.IndexOf('(')-2)+"' and  a.item_id=c.guid  order by act_time desc";
							result = SqlHelper.ExecSQL("60.206.14.43",strSQL);
						}
					}
					else if(actionType==1)
					{
						for(int i=0;i<=days;i++)
						{
							strSQL= "select top 500 server_name,actor_name,c.name,item_count,color,act_time,target_name,imoney,left_money,factory_mark,credit,left_credit from FJ_LOG_BAK.dbo.item_log_"+dayofyears+"  a,FJ_LOG_BAK.dbo.FJ_ItemData c where  act_time >=convert(varchar(10),'"+beginDate.ToString("yyyy-MM-dd")+"',120) and  act_time <=convert(varchar(10),'"+endDate.AddDays(1).ToString("yyyy-MM-dd")+"',120) and a.log_type =convert(varchar,'"+type+"')   and a.actor_name='"+char_name+"'   and server_name='"+city.Substring(city.IndexOf('(')+1,city.Length-city.IndexOf('(')-2)+"' and a.item_id=c.guid order by act_time desc";
							dv = SqlHelper.ExecSQL("60.206.14.28",strSQL);
							if(dv!=null && dv.Tables[0].Rows.Count>0)
							{
								result.Merge(dv.Tables[0],false,System.Data.MissingSchemaAction.Add);
							}
							dayofyears+=1;
						}
					
					}
					else
					{

						paramCode = new SqlParameter[9]{
														   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
														   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
														   new SqlParameter("@FJ_Char_Name",SqlDbType.VarChar,20),
														   new SqlParameter("@FJ_ItemName",SqlDbType.VarChar,50),
														   new SqlParameter("@FJ_ActionType",SqlDbType.Int),
														   new SqlParameter("@FJ_Type",SqlDbType.Int),
														   new SqlParameter("@FJ_BeginDate",SqlDbType.DateTime),
														   new SqlParameter("@FJ_EndDate",SqlDbType.DateTime),
														   new SqlParameter("@FJ_TableName",SqlDbType.VarChar,20)
													   };
						paramCode[0].Value = ServerIP;
						paramCode[1].Value = city;
						paramCode[2].Value = char_name;
						paramCode[3].Value = itemCode;
						paramCode[4].Value = actionType;
						paramCode[5].Value = type;
						paramCode[6].Value = beginDate;
						paramCode[7].Value = endDate;
						paramCode[8].Value = tableName;
						result = SqlHelper.ExecSPDataSet("FJ_ITEMLOG_Query",paramCode);

					}
				}
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+ServerIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 英雄卡道具领取查询
		/// <summary>
		/// 英雄卡道具领取查询
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet FJ_AccountReward__Query(string ServerIP,string account)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,20)
											   };
				paramCode[0].Value = ServerIP;
				paramCode[1].Value = account;
				result = SqlHelper.ExecSPDataSet("FJ_AccountReward_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+ServerIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 英雄卡道具领取道具明细查询
		/// <summary>
		/// 英雄卡道具领取道具明细查询
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet FJ_RewardInfo_Query(string ServerIP,string city,string Account,string style)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_style",SqlDbType.VarChar,30)
											   };
				paramCode[0].Value = ServerIP;
				paramCode[1].Value = city;
				paramCode[2].Value = Account;
				paramCode[3].Value = style;
				result = SqlHelper.ExecSPDataSet("FJ_RewardInfo_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+ServerIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region GM玩家日志查询
		/// <summary>
		/// GM玩家日志查询
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet GMLog_Query(string ServerIP,string city,string char_name,int actionType,DateTime beginDate,DateTime endDate)
		{
			DataSet result = new DataSet();
			DataSet dv = null;
			string strSQL = "";
			SqlParameter[] paramCode;
			try
			{
				System.TimeSpan interval = endDate - beginDate;
				int dayofyears = beginDate.DayOfYear;
				int endofyears = endDate.DayOfYear;
				int nowsofyears = DateTime.Now.DayOfYear;
				int days = interval.Days;
				if(actionType==2)
				{
					strSQL="select server_name,char_name,act_time,loc_x,loc_y,loc_z,target_name,command_content  from FJLogin.dbo.gm_log where char_name ='"+char_name+"' and server_name='"+city.Substring(city.IndexOf('(')+1,city.Length-city.IndexOf('(')-2)+"' and  act_time >=convert(varchar(10),'"+beginDate.ToString("yyyy-MM-dd")+"',120)  and act_time <=convert(varchar(10),'"+endDate.ToString("yyyy-MM-dd")+"',120) order by act_time desc";
					if((dayofyears % 2)==0)
					{
						result = SqlHelper.ExecSQL("60.206.14.44",strSQL);

					}
					else
					{
						result = SqlHelper.ExecSQL("60.206.14.43",strSQL);
					}
				}
				else if(actionType==1)
				{
					for(int i=0;i<=days;i++)
					{
						strSQL="select server_name,char_name,act_time,loc_x,loc_y,loc_z,target_name,command_content  from FJ_LOG_BAK.dbo.gm_log_"+dayofyears+" where char_name ='"+char_name+"' and server_name='"+city.Substring(city.IndexOf('(')+1,city.Length-city.IndexOf('(')-2)+"' and  act_time >=convert(varchar(10),'"+beginDate.ToString("yyyy-MM-dd")+"',120)  and act_time <=convert(varchar(10),'"+endDate.ToString("yyyy-MM-dd")+"',120) order by act_time desc";
						dv = SqlHelper.ExecSQL("60.206.14.28",strSQL);
						if(dv!=null && dv.Tables[0].Rows.Count>0)
						{
							result.Merge(dv.Tables[0],false,System.Data.MissingSchemaAction.Add);
						}
						dayofyears+=1;
					}
					
				}
				else
				{
					paramCode = new SqlParameter[5]{
													   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
													   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
													   new SqlParameter("@FJ_Char_Name",SqlDbType.VarChar,20),
													   new SqlParameter("@FJ_beginDate",SqlDbType.DateTime),
													   new SqlParameter("@FJ_endDate",SqlDbType.DateTime),
					};
					paramCode[0].Value = ServerIP;
					paramCode[1].Value = city;
					paramCode[2].Value = char_name;
					paramCode[3].Value = beginDate;
					paramCode[4].Value = endDate;
					result = SqlHelper.ExecSPDataSet("FJ_GMUser_Query",paramCode);
				}
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+ServerIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region GM玩家日志查询
		/// <summary>
		/// GM玩家日志查询
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet GMUserBanLog_Query(string ServerIP,string city,string account)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,20),
				};
				paramCode[0].Value = ServerIP;
				paramCode[1].Value = city;
				paramCode[2].Value = account;
				result = SqlHelper.ExecSPDataSet("FJ_GMBanAccount_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+ServerIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region GM帐号的添加删除、权限等级设定
		/// <summary>
		/// GM帐号的添加删除、权限等级设定
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static int GMAccount_Update(int GMUser,string ServerIP,string city,string account,int power)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@GM_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_Power",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)
											   };
				paramCode[0].Value = GMUser;
				paramCode[1].Value = ServerIP;
				paramCode[2].Value = city;
				paramCode[3].Value = account;
				paramCode[4].Value = power;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_GMUser_Update",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+ServerIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查询玩家使用外挂情况
		/// <summary>
		/// 查询玩家使用外挂情况
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet UserKill_Query(string ServerIP,string account,string tableName,string city)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,50),
												   new SqlParameter("@FJ_TablesName",SqlDbType.VarChar,50),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,50)
											   };
				paramCode[0].Value = ServerIP;
				paramCode[1].Value = account;
				paramCode[2].Value = tableName;
				paramCode[3].Value = city;
				result = SqlHelper.ExecSPDataSet("FJ_KillUser_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+ServerIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查询玩家充值信息
		/// <summary>
		/// 查询玩家充值信息
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet FJ_AccountDeposit_Query(string ServerIP,string account,string city)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,50),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,50)
											   };
				paramCode[0].Value = ServerIP;
				paramCode[1].Value = city;
				paramCode[2].Value = account;
				result = SqlHelper.ExecSPDataSet("FJ_AccountDeposit_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+ServerIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 重置玩家充值信息
		/// <summary>
		/// 重置玩家充值信息
		/// </summary>
		/// <param name="userByID"></param>
		/// <param name="serverIP"></param>
		/// <param name="charname"></param>
		/// <param name="taskID"></param>
		/// <param name="taskState"></param>
		/// <returns></returns>
		public static int FJ_AccountDeposit_Update(int userByID,string serverIP,string city,string account,int paySN)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_PaySN",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = city;
				paramCode[3].Value = account;
				paramCode[4].Value = paySN;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_AccountDeposit_Update",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 玩家积分信息
		/// <summary>
		/// 玩家积分信息
		/// </summary>
		public static int FJ_UserChargeConsume(string city,string account,int paySN)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
				
												   new SqlParameter("@userID",SqlDbType.VarChar,30),
												   new SqlParameter("@city",SqlDbType.VarChar,30),
												   new SqlParameter("@deductMoney",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = account;
				paramCode[1].Value = city;
				paramCode[2].Value = paySN;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_UserCharge_Consume",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+city+ex.Message);
			}
			return result;
		}
		#endregion

		public static DataSet FJ_CreditTime_Query(string ServerIP,string account,string querytime,string endtime,int acttype)
		{
			DataSet result = new DataSet();
			DataSet dv = null;
			string strSQL = "";
			string tablename=querytime+"00";
			int tab =int.Parse(tablename);

			try
			{	
				for(int i=0;i<12;i++)
				{
					strSQL="select server_name,account_name,pre_credit,post_credit,pre_time,post_time,log_time from credit_time_log"+tab.ToString()+" where account_name='"+account+"'";
					dv = SqlHelper.ExecSQL(ServerIP,strSQL,"fj");
					if(dv!=null && dv.Tables[0].Rows.Count>0)
					{
						result.Merge(dv.Tables[0],false,System.Data.MissingSchemaAction.Add);
					}
					tab=tab+2;

				}
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+ServerIP+ex.Message);
			}
			return result;
			
		}
		public static DataSet FJ_MakeDream_Query(string ServerIP,string charname,string querytime,string logtype,string endtime,int acttype)
		{
			DataSet result = new DataSet();
			DataSet dv = null;
			string strSQL = "";
			string tablename=querytime+"00";
			int tab =int.Parse(tablename);
			string ip =SqlHelper.ExecuteScalar("select serverIP from GMTools_Serverinfo where city in (select city from GMTools_Serverinfo where gameflag=1 and serverIP='"+ServerIP+"') and gameDBID=3 order by serverIP desc").ToString();
			try
			{	
				//				if(acttype==1)
				//				{
				for(int i=0;i<12;i++)
				{
					strSQL="select server_name,char_name,value1,log_type,log_time from player_log"+tab.ToString()+" where char_name='"+charname+"' and log_type='"+logtype+"'";
					dv = SqlHelper.ExecSQL(ip,strSQL,"fjlog");
					if(dv!=null && dv.Tables[0].Rows.Count>0)
					{
						result.Merge(dv.Tables[0],false,System.Data.MissingSchemaAction.Add);
					}
					tab=tab+2;
			
				}
				//				}
				//				else
				//				{
				//					strSQL="select server_name,char_name,value1,log_type,log_time from player_log"+tab.ToString()+" where char_name='"+charname+"' and log_type='"+logtype+"'";
				//					result = SqlHelper.ExecSQL("60.206.14.44",strSQL);
				//
				//				}
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+ServerIP+ex.Message);
			}
			return result;
			
		}
	}
}
