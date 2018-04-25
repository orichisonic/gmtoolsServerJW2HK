  using System;
using System.Configuration;
using System.Collections;

namespace Common.API
{
	/// <summary>
	/// LanguageConfig 的摘要说明。
	/// </summary>
	public class LanguageAPI
	{
		public LanguageAPI()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		/// <summary>
		/// 根据key值得到value
		/// </summary>
		/// <param name="game">游戏名称</param>
		/// <param name="key">关键字</param>
		/// <returns>value</returns>
		public static String GetValue(String game,String key)
		{
			System.Collections.IDictionary LanguageConfig = (System.Collections.IDictionary)
				System.Configuration.ConfigurationSettings.GetConfig(game);
			if (LanguageConfig == null)
				return game + " IS Not Exist In This GMTools";
			return LanguageConfig[key].ToString();
		}
		
		#region MainFrame
		public static String ServerSocket_Handler_User = GetValue("MainFrame","ServerSocket_Handler_User");
		public static String ServerSocket_Handler_UserLeft = GetValue("MainFrame","ServerSocket_Handler_UserLeft");
		public static String ServerSocket_ServerSocket_GMTools_Title = GetValue("MainFrame","ServerSocket_ServerSocket_GMTools_Title");
		public static String ServerSocket_ServerSocket_GMTools_Port = GetValue("MainFrame","ServerSocket_ServerSocket_GMTools_Port");
		public static String ServerSocket_ServerSocket_GMTools_Accept = GetValue("MainFrame","ServerSocket_ServerSocket_GMTools_Accept");
		public static String ServerSocket_ServerSocket_GMTools_Client = GetValue("MainFrame","ServerSocket_ServerSocket_GMTools_Client");
		public static String ServerSocket_ServerSocket_GMTools_Validate = GetValue("MainFrame","ServerSocket_ServerSocket_GMTools_Validate");
		public static String ServerSocket_Task_Continue = GetValue("MainFrame","ServerSocket_Task_Continue");
		public static String ServerSocket_Task_Query = GetValue("MainFrame","ServerSocket_Task_Query");
		public static String ServerSocket_UpdatePatch_Error = GetValue("MainFrame","ServerSocket_UpdatePatch_Error");
		#endregion 

