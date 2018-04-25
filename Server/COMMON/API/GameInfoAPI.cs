using System;
using System.Data;
using System.Text;
using System.Collections;
using Common.DataInfo;
using Common.Logic;
namespace Common.API
{
	/// <summary>
	/// GameInfoAPI 的摘要说明。
	/// </summary>
	public class GameInfoAPI
	{
		Message msg;
		private int gameID = 0;
		private string gameName = null;
		private string gameContent = null;
		public GameInfoAPI(byte[] packet)
		{
			msg= new Message(packet,(uint)packet.Length); 
		}
		/// <summary>
		/// 得到所有游戏信息
		/// </summary>
		/// <returns>游戏数据集</returns>
		public Message GM_QueryAll()
		{
			GMLogAPI logAPI = new GMLogAPI();
			DataSet ds = null;
			try
			{
				ds = GMGameInfo.SelectAll();
				logAPI.writeTitle(LanguageAPI.API_Display + LanguageAPI.API_GameInfoAPI_GameList,LanguageAPI.API_Display + LanguageAPI.API_GameInfoAPI_GameList);
				logAPI.writeContent(LanguageAPI.API_GameInfoAPI_GameID,LanguageAPI.API_GameInfoAPI_GameTitle,LanguageAPI.API_GameInfoAPI_GameDesp);	
				Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
				for(int i=0;i<ds.Tables[0].Rows.Count;i++)
				{
					Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
					byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]);
					strut.AddTagKey(TagName.GameID,TagFormat.TLV_INTEGER,(uint)bytes.Length,bytes);	
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[1]);
					strut.AddTagKey(TagName.GameName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[2]);
					strut.AddTagKey(TagName.GameContent,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	
					structList[i]=strut;
					logAPI.writeData(ds.Tables[0].Rows[i].ItemArray[0].ToString(),ds.Tables[0].Rows[i].ItemArray[1].ToString(),ds.Tables[0].Rows[i].ItemArray[2].ToString());
				}
				Console.Write(logAPI.Buffer.ToString());
				return Message.COMMON_MES_RESP(structList,Msg_Category.GAME_ADMIN,ServiceKey.GAME_QUERY_RESP,3);

			}
			catch(Common.Logic.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}
		/// <summary>
		/// 得到游戏信息类
		/// </summary>
		/// <param name="gameID">游戏ID</param>
		/// <returns>游戏信息类</returns>
		public Message GM_QueryModuleInfo(int gameID)
		{
			System.Data.DataSet ds = null;
			GMLogAPI logAPI = new GMLogAPI();
			try
			{
				ds = GMModuleInfo.SelectAll();
				if(ds.Tables[0].Rows.Count<=0)
				{
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.GAME_ADMIN,ServiceKey.GAME_MODULE_QUERY_RESP);
				}
				else
				{
					logAPI.writeTitle(LanguageAPI.API_Display + LanguageAPI.API_GameInfoAPI_GameModuleList,LanguageAPI.API_Display + LanguageAPI.API_GameInfoAPI_GameModuleList);
					logAPI.writeContent(LanguageAPI.API_GameInfoAPI_ModuleID,LanguageAPI.API_GameInfoAPI_ModuleTitle,LanguageAPI.API_GameInfoAPI_ModuleClass,LanguageAPI.API_GameInfoAPI_ModuleDesp);	
					Query_Structure[] structList = new Query_Structure[ds.Tables[0].Rows.Count];
					for(int i=0;i<ds.Tables[0].Rows.Count;i++)
					{
						Query_Structure strut = new Query_Structure((uint)ds.Tables[0].Rows[i].ItemArray.Length);
						strut.AddTagKey(TagName.Module_ID,TagFormat.TLV_INTEGER,4,TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[1]));
						strut.AddTagKey(TagName.GameID,TagFormat.TLV_INTEGER,4,TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER,ds.Tables[0].Rows[i].ItemArray[0]));	
						byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[2]);
						strut.AddTagKey(TagName.GameName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[3]);
						strut.AddTagKey(TagName.ModuleName,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[4]);
						strut.AddTagKey(TagName.ModuleClass,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);
						bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING,ds.Tables[0].Rows[i].ItemArray[5]);
						strut.AddTagKey(TagName.ModuleContent,TagFormat.TLV_STRING,(uint)bytes.Length,bytes);	
						structList[i]=strut;
						logAPI.writeData(ds.Tables[0].Rows[i].ItemArray[1].ToString(),ds.Tables[0].Rows[i].ItemArray[2].ToString(),ds.Tables[0].Rows[i].ItemArray[3].ToString(),ds.Tables[0].Rows[i].ItemArray[4].ToString());
					}
					Console.Write(logAPI.Buffer.ToString());
					return Message.COMMON_MES_RESP(structList,Msg_Category.GAME_ADMIN,ServiceKey.GAME_MODULE_QUERY_RESP,6);

				}
			}

			catch(System.Exception ex)
			{
				Console.WriteLine(ex.Message);	
				return null;
				
			}
			
		}
		/// <summary>
		/// 插入一个新的游戏
		/// </summary>
		public Message GM_InsertGameInfo()
		{
			int result = -1;
			int userByID = 0;
			string gameName = null;
			string gameContent= null;
			GMLogAPI logApi = new GMLogAPI();
			try
			{

				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userByID = (int)strut.toInteger();
                gameName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.GameName).m_bValueBuffer);
				gameContent = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.GameContent).m_bValueBuffer);
				result = GMGameInfo.insertRow(userByID,gameName,gameContent);
				if(result==1)
				{
					logApi.writeTitle(LanguageAPI.API_Add + LanguageAPI.API_GameInfoAPI_GameInfo,LanguageAPI.API_Add + LanguageAPI.API_GameInfoAPI_GameInfo + LanguageAPI.API_Success + "！");
					logApi.writeContent(LanguageAPI.API_DepartmentAPI_OperatorID,LanguageAPI.API_GameInfoAPI_GameTitle,LanguageAPI.API_GameInfoAPI_GameDesp);
					logApi.writeData(Convert.ToString(userByID),gameName,gameContent);
					Console.WriteLine(logApi.Buffer.ToString());
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.GAME_ADMIN,ServiceKey.GAME_CREATE_RESP);
				}
				else
				{
					logApi.writeTitle(LanguageAPI.API_Add + LanguageAPI.API_GameInfoAPI_GameInfo,LanguageAPI.API_Add + LanguageAPI.API_GameInfoAPI_GameInfo + LanguageAPI.API_Failure + "！");
					logApi.writeContent(LanguageAPI.API_DepartmentAPI_OperatorID,LanguageAPI.API_GameInfoAPI_GameTitle,LanguageAPI.API_GameInfoAPI_GameDesp);
					logApi.writeData(Convert.ToString(userByID),gameName,gameContent);
					Console.WriteLine(logApi.Buffer.ToString());
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.GAME_ADMIN,ServiceKey.GAME_CREATE_RESP);
				}

			}
			catch(Common.Logic.Exception ex)
			{
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.GAME_ADMIN,ServiceKey.GAME_CREATE_RESP);
			}

		}
		/// <summary>
		///  删除一个游戏信息
		/// </summary>
		public Message GM_DelGameInfo()
		{
			GMLogAPI logAPI = new GMLogAPI();
			int result = -1;
			int userByID = 0;
			int gameID = 0;
			try
			{
				
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userByID = (int)strut.toInteger();
				strut = new TLV_Structure(TagName.GameID,4,msg.m_packet.m_Body.getTLVByTag(TagName.GameID).m_bValueBuffer);
				gameID = (int)strut.toInteger();
				result = GMGameInfo.deleteRow(userByID,gameID);
				if(result==1)
				{
					logAPI.writeTitle(LanguageAPI.API_Delete + LanguageAPI.API_GameInfoAPI_GameInfo,LanguageAPI.API_Delete + LanguageAPI.API_GameInfoAPI_GameInfo + LanguageAPI.API_Success + "！");
					logAPI.writeContent(LanguageAPI.API_DepartmentAPI_OperatorID,LanguageAPI.API_GameInfoAPI_GameID,LanguageAPI.API_GameInfoAPI_GameTitle,LanguageAPI.API_GameInfoAPI_GameDesp);
					logAPI.writeData(Convert.ToString(userByID),Convert.ToString(gameID),gameName,gameContent);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.GAME_ADMIN,ServiceKey.GAME_DELETE_RESP);

				}
				else
				{
					logAPI.writeTitle(LanguageAPI.API_Delete + LanguageAPI.API_GameInfoAPI_GameInfo,LanguageAPI.API_Delete + LanguageAPI.API_GameInfoAPI_GameInfo + LanguageAPI.API_Failure + "！");
					logAPI.writeContent(LanguageAPI.API_DepartmentAPI_OperatorID,LanguageAPI.API_GameInfoAPI_GameID,LanguageAPI.API_GameInfoAPI_GameTitle,LanguageAPI.API_GameInfoAPI_GameDesp);
					logAPI.writeData(Convert.ToString(userByID),Convert.ToString(gameID),gameName,gameContent);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.GAME_ADMIN,ServiceKey.GAME_DELETE_RESP);

				}
			}
			catch(Common.Logic.Exception ex)
			{
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.GAME_ADMIN,ServiceKey.GAME_DELETE_RESP);
			}

		}
		/// <summary>
		/// 更新一个游戏信息
		/// </summary>
		public Message GM_UpdateGameInfo()
		{
			int result = -1;
			int userByID = 0;
			int gameID = 0;
			string gameName = null;
			string gameContent= null;
			GMGameInfo gameInfo = null;
			GMLogAPI logAPI = new GMLogAPI();
			try
			{
				TLV_Structure strut = new TLV_Structure(TagName.UserByID,4,msg.m_packet.m_Body.getTLVByTag(TagName.UserByID).m_bValueBuffer);
				userByID = (int)strut.toInteger();

				strut = new TLV_Structure(TagName.GameID,4,msg.m_packet.m_Body.getTLVByTag(TagName.GameID).m_bValueBuffer);
				gameID = (int)strut.toInteger();
				gameName = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.GameName).m_bValueBuffer);
				gameContent = Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.GameContent).m_bValueBuffer);
				gameInfo = new GMGameInfo(gameID,gameName,gameContent);
				result = gameInfo.updateRow(userByID,gameID,gameName,gameContent);
				if(result==1)
				{
					logAPI.writeTitle(LanguageAPI.API_Update + LanguageAPI.API_GameInfoAPI_GameInfo,LanguageAPI.API_Update + LanguageAPI.API_GameInfoAPI_GameInfo + LanguageAPI.API_Success + "！");
					logAPI.writeContent(LanguageAPI.API_DepartmentAPI_OperatorID,LanguageAPI.API_GameInfoAPI_GameTitle,LanguageAPI.API_GameInfoAPI_GameDesp);
					logAPI.writeData(Convert.ToString(userByID),gameName,gameContent);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.COMMON_MES_RESP("SUCESS",Msg_Category.GAME_ADMIN,ServiceKey.GAME_UPDATE_RESP);

				}
				else
				{
					
					logAPI.writeTitle(LanguageAPI.API_Update + LanguageAPI.API_GameInfoAPI_GameInfo,LanguageAPI.API_Update + LanguageAPI.API_GameInfoAPI_GameInfo + LanguageAPI.API_Failure + "！");
					logAPI.writeContent(LanguageAPI.API_DepartmentAPI_OperatorID,LanguageAPI.API_GameInfoAPI_GameTitle,LanguageAPI.API_GameInfoAPI_GameDesp);
					logAPI.writeData(Convert.ToString(userByID),gameName,gameContent);
					Console.WriteLine(logAPI.Buffer.ToString());
					return Message.COMMON_MES_RESP("FAILURE",Msg_Category.GAME_ADMIN,ServiceKey.GAME_UPDATE_RESP);

				}
			}
			catch(Common.Logic.Exception ex)
			{
				Console.WriteLine(ex.Message);
				return Message.COMMON_MES_RESP(ex.Message,Msg_Category.GAME_ADMIN,ServiceKey.GAME_UPDATE_RESP);
			}

		}
		public int GameID
		{
			get
			{
				return this.gameID;
			}
			set
			{
				this.gameID = value;
			}
		}
		public string GameName
		{
			get
			{
				return this.gameName;
			}
			set
			{
				this.gameName =value;
			}
		}
		public string GameContent
		{
			get
			{
				return this.gameContent;
			}
			set
			{
				this.gameContent =value;
			}
		}
	}
}
