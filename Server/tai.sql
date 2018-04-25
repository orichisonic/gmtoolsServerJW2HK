IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'gmtools')
	DROP DATABASE [gmtools]
GO

CREATE DATABASE [gmtools]  ON (NAME = N'gmtoolsdata', FILENAME = N'd:\gmtools\gmtools.mdf' , SIZE = 2, FILEGROWTH = 10%) LOG ON (NAME = N'gmtoolslog', FILENAME = N'd:\gmtools\gmtools_log.ldf' , SIZE = 1, FILEGROWTH = 10%)
 COLLATE Chinese_PRC_CI_AS
GO

exec sp_dboption N'gmtools', N'autoclose', N'true'
GO

exec sp_dboption N'gmtools', N'bulkcopy', N'false'
GO

exec sp_dboption N'gmtools', N'trunc. log', N'true'
GO

exec sp_dboption N'gmtools', N'torn page detection', N'true'
GO

exec sp_dboption N'gmtools', N'read only', N'false'
GO

exec sp_dboption N'gmtools', N'dbo use', N'false'
GO

exec sp_dboption N'gmtools', N'single', N'false'
GO

exec sp_dboption N'gmtools', N'autoshrink', N'true'
GO

exec sp_dboption N'gmtools', N'ANSI null default', N'false'
GO

exec sp_dboption N'gmtools', N'recursive triggers', N'false'
GO

exec sp_dboption N'gmtools', N'ANSI nulls', N'false'
GO

exec sp_dboption N'gmtools', N'concat null yields null', N'false'
GO

exec sp_dboption N'gmtools', N'cursor close on commit', N'false'
GO

exec sp_dboption N'gmtools', N'default to local cursor', N'false'
GO

exec sp_dboption N'gmtools', N'quoted identifier', N'false'
GO

exec sp_dboption N'gmtools', N'ANSI warnings', N'false'
GO

exec sp_dboption N'gmtools', N'auto create statistics', N'true'
GO

exec sp_dboption N'gmtools', N'auto update statistics', N'true'
GO

if( (@@microsoftversion / power(2, 24) = 8) and (@@microsoftversion & 0xffff >= 724) )
	exec sp_dboption N'gmtools', N'db chaining', N'false'
GO

use [gmtools]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AU2_BanUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AU2_BanUser]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AU2_BoardTask_Query]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AU2_BoardTask_Query]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AU2_ServerIPToGateWay]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AU2_ServerIPToGateWay]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AU2_TaskList_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AU2_TaskList_Insert]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AU2_TaskList_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AU2_TaskList_Update]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AU2_TaskidGetServerIP]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AU2_TaskidGetServerIP]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AU2_UnBanUser]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AU2_UnBanUser]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Gmtool_GmUserModule_Admin]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Gmtool_GmUserModule_Admin]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Gmtool_Gminfo_Add]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Gmtool_Gminfo_Add]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Gmtool_Gminfo_Del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Gmtool_Gminfo_Del]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Gmtool_Gminfo_Edit]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Gmtool_Gminfo_Edit]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Gmtool_PASSWD_Edit]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Gmtool_PASSWD_Edit]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Gmtool_UserInfo_Query]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Gmtool_UserInfo_Query]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ServerInfo_Query]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ServerInfo_Query]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AU2_AutoUnBanUser]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[AU2_AutoUnBanUser]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AU2_BoardMessage]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[AU2_BoardMessage]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AU2_BoardTasker]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[AU2_BoardTasker]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Department]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Department]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GAMELIST]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[GAMELIST]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GMTools_Log]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[GMTools_Log]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GMTools_LogTime]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[GMTools_LogTime]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GMTools_Log_UpdateAgo]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[GMTools_Log_UpdateAgo]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GMTools_Modules]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[GMTools_Modules]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GMTools_Roles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[GMTools_Roles]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GMTools_Serverinfo]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[GMTools_Serverinfo]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GMTools_Users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[GMTools_Users]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[SqlExpress]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[SqlExpress]
GO

if not exists (select * from master.dbo.syslogins where loginname = N'gmtools')
BEGIN
	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'gmtools', @loginlang = N'簡體中文'
	if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)
		select @logindb = N'master'
	if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')
		select @loginlang = @@language
	exec sp_addlogin N'gmtools', null, @logindb, @loginlang
END
GO

if not exists (select * from master.dbo.syslogins where loginname = N'MyDB')
BEGIN
	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'gmtools', @loginlang = N'簡體中文'
	if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)
		select @logindb = N'master'
	if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')
		select @loginlang = @@language
	exec sp_addlogin N'MyDB', null, @logindb, @loginlang