		#region Common
		public static String Logic_Exception_Parameter = GetValue("Common","Logic_Exception_Parameter");
		public static String Logic_Exception_ExpectedType = GetValue("Common","Logic_Exception_ExpectedType");
		public static String Logic_Exception_RealType = GetValue("Common","Logic_Exception_RealType");
		public static String Logic_Exception_ExpectedValue = GetValue("Common","Logic_Exception_ExpectedValue");
		public static String Logic_Exception_RealValue = GetValue("Common","Logic_Exception_RealValue");
		public static String Logic_Exception_Error = GetValue("Common","Logic_Exception_Error");
		public static String Logic_TLV_Structure_NumLength = GetValue("Common","Logic_TLV_Structure_NumLength");
		public static String Logic_TLV_Structure_TagFormatType = GetValue("Common","Logic_TLV_Structure_TagFormatType");
		public static String Logic_UserValidate_User = GetValue("Common","Logic_UserValidate_User");
		public static String Logic_UserValidate_AcceptData = GetValue("Common","Logic_UserValidate_AcceptData");
		public static String Logic_UserValidate_ValidateFailue = GetValue("Common","Logic_UserValidate_ValidateFailue");
		public static String Logic_UserValidate_LoggingFailue = GetValue("Common","Logic_UserValidate_LoggingFailue");
		public static String API_Look = GetValue("Common","API_Look");
		public static String API_Error = GetValue("Common","API_Error");
		public static String API_To = GetValue("Common","API_To");
		public static String API_From = GetValue("Common","API_From");
		public static String API_Add = GetValue("Common","API_Add");
		public static String API_Create = GetValue("Common","API_Create");
		public static String API_Delete = GetValue("Common","API_Delete");
		public static String API_Update = GetValue("Common","API_Update");
		public static String API_Success = GetValue("Common","API_Success");
		public static String API_Failure = GetValue("Common","API_Failure");
		public static String API_Display = GetValue("Common","API_Display");
		public static String API_Description = GetValue("Common","API_Description");
		public static String API_CommonAPI_NewServer = GetValue("Common","API_CommonAPI_NewServer");
		public static String API_CommonAPI_GameID = GetValue("Common","API_CommonAPI_GameID");
		public static String API_CommonAPI_ServerIP = GetValue("Common","API_CommonAPI_ServerIP");
		public static String API_CommonAPI_GameCity = GetValue("Common","API_CommonAPI_GameCity");
		public static String API_CommonAPI_GameListEmpty = GetValue("Common","API_CommonAPI_GameListEmpty");
		public static String API_CommonAPI_NoLog = GetValue("Common","API_CommonAPI_NoLog");
		public static String API_DepartmentAPI_DepInfo = GetValue("Common","API_DepartmentAPI_DepInfo");
		public static String API_DepartmentAPI_NoDepInfo = GetValue("Common","API_DepartmentAPI_NoDepInfo");
		public static String API_DepartmentAPI_OperatorID = GetValue("Common","API_DepartmentAPI_OperatorID");
		public static String API_DepartmentAPI_DepID = GetValue("Common","API_DepartmentAPI_DepID");
		public static String API_DepartmentAPI_DepTitle = GetValue("Common","API_DepartmentAPI_DepTitle");
		public static String API_DepartmentAPI_DepDesp = GetValue("Common","API_DepartmentAPI_DepDesp");
		public static String API_DepartmentAPI_GameList = GetValue("Common","API_DepartmentAPI_GameList");
		public static String API_DepartmentAPI_HoldGame = GetValue("Common","API_DepartmentAPI_HoldGame");
		public static String API_GameInfoAPI_GameList = GetValue("Common","API_GameInfoAPI_GameList");
		public static String API_GameInfoAPI_GameID = GetValue("Common","API_GameInfoAPI_GameID");
		public static String API_GameInfoAPI_GameTitle = GetValue("Common","API_GameInfoAPI_GameTitle");
		public static String API_GameInfoAPI_GameDesp = GetValue("Common","API_GameInfoAPI_GameDesp");
		public static String API_GameInfoAPI_GameInfo = GetValue("Common","API_GameInfoAPI_GameInfo");
		public static String API_GameInfoAPI_ModuleID = GetValue("Common","API_GameInfoAPI_ModuleID");
		public static String API_GameInfoAPI_ModuleTitle = GetValue("Common","API_GameInfoAPI_ModuleTitle");
		public static String API_GameInfoAPI_ModuleDesp = GetValue("Common","API_GameInfoAPI_ModuleDesp");
		public static String API_GameInfoAPI_ModuleClass = GetValue("Common","API_GameInfoAPI_ModuleClass");
		public static String API_GameInfoAPI_GameModuleList = GetValue("Common","API_GameInfoAPI_GameModuleList");
		public static String API_ModuleInfoAPI_ModuleInfo = GetValue("Common","API_ModuleInfoAPI_ModuleInfo");
		public static String API_ModuleInfoAPI_NoModuleInfo = GetValue("Common","API_ModuleInfoAPI_NoModuleInfo");
		public static String API_ModuleInfoAPI_ModuleList = GetValue("Common","API_ModuleInfoAPI_ModuleList");
		public static String API_ModuleInfoAPI_NoModuleList = GetValue("Common","API_ModuleInfoAPI_NoModuleList");
		public static String API_NotesInfoAPI_NotesEmailList = GetValue("Common","API_NotesInfoAPI_NotesEmailList");
		public static String API_NotesInfoAPI_NotesTransEmailList = GetValue("Common","API_NotesInfoAPI_NotesTransEmailList");
		public static String API_NotesInfoAPI_EmailID = GetValue("Common","API_NotesInfoAPI_EmailID");
		public static String API_NotesInfoAPI_EmailSubject = GetValue("Common","API_NotesInfoAPI_EmailSubject");
		public static String API_NotesInfoAPI_EmailContent = GetValue("Common","API_NotesInfoAPI_EmailContent");
		public static String API_NotesInfoAPI_EmailSender = GetValue("Common","API_NotesInfoAPI_EmailSender");
		public static String API_NotesInfoAPI_NoDealWithEmail = GetValue("Common","API_NotesInfoAPI_NoDealWithEmail");
		public static String API_NotesInfoAPI_NoTransDealWithEmail = GetValue("Common","API_NotesInfoAPI_NoTransDealWithEmail");
		public static String API_NotesInfoAPI_DealWithEmailFailure = GetValue("Common","API_NotesInfoAPI_DealWithEmailFailure");
		public static String API_UserInfoAPI_NoUserList = GetValue("Common","API_UserInfoAPI_NoUserList");
		public static String API_UserInfoAPI_AccountInfo = GetValue("Common","API_UserInfoAPI_AccountInfo");
		public static String API_UserInfoAPI_Password = GetValue("Common","API_UserInfoAPI_Password");
		public static String API_UserInfoAPI_NewPassword = GetValue("Common","API_UserInfoAPI_NewPassword");
		public static String API_UserInfoAPI_MAC = GetValue("Common","API_UserInfoAPI_MAC");
		public static String API_UserInfoAPI_NoAdmin = GetValue("Common","API_UserInfoAPI_NoAdmin");
		public static String API_UserInfoAPI_UserStatus = GetValue("Common","API_UserInfoAPI_UserStatus");
		public static String API_UserInfoAPI_LimitDay = GetValue("Common","API_UserInfoAPI_LimitDay");
		public static String API_UserInfoAPI_UserID = GetValue("Common","API_UserInfoAPI_UserID");
		public static String API_UserModuleAPI_NoRecord = GetValue("Common","API_UserModuleAPI_NoRecord");
		public static String API_UserModuleAPI_UserAuth = GetValue("Common","API_UserModuleAPI_UserAuth");
		public static String API_UserModuleAPI_UserModule = GetValue("Common","API_UserModuleAPI_UserModule");
		#endregion

		#region RayCity
		public static String RayCityAPI_Name = GetValue("RayCity","RayCityAPI_Name");
		public static String RayCityAPI_Address = GetValue("RayCity","RayCityAPI_Address");
		public static String RayCityAPI_Account = GetValue("RayCity","RayCityAPI_Account");

		

