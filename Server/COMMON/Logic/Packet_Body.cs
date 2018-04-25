 using System;

namespace Common.Logic
{
	
	public enum Body_Status:ushort
	{
		MSG_STRUCT_OK = 0,
		MSG_STRUCT_ERROR = 1,
		ILLEGAL_SOURCE_ADDR = 2,
		AUTHEN_ERROR = 3,
		OTHER_ERROR = 4
	}
		
	/// <summary>
	/// Packet_Body 的摘要说明。
	/// </summary>
	public class Packet_Body
	{
		/// <summary>
		/// TLV结构体
		/// </summary>
		public TLV_Structure tlv;
		/// <summary>
		/// 最大TLV结构体长度
		/// </summary>
		public const uint MAX_TLV_NUM = 20;
		public Body_Status m_Status = Body_Status.MSG_STRUCT_ERROR;  //标示传入的Buffer正确与否等状态
		/// <summary>
		/// 消息体Byte数组
		/// </summary>
		public byte[] m_bBodyBuffer = new byte[0];
		/// <summary>
		/// 消息体Byte长度
		/// </summary>
		public uint m_uiBodyLen = 0;
		/// <summary>
		/// TLV结构体数组
		/// </summary>
		public System.Collections.ArrayList m_TLVList = new System.Collections.ArrayList();
		/// <summary>
		/// TLV结构体长度
		/// </summary>
		public uint m_uiTLVCnt = 0;
		public uint m_uiColCnt = 0;
		
