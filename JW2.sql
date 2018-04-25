
/****** 对象:  Table [dbo].[GMTools_Log_UpdateAgo]    脚本日期: 02/19/2009 19:13:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GMTools_Log_UpdateAgo](
	[LOG_ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[SP_Name] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ServerIP] [varchar](30) COLLATE Chinese_PRC_CI_AS NULL,
	[Real_Act] [varchar](8000) COLLATE Chinese_PRC_CI_AS NULL,
	[Act_Time] [datetime] NOT NULL CONSTRAINT [DF_GMTools_Log_UpdateAgo_Act_Time]  DEFAULT (getdate())
 
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF


/****** 对象:  Table [dbo].[GMTools_LogTime]    脚本日期: 02/19/2009 19:14:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GMTools_LogTime](
	[LOG_ID] [int] IDENTITY(1,1) NOT NULL,
	[OperateUserID] [int] NOT NULL,
	[OperateMsg] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[LogTime] [datetime] NOT NULL CONSTRAINT [DF_GMTools_LogTime_LogTime]  DEFAULT (getdate()))
  ON [PRIMARY]

GO
SET ANSI_PADDING OFF




/****** 对象:  Table [dbo].[GMTools_Users]    脚本日期: 02/19/2009 15:11:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GMTools_Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[User_Pwd] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[User_MAC] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[ReaLName] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[DepartID] [int] NOT NULL,
	[User_status] [int] NOT NULL,
	[limit] [smalldatetime] NOT NULL,
	[onlineActive] [int] NOT NULL CONSTRAINT [DF_GMTools_Users_onlineActive]  DEFAULT (0),
	[sysAdmin] [tinyint] NULL CONSTRAINT [DF_GMTools_Users_sysAdmin]  DEFAULT (0),
	[userIP] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL
  

) ON [PRIMARY]

GO
SET ANSI_PADDING OFF



GO
/****** 对象:  Table [dbo].[GAMELIST]    脚本日期: 02/19/2009 15:25:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GAMELIST](
	[Game_ID] [int] IDENTITY(1,1) NOT NULL,
	[Game_Name] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[Game_Content] [varchar](400) COLLATE Chinese_PRC_CI_AS NOT NULL
 

) ON [PRIMARY]

GO
SET ANSI_PADDING OFF




GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartID] [int] IDENTITY(1,1) NOT NULL,
	[DepartName] [varchar](50) NULL,
	[Remark] [varchar](200) NULL
) ON [PRIMARY]






GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GMTools_Serverinfo](
	[idx] [int] IDENTITY(1,1) NOT NULL,
	[serverip] [varchar](30) NOT NULL,
	[city] [varchar](50) NOT NULL,
	[gameID] [int] NOT NULL,
	[gamename] [varchar](50) NOT NULL,
	[gameflag] [int] NOT NULL,
	[gamedbID] [int] NOT NULL,
	[descinfo] [varchar](200) NULL,
	[createby] [varchar](20) NULL,
	[createtime] [datetime] NOT NULL CONSTRAINT [DF_GMTools_Serverinfo_createtime]  DEFAULT (getdate())
) ON [PRIMARY]





GO
/****** 对象:  Table [dbo].[GMTools_Modules]    脚本日期: 02/19/2009 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GMTools_Modules](
	[Module_ID] [int] IDENTITY(1,1) NOT NULL,
	[Game_ID] [int] NOT NULL,
	[Module_Name] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[Module_Class] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[Module_Content] [varchar](500) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[status] [int] NOT NULL CONSTRAINT [DF_GMTools_Modules_status]  DEFAULT ((1))


) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

GO
/****** 对象:  Table [dbo].[Mf_Petlevel]    脚本日期: 02/19/2009 15:13:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mf_Petlevel](
	[petlevel] [int] NOT NULL,
	[experience] [int] NULL
) ON [PRIMARY]



GO
/****** 对象:  Table [dbo].[Mf_PetGene]    脚本日期: 02/19/2009 15:13:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Mf_PetGene](
	[Itemid] [int] NOT NULL,
	[Itemname] [varchar](255) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



/*********************************************************************************/
/*	SP NAME : Gmtool_Gminfo_Add			 */
/*	MOD DATE: 2005-12-28							 */
/*	EDITOR  : lxf							 */
/*-------------------------------------------------------------------------------*/
/*	DESC SP : ALTER  new users			 */
/*-------------------------------------------------------------------------------*/
/*	INPUT   : Not describe							 */
/*	RETURN  : Not describe							 */
/*-------------------------------------------------------------------------------*/
/*	MEMO    : 								 */
/*********************************************************************************/
CREATE              PROC [dbo].[Gmtool_Gminfo_Add]
@Gm_OperateUserID int,
@Gm_UserName		VARCHAR(50)
,@Gm_Password		VARCHAR(50)
,@Gm_DepartID int
,@Gm_RealName VARCHAR(50)
,@Gm_LimitTime		datetime	
,@Gm_Status int,
@Gm_SysAdmin int
--@ERRNO			INT	OUTPUT
WITH ENCRYPTION AS
declare  @ERRNO		INT 
declare @UserID INT
set NoCount ON
	
	BEGIN TRAN
	INSERT INTO GMTools_Users				--写入用户表信息
		(UserName, User_Pwd,RealName,DepartID,User_status,limit,SysAdmin)
	VALUES
		(@Gm_UserName, @Gm_Password,@Gm_RealName,@Gm_DepartID,@Gm_Status,@Gm_LimitTime,@Gm_SysAdmin)

        select @UserID = UserID from GMTools_Users where userName=@Gm_UserName
        
	IF (@@ERROR <> 0)
	BEGIN
		ROLLBACK TRAN

	INSERT INTO GMTools_LogTime			--写log表 
		(OperateUserID,OperateMsg)
	VALUES
		(@Gm_OperateUserID,'添加用户名'+@Gm_UserName+'记录失败')
		SELECT @ERRNO = 0
	END
	ELSE
	BEGIN

	COMMIT TRAN

	INSERT INTO GMTools_LogTime			--写log表 
		(OperateUserID,OperateMsg)
	VALUES
		(@Gm_OperateUserID,'添加用户名'+@Gm_UserName+'记录成功')
		SELECT @ERRNO = 1
	END
