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
	/// JW2ItemDataInfo 的摘要说明。
	/// </summary>
	public class JW2ItemDataInfo
	{
		public JW2ItemDataInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 查询故事情节
		/// <summary>
		/// 查询故事情节
		/// </summary>
		public static DataSet RPG_QUERY(string serverIP,int usersn,string userName)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,2);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_RPG_QUERY' and sql_condition='JW2_RPG_QUERY'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2itemDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_RPG_QUERY_查看玩家"+usersn.ToString()+"-"+userName+"故事情节服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查看玩家身上道具信息
		/// <summary>
		/// 查看玩家身上道具信息
		/// </summary>
		public static DataSet ITEMSHOP_BYOWNER_QUERY(string serverIP,int usersn)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,2);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_ITEMSHOP_BYOWNER_QUERY' and sql_condition='JW2_ITEMSHOP_BYOWNER_QUERY'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2itemDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_ITEMSHOP_BYOWNER_QUERY_查看玩家"+usersn.ToString()+"身上道具信息服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查看房间物品清单与期限
		/// <summary>
		/// 查看房间物品清单与期限
		/// </summary>
		public static DataSet HOME_ITEM_QUERY(string serverIP,int usersn)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,2);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_HOME_ITEM_QUERY' and sql_condition='JW2_HOME_ITEM_QUERY'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2itemDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_HOME_ITEM_QUERY_查看玩家"+usersn.ToString()+"房间物品清单与期限服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 查看消耗性道具
		/// <summary>
		/// 查看消耗性道具
		/// </summary>
		public static DataSet WASTE_ITEM_QUERY(string serverIP,int usersn)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,2);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_WASTE_ITEM_QUERY' and sql_condition='JW2_WASTE_ITEM_QUERY'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2itemDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_WASTE_ITEM_QUERY_查看玩家"+usersn.ToString()+"消耗性道具服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		
		#region 查看小喇叭
		/// <summary>
		/// 查看小喇叭
		/// </summary>
		public static DataSet SMALL_BUGLE_QUERY(string serverIP,int usersn,string startTime,string endTime)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,2);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_SMALL_BUGLE_QUERY' and sql_condition='JW2_SMALL_BUGLE_QUERY'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn,startTime,endTime);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2itemDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_SMALL_BUGLE_QUERY_查看玩家"+usersn.ToString()+"小喇叭服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 道具查询
		/// <summary>
		/// 道具查询
		/// </summary>
		public static DataSet ItemInfo_Query(string serverIP,int usersn,int type)
		{
			DataSet result = null;
			string sql = "";
			string db = "";
			try
			{
				switch(type)
				{
					case 0://身上
					{
						serverIP = CommonInfo.JW2_FindDBIP(serverIP,2);
						db = SqlHelper.jw2itemDB;
						break;
					}
					case 1://物品栏
					{
						serverIP = CommonInfo.JW2_FindDBIP(serverIP,2);
						db = SqlHelper.jw2itemDB;
						break;
					}
					case 2://礼物栏
					{
						serverIP = CommonInfo.JW2_FindDBIP(serverIP,6);
						db = SqlHelper.jw2messengerDB;
						break;
					}	
				}
				sql = "select sql_statement from sqlexpress where sql_type='JW2_ItemInfo_Query"+type+"' and sql_condition='JW2_ItemInfo_Query"+type+"'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,db),sql);
				}
				
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_ItemInfo_Query_查看玩家"+usersn.ToString()+"道具查询服务器IP"+serverIP+type.ToString()+ex.Message);
			}
			return result;
		}
		#endregion

		#region 删除道具
		/// <summary>
		/// 删除道具
		/// </summary>
		public static int ITEM_DEL(string serverIP,int usersn,int userByID,string UserName,int itemID,string itemName,int type,int itemNo,ref string strDesc)
		{
			int  result = -1;
			string sql = null;
			string db = "";
			try
			{

				switch(type)
				{
					case 0://身上
					{
						serverIP = CommonInfo.JW2_FindDBIP(serverIP,10);
						db = SqlHelper.jw2itemDB;
						break;
					}
					case 1://物品栏
					{
						serverIP = CommonInfo.JW2_FindDBIP(serverIP,10);
						db = SqlHelper.jw2itemDB;
						break;
					}
					case 2://礼物栏
					{
						serverIP = CommonInfo.JW2_FindDBIP(serverIP,9);
						db = SqlHelper.jw2messengerDB;
						break;
					}	
				}

				sql = "select sql_statement from sqlexpress where sql_type='JW2_ITEM_DEL"+type+"' and sql_condition='JW2_ITEM_DEL"+type+"'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					if(type==2)
					{
						sql = string.Format(sql,itemNo,itemID,usersn);
					}
					else
					{
						sql = string.Format(sql,itemID,usersn);
					}
					result = MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,db),sql);
				}
				if(result==1)
				{
					strDesc = "删除玩家"+UserName.ToString()+"道具"+itemName.ToString()+"，成功，请稍等，系统处理中！";
					SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_ITEM_DEL","删除玩家："+UserName.ToString()+"，道具："+itemName.ToString()+"，成功");
				}
				else
				{
					strDesc = "删除玩家"+UserName.ToString()+"道具"+itemName.ToString()+"，失败，请游戏中确认此道具是否存在！";
					SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_ITEM_DEL","删除玩家："+UserName.ToString()+"，道具："+itemName.ToString()+"，失败");
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

		#region 添加道具
		/// <summary>
		/// 添加道具
		/// </summary>
		public static string ITEM_ADD(string serverIP,int usersn,int userByID,string UserName,string itemName,string strMailTitle,string strMailContent)
		{
			int  result = 0;
			string get_result = "";
			string sql = null;
			int itemID = 0;
			int itemNum = 0;
			string itemLimit = "";
			string itemN = "";
			string ItemSex = "";
			int count = 0;

			try
			{
				string[] item=itemName.Split('|');
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,9);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_ADD_ITEM' and sql_condition='JW2_ADD_ITEM'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					for(int i=0;i<item.Length-1;i++)
					{
						result = 0;
						itemID = int.Parse(item[i].Split(',')[0].ToString());
						itemNum = int.Parse(item[i].Split(',')[1].ToString());
						itemN = CommonInfo.JW2_ProductIDToName(itemID);
						ItemSex = CommonInfo.JW2_ItemID_Sex(itemID);
						switch(CommonInfo.JW2_ItemCodeToLimitDay(itemID))
						{
							case 0:
								itemLimit = "永久";
								break;
							case 7:
								itemLimit = "7天";
								break;
							case 30:
								itemLimit = "30天";
								break;
						}
						for(int j = 0;j<itemNum;j++)
						{
							sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
							sql = string.Format(sql,usersn,strMailTitle,strMailContent,itemID);
							result += MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2messengerDB),sql);
						}

						if(result >0)
						{
							get_result += "添加玩家：【"+UserName.ToString()+"】，道具ID：【"+itemID.ToString()+"】，发送性别：【"+ItemSex.ToString()+"】，道具：【"+itemN.ToString()+"】，数量：【"+result+"】，成功\n";
							SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_ADD_ITEM","添加玩家："+UserName.ToString()+"，道具ID："+itemID.ToString()+"，发送性别："+ItemSex.ToString()+"，道具："+itemN.ToString()+"，道具数量："+result+"，道具期限："+itemLimit+"，成功");
						}
						else
						{
							get_result += "添加玩家：【"+UserName.ToString()+"】，道具ID：【"+itemID.ToString()+"】，发送性别：【"+ItemSex.ToString()+"】，道具：【"+itemN.ToString()+"】，数量：【"+result+"】，失败\n";
							SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_ADD_ITEM","添加玩家："+UserName.ToString()+"，道具ID："+itemID.ToString()+"，发送性别："+ItemSex.ToString()+"，道具："+itemN.ToString()+"，道具数量："+result+"，道具期限："+itemLimit+"，失败");
						}
					}
				}
			}
			catch (MySqlException ex)
			{
				get_result +="数据库连接失败，请稍后尝试";
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return get_result;
		}
		#endregion

		#region 添加道具(批量)
		/// <summary>
		/// 添加道具(批量)
		/// </summary>
		public static string ITEM_ADD_ALL(string serverIP,int userByID,string itemName,string strMailTitle,string strMailContent)
		{
			int  result = 0;
			int userSN = 0;
			string UserName = "";
			string get_result = "";
			string sql = null;
			int itemID = 0;
			int itemNum = 0;
			string itemLimit = "";
			string itemN = "";
			int count = 0;
			string ItemSex = "";
			try
			{
				string[] item=itemName.Split('|');
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,9);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_ADD_ITEM' and sql_condition='JW2_ADD_ITEM'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					for(int i=0;i<item.Length-1;i++)
					{
						result = 0;
						UserName = item[i].Split(',')[0].ToString();
						userSN = CommonInfo.JW2_Account_UserSn(serverIP,UserName);
						if(userSN!=0)
						{
							itemID = int.Parse(item[i].Split(',')[1].ToString());
							itemNum = int.Parse(item[i].Split(',')[2].ToString());
							itemN = CommonInfo.JW2_ProductIDToName(itemID);
							ItemSex = CommonInfo.JW2_ItemID_Sex(itemID);
							switch(CommonInfo.JW2_ItemCodeToLimitDay(itemID))
							{
								case 0:
									itemLimit = "永久";
									break;
								case 7:
									itemLimit = "7天";
									break;
								case 30:
									itemLimit = "30天";
									break;
							}
							for(int j = 0;j<itemNum;j++)
							{
								sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
								sql = string.Format(sql,userSN,strMailTitle,strMailContent,itemID);
								result += MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2messengerDB),sql);
							}
							if(result >0)
							{
								get_result += "添加玩家：【"+UserName.ToString()+"】，道具ID：【"+itemID.ToString()+"】，发送性别：【"+ItemSex.ToString()+"】，道具：【"+itemN.ToString()+"】，数量：【"+result+"】，成功\n";
								SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_ITEM_ADD_ALL","添加玩家："+UserName.ToString()+"，道具ID："+itemID.ToString()+"，发送性别："+ItemSex.ToString()+"，道具："+itemN.ToString()+"，道具数量："+result+"，道具期限："+itemLimit+"，成功");
							}
							else
							{
								get_result += "添加玩家：【"+UserName.ToString()+"】，道具ID：【"+itemID.ToString()+"】，发送性别：【"+ItemSex.ToString()+"】，道具：【"+itemN.ToString()+"】，数量：【"+result+"】，成功\n";
								SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_ITEM_ADD_ALL","添加玩家："+UserName.ToString()+"，道具ID："+itemID.ToString()+"，发送性别："+ItemSex.ToString()+"，道具："+itemN.ToString()+"，道具数量："+result+"，道具期限："+itemLimit+"，成功");
							}
						}
						else
						{
							get_result += "玩家：【"+UserName.ToString()+"】，不存在\n";
							SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_ITEM_ADD_ALL","玩家：【"+UserName.ToString()+"】，不存在");
						}
					}
				}
			}
			catch (MySqlException ex)
			{
				get_result +="数据库连接失败，请稍后尝试";
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return get_result;
		}
		#endregion

		#region 道具查询(模糊查询)
		/// <summary>
		/// 道具查询(模糊查询)
		/// </summary>
		public static DataSet ITEM_SELECT(string itenName,string typeflag,int type)
		{

			DataSet result = null;
			string sql = "select sql_statement from sqlexpress where sql_type='JW2_GetItemList' and sql_condition='"+type+"'";
			System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
			if(ds!=null && ds.Tables[0].Rows.Count>0)
			{
				sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
				sql = string.Format(sql,itenName,typeflag);
				result = SqlHelper.ExecuteDataset(sql);
			}
			return result;
		}
		#endregion

		#region 好友信息查询
		/// <summary>
		/// 好友信息查询
		/// </summary>
		public static DataSet JW2_FriendList_Query(string serverIP,int usersn)
		{
			DataSet result = null;
			string sql = "";
			string db = "";
			try
			{
				sql = "select sql_statement from sqlexpress where sql_type='JW2_FriendList_Query' and sql_condition='JW2_FriendList_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,db),sql);
				}
				
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_JW2_FriendList_Query_查看玩家"+usersn.ToString()+"好友信息查询服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 宠物信息查询
		/// <summary>
		/// 宠物信息查询
		/// </summary>
		public static DataSet PetInfo_Query(string serverIP,int usersn)
		{
			DataSet result = null;
			string sql = "";
			string db = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,2);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_PetInfo_Query' and sql_condition='JW2_PetInfo_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2itemDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_PetInfo_Query_查看玩家"+usersn.ToString()+"宠物信息查询服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 修改宠物名
		/// <summary>
		/// 修改家族名
		/// </summary>
		public static int UpdatePetName_Query(string serverIP,string OLD_petName,string petName,int userByID,int petID,ref string strDesc)
		{
			int  result = -1;
			string sql = null;
			try
			{
				//修改等级1
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,10);//9);//maple
				sql = "select sql_statement from sqlexpress where sql_type='JW2_UpdatePETName_Query' and sql_condition = 'JW2_UpdatePETName_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{	
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,petID,petName);
					result = MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2itemDB),sql);
				}
				if(result==1)
				{
					strDesc = "修改宠物名【"+OLD_petName+"】为新宠物名【"+petName+"】成功，请稍等，系统处理中！";
					SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_UpdatePETName_Query","修改宠物名："+OLD_petName.ToString()+"，为新宠物名："+petName.ToString()+"，成功");
				}
				else
				{
					strDesc = "修改宠物名【"+OLD_petName+"】为新宠物名【"+petName+"】失败，请确定游戏中宠物是否存在！";
					SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_UpdatePETName_Query","修改宠物名："+OLD_petName.ToString()+"，为新宠物名："+OLD_petName.ToString()+"，失败");
				}
			}
			catch (MySqlException ex)
			{
				strDesc = "数据库连接失败，请稍后尝试！";
				SqlHelper.errLog.WriteLog("劲舞团2_UpdatePetName_Query修改宠物名【"+OLD_petName+"】为新宠物名【"+petName+"】"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 合成材料查詢
		/// <summary>
		/// 合成材料查詢
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet Materiallist_Query(string serverIP,int usersn)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,2);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_Materiallist_Query' and sql_condition='JW2_Materiallist_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2itemDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_Materiallist_Query_查看玩家"+usersn.ToString()+"合成材料查詢服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 合成历史记录查询
		/// <summary>
		/// 合成历史记录查询
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet MaterialHistory_Query(string serverIP,int usersn)
		{
			DataSet result = null;
			string sql = "";
			try
			{
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,2);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_MaterialHistory_Query' and sql_condition='JW2_MaterialHistory_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn);
					result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2itemDB),sql);
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_MaterialHistory_Query_查看玩家"+usersn.ToString()+"合成历史记录查询服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 获得需要审核的图片列表
		/// <summary>
		/// 获得需要审核的图片列表
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet GETPIC_Query(string serverIP,string account)
		{
			DataSet result = null;
			string sql = "";
			string str = "";
			try
			{
				if(account!="")
				{
					int UserSn = CommonInfo.JW2_Account_UserSn(serverIP,account);
					str = "and usersn="+UserSn;
				}
				serverIP = CommonInfo.JW2_FindDBIP(serverIP,2);
				sql = "select sql_statement from sqlexpress where sql_type='JW2_GETPIC_Query_Query' and sql_condition='JW2_GETPIC_Query_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,str);
					if(serverIP!="114.80.167.192,3306")
						result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2itemDB),sql);
					else
						result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,"gmtools","E#.92lG^$kd)205K",SqlHelper.jw2itemDB),sql);

				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("浏览JW2_GETPIC_Query_查看玩家"+account+"获得需要审核的图片列表服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion

		#region 审核图片
		/// <summary>
		/// 审核图片
		/// </summary>
		public static int CHKPIC_Query(string serverIP,int usersn,int userByID,string UserName,string Url,int type,ref string strDesc)
		{
			int  result = -1;
			string sql = null;
			string db = "";
			string Pic_Name = "";
			string str = "";
			try
			{
				if(type==2)
					str = "审核通过";
				else
					str = "审核不通过";

				serverIP = CommonInfo.JW2_FindDBIP(serverIP,10);
				sql = "select sql_statement from sqlexpress where sql_type='jw2_CHKPIC_Query' and sql_condition='jw2_CHKPIC_Query'";
				System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					sql = string.Format(sql,usersn,Url,type);
					
					result = MySqlHelper.ExecuteNonQuery(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2itemDB),sql);
				}
				if(result==1)
				{
					strDesc = "审核玩家"+UserName.ToString()+"图片"+Url.ToString()+"，"+str+"，成功，请稍等，系统处理中！";
					SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_MODIFYLEVEL_QUERY","删除玩家："+UserName.ToString()+"，道具："+Url.ToString()+"，"+str+"，成功");
				}
				else
				{
					strDesc = "审核玩家"+UserName.ToString()+"图片"+Url.ToString()+"，"+str+"，失败，请游戏中确认此道具是否存在！";
					SqlHelper.insertGMtoolsLog(userByID,"劲舞团II",serverIP,"JW2_MODIFYLEVEL_QUERY","审核图片："+UserName.ToString()+"，道具："+Url.ToString()+"，"+str+"，失败");
				}
			}
			catch (MySqlException ex)
			{
				strDesc = "数据库连接失败，请重新尝试！";
				SqlHelper.errLog.WriteLog("审核图片->JW2_CHKPIC_Query->服务器IP->"+serverIP+"->帐号->"+UserName+"-"+usersn.ToString()+"->图片->"+Url+"->"+ex.Message);
			}
			return result;
		}
		#endregion

		#region 圖片卡使用情況
		/// <summary>
		/// 圖片卡使用情況
		/// </summary>
		/// <param name="serverIP">服务器Ip</param>
		/// <param name="account">帐号名</param>
		/// <returns></returns>
		public static DataSet PicCard_Query(string serverIP,int BType,int SType, int usersn,string BeginTime,string EndTime)
		{
			DataSet result = null;
			string sql = "";
			string str = "";
			try
			{
				if(BType==1)
				{
					serverIP = CommonInfo.JW2_FindDBIP(serverIP,2);
					sql = "select sql_statement from sqlexpress where sql_type='jw2_PicCard_Query' and sql_condition='jw2_PicCard_Query'";
					System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,usersn,BeginTime,EndTime);
						result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2itemDB),sql);
					}
				}
				else if(BType==2)
				{
					serverIP = CommonInfo.JW2_FindDBIP(serverIP,4);
					sql = "select sql_statement from sqlexpress where sql_type='jw2_Garden_Log_Query' and sql_condition='"+SType+"'";
					System.Data.DataSet ds = SqlHelper.ExecuteDataset(sql);
					if(ds!=null && ds.Tables[0].Rows.Count>0)
					{
						sql = ds.Tables[0].Rows[0].ItemArray[0].ToString();
						sql = string.Format(sql,usersn,BeginTime,EndTime);
						result = MySqlHelper.ExecuteDataset(SqlHelper.JW2GetConnectionString(serverIP,SqlHelper.jw2User,SqlHelper.jw2UserPwd,SqlHelper.jw2logDB),sql);
					}
				}
			}
			catch(MySqlException ex)
			{
				SqlHelper.errLog.WriteLog("获得圖片卡使用情況->JW2_PicCard_Query->服务器IP->"+serverIP+"->帐号->"+usersn.ToString()+"->"+ex.Message);
			}
			return result;
		}
		#endregion
		
		

	}
}
