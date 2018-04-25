using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common.DataInfo;
using Common.Logic;
namespace SDO.SDODataInfo
{
    class ItemLogInfo
    {
        /// <summary>
        /// 用户的充值明细查询
        /// </summary>
        /// <param name="serverIP">服务器地址</param>
        /// <param name="account">玩家帐号</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static DataSet userChargeDetail_Query(string serverIP, string account,DateTime beginTime,DateTime endTime)
        {
            DataSet ds = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[4]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
                                                   new SqlParameter("@SDO_UserID ",SqlDbType.VarChar,20),
                                                   new SqlParameter("@SDO_BeginTime ",SqlDbType.DateTime),
                                                   new SqlParameter("@SDO_EndTime ",SqlDbType.DateTime)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = account;
                paramCode[2].Value = beginTime;
                paramCode[3].Value = endTime;
                ds = SqlHelper.ExecSPDataSet("SDO_M_log_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
        /// <summary>
        /// 用户的充值明细合计
        /// </summary>
        /// <param name="serverIP">服务器地址</param>
        /// <param name="account">玩家帐号</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public static DataSet userChargeSum_Query(string serverIP, string account, DateTime beginTime, DateTime endTime)
        {
            DataSet ds = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[4]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
                                                   new SqlParameter("@SDO_UserID ",SqlDbType.VarChar,20),
                                                   new SqlParameter("@SDO_BeginTime ",SqlDbType.DateTime),
                                                   new SqlParameter("@SDO_EndTime ",SqlDbType.DateTime)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = account;
                paramCode[2].Value = beginTime;
                paramCode[3].Value = endTime;
                ds = SqlHelper.ExecSPDataSet("SDO_M_log_QuerySum", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
        /// <summary>
        /// 用户的Ｇ币合计
        /// </summary>
        /// <param name="serverIP">服务器地址</param>
        /// <param name="account">玩家帐号</param>
        /// <returns></returns>
        public static DataSet userGCash_Query(string serverIP, string account)
        {
            DataSet ds = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[2]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
                                                   new SqlParameter("@SDO_UserID ",SqlDbType.VarChar,20)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = account;
                ds = SqlHelper.ExecSPDataSet("SDO_UserMcash_Query", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
        /// <summary>
        /// 玩家的G币补发
        /// </summary>
        /// <param name="userByID">操作员ID</param>
        /// <param name="serverIP">服务器地址</param>
        /// <param name="account">玩家帐号</param>
        /// <param name="sdo_GCash">补发G币</param>
        /// <returns>补发成功与否</returns>
        public static int SDO_UserMcash_addG(int userByID,string serverIP, string account, int sdo_GCash)
        {
            int result = -1;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
                                                   new SqlParameter("@SDO_GCash",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
                paramCode[0].Value = userByID;
                paramCode[1].Value = serverIP;
                paramCode[2].Value = account;
                paramCode[3].Value = sdo_GCash;
                paramCode[4].Direction = ParameterDirection.ReturnValue;
                result = SqlHelper.ExecSPCommand("SDO_UserMcash_addG", paramCode);
				if(userByID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(userByID);
				}
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
