using System;
using System.Xml;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Text;
using System.Runtime.InteropServices;
using Common.Logic;
using Common.API;
using MySql.Data.MySqlClient;
using Common.DataInfo;
using lg = Common.API.LanguageAPI;

namespace GM_Server.JW2DataInfo
{
	/// <summary>
	/// JW2LoginDataInfo 的摘要说明。
	/// </summary>
	public class JW2LoginDataInfo
	{
		//加密
		

		[DllImport("OperTool.dll", CharSet = CharSet.Ansi)]
		public static extern bool BulletinTool(StringBuilder IP, int port, StringBuilder content);
		[DllImport("OperTool.dll", CharSet = CharSet.Ansi)]
		public static extern bool KickTool(StringBuilder IP, int port, StringBuilder userID);

		#region DLL加载
		/// <summary>
		///创建DSNShell（返回值为DSNShell的句柄）
		/// </summary>
		[DllImport("DSNShell.dll")]
		public static extern int CreateDSNShell();
		/// <summary>
		///销毁DSNShell（句柄）
		/// </summary>
		[DllImport("DSNShell.dll")]
		public static extern void DestroyDSNShell(int handle);
		/// <summary>
		///更新DSNShell（句柄，收到消息的标志）
		/// </summary>
		[DllImport("DSNShell.dll")]
		public static extern int DSNShellUpdate(int handle,byte[] result);
		/// <summary>
		///连接GW（句柄，IP，端口）---Send
		/// </summary>
		[DllImport("DSNShell.dll", CharSet = CharSet.Unicode)]
		public static extern int DSNShellConnectGW(int handle,StringBuilder Ip, int port);
		/// <summary>
		///连接GW（句柄，IP，端口）---Rev
		/// </summary>
		[DllImport("DSNShell.dll")]
		public static extern int DSNShellIsGWConnected(int handle);
		/// <summary>
		///登陆GW（句柄，帐号，密码，版本号）--Send
		/// </summary>
		[DllImport("DSNShell.dll", CharSet = CharSet.Unicode)]
		public static extern int DSNShellLoginGW(int handle,StringBuilder szAccount, StringBuilder szPassword, StringBuilder szVersion);
		/// <summary>
		///登陆GW（句柄，帐号，密码，版本号）--Rev
		/// </summary>
		[DllImport("DSNShell.dll")]
		public static extern int DSNShellLoginGWRet(int handle,byte[] result);
		/// <summary>
		///取得GS列表（句柄）--Send
		/// </summary>
		[DllImport("DSNShell.dll")]
		public static extern int DSNShellServerList(int handle);
		/// <summary>
		///取得GS列表（句柄）--Rev
		/// </summary>
		[DllImport("DSNShell.dll")]
		public static extern int DSNShellServerListRet(int handle, byte[] result);
		/// <summary>
		///设置当前GS的经验倍数（句柄，倍数——仅限1和2）
		/// </summary>
		[DllImport("DSNShell.dll")]
		public static extern int DSNShellSetGPointTimes(int handle,  byte times);
		/// <summary>
		///设置当前GS的G币倍数（句柄，倍数——仅限1和2）
		/// </summary>
		[DllImport("DSNShell.dll")]
		public static extern int DSNShellSetExpTimes(int handle,  byte times);
		/// <summary>
		///连接GS（句柄，IP，端口）--Send
		/// </summary>
		[DllImport("DSNShell.dll", CharSet = CharSet.Unicode)]
		public static extern int DSNShellConnectGS(int handle, StringBuilder Ip, int port);
		/// <summary>
		///连接GS（句柄，IP，端口）--Rev
		/// </summary>
		[DllImport("DSNShell.dll")]
		public static extern int DSNShellIsGSConnected(int handle);
		/// <summary>
		///登陆GS（句柄，用户序号）--Send
		/// </summary>
		[DllImport("DSNShell.dll")]
		public static extern int DSNShellLoginGS(int handle, int ulSerialNo);
		/// <summary>
		///登陆GS（句柄，用户序号）--Rev
		/// </summary>
		[DllImport("DSNShell.dll")]
		public static extern int DSNShellLoginGSRet(int handle, byte result);
		/// <summary>
		/// 踢人（句柄，帐号）
		/// </summary>		
		[DllImport("DSNShell.dll", CharSet = CharSet.Unicode)]
		public static extern int DSNShellKickUser(int handle, StringBuilder szAccount);
		#endregion

