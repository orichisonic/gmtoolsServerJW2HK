using System;
using Common.API;
using lg = Common.API.LanguageAPI;

namespace Common.Logic
{
	/// <summary>
	/// UserValidate 的摘要说明。
	/// </summary>
	public class UserValidate 
	{
		private int userByID = 0;
		private string userName ;
		private string password ;
		private string mac;
		private int status;
		public UserValidate()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public UserValidate(string user_,string passwd_,string mac_)
		{
			this.UserName = user_;
			this.PassWord = passwd_;
			this.MAC = mac_;
		}
		public void parseMsg(byte[] mesPacket)
		{
			Message msg  = null;
			Packet packet = null;
			Packet_Head mesHead = null;
			Packet_Body mesBody = null;
			try
			{
				//Message.Common_ACCOUNT_AUTHOR_RESP(
				msg = new Message(mesPacket,Convert.ToUInt32(mesPacket.Length));
				msg.GetMessageID();
				packet = msg.m_packet;
				mesHead = new Packet_Head(packet.m_Head.m_bHeadBuffer,packet.m_Head.m_uiHeadBufferLen);
				mesBody = new Packet_Body(packet.m_Body.m_bBodyBuffer,packet.m_Body.m_uiBodyLen);
				string wiriter = System.Text.Encoding.Default.GetString(mesBody.getTLVByTag(TagName.MAC).m_bValueBuffer);
				Console.WriteLine(wiriter);

				
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.GMMsg_ErrMessage);
			}

		}
		public Message validateUser()
		{
			int result = -1;
			UserInfoAPI api = new UserInfoAPI();
			if(0==api.GM_ActiveUser(this.userName,this.PassWord,this.MAC))
			{
				result = api.GM_ValidateUser(this.UserName,this.PassWord,this.MAC);
				this.UserByID = result;
				if(result>0)
				{
					Console.WriteLine(lg.Logic_UserValidate_User+UserName+lg.Logic_UserValidate_AcceptData);
					this.Status = 1;
					//设置当前用户上线状态
					api.GM_UpdateActiveUser(result,1);
					//发送成功验证信息
					return Message.Common_ACCOUNT_AUTHOR_RESP(result,"PASS");
				}
				else
				{
					Console.WriteLine(lg.Logic_UserValidate_User+UserName+lg.Logic_UserValidate_ValidateFailue);
					this.Status = 0;
					return Message.Common_ACCOUNT_AUTHOR_RESP(result,"FAILURE");
				}
			}
			else
			{
				Console.WriteLine(lg.Logic_UserValidate_User+UserName+lg.Logic_UserValidate_LoggingFailue);
				this.Status = 0;
				return Message.Common_ACCOUNT_AUTHOR_RESP(result,"MISS");
			}
            
		}
		public Message activeUser()
		{
			int result = -1;
			UserInfoAPI api = new UserInfoAPI();
			result = api.GM_ValidateUser(this.UserName,this.password,this.mac);
			if(result>0)
			{
				Console.WriteLine(lg.Logic_UserValidate_User+UserName+lg.Logic_UserValidate_AcceptData);
				this.Status = 1;
				return Message.Common_ACCOUNT_AUTHOR_RESP(result,"PASS");
			}
			else
			{
				Console.WriteLine(lg.Logic_UserValidate_User+UserName+lg.Logic_UserValidate_ValidateFailue);
				this.Status = 0;
				return Message.Common_ACCOUNT_AUTHOR_RESP(result,"FAILURE");
			}

		}
        public static object validData(object value)   
        {
            object var = null;
			if (value.GetType() == typeof(string))
			{
				if (value != null && value.ToString().Length > 0)
				{
					var = value;
				}
				else
				{
					value = "";
				}
			}
			else if (value.GetType() == typeof(int))
			{
				if (value != null)
				{
					var = value;
				}
				else
				{
					value = 0;
				}
			}
			else
			{
				var = "";
			}
            return var;

        }
		public int UserByID
		{
			get
			{
				return this.userByID;
			}
			set
			{
				this.userByID = value;
			}
		}
		public string UserName
		{
			get
			{
				return this.userName;
			}
			set
			{
				this.userName=value;
			}
		}
		public string PassWord
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password=value;
			}
		}
		public string MAC
		{
			
			get
			{
				return this.mac;
			}
			set
			{
				this.mac=value;
			}
		    
		}
		public int Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

	}
}