		public Packet_Body()
		{
		}
		/// <summary>
		/// 构造消息体
		/// </summary>
		/// <param name="body">消息体</param>
		/// <param name="body_len">消息体长度</param>
		public Packet_Body( byte[] body, uint body_len ) 
		{
			if (body == null ||body.Length != (int)body_len) return;
			this.m_uiBodyLen = body_len;
			this.m_bBodyBuffer = new byte[this.m_uiBodyLen];
			body.CopyTo(this.m_bBodyBuffer,0);

			this.m_Status = this.init( );
		}
		/// <summary>
		/// 初始化消息体
		/// </summary>
		/// <returns>消息体结构是否合法</returns>
        private Body_Status init( ) 
		{
			this.m_uiTLVCnt = 0;
			uint curIndex = 0;
			while (findTLVStructure(ref curIndex )) 
			{
			}
			return this.initTLV();
			//			if (curIndex != this.m_uiBodyLen)
			//				return Body_Status.MSG_STRUCT_ERROR;
			//			else
			//				return Body_Status.MSG_STRUCT_OK;
		}
        /// <summary>
        /// 通过索引值，生成TLV结构体数组
        /// </summary>
        /// <param name="index">索引值</param>
        /// <returns>操作结构</returns>
		private bool findTLVStructure(ref uint index ) 
		{
			if ( this.m_bBodyBuffer == null ||this.m_bBodyBuffer.Length < index+4)
				return false;
			uint tlvlen = ((uint)this.m_bBodyBuffer[index+2])*255+((uint)this.m_bBodyBuffer[index+3])+4;
			if (index+tlvlen > this.m_bBodyBuffer.Length) return false;
			byte[] tlvbuf = new byte[tlvlen];
			System.Array.Copy(this.m_bBodyBuffer,index,tlvbuf,0,tlvlen);
			TLV_Structure tlv = new TLV_Structure(tlvbuf,tlvlen);
			this.m_TLVList.Add(tlv);
			this.m_uiTLVCnt++;
			index += tlvlen;
			return true;
		}
        /// <summary>
        /// 封装消息时，将TLV结构体加入消息体里面
        /// </summary>
        /// <param name="tlvs">TLV结构体数组</param>
        /// <param name="tlv_cnt">TLV结构体长度</param>
		public Packet_Body( TLV_Structure[] tlvs, uint tlv_cnt )
		{
			if (tlvs == null||tlvs.Length != tlv_cnt) return;
			this.m_uiTLVCnt = tlv_cnt;
			this.m_TLVList.AddRange(tlvs);
			
			this.m_Status = this.initTLV();			
		}
		/// <summary>
		/// 封装消息时，将TLV结构体加入消息体里面
		/// </summary>
		/// <param name="tlvs">TLV结构体数组</param>
		/// <param name="tlv_cnt">TLV结构体长度</param>
		/// <param name="colCnt">TLV结构体列长度</param>
		public Packet_Body( TLV_Structure[] tlvs, uint tlv_cnt,uint colCnt )
		{
			if (tlvs == null||tlvs.Length != tlv_cnt) return;
			this.m_uiTLVCnt = tlv_cnt;
			this.m_TLVList.AddRange(tlvs);
			m_uiColCnt = colCnt;			
			this.m_Status = this.initTLV();			
		}
        /// <summary>
        /// 初始化TLV结构体
        /// </summary>
        /// <returns>TLV结构体是否合法</returns>
		private Body_Status initTLV()
		{
			uint size = 0;
			System.Collections.ArrayList bal = new System.Collections.ArrayList(),lenlist = new System.Collections.ArrayList();
			for (int i = 0;i < this.m_uiTLVCnt;i++)
			{
				TLV_Structure tlv = (TLV_Structure)this.m_TLVList[i];
				if (tlv.IsValidTLV)
				{
					size += tlv.m_uiValueLen + 4;
					bal.Add(tlv.ToByteArray());
					lenlist.Add(tlv.m_uiValueLen + 4);
				}
			}
			this.m_uiBodyLen = size;
			this.m_bBodyBuffer = new byte[size];
			int index = 0;
			for (int i = 0;i < bal.Count;i++)
			{
				((byte[])bal[i]).CopyTo(m_bBodyBuffer,index);
				index += (int)((uint)lenlist[i]);
			}
			return Body_Status.MSG_STRUCT_OK;
		}	
		public byte[] ToByteArray()
		{
			if (this.m_Status != Body_Status.MSG_STRUCT_OK)
				return new byte[0];
			return this.m_bBodyBuffer;
		}
		public override string ToString()
		{
			if (this.m_Status != Body_Status.MSG_STRUCT_OK)
				return "Invalid Packet Body\r\n";
			string sOutput = null;
			for (int i = 0;i < this.m_uiTLVCnt;i++)
				sOutput += ((TLV_Structure)this.m_TLVList[i]).ToString();
			return sOutput;
		}
        /// <summary>
        /// 通过TagName得到TLV结构体
        /// </summary>
        /// <param name="tag">标签值</param>
        /// <returns>TLV结构体</returns>
		public TLV_Structure getTLVByTag(  TagName tag ) 
		{
			for ( int index = 0 ; index < this.m_uiTLVCnt ; index++ )
				if ( tag == ((TLV_Structure)this.m_TLVList[ index ]).m_Tag )
					return (TLV_Structure)this.m_TLVList[ index ];
			return null;
		}
        /// <summary>
        /// 通过索引值得到TLV结构体
        /// </summary>
        /// <param name="index">索引值</param>
        /// <returns>TLV结构体</returns>
		public TLV_Structure getTLVByIndex(  int index ) 
		{
			if ( index >= m_uiTLVCnt || index < 0)
				return null;
			else
				return (TLV_Structure)this.m_TLVList[ index ];
		}
		/// <summary>
		/// 得到消息体正常长度，将多余byte删除
		/// </summary>
		/// <param name="packetBody">消息体</param>
		/// <returns></returns>
		public static byte[] getPacketBody(byte[] packetBody)
		{

			System.Collections.ArrayList dest = new System.Collections.ArrayList();
			for(int i=0;i<packetBody.Length;i++)
			{
				if(packetBody[i]=='\0')
					break;
				dest.Add(packetBody[i]);
			}
			byte[] messageBuffer = new byte[dest.Count];
			for (int i = 0;i < dest.Count;i ++)
				messageBuffer[i] = (byte)dest[i];
			return messageBuffer;
		}

		public uint BodyLength
		{
			get
			{
				return this.m_uiBodyLen;
			}
		}
		public uint TLVCount
		{
			get
			{
				return this.m_uiTLVCnt;
			}
		}
	}
}
