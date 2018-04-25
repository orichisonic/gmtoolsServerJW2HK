using System;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Collections;
using Common.Logic;
using Common.DataInfo;

namespace Common.DataInfo
{
    /// <summary>
    /// 客户端补丁文件消息类型
    /// </summary>
    [Serializable()]
    public struct PatchMessage
    {
        public TagFormat eTag;
        public TagName eName;
        public object oContent;
        public string fileName;
        public string fileVersion;
        public string fileSize;
    }
	/// <summary>
	/// UpdatePacth 的摘要说明。
	/// </summary>
	public class UpdatePatch
	{
		NetworkStream stream;
		FileStream filestream;
        Message msg = null;
        Packet_Body mPacketbody = null;
        PatchMessage[,] p_ReturnBody;
        private string[,] saveArrayInfo = null;
        private string[,] tempArrayInfo = null;
		public UpdatePatch(byte[] packet)
		{
            msg = new Message(packet,(uint)packet.Length);
            mPacketbody = msg.m_packet.m_Body;
		}
        public byte[] readFile(string filePath)
        {
            FileStream fs = null;
             BinaryReader br = null;
            byte[] dllbytes = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open);
                br = new BinaryReader(fs);

                int intLength = (int)fs.Length;
                dllbytes = br.ReadBytes(intLength);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                br.Close();
                fs.Close();
            }
            return dllbytes;
        }
        public byte[] transferPatchFile()
        {
            string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string fileName =  System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.UpdateFileName).m_bValueBuffer);
            string filePath = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.UpdateFilePath).m_bValueBuffer);

            return readFile(path+@"\patch\"+filePath+@"\"+fileName);
        }
        public string[,] compareVersion(PatchMessage[,] msgFiles, string[,] patchFiles)
        {
            string[,] updatePatch = null;
            for (int i = 0; i < patchFiles.GetLength(0); i++)
            {
                bool flag = false;
                for (int j = 0; j<msgFiles.GetLength(0); j++)
                {
                    if (patchFiles[i,0].ToString().ToLower().Equals(msgFiles[j,0].oContent.ToString().ToLower()))
                    {
                        if (!(patchFiles[i,1].Equals( msgFiles[j, 1].oContent)))
                            updatePatch = SaveArray(patchFiles[i, 0], patchFiles[i, 1], patchFiles[i, 2],Convert.ToInt32(patchFiles[i, 3]));
                        flag = true;
                    }
                }
                if(flag == false)
                    updatePatch = SaveArray(patchFiles[i, 0], patchFiles[i, 1], patchFiles[i, 2], Convert.ToInt32(patchFiles[i, 3]));
            }
            return updatePatch;
        }
        /// <summary>
        /// 获取本地所有ｄｌｌ文件
        /// </summary>
        /// <returns></returns>
        public string[,] GetLocalDllFile(string strPath)
        {
            byte[] fileSize;
            string[,] dllFilesInfo = null;   //返回指定路径的ｄｌｌ文件信息
            FileVersionInfo fileInfo = null;
            string[] fileInfoSplit = null;
            System.IO.DirectoryInfo dDirectory = new System.IO.DirectoryInfo(strPath);

			try
			{
				foreach (System.IO.FileInfo file in dDirectory.GetFiles("*.*"))
				{

					fileInfo = FileVersionInfo.GetVersionInfo(file.FullName.ToString());
					fileSize = readFile(file.FullName.ToString());
					fileInfoSplit = fileInfo.FileName.Split(new char[] { '\\' });
					dllFilesInfo = SaveArray(fileInfoSplit[fileInfoSplit.Length - 1].ToString(), fileInfo.FileVersion.ToString(), strPath, fileSize.Length);
				}
				saveArrayInfo = null;
			}
			catch (System.Exception)
			{
				//throw new System.Exception(ex.Message);
				return null;
			}
			return dllFilesInfo;;

        }
        /// <summary>
        /// 建立二维数组，并存储信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string[,] SaveArray(string key, string value,string path,int fileSize)
        {
            if (saveArrayInfo == null)
            {
                ///存储第一次调用函数时保存的信息
                saveArrayInfo = new string[1, 4];
                saveArrayInfo[0, 0] = key;
                saveArrayInfo[0, 1] = value;
                saveArrayInfo[0, 2] = path;
                saveArrayInfo[0, 3] = Convert.ToString(fileSize);

            }
            else
            {
                ///存储第二次调用函数时保存的信息

                //获取数组主存中第一维的长度
                int sAFirLength = saveArrayInfo.GetLength(0) + 1;
                //设置数组辅存
                tempArrayInfo = new string[sAFirLength, 4];
                //将主存中的内容赋给辅存
                for (int i = 0; i < sAFirLength - 1; i++)
                {
                    tempArrayInfo[i, 0] = saveArrayInfo[i, 0];
                    tempArrayInfo[i, 1] = saveArrayInfo[i, 1];
                    tempArrayInfo[i, 2] = saveArrayInfo[i, 2];
                    tempArrayInfo[i, 3] = saveArrayInfo[i, 3];
                }
                //将函数被调用的参数内容保存进辅存
                tempArrayInfo[sAFirLength - 1, 0] = key;
                tempArrayInfo[sAFirLength - 1, 1] = value;
                tempArrayInfo[sAFirLength - 1, 2] = path;
                tempArrayInfo[sAFirLength - 1, 3] = Convert.ToString(fileSize);

                //将辅存内容赋给主存
                saveArrayInfo = tempArrayInfo;
            }
            //返回主存内容
            return saveArrayInfo;
        }
        public Message encodeMessage()
        {
			PatchMessage[,] msgPatchFiles = decodeMessage();
			string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
			string[,] compareUpdateFiles;
			string[,] resultUpdateFiles;
			string[,]  rootPatchFiles = GetLocalDllFile(path+@"\patch\root");
			string[,] localPatchFiles = GetLocalDllFile(path+@"\patch\Modules");
			string Encode = System.Globalization.CultureInfo.CurrentUICulture.Name;
			compareUpdateFiles = new string[rootPatchFiles.GetLength(0)+localPatchFiles.GetLength(0),4];
			System.Array.Copy(rootPatchFiles, compareUpdateFiles, rootPatchFiles.GetLength(0) * 4);
			System.Array.Copy(localPatchFiles, 0, compareUpdateFiles, rootPatchFiles.GetLength(0) * 4, localPatchFiles.GetLength(0) * 4);
			compareUpdateFiles = compareVersion(msgPatchFiles, compareUpdateFiles);
			if (compareUpdateFiles != null)
			{
				string[,] langPatchFiles = GetLocalDllFile(path+@"\patch\Lang\"+Encode);
				string[,] schemePatchFiles = GetLocalDllFile(path+@"\patch\Schmem");
				if(langPatchFiles !=null || schemePatchFiles!=null)
				{
					resultUpdateFiles = new string[compareUpdateFiles.GetLength(0)+langPatchFiles.GetLength(0)+schemePatchFiles.GetLength(0), 4];
					System.Array.Copy(compareUpdateFiles,0,resultUpdateFiles,0,compareUpdateFiles.GetLength(0)*4);
					System.Array.Copy(langPatchFiles,0,resultUpdateFiles,compareUpdateFiles.GetLength(0)*4,langPatchFiles.GetLength(0)*4);
					System.Array.Copy(schemePatchFiles,0, resultUpdateFiles,compareUpdateFiles.GetLength(0)*4+langPatchFiles.GetLength(0)*4,schemePatchFiles.GetLength(0) * 4);
				}
				else
				{
					resultUpdateFiles = new string[compareUpdateFiles.GetLength(0), 4];
					System.Array.Copy(compareUpdateFiles,0,resultUpdateFiles,0,compareUpdateFiles.GetLength(0)*4);

				}
				Query_Structure[] structList = new Query_Structure[resultUpdateFiles.GetLength(0)];
				for (int i = 0; i < resultUpdateFiles.GetLength(0); i++)
				{
					Query_Structure strut = new Query_Structure(4);
					//文件名
					byte[] bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, resultUpdateFiles[i, 0]);
					strut.AddTagKey(TagName.UpdateFileName, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					//文件版本
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, resultUpdateFiles[i, 1]);
					strut.AddTagKey(TagName.UpdateFileVersion, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					structList[i] = strut;
					//文件路径
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_STRING, resultUpdateFiles[i, 2]);
					strut.AddTagKey(TagName.UpdateFilePath, TagFormat.TLV_STRING, (uint)bytes.Length, bytes);
					structList[i] = strut;
					//文件路径
					bytes = TLV_Structure.ValueToByteArray(TagFormat.TLV_INTEGER, Convert.ToInt32(resultUpdateFiles[i, 3]));
					strut.AddTagKey(TagName.UpdateFileSize, TagFormat.TLV_INTEGER, (uint)bytes.Length, bytes);
					structList[i] = strut;
				}
				return Message.COMMON_MES_RESP(structList, Msg_Category.COMMON, ServiceKey.CLIENT_PATCH_COMPARE_RESP, 4);
			}
			else
			{
				return Message.COMMON_MES_RESP("FAILURE", Msg_Category.COMMON, ServiceKey.CLIENT_PATCH_COMPARE_RESP, TagName.Status, TagFormat.TLV_STRING);
			}
        }
        public PatchMessage[,] decodeMessage()
        {

             string delimStr = ";";
             string delimStr1 = ",";
            char [] delimiter = delimStr.ToCharArray();
            char[] delimiter1 = delimStr1.ToCharArray();
            string split1 = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.UpdateFilePath).m_bValueBuffer);
            string split2 = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.UpdateFileName).m_bValueBuffer);
            string split = split1 + split2;
            string[] msgStr = split.Split(delimiter);

            p_ReturnBody = new PatchMessage[msgStr.Length-1, 2];
            for (int i = 0; i < msgStr.Length-1; i++)
            {
                string key = null;
                string value = null;
                key = msgStr[i].Split(delimiter1)[0];
                value = msgStr[i].Split(delimiter1)[1]; 
                p_ReturnBody[i, 0].eName = TagName.UpdateFileName;
                p_ReturnBody[i, 0].oContent = key;
                p_ReturnBody[i, 1].eName = TagName.UpdateFileVersion;
                p_ReturnBody[i, 1].oContent = value;
            }
            return p_ReturnBody;
            /*int iField = 0;
            int iCount = 0;		//重复个数
            string split = System.Text.Encoding.Default.GetString(msg.m_packet.m_Body.getTLVByTag(TagName.UpdateFileName).m_bValueBuffer);
            System.Collections.ArrayList t_StructCount = mPacketbody.m_TLVList;
            System.Collections.ArrayList t_StructUsed = (System.Collections.ArrayList)t_StructCount.Clone();
            for (int i = 0; i < t_StructCount.Count; i++)
            {
                for (int j = i + 1; j < t_StructCount.Count; j++)
                {
                    if (((TLV_Structure)t_StructCount[i]).m_Tag != ((TLV_Structure)t_StructCount[j]).m_Tag)
                    {
                        iCount++;
                        t_StructCount.RemoveAt(j);
                        j--;
                    }
                }
            }
            if (t_StructCount.Count == 0)
            {
                p_ReturnBody = new PatchMessage[1, 1];
                p_ReturnBody[0, 0].eName = TagName.ERROR_Msg;
                p_ReturnBody[0, 0].eTag = TagFormat.TLV_STRING;
                p_ReturnBody[0, 0].oContent = "无数据!";

                return p_ReturnBody;
            }
            iField = t_StructUsed.Count / t_StructCount.Count;
            p_ReturnBody = new PatchMessage[t_StructUsed.Count - iCount, iField];

            int iTemp = 0;
            for (int i = 0; i < t_StructCount.Count; i++)
            {
                for (int j = i * iField; j < t_StructUsed.Count; j++)
                {
                    //设置字段标签
                    if (iTemp == iField)
                    {
                        iTemp = 0;
                        break;
                    }

                    TLV_Structure mStruct = (TLV_Structure)t_StructUsed[j];

                    p_ReturnBody[i, iTemp].eName = mStruct.m_Tag;
                    p_ReturnBody = DecodeRecive(i, iTemp, mStruct, p_ReturnBody);

                    iTemp++;
                }
            }
            return p_ReturnBody;*/
        }
        private PatchMessage[,] DecodeRecive(int iRow, int iField, TLV_Structure tTlv, PatchMessage[,] tBody)
        {
            switch (tTlv.m_Tag)
            {
                case TagName.UpdateFileName:// 0x0101 Format:STRING
                    tBody[iRow, iField].eTag = TagFormat.TLV_STRING;
                    tBody[iRow, iField].oContent = tTlv.toString();
                    break;
                case TagName.UpdateFilePath:// 0x0102 Format:STRING
                    tBody[iRow, iField].eTag = TagFormat.TLV_STRING;
                    tBody[iRow, iField].oContent = tTlv.toString();
                    break;
                case TagName.UpdateFileVersion:// 0x0103 Format:STRING
                    tBody[iRow, iField].eTag = TagFormat.TLV_STRING;
                    tBody[iRow, iField].oContent = tTlv.toString();
                    break;
                case TagName.UpdateFileSize:// 0x0104Format:DateTime
                    tBody[iRow, iField].eTag = TagFormat.TLV_DATE;
                    tBody[iRow, iField].oContent = tTlv.toInteger();
                    break;
            }

            return tBody;

        }
		private void sendBinary(ref Socket socket, string filePath)
		{
			try
			{
				long readcount = filePath.Length / 10240;
				long byteremain = filePath.Length % 10240;
				byte[] bb = new byte[10240];
				int number;
				stream = new NetworkStream(socket);
				filestream = File.OpenRead("aa.dll");
				for (long count1 = 1; count1 <= readcount; count1++)
				{
					number = filestream.Read(bb, 0, bb.Length);
					stream.Write(bb, 0, bb.Length);
					stream.Flush();
					bb = new byte[10240];
				}
				if (byteremain != 0)
				{
					bb = new byte[byteremain];
					filestream.Read(bb, 0, bb.Length);
					stream.Write(bb, 0, bb.Length);
					stream.Flush();
				}
				filestream.Close();
				stream.Close();
			}
			catch (System.Exception f)
			{
				Console.Write(f.ToString(), "sendbinary errer");
			}
		}
			//接收文件
			private void getBinary(ref Socket socket, string filePath)
			{
				try
				{
					filestream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
					byte[] bb = new byte[10240];
					NetworkStream netStream = new NetworkStream(socket);
					long readnumber;
					long currentread = 0;
					long readcount = Convert.ToInt64("111") / 10240;
					long byteremain = Convert.ToInt64("222") % 10240;
					while ((readnumber = netStream.Read(bb, 0, bb.Length)) > 0 && currentread != readcount)
					{
						currentread++;
						filestream.Write(bb, 0, bb.Length);
						filestream.Flush();
						if (currentread == readcount)
						{
							if (byteremain != 0)
							{
								bb = new byte[byteremain];
								netStream.Read(bb, 0, bb.Length);
								filestream.Write(bb, 0, bb.Length);
								filestream.Flush();
							}
							break;
						}
					}
					filestream.Close();
					netStream.Close();

				}
				catch (System.Exception f)
				{
					Console.Write("接收文件时错误" + f.ToString() + "\r\n");
				}
			}

	}
}
