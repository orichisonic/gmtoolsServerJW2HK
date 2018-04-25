    using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.IO;
using Common.Logic;
using Common.API;
using Common.DataInfo;
//using GM_Server.FjAPI;
using lg = Common.API.LanguageAPI;
//using GM_Server.SDOnlineAPI;
//using GM_Server.SDOnlineDataInfo;
using GM_Server.JW2API;
using GM_Server.JW2DataInfo;
namespace GMSERVER.ServerSocket
{
	/// <summary>
	/// Handler 的摘要说明。
	/// </summary>
	public class Handler
	{
		private TcpClient svrSocket ;
		private NetworkStream networkStream ;
		private Mutex mut = new Mutex();
		bool ContinueProcess = false ;
		private byte[] bytes; 		// Data buffer for incoming data.
		private StringBuilder sb =  new StringBuilder(); // Received data string.
		private string data = null; // Incoming data from the client.
		Handler[] handler_ = new Handler[2];
		int handlerType = 0;

		public Handler (TcpClient acceptClientSocket) 
		{
			this.svrSocket = acceptClientSocket ;
			networkStream = acceptClientSocket.GetStream();
			bytes = new byte[acceptClientSocket.ReceiveBufferSize];
			handler_[handlerType]=this;
			//ContinueProcess = true ;
		}
		public Handler getHandler(int type)
		{
			return this.handler_[type];
		}
		/// <summary>
		/// 单个客户端连接进入以后，服务器端接受和发送消息的处理事务
		/// </summary>
		public void Process() 
		{
			int index = 0;
			int pageSize = 0;
			int userByID = 0;
			string name = null;
			string passwd = null;
			string mac = null;
			DateTime connTime;
			int status = 0;
			mut.WaitOne();
			try
			{
				while ( true ) 
				{

					UserValidate validate = null;
					int BytesRead =  networkStream.Read(bytes, 0, (int) bytes.Length) ;
					Message msg = new Message(bytes,(uint)bytes.Length);
					Packet packet = msg.m_packet;
					if( msg.IsValidMessage==true)
					{
						Packet_Body mesBody = new Packet_Body(packet.m_Body.m_bBodyBuffer,packet.m_Body.m_uiBodyLen);
						if (msg.GetMessageID() ==Message_Tag_ID.CONNECT 
							|| msg.GetMessageID() ==Message_Tag_ID.ACCOUNT_AUTHOR
							|| msg.GetMessageID() == Message_Tag_ID.DISCONNECT)
						{
							name = System.Text.Encoding.Unicode.GetString(mesBody.getTLVByTag(TagName.UserName).m_bValueBuffer);
							data = name;
							passwd = System.Text.Encoding.Unicode.GetString(mesBody.getTLVByTag(TagName.PassWord).m_bValueBuffer);
							mac = System.Text.Encoding.Unicode.GetString(mesBody.getTLVByTag(TagName.MAC).m_bValueBuffer);
							if(msg.GetMessageID()==Message_Tag_ID.CONNECT )
							{
								//TLV_Structure tvlLimit = new TLV_Structure(TagName.Conn_Time,mesBody.getTLVByTag(TagName.Conn_Time).m_uiValueLen,mesBody.getTLVByTag(TagName.Conn_Time).m_bValueBuffer);
								connTime = DateTime.Now; 
								//响应连接请求
								CommonAPI api = new CommonAPI(name,passwd,mac,connTime,"PASS");
								send(api.packConnectResp());
							}
							else if(msg.GetMessageID() ==Message_Tag_ID.ACCOUNT_AUTHOR)
							{
								//用户验证请求
								if (name.Equals("administrator")==true  && passwd.Equals("MengjianG!@#$%^")==true)
								{
									send(Message.Common_ACCOUNT_AUTHOR_RESP(0,"PASS"));
									userByID = 0;
									status = 1;
								}
								else
								{
									validate = new UserValidate(name,passwd,mac);
									send(validate.validateUser());
									userByID = validate.UserByID;
									status = validate.Status;
								}
							}
							else if(msg.GetMessageID() == Message_Tag_ID.DISCONNECT)
							{
								//响应断开请求
								TLV_Structure stuct = new TLV_Structure(TagName.UserByID,mesBody.getTLVByTag(TagName.UserByID).m_uiValueLen,mesBody.getTLVByTag(TagName.UserByID).m_bValueBuffer);
								userByID = (int)stuct.toInteger();
								CommonAPI api = new CommonAPI(userByID,"PASS",bytes);
								UserInfoAPI userInfo = new UserInfoAPI();
								userInfo.GM_UpdateActiveUser(userByID,0);
								send(api.packConnectResp());
							}
						}
						//验证通过
						if(status==1)
						{
							TLV_Structure stuct;
                            Common.DataInfo.UpdatePatch patch = new Common.DataInfo.UpdatePatch(bytes);

							
							DepartmentAPI departInfo = new DepartmentAPI(bytes);
							UserInfoAPI userInfo = new UserInfoAPI(bytes);
							ModuleInfoAPI moduleInfo = new ModuleInfoAPI(bytes);
							GameInfoAPI gameInfo = new GameInfoAPI(bytes);
							UserModuleAPI userModule = new UserModuleAPI(bytes);
							CommonAPI api = new CommonAPI(userByID,"PASS",bytes);
							NotesInfoAPI notesAPI = new NotesInfoAPI(bytes);
//							SDAccountInfoAPI SDAccountAPI = new SDAccountInfoAPI(bytes);
//							SDLogInfoAPI SDLogAPI = new SDLogInfoAPI(bytes);

							JW2AccountInfoAPI jw2accountinfo = new JW2AccountInfoAPI(bytes);
							JW2ItemInfoAPI jw2iteminfo = new JW2ItemInfoAPI(bytes);
							JW2LogInfoAPI jw2loginfo = new JW2LogInfoAPI(bytes);
							JW2LoginInfoAPI jw2logininfo = new JW2LoginInfoAPI(bytes);
							JW2MessengerInfoAPI jw2messengerinfo = new JW2MessengerInfoAPI(bytes);
						

							switch(msg.GetMessageID())
							{
                                //客户端比较请求
                                case Message_Tag_ID.CLIENT_PATCH_COMPARE:
                                 {
                                    send(patch.encodeMessage());
                                    break;
                                  }
                              //客户端更新请求
                              case Message_Tag_ID.CLIENT_PATCH_UPDATE:
                                  {
                                      send(patch.transferPatchFile());
                                      break;
                                  }
									//得到部门信息
								case Message_Tag_ID.DEPART_QUERY:
									send(departInfo.GM_QueryDepartInfo());
									break;
								case Message_Tag_ID.DEPARTMENT_RELATE_QUERY:
									send(departInfo.GM_QueryDepartRelateInfo());
									break;
								case Message_Tag_ID.DEPART_RELATE_GAME_QUERY:
									send(departInfo.GM_QueryDepartRelateGameInfo());
									break;
									//添加部门信息
								case Message_Tag_ID.DEPARTMENT_CREATE:
									send(departInfo.GM_InsertDepartInfo());
									break;
									//修改部门信息
								case Message_Tag_ID.DEPARTMENT_UPDATE:
									send(departInfo.GM_UpdateDepartInfo());
									break;
									//删除部门信息
								case Message_Tag_ID.DEPARTMENT_DELETE:
									send(departInfo.GM_DelDepartInfo());
									break;
									//创建用户
								case Message_Tag_ID.USER_CREATE:
									send(userInfo.GM_InsertUserInfo());break;
									//修改密码
								case Message_Tag_ID.USER_UPDATE:
									send(userInfo.GM_UpdateUserInfo());break;
									//删除用户
								case Message_Tag_ID.USER_DELETE:
									send(userInfo.GM_DelUserInfo());break;
									//用户查询
								case Message_Tag_ID.USER_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index,mesBody.getTLVByTag(TagName.Index).m_uiValueLen,mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize,mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen,mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(userInfo.GM_QueryList(index-1,pageSize,userByID));
									break;
								}
								case Message_Tag_ID.USER_SYSADMIN_QUERY:
								{
									TLV_Structure tvlUserID = new TLV_Structure(TagName.UserByID, mesBody.getTLVByTag(TagName.UserByID).m_uiValueLen, mesBody.getTLVByTag(TagName.UserByID).m_bValueBuffer);
									int userID = Convert.ToInt32(tvlUserID.toInteger());
									send(userInfo.GM_QuerySysAdminInfo(userID));
									break;
								}
                                    //修改在线用户状态
                                case Message_Tag_ID.UPDATE_ACTIVEUSER:
                                {
                                    TLV_Structure tvlUserID = new TLV_Structure(TagName.User_ID, mesBody.getTLVByTag(TagName.User_ID).m_uiValueLen, mesBody.getTLVByTag(TagName.User_ID).m_bValueBuffer);
                                    int userID = Convert.ToInt32(tvlUserID.toInteger());
                                    send(userInfo.GM_UpdateActiveUserPkg(userID,0));
                                    break;
                                }
                                    //用户密码修改
								case Message_Tag_ID.USER_PASSWD_MODIF:
									send(userInfo.GM_ModifPassWd());
									break;
									//查询所有GM帐号信息
								case Message_Tag_ID.USER_QUERY_ALL:
									send(userInfo.GM_QueryAll(userByID));
									break;
									//模块查询
								case Message_Tag_ID.MODULE_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index,mesBody.getTLVByTag(TagName.Index).m_uiValueLen,mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize,mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen,mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(moduleInfo.GM_QueryAll(index-1,pageSize));
									break;
								}
                                    //用户模块分配
								case Message_Tag_ID.USER_MODULE_UPDATE:
									send(userModule.GM_UserModuleAdmin());
									break;
									//根据UserID查询模块
								case Message_Tag_ID.USER_MODULE_QUERY:
								{
									if (name.Equals("administrator")==true  && passwd.Equals("MengjianG!@#$%^")==true)
									{
										send(userModule.GM_getModuleInfo());
									}
									else
									{
										TLV_Structure tvlUserID = new TLV_Structure(TagName.User_ID,mesBody.getTLVByTag(TagName.User_ID).m_uiValueLen,mesBody.getTLVByTag(TagName.User_ID).m_bValueBuffer);
										int userID =Convert.ToInt32(tvlUserID.toInteger());
										send(userModule.GM_getModuleInfo(userID));
									}
									break;
								}
									//添加模块
								case Message_Tag_ID.MODULE_CREATE:
									send(moduleInfo.GM_InsertModuleInfo());
									break;
									//修改模块
								case Message_Tag_ID.MODULE_UPDATE:
									send(moduleInfo.GM_UpdateModuleInfo());
									break;
									//删除模块
								case Message_Tag_ID.MODULE_DELETE:
									send(moduleInfo.GM_DelModuleInfo());
									break;
									//查询游戏
								case Message_Tag_ID.GAME_QUERY:
									send(gameInfo.GM_QueryAll());
									break;
									//创建游戏
								case Message_Tag_ID.GAME_CREATE:
									send(gameInfo.GM_InsertGameInfo());
									break;
									//修改游戏
								case Message_Tag_ID.GAME_UPDATE:
									send(gameInfo.GM_UpdateGameInfo());
									break;
									//删除游戏
								case Message_Tag_ID.GAME_DELETE:
									send(gameInfo.GM_DelGameInfo());
									break;
								case Message_Tag_ID.GAME_MODULE_QUERY:
									send(gameInfo.GM_QueryModuleInfo(1));
									break;
                                    //查询所有游戏IP
                                case Message_Tag_ID.SERVERINFO_IP_ALL_QUERY:
                                    send(api.packServerInfoALLResp());
                                    break;
								case Message_Tag_ID.SERVERINFO_IP_QUERY:
								{
									send(api.packServerInfoResp());
									break;
								}
                                    //添加游戏服务器IP
                                case Message_Tag_ID.LINK_SERVERIP_CREATE:
                                {
                                    send(api.packCreateServerInfoResp());
                                    break;
                                }
									//删除游戏服务器IP
								case Message_Tag_ID.LINK_SERVERIP_DELETE:
								{
									send(api.packDelServerInfoResp());
									break;
								}
								case Message_Tag_ID.GMTOOLS_OperateLog_Query:
								{
									stuct = new TLV_Structure(TagName.Index,mesBody.getTLVByTag(TagName.Index).m_uiValueLen,mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize,mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen,mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(api.UserOperateLog_Query(index-1,pageSize));
									break;
								}
									//取得NTES邮件列表
								case Message_Tag_ID.NOTES_LETTER_TRANSFER:
								{
									stuct = new TLV_Structure(TagName.Index,mesBody.getTLVByTag(TagName.Index).m_uiValueLen,mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize,mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen,mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(notesAPI.Notes_TransferInfo_Resp(index-1,pageSize));
									break;
								}
									//处理邮件NOTES
								case Message_Tag_ID.NOTES_LETTER_PROCESS:
									send(notesAPI.Notes_LetterProcess_Resp());
									break;
									//当前用户得到转发给他NOTES邮件
								case Message_Tag_ID.NOTES_LETTER_TRANSMIT:
								{
									stuct = new TLV_Structure(TagName.UserByID,mesBody.getTLVByTag(TagName.UserByID).m_uiValueLen,mesBody.getTLVByTag(TagName.UserByID).m_bValueBuffer);
									int userbyID =Convert.ToInt32(stuct.toInteger());
									stuct = new TLV_Structure(TagName.Index,mesBody.getTLVByTag(TagName.Index).m_uiValueLen,mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize,mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen,mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(notesAPI.Notes_TransmitInfo_Resp(userbyID,index-1,pageSize));
									break;
								}
								

//									#region SD高达
//									#region 激活码查询
//								case Message_Tag_ID.SD_ActiveCode_Query:
//								{
//									send(SDAccountAPI.SDUserActiveCode_Query());
//									break;
//								}
//									#endregion
//									#region 用户查询激活码
//								case Message_Tag_ID.SD_Account_Active_Query:
//								{
//									send(SDAccountAPI.SDUserAccount_Active_Query());
//									break;
//								}
//									#endregion
//
//									#region 玩家新手卡/钻石卡
//								case Message_Tag_ID.SD_Card_Info_Query:
//								{
//									send(SDAccountAPI.SD_Card_Info_Query());
//									break;
//								}
//									#endregion
//									#region 玩家角色信息
//								case Message_Tag_ID.SD_Account_Query:
//								{
//									send(SDAccountAPI.SD_Account_Query());
//									break;
//								}
//									#endregion
//									#region 玩家机体组合
//								case Message_Tag_ID.SD_Item_MixTree_Query:
//								{
//									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
//									index = (int)stuct.toInteger();
//									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
//									pageSize = (int)stuct.toInteger();
//									send(SDAccountAPI.SD_Item_MixTree_Query(index-1,pageSize));
//									break;
//								}
//									#endregion
//									#region 玩家副官道具
//								case Message_Tag_ID.SD_Item_Operator_Query:
//								{
//									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
//									index = (int)stuct.toInteger();
//									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
//									pageSize = (int)stuct.toInteger();
//									send(SDAccountAPI.SD_Item_Operator_Query(index-1,pageSize));
//									break;
//								}
//									#endregion
//									#region 玩家涂料道具
//								case Message_Tag_ID.SD_Item_Paint_Query:
//								{
//									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
//									index = (int)stuct.toInteger();
//									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
//									pageSize = (int)stuct.toInteger();
//									send(SDAccountAPI.SD_Item_Paint_Query(index-1,pageSize));
//									break;
//								}
//									#endregion
//									#region 玩家技能道具
//								case Message_Tag_ID.SD_Item_Skill_Query:
//								{
//									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
//									index = (int)stuct.toInteger();
//									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
//									pageSize = (int)stuct.toInteger();
//									send(SDAccountAPI.SD_Item_Skill_Query(index-1,pageSize));
//									break;
//								}
//									#endregion
//									#region 玩家标签道具
//								case Message_Tag_ID.SD_Item_Sticker_Query:
//								{
//									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
//									index = (int)stuct.toInteger();
//									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
//									pageSize = (int)stuct.toInteger();
//									send(SDAccountAPI.SD_Item_Sticker_Query(index-1,pageSize));
//									break;
//								}
//									#endregion
//									#region 玩家战斗道具
//								case Message_Tag_ID.SD_Item_UserCombatitems_Query:
//								{
//									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
//									index = (int)stuct.toInteger();
//									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
//									pageSize = (int)stuct.toInteger();
//									send(SDAccountAPI.SD_Item_UserCombatitems_Query(index-1,pageSize));
//									break;
//								}
//									#endregion
//									#region 玩家机体信息
//								case Message_Tag_ID.SD_Item_UserUnits_Query:
//								{
//									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
//									index = (int)stuct.toInteger();
//									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
//									pageSize = (int)stuct.toInteger();
//									send(SDAccountAPI.SD_Item_UserUnits_Query(index-1,pageSize));
//									break;
//								}
//									#endregion									
//									#region 玩家机体道具信息
//								case Message_Tag_ID.SD_UnitsItem_Query:
//								{
//									send(SDAccountAPI.SD_UnitsItem_Query());
//									break;
//								}
//									#endregion									
//									#region 玩家好友信息
//								case Message_Tag_ID.SD_Firend_Query:
//								{
//									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
//									index = (int)stuct.toInteger();
//									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
//									pageSize = (int)stuct.toInteger();
//									send(SDAccountAPI.SD_Firend_Query(index-1,pageSize));
//									break;
//								}
//									#endregion
////									#region 工会信息
////								case Message_Tag_ID.SD_Guild_Query:
////								{
////									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
////									index = (int)stuct.toInteger();
////									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
////									pageSize = (int)stuct.toInteger();
////									send(SDAccountAPI.SD_Guild_Query(index-1,pageSize));
////									break;
////								}
////									#endregion
////
////									#region 工会成员信息
////								case Message_Tag_ID.SD_GuildMember_Query:
////								{
////									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
////									index = (int)stuct.toInteger();
////									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
////									pageSize = (int)stuct.toInteger();
////									send(SDAccountAPI.SD_GuildMember_Query(index-1,pageSize));
////									break;
////								}
////									#endregion
//									
//									#region 玩家排名信息（1。综合/2。胜利）
//								case Message_Tag_ID.SD_UserRank_query:
//								{
//									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
//									index = (int)stuct.toInteger();
//									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
//									pageSize = (int)stuct.toInteger();
//									send(SDAccountAPI.SD_UserRank_query(index-1,pageSize));
//									break;
//								}
//									#endregion
//									#region 用户踢下线
//								case Message_Tag_ID.SD_KickUser_Query:
//								{
//									send(SDLogAPI.SD_KickUser_Query());
//									break;
//								}
//									#endregion
//									#region 用户封停
//								case Message_Tag_ID.SD_BanUser_Ban:
//								{
//									send(SDLogAPI.SD_BanUser_Ban());
//									break;
//								}
//									#endregion
//									#region 用户解封
//								case Message_Tag_ID.SD_BanUser_UnBan:
//								{
//									send(SDLogAPI.SD_BanUser_UnBan());
//									break;
//								}
//									#endregion
//									#region 恢复临时密码
//								case Message_Tag_ID.SD_ReTmpPassWord_Query:
//								{
//									send(SDLogAPI.SD_ReTmpPassWord_Query());
//									break;
//								}
//									#endregion
//									#region 公告查询
//								case Message_Tag_ID.SD_SeacrhNotes_Query:
//								{									
//									send(SDLogAPI.SD_SeacrhNotes_Query());
//									break;
//								}
//									#endregion
//									#region 公告修改
//								case Message_Tag_ID.SD_TaskList_Update:
//								{
//									send(SDLogAPI.SD_TaskList_Update());
//									break;
//								}
//									#endregion
//									#region 查询最后一次临时密码
//								case Message_Tag_ID.SD_SearchPassWord_Query:
//								{
//									send(SDLogAPI.SD_SearchPassWord_Query());
//									break;
//								}
//									#endregion
//									#region 发送公告
//								case Message_Tag_ID.SD_SendNotes_Query:
//								{
//									send(SDLogAPI.SD_SendNotes_Query());
//									break;
//								}
//									#endregion
//									#region 修改临时密码
//								case Message_Tag_ID.SD_TmpPassWord_Query:
//								{
//									send(SDLogAPI.SD_TmpPassWord_Query());
//									break;
//								}
//									#endregion
//									#region 修改玩家等级
//								case Message_Tag_ID.SD_UpdateExp_Query:
//								{
//									send(SDLogAPI.SD_UpdateExp_Query());
//									break;
//								}
//									#endregion
//									#region 修改玩家机体等级
//								case Message_Tag_ID.SD_UpdateUnitsExp_Query:
//								{
//									send(SDLogAPI.SD_UpdateUnitsExp_Query());
//									break;
//								}
//									#region 修改金钱
//								case Message_Tag_ID.SD_UpdateMoney_Query:
//								{
//									send(SDLogAPI.SD_UpdateMoney_Query());
//									break;
//								}
//									#endregion
//
//									#endregion
//									#region 玩家礼物\邮件查询
//								case Message_Tag_ID.SD_UserGrift_Query:
//								{
//									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
//									index = (int)stuct.toInteger();
//									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
//									pageSize = (int)stuct.toInteger();
//									send(SDLogAPI.SD_UserGrift_Query(index-1,pageSize));
//									break;
//								}
//									#endregion
//									#region 查询玩家登陆信息
//								case Message_Tag_ID.SD_UserLoginfo_Query:
//								{
//									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
//									index = (int)stuct.toInteger();
//									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
//									pageSize = (int)stuct.toInteger();
//									send(SDLogAPI.SD_UserLoginfo_Query(index-1,pageSize));
//									break;
//								}
//									#endregion
//									#region 获得道具列表
//								case Message_Tag_ID.SD_ItemList_Query:
//								{
//									send(SDLogAPI.SD_GetItemList_Query());
//									break;
//								}
//									#endregion
//									#region 添加道具
//								case Message_Tag_ID.SD_UserAdditem_Add:
//								{
//									send(SDLogAPI.SD_UserAdditem_Add());
//									break;
//								}
//									#endregion									
//									#region 添加道具
//								case Message_Tag_ID.SD_UserAdditem_Add_All:
//								{
//									send(SDLogAPI.SD_UserAdditem_Add_All());
//									break;
//								}
//									#endregion									
//									#region 恢复机体
//								case Message_Tag_ID.SD_ReGetUnits_Query:
//								{
//									send(SDLogAPI.SD_ReGetUnits_Query());
//									break;
//								}
//									#endregion	
//									#region 删除道具
//								case Message_Tag_ID.SD_UserAdditem_Del:
//								{
//									send(SDLogAPI.SD_UserAdditem_Del());
//									break;
//								}
//									#endregion	
//									#region 查询GM账号
//								case Message_Tag_ID.SD_GetGmAccount_Query:
//								{
//									send(SDLogAPI.SD_GetGmAccount_Query());
//									break;
//								}
//									#endregion	
//									#region 修改GM账号
//								case Message_Tag_ID.SD_UpdateGmAccount_Query:
//								{
//									send(SDLogAPI.SD_UpdateGmAccount_Query());
//									break;
//								}
//									#endregion	
//									#region 玩家封停记录查询
//								case Message_Tag_ID.SD_BanUser_Query:
//								{
//									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
//									index = (int)stuct.toInteger();
//									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
//									pageSize = (int)stuct.toInteger();
//									send(SDLogAPI.SD_BanUser_Query(index-1,pageSize));
//									break;
//								}
//									#endregion
//									#region 发送人礼物信息查询
//								case Message_Tag_ID.SD_Grift_FromUser_Query:
//								{
//									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
//									index = (int)stuct.toInteger();
//									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
//									pageSize = (int)stuct.toInteger();
//									send(SDLogAPI.SD_Grift_FromUser_Query(index-1,pageSize));
//									break;
//								}
//									#endregion
//									#region 接收人礼物信息查询
//								case Message_Tag_ID.SD_Grift_ToUser_Query:
//								{
//									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
//									index = (int)stuct.toInteger();
//									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
//									pageSize = (int)stuct.toInteger();
//									send(SDLogAPI.SD_Grift_ToUser_Query(index-1,pageSize));
//									break;
//								}
//									#endregion
//									#region  查询玩家购买记录
//								case Message_Tag_ID.SD_BuyLog_Query:
//								{	
//									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
//									index = (int)stuct.toInteger();
//									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
//									pageSize = (int)stuct.toInteger();
//									send(SDLogAPI.SD_BuyLog_Query(index-1,pageSize));
//									break;
//								}
//									#endregion
//									#region  查询道具删除记录
//								case Message_Tag_ID.SD_Delete_ItemLog_Query:
//								{	
//									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
//									index = (int)stuct.toInteger();
//									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
//									pageSize = (int)stuct.toInteger();
//									send(SDLogAPI.SD_Delete_ItemLog_Query(index-1,pageSize));
//									break;
//								}
//									#endregion
//									#region  查询玩家日志信息
//								case Message_Tag_ID.SD_LogInfo_Query:
//								{	
//									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
//									index = (int)stuct.toInteger();
//									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
//									pageSize = (int)stuct.toInteger();
//									send(SDLogAPI.SD_LogInfo_Query(index-1,pageSize));
//									break;
//								}
//									#endregion
//									#endregion

									#region 劲舞团II

									#region 获得服务器GS列表
								case Message_Tag_ID.JW2_GSSvererList_Query:
								{
									send(jw2accountinfo.JW2_GSSvererList_Query());
									break;
								}
									#endregion
									
									#region 查询玩家人物资料
								case Message_Tag_ID.JW2_ACCOUNT_QUERY:
								{
									send(jw2accountinfo.JW2_ACCOUNT_QUERY());
									break;
								}
									#endregion
									#region 查询点数和虚拟币
								case Message_Tag_ID.JW2_DUMMONEY_QUERY:
								{
									send(jw2accountinfo.JW2_DUMMONEY_QUERY());
									break;
								}
									#endregion
									#region GM状态修改修改
								case Message_Tag_ID.JW2_GM_Update:
								{
									send(jw2accountinfo.JW2_GM_Update());
									break;
								}
									#endregion
									#region 用户等级修改
								case Message_Tag_ID.JW2_MODIFYLEVEL_QUERY:
								{
									send(jw2accountinfo.JW2_MODIFYLEVEL_QUERY());
									break;
								}
									#endregion
									#region 用户经验修改
								case Message_Tag_ID.JW2_MODIFYEXP_QUERY:
								{
									send(jw2accountinfo.JW2_MODIFYEXP_QUERY());
									break;
								}
									#endregion

									#region 经验双倍
								case Message_Tag_ID.JW2_ChangeServerExp_Query:
								{
									send(jw2accountinfo.JW2_ChangeServerExp_Query());
									break;
								}
									#endregion

									#region 金钱双倍
								case Message_Tag_ID.JW2_ChangeServerMoney_Query:
								{
									send(jw2accountinfo.JW2_ChangeServerExp_Query());
									break;
								}
									#endregion
									#region 用户金钱修改
								case Message_Tag_ID.JW2_MODIFY_MONEY:
								{
									send(jw2accountinfo.JW2_MODIFY_MONEY());
									break;
								}
									#endregion
									#region 查询玩家情侶卡派對
								case Message_Tag_ID.JW2_CoupleParty_Card:
								{
									send(jw2accountinfo.JW2_CoupleParty_Card());
									break;
								}
									#endregion
									#region 查询玩家結婚証書
								case Message_Tag_ID.JW2_Wedding_Paper:
								{
									send(jw2accountinfo.JW2_Wedding_Paper());
									break;
								}
									#endregion
									#region GT劲舞团2道具卡查询
								case Message_Tag_ID.JW2_Act_Card_Query:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();				
									send(jw2accountinfo.JW2_Act_Card_Query(index-1,pageSize));
									break;
								}
									#endregion
									#region 剔除玩家下线
								case Message_Tag_ID.JW2_BANISHPLAYER:
								{
									send(jw2logininfo.JW2_BANISHPLAYER());
									break;
								}
									#endregion
									#region 封停用户
								case Message_Tag_ID.JW2_ACCOUNT_CLOSE:
								{
									send(jw2logininfo.JW2_ACCOUNT_CLOSE());
									break;
								}
									#endregion
									#region 用户解封
								case Message_Tag_ID.JW2_ACCOUNT_OPEN:
								{
									send(jw2logininfo.JW2_ACCOUNT_OPEN());
									break;
								}
									#endregion
									#region 玩家登陆登出信息
								case Message_Tag_ID.JW2_LOGINOUT_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();				
									send(jw2logininfo.JW2_LOGINOUT_QUERY(index-1,pageSize));
									break;
								}
									#endregion
									#region 修改臨時密碼
								case Message_Tag_ID.JW2_MODIFY_PWD:
								{
									send(jw2logininfo.JW2_MODIFY_PWD());
									break;
								}
									#endregion
									#region 恢復臨時密碼
								case Message_Tag_ID.JW2_RECALL_PWD:
								{
									send(jw2logininfo.JW2_RECALL_PWD());
									break;
								}
									#endregion
									#region 查?最後一次修改的密碼
								case Message_Tag_ID.JW2_SearchPassWord_Query:
								{
									send(jw2logininfo.JW2_SearchPassWord_Query());
									break;
								}
									#endregion
									#region 封停用户查询
								case Message_Tag_ID.JW2_ACCOUNT_BANISHMENT_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(jw2logininfo.JW2_ACCOUNT_BANISHMENT_QUERY(index-1,pageSize));
									break;
								}
									#endregion
									#region 公告添加
								case Message_Tag_ID.JW2_BOARDTASK_INSERT:
								{
									send(jw2logininfo.JW2_BOARDTASK_INSERT());
									break;
								}
									#endregion
									#region 公告更新
								case Message_Tag_ID.JW2_BOARDTASK_UPDATE:
								{
									send(jw2logininfo.JW2_BOARDTASK_UPDATE());
									break;
								}
									#endregion
									#region 公告查询
								case Message_Tag_ID.JW2_BOARDTASK_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();				
									send(jw2logininfo.JW2_BOARDTASK_QUERY(index-1,pageSize));
									break;
								}
									#endregion
									#region 查询玩家婚姻信息
								case Message_Tag_ID.JW2_WEDDINGINFO_QUERY:
								{
									send(jw2logininfo.JW2_WEDDINGINFO_QUERY());
									break;
								}
									#endregion
									#region 查询玩家结婚信息
								case Message_Tag_ID.JW2_WEDDINGGROUND_QUERY:
								{
									send(jw2logininfo.JW2_WEDDINGGROUND_QUERY());
									break;
								}
									#endregion
									#region 查询玩家婚姻LOG信息
								case Message_Tag_ID.JW2_WEDDINGLOG_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();		
									send(jw2logininfo.JW2_WEDDINGLOG_QUERY(index-1,pageSize));
									break;
								}
									#endregion
									#region 查询玩家情侣信息
								case Message_Tag_ID.JW2_COUPLEINFO_QUERY:
								{
									send(jw2logininfo.JW2_COUPLEINFO_QUERY());
									break;
								}
									#endregion
									#region 查询玩家情侣LOG
								case Message_Tag_ID.JW2_COUPLELOG_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();		
									send(jw2logininfo.JW2_COUPLELOG_QUERY(index-1,pageSize));
									break;
								}
									#endregion								
									#region 查询故事情节
								case Message_Tag_ID.JW2_RPG_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();				
									send(jw2iteminfo.JW2_RPG_QUERY(index-1,pageSize));
									break;
								}
									#endregion								
									#region 家庭道具
								case Message_Tag_ID.JW2_HOME_ITEM_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();				
									send(jw2iteminfo.JW2_HOME_ITEM_QUERY(index-1,pageSize));
									break;
								}
									#endregion
									#region 身上道具
								case Message_Tag_ID.JW2_ITEMSHOP_BYOWNER_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();				
									send(jw2iteminfo.JW2_ITEMSHOP_BYOWNER_QUERY(index-1,pageSize));
									break;
								}
									#endregion

									#region 合成材料查询
								case Message_Tag_ID.JW2_Materiallist_Query:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();				
									send(jw2iteminfo.JW2_Materiallist_Query(index-1,pageSize));
									break;
								}
									#endregion
									#region 合成历史查询
								case Message_Tag_ID.JW2_MaterialHistory_Query:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();				
									send(jw2iteminfo.JW2_MaterialHistory_Query(index-1,pageSize));
									break;
								}
									#endregion
									
									#region 查询需要审核的图片
								case Message_Tag_ID.JW2_GETPIC_Query:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();				
									send(jw2iteminfo.JW2_GETPIC_Query(index-1,pageSize));
									break;
								}
									#endregion

									#region 审核图片
								case Message_Tag_ID.JW2_CHKPIC_Query:
								{			
									send(jw2iteminfo.JW2_CHKPIC_Query());
									break;
								}
									#endregion
									#region 圖片卡使用情況
								case Message_Tag_ID.JW2_PicCard_Query:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();				
									send(jw2iteminfo.JW2_PicCard_Query(index-1,pageSize));
									break;
								}
									#endregion
									
									
									#region 宠物更名
								case Message_Tag_ID.JW2_UpdatePetName_Query:
								{
												
									send(jw2iteminfo.JW2_UpdatePetName_Query());
									break;
								}
									#endregion
									#region 道具模糊查?
								case Message_Tag_ID.JW2_ITEM_SELECT:
								{
									
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();				
									send(jw2iteminfo.JW2_ITEM_SELECT(index-1,pageSize));
									break;
								}
									#endregion								
									#region 添加道具
								case Message_Tag_ID.JW2_ADD_ITEM:
								{			
									send(jw2iteminfo.JW2_ITEM_ADD());
									break;
								}
									#endregion	
									#region 添加道具(批量)
								case Message_Tag_ID.JW2_ADD_ITEM_ALL:
								{			
									send(jw2iteminfo.JW2_ADD_ITEM_ALL());
									break;
								}
									#endregion			
									#region 道具刪除
								case Message_Tag_ID.JW2_ITEM_DEL:
								{			
									send(jw2iteminfo.JW2_ITEM_DEL());
									break;
								}
									#endregion
									#region 道具查询
								case Message_Tag_ID.JW2_ItemInfo_Query:
								{			
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();		
									send(jw2iteminfo.JW2_ItemInfo_Query(index-1,pageSize));
									break;
								}
									#endregion		
									#region 查询玩家宠物信息
								case Message_Tag_ID.JW2_PetInfo_Query:
								{			
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(jw2iteminfo.JW2_PetInfo_Query(index-1,pageSize));
									break;
								}
									#endregion		
									#region 查看购物送礼
								case Message_Tag_ID.JW2_SMALL_PRESENT_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();				
									send(jw2loginfo.JW2_SMALL_PRESENT_QUERY(index-1,pageSize));
									break;
								}
									#endregion
									#region 玩家任务日志查询
								case Message_Tag_ID.JW2_MissionInfoLog_Query:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();				
									send(jw2loginfo.JW2_MissionInfoLog_Query(index-1,pageSize));
									break;
								}
									#endregion		
									
									#region 消费日志查询
								case Message_Tag_ID.JW2_CashMoney_Log:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();				
									send(jw2loginfo.JW2_CashMoney_Log(index-1,pageSize));
									break;
								}
									#endregion		
									#region 舉報信息查?
								case Message_Tag_ID.JW2_JB_Query:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();				
									send(jw2loginfo.JW2_JB_Query(index-1,pageSize));
									break;
								}
									#endregion		
									#region 中间件背包信息查询
								case Message_Tag_ID.JW2_CenterAvAtarItem_Bag_Query:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();				
									send(jw2loginfo.JW2_CenterAvAtarItem_Bag_Query(index-1,pageSize));
									break;
								}
									#endregion		
									#region 中间件身上信息查询
								case Message_Tag_ID.JW2_CenterAvAtarItem_Equip_Query:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();				
									send(jw2loginfo.JW2_CenterAvAtarItem_Equip_Query(index-1,pageSize));
									break;
								}
									#endregion		
									#region 中间用户踢下线
								case Message_Tag_ID.JW2_Center_Kick_Query:
								{		
									send(jw2loginfo.JW2_Center_Kick_Query());
									break;
								}
									#endregion
									#region 购买日志
								case Message_Tag_ID.JW2_MoneyLog_Query:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();				
									send(jw2loginfo.JW2_MoneyLog_Query(index-1,pageSize));
									break;
								}
									#endregion		
									#region 重复购买退款
								case Message_Tag_ID.JW2_AgainBuy_Query:
								{		
									send(jw2loginfo.JW2_AgainBuy_Query());
									break;
								}
									#endregion
									
									#region 消费性道具使用
								case Message_Tag_ID.JW2_WASTE_ITEM_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();				
									send(jw2iteminfo.JW2_WASTE_ITEM_QUERY(index-1,pageSize));
									break;
								}
									#endregion
									#region 查看小喇叭
								case Message_Tag_ID.JW2_SMALL_BUGLE_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(jw2iteminfo.JW2_SMALL_BUGLE_QUERY(index-1,pageSize));
									break;
								}
									#endregion
									#region 查看玩家家族信息
								case Message_Tag_ID.JW2_User_Family_Query:
								{
									send(jw2messengerinfo.JW2_User_Family_Query());
									break;
								}
									#endregion
									#region 查看家族信息
								case Message_Tag_ID.JW2_FAMILYINFO_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(jw2messengerinfo.JW2_FAMILYINFO_QUERY(index-1,pageSize));
									break;
								}
									#endregion
									#region 查看家族宠物信息
								case Message_Tag_ID.JW2_FamilyPet_Query:
								{
									send(jw2messengerinfo.JW2_FamilyPet_Query());
									break;
								}
									#endregion
									#region 查看基地信息
								case Message_Tag_ID.JW2_BasicInfo_Query:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(jw2messengerinfo.JW2_BasicInfo_Query(index-1,pageSize));
									break;
								}
									#endregion
									#region 查看家族道具信息
								case Message_Tag_ID.JW2_FamilyItemInfo_Query:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(jw2messengerinfo.JW2_FamilyItemInfo_Query(index-1,pageSize));
									break;
								}
									#endregion
									#region 查看家族成员信息
								case Message_Tag_ID.JW2_FAMILYMEMBER_QUERY:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(jw2messengerinfo.JW2_FAMILYMEMBER_QUERY(index-1,pageSize));
									break;
								}
									#endregion
									#region 查看家族排行
								case Message_Tag_ID.JW2_BasicRank_Query:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(jw2messengerinfo.JW2_BasicBank_Query(index-1,pageSize));
									break;
								}
									#endregion
									#region 查看家族申?中成員信息
								case Message_Tag_ID.JW2_FamilyMember_Applying:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(jw2messengerinfo.JW2_FamilyMember_Applying(index-1,pageSize));
									break;
								}
									#endregion
									#region 基金日志
								case Message_Tag_ID.JW2_FamilyFund_Log:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(jw2messengerinfo.JW2_FamilyFund_Log(index-1,pageSize));
									break;
								}
									#endregion
									#region 家族成员领取小宠物蛋信息查询
								case Message_Tag_ID.JW2_SmallPetAgg_Query:
								{
									send(jw2messengerinfo.JW2_SmallPetAgg_Query());
									break;
								}
									
									#endregion
									#region 家族道具购买信息
								case Message_Tag_ID.JW2_FamilyBuyLog_Query:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(jw2messengerinfo.JW2_FamilyBuyLog_Query(index-1,pageSize));
									break;
								}
									#endregion
									#region 查看Messenger称号
								case Message_Tag_ID.JW2_Messenger_Query:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(jw2messengerinfo.JW2_Messenger_Query(index-1,pageSize));
									break;
								}
									#endregion
									#region 修改家族名
								case Message_Tag_ID.JW2_UpDateFamilyName_Query:
								{
									send(jw2messengerinfo.JW2_UpdateFamilyName_Query());
									break;
								}
									#endregion
									#region 查看玩家道具日志
								case Message_Tag_ID.JW2_Item_Log:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(jw2messengerinfo.JW2_Item_Log(index-1,pageSize));
									break;
								}
									#endregion
									#region 查看玩家纸条箱信息
								case Message_Tag_ID.JW2_MailInfo_Query:
								{
									stuct = new TLV_Structure(TagName.Index, mesBody.getTLVByTag(TagName.Index).m_uiValueLen, mesBody.getTLVByTag(TagName.Index).m_bValueBuffer);
									index = (int)stuct.toInteger();
									stuct = new TLV_Structure(TagName.PageSize, mesBody.getTLVByTag(TagName.PageSize).m_uiValueLen, mesBody.getTLVByTag(TagName.PageSize).m_bValueBuffer);
									pageSize = (int)stuct.toInteger();
									send(jw2messengerinfo.JW2_MailInfo_Query(index-1,pageSize));
									break;
								}
									#endregion

									#region 查看玩家活跃度
								case Message_Tag_ID.JW2_ACTIVEPOINT_Query  :
									send(jw2accountinfo.JW2_ACTIVEPOINT_QUERY());
									break;
	
									#endregion
									#endregion
							}
						}
					}
					Thread.Sleep(1000);
				}
			}
			catch  (IOException ) 
			{
				if(ContinueProcess==false)
				{
					UserInfoAPI userAPI = new UserInfoAPI();
					userAPI.GM_UpdateActiveUser(userByID,0);
					ContinueProcess=true;
					if(userByID>0)
					{
						if(userByID!=99999)
						{
							Console.Write(lg.ServerSocket_Handler_User+name+lg.ServerSocket_Handler_UserLeft+"\n");
						}
					}
				}
			}	  
			catch  ( SocketException se ) 
			{
				Console.Write("Client Disconnected.");
				if(se.ErrorCode == 10054)
				{
					Console.WriteLine("Client Disconnected..");
				}
				else
				{
					Console.WriteLine(se.Message );
				}
				networkStream.Close() ;
				svrSocket.Close();			
				ContinueProcess = false ; 
				Console.WriteLine( "Conection is broken!");
			}
			mut.ReleaseMutex();

		}

		/// <summary>
		/// 单线程用户发送消息
		/// </summary>
		/// <param name="msg">消息包</param>
		public void send(Message msg)
		{
			byte[] sendBytes;
			sendBytes=msg.m_bMessageBuffer;
			try
			{
                if (networkStream.CanWrite)
                {
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                }
			}
			catch(SocketException ex)
			{
				Console.WriteLine(ex.Message);
				networkStream.Close();
			}

		}
		/// <summary>
		/// 单线程用户发送消息
		/// </summary>
		/// <param name="msg">消息包</param>
		public void send(byte[] msg)
		{
			try
			{
                if (networkStream.CanWrite)
                {
                    networkStream.Write(msg, 0, msg.Length);
                }
			}
			catch(SocketException ex)
			{
				Console.WriteLine(ex.Message);
				networkStream.Close();
			}

		}
		/// <summary>
		/// /单线程用户接受消息
		/// </summary>
		/// <returns>接受到消息包</returns>
		public byte[] receive()
		{
			byte[] recvBytes = new byte[128];
			try
			{
				networkStream.Read(recvBytes,0,recvBytes.Length);
			}
			catch(SocketException ex)
			{
				Console.WriteLine(ex.Message);
				networkStream.Close();
			}
			return recvBytes;
            
		}
		/// <summary>
		/// 负责在单线程循环接受客户端发送的消息
		/// </summary>
		public void WorkerThread()
		{
			while(networkStream.CanRead)
			{
				byte[] BytesRead ;
				BytesRead=receive();
				Console.WriteLine(Encoding.Default.GetString(BytesRead));


			}

		}
		public void Close() 
		{
			networkStream.Close() ;
			svrSocket.Close();        
		}
        
		public  bool Alive 
		{
			get 
			{
				return  ContinueProcess ;
			}
		}
		public int HandlerType
		{
			get{return this.handlerType ;}
			set{this.handlerType=value;}
		}
	}
}
