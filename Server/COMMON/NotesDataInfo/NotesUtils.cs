using System;
using System.Text;
using Domino;
//using GlobalCore;
//using EnumCore;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
namespace Common.NotesDataInfo
{
	/// <summary>
	/// Lotus Notes COM 操作
	/// </summary>
	public class NotesUtils
	{
		public NotesUtils(NotesSessionClass pNotesSessionClass,string strDomain, string strDataBase)
		{
			this._strDomain = strDomain;
			this._strDataBase = strDataBase;
			this._strUserName = String.Empty;
	
			this.strMessage = String.Empty;
			this.pInfoList = new ArrayList();
	
			this.pNotesSession = pNotesSessionClass;
		}

	
		public bool OpenDataBase(string strUserName, string strPassword)
		{
			bool bResult = false;
	
			try
			{
				this._strUserName = strUserName;
	
				this.pNotesSession.Initialize(strPassword);
	
				this.pNotesDatabase = this.pNotesSession.GetDatabase(this._strDomain, this._strDataBase, false);
	
				bResult = true;
			}
			catch (Exception ex)
			{
				bResult = false;
	
				this.strMessage = ex.Message;
			}
	
			return bResult;
		}
	
		public bool GetLinkerInfo(string strCategory)
		{
			bool bResult = false;
			NotesView pLinkerView;
			NotesDocument pLinkerDocument;
	
			try
			{
				if (this._strDataBase != "names.nsf")
				{
					this.pNotesDatabase = this.pNotesSession.GetDatabase(this._strDomain, "names.nsf", false);
				}
	
				pLinkerView = this.pNotesDatabase.GetView(strCategory);
	
				pLinkerDocument = pLinkerView.GetFirstDocument();
	
				while (pLinkerDocument != null)
				{
					GlobalStruct[] pLinkerStruct = new GlobalStruct[2];
	
					pLinkerStruct[0].oFieldsName = "Linker_Name";
					pLinkerStruct[0].oFiledsTypes = "String";
					pLinkerStruct[0].oFieldValues = pLinkerDocument.GetFirstItem("ListName").Text;
	
					pLinkerStruct[1].oFieldsName = "Linker_Content";
					pLinkerStruct[1].oFiledsTypes = "String";
					pLinkerStruct[1].oFieldValues = (pLinkerDocument.GetFirstItem("Members") == null) ? "N/A" : pLinkerDocument.GetFirstItem("Members").Text;
	
					this.pInfoList.Add(pLinkerStruct);
	
					pLinkerDocument = pLinkerView.GetNextDocument(pLinkerDocument);
				}
	
				bResult = true;
			}
			catch (Exception ex)
			{
				this.strMessage = ex.Message;
	
				bResult = false;
				this.pInfoList.Clear();
			}
			finally
			{
				pLinkerDocument = null;
				pLinkerView = null;
			}
	
			return bResult;
		}
	
