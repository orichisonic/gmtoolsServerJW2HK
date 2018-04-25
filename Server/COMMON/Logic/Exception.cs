using System;
using lg = Common.API.LanguageAPI;

namespace Common.Logic
{
	/// <summary>
	/// Exception 的摘要说明。
	/// </summary>
	public class Exception : System.Exception
	{
		private int m_iGMMsg_ErrCode;
		private string m_sGMMsg_ErrMessage;
		public Exception ()
		{
			this.m_iGMMsg_ErrCode = 0;
			this.m_sGMMsg_ErrMessage = null;
		}
		public Exception(string sMessage) : base(sMessage)
		{
			this.m_iGMMsg_ErrCode = 0;
			this.m_sGMMsg_ErrMessage = null;
		}


		public string GMMsg_ErrCode
		{
			set
			{
				this.m_iGMMsg_ErrCode = System.Int32.Parse(value.Substring(value.Length-7,7));
			}
			get
			{
				return "GMMsg-0" + this.m_iGMMsg_ErrCode.ToString("D4");
			}
		}

		public string GMMsg_ErrMessage
		{
			set
			{
				this.m_sGMMsg_ErrMessage = value;
			}
			get
			{
				return this.m_sGMMsg_ErrMessage;
			}
		}

		public override string ToString()
		{
			return "[GMMsg-0" 
				+ this.m_iGMMsg_ErrCode.ToString("D4")
				+ "]"
				+ this.m_sGMMsg_ErrMessage;
		}
	}
	public class TypeException : Exception
	{
		public TypeException(string sParameterType,string sExpectedType)
		{
			this.GMMsg_ErrCode = "GMMsg-01";
			this.GMMsg_ErrMessage = lg.Logic_Exception_Parameter+lg.Logic_Exception_ExpectedType+lg.Logic_Exception_Error+"("+lg.Logic_Exception_ExpectedType+"-"+sExpectedType+","+lg.Logic_Exception_RealType+"-"+sParameterType+")";
		}
		public TypeException(string sMessage):base(sMessage)
		{
			this.GMMsg_ErrCode = "GMMsg-01";
			this.GMMsg_ErrMessage = lg.Logic_Exception_Parameter+lg.Logic_Exception_ExpectedType+lg.Logic_Exception_Error;

		}
	}
	public class ValueException : Exception
	{
		public ValueException(string sParameterValue,string sExpectedValue)
		{
			this.GMMsg_ErrCode = "GMMsg-02";
			this.GMMsg_ErrMessage = lg.Logic_Exception_Parameter+lg.Logic_Exception_ExpectedValue+lg.Logic_Exception_Error+"("+lg.Logic_Exception_ExpectedValue+"-"+sExpectedValue+","+lg.Logic_Exception_RealValue+"-"+sParameterValue+")";
			
		}
		public ValueException(string sMessage):base(sMessage)
		{
			this.GMMsg_ErrCode = "GMMsg-02";
			this.GMMsg_ErrMessage = lg.Logic_Exception_Parameter+lg.Logic_Exception_ExpectedValue+lg.Logic_Exception_Error;
		}
	}
}
