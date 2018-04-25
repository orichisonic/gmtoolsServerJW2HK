using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
using Common.DataInfo;

namespace Common.DataInfo
{
	/// <summary>
	/// GMDepartment 的摘要说明。
	/// </summary>
	public class GMDepartmentInfo
	{
		public GMDepartmentInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 查询所有部门信息
		/// <summary>
		/// 查询所有部门信息
		/// </summary>
		/// <returns></returns>
		public static DataSet getDepartInfo()
		{
			string strSQL="select DepartID,DepartName,remark from Department";
			DataSet ds =SqlHelper.ExecuteDataset(strSQL);
			return ds;
		}
		#endregion
		#region 查询关联部门信息
		/// <summary>
		/// 查询关联部门信息
		/// </summary>
		/// <returns></returns>
		public static DataSet getDepartRelateInfo(int deptID)
		{
			DataSet ds  = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@deptID",SqlDbType.Int)};
				paramCode[0].Value = deptID;
				ds = SqlHelper.ExecSPDataSet("Gmtool_DepartAdmin_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return ds;
		}
		#endregion
		#region 查询关联部门信息
		/// <summary>
		/// 查询关联部门信息
		/// </summary>
		/// <returns></returns>
		public static DataSet getDepartRelateGameInfo(int userID)
		{
			DataSet ds  = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@UserID",SqlDbType.Int)};
				paramCode[0].Value = userID;
				ds = SqlHelper.ExecSPDataSet("Gmtool_DepartRelateGame_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return ds;
		}
		#endregion
		#region 添加一个部门信息
		/// <summary>
		/// 添加一个部门信息
		/// </summary>
		/// <param name="operateUserID"></param>
		/// <param name="departName"></param>
		/// <param name="departRemark"></param>
		/// <returns></returns>
		public static int insertRow(int operateUserID,string departName,string departRemark,string gameList)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@Gm_DeprtName",SqlDbType.VarChar,50),
												   new SqlParameter("@Gm_Remark",SqlDbType.VarChar,500),
												   new SqlParameter("@Gm_GameID",SqlDbType.VarChar,500),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = operateUserID;
				paramCode[1].Value=departName;
				paramCode[2].Value=departRemark;
				paramCode[3].Value=gameList;
				paramCode[4].Direction=ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_Department_Insert",paramCode);
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
		#region 更新一个部门记录
		public static int updateRow(int operateUserID,int departID,string departName,string departRemark,string gameContent)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@GM_DepartID",SqlDbType.Int),
												   new SqlParameter("@Gm_DepartName",SqlDbType.VarChar,50),
												   new SqlParameter("@Gm_Remark",SqlDbType.VarChar,500),
												   new SqlParameter("@Gm_DepartRoles",SqlDbType.VarChar,400),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = operateUserID;
				paramCode[1].Value=departID;
				paramCode[2].Value=departName;
				paramCode[3].Value=departRemark;
				paramCode[4].Value=gameContent;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result  = SqlHelper.ExecSPCommand("Gmtool_Department_Update",paramCode);
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
		#region 删除一个部门记录
		/// <summary>
		/// 删除一个部门记录
		/// </summary>
		/// <param name="userByID"></param>
		/// <param name="deptID"></param>
		/// <returns></returns>
		public static int deleteRow(int userByID,int deptID)
		{
			SqlParameter[] paramCode;
			int result= -1;
			//string deleteSql = "delete from GMTOOLS_Modules where ID="+moduleID;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@GM_DepartID",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = deptID;
				paramCode[2].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_Department_Delete",paramCode);
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
