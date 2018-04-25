using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
using Common.DataInfo;

namespace SDO.SDODataInfo
{
	/// <summary>
	/// MemberInfo 的摘要说明。
	/// </summary>
	public class MemberInfo
	{
		public MemberInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 查看玩家的帐号信息，以及帐号的大区激活状态
		/// <summary>
		/// 查看玩家的帐号信息，以及帐号的大区激活状态
		/// </summary>
		/// <returns></returns>
		public static DataSet memberInfo_Query(string serverIP,string account)
		{
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=account;
				paramCode[2].Direction = ParameterDirection.ReturnValue;
				DataSet ds = SqlHelper.ExecSPDataSet("SDO_MemberNew_Query",paramCode);
				if(ds.Tables[0].Rows.Count>0)
				{
					return ds;
				}
				else
					return null;

			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
            
		}
		#endregion
		#region 即时查看玩家当前状态(服务器/房间/在线状态)
		/// <summary>
		/// 即时查看玩家当前状态(服务器/房间/在线状态)
		/// </summary>
		/// <returns></returns>
		public static DataSet T_o2jam_login_Query(string serverIP,string account)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=account;
				paramCode[2].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPDataSet("SDO_LoginNew_Status",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
        #region 即时查看服务器端所有玩家帐号封停状态
        /// <summary>
        /// 即时查看服务器端所有玩家帐号封停状态
        /// </summary>
        /// <returns></returns>
        public static DataSet SDO_Banishment_QueryAll(string serverIP)
        {
            DataSet ds = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[2]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@result",SqlDbType.Int)};
                paramCode[0].Value = serverIP;
                paramCode[1].Direction = ParameterDirection.ReturnValue;
                ds = SqlHelper.ExecSPDataSet("SDO_Banishment_QueryAll", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
        #endregion
        #region 查看本地玩家帐号封停状态
        /// <summary>
        /// 即时查看本地玩家帐号封停状态
        /// </summary>
        /// <returns></returns>
        public static DataSet SDO_BanishmentLocal_Query(string serverIP,string account)
        {
            DataSet ds = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[3]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
                                                   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = account;
                paramCode[2].Direction = ParameterDirection.ReturnValue;
                ds = SqlHelper.ExecSPDataSet("SDO_AccountTemp_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
        #endregion
		#region 即时查看该玩家帐号封停状态
		/// <summary>
		/// 即时查看该玩家帐号封停状态
		/// </summary>
		/// <returns></returns>
		public static int SDO_Banishment_Query(string serverIP,string account)
		{
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=account;
				paramCode[2].Direction = ParameterDirection.ReturnValue;
				DataSet ds = SqlHelper.ExecSPDataSet("SDO_Banishment_Query",paramCode);
				if(ds.Tables[0].Rows.Count>0)
				{
					return 1;
				}
				else
					return 0;
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
				return -1;
			}
		}
		#endregion
		#region 封停超级舞者的帐号
		/// <summary>
		/// 封停超级舞者的帐号
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int SDO_Banishment_Close(int userByID,string serverIP,string account,string reason,int banDate)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
                                                   new SqlParameter("@SDO_Reason",SqlDbType.VarChar,500),
												   new SqlParameter("@SDO_BanDate",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=account;
                paramCode[3].Value = reason;
				paramCode[4].Value = banDate;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_Banishment_Close",paramCode);
				if(userByID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(userByID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;

		}
		#endregion
		

		#region 解封超级舞者的帐号
		/// <summary>
		/// 解封超级舞者的帐号
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int SDO_Banishment_Open(int userByID,string serverIP,string account)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=account;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_Banishment_Open",paramCode);
				if(userByID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(userByID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;

		}
		#endregion
	}
}
