using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
using Common.DataInfo;
namespace SDO.SDODataInfo
{
	/// <summary>
	/// ItemShopInfo 的摘要说明。
	/// </summary>
	public class ItemShopInfo
	{
		public ItemShopInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 查看玩家身上物品道具
		/// <summary>
		/// 查看玩家身上道具信息
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet itemShop_Query(string serverIP,int userIndexID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_UserIndexID",SqlDbType.Int)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=userIndexID;
				result = SqlHelper.ExecSPDataSet("SDO_ItemShopNew_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 删除玩家身上道具
		/// <summary>
		/// 删除玩家物品信息
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int itemShop_Delete(int userByID,string serverIP,int userIndexID,int itemCode)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_UserIndexID",SqlDbType.Int),
												   new SqlParameter("@SDO_ItemCode",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=userIndexID;
				paramCode[3].Value=itemCode;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_ItemShopNew_del",paramCode);
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
		#region 查看玩家礼物盒的道具
		/// <summary>
		/// 查看玩家礼物盒的道具
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet SDOGiftBox_Query(string serverIP,int userIndexID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_UserIndexID",SqlDbType.Int)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=userIndexID;
				result = SqlHelper.ExecSPDataSet("SDO_GiftBox_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看道具的状态
		/// <summary>
		/// 查看道具的状态
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="itemCode">道具代码</param>
		/// <returns></returns>
		public static DataSet itemShop_Status_Query(string serverIP,int itemCode)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_Itemcode",SqlDbType.Int)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=itemCode;
				result = SqlHelper.ExecSPDataSet("SDO_ItemShop_Query2",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看游戏里面道具列表
		/// <summary>
		/// 查看游戏里面道具列表
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="bigType">道具大类</param>
		/// <param name="smallType">道具小类</param>
		/// <param name="itemName">道具名称</param>
		/// <returns>道具数据集</returns>
		public static DataSet itemShop_QueryAll(string serverIP,int bigType,int smallType,string itemName)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_BigType",SqlDbType.Int),
												   new SqlParameter("@SDO_SmallType",SqlDbType.Int),
												   new SqlParameter("@SDO_ItemName",SqlDbType.VarChar,50)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=bigType;
				paramCode[2].Value=smallType;
				paramCode[3].Value=itemName;
				result = SqlHelper.ExecSPDataSet("SDO_ItemShopNew_Query_ALL",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 添加玩家礼物盒上物品
		/// <summary>
		/// 添加玩家礼物盒上物品
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int giftBox_Insert(int userByID,string serverIP,int userIndexID,int itemCode,string title,string context,int timesLimit,DateTime dateLimit)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[9]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_UserIndexID",SqlDbType.Int),
												   new SqlParameter("@SDO_Title",SqlDbType.VarChar,40),
												   new SqlParameter("@SDO_Content",SqlDbType.VarChar,400),
												   new SqlParameter("@SDO_ItemCode",SqlDbType.Int),
												   new SqlParameter("@SDO_TimesLimit",SqlDbType.Int),
												   new SqlParameter("@SDO_DateLimit",SqlDbType.DateTime),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=userIndexID;
				paramCode[3].Value=title;
				paramCode[4].Value=context;
				paramCode[5].Value=itemCode;
				paramCode[6].Value=timesLimit;
				paramCode[7].Value=dateLimit;
				paramCode[8].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_ItemShop_Insert",paramCode);
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
		#region 删除玩家礼物盒上道具
		/// <summary>
		/// 删除玩家礼物盒上道具
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int giftBox_Delete(int userByID,string serverIP,int userIndexID,int itemCode)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_UserIndexID",SqlDbType.Int),
												   new SqlParameter("@SDO_ItemCode",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=userIndexID;
				paramCode[3].Value=itemCode;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_GiftBox_del",paramCode);
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
		#region 检测玩家上下线记录
		/// <summary>
		/// 检测玩家上下线记录
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet userOnline_Query(string serverIP,string account)
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
				result = SqlHelper.ExecSPDataSet("SDO_UserOnline_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 检测单个道具使用期限
		public static DataSet itemlimit_Query(int productID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@SDO_productID",SqlDbType.Int)};
				paramCode[0].Value=productID;
				result = SqlHelper.ExecSPDataSet("SDO_ItemLimit_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 检测玩家消费记录
		/// <summary>
		/// 检测玩家消费记录
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet userConsume_Query(string serverIP,string account,int moneyType,DateTime beginDate,DateTime endDate)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_MoneyType",SqlDbType.Int),
												   new SqlParameter("@SDO_BeginTime",SqlDbType.DateTime),
												   new SqlParameter("@SDO_EndTime",SqlDbType.DateTime)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=account;
				paramCode[2].Value=moneyType;
				paramCode[3].Value = beginDate;
				paramCode[4].Value = endDate;
				result = SqlHelper.ExecSPDataSet("SDO_UserConsume_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
        #region 检测玩家消费记录合计
        /// <summary>
        /// 检测玩家消费记录合计
        /// </summary>
        /// <param name="serverIP">服务器IP</param>
        /// <param name="account">玩家帐号</param>
        /// <returns></returns>
        public static DataSet userConsume_QuerySum(string serverIP, string account, int moneyType, DateTime beginDate, DateTime endDate)
        {
            DataSet result = null;
            SqlParameter[] paramCode;
            try
            {
                paramCode = new SqlParameter[5]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_MoneyType",SqlDbType.Int),
												   new SqlParameter("@SDO_BeginTime",SqlDbType.DateTime),
												   new SqlParameter("@SDO_EndTime",SqlDbType.DateTime)};
                paramCode[0].Value = serverIP;
                paramCode[1].Value = account;
                paramCode[2].Value = moneyType;
                paramCode[3].Value = beginDate;
                paramCode[4].Value = endDate;
                result = SqlHelper.ExecSPDataSet("SDO_UserConsume_QuerySum", paramCode);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion
		#region 检测玩家交易记录
		/// <summary>
		/// 检测玩家交易记录
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet userTrade_Query(string serverIP,string sendUserID,string recvUserID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_SenderUserID",SqlDbType.VarChar,20),
                                                   new SqlParameter("@SDO_ReceiveUserID",SqlDbType.VarChar,20)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=sendUserID;
                paramCode[2].Value = recvUserID;
				result = SqlHelper.ExecSPDataSet("SDO_UserTrade_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 检测玩家积分合计
		/// <summary>
		/// 检测玩家积分合计
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet userIntegral_Query(string serverIP,string account)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_UserID",SqlDbType.VarChar,20)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=account;
				result = SqlHelper.ExecSPDataSet("SDO_UserIntegral_QuerySum",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 查询玩家闯关获得道具
		/// <summary>
		/// 查询玩家闯关获得道具
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet SDO_StageAward_Query(string serverIP)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30)};
				paramCode[0].Value=serverIP;
				result = SqlHelper.ExecSPDataSet("SDO_StageAward_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.log.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 删除玩家闯关获得道具
		/// <summary>
		/// 删除玩家闯关获得道具
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int SDO_StageAward_Delete(int operateUserID,string serverIP,int indexID)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@GM_UserID",SqlDbType.Int),
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_IndexID",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=operateUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=indexID;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_StageAward_Delete",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.log.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 插入玩家闯关获得道具
		/// <summary>
		/// 插入玩家闯关获得道具
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int SDO_StageAward_Insert(int GMUserID,string serverIP,int itemCode1,int timeLimit1,int datelimit1,
			int itemCode2,int timeLimit2,int datelimit2,
			int itemCode3,int timeLimit3,int datelimit3,
			int itemCode4,int timeLimit4,int datelimit4,
			int itemCode5,int timeLimit5,int datelimit5)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[18]{
													new SqlParameter("@Gm_UserID",SqlDbType.Int),
													new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
													new SqlParameter("@SDO_ItemCode1",SqlDbType.Int),
													new SqlParameter("@SDO_TimeLimit1",SqlDbType.Int),
													new SqlParameter("@SDO_DateLimit1",SqlDbType.Int),
													new SqlParameter("@SDO_ItemCode2",SqlDbType.Int),
													new SqlParameter("@SDO_TimeLimit2",SqlDbType.Int),
													new SqlParameter("@SDO_DateLimit2",SqlDbType.Int),
													new SqlParameter("@SDO_ItemCode3",SqlDbType.Int),
													new SqlParameter("@SDO_TimeLimit3",SqlDbType.Int),
													new SqlParameter("@SDO_DateLimit3",SqlDbType.Int),
													new SqlParameter("@SDO_ItemCode4",SqlDbType.Int),
													new SqlParameter("@SDO_TimeLimit4",SqlDbType.Int),
													new SqlParameter("@SDO_DateLimit4",SqlDbType.Int),
													new SqlParameter("@SDO_ItemCode5",SqlDbType.Int),
													new SqlParameter("@SDO_TimeLimit5",SqlDbType.Int),
													new SqlParameter("@SDO_DateLimit5",SqlDbType.Int),
													new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=itemCode1;
				paramCode[3].Value=timeLimit1;
				paramCode[4].Value=datelimit1;
				paramCode[5].Value=itemCode2;
				paramCode[6].Value=timeLimit2;
				paramCode[7].Value=datelimit2;
				paramCode[8].Value=itemCode3;;
				paramCode[9].Value=timeLimit3;
				paramCode[10].Value=datelimit3;
				paramCode[11].Value=itemCode4;
				paramCode[12].Value=timeLimit4;
				paramCode[13].Value=datelimit4;
				paramCode[14].Value=itemCode5;;
				paramCode[15].Value=timeLimit5;
				paramCode[16].Value=datelimit5;
				paramCode[17].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_StageAward_Insert",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.log.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 修改玩家闯关获得道具
		/// <summary>
		/// 修改玩家闯关获得道具
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int SDO_StageAward_Update(int GMUserID,string serverIP,int itemCode,int itemCode1,int timeLimit1,int datelimit1,
			int itemCode2,int timeLimit2,int datelimit2,
			int itemCode3,int timeLimit3,int datelimit3,
			int itemCode4,int timeLimit4,int datelimit4,
			int itemCode5,int timeLimit5,int datelimit5)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[19]{
													new SqlParameter("@Gm_UserID",SqlDbType.Int),
													new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
													new SqlParameter("@SDO_IndexID",SqlDbType.Int),
													new SqlParameter("@SDO_ItemCode1",SqlDbType.Int),
													new SqlParameter("@SDO_TimeLimit1",SqlDbType.Int),
													new SqlParameter("@SDO_DateLimit1",SqlDbType.Int),
													new SqlParameter("@SDO_ItemCode2",SqlDbType.Int),
													new SqlParameter("@SDO_TimeLimit2",SqlDbType.Int),
													new SqlParameter("@SDO_DateLimit2",SqlDbType.Int),
													new SqlParameter("@SDO_ItemCode3",SqlDbType.Int),
													new SqlParameter("@SDO_TimeLimit3",SqlDbType.Int),
													new SqlParameter("@SDO_DateLimit3",SqlDbType.Int),
													new SqlParameter("@SDO_ItemCode4",SqlDbType.Int),
													new SqlParameter("@SDO_TimeLimit4",SqlDbType.Int),
													new SqlParameter("@SDO_DateLimit4",SqlDbType.Int),
													new SqlParameter("@SDO_ItemCode5",SqlDbType.Int),
													new SqlParameter("@SDO_TimeLimit5",SqlDbType.Int),
													new SqlParameter("@SDO_DateLimit5",SqlDbType.Int),
													new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=itemCode;
				paramCode[3].Value=itemCode1;
				paramCode[4].Value=timeLimit1;
				paramCode[5].Value=datelimit1;
				paramCode[6].Value=itemCode2;
				paramCode[7].Value=timeLimit2;
				paramCode[8].Value=datelimit2;
				paramCode[9].Value=itemCode3;;
				paramCode[10].Value=timeLimit3;
				paramCode[11].Value=datelimit3;
				paramCode[12].Value=itemCode4;
				paramCode[13].Value=timeLimit4;
				paramCode[14].Value=datelimit4;
				paramCode[15].Value=itemCode5;;
				paramCode[16].Value=timeLimit5;
				paramCode[17].Value=datelimit5;
				paramCode[18].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_StageAward_Update",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.log.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家宠物信息
		/// <summary>
		/// 查看玩家宠物信息
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet SDOPetInfo_Query(string serverIP,int userIndexID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_UserIndexID",SqlDbType.Int)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=userIndexID;
				result = SqlHelper.ExecSPDataSet("SDO_PetInfo_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.log.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查询宝箱信息
		/// <summary>
		/// 查询宝箱信息
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns></returns>
		public static DataSet baoxiang_Query(string serverIP)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=serverIP;
				paramCode[1].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPDataSet("SDO_BAOXIANG_SPE_CON_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.log.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 修改宝箱概率
		/// <summary>
		/// 修改宝箱概率
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns></returns>
		public static int SDO_BAOXIANG_EPUpdate(int GMUserID,string serverIP,int intid,int intPreValue,int intEndValue,int intNorProFirst,
			int iNorPro,int iSpePro)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[9]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@SDO_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_Msgid",SqlDbType.Int),
												   new SqlParameter("@SDO_PreValue",SqlDbType.Int),
												   new SqlParameter("@SDO_EndValue",SqlDbType.Int),
												   new SqlParameter("@SDO_NorProFirst",SqlDbType.Int),
												   new SqlParameter("@SDO_NorPro",SqlDbType.Int),
												   new SqlParameter("@SDO_SpePro",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=intid;
				paramCode[3].Value=intPreValue;
				paramCode[4].Value=intEndValue;
				paramCode[5].Value=intNorProFirst;
				paramCode[6].Value=iNorPro;
				paramCode[7].Value=iSpePro;
				paramCode[8].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_BAOXIANG_SPE_CON_Update",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.log.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
	}
}
