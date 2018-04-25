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
using Common.DataInfo;
using Common.API;
using MySql.Data.MySqlClient;
namespace GM_Server.SDOnlineDataInfo
{
	/// <summary>
	/// SDLogDataInfo 的摘要说明。
	/// </summary>
	public class SDLogDataInfo
	{
		public SDLogDataInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region 玩家登陆信息
		/// <summary>
		/// 玩家登陆信息
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet UserLoginfo_Query(string serverIP,int userid,DateTime startTime,DateTime endTime)
		{
			DataSet result = null;
			string sql = null;
			string eTime = null;
			try
			{
				string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
				int i=0;
				if(DateTime.Compare(endTime,DateTime.Now.AddDays(1))>0)
					eTime=DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
				else
					eTime=endTime.AddDays(1).ToString("yyyy-MM-dd");
				while(!startTime.AddDays(i).ToString("yyyy-MM-dd").Equals(eTime))
				{
					if(i<45)
					{
						int iTime = Convert.ToInt32(startTime.AddDays(i).ToString("yyyyMMdd"));
						sql = "select sql_statement from sqlexpress where sql_type='SD_UserLoginfo_Query' and sql_condition = 'SD_UserLoginfo_Query'";
						System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
						if(ds!=null && ds.Tables[0].Rows.Count>0)
						{
							sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
							sql = string.Format(sql,userid,startTime,endTime,iTime);
							if(i==0)
							{
								result= SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
							}
							else
							{
								result.Merge(SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql));
							}
						}
					}
					else
					{
						break;
					}
					i++;
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 玩家礼物\邮件查询
		/// <summary>
		/// 玩家礼物\邮件查询
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet UserGrift_Query(string serverIP,int userid,DateTime BeginTime,DateTime EndTime,int type)
		{
			DataSet result = null;
			string sql = null;
			try
			{
				if(type==1)
					sql = "select sql_statement from sqlexpress where sql_type='SD_FromUserGrift_Query' and sql_condition = 'SD_FromUserGrift_Query'";
				else if(type==2)
					sql = "select sql_statement from sqlexpress where sql_type='SD_ToUserGrift_Query' and sql_condition = 'SD_ToUserGrift_Query'";
				else if(type==3)
					sql = "select sql_statement from sqlexpress where sql_type='SD_FromUserMail_Query' and sql_condition = 'SD_FromUserMail_Query'";
				else 
					sql = "select sql_statement from sqlexpress where sql_type='SD_ToUserMail_Query' and sql_condition = 'SD_ToUserMail_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userid,BeginTime,EndTime);
					string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
					result = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 发送人礼物信息查询
		/// <summary>
		/// 发送人礼物信息查询
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet Grift_FromUser_Query(string serverIP,int toidx,int fromidx,DateTime Time)
		{
			DataSet result = null;
			string sql = null;
			try
			{
				int iTime = Convert.ToInt32(Time.ToString("yyyyMMdd"));
				string sTime = Time.ToString("H:mm");
				sql = "select sql_statement from sqlexpress where sql_type='SD_Grift_FromUser_Query' and sql_condition = 'SD_Grift_FromUser_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,iTime,fromidx,toidx,sTime);
					string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
					result = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 接收人礼物信息查询
		/// <summary>
		/// 接收人礼物信息查询
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet Grift_ToUser_Query(string serverIP,int toidx,string item,DateTime Time,int s_id)
		{
			DataSet result = null;
			string sql = null;
			try
			{
				string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
				int i=0;
				while(!Time.AddDays(i).ToString("yyyy-MM-dd").Equals(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")))
				{
					if(i<45)
					{
						string[] iItem = item.Split('|');
						for(int j = 0;j<iItem.Length-1;j++)
						{
							int itemID = Convert.ToInt32(iItem[j].ToString());
							if(itemID!=0)
							{
								int iTime = Convert.ToInt32(Time.AddDays(i).ToString("yyyyMMdd"));
								sql = "select sql_statement from sqlexpress where sql_type='SD_Grift_ToUser_Query' and sql_condition = 'SD_Grift_ToUser_Query'";
								System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
								if(ds!=null && ds.Tables[0].Rows.Count>0)
								{
									sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
									sql = string.Format(sql,iTime,toidx,itemID,s_id,Time);
									if(i==0)
									{
										result= SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
									}
									else
									{
										result.Merge(SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql));
									}
								}
							}
						}
					}
					else
					{
						break;
					}
					i++;
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		
		#region 封停用户
		/// <summary>
		/// 封停用户
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static int BanUser_Ban(string serverIP,string serverName,int UserByID,int userid,string username,string content,DateTime banTime)
		{
			int result = -1;
			string sql = null;
			try
			{
				sql = "select sql_statement from sqlexpress where sql_type='SD_BanUser_Ban' and sql_condition = 'SD_BanUser_Ban'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,banTime,userid);
					string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
					result = SqlHelper.ExecCommand(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql,true);
				}
				if(result==1)
				{
					sql = "select sql_statement from sqlexpress where sql_type='SD_InsertBan_Query' and sql_condition = 'SD_InsertBan_Query'";
					System.Data.DataSet ds1 = SqlHelper.ExecuteDataset(sql);
					if(ds1!=null && ds1.Tables[0].Rows.Count>0)
					{	
						sql = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,userid,username,content,banTime,serverName);
						result = SqlHelper.ExecCommand(sql);
					}
				}
				if(result==1)
				{
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_BanUser_Ban","封停用户"+username.ToString()+"，到"+banTime.ToString()+"，成功");
				}
				else
				{
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_BanUser_Ban","封停用户"+username.ToString()+"，到"+banTime.ToString()+"，失败");
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 解封用户
		/// <summary>
		/// 解封用户
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static int BanUser_UnBan(string serverIP,string serverName,int UserByID,int userid,string username)
		{
			int result = -1;
			string sql = null;
			try
			{
				sql = "select sql_statement from sqlexpress where sql_type='SD_BanUser_UnBan' and sql_condition = 'SD_BanUser_UnBan'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userid);
					string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
					result = SqlHelper.ExecCommand(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql,true);
				}
				if(result==1)
				{
					sql = "select sql_statement from sqlexpress where sql_type='SD_UpdateBanUser_Query' and sql_condition = 'SD_UpdateBanUser_Query'";
					System.Data.DataSet ds1 = SqlHelper.ExecuteDataset(sql);
					if(ds1!=null && ds1.Tables[0].Rows.Count>0)
					{	
						sql = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,userid,serverName);
						SqlHelper.ExecCommand(sql);
					}
					

					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_BanUser_UnBan","解封用户"+username.ToString()+"，成功");
				}
				else
				{
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_BanUser_UnBan","解封用户"+username.ToString()+"，失败");
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 封停用户查询
		/// <summary>
		/// 封停用户查询
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet BanUser_Query(string serverIP,string serverName,int type,string userid)
		{
			DataSet result = null;
			string sql = null;
			try
			{
				sql = "select sql_statement from sqlexpress where sql_type='SD_BanUser_Query' and sql_condition = '"+type+"'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					if(type==0)
						sql = string.Format(sql,serverName);
					else
						sql = string.Format(sql,serverName,userid);
					result = SqlHelper.ExecuteDataset(sql);
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
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
		public static int TmpPassWord_Query(string serverIP,string serverName,int UserByID,int userid,string username,string TmpPwd)
		{
			DataSet ds = null;
			int result = -1;
			string RelPwd = null;
			string sql = null;
			MD5Encrypt st = new MD5Encrypt();
			string sign = st.getMD5ofStr(TmpPwd).ToLower();
			try
			{

				//查询是否被修改过
				string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
				sql = "select sql_statement from sqlexpress where sql_type='SD_SearchTmpPWD_Staute_Query' and sql_condition = 'SD_SearchTmpPWD_Staute_Query'";
				System.Data.DataSet ds4 = SqlHelper.ExecuteDataset(sql);
				if(ds4!=null && ds4.Tables[0].Rows.Count>0)
				{	
					sql = ds4.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,serverName,userid);
					System.Data.DataSet ds5 = SqlHelper.ExecuteDataset(sql);
					if(ds5.Tables[0].Rows.Count==0)
					{
						//获取真实密码
						sql = "select sql_statement from sqlexpress where sql_type='SD_GetPassWord_Query' and sql_condition = 'SD_GetPassWord_Query'";
						System.Data.DataSet ds1 = SqlHelper.ExecuteDataset(sql);
						if(ds1!=null && ds1.Tables[0].Rows.Count>0)
						{	
							sql = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
							sql = string.Format(sql,userid);
							ds = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
							if(ds!=null && ds.Tables[0].Rows.Count>0)
							{
								RelPwd = ds.Tables[0].Rows[0].ItemArray[0].ToString();
								//修改密码
								sql = "select sql_statement from sqlexpress where sql_type='SD_TmpPassWord_Query' and sql_condition = 'SD_TmpPassWord_Query'";
								System.Data.DataSet ds2 = SqlHelper.ExecuteDataset(sql);
								if(ds2!=null && ds2.Tables[0].Rows.Count>0)
								{	
									sql = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
									sql = string.Format(sql,userid,sign);
									result = SqlHelper.ExecCommand(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql,true);
								}
								if(result==1)
								{
									//记录真实和临时密码在125上
									sql = "select sql_statement from sqlexpress where sql_type='SD_InsertTmpPassWord_Query' and sql_condition = 'SD_InsertTmpPassWord_Query'";
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
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"TmpPassWord_Query","修改用户"+username.ToString()+"临时密码，成功");
				}
				else
				{
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"TmpPassWord_Query","修改用户"+username.ToString()+"临时密码，失败");
				}
			}
			catch (MySqlException ex)
			{
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
		public static int ReTmpPassWord_Query(string serverIP,string serverName,int UserByID,int userid,string username)
		{
			DataSet ds = null;
			int result = -1;
			string RelPwd = null;
			string sql = null;
			try
			{
				string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
				//获取本地真实密码
				sql = "select sql_statement from sqlexpress where sql_type='SD_GetRelPassWord_Query' and sql_condition = 'SD_GetRelPassWord_Query'";
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
						sql = "select sql_statement from sqlexpress where sql_type='SD_TmpPassWord_Query' and sql_condition = 'SD_TmpPassWord_Query'";
						System.Data.DataSet ds2 = SqlHelper.ExecuteDataset(sql);
						if(ds2!=null && ds2.Tables[0].Rows.Count>0)
						{	
							sql = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
							sql = string.Format(sql,userid,RelPwd);
							result = SqlHelper.ExecCommand(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql,true);
						}
						if(result==1)
						{
							//更新本地临时密码状态
							sql = "select sql_statement from sqlexpress where sql_type='SD_UpdateTmpPassWord_Query' and sql_condition = 'SD_UpdateTmpPassWord_Query'";
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
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"TmpPassWord_Query","恢复用户"+username.ToString()+"临时密码，成功");
				}
				else
				{
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"TmpPassWord_Query","恢复用户"+username.ToString()+"临时密码，失败");
				}
			}
			catch (MySqlException ex)
			{
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
				string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
				sql = "select sql_statement from sqlexpress where sql_type='SD_SearchPassWord_Query' and sql_condition = 'SD_SearchPassWord_Query'";
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
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 修改玩家等级
		/// <summary>
		/// 修改玩家等级
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static int UpdateExp_Query(string serverIP,int UserByID,int userid,string username,int level)
		{
			int result = -1;
			string sql = null;
			try
			{
				string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
				sql = "select sql_statement from sqlexpress where sql_type='SD_UpdateExp_Query' and sql_condition = 'SD_UpdateExp_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userid,level);
					result = SqlHelper.ExecCommand(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql,true);
				}
				if(result==1)
				{
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_UpdateExp_Query","修改玩家"+username.ToString()+"，等级"+level.ToString()+"，成功");
				}
				else
				{
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_UpdateExp_Query","修改用户"+username.ToString()+"，等级"+level.ToString()+"，失败");
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 修改玩家机体等级
		/// <summary>
		/// 修改玩家机体等级
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static string UpdateUnitsExp_Query(string serverIP,int UserByID,int userid,string username,int level,int CustomLvMax,int UnitNumber)
		{
			DataSet result = null;
			string str = null;
			string sql = null;
			try
			{
				string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
				sql = "select sql_statement from sqlexpress where sql_type='SD_UpdateUnit_Query' and sql_condition = 'SD_UpdateUnit_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userid,UnitNumber,level,CustomLvMax);
					sql = sql.Replace("\t","");
					result = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
					str = result.Tables[0].Rows[0].ItemArray[0].ToString();
					
				}
				SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_UpdateUnitsExp_Query",str);
				
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return str;
		}
		#endregion

		#region 添加道具
		/// <summary>
		/// 添加道具
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static int UserAdditem_Add(string serverIP,int UserByID,int userid,string username,string item,string sendUser,string content)
		{
			string[] itemList = item.Split('|');
			int itemID = 0;
			int itemNum = 0;
			int strItemID = 0;
			string itemName = null;
			int nCategoryNumber1 = 0;
			int	nCategoryNumber2 = 0;
			int nCategoryNumber3 = 0;
			int nCategoryNumber4 = 0;
			int nValidity1 = 0;
			int nValidity2 = 0;
			int nValidity3 = 0;
			int nValidity4 = 0;
			int nItemtblidx1 = 0;
			int nItemtblidx2 = 0;
			int nItemtblidx3 = 0;
			int nItemtblidx4 = 0;
			int result = -1;
			string sql = null;
			try
			{
				string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
				for(int i = 0;i<itemList.Length-1;i++)
				{
					result = -1;
					itemID = int.Parse(itemList[i].Split(' ')[0].ToString());
					itemNum = int.Parse(itemList[i].Split(' ')[1].ToString());
					itemName = itemList[i].Split(' ')[2].ToString();

					sql = "select sql_statement from sqlexpress where sql_type='SD_GetItemBillingCode' and sql_condition = 'SD_GetItemBillingCode'";
					System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{	
						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,itemID);
						DataSet ds1= SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
						switch(ds1.Tables[0].Rows.Count)
						{
							case 1:
								nItemtblidx1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[0].ToString());
								strItemID = int.Parse(ds1.Tables[0].Rows[0].ItemArray[1].ToString());
								nValidity1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[3].ToString());
								nCategoryNumber1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[5].ToString());
								break;
							case 2:
								nItemtblidx1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[0].ToString());
								nItemtblidx2 = int.Parse(ds1.Tables[0].Rows[1].ItemArray[0].ToString());
								strItemID = int.Parse(ds1.Tables[0].Rows[0].ItemArray[1].ToString());
								nValidity1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[3].ToString());
								nValidity2 = int.Parse(ds1.Tables[0].Rows[1].ItemArray[3].ToString());
								nCategoryNumber1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[5].ToString());
								nCategoryNumber2 = int.Parse(ds1.Tables[0].Rows[1].ItemArray[5].ToString());
								break;
							case 3:
								nItemtblidx1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[0].ToString());
								nItemtblidx2 = int.Parse(ds1.Tables[0].Rows[1].ItemArray[0].ToString());
								nItemtblidx3 = int.Parse(ds1.Tables[0].Rows[2].ItemArray[0].ToString());
								strItemID = int.Parse(ds1.Tables[0].Rows[0].ItemArray[1].ToString());
								nValidity1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[3].ToString());
								nValidity2 = int.Parse(ds1.Tables[0].Rows[1].ItemArray[3].ToString());
								nValidity3 = int.Parse(ds1.Tables[0].Rows[2].ItemArray[3].ToString());
								nCategoryNumber1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[5].ToString());
								nCategoryNumber2 = int.Parse(ds1.Tables[0].Rows[1].ItemArray[5].ToString());
								nCategoryNumber3 = int.Parse(ds1.Tables[0].Rows[2].ItemArray[5].ToString());
								break;
							case 4:
								nItemtblidx1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[0].ToString());
								nItemtblidx2 = int.Parse(ds1.Tables[0].Rows[1].ItemArray[0].ToString());
								nItemtblidx3 = int.Parse(ds1.Tables[0].Rows[2].ItemArray[0].ToString());
								nItemtblidx4 = int.Parse(ds1.Tables[0].Rows[3].ItemArray[0].ToString());
								strItemID = int.Parse(ds1.Tables[0].Rows[0].ItemArray[1].ToString());
								nValidity1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[3].ToString());
								nValidity2 = int.Parse(ds1.Tables[0].Rows[1].ItemArray[3].ToString());
								nValidity3 = int.Parse(ds1.Tables[0].Rows[2].ItemArray[3].ToString());
								nValidity4 = int.Parse(ds1.Tables[0].Rows[3].ItemArray[3].ToString());
								nCategoryNumber1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[5].ToString());
								nCategoryNumber2 = int.Parse(ds1.Tables[0].Rows[1].ItemArray[5].ToString());
								nCategoryNumber3 = int.Parse(ds1.Tables[0].Rows[2].ItemArray[5].ToString());
								nCategoryNumber4 = int.Parse(ds1.Tables[0].Rows[3].ItemArray[5].ToString());
								break;
						}
						nValidity1 = nValidity1*itemNum;
						nValidity2 = nValidity2*itemNum;
						nValidity3 = nValidity3*itemNum;
						nValidity4 = nValidity4*itemNum;

						sql = "select sql_statement from sqlexpress where sql_type='SD_UserAdditem_Add' and sql_condition = 'SD_UserAdditem_Add'";
						ds = SqlHelper.ExecuteDataset(sql);
						if(ds!=null && ds.Tables[0].Rows.Count>0)
						{	
							sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
							sql = string.Format(sql,userid,
								nCategoryNumber1,nCategoryNumber2,nCategoryNumber3,nCategoryNumber4,
								nValidity1,nValidity2,nValidity3,nValidity4,
								nItemtblidx1,nItemtblidx2,nItemtblidx3,nItemtblidx4,
								strItemID,sendUser,userid,username,content,itemID);
							ds1= SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
							result = int.Parse(ds1.Tables[0].Rows[0].ItemArray[0].ToString());
						}
					}
					if(result!=-1)
					{
						SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_UserAdditem_Add","添加道具"+itemName.ToString()+"，道具ID"+itemID.ToString()+"，道具数量"+itemNum.ToString()+"，给玩家"+username.ToString()+"，成功");
					}
					else
					{
						SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_UserAdditem_Add","添加道具"+itemName.ToString()+"，道具ID"+itemID.ToString()+"，道具数量"+itemNum.ToString()+"，给玩家"+username.ToString()+"，失败");
					}
				} 
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 添加道具（批量）
		/// <summary>
		/// 添加道具
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static string  UserAdditem_Add_All(string serverIP,int UserByID,string item)
		{
			string[] itemList = item.Split(',');
			int userID = 0;
			string ItemName = null;
			int itemNum = 0;
			string userName = null;
			string itemName = null;
			string get_Result = null;
			int result = -1;
			string sql = null;
			int itemID = 0;
			int strItemID = 0;
			int nCategoryNumber1 = 0;
			int	nCategoryNumber2 = 0;
			int nCategoryNumber3 = 0;
			int nCategoryNumber4 = 0;
			int nValidity1 = 0;
			int nValidity2 = 0;
			int nValidity3 = 0;
			int nValidity4 = 0;
			int nItemtblidx1 = 0;
			int nItemtblidx2 = 0;
			int nItemtblidx3 = 0;
			int nItemtblidx4 = 0;
			
			try
			{
				string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
				for(int i = 0;i<itemList.Length-1;i++)
				{
					result = -1;
					userName = itemList[i].Split('|')[0].ToString();
					userID = CommonInfo.SD_GetUserId_Query(serverIP,userName);
					itemID = int.Parse(itemList[i].Split('|')[1].ToString());
					itemNum = int.Parse(itemList[i].Split('|')[2].ToString());
					itemName = CommonInfo.SD_GetShopItemName_Query(serverIP,itemID);

					sql = "select sql_statement from sqlexpress where sql_type='SD_GetItemBillingCode' and sql_condition = 'SD_GetItemBillingCode'";
					System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{	
						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,itemID);
						DataSet ds1= SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
						switch(ds1.Tables[0].Rows.Count)
						{
							case 1:
								nItemtblidx1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[0].ToString());
								strItemID = int.Parse(ds1.Tables[0].Rows[0].ItemArray[1].ToString());
								nValidity1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[3].ToString());
								nCategoryNumber1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[5].ToString());
								break;
							case 2:
								nItemtblidx1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[0].ToString());
								nItemtblidx2 = int.Parse(ds1.Tables[0].Rows[1].ItemArray[0].ToString());
								strItemID = int.Parse(ds1.Tables[0].Rows[0].ItemArray[1].ToString());
								nValidity1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[3].ToString());
								nValidity2 = int.Parse(ds1.Tables[0].Rows[1].ItemArray[3].ToString());
								nCategoryNumber1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[5].ToString());
								nCategoryNumber2 = int.Parse(ds1.Tables[0].Rows[1].ItemArray[5].ToString());
								break;
							case 3:
								nItemtblidx1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[0].ToString());
								nItemtblidx2 = int.Parse(ds1.Tables[0].Rows[1].ItemArray[0].ToString());
								nItemtblidx3 = int.Parse(ds1.Tables[0].Rows[2].ItemArray[0].ToString());
								strItemID = int.Parse(ds1.Tables[0].Rows[0].ItemArray[1].ToString());
								nValidity1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[3].ToString());
								nValidity2 = int.Parse(ds1.Tables[0].Rows[1].ItemArray[3].ToString());
								nValidity3 = int.Parse(ds1.Tables[0].Rows[2].ItemArray[3].ToString());
								nCategoryNumber1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[5].ToString());
								nCategoryNumber2 = int.Parse(ds1.Tables[0].Rows[1].ItemArray[5].ToString());
								nCategoryNumber3 = int.Parse(ds1.Tables[0].Rows[2].ItemArray[5].ToString());
								break;
							case 4:
								nItemtblidx1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[0].ToString());
								nItemtblidx2 = int.Parse(ds1.Tables[0].Rows[1].ItemArray[0].ToString());
								nItemtblidx3 = int.Parse(ds1.Tables[0].Rows[2].ItemArray[0].ToString());
								nItemtblidx4 = int.Parse(ds1.Tables[0].Rows[3].ItemArray[0].ToString());
								strItemID = int.Parse(ds1.Tables[0].Rows[0].ItemArray[1].ToString());
								nValidity1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[3].ToString());
								nValidity2 = int.Parse(ds1.Tables[0].Rows[1].ItemArray[3].ToString());
								nValidity3 = int.Parse(ds1.Tables[0].Rows[2].ItemArray[3].ToString());
								nValidity4 = int.Parse(ds1.Tables[0].Rows[3].ItemArray[3].ToString());
								nCategoryNumber1 = int.Parse(ds1.Tables[0].Rows[0].ItemArray[5].ToString());
								nCategoryNumber2 = int.Parse(ds1.Tables[0].Rows[1].ItemArray[5].ToString());
								nCategoryNumber3 = int.Parse(ds1.Tables[0].Rows[2].ItemArray[5].ToString());
								nCategoryNumber4 = int.Parse(ds1.Tables[0].Rows[3].ItemArray[5].ToString());
								break;
						}
						nValidity1 = nValidity1*itemNum;
						nValidity2 = nValidity2*itemNum;
						nValidity3 = nValidity3*itemNum;
						nValidity4 = nValidity4*itemNum;
						sql = "select sql_statement from sqlexpress where sql_type='SD_UserAdditem_Add' and sql_condition = 'SD_UserAdditem_Add'";
						ds = SqlHelper.ExecuteDataset(sql);
						if(ds!=null && ds.Tables[0].Rows.Count>0)
						{
							sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
							sql = string.Format(sql,userID,
								nCategoryNumber1,nCategoryNumber2,nCategoryNumber3,nCategoryNumber4,
								nValidity1,nValidity2,nValidity3,nValidity4,
								nItemtblidx1,nItemtblidx2,nItemtblidx3,nItemtblidx4,
								strItemID,"SD敢达OnLine",userID,userName,"SD敢达OnLine发送礼物",itemID);
							ds1= SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
							result = int.Parse(ds1.Tables[0].Rows[0].ItemArray[0].ToString());
						}
					}
					if(result!=-1)
					{
						get_Result +="添加道具"+itemName.ToString()+"，道具ID"+itemID.ToString()+"，道具数量"+itemNum.ToString()+"，给玩家"+userName.ToString()+"，成功\n";
						SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_UserAdditem_Add_All","批量添加道具"+itemName.ToString()+"，道具ID"+itemID.ToString()+"，道具数量"+itemNum.ToString()+"，给玩家"+userName.ToString()+"，成功");
					}
					else
					{
						SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_UserAdditem_Add_All","批量添加道具"+itemName.ToString()+"，道具ID"+itemID.ToString()+"，道具数量"+itemNum.ToString()+"，给玩家"+userName.ToString()+"，失败");
					}
				} 
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return get_Result;
		}
		#endregion

		#region 获得道具列表
		/// <summary>
		/// 获得道具列表
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet GetItemList_Query(string serverIP,int type,string itemName)
		{
			DataSet result = null;
			string sql = null;
			try
			{
				string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
				sql = "select sql_statement from sqlexpress where sql_type='SD_GetItemList_Query' and sql_condition = '"+type+"'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					if(type==2)
						sql = string.Format(sql,itemName);
					result = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 发送公告任务
		public static int BoardTask_Insert(int userByID,string serverIP,string boardMessage,DateTime begintime,DateTime endTime,int interval,int noticeType,int Type)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[9]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@SD_ServerIP",SqlDbType.VarChar,2000),
												   new SqlParameter("@SD_BoardMessage",SqlDbType.VarChar,500),
												   new SqlParameter("@SD_begintime",SqlDbType.DateTime),
												   new SqlParameter("@SD_endTime",SqlDbType.DateTime),
												   new SqlParameter("@SD_NoticeType",SqlDbType.Int),
												   new SqlParameter("@SD_interval",SqlDbType.Int),
												   new SqlParameter("@Type",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = boardMessage;
				paramCode[3].Value = begintime;
				paramCode[4].Value = endTime;
				paramCode[5].Value = noticeType;
				paramCode[6].Value = interval;
				paramCode[7].Value = Type;
				paramCode[8].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SD_TaskList_Insert", paramCode);
			}
			catch (SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region	查询公告列表
		public static DataSet BoardTask_Query()
		{
			DataSet result = null;
			try
			{
				result = SqlHelper.ExecuteDataset("SD_BoardTask_Query");
			}
			catch (SqlException ex)
			{
				SqlHelper.errLog.WriteLog(ex.Message);
			}
			return result;
		}
		#endregion

		#region 修改公告状态
		public static int BoardTask_Update(string serverIP,int userByID,int taskID,DateTime beginTime,DateTime endTime,int interval,int NoticeType,int status,string boardMessage)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[10]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@SD_serverip",SqlDbType.VarChar,300),
												   new SqlParameter("@SD_TaskID",SqlDbType.Int),
												   new SqlParameter("@SD_BoardMessage",SqlDbType.VarChar,500),
												   new SqlParameter("@SD_begintime",SqlDbType.DateTime),
												   new SqlParameter("@SD_endTime",SqlDbType.DateTime),
												   new SqlParameter("@SD_interval",SqlDbType.Int),
												   new SqlParameter("@SD_NoticeType",SqlDbType.Int),
												   new SqlParameter("@SD_status",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = userByID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = taskID;
				paramCode[3].Value = boardMessage;
				paramCode[4].Value = beginTime;
				paramCode[5].Value = endTime;
				paramCode[6].Value = interval;
				paramCode[7].Value = NoticeType;
				paramCode[8].Value = status;
				paramCode[9].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("SD_TaskList_Update", paramCode);
			}
			catch (SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 删除道具
		/// <summary>
		/// 删除道具
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static int UserAdditem_Del(string serverIP,int UserByID,int userid,string username,int ItemID,string ItemName,int type)
		{
			int result = -1;
			string sql = null;
			try
			{
				string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
				sql = "select sql_statement from sqlexpress where sql_type='SD_UserAdditem_Del' and sql_condition = '"+type.ToString()+"'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userid,ItemID,result);
					DataSet ds1= SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
					result = int.Parse(ds1.Tables[0].Rows[0].ItemArray[0].ToString());
				}
				if(result==0)
				{
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_UserAdditem_Del","删除玩家"+username.ToString()+"，道具种类"+type.ToString()+"，道具id"+type.ToString()+"，道具名"+ItemName.ToString()+"，成功");
				}
				else
				{
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_UserAdditem_Del","删除玩家"+username.ToString()+"，道具种类"+type.ToString()+"，道具id"+type.ToString()+"，道具名"+ItemName.ToString()+"，失败");
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 购买记录查询
		/// <summary>
		/// 购买记录查询
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet BuyLog_Query(string serverIP,int userid,DateTime startTime,DateTime endTime)
		{

			DataSet result = null;
			string sql = null;
			string eTime = null;
			try
			{
				string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
				int i=0;
				if(DateTime.Compare(endTime,DateTime.Now.AddDays(1))>0)
					eTime=DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
				else
					eTime=endTime.AddDays(1).ToString("yyyy-MM-dd");
				while(!startTime.AddDays(i).ToString("yyyy-MM-dd").Equals(eTime))
				{
					if(i<45)
					{
						int iTime = Convert.ToInt32(startTime.AddDays(i).ToString("yyyyMMdd"));
						sql = "select sql_statement from sqlexpress where sql_type='SD_BuyLog_Query' and sql_condition = 'SD_BuyLog_Query'";
						System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
						if(ds!=null && ds.Tables[0].Rows.Count>0)
						{
							sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
							sql = string.Format(sql,userid,startTime,endTime,iTime);
							if(i==0)
							{
								result= SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
							}
							else
							{
								result.Merge(SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql));
							}
						}
					}
					else
					{
						break;
					}
					i++;
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
			
		}
		#endregion

		#region 物品删除记录
		/// <summary>
		/// 物品删除记录
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet Delete_ItemLog_Query(string serverIP,int userid,int type,DateTime startTime,DateTime endTime)
		{

			DataSet result = null;
			string sql = null;
			string eTime = null;
			try
			{
				string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
				int i=0;
				if(DateTime.Compare(endTime,DateTime.Now.AddDays(1))>0)
					eTime=DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
				else
					eTime=endTime.AddDays(1).ToString("yyyy-MM-dd");
				while(!startTime.AddDays(i).ToString("yyyy-MM-dd").Equals(eTime))
				{
					if(i<45)
					{
						int iTime = Convert.ToInt32(startTime.AddDays(i).ToString("yyyyMMdd"));
						sql = "select sql_statement from sqlexpress where sql_type='SD_Delete_ItemLog_Query' and sql_condition = '"+type+"'";
						System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
						if(ds!=null && ds.Tables[0].Rows.Count>0)
						{
							sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
							sql = string.Format(sql,userid,startTime,endTime,iTime);
							if(i==0)
							{
								result= SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
							}
							else
							{
								result.Merge(SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql));
							}
						}
					}
					else
					{
						break;
					}
					i++;
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion

		#region 玩家日志查询
		/// <summary>
		/// 玩家日志查询
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet LogInfo_Query(string serverIP,int userid,int type,DateTime startTime,DateTime endTime)
		{

			DataSet result = null;
			string sql = null;
			string eTime = null;
			try
			{
				string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
				if(type==2||type==3||type==8||type==7)
				{
					int i=0;
					if(DateTime.Compare(endTime,DateTime.Now.AddDays(1))>0)
						eTime=DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
					else
						eTime=endTime.AddDays(1).ToString("yyyy-MM-dd");
					while(!startTime.AddDays(i).ToString("yyyy-MM-dd").Equals(eTime))
					{
						if(i<45)
						{
							int iTime = Convert.ToInt32(startTime.AddDays(i).ToString("yyyyMMdd"));
							sql = "select sql_statement from sqlexpress where sql_type='SD_LogInfo_Query' and sql_condition = '"+type+"'";
							System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
							if(ds!=null && ds.Tables[0].Rows.Count>0)
							{
								sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
								sql = string.Format(sql,iTime,userid,startTime,endTime);
								if(i==0)
								{
									result= SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
								}
								else
								{
									result.Merge(SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql));
								}
							}
						}
						else
						{
							break;
						}
						i++;
					}
				}
				else if(type==9)
				{
					int iTime = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
					sql = "select sql_statement from sqlexpress where sql_type='SD_LogInfo_Query' and sql_condition = '"+type+"'";
					System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,iTime,userid,startTime,endTime);
						result= SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
					}					
				}
				else
				{
					sql = "select sql_statement from sqlexpress where sql_type='SD_LogInfo_Query' and sql_condition = '"+type+"'";
					System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,userid,startTime,endTime);
						result= SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
					}
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion

		#region 获取GM账号列表
		/// <summary>
		/// 获取GM账号列表
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet GetGmAccount_Query(string serverIP,int type,string UserName)
		{
			DataSet result = null;
			string sql = null;
			try
			{
				string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
				sql = "select sql_statement from sqlexpress where sql_type='SD_GetGmAccount_Query' and sql_condition = '"+type+"'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					if(type==2)
						sql = string.Format(sql,UserName);
					result = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 修改GM账号
		/// <summary>
		/// 修改GM账号
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static int UpdateGmAccount_Query(string serverIP,int UserByID,int userid,string username,string PWD,string oldUserName,string pilotName)
		{
			int result = -1;
			string sql = null;
			try
			{
				MD5Encrypt st = new MD5Encrypt();
				string sign = st.getMD5ofStr(PWD).ToLower();

				string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
				sql = "select sql_statement from sqlexpress where sql_type='SD_UpdateGmAccount_Query' and sql_condition = 'SD_UpdateGmAccount_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userid,username,sign,pilotName);
					result = SqlHelper.ExecCommand(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql,true);
				}
				if(result==2)
				{
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_UpdateGmAccount_Query","修改GM账号为"+username.ToString()+"/密码/呢称"+pilotName.ToString()+"，成功");
					//记入本地
					sql = "select sql_statement from sqlexpress where sql_type='SD_InGmUser_Query' and sql_condition = 'SD_InGmUser_Query'";
					System.Data.DataSet ds2 = SqlHelper.ExecuteDataset(sql);
					if(ds2!=null && ds2.Tables[0].Rows.Count>0)
					{	
						sql = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,serverIP,oldUserName,username,sign,PWD,pilotName);
						result = SqlHelper.ExecCommand(sql);
					}
				}
				else
				{
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_UpdateGmAccount_Query","修改GM账号为"+username.ToString()+"/密码/呢称"+pilotName.ToString()+"，失败");
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 添加金钱
		/// <summary>
		/// 添加金钱
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static int UpdateMoney_Query(string serverIP,int UserByID,int userid,string username,int money,int old_money)
		{
			int result = -1;
			string sql = null;
			try
			{
				string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
				sql = "select sql_statement from sqlexpress where sql_type='SD_UpdateMoney_Query' and sql_condition = 'SD_UpdateMoney_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userid,money);
					result = SqlHelper.ExecCommand(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql,true);
				}
				if(result==1)
				{
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_UpdateGmAccount_Query","用户"+username.ToString()+"，金钱从"+old_money.ToString()+"改成"+money.ToString()+"，成功");
				}
				else
				{
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_UpdateGmAccount_Query","用户"+username.ToString()+"，金钱从"+old_money.ToString()+"改成"+money.ToString()+"，失败");
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 恢复机体
		/// <summary>
		/// 恢复机体
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static string  ReGetUnits_Query(string serverIP,int UserByID,int userid,string username,int SDID,int itemid,string itemName,string getDate)
		{
			string __Result = null;
			System.Data.DataSet get_Result = null;
			int result = -1;
			string[] iList = null;
			int SlotIndex = -1;
			int UserUnitNum = -1;
			long ItemSerialNum = -1;
			int nCustomLvMax = -1;
			int customlvMax = -1;
			string sql = null;
			try
			{
				if(CommonInfo.SD_IsFullBox_Query(serverIP,userid)>0)
				{
					
					string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
					int iTime = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
					iList = CommonInfo.SD_GetReData_Query(serverIP,iTime,userid,SDID,itemid,getDate);
					if(iList!=null && iList.Length>0)
					{
						int number = int.Parse(iList[0].ToString());
						int itemID = int.Parse(iList[1].ToString());
						string shopitemid = iList[2].ToString();
						int itemtblidx = int.Parse(iList[3].ToString());
						int CategoryNumber = int.Parse(iList[4].ToString());
						string DateEnd = iList[5].ToString();
						int ShopUnit = int.Parse(iList[6].ToString());
						int TimeLimit = int.Parse(iList[7].ToString());
					
						sql = "select sql_statement from sqlexpress where sql_type='SD_ReGetUnits_Query' and sql_condition = '1'";
						System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
						if(ds!=null && ds.Tables[0].Rows.Count>0)
						{	
							sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
							sql = string.Format(sql,number,itemID,shopitemid,itemtblidx,CategoryNumber,DateEnd,ShopUnit,TimeLimit);
							get_Result = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
						}
						if (get_Result != null && get_Result.Tables[0].Rows.Count > 0)
						{
							SlotIndex = int.Parse(get_Result.Tables[0].Rows[0].ItemArray[0].ToString());
							UserUnitNum = int.Parse(get_Result.Tables[0].Rows[0].ItemArray[1].ToString());
							ItemSerialNum = long.Parse(get_Result.Tables[0].Rows[0].ItemArray[2].ToString());
							nCustomLvMax = int.Parse(get_Result.Tables[0].Rows[0].ItemArray[3].ToString());
							customlvMax = CommonInfo.SD_isItemName_Query(itemName);
							if(customlvMax!=-1)
							{
								sql = "select sql_statement from sqlexpress where sql_type='SD_ReGetUnits_Query' and sql_condition = '2'";
								System.Data.DataSet ds1 = SqlHelper.ExecuteDataset(sql);
								if(ds1!=null && ds1.Tables[0].Rows.Count>0)
								{	
									sql = ds1.Tables[0].Rows[0].ItemArray[0].ToString();
									sql = string.Format(sql,customlvMax,userid,SlotIndex,ItemSerialNum);
									result = SqlHelper.ExecCommand(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql,true);
								}
							}
							else
							{
								result = 1;
							}
						}
					}
				}
				else
				{
					result = 2;
				}
				if(result==1)
				{
					__Result = "用户"+username.ToString()+"，恢复机体"+itemName.ToString()+"到第"+SlotIndex+"个格子，机体序列号为"+ItemSerialNum.ToString()+"强化等级为"+nCustomLvMax.ToString()+"，成功";
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_UpdateGmAccount_Query","用户"+username.ToString()+"，恢复机体"+itemName.ToString()+"到第"+SlotIndex+"个格子，机体序列号为"+ItemSerialNum.ToString()+"强化等级为"+nCustomLvMax.ToString()+"，成功");
				}
				else if(result==2)
				{
					__Result = "用户"+username.ToString()+"，恢复机体"+itemName.ToString()+"到第"+SlotIndex+"个格子，机体序列号为"+ItemSerialNum.ToString()+"强化等级为"+nCustomLvMax.ToString()+"，用户机体道具以满";
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_UpdateGmAccount_Query","用户"+username.ToString()+"，恢复机体"+itemName.ToString()+"到第"+SlotIndex+"个格子，机体序列号为"+ItemSerialNum.ToString()+"强化等级为"+nCustomLvMax.ToString()+"，用户机体道具以满");
				}
				else
				{
					__Result = "用户"+username.ToString()+"，恢复机体"+itemName.ToString()+"到第"+SlotIndex+"个格子，机体序列号为"+ItemSerialNum.ToString()+"强化等级为"+nCustomLvMax.ToString()+"，失败";
					SqlHelper.insertGMtoolsLog(UserByID,"SD高达",serverIP,"SD_UpdateGmAccount_Query","用户"+username.ToString()+"，恢复机体"+itemName.ToString()+"到第"+SlotIndex+"个格子，机体序列号为"+ItemSerialNum.ToString()+"强化等级为"+nCustomLvMax.ToString()+"，失败");
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return __Result;
		}
		#endregion
	}
}