		public static String RayCityAPI_Char = GetValue("RayCity","RayCityAPI_Char");
		public static String RayCityAPI_Again = GetValue("RayCity","RayCityAPI_Again");
		public static String RayCityAPI_Set = GetValue("RayCity","RayCityAPI_Set");
		public static String RayCityAPI_AccountInfo = GetValue("RayCity","RayCityAPI_AccountInfo");
		public static String RayCityAPI_NoAccountInfo = GetValue("RayCity","RayCityAPI_NoAccountInfo");
		public static String RayCityAPI_CurrentState = GetValue("RayCity","RayCityAPI_CurrentState");
		public static String RayCityAPI_NoCurrentState = GetValue("RayCity","RayCityAPI_NoCurrentState");
		public static String RayCityAPI_Car_CurrentState = GetValue("RayCity","RayCityAPI_Car_CurrentState");
		public static String RayCityAPI_Car_NoCurrentState = GetValue("RayCity","RayCityAPI_Car_NoCurrentState");
		public static String RayCityAPI_Car_itemState = GetValue("RayCity","RayCityAPI_Car_itemState");
		public static String RayCityAPI_Car_NoitemState = GetValue("RayCity","RayCityAPI_Car_NoitemState");
		public static String RayCityAPI_Car_ConnectState = GetValue("RayCity","RayCityAPI_Car_ConnectState");
		public static String RayCityAPI_Car_NoConnectState = GetValue("RayCity","RayCityAPI_Car_NoConnectState");


		
		public static String RayCityAPI_FirendState = GetValue("RayCity","RayCityAPI_FirendState");
		public static String RayCityAPI_NoFirendState = GetValue("RayCity","RayCityAPI_NoFirendState");
		public static String RayCityAPI_SpeedState = GetValue("RayCity","RayCityAPI_SpeedState");
		public static String RayCityAPI_NoSpeedState = GetValue("RayCity","RayCityAPI_NoSpeedState");
		public static String RayCityAPI_firstAddress = GetValue("RayCity","RayCityAPI_firstAddress");
		public static String RayCityAPI_UserChargeInfo = GetValue("RayCity","RayCityAPI_UserChargeInfo");
		public static String RayCityAPI_NoUserChargeInfo = GetValue("RayCity","RayCityAPI_NoUserChargeInfo");
		public static String RayCityAPI_Coupon_Query = GetValue("RayCity","RayCityAPI_Coupon_Query");
		public static String RayCityAPI_NoCoupon_Query = GetValue("RayCity","RayCityAPI_NoCoupon_Query");
		public static String RayCityAPI_ActiveCard_Query = GetValue("RayCity","RayCityAPI_ActiveCard_Query");
		public static String RayCityAPI_BuyCarInfo = GetValue("RayCity","RayCityAPI_BuyCarInfo");
		public static String RayCityAPI_NoBuyCarInfo = GetValue("RayCity","RayCityAPI_NoBuyCarInfo");
		public static String RayCityAPI_BuySellItemInfo = GetValue("RayCity","RayCityAPI_BuySellItemInfo");
		public static String RayCityAPI_NoBuySellItemInfo = GetValue("RayCity","RayCityAPI_NoBuySellItemInfo");
		public static String RayCityAPI_TradeItemInfo = GetValue("RayCity","RayCityAPI_TradeItemInfo");


		public static String RayCityAPI_NoTradeItemInfo = GetValue("RayCity","RayCityAPI_NoTradeItemInfo");
		public static String RayCityAPI_TradeItemInfoDetail = GetValue("RayCity","RayCityAPI_TradeItemInfoDetail");
		public static String RayCityAPI_NoTradeItemInfoDetail = GetValue("RayCity","RayCityAPI_NoTradeItemInfoDetail");
		public static String RayCityAPI_BingoCard = GetValue("RayCity","RayCityAPI_BingoCard");
		public static String RayCityAPI_NoBingoCard = GetValue("RayCity","RayCityAPI_NoBingoCard");
		public static String RayCityAPI_InOut = GetValue("RayCity","RayCityAPI_InOut");
		public static String RayCityAPI_NoInOut = GetValue("RayCity","RayCityAPI_NoInOut");
		public static String RayCityAPI_MissionInfo = GetValue("RayCity","RayCityAPI_MissionInfo");
		public static String RayCityAPI_NoMissionInfo = GetValue("RayCity","RayCityAPI_NoMissionInfo");
		public static String RayCityAPI_MoneyLog = GetValue("RayCity","RayCityAPI_MoneyLog");
		public static String RayCityAPI_NoMoneyLog = GetValue("RayCity","RayCityAPI_NoMoneyLog");
		public static String RayCityAPI_RaceLog = GetValue("RayCity","RayCityAPI_RaceLog");
		public static String RayCityAPI_NoRaceLog = GetValue("RayCity","RayCityAPI_NoRaceLog");
		public static String RayCityAPI_MailLog = GetValue("RayCity","RayCityAPI_MailLog");
		public static String RayCityAPI_NoMailLog = GetValue("RayCity","RayCityAPI_NoMailLog");
		public static String RayCityAPI_RaceUserConsume = GetValue("RayCity","RayCityAPI_RaceUserConsume");

		public static String RayCityAPI_NoRaceUserConsume = GetValue("RayCity","RayCityAPI_NoRaceUserConsume");
		public static String RayCityAPI_CashItemDetailLog = GetValue("RayCity","RayCityAPI_CashItemDetailLog");
		public static String RayCityAPI_NoCashItemDetailLog = GetValue("RayCity","RayCityAPI_NoCashItemDetailLog");
		public static String RayCityAPI_QuestLog = GetValue("RayCity","RayCityAPI_QuestLog");
		public static String RayCityAPI_NoQuestLog = GetValue("RayCity","RayCityAPI_NoQuestLog");
		public static String RayCityAPI_Guild = GetValue("RayCity","RayCityAPI_Guild");
		public static String RayCityAPI_NoGuild = GetValue("RayCity","RayCityAPI_NoGuild");
		public static String RayCityAPI_GuildMember = GetValue("RayCity","RayCityAPI_GuildMember");
		public static String RayCityAPI_NoGuildMember = GetValue("RayCity","RayCityAPI_NoGuildMember");
		public static String RayCityAPI_WarehousePassword = GetValue("RayCity","RayCityAPI_WarehousePassword");
		public static String RayCityAPI_BanAccount = GetValue("RayCity","RayCityAPI_BanAccount");
		public static String RayCityAPI_GMAccount = GetValue("RayCity","RayCityAPI_GMAccount");
		public static String RayCityAPI_Notices = GetValue("RayCity","RayCityAPI_Notices");
		public static String RayCityAPI_NoNotices = GetValue("RayCity","RayCityAPI_NoNotices");
		public static String RayCityAPI_ItemShopType = GetValue("RayCity","RayCityAPI_ItemShopType");
		public static String RayCityAPI_NoItemInfo = GetValue("RayCity","RayCityAPI_NoItemInfo");
		public static String RayCityAPI_SkillInfo = GetValue("RayCity","RayCityAPI_SkillInfo");
		public static String RayCityAPI_ItemInfo = GetValue("RayCity","RayCityAPI_ItemInfo");
		public static String RayCityAPI_NoSkillInfo = GetValue("RayCity","RayCityAPI_NoSkillInfo");
		

