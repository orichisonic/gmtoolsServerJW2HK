using System;
using System.Xml;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Text;
using Common.Logic;
using Common.API;
using MySql.Data.MySqlClient;
using Common.DataInfo;

namespace GM_Server.JW2DataInfo
{
	/// <summary>
	/// JW2MessengerDataInfo 的摘要说明。
	/// </summary>
	public class JW2MessengerDataInfo
	{
		public JW2MessengerDataInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 查看玩家家族信息
		/// <summary>
		/// 查看玩家家族信息
		/// </summary>
		public static DataSet User_Family_Query(string serverIP,int usersn)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,6);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_User_Family_Query' and sql_condition='JW2_User_Family_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2messengerDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_User_Family_Query_查看玩家"+usersn.ToString()+"家族信息服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查看家族信息
		/// <summary>
		/// 查看玩家家族信息
		/// </summary>
		public static DataSet FAMILYINFO_QUERY(string serverIP,string FamilyName)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,6);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_FAMILYINFO_QUERY' and sql_condition='JW2_FAMILYINFO_QUERY'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,FamilyName);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2messengerDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_FAMILYINFO_QUERY_查看家族"+FamilyName+"家族信息服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查看家族宠物信息
		/// <summary>
		/// 查看家族宠物信息
		/// </summary>
		public static DataSet FamilyPet_Query(string serverIP,int FamilyID)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,6);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_Family_Pet_Query' and sql_condition='JW2_Family_Pet_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,FamilyID);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2messengerDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_FamilyPet_Query_查看家族"+FamilyID.ToString()+"宠物信息服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查看家族成员信息
		/// <summary>
		/// 查看家族成员信息
		/// </summary>
		public static DataSet FAMILYMEMBER_QUERY(string serverIP,int FamilyNameID)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,6);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_FAMILYMEMBER_QUERY' and sql_condition='JW2_FAMILYMEMBER_QUERY'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,FamilyNameID);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2messengerDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_FAMILYMEMBER_QUERY_查看家族"+FamilyNameID.ToString()+"成员信息服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查询家族道具信息
		/// <summary>
		/// 查询家族道具信息
		/// </summary>
		public static DataSet FamilyItemInfo_Query(string serverIP,int FamilyNameID)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,6);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_FamilyItemInfo_Query' and sql_condition='JW2_FamilyItemInfo_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,FamilyNameID);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2messengerDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_FamilyItemInfo_Query_查看家族"+FamilyNameID.ToString()+"道具信息服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查看家族申请中成员信息
		/// <summary>
		/// 查看家族申请中成员信息
		/// </summary>
		public static DataSet FamilyMember_Applying(string serverIP,int FamilyNameID)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,6);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_FamilyMember_Applying' and sql_condition='JW2_FamilyMember_Applying'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,FamilyNameID);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2messengerDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_FamilyMember_Applying_查看家族"+FamilyNameID.ToString()+"申请中成员信息服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查看家族排名信息
		/// <summary>
		/// 查看家族排名信息
		/// </summary>
		public static DataSet BasicRank_Query(string serverIP,int FamilyNameID)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,6);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_BasicRank_Query' and sql_condition='JW2_BasicRank_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,FamilyNameID);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2messengerDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_BasicRank_Query_查看家族"+FamilyNameID.ToString()+"排名信息服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		
		#region 查看Messenger称号
		/// <summary>
		/// 查看Messenger称号
		/// </summary>
		public static DataSet Messenger_Query(string serverIP,int userSN)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,6);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_Messenger_Query' and sql_condition='JW2_Messenger_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userSN);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2messengerDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_Messenger_Query_查看家族"+userSN.ToString()+"称号信息服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查询家族道具购买日志
		/// <summary>
		/// 查询家族道具购买日志
		/// </summary>
		public static DataSet FamilyBuyLog_Query(string serverIP,int FamilyNameID,string BeginTime ,string EndTime)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,6);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_FamilyBuyLog_Query' and sql_condition='JW2_FamilyBuyLog_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,FamilyNameID,BeginTime,EndTime);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2messengerDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_FamilyBuyLog_Query_查看家族"+FamilyNameID.ToString()+"道具购买日志服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 基金日志(0,捐赠,1,消费)
		/// <summary>
		/// 基金日志(0,捐赠,1,消费)
		/// </summary>
		public static DataSet FamilyFund_Log(string serverIP,int FamilyNameID,string BeginTime ,string EndTime,int type)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,6);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_FamilyFund_Log' and sql_condition='"+type+"'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,FamilyNameID,BeginTime,EndTime);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2messengerDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_FamilyFund_Log_查看家族"+FamilyNameID.ToString()+"基金日志服务器IP"+serverIP+type+ex.Message);
			}
			return result;
		}
		#endregion

		#region 家族成员领取小宠物信息查询
		/// <summary>
		/// 家族成员领取小宠物信息查询
		/// </summary>
		public static DataSet SmallPetAgg_Query(string serverIP,int userSN,string BeginTime ,string EndTime)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,6);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_SmallPetAgg_Query' and sql_condition='JW2_SmallPetAgg_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userSN,BeginTime,EndTime);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2messengerDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_SmallPetAgg_Query_查看玩家"+userSN.ToString()+"领取小宠物信息查询服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		
		#region 道具日志
		/// <summary>
		/// 道具日志
		/// </summary>
		public static DataSet Item_Log(string serverIP,int userSN,string BeginTime ,string EndTime)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,6);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_Item_Log' and sql_condition='JW2_Item_Log'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userSN,BeginTime,EndTime);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2messengerDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_Item_Log_查看玩家"+userSN.ToString()+"道具日志查询服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查看基地信息
		/// <summary>
		/// 查看玩家家族信息
		/// </summary>
		public static DataSet BasicInfo_Query(string serverIP,int FamilyNameID)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,6);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_BasicInfo_Query' and sql_condition='JW2_BasicInfo_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,FamilyNameID);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2messengerDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_BasicInfo_Query_查看家族"+FamilyNameID.ToString()+"基本信息服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查詢紙條箱
		/// <summary>
		/// 查詢紙條箱
		/// </summary>
		public static DataSet MailInfo_Query(string serverIP,int userSN,string BeginTime,string EndTime)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,6);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_MailInfo_Query' and sql_condition='JW2_MailInfo_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userSN,BeginTime,EndTime);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2messengerDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_MailInfo_Query_查看玩家"+userSN.ToString()+"紙條箱信息服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
	
		#region 修改家族名
		/// <summary>
		/// 修改家族名
		/// </summary>
		public static int UpdateFamilyName_Query(string serverIP,string OLD_familyName,string familyName,int userByID,int familyID)
		{
			int  result = -1;
			string sql = null;
			try
			{
				//修改等级1
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,9);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_UpdateFamilyName_Query' and sql_condition = 'JW2_UpdateFamilyName_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,familyID,familyName);
					result = MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2messengerDB),sql);
				}
				if(result==1)
				{
					SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_UpdateFamilyName_Query","修改家族名："+OLD_familyName.ToString()+"，为新家族名："+familyName.ToString()+"，成功(修改家族名,jw2)");
				}
				else
				{
					SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_UpdateFamilyName_Query","修改家族名："+OLD_familyName.ToString()+"，为新家族名："+familyName.ToString()+"，失败(修改家族名,jw2)");
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		
		
	}
}
