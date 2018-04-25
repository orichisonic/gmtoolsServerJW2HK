using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
namespace Common.DataInfo
{
	/// <summary>
	/// GMNotesInfo 的摘要说明。
	/// </summary>
	public class GMNotesInfo
	{
		/// <summary>
		/// 邮件ID
		/// </summary>
		int letterID = 0;
		/// <summary>
		/// 发件人
		/// </summary>
		string letterSender;
		/// <summary>
		/// 收件人
		/// </summary>
		string letterReceiver; 
		/// <summary>
		/// 邮件主题
		/// </summary>
		string letterSubject;
		/// <summary>
		/// 邮件内容
		/// </summary>
		string letterText;
		/// <summary>
		/// 发送日期
		/// </summary>
		DateTime sendDate;
		/// <summary>
		/// 处理人
		/// </summary>
		string processMan; 
		/// <summary>
		/// 处理日期
		/// </summary>
		DateTime processDate;
		/// <summary>
		/// 转发收件人
		/// </summary>
		string transmitMan; 
		/// <summary>
		/// 是否处理
		/// </summary>
		int isProcess =0;
		public GMNotesInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 得到当前用户处理过的邮件
		/// <summary>
		/// 取得NOTES里面信件列表
		/// </summary>
		/// <returns>NOTES里面信件列表</returns>
		public static DataSet SelectTransmitLetter(int userByID)
		{
			string strSQL="select b.*,a.realName from GMTOOLS_users a,letter_Info b where a.userID=b.processMan and isProcess=1 and transmitMan ="+userByID+" order by sendDate desc";
			DataSet ds =SqlHelper.ExecuteDataset(strSQL);
			return ds;
		}
		#endregion
		#region 取得NOTES里面信件列表
		/// <summary>
		/// 取得NOTES里面信件列表
		/// </summary>
		/// <returns>NOTES里面信件列表</returns>
		public static DataSet SelectAll()
		{
			string strSQL="select * from letter_Info where isProcess=0";
			DataSet ds =SqlHelper.ExecuteDataset(strSQL);
			return ds;
		}
		#endregion
		#region 邮件处理
		public static int updateRow(int userByID,int letterID,int processMan,DateTime processDate,int transmitMan,int isProcess,string reason)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[8]{   new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@Gm_LetterID",SqlDbType.Int),
												   new SqlParameter("@Gm_ProcessMan",SqlDbType.Int),
												   new SqlParameter("@GM_ProcessDate",SqlDbType.DateTime),
												   new SqlParameter("@GM_TransmitMan",SqlDbType.Int),
												   new SqlParameter("@GM_IsProcess",SqlDbType.Int),
												   new SqlParameter("@Gm_NotesReason",SqlDbType.VarChar,2000),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=letterID;
				paramCode[2].Value = processMan;
				paramCode[3].Value = processDate;
				paramCode[4].Value = transmitMan;
				paramCode[5].Value = isProcess;
				paramCode[6].Value = reason;
				paramCode[7].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_GmNotes_Update",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		public int LetterID
		{
			get
			{
				return letterID;
			}
			set
			{
				letterID = value;
			}
		}
		public string LetterSender
		{
			get
			{
				return this.letterSender;
			}
			set
			{
				letterSender = value;
			}
		}
		public string LetterReceiver
		{
			get
			{
				return this.letterReceiver;
			}
			set
			{
				letterReceiver = value;
			}
		}
		public string LetterSubject
		{
			get
			{
				return this.letterSubject;
			}
			set
			{
				letterSubject = value;
			}
		}
		public string LetterText
		{
			get
			{
				return this.letterText;
			}
			set
			{
				letterText = value;
			}
		}
		public DateTime SendDate
		{
			get
			{
				return this.sendDate;
			}
			set
			{
				sendDate = value;
			}

		}
		public string ProcessMan
		{
			get
			{
				return this.processMan;
			}
			set
			{
				processMan = value;
			}
		}
		public DateTime ProcessDate
		{
			get
			{
				return this.processDate;
			}
			set
			{
				processDate = value;
			}

		}
		public string TransmitMan
		{
			get
			{
				return this.transmitMan;
			}
			set
			{
				transmitMan = value;
			}

		}
		public int IsProcess
		{
			get
			{
				return this.isProcess;
			}
			set
			{
				isProcess = value;
			}

		}
	}
}