		#endregion

		#region SD
		public static String SDAPI_SD = GetValue("SD","SDAPI_SD");
		public static String SDAPI_SDChallengeDataAPI_Challenge = GetValue("SD","SDAPI_SDChallengeDataAPI_Challenge");
		public static String SDAPI_SDChallengeDataAPI_Scene = GetValue("SD","SDAPI_SDChallengeDataAPI_Scene");
		public static String SDAPI_SDChallengeDataAPI_SceneProbability = GetValue("SD","SDAPI_SDChallengeDataAPI_SceneProbability");
		public static String SDAPI_SDChallengeDataAPI_GameMusicList = GetValue("SD","SDAPI_SDChallengeDataAPI_GameMusicList");
		public static String SDAPI_SDChallengeDataAPI_ProbabilityList = GetValue("SD","SDAPI_SDChallengeDataAPI_ProbabilityList");
		public static String SDAPI_SDChallengeDataAPI_NoChallengeScene = GetValue("SD","SDAPI_SDChallengeDataAPI_NoChallengeScene");
		public static String SDAPI_SDChallengeDataAPI_NoGameMusicList = GetValue("SD","SDAPI_SDChallengeDataAPI_NoGameMusicList");
		public static String SDAPI_SDChallengeDataAPI_NoSceneList = GetValue("SD","SDAPI_SDChallengeDataAPI_NoSceneList");
		public static String SDAPI_SDChallengeDataAPI_NoProbabilityList = GetValue("SD","SDAPI_SDChallengeDataAPI_NoProbabilityList");
		public static String SDAPI_SDCharacterInfoAPI_NoAccount = GetValue("SD","SDAPI_SDCharacterInfoAPI_NoAccount");
		public static String SDAPI_SDCharacterInfoAPI_AccountInfo = GetValue("SD","SDAPI_SDCharacterInfoAPI_AccountInfo");
		public static String SDAPI_SDCharacterInfoAPI_NoRelativeInfo = GetValue("SD","SDAPI_SDCharacterInfoAPI_NoRelativeInfo");
		public static String SDAPI_SDItemLogInfoAPI_Account = GetValue("SD","SDAPI_SDItemLogInfoAPI_Account");
		public static String SDAPI_SDItemLogInfoAPI_FillDetail = GetValue("SD","SDAPI_SDItemLogInfoAPI_FillDetail");
		public static String SDAPI_SDItemLogInfoAPI_Sum = GetValue("SD","SDAPI_SDItemLogInfoAPI_Sum");
		public static String SDAPI_SDItemLogInfoAPI_GCash = GetValue("SD","SDAPI_SDItemLogInfoAPI_GCash");
		public static String SDAPI_SDItemLogInfoAPI_Compensate = GetValue("SD","SDAPI_SDItemLogInfoAPI_Compensate");
		public static String SDAPI_SDItemLogInfoAPI_NoChargeRecord = GetValue("SD","SDAPI_SDItemLogInfoAPI_NoChargeRecord");
		public static String SDAPI_SDItemLogInfoAPI_NoTotalValue = GetValue("SD","SDAPI_SDItemLogInfoAPI_NoTotalValue");
		public static String SDAPI_SDItemShopAPI_GameItem = GetValue("SD","SDAPI_SDItemShopAPI_GameItem");
		public static String SDAPI_SDItemShopAPI_PersonalItem = GetValue("SD","SDAPI_SDItemShopAPI_PersonalItem");
		public static String SDAPI_SDItemShopAPI_GiftItem = GetValue("SD","SDAPI_SDItemShopAPI_GiftItem");
		public static String SDAPI_SDItemShopAPI_OnlineStatus = GetValue("SD","SDAPI_SDItemShopAPI_OnlineStatus");
		public static String SDAPI_SDItemShopAPI_ConsumeRecord = GetValue("SD","SDAPI_SDItemShopAPI_ConsumeRecord");
		public static String SDAPI_SDItemShopAPI_TradeRecord = GetValue("SD","SDAPI_SDItemShopAPI_TradeRecord");
		public static String SDAPI_SDItemShopAPI_NoItem = GetValue("SD","SDAPI_SDItemShopAPI_NoItem");
		public static String SDAPI_SDItemShopAPI_NoItemOnPlayer = GetValue("SD","SDAPI_SDItemShopAPI_NoItemOnPlayer");
		public static String SDAPI_SDItemShopAPI_NoItemOnGift = GetValue("SD","SDAPI_SDItemShopAPI_NoItemOnGift");
		public static String SDAPI_SDItemShopAPI_NoOnlineStatus = GetValue("SD","SDAPI_SDItemShopAPI_NoOnlineStatus");
		public static String SDAPI_SDItemShopAPI_NoItemLimit = GetValue("SD","SDAPI_SDItemShopAPI_NoItemLimit");
		public static String SDAPI_SDItemShopAPI_NoChargeRecord = GetValue("SD","SDAPI_SDItemShopAPI_NoChargeRecord");
		public static String SDAPI_SDItemShopAPI_NoTradeRecord = GetValue("SD","SDAPI_SDItemShopAPI_NoTradeRecord");
		public static String SDAPI_SDMemberInfoAPI_ActiveState = GetValue("SD","SDAPI_SDMemberInfoAPI_ActiveState");
		public static String SDAPI_SDMemberInfoAPI_NoActived = GetValue("SD","SDAPI_SDMemberInfoAPI_NoActived");
		public static String SDAPI_SDMemberInfoAPI_AllBanAccount = GetValue("SD","SDAPI_SDMemberInfoAPI_AllBanAccount");
		public static String SDAPI_SDMemberInfoAPI_NoBanAccount = GetValue("SD","SDAPI_SDMemberInfoAPI_NoBanAccount");
		public static String SDAPI_SDMemberInfoAPI_BanInfo = GetValue("SD","SDAPI_SDMemberInfoAPI_BanInfo");
		public static String SDAPI_SDMemberInfoAPI_NoBanInfo = GetValue("SD","SDAPI_SDMemberInfoAPI_NoBanInfo");
		public static String SDAPI_SDMemberInfoAPI_AccountUnlock = GetValue("SD","SDAPI_SDMemberInfoAPI_AccountUnlock");
		public static String SDAPI_SDMemberInfoAPI_AccountLock = GetValue("SD","SDAPI_SDMemberInfoAPI_AccountLock");
		public static String SDAPI_SDMemberInfoAPI_NoCurrentInfo = GetValue("SD","SDAPI_SDMemberInfoAPI_NoCurrentInfo");
		public static String SDAPI_SDMemberInfoAPI_CurrentState = GetValue("SD","SDAPI_SDMemberInfoAPI_CurrentState");
		public static String SDAPI_SDNoticeInfoAPI_SendNoticeInfo = GetValue("SD","SDAPI_SDNoticeInfoAPI_SendNoticeInfo");
		public static String SDAPI_SDNoticeInfoAPI_SendNoticeList = GetValue("SD","SDAPI_SDNoticeInfoAPI_SendNoticeList");
		public static String SDAPI_SDNoticeInfoAPI_NoNoticeList = GetValue("SD","SDAPI_SDNoticeInfoAPI_NoNoticeList");
		public static String SDAPI_SDNoticeInfoAPI_ChannelList = GetValue("SD","SDAPI_SDNoticeInfoAPI_ChannelList");
		public static String SDAPI_SDNoticeInfoAPI_NoChannelInfo = GetValue("SD","SDAPI_SDNoticeInfoAPI_NoChannelInfo");
		public static String SDAPI_SDNoticeInfoAPI_LoginFailure = GetValue("SD","SDAPI_SDNoticeInfoAPI_LoginFailure");
		public static String SDAPI_SDCharacterInfoAPI_FriendList = GetValue("SD","SDAPI_SDCharacterInfoAPI_FriendList");
		public static String SDAPI_SDCharacterInfoAPI_NoFriendList = GetValue("SD","SDAPI_SDCharacterInfoAPI_NoFriendList");
		public static String SDAPI_SDCharacterInfoAPI_NotOnline = GetValue("SD","SDAPI_SDCharacterInfoAPI_NotOnline");
		public static String SDAPI_SDCharacterInfoAPI_KickSuccess = GetValue("SD","SDAPI_SDCharacterInfoAPI_KickSuccess");
		public static String SDAPI_SDCharacterInfoAPI_KickFailure = GetValue("SD","SDAPI_SDCharacterInfoAPI_KickFailure");
		public static String SDAPI_SDCharacterInfoAPI_GateWay = GetValue("SD","SDAPI_SDCharacterInfoAPI_GateWay");
		public static String SDAPI_SDItemShopAPI_Integral = GetValue("SD","SDAPI_SDItemShopAPI_Integral");
		public static String SDAPI_SDItemShopAPI_NoIntegral = GetValue("SD","SDAPI_SDItemShopAPI_NoIntegral");
		