RETURN @ERRNO
















GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







/*********************************************************************************/
/*	SP NAME : Gmtool_Gminfo_Del			 */
/*	MOD DATE: 2005-12-28							 */
/*	EDITOR  : lxf							 */
/*-------------------------------------------------------------------------------*/
/*	DESC SP : delete user		 */
/*-------------------------------------------------------------------------------*/
/*	INPUT   : Not describe							 */
/*	RETURN  : Not describe							 */
/*-------------------------------------------------------------------------------*/
/*	MEMO    : 								 */
/*********************************************************************************/
CREATE            PROC [dbo].[Gmtool_Gminfo_Del]

@Gm_UserID		INT
,@Gm_OperateUserID	INT
--,@ERRNO			INT	OUTPUT
WITH ENCRYPTION AS
declare  @ERRNO		INT
declare  @UserID	int
declare	 @UserName	varchar(50)
declare  @User_Pwd	varchar(50)
declare  @User_MAC	varchar(50)
declare  @RealName	varchar(50)
declare  @DepartID	int
declare  @User_status	int
declare  @limit		smalldatetime
declare  @onlineActive  int
set NoCount ON
	
	BEGIN TRAN
	select @UserID=UserID from GMTools_Users where UserID = @Gm_UserID
	select @UserName=UserName from GMTools_Users where UserID = @Gm_UserID
	select @User_Pwd=User_Pwd from GMTools_Users where UserID = @Gm_UserID
	select @User_MAC=User_MAC from GMTools_Users where UserID = @Gm_UserID
	select @RealName=RealName from GMTools_Users where UserID =@Gm_UserID
	select @DepartID=DepartID from GMTools_Users where UserID = @Gm_UserID
	select @User_status=User_status from GMTools_Users where UserID = @Gm_UserID
	select @limit=limit from GMTools_Users where UserID = @Gm_UserID
	select @onlineActive=onlineActive from GMTools_Users where UserID = @Gm_UserID
	print @UserName
	--insert into GMTools_Log_UpdateAgo(UserID,SP_Name,Real_Act) values (@Gm_OperateUserID,'Gmtool_GAME_Edit','UserID_'+convert(varchar(10),@UserID)+'__UserName_'+@UserName+'__User_MAC_'+@User_MAC+'__RealName_'+@RealName+'__User_Pwd_'+@User_Pwd+'__DepartID_'+convert(varchar(10),@DepartID)+'__User_status_'+convert(varchar(10),@User_status)+'__limit_'+convert(varchar(30),@limit)+'__onlineActive_'+convert(varchar(10),@onlineActive)+'备份成功')	
	insert into GMTools_Log_UpdateAgo(UserID,SP_Name,Real_Act) values (@Gm_OperateUserID,'Gmtool_Gminfo_Del','UserID_'+convert(varchar(10),@UserID)+'__UserName_'+@UserName+'__User_MAC_'+@User_MAC+'__RealName_'+@RealName+'__User_Pwd_'+@User_Pwd+'__DepartID_'+convert(varchar(10),@DepartID)+'__User_status_'+convert(varchar(10),@User_status)+'__limit_'+convert(varchar(30),@limit)+'__onlineActive_'+convert(varchar(10),@onlineActive)+'备份成功')	
	delete from GMTools_Users				--删除用户表信息
	where	UserID = @Gm_UserID
		
	IF (@@ROWCOUNT = 0)
	BEGIN
		ROLLBACK TRAN
	insert into  GMTools_LogTime (OperateUserID,OperateMsg) values (@Gm_OperateUserID,'删除用户ID'+Convert(varchar(5),@Gm_UserID)+'记录失败')	--记录log表 
		SELECT @ERRNO = 0
	END
	ELSE
	BEGIN
		COMMIT TRAN
	insert into  GMTools_LogTime (OperateUserID,OperateMsg) values (@Gm_OperateUserID,'删除用户ID'+Convert(varchar(5),@Gm_UserID)+'记录成功')	--记录log表 
		SELECT @ERRNO = 1
	END
RETURN @ERRNO


--exec Gmtool_Gminfo_Del 21,15










GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GMTools_O2JAM2_AccountTemp](
	[idx] [int] IDENTITY(1,1) NOT NULL,
	[GM_UserID] [int] NULL,
	[USER_ID] [varchar](50) NOT NULL,
	[Reason] [varchar](800) NULL,
	[Content] [varchar](50) NULL,
	[Ban_Date] [datetime] NULL,
	[ServerIP] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GMTOOLS_UpdateList](
	[Update_ID] [int] IDENTITY(1,1) NOT NULL,
	[Update_Module] [varchar](50) NULL,
	[Update_Context] [varchar](500) NULL,
	[Update_Date] [datetime] NULL CONSTRAINT [DF_GMTOOLS_UpdateList_Update_Date]  DEFAULT (getdate()),
	[Update_Status] [bit] NULL CONSTRAINT [DF_GMTOOLS_UpdateList_Update_Status]  DEFAULT (0)
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO















/*********************************************************************************/
/*	SP NAME : Gmtool_Gminfo_Edit			 */
/*	MOD DATE: 2005-12-28							 */
/*	EDITOR  : lxf							 */
/*-------------------------------------------------------------------------------*/
/*	DESC SP : edit user info		 */
/*-------------------------------------------------------------------------------*/
/*	INPUT   : Not describe							 */
/*	RETURN  : Not describe							 */
/*-------------------------------------------------------------------------------*/
/*	MEMO    : 								 */
/*********************************************************************************/
CREATE              PROC [dbo].[Gmtool_Gminfo_Edit]
@Gm_OperateUserID int,
@Gm_UserID		int
,@Gm_RealName varchar(100)
,@Gm_DeptID int
,@Gm_LimitTime		smalldatetime
,@Gm_Status int,
@Gm_OnlineActive int,
@Gm_SysAdmin int
--,@ERRNO			INT	OUTPUT
WITH ENCRYPTION AS
declare  @ERRNO		INT 
declare  @realName	varchar(50)
declare  @DepartID	int
declare  @limit		smalldatetime
declare  @User_status	int
declare  @OnlineActive  int
set NoCount ON
	
	BEGIN TRAN

	select @RealName=RealName from GMTools_Users where UserID =@Gm_UserID
	select @DepartID=DepartID from GMTools_Users where UserID = @Gm_UserID
	select @User_status=User_status from GMTools_Users where UserID = @Gm_UserID
	select @limit=limit from GMTools_Users where UserID = @Gm_UserID
	select @onlineActive=onlineActive from GMTools_Users where UserID = @Gm_UserID
	insert into GMTools_Log_UpdateAgo(UserID,SP_Name,Real_Act) values (@Gm_OperateUserID,'Gmtool_Gminfo_Edit','__RealName_'+@RealName+'__DepartID_'+convert(varchar(10),@DepartID)+'__User_status_'+convert(varchar(10),@User_status)+'__limit_'+convert(varchar(30),@limit)+'__onlineActive_'+convert(varchar(10),@onlineActive)+'备份成功')	

	update GMTools_Users					--修改用户表信息
		set realName=@Gm_RealName,DepartID=@Gm_DeptID, limit = @Gm_LimitTime,user_status=@Gm_Status,OnlineActive=@Gm_OnlineActive,sysAdmin=@Gm_SysAdmin
	where	UserID = @Gm_UserID

	IF (@@rowcount = 0)
	BEGIN
		ROLLBACK TRAN
	insert into GMTools_LogTime (operateUserID,operateMsg) values (@Gm_OperateUserID,'修改用户ID'+Convert(varchar(5),@Gm_UserID)+'的信息失败')	
		SELECT @ERRNO = 0
	END
	ELSE
	BEGIN
		COMMIT TRAN
	insert into GMTools_LogTime (operateUserID,operateMsg) values (@Gm_OperateUserID,'修改用户ID'+Convert(varchar(5),@Gm_UserID)+'的信息成功')		--记录log表 
		SELECT @ERRNO = 1
	END
RETURN @ERRNO




GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GMTools_SDO_Message](
	[Seq] [int] IDENTITY(1,1) NOT NULL,
	[ReceiverIndexID] [int] NOT NULL,
	[SenderNickName] [varchar](16) NOT NULL,
	[Title] [varchar](40) NOT NULL,
	[Content] [varchar](400) NULL,
	[WriteDate] [smalldatetime] NULL,
	[itemcode] [int] NOT NULL,
	[timeslimit] [smallint] NULL,
	[datelimit] [smalldatetime] NULL,
	[SendReason] [text] NULL,
	[GM_User] [int] NULL,
	[ReceiverUserID] [varchar](30) NULL,
	[ReceiverNick] [varchar](30) NULL,
	[ServerIp] [varchar](50) NULL,
	[moneytype] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




/*********************************************************************************/
/*	SP NAME : Gmtool_Gminfo_Select			 */
/*	MOD DATE: 2005-12-28							 */
/*	EDITOR  : lxf							 */
/*-------------------------------------------------------------------------------*/
/*	DESC SP : select user info		 */
/*-------------------------------------------------------------------------------*/
/*	INPUT   : Not describe							 */
/*	RETURN  : Not describe							 */
/*-------------------------------------------------------------------------------*/
/*	MEMO    : 								 */
/*********************************************************************************/
CREATE   PROC [dbo].[Gmtool_Gminfo_Select]
@Gm_UserID		int
WITH ENCRYPTION AS
	
	select a.UserName,a.user_Pwd,b.Module_ID from GMTools_Users a inner join GMTools_Roles b on a.UserID = @Gm_UserID 
		
	





GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GMTools_MJ_AccountPWD](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[login_account] [varchar](20) NOT NULL,
	[login_password] [varchar](20) NULL,
	[login_password_md5] [varchar](16) NULL,
	[serverip] [varchar](30) NULL,
	[createtime] [datetime] NULL,
	[gameflag] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



/******************************************************************************/
/*	SP NAME : Gmtool_Gminfo_Edit			 */
/*	MOD DATE: 2005-12-28							 */
/*	EDITOR  : lxf							 */
/*-------------------------------------------------------------------------------*/
/*	DESC SP : edit user info		 */
/*-------------------------------------------------------------------------------*/
/*	INPUT   : Not describe							 */
/*	RETURN  : Not describe							 */
/*-------------------------------------------------------------------------------*/
/*	MEMO    : 								 */
/*********************************************************************************/
CREATE           PROC [dbo].[Gmtool_PASSWD_Edit]
@Gm_OperateUserID int,
@Gm_UserID		int
,@Gm_Password		VARCHAR(50)
--,@ERRNO			INT	OUTPUT
WITH ENCRYPTION AS
declare  @ERRNO		INT 
declare  @UserID	int
declare	 @UserName	varchar(50)
declare  @User_Pwd	varchar(50)
declare  @User_MAC	varchar(50)
declare  @RealName	varchar(50)
declare  @DepartID	int
declare  @User_status	int
declare  @limit		smalldatetime
declare  @onlineActive  int
set NoCount ON
	
	BEGIN TRAN

	select @UserID=UserID from GMTools_Users where UserID = @Gm_UserID
	select @UserName=UserName from GMTools_Users where UserID = @Gm_UserID
	select @User_Pwd=User_Pwd from GMTools_Users where UserID = @Gm_UserID
	select @User_MAC=User_MAC from GMTools_Users where UserID = @Gm_UserID
	select @RealName=RealName from GMTools_Users where UserID =@Gm_UserID
	select @DepartID=DepartID from GMTools_Users where UserID = @Gm_UserID
	select @User_status=User_status from GMTools_Users where UserID = @Gm_UserID
	select @limit=limit from GMTools_Users where UserID = @Gm_UserID
	select @onlineActive=onlineActive from GMTools_Users where UserID = @Gm_UserID
	
		
	insert into GMTools_Log_UpdateAgo(UserID,SP_Name,Real_Act) values (@Gm_OperateUserID,'Gmtool_PASSWD_Edit','UserID_'+convert(varchar(10),@UserID)+'__UserName_'+@UserName+'__User_MAC_'+@User_MAC+'__RealName_'+@RealName+'__User_Pwd_'+@User_Pwd+'__DepartID_'+convert(varchar(10),@DepartID)+'__User_status_'+convert(varchar(10),@User_status)+'__limit_'+convert(varchar(30),@limit)+'__onlineActive_'+convert(varchar(10),@onlineActive)+'备份成功')	
	

	update GMTools_Users					--修改用户表信息
		set User_Pwd = @Gm_Password
	where	UserID = @Gm_UserID

	IF (@@rowcount= 0)
	BEGIN
		ROLLBACK TRAN
	insert into GMTools_LogTime (operateUserID,operateMsg) values (@Gm_OperateUserID,'修改用户ID'+Convert(varchar(5),@Gm_UserID)+'的密码失败')	
		SELECT @ERRNO = 0
	END
	ELSE
	BEGIN
		COMMIT TRAN
	insert into GMTools_LogTime (operateUserID,operateMsg) values (@Gm_OperateUserID,'修改用户ID'+Convert(varchar(5),@Gm_UserID)+'的密码成功')		--记录log表 
		SELECT @ERRNO = 1
	END
RETURN @ERRNO


--exec Gmtool_PASSWD_Edit 24,24,'24'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GMTools_Roles](
	[Role_ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Module_ID] [int] NOT NULL,
	[Game_ID] [int] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




USE [gmtools]
GO
/****** 对象:  StoredProcedure [dbo].[Mf_GeneName_Query]    脚本日期: 02/19/2009 15:18:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create              proc [dbo].[Mf_GeneName_Query]
@GeneID int
as
begin

select Itemname from Mf_PetGene where Itemid= @GeneID

end






GO
/****** 对象:  StoredProcedure [dbo].[Mf_PetLevel_Query]    脚本日期: 02/19/2009 15:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create              proc [dbo].[Mf_PetLevel_Query]
@MfID int
as
begin
select * from Mf_Petlevel
end



GO
/****** 对象:  StoredProcedure [dbo].[ServerInfo_Query]    脚本日期: 02/19/2009 18:23:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE procedure [dbo].[ServerInfo_Query]
@GM_gameID int,
@GM_gameDBID int

as
begin
  declare @strQuery varchar(700)
  set @strQuery = 'select serverIP,city,gameName from GMTools_Serverinfo where gamedbID='+convert(varchar(10),@GM_gameDBID)+' and gameID='+convert(varchar(10),@GM_gameID)+' and gameflag=1 order by city'
  execute(@strQuery)
  print(@strQuery)
end



/****** 对象:  Table [dbo].[SqlExpress]    脚本日期: 02/19/2009 18:50:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SqlExpress](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[sql_type] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[sql_field] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[sql_statement] [varchar](8000) COLLATE Chinese_PRC_CI_AS NULL,
	[sql_condition] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF



/****** 对象:  StoredProcedure [dbo].[ServerName_Query]    脚本日期: 02/19/2009 18:44:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO










CREATE   procedure [dbo].[ServerName_Query]
@ServerIP varchar(30)

as
begin
  declare @strQuery varchar(700)
  set @strQuery = 'select city from GMTools_Serverinfo where serverIP='''+@ServerIP+''' and gameflag=1'
  execute(@strQuery)
  print(@strQuery)
end

--execute ServerName_Query '218.83.158.160'



GO
/****** 对象:  StoredProcedure [dbo].[Gmtool_UserInfo_Query]    脚本日期: 02/19/2009 19:08:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









/*********************************************************************************/
/*	SP NAME : Gmtool_UserInfo_Query			 */
/*	MOD DATE: 2005-12-28							 */
/*	EDITOR  : lxf							 */
/*-------------------------------------------------------------------------------*/
/*	DESC SP : select user info		 */
/*-------------------------------------------------------------------------------*/
/*	INPUT   : Not describe							 */
/*	RETURN  : Not describe							 */
/*-------------------------------------------------------------------------------*/
/*	MEMO    : 								 */
/*********************************************************************************/
CREATE        PROC [dbo].[Gmtool_UserInfo_Query]
@Gm_UserID int
as
        declare @sysAdmin int
        declare @deptID int
        select @sysAdmin = sysAdmin from gmtools_users where userid = @Gm_UserID
      if @sysAdmin = 1
        select a.userID,a.userName,a.user_Pwd,a.user_mac,a.realName,a.DepartID,b.departName,a.user_Status,a.limit,a.Onlineactive,a.SysAdmin,userIP from GMTOOLS_Users a,Department b where a.departID=b.departID and a.user_status=1
     else
       begin
        select @deptID = departID from gmtools_users where userid = @Gm_UserID
        select a.userID,a.userName,a.user_Pwd,a.user_mac,a.realName,a.DepartID,b.departName,a.user_Status,a.limit,a.Onlineactive,a.SysAdmin,userIP from GMTOOLS_Users a,Department b where a.sysAdmin=0 and a.departID=b.departID and b.departID = @deptID   and a.user_status=1
       end
--execute Gmtool_DepartAdmin_Query 9



/****** 对象:  StoredProcedure [dbo].[Gmtool_GmModule_Delete]    脚本日期: 02/19/2009 19:22:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









/*********************************************************************************/
/*	SP NAME : Gmtool_GmModule_Delete			 */
/*	MOD DATE: 2005-12-28							 */
/*	EDITOR  : lxf							 */
/*-------------------------------------------------------------------------------*/
/*	DESC SP : delete game modules		 */
/*-------------------------------------------------------------------------------*/
/*	INPUT   : Not describe							 */
/*	RETURN  : Not describe							 */
/*-------------------------------------------------------------------------------*/
/*	MEMO    : 								 */
/*********************************************************************************/
CREATE        PROC [dbo].[Gmtool_GmModule_Delete]
@Gm_OperateUserID INT,
@GM_ModuleID		INT
--,@ERRNO			INT	OUTPUT
AS
declare  @ERRNO		INT 
declare  @Module_ID	int
declare  @Game_ID	int
declare	 @Module_Name  	varchar(50)
declare  @Module_Class	varchar(50)
declare  @Module_Content varchar(500)
set NoCount ON
	
	BEGIN TRAN
	select @Module_ID=Module_ID from GMTools_Modules where Module_ID = @GM_ModuleID
	select @Game_ID=Game_ID from GMTools_Modules where Module_ID = @GM_ModuleID
	select @Module_Name=Module_Name from GMTools_Modules where Module_ID = @GM_ModuleID
	select @Module_Class=Module_Class from GMTools_Modules where Module_ID = @GM_ModuleID
	select @Module_Content=Module_Content from GMTools_Modules where Module_ID = @GM_ModuleID
	insert into GMTools_Log_UpdateAgo(UserID,SP_Name,Real_Act) values (@Gm_OperateUserID,'Gmtool_GmModule_Delete','Module_ID_'+convert(varchar(10),@Module_ID)+'__Game_ID_'+convert(varchar(10),@Game_ID)+'__Module_Name_'+@Module_Name+'__Module_Class_'+@Module_Class+'__Module_Content_'+@Module_Content+'备份成功')	
	delete from GMTools_Modules				--删除模块表信息
		
	where	Module_ID = @GM_ModuleID
	
	
	IF (@@ROWCOUNT = 0)
	BEGIN
		ROLLBACK TRAN
	insert into  GMTools_LogTime (OperateUserID,OperateMsg) values (@Gm_OperateUserID,'删除模块ID'+Convert(varchar(5),@GM_ModuleID)+'记录失败')
		SELECT @ERRNO = 0
	END
	ELSE
	BEGIN
		COMMIT TRAN
	insert into  GMTools_LogTime (OperateUserID,OperateMsg) values (@Gm_OperateUserID,'删除模块ID'+Convert(varchar(5),@GM_ModuleID)+'记录成功')
		SELECT @ERRNO = 1
	END
RETURN @ERRNO



--exec Gmtool_GmModule_Delete 37,37


/****** 对象:  StoredProcedure [dbo].[Gmtool_GmModule_Insert]    脚本日期: 02/19/2009 19:23:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






/*********************************************************************************/
/*	SP NAME : Gmtool_GmModule_Insert			 */
/*	MOD DATE: 2005-12-28							 */
/*	EDITOR  : lxf							 */
/*-------------------------------------------------------------------------------*/
/*	DESC SP : ALTER  new Modules			 */
/*-------------------------------------------------------------------------------*/
/*	INPUT   : Not describe							 */
/*	RETURN  : Not describe							 */
/*-------------------------------------------------------------------------------*/
/*	MEMO    : 								 */
/*********************************************************************************/
CREATE    PROC [dbo].[Gmtool_GmModule_Insert]
@Gm_OperateUserID INT,
@GM_GameID		INT
,@GM_Name		VARCHAR(50)
,@GM_Class		VARCHAR(50)
,@GM_Content		varchar(400)
--,@ERRNO		INT	OUTPUT
AS
declare  @ERRNO		INT 
set NoCount ON
	
	BEGIN TRAN
	INSERT INTO GMTools_Modules				--写入Module表信息
		(Game_ID, Module_Name, Module_Class,Module_Content)
	VALUES
		(@GM_GameID, @GM_Name, @GM_Class,@GM_Content)

	
	IF (@@ERROR <> 0)
	BEGIN
		ROLLBACK TRAN
	insert into  GMTools_LogTime (OperateUserID,OperateMsg) values (@Gm_OperateUserID,'添加模块ID'+@GM_Name+'记录成功')
		SELECT @ERRNO = 0
	END
	ELSE
	BEGIN
		COMMIT TRAN
	insert into  GMTools_LogTime (OperateUserID,OperateMsg) values (@Gm_OperateUserID,'添加模块ID'+@GM_Name+'记录失败')	--记录log表 
		SELECT @ERRNO = 1
	END
RETURN @ERRNO





/****** 对象:  StoredProcedure [dbo].[Gmtool_GmModule_Update]    脚本日期: 02/19/2009 19:25:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







/*********************************************************************************/
/*	SP NAME : Gmtool_GmModule_Update			 */
/*	MOD DATE: 2005-12-28							 */
/*	EDITOR  : lxf							 */
/*-------------------------------------------------------------------------------*/
/*	DESC SP : edit game modules	 */
/*-------------------------------------------------------------------------------*/
/*	INPUT   : Not describe							 */
/*	RETURN  : Not describe							 */
/*-------------------------------------------------------------------------------*/
/*	MEMO    : 								 */
/*********************************************************************************/
CREATE      PROC [dbo].[Gmtool_GmModule_Update]
@Gm_OperateUserID INT,
@GM_ModuleID INT,
@GM_GameID		INT
,@GM_Name		VARCHAR(50)
,@GM_Class		VARCHAR(50)
,@GM_Content		varchar(400)
--,@ERRNO			INT	OUTPUT
AS
declare  @ERRNO		INT 
declare  @Module_ID	int
declare  @Game_ID	int
declare	 @Module_Name  	varchar(50)
declare  @Module_Class	varchar(50)
declare  @Module_Content varchar(500)
set NoCount ON
	
	BEGIN TRAN
	select @Module_ID=Module_ID from GMTools_Modules where Module_ID = @GM_ModuleID
	select @Game_ID=Game_ID from GMTools_Modules where Module_ID = @GM_ModuleID
	select @Module_Name=Module_Name from GMTools_Modules where Module_ID = @GM_ModuleID
	select @Module_Class=Module_Class from GMTools_Modules where Module_ID = @GM_ModuleID
	select @Module_Content=Module_Content from GMTools_Modules where Module_ID = @GM_ModuleID
	insert into GMTools_Log_UpdateAgo(UserID,SP_Name,Real_Act) values (@Gm_OperateUserID,'Gmtool_GmModule_Update','Module_ID_'+convert(varchar(10),@Module_ID)+'__Game_ID_'+convert(varchar(10),@Game_ID)+'__Module_Name_'+@Module_Name+'__Module_Class_'+@Module_Class+'__Module_Content_'+@Module_Content+'备份成功')	
	
	update GMTools_Modules					
		set Game_ID = @GM_GameID, Module_Name = @GM_Name,Module_Class = @GM_Class,Module_Content = @GM_Content
	where	Module_ID = @GM_ModuleID
		
	
	IF (@@rowcount= 0)
	BEGIN
		ROLLBACK TRAN
	insert into  GMTools_LogTime (OperateUserID,OperateMsg) values (@Gm_OperateUserID,'修改模块ID'+Convert(varchar(5),@GM_ModuleID)+'记录失败')
		SELECT @ERRNO = 0
	END
	ELSE
	BEGIN
		COMMIT TRAN
	insert into  GMTools_LogTime (OperateUserID,OperateMsg) values (@Gm_OperateUserID,'修改模块ID'+Convert(varchar(5),@GM_ModuleID)+'记录成功')
		SELECT @ERRNO = 1
	END
RETURN @ERRNO



--exec Gmtool_GmModule_Update 38,38,44,'44','44','44'



GO
/****** 对象:  StoredProcedure [dbo].[Gmtool_DepartRelateGame_Query]    脚本日期: 02/20/2009 11:03:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







/*********************************************************************************/
/*	SP NAME : Gmtool_DepartRelateGame_Query			 */
/*	MOD DATE: 2005-12-28							 */
/*	EDITOR  : lxf							 */
/*-------------------------------------------------------------------------------*/
/*	DESC SP : select user info		 */
/*-------------------------------------------------------------------------------*/
/*	INPUT   : Not describe							 */
/*	RETURN  : Not describe							 */
/*-------------------------------------------------------------------------------*/
/*	MEMO    : 								 */
/*********************************************************************************/
Create      PROC [dbo].[Gmtool_DepartRelateGame_Query]
@UserID int
AS
      declare @sysAdmin int
        select @sysAdmin = sysAdmin from gmtools_users where userid = @UserID
        if @sysAdmin = 1
           select game_ID,game_Name from gamelist
        else
	select c.gameID,d.game_Name from department a,gmtools_users b, DeptRelateGame c,GameList d where a.departID = c.deptID and a.departID =b.departID and c.gameID=d.game_ID and b.userid=@UserID

--execute Gmtool_DepartRelateGame_Query 1




GO
/****** 对象:  Table [dbo].[JW2_BoardTasker]    脚本日期: 08/03/2010 14:41:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[JW2_BoardTasker](
	[taskid] [int] IDENTITY(1,1) NOT NULL,
	[SendBeginTime] [datetime] NULL,
	[SendEndTime] [datetime] NULL,
	[Interval] [int] NULL,
	[command] [int] NULL,
	[status] [int] NULL,
	[username] [varchar](32) COLLATE Chinese_PRC_CI_AS NULL,
	[usernick] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[boardMessage] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[atonce] [int] NULL CONSTRAINT [DF_JW2_BoardTasker_atonce]  DEFAULT ((-1)),
	[ServerIP] [varchar](2000) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF




GO
/****** 对象:  Table [dbo].[JW2_BoardMessage]    脚本日期: 08/03/2010 14:42:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[JW2_BoardMessage](
	[boardID] [int] IDENTITY(1,1) NOT NULL,
	[taskid] [int] NULL,
	[userID] [int] NULL,
	[serverIP] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[GSServerIP] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF




GO
/****** 对象:  Table [dbo].[JW2_AutoUnBanUser]    脚本日期: 08/03/2010 14:42:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[JW2_AutoUnBanUser](
	[BanPlayerid] [int] IDENTITY(1,1) NOT NULL,
	[BanPlayerAccount] [int] NULL,
	[BanPlayerUserId] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[BanPlayerUserNick] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[ServerIP] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Bantime] [datetime] NULL,
	[State] [int] NULL,
	[BanReason] [varchar](1000) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF






set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go



create  PROCEDURE [dbo].[JW2_BanUser]
@Gm_UserByID int,
@JW2_UserSN int,
@JW2_UserID varchar(256),
@JW2_UserNick varchar(256),
@JW2_ServerIP varchar(256),
@JW2_Reason  varchar(2000)
as
BEGIN
	begin
	insert into JW2_AutoUnBanUser(BanPlayerAccount,BanPlayerUserId,BanPlayerUserNick,ServerIP,BanReason) values(@JW2_UserSN,@JW2_UserID,@JW2_UserNick,@JW2_ServerIP,@JW2_Reason)
	end
	declare  @ERRNO int
 	 IF (@@ERROR <> 0)
	BEGIN
		--ROLLBACK TRAN
	insert into GMTools_Log (UserID,SP_Name,Game_Name,ServerIP,Real_Act) values (@Gm_UserByID,'JW2_BanUser','劲舞团2',@JW2_ServerIP,'封停账号'+@JW2_UserID+'失败，原因:'+@JW2_Reason)	
		SELECT @ERRNO = 0
	END
	ELSE
	BEGIN
		--COMMIT TRAN
            	SELECT @ERRNO = 1
	insert into GMTools_Log (UserID,SP_Name,Game_Name,ServerIP,Real_Act) values (@Gm_UserByID,'JW2_BanUser','劲舞团2',@JW2_ServerIP,'封停账号'+@JW2_UserID+'成功，原因:'+@JW2_Reason)	
	
	END

	RETURN @ERRNO

END




set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go



create             procedure [dbo].[JW2_BOARDTASK_INSERT]
@Gm_UserID 		int,
@JW2_ServerIP		varchar(2000),
@JW2_GSServerIP varchar(2000),
@JW2_BoardMessage varchar(500),
@JW2_begintime datetime,
@JW2_endTime datetime,
@JW2_interval int
as
begin
    declare @var varchar(2000)
	declare @identityid int
    if (@JW2_interval>0)
    insert into  JW2_BoardTasker(SendBeginTime, SendEndTime ,Interval ,status,command,BoardMessage,atonce,serverIP) values (@JW2_begintime,@JW2_endTime,@JW2_interval,0,0,@JW2_BoardMessage,0,@JW2_ServerIP)
   else 
   insert into  JW2_BoardTasker(SendBeginTime, SendEndTime ,Interval ,status,command,BoardMessage,serverIP)values (dateadd(mi,-1,getdate()),dateadd(mi,5,getdate()),@JW2_interval,0,0,@JW2_BoardMessage,@JW2_ServerIP)
    set @identityid = @@IDENTITY 
    if(@JW2_ServerIP='255.255.255.255')
    begin
      insert into  JW2_BoardMessage (taskID,serverIP,GSServerIP,UserID) values (@identityid,@JW2_ServerIP,@JW2_ServerIP,@Gm_UserID)
    end
    else
    begin
	    if (@JW2_GSServerIP='255.255.255.255' and charindex('|',@JW2_ServerIP)>1)
	     begin
	      set @var = @JW2_ServerIP
	    while charindex('|',@var)>0
	        begin

	        insert into JW2_BoardMessage (taskID,serverIP,GSServerIP,UserID)  values (@identityid,left(@var,charindex('|',@var)-1),@JW2_GSServerIP,@Gm_UserID)
		    set @var=stuff(@var,1,charindex('|',@var),'')
	       end 
	    end
	    else
	    begin
	    set @var = @JW2_GSServerIP
	    while charindex('|',@var)>0
	        begin
	        insert into JW2_BoardMessage (taskID,serverIP,GSServerIP,UserID)  values (@identityid,@JW2_ServerIP,left(@var,charindex('|',@var)-1),@Gm_UserID)
		    set @var=stuff(@var,1,charindex('|',@var),'')
	       end 
	    end

    end
    declare  @ERRNO int
  IF (@@ERROR <> 0)
	BEGIN
		--ROLLBACK TRAN
	insert into GMTools_Log (UserID,SP_Name,Game_Name,ServerIP,Real_Act) values (@Gm_UserID,'JW2_BOARDTASK_INSERT','劲舞团2',@JW2_ServerIP,'添加公告任务'+@JW2_BoardMessage+'信息失败')	
		SELECT @ERRNO = 0
	END
	ELSE
	BEGIN
		--COMMIT TRAN
            	SELECT @ERRNO = 1
	insert into GMTools_Log (UserID,SP_Name,Game_Name,ServerIP,Real_Act) values (@Gm_UserID,'JW2_BOARDTASK_INSERT','劲舞团2',@JW2_ServerIP,'添加公告任务'+@JW2_BoardMessage+'信息成功')	
	
	END

	RETURN @ERRNO
end




set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

create  PROCEDURE [dbo].[JW2_BOARDTASK_QUERY]
as
BEGIN
set NoCount on
declare  @tmpServerIP 	varchar(200)
declare @serverIP varchar(2000)
select  @tmpServerIP = ServerIP from JW2_BoardTasker where status=0 or status=2 group by ServerIP,taskid order by taskid desc
declare @str char(40)
declare @var varchar(200)
set concat_null_yields_null off   
while(CHARINDEX('|',@tmpServerIP,0)>0)
begin
  set @str = substring(@tmpServerIP,0,CHARINDEX('|',@tmpServerIP,0))
  select @var = city from GMTools_Serverinfo where serverip = @str
  select @var = @var+'|'
  set @serverIP = @serverIP + @var
  set @tmpServerIP=substring(@tmpServerIP,CHARINDEX('|',@tmpServerIP,0)+1,len(@tmpServerIP)-len(@str))
END
select distinct taskid as jw2_taskid,convert(varchar(20),sendBeginTime,120) as TIMEBegin,convert(varchar(20),sendEndTime,120) as TIMEEend,Interval as jw2_interval,status as jw2_status,boardmessage as jw2_boardmessage,@serverIP as jw2_serverip from JW2_BoardTasker where status=0 or status=2 order by taskid desc
END




set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go




create              procedure [dbo].[JW2_BOARDTASK_UPDATE]
@Gm_UserID 		int,
@JW2_TaskID int,
@JW2_BoardMessage varchar(500),
@JW2_begintime datetime,
@JW2_endTime datetime,
@JW2_interval int,
@JW2_Status int
as
begin
    declare @var varchar(300)
    declare @JW2_ServerIP varchar(4000)
	declare @identityid int
    if (@JW2_Status =1)
         update   JW2_BoardTasker set command=@JW2_Status,status=@JW2_Status  where taskid = @JW2_TaskID
   else
   	begin
   	update   JW2_BoardTasker set SendBeginTime =@JW2_begintime, SendEndTime=@JW2_endTime ,Interval=@JW2_interval ,status=@JW2_status,command=@JW2_Status,BoardMessage=@JW2_BoardMessage where taskid = @JW2_TaskID
  	end

   select @JW2_ServerIP=ServerIP from JW2_BoardTasker where taskid = @JW2_TaskID
    declare  @ERRNO int
   IF (@@ERROR <> 0)
	BEGIN
		--ROLLBACK TRAN
	insert into GMTools_Log (UserID,SP_Name,Game_Name,ServerIP,Real_Act) values (@Gm_UserID,'JW2_BOARDTASK_UPDATE','劲舞团2',@JW2_ServerIP,'修改公告信息失败')	
		SELECT @ERRNO = 0
	END
	ELSE
	BEGIN
		--COMMIT TRAN
	insert into GMTools_Log (UserID,SP_Name,Game_Name,ServerIP,Real_Act) values (@Gm_UserID,'JW2_BOARDTASK_UPDATE','劲舞团2',@JW2_ServerIP,'修改公告信息成功')	
		SELECT @ERRNO = 1
	END

	RETURN @ERRNO
end




set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

create PROCEDURE [dbo].[JW2_FindDBIP] 
@serverip varchar(50),
@dbid int
AS
begin
	declare @gameID varchar(50)
	declare @city varchar(50)
	select @gameID=gameID,@city=city from gmtools_serverinfo where serverip=@serverip and gameflag=1
	select serverip from gmtools_serverinfo where city=@city and gamedbid=@dbid and gameid=@gameid
end


set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go



create PROCEDURE [dbo].[JW2_FindDBName] 
@serverip varchar(50)
AS
begin
	declare @gameID varchar(50)
	declare @city varchar(50)
	select @gameID=gameID,@city=city from gmtools_serverinfo where serverip=@serverip and gameflag=1
	select distinct city from gmtools_serverinfo where city=@city and gameid=@gameid
end


set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go



create PROCEDURE [dbo].[JW2_FindTitleName_New]
@iTitleType int,
@iTitleNo int
 AS
begin
	select titlename from jw2_title_new where titleType=@iTitleType and TitleNo=@iTitleNo
end



set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go



create PROCEDURE [dbo].[JW2_Garden_ExpToLevel]
@Exp float
AS
begin
	select  top 1 level GardenLv from jw2_gardenlv where  exp<=@exp order by level desc
end


set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


create PROCEDURE [dbo].[JW2_ItemCodeToLimitDay]
@itemcode  varchar(256)
AS
begin
	select dm  from jw2_item where productid=@itemcode or itemcode= @itemcode
end




set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


create PROCEDURE [dbo].[JW2_ItemCodeToName] 
@itemcode  varchar(256)
AS
begin
	select itemname from jw2_product where itemcode=@itemcode
end


set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


create PROCEDURE [dbo].[JW2_ProductIDToName] 
@ProductID  varchar(256)
AS
begin
	select p.itemName from jw2_item as i,jw2_Product as p where i.itemcode=p.itemcode and i.ProductID=@ProductID 
end



set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go



create  PROCEDURE [dbo].[JW2_UnBanUser]
@Gm_UserByID int,
@JW2_UserSN int,
@JW2_UserID varchar(256),
@JW2_ServerIP  varchar(256),
@JW2_Reason  varchar(2000)
as
BEGIN
	begin
	     delete from JW2_AutoUnBanUser where BanPlayerAccount=@JW2_UserSN
	end
	declare  @ERRNO int
 	 IF (@@ERROR <> 0)
	BEGIN
		--ROLLBACK TRAN
	insert into GMTools_Log (UserID,SP_Name,Game_Name,ServerIP,Real_Act) values (@Gm_UserByID,'JW2_UnBanUser','劲舞团2',@JW2_ServerIP,'解封账号'+@JW2_UserID+'失败，原因:'+@JW2_Reason)	
		SELECT @ERRNO = 0
	END
	ELSE
	BEGIN
		--COMMIT TRAN
            	SELECT @ERRNO = 1
	insert into GMTools_Log (UserID,SP_Name,Game_Name,ServerIP,Real_Act) values (@Gm_UserByID,'JW2_UnBanUser','劲舞团2',@JW2_ServerIP,'解封账号'+@JW2_UserID+'成功，原因:'+@JW2_Reason)	
	
	END

	RETURN @ERRNO

END





