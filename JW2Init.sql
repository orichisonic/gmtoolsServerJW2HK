use gmtools
insert into dbo.department(departname,remark) values('GM管理中心','GM管理中心')
insert into dbo.gamelist(game_name,game_content) values('GM管理工具','GM管理工具')
insert into dbo.gamelist(game_name,game_content) values('JW2','JW2')


insert into dbo.gmtools_modules(game_id,module_Name,module_class,module_content) values('1','用戶管理','AccountList','用戶管理')
insert into dbo.gmtools_modules(game_id,module_Name,module_class,module_content) values('1','遊戲管理','GameList','遊戲管理')
insert into dbo.gmtools_modules(game_id,module_Name,module_class,module_content) values('1','模組管理','ModuleList','模組管理')
insert into dbo.gmtools_modules(game_id,module_Name,module_class,module_content) values('2','用戶資料查詢','FrmJW2UserInfo','用戶資料查詢')
insert into dbo.gmtools_modules(game_id,module_Name,module_class,module_content) values('2','強制玩家下線','FrmGDKickPlayer','強制玩家下線')
insert into dbo.gmtools_modules(game_id,module_Name,module_class,module_content) values('2','玩家帳號解/封停','FrmGDBanPlayer','玩家帳號解/封停')
insert into dbo.gmtools_modules(game_id,module_Name,module_class,module_content) values('2','玩家日志信息','FrmJW2PlayerLog','玩家日志信息')
insert into dbo.gmtools_modules(game_id,module_Name,module_class,module_content) values('2','公告管理','FrmGDNoticeManage','公告管理')


insert into dbo.gmtools_roles(userid,module_id,game_id) values('1','1','1')
insert into dbo.gmtools_roles(userid,module_id,game_id) values('1','2','1')
insert into dbo.gmtools_roles(userid,module_id,game_id) values('1','3','1')
insert into dbo.gmtools_roles(userid,module_id,game_id) values('1','4','2')
insert into dbo.gmtools_roles(userid,module_id,game_id) values('1','5','2')
insert into dbo.gmtools_roles(userid,module_id,game_id) values('1','6','2')
insert into dbo.gmtools_roles(userid,module_id,game_id) values('1','7','2')
insert into dbo.gmtools_roles(userid,module_id,game_id) values('1','8','2')



insert into gmtools_users(username,user_pwd,realname,departid,user_status,limit,onlineactive,sysadmin) values('admin','C4-CA-42-38-A0-B9-23-82-0D-CC-50-9A-6F-75-84-9B','GM管理員','1','1','2012-10-20','0','1')
insert into gmtools_serverinfo(serverip,city,gameID,gamename,gameflag,gamedbid,descinfo) values('222.73.124.66,3306','華東一區','2','JW2','1','1','GameDB-RE')
insert into gmtools_serverinfo(serverip,city,gameID,gamename,gameflag,gamedbid,descinfo) values('222.73.124.66,3306','華東一區','2','JW2','1','2','ItemDB-RE')
insert into gmtools_serverinfo(serverip,city,gameID,gamename,gameflag,gamedbid,descinfo) values('222.73.124.66,3306','華東一區','2','JW2','1','3','LoginDB-RE')
insert into gmtools_serverinfo(serverip,city,gameID,gamename,gameflag,gamedbid,descinfo) values('222.73.124.66,3306','華東一區','2','JW2','1','4','LogDB-RE')
insert into gmtools_serverinfo(serverip,city,gameID,gamename,gameflag,gamedbid,descinfo) values('222.73.124.66','華東一區','2','JW2','1','5','GateWay')
insert into gmtools_serverinfo(serverip,city,gameID,gamename,gameflag,gamedbid,descinfo) values('222.73.124.66,3306','華東一區','2','JW2','1','6','Messenger-RE')
insert into gmtools_serverinfo(serverip,city,gameID,gamename,gameflag,gamedbid,descinfo) values('222.73.124.66,3306','華東一區','2','JW2','1','7','GameDB-Master')
insert into gmtools_serverinfo(serverip,city,gameID,gamename,gameflag,gamedbid,descinfo) values('222.73.124.66,3306','華東一區','2','JW2','1','8','Logindb-Master')
insert into gmtools_serverinfo(serverip,city,gameID,gamename,gameflag,gamedbid,descinfo) values('222.73.124.66,3306','華東一區','2','JW2','1','9','Messenger-Master')
insert into gmtools_serverinfo(serverip,city,gameID,gamename,gameflag,gamedbid,descinfo) values('222.73.124.66,3306','華東一區','2','JW2','1','10','Itemdb-Master')
insert into gmtools_serverinfo(serverip,city,gameID,gamename,gameflag,gamedbid,descinfo) values('222.73.124.66,3306','華東一區','2','JW2','1','11','Logdb-Master')