		public JW2LoginDataInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		public struct PacketHeaderL
		{
			public byte[] sIP;
		}
		#region DLL接口调用
		#region 创建DSNShell（返回值为DSNShell的句柄）
		/// <summary>
		/// 创建DSNShell（返回值为DSNShell的句柄）
		/// </summary>
		/// <returns></returns>
		public static int JW2_CreateDLL()
		{
			return CreateDSNShell();
		}
		#endregion

		#region 销毁DSNShell（句柄）
		/// <summary>
		/// 销毁DSNShell（句柄）
		/// </summary>
		/// <returns></returns>
		public static void JW2_DestroyDLL(int C_Handle)
		{
			DestroyDSNShell(C_Handle);
		}
		#endregion

		#region 连接GW（句柄，IP，端口）---Send/Rev
		/// <summary>
		/// 连接GW（句柄，IP，端口）---Send/Rev
		/// </summary>
		/// <param name="C_Handle"></param>
		/// <returns></returns>
		public static int JW2_ConnectGW(int C_Handle,string Ip,int Port)
		{
			int state = -1;
			try
			{
				byte[] result_GW = new byte[255];
				StringBuilder sb_IP = new StringBuilder(Ip);
				//连接LOGIN数据库
				if (2 == DSNShellIsGWConnected(C_Handle))
				{
					if (0 == DSNShellConnectGW(C_Handle, sb_IP, Port))
					{
						Console.WriteLine("Begin Connect->GS：" + Ip + "-->" + Port);
					}
				}
				int x = 0;
				//判断LOGIN 连接状态
				while (true)
				{
					System.Threading.Thread.Sleep(500);
					DSNShellUpdate(C_Handle, result_GW);
					state = DSNShellIsGWConnected(C_Handle);
					Console.WriteLine("Now GS：" + Ip + "-->" + Port + "-->Connection Status：->" + state);
					if (0 == state)
					{
						Console.WriteLine(Ip + "-->" + Port + "-->Connection Success");
						break;
					}
					else
					{
						x++;
						if (x > 30)
						{
							Console.WriteLine(Ip + "-->" + Port + "-->Connection OverTime");
							break;
						}
					}
				}
			}
			catch(System.Exception ex)
			{
				
			}
			return state;
		}
		#endregion
		#endregion
		public static int BanishPlayer  (string serverIP,string userName)
		{
			byte[] result = new byte[255];
			int handle = -1;
			int int_Result = -1;
			try
			{
				
				handle = JW2_CreateDLL();
				string serverIPStard = CommonInfo.JW2_FindDBIP(serverIP,5);
				int state = JW2_ConnectGW(handle,serverIPStard,58118);
				if (state == 0)
				{
					DSNShellUpdate(handle,result);
					StringBuilder S_UserName = new StringBuilder(userName);
					int_Result = DSNShellKickUser(handle,S_UserName);
				}
			}
			catch(System.Exception ex)
			{
				string exc = ex.Message;
			}
			finally
			{
				JW2_DestroyDLL(handle);
			}
			return int_Result;
		}

