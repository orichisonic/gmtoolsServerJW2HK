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
using Common.DataInfo;
namespace RayCity.RayCityDataInfo
{
	/// <summary>
	/// RayCityCharacterInfo 的摘要说明。
	/// </summary>
	public class CharacterInfo
	{
		public CharacterInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 查看玩家资料
		/// <summary>
		/// 查看玩家资料
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet characterInfo_Query(string serverIP,int characterID,int accountID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AccountID",SqlDbType.VarChar,20),
												   new SqlParameter("@CharacterID",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = accountID;
				paramCode[2].Value = characterID;
				result = SqlHelper.ExecSPDataSet("RayCity_CharacterInfo",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家状态信息
		/// <summary>
		/// 查看玩家状态信息
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet characterStateInfo_Query(string serverIP,int characterID,int accountID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@AccountID",SqlDbType.VarChar,20),
												   new SqlParameter("@CharacterID",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = accountID;
				paramCode[2].Value = characterID;
				result = SqlHelper.ExecSPDataSet("RayCity_CharacterStateInfo",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家车的信息
		/// <summary>
		/// 查看玩家车的信息
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet CarInfo_Query(string serverIP,int characterID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@CharacterID",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = characterID;
				result = SqlHelper.ExecSPDataSet("RayCity_CarInfo",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家车装备的信息
		/// <summary>
		/// 查看玩家车装备的信息
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet CarInventoryInfo_Query(string serverIP,int characterID,int garageIDX)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@characterID",SqlDbType.Int),
												   new SqlParameter("@GarageIDX",SqlDbType.Int)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = characterID;
				paramCode[2].Value = garageIDX;
				result = SqlHelper.ExecSPDataSet("RayCity_CarInventoryInfo",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家连接状态
		/// <summary>
		/// 查看玩家连接状态
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet ConnectionStateInfo_Query(string serverIP,int characterID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@CharacterID",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = characterID;
				result = SqlHelper.ExecSPDataSet("RayCity_ConnectionStateInfo",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家的好友列表
		/// <summary>
		/// 查看玩家的好友列表
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet FriendList_Query(string serverIP,int characterID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@CharacterID",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = characterID;
				result = SqlHelper.ExecSPDataSet("RayCity_FriendInfo",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看速度信息
		/// <summary>
		/// 查看速度信息
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static DataSet RaceState_Query(string serverIP,int characterID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@CharacterID",SqlDbType.VarChar,20)};
				paramCode[0].Value = serverIP;
				paramCode[1].Value = characterID;
				result = SqlHelper.ExecSPDataSet("RayCity_RaceStateInfo",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 重置游戏初始位置
		/// <summary>
		/// 重置游戏初始位置
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static int SetResetPos(int operateUserID,string serverIP,string account,int characterID)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@GMUserID",SqlDbType.Int),
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@RayCity_Account",SqlDbType.VarChar,30),
												   new SqlParameter("@characterID",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
			    paramCode[0].Value = operateUserID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = account;
				paramCode[3].Value = characterID;
				paramCode[4].Direction = ParameterDirection.ReturnValue;;
				 result = SqlHelper.ExecSPCommand("RayCity_SetUserResetPos",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 活动需要生成玩家帐号
		/// <summary>
		/// 活动需要生成玩家帐号
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static int CreatePlayerAccount(int operateUserID,string serverIP,string prefixName,string passWord,int startNum,int endNum)
		{
			int result = -1;
			SqlParameter[] paramCode;
			MD5Encrypt st = new MD5Encrypt();;
			try
			{
				paramCode = new SqlParameter[7]{
												   new SqlParameter("@GM_UserID",SqlDbType.Int),
												   new SqlParameter("@RayCity_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@prefixName",SqlDbType.VarChar,30),
												   new SqlParameter("@Passwd",SqlDbType.VarChar,50),
												   new SqlParameter("@startNum",SqlDbType.Int),
												   new SqlParameter("@endNum",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value = operateUserID;
				paramCode[1].Value = serverIP;
				paramCode[2].Value = prefixName;
			    paramCode[3].Value = st.getMD5ofStr(passWord).ToLower();
				paramCode[4].Value = startNum;
				paramCode[5].Value = endNum;
				paramCode[6].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("RayCity_GenerateAccount1",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看活动卡信息
		/// <summary>
		/// 查看激活码信息
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static ArrayList ActiveCard_Query(string account, int actionType,ref string strDesc)
		{
			string getUser = null;
			string sign = null;
			string parameter ="";
			XmlDocument xmlfile = new XmlDocument();
			getUser =account;
			parameter = account;
			MD5Encrypt md5 = new MD5Encrypt();
			sign = md5.getMD5ofStr(actionType.ToString()+parameter+"|T4pb31.QueryRcReward").ToLower();
			try   
			{
				System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=10");
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					string serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					string url = "http://"+serverIP+"/PayCenter/QueryRcReward.php";
					HttpWebRequest  request  = (HttpWebRequest)
						WebRequest.Create(url);
					request.ContentType="application/x-www-form-urlencoded";
					request.KeepAlive=false; 
					request.Method="POST";
					//参数POST到商城的接口
					Stream writer = request.GetRequestStream(); 
					string postData="type="+actionType.ToString()+"&key="+account+"&sign="+sign+"&encoding=UTF-8";  
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
	}
}