insert into sqlexpress(sql_type,sql_field,sql_statement,sql_condition) values('JW2_ACCOUNT_QUERYBYACCOUNT_bak','JW2帳戶查詢by account','select a.UserSn as jw2_usersn,a.UserNick as jw2_usernick,a.UserID as jw2_userid,a.Gender as jw2_sex,a.UserSn as jw2_lastlogindate,b.level as jw2_level,b.exp AS jw2_exp,b.cash as jw2_cash,b.money as jw2_money,goldenCoin AS LORD_Gold,CASE WHEN g.exp IS NULL THEN 0 ELSE g.exp END AS f_levelname,(ConsumeRepute+LevelRepute+FamilyRepute+VideoRepute+HouseLevelRepute+PetLevelRepute+PartnerLevelRepute+OnlineRepute+RPGRepute+FriendRepute) as jw2_repute,d.nowTitle as jw2_nowtitle,a.UserSn as jw2_registdate, case when e.ActivePoint is null then '0' else convert (e.ActivePoint,char(20)) end as jw2_activepoint , a.UserSn as jw2_frmLove  from game_db.userinfo a left join game_db.userextendinfo b on b.usersn = a.usersn left join game_db.userrepute c on c.usersn = a.usersn left join game_db.usertitle d on d.usersn = a.usersn left join game_db.user_active e on e.usersn = a.usersn LEFT JOIN game_db.garden_info g ON g.usersn = a.usersn where a.usersn=b.usersn and a.usersn=c.usersn and  a.userid=''{0}''','JW2_ACCOUNT_QUERYBYACCOUNT_bak')

insert into sqlexpress(sql_type,sql_field,sql_statement,sql_condition) values('JW2_RPG_QUERY','查看故事情節','select usersn as jw2_usersn,chapter as jw2_chapter,stage0 as jw2_stage0,stage1 as jw2_stage1,stage2 as jw2_stage2,stage3 as jw2_stage3,stage4 as jw2_stage4,stage5 as jw2_stage5,stage6 as jw2_stage6,stage7 as jw2_stage7,stage8 as jw2_stage8,stage9 as jw2_stage9,stage10 as jw2_stage10,stage11 as jw2_stage11,stage12 as jw2_stage12,stage13 as jw2_stage13,stage14 as jw2_stage14,stage15 as jw2_stage15,stage16 as jw2_stage16,stage17 as jw2_stage17,stage18 as jw2_stage18,stage19 as jw2_stage19,curstage as jw2_curstage,maxstage as jw2_maxstage from item_db.rpg where usersn={0}','JW2_RPG_QUERY')

insert into sqlexpress(sql_type,sql_field,sql_statement,sql_condition) values('JW2_ITEMSHOP_BYOWNER_QUERY','查看玩家身上道具信息回應（7－7）','select usersn as jw2_usersn,avataritem as jw2_avataritem,avataritem as jw2_avataritemname,convert(expiredate,char(20))  as jw2_expiredate, useitem as jw2_useitem,RemainCount as jw2_remaincount from item_db.avataritemlist  where usersn={0} and avataritem>=1 and avataritem<=100000','JW2_ITEMSHOP_BYOWNER_QUERY')

insert into sqlexpress(sql_type,sql_field,sql_statement,sql_condition) values('JW2_HOME_ITEM_QUERY','查看房間物品清單與期限（7－9）','select usersn as jw2_usersn,avataritem as jw2_avataritem,avataritem as jw2_avataritemname,convert(expiredate,char(20))  as jw2_expiredate, useitem as jw2_useitem,RemainCount as jw2_remaincount from item_db.avataritemlist  where usersn={0} and avataritem>=760000 and avataritem<=799999','JW2_HOME_ITEM_QUERY')

insert into sqlexpress(sql_type,sql_field,sql_statement,sql_condition) values('JW2_ACCOUNT_CLOSE','封停帳號','delete  from notallowedsnlist where bansn={0};insert into login_db.notallowedsnlist value ({0})','JW2_ACCOUNT_CLOSE')

insert into sqlexpress(sql_type,sql_field,sql_statement,sql_condition) values('JW2_ACCOUNT_BANISHMENT_QUERY_ALL','玩家封停帳號查詢_所有','select distinct  BanPlayerAccount as jw2_usersn,BanPlayerUserNick as jw2_usernick,BanPlayerUserId as jw2_userid,city as jw2_serverip,BanReason as jw2_reason from JW2_AutoUnBanUser a, gmtools_serverinfo b where a.serverip=b.serverip  and a.serverip=''{0}''','JW2_ACCOUNT_BANISHMENT_QUERY_ALL')

