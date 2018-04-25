 using System;
using lg = Common.API.LanguageAPI;

namespace Common.Logic
{
	
	/// <summary>
	/// TLV_Structure 的摘要说明。
	/// </summary>
	public class TLV_Structure
	{
		/// <summary>
		/// TLV长度
		/// </summary>
		public const int MAX_TLV_LENGTH = 50;
		/// <summary>
		/// 标签名
		/// </summary>
		public TagName m_Tag;
        /// <summary>
        /// 标签格式
        /// </summary>
        public TagFormat m_TagFormat;
		/// <summary>
		/// 值的长度
		/// </summary>
		public uint m_uiValueLen;
		/// <summary>
		/// 值的字节数组
		/// </summary>
		public byte[] m_bValueBuffer;
		/// <summary>
		/// TLV结构体是否合法
		/// </summary>
		public bool IsValidTLV = false;

		public  TLV_Structure()
		{
		}
		/// <summary>
		/// 构造TLV结构
		/// </summary>
		/// <param name="tag">标签</param>
		/// <param name="len">长度</param>
		/// <param name="val">值</param>
		public TLV_Structure(TagName tag,uint len,byte[] val ) 
		{
			this.m_Tag = tag;
			this.m_uiValueLen = len;
			this.m_bValueBuffer = new byte[this.m_uiValueLen];
			this.init(len,val);
		}

        /// <summary>
        /// 构造TLV结构
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="len">长度</param>
        /// <param name="val">值</param>
        public TLV_Structure(TagName tag, TagFormat format,uint len, byte[] val)
        {
            this.m_Tag = tag;
            this.m_TagFormat = format;
            this.m_uiValueLen = len;
            this.m_bValueBuffer = new byte[this.m_uiValueLen];
            this.init(len, val);
        }
        /// <summary>
        /// 构造TLV结构体
        /// </summary>
        /// <param name="tlv">TLV结构体</param>
        /// <param name="len">长度</param>
		public TLV_Structure(  byte[] tlv,  uint len) //返回整个TLV的长度
		{
			if ( tlv == null || tlv.Length < 4 )
				return ;
			try
			{
				this.m_Tag = (TagName)( tlv[ 0 ] + ( tlv[ 1 ] << 8 ) );
				this.m_uiValueLen = (uint)tlv[ 2 ]*255+(uint)tlv[3];
				this.m_bValueBuffer = new byte[this.m_uiValueLen];
				
				byte[] val = new byte[tlv.Length - 4];
				System.Array.Copy(tlv,4,val,0,tlv.Length - 4);

				this.init((uint)tlv.Length - 4,val);
			}
			catch(System.Exception)
			{
                
			}
		}
		/// <summary>
		/// 初始化TLV结构体
		/// </summary>
		/// <param name="len">长度</param>
		/// <param name="val">值</param>
		private void init(uint len,byte[] val)
		{
			//if (!this.CheckTagLength())
				//return;

			if (val == null)
			{
				if (len == 0)
					this.IsValidTLV = true;
			}
			else if (len < val.Length)
				System.Array.Copy(val,0,this.m_bValueBuffer,0,len);
			else if (len > val.Length)
				val.CopyTo(this.m_bValueBuffer,0);
			else
			{
				val.CopyTo(this.m_bValueBuffer,0);
				this.IsValidTLV = true;
			}
		}
        /// <summary>
        /// 检查TLV的TAG的VALUE的长度
        /// </summary>
        /// <returns></returns>
		private bool CheckTagLength()
		{
			bool bResult = false;
			switch(this.m_Tag)
			{
				case TagName.ModuleName:
					bResult = (this.m_uiValueLen == 6)?true:false;
					break;
				case TagName.ModuleClass:
					bResult = (this.m_uiValueLen == 6)?true:false;
					break;
				case TagName.ModuleContent:
					bResult = (this.m_uiValueLen == 6)?true:false;
					break; 
			}
			//return bResult;
			return true;
		}

