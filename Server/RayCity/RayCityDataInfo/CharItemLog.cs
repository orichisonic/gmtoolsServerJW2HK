using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
using Common.API;
using Common.DataInfo;
namespace RayCity.RayCityDataInfo
{
	/// <summary>
	/// CharItemLog 的摘要说明。
	/// </summary>
	public class CharItemLog
	{
		public CharItemLog()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 查看玩家充值的记录
		/// <summary>
		/// 查看玩家充值的记录
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet UserChargeInfo_Query(string serverIP,int accountID,DateTime startDate,DateTime endDate)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AccountID",SqlDbType.Int),
												   new SqlParameter("@StartDate",SqlDbType.SmallDateTime),
												   new SqlParameter("@EndDate",SqlDbType.SmallDateTime),};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = accountID;
				paramCode[2].Value = startDate;
				paramCode[3].Value = endDate;
				result = SqlHelper.ExecSPDataSet("RayCity_UserChargeData_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家购买车的记录
		/// <summary>
		/// 查看玩家购买车的记录
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet BuyCarLog_Query(int index,int pageSize,string serverIP,int characterID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@CharacterID",SqlDbType.Int),
											   	   new SqlParameter("@startRowIndex",SqlDbType.Int),
												   new SqlParameter("@maximumRows",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = characterID;
				paramCode[2].Value = index;
				paramCode[3].Value = pageSize;
				result = SqlHelper.ExecSPDataSet("RayCity_BuyCarLogSelect",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家道具买卖交易的记录
		/// <summary>
		/// 查看玩家道具买卖交易的记录
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet BuySellItemInfo_Query(int index,int pageSize,string serverIP,int characterID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@CharacterID",SqlDbType.Int),
												   new SqlParameter("@startRowIndex",SqlDbType.Int),
												   new SqlParameter("@maximumRows",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = characterID;
				paramCode[2].Value = index;
				paramCode[3].Value = pageSize;
				result = SqlHelper.ExecSPDataSet("RayCity_ItemBuySellLogSelect",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家道具交易的记录
		/// <summary>
		/// 查看玩家道具交易的记录
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet TradeList_Query(string serverIP,int characterID,int sessionState,DateTime startDate,DateTime endDate)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@CharacterID",SqlDbType.Int),
												   new SqlParameter("@TradeState",SqlDbType.TinyInt),
												   new SqlParameter("@StartDate",SqlDbType.DateTime),
												   new SqlParameter("@EndDate",SqlDbType.DateTime)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = characterID;
			    paramCode[2].Value = sessionState;
				paramCode[3].Value = startDate;
				paramCode[4].Value = endDate;
				result = SqlHelper.ExecSPDataSet("RayCity_GetTradeSelect",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家道具交易的记录明细
		/// <summary>
		/// 查看玩家道具交易的记录明细
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet TradeInfoDetail_Query(string serverIP,int characterID,int TradeSessionIDX)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@CharacterID",SqlDbType.Int),
												   new SqlParameter("@TradeSessionIDX",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = characterID;
				paramCode[2].Value = TradeSessionIDX;
				result = SqlHelper.ExecSPDataSet("RayCity_GetTradeInfo",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家合成道具的记录
		/// <summary>
		/// 查看玩家合成道具的记录
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet BingoCard_Query(string serverIP,int characterID,int sessionState,DateTime startDate,DateTime endDate)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@CharacterID",SqlDbType.Int),
												   new SqlParameter("@BingoCardState",SqlDbType.TinyInt),
												   new SqlParameter("@StartDate",SqlDbType.DateTime),
												   new SqlParameter("@EndDate",SqlDbType.DateTime)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = characterID;
				paramCode[2].Value = sessionState;
				paramCode[3].Value = startDate;
				paramCode[4].Value = endDate;
				result = SqlHelper.ExecSPDataSet("RayCity_GetBingoCardSelect",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家登入登出的记录
		/// <summary>
		/// 查看玩家登入登出的记录
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet LoginOut_Query(int index,int pageSize,string serverIP,int characterID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@CharacterID",SqlDbType.Int),
												   new SqlParameter("@startRowIndex",SqlDbType.Int),
												   new SqlParameter("@maximumRows",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = characterID;
				paramCode[2].Value = index;
				paramCode[3].Value = pageSize;
				result = SqlHelper.ExecSPDataSet("RayCity_ConnectionLogSelect",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家过关的记录
		/// <summary>
		/// 查看玩家过关的记录
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet MissionLog_Query(int index,int pageSize,string serverIP,int characterID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@CharacterID",SqlDbType.Int),
												   new SqlParameter("@startRowIndex",SqlDbType.Int),
												   new SqlParameter("@maximumRows",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = characterID;
				paramCode[2].Value = index;
				paramCode[3].Value = pageSize;
				result = SqlHelper.ExecSPDataSet("RayCity_MissionLogSelect",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家任务的记录
		/// <summary>
		/// 查看玩家任务的记录
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet QuestLog_Query(int index,int pageSize,string serverIP,int questState,int characterID,int carIDX,DateTime startTime,DateTime endTime)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[8]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@CharacterID",SqlDbType.Int),
												   new SqlParameter("@CarIDX",SqlDbType.Int),
												   new SqlParameter("@QuestState",SqlDbType.Int),
												   new SqlParameter("@StartDate",SqlDbType.DateTime),
												   new SqlParameter("@EndDate",SqlDbType.DateTime),
												   new SqlParameter("@startRowIndex",SqlDbType.Int),
												   new SqlParameter("@maximumRows",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = characterID;
				paramCode[2].Value = carIDX;
				paramCode[3].Value = questState;
				paramCode[4].Value = startTime;
				paramCode[5].Value = endTime;
				paramCode[6].Value = index;
				paramCode[7].Value = pageSize;
				result = SqlHelper.ExecSPDataSet("RayCity_QuestLogSelect",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家金钱交易记录
		/// <summary>
		/// 查看玩家金钱交易记录
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet MoneyLog_Query(int index,int pageSize,string serverIP,int characterID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@CharacterID",SqlDbType.Int),
												   new SqlParameter("@startRowIndex",SqlDbType.Int),
												   new SqlParameter("@maximumRows",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = characterID;
				paramCode[2].Value = index;
				paramCode[3].Value = pageSize;
				result = SqlHelper.ExecSPDataSet("RayCity_MoneyLogSelect",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家比赛记录
		/// <summary>
		/// 查看玩家比赛记录
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet RaceLogSelect_Query(int index,int pageSize,string serverIP,int characterID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@CharacterID",SqlDbType.Int),
												   new SqlParameter("@startRowIndex",SqlDbType.Int),
												   new SqlParameter("@maximumRows",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = characterID;
				paramCode[2].Value = index;
				paramCode[3].Value = pageSize;
				result = SqlHelper.ExecSPDataSet("RayCity_RaceLogSelect",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家邮件里面信息
		/// <summary>
		/// 查看玩家邮件里面信息
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet RaceMail_Query(string serverIP,int characterID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@CharacterID",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = characterID;
				result = SqlHelper.ExecSPDataSet("RayCity_Gift_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家消费记录,赠送记录以及余额情况
		/// <summary>
		/// 查看玩家消费记录,赠送记录以及余额情况
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet RaceUserConsume_Query(string serverIP,int characterID,DateTime beginDate,DateTime endDate)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@RayCity_CharacterID",SqlDbType.Int),
												   new SqlParameter("@beginDate",SqlDbType.DateTime),
												   new SqlParameter("@endDate",SqlDbType.DateTime)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = characterID;
				paramCode[2].Value = beginDate;
				paramCode[3].Value = endDate;
				result = SqlHelper.ExecSPDataSet("RayCity_GetCashItemAmount",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家购买道具LOG
		/// <summary>
		/// 查看玩家购买道具LOG
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet CashItemLog_Query(string serverIP,int characterID,DateTime beginDate,DateTime endDate)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@RayCity_CharacterID",SqlDbType.Int),
												   new SqlParameter("@beginDate",SqlDbType.DateTime),
												   new SqlParameter("@endDate",SqlDbType.DateTime)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = characterID;
				paramCode[2].Value = beginDate;
				paramCode[3].Value = endDate;
				result = SqlHelper.ExecSPDataSet("RayCity_CashItemLog_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家详细的道具列表
		/// <summary>
		/// 查看玩家详细的道具列表
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet CashItemDetailLog_Query(string serverIP,int CashItemLogIDX)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@CashItemLogIDX",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = CashItemLogIDX;
				result = SqlHelper.ExecSPDataSet("RayCity_TradeItemDetail_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家详细的道具列表
		/// <summary>
		/// 查看玩家详细的道具列表
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet CashAllPrice_Query(string serverIP,int CashItemLogIDX)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@CashItemLogIDX",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = CashItemLogIDX;
				result = SqlHelper.ExecSPDataSet("RayCity_AllPrice_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 序列号查询及已发送序列号列表
		/// <summary>
		/// 序列号查询及已发送序列号列表
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet Coupon_Query(string serverIP,int couponIDX)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@RayCity_CouponIDX",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = couponIDX;
				result = SqlHelper.ExecSPDataSet("RayCity_Coupon_Query",paramCode);
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
