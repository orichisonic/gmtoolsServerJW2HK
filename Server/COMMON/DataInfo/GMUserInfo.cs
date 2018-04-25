using System;
using System.Data;
using System.Data.SqlClient;
using Common.Logic;
using System.Security.Cryptography;
namespace Common.DataInfo
{
	/// <summary>
	/// GMUserInfo 的摘要说明。
	/// </summary>
	public class GMUserInfo
	{
		/// <summary>
		/// 用户名
		/// </summary>
		private string userName;
		/// <summary>
		/// 密码
		/// </summary>
		private string passWord;
		/// <summary>
		/// MAC码
		/// </summary>
		private string mac;
		/// <summary>
		/// 姓名
		/// </summary>
		private string realName;
		/// <summary>
		/// 系统管理员
		/// </summary>
		private int sysAdmin;

		public GMUserInfo(byte[] packet)
		{
			Message msg = new Message(packet,(uint)packet.Length);
			msg.GetMessageID();
		}
		public GMUserInfo(string userName,string password)
		{
			this.UserName=userName;
			this.PassWord=password;
		}
		#region 查询所有用户GM帐号的信息
		/// <summary>
		/// 查询所有用户GM帐号的信息
		/// </summary>
		/// <returns></returns>
		public static DataSet SelectAll()
		{
			string strSQL = "";
			strSQL = "select a.userID,a.userName,a.user_Pwd,a.user_mac,a.realName,a.DepartID,b.departName,a.user_Status,a.limit,a.Onlineactive,a.SysAdmin from GMTOOLS_Users a,Department b where a.departID=b.departID";
			DataSet ds  = null;
			try
			{
				ds = SqlHelper.ExecuteDataset(strSQL);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return ds;
		}
		#endregion
		#region 查询所有用户GM帐号的信息
		/// <summary>
		/// 查询所有用户GM帐号的信息
		/// </summary>
		/// <returns></returns>
		public static DataSet SelectAll(int userID)
		{
			DataSet ds  = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{ 
												   new SqlParameter("@Gm_UserID",SqlDbType.Int)
											   };
				paramCode[0].Value =  userID;
				ds = SqlHelper.ExecSPDataSet("Gmtool_UserInfo_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
            return ds;
		}
		#endregion
		#region 服务器断开以后，更新所有用户为断开状态
		/// <summary>
		/// 服务器断开以后，更新所有用户为断开状态
		/// </summary>
		/// <param name="status"></param>
		/// <returns></returns>
		public static int updateActiveUser(int status)
		{
			int result  =-1;
			SqlParameter[] paramCode;
			string updateSql = "Update GMTOOLS_Users set onlineActive="+@status;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@status",SqlDbType.Int)
											   };
				paramCode[0].Value = status;
				result = SqlHelper.commitTrans(updateSql,paramCode);

			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;

		}
		#endregion
		#region 将上线或下线用户,设定1或0
		/// <summary>
		/// 将上线或下线用户,设定1或0
		/// </summary>
		/// <param name="userByID">上线或下线用户</param>
		/// <param name="status">状态</param>
		/// <returns></returns>
		public static int updateActiveUser(int userByID,int status)
		{
			status = 0;
			int result  =-1;
				SqlParameter[] paramCode;
				string updateSql = "Update GMTOOLS_Users set onlineActive="+@status+" where userID='"+@userByID+"'";
				try
				{
					paramCode = new SqlParameter[2]{
													   new SqlParameter("@userByID",SqlDbType.Int),
													   new SqlParameter("@status",SqlDbType.Int)
												   };
					paramCode[0].Value = userByID;
					paramCode[1].Value = status;
					result = SqlHelper.commitTrans(updateSql,paramCode);

				}
				catch(SqlException ex)
				{
					Console.WriteLine(ex.Message);
				}
			return result;

		}
		#endregion
		#region 将用户的MAC存到数据库里面
		public static int insertMac(string userName,string userpwd,string mac)
		{
			int result  =-1;
			//判断该用户MAC是否为空，空得话就写进去
			if (getUserInfo(userName,userpwd)==0)
			{

				SqlParameter[] paramCode;
				string updateSql = "Update GMTOOLS_Users set User_Mac='"+@mac+"' where username='"+@userName+"'and user_pwd='"+convertToMD5(@userpwd)+"'";
				try
				{
					paramCode = new SqlParameter[3]{
													   new SqlParameter("@userName",SqlDbType.VarChar,20),
													   new SqlParameter("@userpwd",SqlDbType.VarChar,100,ParameterDirection.Input.ToString()),
													   new SqlParameter("@mac",SqlDbType.VarChar,20,ParameterDirection.Input.ToString())
												   };
					paramCode[0].Value = userName;
                    paramCode[1].Value = convertToMD5(userpwd);
					paramCode[2].Value = mac;
					result = SqlHelper.commitTrans(updateSql,paramCode);

				}
				catch(SqlException ex)
				{
					Console.WriteLine(ex.Message);
					result = 0;
				}
			}
            return result;

		}
		#endregion
		#region 添加一个GM帐号
		/// <summary>
		/// 插入一个新的GM帐号
		/// </summary>
		/// <param name="operateUserID">操作员ID</param>
		/// <param name="userName">用户名</param>
		/// <param name="pwd">密码</param>
		/// <param name="limit">使用时效</param>
		/// <param name="status">状态</param>
		/// <returns>操作结果</returns>
		public static int insertRow(int operateUserID,int departID,string userName,string pwd,string realName,DateTime limit,int status,int SysAdmin)
		{
			int result = -1;
			//string insertSql = "Insert into GMTOOLS_Users (name,pwd,MAC) values (@name,@pwd,@mac) ";
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[9]{
												new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												new SqlParameter("@Gm_UserName",SqlDbType.VarChar,50),
												new SqlParameter("@Gm_Password",SqlDbType.VarChar,50),
												new SqlParameter("@Gm_DepartID",SqlDbType.Int),
												new SqlParameter("@Gm_RealName",SqlDbType.VarChar,50),
												new SqlParameter("@Gm_LimitTime",SqlDbType.DateTime),
												new SqlParameter("@Gm_Status",SqlDbType.Int),
												new SqlParameter("@Gm_SysAdmin",SqlDbType.Int),
												new SqlParameter("@result",SqlDbType.Int)



											};
				paramCode[0].Value = operateUserID;
				paramCode[1].Value=userName.Trim();
                paramCode[2].Value = convertToMD5(pwd);
				paramCode[3].Value = departID;
				paramCode[4].Value = realName;
				paramCode[5].Value=limit;
				paramCode[6].Value =status;
				paramCode[7].Value = SysAdmin;
				paramCode[8].Direction=ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_Gminfo_Add",paramCode);
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
		#region 删除一个GM帐号
		/// <summary>
		/// 删除一个GM帐号
		/// </summary>
		/// <param name="userID">被删除的用户ID</param>
		/// <param name="userByID">删除该操作员ID</param>
		/// <returns></returns>
		public static int deleteRow(int userID,int operateUserID)
		{
			int result = -1;
			//string deleteSql = "delete from Tools_Users where ID="+@userID;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
											       new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userID;
				paramCode[1].Value=operateUserID;
				paramCode[2].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_Gminfo_Del",paramCode);
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
		#region 修改用户信息
		/// <summary>
		/// 更新GM帐号信息
		/// </summary>
		/// <param name="userbyID">操作员ID</param>
		/// <param name="userID">用户ID</param>
		/// <param name="limitTime">使用时效</param>
		/// <param name="status">状态</param>
		/// <returns>操作结果</returns>
		public static int UpdateRow(int userbyID,int deptID,int userID,string realName,DateTime limitTime,int status,int onlineActive,int SysAdmin)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[9]{ new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@Gm_RealName",SqlDbType.VarChar,100),
												   new SqlParameter("@Gm_DeptID",SqlDbType.Int),
												   new SqlParameter("@Gm_LimitTime",SqlDbType.DateTime),
												   new SqlParameter("@Gm_Status",SqlDbType.Int),
                                                   new SqlParameter("@Gm_OnlineActive",SqlDbType.Int),
												   new SqlParameter("@Gm_SysAdmin",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userbyID;
				paramCode[1].Value=userID;
				paramCode[2].Value = realName;
				paramCode[3].Value = deptID;
				paramCode[4].Value = limitTime;
				paramCode[5].Value = status;
                paramCode[6].Value = onlineActive;
				paramCode[7].Value = SysAdmin;
				paramCode[8].Direction = ParameterDirection.ReturnValue;
                result = SqlHelper.ExecSPCommand("Gmtool_Gminfo_Edit",paramCode);
				if(userbyID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(userbyID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
		#region 修改密码
		/// <summary>
		/// 修改密码
		/// </summary>
		/// <param name="userbyID">操作员ID</param>
		/// <param name="userID">用户ID</param>
		/// <param name="passwd">密码</param>
		/// <returns>操作结果</returns>
		public static int UpdateRow(int userbyID,int userID,string passwd)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{ new SqlParameter("@Gm_OperateUserID",SqlDbType.Int),
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@Gm_Password",SqlDbType.VarChar,50),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userbyID;
				paramCode[1].Value=userID;
                paramCode[2].Value = convertToMD5(passwd);
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Gmtool_PASSWD_Edit",paramCode);
				if(userbyID == 0)
				{
					CommonInfo.SDO_OperatorLogDel(userbyID);
				}
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion
        #region 根据用户ID得到一个用户信息
        /// <summary>
		/// 得到一个用户信息
		/// </summary>
		/// <param name="userID">用户ID</param>
		/// <returns></returns>
		public static DataSet getUserInfo(int userID)
		{
			string strSQL;
			strSQL = "select * from GMTOOLS_Users where User_status =1 and userID="+userID;

			System.Data.DataSet ds = SqlHelper.ExecuteDataset(strSQL);

			return ds;
		}
		#endregion
		#region 根据用户名和密码和MAC判断当前用户已经登录
		/// <summary>
		/// 根据用户名和密码和MAC判断当前用户已经登录
		/// </summary>
		/// <param name="userName">用户名</param>
		/// <param name="passWord">密码</param>
		/// <returns>数据是否存在</returns>
		public static int getActiveUser(string userName,string passWord,string mac)
		{
			string querySQL = "select onlineActive,sysAdmin from GMTOOLS_Users where user_status=1 and userName='"+userName+"' and user_pwd='"+convertToMD5(passWord)+"' and user_mac='"+mac+"'";
			System.Data.DataSet result = SqlHelper.ExecuteDataset(querySQL);
			if(result.Tables[0].Rows.Count>0)
			{
				return Convert.ToInt32(result.Tables[0].Rows[0].ItemArray[0].ToString());
			}
			else
				return 0;
		}
		#endregion
		#region 根据用户名和密码验证数据存在
		/// <summary>
		/// 根据用户名和密码验证数据存在
		/// </summary>
		/// <param name="userName">用户名</param>
		/// <param name="passWord">密码</param>
		/// <returns>数据是否存在</returns>
		public static int getUserInfo(string userName,string passWord)
		{
				string querySQL = "select user_mac from GMTOOLS_Users where user_status=1 and userName='"+userName+"' and user_pwd='"+convertToMD5(passWord)+"'";
				System.Data.DataSet result = SqlHelper.ExecuteDataset(querySQL);
				if(result.Tables[0].Rows.Count>0)
				{
					if(result.Tables[0].Rows[0].ItemArray[0].ToString().Length<=0 || result.Tables[0].Rows[0].IsNull(0)==true)
					{
						return 0;
					}
					else
						return 1;
				}
				else
					return 1;
		}
		#endregion
		#region 用户身份验证
		/// <summary>
		/// 根据用户名和密码验证数据存在
		/// </summary>
		/// <param name="userName">用户名</param>
		/// <param name="passWord">密码</param>
		/// <param name="MAC">MAC码</param>
		/// <returns>数据是否存在</returns>
		public static int getUserInfo(string userName,string passWord,string MAC)
		{
			string querySQL = "select * from GMTOOLS_Users where user_status = 1 and userName='"+userName+"' and user_pwd='"+convertToMD5(passWord)+"' and user_MAC='"+MAC+"' and datediff(d,limit,getdate())<0";
			System.Data.DataSet result = SqlHelper.ExecuteDataset(querySQL);
			if(result.Tables[0].Rows.Count>0)
			{
				return Convert.ToInt32(result.Tables[0].Rows[0].ItemArray[0].ToString());
			}
			else
				return Convert.ToInt32(result.Tables[0].Rows[0].ItemArray[0].ToString());

		}
		#endregion
		#region 用户名
		public string  UserName
		{
			get
			{
				return this.userName;
			}
			set
			{
				this.userName =value;
			}

		}
		#endregion
		#region 密码
		public string  PassWord
		{
			get
			{
				return this.passWord;
			}
			set
			{
				this.passWord =value;
			}

		}
		#endregion
		#region MAC
		public string MAC
		{
			get
			{
				return this.mac;
			}
			set
			{
				this.mac =value;
			}
		}
		#endregion
		#region 姓名
		public string RealName
		{
			get
			{
				return this.realName;
			}
			set
			{
				this.realName =value;
			}
		}

		#endregion
		#region 系统管理员
		public int SysAdmin
		{
			get
			{
				return this.sysAdmin;
			}
			set
			{
				this.sysAdmin =value;
			}
		}

		#endregion
        #region 加密代码
        public static string qswhMD5(string str)
        {
            byte[] b=System.Text.Encoding.Default.GetBytes(str);
            b=new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
            string ret="";
            for(int i=0;i<b.Length;i++)
                ret+=b[i].ToString ("x").PadLeft(2,'0');
            return ret;
        }
        #endregion
        #region 明文转换密钥
        public static string convertToMD5(string source)
        {
            byte[] md5 = System.Text.UTF8Encoding.UTF8.GetBytes(source.Trim());
            MD5CryptoServiceProvider objMD5 = new MD5CryptoServiceProvider();
            byte[] output = objMD5.ComputeHash(md5, 0, md5.Length);
            return BitConverter.ToString(output);  
        }
        #endregion

    }
}