		/// <summary>
		/// 转换成Byte数组
		/// </summary>
		/// <returns></returns>
		public byte[] ToByteArray()
		{
			if (!this.IsValidTLV || m_uiValueLen < ((m_bValueBuffer == null)?0:m_bValueBuffer.Length)) return new byte[0];
			byte[] temp = new byte[this.m_uiValueLen + 4];
			temp[0] = (byte)this.m_Tag;
			temp[1] = (byte)((ushort)this.m_Tag >> 8);
			temp[2] = Convert.ToByte(this.m_uiValueLen/255);
			temp[3] = Convert.ToByte(this.m_uiValueLen%255);
			if (this.m_bValueBuffer != null)
				this.m_bValueBuffer.CopyTo(temp,4);
			return temp;
		}
		/// <summary>
		/// 转换成string
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (!this.IsValidTLV)
				return "Invalid TLV Structure\r\n";
			else
				return "TagName:"+this.m_Tag.ToString("X")+"Length:"+this.m_uiValueLen.ToString("X")+"Value:"+this.ValueToString()+"\r\n";
		}
		private string ValueToString() 
		{
			for ( uint indexB = 0 ; indexB < TLV.Tags.Length ; indexB++ ) 
			{
				if ( this.m_Tag == TLV.Tags[ indexB ].tag )
					return this.FormatString(TLV.Tags[ indexB ].format);
			}
			return  "ERROR TLV STRUCTURE:Not Exist Such Tag(" + this.m_Tag.ToString("X")+")";
		}
        /// <summary>
        /// 格式化数据类型
        /// </summary>
        /// <param name="format">数据类型</param>
        /// <returns></returns>
		private string FormatString( TagFormat format ) 
		{
			string sDest = null;
			switch ( format ) 
			{
				case TagFormat.TLV_INTEGER:
					sDest = toInteger().ToString();
					break;
				case TagFormat.TLV_DATE:
					sDest = (toDate() == new DateTime())?"Date Paser Error":toDate().ToString("yyyy-MM-dd");
					break;
				case TagFormat.TLV_TIME:
					sDest = (toTime() == new DateTime())?"Time Paser Error":toTime().ToString("hh:mm:ss");
					break;
				case TagFormat.TLV_TIMESTAMP:
					sDest = (toTimeStamp() == new DateTime())?"DateTime Paser Error":toTimeStamp().ToString("yyyy-MM-dd hh:mm:ss");
					break;
				case TagFormat.TLV_STRING:
					sDest = toString();
					break;
				case TagFormat.TLV_MONEY:
					sDest = toMoney().ToString("C");
					break;
				case TagFormat.TLV_NUMBER:
					sDest = toNumber().ToString();
					break;
				case TagFormat.TLV_EXTEND: 
					sDest = (toExtend() == null)?"(NULL)":"("+toExtend().ToString()+")";
					break;
				default:
					sDest = "ERROR:Not find TagFormat to format value";
					break;
			}
			return sDest;
		}
        /// <summary>
        /// 转换成整型
        /// </summary>
        /// <returns></returns>
		public uint toInteger() //can not support more than 4 bytes
		{
			if ( this.m_uiValueLen > 4 )
				return 0xffffffff;
			uint ret = 0;
			for ( int i = 0;i < ( int ) m_uiValueLen;i++ )
				ret += (uint)( this.m_bValueBuffer[ i ] << ( i * 8 ) );
			return ret;
		}
		/// <summary>
		/// 转换成LONG
		/// </summary>
		/// <returns></returns>
		public long toLong() //can not support more than 4 bytes
		{
			long ret = 0;
			for ( int i = 0;i < ( int ) m_uiValueLen;i++ )
				ret += (uint)( this.m_bValueBuffer[ i ] << ( i * 8 ) );
			return ret;
		}
        /// <summary>
        /// 转换成日期型
        /// </summary>
        /// <returns></returns>
		public DateTime toDate() 
		{			
			if ( this.m_bValueBuffer == null ||this.m_bValueBuffer.Length < 3 )
				return new DateTime();
			uint year = (uint)m_bValueBuffer[ 0 ] + 1900;
			uint month = (uint) m_bValueBuffer[ 1 ];
			uint day = (uint) m_bValueBuffer[ 2 ];
			try
			{
				return new DateTime((int)year,(int)month,(int)day);
			}
			catch(System.Exception)
			{
				return new DateTime();
			}
		}
        /// <summary>
        /// 转换时间类型
        /// </summary>
        /// <returns></returns>
		public DateTime toTime() 
		{
			if ( this.m_bValueBuffer == null ||this.m_bValueBuffer.Length < 3 )
				return new DateTime();
			uint hour = (uint)m_bValueBuffer[ 0 ];
			uint minute = (uint) m_bValueBuffer[ 1 ];
			uint second = (uint) m_bValueBuffer[ 2 ];
			try
			{
				return new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Second,(int)hour,(int)minute,(int)second);
			}
			catch(System.Exception)
			{
				return new DateTime();
			}
		}
        /// <summary>
        /// 转换时间分隔
        /// </summary>
        /// <returns></returns>
		public DateTime toTimeStamp() 
		{
			if ( this.m_bValueBuffer == null ||this.m_bValueBuffer.Length < 6 )
				return new DateTime();
			uint year = (uint)m_bValueBuffer[ 0 ] + 1900;
			uint month = (uint) m_bValueBuffer[ 1 ];
			uint day = (uint) m_bValueBuffer[ 2 ];
			uint hour = (uint)m_bValueBuffer[ 3 ];
			uint minute = (uint) m_bValueBuffer[ 4 ];
			uint second = (uint) m_bValueBuffer[ 5 ];
			try
			{
				return new DateTime((int)year,(int)month,(int)day,(int)hour,(int)minute,(int)second);
			}
			catch(System.Exception)
			{
				return new DateTime();
			}
		}

		public string toString() 
		{
			if ( this.m_bValueBuffer == null|| this.m_bValueBuffer.Length == 0 )
				return "";
			return System.Text.Encoding.Default.GetString(m_bValueBuffer);
		}
		/// <summary>
		/// 转换成货币型
		/// </summary>
		/// <returns></returns>
		private double toMoney() 
		{
			if ( this.m_bValueBuffer == null || this.m_bValueBuffer.Length < 4 )
				return 0.0;
			double ret = 0.0;
			ret += ( double ) ( ( m_bValueBuffer[ 0 ] << 16 ) + ( m_bValueBuffer[ 1 ] << 8 ) + m_bValueBuffer[ 2 ] );
			ret += ( double ) ( m_bValueBuffer[ 3 ] ) / ( double ) 100;
			return ret;
		}
        /// <summary>
        /// 转换成数字型
        /// </summary>
        /// <returns></returns>
		private string toNumber()    //telephone number
		{
			if ( this.m_bValueBuffer == null || this.m_bValueBuffer.Length < 1 )
				return "Number Not Exist";

			uint num_length_ = (uint)m_bValueBuffer[ 0 ];
			if ( ( uint ) ( ( num_length_ + 1 ) / 2 + 1 ) > this.m_bValueBuffer.Length ) 
				return "Number Length ERROR" ;

			string sNum = null;
			for ( int i = 0 ; i < (int)num_length_ ; i++ ) 
			{
				if ( i % 2 == 0 )
					sNum += ((uint)( m_bValueBuffer[ 1 + i / 2 ] >> 4 )).ToString();
				else
					sNum += ((uint)( m_bValueBuffer[ 1 + i / 2 ] & 0x0f )).ToString() ;
			}
			return sNum;
		}
		private Packet_Body toExtend()    // current extend format is date-time format
		{
			if ( this.m_bValueBuffer == null || this.m_bValueBuffer.Length < 3 )
				return null;
			return new Packet_Body( m_bValueBuffer,m_uiValueLen );
		}
        /// <summary>
        /// 转换成byte
        /// </summary>
        /// <param name="tagformat">数据类型</param>
        /// <param name="val">值</param>
        /// <returns></returns>
		public static byte[] ValueToByteArray(TagFormat tagformat,object val)
		{
			switch(tagformat)
			{
				case TagFormat.TLV_DATE:
				{
					if (val.GetType() != typeof(System.DateTime))
						throw new TypeException(val.GetType().ToString(),typeof(System.DateTime).ToString());
					int year = ((DateTime)val).Year - 1900;
					int month = ((DateTime)val).Month;
					int day = ((DateTime)val).Day;
					return new byte[]{
										 (byte)year,(byte)month,(byte)day
									 };
				}
				case TagFormat.TLV_EXTEND:
				{
					if (val.GetType() != typeof(Packet_Body))
						throw new TypeException(val.GetType().ToString(),typeof(Packet_Body).ToString());
					else if (((Packet_Body)val).m_Status != Common.Logic.Body_Status.MSG_STRUCT_OK)
						throw new ValueException(((Packet_Body)val).m_Status.ToString(),Body_Status.MSG_STRUCT_OK.ToString());
					return ((Packet_Body)val).ToByteArray();
				}
				case TagFormat.TLV_INTEGER:
				{
					byte[] ret = null;
					System.Type type = val.GetType();
					if  (type == typeof(ulong))
					{
						ulong val_int = (ulong)val;
						ret = new byte[]{
											(byte)val_int,(byte)(val_int>>8),(byte)(val_int>>8*2),
											(byte)(val_int>>8*3),(byte)(val_int>>8*4),(byte)(val_int>>8*5),
											(byte)(val_int>>8*6),(byte)(val_int>>8*7)
										};
					}
                    else if (type == typeof(long))
                    {
                        ulong val_int = (ulong)((long)val);
                        ret = new byte[]{
											(byte)val_int,(byte)(val_int>>8),(byte)(val_int>>8*2),
											(byte)(val_int>>8*3),(byte)(val_int>>8*4),(byte)(val_int>>8*5),
											(byte)(val_int>>8*6),(byte)(val_int>>8*7)
										};
                    }
                    else if (type == typeof(uint))
                    {
                        uint val_int = (uint)val;
                        ret = new byte[]{
											(byte)val_int,(byte)(val_int>>8),(byte)(val_int>>16),(byte)(val_int>>24)
										};
                    }
                    else if (type == typeof(int))
                    {
                        uint val_int = (uint)((int)val);
                        ret = new byte[]{
											(byte)val_int,(byte)(val_int>>8),(byte)(val_int>>16),(byte)(val_int>>24)
										};
                    }
                    else if (type == typeof(short))
                    {
                        short val_int = (short)val;
                        ret = new byte[]{
											(byte)val_int,(byte)(val_int>>8)
										};
                    }
                    else if (type == typeof(ushort))
                    {
                        ushort val_int = (ushort)val;
                        ret = new byte[]{
											(byte)val_int,(byte)(val_int>>8)
										};
                    }
                    else if (type == typeof(byte))
                    {
                        byte val_int = (byte)val;
                        ret = new byte[] { (byte)val_int };
                    }
                    else if (type == typeof(sbyte))
                    {
                        byte val_int = (byte)((sbyte)val);
                        ret = new byte[] { (byte)val_int };
                    }
                    else
                        throw new TypeException(val.GetType().ToString(), "ulong,long,uint,int,byte,sbyte");
					return ret;
				}
				case TagFormat.TLV_MONEY:
				{
					if (val.GetType() != typeof(double))
						throw new TypeException(val.GetType().ToString(),typeof(double).ToString());
					uint val_int = (uint)((double)val);
					uint val_dec = (uint)(((double)val - val_int)*100);
					return new byte[]{
										 (byte)(val_int>>16),(byte)(val_int>>8),(byte)val_int,(byte)val_dec
									 };
				}
				case TagFormat.TLV_NUMBER:
				{
					if (val.GetType() != typeof(string))
						throw new TypeException(val.GetType().ToString(),typeof(string).ToString());
					string val_string = (string)val;
					if (!System.Text.RegularExpressions.Regex.IsMatch(val_string,@"^\d{11,12}$",System.Text.RegularExpressions.RegexOptions.Compiled))
						throw new ValueException(val_string,lg.Logic_TLV_Structure_NumLength);
					byte[] ret = new byte[1 + (val_string.Length + 1)/2];
					ret[0] = (byte)val_string.Length;
					for ( int i = 0; i < val_string.Length ;i++)
					{
						if (i %2 == 0)
						{
							ret[1 + i/2] += (byte)(int.Parse(val_string.Substring(i,1))<<4);
						}
						else
						{
							ret[1 + i/2] += (byte)(int.Parse(val_string.Substring(i,1)));
						}
					}
					return ret;
				}
				case TagFormat.TLV_STRING:
				{
					if (val.GetType() != typeof(string))
						throw new TypeException(val.GetType().ToString(),typeof(string).ToString());
	                return System.Text.Encoding.Default.GetBytes((string)val);
                   
				}
				case TagFormat.TLV_TIME:
				{
					if (val.GetType() != typeof(System.DateTime))
						throw new TypeException(val.GetType().ToString(),typeof(System.DateTime).ToString());
					int hour = ((DateTime)val).Hour;
					int minute = ((DateTime)val).Minute;
					int second = ((DateTime)val).Second;
					return new byte[]{
										 (byte)hour,(byte)minute,(byte)second
									 };
				}
				case TagFormat.TLV_UNICODE:
				{
					if (val.GetType() != typeof(string))
						throw new TypeException(val.GetType().ToString(),typeof(string).ToString());
					return System.Text.Encoding.Unicode.GetBytes((string)val);
				}
				case TagFormat.TLV_TIMESTAMP:
				{
					if (val.GetType() != typeof(System.DateTime))
						throw new TypeException(val.GetType().ToString(),typeof(System.DateTime).ToString());
					int year = ((DateTime)val).Year - 1900;
					int month = ((DateTime)val).Month;
					int day = ((DateTime)val).Day;
					int hour = ((DateTime)val).Hour;
					int minute = ((DateTime)val).Minute;
					int second = ((DateTime)val).Second;
					return new byte[]{
										 (byte)year,(byte)month,(byte)day,(byte)hour,(byte)minute,(byte)second
									 };
				}
				case TagFormat.TLV_BOOLEAN:
				{
					if (val.GetType() != typeof(bool))
						throw new TypeException(val.GetType().ToString(),typeof(bool).ToString());
						bool val_int = (bool)val;
						return new byte[]{Convert.ToByte(val_int)};
				}
				default:
					throw new ValueException(val.ToString(),lg.Logic_TLV_Structure_TagFormatType);
			}

		}

	}
}
