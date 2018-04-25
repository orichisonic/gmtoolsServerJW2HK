using System;

namespace Common.Logic
{
	/// <summary>
	/// SeqId_Generator 的摘要说明。
	/// </summary>
	public class SeqID_Generator
	{
		/// <summary>
		/// 错误ID
		/// </summary>
		public const uint ERROR_ID = 0;
		/// <summary>
		/// 实例化静态的对象
		/// </summary>
		private static SeqID_Generator theSingleton = null;
		/// <summary>
		/// 序列号
		/// </summary>
		private uint m_uiID;
		/// <summary>
		/// 构造实例化对象
		/// </summary>
		public SeqID_Generator() 
		{
			this.m_uiID = 1;
		}

		public SeqID_Generator( uint uiInitID ) 
		{
			if (uiInitID == 0) uiInitID = 1;
			this.m_uiID = uiInitID;
		}

		public static SeqID_Generator Instance() 
		{
			if (theSingleton == null)
				theSingleton = new SeqID_Generator();
			return theSingleton;
		}

		public uint GetNewSeqID() 
		{
			if ( this.m_uiID == 0xFFFFFFFF )
				return this.m_uiID = 1;
			else
				return this.m_uiID ++;
		}

		public void SetCurrSeqID( uint uiID ) 
		{
			if (uiID == 0) uiID = 1;
			this.m_uiID = uiID;
		}
	}
}
