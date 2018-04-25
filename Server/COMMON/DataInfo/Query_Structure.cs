using System;
using System.Collections;
using Common.Logic;
namespace Common.DataInfo
{
	public class TagStruct
	{
		public TagStruct(TagName _tag,TagFormat _format,uint _len,byte[] _tag_buf)
		{
			tag = _tag;
			format = _format;
			len = _len;
			this.tag_buf = new byte[len];
			tag_buf= _tag_buf;
		}
		public TagName tag;
		public TagFormat format;
		public uint len;
		public byte[] tag_buf;
		
	}
	/// <summary>
	/// Query_Structure 的摘要说明。
	/// </summary>
	public class Query_Structure : TLV_Structure
	{		
		public TagStruct[] m_tagList;
		public uint structLen = 0;
		public Query_Structure(uint len)
		{
			structLen = len;
			m_tagList = new TagStruct[structLen];
                                   			
		}
		public void AddTagKey(TagName tag,TagFormat style,uint len,byte[] val)
		{
			int i=0;
			for(i=0;i<structLen;i++)
			{
				if(null==m_tagList[i])
					break;
			}
          m_tagList[i] = new TagStruct( tag,style, len, val);
		}
		
	}
}
