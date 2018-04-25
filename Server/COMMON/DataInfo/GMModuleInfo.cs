using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
namespace Common.DataInfo
{
	/// <summary>
	/// GMModuleInfo 的摘要说明。
	/// </summary>
	public class GMModuleInfo
	{
		/// <summary>
		/// 游戏ID
		/// </summary>
		int gameID = 0;
		/// <summary>
		/// 模块ID
		/// </summary>
		private int moduleID;
		/// <summary>
		/// 模块名称
		/// </summary>
		private string moduleName;
		/// <summary>
		/// 模块类
		/// </summary>
		private string moduleClass;
		/// <summary>
		/// 模块描述
		/// </summary>
		private string moduleContent;
		public GMModuleInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public GMModuleInfo(int layer,int moduleID,string moduleName,string moduleClass,string moduleContent)
		{
			this.GameID = layer;
			this.ModuleID =moduleID;
			this.ModuleName=moduleName;
			this.ModuleClass=moduleClass;
			this.ModuleContent=moduleContent;
		}
		#region 得到所有模块的信息
		/// <summary>
		/// 得到模块的信息
		/// </summary>
		/// <returns></returns>
		public static DataSet SelectAll()
		{
			string strSQL="select a.game_ID,b.module_ID,a.game_Name,b.module_name,b.module_class,b.module_content from GAMELIST a ,GMTOOLS_Modules b where a.game_ID=b.game_id order by a.game_ID";
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
		#region 添加一个模块信息
		/// <summary>
		/// 插入一个模块信息
		/// </summary>
		/// <param name="operateUserID">操作员ID</param>
		/// <param name="gameID">游戏ID</param>
		/// <param name="Name">模块名称</param>
		/// <param name="Class">模块类名</param>
		/// <param name="content">模块描述</param>
		public static int insertRow(int operateUserID,int gameID,string Name,string Class,string content)
		{
			int result = -1;
			//string insertSql = "Insert into GMTOOLS_Modules (layer,Name,class,content) values (@layer,@name,@class,@content) ";
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@GM_GameID",SqlDbType.Int),
												   new SqlParameter("@GM_Name",SqlDbType.VarChar,50),
												   new SqlParameter("@GM_Class",SqlDbType.VarChar,50),
												   new SqlParameter("@GM_Content",SqlDbType.VarChar,400),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = operateUserID;
				paramCode[1].Value=gameID;
				paramCode[2].Value=Name;
				paramCode[3].Value=Class;
				paramCode[4].Value=content;
				paramCode[5].Direction=ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_GmModule_Insert",paramCode);
				if(operateUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(operateUserID);
				}
                
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);

			}
			return result;
		}
		#endregion
		#region 得到一个模块信息
		public static DataSet QueryModuleInfo(int moduleID)
		{
			string strSQL;
			strSQL = "select * from GMTOOLS_Modules where ID="+moduleID;

			System.Data.DataSet ds = SqlHelper.ExecuteDataset(strSQL);

			return ds;
		}
		#endregion
		#region 得到用户所对应的模块信息
		public static DataSet getModuleInfo(int userID)
		{
			System.Data.DataSet ds;
			string strSQL;
			try
			{
				strSQL = "select a.game_ID,b.module_ID,a.game_Name,b.module_name,b.module_class,b.module_content from GAMELIST a ,GMTOOLS_Modules b,GMTOOLS_Users c,GMTOOLS_Roles d where a.game_ID=d.game_id and b.module_ID=d.module_ID and c.userID=d.userID and c.userID="+userID;
				ds = SqlHelper.ExecuteDataset(strSQL);
				return ds;
                
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}

		}
		#endregion
		#region 更新一个模块记录
		public int updateRow(int operateUserID,int moduleID)
		{
			int result = -1;
			//string updateSql ="update GMTOOLS_Modules set moduleName=@moduleName,moduleClass=@moduleClass,moduleContent=@moduleContent where ID="+moduleID;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[7]{
												   new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@GM_ModuleID",SqlDbType.Int),
												   new SqlParameter("@GM_GameID",SqlDbType.Int),
												   new SqlParameter("@GM_Name",SqlDbType.VarChar,50),
												   new SqlParameter("@GM_Class",SqlDbType.VarChar,50),
												   new SqlParameter("@GM_Content",SqlDbType.VarChar,400),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = operateUserID;
				paramCode[1].Value=ModuleID;
				paramCode[2].Value=GameID;
				paramCode[3].Value=ModuleName;
				paramCode[4].Value=ModuleClass;
				paramCode[5].Value=ModuleContent;
				paramCode[6].Direction = ParameterDirection.ReturnValue;
				result  = SqlHelper.ExecSPCommand("Gmtool_GmModule_Update",paramCode);
				if(operateUserID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(operateUserID);
				}

			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);

			}
			return result;
		}
        #endregion
		#region 删除一个模块记录
        /// <summary>
        /// 删除模块信息
        /// </summary>
        /// <param name="userByID">操作员ID</param>
        /// <param name="moduleID">操作员ID</param>
		public static int deleteRow(int userByID,int moduleID)
		{
			SqlParameter[] paramCode;
			int result= -1;
			//string deleteSql = "delete from GMTOOLS_Modules where ID="+moduleID;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@GM_ModuleID",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = moduleID;
				paramCode[2].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_GmModule_Delete",paramCode);
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
		#region 游戏ID
		public int GameID
		{
			get
			{
				return this.gameID;
			}
			set
			{
				this.gameID=value;
			}
		}
		#endregion
		#region 模块ID
		public int ModuleID
		{
			get
			{
				return this.moduleID;
			}
			set
			{
				this.moduleID = value;
			}
		}
		#endregion
		#region 模块名称
		public string ModuleName
		{
			get
			{
				return this.moduleName;
			}
			set
			{
				this.moduleName=value;
			}
		}
		#endregion
		#region 模块类型
		public string ModuleClass
		{

			get
			{
				return this.moduleClass;
			}
			set
			{
				this.moduleClass=value;
			}
		}
		#endregion
		#region 模块描述
		public string ModuleContent
		{
			get
			{
				return this.moduleContent;
			}
			set
			{
				this.moduleContent=value;
			}
		}
        #endregion
	}
}
