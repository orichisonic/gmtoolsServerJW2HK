using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
using System.IO;
using System.Xml;
using System.Web;
using System.Net;
using System.Web.Mail;
using Common.API;
using Common.DataInfo;
namespace RayCity.RayCityDataInfo
{
	/// <summary>
	/// RayCityGlobalAccount 的摘要说明。
	/// </summary>
	public class GlobalAccount
	{
		public GlobalAccount()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public static DataSet BasicAccount_Query(string serverIP,string account,string userNick)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@RayCity_UserID",SqlDbType.VarChar,20),												  
												   new SqlParameter("@RayCity_UserNick",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = account;
				paramCode[2].Value = userNick;
				result = SqlHelper.ExecSPDataSet("RayCity_AccountInfo",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#region 查询玩家的邮件
		/// <summary>
		/// 查询玩家的邮件
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static string SDOEmail_Query(string account)
		{
			XmlDocument xmlfile= new XmlDocument();

			string md5string = MD5EncryptAPI.MDString(account+"QUXUEXIBA");
			System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=999");
			if(ds!=null && ds.Tables[0].Rows.Count>0)
			{
				string serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
				string url = "http://"+serverIP+"/user?req=getUserInfo&id=GMTOOLS&userid="+account+"&usertype=0&type=userinfo&s="+md5string;
				HttpWebRequest  request  = (HttpWebRequest)
					WebRequest.Create(url);
				request.KeepAlive=false;
				WebResponse resp = request.GetResponse();
				StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
				xmlfile.Load(sr);
				XmlNode email = xmlfile.SelectSingleNode("you9/userinfo/email");
				sr.Close();
				return email.InnerText;
			}
			return null;
		}
		#endregion
		public static string CharacterName_Query(string serverIP,int characterID)
		{
			DataSet result = null;
			string AccountName = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@CharacterID",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = characterID;
				result = SqlHelper.ExecSPDataSet("RayCity_AccountName_Query",paramCode);
				if(result!=null && result.Tables[0].Rows.Count>0)
				{
					AccountName = result.Tables[0].Rows[0][0].ToString();

				}
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return AccountName;
		}
		public static DataSet BasicCharacter_Query(string serverIP,int accountID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@AcountID",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = accountID;
				result = SqlHelper.ExecSPDataSet("RayCity_BasicCharacterInfo",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		
		public static DataSet Guild_Query(string serverIP,string guildName)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@RayCity_GuildName",SqlDbType.VarChar,30),};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = guildName;
				result = SqlHelper.ExecSPDataSet("RayCity_Guild",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		public static DataSet GuildMember_Query(string serverIP,int guildID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@RayCity_GuildID",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = guildID;
				result = SqlHelper.ExecSPDataSet("RayCity_GuildMember",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		public static DataSet GMAccount_Query(string serverIP,string NyUserID,int accountState)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@RayCity_NyUserID",SqlDbType.VarChar,20),
												   new SqlParameter("@accountState",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = NyUserID;
				paramCode[2].Value = accountState;
				result = SqlHelper.ExecSPDataSet("RayCity_GMUser1_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		public static int GMAccount_Update(int GM_UserID,string serverIP,string NyUserID,int accountState)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@GM_UserID",SqlDbType.Int),
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@RayCity_NyUserID",SqlDbType.VarChar,30),
												   new SqlParameter("@accountState",SqlDbType.Int),
								 				   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = GM_UserID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = NyUserID;
				paramCode[3].Value = accountState;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("RayCity_GMAccount_Update",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		public static int Get_WarehousePassword(int GM_UserID,string serverIP,int characterID,string account,string sourcePwd,string passwd)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[7]{
												   new SqlParameter("@GM_UserID",SqlDbType.Int),
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@CharacterID",SqlDbType.Int),
												   new SqlParameter("@Account",SqlDbType.VarChar,30),
												   new SqlParameter("@SourcePwd",SqlDbType.VarChar,20),
												   new SqlParameter("@warehousePwd",SqlDbType.VarChar,32),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = GM_UserID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = characterID;
				paramCode[3].Value = account;
				paramCode[4].Value = sourcePwd;
				paramCode[5].Value = passwd;
				paramCode[6].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("RayCity_WareHousePwd_Update",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
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
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30)};
				paramCode[0].Value = serverIP;
				result = SqlHelper.ExecSPDataSet("RayCity_BoardList_Query",paramCode);
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
		public static int RayCityBoardList_Insert(int userByID,string serverIP,string city,string Message,DateTime startTime,DateTime endTime,int interval)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[7]{
													new SqlParameter("@Gm_UserID",SqlDbType.Int),
													new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
													new SqlParameter("@RayCity_Message",SqlDbType.VarChar,980),
													new SqlParameter("@RayCity_StartTime",SqlDbType.DateTime),
													new SqlParameter("@RayCity_End_time",SqlDbType.DateTime),
													new SqlParameter("@RayCity_Interval",SqlDbType.Int),
													new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = Message;
				paramCode[3].Value = startTime;
				paramCode[4].Value = endTime;
				paramCode[5].Value = interval;
				paramCode[6].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("RayCity_BoardMsg_Create",paramCode);
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
		public static int RayCityBoardList_Delete(int userByID,string serverIP,int boardID)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@NoticeIDX",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = boardID;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("RayCity_BoardMsg_Delete",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		
	}
}
