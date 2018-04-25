using System;
using Common.Logic;
namespace Common.NotesDataInfo
{
	/// <summary>
	/// 通用数据结构体
	/// </summary>
	public struct GlobalStruct
	{
		/// <summary>
		/// 字段名称
		/// </summary>
		public object oFieldsName;
		/// <summary>
		/// 字段类型
		/// </summary>
		public object oFiledsTypes;
		/// <summary>
		/// 字段变量
		/// </summary>
		public object oFieldValues;
	}

	/// <summary>
	/// DataStruct 的摘要说明。
	/// </summary>
	public class DataStruct
	{
		public DataStruct()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public static TagName getTagName(string strFieldName)
		{
			TagName mTagName = TagName.ERROR_Msg;
			switch (strFieldName)
			{
					#region 领土ONLINE
				case"LORD_NoteTitle":
					mTagName = TagName.LORD_NoteTitle;
					break;
				case"LORD_userNick":
					mTagName = TagName.LORD_userNick;
					break;
				case"LORD_NoteContent":
					mTagName = TagName.LORD_NoteContent;
					break;
				case"LORD_STime":
					mTagName = TagName.LORD_STime;
					break;
				case"LORD_ServerName":
					mTagName = TagName.LORD_ServerName;
					break;
				case"LORD_ServerIP":
					mTagName = TagName.LORD_ServerIP;
					break;
				case"LORD_Content":
					mTagName = TagName.LORD_Content;
					break;
				case"LORD_ResourceID":
					mTagName = TagName.LORD_ResourceID;
					break;
				case"LORD_ResourceName":
					mTagName = TagName.LORD_ResourceName;
					break;
				case"LORD_BuildID":
					mTagName = TagName.LORD_BuildID;
					break;
				case"LORD_Cur":
					mTagName = TagName.LORD_Cur;
					break;
				case"LORD_Work":
					mTagName = TagName.LORD_Work;
					break;
				case"LORD_Work_STime":
					mTagName = TagName.LORD_Work_STime;
					break;
				case"LORD_Work_ETime":
					mTagName = TagName.LORD_Work_ETime;
					break;
				case"LORD_IsIdentified":
					mTagName = TagName.LORD_IsIdentified;
					break;
				case"LORD_TypeID":
					mTagName = TagName.LORD_TypeID;
					break;
				case"LORD_WeaponLevels":
					mTagName = TagName.LORD_WeaponLevels;
					break;
				case"LORD_MerchantLoad":
					mTagName = TagName.LORD_MerchantLoad;
					break;
				case"LORD_Name":
					mTagName = TagName.LORD_Name;
					break;
				case"LORD_Account":
					mTagName = TagName.LORD_Account;
					break;
				case"LORD_Type":
					mTagName = TagName.LORD_Type;
					break;
				case"LORD_Race":
					mTagName = TagName.LORD_Race;
					break;
				case"LORD_AllianceName":
					mTagName = TagName.LORD_AllianceName;
					break;
				case"LORD_AllianceJob":
					mTagName = TagName.LORD_AllianceJob;
					break;
				case"LORD_Population":
					mTagName = TagName.LORD_Population;
					break;
				case"LORD_Gold":
					mTagName = TagName.LORD_Gold;
					break;
				case"LORD_Diamond":
					mTagName = TagName.LORD_Diamond;
					break;
				case"LORD_OnwerName":
					mTagName = TagName.LORD_OnwerName;
					break;
				case"LORD_RealmCount":
					mTagName = TagName.LORD_RealmCount;
					break;
				case"LORD_PunishedLevelInNewbieProtectPolicy":
					mTagName = TagName.LORD_PunishedLevelInNewbieProtectPolicy;
					break;
				case"LORD_ImmeBuilders":
					mTagName = TagName.LORD_ImmeBuilders;
					break;
				case"LORD_BuildTaskQueueCapacity":
					mTagName = TagName.LORD_BuildTaskQueueCapacity;
					break;
				case"LORD_AutoRepairTools":
					mTagName = TagName.LORD_AutoRepairTools;
					break;
				case"LORD_PlusAccountFlags1":
					mTagName = TagName.LORD_PlusAccountFlags1;
					break;
				case"LORD_PlusAccountFlags2":
					mTagName = TagName.LORD_PlusAccountFlags2;
					break;
				case"LORD_PlusAccountStartTime":
					mTagName = TagName.LORD_PlusAccountStartTime;
					break;
				case"LORD_PlusAccountEndTime":
					mTagName = TagName.LORD_PlusAccountEndTime;
					break;
				case"LORD_Trusteeship":
					mTagName = TagName.LORD_Trusteeship;
					break;
				case"LORD_Ability1":
					mTagName = TagName.LORD_Ability1;
					break;
				case"LORD_Ability2":
					mTagName = TagName.LORD_Ability2;
					break;
				case"LORD_TotalOnlineTime":
					mTagName = TagName.LORD_TotalOnlineTime;
					break;
				case"LORD_Privilege":
					mTagName = TagName.LORD_Privilege;
					break;
				case"LORD_RegisterTime":
					mTagName = TagName.LORD_RegisterTime;
					break;
				case"LORD_LastLoginTime":
					mTagName = TagName.LORD_LastLoginTime;
					break;
				case"LORD_LastLogoutTime":
					mTagName = TagName.LORD_LastLogoutTime;
					break;
					
				case"LORD_ID":
					mTagName = TagName.LORD_ID;
					break;
				case"Lord_uid":
					mTagName = TagName.LORD_Uid;
					break;
				case"RealmLocation":
					mTagName = TagName.LORD_RealmLocation;
					break;
				case"CurrentLocation":
					mTagName = TagName.LORD_CurrentLocation;
					break;
				case"LORD_Level":
					mTagName = TagName.LORD_Level;
					break;
				case"LORD_Exp":
					mTagName = TagName.LORD_Exp;
					break;
				case"LORD_SP":
					mTagName = TagName.LORD_SP;
					break;
				case"LORD_HP":
					mTagName = TagName.LORD_HP;
					break;
				case"LORD_Attack":
					mTagName = TagName.LORD_Attack;
					break;
				case"LORD_Defense":
					mTagName = TagName.LORD_Defense;
					break;
				case"LORD_Speed":
					mTagName = TagName.LORD_Speed;
					break;
				case"LORD_skillname0":
					mTagName = TagName.LORD_skillname0;
					break;
				case"LORD_Skill0Level":
					mTagName = TagName.LORD_Skill0Level;
					break;
				case"LORD_skillname1":
					mTagName = TagName.LORD_skillname1;
					break;
				case"LORD_Skill1Level":
					mTagName = TagName.LORD_Skill1Level;
					break;
				case"LORD_skillname2":
					mTagName = TagName.LORD_skillname2;
					break;
				case"LORD_Skill2Level":
					mTagName = TagName.LORD_Skill2Level;
					break;
				case"LORD_skillname3":
					mTagName = TagName.LORD_skillname3;
					break;
				case"LORD_Skill3Level":
					mTagName = TagName.LORD_Skill3Level;
					break;
				case"LORD_itemname0":
					mTagName = TagName.LORD_itemname0;
					break;
				case"LORD_Item0Usable":
					mTagName = TagName.LORD_Item0Usable;
					break;
				case"LORD_itemname1":
					mTagName = TagName.LORD_itemname1;
					break;
				case"LORD_Item1Usable":
					mTagName = TagName.LORD_Item1Usable;
					break;
				case"LORD_itemname2":
					mTagName = TagName.LORD_itemname2;
					break;
				case"LORD_Item2Usable":
					mTagName = TagName.LORD_Item2Usable;
					break;
				case"LORD_itemname3":
					mTagName = TagName.LORD_itemname3;
					break;
				case"LORD_Item3Usable":
					mTagName = TagName.LORD_Item3Usable;
					break;
				case"LORD_itemname4":
					mTagName = TagName.LORD_itemname4;
					break;
				case"LORD_Item4Usable":
					mTagName = TagName.LORD_Item4Usable;
					break;
				case"LORD_itemname5":
					mTagName = TagName.LORD_itemname5;
					break;
				case"LORD_Item5Usable":
					mTagName = TagName.LORD_Item5Usable;
					break;

				case"LORD_HealthRecovery":
					mTagName = TagName.LORD_HealthRecovery;
					break;
				case"LORD_LastCheckRecoveryTime":
					mTagName = TagName.LORD_LastCheckRecoveryTime;
					break;
				case"LORD_ReliveProgressStartTime":
					mTagName = TagName.LORD_ReliveProgressStartTime;
					break;
				case"LORD_ReliveProgressExpireTime":
					mTagName = TagName.LORD_ReliveProgressExpireTime;
					break;
				case"LORD_IsLevelLocked":
					mTagName = TagName.LORD_IsLevelLocked;
					break;
				
				case"LORD_Location":
					mTagName = TagName.LORD_Location;
					break;
				case"LORD_Orbit":
					mTagName = TagName.LORD_Orbit;
					break;
				case"LORD_Scout":
					mTagName = TagName.LORD_Scout;
					break;
				case"LORD_Wood":
					mTagName = TagName.LORD_Wood;
					break;
				case"LORD_Stone":
					mTagName = TagName.LORD_Stone;
					break;
				case"LORD_Iron":
					mTagName = TagName.LORD_Iron;
					break;
				case"LORD_Food":
					mTagName = TagName.LORD_Food;
					break;
				case"LORD_InnerRealmBuilderCount":
					mTagName = TagName.LORD_InnerRealmBuilderCount;
					break;
				case"LORD_OuterRealmBuilderCount":
					mTagName = TagName.LORD_OuterRealmBuilderCount;
					break;
				case"LORD_UsedMerchantCount":
					mTagName = TagName.LORD_UsedMerchantCount;
					break;
				case"LORD_MerchantSpeed":
					mTagName = TagName.LORD_MerchantSpeed;
					break;
				case"LORD_ArmorLevels":
					mTagName = TagName.LORD_ArmorLevels;
					break;
				case"LORD_RecruitTime":
					mTagName = TagName.LORD_RecruitTime;
					break;
				case"LORD_PopulationVolatile":
					mTagName = TagName.LORD_PopulationVolatile;
					break;
				case"LORD_LastCheckStarvationTime":
					mTagName = TagName.LORD_LastCheckStarvationTime;
					break;
				case"LORD_DailyAttackedCount":
					mTagName = TagName.LORD_DailyAttackedCount;
					break;
				case"LORD_Nofight":
					mTagName = TagName.LORD_Nofight;
					break;
				case"LORD_DefenseRealmLocation":
					mTagName = TagName.LORD_DefenseRealmLocation;
					break;
				case"LORD_fight":
					mTagName = TagName.LORD_fight;
					break;
				case"LORD_Time":
					mTagName = TagName.LORD_Time;
					break;
				case"LORD_isRead":
					mTagName = TagName.LORD_isRead;
					break;
				case"LORD_isDeleted":
					mTagName = TagName.LORD_isDeleted;
					break;
				case"LORD_AttackLord":
					mTagName = TagName.LORD_AttackLord;
					break;
				case"LORD_OffenseRealmLocation":
					mTagName = TagName.LORD_OffenseRealmLocation;
					break;
				case"LORD_DefenseLord":
					mTagName = TagName.LORD_DefenseLord;
					break;
				case"LORD_object_Name":
					mTagName = TagName.LORD_object_Name;
					break;
				case"LORD_Attack_Name":
					mTagName = TagName.LORD_Attack_Name;
					break;
				case"LORD_FromPlayer":
					mTagName = TagName.LORD_FromPlayer;
					break;
				case"LORD_FromRealmLord":
					mTagName = TagName.LORD_FromRealmLord;
					break;
				case"LORD_FromRealm":
					mTagName = TagName.LORD_FromRealm;
					break;
				case"LORD_ToPlayer":
					mTagName = TagName.LORD_ToPlayer;
					break;
				case"LORD_ToRealmLord":
					mTagName = TagName.LORD_ToRealmLord;
					break;
				case"LORD_ToRealm":
					mTagName = TagName.LORD_ToRealm;
					break;
				case"LORD_Reference":
					mTagName = TagName.LORD_Reference;
					break;	
				case"LORD_AutReamlLord":
					mTagName = TagName.LORD_AutReamlLord;
					break;	
				case"LORD_AutReaml":
					mTagName = TagName.LORD_AutReaml;
					break;	
				case"LORD_object_Lord":
					mTagName = TagName.LORD_object_Lord;
					break;	
				case"LORD_SpiedInfos":
					mTagName = TagName.LORD_SpiedInfos;
					break;	
				case"LORD_StarvedTroops":
					mTagName = TagName.LORD_StarvedTroops;
					break;	
				case"LORD_Entity":
					mTagName = TagName.LORD_Entity;
					break;
				case"LORD_SearchLocation":
					mTagName = TagName.LORD_SearchLocation;
					break;
				case"LORD_Awards":
					mTagName = TagName.LORD_Awards;
					break;
				case"LORD_Result":
					mTagName = TagName.LORD_Result;
					break;
				case"LORD_object_Location":
					mTagName = TagName.LORD_object_Location;
					break;
				case"LORD_FromTime":
					mTagName = TagName.LORD_FromTime;
					break;
				case"LORD_ToTime":
					mTagName = TagName.LORD_ToTime;
					break;
				case"LORD_IsBack":
					mTagName = TagName.LORD_IsBack;
					break;
				case"LORD_HasBeenInReinforcee":
					mTagName = TagName.LORD_HasBeenInReinforcee;
					break;
				case"LORD_Attack_ID":
					mTagName = TagName.LORD_Attack_ID;
					break;
				case"LORD_object_ID":
					mTagName = TagName.LORD_object_ID;
					break;
				case"LORD_PlayID":
					mTagName = TagName.LORD_PlayID;
					break;
				case"LORD_HeroID":
					mTagName = TagName.LORD_HeroID;
					break;
				case"LORD_HeroName":
					mTagName = TagName.LORD_HeroName;
					break;
				case"LORD_UpTime":
					mTagName = TagName.LORD_UpTime;
					break;
				case"LORD_LevelBefore":
					mTagName = TagName.LORD_LevelBefore;
					break;
				case"LORD_LevelAfter":
					mTagName = TagName.LORD_LevelAfter;
					break;
				case"LORD_Item_ID":
					mTagName = TagName.LORD_Item_ID;
					break;
				case"LORD_Item_Name":
					mTagName = TagName.LORD_Item_Name;
					break;
				case"LORD_Item_Num":
					mTagName = TagName.LORD_Item_Num;
					break;
				case"LORD_ItemIdentifiedState":
					mTagName = TagName.LORD_ItemIdentifiedState;
					break;
				case"LORD_ItemPowerLevel":
					mTagName = TagName.LORD_ItemPowerLevel;
					break;
				case"LORD_ItemUsable":
					mTagName = TagName.LORD_ItemUsable;
					break;
				case"LORD_Reason":
					mTagName = TagName.LORD_Reason;
					break;
				case"LORD_UsableBefore":
					mTagName = TagName.LORD_UsableBefore;
					break;
				case"LORD_UsableAfter":
					mTagName = TagName.LORD_UsableAfter;
					break;
				case"LORD_IsHeroItem":
					mTagName = TagName.LORD_IsHeroItem;
					break;
				case"LORD_FixBefore":
					mTagName = TagName.LORD_FixBefore;
					break;
				case"LORD_FixAfter":
					mTagName = TagName.LORD_FixAfter;
					break;
				case"LORD_StartTime":
					mTagName = TagName.LORD_StartTime;
					break;
				case"LORD_EndTime":
					mTagName = TagName.LORD_EndTime;
					break;
				case"LORD_SoldierType":
					mTagName = TagName.LORD_SoldierType;
					break;
				case"LORD_TroopName":
					mTagName = TagName.LORD_TroopName;
					break;
				case"LORD_SoldierCount":
					mTagName = TagName.LORD_SoldierCount;
					break;
				case"LORD_Lord_ID":
					mTagName = TagName.LORD_Lord_ID;
					break;
				case"LORD_BuildName":
					mTagName = TagName.LORD_BuildName;
					break;
				case"LORD_BuildingLocation":
					mTagName = TagName.LORD_BuildingLocation;
					break;
				case"LORD_Item00":
					mTagName = TagName.LORD_Item00;
					break;
				case"LORD_Item01":
					mTagName = TagName.LORD_Item01;
					break;
				case"LORD_Item02":
					mTagName = TagName.LORD_Item02;
					break;
				case"LORD_Item03":
					mTagName = TagName.LORD_Item03;
					break;
				case"LORD_Item04":
					mTagName = TagName.LORD_Item04;
					break;
				case"LORD_Item05":
					mTagName = TagName.LORD_Item05;
					break;
				case"LORD_Item06":
					mTagName = TagName.LORD_Item06;
					break;
				case"LORD_Item07":
					mTagName = TagName.LORD_Item07;
					break;
				case"LORD_Item08":
					mTagName = TagName.LORD_Item08;
					break;
				case"LORD_Item09":
					mTagName = TagName.LORD_Item09;
					break;
				case"LORD_Item10":
					mTagName = TagName.LORD_Item10;
					break;
				case"LORD_Item11":
					mTagName = TagName.LORD_Item11;
					break;
				case"LORD_Item12":
					mTagName = TagName.LORD_Item12;
					break;
				case"LORD_Item13":
					mTagName = TagName.LORD_Item13;
					break;
				case"LORD_Item14":
					mTagName = TagName.LORD_Item14;
					break;
				case"LORD_Item15":
					mTagName = TagName.LORD_Item15;
					break;
				case"LORD_Item16":
					mTagName = TagName.LORD_Item16;
					break;
				case"LORD_Item17":
					mTagName = TagName.LORD_Item17;
					break;
				case"LORD_Item18":
					mTagName = TagName.LORD_Item18;
					break;
				case"LORD_Item19":
					mTagName = TagName.LORD_Item19;
					break;
				case"LORD_Item20":
					mTagName = TagName.LORD_Item20;
					break;
				case"LORD_Item21":
					mTagName = TagName.LORD_Item21;
					break;
				case"LORD_Item22":
					mTagName = TagName.LORD_Item22;
					break;
				case"LORD_Item23":
					mTagName = TagName.LORD_Item23;
					break;
				case"LORD_Item24":
					mTagName = TagName.LORD_Item24;
					break;

				case"LORD_Item00PowerLevel":
					mTagName = TagName.LORD_Item00PowerLevel;
					break;
				case"LORD_Item01PowerLevel":
					mTagName = TagName.LORD_Item01PowerLevel;
					break;
				case"LORD_Item02PowerLevel":
					mTagName = TagName.LORD_Item02PowerLevel;
					break;
				case"LORD_Item03PowerLevel":
					mTagName = TagName.LORD_Item03PowerLevel;
					break;
				case"LORD_Item04PowerLevel":
					mTagName = TagName.LORD_Item04PowerLevel;
					break;
				case"LORD_Item05PowerLevel":
					mTagName = TagName.LORD_Item05PowerLevel;
					break;
				case"LORD_Item06PowerLevel":
					mTagName = TagName.LORD_Item06PowerLevel;
					break;
				case"LORD_Item07PowerLevel":
					mTagName = TagName.LORD_Item07PowerLevel;
					break;
				case"LORD_Item08PowerLevel":
					mTagName = TagName.LORD_Item08PowerLevel;
					break;
				case"LORD_Item09PowerLevel":
					mTagName = TagName.LORD_Item09PowerLevel;
					break;
				case"LORD_Item10PowerLevel":
					mTagName = TagName.LORD_Item10PowerLevel;
					break;
				case"LORD_Item11PowerLevel":
					mTagName = TagName.LORD_Item11PowerLevel;
					break;
				case"LORD_Item12PowerLevel":
					mTagName = TagName.LORD_Item12PowerLevel;
					break;
				case"LORD_Item13PowerLevel":
					mTagName = TagName.LORD_Item13PowerLevel;
					break;
				case"LORD_Item14PowerLevel":
					mTagName = TagName.LORD_Item14PowerLevel;
					break;
				case"LORD_Item15PowerLevel":
					mTagName = TagName.LORD_Item15PowerLevel;
					break;
				case"LORD_Item16PowerLevel":
					mTagName = TagName.LORD_Item16PowerLevel;
					break;
				case"LORD_Item17PowerLevel":
					mTagName = TagName.LORD_Item17PowerLevel;
					break;
				case"LORD_Item18PowerLevel":
					mTagName = TagName.LORD_Item18PowerLevel;
					break;
				case"LORD_Item19PowerLevel":
					mTagName = TagName.LORD_Item19PowerLevel;
					break;
				case"LORD_Item20PowerLevel":
					mTagName = TagName.LORD_Item20PowerLevel;
					break;
				case"LORD_Item21PowerLevel":
					mTagName = TagName.LORD_Item21PowerLevel;
					break;
				case"LORD_Item22PowerLevel":
					mTagName = TagName.LORD_Item22PowerLevel;
					break;
				case"LORD_Item23PowerLevel":
					mTagName = TagName.LORD_Item23PowerLevel;
					break;
				case"LORD_Item24PowerLevel":
					mTagName = TagName.LORD_Item24PowerLevel;
					break;
					#endregion
					#region SD高达
				case"SD_LostMoney":
					mTagName = TagName.SD_LostMoney;
					break;
				case"SD_LostCodeMoney":
					mTagName = TagName.SD_LostCodeMoney;
					break;
				case"SD_UseCodeMoney":
					mTagName = TagName.SD_UseCodeMoney;
					break;
				case"SD_LevelUpTime":
					mTagName = TagName.SD_LevelUpTime;
					break;
				case"SD_Star":
					mTagName = TagName.SD_Star;
					break;
				case"SD_TypeName":
					mTagName = TagName.SD_TypeName;
					break;
				case"SD_UnitLevelName":
					mTagName = TagName.SD_UnitLevelName;
					break;
				case"SD_RewardCount":
					mTagName = TagName.SD_RewardCount;
					break;
				case"SD_SkillName":
					mTagName = TagName.SD_SkillName;
					break;
				case"SD_UColor_1":
					mTagName = TagName.SD_UColor_1;
					break;
				case"SD_UColor_2":
					mTagName = TagName.SD_UColor_2;
					break;
				case"SD_UColor_3":
					mTagName = TagName.SD_UColor_3;
					break;
				case"SD_UColor_4":
					mTagName = TagName.SD_UColor_4;
					break;
				case"SD_UColor_5":
					mTagName = TagName.SD_UColor_5;
					break;
				case"SD_UColor_6":
					mTagName = TagName.SD_UColor_6;
					break;
				case"SD_UDecal_1":
					mTagName = TagName.SD_UDecal_1;
					break;
				case"SD_UDecal_2":
					mTagName = TagName.SD_UDecal_2;
					break;
				case"SD_UDecal_3":
					mTagName = TagName.SD_UDecal_3;
					break;
				case"SD_DelTime":
					mTagName = TagName.SD_DelTime;
					break;
				case"SD_HP":
					mTagName = TagName.SD_HP;
					break;
				case"SD_DashLevel":
					mTagName = TagName.SD_DashLevel;
					break;
				case"SD_FatalAttack":
					mTagName = TagName.SD_FatalAttack;
					break;
				case"SD_DefensivePower":
					mTagName = TagName.SD_DefensivePower;
					break;
				case"SD_MotivePower":
					mTagName = TagName.SD_MotivePower;
					break;
				case"SD_StrikingPower":
					mTagName = TagName.SD_StrikingPower;
					break;

				case"SD_QusetID":
					mTagName = TagName.SD_QusetID;
					break;
				case"SD_QusetName":
					mTagName = TagName.SD_QusetName;
					break;
				case"SD_ClearedDate":
					mTagName = TagName.SD_ClearedDate;
					break;
				case"SD_State":
					mTagName = TagName.SD_State;
					break;
				case"SD_UnitType":
					mTagName = TagName.SD_UnitType;
					break;
				case"SD_ShopType":
					mTagName = TagName.SD_ShopType;
					break;
				case"SD_LimitType":
					mTagName = TagName.SD_LimitType;
					break;
				case"SD_GetMoney":
					mTagName = TagName.SD_GetMoney;
					break;
				case"SD_DeleteResult":
					mTagName = TagName.SD_DeleteResult;
					break;
				case"SD_LastMoney":
					mTagName = TagName.SD_LastMoney;
					break;
				case"SD_UseMoney":
					mTagName = TagName.SD_UseMoney;
					break;
				case"SD_serverIP":
					mTagName = TagName.SD_ServerIP;
					break;
				case"SD_Type":
					mTagName = TagName.SD_Type;
					break;
				case"SD_Content":
					mTagName = TagName.SD_Content;
					break;
				case"SD_SendTime":
					mTagName = TagName.SD_SendTime;
					break;
				case"SD_ItemID":
					mTagName = TagName.SD_ItemID;
					break;
				case"SD_ItemID1":
					mTagName = TagName.SD_ItemID1;
					break;
				case"SD_ItemID2":
					mTagName = TagName.SD_ItemID2;
					break;
				case"SD_ItemID3":
					mTagName = TagName.SD_ItemID3;
					break;
				case"SD_TempPassWord":
					mTagName = TagName.SD_TempPassWord;
					break;
				case"SD_OperatorNickname":
					mTagName = TagName.SD_OperatorNickname;
					break;	
				case"SD_BatItemName":
					mTagName = TagName.SD_BatItemName;
					break;			
				case"SD_UnitName":
					mTagName = TagName.SD_UnitName;
					break;
				case"SD_UnitExp":
					mTagName = TagName.SD_UnitExp;
					break;
				case"SD_UnitLevelNumber":
					mTagName = TagName.SD_UnitLevelNumber;
					break;
				case"SD_UnitNickName":
					mTagName = TagName.SD_UnitNickName;
					break;
				case"SD_UseDate":
					mTagName = TagName.SD_UseDate;
					break;
				case"SD_StateSaleIntention":
					mTagName = TagName.SD_StateSaleIntention;
					break;
				case"SD_FromIdx":
					mTagName = TagName.SD_FromIdx;
					break;
				case"SD_ToIdx":
					mTagName = TagName.SD_ToIdx;
					break;
				case"SD_FromUser":
					mTagName = TagName.SD_FromUser;
					break;
				case"SD_Make":
					mTagName = TagName.SD_Make;
					break;
				case"SD_ToUser":
					mTagName = TagName.SD_ToUser;
					break;
				case"SD_DateEnd":
					mTagName = TagName.SD_DateEnd;
					break;
				case"SD_CustomLvMax":
					mTagName = TagName.SD_CustomLvMax;
					break;
				case"SD_CustomPoint":
					mTagName = TagName.SD_CustomPoint;
					break;
				case"SD_BuyType":
					mTagName = TagName.SD_BuyType;
					break;
				case"SD_SlotNumber":
					mTagName = TagName.SD_SlotNumber;
					break;
				case"SD_ItemName":
					mTagName = TagName.SD_ItemName;
					break;
				case"SD_ItemName1":
					mTagName = TagName.SD_ItemName1;
					break;
				case"SD_ItemName2":
					mTagName = TagName.SD_ItemName2;
					break;
				case"SD_ItemName3":
					mTagName = TagName.SD_ItemName3;
					break;
				case"SD_Number":
					mTagName = TagName.SD_Number;
					break;
				case"SD_Number1":
					mTagName = TagName.SD_Number1;
					break;
				case"SD_Number2":
					mTagName = TagName.SD_Number2;
					break;
				case"SD_Number3":
					mTagName = TagName.SD_Number3;
					break;
				case"SD_N10":
					mTagName = TagName.SD_N10;
					break;
				case"SD_N11":
					mTagName = TagName.SD_N11;
					break;
				case"SD_N12":
					mTagName = TagName.SD_N12;
					break;
				case"SD_RemainCount":
					mTagName = TagName.SD_RemainCount;
					break;
				case"SD_GetTime":
					mTagName = TagName.SD_GetTime;
					break;
				case"SD_passWd":
					mTagName = TagName.SD_passWd;
					break;
				case"SD_Limit":
					mTagName = TagName.SD_Limit;
					break;
				case"SD_StartTime":
					mTagName = TagName.SD_StartTime;
					break;
				case"SD_EndTime":
					mTagName = TagName.SD_EndTime;
					break;
				case"SD_ID":
					mTagName = TagName.SD_ID;
					break;
				case"SD_ServerName":
					mTagName = TagName.SD_ServerName;
					break;
				case"f_idx":
					mTagName = TagName.f_idx;
					break;
				case"SD_UserName":
					mTagName = TagName.SD_UserName;
					break;
				case"f_pilot":
					mTagName = TagName.f_pilot;
					break;
				case"f_gender":
					mTagName = TagName.f_gender;
					break;
				case"SD_ban":
					mTagName = TagName.SD_ban;
					break;
				case"f_level":
					mTagName = TagName.f_level;
					break;
				case"f_levelname":
					mTagName = TagName.f_levelname;
					break;
				case"f_Exp":
					mTagName = TagName.f_Exp;
					break;
				case"SD_NeedExp":
					mTagName = TagName.SD_NeedExp;
					break;
				case"SD_GC":
					mTagName = TagName.SD_GC;
					break;
				case"SD_GP":
					mTagName = TagName.SD_GP;
					break;
				case"f_shootCnt":
					mTagName = TagName.f_shootCnt;
					break;
				case"SD_KillNum":
					mTagName = TagName.SD_KillNum;
					break;
				case"f_fightCnt":
					mTagName = TagName.f_fightCnt;
					break;
				case"f_winCnt":
					mTagName = TagName.f_winCnt;
					break;
				case"f_loseCnt":
					mTagName = TagName.f_loseCnt;
					break;
				case"f_regDate":
					mTagName = TagName.f_regDate;
					break;
				case"f_loginDate":
					mTagName = TagName.f_loginDate;
					break;
				case"f_logoutDate":
					mTagName = TagName.f_logoutDate;
					break;
					#endregion
                    
					#region 劲舞团II

				case"jw2_PetAggID_Small":
					mTagName = TagName.jw2_PetAggID_Small;
					break;
				case"jw2_PetAggName_Small":
					mTagName = TagName.jw2_PetAggName_Small;
					break;
				case"jw2_OverTimes":
					mTagName = TagName.jw2_OverTimes;
					break;

				case"jw2_FirstFamilySN":
					mTagName = TagName.jw2_FirstFamilySN;
					break;
				case"jw2_SecondFamilySN":
					mTagName = TagName.jw2_SecondFamilySN;
					break;
				case"jw2_FirstFamilyName":
					mTagName = TagName.jw2_FirstFamilyName;
					break;
				case"jw2_SecondFamilyName":
					mTagName = TagName.jw2_SecondFamilyName;
					break;
				case"jw2_FirstFamilyMaster":
					mTagName = TagName.jw2_FirstFamilyMaster;
					break;
				case"jw2_SecondFamilyMaster":
					mTagName = TagName.jw2_SecondFamilyMaster;
					break;
				case"jw2_ApplyDate":
					mTagName = TagName.jw2_ApplyDate;
					break;
				case"jw2_ApplyState":
					mTagName = TagName.jw2_ApplyState;
					break;
				case"GardenLv":
					mTagName = TagName.jw2_GardenLv;
					break;
				case"AU_gradename":
					mTagName = TagName.AU_gradename;
					break;
				case"jw2_Wedding_Home":
					mTagName = TagName.jw2_Wedding_Home;
					break;
				case"jw2_pic_Name":
					mTagName = TagName.jw2_pic_Name;
					break;
				case"jw2_MaterialCode":
					mTagName = TagName.jw2_MaterialCode;
					break;
				case"jw2_serverno":
					mTagName = TagName.jw2_serverno;
					break;
				case"jw2_port":
					mTagName = TagName.jw2_port;
					break;
				case"jw2_servername":
					mTagName = TagName.JW2_ServerName;
					break;
				case"jw2_goodsprice":
				case"JW2_GOODSPRICE":
					mTagName = TagName.jw2_goodsprice;
					break;
				case"jw2_beforemoney":
				case"JW2_BEFOREMONEY":
					mTagName = TagName.jw2_beforemoney;
					break;
				case"jw2_aftermoney":
				case"JW2_AFTERMONEY":
					mTagName = TagName.jw2_aftermoney;
					break;
				case"jw2_buylimitday":
				case "JW2_BUYLIMITDAY":
					mTagName = TagName.JW2_BuyLimitDay;
					break;
				case"jw2_buymoneycost":
					mTagName = TagName.JW2_BuyMoneyCost;
					break;
				case"jw2_buyorgCost":
					mTagName = TagName.JW2_BuyOrgCost;
					break;
				case"jw2_family_money":
					mTagName = TagName.JW2_Family_Money;
					break;
				case"JW2_IP":
					mTagName = TagName.JW2_IP;
					break;
				case"jw2_center_endtime":
				case"JW2_CENTER_ENDTIME":
					mTagName = TagName.JW2_Center_EndTime;
					break;
				case"jw2_forever":
				case"JW2_FOREVER":
					mTagName = TagName.JW2_Forever;
					break;
				case"jw2_center_avatarid":
				case"JW2_CENTER_AVATARID":
					mTagName = TagName.JW2_Center_Avatarid;
					break;
				case"jw2_center_avatarname":
				case"JW2_CENTER_AVATARNAME":
					mTagName = TagName.JW2_Center_AvatarName;
					break;
				case"jw2_center_state":
				case"JW2_CENTER_STATE":
					mTagName = TagName.JW2_Center_State;
					break;
				case"JW2_LAST_OP_TIME":
					mTagName = TagName.JW2_Last_Op_Time;
					break;
				case"jw2_logdate":
				case"JW2_LOGDATE":
					mTagName = TagName.JW2_LOGDATE;
					break;
				case"jw2_zoneid":
				case"JW2_ZONEID":
					mTagName = TagName.JW2_ZoneID;
					break;
				case"jw2_vailedday":
				case"JW2_VAILEDDAY":
					mTagName = TagName.JW2_VailedDay;
					break;
				case"jw2_intro":
				case"JW2_INTRO":
					mTagName = TagName.JW2_IntRo;
					break;
				case"jw2_subgameid":
				case"JW2_SUBGAMEID":
					mTagName = TagName.JW2_SubGameID;
					break;
				case"jw2_beforecash":
					mTagName = TagName.JW2_BeforeCash;
					break;
				case"jw2_aftercash":
					mTagName = TagName.JW2_AfterCash;
					break;
				case"jw2_grade":
					mTagName = TagName.jw2_grade;
					break;
				case"JW2_TYPE":
				case"jw2_type":
					mTagName = TagName.JW2_Type;
					break;
				case"jw2_messengername":
					mTagName = TagName.JW2_MessengerName;
					break;
				case "jw2_snum":
					mTagName = TagName.JW2_sNum;
					break;
				case "jw2_senduser":
				case "JW2_SENDUSER":
					mTagName = TagName.JW2_SendUser;
					break;
				case "jw2_recuser":
				case "JW2_RECUSER":
					mTagName = TagName.JW2_RecUser;
					break;		
				case "jw2_account":
					mTagName = TagName.JW2_ACCOUNT;
					break;
				case "power":
					mTagName = TagName.JW2_POWER;
					break;
				case "jw2_exp":
					mTagName = TagName.JW2_Exp;
					break;
				case "jw2_money":
					mTagName = TagName.JW2_Money;
					break;
				case "jw2_cash":
					mTagName = TagName.JW2_Cash;
					break;
				case "goldmedal":
					mTagName = TagName.JW2_GoldMedal;
					break;
				case "silvermedal":
					mTagName = TagName.JW2_SilverMedal;
					break;
				case "coppermedal":
					mTagName = TagName.JW2_CopperMedal;
					break;
				case "jw2_level":
					mTagName = TagName.JW2_Level;
					break;
				case "region":
					mTagName = TagName.JW2_Region;
					break;
				case "qq":
					mTagName = TagName.JW2_QQ;
					break;
				case "para":
					mTagName = TagName.JW2_PARA;
					break;
				case "jw2_usernick":
				case "JW2_USERNICK":
					mTagName = TagName.JW2_UserNick;
					break;
				case "jw2_userid":
				case "JW2_USERID":
					mTagName = TagName.JW2_UserID;
					break;
				case "jw2_sex":
					mTagName = TagName.JW2_Sex;
					break;
				case "jw2_avataritem":		
				case "JW2_AVATARITEM":	
					mTagName = TagName.JW2_AvatarItem;
					break;
				case "jw2_avataritemname":				
					mTagName = TagName.JW2_AvatarItemName;
					break;
				case "jw2_expiredate":			
					mTagName = TagName.JW2_ExpireDate;
					break;
				case "jw2_useitem":
					mTagName = TagName.JW2_UseItem;
					break;
				case "jw2_remaincount":				
					mTagName = TagName.JW2_RemainCount;
					break;
				case "jw2_center_usersn":
				case "jw2_usersn":
				case "JW2_USERSN":
				case "JW2_LOG_USERSN":
				case "JW2_CENTER_USERSN":
					mTagName = TagName.JW2_UserSN;
					break;
				case "jw2_boardsn":
					mTagName = TagName.JW2_BOARDSN;
					break;
				case "jw2_bugletype":
					mTagName = TagName.JW2_BUGLETYPE;
					break;
				case "jw2_boardmessage":
					mTagName = TagName.JW2_BoardMessage;
					break;
				case "jw2_usetime":
					mTagName = TagName.JW2_UseTime;
					break;
				case "jw2_taskid":
					mTagName = TagName.JW2_TaskID;
					break;
				case "jw2_begintime":
					mTagName = TagName.JW2_BeginTime;
					break;
				case "jw2_endtime":
					mTagName = TagName.JW2_EndTime;
					break;
				case "jw2_interval":
					mTagName = TagName.JW2_Interval;
					break;
				case "jw2_status":
					mTagName = TagName.JW2_Status;
					break;
				case "jw2_serverip":
					mTagName = TagName.JW2_ServerIP;
					break;
				case "jw2_stage0":
					mTagName = TagName.JW2_Stage0;
					break;
				case "jw2_stage1":
					mTagName = TagName.JW2_Stage1;
					break;
				case "jw2_stage2":
					mTagName = TagName.JW2_Stage2;
					break;
				case "jw2_stage3":
					mTagName = TagName.JW2_Stage3;
					break;
				case "jw2_stage4":
					mTagName = TagName.JW2_Stage4;
					break;
				case "jw2_stage5":
					mTagName = TagName.JW2_Stage5;
					break;
				case "jw2_stage6":
					mTagName = TagName.JW2_Stage6;
					break;
				case "jw2_stage7":
					mTagName = TagName.JW2_Stage7;
					break;
				case "jw2_stage8":
					mTagName = TagName.JW2_Stage8;
					break;
				case "jw2_stage9":
					mTagName = TagName.JW2_Stage9;
					break;
				case "jw2_stage10":
					mTagName = TagName.JW2_Stage10;
					break;
				case "jw2_stage11":
					mTagName = TagName.JW2_Stage11;
					break;
				case "jw2_stage12":
					mTagName = TagName.JW2_Stage12;
					break;
				case "jw2_stage13":
					mTagName = TagName.JW2_Stage13;
					break;
				case "jw2_stage14":
					mTagName = TagName.JW2_Stage14;
					break;
				case "jw2_stage15":
					mTagName = TagName.JW2_Stage15;
					break;
				case "jw2_stage16":
					mTagName = TagName.JW2_Stage16;
					break;
				case "jw2_stage17":
					mTagName = TagName.JW2_Stage17;
					break;
				case "jw2_stage18":
					mTagName = TagName.JW2_Stage18;
					break;
				case "jw2_stage19":
					mTagName = TagName.JW2_Stage19;
					break;
				case "jw2_chapter":
					mTagName = TagName.JW2_Chapter;
					break;
				case "jw2_curstage":
					mTagName = TagName.JW2_CurStage;
					break;
				case "jw2_maxstage":
					mTagName = TagName.JW2_MaxStage;
					break;
				case"JW2_BUYSN":
				case "jw2_buysn":
					mTagName = TagName.JW2_BUYSN;
					break;
				case "jw2_goodstype":
					mTagName = TagName.JW2_GOODSTYPE;
					break;
				case "jw2_goodsindex":
					mTagName = TagName.JW2_GOODSINDEX;
					break;
				case "jw2_recesn":
				case "jw2_recvsn":
					mTagName = TagName.JW2_RECESN;
					break;
				case "jw2_buydate":
					mTagName = TagName.JW2_BUYDATE;
					break;
				case "jw2_reason":
					mTagName = TagName.JW2_Reason;
					break;
				case "Magic_Dates":
					mTagName = TagName.Magic_Dates;
					break;
				case "TIMEBegin":
					mTagName = TagName.TIMEBegin;
					break;
				case "TIMEEend":
					mTagName = TagName.TIMEEend;
					break;
				case "jw2_receid":
				case "JW2_RECEID":
					mTagName = TagName.JW2_RECEID;
					break;
				case "jw2_malesn"://男性SN
					mTagName = TagName.JW2_MALESN;
					break;
				case "jw2_maleusername"://男性用户名
					mTagName = TagName.JW2_MALEUSERNAME;
					break;
				case "jw2_maleusernick"://男性昵称
					mTagName = TagName.JW2_MALEUSERNICK;
					break;
				case "jw2_femalesn"://女性SN
					mTagName = TagName.JW2_FEMAILESN;
					break;
				case "jw2_femaleusername"://女性用户名
					mTagName = TagName.JW2_FEMALEUSERNAME;
					break;
				case "jw2_femaleusernick"://女性昵称
					mTagName = TagName.JW2_FEMAILEUSERNICK;
					break;
				case "jw2_weddingdate"://结婚时间
					mTagName = TagName.JW2_WEDDINGDATE;
					break;
				case "jw2_unweddingdate"://离婚时间
					mTagName = TagName.JW2_UNWEDDINGDATE;
					break;
				case "jw2_weddingname"://婚礼名称
					mTagName = TagName.JW2_WEDDINGNAME;
					break;
				case "jw2_weddingvow"://婚礼誓言
					mTagName = TagName.JW2_WEDDINGVOW;
					break;
				case "jw2_ringlevel"://戒指等级
					mTagName = TagName.JW2_RINGLEVEL;
					break;
				case "jw2_redheartnum"://红心数量
					mTagName = TagName.JW2_REDHEARTNUM;
					break;
				case "jw2_weddingsn":
				case "jw2_weddingno"://婚姻序号
					mTagName = TagName.JW2_WEDDINGNO;
					break;
				case "jw2_confirmsn"://见证者SN
					mTagName = TagName.JW2_CONFIRMSN;
					break;
				case "jw2_confirmusername"://见证者用户名
					mTagName = TagName.JW2_CONFIRMUSERNAME;
					break;
				case "jw2_confirmusernick"://://见证者昵称
					mTagName = TagName.JW2_CONFIRMUSERNICK;
					break;
				case "jw2_coupledate"://情侣日期
					mTagName = TagName.JW2_COUPLEDATE;
					break;
				case "jw2_kissnum"://kiss次数
					mTagName = TagName.JW2_KISSNUM;
					break;
				case "jw2_lastkissdate"://最后一次Kiss时间
					mTagName = TagName.JW2_LASTKISSDATE;
					break;
				case "jw2_breakdate"://分手时间
					mTagName = TagName.JW2_BREAKDATE;
					break;
				case "jw2_cmbreakdate"://确认分手时间
					mTagName = TagName.JW2_CMBREAKDATE;
					break;
				case "jw2_breaksn"://提出SN
					mTagName = TagName.JW2_BREAKSN;
					break;
				case "jw2_breakusername"://提出用户名
					mTagName = TagName.JW2_BREAKUSERNAME;
					break;
				case "jw2_breakusernick"://提出昵称
					mTagName = TagName.JW2_BREAKUSERNICK;
					break;
				case "jw2_lastlogindate"://最后登陆时间
					mTagName = TagName.JW2_LASTLOGINDATE;
					break;
				case "jw2_registdate"://激活时间
					mTagName = TagName.JW2_REGISTDATE;
					break;
				case "jw2_fcmstatus"://防沉迷状态
					mTagName = TagName.JW2_FCMSTATUS;
					break;
				case "jw2_familyid"://家族ID
					mTagName = TagName.JW2_FAMILYID;
					break;
				case "jw2_familyname"://家族名称
					mTagName = TagName.JW2_FAMILYNAME;
					break;
				case "jw2_dutyid"://职业号
					mTagName = TagName.JW2_DUTYID;
					break;
				case "jw2_dutyname"://职业名
					mTagName = TagName.JW2_DUTYNAME;
					break;
				case "jw2_attendtime"://加入时间
					mTagName = TagName.JW2_ATTENDTIME;
					break;
				case "jw2_couplesn"://加入时间
					mTagName = TagName.JW2_COUPLESN;
					break;
				case "jw2_createtime"://创建时间
					mTagName = TagName.JW2_CREATETIME;
					break;
				case "jw2_cnt"://人数
					mTagName = TagName.JW2_CNT;
					break;
				case "jw2_point"://积分
					mTagName = TagName.JW2_POINT;
					break;
				case "jw2_logintype"://类型，0登入，1登出
					mTagName = TagName.JW2_LOGINTYPE;
					break;
				case "jw2_time"://时间
				case "JW2_TIME":
					mTagName = TagName.JW2_TIME;
					break;
				case "jw2_ip"://IP地址
					mTagName = TagName.JW2_IP;
					break;
				case "jw2_itempos"://物品位置
					mTagName = TagName.JW2_ItemPos;
					break;
				case "jw2_itemno"://物品序号
					mTagName = TagName.JW2_ItemNo;
					break;
				case "jw2_mailtitle"://邮件主题
					mTagName = TagName.JW2_MailTitle;
					break;
				case "jw2_mailcontent"://邮件内容
					mTagName = TagName.JW2_MailContent;
					break;
				case "jw2_frmLove":
					mTagName = TagName.jw2_frmLove;
					break;
				case "jw2_useLove":
					mTagName = TagName.jw2_useLove;
					break;
				case "jw2_repute"://声望
					mTagName = TagName.JW2_Repute;
					break;
				case "jw2_nowtitle"://称号
					mTagName = TagName.JW2_NowTitle;
					break;
				case "jw2_familylevel"://等级
					mTagName = TagName.JW2_FamilyLevel;
					break;
				case "jw2_attendrank"://人气排名
					mTagName = TagName.JW2_AttendRank;
					break;
				case "jw2_moneyrank"://财富排名
					mTagName = TagName.JW2_MoneyRank;
					break;
				case "jw2_powerrank"://实力排名
					mTagName = TagName.JW2_PowerRank;
					break;
				case "jw2_itemcode"://道具ID
					mTagName = TagName.JW2_ItemCode;
					break;
				case "jw2_itemname"://道具名称
					mTagName = TagName.JW2_ItemName;
					break;
				case "jw2_productid"://商品ID
					mTagName = TagName.JW2_Productid;
					break;
				case "jw2_productname"://商品名称
				case "JW2_PRODUCTNAME":
					mTagName = TagName.JW2_ProductName;
					break;
				case "jw2_familypoint"://家族点数
					mTagName = TagName.JW2_FamilyPoint;
					break;
				case "jw2_petsn"://宠物ID
					mTagName = TagName.JW2_PetSn;
					break;
				case "jw2_petname"://宠物名称
					mTagName = TagName.JW2_PetName;
					break;
				case"jw2_eggnum"://宠物蛋数量
					mTagName = TagName.jw2_EggNum;
					break;
				case"jw2_petaggid"://宠物蛋ID
					mTagName = TagName.jw2_petaggID;
					break;
				case "jw2_petnick"://宠物自定义名称
					mTagName = TagName.JW2_PetNick;
					break;
				case "jw2_petlevel"://宠物等级
					mTagName = TagName.JW2_PetLevel;
					break;
				case "jw2_petexp"://宠物经验
					mTagName = TagName.JW2_PetExp;
					break;
				case "jw2_petevol"://进阶阶段
					mTagName = TagName.JW2_PetEvol;
					break;
				case "jw2_mailsn"://序列号
					mTagName = TagName.JW2_MailSn;
					break;
				case "jw2_sendnick"://发件人昵称
					mTagName = TagName.JW2_SendNick;
					break;
				case "jw2_senddate"://发送日期
					mTagName = TagName.JW2_SendDate;
					break;
				case"jw2_sn_num":
				case "jw2_num"://数量
					mTagName = TagName.JW2_Num;
					break;
				case "jw2_rate"://男女比例
					mTagName = TagName.JW2_Rate;
					break;
				case "jw2_shaikhnick"://族长名称
					mTagName = TagName.JW2_ShaikhNick;
					break;
				case "jw2_subfamilyname"://隶属家族名称
					mTagName = TagName.JW2_SubFamilyName;
					break;

				case "jw2_LastGetPointDate": //最后一次获得活跃度的日期
					mTagName = TagName.jw2_LastGetPointDate;
					break;
				case "jw2_MultiDays": //连续活跃天数 
					mTagName = TagName.jw2_MultiDays;
					break;
				case "jw2_TodayActivePoint"://今天获得的活跃度点数 
					mTagName = TagName.jw2_TodayActivePoint;
					break;
				case "jw2_activepoint"://活跃度
					mTagName = TagName.jw2_activepoint ;
					break;
					#endregion
				case"AU_TaskID":
					mTagName = TagName.AU_TaskID;
					break;
				case"AU_City":
					mTagName = TagName.AU_City;
					break;
				case"AU_BoardMessage":
					mTagName = TagName.AU_BoardMessage;
					break;
				case"AU_BeginTime":
					mTagName = TagName.AU_BeginTime;
					break;
				case"AU_EndTime":
					mTagName = TagName.AU_EndTime;
					break;
					#region 魔力宝贝2
				case"Magic_AccountBan":
					mTagName = TagName.Magic_AccountBan;
					break;
				case"Magic_CharBan":
					mTagName = TagName.Magic_CharBan;
					break;
				case"Magic_LeftMoney":
					mTagName = TagName.Magic_LeftMoney;
					break;
				case"Magic_sendPetName":
					mTagName = TagName.Magic_sendPetName;
					break;
				case"Magic_getPetName":
					mTagName = TagName.Magic_getPetName;
					break;
				case"Magic_sendPetID":
					mTagName = TagName.Magic_sendPetID;
					break;
				case"Magic_getPetID":
					mTagName = TagName.Magic_getPetID;
					break;
				case"Magic_getMoney":
					mTagName = TagName.Magic_getMoney;
					break;
				case"Magic_sendMoney":
					mTagName = TagName.Magic_sendMoney;
					break;
				case"Magic_sendItem":
					mTagName = TagName.Magic_sendItem;
					break;
				case"Magic_getItem":
					mTagName = TagName.Magic_getItem;
					break;

				case"Magic_GetTime":
					mTagName = TagName.Magic_GetTime;
					break;
				case"Magic_PetSrcName":
					mTagName = TagName.Magic_PetSrcName;
					break;
				case"AuShop_ExpireType":
					mTagName = TagName.AuShop_ExpireType;
					break;
				case"Magic_Wind":
					mTagName = TagName.Magic_Wind;
					break;
				case"Magic_Soil":
					mTagName = TagName.Magic_Soil;
					break;
				case"Magic_Water":
					mTagName = TagName.Magic_Water;
					break;
				case"Magic_Frie":
					mTagName = TagName.Magic_Frie;
					break;

				case"Magic_petID":
					mTagName = TagName.Magic_PetID;
					break;
				case"AuShop_pid":
					mTagName = TagName.AuShop_pid;
					break;
				case"AuShop_username":
					mTagName = TagName.AuShop_username;
					break;
				case"AuShop_Reason":
					mTagName = TagName.AuShop_Reason;
					break;
				case"AuShop_Time":
					mTagName = TagName.AuShop_Time;
					break;
				case"AuShop_Result":
					mTagName = TagName.AuShop_Result;
					break;
				case"Magic_userItemName":
					mTagName = TagName.Magic_userItemName;
					break;
				case"Mggic_SkillType":
					mTagName = TagName.Mggic_SkillType;
					break;
				case"Magic_CreateTime":
					mTagName = TagName.Magic_CreateTime;
					break;
				case"Magic_IP":
					mTagName = TagName.Magic_Ip;
					break;
				case"Magic_Online":
					mTagName = TagName.Magic_IsOnline;
					break;
				case"content":
					mTagName = TagName.Magic_Content;
					break;
				case"guildId":
					mTagName = TagName.Magic_GuildID;
					break;
				case"guildName":
					mTagName = TagName.Magic_GuildName;
					break;
				case"honor":
					mTagName = TagName.Magic_GuildHoro;
					break;
				case"memberCnt":
					mTagName = TagName.Magic_GuildNum;
					break;
				case"masterName":
					mTagName = TagName.Magic_Master;
					break;
				case"masterIdkey":
					mTagName = TagName.Magic_MasterIdkey;
					break;
				case"rank":
					mTagName = TagName.Magic_GuildRank;
					break;
				case"iuReservet02":
					mTagName = TagName.Magic_PetFullLvl;
					break;
				case"max_lp":
				case"max_pet_lp":
					mTagName = TagName.Magic_PetMaxBlood;
					break;
				case"max_fp":
				case"max_pet_fp":
					mTagName = TagName.Magic_PetMaxMag;
					break;
				case"reserve01":
					mTagName = TagName.Magic_ComposeTimes;
					break;
				case"reserve02":
					mTagName = TagName.Magic_ComposeItemID;
					break;
				case"magic_Fnick":
					mTagName = TagName.Magic_FriendCharNum;
					break;
				case"friendIDkey":
					mTagName = TagName.Magic_FriendIDKey;
					break;
				case"note_id":
					mTagName = TagName.Magic_NoticeID;
					break;
				case"gm_id":
					mTagName = TagName.Magic_NoticeID;
					break;
				case"serverip":
					mTagName = TagName.ServerInfo_IP;
					break;
				case"note_Content":
					mTagName = TagName.Magic_Content;
					break;
				case"datetime":
					mTagName = TagName.BeginTime;
					break;
				case"endtime":
					mTagName = TagName.EndTime;
					break;
				case"paramBase01":
				case"Magic_Physical":
					mTagName = TagName.Magic_Physical;
					break;
				case"paramBase02":
					mTagName = TagName.Magic_Power;
					break;
				case"paramBase03":
					mTagName = TagName.Magic_Smart;
					break;
				case"paramBase04":
					mTagName = TagName.Magic_Speed;
					break;
				case"paramBase05":
					mTagName = TagName.Magic_Spirit;
					break;
				case"Magic_Times":
					mTagName = TagName.Magic_Times;
					break;
				case"map_name":
					mTagName = TagName.Magic_MapName;
					break;
				case"magic_mapid":
					mTagName = TagName.Magic_MapID;
					break;
				case"Magic_TypeId":
					mTagName = TagName.Magic_TypeID;
					break;
				case"Magic_TypeName":
					mTagName = TagName.Magic_TypeName;
					break;
				case"charisma":
					mTagName = TagName.Magic_Charm;
					break;
				case"prison":
					mTagName = TagName.Magic_InPrison;
					break;
				case"baseParam00":
					mTagName = TagName.Magic_LeftPoint;
					break;
				case"baseParam01":
					mTagName = TagName.Magic_Physical;
					break;
				case"baseParam02":
					mTagName = TagName.Magic_Power;
					break;
				case"baseParam03":
					mTagName = TagName.Magic_Smart;
					break;
				case"baseParam04":
					mTagName = TagName.Magic_Speed;
					break;
					
				case"baseParam05":
					mTagName = TagName.Magic_Spirit;
					break;
				case"paramParam01":
					mTagName = TagName.Magic_MaxBlood;
					break;
				case"paramParam02":
				case"Magic_MaxMag":
					mTagName = TagName.Magic_MaxMag;
					break;
				case"paramParam03":
				case"Magic_Attack":
					mTagName = TagName.Magic_Attack;
					break;
				case"paramParam04":
				case"Magic_Defend":
					mTagName = TagName.Magic_Defend;
					break;
				case"paramParam05":
					mTagName = TagName.Magic_Cheesy;
					break;
				case"paramParam06":
					mTagName = TagName.Magic_MagAttack;
					break;
				case"paramParam07":
					mTagName = TagName.Magic_MagDefend;
					break;
				case"paramParam08":
					mTagName = TagName.Magic_Recover;
					break;
				case"paramParam09":
					mTagName = TagName.Magic_Hit;
					break;
				case"paramParam10":
					mTagName = TagName.Magic_Avoid;
					break;
				case"paramParam11":
					mTagName = TagName.Magic_Critical;
					break;
				case"paramParam12":
					mTagName = TagName.Magic_Retort;
					break;
				case"Magic_PetAttack":
					mTagName = TagName.Magic_PetAttack;
					break;
				case"Magic_PetDefend":
					mTagName = TagName.Magic_PetDefend;
					break;
				case"Magic_PetCheesy":
					mTagName = TagName.Magic_PetCheesy;
					break;
				case"Magic_PetMagAttack":
					mTagName = TagName.Magic_PetMagAttack;
					break;
				case"Magic_PetMagDefend":
					mTagName = TagName.Magic_PetMagDefend;
					break;
				case"Magic_PetRecover":
					mTagName = TagName.Magic_PetRecover;
					break;
				case"Magic_PetHit":
					mTagName = TagName.Magic_PetHit;
					break;
				case"Magic_PetAvoid":
					mTagName = TagName.Magic_PetAvoid;
					break;
				case"Magic_PetCritical":
					mTagName = TagName.Magic_PetCritical;
					break;
				case"Magic_PetRetort":
					mTagName = TagName.Magic_PetRetort;
					break;
				case"charNo":
					mTagName = TagName.Magic_CharNo;
					break;
				case"mag_itemID":
					mTagName = TagName.Magic_PetItemID;
					break;
				case"mag_itemName":
				case"magic_itemName":
					mTagName = TagName.Magic_ItemName;
					break;
				case"loginTime":
					mTagName = TagName.Magic_LatestLoginTime;
					break;
				case"logoutTime":
					mTagName = TagName.Magic_LatestLogoutTime;
					break;
				case"charCount":
					mTagName = TagName.Magic_UserNum;
					break;
				case"friendVal":
					mTagName = TagName.Magic_FriendShip;
					break;
				case"qualityRank":
					mTagName = TagName.Magic_QualityLevel;
					break;
				case"magic_itemID":
				case"Magic_LogitemID":
					mTagName = TagName.Magic_PetItemID;
					break;
				case"str":
					mTagName = TagName.Magic_Power;
					break;
				case"vit":
					mTagName = TagName.Magic_PetPhysical;
					break;
				case"active":
					mTagName = TagName.Magic_Smart;
					break;
				case"pet_speed":
					mTagName = TagName.Magic_Speed;
					break;
				case"spirit":
					mTagName = TagName.Magic_Spirit;
					break;
				case"loyalty":
					mTagName = TagName.Magic_PetFaith;
					break;
				case"petSlot":
					mTagName = TagName.Magic_PetID;
					break;
				case"getTime":
					mTagName = TagName.Magic_PetItemGetTime;
					break;
				case"chargeCombineItemID":
					mTagName = TagName.Magic_ComposeItemID;
					break;
				case"itemID":
					mTagName = TagName.Magic_PetItemID;
					break;
				case"repairNum":
					mTagName = TagName.Magic_FixTimes;
					break;
				case"dur":
					mTagName = TagName.Magic_CurEndure;
					break;
				case"palette":
					mTagName = TagName.Magic_Palette;
					break;
					
				case"maxDur":
					mTagName = TagName.Magic_MaxEndure;
					break;
				case"itemSlot":
					mTagName = TagName.Magic_PetItemCol;
					break;
				case"pet_name":
					mTagName = TagName.Magic_PetSrcName;
					break;
				case"youself_name":
					mTagName = TagName.Magic_PetNewName;
					break;
				case"lp":
					mTagName = TagName.Magic_Blood;
					break;
				case"fp":
				case"Magic_Magic":
					mTagName = TagName.Magic_Magic;
					break;
				case"fame":
					mTagName = TagName.Magic_Fame;
					break;
				case"cdkey":
					mTagName = TagName.Magic_UserName;
					break;
				case"IDKey":
				case"IdKey":
					mTagName = TagName.Magic_UserID;
					break;
				case"magic_nick":
				case"Magic_NickName":
					mTagName = TagName.Magic_nickname;
					break;
				case"lv":
					mTagName = TagName.Magic_Level;
					break;
				case"exp":
					mTagName = TagName.Magic_Exp;
					break;
				case"magic_sex":
					mTagName = TagName.Magic_Sex;
					break;
				case"jobID":
					mTagName = TagName.Magic_JobID;
					break;
				case"gold":
					mTagName = TagName.Magic_Money;
					break;
				case"masterGuildID":
					mTagName = TagName.Magic_isChairman;
					break;
				case"choiceGuildID":
					mTagName = TagName.Magic_GuildID;
					break;
				case"injury":
					mTagName = TagName.Magic_Injured;
					break;
				case"soulInjury":
					mTagName = TagName.Magic_Status;
					break;
				case"strength":
					mTagName = TagName.Magic_Physical;
					break;
				case"Magic_attack":
					mTagName = TagName.Magic_Attack;
					break;
				case"duelPoint":
					mTagName = TagName.Magic_Fight;
					break;
				case"dex":
					mTagName = TagName.Magic_Smart;
					break;
				case"speed":
					mTagName = TagName.Magic_Speed;
					break;
				case"vitality":
					mTagName = TagName.Magic_Spirit;
					break;
				case"job_name":
					mTagName = TagName.Magic_Profession;
					break;
				case"job_id":
					mTagName = TagName.Magic_JobID;
					break;
				case"jobRank":
					mTagName = TagName.Magic_ProfessionLevel;
					break;
				case"userTitle":
					mTagName = TagName.Magic_OwnName;
					break;
				case"mapId":
					mTagName = TagName.Magic_MapID;
					break;
				case"map":
					mTagName = TagName.Magic_Coordinate;
					break;
				case"fameTitle":
					mTagName = TagName.Magic_ProHonorRank;
					break;
				case"tmpTitle":
					mTagName = TagName.Magic_tempRank;
					break;
				case"slotNo":
					mTagName = TagName.Magic_SlotNum;
					break;
				case"friendName":
					mTagName = TagName.Magic_FriendCharNum;
					break;
				case"stackNum":
					mTagName = TagName.Magic_CombineNum;
					break;
				case"f_IdKey":
					mTagName = TagName.Magic_FriendIDKey;
					break;
				case"skillID":
					mTagName = TagName.Magic_SkillName;
					break;
				case"Magic_SkillID":
					mTagName = TagName.Magic_SkillID;
					break;
				case"skillRank":
					mTagName = TagName.Magic_SkillLevel;
					break;
				case"skillExp":
					mTagName = TagName.Magic_SkillExp;
					break;
				case"targetIDKey":
					mTagName = TagName.Magic_targetIDKey;
					break;
				case"targetName":
					mTagName = TagName.Magic_targetName;
					break;
				case"category":
					mTagName = TagName.Magic_category;
					break;
				case"action":
					mTagName = TagName.Magic_action;
					break;
				case"mgvalue":
					mTagName = TagName.Magic_mgvalue;
					break;
				case"mgstring":
					mTagName = TagName.Magic_mgstring;
					break;
				case"logtime":
					mTagName = TagName.Magic_logtime;
					break;
					#endregion

				case "Linker_Name":
					mTagName = TagName.LINKER_NAME;
					break;
				case "Linker_Content":
					mTagName = TagName.LINKER_CONTENT;
					break;
				case "Notes_Category":
					mTagName = TagName.NOTES_Category;
					break;
				case "Notes_ID":
					mTagName = TagName.NOTES_ID;
					break;
				case "Notes_UID":
					mTagName = TagName.NOTES_UID;
					break;
				case "Notes_PUID":
					mTagName = TagName.NOTES_PUID;
					break;
				case "Notes_Subject":
					mTagName = TagName.NOTES_SUBJECT;
					break;
				case "Notes_From":
					mTagName = TagName.NOTES_FROM;
					break;
				case "Notes_Date":
					mTagName = TagName.NOTES_DATE;
					break;
				case "Notes_Sender":
					mTagName = TagName.NOTES_Sender;
					break;
				case "Notes_Recive":
					mTagName = TagName.NOTES_RECEIVER;
					break;
				case "Notes_Flag":
					mTagName = TagName.NOTES_Flag;
					break;
				case "Notes_Supervisors":
					mTagName = TagName.NOTES_SUPERVISORS;
					break;
				case "Notes_Content":
					mTagName = TagName.NOTES_CONTENT;
					break;
				case "Notes_AttachmentCount":
					mTagName = TagName.NOTES_AttachmentCount;
					break;
				case "Notes_View":
					mTagName = TagName.NOTES_View;
					break;
				case "Notes_Attachment":
					mTagName = TagName.NOTES_Attachment;
					break;
				case "Notes_AttachmentName":
					mTagName = TagName.Notes_AttachmentName;
					break;
				case "Notes_Status":
					mTagName = TagName.NOTES_Status;
					break;

				case "dateLimit":
					mTagName = TagName.SDO_DateLimit;
					break;
				case "droptime":
					mTagName = TagName.SDO_EndTime;
					break;
				case "UserIndexID":
					mTagName = TagName.SDO_UserIndexID;
					break;
				case "state":
					mTagName = TagName.SDO_State;
					break;
				case "food":
					mTagName = TagName.SDO_Food;
					break;
				case "mood":
					mTagName = TagName.SDO_mood;
					break;
				case "PresentID":
					mTagName = TagName.AuShop_PresentID;
					break;
				case "UserSN":
				case "usersn":
					mTagName = TagName.AU_UserSN;
					break;
				case "sendSN":
					mTagName = TagName.AU_SendSN;
					break;
				case "sendNick":
					mTagName = TagName.AU_SendNick;
					break;
				case "BuyNick":
					mTagName = TagName.AU_BuyNick;
					break;
				case "RecvSN":
					mTagName = TagName.AU_RecvSN;
					break;
				case "RecvNick":
					mTagName = TagName.AU_RecvNick;
					break;
				case "period":
					mTagName = TagName.AU_Period;
					break;
				case "sendDate":
					mTagName = TagName.AU_SendDate;
					break;
				case "RecvDate":
					mTagName = TagName.AU_RecvDate;
					break;
				case "sdate":
					mTagName = TagName.AuShop_sDate;
					break;
				case "stime":
					mTagName = TagName.AuShop_sTime;
					break;
				case "ExpiredType":
					mTagName = TagName.AuShop_ExpireType;
					break;
				case "Cash":
				case "mcash":
					mTagName = TagName.AU_Cash;
					break;
				case "hair":
					mTagName = TagName.AU_hair;
					break;
				case "face":
					mTagName = TagName.AU_face;
					break;
				case "jacket":
					mTagName = TagName.AU_jacket;
					break;
				case "pants":
					mTagName = TagName.AU_pants;
					break;
				case "shoes":
					mTagName = TagName.AU_shoes;
					break;
				case "sets1":
					mTagName = TagName.AU_sets1;
					break;
				case "sets2":
					mTagName = TagName.AU_sets2;
					break;
				case "pet":
					mTagName = TagName.AU_pet;
					break;
				case "ItemID":
					mTagName = TagName.AU_ItemID;
					break;
				case "ItemName":
				case "itemName":
				case "AU_ItemName":
					mTagName = TagName.AU_ItemName;
					break;
				case "UseItem":
					mTagName = TagName.AU_UseItem;
					break;
				case "UseCount":
					mTagName = TagName.AU_UsedCount;
					break;
				case "Success":
					mTagName = TagName.AU_Success;
					break;
				case "RoomName":
					mTagName = TagName.AU_RomName;
					break;
				case "Finger":
					mTagName = TagName.AU_Finger;
					break;
				case "Flower":
					mTagName = TagName.AU_Flower;
					break;
				case "PresentMoney":
					mTagName = TagName.AU_PresentMoney;
					break;
				case "RemainCount":
					mTagName = TagName.AU_RemainCount;
					break;
				case "ExpiredDate":
					mTagName = TagName.AU_ExpireDate;
					break;
				case "GUID":
					mTagName = TagName.FJ_GuidID;
					break;
				case "color_level": 
					mTagName=TagName.FJ_color_level;
					break;
				case "GType": 
					mTagName=TagName.FJ_GType;
					break;
				case "FStr": 
					mTagName=TagName.FJ_FStr;
					break;
				case "FDex": 
					mTagName=TagName.FJ_FDex;
					break;
				case "FCon": 
					mTagName=TagName.FJ_FCon;
					break;
				case "FInt": 
					mTagName=TagName.FJ_FInt;
					break;
				case "FWit": 
					mTagName=TagName.FJ_FWit;
					break;
				case "FSpi": 
					mTagName=TagName.FJ_FSpi;
					break;
				case "FSpeed": 
					mTagName=TagName.FJ_FSpeed;
					break;
				case "FSwordMaster": 
					mTagName=TagName.FJ_FSwordMaster;
					break;
				case "FBluntMaster": 
					mTagName=TagName.FJ_FBluntMaster;
					break;
				case "FDaggerMaster": 
					mTagName=TagName.FJ_FDaggerMaster;
					break;
				case "FLanceMaster": 
					mTagName=TagName.FJ_FLanceMaster;
					break;
				case "FMagWeaponMaster": 
					mTagName=TagName.FJ_FMagWeaponMaster;
					break;
				case "FBowMovingShoot":
					mTagName=TagName.FJ_FBowMovingShoot;
					break;
				case "FBowAttackDist": 
					mTagName=TagName.FJ_FBowAttackDist;
					break;
				case "FBowMaster": 
					mTagName=TagName.FJ_FBowMaster;
					break;
				case "FLightArmorMaster": 
					mTagName=TagName.FJ_FLightArmorMaster;
					break;
				case "FHeavyArmorMaster": 
					mTagName=TagName.FJ_FHeavyArmorMaster;
					break;
				case "FMagArmorMaster": 
					mTagName=TagName.FJ_FMagArmorMaster;
					break;
				case "FShieldMaster": 
					mTagName=TagName.FJ_FShieldMaster;
					break;
				case "FDualMaster": 
					mTagName=TagName.FJ_FDualMaster;
					break;
				case "FHPMax": 
					mTagName=TagName.FJ_FHPMax;
					break;
				case "FHPReg": 
					mTagName=TagName.FJ_FHPReg;
					break;
				case "FMPMax": 
					mTagName=TagName.FJ_FMPMax;
					break;
				case "FMPReg": 
					mTagName=TagName.FJ_FMPReg;
					break;
				case "FPhyAttack": 
					mTagName=TagName.FJ_FPhyAttack;
					break;
				case "FPhyDefend":
					mTagName=TagName.FJ_FPhyDefend;
					break;
				case "FPhyHit": 
					mTagName=TagName.FJ_FPhyHit;
					break;
				case "FPhyAvoid": 
					mTagName=TagName.FJ_FPhyAvoid;
					break;
				case "FPhyAttackSpeed": 
					mTagName=TagName.FJ_FPhyAttackSpeed;
					break;
				case "FPhyCritical": 
					mTagName=TagName.FJ_FPhyCritical;
					break;
				case "FPhyCriticalPower":
					mTagName=TagName.FJ_FPhyCriticalPower;
					break;
				case "FPhyHPAbsorb":
					mTagName=TagName.FJ_FPhyHPAbsorb;
					break;
				case "FPhyMPAbsorb": 
					mTagName=TagName.FJ_FPhyMPAbsorb;
					break;
				case "FPhyAttackDist": 
					mTagName=TagName.FJ_FPhyAttackDist;
					break;
				case "FMagAttack": 
					mTagName=TagName.FJ_FMagAttack;
					break;
				case "FMagDefend": 
					mTagName=TagName.FJ_FMagDefend;
					break;
				case "FMagCritical": 
					mTagName=TagName.FJ_FMagCritical;
					break;
				case "FMagCriticalPower": 
					mTagName=TagName.FJ_FMagCriticalPower;
					break;
				case "FMagIntent": 
					mTagName=TagName.FJ_FMagIntent;
					break;
				case "FMagSpeed": 
					mTagName=TagName.FJ_FMagSpeed;
					break;
				case "FHealPower": 
					mTagName=TagName.FJ_FHealPower;
					break;
				case "FHealEff": 
					mTagName=TagName.FJ_FHealEff;
					break;
				case "FThorn": 
					mTagName=TagName.FJ_FThorn;
					break;
				case "FCDScale": 
					mTagName=TagName.FJ_FCDScale;
					break;
				case "FASI_0": 
					mTagName=TagName.FJ_FASI_0;
					break;
				case "FASI_1": 
					mTagName=TagName.FJ_FASI_1;
					break;
				case "FASI_2":
					mTagName=TagName.FJ_FASI_2;
					break;
				case "FASI_3": 
					mTagName=TagName.FJ_FASI_3;
					break;
				case "FASI_4": 
					mTagName=TagName.FJ_FASI_4;
					break;
				case "FASI_5": 
					mTagName=TagName.FJ_FASI_5;
					break;
				case "FASI_6": 
					mTagName=TagName.FJ_FASI_6;
					break;
				case "FASI_7": 
					mTagName=TagName.FJ_FASI_7;
					break;
				case "FASI_8":
					mTagName=TagName.FJ_FASI_8;
					break;
				case "FASI_9": 
					mTagName=TagName.FJ_FASI_9;
					break;
				case "FManaShield":
					mTagName=TagName.FJ_FManaShield;
					break;
				case "FJumpMaster":
					mTagName=TagName.FJ_FJumpMaster;
					break;
				case "FExpScale": 
					mTagName=TagName.FJ_FExpScale;
					break;
				case "FSPScale": 
					mTagName=TagName.FJ_FSPScale;
					break;
				case "FDamage2MP": 
					mTagName=TagName.FJ_FDamage2MP;
					break;
				case "FPhyDefScale": 
					mTagName=TagName.FJ_FPhyDefScale;
					break;
				case "FMagDefScale": 
					mTagName=TagName.FJ_FMagDefScale;
					break;
				case "FMagIceAttack": 
					mTagName=TagName.FJ_FMagIceAttack;
					break;
				case "FMagFireAttack": 
					mTagName=TagName.FJ_FMagFireAttack;
					break;
				case "FMagThunderAttack": 
					mTagName=TagName.FJ_FMagThunderAttack;
					break;
				case "FMagPoisonAttack": 
					mTagName=TagName.FJ_FMagPoisonAttack;
					break;
				case "FMagIceDefend": 
					mTagName=TagName.FJ_FMagIceDefend;
					break;
				case "FMagFireDefend": 
					mTagName=TagName.FJ_FMagFireDefend;
					break;
				case "FMagThunderDefend": 
					mTagName=TagName.FJ_FMagThunderDefend;
					break;
				case "FMagPoisonDefend": 
					mTagName=TagName.FJ_FMagPoisonDefend;
					break;
				case "FCBRate":
					mTagName=TagName.FJ_FCBRate;
					break;
				case "FCBPer": 
					mTagName=TagName.FJ_FCBPer;
					break;
				case "CarIDX":
					mTagName = TagName.RayCity_CarIDX;
					break;
				case "CharacterID":
					mTagName = TagName.RayCity_CharacterID;
					break;
				case "CarID":
					mTagName = TagName.RayCity_CarID;
					break;
				case "CarName":
					mTagName = TagName.RayCity_CarName;
					break;
				case "CarType":
					mTagName = TagName.RayCity_CarType;
					break;
				case "LastEquipInventoryIDX":
					mTagName = TagName.RayCity_LastEquipInventoryIDX;
					break;
				case "CarState":
					mTagName = TagName.RayCity_CarState;
					break;
				case "CreateDate":
					mTagName = TagName.RayCity_CreateDate;
					break;
				case "LastUpdateDate":
					mTagName = TagName.RayCity_LastUpdateDate;
					break;
				case "AccountID":
					mTagName = TagName.RayCity_AccountID;
					break;
				case "CharacterName":
					mTagName = TagName.RayCity_CharacterName;
					break;
				case "RecentMailIDX":
					mTagName = TagName.RayCity_RecentMailIDX;
					break;
				case "RecentGiftIDX":
					mTagName = TagName.RayCity_RecentGiftIDX;
					break;
				case "LastUseCarIDX":
					mTagName = TagName.RayCity_LastUseCarIDX;
					break;
				case "GarageIDX":
					mTagName = TagName.RayCity_GarageIDX;
					break;
				case "LastTutorialID":
					mTagName = TagName.RayCity_LastTutorialID;
					break;
				case "CharacterState":
					mTagName = TagName.RayCity_CharacterState;
					break;

				case "FriendIDX":
					mTagName = TagName.RayCity_FriendIDX;
					break;
				case "FriendCharacterID":
					mTagName = TagName.RayCity_FriendCharacterID;
					break;
				case "FriendCharacterName":
					mTagName = TagName.RayCity_FriendCharacterName;
					break;
				case "FriendGroupIDX":
					mTagName = TagName.RayCity_FriendGroupIDX;
					break;
				case "FriendState":
					mTagName = TagName.RayCity_FriendState;
					break;

				case "FriendGroupName":
					mTagName = TagName.RayCity_FriendGroupName;
					break;
				case "FriendGroupType":
					mTagName = TagName.RayCity_FriendGroupType;
					break;
				case "FriendGroupState":
					mTagName = TagName.RayCity_FriendGroupState;
					break;
				case "CarInventoryIDX":
					mTagName = TagName.RayCity_CarInventoryIDX;
					break;
				case "InventoryType":
					mTagName = TagName.RayCity_InventoryType;
					break;
				case "MaxInventorySize":
					mTagName = TagName.RayCity_MaxInventorySize;
					break;
				case "CurrentInventorySize":
					mTagName = TagName.RayCity_CurrentInventorySize;
					break;
				case "QuestLogIDX":
					mTagName = TagName.RayCity_QuestLogIDX;
					break;
				case "QuestID":
					mTagName = TagName.RayCity_QuestID;
					break;
				case "QuestState":
					mTagName = TagName.RayCity_QuestState;
					break;
				case "DealLogIDX":
					mTagName = TagName.RayCity_DealLogIDX;
					break;
				case "ItemIDX":
					mTagName = TagName.RayCity_ItemID;
					break;
				case "BuyDealClientID":
					mTagName = TagName.RayCity_BuyDealClientID;
					break;
				case "SellDealClientID":
					mTagName = TagName.RayCity_SellDealClientID;
					break;
				case "BuyPrice":
					mTagName = TagName.RayCity_BuyPrice;
					break;
				case "NyCashBalance":
					mTagName = TagName.RayCity_NyCashBalance;
					break;
				case "SellPrice":
					mTagName = TagName.RayCity_SellPrice;
					break;
				case "DealLogState":
					mTagName = TagName.RayCity_DealLogState;
					break;
				case "QuestOJTLogIDX":
					mTagName = TagName.RayCity_QuestOJTLogIDX;
					break;
				case "QuestOJTIDX":
					mTagName = TagName.RayCity_QuestOJTIDX;
					break;
				case "TaskValue":
					mTagName = TagName.RayCity_TaskValue;
					break;
				case "ExecuteCount":
					mTagName = TagName.RayCity_ExecuteCount;
					break;
				case "QuestOJTState":
					mTagName = TagName.RayCity_QuestOJTState;
					break;

				case "CharacterLevel":
					mTagName = TagName.RayCity_CharacterLevel;
					break;
				case "CharacterExp":
					mTagName = TagName.RayCity_CharacterExp;
					break;
				case "CharacterMoney":
					mTagName = TagName.RayCity_CharacterMoney;
					break;
				case "CharacterMileage":
					mTagName = TagName.RayCity_CharacterMileage;
					break;
				case "MaxCombo":
					mTagName = TagName.RayCity_MaxCombo;
					break;
				case "MaxSP":
					mTagName = TagName.RayCity_MaxSP;
					break;
				case "MaxMailCount":
					mTagName = TagName.RayCity_MaxMailCount;
					break;
				case "CurMailCount":
					mTagName = TagName.RayCity_CurMailCount;
					break;
				case "CurrentRP":
					mTagName = TagName.RayCity_CurrentRP;
					break;
				case "AccumulatedRP":
					mTagName = TagName.RayCity_AccumulatedRP;
					break;
				case "RelaxGauge":
					mTagName = TagName.RayCity_RelaxGauge;
					break;
				case "StartPos_TownCode":
					mTagName = TagName.RayCity_StartPos_TownCode;
					break;
				case "StartPos_FieldCode":
					mTagName = TagName.RayCity_StartPos_FieldCode;
					break;
				case "StartPos_X":
					mTagName = TagName.RayCity_StartPos_X;
					break;
				case "StartPos_Y":
					mTagName =  TagName.RayCity_StartPos_Y;
					break;
				case "StartPos_Z":
					mTagName =  TagName.RayCity_StartPos_Z;
					break;
				case "DealInventoryItemIDX":
					mTagName = TagName.RayCity_DealInventoryItemIDX;
					break;
				case "InventoryCellNo":
					mTagName = TagName.RayCity_InventoryCellNo;
					break;
				case "DealInventoryItemState":
					mTagName = TagName.RayCity_DealInventoryItemState;
					break;
				case "CarLevel":
					mTagName = TagName.RayCity_CarLevel;
					break;
				case "CarExp":
					mTagName = TagName.RayCity_CarExp;
					break;
				case "CarMileage":
					mTagName = TagName.RayCity_CarMileage;
					break;
				case "FuelQuantity":
					mTagName = TagName.RayCity_FuelQuantity;
					break;
				case "MissionLogIDX":
					mTagName = TagName.RayCity_MissionLogIDX;
					break;
				case "MissionID":
					mTagName = TagName.RayCity_MissionID;
					break;
				case "TotMissionCnt":
					mTagName = TagName.RayCity_TotMissionCnt;
					break;
				case "TotMissionSuccessCnt":
					mTagName = TagName.RayCity_TotMissionSuccessCnt;
					break;
				case "TotMissionFailCnt":
					mTagName = TagName.RayCity_TotMissionFailCnt;
					break;
				case "TotEXP":
					mTagName = TagName.RayCity_TotEXP;
					break;
				case "TotCarEXP":
					mTagName = TagName.RayCity_TotCarEXP;
					break;
				case "TotIncoming":
					mTagName = TagName.RayCity_TotIncoming;
					break;
				case "BingoCardIDX":
					mTagName = TagName.RayCity_BingoCardIDX;
					break;
				case "BingoCardID":
					mTagName = TagName.RayCity_BingoCardID;
					break;
				case "BingoRewardType":
					mTagName = TagName.RayCity_BingoRewardType;
					break;
				case "BingoRewardValue":
					mTagName = TagName.RayCity_BingoRewardValue;
					break;
				case "BingoRewardAmount":
					mTagName = TagName.RayCity_BingoRewardAmount;
					break;
				case "BingoCardState":
					mTagName = TagName.RayCity_BingoCardState;
					break;
				case "NyUserID":
					mTagName = TagName.RayCity_NyUserID;
					break;
				case "NyPassword":
					mTagName = TagName.RayCity_NyPassword;
					break;
				case "NyNickName":
					mTagName = TagName.RayCity_NyNickName;
					break;
				case "NyGender":
					mTagName = TagName.RayCity_NyGender;
					break;
				case "NyBirthyear":
					mTagName = TagName.RayCity_NyBirthYear;
					break;
				case "AccountState":
					mTagName = TagName.RayCity_AccountState;
					break;
				case "Createdate":
					mTagName = TagName.RayCity_CreateDate;
					break;
				case "CharacterType":
					mTagName = TagName.RayCity_CharacterType;
					break;
				case "TotPlayTime":
					mTagName = TagName.RayCity_TotPlayTime;
					break;
				case "LoginCount":
					mTagName = TagName.RayCity_LoginCount;
					break;
				case "LastLoginTime":
					mTagName = TagName.RayCity_LastLoginTime;
					break;
				case "LastLogoutTime":
					mTagName = TagName.RayCity_LastLogoutTime;
					break;
				case "LastLoginIPPrv":
					mTagName = TagName.RayCity_LastLoginIPPrv;
					break;
				case "LastLoginIPPub":
					mTagName = TagName.RayCity_LastLoginIPPub;
					break;
				case "TodayPlayTime":
					mTagName = TagName.RayCity_TodayPlayTime;
					break;
				case "TodayOfflineTime":
					mTagName = TagName.RayCity_TodayOfflineTime;
					break;
				case "IsLogin":
					mTagName = TagName.RayCity_IsLogin;
					break;
				case "SoloRaceWin":
					mTagName = TagName.RayCity_SoloRaceWin;
					break;
				case "SoloRaceLose":
					mTagName = TagName.RayCity_SoloRaceLose;
					break;
				case "SoloRaceRetire":
					mTagName = TagName.RayCity_SoloRaceRetire;
					break;
				case "TeamRaceWin":
					mTagName = TagName.RayCity_TeamRaceWin;
					break;
				case "TeamRaceLose":
					mTagName = TagName.RayCity_TeamRaceLose;
					break;
				case "FieldSoloRaceWin":
					mTagName = TagName.RayCity_FieldSoloRaceWin;
					break;
				case "FieldSoloRaceLose":
					mTagName = TagName.RayCity_FieldSoloRaceLose;
					break;
				case "FieldSoloRaceRetire":
					mTagName = TagName.RayCity_FieldSoloRaceRetire;
					break;
				case "FieldTeamRaceWin":
					mTagName = TagName.RayCity_FieldTeamRaceWin;
					break;
				case "FieldTeamRaceLose":
					mTagName = TagName.RayCity_FieldTeamRaceLose;
					break;
				case "ConnectionLogIDX":
					mTagName = TagName.RayCity_ConnectionLogIDX;
					break;
				case "ConnectionLogKey":
					mTagName = TagName.RayCity_ConnectionLogKey;
					break;
				case "ServerID":
					mTagName = TagName.RayCity_ServerID;
					break;
				case "IPAddress":
					mTagName = TagName.RayCity_IPAddress;
					break;
				case "CharacterLev":
					mTagName = TagName.RayCity_CharacterLevel;
					break;
				case "ActionType":
					mTagName = TagName.RayCity_ActionType;
					break;
				case "BuyType":
					mTagName = TagName.SD_BuyType;
					break;
				case "RCItemName":
					mTagName = TagName.RayCity_ItemName;
					break;
				case "ItemBuySellLogIDX":
					mTagName = TagName.RayCity_ItemBuySellLogIDX;
					break;
				case "Bonding":
					mTagName = TagName.RayCity_Bonding;
					break;
				case "MaxEndurance":
					mTagName = TagName.RayCity_MaxEndurance;
					break;
				case "CurEndurance":
					mTagName = TagName.RayCity_CurEndurance;
					break;
				case "ExpireDate":
					mTagName = TagName.RayCity_ExpireDate;
					break;
				case "ItemOption1":
					mTagName = TagName.RayCity_ItemOption1;
					break;
				case "ItemOption2":
					mTagName = TagName.RayCity_ItemOption2;
					break;
				case "ItemOption3":
					mTagName = TagName.RayCity_ItemOption3;
					break;
				case "ItemState":
					mTagName = TagName.RayCity_ItemState;
					break;
				case "ItemPrice":
					mTagName = TagName.RayCity_ItemPrice;
					break;
				case "BeforeCharacterMoney":
					mTagName = TagName.RayCity_BeforeCharacterMoney;
					break;

				case "AfterCharacterMoney":
					mTagName = TagName.RayCity_AfterCharacterMoney;
					break;
				case "MoneyType":
					mTagName = TagName.RayCity_MoneyType;
					break;
				case "QuestName":
					mTagName = TagName.RayCity_QuestName;
					break;
				case "GuildGroupIDX":
					mTagName = TagName.RayCity_GuildGroupIDX;
					break;
				case "GuildMemberState":
					mTagName = TagName.RayCity_GuildMemberState;
					break;
				case "GuildGroupName":
					mTagName = TagName.RayCity_GuildGroupName;
					break;
				case "GuildGroupRole":
					mTagName = TagName.RayCity_GuildGroupRole;
					break;
				case "GuildGroupState":
					mTagName = TagName.RayCity_GuildGroupState;
					break;
				case "GuildIDX":
					mTagName = TagName.RayCity_GuildIDX;
					break;
				case "GuildName":
					mTagName = TagName.RayCity_GuildName;
					break;
				case "GuildMessage":
					mTagName = TagName.RayCity_GuildMessage;
					break;
				case "MaxGuildMember":
					mTagName = TagName.RayCity_MaxGuildMember;
					break;
				case "CurGuildMember":
					mTagName = TagName.RayCity_CurGuildMember;
					break;
				case "IncreaseItemCount":
					mTagName = TagName.RayCity_IncreaseItemCount;
					break;
				case "TrackRaceWin":
					mTagName = TagName.RayCity_TrackRaceWin;
					break;
				case "TrackRaceLose":
					mTagName = TagName.RayCity_TrackRaceLose;
					break;
				case "FieldRaceWin":
					mTagName = TagName.RayCity_FieldRaceWin;
					break;
				case "FieldRaceLose":
					mTagName = TagName.RayCity_FieldRaceLose;
					break;
				case "GuildState":
					mTagName = TagName.RayCity_GuildState;
					break;
				case "RewardExp":
					mTagName = TagName.RayCity_RewardExp;
					break;
				case "RewardCarExp":
					mTagName = TagName.RayCity_RewardCarExp;
					break;
				case "RewardMoney":
					mTagName = TagName.RayCity_RewardMoney;
					break;
				case "MissionGrade":
					mTagName = TagName.RayCity_MissionGrade;
					break;
				case "MissionResult":
					mTagName = TagName.RayCity_MissionResult;
					break;
				case "Duration":
					mTagName = TagName.RayCity_Duration;
					break;
				case "MoneyLogIDX":
					mTagName = TagName.RayCity_MoneyLogIDX;
					break;
				case "AdjustType":
					mTagName = TagName.RayCity_AdjustType;
					break;
				case "UpdateSource":
					mTagName = TagName.RayCity_UpdateSource;
					break;
				case "MoneyAmount":
					mTagName = TagName.RayCity_MoneyAmount;
					break;
				case "RaceLogIDX":
					mTagName = TagName.RayCity_RaceLogIDX;
					break;
				case "RaceID":
					mTagName = TagName.RayCity_RaceID;
					break;
				case "RaceType":
					mTagName = TagName.RayCity_RaceType;
					break;
				case "TrackID":
					mTagName = TagName.RayCity_TrackID;
					break;
				case "RewardRP_RankBase":
					mTagName = TagName.RayCity_RewardRP_RankBase;
					break;
				case "RewardRP_TimeBase":
					mTagName = TagName.RayCity_RewardRP_TimeBase;
					break;
				case "Rank":
					mTagName = TagName.RayCity_Rank;
					break;
				case "RaceTime":
					mTagName = TagName.RayCity_RaceTime;
					break;
				case "RaceResult":
					mTagName = TagName.RayCity_RaceResult;
					break;
				case "FixedTime":
					mTagName = TagName.RayCity_FixedTime;
					break;
				case "SkillID":
					mTagName = TagName.RayCity_SkillID;
					break;
				case "SkillName":
					mTagName = TagName.RayCity_SkillName;
					break;
				case "SkillLevel":
					mTagName = TagName.RayCity_SkillLevel;
					break;
				case "SkillIDX":
					mTagName = TagName.RayCity_SkillIDX;
					break;

				case "ItemTypeID":
					mTagName = TagName.RayCity_ItemTypeID;
					break;
				case "ItemTypeName":
					mTagName = TagName.RayCity_ItemTypeName;
					break;
				case "TradeWaitItemIDX":
					mTagName = TagName.RayCity_TradeWaitItemIDX;
					break;
				case "CarInventoryItemIDX":
					mTagName = TagName.RayCity_CarInventoryItemIDX;
					break;
				case "TradeWaitCellNo":
					mTagName = TagName.RayCity_TradeWaitCellNo;
					break;
				case "TargetCarInventoryIDX":
					mTagName = TagName.RayCity_TargetCarInventoryIDX;
					break;
				case "TargetInventoryCellNo":
					mTagName = TagName.RayCity_TargetInventoryCellNo;
					break;
				case "TradeWaitItemState":
					mTagName = TagName.RayCity_TradeWaitItemState;
					break;
				case "TradeSessionIDX":
					mTagName = TagName.RayCity_TradeSessionIDX;
					break;
				case "TargetTradeSessionIDX":
					mTagName = TagName.RayCity_TargetTradeSessionIDX;
					break;
				case "IssueCouponIDX":
					mTagName = TagName.RayCity_IssueCouponIDX;
					break;
				case "TargetCharacterID":
					mTagName = TagName.RayCity_TargetCharacterID;
					break;
				case "TradeMoney":
					mTagName = TagName.RayCity_TradeMoney;
					break;
				case "TradeSessionState":
					mTagName = TagName.RayCity_TradeSessionState;
					break;
				case "ActionTypeName":
					mTagName  = TagName.RayCity_ActionTypeName;
					break;
				case "FJAccount":
					mTagName = TagName.FJ_UserID;
					break;
				case "guid":
					mTagName = TagName.FJ_GuidID;
					break;
				case "issuer":
					mTagName = TagName.FJ_issuer;
					break;
				case "descr":
					mTagName = TagName.FJ_descr;
					break;
				case "remain_time":
					mTagName = TagName.FJ_remain_time;
					break;
				case "sell_price":
					mTagName = TagName.FJ_sell_price;
					break;
				case "sell_credit":
					mTagName = TagName.FJ_sell_credit;
					break;
				case "sell_item_factory_mark":
					mTagName = TagName.FJ_sell_item_factory_mark;
					break;
				case "sell_item_guid":
					mTagName = TagName.FJ_sell_item_guid;
					break;
				case "sell_num":
					mTagName = TagName.FJ_sell_num;
					break;
				case "sell_inst_cur":
					mTagName = TagName.FJ_sell_inst_cur;
					break;
				case "sell_inst_max":
					mTagName = TagName.FJ_sell_inst_max;
					break;
				case "sell_remain_sec":
					mTagName = TagName.FJ_sell_remain_sec;
					break;
				case "sell_level":
					mTagName = TagName.FJ_sell_level;
					break;
				case "sell_signature":
					mTagName = TagName.FJ_sell_signature;
					break;
				case "sell_embed_0":
					mTagName = TagName.FJ_sell_embed_0;
					break;
				case "sell_embed_1":
					mTagName = TagName.FJ_sell_embed_1;
					break;
				case "sell_embed_2":
					mTagName = TagName.FJ_sell_embed_2;
					break;
				case "sell_embed_3":
					mTagName = TagName.FJ_sell_embed_3;
					break;
				case "sell_embed_4":
					mTagName = TagName.FJ_sell_embed_4;
					break;
				case "sell_embed_5":
					mTagName = TagName.FJ_sell_embed_5;
					break;
				case "sell_slots_opened":
					mTagName = TagName.FJ_sell_slots_opened;
					break;
				case "sell_rand_eff_id":
					mTagName = TagName.FJ_sell_rand_eff_id;
					break;
				case "bid_name":
					mTagName = TagName.FJ_bid_name;
					break;
				case "price_type":
					mTagName = TagName.FJ_price_type;
					break;
				case "name":
					mTagName = TagName.Rich_Name;
					break;
				case "CardName":
					mTagName = TagName.MF_cardname;
					break;
				case "online":
					mTagName = TagName.Rich_online;
					break;
				case "ServerIndex":
					mTagName = TagName.Rich_ServerIndex;
					break;
				case "AuthDate":
					mTagName = TagName.Rich_AuthDate;
					break;
				case "AuthReason":
					mTagName = TagName.Rich_AuthReason;
					break;
				case "JPPMoney":
					mTagName = TagName.Rich_JPPMoney;
					break;
				case "VisitTime":
					mTagName = TagName.Rich_VisitTime;
					break;
				case "DayOnlineSeconds":
					mTagName = TagName.Rich_DayOnlineSeconds;
					break;
				case "AllOnlineSeconds":
					mTagName = TagName.Rich_AllOnlineSeconds;
					break;
				case "LastLoginDate":
					mTagName = TagName.Rich_LastLoginDate;
					break;
				case "Birthday":
					mTagName = TagName.Rich_Birthday;
					break;
				case "Gender":
					mTagName = TagName.Rich_Gender;
					break;
	
				case "uid":
				case "id":
					mTagName = TagName.Rich_uid;
					break;
				case "score":
					mTagName = TagName.Rich_score;
					break;
				case "level":
					mTagName = TagName.Rich_level;
					break;
				case "win":
					mTagName = TagName.Rich_win;
					break;
				case "loss":
					mTagName = TagName.Rich_loss;
					break;
				case "equal":
					mTagName = TagName.Rich_equal;
					break;
				case "role":
					mTagName = TagName.Rich_role;
					break;
				case "roleid":
					mTagName = TagName.Rich_roleid;
					break;
				case "rolename":
					mTagName = TagName.Rich_rolename;
					break;
				case "gmoney":
					mTagName = TagName.Rich_gmoney;
					break;
				case "experience":
					mTagName = TagName.Rich_experience;
					break;
				case "introduce":
					mTagName = TagName.Rich_introduce;
					break;
				case "CreateTime":
					mTagName = TagName.Rich_CreateTime;
					break;
				case "stock_Bean":
					mTagName = TagName.Rich_stock_Bean;
					break;
				case "stock_Time":
					mTagName = TagName.Rich_stock_Time;
					break;
				case "Result":
					mTagName = TagName.Rich_Result;
					break;


				case "WeddingSN":
					mTagName = TagName.AU_WeddingSN;
					break;
				case "MaleSN":
					mTagName = TagName.AU_MaleSN;
					break;
				case "FemaleSN":
					mTagName = TagName.AU_FemaleSN;
					break;
				case "WeddingDate":
					mTagName = TagName.AU_WeddingDate;
					break;
				case "GotDen":
					mTagName = TagName.AU_GotDen;
					break;
				case "CoupleName":
					mTagName = TagName.AU_CoupleName;
					break;
				case "CoupleNotice":
					mTagName = TagName.AU_CoupleNotice;
					break;
				case "LogSN":
					mTagName = TagName.AU_LogSN;
					break;
				case "MaleName":
					mTagName = TagName.AU_MaleName;
					break;
				case "FemaleName":
					mTagName = TagName.AU_FemaleName;
					break;
				case "DivorceDate":
					mTagName = TagName.AU_DivorceDate;
					break;
				case "ConfirmDate":
					mTagName = TagName.AU_ConfirmDate;
					break;
				case "DivorceSN":
					mTagName = TagName.AU_DivorceSN;
					break;
				case "BeforeDen":
					mTagName = TagName.AU_BeforeDen;
					break;
				case "AfterDen":
					mTagName = TagName.AU_AfterDen;
					break;
				case "CoupleSN":
					mTagName = TagName.AU_CoupleSN;
					break;
				case "CoupleDate":
					mTagName = TagName.AU_CoupleDate;
					break;
				case "KissPoint":
					mTagName = TagName.AU_KissPoint;
					break;
				case "LastKissPointTime":
					mTagName = TagName.AU_LastKissPointTime;
					break;
				case "SeparateDate":
					mTagName = TagName.AU_SeparateDate;
					break;
				case "SeparateSN":
					mTagName = TagName.AU_SeparateSN;
					break;
				case "MaleLevel":
					mTagName = TagName.AU_MaleLevel;
					break;
				case "FemaleLevel":
					mTagName = TagName.AU_FemaleLevel;
					break;
				case "OrderID":
					mTagName = TagName.AuShop_orderid;
					break;
				case "SendSN":
					mTagName = TagName.AU_SendSN;
					break;
				case "SendNick":
					mTagName = TagName.AU_SendNick;
					break;
				case "Period":
					mTagName = TagName.AU_Period;
					break;
				case "Msg":
					mTagName = TagName.AU_Memo;
					break;
				case "SendDate":
					mTagName = TagName.AU_SendDate;
					break;
				case "useTime":
					mTagName = TagName.AU_UseTime;
					break;
				case "NyCashChargeLogIDX":
					mTagName = TagName.RayCity_NyCashChargeLogIDX;
					break;
				case "NyPayID":
					mTagName = TagName.RayCity_NyPayID;
					break;
				case "ChargeAmount":
					mTagName = TagName.RayCity_ChargeAmount;
					break;
				case "ChargeState":
					mTagName = TagName.RayCity_ChargeState;
					break;
				case "GuildPoint":
					mTagName = TagName.RayCity_GuildPoint;
					break;
				case "FromCharacterID":
					mTagName = TagName.RayCity_FromCharacterID;
					break;
				case "FromCharacterName":
					mTagName = TagName.RayCity_FromCharacterName;
					break;
				case "PaymentType":
					mTagName = TagName.RayCity_PaymentType;
					break;
				case "CashItemLogIDX":
					mTagName = TagName.RayCity_CashItemLogIDX;
					break;
				case "StockID":
					mTagName = TagName.RayCity_StockID;
					break;
				case "GiftMessage":
					mTagName = TagName.RayCity_GiftMessage;
					break;
				case "MailSendDate":
					mTagName = TagName.RayCity_SendDate;
					break;
				case "GiftState":
					mTagName = TagName.RayCity_GiftState;
					break;
				case "SendMessageDate":
					mTagName = TagName.AU_SendMessageDate;
					break;
				case "chName":
					mTagName = TagName.RC_CharName;
					break;
				case "Split_ISex":
					mTagName = TagName.RC_Sex;
					break;
				case "Stat_IExperience":
					mTagName = TagName.RC_Exp;
					break;
				case "Proud_ILevel":
					mTagName = TagName.RC_Level;
					break;
				case "Split_IGroup":
					mTagName = TagName.RC_IGroup;
					break;
				case "Split_IForce":
					mTagName = TagName.RC_Split_IForce;
					break;
				case "Proud_IPrefix":
					mTagName = TagName.RC_Rank;
					break;
				case "Race_IRunDistance":
					mTagName = TagName.RC_IRunDistance;
					break;
				case "Race_ILostConnection":
					mTagName = TagName.RC_ILostConnection;
					break;
				case "Race_IPlayCounter":
					mTagName = TagName.RC_PlayCounter;
					break;
				case "Race_IEscapeCounter":
					mTagName = TagName.RC_IEscapeCounter;
					break;
				case "Race_IWinCounter":
					mTagName = TagName.RC_IWinCounter;
					break;
				case "IGameMoney":
					mTagName = TagName.RC_IGameMoney;
					break;
				case "AllOnlineTime":
					mTagName = TagName.RC_AllOnlineTime;
					break;
				case "Sys_timeLastLogout":
					mTagName = TagName.RC_Sys_timeLastLogout;
					break;
				case "Sys_ILastLoginIp":
					mTagName = TagName.RC_Sys_ILastLoginIp;
					break;
				case "ID":
					mTagName = TagName.RC_ID;
					break;
				case "Car_Level":
					mTagName = TagName.RC_Car_Level;
					break;
				case "Pref_DM":
					mTagName = TagName.RC_Pref_DM;
					break;
				case "Add_Nos":
					mTagName = TagName.RC_Add_Nos;
					break;
				case "Pref_TS":
					mTagName = TagName.RC_Pref_TS;
					break;
				case "Pref_OS":
					mTagName = TagName.RC_Pref_OS;
					break;
				case "Pref_TR":
					mTagName = TagName.RC_Pref_TR;
					break;
				case "Pref_AF":
					mTagName = TagName.RC_Pref_AF;
					break;
				case "Pref_AB":
					mTagName = TagName.RC_Pref_AB;
					break;
				case "BodyKit":
					mTagName = TagName.RC_BodyKit;
					break;
				case "BodyKit_Front":
					mTagName = TagName.RC_BodyKit_Front;
					break;
				case "BodyKit_Middle":
					mTagName = TagName.RC_BodyKit_Middle;
					break;
				case "BodyKit_Rear":
					mTagName = TagName.RC_BodyKit_Rear;
					break;
				case "BodyKit_Hood":
					mTagName = TagName.RC_BodyKit_Hood;
					break;
				case "BodyKit_Wing":
					mTagName = TagName.RC_BodyKit_Wing;
					break;
				case "BodyKit_Wheel":
					mTagName = TagName.RC_BodyKit_Wheel;
					break;
				case "Tex_TailLogo":
					mTagName = TagName.RC_Tex_TailLogo;
					break;
				case "Tex_Diffuse":
					mTagName = TagName.RC_Tex_Diffuse;
					break;
				case "Color_Body":
					mTagName = TagName.RC_Color_Body;
					break;
				case "Color_Wheel":
					mTagName = TagName.RC_Color_Wheel;
					break;
				case "Color_Glass":
					mTagName = TagName.RC_Color_Glass;
					break;
				case "Color_Tyre":
					mTagName = TagName.RC_Color_Tyre;
					break;
				case "Login_Account":
					mTagName = TagName.RC_Account;
					break;
				case "UserNick":
					mTagName = TagName.RC_UserNick;
					break;
				case "Sex":
					mTagName = TagName.RC_Sex;
					break;
				case "isAdult":
					mTagName = TagName.RC_isAdult;
					break;
				case "CreatDate":
					mTagName = TagName.RC_CreatDate;
					break;
				case "chNotes":
					mTagName = TagName.RC_chNotes;
					break;
				case "ICurrentVehicleID":
					mTagName = TagName.RC_ICurrentVehicleID;
					break;
				
					
				case "avatar0":
					mTagName = TagName.Rich_avatar0;
					break;
				case "avatar1":
					mTagName = TagName.Rich_avatar1;
					break;
				case "avatar2":
					mTagName = TagName.Rich_avatar2;
					break;
				case "avatar3":
					mTagName = TagName.Rich_avatar3;
					break;
				case "avatar4":
					mTagName = TagName.Rich_avatar4;
					break;
				case "avatar5":
					mTagName = TagName.Rich_avatar5;
					break;
				case "avatar6":
					mTagName = TagName.Rich_avatar6;
					break;
				case "avatar7":
					mTagName = TagName.Rich_avatar7;
					break;
				case "avatar8":
					mTagName = TagName.Rich_avatar8;
					break;
				case "avatar9":
					mTagName = TagName.Rich_avatar9;
					break;
		
				case "avatar10":
					mTagName = TagName.Rich_avatar10;
					break;
				case "avatar11":
					mTagName = TagName.Rich_avatar11;
					break;
				case "avatar12":
					mTagName = TagName.Rich_avatar12;
					break;
				case "avatar13":
					mTagName = TagName.Rich_avatar13;
					break;
				case "avatar14":
					mTagName = TagName.Rich_avatar14;
					break;
				case "avatar15":
					mTagName = TagName.Rich_avatar15;
					break;
				case "avatar16":
					mTagName = TagName.Rich_avatar16;
					break;
				case "avatar17":
					mTagName = TagName.Rich_avatar17;
					break;
				case "avatar18":
					mTagName = TagName.Rich_avatar18;
					break;
				case "avatar19":
					mTagName = TagName.Rich_avatar19;
					break;

				case "avatar20":
					mTagName = TagName.Rich_avatar20;
					break;
				case "avatar21":
					mTagName = TagName.Rich_avatar21;
					break;
				case "avatar22":
					mTagName = TagName.Rich_avatar22;
					break;
				case "avatar23":
					mTagName = TagName.Rich_avatar23;
					break;
				case "avatar24":
					mTagName = TagName.Rich_avatar24;
					break;
				case "avatar25":
					mTagName = TagName.Rich_avatar25;
					break;
				case "avatar26":
					mTagName = TagName.Rich_avatar26;
					break;
				case "avatar27":
					mTagName = TagName.Rich_avatar27;
					break;
				case "avatar28":
					mTagName = TagName.Rich_avatar28;
					break;
				case "avatar29":
					mTagName = TagName.Rich_avatar29;
					break;

				case "avatar30":
					mTagName = TagName.Rich_avatar30;
					break;
				case "avatar31":
					mTagName = TagName.Rich_avatar31;
					break;
				case "av0":
					mTagName = TagName.Rich_ExpireTime0;
					break;
				case "av1":
					mTagName = TagName.Rich_ExpireTime1;
					break;
				case "av2":
					mTagName = TagName.Rich_ExpireTime2;
					break;
				case "av3":
					mTagName = TagName.Rich_ExpireTime3;
					break;
				case "av4":
					mTagName = TagName.Rich_ExpireTime4;
					break;
				case "av5":
					mTagName = TagName.Rich_ExpireTime5;
					break;
				case "av6":
					mTagName = TagName.Rich_ExpireTime6;
					break;
				case "av7":
					mTagName = TagName.Rich_ExpireTime7;
					break;
				case "av8":
					mTagName = TagName.Rich_ExpireTime8;
					break;
				case "av9":
					mTagName = TagName.Rich_ExpireTime9;
					break;
				case "av10":
					mTagName = TagName.Rich_ExpireTime10;
					break;
				case "av11":
					mTagName = TagName.Rich_ExpireTime11;
					break;
				case "av12":
					mTagName = TagName.Rich_ExpireTime12;
					break;
				case "av13":
					mTagName = TagName.Rich_ExpireTime13;
					break;
				case "av14":
					mTagName = TagName.Rich_ExpireTime14;
					break;
				case "av15":
					mTagName = TagName.Rich_ExpireTime15;
					break;
				case "av16":
					mTagName = TagName.Rich_ExpireTime16;
					break;
				case "av17":
					mTagName = TagName.Rich_ExpireTime17;
					break;
				case "av18":
					mTagName = TagName.Rich_ExpireTime18;
					break;
				case "av19":
					mTagName = TagName.Rich_ExpireTime19;
					break;
				case "av20":
					mTagName = TagName.Rich_ExpireTime20;
					break;
				case "av21":
					mTagName = TagName.Rich_ExpireTime21;
					break;
				case "av22":
					mTagName = TagName.Rich_ExpireTime22;
					break;
				case "av23":
					mTagName = TagName.Rich_ExpireTime23;
					break;
				case "av24":
					mTagName = TagName.Rich_ExpireTime24;
					break;
				case "av25":
					mTagName = TagName.Rich_ExpireTime25;
					break;
				case "av26":
					mTagName = TagName.Rich_ExpireTime26;
					break;
				case "av27":
					mTagName = TagName.Rich_ExpireTime27;
					break;
				case "av28":
					mTagName = TagName.Rich_ExpireTime28;
					break;
				case "av29":
					mTagName = TagName.Rich_ExpireTime29;
					break;
				case "av30":
					mTagName = TagName.Rich_ExpireTime30;
					break;
				case "av31":
					mTagName = TagName.Rich_ExpireTime31;
					break;
				case "Time":
					mTagName = TagName.Rich_Time;
					break;
				case "IP":
					mTagName = TagName.Rich_IP;
					break;
				case "LoginLogout":
					mTagName = TagName.Rich_LoginLogout;
					break;
				case "mailTime":
					mTagName = TagName.Rich_mailTime;
					break;
				case "SendID":
					mTagName = TagName.Rich_SendID;
					break;
				case "SendName":
					mTagName = TagName.Rich_SendName;
					break;
				case "RecvID":
					mTagName = TagName.Rich_RecvID;
					break;
				case "RecvName":
					mTagName = TagName.Rich_RecvName;
					break;
				case "IsItem":
					mTagName = TagName.Rich_IsItem;
					break;
				case "richItemID":
					mTagName = TagName.Rich_ItemID;
					break;
				case "ItemValidityTime":
					mTagName = TagName.Rich_ItemValidityTime;
					break;
				case "Account":
					mTagName = TagName.Rich_Account;
					break;
				case "State":
					mTagName = TagName.Rich_State;
					break;
				case "Itemid":
					mTagName = TagName.Rich_Itemid;
					break;
				case "Itemname":
					mTagName = TagName.Rich_Itemname;
					break;
				case "FromRoleName":
					mTagName = TagName.Rich_FromRoleName;
					break;
				case "ToPlayerIndex":
					mTagName = TagName.Rich_ToPlayerIndex;
					break;
				case "ToRoleName":
					mTagName = TagName.Rich_ToRoleName;
					break;
				case "ReadAlready":
					mTagName = TagName.Rich_ReadAlready;
					break;
				case "Date":
					mTagName = TagName.Rich_Date;
					break;
				case "Title":
					mTagName = TagName.Rich_Title;
					break;
				case "Content":
					mTagName = TagName.Rich_Content;
					break;
				case "UserID":
					mTagName = TagName.Rich_UserID;
					break;
				case "Money":
					mTagName = TagName.Rich_Money;
					break;
				case "RichMoneyType":
					mTagName = TagName.Rich_MoneyType;
					break;
				case "RichMoney":
					mTagName = TagName.Rich_RichMoney;
					break;
				case "buytime":
					mTagName = TagName.Rich_buytime;
					break;
				case "gender":
					mTagName = TagName.Rich_Gender;
					break;
				case "op":
					mTagName = TagName.Rich_OP;
					break;
				case "amount":
					mTagName = TagName.Rich_amount;
					break;
				case "balance":
					mTagName = TagName.Rich_balance;
					break;
				case "jppmoney":
					mTagName = TagName.Rich_JPPMoney;
					break;
				case "Magic_ServerIp":
					mTagName = TagName.Magic_Serverip;
					break;
				case "paytime":
					mTagName = TagName.Rich_paytime;
					break;
				case "senduser":
					mTagName = TagName.Rich_SendUser;
					break;
				case "getuser":
					mTagName = TagName.Rich_getuser;
					break;
				case "paysn":
					mTagName = TagName.Rich_PaySN;
					break;
				case "Bean":
					mTagName = TagName.Rich_Bean;
					break;
				case "activetime":
					mTagName = TagName.Rich_activetime;
					break;
				case "PropID":
					mTagName = TagName.Rich_Propname;
					break;
				case "ActiveID":
					mTagName = TagName.Rich_Activename;
					break;


				case "Type":
					mTagName = TagName.CS_Type;
					break;
				case "chAccount":
					mTagName = TagName.RC_chAccount;
					break;

				case "ItemType":
					mTagName = TagName.RC_ItemType;
					break;

				case "BuyTime":
					mTagName = TagName.RC_BuyTime;
					break;
				case "车辆等级":
					mTagName = TagName.车辆等级;
					break;
//				case "车辆损伤情况":
//					mTagName = TagName.车辆损伤情况;
					break;
//				case "液氮能力等级":
//					mTagName = TagName.液氮能力等级;
//					break;
//				case "刹车":
//					mTagName = TagName.刹车;
//					break;
//				case "引擎":  
//					mTagName = TagName.引擎;
//					break;
//				case "变速箱":
//					mTagName = TagName.变速箱;
//					break;
//				case "车架":
//					mTagName = TagName.车架;
//					break;
//				case "车身":
//					mTagName = TagName.车身;
//					break;
//				case "大包围":
//					mTagName = TagName.大包围;
//					break;
//				case "引擎盖":
//					mTagName = TagName.引擎盖;
//					break;
//				case "尾翼":
//					mTagName = TagName.尾翼;
//					break;
//				case "轮圈":
//					mTagName = TagName.轮圈;
//					break;
//				case "牌照":
//					mTagName = TagName.牌照;
//					break;
				case "喷绘":
					mTagName = TagName.喷绘;
					break;
//				case "轮圈漆":
//					mTagName = TagName.轮圈漆;
//					break;
				case "车窗贴":
					mTagName = TagName.车窗贴;
					break;
				case "轮胎色":
					mTagName = TagName.轮胎色;
					break;
				case "液氮槽1":
					mTagName = TagName.液氮槽1;
					break;
				case "液氮槽2":
					mTagName = TagName.液氮槽2;
					break;
				case "液氮槽3":
					mTagName = TagName.液氮槽3;
					break;
				case "液氮槽4":
					mTagName = TagName.液氮槽4;
					break;
				case "液氮槽5":
					mTagName = TagName.液氮槽5;
					break;
				case "原始车辆":
					mTagName = TagName.原始车辆;
					break;
				case "轮胎":
					mTagName = TagName.轮胎;
					break;
//				case "车身漆":
//					mTagName = TagName.车身漆;
					break;
				case "FromID":
					mTagName = TagName.CS_FromID;
					break;
				case "ToID":
					mTagName = TagName.CS_ToID;
					break;
				case "SendTime":
					mTagName = TagName.CS_SendTime;
					break;
				case "ItemLogID":
					mTagName = TagName.CS_ItemLogID;
					break;
				case "FromName":
					mTagName = TagName.CS_FromName;
					break;
				case "Message":
					mTagName = TagName.CS_Message;
					break;
				case "ItemInstanceID":
					mTagName = TagName.RC_ItemInstanceID;
					break;
				case "SenderID":
					mTagName = TagName.RC_SenderID;
					break;
				case "SenderOperation":
					mTagName = TagName.RC_SenderOperation;
					break;
				case "ReceiverID":
					mTagName = TagName.RC_ReceiverID;
					break;
				case "ReceiveTime":
					mTagName = TagName.RC_ReceiveTime;
					break;
				case "ReceiverOperation":
					mTagName = TagName.RC_ReceiverOperation;
					break;
				case "PlayerID":
					mTagName = TagName.RC_Account;
					break;
				case "PlayerOperation":
					mTagName = TagName.RC_PlayerOperation;
					break;
				case "OperationTime":
					mTagName = TagName.RC_OperationTime;
					break;
				case "MoneyCount":
					mTagName = TagName.RC_MoneyCount;
					break;
				case "login_account":
					mTagName = TagName.RC_login_account;
					break;
				case "state_number":
					mTagName = TagName.RC_state_number;
					break;
				case "usernick":
					mTagName = TagName.RC_usernick;
					break;
				case "TimeLoop":
					mTagName = TagName.RC_TimeLoop;
					break;
				case "StartTime":
					mTagName = TagName.RC_BeginDate;
					break;
				case "EndTime":
					mTagName = TagName.RC_EndDate;
					break;
				case "Text":
					mTagName = TagName.RC_Text;
					break;
				case "ChannelServerID":
					mTagName = TagName.RC_ChannelServerID;
					break;
				case "MultipleType":
					mTagName = TagName.RC_MultipleType;
					break;
				case "MultipleValue":
					mTagName = TagName.RC_MultipleValue;
					break;
				case "DeleteFlag":
					mTagName = TagName.RC_DeleteFlag;
					break;
				case "mfid":
					mTagName = TagName.MF_mfid;
					break;
				case "username":
					mTagName = TagName.MF_username;
					break;
				case "nickname":
					mTagName = TagName.MF_nickname;
					break;
				case "regist_date":
					mTagName = TagName.MF_regist_date;
					break;
				case "is_token":
					mTagName = TagName.MF_is_token;
					break;
				case "g_cash":
					mTagName = TagName.MF_g_cash;
					break;
				case "m_cash":
					mTagName = TagName.MF_m_cash;
					break;
				case "9youmoney":
					mTagName = TagName.MF_9youmoney;
					break;
				case "graname":
					mTagName = TagName.MF_graname;
					break;

				case "gradation_level":
					mTagName = TagName.MF_gradation_level;
					break;
				case "levelname":
					mTagName = TagName.MF_levelname;
					break;
				case "remark":
					mTagName = TagName.MF_remark;
					break;
				case "owner_id":
					mTagName = TagName.MF_owner_id;
					break;
				case "max_count":
					mTagName = TagName.MF_max_count;
					break;
				case "position":
					mTagName = TagName.MF_position;
					break;
				case "type_id":
					mTagName = TagName.MF_type_id;
					break;
				case "mfitemname":
					mTagName = TagName.MF_mfitemname;
					break;
				case "times":
					mTagName = TagName.MF_times;
					break;
				case "binded":
					mTagName = TagName.MF_binded;
					break;
				case "deadline":
					mTagName = TagName.MF_deadline;
					break;
				case "petid":
					mTagName = TagName.MF_petid;
					break;
				case "petname":
					mTagName = TagName.MF_petname;
					break;
				case "card_group":
					mTagName = TagName.MF_card_group;
					break;
				case "activity_status":
					mTagName = TagName.MF_activity_status;
					break;
				case "father":
					mTagName = TagName.MF_father;
					break;
				case "mother":
					mTagName = TagName.MF_mother;
					break;
				case "birthday":
					mTagName = TagName.MF_birthday;
					break;
				case "generation":
					mTagName = TagName.MF_generation;
					break;
				case "petlevel":
					mTagName = TagName.MF_petlevel;
					break;
				case "remaining_attribute":
					mTagName = TagName.MF_remaining_attribute;
					break;
				case "state_updatetime":
					mTagName = TagName.MF_state_updatetime;
					break;
				case "fatigue":
					mTagName = TagName.MF_fatigue;
					break;
				case "growth":
					mTagName = TagName.MF_growth;
					break;
				case "procreation_count":
					mTagName = TagName.MF_procreation_count;
					break;
				case "forbid_procreate":
					mTagName = TagName.MF_forbid_procreate;
					break;
				case "character_id":
					mTagName = TagName.MF_character_id;
					break;
				case "mfip":
					mTagName = TagName.MF_mfip;
					break;
				case "login":
					mTagName = TagName.MF_login;
					break;
				case "logout":
					mTagName = TagName.MF_logout;
					break;
				case "index":
					mTagName = TagName.MF_index;
					break;
				case "card1":
					mTagName = TagName.MF_card1;
					break;
				case "card2":
					mTagName = TagName.MF_card2;
					break;
				case "card3":
					mTagName = TagName.MF_card3;
					break;
				case "card4":
					mTagName = TagName.MF_card4;
					break;
				case "card5":
					mTagName = TagName.MF_card5;
					break;
				case "card6":
					mTagName = TagName.MF_card6;
					break;
				case "card7":
					mTagName = TagName.MF_card7;
					break;
				case "card8":
					mTagName = TagName.MF_card8;
					break;
				case "mfpassword":
					mTagName = TagName.MF_mfpassword;
					break;
				case "gene_pos":
					mTagName = TagName.MF_gene_pos;
					break;
				case "gene_left":
					mTagName = TagName.MF_gene_left;
					break;
				case "gene_right":
					mTagName = TagName.MF_gene_right;
					break;
				case "mftype":
					mTagName = TagName.MF_mftype;
					break;
				case "lock_time":
					mTagName = TagName.MF_lock_time;
					break;
				case "unlock_time":
					mTagName = TagName.MF_unlock_time;
					break;
				case "mfreason":
					mTagName = TagName.MF_mfreason;
					break;
				case "CouponIDX":
					mTagName = TagName.RayCity_CouponIDX;
					break;
				case "CountryCode":
					mTagName = TagName.RayCity_CountryCode;
					break;
				case "CouponName":
					mTagName = TagName.RayCity_CouponName;
					break;
				case "IssueCount":
					mTagName = TagName.RayCity_IssueCount;
					break;
				case "CouponState":
					mTagName = TagName.RayCity_CouponState;
					break;
				case "StartDate":
					mTagName = TagName.RayCity_StartDate;
					break;
				case "EndDate":
					mTagName = TagName.RayCity_EndDate;
					break;
				case "cfid":
					mTagName = TagName.MF_cfid;
					break;
				case "cfname":
					mTagName = TagName.MF_cfname;
					break;
				case "leader":
					mTagName = TagName.MF_leader;
					break;
				case "information":
					mTagName = TagName.MF_information;
					break;
				case "cflevel":
					mTagName = TagName.MF_cflevel;
					break;
				case "construct":
					mTagName = TagName.MF_construct;
					break;
				case "fund":
					mTagName = TagName.MF_fund;
					break;
				case "ninki":
					mTagName = TagName.MF_ninki;
					break;
				case "research":
					mTagName = TagName.MF_research;
					break;
				case "grid":
					mTagName = TagName.MF_grid;
					break;
				case "grname":
					mTagName = TagName.MF_grname;
					break;
				case "description":
					mTagName = TagName.MF_description;
					break;
				case "dishware":
					mTagName = TagName.MF_dishware;
					break;
				case "main_work":
					mTagName = TagName.MF_main_work;
					break;
				case "nation_id":
					mTagName = TagName.MF_nation_id;
					break;
				case "prestige":
					mTagName = TagName.MF_prestige;
					break;
				case "plant_type":
					mTagName = TagName.MF_plant_type;
					break;
				case "water":
					mTagName = TagName.MF_water;
					break;
				case "granule":
					mTagName = TagName.MF_granule;
					break;
				case "byproduct_granule":
					mTagName = TagName.MF_byproduct_granule;
					break;
				case "plstate":
					mTagName = TagName.MF_plstate;
					break;
				case "update_time":
					mTagName = TagName.MF_update_time;
					break;
				case "planting_time":
					mTagName = TagName.MF_planting_time;
					break;
				case "friendid":
					mTagName = TagName.MF_friendid;
					break;
				case "friendname":
					mTagName = TagName.MF_friendname;
					break;
				case "mf_senduser":
					mTagName = TagName.MF_senduser;
					break;
				case "mf_getuser":
					mTagName = TagName.MF_getuser;
					break;
				case "mf_paymoney":
					mTagName = TagName.MF_paymoney;
					break;
				case "mf_paysn":
					mTagName = TagName.MF_paySN;
					break;
				case "mf_paytime":
					mTagName = TagName.MF_paytime;
					break;
				case "playerfrom2":
					mTagName = TagName.MF_playerfrom2;
					break;
				case "ActiveTime":
					mTagName = TagName.MF_ActiveTime;
					break;
				case "char_id":
					mTagName = TagName.MF_char_id;
					break;
				case "richendtime":
					mTagName = TagName.Rich_richendtime;
					break;
				case "richchannelid":
					mTagName = TagName.Rich_richchannelid;
					break;
				case "richnums":
					mTagName = TagName.Rich_richnums;
					break;
				case "Pawn":
					mTagName = TagName.Rich_Pawn;
					break;
				case "richMinute":
					mTagName = TagName.Rich_richMinute;
					break;
				case "Id1":
					mTagName = TagName.Rich_Id1;
					break;
				case "Name1":
					mTagName = TagName.Rich_Name1;
					break;
				case "Bean1":
					mTagName = TagName.Rich_Bean1;
					break;
				case "Prize1":
					mTagName = TagName.Rich_Prize1;
					break;
				case "Exp1":
					mTagName = TagName.Rich_Exp1;
					break;
				case "Id2":
					mTagName = TagName.Rich_Id2;
					break;
				case "Name2":
					mTagName = TagName.Rich_Name2;
					break;
				case "Bean2":
					mTagName = TagName.Rich_Bean2;
					break;
				case "Prize2":
					mTagName = TagName.Rich_Prize2;
					break;
				case "Exp2":
					mTagName = TagName.Rich_Exp2;
					break;
				case "Id3":
					mTagName = TagName.Rich_Id3;
					break;
				case "Name3":
					mTagName = TagName.Rich_Name3;
					break;
				case "Bean3":
					mTagName = TagName.Rich_Bean3;
					break;
				case "Prize3":
					mTagName = TagName.Rich_Prize3;
					break;
				case "Exp3":
					mTagName = TagName.Rich_Exp3;
					break;
				case "Id4":
					mTagName = TagName.Rich_Id4;
					break;
				case "Name4":
					mTagName = TagName.Rich_Name4;
					break;
				case "Bean4":
					mTagName = TagName.Rich_Bean4;
					break;
				case "Prize4":
					mTagName = TagName.Rich_Prize4;
					break;
				case "Exp4":
					mTagName = TagName.Rich_Exp4;
					break;
				case "LotteryID":
					mTagName = TagName.Rich_LotteryID;
					break;
				case "mailid":
					mTagName = TagName.MF_mailid;
					break;
				case "charge":
					mTagName = TagName.MF_charge;
					break;
				case "send_back":
					mTagName = TagName.MF_send_back;
					break;
				case "quest_id":
					mTagName = TagName.MF_quest_id;
					break;
				case "finished":
					mTagName = TagName.MF_finished;
					break;
				case "questname":
					mTagName = TagName.MF_questname;
					break;
				case "correspond_id":
					mTagName = TagName.MF_correspond_id;
					break;
				case "upper_limit":
					mTagName = TagName.MF_upper_limit;
					break;
				case "open":
					mTagName = TagName.MF_open;
					break;
				case "rslevel":
					mTagName = TagName.MF_rslevel;
					break;
				case "cardname1":
					mTagName = TagName.MF_cardname1;
					break;
				case "cardname2":
					mTagName = TagName.MF_cardname2;
					break;
				case "cardname3":
					mTagName = TagName.MF_cardname3;
					break;
				case "cardname4":
					mTagName = TagName.MF_cardname4;
					break;
				case "cardname5":
					mTagName = TagName.MF_cardname5;
					break;
				case "cardname6":
					mTagName = TagName.MF_cardname6;
					break;
				case "cardname7":
					mTagName = TagName.MF_cardname7;
					break;
				case "cardname8":
					mTagName = TagName.MF_cardname8;
					break;
				case "max_pile":
					mTagName = TagName.MF_max_pile;
					break;
				case "free_time":
					mTagName = TagName.MF_free_time;
					break;
				case "count":
					mTagName = TagName.MF_count;
					break;
				case "phase":
					mTagName = TagName.MF_phase;
					break;
				case "is_adult":
					mTagName = TagName.MF_is_adult;
					break;
				case "defense":
					mTagName = TagName.MF_defense;
					break;
				case "attack":
					mTagName = TagName.MF_attack;
					break;
				case "Newstext":
					mTagName = TagName.MF_Newstext;
					break;
				case "Newstime":
					mTagName = TagName.MF_Newstime;
					break;
				case "buydate":
					mTagName = TagName.AU_BuyDate;
					break;
				case "beforebank":
					mTagName = TagName.AU_beforebank;
					break;
				case "afterbank":
					mTagName = TagName.AU_afterbank;
					break;
				case "itemgrade":
					mTagName = TagName.AU_itemgrade;
					break;					
				case "priceden":
					mTagName = TagName.AU_priceden;
					break;						
				case "pricecash":
					mTagName = TagName.AU_pricecash;
					break;						
				case "famnew":
					mTagName = TagName.AU_famnew;
					break;		
				case "discount":
					mTagName = TagName.AU_discount;
					break;	
				case "famid":
					mTagName = TagName.AU_famid;
					break;	
				case"server_name":
					mTagName = TagName.FJ_Server_Name;
					break;
				case"actor_name":
					mTagName = TagName.FJ_UseAccount;
					break;
				case"log_type":
					mTagName = TagName.FJ_Type;
					break;
				case"act_time":
					mTagName = TagName.ACT_Time;
					break;
				case"item_name":
					mTagName = TagName.FJ_ItemName;
					break;
				case "Receiver":
					mTagName = TagName.FJ_Receiver;
					break;
				case "City":
					mTagName = TagName.ServerInfo_City;
					break;
				case "ChargeID":
					mTagName = TagName.FJ_ChargeID;
					break;
				case "deductMoney":
					mTagName = TagName.FJ_deductMoney;
					break;
				case "deductDate":
					mTagName = TagName.FJ_deductDate;
					break;
				case "cardname":
					mTagName = TagName.MF_cardname;
					break;
				case "cardlevel":
					mTagName = TagName.MF_cardlevel;
					break;
				case "genelevel":
					mTagName = TagName.MF_genelevel;
					break;

				case "logid":
					mTagName = TagName.MF_logid;
					break;
				case "owner_type":
					mTagName = TagName.MF_owner_type;
					break;
				case "record_time":
					mTagName = TagName.MF_record_time;
					break;
				case "changetype":
					mTagName = TagName.MF_changetype;
					break;
				case "cash_type":
					mTagName = TagName.MF_cash_type;
					break;
				case "is_increase":
					mTagName = TagName.MF_is_increase;
					break;
				case "number":
					mTagName = TagName.MF_number;
					break;
				case "cash_to":
					mTagName = TagName.MF_cash_to;
					break;
				case "item_number":
					mTagName = TagName.MF_item_number;
					break;
				case "playerfrom":
					mTagName = TagName.MF_playerfrom;
					break;
				case "playerto":
					mTagName = TagName.MF_playerto;
					break;
				case "change_number":
					mTagName = TagName.MF_change_number;
					break;
				case "change_from":
					mTagName = TagName.MF_change_from;
					break;
				case "cash_id":
					mTagName = TagName.MF_cash_id;
					break;
				case "item_type":
					mTagName = TagName.MF_item_type;
					break;
				case "packetid":
					mTagName = TagName.MF_packetid;
					break;
				case "inserttype":
					mTagName = TagName.MF_inserttype;
					break;
				case "reasontype":
					mTagName = TagName.MF_reasontype;
					break;
				case "change_num":
					mTagName = TagName.MF_change_num;
					break;
				case "vip":
					mTagName = TagName.MF_MemberVip;
					break;
				case "species":
					mTagName = TagName.MF_species;
					break;
				case "change_num1":
					mTagName = TagName.MF_change_num1;
					break;
				case "playerto1":
					mTagName = TagName.MF_playerto1;
					break;
				case "playerto2":
					mTagName = TagName.MF_playerto2;
					break;
				case "LogType":
					mTagName = TagName.MF_LogType;
					break;
				case "key":
					mTagName = TagName.MF_key;
					break;
				case "activetype":
					mTagName = TagName.MF_activetype;
					break;
				case "used":
					mTagName = TagName.MF_used;
					break;
				case "exp_change_number":
					mTagName = TagName.MF_exp_change_number;
					break;
				case "exp_result_number":
					mTagName = TagName.MF_exp_result_number;
					break;
				case "itemchange_result_number":
					mTagName = TagName.MF_itemchange_result_number;
					break;
				case "gcash_change_result_number":
					mTagName = TagName.MF_gcash_change_result_number;
					break;
				case "cc_result_number":
					mTagName = TagName.MF_cc_result_number;
					break;
				case "ichange_from":
					mTagName = TagName.MF_ichange_from;
					break;
				case "gchange_from":
					mTagName = TagName.MF_gchange_from;
					break;
				case "msgtype":
					mTagName = TagName.MF_msgtype;
					break;
				case "context":
					mTagName = TagName.MF_context;
					break;
				case "taskid":
					mTagName = TagName.SD_ID;
					break;
				case "sendBeginTime":
					mTagName = TagName.SD_StartTime;
					break;
				case "sendEndTime":
					mTagName = TagName.SD_EndTime;
					break;
			    case "Interval":
					mTagName = TagName.SD_Limit;
					break;
				case "status":
					mTagName = TagName.SD_Status;
					break;
				case "boardmessage":
					mTagName = TagName.SD_Content;
					break;
				case "zone":
					mTagName = TagName.MF_zone;
					break;
				case "key_code":
					mTagName = TagName.MF_key_code;
					break;
				case "mftitle":
					mTagName = TagName.MF_mftitle;
					break;
				case "mftext":
					mTagName = TagName.MF_mftext;
					break;
				case "mfupdate_time":
					mTagName = TagName.MF_mfupdate_time;
					break;
				case "CurrentPartnerID":
					mTagName = TagName.RC_CurrentPartnerID;
					break;
				case "CurrentTitleID":
					mTagName = TagName.RC_CurrentTitleID;
					break;
				case "I9youMoney":
					mTagName = TagName.RC_I9youMoney;
					break;
				case "Activetype":
					mTagName = TagName.RC_Activetype;
					break;
				case "loginip":
					mTagName = TagName.RC_loginip;
					break;
				case "logintime":
					mTagName = TagName.RC_logintime;
					break;
				case "PLAYER_ID":
					mTagName = TagName.RC_Account;
					break;
				case "VEHICLE_ID":
					mTagName = TagName.VEHICLE_ID;
					break;
				case "TRACK_ID":
					mTagName = TagName.TRACK_ID;
					break;
				case "COMPLETE_USING_TIME":
					mTagName = TagName.COMPLETE_USING_TIME;
					break;
				case "ADDITION_SCOER1":
					mTagName = TagName.ADDITION_SCOER1;
					break;
				case "ADDITION_SCOER2":
					mTagName = TagName.ADDITION_SCOER2;
					break;
				case "HAPPEND_TIME":
					mTagName = TagName.HAPPEND_TIME;
					break;
				case "RACE_TYPE_FLAG":
					mTagName = TagName.RACE_TYPE_FLAG;
					break;
				case "RECORD_TYPE":
					mTagName = TagName.RECORD_TYPE;
					break;
				case "PetGeneid":
					mTagName = TagName.MF_PetGeneid;
					break;
				case "PetGeneName":
					mTagName = TagName.MF_PetGeneName;
					break;
				case "ItemBigType":
					mTagName = TagName.RC_ItemBigType;
					break;
				case "OperationType":
					mTagName = TagName.RC_OperationType;
					break;
					
					



			}
			return mTagName;
		}
		public static TagFormat getTagFormat(string strFieldType)
		{
			TagFormat mFormat = TagFormat.TLV_STRING;

			switch (strFieldType)
			{
				case "Int16":
				case "UInt16":
				case "Int32":
				case "UInt32":
				case "Int64":
				case "UInt64":
				case "SByte":
				case "Byte":
				case "int":
					mFormat = TagFormat.TLV_INTEGER;
					break;
				case "String":
					mFormat = TagFormat.TLV_STRING;
					break;
				case "Decimal":
				case "Double":
				case "Single":
					mFormat = TagFormat.TLV_MONEY;
					break;
				case "Byte[]":
					mFormat = TagFormat.TLV_EXTEND;
					break;
				case "Boolean":
				case "boolean":
					mFormat = TagFormat.TLV_BOOLEAN;
					break;
				case "DateTime":
					mFormat = TagFormat.TLV_TIMESTAMP;
					break;
				case "Date":
					mFormat = TagFormat.TLV_DATE;
					break;
				case "TimeSpan":
					mFormat=TagFormat.TLV_TIME;
					break;

			}

			return mFormat;
		}
        
	}

}
