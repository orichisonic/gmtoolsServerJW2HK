#pragma once

#ifdef DSNSHELL_EXPORTS
#define DSNSHELL_API __declspec(dllexport)
#else
#define DSNSHELL_API __declspec(dllimport)
#endif

class INormalTimer;
class INetHandler;
class CNetPacket;

enum DSNSHELL_STATECODE
{
	DSNSHELL_OK,

	DSNSHELL_NOTCREATE,

	DSNSHELL_NOTCONNECT,
	DSNSHELL_CONNECTINGNOW,
	DSNSHELL_CONNECTFAIL,

	DSNSHELL_UNKNOWN,

	DSNSHELL_BUOK = -1,
};

enum DSNSHELL_MSGIDX
{
	MSGIDX_LOGINGW = 1,
	MSGIDX_SERVERLIST,

	MSGIDX_LOGINGS,
	MSGIDX_CHANNELLIST,
	MSGIDX_ENTERCHANNEL,

	MSGIDX_USERLIST,
	MSGIDX_ROOMLIST,
	MSGIDX_ROOMDETAIL,
	MSGIDX_CHAT,
	MSGIDX_WHISPER,

	MSGIDX_ROOMCREATED,
	MSGIDX_ROOMDESTROYED,
	MSGIDX_USERENTERCHANNEL,
	MSGIDX_USERLEAVECHANNEL,
	MSGIDX_LABA,

	MSGIDX_ROOMSLOTLIST,
};

#pragma pack(push)
#pragma pack(1)

//Update函数的返回值
struct UPDATE_RET
{
	unsigned char m_ucMsgCount;				//本次Update函数调用共收到多少条消息
	unsigned char m_szMsgIdx[255];			//本次Update函数调用收到的消息ID，对应枚举类型DSNSHELL_MSGIDX

	UPDATE_RET();
	void Clear(void);
	void AddMsg(unsigned char idx);
};

//登陆GW的返回消息
struct LOGINGW_RET
{
	unsigned char m_ucRetval;				//返回值，如无特殊说明，均是0为正确，其他值为错误，下同
	TCHAR m_szNickName[255];				//用户昵称
	unsigned long m_ulSerialNo;				//用户序号（登陆GS要用的）
	unsigned char m_ucGTownExist;			//GTown是否存在
};

//一个GS的信息
struct SERVER_INFO
{
	unsigned short m_usServerNo;			//GS序号
	TCHAR m_szServerName[255];				//GS名称
	TCHAR m_szServerIp[255];				//IP
	unsigned short m_usServerPort;			//端口
	unsigned short m_usGrade;				//GS等级（好像没用）
	unsigned char m_ucRate;					//GS内玩家比率
};

//请求GS列表的返回消息
struct SERVERLIST_RET
{
	unsigned char m_ucRetval;
	unsigned short m_usServerCount;			//GS数量
	SERVER_INFO m_szServerList[10];			//每个GS的信息
};

//一个频道的信息
struct CHANNEL_INFO
{
	unsigned char m_ucChannelNo;			//频道序号
	TCHAR m_szChannelName[255];				//频道名
	unsigned short m_usCurrentPlayer;		//当前玩家数量
	unsigned short m_usMaxPlayer;			//允许进入的最大玩家数量
	unsigned short m_usMinLevel;			//允许进入的等级下限
	unsigned short m_usMaxLevel;			//允许进入的等级上限
	unsigned char m_ucEventNum;				//不知道
};

//请求频道列表的返回消息
struct CHANNELLIST_RET
{
	unsigned char m_ucRetval;				//2表示列表接收完毕
	unsigned short m_usChannelCount;			//频道数量
	CHANNEL_INFO m_szChannelInfo[25];		//频道信息
};

//一个用户的信息
struct USER_INFO
{
	TCHAR m_szNickName[255];				//昵称
	TCHAR m_szAccount[255];					//帐号
	short m_sRoomNo;						//房间号，-1为没房间
};