		public static String SDAPI_SDItemMsG = GetValue("SD","SDAPI_SDItemMsG");
		public static String SDAPI_SDItemMsG1 = GetValue("SD","SDAPI_SDItemMsG1");
		public static String SDAPI_SDItemMsG2 = GetValue("SD","SDAPI_SDItemMsG2");
		public static String SDAPI_SDItemMsG3 = GetValue("SD","SDAPI_SDItemMsG3");

		public static String SDAPI_SDItemMsG4 = GetValue("SD","SDAPI_SDItemMsG4");
		public static String SDAPI_SDItemMsG5 = GetValue("SD","SDAPI_SDItemMsG5");
		public static String SDAPI_SDItemMsG6 = GetValue("SD","SDAPI_SDItemMsG6");
		public static String SDAPI_SDItemMsG7 = GetValue("SD","SDAPI_SDItemMsG7");
		public static String SDAPI_SDItemMsG8 = GetValue("SD","SDAPI_SDItemMsG8");
		public static String SDAPI_SDItemMsG9 = GetValue("SD","SDAPI_SDItemMsG9");

		public static String SDAPI_SDItemMsG10 = GetValue("SD","SDAPI_SDItemMsG10");
		public static String SDAPI_SDItemMsG11 = GetValue("SD","SDAPI_SDItemMsG11");
		public static String SDAPI_SDItemMsG12 = GetValue("SD","SDAPI_SDItemMsG12");
		#endregion
        