		public bool GetMailInfo()
		{
			bool bResult = false;
			NotesView pMailView;
			NotesDocument pMailDocument;
	
			try
			{
				if (this._strDataBase == "names.nsf")
				{
					this.pNotesDatabase = this.pNotesSession.GetDatabase(this._strDomain, this._strDataBase, false);
				}
	
				pMailView = this.pNotesDatabase.GetView("($inbox)");
	
				pMailDocument = pMailView.GetFirstDocument();
	
				while (pMailDocument != null)
				{
					if (((object[])pMailDocument.GetItemValue("Reader"))[0].ToString() != "YES")
					{
						string strPUID = string.Empty;
						string strSupervisors = (((object[])pMailDocument.GetItemValue("CopyTo")).Length == 0) ? "N/A" : ((object[])pMailDocument.GetItemValue("CopyTo"))[0].ToString();
	
						if (strSupervisors != "N/A")
						{
							for (int i = 1; i < ((object[])pMailDocument.GetItemValue("CopyTo")).Length; i++)
							{
								strSupervisors = strSupervisors + ";" + ((object[])pMailDocument.GetItemValue("CopyTo"))[i].ToString();
							}
						}
	
						string strBody = ((object[])pMailDocument.GetItemValue("Body"))[0].ToString();
	
						if (((object[])pMailDocument.GetItemValue("客服中心"))[0].ToString().Length != 0)
						{
							strPUID = ((object[])pMailDocument.GetItemValue("客服中心"))[0].ToString();
						}
						else if (((object[])pMailDocument.GetItemValue("客服中心"))[0].ToString().Length == 0 && pMailDocument.ParentDocumentUNID == null)
						{
							strPUID = "N/A";
						}
						else
						{
							strPUID = pMailDocument.ParentDocumentUNID;
						}
						/*
							if (pMailDocument.HasItem("SMS Agent"))
							{
								strPUID = ((object[])pMailDocument.GetItemValue("SMS Agent"))[0].ToString();
							}
							else if (!pMailDocument.HasItem("SMS Agent") && pMailDocument.ParentDocumentUNID == null)
							{
								strPUID = "N/A";
							}
							else
							{
								strPUID = pMailDocument.ParentDocumentUNID;
							}
							*/
	
						GlobalStruct[] pMailStruct = new GlobalStruct[10];
	
						pMailStruct[0].oFieldsName = "Notes_UID";
						pMailStruct[0].oFiledsTypes = "String";
						pMailStruct[0].oFieldValues = pMailDocument.UniversalID;
	
						pMailStruct[1].oFieldsName = "Notes_PUID";
						pMailStruct[1].oFiledsTypes = "String";
						pMailStruct[1].oFieldValues = strPUID; // (pMailDocument.ParentDocumentUNID == null) ? "N/A" : pMailDocument.ParentDocumentUNID;
	
						pMailStruct[2].oFieldsName = "Notes_Subject";
						pMailStruct[2].oFiledsTypes = "String";
						pMailStruct[2].oFieldValues = (((object[])pMailDocument.ColumnValues)[5] == null) ? "N/A" : ((object[])pMailDocument.ColumnValues)[5].ToString();
	
						pMailStruct[3].oFieldsName = "Notes_From";
						pMailStruct[3].oFiledsTypes = "String";
						pMailStruct[3].oFieldValues = ((object[])pMailDocument.GetItemValue("Principal"))[0].ToString(); //((object[])pMailDocument.ColumnValues)[1].ToString();
	
						pMailStruct[4].oFieldsName = "Notes_Date";
						pMailStruct[4].oFiledsTypes = "String";
						pMailStruct[4].oFieldValues = ((object[])pMailDocument.ColumnValues)[2].ToString();
	
						pMailStruct[5].oFieldsName = "Notes_Supervisors";
						pMailStruct[5].oFiledsTypes = "String";
						pMailStruct[5].oFieldValues = (strSupervisors == "") ? "N/A" : strSupervisors;
	
						pMailStruct[6].oFieldsName = "Notes_Content";
						pMailStruct[6].oFiledsTypes = "String";
						pMailStruct[6].oFieldValues = (strBody == "") ? "N/A" : System.Web.HttpUtility.HtmlDecode(strBody);
	
						//System.Web.HttpUtility.HtmlDecode(strBody)
	
						pMailStruct[7].oFieldsName = "Notes_AttachmentCount";
						pMailStruct[7].oFiledsTypes = "String";
						pMailStruct[7].oFieldValues = (((NotesRichTextItem)pMailDocument.GetFirstItem("Body")).EmbeddedObjects == null) ? "0" : ((object[])((NotesRichTextItem)pMailDocument.GetFirstItem("Body")).EmbeddedObjects).Length.ToString();
	                        
						pMailStruct[8].oFieldsName = "Notes_Category";
						pMailStruct[8].oFiledsTypes = "String";
						pMailStruct[8].oFieldValues =  (pMailStruct[2].oFieldValues.ToString().IndexOf("|")==-1)? "0" : pMailStruct[2].oFieldValues.ToString().Substring(pMailStruct[2].oFieldValues.ToString().IndexOf("|")+1,1);
							
						pMailStruct[9].oFieldsName = "Notes_Status";
						pMailStruct[9].oFiledsTypes = "String";
						pMailStruct[9].oFieldValues =  (pMailStruct[2].oFieldValues.ToString().IndexOf("催")==-1)? 0 : 1;
						this.pInfoList.Add(pMailStruct);
	
						if (pMailDocument.HasItem("Reader"))
						{
							pMailDocument.ReplaceItemValue("Reader", "YES");
						}
						else
						{
							pMailDocument.AppendItemValue("Reader", "YES");
						}
	
						pMailDocument.Save(true, true, true);
					}
	
					pMailDocument = pMailView.GetNextDocument(pMailDocument);
				}
	
				bResult = true;
			}
			catch (Exception ex)
			{
				this.strMessage = ex.Message;
	
				bResult = false;
				this.pInfoList.Clear();
			}
			finally
			{
				pMailDocument = null;
				pMailView = null;            
			}
	
			return bResult;
		}
	