		#region 踢用户下线
		/// <summary>
		/// 踢用户下线
		/// </summary>
		public static int BANISHPLAYER(string serverIP,string userName,int userbyid,ref string strDesc)
		{
			int  result = -1;
			string sql = null;
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,5);
				string usby = "0";
				result = BanishPlayer(serverIP,userName);
				if(result==0)
				{
					strDesc = lg.JW2API_BANISHPLAYER+userName.ToString()+lg.JW2API_SuccessPleaseWait;
					SqlHelper.insertGMtoolsLog(userbyid,"jw2",serverIP,"JW2_BanishPlayer",lg.JW2API_BANISHPLAYER+userName.ToString()+lg.JW2API_Success);
				}
				else
				{
					strDesc = lg.JW2API_BANISHPLAYER+userName.ToString()+lg.JW2API_Failure;
					SqlHelper.insertGMtoolsLog(userbyid,"jw2",serverIP,"JW2_BanishPlayer",lg.JW2API_BANISHPLAYER+userName.ToString()+lg.JW2API_Failure);
				}
			}
			catch (System.Exception ex)
			{
				strDesc = lg.JW2API_DatebaseConnectError;
				SqlHelper.errLog.WriteLog("ServerIP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 解封用户
		/// <summary>
		/// 解封用户
		/// </summary>
		public static int ACCOUNT_OPEN(string serverIP,int usersn,string userNick,string userName,int userbyid,string Reason,ref string strDesc)
		{
			int  result = -1;
			string sql = null;
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,8);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_ACCOUNT_OPEN' and sql_condition = 'JW2_ACCOUNT_OPEN'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn);
					result = MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2loginDB),sql);
				}
				if(result==1)
				{
					strDesc =lg.JW2API_ACCOUNTOPEN+userName.ToString()+lg.JW2API_PlayerSN+userNick.ToString()+lg.JW2API_Success;
					result = CommonInfo.JW2_UnBanUser(serverIP,userbyid,usersn,userName,Reason);
					SqlHelper.insertGMtoolsLog(userbyid,"JW2",serverIP,"JW2_ACCOUNT_OPEN",lg.JW2API_ACCOUNTOPEN+userName.ToString()+lg.JW2API_Success);
				}
				else
				{
					strDesc =lg.JW2API_ACCOUNTOPEN+userName.ToString()+lg.JW2API_PlayerSN+userNick.ToString()+lg.JW2API_Failure;
					result = CommonInfo.JW2_UnBanUser(serverIP,userbyid,usersn,userName,Reason);
					SqlHelper.insertGMtoolsLog(userbyid,"JW2",serverIP,"JW2_ACCOUNT_OPEN",lg.JW2API_ACCOUNTOPEN+userName.ToString()+lg.JW2API_Failure);
				}
			}
			catch (System.Exception  ex)
			{
				strDesc = lg.JW2API_DatebaseConnectError;
				SqlHelper.errLog.WriteLog("ServerIP"+serverIP+ex.Message);
			}