		#region JW
		public static String JW2API_BrowseServerAddress = GetValue("JW","JW2API_BrowseServerAddress");
		public static String JW2API_PlayerAccount=GetValue("JW","JW2API_PlayerAccount");
		public static String JW2API_RoleInfomation=GetValue("JW","JW2API_RoleInfomation");
//		public static String JW2API_PlayerNicker=GetValue("JW","JW2API_PlayerNicker");
		public static string JW2API_NoPlayerInformation=GetValue("JW","JW2API_NoPlayerInformation");
		public static String JW2API_QueryPlayerServerIP=GetValue("JW","JW2API_QueryPlayerServerIP");
		public static String JW2API_InputContentNotCorrent= GetValue("JW","JW2API_InputContentNotCorrent");
		public static String JW2API_PointAndVirtualCurrency= GetValue("JW","JW2API_PointAndVirtualCurrency");
		public static String JW2API_NoPointAndVirtualCurrency= GetValue("JW","JW2API_NoPointAndVirtualCurrency");
		public static String JW2API_QueryPointAndVirtualCurrency= GetValue("JW","JW2API_QueryPointAndVirtualCurrency");
		public static String JW2API_ModiPlayer= GetValue("JW","JW2API_ModiPlayer");
		public static String JW2API_Level= GetValue("JW","JW2API_Level");
		public static String JW2API_Success= GetValue("JW","JW2API_Success");
		public static String JW2API_Failure= GetValue("JW","JW2API_Failure");
		public static String JW2API_UserLevelModi= GetValue("JW","JW2API_UserLevelModi");
		public static String JW2API_Experience= GetValue("JW","JW2API_Experience");
		public static String JW2API_UserExperienceModi= GetValue("JW","JW2API_UserExperienceModi");
		public static String JW2API_FailureConformAccount= GetValue("JW","JW2API_FailureConformAccount");
		public static String JW2API_Money= GetValue("JW","JW2API_Money");
		public static String JW2API_UserMoneyModi= GetValue("JW","JW2API_UserMoneyModi");
		public static String JW2API_MarriageCertificate= GetValue("JW","JW2API_MarriageCertificate");
        public static string JW2API_NoPlayerMarriageCertificate=GetValue("JW","JW2API_NoPlayerMarriageCertificate");
		public static String JW2API_QueryPlayerMarriageCertificate= GetValue("JW","JW2API_QueryPlayerMarriageCertificate");
		public static String JW2API_CouplesPartyCard= GetValue("JW","JW2API_CouplesPartyCard");
		public static String JW2API_NoPlayerCouplesPartyCard= GetValue("JW","JW2API_NoPlayerCouplesPartyCard");
		public static String JW2API_QueryPlayerCouplesPartyCard= GetValue("JW","JW2API_QueryPlayerCouplesPartyCard");
		public static String JW2API_ModiServerAddress= GetValue("JW","JW2API_ModiServerAddress");
        public static String JW2API_Type= GetValue("JW","JW2API_Type");
		public static String JW2API_GMAccount= GetValue("JW","JW2API_GMAccount");
		public static String JW2API_StatusSuccess= GetValue("JW","JW2API_StatusSuccess");
		public static String JW2API_GMStatusModi= GetValue("JW","JW2API_GMStatusModi");
		public static String JW2API_StatusModiFailure= GetValue("JW","JW2API_StatusModiFailure");
		public static String JW2API_BrowseItemCardNumber= GetValue("JW","JW2API_BrowseItemCardNumber");
		public static String JW2API_PrizeInformation= GetValue("JW","JW2API_PrizeInformation");
		public static String JW2API_ItemCardPrizeInformation= GetValue("JW","JW2API_ItemCardPrizeInformation");
		public static String JW2API_CardNumber= GetValue("JW","JW2API_CardNumber");
		public static String JW2API_GSListQuery= GetValue("JW","JW2API_GSListQuery");
		public static String JW2API_NoServerGSList= GetValue("JW","JW2API_NoServerGSList");
		public static String JW2API_BrowseModiServer= GetValue("JW","JW2API_BrowseModiServer");
		public static String JW2API_ExperienceMoneyComplete= GetValue("JW","JW2API_ExperienceMoneyComplete");
		public static String JW2API_ExperienceMoneyFailure= GetValue("JW","JW2API_ExperienceMoneyFailure");
		public static String JW2API_ServerDoubleModiError= GetValue("JW","JW2API_ServerDoubleModiError");
		public static String JW2API_ServerIP= GetValue("JW","JW2API_ServerIP");
		public static String JW2API_PlayerSN= GetValue("JW","JW2API_PlayerSN");
		public static String JW2API_LivingnessInfo= GetValue("JW","JW2API_LivingnessInfo");
		public static String JW2API_NoLivingnessInfo= GetValue("JW","JW2API_NoLivingnessInfo");
		public static String JW2API_QueryPlayerLivingnessInfo= GetValue("JW","JW2API_QueryPlayerLivingnessInfo");

