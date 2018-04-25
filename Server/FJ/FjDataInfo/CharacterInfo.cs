using System;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections;
using Common.Logic;
using Common.API;

namespace GM_Server.FJDataInfo
{
	/// <summary>
	/// CharacterInfo 的摘要说明。
	/// </summary>
	public class CharacterInfo
	{
		public CharacterInfo()
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
			sign = md5.getMD5ofStr(parameter+"|T4pb5A.QueryFjCode").ToLower();
			try   
			{
				System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=10");
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					string serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					string url = "http://"+serverIP+"/PayCenter/QueryFjCode.php";
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
		#region 查看激活码信息
		/// <summary>
		/// 		/// 查看激活码信息
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static ArrayList FJ_JPQuery(string account, int actionType,ref string strDesc)
		{
			string getUser = null;
			string sign = null;
			string parameter ="";
			XmlDocument xmlfile = new XmlDocument();
			getUser =account;
			parameter = Convert.ToString(actionType)+account;
			MD5Encrypt md5 = new MD5Encrypt();
			sign = md5.getMD5ofStr(parameter+"|T4pb32.QueryfjJp").ToLower();
			try   
			{
				System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=10");
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					string serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					string url = "http://"+serverIP+"/PayCenter/QueryfjJp.php";
					HttpWebRequest  request  = (HttpWebRequest)
						WebRequest.Create(url);
					request.ContentType="application/x-www-form-urlencoded";
					request.KeepAlive=false; 
					request.Method="POST";
					//参数POST到商城的接口
					Stream writer = request.GetRequestStream(); 
					string postData="type="+actionType+"&key="+account+"&sign="+sign+"&encoding=UTF-8";  
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
					else
					{
						strDesc = "异常";
					}
					int i = 0;
					XmlNode nodes ;
					if(actionType==1)
					{
						nodes=xmlfile.SelectSingleNode("you9/user");
						System.Collections.ArrayList colList = new System.Collections.ArrayList();
						foreach(XmlNode xmlnodes in nodes.ChildNodes)
						{
							colList.Add(xmlnodes.InnerText);
						}
						sr.Close();
						return colList;
					}
					else
					{
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

			}
			catch (SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+account+ex.Message);
				strDesc = "异常";
			}
			return null;
		}
		#endregion
		#region 风火查询个人充值信息
		/// <summary>
		///  风火查询个人充值信息
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static ArrayList FJUserCharge_Query(int areaid,string account,ref string strDesc)
		{
//			string getUser = null;
//			string sign = null;
//			string parameter ="";
//			XmlDocument xmlfile = new XmlDocument();
//			getUser =account;
//			parameter = Convert.ToString(areaid)+account;
//			MD5Encrypt md5 = new MD5Encrypt();
//			sign = md5.getMD5ofStr(parameter+"|T4pb32.QueryfjGold").ToLower();
//			try   
//			{
//				System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=10");
//				if(ds!=null && ds.Tables[0].Rows.Count>0)
//				{
//					string serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
//					string url = "http://"+serverIP+"/PayCenter/QueryfjGold.php";
//					HttpWebRequest  request  = (HttpWebRequest)
//						WebRequest.Create(url);
//					request.ContentType="application/x-www-form-urlencoded";
//					request.KeepAlive=false; 
//					request.Method="POST";
//					//参数POST到商城的接口
//					Stream writer = request.GetRequestStream(); 
//					string postData="area="+areaid+"&getUser="+account+"&sign="+sign+"&encoding=UTF-8";
//
//					ASCIIEncoding encoder = new ASCIIEncoding();
//					byte[] ByteArray = encoder.GetBytes(postData);
//					writer.Write(ByteArray,0,postData.Length);
//					writer.Close();
//					//得到商城接口的回应
//					WebResponse
//						resp = request.GetResponse();
//					StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
//					//Console.WriteLine(sr.ReadToEnd().Trim());
//					xmlfile.Load(sr);
//					XmlNode descNodes = xmlfile.SelectSingleNode("you9/status");
//					strDesc = descNodes.InnerText;
//					if(strDesc!=null && strDesc.Equals("RESULT_0"))
//					{
//						strDesc = "查询成功";                        
//					}
//					else if(strDesc!=null && strDesc.Equals("RESULT_3"))
//					{
//						strDesc = "无此充值信息";
//					}
//					else
//					{
//						strDesc = "异常";
//					}
//					XmlNode nodes=xmlfile.SelectSingleNode("you9/user");
//					System.Collections.ArrayList colList = new System.Collections.ArrayList();
//
//					foreach(XmlNode xmlnodes in nodes.ChildNodes)
//					{
//						colList.Add(xmlnodes.InnerText);
//					}
//					sr.Close();
//					return colList;
//					
//				}
//
//			}
//			catch (SqlException ex)
//			{
//				SqlHelper.errLog.WriteLog("服务器IP"+account+ex.Message);
//				strDesc = "异常";
//			}
//			return null;

			string getUser = null;
			string sign = null;
			string parameter ="";
			XmlDocument xmlfile = new XmlDocument();
			getUser =account;
			parameter = account;
			MD5Encrypt md5 = new MD5Encrypt();
			sign = md5.getMD5ofStr(areaid.ToString()+parameter+"|T4pb32.QueryfjGold").ToLower();
			try   
			{
				System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=10");
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					string serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					string url = "http://"+serverIP+"/PayCenter//QueryfjGold.php";
					HttpWebRequest  request  = (HttpWebRequest)
						WebRequest.Create(url);
					request.ContentType="application/x-www-form-urlencoded";
					request.KeepAlive=false; 
					request.Method="POST";
					//参数POST到商城的接口
					Stream writer = request.GetRequestStream(); 
					string postData="area="+areaid.ToString()+"&getuser="+account+"&sign="+sign+"&encoding=UTF-8";  
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
		#region 添加玩家充值明细
		/// <summary>
		/// 添加玩家充值明细
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet FJ_userCharge_Consume(string serverip,string account,int areaID,int deduct)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@userID",SqlDbType.VarChar,20),
												   new SqlParameter("@areaID",SqlDbType.Int),
												   new SqlParameter("@deduct",SqlDbType.Int)};
				paramCode[0].Value = account;
				paramCode[1].Value = areaID;
				paramCode[2].Value = deduct;
				result = SqlHelper.ExecSPDataSet("FJ_UserCharge_Consume",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverip+"呢称"+account+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看帐号信息
		/// <summary>
		/// 查看帐号信息
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static ArrayList Account_Query(string account, int actionType,ref string strDesc)
		{
			string getUser = null;
			string sign = null;
			string parameter ="";
			XmlDocument xmlfile = new XmlDocument();
			getUser =account;
			parameter = account;
			MD5Encrypt md5 = new MD5Encrypt();
			sign = md5.getMD5ofStr(parameter+"|T4pb3A.QueryFjUser").ToLower();
			try   
			{
				System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=10");
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					string serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					string url = "http://"+serverIP+"/PayCenter/QueryFjUser.php";
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
						strDesc = "此帐号未激活";
					}
					else
					{
						strDesc = "异常";
					}
					int i=0;

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
				SqlHelper.errLog.WriteLog("函数名Account_Query,服务器IP"+account+ex.Message);
				strDesc ="异常";
			}
			return null;
		}
		#endregion
		#region 查询玩家临时修改密码
		/// <summary>
		/// 查询玩家临时修改密码
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <param name="passwd"></param>
		/// <returns></returns>
		public static DataSet FJ_ModifPwd_Query(string account,string serverIP,string city)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,30),
											   };
				paramCode[0].Value=serverIP;
			    paramCode[1].Value=city;
				paramCode[2].Value=account;
				result = SqlHelper.ExecSPDataSet("FJ_PassWD_Query",paramCode);
                
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);

			}
			return result;
		}
		#endregion
		#region 恢复玩家密码
		/// <summary>
		/// 恢复玩家密码
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <param name="passwd"></param>
		/// <returns></returns>
		public static int FJ_RecoverPwd(int gmUserID,string account,string serverIP,string city)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,30),
												   new SqlParameter("@result",SqlDbType.Int)
											   };
				paramCode[0].Value=gmUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=city;
				paramCode[3].Value =account;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_PassWD_Recover",paramCode);
                
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);

			}
			return result;
		}
		#endregion
		#region 修改玩家临时密码
		/// <summary>
		/// 修改玩家临时密码
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <param name="passwd"></param>
		/// <returns></returns>
		public static int FJ_ModifPwd(int operateUserID,string account,string passwd,string serverIP,string city)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Passwd",SqlDbType.VarChar,30),
												   new SqlParameter("@result",SqlDbType.Int)
											   };
				paramCode[0].Value=operateUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=city;
				paramCode[3].Value =account;
				paramCode[4].Value= passwd;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_PassWD_Update",paramCode);
                
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);

			}
			return result;
		}
		#endregion
		#region 称号查询
		/// <summary>
		/// 称号查询
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet FJ_curRank_Query(string serverip,string char_name)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_char_name",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverip;
				paramCode[1].Value = char_name;
				result = SqlHelper.ExecSPDataSet("FJ_CurRank_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverip+"呢称"+char_name+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家资料
		/// <summary>
		/// 查看玩家资料
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet FJAccount_Query(string serverIP,string city,string account)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = city;
				paramCode[2].Value = account;
				result = SqlHelper.ExecSPDataSet("FJ_Account_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家资料
		/// <summary>
		/// 查看玩家资料
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet characterInfo_Query(string serverIP,string city,string account,string userNick)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
                                                   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
                                                   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_UserNick",SqlDbType.VarChar,30)};
                paramCode[0].Value = serverIP;
				paramCode[1].Value = city;
				paramCode[2].Value = account;
                paramCode[3].Value = userNick;
				if(userNick.Equals("ALL"))
				{
					result = SqlHelper.ExecSPDataSet("FJ_CharacterInfo_QueryAll",paramCode);
				}
				else
				{
					result = SqlHelper.ExecSPDataSet("FJ_CharacterInfo_Query",paramCode);
				}
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 修改玩家人物资料
		/// <summary>
		/// 修改玩家资料
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
		public static int characterInfo_Update(int userByID,string serverIP,string account,string char_name,int level,int imoney,int fexp,int ihonor,int icurRank)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[10]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_Char_Name",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_iLevel",SqlDbType.Int),
												   new SqlParameter("@FJ_iMoney",SqlDbType.Int),
												   new SqlParameter("@FJ_fexp",SqlDbType.Int),
												   new SqlParameter("@FJ_ihonor",SqlDbType.Int),
												   new SqlParameter("@FJ_icurrank",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=account;
				paramCode[3].Value=char_name;
				paramCode[4].Value=level;
				paramCode[5].Value=imoney;
				paramCode[6].Value=fexp;
				paramCode[7].Value=ihonor;
				paramCode[8].Value=icurRank;
				paramCode[9].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_CharacterInfo_Update",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region 查询玩家等级金钱排名
		/// <summary>
		/// 查询玩家等级金钱排名
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet LevelMoneySort_Query(string serverIP,int type)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_type",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = type;
				result = SqlHelper.ExecSPDataSet("FJ_PlayerlevelMoney_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 帮派查询
		/// <summary>
		///帮派查询
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet GuildName_Query(string serverIP,int guildID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Guild",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = guildID;
				result = SqlHelper.ExecSPDataSet("FJ_Guild_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		/// <summary>
		///帮派查询
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet Guild_Query(string serverIP,string guildName)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_GuildName",SqlDbType.VarChar,50)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = guildName;
				result = SqlHelper.ExecSPDataSet("FJ_Guild_QueryAll",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 帮派成员查询
		/// <summary>
		///帮派成员查询
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet GuildMember_Query(string serverIP,int guildID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_GuildID",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = guildID;
				result = SqlHelper.ExecSPDataSet("FJ_Guild_Member",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 加入帮会
		/// <summary>
		/// 加入帮会
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <returns></returns>
		public static int FJ_JointGuild(int userByID,string serverIP,string char_name,int guildID)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				System.DateTime date1 = new System.DateTime(1970, 1, 1, 0, 0, 0);
				System.TimeSpan diff1 = DateTime.Now.Subtract(date1);

				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Char_Name",SqlDbType.VarChar,50),
												   new SqlParameter("@FJ_guild_id",SqlDbType.Int),
												   new SqlParameter("@FJ_JoinTime",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=char_name;
				paramCode[3].Value=guildID;
				paramCode[4].Value=diff1.TotalSeconds;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_JointGuild",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region 退出帮会
		/// <summary>
		/// 退出帮会
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <returns></returns>
		public static int FJ_DeleteGuild(int userByID,string serverIP,string char_name)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Char_Name",SqlDbType.VarChar,50),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=char_name;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_DeleteGuild",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region 添加玩家技能
		/// <summary>
		/// 添加玩家技能
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <returns></returns>
		public static int FJ_PlayerSkill_Insert(int userByID,string serverIP,string char_name,int skillID,int skilllvl)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_char_name",SqlDbType.VarChar,50),
												   new SqlParameter("@FJ_skillid",SqlDbType.Int),
												   new SqlParameter("@FJ_skillLvl",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=char_name;
				paramCode[3].Value=skillID;
				paramCode[4].Value=skilllvl;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_SkillLvl_Insert",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region 修改玩家技能
		/// <summary>
		/// 修改玩家技能
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <returns></returns>
		public static int FJ_Skill_Update(int userByID,string serverIP,int skill_lvl,string char_name,int skillid)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
													new SqlParameter("@Gm_UserID",SqlDbType.Int),
													new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
													new SqlParameter("@FJ_skill_lvl",SqlDbType.Int),
													new SqlParameter("@FJ_skillid",SqlDbType.Int),
												    new SqlParameter("@FJ_char_name",SqlDbType.VarChar,50),
													new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=skill_lvl;
				paramCode[3].Value=skillid;
				paramCode[4].Value=char_name;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_SkillLvl_Update",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region 删除玩家技能
		/// <summary>
		/// 删除玩家技能
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <returns></returns>
		public static int FJ_Skill_Delete(int userByID,string serverIP,string char_name,int skillid)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_char_name",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_skill_id",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=char_name;
				paramCode[3].Value=skillid;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_SkillLvl_Delete",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region 玩家技能查询
		/// <summary>
		/// 玩家技能查询
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="char_name"></param>
		/// <returns></returns>
		public static DataSet FJ_SkillLvl_Query(string serverIP,string char_name)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_char_name",SqlDbType.VarChar,50)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=char_name;
				result = SqlHelper.ExecSPDataSet("FJ_SkillLvl_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region 技能列表查询
		/// <summary>
		/// 技能列表查询
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="char_name"></param>
		/// <returns></returns>
		public static DataSet FJ_SkillList_Query(int professionid)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@FJ_skill_pro",SqlDbType.Int)};
				paramCode[0].Value=professionid;
				result = SqlHelper.ExecSPDataSet("FJ_SkillList_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;

		}
		#endregion
		#region 踢在线玩家
		/// <summary>
		///  踢在线玩家
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <returns></returns>
		public static int userOnline_out(int userByID,string serverIP,string city,string account)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=city;
				paramCode[3].Value=account;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_Loginout_del",paramCode);
			}
			catch(SqlException ex)
			{
				;SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region 查询所有帮派
		/// <summary>
		///  查询所有帮派
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <returns></returns>
		public static DataSet FJ_Guild_Query(string serverIP)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,30)};
				paramCode[0].Value=serverIP;
				result = SqlHelper.ExecSPDataSet("FJ_Guild_QueryALL",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region 师徒信息管理
		/// <summary>
		///  师徒信息管理
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <returns></returns>
		public static DataSet FJ_SocialRelate_Query(string serverIP,string char_name,int type)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_char_name",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_Type",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=char_name;
				paramCode[2].Value=type;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPDataSet("FJ_SocialRelate_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region 师徒信息管理
		/// <summary>
		///  师徒信息管理
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <returns></returns>
		public static int FJ_SocialRelate_Insert(int userByID,string serverIP,string char_name,string RelateCharName,int type)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_char_name",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_RelateCharName",SqlDbType.VarChar,50),
												   new SqlParameter("@FJ_type",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=char_name;
				paramCode[3].Value=RelateCharName;
				paramCode[4].Value=type;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_SocialRelate_Insert",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region 解除师徒关系
		/// <summary>
		///  解除师徒关系
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <returns></returns>
		public static int FJ_SocialRelate_Delete(int userByID,string serverIP,string char_name,string relate_char_name,int type)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_char_name",SqlDbType.VarChar,50),
												   new SqlParameter("@Fj_RelateCharName",SqlDbType.VarChar,50),
												   new SqlParameter("@FJ_Type",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=char_name;
				paramCode[3].Value=relate_char_name;
				paramCode[4].Value=type;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_SocialRelate_Delete",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region 玩家婚姻信息
		/// <summary>
		///  玩家婚姻信息
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <returns></returns>
		public static DataSet FJ_Wedding_Query(string serverIP,string char_name)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_char_name",SqlDbType.VarChar,20)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=char_name;
				result = SqlHelper.ExecSPDataSet("FJ_Wedding_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region 玩家结婚管理
		/// <summary>
		///  玩家结婚管理
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <returns></returns>
		public static int FJ_CreateWedding(int userByID,string serverIP,string char_name,string RelateCharName)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_char_name",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_CoupleCharName",SqlDbType.VarChar,50),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=char_name;
				paramCode[3].Value=RelateCharName;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_CreateWedding",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region 玩家离婚管理
		/// <summary>
		///  玩家离婚管理
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <returns></returns>
		public static int FJ_DeleteWedding(int userByID,string serverIP,string char_name,string RelateCharName)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_char_name",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_CoupleCharName",SqlDbType.VarChar,50),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=char_name;
				paramCode[3].Value=RelateCharName;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_DeleteWedding",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region 查询游戏里面所有地图
		/// <summary>
		///  查询游戏里面所有地图
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <returns></returns>
		public static DataSet FJMapQuery()
		{
			DataSet result = null;
			//SqlParameter[] paramCode;
			try
			{
//				paramCode = new SqlParameter[2]{
//												   new SqlParameter("@FJ_PosX",SqlDbType.Float),
//												   new SqlParameter("@FJ_PosY",SqlDbType.Float)};
//				paramCode[0].Value=posx;
//				paramCode[1].Value=posy;
				result = SqlHelper.ExecSPDataSet("FJ_MoveMap_Query");
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;

		}
		#endregion
		#region 查询玩家在游戏位置
		/// <summary>
		///  查询玩家在游戏位置
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <returns></returns>
		public static DataSet FJPlayerPosition(string serverIP,string account,string char_name)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_CharName",SqlDbType.VarChar,30),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=account;
				paramCode[2].Value=char_name;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPDataSet("FJ_PlayerPositionofMap_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region 更新玩家所在游戏的位置
		/// <summary>
		///  更新玩家所在游戏的位置
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <returns></returns>
		public static int FJPlayerPosition_Update(int userByID,string serverIP,string account,string char_name,float posx,float posy,float posz)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[8]{
												    new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_Char_Name",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_PosX",SqlDbType.Float),
												   new SqlParameter("@FJ_PosY",SqlDbType.Float),
												   new SqlParameter("@FJ_PosZ",SqlDbType.Float),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=account;
				paramCode[3].Value=char_name;
				paramCode[4].Value=posx;
				paramCode[5].Value=posy;
				paramCode[6].Value=posz;
				paramCode[7].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_PlayerPositionofMap_Update",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region 查询玩家登录登出记录
		/// <summary>
		///  查询玩家登录登出记录
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <returns></returns>
		public static DataSet FJPlayLoginOut(string serverIP,string city,string charName,int actionType,string loginIP,DateTime startDate,DateTime endDate)
		{
			DataSet result = new DataSet();
			DataSet dv = null;
			SqlParameter[] paramCode;
			string strSQL = null;
			try
			{
			    System.TimeSpan interval = endDate - startDate;
				int dayofyears = startDate.DayOfYear;
				int endofyears = endDate.DayOfYear;
				int nowsofyears = DateTime.Now.DayOfYear;
				int days = interval.Days;
				if(actionType==2)
				{
					if(loginIP.Length>0)
					{
						strSQL="select distinct(char_name),max(action_time),case max(log_type) when 4 then '游戏开始' else '游戏结束' end as log_type,max(address) from  FJLogin.dbo.account_log where server_name='"+city.Substring(city.IndexOf('(')+1,city.Length-city.IndexOf('(')-2)+"' and address like '%"+loginIP+"%' and log_type in (4,5)  and action_time >=convert(varchar(10),'"+startDate.ToString("yyyy-MM-dd")+"',120)  and action_time <=convert(varchar(10),'"+endDate.ToString("yyyy-MM-dd")+"',120)  group by char_name order by char_name";
					}
					else
					{
						strSQL="select char_name,action_time,case log_type when 4 then '游戏开始' else '游戏结束' end as log_type,address from FJLogin.dbo.account_log where server_name='"+city.Substring(city.IndexOf('(')+1,city.Length-city.IndexOf('(')-2)+"' and char_name='"+charName+"' and log_type in (4,5)  and action_time >=convert(varchar(10),'"+startDate.ToString("yyyy-MM-dd")+"',120)  and action_time <=convert(varchar(10),'"+endDate.ToString("yyyy-MM-dd")+"',120) order by char_name,action_time";
					}
					if((dayofyears % 2)==0)
					{
						result = SqlHelper.ExecSQL("60.206.14.44",strSQL);

					}
					else
					{
						result = SqlHelper.ExecSQL("60.206.14.43",strSQL);
					}
				}
				else if(actionType==1)
				{
					for(int i=0;i<=days;i++)
					{
						if(loginIP.Length>0)
						{
							strSQL="select distinct(char_name),max(action_time),case max(log_type) when 4 then '游戏开始' else '游戏结束' end as log_type,max(address) from  FJ_LOG_BAK.dbo.account_log_"+dayofyears+" where server_name='"+city.Substring(city.IndexOf('(')+1,city.Length-city.IndexOf('(')-2)+"' and address like '%"+loginIP+"%' and log_type in (4,5)  and action_time >=convert(varchar(10),'"+startDate.ToString("yyyy-MM-dd")+"',120)  and action_time <=convert(varchar(10),'"+endDate.ToString("yyyy-MM-dd")+"',120)  group by char_name order by char_name";
						}
						else
						{
							strSQL="select char_name,action_time,case log_type when 4 then '游戏开始' else '游戏结束' end as log_type,address from FJ_LOG_BAK.dbo.account_log_"+dayofyears+" where server_name='"+city.Substring(city.IndexOf('(')+1,city.Length-city.IndexOf('(')-2)+"' and char_name='"+charName+"' and log_type in (4,5)  and action_time >=convert(varchar(10),'"+startDate.ToString("yyyy-MM-dd")+"',120)  and action_time <=convert(varchar(10),'"+endDate.ToString("yyyy-MM-dd")+"',120) order by char_name,action_time";
						}
						dv = SqlHelper.ExecSQL("60.206.14.28",strSQL);
						//							}
						if(dv!=null && dv.Tables[0].Rows.Count>0)
						{
							result.Merge(dv.Tables[0],false,System.Data.MissingSchemaAction.Add);
						}
						dayofyears+=1;
					}
					
				}
				else
				{
					paramCode = new SqlParameter[7]{
													   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,30),
													   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
													   new SqlParameter("@FJ_CharName",SqlDbType.VarChar,20),
													   new SqlParameter("@FJ_StartDate",SqlDbType.DateTime),
													   new SqlParameter("@FJ_EndDate",SqlDbType.DateTime),
													   new SqlParameter("@FJ_LoginIP",SqlDbType.VarChar,30),
													   new SqlParameter("@result",SqlDbType.Int)};
					paramCode[0].Value=serverIP;
					paramCode[1].Value=city;
					paramCode[2].Value=charName;
					paramCode[3].Value=startDate;
					paramCode[4].Value=endDate;
					paramCode[5].Value=loginIP;
					paramCode[6].Direction = ParameterDirection.ReturnValue;
					result = SqlHelper.ExecSPDataSet("FJ_PlayLoginLogout_query",paramCode);
				}
				
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region 删除角色
		/// <summary>
		///  删除角色
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <returns></returns>
		public static int FJ_Role_Delete(int userByID,string serverIP,string char_name)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_CharName",SqlDbType.VarChar,50),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=char_name;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_delete_characters",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region 恢复角色
		/// <summary>
		///  恢复角色
		/// </summary>
		/// <param name="userByID">操作员ID</param>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">帐号</param>
		/// <returns></returns>
		public static int FJ_RoleRecover(int userByID,string serverIP,string char_name)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[4]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_recover_char_name",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=char_name;
				paramCode[3].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_recover_characters",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		#region GM帐号查询
		/// <summary>
		///GM帐号查询
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet GMAccounts_Query(string serverIP,string city,string account)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,50)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = city;
				paramCode[2].Value = account;
				result = SqlHelper.ExecSPDataSet("FJ_GMAccounts_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查询所有开通绿色通道的玩家
		/// <summary>
		/// 查询所有开通绿色通道的玩家
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet FJGreenGate_Query(string serverIP,string city,string account)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
											   	   new SqlParameter("@FJ_Account",SqlDbType.VarChar,30)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=city;
				paramCode[2].Value=account;
				result = SqlHelper.ExecSPDataSet("FJ_GreenGate_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 开通绿色通道
		/// <summary>
		/// 开通绿色通道
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int FJGreenGate_Open(int operateUserID,string serverIP,string city,string account)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@GM_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,30),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=operateUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=city;
				paramCode[3].Value=account;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_GreenGate_Open",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 关闭绿色通道
		/// <summary>
		/// 关闭绿色通道
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int FJGreenGate_Close(int operateUserID,string serverIP,string city,string account)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@GM_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,30),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=operateUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=city;
				paramCode[3].Value=account;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_GreenGate_Close",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 剔除玩家登陆卡号问题
		/// <summary>
		/// 剔除玩家登陆卡号问题
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int FJExceptLogin_Del(int operateUserID,string serverIP,string city,string account)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@GM_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,30),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=operateUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=city;
				paramCode[3].Value=account;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_ExceptLogin_delete",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查询所有大区GM帐号信息
		public static DataSet GMAccountDesc_QueryAll(string ServerIP)
		{	
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@ServerIP",SqlDbType.VarChar,30)};
	
				paramCode[0].Value = ServerIP;

				result = SqlHelper.ExecSPDataSet("Fj_GMAccountDesc",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+ServerIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 修改可用时间
		public static int FJPlayerCardtime_Update(int userByID,string serverIP,string city,string account,string time,string oldtime)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[7]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_UserID",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_Time",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_oldTime",SqlDbType.VarChar,20),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=city;
				paramCode[3].Value=account;
				paramCode[4].Value=time;
				paramCode[5].Value=oldtime;
				paramCode[6].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJPlayerCardtime_Update",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion
		public static int CreatePlayerAccount(int operateUserID,string serverIP,string prefixName,string passWord,string City,int startNum,int endNum,DateTime overtime,string strid,string sender,string stravname)
		{
			int result = -1;
			SqlParameter[] paramCode;
			//MD5Encrypt st = new MD5Encrypt();;
			try
			{
				paramCode = new SqlParameter[12]{
												   new SqlParameter("@GM_UserID",SqlDbType.Int),
												   new SqlParameter("@Serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@prefixName",SqlDbType.VarChar,30),
												   new SqlParameter("@Passwd",SqlDbType.VarChar,50),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,50),
												   new SqlParameter("@startNum",SqlDbType.Int),
												   new SqlParameter("@endNum",SqlDbType.Int),
												   new SqlParameter("@Overtime",SqlDbType.DateTime),
												   new SqlParameter("@StrGuid",SqlDbType.VarChar,200),
												   new SqlParameter("@Sender",SqlDbType.VarChar,200),
												   new SqlParameter("@avname",SqlDbType.VarChar,200),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = operateUserID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = prefixName;
				paramCode[3].Value = passWord;
				paramCode[4].Value = City;
				paramCode[5].Value = startNum;
				paramCode[6].Value = endNum;
				paramCode[7].Value = overtime;
				paramCode[8].Value = strid;
				paramCode[9].Value = sender;
				paramCode[10].Value = stravname;
				paramCode[11].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Fj_CreateTempAccount",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		public static int SdoCreatePlayerAccount(int operateUserID,string serverIP,string prefixName,string passWord,string City,int startNum,int endNum,DateTime overtime,string strid,string sender,string stravname)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[12]{
													new SqlParameter("@GM_UserID",SqlDbType.Int),
													new SqlParameter("@Serverip",SqlDbType.VarChar,30),
													new SqlParameter("@prefixName",SqlDbType.VarChar,30),
													new SqlParameter("@Passwd",SqlDbType.VarChar,50),
													new SqlParameter("@FJ_City",SqlDbType.VarChar,50),
													new SqlParameter("@startNum",SqlDbType.Int),
													new SqlParameter("@endNum",SqlDbType.Int),
													new SqlParameter("@Overtime",SqlDbType.DateTime),
													new SqlParameter("@StrGuid",SqlDbType.VarChar,200),
													new SqlParameter("@Sender",SqlDbType.VarChar,200),
													new SqlParameter("@avname",SqlDbType.VarChar,200),
													new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = operateUserID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = prefixName;
				paramCode[3].Value = passWord;
				paramCode[4].Value = City;
				paramCode[5].Value = startNum;
				paramCode[6].Value = endNum;
				paramCode[7].Value = overtime;
				paramCode[8].Value = strid;
				paramCode[9].Value = sender;
				paramCode[10].Value = stravname;
				paramCode[11].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("sdo_CreateTempAccount",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		public static int RcCreatePlayerAccount(int operateUserID,string serverIP,string prefixName,string passWord,string City,int startNum,int endNum,DateTime overtime,string strid,string sender,string stravname,string truepwd)
		{
			int result = -1;
			SqlParameter[] paramCode;
			MD5Encrypt st = new MD5Encrypt();
			try
			{
				paramCode = new SqlParameter[13]{
													new SqlParameter("@GM_UserID",SqlDbType.Int),
													new SqlParameter("@Serverip",SqlDbType.VarChar,30),
													new SqlParameter("@prefixName",SqlDbType.VarChar,30),
													new SqlParameter("@Passwd",SqlDbType.VarChar,50),
													new SqlParameter("@FJ_City",SqlDbType.VarChar,50),
													new SqlParameter("@startNum",SqlDbType.Int),
													new SqlParameter("@endNum",SqlDbType.Int),
													new SqlParameter("@Overtime",SqlDbType.DateTime),
													new SqlParameter("@StrGuid",SqlDbType.VarChar,200),
													new SqlParameter("@Sender",SqlDbType.VarChar,200),
													new SqlParameter("@avname",SqlDbType.VarChar,200),
													new SqlParameter("@truepwd",SqlDbType.VarChar,200),
													new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = operateUserID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = prefixName;
				paramCode[3].Value = st.getMD5ofStr(passWord).ToLower();;
				paramCode[4].Value = City;
				paramCode[5].Value = startNum;
				paramCode[6].Value = endNum;
				paramCode[7].Value = overtime;
				paramCode[8].Value = strid;
				paramCode[9].Value = sender;
				paramCode[10].Value = stravname;
				paramCode[11].Value = truepwd;
				paramCode[12].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("Raycity_CreateTempAccount",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		public static DataSet IsFJAccount_Query(string serverIP,string city,string account)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_City",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = city;
				paramCode[2].Value = account;
				result = SqlHelper.ExecSPDataSet("FJ_IsCreateAccount_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		public static DataSet IsSdoAccount_Query(string serverIP,string account)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverIP;
			
				paramCode[1].Value = account;
				result = SqlHelper.ExecSPDataSet("Sdo_IsCreateAccount_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		public static DataSet IsRcAccount_Query(string serverIP,string account)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												
												   new SqlParameter("@FJ_Account",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverIP;
			
				paramCode[1].Value = account;
				result = SqlHelper.ExecSPDataSet("Rc_IsCreateAccount_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		public static DataSet TempAccount_Query(string gamename,string avname)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@Gamename",SqlDbType.VarChar,50),
												   new SqlParameter("@Avname",SqlDbType.VarChar,50)};
											
				paramCode[0].Value = gamename;
				paramCode[1].Value = avname;

				result = SqlHelper.ExecSPDataSet("Query_CreateTempAccont",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog(ex.Message);
			}
			return result;
		}
		public static DataSet CreateFJAccount_Query(string StrGuid)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@StrGuid",SqlDbType.VarChar,200)};
										
				paramCode[0].Value = StrGuid;
	
				result = SqlHelper.ExecSPDataSet("FJ_CreateAccounts_Query",paramCode);
			}
			catch(SqlException ex)
			{
				Console.WriteLine(ex.Message);
			}
			return result;
		}

		#region 玩家积分扣除记录
		/// <summary>
		/// 玩家积分扣除记录
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet FJUserConsume_Query(string account)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{ new SqlParameter("@UserID",SqlDbType.VarChar,30)};
				paramCode[0].Value=account;
				result = SqlHelper.ExecSPDataSet("FJUserPonit_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+"192.168.0.125"+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查询并区被物理删除的角色信息
		/// <summary>
		/// 查询并区被物理删除的角色信息
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="char_name"></param>
		/// <returns></returns>
		public static DataSet FJ_DeletedChar_Query(string serverIP,string char_name,string userid)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@FJ_serverIP",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_UserID",SqlDbType.VarChar,50),
													new SqlParameter("@FJ_NickName",SqlDbType.VarChar,50)
											   };
				paramCode[0].Value=serverIP;
				paramCode[1].Value=userid;
				paramCode[2].Value=char_name;

				result = SqlHelper.ExecSPDataSet("FJ_DelCharinfo_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;

		}
		#endregion



    }
}