//请求用户列表的返回消息
struct USERLIST_RET
{
	unsigned char m_ucRetval;				//2表示列表接收完毕
	unsigned short m_usUserCount;			//用户数量
	USER_INFO m_szUserInfo[512];			//用户信息
};

//一个房间的信息
struct ROOM_INFO
{
	unsigned short m_usRoomNo;				//房间号
	TCHAR m_szRoomName[255];				//房间名
};

//请求房间列表的返回消息
struct ROOMLIST_RET
{
	unsigned char m_ucRetval;				//2表示列表接收完毕
	unsigned short m_usRoomCount;			//房间数量
	ROOM_INFO m_szRoomInfo[512];			//房间信息
};

//一个房间的详细信息
struct ROOMDETAIL_RET
{
	unsigned char m_ucRetval;
	unsigned short m_usRoomNo;				//房间号
	TCHAR m_szRoomName[255];				//房间名
	TCHAR m_szNickName[18][255];			//昵称
};

//房间内一个slot的信息
struct ROOMSLOT
{
	unsigned char m_ucStatus;				//状态（0没人，1关闭，2有人，3NPC）
	TCHAR m_szNickName[255];				//昵称
};

//房间slot信息列表
struct ROOMSLOTLIST_RET
{
	unsigned char m_ucRetval;				//2表示接收完毕
	ROOMSLOT m_SlotList[19];				//每个slot的信息（0~18，0~7游戏位置，8保留，9~18旁观位置）
};

#pragma pack(pop)

class CDSNShell
{
protected:

	INormalTimer* m_pNormalTimer;

	UPDATE_RET m_UpdateRetVal;

	INetHandler* m_pNetHandlerGW;
	int m_nNetStateGW;
	unsigned long m_ulConnectTimeGW;
	
	queue<LOGINGW_RET> m_qLoginGWRet;
	queue<SERVERLIST_RET> m_qServerListRet;

	INetHandler* m_pNetHandlerGS;
	int m_nNetStateGS;
	unsigned long m_ulConnectTimeGS;

	queue<unsigned char> m_qLoginGSRet;
	
	bool m_bReceivingChannelList;
	CHANNELLIST_RET m_ChannelListTmp;
	unsigned short m_usChannelListIdx;
	queue<CHANNELLIST_RET> m_qChannelListRet;

	queue<unsigned char> m_qEnterChannelRet;

	bool m_bReceivingUserList;
	USERLIST_RET m_UserListTmp;
	unsigned short m_usUserListIdx;
	queue<USERLIST_RET> m_qUserListRet;

	bool m_bReceivingRoomList;
	ROOMLIST_RET m_RoomListTmp;
	unsigned short m_usRoomListIdx;
	queue<ROOMLIST_RET> m_qRoomListRet;
	queue<ROOMDETAIL_RET> m_qRoomDetailRet;
	queue<tstring> m_qChatRet;
	queue<tstring> m_qWhisperRet;

	queue<ROOM_INFO> m_qRoomCreated;
	queue<unsigned short> m_qRoomDestroyed;
	queue<USER_INFO> m_qUserEnter;
	queue<tstring> m_qUserLeave;
	queue<tstring> m_qLaba;

	bool m_bReceivingRoomSlotList;
	ROOMSLOTLIST_RET m_RoomSlotListTmp;
	queue<ROOMSLOTLIST_RET> m_qRoomSlotListRet;

public:

	CDSNShell();
	~CDSNShell();

	int Update(UPDATE_RET& ret);

	int ConnectGW(LPCTSTR szIPAddress, int nPort);
	int IsGWConnected(void);

	int LoginGW(LPCTSTR szAccount, LPCTSTR szPassword, LPCTSTR szVersion);
	int LoginGWRet(LOGINGW_RET& ret);
	int GetServerList(void);
	int ServerListRet(SERVERLIST_RET& ret);

	int SendBulletin(LPCTSTR szContent);
	int KickUser(LPCTSTR szAccount);

	int ConnectGS(LPCTSTR szIPAddress, int nPort);
	int IsGSConnected(void);

