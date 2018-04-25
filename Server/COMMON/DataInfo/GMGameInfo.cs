using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
namespace Common.DataInfo
{
	/// <summary>
	/// GMGameInfo 的摘要说明。
	/// </summary>
	public class GMGameInfo
	{
		/// <summary>
		/// 游戏ID
		/// </summary>
		private int gameID = 0;
		/// <summary>
		/// 游戏名称
		/// </summary>
		private string gameName = null;
		/// <summary>
		/// 游戏描述
		/// </summary>
		private string gameContent = null;
		public GMGameInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public GMGameInfo(int gameID_,string gameName_,string gameContent_)
		{
			gameID = gameID_;
			gameName = gameName_;
			gameContent = gameContent_;
			
		}
		#region 得到所有游戏信息
		/// <summary>
		/// 得到模块的信息
		/// </summary>
		/// <returns></returns>
		public static DataSet SelectAll()
		{
			string strSQL="select * from GAMELIST";
			try
			{
				return SqlHelper.ExecuteDataset(strSQL);

			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}
		#endregion
		#region 得到一个游戏信息集
		/// <summary>
		/// 得到一个游戏信息集
		/// </summary>
		/// <param name="gameID">游戏ID</param>
		/// <returns></returns>
		public static DataSet QueryModuleInfo(int gameID)
		{
			string strSQL;
			strSQL = "select * from GameList where ID="+gameID;

			System.Data.DataSet ds = SqlHelper.ExecuteDataset(strSQL);

			return ds;
		}
		#endregion
		#region 插入一个游戏信息
        /// <summary>
        /// 插入一个游戏信息
        /// </summary>
        /// <param name="Name">游戏名称</param>
        /// <param name="content">游戏描述</param>
        /// <returns>操作结果</returns>
		public static int insertRow(int userByID,string Name,string content)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@Gm_GameName",SqlDbType.VarChar,50),
												   new SqlParameter("@Gm_GameContext",SqlDbType.VarChar,400),
												   new SqlParameter("@result",SqlDbType.Int)
											   };
				paramCode[0].Value=userByID;
				paramCode[1].Value=Name.Trim();
				paramCode[2].Value=content.Trim();
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_GAME_Add",paramCode);
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
		#region 更新某个游戏信息
		/// <summary>
		/// 更新某个游戏信息
		/// </summary>
		/// <param name="gameID">游戏ID</param>
		/// <param name="gameName">游戏名称</param>
		/// <param name="gameContent">游戏描述</param>
		public int updateRow(int userByID,int gameID,string gameName,string gameContent)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@Gm_GameID",SqlDbType.Int),
												   new SqlParameter("@Gm_GameName",SqlDbType.VarChar,50),
												   new SqlParameter("@Gm_GameContext",SqlDbType.VarChar,400),
												   new SqlParameter("@result",SqlDbType.Int)
											   };
				paramCode[0].Value=userByID;
				paramCode[1].Value=gameID;
				paramCode[2].Value=gameName.Trim();
				paramCode[3].Value=gameContent.Trim();
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_GAME_Edit",paramCode);
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
		#region 删除游戏信息
		/// <summary>
		/// 删除游戏信息
		/// </summary>
		/// <param name="gameID">游戏ID</param>
		public static int  deleteRow(int userByID,int gameID)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{ new SqlParameter("@Gm_GAMEID",SqlDbType.Int),
												   new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=gameID;
				paramCode[1].Value=userByID;
				paramCode[2].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_GAME_Del",paramCode);
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
		public int GameID
		{
			get
			{
				return this.gameID;
			}
			set
			{
				this.gameID = value;
			}
		}
		public string GameName
		{
			get
			{
				return this.gameName;
			}
			set
			{
				this.gameName =value;
			}
		}
		public string GameContent
		{
			get
			{
				return this.gameContent;
			}
			set
			{
				this.gameContent =value;
			}
		}
	}
}
