using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using STONE.HU.HELPER.UTIL;
using System.Configuration;

namespace Common.Logic
{

	/**//// <summary>
	/// Summary description for SqlSvrHelper.
	/// </summary>
	public class SqlHelper
	{

		private static string auUserID = null;
		private static string  auPwdID = null;
		private string url = null;
		private string userID = null;
		private string pwd = null;
		public static Log log = null;
		public static Log errLog = null;
		public static string ConnectionString;

		#region SD 高达
		private static string SDUser = ConfigurationSettings.AppSettings["SDUser"];
		private static string SDPwd = ConfigurationSettings.AppSettings["SDPwd"];
		private static string SDMember = ConfigurationSettings.AppSettings["SDMember"];
		#endregion

		#region 劲舞团2
		private static string jw2Userid = ConfigurationSettings.AppSettings["jw2user"];
		private static string jw2pwd = ConfigurationSettings.AppSettings["jw2pwd"];


		private static string jw2gwUserid = ConfigurationSettings.AppSettings["jw2GWuser"];
		private static string jw2gwpwd = ConfigurationSettings.AppSettings["jw2GWpwd"];
		private static string jw2gwversion = ConfigurationSettings.AppSettings["jw2GWVersion"];

		private static string jw2GameDB = ConfigurationSettings.AppSettings["jw2GameDB"];
		private static string jw2ItemDB = ConfigurationSettings.AppSettings["jw2ItemDB"];
		private static string jw2LogDB = ConfigurationSettings.AppSettings["jw2LogDB"];
		private static string jw2LoginDB = ConfigurationSettings.AppSettings["jw2LoginDB"];
		private static string jw2MessengerDB = ConfigurationSettings.AppSettings["jw2MessengerDB"];

		
		private static int jw2GameDB_Rep = 1;
		private static int jw2ItemDB_Rep = 2;
		private static int jw2LoginDB_Rep = 3;
		private static int jw2LogDB_Rep = 4;
		private static int jw2GateWay = 5;
		private static int jw2Messenger_Rep = 6;
		private static int jw2GameDB_Master = 7;
		private static int jw2LoginDB_Master = 8;
		private static int jw2Messenger_Master = 9;
		private static int jw2ItemDB_Master = 10;
		private static int jw2LogDB_Master = 11;
		#endregion
        
		#region jw2 oracle访问
		private static string OracleU = ConfigurationSettings.AppSettings["OracleUser"];
		private static string OracleP = ConfigurationSettings.AppSettings["OraclePwd"];
		private static string OracleD = ConfigurationSettings.AppSettings["OracleData"];
		#endregion

		public SqlHelper()
		{
		}

		public static string GetConnectionString(bool includedb,string host,string userName,string passwd,string dbName)
		{
			if (includedb)
			{
				string strConn = String.Format("server={0};user id={1};password={2};database={3};" +
					"pooling=false;charset=gb2312", host,userName,passwd,dbName);
				return strConn;
			}
			return String.Format("server={0};user id={1};password={2};" +
				"persist security info=true;", host, userName, passwd);
		}


		public static string GetConnectionString(string host,string userName,string passwd,string dbName)
		{
			return String.Format("server={0};user id={1};password={2};database={3};" +
				"persist security info=true;", host, userName, passwd,dbName);
		}

		public static string JW2GetConnectionString(string host,string userName,string passwd,string dbName)
		{
			string serverip = host.Split(',')[0].ToString();
			string strprot = host.Split(',')[1].ToString();
			return String.Format("server={0};user id={1};password={2};database={3};" +
				"pooling=false;Port={4};charset=gbk", serverip,userName,passwd,dbName,strprot);
		}


		#region ExecCommand
		/// <summary>
		/// 返回查询满足条件数据的结果
		/// </summary>
		/// <param name="sqlcom">输入查询语句</param>
		/// <returns>查询结果</returns>
		public static int ExecCommand(SqlCommand sqlcom)
		{
			SqlTransaction myTrans =null;
			SqlConnection conn=new SqlConnection(ConnectionString);
			sqlcom.Connection =conn;
			conn.Open();
			try
			{
				myTrans = conn.BeginTransaction();
				sqlcom.Transaction = myTrans;
				int rtn=sqlcom.ExecuteNonQuery();
				myTrans.Commit();
				return rtn;
			}
			catch(Exception ex) 
			{
				SqlHelper.errLog.WriteLog(ex.Message);
				myTrans.Rollback();
				return -1;
			}
			finally
			{
				conn.Close();

			}


		}
		public static int ExecCommand(string sql)
		{
			if (sql.EndsWith(",")) sql=sql.Substring(0,sql.Length-1);
        
			SqlCommand sqlcom=new SqlCommand(sql);
			return ExecCommand(sqlcom);                
		}
		#endregion       

