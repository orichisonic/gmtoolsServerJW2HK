using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
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
	/// FJItemShopInfo 的摘要说明。
	/// </summary>
	public class FJItemShopInfo
	{
		public FJItemShopInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 查看玩家身上物品道具
		/// <summary>
		/// 查看玩家身上道具信息
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet itemShop_Query(string serverIP,string charName)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_CharName",SqlDbType.VarChar,30),
				                                   new SqlParameter("@FJ_Style",SqlDbType.VarChar,30)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=charName;
				paramCode[2].Value="body";
				result = SqlHelper.ExecSPDataSet("FJ_ItemShop_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看附加道具的分类
		/// <summary>
		/// 查看附加道具的分类
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet ItemAppendProperty_Style_Query()
		{
			DataSet result = null;
			try
			{
				result = SqlHelper.ExecSPDataSet("FJ_RandEffStyle_Query");
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog(ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看游戏里附加道具信息
		/// <summary>
		/// 查看游戏里附加道具信息
		/// </summary>
		/// <returns></returns>
		public static DataSet ItemAppendProperty_Query(string style)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@FJ_Style",SqlDbType.VarChar,30)};
				paramCode[0].Value=style;
				result = SqlHelper.ExecSPDataSet("FJ_RandEff_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("函数名ItemAppendProperty_Query()"+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看游戏里附加道具列表
		/// <summary>
		/// 查看游戏里附加道具列表
		/// </summary>
		/// <returns></returns>
		public static DataSet ItemAppendPropertyList_Query(string style)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@fj_condition",SqlDbType.VarChar,100)};
				paramCode[0].Value=style;
				result = SqlHelper.ExecSPDataSet("FJ_Rand_Condition_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog(ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看游戏道具分类
		/// <summary>
		/// 查看游戏道具分类
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet itemShopStyle_Query()
		{
			DataSet result = null;
			try
			{
				result = SqlHelper.ExecuteDataset("FJ_ItemStyle_Query");
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog(ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看游戏里面所有道具
		/// <summary>
		/// 查看游戏里面所有道具
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet itemShop_QueryAll(string style,string itemName)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@FJ_Style",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_ItemName",SqlDbType.VarChar,30)};
				paramCode[0].Value=style;
				paramCode[1].Value=itemName;
				result = SqlHelper.ExecSPDataSet("FJ_ItemShop_QueryAll",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog(ex.Message);
			}
			return result;
		}
		#endregion
		#region 添加玩家身上道具
		/// <summary>
		/// 添加玩家身上道具
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int itemShop_Insert(int userByID,string serverIP,string charName,int itemCode,string message)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[6]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_CharName",SqlDbType.VarChar,50),
												   new SqlParameter("@FJ_ItemCode",SqlDbType.Int),
												   new SqlParameter("@FJ_Message",SqlDbType.VarChar,500),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=charName;
				paramCode[3].Value=itemCode;
				paramCode[4].Value=message;
				paramCode[5].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_ItemShop_Insert",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 批量添加玩家身上道具
		/// <summary>
		/// 批量添加玩家身上道具
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int itemShop_BatchInsert(int userByID,string serverIP,string charName,string itemGuid,string appentItemGuid,string embed,int slotItemNum,int slotStoneNum,int money,string title,string message)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				if(itemGuid!=null && itemGuid.ToString().Length>0)
				{
					paramCode = new SqlParameter[87]{
														new SqlParameter("@Gm_UserID",SqlDbType.Int),
														new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
														new SqlParameter("@FJ_CharName",SqlDbType.VarChar,50),
														new SqlParameter("@Item_0_guid",SqlDbType.Int),
														new SqlParameter("@Item_0_num",SqlDbType.Int),
														new SqlParameter("@Item_0_mark",SqlDbType.VarChar,500),
														new SqlParameter("@Item_0_cur_dur",SqlDbType.Decimal),
														new SqlParameter("@Item_0_max_dur",SqlDbType.Decimal),
														new SqlParameter("@Item_0_Level",SqlDbType.Int),
														new SqlParameter("@Item_0_Remain_Sec",SqlDbType.Int),
														new SqlParameter("@item_0_signature",SqlDbType.VarChar,20),
														new SqlParameter("@item_0_slots_opened",SqlDbType.Int),
														new SqlParameter("@Item_0_embed_0",SqlDbType.Int),
														new SqlParameter("@Item_0_embed_1",SqlDbType.Int),
														new SqlParameter("@Item_0_embed_2",SqlDbType.Int),
														new SqlParameter("@Item_0_embed_3",SqlDbType.Int),
														new SqlParameter("@Item_0_embed_4",SqlDbType.Int),
														new SqlParameter("@Item_0_embed_5",SqlDbType.Int),

														new SqlParameter("@Item_1_guid",SqlDbType.Int),
														new SqlParameter("@Item_1_num",SqlDbType.Int),
														new SqlParameter("@Item_1_mark",SqlDbType.VarChar,500),
														new SqlParameter("@Item_1_cur_dur",SqlDbType.Decimal),
														new SqlParameter("@Item_1_max_dur",SqlDbType.Decimal),
														new SqlParameter("@Item_1_Level",SqlDbType.Int),
														new SqlParameter("@Item_1_Remain_Sec",SqlDbType.Int),
														new SqlParameter("@item_1_signature",SqlDbType.VarChar,20),
                                                    
														new SqlParameter("@item_1_slots_opened",SqlDbType.Int),
														new SqlParameter("@Item_1_embed_0",SqlDbType.Int),
														new SqlParameter("@Item_1_embed_1",SqlDbType.Int),
														new SqlParameter("@Item_1_embed_2",SqlDbType.Int),
														new SqlParameter("@Item_1_embed_3",SqlDbType.Int),
														new SqlParameter("@Item_1_embed_4",SqlDbType.Int),
														new SqlParameter("@Item_1_embed_5",SqlDbType.Int),

														new SqlParameter("@Item_2_guid",SqlDbType.Int),
														new SqlParameter("@Item_2_num",SqlDbType.Int),
														new SqlParameter("@Item_2_mark",SqlDbType.VarChar,500),
														new SqlParameter("@Item_2_cur_dur",SqlDbType.Decimal),
														new SqlParameter("@Item_2_max_dur",SqlDbType.Decimal),
														new SqlParameter("@Item_2_Level",SqlDbType.Int),
														new SqlParameter("@Item_2_Remain_Sec",SqlDbType.Int),
														new SqlParameter("@item_2_signature",SqlDbType.VarChar,20),

														new SqlParameter("@item_2_slots_opened",SqlDbType.Int),
														new SqlParameter("@Item_2_embed_0",SqlDbType.Int),
														new SqlParameter("@Item_2_embed_1",SqlDbType.Int),
														new SqlParameter("@Item_2_embed_2",SqlDbType.Int),
														new SqlParameter("@Item_2_embed_3",SqlDbType.Int),
														new SqlParameter("@Item_2_embed_4",SqlDbType.Int),
														new SqlParameter("@Item_2_embed_5",SqlDbType.Int),

														new SqlParameter("@Item_3_guid",SqlDbType.Int),
														new SqlParameter("@Item_3_num",SqlDbType.Int),
														new SqlParameter("@Item_3_mark",SqlDbType.VarChar,500),
														new SqlParameter("@Item_3_cur_dur",SqlDbType.Decimal),
														new SqlParameter("@Item_3_max_dur",SqlDbType.Decimal),
														new SqlParameter("@Item_3_Level",SqlDbType.Int),
														new SqlParameter("@Item_3_Remain_Sec",SqlDbType.Int),
														new SqlParameter("@item_3_signature",SqlDbType.VarChar,20),

														new SqlParameter("@item_3_slots_opened",SqlDbType.Int),
														new SqlParameter("@Item_3_embed_0",SqlDbType.Int),
														new SqlParameter("@Item_3_embed_1",SqlDbType.Int),
														new SqlParameter("@Item_3_embed_2",SqlDbType.Int),
														new SqlParameter("@Item_3_embed_3",SqlDbType.Int),
														new SqlParameter("@Item_3_embed_4",SqlDbType.Int),
														new SqlParameter("@Item_3_embed_5",SqlDbType.Int),

														new SqlParameter("@Item_4_guid",SqlDbType.Int),
														new SqlParameter("@Item_4_num",SqlDbType.Int),
														new SqlParameter("@Item_4_mark",SqlDbType.VarChar,500),
														new SqlParameter("@Item_4_cur_dur",SqlDbType.Decimal),
														new SqlParameter("@Item_4_max_dur",SqlDbType.Decimal),
														new SqlParameter("@Item_4_Level",SqlDbType.Int),
														new SqlParameter("@Item_4_Remain_Sec",SqlDbType.Int),
														new SqlParameter("@item_4_signature",SqlDbType.VarChar,20),

														new SqlParameter("@item_4_slots_opened",SqlDbType.Int),
														new SqlParameter("@Item_4_embed_0",SqlDbType.Int),
														new SqlParameter("@Item_4_embed_1",SqlDbType.Int),
														new SqlParameter("@Item_4_embed_2",SqlDbType.Int),
														new SqlParameter("@Item_4_embed_3",SqlDbType.Int),
														new SqlParameter("@Item_4_embed_4",SqlDbType.Int),
														new SqlParameter("@Item_4_embed_5",SqlDbType.Int),

														new SqlParameter("@Item_0_rand_eff_id",SqlDbType.Int),
														new SqlParameter("@Item_1_rand_eff_id",SqlDbType.Int),
														new SqlParameter("@Item_2_rand_eff_id",SqlDbType.Int),
														new SqlParameter("@Item_3_rand_eff_id ",SqlDbType.Int),
														new SqlParameter("@Item_4_rand_eff_id",SqlDbType.Int),
														new SqlParameter("@Money",SqlDbType.Int),
														new SqlParameter("@FJ_Title",SqlDbType.VarChar,50),

														new SqlParameter("@FJ_Content",SqlDbType.VarChar,500),
														new SqlParameter("@result",SqlDbType.Int)};
					paramCode[0].Value=userByID;
					paramCode[1].Value=serverIP;
					paramCode[2].Value=charName;

					string delimStr = ",";
					char [] delimiter = delimStr.ToCharArray();
					string[] itemGuidLists  = itemGuid.Split(delimiter);
					int  k =3;
					//道具ID1;槽；石头1：石头2：石头3;数量；等级；期限，道具ID2;槽；石头1：石头2：石头3;数量；等级；期限
					for(int i=0;i<itemGuidLists.Length;i++)
					{
						string mark = "";
						decimal cur_dur=0;
						decimal max_dur=0;
						int stockMax = 0;
						string delim = ";";
						char [] itemdelimit = delim.ToCharArray();
						string[] itemGuidList = itemGuidLists[i].Split(itemdelimit);
						paramCode[k].Value=itemGuidList[0];//道具的GUID
						k+=1;
						paramCode[k].Value=itemGuidList[3];//道具的个数
						k+=1;
						DataSet ds = FJ_ItemProperty_Query(Convert.ToInt32(itemGuidList[0]));
						if(ds!=null && ds.Tables[0].Rows.Count>0)
						{
							mark = Convert.ToString(ds.Tables[0].Rows[0].ItemArray[0]);
							paramCode[k].Value=mark;//道具描述
							k+=1;
							if(ds.Tables[0].Rows[0].ItemArray[1].ToString().Length>0 && ds.Tables[0].Rows[0].ItemArray[2].ToString().Length>0)
							{
								cur_dur = Convert.ToDecimal(ds.Tables[0].Rows[0].ItemArray[1]);
								max_dur = Convert.ToDecimal(ds.Tables[0].Rows[0].ItemArray[2]);
								stockMax = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[4]);
								paramCode[k].Value=cur_dur;//道具当前数量
								k+=1;
								paramCode[k].Value=max_dur;//道具最大的叠加数量
								k+=1;
								paramCode[k].Value=itemGuidList[4];//道具等级
								k+=1;
								if(stockMax<=1)
								{
									paramCode[k].Value=Convert.ToInt32(itemGuidList[5])*24*60*60;//道具使用期限
									k+=1;
								}
								else
								{
									paramCode[k].Value=0;//道具使用期限
									k+=1;
								}
								if(ds.Tables[0].Rows[0].IsNull(5)==false && Convert.ToString(ds.Tables[0].Rows[0][5]).Length>0)
								{
									paramCode[k].Value=itemGuidList[6];
									k+=1;
								}
								else
								{
									paramCode[k].Value="";
									k+=1;
								}
							}
							else
							{
								paramCode[k].Value=0;
								k+=1;
								paramCode[k].Value=0;
								k+=1;
								paramCode[k].Value=0;
								k+=1;
								paramCode[k].Value=0;
								k+=1;
								paramCode[k].Value="";
								k+=1;

							}
						}
						else
						{
							paramCode[k].Value="";
							k+=1;
							paramCode[k].Value=0;
							k+=1;
							paramCode[k].Value=0;
							k+=1;
							paramCode[k].Value=0;
							k+=1;
							paramCode[k].Value=0;
							k+=1;
							paramCode[k].Value="";
							k+=1;

						}//else
						paramCode[k].Value=itemGuidList[1];
						k+=1;
						string delims = ":";
						char [] stonedelimit = delims.ToCharArray();
						string[] stoneGuidList = itemGuidList[2].Split(stonedelimit);

						for(int j=0;j<stoneGuidList.Length;j++)
						{
							paramCode[k].Value=stoneGuidList[j];
							k+=1;                                                                        
						}//for  stoneGuidList
						if(stoneGuidList.Length<6)
						{
							for(int n=0;n<6-stoneGuidList.Length;n++)
							{
								paramCode[k].Value=0;
								k+=1;
                        
							}//for stoneGuidList
						}//if stoneGuidList
					}
					if(itemGuidLists.Length<5)
					{
						for(int cur=0;cur<5-itemGuidLists.Length;cur++)
						{
							paramCode[k].Value=0;
							k+=1;
							paramCode[k].Value=0;
							k+=1;
							paramCode[k].Value="";
							k+=1;
							paramCode[k].Value=0;
							k+=1;
							paramCode[k].Value=0;
							k+=1;
							paramCode[k].Value=0;
							k+=1;
							paramCode[k].Value=0;
							k+=1;
							paramCode[k].Value="";
							k+=1;
							paramCode[k].Value=0;
							k+=1;
							paramCode[k].Value=0;
							k+=1;
							paramCode[k].Value=0;
							k+=1;
							paramCode[k].Value=0;
							k+=1;
							paramCode[k].Value=0;
							k+=1;
							paramCode[k].Value=0;
							k+=1;
							paramCode[k].Value=0;
							k+=1;
						}
						
					}
					string[] appendItemGuidList = appentItemGuid.Split(delimiter);
					for(int j=0;j<appendItemGuidList.Length;j++)
					{
						paramCode[k].Value=appendItemGuidList[j];
						k+=1;
					}
					if(appendItemGuidList.Length<5)
					{
						for(int n=0;n<5-appendItemGuidList.Length;n++)
						{
							paramCode[k].Value=0;
							k+=1;
						}
					}		
					paramCode[k].Value=money;
					paramCode[k+1].Value=title;
					paramCode[k+2].Value=message;
					paramCode[k+3].Direction = ParameterDirection.ReturnValue;
					result = SqlHelper.ExecSPCommand("FJ_ItemShop_BatchInsert",paramCode);
				}
				else
				{
					paramCode = new SqlParameter[7]{
													   new SqlParameter("@Gm_UserID",SqlDbType.Int),
													   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
													   new SqlParameter("@FJ_CharName",SqlDbType.VarChar,50),
													   new SqlParameter("@Money",SqlDbType.Int),
													   new SqlParameter("@FJ_Title",SqlDbType.VarChar,50),
													   new SqlParameter("@FJ_Content",SqlDbType.VarChar,500),
													   new SqlParameter("@result",SqlDbType.Int)};
					paramCode[0].Value=userByID;
					paramCode[1].Value=serverIP;
					paramCode[2].Value=charName;
					paramCode[3].Value=money;
					paramCode[4].Value=title;
					paramCode[5].Value=message;
					paramCode[6].Direction = ParameterDirection.ReturnValue;
					result = SqlHelper.ExecSPCommand("FJ_SendMoney",paramCode);

				}
			}

			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 删除玩家身上道具
		/// <summary>
		/// 删除玩家物品信息
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int itemShop_Delete(int userByID,string serverIP,string charName,string style,string itemMark,int itemCode)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[7]{
												   new SqlParameter("@Gm_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_serverip",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_CharName",SqlDbType.VarChar,50),
												   new SqlParameter("@FJ_Style",SqlDbType.VarChar,50),
												   new SqlParameter("@FJ_ItemCode",SqlDbType.Int),
												   new SqlParameter("@FJ_ItemMark",SqlDbType.VarChar,300),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=userByID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=charName;
				paramCode[3].Value=style;
				paramCode[4].Value=itemCode;
				paramCode[5].Value=itemMark;
				paramCode[6].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_ItemShop_Delete",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家礼物盒的道具
		/// <summary>
		/// 查看玩家礼物盒的道具
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet FJGiftBox_Query(string serverIP,string charName)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_CharName",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Style",SqlDbType.VarChar,30)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=charName;
				paramCode[2].Value="Package";
				result = SqlHelper.ExecSPDataSet("FJ_ItemShop_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家仓库里的道具
		/// <summary>
		/// 查看玩家仓库里的道具
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet FJWareHouse_Query(string serverIP,string charName)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_CharName",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Style",SqlDbType.VarChar,30)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=charName;
				paramCode[2].Value="wareHouse";
				result = SqlHelper.ExecSPDataSet("FJ_ItemShop_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看玩家快捷栏里的道具
		/// <summary>
		/// 查看玩家快捷栏里的道具
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet FJShortCut_Query(string serverIP,string charName)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[3]{
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_CharName",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Style",SqlDbType.VarChar,30)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=charName;
				paramCode[2].Value="Shortcut";
				result = SqlHelper.ExecSPDataSet("FJ_ItemShop_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看拍卖行里的道具
		/// <summary>
		/// 查看拍卖行里的道具
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet FJAuction_Query(string serverIP,string charName)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[2]{
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,20),
												   new SqlParameter("@FJ_CharName",SqlDbType.VarChar,30)};
				paramCode[0].Value=serverIP;
				paramCode[1].Value=charName;
				result = SqlHelper.ExecSPDataSet("FJ_Auction_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 删除拍卖行里的道具
		/// <summary>
		/// 删除拍卖行里的道具
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static int FJAuction_Del(int operateUserID,string charName,string serverIP,int guid)
		{
			int result = -1;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[5]{
												   new SqlParameter("@GM_UserID",SqlDbType.Int),
												   new SqlParameter("@FJ_ServerIP",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_CharName",SqlDbType.VarChar,30),
												   new SqlParameter("@FJ_Guid",SqlDbType.Int),
												   new SqlParameter("@result",SqlDbType.Int)};
				paramCode[0].Value=operateUserID;
				paramCode[1].Value=serverIP;
				paramCode[2].Value=charName;
				paramCode[3].Value=guid;
				paramCode[4].Direction = ParameterDirection.ReturnValue;
				result = SqlHelper.ExecSPCommand("FJ_Auction_delete",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看道具的属性
		/// <summary>
		/// 查看道具的属性
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet FJ_ItemProperty_Query(int guID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@FJ_GuID",SqlDbType.VarChar,20),};
				paramCode[0].Value=guID;
				result = SqlHelper.ExecSPDataSet("FJ_ItemProperty_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog(ex.Message);
			}
			return result;
		}
		#endregion
		#region 查看道具的属性
		/// <summary>
		/// 查看道具的属性
		/// </summary>
		/// <param name="serverIP">服务器IP</param>
		/// <param name="account">玩家帐号</param>
		/// <returns></returns>
		public static DataSet FJ_ItemName_Query(int guID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@FJ_GuID",SqlDbType.VarChar,20)};
				paramCode[0].Value=guID;
				result = SqlHelper.ExecSPDataSet("FJ_ItemName_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog(ex.Message);
			}
			return result;
		}

		public static DataSet Rich_ItemName_Query(int guID)
		{
			DataSet result = null;
			SqlParameter[] paramCode;
			try
			{
				paramCode = new SqlParameter[1]{
												   new SqlParameter("@RichID",SqlDbType.VarChar,20)};
				paramCode[0].Value=guID;
				result = SqlHelper.ExecSPDataSet("Rich_ItemName_Query",paramCode);
			}
			catch(SqlException ex)
			{
				SqlHelper.errLog.WriteLog(ex.Message);
			}
			return result;
		}
		#endregion
		#region 用户累积总积分查询
		/// <summary>
		/// 用户累积总积分查询 UserPoint
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static ArrayList UserPoint_Query(string account,ref string strDesc)
		{
			string getUser = null;
			string sign = null;
			string parameter ="";
			XmlDocument xmlfile = new XmlDocument();
			getUser =account;
			parameter = account;
			MD5Encrypt md5 = new MD5Encrypt();
			sign = md5.getMD5ofStr(parameter+"|IS8cF0.QueryFJDISdetail").ToLower();
			try   
			{
				System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=10");
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					string serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					string url = "http://"+serverIP+"/PayCenter/FjUserPoint.php";
					HttpWebRequest  request  = (HttpWebRequest)
						WebRequest.Create(url);
					request.ContentType="application/x-www-form-urlencoded";
					request.KeepAlive=false; 
					request.Method="POST";
					//参数POST到商城的接口
					Stream writer = request.GetRequestStream(); 
					string postData="username="+account.ToString()+"&sign="+sign+"&encoding=UTF-8";  
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
						strDesc = "查询密钥错误";
					}
					else if(strDesc!=null && strDesc.Equals("RESULT_4"))
					{
						strDesc = "查询无数据";
					}
					else if(strDesc!=null && strDesc.Equals("RESULT_2"))
					{
						strDesc = "参数选择错误";
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
		#region 查看用户竞拍大乐透信息
		/// <summary>
		/// 查看用户竞拍大乐透信息
		/// </summary>
		/// <param name="serverIP"></param>
		/// <param name="account"></param>
		/// <returns></returns>
		public static ArrayList UserJPInfo_Query(string account, string actionType,ref string strDesc)
		{
			string getUser = null;
			string sign = null;
			string parameter ="";
			XmlDocument xmlfile = new XmlDocument();
			getUser =account;
			parameter = account;
			MD5Encrypt md5 = new MD5Encrypt();
			sign = md5.getMD5ofStr(parameter+actionType+"|IS8cF0.QueryFJDISdetail").ToLower();
			try   
			{
				System.Data.DataSet ds = SqlHelper.ExecuteDataset("select ServerIP from gmtools_serverInfo where gameid=10");
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					string serverIP = ds.Tables[0].Rows[0].ItemArray[0].ToString();
					string url = "http://"+serverIP+"/PayCenter/FjJdInfo.php";
					HttpWebRequest  request  = (HttpWebRequest)
						WebRequest.Create(url);
					request.ContentType="application/x-www-form-urlencoded";
					request.KeepAlive=false; 
					request.Method="POST";
					//参数POST到商城的接口
					Stream writer = request.GetRequestStream(); 
					string postData="username="+account.ToString()+"&type="+actionType+"&sign="+sign+"&encoding=UTF-8";  
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
						strDesc = "参数选择错误";
					}
					else if(strDesc!=null && strDesc.Equals("RESULT_4"))
					{
						strDesc = "查询无数据";
					}
					else
					{
						strDesc = "异常";
					}
					
					int i=1;
					System.Collections.ArrayList rowList = new System.Collections.ArrayList();
					XmlNode nodes;
					while((nodes=xmlfile.SelectSingleNode("you9/row"+i))!=null)
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
				SqlHelper.errLog.WriteLog("服务器IP"+account+ex.Message);
				strDesc = "异常";
			}
			return null;
		}
		#endregion


	}
}