		public static String JW2API_BeginTime= GetValue("JW","JW2API_BeginTime");
        public static String JW2API_EndTime= GetValue("JW","JW2API_EndTime");
		public static String JW2API_Story= GetValue("JW","JW2API_Story");
		public static String JW2API_NoPlayerStory= GetValue("JW","JW2API_NoPlayerStory");
		public static String JW2API_QueryStory= GetValue("JW","JW2API_QueryStory");
		public static String JW2API_BodyItem= GetValue("JW","JW2API_BodyItem");
		public static String JW2API_NoPlayerBodyItem= GetValue("JW","JW2API_NoPlayerBodyItem");
		public static String JW2API_QueryPlayerBodyItem= GetValue("JW","JW2API_QueryPlayerBodyItem");
		public static String JW2API_RoomItemList= GetValue("JW","JW2API_RoomItemList");
		public static String JW2API_NoPlayerRoomItemList= GetValue("JW","JW2API_NoPlayerRoomItemList");
		public static String JW2API_QueryPlayerRoomItemList= GetValue("JW","JW2API_QueryPlayerRoomItemList");
		public static String JW2API_ConsumeItem= GetValue("JW","JW2API_ConsumeItem");
		public static String JW2API_Trumpet= GetValue("JW","JW2API_Trumpet");
		public static String JW2API_NoPlayerTrumpet= GetValue("JW","JW2API_NoPlayerTrumpet");
		public static String JW2API_QueryTrumpet= GetValue("JW","JW2API_QueryTrumpet");
		public static String JW2API_ItemInfo= GetValue("JW","JW2API_ItemInfo");
		public static String JW2API_NoPlayerItemInfo= GetValue("JW","JW2API_NoPlayerItemInfo");
		public static String JW2API_QueryItemInfo= GetValue("JW","JW2API_QueryItemInfo");
		public static String JW2API_DelPlayer= GetValue("JW","JW2API_DelPlayer");
		public static String JW2API_Item= GetValue("JW","JW2API_Item");
		public static String JW2API_AddItem= GetValue("JW","JW2API_AddItem");
		public static String JW2API_QueryAddItem= GetValue("JW","JW2API_QueryAddItem");
		public static String JW2API_AddItemBatch= GetValue("JW","JW2API_AddItemBatch");
		public static String JW2API_QueryAddItemBatch= GetValue("JW","JW2API_QueryAddItemBatch");
		public static String JW2API_BrowseServerItem= GetValue("JW","JW2API_BrowseServerItem");
		public static String JW2API_NoItemInfo= GetValue("JW","JW2API_NoItemInfo");
		public static String JW2API_ItemQuery= GetValue("JW","JW2API_ItemQuery");
		public static String JW2API_PetInfo= GetValue("JW","JW2API_PetInfo");
		public static String JW2API_QueryPetInfo= GetValue("JW","JW2API_QueryPetInfo");
		public static String JW2API_ModiPetName= GetValue("JW","JW2API_ModiPetName");
		public static String JW2API_QueryModiPetName= GetValue("JW","JW2API_QueryModiPetName");
		public static String JW2API_OldPetName= GetValue("JW","JW2API_OldPetName");
		public static String JW2API_PetName= GetValue("JW","JW2API_PetName");
		public static String JW2API_ErrorPetName= GetValue("JW","JW2API_ErrorPetName");
		public static String JW2API_MaterialList= GetValue("JW","JW2API_MaterialList");
		public static String JW2API_NoPlayerMaterialList= GetValue("JW","JW2API_NoPlayerMaterialList");
		public static String JW2API_QueryPlayerMaterialList= GetValue("JW","JW2API_QueryPlayerMaterialList");
		public static String JW2API_MaterialHistory= GetValue("JW","JW2API_MaterialHistory");
		public static String JW2API_NoPlayerMaterialHistory= GetValue("JW","JW2API_NoPlayerMaterialHistory");
		public static String JW2API_QueryMaterialHistory= GetValue("JW","JW2API_QueryMaterialHistory");
		public static String JW2API_GETPIC= GetValue("JW","JW2API_GETPIC");
		public static String JW2API_NoGETPIC= GetValue("JW","JW2API_NoGETPIC");
		public static String JW2API_QueryGETPIC= GetValue("JW","JW2API_QueryGETPIC");
		public static String JW2API_CHKPICPlayer= GetValue("JW","JW2API_CHKPICPlayer");
		public static String JW2API_CHKPIC= GetValue("JW","JW2API_CHKPIC");
		public static String JW2API_QueryCHKPIC= GetValue("JW","JW2API_QueryCHKPIC");
		public static String JW2API_LogInfo= GetValue("JW","JW2API_LogInfo");
		public static String JW2API_QueryLogInfo= GetValue("JW","JW2API_QueryLogInfo");
		public static String JW2API_NoLogInfo= GetValue("JW","JW2API_NoLogInfo");
		public static String JW2API_ShoppingPresent= GetValue("JW","JW2API_ShoppingPresent");
		public static String JW2API_NoShppingPresent= GetValue("JW","JW2API_NoShppingPresent");
		public static String JW2API_QueryShoppingPresent= GetValue("JW","JW2API_QueryShoppingPresent");
		public static String JW2API_ConsumerLog= GetValue("JW","JW2API_ConsumerLog");
		public static String JW2API_NoConsumerLog= GetValue("JW","JW2API_NoConsumerLog");
		public static String JW2API_QueryConsumerLog= GetValue("JW","JW2API_QueryConsumerLog");
	
		public static String JW2API_PurchaseLog= GetValue("JW","JW2API_PurchaseLog");
		public static String JW2API_NoPurchaseLog= GetValue("JW","JW2API_NoPurchaseLog");
		