		/// <summary>
		/// 访问不同端口mysql数据库
		/// </summary>
		/// <param name="host">ip地址</param>
		/// <param name="userName">用户名</param>
		/// <param name="passwd">密码</param>
		/// <param name="dbName">数据库名</param>
		/// <param name="strprot">访问端口</param>
		/// <returns></returns>
		public static string GetConnectionString(string host,string userName,string passwd,string dbName,string strprot)
		{
			return String.Format("server={0};user id={1};password={2};database={3};" +
				"pooling=false;Port={4};charset=gb2312", host,userName,passwd,dbName,strprot);
		}

		public static string GetConnectionString(bool includedb,string host,string userName,string passwd,string dbName,string charset)
		{
			
			string strConn  = String.Format("server={0};user id={1};password={2};database={3};" +
				"charset={4};pooling=false", host,userName,passwd,dbName,charset);
			return strConn;
		}

		public void init(string url,string dbName,string userID,string pwd)
		{
			string path= AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
			log = new Log(path+"\\log.txt", true, false, 0);	
			errLog = new Log(path+"\\errLog\\log.txt", true, false, 0);
			ConnectionString = "Data Source="+url+";Network Library=DBMSSOCN;Initial Catalog="+dbName+";User ID="+userID+";Password="+pwd+";Pooling=true;Min Pool Size=0;Max Pool Size=10;";
		}