	int LoginGS(unsigned long ulSerialNo);
	int LoginGSRet(unsigned char& ret);
	int GetChannelList(void);
	int ChannelListRet(CHANNELLIST_RET& ret);
	int EnterChannel(unsigned char ucChannelNo);
	int EnterChannelRet(unsigned char& ret);

	int GetUserList(void);
	int UserListRet(USERLIST_RET& ret);
	int GetFriendList(void);
	int GetRoomList(void);
	int RoomListRet(ROOMLIST_RET& ret);
	int GetRoomDetail(unsigned short usRoomNo);
	int RoomDetailRet(ROOMDETAIL_RET& ret);
	int Chat(LPCTSTR szWord);
	int ChatRet(LPTSTR szWord);
	int Whisper(LPCTSTR szNick, LPCTSTR szWord);
	int WhisperRet(LPTSTR szWord);
	int SetGPointTimes(unsigned char times);
	int SetExpTimes(unsigned char times);
	int JinYan(LPCTSTR szNick, unsigned char type, unsigned char flag);

	int RoomCreated(ROOM_INFO& room);
	int RoomDestroyed(unsigned short& usRoomNo);
	int UserEnterChannel(USER_INFO& user);
	int UserLeaveChannel(LPTSTR szNick);
	int GetLaba(LPTSTR szWord);

	int EnterRoom(unsigned short usRoomNo);
	int LeaveRoom(void);
	int GetRoomSlotList(void);
	int RoomSlotListRet(ROOMSLOTLIST_RET& ret);
	int ChangeSlot(unsigned char ucSlot);
	int ChangeRoomName(LPCTSTR szName);

protected:

	void ParseMsgGW(CNetPacket& msg);
	void ParseMsgGS(CNetPacket& msg);
};

//创建DSNShell（返回值为DSNShell的句柄）
extern "C" DSNSHELL_API int CreateDSNShell(void);
//销毁DSNShell（句柄）
extern "C" DSNSHELL_API void DestroyDSNShell(int handle);
//更新DSNShell（句柄，收到消息的标志）
extern "C" DSNSHELL_API int DSNShellUpdate(int handle, UPDATE_RET& ret);

//连接GW（句柄，IP，端口）
extern "C" DSNSHELL_API int DSNShellConnectGW(int handle, LPCTSTR szIPAddress, int nPort);
extern "C" DSNSHELL_API int DSNShellIsGWConnected(int handle);

//登陆GW（句柄，帐号，密码，版本号）
extern "C" DSNSHELL_API int DSNShellLoginGW(int handle, LPCTSTR szAccount, LPCTSTR szPassword, LPCTSTR szVersion);
extern "C" DSNSHELL_API int DSNShellLoginGWRet(int handle, LOGINGW_RET& ret);
//取得GS列表（句柄）
extern "C" DSNSHELL_API int DSNShellServerList(int handle);
extern "C" DSNSHELL_API int DSNShellServerListRet(int handle, SERVERLIST_RET& ret);

//发公告（句柄，公告内容）
extern "C" DSNSHELL_API int DSNShellSendBulletin(int handle, LPCTSTR szContent);
//踢人（句柄，帐号）
extern "C" DSNSHELL_API int DSNShellKickUser(int handle, LPCTSTR szAccount);

//连接GS（句柄，IP，端口）
extern "C" DSNSHELL_API int DSNShellConnectGS(int handle, LPCTSTR szIPAddress, int nPort);
extern "C" DSNSHELL_API int DSNShellIsGSConnected(int handle);

//登陆GS（句柄，用户序号）
extern "C" DSNSHELL_API int DSNShellLoginGS(int handle, unsigned long ulSerialNo);
extern "C" DSNSHELL_API int DSNShellLoginGSRet(int handle, unsigned char& ret);
//取得频道列表（句柄）
extern "C" DSNSHELL_API int DSNShellChannelList(int handle);
extern "C" DSNSHELL_API int DSNShellChannelListRet(int handle, CHANNELLIST_RET& ret);
//进入频道（句柄，频道序号）
extern "C" DSNSHELL_API int DSNShellEnterChannel(int handle, unsigned char ucChannelNo);
extern "C" DSNSHELL_API int DSNShellEnterChannelRet(int handle, unsigned char& ret);