END
GO

if not exists (select * from master.dbo.syslogins where loginname = N'tmpgmtools')
BEGIN
	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'master', @loginlang = N'簡體中文'
	if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)
		select @logindb = N'master'
	if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')
		select @loginlang = @@language
	exec sp_addlogin N'tmpgmtools', null, @logindb, @loginlang
END
GO

exec sp_addsrvrolemember N'gmtools', sysadmin
GO

exec sp_addsrvrolemember N'tmpgmtools', sysadmin
GO

exec sp_addsrvrolemember N'gmtools', securityadmin
GO

exec sp_addsrvrolemember N'gmtools', serveradmin
GO

if not exists (select * from dbo.sysusers where name = N'gmtools')
	EXEC sp_grantdbaccess N'gmtools', N'gmtools'
GO

if not exists (select * from dbo.sysusers where name = N'guest' and hasdbaccess = 1)
	EXEC sp_grantdbaccess N'guest'
GO

exec sp_addrolemember N'db_accessadmin', N'gmtools'
GO

exec sp_addrolemember N'db_owner', N'gmtools'
GO

exec sp_addrolemember N'db_securityadmin', N'gmtools'
GO

CREATE TABLE [dbo].[AU2_AutoUnBanUser] (
	[BanPlayerid] [int] IDENTITY (1, 1) NOT NULL ,
	[BanPlayerAccount] [int] NULL ,
	[BanPlayerUserId] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[BanPlayerUserNick] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[ServerIP] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[Bantime] [datetime] NULL ,
	[State] [int] NULL ,
	[BanReason] [varchar] (1000) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AU2_BoardMessage] (
	[boardID] [int] IDENTITY (1, 1) NOT NULL ,
	[taskid] [int] NULL ,
	[userID] [int] NULL ,
	[serverIP] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[GSServerIP] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AU2_BoardTasker] (
	[taskid] [int] IDENTITY (1, 1) NOT NULL ,
	[SendBeginTime] [datetime] NULL ,
	[SendEndTime] [datetime] NULL ,
	[Interval] [int] NULL ,
	[command] [int] NULL ,
	[status] [int] NULL ,
	[username] [varchar] (32) COLLATE Chinese_PRC_CI_AS NULL ,
	[usernick] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[boardMessage] [varchar] (500) COLLATE Chinese_PRC_CI_AS NULL ,
	[atonce] [int] NULL ,
	[ServerIP] [varchar] (2000) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Department] (
	[DepartID] [int] IDENTITY (1, 1) NOT NULL ,
	[DepartName] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[Remark] [varchar] (200) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[GAMELIST] (
	[Game_ID] [int] IDENTITY (1, 1) NOT NULL ,
	[Game_Name] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[Game_Content] [varchar] (400) COLLATE Chinese_PRC_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[GMTools_Log] (
	[LOG_ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[UserID] [int] NOT NULL ,
	[SP_Name] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[Game_Name] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[ServerIP] [varchar] (3000) COLLATE Chinese_PRC_CI_AS NULL ,
	[Real_Act] [varchar] (3000) COLLATE Chinese_PRC_CI_AS NULL ,
	[Act_Time] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[GMTools_LogTime] (
	[LOG_ID] [int] IDENTITY (1, 1) NOT NULL ,
	[OperateUserID] [int] NOT NULL ,
	[OperateMsg] [varchar] (500) COLLATE Chinese_PRC_CI_AS NULL ,
	[LogTime] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[GMTools_Log_UpdateAgo] (
	[LOG_ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[UserID] [int] NOT NULL ,
	[SP_Name] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ServerIP] [varchar] (30) COLLATE Chinese_PRC_CI_AS NULL ,
	[Real_Act] [varchar] (7000) COLLATE Chinese_PRC_CI_AS NULL ,
	[Act_Time] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[GMTools_Modules] (
	[Module_ID] [int] IDENTITY (1, 1) NOT NULL ,
	[Game_ID] [int] NOT NULL ,
	[Module_Name] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[Module_Class] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[Module_Content] [varchar] (500) COLLATE Chinese_PRC_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[GMTools_Roles] (
	[Role_ID] [int] IDENTITY (1, 1) NOT NULL ,
	[UserID] [int] NOT NULL ,
	[Module_ID] [int] NOT NULL ,
	[Game_ID] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[GMTools_Serverinfo] (
	[idx] [int] IDENTITY (1, 1) NOT NULL ,
	[serverip] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[city] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gameID] [int] NOT NULL ,
	[gamename] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gameflag] [int] NOT NULL ,
	[gamedbID] [int] NOT NULL ,
	[descinfo] [varchar] (200) COLLATE Chinese_PRC_CI_AS NULL ,
	[createby] [varchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[createtime] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[GMTools_Users] (
	[UserID] [int] IDENTITY (1, 1) NOT NULL ,
	[UserName] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[User_Pwd] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[User_MAC] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[ReaLName] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[DepartID] [int] NOT NULL ,
	[User_status] [int] NOT NULL ,
	[limit] [smalldatetime] NOT NULL ,
	[onlineActive] [int] NOT NULL ,
	[sysAdmin] [tinyint] NULL ,
	[userIP] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SqlExpress] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[sql_type] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[sql_field] [varchar] (500) COLLATE Chinese_PRC_CI_AS NULL ,
	[sql_statement] [varchar] (4000) COLLATE Chinese_PRC_CI_AS NULL ,
	[sql_condition] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AU2_BoardTasker] ADD 
	CONSTRAINT [DF__AU2_Board__statu__7A9C383C] DEFAULT (0) FOR [status],
	CONSTRAINT [DF_AU2_BoardTasker_atonce] DEFAULT ((-1)) FOR [atonce]
GO

ALTER TABLE [dbo].[GMTools_Log] ADD 
	CONSTRAINT [DF__GMTools_L__Act_T__023D5A04] DEFAULT (getdate()) FOR [Act_Time]
GO

ALTER TABLE [dbo].[GMTools_LogTime] ADD 
	CONSTRAINT [DF__GMTools_L__LogTi__0425A276] DEFAULT (getdate()) FOR [LogTime]
GO

ALTER TABLE [dbo].[GMTools_Log_UpdateAgo] ADD 
	CONSTRAINT [DF__GMTools_L__Act_T__78B3EFCA] DEFAULT (getdate()) FOR [Act_Time]
GO

ALTER TABLE [dbo].[GMTools_Serverinfo] ADD 
	CONSTRAINT [DF__GMTools_S__creat__00551192] DEFAULT (getdate()) FOR [createtime]
GO

ALTER TABLE [dbo].[GMTools_Users] ADD 
	CONSTRAINT [DF_GMTools_Users_onlineActive] DEFAULT (0) FOR [onlineActive]
GO

GRANT  REFERENCES ,  SELECT ,  UPDATE ,  INSERT ,  DELETE  ON [dbo].[AU2_AutoUnBanUser]  TO [gmtools]
GO

GRANT  REFERENCES ,  SELECT ,  UPDATE ,  INSERT ,  DELETE  ON [dbo].[AU2_BoardMessage]  TO [gmtools]
GO

GRANT  REFERENCES ,  SELECT ,  UPDATE ,  INSERT ,  DELETE  ON [dbo].[AU2_BoardTasker]  TO [gmtools]
GO

GRANT  REFERENCES ,  SELECT ,  UPDATE ,  INSERT ,  DELETE  ON [dbo].[Department]  TO [gmtools]
GO

GRANT  REFERENCES ,  SELECT ,  UPDATE ,  INSERT ,  DELETE  ON [dbo].[GAMELIST]  TO [gmtools]
GO

GRANT  REFERENCES ,  SELECT ,  UPDATE ,  INSERT ,  DELETE  ON [dbo].[GMTools_Log]  TO [gmtools]
GO

GRANT  REFERENCES ,  SELECT ,  UPDATE ,  INSERT ,  DELETE  ON [dbo].[GMTools_LogTime]  TO [gmtools]
GO

GRANT  REFERENCES ,  SELECT ,  UPDATE ,  INSERT ,  DELETE  ON [dbo].[GMTools_Log_UpdateAgo]  TO [gmtools]
GO

GRANT  REFERENCES ,  SELECT ,  UPDATE ,  INSERT ,  DELETE  ON [dbo].[GMTools_Modules]  TO [gmtools]
GO

GRANT  REFERENCES ,  SELECT ,  UPDATE ,  INSERT ,  DELETE  ON [dbo].[GMTools_Roles]  TO [gmtools]
GO

GRANT  REFERENCES ,  SELECT ,  UPDATE ,  INSERT ,  DELETE  ON [dbo].[GMTools_Serverinfo]  TO [gmtools]
GO

GRANT  REFERENCES ,  SELECT ,  UPDATE ,  INSERT ,  DELETE  ON [dbo].[GMTools_Users]  TO [gmtools]
GO

GRANT  REFERENCES ,  SELECT ,  UPDATE ,  INSERT ,  DELETE  ON [dbo].[SqlExpress]  TO [gmtools]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO










-- =============================================
-- Author:		<Author,,Name>
-- ALTER  date: <ALTER  Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE [dbo].[AU2_BanUser]
@Gm_UserByID int,
@AU2_UserSN int,
@AU2_UserID varchar(256),
@AU2_UserNick varchar(256),
@AU2_ServerIP varchar(256),
@AU2_Reason  varchar(2000)
as
BEGIN
	begin
	insert into AU2_AutoUnBanUser(BanPlayerAccount,BanPlayerUserId,BanPlayerUserNick,ServerIP,BanReason) values(@AU2_UserSN,@AU2_UserID,@AU2_UserNick,@AU2_ServerIP,@AU2_Reason)
	end
	declare  @ERRNO int
 	 IF (@@ERROR <> 0)
	BEGIN
		--ROLLBACK TRAN
	insert into GMTools_Log (UserID,SP_Name,Game_Name,ServerIP,Real_Act) values (@Gm_UserByID,'AU2_BanUser','火雞王',@AU2_ServerIP,'封停帳號'+@AU2_UserID+'失敗,原因:'+@AU2_Reason)	
		SELECT @ERRNO = 0
	END
	ELSE
	BEGIN
		--COMMIT TRAN
            	SELECT @ERRNO = 1
	insert into GMTools_Log (UserID,SP_Name,Game_Name,ServerIP,Real_Act) values (@Gm_UserByID,'AU2_BanUser','火雞王',@AU2_ServerIP,'封停帳號'+@AU2_UserID+'成功,原因:'+@AU2_Reason)	
	
	END

	RETURN @ERRNO

END


--substring(a.zoneIP,0,charindex(':',a.zoneIP))=


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[AU2_BanUser]  TO [gmtools]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO










-- =============================================
-- Author:		<Author,,Name>
-- ALTER  date: <ALTER  Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE [dbo].[AU2_BoardTask_Query]
as
BEGIN
	



select distinct taskid as au2_taskid,sendBeginTime as au2_begintime,sendEndTime as au2_endtime,Interval as au2_interval,status as au2_status,boardmessage as au2_boardmessage,ServerIP as au2_serverip from AU2_BoardTasker where status=0 or status=2 order by taskid desc
END


--substring(a.zoneIP,0,charindex(':',a.zoneIP))=


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[AU2_BoardTask_Query]  TO [gmtools]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO



CREATE PROCEDURE AU2_ServerIPToGateWay
@serverIP varchar(50)
 AS
begin
declare @city  varchar(50)
select @city=city from dbo.GMTools_Serverinfo where serverip =@serverIP
select serverip from dbo.GMTools_Serverinfo where city=@city and descinfo='AU2GateWay'
end


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[AU2_ServerIPToGateWay]  TO [gmtools]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

























--execute AU2_TaskList_Insert 48,'61.172.206.201,127.0.0.1,127.0.0.2,127.0.0.3,127.0.0.4,127.0.0.5,127.0.0.6,127.0.0.7;','aaaaaa','2006-7-1','2006-1-1',5
CREATE              procedure [dbo].[AU2_TaskList_Insert]
@Gm_UserID 		int,
@AU2_ServerIP		varchar(2000),
@AU2_GSServerIP varchar(2000),
@AU2_BoardMessage varchar(500),
@AU2_begintime datetime,
@AU2_endTime datetime,
@AU2_interval int
as
begin
    declare @var varchar(2000)
	declare @identityid int
    if (@AU2_interval>0)
    insert into  AU2_BoardTasker(SendBeginTime, SendEndTime ,Interval ,status,command,BoardMessage,atonce,serverIP) values (@AU2_begintime,@AU2_endTime,@AU2_interval,0,0,@AU2_BoardMessage,0,@AU2_ServerIP)
   else 
   insert into  AU2_BoardTasker(SendBeginTime, SendEndTime ,Interval ,status,command,BoardMessage,serverIP)values (dateadd(mi,-1,getdate()),dateadd(mi,5,getdate()),@AU2_interval,0,0,@AU2_BoardMessage,@AU2_ServerIP)
    set @identityid = @@IDENTITY 
    if(@AU2_ServerIP='255.255.255.255')
    begin
      insert into  AU2_BoardMessage (taskID,serverIP,GSServerIP,UserID) values (@identityid,@AU2_ServerIP,@AU2_ServerIP,@Gm_UserID)
    end
    else
    begin
	    if (@AU2_GSServerIP='255.255.255.255' and charindex(',',@AU2_ServerIP)>1)
	     begin
	      set @var = @AU2_ServerIP
	    while charindex(',',@var)>0
	        begin

	        insert into AU2_BoardMessage (taskID,serverIP,GSServerIP,UserID)  values (@identityid,left(@var,charindex(',',@var)-1),@AU2_GSServerIP,@Gm_UserID)
		    set @var=stuff(@var,1,charindex(',',@var),'')
	       end 
	    end
	    else
	    begin
	    set @var = @AU2_GSServerIP
	    while charindex(',',@var)>0
	        begin
	        insert into AU2_BoardMessage (taskID,serverIP,GSServerIP,UserID)  values (@identityid,@AU2_ServerIP,left(@var,charindex(',',@var)-1),@Gm_UserID)
		    set @var=stuff(@var,1,charindex(',',@var),'')
	       end 
	    end

    end
    declare  @ERRNO int
  IF (@@ERROR <> 0)
	BEGIN
		--ROLLBACK TRAN
	insert into GMTools_Log (UserID,SP_Name,Game_Name,ServerIP,Real_Act) values (@Gm_UserID,'AU2_TaskList_Insert','火雞王',@AU2_ServerIP,'添加公告任務的'+@AU2_BoardMessage+'資訊失敗')	
		SELECT @ERRNO = 0
	END
	ELSE
	BEGIN
		--COMMIT TRAN
            	SELECT @ERRNO = 1
	insert into GMTools_Log (UserID,SP_Name,Game_Name,ServerIP,Real_Act) values (@Gm_UserID,'AU2_TaskList_Insert','火雞王',@AU2_ServerIP,'添加公告任務的'+@AU2_BoardMessage+'資訊成功')	
	
	END

	RETURN @ERRNO
end


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[AU2_TaskList_Insert]  TO [gmtools]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO



CREATE              procedure [dbo].[AU2_TaskList_Update]
@Gm_UserID 		int,
@AU2_ServerIP		varchar(300),
@AU2_TaskID int,
@AU2_BoardMessage varchar(500),
@AU2_begintime datetime,
@AU2_endTime datetime,
@AU2_interval int,
@AU2_Status int
as
begin
    declare @var varchar(300)
	declare @identityid int
    if (@AU2_Status =1)
         update   AU2_BoardTasker set command=@AU2_Status,status=@AU2_Status  where taskid = @AU2_TaskID
   else
   begin
   update   AU2_BoardTasker set SendBeginTime =@AU2_begintime, SendEndTime=@AU2_endTime ,Interval=@AU2_interval ,status=@AU2_status,command=@AU2_Status,BoardMessage=@AU2_BoardMessage where taskid = @AU2_TaskID
   end
    declare  @ERRNO int
   IF (@@ERROR <> 0)
	BEGIN
		--ROLLBACK TRAN
	insert into GMTools_Log (UserID,SP_Name,Game_Name,ServerIP,Real_Act) values (@Gm_UserID,'AU2_TaskList_Update','火雞王',@AU2_ServerIP,'修改公告任務的資訊失敗')	
		SELECT @ERRNO = 0
	END
	ELSE
	BEGIN
		--COMMIT TRAN
	insert into GMTools_Log (UserID,SP_Name,Game_Name,ServerIP,Real_Act) values (@Gm_UserID,'AU2_TaskList_Update','火雞王',@AU2_ServerIP,'修改公告任務的資訊成功')	
		SELECT @ERRNO = 1
	END

	RETURN @ERRNO
end


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[AU2_TaskList_Update]  TO [gmtools]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO



CREATE PROCEDURE AU2_TaskidGetServerIP
@taskid int
 AS
declare @serverip varchar(255)
begin
select @serverip=serverip from dbo.au2_boardmessage where taskid=@taskid
if(@serverip='255.255.255.255')
select serverip from dboGMTools_Serverinfo where gameID=2 and descinfo='AU2GameDB'
else
select serverip from dbo.au2_boardmessage where taskid=@taskid
end


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[AU2_TaskidGetServerIP]  TO [gmtools]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO










-- =============================================
-- Author:		<Author,,Name>
-- ALTER  date: <ALTER  Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE [dbo].[AU2_UnBanUser]
@Gm_UserByID int,
@AU2_UserSN int,
@AU2_UserID varchar(256),
@AU2_ServerIP  varchar(256),
@AU2_Reason  varchar(2000)
as
BEGIN
	begin
	     delete from AU2_AutoUnBanUser where BanPlayerAccount=@AU2_UserSN
	end
	declare  @ERRNO int
 	 IF (@@ERROR <> 0)
	BEGIN
		--ROLLBACK TRAN
	insert into GMTools_Log (UserID,SP_Name,Game_Name,ServerIP,Real_Act) values (@Gm_UserByID,'AU2_BanUser','火雞王',@AU2_ServerIP,'解封帳號'+@AU2_UserID+'失敗,原因:'+@AU2_Reason)	
		SELECT @ERRNO = 0
	END
	ELSE
	BEGIN
		--COMMIT TRAN
            	SELECT @ERRNO = 1
	insert into GMTools_Log (UserID,SP_Name,Game_Name,ServerIP,Real_Act) values (@Gm_UserByID,'AU2_BanUser','火雞王',@AU2_ServerIP,'解封帳號'+@AU2_UserID+'成功,原因:'+@AU2_Reason)	
	
	END

	RETURN @ERRNO

END


--substring(a.zoneIP,0,charindex(':',a.zoneIP))=


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[AU2_UnBanUser]  TO [gmtools]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

















--execute Gmtool_GmUserModule_Admin 36,'3,4,5,6,9,8,'

/*********************************************************************************/
/*	SP NAME : Gmtool_GmUserModule_Insert			 */
/*	MOD DATE: 2005-12-28							 */
/*	EDITOR  : lxf							 */
/*-------------------------------------------------------------------------------*/
/*	DESC SP : ALTER  new UserModules			 */
/*-------------------------------------------------------------------------------*/
/*	INPUT   : Not describe							 */
/*	RETURN  : Not describe							 */
/*-------------------------------------------------------------------------------*/
/*	MEMO    : 								 */
/*********************************************************************************/
CREATE           PROC [dbo].[Gmtool_GmUserModule_Admin]
@GM_UserID		int
,@GM_ModuleList		varchar(2000)
--,@ERRNO		INT	OUTPUT
AS
declare  @ERRNO		INT 
declare  @Role_ID	int
declare  @UserID	int
declare  @Module_ID	int
declare  @Game_ID	int

select @Role_ID=Role_ID from  GMTOOLS_Roles where UserID=@GM_UserID   
select @UserID=UserID from  GMTOOLS_Roles where UserID=@GM_UserID   
select @Module_ID=Module_ID from  GMTOOLS_Roles where UserID=@GM_UserID   
select @Game_ID=Game_ID from  GMTOOLS_Roles where UserID=@GM_UserID   
insert into GMTools_Log_UpdateAgo(UserID,SP_Name,Real_Act) values (@GM_UserID,'Gmtool_GmUserModule_Admin','Role_ID_'+convert(varchar(10),@Role_ID)+'__UserID_'+convert(varchar(10),@UserID)+'__Module_ID_'+convert(varchar(30),@Module_ID)+'__Game_ID_'+convert(varchar(10),@Game_ID)+'備份成功')	

declare  @moduleID      INT
declare  @strQuery 	varchar(5000)
declare  @CrsrVar 	Cursor


set @ERRNO = 0
--刪除已不存在的MODULEID
set @strQuery = 'delete from GMTOOLS_Roles where userid='+convert(varchar(5),@GM_UserID)+' and Module_ID not in ('+substring(@GM_ModuleList,0,len(@GM_ModuleList))+')'
execute(@strQuery)
declare @RECNT int
declare @i int
declare @pos int
set @RECNT= 0
set @i =0
set @pos =0
declare @str char(40)
declare @gameID int
while(CHARINDEX(',',@GM_ModuleList,0)>0)
begin
  --得到MODULEID的元素
  set @str = substring(@GM_ModuleList,0,CHARINDEX(',',@GM_ModuleList,0))
  print @str
  --取相反的MODULEID的元素
  set @GM_ModuleList=substring(@GM_ModuleList,CHARINDEX(',',@GM_ModuleList,0)+1,len(@GM_ModuleList)-len(@str))
  print @GM_ModuleList
--判斷表裏是否存在的MODULEID
select @RECNT =count(*) from GMTOOLS_Roles where Userid= convert(char,@GM_UserID) and Module_ID= @str
  --print @RECNT
   if(@RECNT=0)
   begin
	set NoCount ON
	BEGIN TRAN
           
	        select @gameID = game_id from GMTOOLS_Modules where module_id=convert(int,@str)
		INSERT INTO GMTools_Roles				
			(Userid,Module_id,game_ID)
		VALUES
			(@GM_UserID, @str,@gameID)
	
		
		IF (@@ERROR <> 0)
		BEGIN
			ROLLBACK TRAN
			SELECT @ERRNO = 0
		END
		ELSE
		BEGIN
			COMMIT TRAN
			SELECT @ERRNO = 1
		END

     end --沒有這個MODULEID，就往裏面插資料
    select @i=@i+1   


/*SET @CrsrVar = Cursor Forward_Only Static For
	SELECT 
		moduleID 
	FROM
		dbo.GMTOOLS_Roles
OPEN @CrsrVar
FETCH NEXT FROM @CrsrVar
Into @moduleID
WHILE (@@FETCH_STATUS = 0)
begin

    FETCH NEXT FROM @CrsrVar Into @moduleID*/
	
END

RETURN @ERRNO



--exec Gmtool_GmUserModule_Admin 333,'12,32'


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[Gmtool_GmUserModule_Admin]  TO [gmtools]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
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
AS
declare  @ERRNO		INT 
declare @UserID INT
set NoCount ON
	
	BEGIN TRAN
	INSERT INTO GMTools_Users				--寫入用戶表資訊
		(UserName, User_Pwd,RealName,DepartID,User_status,limit,SysAdmin)
	VALUES
		(@Gm_UserName, @Gm_Password,@Gm_RealName,@Gm_DepartID,@Gm_Status,@Gm_LimitTime,@Gm_SysAdmin)

        select @UserID = UserID from GMTools_Users where userName=@Gm_UserName
        
	IF (@@ERROR <> 0)
	BEGIN
		ROLLBACK TRAN

	INSERT INTO GMTools_LogTime			--寫log表 
		(OperateUserID,OperateMsg)
	VALUES
		(@Gm_OperateUserID,'添加用戶名'+@Gm_UserName+'記錄失敗')
		SELECT @ERRNO = 0
	END
	ELSE
	BEGIN

	COMMIT TRAN

	INSERT INTO GMTools_LogTime			--寫log表 
		(OperateUserID,OperateMsg)
	VALUES
		(@Gm_OperateUserID,'添加用戶名'+@Gm_UserName+'記錄成功')
		SELECT @ERRNO = 1
	END
RETURN @ERRNO


















GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[Gmtool_Gminfo_Add]  TO [gmtools]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
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
AS
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
	--insert into GMTools_Log_UpdateAgo(UserID,SP_Name,Real_Act) values (@Gm_OperateUserID,'Gmtool_GAME_Edit','UserID_'+convert(varchar(10),@UserID)+'__UserName_'+@UserName+'__User_MAC_'+@User_MAC+'__RealName_'+@RealName+'__User_Pwd_'+@User_Pwd+'__DepartID_'+convert(varchar(10),@DepartID)+'__User_status_'+convert(varchar(10),@User_status)+'__limit_'+convert(varchar(30),@limit)+'__onlineActive_'+convert(varchar(10),@onlineActive)+'備份成功')	
	insert into GMTools_Log_UpdateAgo(UserID,SP_Name,Real_Act) values (@Gm_OperateUserID,'Gmtool_Gminfo_Del','UserID_'+convert(varchar(10),@UserID)+'__UserName_'+@UserName+'__User_MAC_'+@User_MAC+'__RealName_'+@RealName+'__User_Pwd_'+@User_Pwd+'__DepartID_'+convert(varchar(10),@DepartID)+'__User_status_'+convert(varchar(10),@User_status)+'__limit_'+convert(varchar(30),@limit)+'__onlineActive_'+convert(varchar(10),@onlineActive)+'備份成功')	
	delete from GMTools_Users				--刪除用戶表資訊
	where	UserID = @Gm_UserID
		
	IF (@@ROWCOUNT = 0)
	BEGIN
		ROLLBACK TRAN
	insert into  GMTools_LogTime (OperateUserID,OperateMsg) values (@Gm_OperateUserID,'刪除用戶ID'+Convert(varchar(5),@Gm_UserID)+'記錄失敗')	--記錄log表 
		SELECT @ERRNO = 0
	END
	ELSE
	BEGIN
		COMMIT TRAN
	insert into  GMTools_LogTime (OperateUserID,OperateMsg) values (@Gm_OperateUserID,'刪除用戶ID'+Convert(varchar(5),@Gm_UserID)+'記錄成功')	--記錄log表 
		SELECT @ERRNO = 1
	END
RETURN @ERRNO


--exec Gmtool_Gminfo_Del 21,15












GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[Gmtool_Gminfo_Del]  TO [gmtools]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
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
AS
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
	insert into GMTools_Log_UpdateAgo(UserID,SP_Name,Real_Act) values (@Gm_OperateUserID,'Gmtool_Gminfo_Edit','__RealName_'+@RealName+'__DepartID_'+convert(varchar(10),@DepartID)+'__User_status_'+convert(varchar(10),@User_status)+'__limit_'+convert(varchar(30),@limit)+'__onlineActive_'+convert(varchar(10),@onlineActive)+'備份成功')	

	update GMTools_Users					--修改用戶表資訊
		set realName=@Gm_RealName,DepartID=@Gm_DeptID, limit = @Gm_LimitTime,user_status=@Gm_Status,OnlineActive=@Gm_OnlineActive,sysAdmin=@Gm_SysAdmin
	where	UserID = @Gm_UserID

	IF (@@rowcount = 0)
	BEGIN
		ROLLBACK TRAN
	insert into GMTools_LogTime (operateUserID,operateMsg) values (@Gm_OperateUserID,'修改用戶ID'+Convert(varchar(5),@Gm_UserID)+'的資訊失敗')	
		SELECT @ERRNO = 0
	END
	ELSE
	BEGIN
		COMMIT TRAN
	insert into GMTools_LogTime (operateUserID,operateMsg) values (@Gm_OperateUserID,'修改用戶ID'+Convert(varchar(5),@Gm_UserID)+'的資訊成功')		--記錄log表 
		SELECT @ERRNO = 1
	END
RETURN @ERRNO






GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[Gmtool_Gminfo_Edit]  TO [gmtools]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
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
AS
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
	
		
	insert into GMTools_Log_UpdateAgo(UserID,SP_Name,Real_Act) values (@Gm_OperateUserID,'Gmtool_PASSWD_Edit','UserID_'+convert(varchar(10),@UserID)+'__UserName_'+@UserName+'__User_MAC_'+@User_MAC+'__RealName_'+@RealName+'__User_Pwd_'+@User_Pwd+'__DepartID_'+convert(varchar(10),@DepartID)+'__User_status_'+convert(varchar(10),@User_status)+'__limit_'+convert(varchar(30),@limit)+'__onlineActive_'+convert(varchar(10),@onlineActive)+'備份成功')	
	

	update GMTools_Users					--修改用戶表資訊
		set User_Pwd = @Gm_Password
	where	UserID = @Gm_UserID

	IF (@@rowcount= 0)
	BEGIN
		ROLLBACK TRAN
	insert into GMTools_LogTime (operateUserID,operateMsg) values (@Gm_OperateUserID,'修改用戶ID'+Convert(varchar(5),@Gm_UserID)+'的密碼失敗')	
		SELECT @ERRNO = 0
	END
	ELSE
	BEGIN
		COMMIT TRAN
	insert into GMTools_LogTime (operateUserID,operateMsg) values (@Gm_OperateUserID,'修改用戶ID'+Convert(varchar(5),@Gm_UserID)+'的密碼成功')		--記錄log表 
		SELECT @ERRNO = 1
	END
RETURN @ERRNO


--exec Gmtool_PASSWD_Edit 24,24,'24'


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[Gmtool_PASSWD_Edit]  TO [gmtools]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
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
CREATE        PROC [dbo].Gmtool_UserInfo_Query
@Gm_UserID int
as
        declare @sysAdmin int
        declare @deptID int
        select @sysAdmin = sysAdmin from gmtools_users where userid = @Gm_UserID
      if @sysAdmin = 1
        select a.userID,a.userName,a.realName,b.departName,a.limit,a.SysAdmin from GMTOOLS_Users a,Department b where a.departID=b.departID and a.user_status=1
     else
       begin
        select @deptID = departID from gmtools_users where userid = @Gm_UserID
        select a.userID,a.userName,a.realName,b.departName,a.limit,a.SysAdmin from GMTOOLS_Users a,Department b where a.sysAdmin=0 and a.departID=b.departID and b.departID = @deptID   and a.user_status=1
       end
--execute Gmtool_DepartAdmin_Query 9


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[Gmtool_UserInfo_Query]  TO [gmtools]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO









CREATE procedure ServerInfo_Query
@GM_gameID int,
@GM_gameDBID int

as
begin
  declare @strQuery varchar(700)
  set @strQuery = 'select serverIP,city,gameName from GMTools_Serverinfo where gamedbID='+convert(varchar(10),@GM_gameDBID)+' and gameID='+convert(varchar(10),@GM_gameID)+' and gameflag=1 order by city'
  execute(@strQuery)
  print(@strQuery)
end


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[ServerInfo_Query]  TO [gmtools]
GO