		public static int insertGMtoolsLog(int userID,string gameName,string serverIP,string sp_name,string RealAct)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@Gm_GameName",SqlDbType.VarChar,30),
												   new SqlParameter("@Gm_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@Gm_SPname",SqlDbType.VarChar,30),
												   new SqlParameter("@Gm_RealAct",SqlDbType.VarChar,500),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userID;
				paramCode[1].Value=gameName;
				paramCode[2].Value=serverIP;
				paramCode[3].Value=sp_name;
				paramCode[4].Value=RealAct;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				SqlHelper.ExecSPCommand("sp_InsertGMtoolslog",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		public static int ExecCommand(string Con,string sql,bool b)
		{     
			SqlTransaction myTrans =null;
			int rtn=-1;
			SqlConnection conn=new SqlConnection(Con);
			SqlCommand sqlcom=new SqlCommand(sql,conn);
			sqlcom.Connection =conn;
			conn.Open();
			try
			{		
				return rtn=sqlcom.ExecuteNonQuery();
			}
			catch(Exception ex) 
			{
				SqlHelper.errLog.WriteLog(ex.Message);
				return -1;
			}
			finally
			{
				conn.Close();
			}
		}
		#region GetDataSet
		/// <summary>
		///  得到一个查询的数据集
		/// </summary>
		/// <param name="commandText">输入查询语句</param>
		/// <returns>查询的数据集</returns>
		public static DataSet ExecuteDataset(string commandText)
		{
			DataSet ds = null;
			try
			{
				if( ConnectionString == null || ConnectionString.Length == 0 ) throw new ArgumentNullException( "ConnectionString" );

				using (SqlConnection connection = new SqlConnection(ConnectionString))
				{
					connection.Open();
					// Create the DataAdapter & DataSet
					SqlCommand cmd = new SqlCommand(commandText,connection);
					cmd.CommandType = CommandType.Text;
					using( SqlDataAdapter da = new SqlDataAdapter(cmd) )
					{
						ds = new DataSet();
						da.Fill(ds);
						connection.Close();


					}

				}  
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog(ex.Message);
			}
			return ds;
		}
		#endregion
		#region GetDataSet
		/// <summary>
		///  得到一个查询的数据集
		/// </summary>
		/// <param name="commandText">输入查询语句</param>
		/// <returns>查询的数据集</returns>
		public static DataSet ExecuteDataset(string ConnectionString,string commandText)
		{
			DataSet ds = null;
			try
			{
				if( ConnectionString == null || ConnectionString.Length == 0 ) throw new ArgumentNullException( "ConnectionString" );

				using (SqlConnection connection = new SqlConnection(ConnectionString))
				{
					connection.Open();
					// Create the DataAdapter & DataSet
					SqlCommand cmd = new SqlCommand(commandText,connection);
					cmd.CommandType = CommandType.Text;
					using( SqlDataAdapter da = new SqlDataAdapter(cmd) )
					{
						ds = new DataSet();
						da.Fill(ds);
						connection.Close();


					}

				}  
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog(ex.Message);
			}
			return ds;
		}
		
		#endregion
 		
		#region ExecuteScalar
		/// <summary>
		/// 查询满足条件的数据集的对象
		/// </summary>
		/// <param name="sql">输入查询语句</param>
		/// <returns>返回数据集的对象</returns>
		public static object ExecuteScalar(string sql)
		{
			SqlConnection conn=new SqlConnection(ConnectionString);
			SqlCommand sqlcom=new SqlCommand(sql,conn);
			conn.Open();
			try
			{
				object rtn=sqlcom.ExecuteScalar ();
				return rtn;
			}
			catch(Exception ex) 
			{
				throw ex;                
			}
			finally
			{
				conn.Close();
			}
		}
		#endregion
		#region ExecSPCommand
		/// <summary>
		/// 执行存储过程
		/// </summary>
		/// <param name="strProc">存储过程名</param>
		/// <param name="param">输入存储过程的参数集</param>
		/// <param name="count">参数个数</param>
		/// <returns>执行结果</returns>
		public static int ExecSPCommand(string strProc,ArrayList param,int count)
		{
			int returnValue =0;
			SqlConnection conn=new SqlConnection(ConnectionString);
			SqlCommand sqlcom=new SqlCommand(strProc,conn);
			sqlcom.CommandType= CommandType.StoredProcedure ;
			try
			{
				conn.Open();
				if(param!=null && count!=0)
				{
					foreach(System.Data.IDataParameter paramer in param)
					{
						sqlcom.Parameters.Add(paramer);
					}  
				}

				sqlcom.ExecuteNonQuery();
				if(count!=0)
				{

					if(!Convert.IsDBNull(sqlcom.Parameters["@result"].Value))
					{
						returnValue=Convert.ToInt32(sqlcom.Parameters["@result"].Value);
					}
				}
				sqlcom.Parameters.Clear();
				conn.Close();
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				conn.Close();
			}
			return returnValue;
			//string sourceIP=(string)param[1];


		}
		#endregion
		#region ExecSPCommand
		/// <summary>
		/// 调用存储过程
		/// </summary>
		/// <param name="strProc">存储过程名</param>
		/// <param name="paramers">输入参数集</param>
		public static int ExecSPCommand(string strProc,System.Data.IDataParameter[] paramers)
		{
			int returnValue = 0;
			SqlConnection conn=new SqlConnection(ConnectionString);
			SqlCommand sqlcom=new SqlCommand(strProc,conn);
			sqlcom.CommandType= CommandType.StoredProcedure ;

			foreach(System.Data.IDataParameter paramer in paramers)
			{
				sqlcom.Parameters.Add(paramer);
			}            
			conn.Open();
			try
			{
				sqlcom.ExecuteNonQuery();
				if(!Convert.IsDBNull(sqlcom.Parameters["@result"].Value))
				{
					returnValue=Convert.ToInt32(sqlcom.Parameters["@result"].Value);
				}
			}
			catch(Exception ex) 
			{
				string s=ex.Message ;
			}
			finally
			{
				conn.Close();
			}
			return returnValue;
		}
		#endregion
		#region ExecSPDataSet
		/// <summary>
		/// 得到存储过程返回数据集
		/// </summary>
		/// <param name="strProc">存储过程名</param>
		/// <param name="paramers">输入参数集</param>
		/// <returns>返回数据集</returns>
		public static DataSet ExecSQL(string host,string strSQL)
		{
			try
			{
				SqlConnection conn=new SqlConnection(GetConnectionString(false,host,"fjlogAnalyese","fjlogAnalyese^*$%","fj_log_bak"));
				SqlCommand sqlcom=new SqlCommand(strSQL,conn);
				sqlcom.CommandTimeout = 6000;
				sqlcom.CommandType= CommandType.Text ;
				conn.Open();
				SqlDataAdapter da=new SqlDataAdapter();
				da.SelectCommand=sqlcom;
				DataSet ds=new DataSet();
				da.Fill(ds);
				conn.Close();
				return ds;
			}
			catch(System.Exception)
			{
				return null;
			}
		}
		public static DataSet ExecSQL(string host,string strSQL,string game,int i)
		{
			try
			{
				SqlConnection conn=new SqlConnection(GetConnectionString(host,"fjlogAnalyese","fjlogAnalyese^*$%","cg_itemlog"));
				SqlCommand sqlcom=new SqlCommand(strSQL,conn);
				sqlcom.CommandTimeout = 6000;
				sqlcom.CommandType= CommandType.Text ;
				conn.Open();
				SqlDataAdapter da=new SqlDataAdapter();
				da.SelectCommand=sqlcom;
				DataSet ds=new DataSet();
				da.Fill(ds);
				conn.Close();
				return ds;
			}
			catch(System.Exception ex)
			{
				string str = ex.Message;
				return null;
			}
		}
		public static DataSet ExecSQL(string host,string strSQL,string game)
		{
			try
			{
				//SqlConnection conn=new SqlConnection(GetConnectionString(false,host,"wow","Blizzard","fjlog"));
				SqlConnection   conn   =   new   SqlConnection("data source="+host.ToString()+";uid=wow;pwd=Blizzard;Initial Catalog=fjlog"); 
				SqlCommand sqlcom=new SqlCommand(strSQL,conn);
				sqlcom.CommandTimeout = 6000;
				sqlcom.CommandType= CommandType.Text ;
				conn.Open();
				SqlDataAdapter da=new SqlDataAdapter();
				da.SelectCommand=sqlcom;
				DataSet ds=new DataSet();
				da.Fill(ds);
				conn.Close();
				return ds;
			}
			catch(SqlException ex)
			{	
				string ss =ex.Message;
				return null;
			}
		}

		#endregion ExecSPDataSet
		#region ExecSPDataSet
		/// <summary>
		/// 得到存储过程返回数据集
		/// </summary>
		/// <param name="strProc">存储过程名</param>
		/// <param name="paramers">输入参数集</param>
		/// <returns>返回数据集</returns>
		public static DataSet ExecSPDataSet(string strProc,System.Data.IDataParameter[] paramers)
		{
			SqlConnection conn=new SqlConnection(ConnectionString);
			SqlCommand sqlcom=new SqlCommand(strProc,conn);
			sqlcom.CommandType= CommandType.StoredProcedure ;

			foreach(System.Data.IDataParameter paramer in paramers)
			{
				sqlcom.Parameters.Add(paramer);
			}            
			conn.Open();
            
			SqlDataAdapter da=new SqlDataAdapter();
			da.SelectCommand=sqlcom;
			DataSet ds=new DataSet();
			da.Fill(ds);
        
			conn.Close();
			return ds;
		}

		#endregion ExecSPDataSet
		#region ExecSPDataSet
		/// <summary>
		/// 得到存储过程返回数据集
		/// </summary>
		/// <param name="strProc">存储过程名</param>
		/// <param name="paramers">输入参数集</param>
		/// <returns>返回数据集</returns>
		public static DataSet ExecSPDataSet(string strProc)
		{
			SqlConnection conn=new SqlConnection(ConnectionString);
			SqlCommand sqlcom=new SqlCommand(strProc,conn);
			sqlcom.CommandType= CommandType.StoredProcedure ;
           
			conn.Open();
            
			SqlDataAdapter da=new SqlDataAdapter();
			da.SelectCommand=sqlcom;
			DataSet ds=new DataSet();
			da.Fill(ds);
        
			conn.Close();
			return ds;
		}

		#endregion DbType
		#region DbType
		private static System.Data.DbType GetDbType(Type type)
		{
			DbType result = DbType.String;
			if( type.Equals(typeof(int)) ||  type.IsEnum)
				result = DbType.Int32;
			else if( type.Equals(typeof(long)))
				result = DbType.Int32;
			else if( type.Equals(typeof(double)) || type.Equals( typeof(Double)))
				result = DbType.Decimal;
			else if( type.Equals(typeof(DateTime)))
				result = DbType.DateTime;
			else if( type.Equals(typeof(bool)))
				result = DbType.Boolean;
			else if( type.Equals(typeof(string) ) )
				result = DbType.String;
			else if( type.Equals(typeof(decimal)))
				result = DbType.Decimal;
			else if( type.Equals(typeof(byte[])))
				result = DbType.Binary;
			else if( type.Equals(typeof(Guid)))
				result = DbType.Guid;
        
			return result;
            
		}

		#endregion UpdateTable
		#region UpdateTable
		public static void UpdateTable(DataTable dt,string TableName,string KeyName)
		{
			foreach(DataRow dr in dt.Rows)
			{
				updateRow(dr,TableName,KeyName);
			}
		}
		#endregion InsertTable
		#region InsertTable
		//用于主键是数据库表名+ID类型的
		public static void InsertTable(DataTable dt)
		{
			string TableName="["+dt.TableName+"]";
			string KeyName=dt.TableName+"ID";
			foreach(DataRow dr in dt.Rows)
			{
				insertRow(dr,TableName,KeyName);
			}
		}
		//用于主键是任意类型的
		public static void InsertTable(DataTable dt,string KeyName)
		{
			string TableName="["+dt.TableName+"]";
			foreach(DataRow dr in dt.Rows)
			{
				insertRow(dr,TableName,KeyName);
			}
		}
		#endregion updateRow
		#region updateRow
		private static void  updateRow(DataRow dr,string TableName,string KeyName)
		{
			if (dr[KeyName]==DBNull.Value ) 
			{
				throw new Exception(KeyName +"的值不能为空");
			}
            
			if (dr.RowState ==DataRowState.Deleted)
			{
				deleteRow(dr,TableName,KeyName);
 
			}
			else if (dr.RowState ==DataRowState.Modified )
			{
				midifyRow(dr,TableName,KeyName);
			}
			else if (dr.RowState ==DataRowState.Added  )
			{
				insertRow(dr,TableName,KeyName);
			}
			else if (dr.RowState ==DataRowState.Unchanged )
			{
				midifyRow(dr,TableName,KeyName);
			}           
		}

		#endregion deleteRow
		#region deleteRow
		private static void  deleteRow(DataRow dr,string TableName,string KeyName)
		{
			string sql="Delete {0} where {1} =@{1}";
			DataTable dtb=dr.Table ;
			sql=string.Format(sql,TableName,KeyName);

			SqlCommand sqlcom=new SqlCommand(sql);
			System.Data.IDataParameter iparam=new  SqlParameter();
			iparam.ParameterName    = "@"+ KeyName;
			iparam.DbType            = GetDbType(dtb.Columns[KeyName].DataType);
			iparam.Value            = dr[KeyName];
			sqlcom.Parameters .Add(iparam);
            
			ExecCommand(sqlcom);
		}
		#endregion midifyRow
		#region midifyRow
		private static void  midifyRow(DataRow dr,string TableName,string KeyName)
		{
			string UpdateSql            = "Update {0} set {1} {2}";
			string setSql="{0}= @{0}";
			string wherSql=" Where {0}=@{0}";
			StringBuilder setSb    = new StringBuilder();

			SqlCommand sqlcom=new SqlCommand();
			DataTable dtb=dr.Table;
        
			for (int k=0; k<dr.Table.Columns.Count; ++k)
			{
				System.Data.IDataParameter iparam=new  SqlParameter();
				iparam.ParameterName    = "@"+ dtb.Columns[k].ColumnName;
				iparam.DbType            = GetDbType(dtb.Columns[k].DataType);
				iparam.Value            = dr[k];
				sqlcom.Parameters .Add(iparam);

				if (dtb.Columns[k].ColumnName==KeyName)
				{
					wherSql=string.Format(wherSql,KeyName);
				}
				else
				{
					setSb.Append(string.Format(setSql,dtb.Columns[k].ColumnName));    
					setSb.Append(",");
				}
                
			}
            
			string setStr=setSb.ToString();
			setStr=setStr.Substring(0,setStr.Length -1); //trim ,
            
			string sql = string.Format(UpdateSql, TableName, setStr,wherSql);
			sqlcom.CommandText =sql;    
			try
			{
				ExecCommand(sqlcom);
			}
			catch(Exception ex)
			{
				throw ex;            
			}
		}
		#endregion insertRow
		#region insertRow
		private static void  insertRow(DataRow dr,string TableName,string KeyName)
		{
			string InsertSql = "Insert into {0}({1}) values({2})";
			SqlCommand sqlcom=new SqlCommand();
			DataTable dtb=dr.Table ;
			StringBuilder insertValues    = new StringBuilder();
			StringBuilder cloumn_list    = new StringBuilder();
			for (int k=0; k<dr.Table.Columns.Count; ++k)
			{
				//just for genentae，
				if (dtb.Columns[k].ColumnName==KeyName) continue;
				System.Data.IDataParameter iparam=new  SqlParameter();
				iparam.ParameterName    = "@"+ dtb.Columns[k].ColumnName;
				iparam.DbType            = GetDbType(dtb.Columns[k].DataType);
				iparam.Value            = dr[k];
				sqlcom.Parameters .Add(iparam);

				cloumn_list.Append(dtb.Columns[k].ColumnName);
				insertValues.Append("@"+dtb.Columns[k].ColumnName);

				cloumn_list.Append(",");
				insertValues.Append(",");
			}
            
			string cols=cloumn_list.ToString();
			cols=cols.Substring(0,cols.Length -1);

			string values=insertValues.ToString();
			values=values.Substring(0,values.Length -1);
            
			string sql = string.Format(InsertSql, TableName,cols ,values);
			sqlcom.CommandText =sql;    
			try
			{
				ExecCommand(sqlcom);
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
		#endregion
		#region commitTrans
		public static int commitTrans(string strSQL,System.Data.IDataParameter[] paramers)
		{
			SqlConnection myConnection = new SqlConnection(SqlHelper.ConnectionString);
			myConnection.Open();
			// Start a local transaction.
			SqlTransaction myTrans = myConnection.BeginTransaction();

			// Enlist the command in the current transaction.
			SqlCommand myCommand = myConnection.CreateCommand();
			myCommand.Transaction = myTrans;
			myCommand.CommandType = CommandType.Text;

			foreach(System.Data.IDataParameter paramer in paramers)
			{
				myCommand.Parameters.Add(paramer);
			}   
			
			try
			{
				myCommand.CommandText = strSQL;
				myCommand.ExecuteNonQuery();
				myTrans.Commit();
				return 1;
			}
			catch(Exception e)
			{
				try
				{
					myTrans.Rollback();
					return 0;
				}
				catch (SqlException ex)
				{
					if (myTrans.Connection != null)
					{
						Console.WriteLine("An exception of type " + ex.GetType() +
							" was encountered while attempting to roll back the transaction.");
					}
				}

				Console.WriteLine("An exception of type " + e.GetType() +
					"was encountered while inserting the data.");
				Console.WriteLine("Neither record was written to database.");
			}
			finally
			{
				myConnection.Close();
			}
			return 1;

		}
		#endregion
		#region commitTrans
		public static void commitTrans(string strSQL)
		{
			SqlConnection myConnection = new SqlConnection(SqlHelper.ConnectionString);
			myConnection.Open();
			// Start a local transaction.
			SqlTransaction myTrans = myConnection.BeginTransaction();

			// Enlist the command in the current transaction.
			SqlCommand myCommand = myConnection.CreateCommand();
			myCommand.Transaction = myTrans;
			myCommand.CommandType = CommandType.Text;

			try
			{
				myCommand.CommandText = strSQL;
				myCommand.ExecuteNonQuery();
				myTrans.Commit();
				Console.WriteLine("Both records are written to database.");
			}
			catch(Exception e)
			{
				try
				{
					myTrans.Rollback();
				}
				catch (SqlException ex)
				{
					if (myTrans.Connection != null)
					{
						Console.WriteLine("An exception of type " + ex.GetType() +
							" was encountered while attempting to roll back the transaction.");
					}
				}

				Console.WriteLine("An exception of type " + e.GetType() +
					"was encountered while inserting the data.");
				Console.WriteLine("Neither record was written to database.");
			}
			finally
			{
				myConnection.Close();
			}


		}
		#endregion
		#region transcation
		public static void transcation(string procName)
		{
			SqlConnection myConnection = new SqlConnection("Data Source=192.168.9.124;Initial Catalog=Northwind;Integrated Security=SSPI;");
			myConnection.Open();

			// Start a local transaction.
			SqlTransaction myTrans = myConnection.BeginTransaction();

			// Enlist the command in the current transaction.
			SqlCommand myCommand = myConnection.CreateCommand();
			myCommand.Transaction = myTrans;
			myCommand.CommandType = CommandType.StoredProcedure;

			try
			{
				myCommand.CommandText = procName;
				myCommand.ExecuteNonQuery();
				myTrans.Commit();
				Console.WriteLine("Both records are written to database.");
			}
			catch(Exception e)
			{
				try
				{
					myTrans.Rollback();
				}
				catch (SqlException ex)
				{
					if (myTrans.Connection != null)
					{
						Console.WriteLine("An exception of type " + ex.GetType() +
							" was encountered while attempting to roll back the transaction.");
					}
				}

				Console.WriteLine("An exception of type " + e.GetType() +
					"was encountered while inserting the data.");
				Console.WriteLine("Neither record was written to database.");
			}
			finally
			{
				myConnection.Close();
			}

		}

        #endregion
		#region URL
		public  string URL
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}
		#endregion
		#region USERID
		public  string UserID
		{
			get
			{
				return this.userID;
			}
			set
			{
				this.userID = value;
			}
		}
		#endregion
		#region PWD
		public  string Pwd
		{
			get
			{
				return this.pwd;
			}
			set
			{
				this.pwd = value;
			}
		}
		#endregion


		#region AUUser
		public static string AuUser
		{
			get
			{
				return SqlHelper.auUserID;
			}
			set
			{
				
				SqlHelper.auUserID = System.Text.Encoding.Default.GetString(Convert.FromBase64String(value));
			}
		}
		#endregion
		#region AUPWD
		public static string AuPwd
		{
			get
			{
				return SqlHelper.auPwdID;
			}
			set
			{
				SqlHelper.auPwdID = System.Text.Encoding.Default.GetString(Convert.FromBase64String(value));
			}
		}
		#endregion

		public static string SdUser
		{
			get
			{
				return SqlHelper.SDUser;
			}
		}
		public static string SdPwd
		{
			get
			{
				return SqlHelper.SDPwd;
			}
		}
		
		public static string SdMember
		{
			get
			{
				return SqlHelper.SDMember;
			}
		}

		public static string jw2gwUser
		{
			get
			{
				return SqlHelper.jw2gwUserid;
			}
		}

		public static string jw2gwUserPwd
		{
			get
			{
				return SqlHelper.jw2gwpwd;
			}
		}
		public static string jw2gwVersion
		{
			get
			{
				return SqlHelper.jw2gwversion;
			}
		}
		
		public static string jw2User
		{
			get
			{
				return SqlHelper.jw2Userid;
			}
		}

		public static string jw2UserPwd
		{
			get
			{
				return SqlHelper.jw2pwd;
			}
		}

		public static string jw2gameDB
		{
			get
			{
				return SqlHelper.jw2GameDB;
			}
		}

		public static string jw2itemDB
		{
			get
			{
				return SqlHelper.jw2ItemDB;
			}
		}

		public static string jw2logDB
		{
			get
			{
				return SqlHelper.jw2LogDB;
			}
		}

		public static string jw2loginDB
		{
			get
			{
				return SqlHelper.jw2LoginDB;
			}
		}

		public static string jw2messengerDB
		{
			get
			{
				return SqlHelper.jw2MessengerDB;
			}
		}


		#region JW2_DB
		public static int jw2gameDB_Rep
		{
			get
			{
				return SqlHelper.jw2GameDB_Rep;
			}
		}

		public static int jw2itemDB_Rep
		{
			get
			{
				return SqlHelper.jw2ItemDB_Rep;
			}
		}

		public static int jw2logDB_Rep
		{
			get
			{
				return SqlHelper.jw2LogDB_Rep;
			}
		}

		public static int jw2loginDB_Rep
		{
			get
			{
				return SqlHelper.jw2LoginDB_Rep;
			}
		}

		public static int jw2messenger_Rep
		{
			get
			{
				return SqlHelper.jw2Messenger_Rep;
			}
		}

		public static int jw2gameDB_Master
		{
			get
			{
				return SqlHelper.jw2GameDB_Master;
			}
		}

		public static int jw2itemDB_Master
		{
			get
			{
				return SqlHelper.jw2ItemDB_Master;
			}
		}

		public static int jw2logDB_Master
		{
			get
			{
				return SqlHelper.jw2LogDB_Master;
			}
		}

		public static int jw2loginDB_Master
		{
			get
			{
				return SqlHelper.jw2LoginDB_Master;
			}
		}

		public static int jw2messenger_Master
		{
			get
			{
				return SqlHelper.jw2Messenger_Master;
			}
		}
		#endregion

		public static string oracleUser
		{
			get
			{
				return SqlHelper.OracleU;
			}
		}
		public static string oraclePwd
		{
			get
			{
				return SqlHelper.OracleP;
			}
		}
		public static string oracleData
		{
			get
			{
				return SqlHelper.OracleD;
			}
		}
	}
}