		public bool GetMailAttachment(string strNotesUID)
		{
			bool bResult = false;
			NotesView pMailView;
			NotesDocument pMailDocument;
	
			try
			{
				if (this._strDataBase == "names.nsf")
				{
					this.pNotesDatabase = this.pNotesSession.GetDatabase(this._strDomain, this._strDataBase, false);
				}
	
				pMailView = this.pNotesDatabase.GetView("($inbox)");
	
				pMailDocument = pMailView.GetFirstDocument();
	
				while (pMailDocument != null)
				{
					if (pMailDocument.UniversalID == strNotesUID)
					{
						object[] arrItemObject = (object[])((NotesRichTextItem)pMailDocument.GetFirstItem("Body")).EmbeddedObjects;
	
						for (int i = 0; i < arrItemObject.Length; i++)
						{
							NotesEmbeddedObject pEmbeddedObject = (NotesEmbeddedObject)arrItemObject[i];
	
							pEmbeddedObject.ExtractFile(System.AppDomain.CurrentDomain.BaseDirectory + "\\Attachment\\" + pEmbeddedObject.Source);
	
							FileStream pAttachmentStream = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + "\\Attachment\\" + pEmbeddedObject.Source, FileMode.Open, FileAccess.Read);
							byte[] pAttachmentContent = new byte[pAttachmentStream.Length];
							pAttachmentStream.Read(pAttachmentContent, 0, pAttachmentContent.Length);
							pAttachmentStream.Close();
	
							File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + "\\Attachment\\" + pEmbeddedObject.Source);
	
							GlobalStruct[] pAttachmentStruct = new GlobalStruct[2];
	
							pAttachmentStruct[0].oFieldsName = "Notes_AttachmentName";
							pAttachmentStruct[0].oFiledsTypes = "String";
							pAttachmentStruct[0].oFieldValues = pEmbeddedObject.Source;
	
							pAttachmentStruct[1].oFieldsName = "Notes_Attachment";
							pAttachmentStruct[1].oFiledsTypes = "Byte[]";
							pAttachmentStruct[1].oFieldValues = pAttachmentContent;
	
							pAttachmentContent = null;
	
							this.pInfoList.Add(pAttachmentStruct);
						}
					}
	
					pMailDocument = pMailView.GetNextDocument(pMailDocument);
				}
	
				bResult = true;
			}
			catch (Exception ex)
			{
				this.strMessage = ex.Message;
	
				bResult = false;
				this.pInfoList.Clear();
			}
			finally
			{
				pMailDocument = null;
				pMailView = null;
			}
	
			if (this.pInfoList.Count == 0)
			{
				return false;
			}
	