		public static String JW2API_QueryPurchaseLog= GetValue("JW","JW2API_QueryPurchaseLog");
		public static String JW2API_CenterAvAtarItemBag= GetValue("JW","JW2API_CenterAvAtarItemBag");
		public static String JW2API_QueryCenterAvAtarItemBag= GetValue("JW","JW2API_QueryCenterAvAtarItemBag");
		public static String JW2API_CenterAvAtarItemEquip= GetValue("JW","JW2API_CenterAvAtarItemEquip");
		public static String JW2API_NoCenterAvAtarItemEquip= GetValue("JW","JW2API_NoCenterAvAtarItemEquip");
		public static String JW2API_NoCenterAvAtarItemBag= GetValue("JW","JW2API_NoCenterAvAtarItemBag");
		public static String JW2API_QueryCenterAvAtarItemEquip= GetValue("JW","JW2API_QueryCenterAvAtarItemEquip");
		public static String JW2API_JB= GetValue("JW","JW2API_JB");
		public static String JW2API_NoJB= GetValue("JW","JW2API_NoJB");
		public static String JW2API_QueryJB= GetValue("JW","JW2API_QueryJB");
		public static String JW2API_CenterKick= GetValue("JW","JW2API_CenterKick");
		public static String JW2API_NoCenterKick= GetValue("JW","JW2API_NoCenterKick");
		public static String JW2API_QueryCenterKick= GetValue("JW","JW2API_QueryCenterKick");
		public static String JW2API_MissionInfoLog= GetValue("JW","JW2API_MissionInfoLog");
		public static String JW2API_NoMissionInfoLog= GetValue("JW","JW2API_NoMissionInfoLog");
		public static String JW2API_QueryMissionInfoLog= GetValue("JW","JW2API_QueryMissionInfoLog");
		public static String JW2API_RepeatPurchaseItem= GetValue("JW","JW2API_RepeatPurchaseItem");
		public static String JW2API_ReFund= GetValue("JW","JW2API_ReFund");
		public static String JW2API_NoRepeatPurchaseItem= GetValue("JW","JW2API_NoRepeatPurchaseItem");
		public static String JW2API_QueryRepeatPurchaseItem= GetValue("JW","JW2API_QueryRepeatPurchaseItem");
		public static String JW2API_BANISHPLAYER= GetValue("JW","JW2API_BANISHPLAYER");
		public static String JW2API_QueryBANISHPLAYER= GetValue("JW","JW2API_QueryBANISHPLAYER");
		public static String JW2API_NoBANISHPLAYER= GetValue("JW","JW2API_NoBANISHPLAYER");
		public static String JW2API_ACCOUNTOPEN= GetValue("JW","JW2API_ACCOUNTOPEN");
		public static String JW2API_QueryACCOUNTOPEN= GetValue("JW","JW2API_QueryACCOUNTOPEN");
		public static String JW2API_NoACCOUNTOPEN= GetValue("JW","JW2API_NoACCOUNTOPEN");
		public static String JW2API_ACCOUNTCLOSE= GetValue("JW","JW2API_ACCOUNTCLOSE");
		public static String JW2API_QueryACCOUNTCLOSE= GetValue("JW","JW2API_QueryACCOUNTCLOSE");
		public static String JW2API_NoACCOUNTCLOSE= GetValue("JW","JW2API_NoACCOUNTCLOSE");
		public static String JW2API_BANISHMENT= GetValue("JW","JW2API_BANISHMENT");
		public static String JW2API_NoBANISHMENT= GetValue("JW","JW2API_NoBANISHMENT");
		public static String JW2API_QueryBANISHMENT= GetValue("JW","JW2API_QueryBANISHMENT");
		public static String JW2API_LOGINOUT= GetValue("JW","JW2API_LOGINOUT");
		public static String JW2API_NoLOGINOUT= GetValue("JW","JW2API_NoLOGINOUT");
		public static String JW2API_QueryLOGINOUT= GetValue("JW","JW2API_QueryLOGINOUT");
		public static String JW2API_BOARDTASKUPDATE= GetValue("JW","JW2API_BOARDTASKUPDATE");
		public static String JW2API_QueryBOARDTASKUPDATE= GetValue("JW","JW2API_QueryBOARDTASKUPDATE");
		public static String JW2API_NoBOARDTASK= GetValue("JW","JW2API_NoBOARDTASK");
		public static String JW2API_QueryBOARDTASK= GetValue("JW","JW2API_QueryBOARDTASK");
		public static String JW2API_BOARDTASKINSERT= GetValue("JW","JW2API_BOARDTASKINSERT");
		public static String JW2API_QueryBOARDTASKINSERT= GetValue("JW","JW2API_QueryBOARDTASKINSERT");
		public static String JW2API_WEDDINGINFO= GetValue("JW","JW2API_WEDDINGINFO");
		public static String JW2API_NoWEDDINGINFO= GetValue("JW","JW2API_NoWEDDINGINFO");
		public static String JW2API_QueryWEDDINGINFO= GetValue("JW","JW2API_QueryWEDDINGINFO");
		public static String JW2API_WEDDINGLOG= GetValue("JW","JW2API_WEDDINGLOG");
		public static String JW2API_NoWEDDINGLOG= GetValue("JW","JW2API_NoWEDDINGLOG");
		public static String JW2API_QueryWEDDINGLOG= GetValue("JW","JW2API_QueryWEDDINGLOG");
		public static String JW2API_COUPLEINFO= GetValue("JW","JW2API_COUPLEINFO");
		public static String JW2API_NoCOUPLEINFO= GetValue("JW","JW2API_NoCOUPLEINFO");
		public static String JW2API_QueryCOUPLEINFO= GetValue("JW","JW2API_QueryCOUPLEINFO");
		public static String JW2API_COUPLELOG= GetValue("JW","JW2API_COUPLELOG");
		public static String JW2API_QueryCOUPLELOG= GetValue("JW","JW2API_QueryCOUPLELOG");
		public static String JW2API_NoQueryCOUPLELOG= GetValue("JW","JW2API_NoQueryCOUPLELOG");
		public static String JW2API_ModiUser= GetValue("JW","JW2API_ModiUser");
		public static String JW2API_MODIFYPWD= GetValue("JW","JW2API_MODIFYPWD");
		public static String JW2API_NoMODIFYPWD= GetValue("JW","JW2API_NoMODIFYPWD");
		public static String JW2API_QueryMODIFYPWD= GetValue("JW","JW2API_QueryMODIFYPWD");
		public static String JW2API_NoExistMODIFYPWD= GetValue("JW","JW2API_NoExistMODIFYPWD");
		public static String JW2API_RECALLUser= GetValue("JW","JW2API_RECALLUser");
		public static String JW2API_ModiPassServerAddress= GetValue("JW","JW2API_ModiPassServerAddress");
		public static String JW2API_RECALLPWD= GetValue("JW","JW2API_RECALLPWD");
		public static String JW2API_NoRECALLPWD= GetValue("JW","JW2API_NoRECALLPWD");
		public static String JW2API_QueryRECALLPWD= GetValue("JW","JW2API_QueryRECALLPWD");
		public static String JW2API_RecoverServerAddress= GetValue("JW","JW2API_RecoverServerAddress");
		public static String JW2API_NoExistRECALLPWD= GetValue("JW","JW2API_NoExistRECALLPWD");
		public static String JW2API_NoSearchPassWord= GetValue("JW","JW2API_NoSearchPassWord");
		public static String JW2API_QuerySearchPassWord= GetValue("JW","JW2API_QuerySearchPassWord");
		public static String JW2API_SearchPassWord= GetValue("JW","JW2API_SearchPassWord");
 
		public static String JW2API_SuccessPleaseWait= GetValue("JW","JW2API_SuccessPleaseWait");
        public static String JW2API_DatebaseConnectError= GetValue("JW","JW2API_DatebaseConnectError");

		#endregion

	}
}
