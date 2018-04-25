using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
namespace Common.NotesDataInfo
{
	public class NotesData
	{
		public NotesData()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 查看NOTES邮件
		/// <summary>
		/// 查看NOTES邮件
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet NoteContent_Query(int status)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[]{
												   new SqlParameter("@iStatus",SqlDbType.Int,4)											   
											   };

				paramCode[0].Value = status;
				result = SqlHelper.ExecSPDataSet("SP_ReadNotesContent",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog(ex.Message);
			}
			return result;
		}
		#endregion
		#region 更新NOTES邮件状态
		/// <summary>
		/// 更新NOTES邮件状态
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static int NoteContent_Update(int userByID,int notesID,int status)
		{
			int result = -1;
			SqlParameter[] paramCache;
			try
			{
				paramCache = new SqlParameter[]{
												  new SqlParameter("@Gm_UserID",SqlDbType.Int,4),
												  new SqlParameter("@iNotesID",SqlDbType.Int,4),	
												  new SqlParameter("@iStatus",SqlDbType.Int,4),
											      new SqlParameter("@result",SqlDbType.Int)
											  };

				paramCache[0].Value = userByID;
				paramCache[1].Value = notesID;
				paramCache[2].Value = status;
				paramCache[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SP_UpdateNotesStatus",paramCache);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog(ex.Message);
			}
			return result;
		}
		#endregion
		#region 删除NOTES邮件
		/// <summary>
		/// 删除NOTES邮件
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static int NoteContent_Delete(int userByID,int notesID)
		{
			int result = -1;
			SqlParameter[] paramCache;
			try
			{
				paramCache = new SqlParameter[]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int,4),
												   new SqlParameter("@iNotesID",SqlDbType.Int,4),
												   new SqlParameter("@result",SqlDbType.Int)
											   };

				paramCache[0].Value = userByID;
				paramCache[1].Value = notesID;
				paramCache[2].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SP_DeleteNotes",paramCache);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog(ex.Message);
			}
			return result;
		}
		#endregion
		#region 接收NOTES里新邮件
		/// <summary>
		/// 接收NOTES里新邮件
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <param name="level">等级</param>
		/// <param name="experience">经验值</param>
		/// <param name="battle">总局数</param>
		/// <param name="win">胜局</param>
		/// <param name="draw">平局</param>
		/// <param name="lose">负局</param>
		/// <param name="MCash">M币</param>
		/// <param name="GCash">G币</param>
		/// <returns></returns>
		public static int PutNotesContent(object[] pMailStruct)
		{
			int result = -1;
			SqlParameter[] paramCache;
			try
			{
				for (int i = 0; i < pMailStruct.GetLength(0); i++)
				{

					GlobalStruct[] pMailInfo = (GlobalStruct[])pMailStruct[i];


					paramCache = new SqlParameter[]{
													   new SqlParameter("@iCategory",SqlDbType.Int, 4),
													   new SqlParameter("@strUID",SqlDbType.VarChar,100),
													   new SqlParameter("@strPUID",SqlDbType.VarChar,100),
													   new SqlParameter("@strSubject",SqlDbType.VarChar,100),
													   new SqlParameter("@dtPost",SqlDbType.DateTime),
													   new SqlParameter("@strSender",SqlDbType.VarChar,50),
													   new SqlParameter("@strRecive",SqlDbType.Text),
													   new SqlParameter("@strContent",SqlDbType.Text),
													   new SqlParameter("@strCount",SqlDbType.VarChar, 100),
													   new SqlParameter("@iView",SqlDbType.Int, 4),
													   new SqlParameter("@iStatus",SqlDbType.Int, 4),
													   new SqlParameter("@result",SqlDbType.Int)
												   };

					paramCache[0].Value = pMailInfo[8].oFieldValues;
					paramCache[1].Value = pMailInfo[0].oFieldValues;
					paramCache[2].Value = pMailInfo[1].oFieldValues;
					paramCache[3].Value = pMailInfo[2].oFieldValues;
					paramCache[4].Value = pMailInfo[4].oFieldValues;
					paramCache[5].Value = pMailInfo[3].oFieldValues;
					paramCache[6].Value = pMailInfo[5].oFieldValues.ToString().Trim();
					paramCache[7].Value = pMailInfo[6].oFieldValues;
					paramCache[8].Value = pMailInfo[7].oFieldValues;
					paramCache[9].Value = 0;
					paramCache[10].Value = pMailInfo[9].oFieldValues;
					paramCache[11].Direction = ParameterDirection.ReturnValue;
					result = SqlHelper.ExecSPCommand("SP_PUT_NOTESCONTENT",paramCache);
				}
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog(ex.Message);
			}
			return result;

		}
		#endregion
	}

}