			return bResult;        
		}
		public bool SendMailInfo(object receiver,object pSupervisors, object pSendSecret, object pSubject, string strMailContent)
		{
			bool bResult = false;
			NotesDocument pMailDocument;
			NotesRichTextItem pContentItem;
	
			try
			{
				pMailDocument = this.pNotesDatabase.CreateDocument();
	
				pMailDocument.ReplaceItemValue("Form", "Memo");
				pMailDocument.ReplaceItemValue("CopyTo", pSupervisors);//抄送
				pMailDocument.ReplaceItemValue("BlindCopyTo", pSendSecret); //密送
				pMailDocument.ReplaceItemValue("Subject", pSubject);
				pMailDocument.ReplaceItemValue("PostedDate", DateTime.Now.ToString());
	
				pContentItem = pMailDocument.CreateRichTextItem("Body");
				pContentItem.AppendText(strMailContent);
	
				object pSendOwner = receiver;
				pMailDocument.Send(false, ref pSendOwner);
	 
				bResult = true;
			}
			catch (Exception ex)
			{
				this.strMessage = ex.Message;
	
				bResult = false;
			}
			finally
			{
				pContentItem = null;
				pMailDocument = null;
			}
	
			return bResult;
		}
	
		public bool RelayMailInfo(object pSupervisors, object pSendSecret, string strNotesUID, string strMailContent,object strSender)
		{
			bool bResult = false;
			NotesView pParentView;
			NotesDocument pParentDocument;
	
			try
			{
				if (this._strDataBase == "names.nsf")
				{
					this.pNotesDatabase = this.pNotesSession.GetDatabase(this._strDomain, this._strDataBase, false);
				}
	
				pParentView = this.pNotesDatabase.GetView("($inbox)");
				pParentDocument = pParentView.GetFirstDocument();
	
				while (pParentDocument != null)
				{
					if (pParentDocument.UniversalID == strNotesUID)
					{
						NotesDocument pRelayDocument = pParentDocument.CreateReplyMessage(false);
						string strRelaySubject = (((object[])pParentDocument.GetItemValue("Subject"))[0] == null) ? "N/A" : ((object[])pParentDocument.GetItemValue("Subject"))[0].ToString();
	
						pParentDocument.ReplaceItemValue("Form", "Reply");
						pParentDocument.ReplaceItemValue("CopyTo", pSupervisors);//抄送
						pParentDocument.ReplaceItemValue("BlindCopyTo", pSendSecret); //密送
						pParentDocument.ReplaceItemValue("Subject", "回复:" + strRelaySubject);
						pParentDocument.ReplaceItemValue("PostedDate", DateTime.Now.ToString());
						pParentDocument.ReplaceItemValue("Principal", "CN=tcsadmin/OU=技术支持部/OU=产品运营中心/O=runstar");
						//pParentDocument.ReplaceItemValue("Principal", strSender);
						pParentDocument.ReplaceItemValue("Body", "");
						pParentDocument.ReplaceItemValue("客服中心", strNotesUID);
	
						if (pParentDocument.HasItem("Reader"))
						{
							pParentDocument.ReplaceItemValue("Reader", "NO");
						}
	
						NotesRichTextItem pOldItem = (NotesRichTextItem)pParentDocument.GetFirstItem("Body");
						pOldItem.AppendText(strMailContent);
						pOldItem.AddNewLine(5, false);
						pOldItem.AppendRTItem((NotesRichTextItem)pRelayDocument.GetFirstItem("Body"));
	
						object pSendOwner = strSender;//"孙露"
						pParentDocument.Send(false, ref pSendOwner);
	
						bResult = true;
	
						break;
					}
					else
					{
						bResult = false;
	
						this.strMessage = "不能找到原始信件，原信件可能已删除，请新建一封信的信件给接收人！";
					}
	
					pParentDocument = pParentView.GetNextDocument(pParentDocument);
				}
			}
			catch (Exception ex)
			{
				this.strMessage = ex.Message;
	
				bResult = false;
			}
			finally
			{
				pParentDocument = null;
				pParentView = null;
			}
	
			return bResult;
		}
	
		public string Message
		{
			get
			{
				return this.strMessage;
			}
		}
	
		public object[] Records
		{
			get
			{
				return this.pInfoList.ToArray();
			}
		}
	
		private string _strDomain;
		private string _strDataBase;
		private string _strUserName;
		private string strMessage;
		private ArrayList pInfoList;
	
		private NotesSessionClass pNotesSession;
		private NotesDatabase pNotesDatabase;
	}
}
