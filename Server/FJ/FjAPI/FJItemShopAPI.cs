using System;
using System.Data;
using System.Text;
using System.Collections;
using Common.Logic;
using Common.DataInfo;
using GM_Server.FJDataInfo;
namespace GM_Server.FjAPI
{
	/// <summary>
	/// FJItemShopAPI 的摘要说明。
	/// </summary>
	public class FJItemShopAPI
	{

		Message msg = null;
		public FJItemShopAPI(byte[] packet)
		{
			msg = new Message(packet,(uint)packet.Length);
		}
		/// <summary>
		/// 查看道具分类
		/// </summary>
		/// <returns></returns>
		public Message itemShop_Style_Query()
		{
			string serverIP = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"道具分类信息!");
				Console.WriteLine(DateTime.Now+" - 浏览风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"道具分类信息!");
				//请求玩家身上的道具
				ds = FJItemShopInfo.itemShopStyle_Query();	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						//道具编号
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.FJ_Style,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						//道具名
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.FJ_StyleDesc,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);

						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemShop_Style_Query_Resp,2);
				}
				else
					return Message.COMMON_MES_RESP("没有道具分类信息",Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemShop_Style_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);

			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("大区"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("没有道具分类信息",Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemShop_Style_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 查看所有道具信息
		/// </summary>
		/// <returns></returns>
		public Message itemShop_QueryAll()
		{
			string itemName = null;
			string style = null;
			DataSet ds = null;
			try
			{
				style = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Style).m_bValueBuffer);
				itemName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ItemName).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址道具信息!");
				Console.WriteLine(DateTime.Now+" - 浏览风火之旅+>道具信息!");
				//请求玩家身上的道具
				ds = FJItemShopInfo.itemShop_QueryAll(style,itemName);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						//道具编号
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.FJ_GuidID,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						//道具名
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,"["+ds.Tables[0].Rows[i].ItemArray[0]+"]"+ds.Tables[0].Rows[i].ItemArray[1]+"["+ds.Tables[0].Rows[i].ItemArray[3]+"]");
						strut.AddTagKey(TagName.FJ_ItemName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);

						//道具等级
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[2]));
						strut.AddTagKey(TagName.FJ_Level, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

						//道具颜色
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.FJ_Color, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

						//描述
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[4]);
						strut.AddTagKey(TagName.FJ_Inst_Desc, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						
						//耐久正确值
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[5]);
						strut.AddTagKey(TagName.FJ_Inst_Cur, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

						//耐久最大值
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[6]);
						strut.AddTagKey(TagName.FJ_Inst_Max, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[7]));
						strut.AddTagKey(TagName.FJ_StockMax, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[8]);
						strut.AddTagKey(TagName.FJ_SlotMax, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[9]);
						strut.AddTagKey(TagName.FJ_has_rand_eff, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[10]);
						strut.AddTagKey(TagName.FJ_Can_Sign, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemShop_QueryAll_Resp,11);
				}
				else
					return Message.COMMON_MES_RESP("该玩家身上没有道具",Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemShop_QueryAll_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);

			}
			catch(Common.Logic.Exception ex)
			{
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemShop_QueryAll_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 查看附加道具分类
		/// </summary>
		/// <returns></returns>
		public Message AppendProperty_Style_Query()
		{
			DataSet ds = null;
			try
			{
				SqlHelper.log.WriteLog("浏览风火之旅+>道具分类信息!");
				Console.WriteLine(DateTime.Now+" - 浏览风火之旅+>道具分类信息!");
				//请求玩家身上的道具
				ds = FJItemShopInfo.ItemAppendProperty_Style_Query();	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						//道具编号
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.FJ_Style,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						//道具名
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.FJ_StyleDesc,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);

						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemAppend_Style_Query_Resp,2);
				}
				else
					return Message.COMMON_MES_RESP("没有道具分类信息",Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemAppend_Style_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);

			}
			catch(Common.Logic.Exception ex)
			{
				return Message.COMMON_MES_RESP("没有道具分类信息",Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemAppend_Style_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 查看所有附加道具信息
		/// </summary>
		/// <returns></returns>
		public Message AppendProperty_Query()
		{
			string style = null;
			DataSet ds = null;
			try
			{
				style = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Style).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览风火之旅+>道具信息!");
				Console.WriteLine(DateTime.Now+" - 浏览风火之旅+>道具信息!");
				//请求玩家身上的道具
				ds = FJItemShopInfo.ItemAppendProperty_Query(style);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);

						//道具编号
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,Convert.ToString(ds.Tables[0].Rows[i].ItemArray[0]));
						strut.AddTagKey(TagName.FJ_Max,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);

						//道具名
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,Convert.ToString(ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.FJ_Min,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemAppend_Query_Resp,2);
				}
				else
					return Message.COMMON_MES_RESP("该玩家身上没有道具",Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemAppend_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);

			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemAppend_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 查看所有附加道具列表
		/// </summary>
		/// <returns></returns>
		public Message AppendPropertyList_Query()
		{
			string style = null;
			DataSet ds = null;
			try
			{
				style = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Map).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览风火之旅+>道具信息!");
				Console.WriteLine(DateTime.Now+" - 浏览风火之旅+>道具信息!");
				//请求玩家身上的道具
				ds = FJItemShopInfo.ItemAppendPropertyList_Query(style);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = Message.buildTLV(ds,0,100,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemAppendList_Query_Resp,(int)structList[0].structLen);
				}
				else
					return Message.COMMON_MES_RESP("该玩家身上没有道具",Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemAppendList_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);

			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemAppendList_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 查看玩家身上道具
		/// </summary>
		/// <returns></returns>
		public Message itemShop_Owner_Query(int index,int pageSize)
		{
			string serverIP = null;
			string charName = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				charName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserNick).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+charName+"身上道具信息!");
				Console.WriteLine(DateTime.Now+" - 浏览风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+charName+"身上道具信息!");
				//请求玩家身上的道具
				ds = FJItemShopInfo.itemShop_Query(serverIP,charName);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					//总页数
					int pageCount = 0;
					pageCount = ds.Tables[0].Rows.Count % pageSize;
					if (pageCount > 0)
					{
						pageCount = ds.Tables[0].Rows.Count / pageSize + 1;
					}
					else
						pageCount = ds.Tables[0].Rows.Count / pageSize;
					if (index + pageSize > ds.Tables[0].Rows.Count)
					{
						pageSize = ds.Tables[0].Rows.Count - index;
					}
					Query_Structure[] structList = new Query_Structure[pageSize];
					for (int i = index; i < index + pageSize; i++)	
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length+7);
						//道具编号
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.FJ_ItemCode,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						//道具名
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.FJ_ItemName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);

						//位置
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.FJ_SlotStoneNum,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						//道具时间限制
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]));
						strut.AddTagKey(TagName.FJ_ItemLimit, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

						//道具个数
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]));
						strut.AddTagKey(TagName.FJ_ItemCount, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

						//最小等级
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[5]));
						strut.AddTagKey(TagName.FJ_MinLevel,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[6]);
						strut.AddTagKey(TagName.FJ_ItemMark,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						//颜色
                        int color = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[7]);
						string colorDesc  = null;
						switch(color)
						{
							case 1:
								colorDesc = "白色";
								break;
							case 2:
								colorDesc = "绿色";
								break;
							case 3:
								colorDesc = "蓝色";
								break;
							case 4:
								colorDesc = "紫色";
								break;
							case 5:
								colorDesc = "金色";
								break;
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,colorDesc);
						strut.AddTagKey(TagName.FJ_Color,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	

						//道具是否绑定
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[8]));
						strut.AddTagKey(TagName.FJ_isBind,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_MONEY,ds.Tables[0].Rows[i].ItemArray[9]);
						strut.AddTagKey(TagName.FJ_Inst_Cur,TagFormat.TLV_MONEY,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_MONEY,ds.Tables[0].Rows[i].ItemArray[10]);
						strut.AddTagKey(TagName.FJ_Inst_Max,TagFormat.TLV_MONEY,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[11]);
						strut.AddTagKey(TagName.FJ_sell_signature,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						DataSet nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[12]));
						string name = "";
						string stonecolor = "";
						if(nameDS!=null && nameDS.Tables[0].Rows.Count>0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
							stonecolor = nameDS.Tables[0].Rows[0][1].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,name);
						strut.AddTagKey(TagName.FJ_sell_embed_0,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,stonecolor);
						strut.AddTagKey(TagName.FJ_Color0,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[13]));
						name = "";
						stonecolor = "";
						if(nameDS!=null && nameDS.Tables[0].Rows.Count>0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						    stonecolor = nameDS.Tables[0].Rows[0][1].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,name);
						strut.AddTagKey(TagName.FJ_sell_embed_1,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,stonecolor);
						strut.AddTagKey(TagName.FJ_Color1,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[14]));
						name = "";
						stonecolor = "";
						if(nameDS!=null && nameDS.Tables[0].Rows.Count>0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						    stonecolor = nameDS.Tables[0].Rows[0][1].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,name);
						strut.AddTagKey(TagName.FJ_sell_embed_2,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,stonecolor);
						strut.AddTagKey(TagName.FJ_Color2,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[15]));
						name = "";
						stonecolor="";
						if(nameDS!=null && nameDS.Tables[0].Rows.Count>0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						    stonecolor = nameDS.Tables[0].Rows[0][1].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,name);
						strut.AddTagKey(TagName.FJ_sell_embed_3,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,stonecolor);
						strut.AddTagKey(TagName.FJ_Color3,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[16]));
						name = "";
						stonecolor="";
						if(nameDS!=null && nameDS.Tables[0].Rows.Count>0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
					        stonecolor = nameDS.Tables[0].Rows[0][1].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,name);
						strut.AddTagKey(TagName.FJ_sell_embed_4,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,stonecolor);
						strut.AddTagKey(TagName.FJ_Color4,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[17]));
						name = "";
						stonecolor="";
						if(nameDS!=null && nameDS.Tables[0].Rows.Count>0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
							stonecolor = nameDS.Tables[0].Rows[0][1].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,name);
						strut.AddTagKey(TagName.FJ_sell_embed_5,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,stonecolor);
						strut.AddTagKey(TagName.FJ_Color5,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));


						structList[i - index] = strut;

					}                                      

					return Message.COMMON_MES_RESP(structList,Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemShop_Query,19);
				}
				else
					return Message.COMMON_MES_RESP("该玩家身上没有道具",Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemShop_Query,TagName.ERROR_Msg,TagFormat.TLV_STRING);

			}
			catch(System.Exception ex)
			{
				SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemShop_Query,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 添加玩家道具
		/// </summary>
		/// <returns></returns>
		public Message ItemShop_Insert()
		{
			int result = -1;
			int operateUserID = 0;
			string charName = null;
			string serverIP = null;
			int itemCode = 0 ;
			string message  =null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				charName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserNick).m_bValueBuffer);
				strut = new TLV_Structure(TagName.FJ_ItemCode,4,msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ItemCode).m_bValueBuffer);
				itemCode  =(int)strut.toInteger();
				message = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Message).m_bValueBuffer);
				result = FJItemShopInfo.itemShop_Insert(operateUserID,serverIP,charName,itemCode,message);
				if(result==1)
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"添加玩家"+charName+"身上的道具"+itemCode+"信息成功!");
					Console.WriteLine(DateTime.Now+" - 超级舞者+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"添加玩家"+charName+"身上的道具"+itemCode+"信息成功!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemShop_Insert_Resp);
				}
				else
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"添加玩家"+charName+"身上的道具"+itemCode+"信息失败!");
					Console.WriteLine(DateTime.Now+" - 超级舞者+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"添加玩家"+charName+"身上的道具"+itemCode+"信息失败!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemShop_Insert_Resp); 
				}
			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemShop_Insert_Resp);
			}

		}
		/// <summary>
		/// 批量添加玩家道具
		/// </summary>
		/// <returns></returns>
		public Message ItemShop_BatchInsert()
		{
			int result = -1;
			int operateUserID = 0;
			string charNames = null;
			string serverIP = null;
			string itemName = null ;
			string embed = null;
			string appendItem = null;
			string title = null;
			string message  =null;
			int slotitemNum = 0;
			int slotStoneNum = 0;
			int money  = 0 ;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				TLV_Structure tlvstrut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)tlvstrut.toInteger();
				charNames = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserNick).m_bValueBuffer);
				itemName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Item_GuID).m_bValueBuffer); 
				if(itemName!=null && itemName.ToString().Length>0)
				{
					appendItem = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_AppendItem_GuID).m_bValueBuffer);
					tlvstrut = new TLV_Structure(TagName.FJ_SlotItemNum,4,msg.m_packet.m_Body.getTLVByTag(TagName.FJ_SlotItemNum).m_bValueBuffer);
					slotitemNum =(int)tlvstrut.toInteger();
				}
				tlvstrut = new TLV_Structure(TagName.FJ_MCash,4,msg.m_packet.m_Body.getTLVByTag(TagName.FJ_MCash).m_bValueBuffer);
				money =(int)tlvstrut.toInteger();
				title = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Title).m_bValueBuffer);
				message = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Message).m_bValueBuffer);
				string delimStr = ",";
				char [] delimiter = delimStr.ToCharArray();
				string[] charName = charNames.Split(delimiter);
				Query_Structure[] structList = new Query_Structure[charName.Length];
				for(int i=0;i<charName.Length;i++)
				{
					result = FJItemShopInfo.itemShop_BatchInsert(operateUserID,serverIP,charName[i],itemName,appendItem,embed,slotitemNum,slotStoneNum,money,title,message);
					if(result ==1)
					{
						SqlHelper.log.WriteLog("风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"添加玩家"+charName[i]+"身上的道具"+itemName+"信息成功!");
						Console.WriteLine(DateTime.Now+" - 风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"添加玩家"+charName[i]+"身上的道具"+itemName+"信息成功!");
					}
					else
					{
						SqlHelper.log.WriteLog("风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"添加玩家"+charName+"身上的道具"+itemName+"信息失败!");
						Console.WriteLine(DateTime.Now+" - 风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"添加玩家"+charName+"身上的道具"+itemName+"信息失败!");
						Query_Structure strut = new Query_Structure((uint)3);
						//道具编号
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,charName[i]);
						strut.AddTagKey(TagName.FJ_UserNick,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						//道具名
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,itemName);
						strut.AddTagKey(TagName.FJ_ItemName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);

						//道具时间限制
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,appendItem);
						strut.AddTagKey(TagName.FJ_ItemLimit, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
				}
				if(result==1)
				{
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemShop_Insert_Resp);
				}
				else
				{
					return Message.COMMON_MES_RESP(structList,Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemShop_Insert_Resp,3);
				}

			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemShop_Insert_Resp,TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}

		}

		/// <summary>
		/// 删除玩家身上道具
		/// </summary>
		/// <returns></returns>
		public Message ItemShop_Delete()
		{
			int result = -1;
			int operateUserID = 0;
			string charName = null;
			string itemMark  =null;
			string serverIP = null;
			int itemCode = 0 ;
			string style = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				charName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserNick).m_bValueBuffer);
				itemMark = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ItemMark).m_bValueBuffer);
                style = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Style).m_bValueBuffer);
				strut = new TLV_Structure(TagName.FJ_ItemCode,4,msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ItemCode).m_bValueBuffer);
				itemCode  =(int)strut.toInteger();
				result = FJItemShopInfo.itemShop_Delete(operateUserID,serverIP,charName,style,itemMark,itemCode);
				if(result==1)
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+charName+"身上的道具"+itemMark+"删除成功!");
					Console.WriteLine(DateTime.Now+" - 风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+charName+"身上的道具"+itemMark+"删除成功!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemShop_Delete_Resp);
				}
				else
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+charName+"身上的道具"+itemMark+"删除失败!");
					Console.WriteLine(DateTime.Now+" - 风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+charName+"身上的道具"+itemMark+"删除失败!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemShop_Delete_Resp); 
				}
			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemShop_Delete_Resp);
			}

		}
		/// <summary>
		/// 查看玩家包裹的道具
		/// </summary>
		/// <returns></returns>
		public Message giftBox_Query(int index,int pageSize)
		{
			string serverIP = null;
			string charName = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				charName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserNick).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+charName+"礼物盒的道具!");
				Console.WriteLine(DateTime.Now+" - 浏览风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+charName+"礼物盒的道具!");
				//请求所有分类道具列表
				ds = FJItemShopInfo.FJGiftBox_Query(serverIP,charName);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					//总页数
					int pageCount = 0;
					pageCount = ds.Tables[0].Rows.Count % pageSize;
					if (pageCount > 0)
					{
						pageCount = ds.Tables[0].Rows.Count / pageSize + 1;
					}
					else
						pageCount = ds.Tables[0].Rows.Count / pageSize;
					if (index + pageSize > ds.Tables[0].Rows.Count)
					{
						pageSize = ds.Tables[0].Rows.Count - index;
					}
					Query_Structure[] structList = new Query_Structure[pageSize];
					for(int i=index;i<index+pageSize;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length+1);
						//道具编号
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.FJ_ItemCode,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						//道具名
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.FJ_ItemName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						//位置
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.FJ_SlotStoneNum,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						//道具时间限制
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[3]));
						strut.AddTagKey(TagName.FJ_ItemLimit, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

						//道具个数
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[4]));
						strut.AddTagKey(TagName.FJ_ItemCount, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

						//最小等级
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[5]));
						strut.AddTagKey(TagName.FJ_MinLevel,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[6]);
						strut.AddTagKey(TagName.FJ_ItemMark,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						int color = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[7]);
						string colorDesc  = null;
						switch(color)
						{
							case 1:
								colorDesc = "白色";
								break;
							case 2:
								colorDesc = "绿色";
								break;
							case 3:
								colorDesc = "蓝色";
								break;
							case 4:
								colorDesc = "紫色";
								break;
							case 5:
								colorDesc = "金色";
								break;
						}
						//颜色
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,colorDesc);
						strut.AddTagKey(TagName.FJ_Color,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	

						//道具是否绑定
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[8]));
						strut.AddTagKey(TagName.FJ_isBind,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_MONEY,ds.Tables[0].Rows[i].ItemArray[9]);
						strut.AddTagKey(TagName.FJ_Inst_Cur,TagFormat.TLV_MONEY,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_MONEY,ds.Tables[0].Rows[i].ItemArray[10]);
						strut.AddTagKey(TagName.FJ_Inst_Max,TagFormat.TLV_MONEY,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[11]);
						strut.AddTagKey(TagName.FJ_sell_signature,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						DataSet nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[12]));
						string name = "";
						if(nameDS!=null && nameDS.Tables[0].Rows.Count>0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,name);
						strut.AddTagKey(TagName.FJ_sell_embed_0,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[13]));
						name = "";
						if(nameDS!=null && nameDS.Tables[0].Rows.Count>0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,name);
						strut.AddTagKey(TagName.FJ_sell_embed_1,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[14]));
						name = "";
						if(nameDS!=null && nameDS.Tables[0].Rows.Count>0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,name);
						strut.AddTagKey(TagName.FJ_sell_embed_2,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[15]));
						name = "";
						if(nameDS!=null && nameDS.Tables[0].Rows.Count>0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,name);
						strut.AddTagKey(TagName.FJ_sell_embed_3,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[16]));
						name = "";
						if(nameDS!=null && nameDS.Tables[0].Rows.Count>0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,name);
						strut.AddTagKey(TagName.FJ_sell_embed_4,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[17]));
						name = "";
						if(nameDS!=null && nameDS.Tables[0].Rows.Count>0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,name);
						strut.AddTagKey(TagName.FJ_sell_embed_5,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i - index] = strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.FJ_ADMIN,ServiceKey.FJ_Message_Query_Resp,19);
				}
				else
				{
					return Message.COMMON_MES_RESP("该玩家礼物盒里面没有道具",Msg_Category.FJ_ADMIN,ServiceKey.FJ_Message_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP(ex.Message, Msg_Category.FJ_ADMIN, ServiceKey.FJ_Message_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		
		}
		/// <summary>
		/// 查看玩家仓库的道具
		/// </summary>
		/// <returns></returns>
		public Message warehouse_Query(int index,int pageSize)
		{
			string serverIP = null;
			string charName = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				charName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserNick).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+charName+"仓库里的道具!");
				Console.WriteLine(DateTime.Now+" - 浏览风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+charName+"仓库里的道具!");
				//请求所有分类道具列表
				ds = FJItemShopInfo.FJWareHouse_Query(serverIP,charName);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					//总页数
					int pageCount = 0;
					pageCount = ds.Tables[0].Rows.Count % pageSize;
					if (pageCount > 0)
					{
						pageCount = ds.Tables[0].Rows.Count / pageSize + 1;
					}
					else
						pageCount = ds.Tables[0].Rows.Count / pageSize;
					if (index + pageSize > ds.Tables[0].Rows.Count)
					{
						pageSize = ds.Tables[0].Rows.Count - index;
					}
					Query_Structure[] structList = new Query_Structure[pageSize];
					for(int i=index;i<index+pageSize;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length+1);
						//道具编号
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.FJ_ItemCode,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						//道具名
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,UserValidate.validData(ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.FJ_ItemName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						//位置
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.FJ_SlotStoneNum,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						//道具时间限制
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.FJ_ItemLimit, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

						//道具个数
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[4]);
						strut.AddTagKey(TagName.FJ_ItemCount, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

						//最小等级
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[5]);
						strut.AddTagKey(TagName.FJ_MinLevel,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[6]);
						strut.AddTagKey(TagName.FJ_ItemMark,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						int color = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[7]);
						string colorDesc  = null;
						switch(color)
						{
							case 1:
								colorDesc = "白色";
								break;
							case 2:
								colorDesc = "绿色";
								break;
							case 3:
								colorDesc = "蓝色";
								break;
							case 4:
								colorDesc = "紫色";
								break;
							case 5:
								colorDesc = "金色";
								break;
						}
						//颜色
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,colorDesc);
						strut.AddTagKey(TagName.FJ_Color,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	

						//道具是否绑定
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[8]);
						strut.AddTagKey(TagName.FJ_isBind,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_MONEY,ds.Tables[0].Rows[i].ItemArray[9]);
						strut.AddTagKey(TagName.FJ_Inst_Cur,TagFormat.TLV_MONEY,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_MONEY,ds.Tables[0].Rows[i].ItemArray[10]);
						strut.AddTagKey(TagName.FJ_Inst_Max,TagFormat.TLV_MONEY,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[11]);
						strut.AddTagKey(TagName.FJ_sell_signature,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						DataSet nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[12]));
						string name = "";
						if(nameDS!=null && nameDS.Tables[0].Rows.Count>0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,name);
						strut.AddTagKey(TagName.FJ_sell_embed_0,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[13]));
						name = "";
						if(nameDS!=null && nameDS.Tables[0].Rows.Count>0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,name);
						strut.AddTagKey(TagName.FJ_sell_embed_1,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[14]));
						name = "";
						if(nameDS!=null && nameDS.Tables[0].Rows.Count>0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,name);
						strut.AddTagKey(TagName.FJ_sell_embed_2,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[15]));
						name = "";
						if(nameDS!=null && nameDS.Tables[0].Rows.Count>0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,name);
						strut.AddTagKey(TagName.FJ_sell_embed_3,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[16]));
						name = "";
						if(nameDS!=null && nameDS.Tables[0].Rows.Count>0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,name);
						strut.AddTagKey(TagName.FJ_sell_embed_4,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						nameDS = FJItemShopInfo.FJ_ItemName_Query(Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[17]));
						name = "";
						if(nameDS!=null && nameDS.Tables[0].Rows.Count>0)
						{
							name = nameDS.Tables[0].Rows[0][0].ToString();
						}
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,name);
						strut.AddTagKey(TagName.FJ_sell_embed_5,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i - index] = strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.FJ_ADMIN,ServiceKey.FJ_WareHouse_Query_Resp,19);
				}
				else
				{
					return Message.COMMON_MES_RESP("该玩家仓库里面没有道具",Msg_Category.FJ_ADMIN,ServiceKey.FJ_WareHouse_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP(ex.Message, Msg_Category.FJ_ADMIN, ServiceKey.FJ_WareHouse_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 查看玩家快捷键的道具
		/// </summary>
		/// <returns></returns>
		public Message FJShortCut_Query(int index,int pageSize)
		{
			string serverIP = null;
			string charName = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				charName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserNick).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+charName+"快捷键里的道具!");
				Console.WriteLine(DateTime.Now+" - 浏览风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+charName+"快捷键里的道具!");
				//请求所有分类道具列表
				ds = FJItemShopInfo.FJShortCut_Query(serverIP,charName);
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					//总页数
					int pageCount = 0;
					pageCount = ds.Tables[0].Rows.Count % pageSize;
					if (pageCount > 0)
					{
						pageCount = ds.Tables[0].Rows.Count / pageSize + 1;
					}
					else
						pageCount = ds.Tables[0].Rows.Count / pageSize;
					if (index + pageSize > ds.Tables[0].Rows.Count)
					{
						pageSize = ds.Tables[0].Rows.Count - index;
					}
					Query_Structure[] structList = new Query_Structure[pageSize];
					for(int i=index;i<index+pageSize;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length+1);
						//道具编号
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.FJ_ItemCode,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);
						//道具名
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
						strut.AddTagKey(TagName.FJ_ItemName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						//道具时间限制
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.FJ_Style, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
			
						strut.AddTagKey(TagName.PageCount, TagFormat.TLV_INTEGER, 4, TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, pageCount));
						structList[i - index] = strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.FJ_ADMIN,ServiceKey.FJ_ShortCut_Query_Resp,4);
				}
				else
				{
					return Message.COMMON_MES_RESP("该玩家快捷栏里面没有道具",Msg_Category.FJ_ADMIN,ServiceKey.FJ_ShortCut_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP("该玩家快捷栏里面没有道具", Msg_Category.FJ_ADMIN, ServiceKey.FJ_ShortCut_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 查看玩家拍卖行里的道具
		/// </summary>
		/// <returns></returns>
		public Message FJ_Auction_Query(int index,int pageSize)
		{
			string serverIP = null;
			string charName = null;
			DataSet ds = null;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				charName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserNick).m_bValueBuffer);
				SqlHelper.log.WriteLog("浏览风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+charName+"拍卖行里的道具!");
				Console.WriteLine(DateTime.Now+" - 浏览风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+charName+"拍卖行里的道具!");
				//请求所有分类道具列表
				ds = FJItemShopInfo.FJAuction_Query(serverIP,charName);
			    if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
                    Query_Structure[] structList = Message.buildTLV(ds,index,pageSize,false);
					return Message.COMMON_MES_RESP(structList,Msg_Category.FJ_ADMIN,ServiceKey.FJ_Auction_Query_Resp,(int)structList[0].structLen);
				}
				else
				{
					return Message.COMMON_MES_RESP("该玩家没有要拍卖的道具",Msg_Category.FJ_ADMIN,ServiceKey.FJ_Auction_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
				}
			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP("该玩家没有要拍卖的道具", Msg_Category.FJ_ADMIN, ServiceKey.FJ_Auction_Query_Resp, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 删除玩家拍卖行里的道具
		/// </summary>
		/// <returns></returns>
		public Message FJ_Auction_delete()
		{
			int result = -1;
			string serverIP = null;
			string charName = null;
			int operateUserID = 0;
			int guid  =0;
			try
			{
				serverIP = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_ServerIP).m_bValueBuffer);
				charName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UserNick).m_bValueBuffer);
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				operateUserID =(int)strut.toInteger();
				strut = new TLV_Structure(TagName.FJ_GuidID,4,msg.m_packet.m_Body.getTLVByTag(TagName.FJ_GuidID).m_bValueBuffer);
				guid  =(int)strut.toInteger();
				result = FJItemShopInfo.FJAuction_Del(operateUserID,charName,serverIP,guid);
				if(result==1)
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+charName+"身上的道具"+guid+"删除成功!");
					Console.WriteLine(DateTime.Now+" - 风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+charName+"身上的道具"+guid+"删除成功!");
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.FJ_ADMIN,ServiceKey.FJ_Auction_Del_Resp);
				}
				else
				{
					SqlHelper.log.WriteLog("风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+charName+"身上的道具"+guid+"删除失败!");
					Console.WriteLine(DateTime.Now+" - 风火之旅+>服务器地址"+CommonInfo.serverIP_Query(serverIP)+"玩家"+charName+"身上的道具"+guid+"删除失败!");
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.FJ_ADMIN,ServiceKey.FJ_Auction_Del_Resp); 
				}
			}
			catch(System.Exception ex)
			{
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.FJ_ADMIN,ServiceKey.FJ_Auction_Del_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}

		}
		/// <summary>
		/// 查看道具名称信息
		/// </summary>
		/// <returns></returns>
		public Message ItemName_Query()
		{
			DataSet ds = null;
			int style = 0;
			int guid =0;
			try
			{
				SqlHelper.log.WriteLog("浏览风火之旅+>道具名称信息!");
				Console.WriteLine(DateTime.Now+" - 浏览风火之旅+>道具名称信息!");
				//请求玩家身上的道具
				TLV_Structure tlvstrut = new TLV_Structure(TagName.FJ_Item_GuID,4,msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Item_GuID).m_bValueBuffer);
				guid =(int)tlvstrut.toInteger();
				ds = FJItemShopInfo.FJ_ItemName_Query(guid);	
				if(ds!=null && ds.Tables[0].Rows.Count>0)
				{
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						//道具名
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[0]);
						strut.AddTagKey(TagName.FJ_ItemName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);

						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList,Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemName_Query_Resp,1);
				}
				else
					return Message.COMMON_MES_RESP("没有道具信息",Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemName_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);

			}
			catch(Common.Logic.Exception ex)
			{
				return Message.COMMON_MES_RESP("没有道具信息",Msg_Category.FJ_ADMIN,ServiceKey.FJ_ItemName_Query_Resp,TagName.ERROR_Msg,TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 查询用户竞拍信息
		/// </summary>
		/// <returns></returns>
		public Message UserJPInfo_Query()
		{
			string account = null;
			string statDesc = null;
			//string strtype = null;
			ArrayList ds = null;
			try
			{
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UseAccount).m_bValueBuffer);
				//strtype = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Type).m_bValueBuffer);

				SqlHelper.log.WriteLog("浏览久之游玩家" + account + "竞拍大乐透信息");
				Console.WriteLine(DateTime.Now + " - 浏览久之游玩家" + account + "竞拍大乐透信息!");
				ds = FJItemShopInfo.UserJPInfo_Query(account,"JP",ref statDesc);
				Query_Structure[] structList = new Query_Structure[(uint)ds.Count];
				if(ds!=null && ds.Count>0)
				{
					for (int i =0;i<ds.Count;i++)
					{
						ArrayList colList = (ArrayList)ds[i];
						Query_Structure strut = new Query_Structure((uint)colList.Count);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,colList[0].ToString());
						strut.AddTagKey(TagName.FJ_propname, TagFormat.TLV_STRING, (uint)bytes.Length,bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, colList[1].ToString());
						strut.AddTagKey(TagName.FJ_nominal, TagFormat.TLV_STRING, (uint)bytes.Length,bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP,Convert.ToDateTime(colList[2].ToString()));
						strut.AddTagKey(TagName.FJ_CreateTime, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(colList[3].ToString()));
						strut.AddTagKey(TagName.FJ_pointamt1, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,colList[4].ToString());
						strut.AddTagKey(TagName.FJ_result, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_GamesUserFjJPInfo_RESP,5);
				}
				else
				{
					return Message.COMMON_MES_RESP(statDesc, Msg_Category.FJ_ADMIN, ServiceKey.FJ_GamesUserFjJPInfo_RESP,TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
			}
			catch (System.Exception ex)
			{
				//SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("没有该用户的竞拍信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_GamesUserFjJPInfo_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		public Message UserDLTInfo_Query()
		{
			string account = null;
			string statDesc = null;
			//string strtype = null;
			ArrayList ds = null;
			try
			{
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UseAccount).m_bValueBuffer);
				//strtype = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Type).m_bValueBuffer);

				SqlHelper.log.WriteLog("浏览久之游玩家" + account + "竞拍大乐透信息");
				Console.WriteLine(DateTime.Now + " - 浏览久之游玩家" + account + "竞拍大乐透信息!");
				ds = FJItemShopInfo.UserJPInfo_Query(account,"DLT",ref statDesc);
				Query_Structure[] structList = new Query_Structure[(uint)ds.Count];
				if(ds!=null && ds.Count>0)
				{
					for (int i =0;i<ds.Count;i++)
					{
						ArrayList colList = (ArrayList)ds[i];

						Query_Structure strut = new Query_Structure((uint)colList.Count);

						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,colList[0].ToString());
						strut.AddTagKey(TagName.FJ_strkey, TagFormat.TLV_STRING, (uint)bytes.Length,bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, colList[1].ToString());
						strut.AddTagKey(TagName.FJ_result, TagFormat.TLV_STRING, (uint)bytes.Length,bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(colList[2].ToString()));
						strut.AddTagKey(TagName.FJ_pointamt1, TagFormat.TLV_INTEGER, (uint)bytes.Length,bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP,Convert.ToDateTime(colList[3].ToString()));
						strut.AddTagKey(TagName.FJ_CreateTime, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_GamesUserFjDLTInfo_RESP,4);
				}
				else
				{
					return Message.COMMON_MES_RESP(statDesc, Msg_Category.FJ_ADMIN, ServiceKey.FJ_GamesUserFjDLTInfo_RESP,TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
			}
			catch (System.Exception ex)
			{
				//SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("没有该用户的大乐透信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_GamesUserFjDLTInfo_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 查询用户兑换信息(竞拍，大乐透，兑换)
		/// </summary>
		/// <returns></returns>
		public Message UserDHInfo_Query()
		{
			string account = null;
			string statDesc = null;
			//string strtype = null;
			ArrayList ds = null;
			try
			{
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UseAccount).m_bValueBuffer);
				//strtype = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Type).m_bValueBuffer);

				SqlHelper.log.WriteLog("浏览久之游玩家" + account + "兑换大乐透信息");
				Console.WriteLine(DateTime.Now + " - 浏览久之游玩家" + account + "兑换大乐透信息!");
				ds = FJItemShopInfo.UserJPInfo_Query(account,"DH",ref statDesc);
				Query_Structure[] structList = new Query_Structure[(uint)ds.Count];
				if(ds!=null && ds.Count>0)
				{
					for (int i =0;i<ds.Count;i++)
					{
						ArrayList colList = (ArrayList)ds[i];
						Query_Structure strut = new Query_Structure((uint)colList.Count);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,Convert.ToInt32(colList[0].ToString()));
						strut.AddTagKey(TagName.FJ_pointamt1, TagFormat.TLV_INTEGER, (uint)bytes.Length,bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, colList[1].ToString());
						strut.AddTagKey(TagName.FJ_propname, TagFormat.TLV_STRING, (uint)bytes.Length,bytes);

						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_TIMESTAMP,Convert.ToDateTime(colList[2].ToString()));
						strut.AddTagKey(TagName.FJ_CreateTime, TagFormat.TLV_TIMESTAMP, (uint)bytes.Length, bytes);
						structList[i] = strut;
					}
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_GamesUserFjJPInfo_RESP,3);
				}
				else
				{
					return Message.COMMON_MES_RESP(statDesc, Msg_Category.FJ_ADMIN, ServiceKey.FJ_GamesUserFjJPInfo_RESP,TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
			}
			catch (System.Exception ex)
			{
				//SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("没有该用户的竞拍信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_GamesUserFjJPInfo_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
		/// <summary>
		/// 用户积分查询
		/// </summary>
		/// <returns></returns>
		public Message youUserPoint_Query()
		{
			string account = null;
			string statDesc = null;
			//string strtype = null;
			ArrayList ds = null;
			try
			{
				account = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_UseAccount).m_bValueBuffer);
				//strtype = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.FJ_Type).m_bValueBuffer);

				SqlHelper.log.WriteLog("浏览久之游玩家" + account + "积分信息");
				Console.WriteLine(DateTime.Now + " - 浏览久之游玩家" + account + "积分信息!");
				ds = FJItemShopInfo.UserPoint_Query(account,ref statDesc);
				Query_Structure[] structList = new Query_Structure[1];
				if(ds!=null && ds.Count>0)
				{
						Query_Structure strut = new Query_Structure((uint)ds.Count);
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds[0].ToString());
						strut.AddTagKey(TagName.FJ_UserID, TagFormat.TLV_STRING, (uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,int.Parse(ds[1].ToString()));
						strut.AddTagKey(TagName.FJ_pointamt, TagFormat.TLV_INTEGER, (uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,int.Parse(ds[2].ToString()));
						strut.AddTagKey(TagName.FJ_pointused1, TagFormat.TLV_INTEGER, (uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,int.Parse(ds[3].ToString()));
						strut.AddTagKey(TagName.FJ_pointfrozen, TagFormat.TLV_INTEGER, (uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,int.Parse(ds[4].ToString()));
						strut.AddTagKey(TagName.FJ_residual, TagFormat.TLV_INTEGER, (uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,int.Parse(ds[5].ToString()));
						strut.AddTagKey(TagName.FJ_usable, TagFormat.TLV_INTEGER, (uint)bytes.Length,bytes);

						structList[0] = strut;
					
					return Message.COMMON_MES_RESP(structList, Msg_Category.FJ_ADMIN, ServiceKey.FJ_GamesUserUserPoint_RESP,6);
				}
				else
				{
					return Message.COMMON_MES_RESP(statDesc, Msg_Category.FJ_ADMIN, ServiceKey.FJ_GamesUserUserPoint_RESP,TagName.ERROR_Msg, TagFormat.TLV_STRING);
				}
			}
			catch (System.Exception ex)
			{
				//SqlHelper.errLog.WriteLog("服务器IP"+serverIP+ex.Message);
				return Message.COMMON_MES_RESP("没有该用户的积分信息", Msg_Category.FJ_ADMIN, ServiceKey.FJ_GamesUserUserPoint_RESP, TagName.ERROR_Msg, TagFormat.TLV_STRING);
			}
		}
	}
}
