using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
namespace Common.DataInfo
{
	/// <summary>
	/// GMUserModule 的摘要说明。
	/// </summary>
	public class GMUserModule
	{
		/// <summary>
		/// 用户ID
		/// </summary>
		private int userID = 0;
		/// <summary>
		/// 模块列表
		/// </summary>
		private string moduleList;
		public GMUserModule()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
        }
        #region 查询所有用户GM帐号的信息
        /// <summary>
		/// 查询所有用户GM帐号的信息
		/// </summary>
		/// <returns></returns>
		public static DataSet SelectAll(int userID)
		{
			string strSQL="select * from GMTOOLS_Roles where [user]= "+userID;
			DataSet ds = new DataSet("GMTOOLSRoles");
			try
			{
				ds =SqlHelper.ExecuteDataset(strSQL);
			}
			catch(SqlException ex)
			{
				System.Console.WriteLine(ex.Message);
			}
			return ds;
		}
		#endregion
		#region 用户与模块的管理
		public static int UserModuleAdmin(int userID,string moduleList)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{ new SqlParameter("@GM_UserID",SqlDbType.Int),
												   new SqlParameter("@GM_ModuleList",SqlDbType.VarChar,1000),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userID;
				paramCode[1].Value = moduleList;
				paramCode[2].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("dbo.Gmtool_GmUserModule_Admin",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		public int UserID
		{
			get
			{
				return this.userID;
			}
			set
			{
				this.userID =value;
			}

		}
		public string ModuleList
		{
			get
			{
				return this.moduleList;
			}
			set
			{
				this.moduleList =value;
			}

		}
	}
}
