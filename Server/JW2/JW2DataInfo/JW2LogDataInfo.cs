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

namespace GM_Server.JW2DataInfo
{
	/// <summary>
	/// JW2LogDataInfo 的摘要说明。
	/// </summary>
	public class JW2LogDataInfo
	{
		public JW2LogDataInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 查看购物，送礼
		/// <summary>
		/// 查看购物，送礼
		/// </summary>
		public static DataSet SMALL_PRESENT_QUERY(string serverIP,int usersn,int type)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,4);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_SMALL_PRESENT_QUERY' and sql_condition='JW2_SMALL_PRESENT_QUERY'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					if(type==1)
					{
						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,usersn,"M","G");
					}
					else if(type==2)
					{
						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,usersn,"M","M");
					}
					else if(type==3)
					{
						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,usersn,"G","G");
					}
					
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2logDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_SMALL_PRESENT_QUERY_查看玩家"+usersn.ToString()+"购物，送礼服务器IP"+serverIP+type.ToString()+ex.Message);
			}
			return result;
		}
		#endregion

		#region 消费日志
		/// <summary>
		/// 消费日志
		/// </summary>
		public static DataSet CashMoney_Log(string serverIP,int usersn,int type,string BeginTime,string EndTime)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				if(type==6)
				{
					int ZoneID = CommonInfo.JW2_ServerIPToZoneID(serverIP);
					sql = "select sql_statement from sqlexpress where sql_type='JW2_Center_BuyInfo' and sql_condition='JW2_Center_BuyInfo'";
					System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,ZoneID,usersn,BeginTime,EndTime);
						result = CommonInfo.RunOracle(sql,ZoneID);
					}
				}
				else if(type==15)
				{
					serverIP = CommonInfo.JW2_FindDBIP(serverIP,4);
					sql = "select sql_statement from sqlexpress where sql_type='jw2_wedding_log_Query' and sql_condition='jw2_wedding_log_Query'";
					System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,usersn,type,BeginTime,EndTime);
						result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2logDB),sql);
					}
				}
				else
				{
					serverIP = CommonInfo.JW2_FindDBIP(serverIP,4);
					sql = "select sql_statement from sqlexpress where sql_type='JW2_CashMoney_Log' and sql_condition='JW2_CashMoney_Log'";
					System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,usersn,type,BeginTime,EndTime);
						result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2logDB),sql);
					}
				}
				
				
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_CashMoney_Log_查看玩家"+usersn.ToString()+"消费日志服务器IP"+serverIP+type.ToString()+"开始时间"+BeginTime+"结束时间"+EndTime+ex.Message);
			}
			return result;
		}
		#endregion

		#region 购买日志
		/// <summary>
		/// 购买日志
		/// </summary>
		public static DataSet MoneyLog_Query(string serverIP,int usersn,int goodstype,string BeginTime,string EndTime,int type,string itemName)
		{
			DataSet result = new DataSet();;
			int itemID = 0;
			string sql = "";
			int zone = 0;
			string serverName = "";
			try
			{
				serverName = CommonInfo.JW2_FindDBName(serverIP);
				zone = CommonInfo.JW2_GetZone_Query(13,serverName);
				if(serverName=="华北一区"||serverName=="华东一区"||serverName=="华南一区"||serverName=="西南一区")
				{
					serverIP = CommonInfo.JW2_FindDBIP(serverIP,4);
					sql = "select sql_statement from sqlexpress where sql_type='JW2_AgainBuyLog_Query_new' and sql_condition='"+type+"'";
					System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						if(itemName =="")
						{
							if(goodstype==1)
							{
								sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
								sql = string.Format(sql,usersn,"M","G","C",BeginTime,EndTime,"");
							}
							else if(goodstype==2)
							{
								sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
								sql = string.Format(sql,usersn,"M","M","M",BeginTime,EndTime,"");
							}
							else if(goodstype==3)
							{
								sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
								sql = string.Format(sql,usersn,"G","G","G",BeginTime,EndTime,"");
							}
							else if(goodstype==4)
							{
								sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
								sql = string.Format(sql,usersn,"C","C","C",BeginTime,EndTime,"");
							}
							result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2logDB),sql);
						}
						else
						{
							System.Data.DataSet ds1 = CommonInfo.JW2_ProductNameToID(itemName);
							for(int i = 0; i<ds1.Tables[0].Rows.Count;i++)
							{
								itemID = int.Parse(ds1.Tables[0].Rows[i].ItemArray[0].ToString());
								string str = "and goodsindex="+itemID.ToString();
								if(goodstype==1)
								{
									sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
									sql = string.Format(sql,usersn,"M","G","C",BeginTime,EndTime,str);
								}
								else if(goodstype==2)
								{
									sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
									sql = string.Format(sql,usersn,"M","M","M",BeginTime,EndTime,str);
								}
								else if(goodstype==3)
								{
									sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
									sql = string.Format(sql,usersn,"G","G","G",BeginTime,EndTime,str);
								}
								else if(goodstype==4)
								{
									sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
									sql = string.Format(sql,usersn,"C","C","C",BeginTime,EndTime,"");
								}
								result.Merge(MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2logDB),sql));
							}
						}
					}
				}
				else
				{
					sql = "select sql_statement from sqlexpress where sql_type='JW2_AgainBuyLog_Oracle_Query' and sql_condition='"+type+"'";
					System.Data.DataSet ds2 = SqlHelper.ExecuteDataset(sql);
					if(ds2!=null && ds2.Tables[0].Rows.Count>0)
					{
						if(itemName =="")
						{
							if(goodstype==1)
							{
								sql = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
								sql = string.Format(sql,usersn,"M","G",BeginTime,EndTime,"",zone);
							}
							else if(goodstype==2)
							{
								sql = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
								sql = string.Format(sql,usersn,"M","M",BeginTime,EndTime,"",zone);
							}
							else if(goodstype==3)
							{
								sql = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
								sql = string.Format(sql,usersn,"G","G",BeginTime,EndTime,"",zone);
							}
							result = CommonInfo.RunOracle(sql,SqlHelper.oracleData,SqlHelper.oracleUser,SqlHelper.oraclePwd);
						}
						else
						{
							System.Data.DataSet ds3 = CommonInfo.JW2_ProductNameToID(itemName);
							for(int i = 0; i<ds3.Tables[0].Rows.Count;i++)
							{
								itemID = int.Parse(ds3.Tables[0].Rows[i].ItemArray[0].ToString());
								string str = "and buy_goods="+itemID.ToString();
								if(goodstype==1)
								{
									sql = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
									sql = string.Format(sql,usersn,"M","G",BeginTime,EndTime,str,zone);
								}
								else if(goodstype==2)
								{
									sql = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
									sql = string.Format(sql,usersn,"M","M",BeginTime,EndTime,str,zone);
								}
								else if(goodstype==3)
								{
									sql = ds2.Tables[0].Rows[0].ItemArray[0].ToString();
									sql = string.Format(sql,usersn,"G","G",BeginTime,EndTime,str,zone);
								}
								result = CommonInfo.RunOracle(sql,SqlHelper.oracleData,SqlHelper.oracleUser,SqlHelper.oraclePwd);
							}
						}
					}
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_MoneyLog_Query_查看玩家"+usersn.ToString()+"购买日志服务器IP"+serverIP+type.ToString()+"开始时间"+BeginTime+"结束时间"+EndTime+goodstype.ToString()+ex.Message);
			}
			return result;
		}
		#endregion

		#region 中间件背包查询
		/// <summary>
		/// 中间件背包查询
		/// </summary>
		public static DataSet CenterAvAtarItem_Bag_Query(string serverIP,int usersn)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				int zoneID = CommonInfo.JW2_ServerIPToZoneID(serverIP);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_Gtavatar_Item' and sql_condition='JW2_Gtavatar_Item'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn,zoneID);
					result = CommonInfo.RunOracle(sql,zoneID);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_CenterAvAtarItem_Bag_Query_查看玩家"+usersn.ToString()+"中间件背包查询服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 中间件身上道具查询
		/// <summary>
		/// 中间件身上道具查询
		/// </summary>
		public static DataSet CenterAvAtarItem_Equip_Query(string serverIP,int usersn)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				int zoneID = CommonInfo.JW2_ServerIPToZoneID(serverIP);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_Gtavatar_Equip' and sql_condition='JW2_Gtavatar_Equip'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn);
					result = CommonInfo.RunOracle(sql,zoneID);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_CenterAvAtarItem_Equip_Query_查看玩家"+usersn.ToString()+"中间件身上道具查询服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查看举报信息
		/// <summary>
		/// 查看举报信息
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="userSN">用户ID</param>
		/// <returns></returns>
		public static DataSet JB_Query(string serverIP,int type,string typeName,string BeginTime,string EndTime)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,4);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_JB_Query' and sql_condition='JW2_JB_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,type,typeName);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2logDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_JB_Query_查看举报信息服务器IP"+serverIP+"->"+type.ToString()+"->"+typeName+"开始时间"+BeginTime+"结束时间"+EndTime+ex.Message);
			}
			return result;
		}
		#endregion

		#region 任务日志查询
		/// <summary>
		/// 任务日志查询
		/// </summary>
		public static DataSet MissionInfoLog_Query(string serverIP,int usersn,int type,string BeginTime,string EndTime)
		{
			DataSet result = null;
			string sql = "";
			try
			{	
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,4);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_MissionInfoLog_Query' and sql_condition='"+type+"'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn,type,BeginTime,EndTime);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2logDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_MissionInfoLog_Query_查看玩家"+usersn.ToString()+"任务日志查询服务器IP"+serverIP+"->"+type.ToString()+"开始时间"+BeginTime+"结束时间"+EndTime+ex.Message);
			}
			return result;
		}
		#endregion

		#region 重復購買退款
		/// <summary>
		/// 重復購買退款
		/// </summary>
		public static int AgainBuy_Query(string serverIP,int buySN,int userSN,int userByID,int cash,string userID,string itemName)
		{
			int  result = -1;
			string sql = null;
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,8);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_RevBuyCash_Query' and sql_condition = 'JW2_RevBuyCash_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,userSN,cash,System.DateTime.Now.ToString());
					result = MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2loginDB),sql);
				}
				if(result==1)
				{
					serverIP = CommonInfo.JW2_FindDBIP(serverIP,4);
					sql = "select sql_statement from sqlexpress where sql_type='JW2_DELBuyLog_Query' and sql_condition = 'JW2_DELBuyLog_Query'";
					System.Data.DataSet ds_local = SqlHelper.ExecuteDataset(sql);
					if(ds_local!=null && ds_local.Tables[0].Rows.Count>0)
					{	
						sql = ds_local.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,buySN);
						result = MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2logDB),sql);
					}
					if(result==1)
						SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_RevBuyCash_Query","用户："+userID.ToString()+"，重复购买道具"+itemName.ToString()+"退款M币："+cash.ToString()+"，成功(重复购买退款,jw2)");
					else
						SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_RevBuyCash_Query","用户："+userID.ToString()+"，重复购买道具"+itemName.ToString()+"退款M币："+cash.ToString()+"，失败(重复购买退款,jw2)");
				}
				else
				{
					SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_RevBuyCash_Query","用户："+userID.ToString()+"，重复购买道具"+itemName.ToString()+"退款M币："+cash.ToString()+"，失败(重复购买退款,jw2)");
				}
			}
			catch (MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_AgainBuy_Query_查看玩家"+userSN.ToString()+"-"+buySN.ToString()+"重復購買退款服务器IP"+serverIP+"用户名"+userID+"道具名"+itemName+"开始时间"+ex.Message);
			}
			return result;
		}
		#endregion

		
	}
}
