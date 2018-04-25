using System;
using System.Text;
namespace Common.API
{
	/// <summary>
	/// GMLogAPI 的摘要说明。
	/// </summary>
	public class GMLogAPI
	{
		public StringBuilder Buffer;
		private int userByID = 0;
		public GMLogAPI()
		{
			Buffer = new StringBuilder();
		}
		/// <summary>
		/// 打印标题
		/// </summary>
		/// <param name="moduleName">模块名称</param>
		/// <param name="moduleContent"></param>
		public  void writeTitle(string moduleName,string moduleContent)
		{
			Buffer.Append("+-----------------+---------------------------------------------+\n");
			Buffer.Append("+    "+LanguageAPI.API_GameInfoAPI_ModuleDesp+"     +                  "+LanguageAPI.API_Description+"                       +\n");
			Buffer.Append("+-----------------+---------------------------------------------+\n");
			Buffer.Append("+ "+moduleName+"  +            "+moduleContent+"                +\n");
		}
		/// <summary>
		/// 打印名称
		/// </summary>
		/// <param name="param1"></param>
		/// <param name="param2"></param>
		/// <param name="param3"></param>
		public void writeContent(string param1,string param2,string param3)
		{
			Buffer.Append("+-----------------+--------------------+------------------------+\n");
			Buffer.Append("+   "+param1+"    +   "+ param2+"      +       "+param3+"       +\n");
			Buffer.Append("+-----------------+--------------------+------------------------+\n");
		}
		/// <summary>
		/// 打印内容
		/// </summary>
		/// <param name="param1"></param>
		/// <param name="param2"></param>
		/// <param name="param3"></param>
		/// <param name="param4"></param>
		public void writeContent(string param1,string param2,string param3,string param4)
		{
			Buffer.Append("+-----------------+--------------------+------------------------+------------------------+\n");
			Buffer.Append("+    "+param1+"   +     "+param2+"     +      "+param3+"        +     "+param4+"         +\n");
			Buffer.Append("+-----------------+--------------------+------------------------+------------------------+\n");
		}
		public void writeData(string paramValue1,string paramValue2,string paramValue3)
		{
			Buffer.Append("+ "+paramValue1+" +"   +paramValue2+"  + "+    paramValue3+"    +\n");
			Buffer.Append("+-----------------+--------------------+------------------------+\n");
		}
		public void writeData(string paramValue1,string paramValue2,string paramValue3,string paramValue4)
		{
			Buffer.Append("+" +paramValue1+" +  "+paramValue2+"   +      "+paramValue3+"   +     "+paramValue4+"    +\n");
			Buffer.Append("+-----------------+--------------------+------------------------+------------------------+\n");
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
	}
}