//			catch (MySqlException  ex)
//			{
//				strDesc = "数据库连接失败，请重新尝试！";
//				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
//			}
			return result;
		}
		#endregion

		#region 封停用户
		/// <summary>
		/// 封停用户
		/// </summary>
		public static int ACCOUNT_CLOSE(string serverIP,int usersn,string userNick,string userName,int userbyid,string Reason,ref string strDesc)
		{
			int  result = -1;
			string sql = null;
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,8);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_ACCOUNT_CLOSE' and sql_condition = 'JW2_ACCOUNT_CLOSE'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn);
					result = MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2loginDB),sql);
				}
				if(result>0)
				{
					strDesc = lg.JW2API_ACCOUNTCLOSE+userName.ToString()+lg.JW2API_PlayerSN+userNick.ToString()+lg.JW2API_Success;
					result = CommonInfo.JW2_BanUser(serverIP,userbyid,usersn,userName,userNick,Reason);
					SqlHelper.insertGMtoolsLog(userbyid,"JW2",serverIP,"JW2_ACCOUNT_CLOSE",lg.JW2API_ACCOUNTCLOSE+userName.ToString()+lg.JW2API_Success);
				}
				else
				{
					strDesc =  lg.JW2API_ACCOUNTCLOSE+userName.ToString()+lg.JW2API_PlayerSN+userNick.ToString()+lg.JW2API_Failure;
					result = CommonInfo.JW2_BanUser(serverIP,userbyid,usersn,userName,userNick,Reason);
					SqlHelper.insertGMtoolsLog(userbyid,"JW2",serverIP,"JW2_ACCOUNT_CLOSE",lg.JW2API_ACCOUNTCLOSE+userName.ToString()+lg.JW2API_Failure);
				}
			}
			catch (MySqlException ex)
			{
				strDesc =  lg.JW2API_DatebaseConnectError;
				SqlHelper.errLog.WriteLog("ServerIP"+serverIP+ex.Message);
				if(ex.Message=="Duplicate entry '"+usersn+"' for key 1")
				{
					result=2;
				}
			}
			return result;
		}
		#endregion

		#region 玩家封停帐号信息查询
		/// <summary>
		/// 玩家封停帐号信息查询
		/// </summary>
		public static DataSet ACCOUNT_BANISHMENT_QUERY(string serverIP,string userid,int type)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,8);
				if(type==0)
				{
					sql = "select sql_statement from sqlexpress where sql_type='JW2_ACCOUNT_BANISHMENT_QUERY_ALL' and sql_condition='JW2_ACCOUNT_BANISHMENT_QUERY_ALL'";
					System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,serverIP);
						result = SqlHelper.ExecuteDataset(sql);
					}
				}
				else
				{
					sql = "select sql_statement from sqlexpress where sql_type='JW2_ACCOUNT_BANISHMENT_QUERY' and sql_condition='JW2_ACCOUNT_BANISHMENT_QUERY'";
					System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,userid,serverIP);
						result = SqlHelper.ExecuteDataset(sql);
					}
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("ServerIP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 公告更新
		/// <summary>
		/// 公告更新
		/// </summary>
		public static int BOARDTASK_UPDATE(string serverIP,int Taskid,string BoardMessage,string BeginTime,string EndTime,int userbyid,int Interval,int Status)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[8]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@JW2_TaskID",SqlDbType.Int),
												   new SqlParameter("@JW2_BoardMessage",SqlDbType.VarChar,256),
												   new SqlParameter("@JW2_begintime",SqlDbType.DateTime),
												   new SqlParameter("@JW2_endTime",SqlDbType.DateTime),
												   new SqlParameter("@JW2_interval",SqlDbType.Int),
												   new SqlParameter("@JW2_Status",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userbyid;
				paramCode[1].Value = Taskid;
				paramCode[2].Value = BoardMessage;
				paramCode[3].Value = BeginTime; 
				paramCode[4].Value = EndTime;
				paramCode[5].Value = Interval;
				paramCode[6].Value = Status;
				paramCode[7].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("JW2_BOARDTASK_UPDATE",paramCode);
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return  result;
		}
		#endregion

		#region 公告查询
		/// <summary>
		/// 公告查询
		/// </summary>
		public static DataSet BOARDTASK_QUERY()
		{
			DataSet ds = null;
			try
			{
				ds = SqlHelper.ExecSPDataSet("JW2_BOARDTASK_QUERY");
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return ds;
		}
		#endregion

		#region 公告添加
		/// <summary>
		/// 公告添加
		/// </summary>
		public static int BOARDTASK_INSERT(string serverIP,string GSserverIp,string BoardMessage,string BeginTime,string EndTime,int userbyid,int Interval)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[8]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@JW2_ServerIP",SqlDbType.VarChar,500),
												   new SqlParameter("@JW2_GSServerIP",SqlDbType.VarChar,500),
												   new SqlParameter("@JW2_BoardMessage",SqlDbType.VarChar,256),
												   new SqlParameter("@JW2_begintime",SqlDbType.DateTime),
												   new SqlParameter("@JW2_endTime",SqlDbType.DateTime),
												   new SqlParameter("@JW2_interval",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userbyid;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = GSserverIp;
				paramCode[3].Value = BoardMessage;
				paramCode[4].Value = BeginTime;
				paramCode[5].Value = EndTime;
				paramCode[6].Value = Interval;
				paramCode[7].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("JW2_BOARDTASK_INSERT",paramCode);


			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return  result;
		}
		#endregion

		#region 查看玩家登陆登出信息
		/// <summary>
		/// 查看玩家登陆登出信息
		/// </summary>
		public static DataSet LOGINOUT_QUERY(string serverIP,int usersn,string login_IP,string BeginTime,string EndTime)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,3);
				if(login_IP=="")
				{
					sql = "select sql_statement from sqlexpress where sql_type='JW2_LOGINOUT_QUERY' and sql_condition='1'";
					System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,usersn,BeginTime,EndTime);
						result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2loginDB),sql);
					}
				}
				else
				{
					sql = "select sql_statement from sqlexpress where sql_type='JW2_LOGINOUT_QUERY' and sql_condition='2'";
					System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,login_IP,BeginTime,EndTime);
						result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2loginDB),sql);
					}
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_LOGINOUT_QUERY_查看玩家"+usersn.ToString()+"登陆登出信息服务器IP"+serverIP+"登陆ip"+login_IP+"开始时间"+BeginTime+"结束时间"+EndTime+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查看玩家婚姻信息
		/// <summary>
		/// 查看玩家婚姻信息
		/// </summary>
		public static DataSet WEDDINGINFO_QUERY(string serverIP,int usersn)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,1);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_WEDDINGINFO_QUERY' and sql_condition='JW2_WEDDINGINFO_QUERY'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2gameDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_WEDDINGINFO_QUERY_查看玩家"+usersn.ToString()+"婚姻信息服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查看玩家婚姻历史
		/// <summary>
		/// 查看玩家婚姻历史
		/// </summary>
		public static DataSet WEDDINGLOG_QUERY(string serverIP,int usersn)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,1);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_WEDDINGLOG_QUERY' and sql_condition='JW2_WEDDINGLOG_QUERY'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2gameDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_WEDDINGLOG_QUERY_查看玩家"+usersn.ToString()+"婚姻历史服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		
		#region 查看玩家结婚信息
		/// <summary>
		/// 查看玩家结婚信息
		/// </summary>
		public static DataSet WEDDINGGROUND_QUERY(string serverIP,int usersn)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,1);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_WEDDINGGROUND_QUERY' and sql_condition='JW2_WEDDINGGROUND_QUERY'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2gameDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_WEDDINGGROUND_QUERY_查看玩家"+usersn.ToString()+"结婚信息服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		
		#region 查看玩家情侣信息
		/// <summary>
		/// 查看玩家情侣信息
		/// </summary>
		public static DataSet COUPLEINFO_QUERY(string serverIP,int usersn)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,1);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_COUPLEINFO_QUERY' and sql_condition='JW2_COUPLEINFO_QUERY'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2gameDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_COUPLEINFO_QUERY_查看玩家"+usersn.ToString()+"情侣信息服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查看玩家情侣历史
		/// <summary>
		/// 查看玩家情侣历史
		/// </summary>
		public static DataSet COUPLELOG_QUERY(string serverIP,int usersn)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,1);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_COUPLELOG_QUERY' and sql_condition='JW2_COUPLELOG_QUERY'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2gameDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_COUPLELOG_QUERY_查看玩家"+usersn.ToString()+"情侣历史服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 修改临时密码
		/// <summary>
		/// 修改临时密码
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static int TmpPassWord_Query(string serverIP,string serverName,int UserByID,int userid,string username,string TmpPwd,ref string strDesc)
		{
			DataSet ds = null;
			int result = -1;
			string RelPwd = null;
			string sql = null;
			MD5Encrypt st = new MD5Encrypt();
			//string sign = st.getMD5ofStr(TmpPwd).ToLower();
			string sign = TmpPwd;
			try
			{
				//查询是否被修改过
				sql = "select sql_statement from sqlexpress where sql_type='JW2_SearchTmpPWD_Staute_Query' and sql_condition = 'JW2_SearchTmpPWD_Staute_Query'";
				System.Data.DataSet ds4 = SqlHelper.ExecuteDataset(sql);
				if(ds4!=null && ds4.Tables[0].Rows.Count>0)
				{	
					sql = ds4.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,serverName,userid);
					System.Data.DataSet ds5 = SqlHelper.ExecuteDataset(sql);
					if(ds5.Tables[0].Rows.Count==0)
					{
						//获取真实密码
						serverIP = CommonInfo.JW2_FindDBIP(serverIP,3);
						sql = "select sql_statement from sqlexpress where sql_type='JW2_GetPassWord_Query' and sql_condition = 'JW2_GetPassWord_Query'";
						System.Data.DataSet ds1 = SqlHelper.ExecuteDataset(sql);
						if(ds1!=null && ds1.Tables[0].Rows.Count>0)
						{	
							sql = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
							sql = string.Format(sql,userid);
							ds = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2loginDB),sql);
							if(ds!=null && ds.Tables[0].Rows.Count>0)
							{
								serverIP = CommonInfo.JW2_FindDBIP(serverIP,8);
								RelPwd = ds.Tables[0].Rows[0].ItemArray[0].ToString();
								//修改密码
								sql = "select sql_statement from sqlexpress where sql_type='JW2_TmpPassWord_Query' and sql_condition = 'JW2_TmpPassWord_Query'";
								System.Data.DataSet ds2 = SqlHelper.ExecuteDataset(sql);
								if(ds2!=null && ds2.Tables[0].Rows.Count>0)
								{	
									sql = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
									sql = string.Format(sql,sign,userid);
									result = MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2loginDB),sql);
								}
								if(result==1)
								{
									//记录真实和临时密码在125上
									sql = "select sql_statement from sqlexpress where sql_type='JW2_InsertTmpPassWord_Query' and sql_condition = 'JW2_InsertTmpPassWord_Query'";
									System.Data.DataSet ds3 = SqlHelper.ExecuteDataset(sql);
									if(ds3!=null && ds3.Tables[0].Rows.Count>0)
									{	
										sql = ds3.Tables[0].Rows[0].ItemArray[0].ToString();
										sql = string.Format(sql,serverName,userid,username,RelPwd,TmpPwd,sign,1);
										result = SqlHelper.ExecCommand(sql);
									}
								}
							}
						}
					}
					else
					{
						result = 2;
					}
				}
				if(result==1)
				{
					strDesc = "修改用户"+username.ToString()+"临时密码成功，请稍等，系统处理中！";
					SqlHelper.insertGMtoolsLog(UserByID,"劲舞团2",serverIP,"JW2_TmpPassWord_Query","修改用户"+username.ToString()+"临时密码，成功(修改临时密码,jw2)");
				}
				else
				{
					strDesc = "修改用户"+username.ToString()+"临时密码失败，确认该玩家是否设置临时密码！";
					SqlHelper.insertGMtoolsLog(UserByID,"劲舞团2",serverIP,"JW2_TmpPassWord_Query","修改用户"+username.ToString()+"临时密码，失败(修改临时密码,jw2)");
				}
			}
			catch (System.Exception ex)
			{
				strDesc = "数据库连接失败，请重新尝试！";
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 恢复临时密码
		/// <summary>
		/// 恢复临时密码
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static int ReTmpPassWord_Query(string serverIP,string serverName,int UserByID,int userid,string username,ref string strDesc)
		{
			DataSet ds = null;
			int result = -1;
			string RelPwd = null;
			string sql = null;
			try
			{
				//获取本地真实密码
				sql = "select sql_statement from sqlexpress where sql_type='JW2_GetRelPassWord_Query' and sql_condition = 'JW2_GetRelPassWord_Query'";
				System.Data.DataSet ds1 = SqlHelper.ExecuteDataset(sql);
				if(ds1!=null && ds1.Tables[0].Rows.Count>0)
				{	
					sql = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,serverName,userid);
					ds = SqlHelper.ExecuteDataset(sql);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						RelPwd = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						//恢复临时密码
						serverIP = CommonInfo.JW2_FindDBIP(serverIP,8);
						sql = "select sql_statement from sqlexpress where sql_type='JW2_TmpPassWord_Query' and sql_condition = 'JW2_TmpPassWord_Query'";
						System.Data.DataSet ds2 = SqlHelper.ExecuteDataset(sql);
						if(ds2!=null && ds2.Tables[0].Rows.Count>0)
						{	
							sql = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
							sql = string.Format(sql,RelPwd,userid);
							result = MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2loginDB),sql);
						}
						if(result==1)
						{
							//更新本地临时密码状态
							sql = "select sql_statement from sqlexpress where sql_type='JW2_UpdateTmpPassWord_Query' and sql_condition = 'JW2_UpdateTmpPassWord_Query'";
							System.Data.DataSet ds3 = SqlHelper.ExecuteDataset(sql);
							if(ds3!=null && ds3.Tables[0].Rows.Count>0)
							{	
								sql = ds3.Tables[0].Rows[0].ItemArray[0].ToString();
								sql = string.Format(sql,serverName,userid,0);
								SqlHelper.ExecCommand(sql);
							}
						}
					}
					else
					{
						result = 2;
					}
				}
				if(result==1)
				{
					strDesc = "恢复用户"+username.ToString()+"临时密码成功，请稍等，系统处理中！";
					SqlHelper.insertGMtoolsLog(UserByID,"劲舞团2",serverIP,"JW2_TmpPassWord_Query","恢复用户"+username.ToString()+"临时密码，成功(恢复临时密码,jw2)");
				}
				else
				{
					strDesc = "恢复用户"+username.ToString()+"临时密码失败，确认该玩家是否设置临时密码！";
					SqlHelper.insertGMtoolsLog(UserByID,"劲舞团2",serverIP,"JW2_TmpPassWord_Query","恢复用户"+username.ToString()+"临时密码，失败(恢复临时密码,jw2)");
				}
			}
			catch (MySqlException ex)
			{
				strDesc = "数据库连接失败，请重新尝试！";
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查询最后一次临时密码
		/// <summary>
		/// 查询最后一次临时密码
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet SearchPassWord_Query(string serverIP,string serverName,int userid,string username)
		{
			DataSet result = null;
			string sql = null;
			try
			{
				sql = "select sql_statement from sqlexpress where sql_type='JW2_SearchPassWord_Query' and sql_condition = 'JW2_SearchPassWord_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,serverName,userid);
					result = SqlHelper.ExecuteDataset(sql);
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_SearchPassWord_Query_查看玩家"+userid.ToString()+"-"+username+"最后一次临时密码服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		

		



	}

}
