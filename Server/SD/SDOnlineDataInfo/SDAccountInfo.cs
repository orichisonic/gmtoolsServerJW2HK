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
using Common.DataInfo;
using MySql.Data.MySqlClient;
namespace GM_Server.SDOnlineDataInfo
{
	/// <summary>
	/// AccountDataInfo 的摘要说明。
	/// </summary>
	public class SDAccountInfo
	{
		public SDAccountInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 查看激活码信息
		/// <summary>
		/// 		/// 查看激活码信息
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static ArrayList ActiveCode_Query(string account, int actionType,ref string strDesc)
		{
			string getUser = null;
			string sign = null;
			string parameter ="";
			XmlDocument xmlfile = new XmlDocument();
			getUser =account;
			parameter = account;
			MD5Encrypt md5 = new MD5Encrypt();
			sign = md5.getMD5ofStr(parameter+"|T4pb5A.QueryGdCode").ToLower();
			try   
			{
				System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=10");
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					string serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					string url = "http://"+serverIP+"/PayCenter/QueryGdCode.php";
					HttpWebRequest  request  = (HttpWebRequest)
						WebRequest.Create(url);
					request.ContentType="application/x-www-form-urlencoded";
					request.KeepAlive=false; 
					request.Method="POST";
					//参数POST到商城的接口
					Stream writer = request.GetRequestStream(); 
					string postData="getcode="+account+"&sign="+sign+"&encoding=UTF-8";  
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
					if(strDesc!=null && strDesc.Equals("RESULT_0"))
					{
						strDesc = "查询成功";                        
					}
					else if(strDesc!=null && strDesc.Equals("RESULT_3"))
					{
						strDesc = "无此激活码";
					}
					else if(strDesc!=null && strDesc.Equals("RESULT_2"))
					{
						strDesc = "查询密钥错误";
					}
					else
					{
						strDesc = "异常";
					}
					XmlNode nodes=xmlfile.SelectSingleNode("you9/user");
					System.Collections.ArrayList colList = new System.Collections.ArrayList();
					foreach(XmlNode xmlnodes in nodes.ChildNodes)
					{
						colList.Add(xmlnodes.InnerText);
					}
					sr.Close();
					return colList;
					
				}

			}
			catch (SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+account+ex.Message);
				strDesc = "异常";
			}
			return null;
		}
		#endregion

		#region 根据用户查看激活码信息
		/// <summary>
		/// 		/// 根据用户查看激活码信息
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static ArrayList Account_Active_Query(string account,ref string strDesc)
		{
			string getUser = null;
			string sign = null;
			string parameter ="";
			XmlDocument xmlfile = new XmlDocument();
			getUser =account;
			parameter = account;
			MD5Encrypt md5 = new MD5Encrypt();
			sign = md5.getMD5ofStr(parameter+"|T4pb3A.QueryGDUser").ToLower();
			try   
			{
				System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=10");
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					string serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					string url = "http://"+serverIP+"/PayCenter/QueryGDUser.php";
					HttpWebRequest  request  = (HttpWebRequest)
						WebRequest.Create(url);
					request.ContentType="application/x-www-form-urlencoded";
					request.KeepAlive=false; 
					request.Method="POST";
					//参数POST到商城的接口
					Stream writer = request.GetRequestStream(); 
					string postData="getuser="+account+"&sign="+sign+"&encoding=UTF-8";  
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
					if(strDesc!=null && strDesc.Equals("RESULT_0"))
					{
						strDesc = "查询成功";                        
					}
					else if(strDesc!=null && strDesc.Equals("RESULT_3"))
					{
						strDesc = "无此激活码";
					}
					else if(strDesc!=null && strDesc.Equals("RESULT_2"))
					{
						strDesc = "查询密钥错误";
					}
					else
					{
						strDesc = "异常";
					}
					XmlNode nodes=xmlfile.SelectSingleNode("you9/row0");
					System.Collections.ArrayList colList = new System.Collections.ArrayList();
					foreach(XmlNode xmlnodes in nodes.ChildNodes)
					{
						colList.Add(xmlnodes.InnerText);
					}
					sr.Close();
					return colList;
					
				}

			}
			catch (SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+account+ex.Message);
				strDesc = "异常";
			}
			return null;
		}
		#endregion

		#region 查看新手/钻石卡信息
		/// <summary>
		/// 查看新手/钻石卡信息
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static ArrayList Card_Info_Query(string account,string card,ref string strDesc)
		{
			string getUser = null;
			string getCard = null;
			string sign = null;
			string parameter ="";
			XmlDocument xmlfile = new XmlDocument();
			getUser =account;
			getCard = card;
			parameter = account+card;
			MD5Encrypt md5 = new MD5Encrypt();
			sign = md5.getMD5ofStr(parameter+"|T4pb3A.QueryGDUser").ToLower();
			try
			{
				System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=10");
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					string serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					string url = "http://"+serverIP+"/PayCenter/gd_card_info.php";
					HttpWebRequest  request  = (HttpWebRequest)
						WebRequest.Create(url);
					request.ContentType="application/x-www-form-urlencoded";
					request.KeepAlive=false; 
					request.Method="POST";
					//参数POST到商城的接口
					Stream writer = request.GetRequestStream(); 
					string postData="getuser="+account+"&cardnub="+card+"&sign="+sign+"&encoding=UTF-8";  
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
					if(strDesc!=null && strDesc.Equals("RESULT_0"))
					{
						strDesc = "查询成功";                        
					}
					else if(strDesc!=null && strDesc.Equals("RESULT_3"))
					{
						strDesc = "无此卡";
					}
					else if(strDesc!=null && strDesc.Equals("RESULT_2"))
					{
						strDesc = "查询密钥错误";
					}
					else
					{
						strDesc = "异常";
					}
					XmlNode nodes=xmlfile.SelectSingleNode("you9/row1");
					System.Collections.ArrayList colList = new System.Collections.ArrayList();
					foreach(XmlNode xmlnodes in nodes.ChildNodes)
					{
						colList.Add(xmlnodes.InnerText);
					}
					sr.Close();
					return colList;
					
				}

			}
			catch (SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+account+ex.Message);
				strDesc = "异常";
			}
			return null;
		}
		#endregion

		#region 查看玩家资料(根据账号)
		/// <summary>
		/// 查看玩家资料
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet Account_Query(string serverIP,string strname)
		{
			DataSet result = null;
			string sql = null;
			try
			{
				sql = "select sql_statement from sqlexpress where sql_type='SD_Account_Query' and sql_condition = 'SD_Account_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,strname);
					sql = sql.Replace("\t"," ");
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

		#region 查看玩家资料(根据呢称)
		/// <summary>
		/// 查看玩家资料
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet Nick_Query(string serverIP,string strNick)
		{
			DataSet result = null;
			string sql = null;
			try
			{
				sql = "select sql_statement from sqlexpress where sql_type='SD_Nick_Query' and sql_condition = 'SD_Nick_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,strNick);
					sql = sql.Replace("\t"," ");
					string serverPwd = CommonInfo.SD_GameDBInfo_Query(serverIP)[1].ToString();
					result = SqlHelper.ExecuteDataset(SqlHelper.GetConnectionString(serverIP,SqlHelper.SdUser,serverPwd,SqlHelper.SdMember),sql);
				}
			}
			catch (System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 玩家机体组合
		/// <summary>
		/// 玩家机体组合
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet Item_MixTree_Query(string serverIP,int userid)
		{
			DataSet result = null;
			string sql = null;
			try
			{
				sql = "select sql_statement from sqlexpress where sql_type='SD_Item_MixTree_Query' and sql_condition = 'SD_Item_MixTree_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userid);
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

		#region 玩家机体信息
		/// <summary>
		/// 玩家机体信息
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet Item_UserUnits_Query(string serverIP,int userid)
		{
			DataSet result = null;
			string sql = null;
			try
			{
				sql = "select sql_statement from sqlexpress where sql_type='SD_Item_UserUnits_Query' and sql_condition = 'SD_Item_UserUnits_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userid);
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

		#region 玩家机体道具信息
		/// <summary>
		/// 玩家机体道具信息
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet UnitsItem_Query(string serverIP,int userid,int unitID)
		{
			DataSet result = null;
			string sql = null;
			try
			{
				sql = "select sql_statement from sqlexpress where sql_type='SD_UnitsItem_Query' and sql_condition = 'SD_UnitsItem_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userid,unitID);
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

		#region 玩家战斗道具
		/// <summary>
		/// 玩家战斗道具
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet Item_UserCombatitems_Query(string serverIP,int userid)
		{
			DataSet result = null;
			string sql = null;
			try
			{
				sql = "select sql_statement from sqlexpress where sql_type='SD_Item_UserCombatitems_Query' and sql_condition = 'SD_Item_UserCombatitems_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userid);
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

		#region 玩家副官道具
		/// <summary>
		/// 玩家副官道具
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet Item_Operator_Query(string serverIP,int userid)
		{
			DataSet result = null;
			string sql = null;
			try
			{
				sql = "select sql_statement from sqlexpress where sql_type='SD_Item_Operator_Query' and sql_condition = 'SD_Item_Operator_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userid);
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

		#region 玩家涂料道具
		/// <summary>
		/// 玩家涂料道具
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet Item_Paint_Query(string serverIP,int userid)
		{
			DataSet result = null;
			string sql = null;
			try
			{
				sql = "select sql_statement from sqlexpress where sql_type='SD_Item_Paint_Query' and sql_condition = 'SD_Item_Paint_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userid);
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

		#region 玩家标签道具
		/// <summary>
		/// 玩家标签道具
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet Item_Sticker_Query(string serverIP,int userid)
		{
			DataSet result = null;
			string sql = null;
			try
			{
				sql = "select sql_statement from sqlexpress where sql_type='SD_Item_Sticker_Query' and sql_condition = 'SD_Item_Sticker_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userid);
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

		#region 玩家技能道具
		/// <summary>
		/// 玩家技能道具
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet Item_Skill_Query(string serverIP,int userid)
		{
			DataSet result = null;
			string sql = null;
			try
			{
				sql = "select sql_statement from sqlexpress where sql_type='SD_Item_Skill_Query' and sql_condition = 'SD_Item_Skill_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userid);
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

		#region 玩家好友列表
		/// <summary>
		/// 玩家好友列表
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet Firend_Query(string serverIP,int userid)
		{
			DataSet result = null;
			string sql = null;
			try
			{
				sql = "select sql_statement from sqlexpress where sql_type='SD_Firend_Query' and sql_condition = 'SD_Firend_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userid);
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

		#region 玩家排名查询
		/// <summary>
		/// 玩家排名查询
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet UserRank_Query(string serverIP,int type)
		{
			DataSet result = null;
			string sql = null;
			try
			{
				if(type==1)
					sql = "select sql_statement from sqlexpress where sql_type='SD_UserExp_Query' and sql_condition = 'SD_UserExp_Query'";
				else
					sql = "select sql_statement from sqlexpress where sql_type='SD_UserWin_Query' and sql_condition = 'SD_UserWin_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql);
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
	}
}