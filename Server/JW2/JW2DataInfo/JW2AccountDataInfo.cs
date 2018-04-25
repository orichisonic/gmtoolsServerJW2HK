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
using Common.Logic;
using Common.API;
using MySql.Data.MySqlClient;
using Common.DataInfo;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace GM_Server.JW2DataInfo
{
	/// <summary>
	/// JW2AccountDataInfo 的摘要说明。
	/// </summary>
	public class JW2AccountDataInfo
	{
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
		public static extern int DSNShellConnectGW(int handle,string Ip, int port);
		/// <summary>
		///连接GW（句柄，IP，端口）---Rev
		/// </summary>
		[DllImport("DSNShell.dll")]
		public static extern int DSNShellIsGWConnected(int handle);
		/// <summary>
		///登陆GW（句柄，帐号，密码，版本号）--Send
		/// </summary>
		[DllImport("DSNShell.dll", CharSet = CharSet.Unicode)]
		public static extern int DSNShellLoginGW(int handle, string szAccount, string szPassword, string szVersion);
		/// <summary>
		///登陆GW（句柄，帐号，密码，版本号）--Rev
		/// </summary>
		[DllImport("DSNShell.dll")]
		public static extern int DSNShellLoginGWRet(int handle,byte[] result);
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
		public static extern int DSNShellConnectGS(int handle, string Ip, int port);
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
		public static extern int DSNShellLoginGSRet(int handle, byte[] result);
		#endregion
		#region ?量
		private static byte[] result = new byte[256];
		private static byte[] result_OK = new byte[1024];
		#endregion
		enum DSNSHELL_MSGIDX
		{
			MSGIDX_LOGINGW = 1,
			MSGIDX_SERVERLIST,

			MSGIDX_LOGINGS,
			MSGIDX_CHANNELLIST,
			MSGIDX_ENTERCHANNEL,

			MSGIDX_USERLIST,
			MSGIDX_ROOMLIST,
			MSGIDX_ROOMDETAIL,
			MSGIDX_CHAT,
			MSGIDX_WHISPER,

			MSGIDX_ROOMCREATED,
			MSGIDX_ROOMDESTROYED,
			MSGIDX_USERENTERCHANNEL,
			MSGIDX_USERLEAVECHANNEL,
			MSGIDX_LABA,

			MSGIDX_ROOMSLOTLIST,
		};
		public JW2AccountDataInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region MD5加密
		private static string MD5ToString(String argString) 
		{ 
			MD5 md5 = new MD5CryptoServiceProvider(); 
			byte[] data = System.Text.Encoding.Default.GetBytes(argString); 
			byte[] result = md5.ComputeHash(data); 
			String strReturn = String.Empty; 
			for (int i = 0; i < result.Length; i++) 
				strReturn += result[i].ToString("x").PadLeft(2, '0'); 
			return strReturn; 
		} 
		#endregion

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
				byte[] result_GW = new byte[256];
				//连接LOGIN数据库
				if (2 == DSNShellIsGWConnected(C_Handle))
				{
					if (0 == DSNShellConnectGW(C_Handle, Ip, Port))
					{
						//Console.WriteLine("Begin Connect->GW：" + Ip + "-->" + Port);
					}
				}
				int x = 0;
				//判断LOGIN 连接状态
				while (true)
				{
					System.Threading.Thread.Sleep(500);
					DSNShellUpdate(C_Handle, result_GW);
					System.Threading.Thread.Sleep(500);
					state = DSNShellIsGWConnected(C_Handle);
					Console.WriteLine("Now GW：" + Ip + "-->" + Port + "-->Connection Status：->" + state);
					if (0 == state)
					{
						//Console.WriteLine(Ip + "-->" + Port + "-->GW-->Connection Success");
						break;
					}
					else
					{
						x++;
						if (x > 10)
						{
							Console.WriteLine(Ip + "-->" + Port + "-->GW-->連接超時 OverTime");
							break;
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				Console.WriteLine(Ip + "-->" + Port + "-->GW-->Connection Faile");
			}
			return state;
		}
		#endregion

		#region 登陆GW（句柄，帐号，密码，版本号）--Send/Rev
		/// <summary>
		/// 登陆GW（句柄，帐号，密码，版本号）--Send/Rev
		/// </summary>
		/// <param name="C_Handle"></param>
		/// <returns></returns>
		public static int JW2_LoginGW(int state, int C_Handle, string szAccount, string szPassword, string szVersion)
		{
			try
			{
				if (state == 0)
				{
					state = -1;
					DSNShellLoginGW(C_Handle, szAccount, szPassword, szVersion);
					int x = 0;
					while (true)
					{
						System.Threading.Thread.Sleep(500);
						DSNShellUpdate(C_Handle, result);
						System.Threading.Thread.Sleep(500);
						if (result[0] == (byte)1 && result[1] == (byte)1)
						{
							DSNShellLoginGWRet(C_Handle, result_OK);
							if (result_OK[0] == (byte)0)
							{
								state = 0;
								Console.WriteLine("登陆GW成功");
								break;
							}
							else
							{
								state = -1;
								Console.WriteLine("登陆GW失敗");
								break;
							}
						}
						else
						{
							x++;
							if (x > 10)
							{
								Console.WriteLine("登陆GW OverTime1");
								state = -1;
								break;
							}
						}
					}

				}
			}
			catch (System.Exception ex)
			{
				Console.WriteLine("登陆GW失败->"+ex.Message);
			}
			return state;
		}
		#endregion

		#region 连接GS（句柄，IP，端口）---Send/Rev
		/// <summary>
		/// 连接GS（句柄，IP，端口）---Send/Rev
		/// </summary>
		/// <param name="C_Handle"></param>
		/// <returns></returns>
		public static int JW2_ConnectGS(int C_Handle, string Ip, int Port)
		{
			int state = -1;
			try
			{
				byte[] result_GS = new byte[256];
				//连接LOGIN数据库
				if (2 == DSNShellIsGSConnected(C_Handle))
				{
					if (0 == DSNShellConnectGS(C_Handle, Ip, Port))
					{
						//Console.WriteLine("Begin Connect->GS：" + Ip + "-->" + Port);
					}
				}
				int x = 0;
				//判断LOGIN 连接状态
				while (true)
				{
					DSNShellUpdate(C_Handle, result_GS);
					System.Threading.Thread.Sleep(500);
					state = DSNShellIsGSConnected(C_Handle);
					Console.WriteLine("Now GS：" + Ip + "-->" + Port + "-->Connection Status：->" + state);
					if (0 == state)
					{
						Console.WriteLine(Ip + "-->" + Port + "GS-->Connection Success");
						break;
					}
					else
					{
						x++;
						if (x > 10)
						{
							Console.WriteLine(Ip + "-->" + Port + "GS-->Connection OverTime");
							break;
						}
					}
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(Ip + "-->" + Port + "-->Connection Faile");
			}
			return state;
		}
		#endregion

		#region 登陆GS（句柄，帐号，密码，版本号）--Send/Rev
		/// <summary>
		/// 登陆GS（句柄，帐号，密码，版本号）--Send/Rev
		/// </summary>
		/// <param name="C_Handle"></param>
		/// <returns></returns>
		public static int JW2_LoginGS(int state, int C_Handle, int userSN)
		{
			try
			{
				byte[] result = new byte[256];
				byte[] b = new byte[1024];
				if (state == 0)
				{
					state = -1;
					DSNShellUpdate(C_Handle, result);
					DSNShellLoginGS(C_Handle, userSN);
					int x = 0;
					while (true)
					{                       
						DSNShellUpdate(C_Handle, result);
						System.Threading.Thread.Sleep(500);
						if (result[0] == (byte)1 && result[1] == (byte)3)
						{
							DSNShellLoginGSRet(C_Handle, b);
							if (b[0] == 0)
							{
								state = 0;
								Console.WriteLine("登陆GS 成功");
								break;
							}
							else
							{
								state = -1;
							}
						}
						else
						{
							x++;
							if (x > 10)
							{
								Console.WriteLine("登陆GS OverTime1");
								state = -1;
								break;
							}
						}
					}
				}
			}
			catch (System.Exception)
			{
				Console.WriteLine("登陆GS 失败");
			}
			return state;
		}
		#endregion

		#region 设置当前GS的金钱倍数（句柄，倍数——仅限1和2）
		/// <summary>
		/// 设置当前GS的经验倍数（句柄，倍数——仅限1和2）
		/// </summary>
		/// <param name="C_Handle"></param>
		/// <returns></returns>
		public static string JW2_SetGPointTimes(int state, int C_Handle, byte times, string city, string name,int userByID)
		{
			string get_Result = "";
			try
			{				
				if (state == 0)
				{
					state = -1;
					state = DSNShellSetGPointTimes(C_Handle, times);
					if(state==0)
					{
						SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",city,"JW2_ChangeServerExp_Query","【"+city.ToString()+"】大区，修改GS服务器：【"+name.ToString()+"】，金钱【："+times.ToString()+"】倍，成功");
						get_Result = "【"+city.ToString()+"】大区，修改GS服务器：【"+name.ToString()+"】，金钱【："+times.ToString()+"】倍，成功\n";
						Console.WriteLine(city + "-->" + name + "-->修改雙倍金錢，成功");
					}
					else
					{
						SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",city,"JW2_ChangeServerExp_Query","【"+city.ToString()+"】大区，修改GS服务器：【"+name.ToString()+"】，金钱【："+times.ToString()+"】倍，失敗");
						get_Result = "【"+city.ToString()+"】大区，修改GS服务器：【"+name.ToString()+"】，金钱【："+times.ToString()+"】倍，失敗\n";
						Console.WriteLine(city + "-->" + name + "-->修改雙倍金錢，失敗");
					}
				}
			}
			catch(System.Exception ex)
			{
				SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",city,"JW2_ChangeServerExp_Query","【"+city.ToString()+"】大区，修改GS服务器：【"+name.ToString()+"】，金钱【："+times.ToString()+"】倍，失敗");
				get_Result = "【"+city.ToString()+"】大区，修改GS服务器：【"+name.ToString()+"】，金钱【："+times.ToString()+"】倍，失敗\n";
				Console.WriteLine(city + "-->" + name + "-->修改雙倍金錢，失敗");
			}
			return get_Result;
		}
		#endregion

		#region 设置当前GS的经验倍数（句柄，倍数——仅限1和2）
		/// <summary>
		/// 设置当前GS的经验倍数（句柄，倍数——仅限1和2）
		/// </summary>
		/// <param name="C_Handle"></param>
		/// <returns></returns>
		public static string  JW2_SetExpTimes(int state, int C_Handle, byte times,string city,string name,int userByID)
		{
			string get_Result = "";
			try
			{
				if (state == 0)
				{
					state = -1;
					state = DSNShellSetExpTimes(C_Handle, times);
					if (state == 0)
					{
						SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",city,"JW2_ChangeServerExp_Query","【"+city.ToString()+"】大区，修改GS服务器：【"+name.ToString()+"】，??【："+times.ToString()+"】倍，成功");
						get_Result = "【"+city.ToString()+"】大区，修改GS服务器：【"+name.ToString()+"】，??【："+times.ToString()+"】倍，成功\n";
						Console.WriteLine(city + "-->" + name + "-->修改雙倍??，成功");
					}
					else
					{
						SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",city,"JW2_ChangeServerExp_Query","【"+city.ToString()+"】大区，修改GS服务器：【"+name.ToString()+"】，??【："+times.ToString()+"】倍，失敗");
						get_Result = "【"+city.ToString()+"】大区，修改GS服务器：【"+name.ToString()+"】，??【："+times.ToString()+"】倍，失敗\n";
						Console.WriteLine(city + "-->" + name + "-->修改雙倍??，失敗");
					}
				}
			}
			catch (System.Exception ex)
			{
				SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",city,"JW2_ChangeServerExp_Query","【"+city.ToString()+"】大区，修改GS服务器：【"+name.ToString()+"】，??【："+times.ToString()+"】倍，失敗");
				get_Result = "【"+city.ToString()+"】大区，修改GS服务器：【"+name.ToString()+"】，??【："+times.ToString()+"】倍，失敗\n";
				Console.WriteLine(city + "-->" + name + "-->修改雙倍??，失敗");
			}
			return get_Result;
		}
		#endregion
		#endregion

		#region 修改雙倍狀態
		/// <summary>
		/// GM修改雙倍狀態
		/// </summary>
		public static string  ChangeServerExp_Query(string serverIP,int iExp,int iMoney,int userByID,string serverName,int type)
		{

			int handle = -1;
			int state = -1;
			string get_Result = "";
			serverIP = CommonInfo.JW2_FindDBIP(serverIP,5);
			string userPwd = MD5EncryptAPI.MDString(SqlHelper.jw2gwUserPwd);
			try
			{
				string[] serverList = serverName.Split('|');
				for(int i = 0;i<serverList.Length-1;i++)
				{
					string[] GSserverList = serverList[i].Split(',');
					string GSserverName = GSserverList[0].ToString();
					string GSserverIP = GSserverList[1].ToString();
					int GSserverNo = int.Parse(GSserverList[2].ToString());
					int port = int.Parse(GSserverList[3].ToString());
					string city = CommonInfo.serverIP_Query(serverIP);
					handle = JW2_CreateDLL();
					state = JW2_ConnectGW(handle, serverIP, 58118);
					if (state == 0)
					{
						state = JW2_LoginGW(state, handle, SqlHelper.jw2gwUser, userPwd, SqlHelper.jw2gwVersion);
						if (state == 0)
						{

							state = JW2_ConnectGS(handle, GSserverIP, port);
							if (state == 0)
							{
								state = JW2_LoginGS(state, handle, 7044699);
								if (state == 0)
								{
									if(type==0)
										get_Result += JW2_SetGPointTimes(state, handle, (byte)iMoney,city ,GSserverName,userByID);
									else if(type==1)
										get_Result = JW2_SetExpTimes(state, handle, (byte)iExp,city,GSserverName,userByID);
									else if(type==2)
									{
										get_Result += JW2_SetGPointTimes(state, handle, (byte)iMoney,city ,GSserverName,userByID);
										get_Result += JW2_SetExpTimes(state, handle, (byte)iExp,  city,GSserverName,userByID);
									}        
								}
								else
								{
									get_Result += "【"+city.ToString()+"】大区，登?GS服务器：【"+GSserverName.ToString()+"】，失敗\n";
								}
							}
							else
							{
								get_Result += "【"+city.ToString()+"】大区，連接GS服务器：【"+GSserverName.ToString()+"】，失敗\n";
							}
						}
						else
						{
							get_Result += "【"+city.ToString()+"】大区，登?，失敗\n";
						}
					}
					else
					{
						get_Result += "【"+city.ToString()+"】大区，連接，失敗\n";
					}
				}
			}
			catch (System.Exception ex)
			{
				get_Result +=  "数据库连接失败，请重新尝试！";
				Console.WriteLine("789"+ex.Message);
			}
			JW2_DestroyDLL(handle);
			return get_Result;
		}
		#endregion

		#region 查看玩家资料
		/// <summary>
		/// 查看玩家资料
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet ACCOUNT_QUERY(string serverIP,string strname,ref string strDesc)
		{
			DataSet result = null;
			string sql = "";
			int zone = 0;
			string serverName = "";
			try
			{
				serverName = CommonInfo.JW2_FindDBName(serverIP);
//				zone = CommonInfo.JW2_GetZone_Query(13,serverName);
//				if(serverName=="华北一区"||serverName=="华东一区"||serverName=="华南一区"||serverName=="西南一区")
//				{
					serverIP = CommonInfo.JW2_FindDBIP(serverIP,1);

					sql = "select sql_statement from sqlexpress where sql_type='JW2_ACCOUNT_QUERYBYACCOUNT_bak' and sql_condition='JW2_ACCOUNT_QUERYBYACCOUNT_bak'";
					System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,strname);
						result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2gameDB),sql);
					}
//				}
//				else
//				{
//					sql = "select sql_statement from sqlexpress where sql_type='JW2_ACCOUNT_QUERYBYACCOUNT_ORACLE' and sql_condition='JW2_ACCOUNT_QUERYBYACCOUNT_ORACLE'";
//					System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
//					if(ds!=null && ds.Tables[0].Rows.Count>0)
//					{
//						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
//						sql = string.Format(sql,strname,zone);
//						result = CommonInfo.RunOracle(sql,SqlHelper.oracleData,SqlHelper.oracleUser,SqlHelper.oraclePwd);
//					}
//				}
			}
			catch(MySqlException ex)
			{
				strDesc = "数据库连接失败";
				SqlHelper.errLog.WriteLog("浏览JW2_ACCOUNT_QUERY_玩家"+strname+"信息服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		/// <summary>
		/// 查看玩家资料
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">昵称</param>
		/// <returns></returns>
		public static DataSet USERNICK_QUERY(string serverIP,string strname)
		{
			DataSet result = null;
			string sql = "";
			int zone = 0;
			string serverName = "";
			try
			{
				serverName = CommonInfo.JW2_FindDBName(serverIP);
				zone = CommonInfo.JW2_GetZone_Query(13,serverName);
				if(serverName=="华北一区"||serverName=="华东一区"||serverName=="华南一区"||serverName=="西南一区")
				{
					serverIP = CommonInfo.JW2_FindDBIP(serverIP,1);
					sql = "select sql_statement from sqlexpress where sql_type='JW2_ACCOUNT_QUERYBYNICKNAME_Bak' and sql_condition='JW2_ACCOUNT_QUERYBYNICKNAME_Bak'";
					System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,strname);
						result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2gameDB),sql);
					}
				}
				else
				{
					sql = "select sql_statement from sqlexpress where sql_type='JW2_ACCOUNT_QUERYBYNICKNAME_ORACLE' and sql_condition='JW2_ACCOUNT_QUERYBYNICKNAME_ORACLE'";
					System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,strname,zone);
						result = CommonInfo.RunOracle(sql,SqlHelper.oracleData,SqlHelper.oracleUser,SqlHelper.oraclePwd);
					}
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_USERNICK_QUERY_玩家昵称"+strname+"信息服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查询点数和虚拟币
		/// <summary>
		/// 查询点数和虚拟币
		/// </summary>
		public static DataSet DUMMONEY_QUERY(string serverIP,int usersn)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,1);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_DUMMONEY_QUERY' and sql_condition='JW2_DUMMONEY_QUERY'";
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
				SqlHelper.errLog.WriteLog("浏览JW2_DUMMONEY_QUERY_玩家"+usersn.ToString()+"点数和虚拟币服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 修改等级
		/// <summary>
		/// 修改等级
		/// </summary>
		public static int MODIFYLEVEL_QUERY(string serverIP,int usersn,int iLevel,int userByID,string UserName,ref string strDesc)
		{
			int  result = -1;
			string sql = null;
			try
			{
				//修改等级1
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,7);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_MODIFYLEVEL_QUERY1' and sql_condition = 'JW2_MODIFYLEVEL_QUERY1'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,iLevel,usersn);
					result = MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2gameDB),sql);
				}
				//修改等级2
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,9);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_MODIFYLEVEL_QUERY2' and sql_condition = 'JW2_MODIFYLEVEL_QUERY2'";
				System.Data.DataSet ds1 = SqlHelper.ExecuteDataset(sql);
				if(ds1!=null && ds1.Tables[0].Rows.Count>0)
				{	
					sql = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,iLevel,usersn);
					result += MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2messengerDB),sql);
				}
				//查询等级对应的经验值
				float iExp = CommonInfo.JW2_LevelToExp(iLevel);
				//修改经验
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,7);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_MODIFYEXP_QUERY' and sql_condition = 'JW2_MODIFYEXP_QUERY'";
				System.Data.DataSet ds2 = SqlHelper.ExecuteDataset(sql);
				if(ds2!=null && ds2.Tables[0].Rows.Count>0)
				{	
					sql = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,iExp,usersn);
					result += MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2gameDB),sql);
				}
				if(result==3)
				{
					strDesc ="修改玩家："+UserName.ToString()+"，等级："+iLevel.ToString()+"，经验："+iExp.ToString()+"，成功，请稍等，系统处理中！";
					SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_MODIFYLEVEL_QUERY","修改玩家："+UserName.ToString()+"，等级："+iLevel.ToString()+"，经验："+iExp.ToString()+"，成功(修改等级,jw2)");
				}
				else
				{
					strDesc ="修改玩家："+UserName.ToString()+"，等级："+iLevel.ToString()+"，经验："+iExp.ToString()+"，失败，请确认该用户是否存在";
					SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_MODIFYLEVEL_QUERY","修改玩家："+UserName.ToString()+"，等级："+iLevel.ToString()+"，经验："+iExp.ToString()+"，失败(修改等级,jw2)");
				}
			}
			catch (System.Exception ex)
			{
				strDesc = "数据库连接失败，请重新尝试！";
				SqlHelper.errLog.WriteLog("浏览JW2_MODIFYLEVEL_QUERY_玩家"+usersn.ToString()+"-"+UserName+"修改等级服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 修改经验
		/// <summary>
		/// 修改经验
		/// </summary>
		public static int MODIFYEXP_QUERY(string serverIP,int usersn,float iExp,int userByID,string UserName,ref string strDesc)
		{
			int  result = -1;
			string sql = null;
			try
			{
				//查询等级对应的经验值
				long iLevel = CommonInfo.JW2_ExpToLevel(iExp);
				//修改等级1
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,7);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_MODIFYLEVEL_QUERY1' and sql_condition = 'JW2_MODIFYLEVEL_QUERY1'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,iLevel,usersn);
					result = MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2gameDB),sql);
				}
				//修改等级2
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,9);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_MODIFYLEVEL_QUERY2' and sql_condition = 'JW2_MODIFYLEVEL_QUERY2'";
				System.Data.DataSet ds1 = SqlHelper.ExecuteDataset(sql);
				if(ds1!=null && ds1.Tables[0].Rows.Count>0)
				{	
					sql = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,iLevel,usersn);
					result += MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2messengerDB),sql);
				}
				
				//修改经验
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,7);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_MODIFYEXP_QUERY' and sql_condition = 'JW2_MODIFYEXP_QUERY'";
				System.Data.DataSet ds2 = SqlHelper.ExecuteDataset(sql);
				if(ds2!=null && ds2.Tables[0].Rows.Count>0)
				{	
					sql = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,iExp,usersn);
					result += MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2gameDB),sql);
				}
				if(result==3)
				{
					strDesc ="修改玩家："+UserName.ToString()+"，等级："+iLevel.ToString()+"，经验："+iExp.ToString()+"，成功，请稍等，系统处理中！";
					SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_MODIFYEXP_QUERY","修改玩家："+UserName.ToString()+"，等级："+iLevel.ToString()+"，经验："+iExp.ToString()+"，成功(修改经验,jw2)");
				}
				else
				{
					strDesc ="修改玩家："+UserName.ToString()+"，等级："+iLevel.ToString()+"，经验："+iExp.ToString()+"，失败，请确认该角色是否存在！";
					SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_MODIFYEXP_QUERY","修改玩家："+UserName.ToString()+"，等级："+iLevel.ToString()+"，经验："+iExp.ToString()+"，失败(修改经验,jw2)");
				}
			}
			catch (System.Exception  ex)
			{
				strDesc = "数据库连接失败，请重新尝试！";
				SqlHelper.errLog.WriteLog("浏览JW2_MODIFYEXP_QUERY_玩家"+usersn.ToString()+"-"+UserName+"修改经验服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 修改金钱
		/// <summary>
		/// 修改金钱
		/// </summary>
		public static int MODIFY_MONEY(string serverIP,int usersn,int iMoney,int userByID,string UserName,ref string strDesc)
		{
			int  result = -1;
			string sql = null;
			try
			{
				//修改金钱
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,7);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_MODIFY_MONEY' and sql_condition = 'JW2_MODIFY_MONEY'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,iMoney,usersn);
					result = MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2gameDB),sql);
				}
				if(result==1)
				{
					strDesc ="修改玩家："+UserName.ToString()+"，金钱："+iMoney.ToString()+"，成功，请稍等，系统处理中！";
					SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_MODIFY_MONEY","修改玩家："+UserName.ToString()+"，金钱："+iMoney.ToString()+"，成功(修改金钱,jw2)");
				}
				else
				{
					strDesc ="修改玩家："+UserName.ToString()+"，金钱："+iMoney.ToString()+"，失败，请确认该角色是否存在！";
					SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_MODIFY_MONEY","修改玩家："+UserName.ToString()+"，金钱："+iMoney.ToString()+"，失败(修改金钱,jw2)");
				}
			}
			catch (System.Exception  ex)
			{
				strDesc = "数据库连接失败，请重新尝试！";
				SqlHelper.errLog.WriteLog("浏览JW2_MODIFY_MONEY_玩家"+usersn.ToString()+"-"+UserName+"修改金钱服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查看玩家结婚证书
		/// <summary>
		/// 查看玩家结婚证书
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="userSN">用户ID</param>
		/// <returns></returns>
		public static DataSet Wedding_Paper(string serverIP,int userSN)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,1);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_Wedding_Paper' and sql_condition='JW2_Wedding_Paper'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userSN);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2gameDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_Wedding_Paper_查看玩家"+userSN.ToString()+"结婚证书服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		
		#region 查看玩家情侣派对卡
		/// <summary>
		/// 查看玩家情侣派对卡
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="userSN">用户ID</param>
		/// <returns></returns>
		public static DataSet CoupleParty_Card(string serverIP,int userSN)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,1);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_CoupleParty_Card' and sql_condition='JW2_CoupleParty_Card'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userSN);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2gameDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_CoupleParty_Card_查看玩家"+userSN.ToString()+"情侣派对卡服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region GM狀態修改
		/// <summary>
		/// GM狀態修改
		/// </summary>
		public static int GM_Update(string serverIP,int usersn,int type,int userByID,string userName,ref string strDesc)
		{
			int  result = -1;
			string sql = null;
			string typeName = "";
			try
			{
				switch(type)
				{
					case 0:
						typeName="管理员";
						break;
					case 1:
						typeName="普通员";
						break;
					case 2:
						typeName="观察员";
						break;

				}
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,1);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_GM_Update' and sql_condition = 'JW2_GM_Update'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn,type);
					result = MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2gameDB),sql);
				}
				if(result==1)
				{
					strDesc ="修改GM玩家："+userName.ToString()+"，狀態："+typeName.ToString()+"，成功，请稍等，系统处理中！";
					SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_GM_Update","修改GM玩家："+userName.ToString()+"，狀態："+typeName.ToString()+"，成功(GM状态修改,jw2)");
				}
				else
				{
					strDesc ="修改GM玩家："+userName.ToString()+"，狀態："+typeName.ToString()+"，失败，确认该用户是否存在！";
					SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_GM_Update","修改GM玩家："+userName.ToString()+"，狀態："+typeName.ToString()+"，失败(GM状态修改,jw2)");
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

		#region 活动卡查询
		/// <summary>
		/// 活动卡查询
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static ArrayList Act_Card_Query(string userName,string card,ref string strDesc)
		{
			string getUser = null;
			string sign = null;
			string parameter ="";
			XmlDocument xmlfile = new XmlDocument();
			getUser =userName;
			parameter = getUser+card;
			MD5Encrypt md5 = new MD5Encrypt();
			sign = md5.getMD5ofStr(parameter+"|T4pa3A.jw2").ToLower();
			try   
			{
				System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=10");
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					string serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					string url = null;
					url= "http://"+serverIP+"/PayCenter/jw2actcard.php";
					HttpWebRequest  request  = (HttpWebRequest)
						WebRequest.Create(url);
					request.ContentType="application/x-www-form-urlencoded";
					request.KeepAlive=false; 
					request.Method="POST";
					//参数POST到商城的接口
					Stream writer = request.GetRequestStream(); 
					string postData="getuser="+getUser+"&card="+card+"&sign="+sign+"&encoding=UTF-8";  
					ASCIIEncoding encoder = new ASCIIEncoding();
					byte[] ByteArray = encoder.GetBytes(postData);
					writer.Write(ByteArray,0,postData.Length);
					writer.Close();
					//得到商城接口的回应
					WebResponse
						resp = request.GetResponse();
					StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
					//Console.WriteLine(sr.ReadToEnd().Trim());
					xmlfile.Load(sr);
					XmlNode descNodes = xmlfile.SelectSingleNode("you9/status");
					strDesc = descNodes.InnerText;
					int i=0;
					if(strDesc!=null && strDesc.Equals("RESULT_0"))
					{
						strDesc = "查询成功";                        
					}
					else if(strDesc!=null && strDesc.Equals("RESULT_1"))
					{
						strDesc = "参数错误";
					}
					else if(strDesc!=null && strDesc.Equals("RESULT_2"))
					{
						strDesc = "查询密钥错误";
					}
					else if(strDesc!=null && strDesc.Equals("RESULT_3"))
					{
						strDesc = "输入参数查询无结果";
					}
					else
					{
						strDesc = "异常";
					}
					XmlNode nodes ;
					System.Collections.ArrayList rowList = new System.Collections.ArrayList();
					while( (nodes=xmlfile.SelectSingleNode("you9/row"+i))!=null)
					{
						System.Collections.ArrayList colList = new System.Collections.ArrayList();
						foreach(XmlNode xmlnodes in nodes.ChildNodes)
						{
							colList.Add(xmlnodes.InnerText);
						}
						i++;
						rowList.Add(colList);
					}
					sr.Close();
					return rowList;
				}
			}
			catch (SqlException ex)
			{
				strDesc = "异常";
				SqlHelper.errLog.WriteLog("浏览JW2_Act_Card_Query_查看玩家"+userName+"活动卡查询"+ex.Message);
			}
			return null;
		}
		#endregion

		#region 获得服务器GS列表
		/// <summary>
		/// 获得服务器GS列表
		/// </summary>
		public static DataSet GSSvererList_Query(string serverIP)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,1);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_GSServerList_Query' and sql_condition='JW2_GSServerList_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2gameDB),sql);
				}
			}
			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}
		#endregion

		#region 查看玩家活跃度
		/// <summary>
		/// 查看玩家活跃度
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet ACTIVEPOINT_ACCOUNT_QUERY(string serverIP,int intusersn,string BeginTime,string EndTime)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,1);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_ACTIVEPOINT_QUERYBYACCOUNT' and sql_condition='JW2_ACTIVEPOINT_QUERYBYACCOUNT'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,intusersn,BeginTime,EndTime);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2gameDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_ACTIVEPOINT_ACCOUNT_QUERY_查看玩家"+intusersn.ToString()+"活跃度服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		/// <summary>
		/// 查看玩家活跃度
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">昵称</param>
		/// <returns></returns>
		public static DataSet ACTIVEPOINT_USERNICK_QUERY(string serverIP,string strname,string BeginTime,string EndTime)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,1);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_ACTIVEPOINT_QUERYBYNICKNAME' and sql_condition='JW2_ACTIVEPOINT_QUERYBYNICKNAME'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,strname,BeginTime,EndTime);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2gameDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_ACTIVEPOINT_USERNICK_QUERY_查看玩家"+strname+"活跃度服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		

	}
}