//取得频道内用户列表（句柄）
extern "C" DSNSHELL_API int DSNShellUserList(int handle);
extern "C" DSNSHELL_API int DSNShellUserListRet(int handle, USERLIST_RET& ret);
//取得好友列表（句柄）
extern "C" DSNSHELL_API int DSNShellFriendList(int handle);
//取得频道内房间列表（句柄）
extern "C" DSNSHELL_API int DSNShellRoomList(int handle);
extern "C" DSNSHELL_API int DSNShellRoomListRet(int handle, ROOMLIST_RET& ret);
//取得房间详细信息（句柄，房间序号）
extern "C" DSNSHELL_API int DSNShellRoomDetail(int handle, unsigned short usRoomNo);
extern "C" DSNSHELL_API int DSNShellRoomDetailRet(int handle, ROOMDETAIL_RET& ret);
//聊天（句柄，聊天内容）
extern "C" DSNSHELL_API int DSNShellChat(int handle, LPCTSTR szWord);
extern "C" DSNSHELL_API int DSNShellChatRet(int handle, LPTSTR szWord);
//私聊（句柄，昵称，聊天内容）
extern "C" DSNSHELL_API int DSNShellWhisper(int handle, LPCTSTR szNick, LPCTSTR szWord);
extern "C" DSNSHELL_API int DSNShellWhisperRet(int handle, LPTSTR szWord);
//设置当前GS的G币倍数（句柄，倍数——仅限1和2）
extern "C" DSNSHELL_API int DSNShellSetGPointTimes(int handle, unsigned char times);
//设置当前GS的经验倍数（句柄，倍数——仅限1和2）
extern "C" DSNSHELL_API int DSNShellSetExpTimes(int handle, unsigned char times);
//禁言设置（句柄，昵称，禁言类型——0聊天1小喇叭2大喇叭3横幅，标志——0禁止1开放）
extern "C" DSNSHELL_API int DSNShellJinYan(int handle, LPCTSTR szNick, unsigned char type, unsigned char flag);

//广播，创建房间（句柄）
extern "C" DSNSHELL_API int DSNShellRoomCreated(int handle, ROOM_INFO& room);
//广播，房间没了（句柄）
extern "C" DSNSHELL_API int DSNShellRoomDestroyed(int handle, unsigned short& usRoomNo);
//广播，有人进来（句柄）
extern "C" DSNSHELL_API int DSNShellUserEnterChannel(int handle, USER_INFO& user);
//广播，有人逃走（句柄）
extern "C" DSNSHELL_API int DSNShellUserLeaveChannel(int handle, LPTSTR szNick);
//广播，收到喇叭（句柄）
extern "C" DSNSHELL_API int DSNShellGetLaba(int handle, LPTSTR szWord);

//进入房间（句柄，房间序号）
extern "C" DSNSHELL_API int DSNShellEnterRoom(int handle, unsigned short usRoomNo);
//退出房间（句柄）
extern "C" DSNSHELL_API int DSNShellLeaveRoom(int handle);
//取房间内slot信息（句柄）
extern "C" DSNSHELL_API int DSNShellRoomSlotList(int handle);
//这条返回消息在刚进房间的时候会直接收到
extern "C" DSNSHELL_API int DSNShellRoomSlotListRet(int handle, ROOMSLOTLIST_RET& ret);
//改变slot状态（句柄，slot——有人就踢人变成没人/没人变成关闭/关闭变成没人）
extern "C" DSNSHELL_API int DSNShellChangeSlot(int handle, unsigned char ucSlot);
//改变房间名称（句柄，房间名）
extern "C" DSNSHELL_API int DSNShellChangeRoomName(int handle, LPCTSTR szName);