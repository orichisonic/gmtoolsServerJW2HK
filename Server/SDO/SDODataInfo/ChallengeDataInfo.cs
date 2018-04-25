using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
using Common.DataInfo;

namespace SDO.SDODataInfo
{
	/// <summary>
	/// ChallengeDataInfo 的摘要说明。
	/// </summary>
	public class ChallengeDataInfo
	{
		public ChallengeDataInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 查看游戏里面大赛列表
		/// <summary>
		/// 查看游戏里面场景列表
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static DataSet TO2JAM_Challenge_Query(string serverIP)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30)};
				paramCode[0].Value=serverIP;
				result = SqlHelper.ExecSPDataSet("SDO_TO2JAM_Challenge_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看游戏里面音乐列表
		/// <summary>
		/// 查看游戏里面音乐列表
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static DataSet TO2JAM_MusicData_Query(string serverIP)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30)};
				paramCode[0].Value=serverIP;
				result = SqlHelper.ExecSPDataSet("SDO_TO2JAM_MusicData_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 根据ID查看游戏里面音乐列表
		/// <summary>
		/// 根据ID查看游戏里面音乐列表
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static DataSet TO2JAM_MusicData_Query(string serverIP,int musicID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_MusicID",SqlDbType.Int)};
				paramCode[0].Value =serverIP;
				paramCode[1].Value = musicID;
				result = SqlHelper.ExecSPDataSet("SDO_TO2JAM_MusicData_SingleQuery",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看游戏里面场景列表
		/// <summary>
		/// 查看游戏里面场景列表
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static DataSet TO2JAM_SceneListQuery()
		{
			DataSet result = null;
			try
			{
				result = SqlHelper.ExecSPDataSet("SDO_TO2JAM_Scene_Query");
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 添加游戏里面场景列表
		/// <summary>
		/// 添加游戏里面场景
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static int TO2JAM_ChallengeScene_Insert(int GMUserID,int sceneID,string sceneTag)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
													new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
													new SqlParameter("@SDO_SceneID",SqlDbType.Int),
													new SqlParameter("@SDO_SceneTag",SqlDbType.VarChar,400),
													new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=sceneID;
				paramCode[2].Value=sceneTag;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TO2JAM_Scene_Insert",paramCode);
				if(GMUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(GMUserID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 更新游戏里面场景列表
		/// <summary>
		/// 更新游戏里面场景
		/// </summary>
		/// <returns>道具数据集</returns>
		public static int TO2JAM_ChallengeScene_Update(int GMUserID,int sceneID,string sceneTag)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_SceneID",SqlDbType.Int),
												   new SqlParameter("@SDO_SceneTag",SqlDbType.VarChar,400),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=sceneID;
				paramCode[2].Value=sceneTag;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TO2JAM_Scene_Update",paramCode);
				if(GMUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(GMUserID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 删除游戏里面场景列表
		/// <summary>
		/// 删除游戏里面场景
		/// </summary>
		/// <returns>道具数据集</returns>
		public static int TO2JAM_ChallengeScene_Delete(int GMUserID,int sceneID)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_SceneID",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=sceneID;
				paramCode[2].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TO2JAM_Scene_Delete",paramCode);
				if(GMUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(GMUserID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看场景中获得道具概率列表
		/// <summary>
		/// 查看场景中获得道具概率列表
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static DataSet TO2JAM_Medalitem_Query(string serverIP)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30)};
				paramCode[0].Value=serverIP;
				result = SqlHelper.ExecSPDataSet("SDO_TO2JAM_Medalitem_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 根据道具名查看场景中获得道具概率列表
		/// <summary>
		/// 根据道具名查看场景中获得道具概率列表
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static DataSet TO2JAM_Medalitem_Owner_Query(string serverIP,string itemName)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_Name",SqlDbType.VarChar,50)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=itemName;
				result = SqlHelper.ExecSPDataSet("SDO_TO2JAM_Medalitem_Own_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 添加在场景获得概率
		/// <summary>
		/// 添加在场景获得概率
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static int TO2JAM_Medalitem_Insert(int GMUserID,string serverIP,int itemcode,int percent,int timeslimit,int dayslimit)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[7]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_ItemCode",SqlDbType.Int),
												   new SqlParameter("@SDO_Percent",SqlDbType.Int),
												   new SqlParameter("@SDO_timeslimit",SqlDbType.Int),
												   new SqlParameter("@SDO_DaysLimit",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=itemcode;
				paramCode[3].Value=percent;
				paramCode[4].Value=timeslimit;
				paramCode[5].Value=dayslimit;
				paramCode[6].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TO2JAM_Medalitem_Insert",paramCode);
				if(GMUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(GMUserID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 更新在场景获得概率
		/// <summary>
		/// 更新在场景获得概率
		/// </summary>
		/// <returns>道具数据集</returns>
		public static int TO2JAM_Medalitem_Update(int GMUserID,string serverIP,int itemcode,int percent,int timeslimit,int dayslimit)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[7]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_ItemCode",SqlDbType.Int),
												   new SqlParameter("@SDO_Percent",SqlDbType.Int),
												   new SqlParameter("@SDO_timeslimit",SqlDbType.Int),
												   new SqlParameter("@SDO_DaysLimit",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=itemcode;
				paramCode[3].Value=percent;
				paramCode[4].Value=timeslimit;
				paramCode[5].Value=dayslimit;
				paramCode[6].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TO2JAM_Medalitem_Update",paramCode);
				if(GMUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(GMUserID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 删除在场景获得概率
		/// <summary>
		/// 删除在场景获得概率
		/// </summary>
		/// <returns>道具数据集</returns>
		public static int TO2JAM_Medalitem_Delete(int GMUserID,string serverIP,int itemcode)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_ItemCode",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=itemcode;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TO2JAM_Medalitem_Delete",paramCode);
				if(GMUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(GMUserID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 添加游戏里面场景
		/// <summary>
		/// 添加游戏里面场景
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static int TO2JAM_Challenge_Insert(int GMUserID,string serverIP,int WeekDay,int MatPt_min,int StPt_min,int GCash,int Scene,int musicID1,int lv1,int musicID2,int lv2,int musicID3,int lv3,int musicID4,int lv4,int musicID5,int lv5,int isBattle)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[19]{
													new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
													new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
													new SqlParameter("@SDO_WeekDay",SqlDbType.Int),
													new SqlParameter("@SDO_MatPtMin",SqlDbType.Int),
													new SqlParameter("@SDO_StPtMin",SqlDbType.Int),
													new SqlParameter("@SDO_GCash",SqlDbType.Int),
													new SqlParameter("@SDO_Sence",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID1",SqlDbType.Int),
													new SqlParameter("@SDO_LV1",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID2",SqlDbType.Int),
													new SqlParameter("@SDO_LV2",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID3",SqlDbType.Int),
													new SqlParameter("@SDO_LV3",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID4",SqlDbType.Int),
													new SqlParameter("@SDO_LV4",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID5",SqlDbType.Int),
													new SqlParameter("@SDO_LV5",SqlDbType.Int),
													new SqlParameter("@SDO_IsBattle",SqlDbType.Int),
													new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=WeekDay;
				paramCode[3].Value=MatPt_min;
				paramCode[4].Value=StPt_min;
				paramCode[5].Value=GCash;
				paramCode[6].Value=Scene;
				paramCode[7].Value=musicID1;
				paramCode[8].Value=lv1;
				paramCode[9].Value=musicID2;
				paramCode[10].Value=lv2;
				paramCode[11].Value=musicID3;
				paramCode[12].Value=lv3;
				paramCode[13].Value=musicID4;
				paramCode[14].Value=lv4;
				paramCode[15].Value=musicID5;
				paramCode[16].Value=lv5;
				paramCode[17].Value=isBattle;
				paramCode[18].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TO2JAM_Challenge_Insert",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine("IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 更新游戏里面场景
		/// <summary>
		/// 更新游戏里面场景
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static int TO2JAM_Challenge_Update(int GMUserID,string serverIP,int sceneID,int WeekDay,int MatPt_min,int StPt_min,int GCash,int Scene,int musicID1,int lv1,int musicID2,int lv2,int musicID3,int lv3,int musicID4,int lv4,int musicID5,int lv5,int isBattle)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[20]{
													new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
													new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
													new SqlParameter("@SDO_SceneID",SqlDbType.Int),
													new SqlParameter("@SDO_WeekDay",SqlDbType.Int),
													new SqlParameter("@SDO_MatPtMin",SqlDbType.Int),
													new SqlParameter("@SDO_StPtMin",SqlDbType.Int),
													new SqlParameter("@SDO_GCash",SqlDbType.Int),
													new SqlParameter("@SDO_Sence",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID1",SqlDbType.Int),
													new SqlParameter("@SDO_LV1",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID2",SqlDbType.Int),
													new SqlParameter("@SDO_LV2",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID3",SqlDbType.Int),
													new SqlParameter("@SDO_LV3",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID4",SqlDbType.Int),
													new SqlParameter("@SDO_LV4",SqlDbType.Int),
													new SqlParameter("@SDO_MusicID5",SqlDbType.Int),
													new SqlParameter("@SDO_LV5",SqlDbType.Int),
													new SqlParameter("@SDO_isBattle",SqlDbType.Int),
													new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=sceneID;
				paramCode[3].Value=WeekDay;
				paramCode[4].Value=MatPt_min;
				paramCode[5].Value=StPt_min;
				paramCode[6].Value=GCash;
				paramCode[7].Value=Scene;
				paramCode[8].Value=musicID1;
				paramCode[9].Value=lv1;
				paramCode[10].Value=musicID2;
				paramCode[11].Value=lv2;
				paramCode[12].Value=musicID3;
				paramCode[13].Value=lv3;
				paramCode[14].Value=musicID4;
				paramCode[15].Value=lv4;
				paramCode[16].Value=musicID5;
				paramCode[17].Value=lv5;
				paramCode[18].Value=isBattle;
				paramCode[19].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TO2JAM_Challenge_Update",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine("IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 删除游戏里面场景列表
		/// <summary>
		/// 删除游戏里面场景列表
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static int TO2JAM_Challenge_Del(string serverIP,int gmUserID,int sceneID)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_SceneID",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=gmUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=sceneID;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_TO2JAM_Challenge_Del",paramCode);
				if(gmUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(gmUserID);
				}
			} 
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看场景中获得道具概率列表
		/// <summary>
		/// 查看在游戏里摇摇乐获得概率
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static DataSet SDO_YYHappy_Query(string serverIP,string itemName)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@SDO_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_Name",SqlDbType.VarChar,50)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=itemName;
				result = SqlHelper.ExecSPDataSet("SDO_YYHappyItem_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 添加摇摇乐获得概率
		/// <summary>
		/// 添加摇摇乐获得概率
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <returns>道具数据集</returns>
		public static int SDO_YYHappyitem_Insert(int GMUserID,string serverIP,int itemcode1,int itemcode2,string itemName1,string itemName2,int level,int levPercent,int percent,int timeslimit,int dayslimit)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[12]{
													new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
													new SqlParameter("@SDO_ServerIP",SqlDbType.VarChar,30),
													new SqlParameter("@SDO_ItemCode1",SqlDbType.Int),
													new SqlParameter("@SDO_ItemName1",SqlDbType.VarChar,50),
													new SqlParameter("@SDO_ItemCode2",SqlDbType.Int),
													new SqlParameter("@SDO_ItemName2",SqlDbType.VarChar,50),
													new SqlParameter("@SDO_Level",SqlDbType.TinyInt),
													new SqlParameter("@SDO_LevPerc",SqlDbType.Int),
													new SqlParameter("@SDO_Percent",SqlDbType.Int),
													new SqlParameter("@SDO_timeslimit",SqlDbType.Int),
													new SqlParameter("@SDO_DaysLimit",SqlDbType.Int),
													new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=itemcode1;
				paramCode[3].Value=itemName1;
				paramCode[4].Value=itemcode2;
				paramCode[5].Value=itemName2;
				paramCode[6].Value=level;
				paramCode[7].Value=levPercent;
				paramCode[8].Value=percent;
				paramCode[9].Value=timeslimit;
				paramCode[10].Value=dayslimit;
				paramCode[11].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_YYHappyItem_Insert",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine("IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 更新摇摇乐获得概率
		/// <summary>
		/// 更新摇摇乐获得概率
		/// </summary>
		/// <returns>道具数据集</returns>
		public static int SDO_YYHappyitem_Update(int GMUserID,string serverIP,int itemcode1,string itemName1,int itemcode2,string itemName2,int level,int levPercent,int percent,int timeslimit,int dayslimit,int itemCodeBy)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[13]{
													new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
													new SqlParameter("@SDO_ItemCodeBy",SqlDbType.Int),
													new SqlParameter("@SDO_ServerIP",SqlDbType.VarChar,30),
													new SqlParameter("@SDO_ItemCode1",SqlDbType.Int),
													new SqlParameter("@SDO_ItemName1",SqlDbType.VarChar,50),
													new SqlParameter("@SDO_ItemCode2",SqlDbType.Int),
													new SqlParameter("@SDO_ItemName2",SqlDbType.VarChar,50),
													new SqlParameter("@SDO_Level",SqlDbType.TinyInt),
													new SqlParameter("@SDO_LevPerc",SqlDbType.Int),
													new SqlParameter("@SDO_Percent",SqlDbType.Int),
													new SqlParameter("@SDO_timeslimit",SqlDbType.Int),
													new SqlParameter("@SDO_DaysLimit",SqlDbType.Int),
													new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=itemCodeBy;
				paramCode[2].Value=serverIP;
				paramCode[3].Value=itemcode1;
				paramCode[4].Value=itemName1;
				paramCode[5].Value=itemcode2;
				paramCode[6].Value=itemName2;
				paramCode[7].Value=level;
				paramCode[8].Value=levPercent;
				paramCode[9].Value=percent;
				paramCode[10].Value=timeslimit;
				paramCode[11].Value=dayslimit;
				paramCode[12].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_YYHappyItem_Update",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine("IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 删除摇摇乐获得概率
		/// <summary>
		/// 删除摇摇乐获得概率
		/// </summary>
		/// <returns>道具数据集</returns>
		public static int SDO_YYHappyitem_Delete(int GMUserID,string serverIP,int itemcode)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@SDO_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@SDO_ItemCode",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=GMUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=itemcode;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SDO_YYHapppyItem_Delete",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 查询道具名称
		public static DataSet SDO_ItemName_Query(string itemcode)
		{
			//string itemName = "";
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@itemcode",SqlDbType.VarChar,100)};
				paramCode[0].Value = itemcode;
				DataSet ds = SqlHelper.ExecSPDataSet("sdo_itemName_query", paramCode);
				return ds;
			}
			catch (SqlException ex)
			{
				Console.WriteLine("函数名SDO_ItemName_Query道具ID"+itemcode+ex.Message);
				return null;
			}
		}
		#endregion
	}
}