insert into sqlexpress(sql_type,sql_field,sql_statement,sql_condition) values('JW2_ACCOUNT_BANISHMENT_QUERY','玩家封停帳號查詢','select distinct  BanPlayerAccount as jw2_usersn,convert(varchar(50),BanPlayerUserNick) as jw2_usernick,BanPlayerUserId as jw2_userid,city as jw2_serverip,BanReason as jw2_reason from JW2_AutoUnBanUser a, gmtools_serverinfo b where a.serverip=b.serverip and BanPlayerUserId=''{0}'' and a.serverip=''{1}''','JW2_ACCOUNT_BANISHMENT_QUERY')

insert into sqlexpress(sql_type,sql_field,sql_statement,sql_condition) values('JW2_ACCOUNT_OPEN','解封帳號','delete from login_db.notallowedsnlist where banSn={0}','JW2_ACCOUNT_OPEN')

insert into sqlexpress(sql_type,sql_field,sql_statement,sql_condition) values('jw2_PicCard_Query','圖片卡使用情況','select usersn as jw2_usersn,usersn as jw2_senduser,itemid as jw2_avataritem,itemid as jw2_avataritemname,convert(useTime,char(20)) as Magic_Dates from item_db.game_item_use_info  where itemid=30001647 and usersn={0} and useTime>''{1}''and useTime<''{2}''','jw2_PicCard_Query')

insert into sqlexpress(sql_type,sql_field,sql_statement,sql_condition) values('jw2_Garden_Log_Query','精品店物品购买日志','select buysn as jw2_buysn,'金币' as jw2_type,userSN as jw2_usersn,userSN as jw2_senduser,recesn as jw2_receid,recesn as jw2_recuser,goodsindex as jw2_avataritem,goodsindex as jw2_productname,goodsindex as jw2_buylimitday,GoodsPrice as JW2_GOODSPRICE,beforeMoney as jw2_beforemoney, afterMoney as jw2_aftermoney,convert(buydate,char(20)) as jw2_time from log_db.buylog where usersn={0} and (goodstype='C' )  and buydate>=''{1}'' and buydate<=''{2}''','1')

insert into sqlexpress(sql_type,sql_field,sql_statement,sql_condition) values('jw2_Garden_Log_Query','栏位购买日志','SELECT CASE TYPE WHEN 16 THEN 'M币' WHEN 17 THEN '金币' ELSE '其他' END  AS jw2_type,userSN AS jw2_usersn,userSN AS jw2_senduser,convert((beforeCash- afterCash),char(20)) AS JW2_GOODSPRICE,beforeCash AS jw2_beforemoney ,afterCash AS jw2_aftermoney,CONVERT(DATE,CHAR(20)) AS jw2_time  FROM cashmoney_log WHERE usersn ={0} AND (TYPE='16' OR TYPE='17') AND date>=''{1}'' AND date<=''{2}''','2')

insert into sqlexpress(sql_type,sql_field,sql_statement,sql_condition) values('jw2_Garden_Log_Query','化肥购买日志','select buysn as jw2_buysn,goodstype  as jw2_type,userSN as jw2_usersn,userSN as jw2_senduser,recesn as jw2_receid,recesn as jw2_recuser,goodsindex as jw2_avataritem,goodsindex as jw2_productname,goodsindex as jw2_buylimitday,GoodsPrice as JW2_GOODSPRICE,beforeMoney as jw2_beforemoney, afterMoney as jw2_aftermoney,convert(buydate,char(20)) as jw2_time from log_db.buylog where usersn={0}  and buydate>=''{1}'' and buydate<=''{2}'' and (goodsindex like '%30041800%' or goodsindex like '%30041801%'or goodsindex like '%30041802%' or goodsindex like '%30041803%')','3')

insert into sqlexpress(sql_type,sql_field,sql_statement,sql_condition) values('jw2_Garden_Log_Query','种子购买日志','select buysn as jw2_buysn,goodstype AS jw2_type,userSN as jw2_usersn,userSN as jw2_senduser,recesn as jw2_receid,recesn as jw2_recuser,goodsindex as jw2_avataritem,goodsindex as jw2_productname,goodsindex as jw2_buylimitday,GoodsPrice as JW2_GOODSPRICE,beforeMoney as jw2_beforemoney, afterMoney as jw2_aftermoney,convert(buydate,char(20)) as jw2_time from log_db.buylog where usersn={0}  and buydate>='{1}' and buydate<='{2}' and ((goodsindex>130041000 and goodsindex<130041471) or(goodsindex>230041000 and goodsindex<230041471) or(goodsindex>330041000 and goodsindex<330041471) or(goodsindex>430041000 and goodsindex<430041471))','4')

insert into sqlexpress(sql_type,sql_field,sql_statement,sql_condition) values('JW2_UserSn_Account','账号ID转帐号','select userID from game_db.userinfo where usersn={0}','JW2_UserSn_Account')