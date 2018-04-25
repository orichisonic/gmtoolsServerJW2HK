using System;
using System.Net.Sockets;
using System.IO;
using lg = Common.API.LanguageAPI;

namespace MainFrame
{
	/// <summary>
	/// UpdatePacth 的摘要说明。
	/// </summary>
	public class UpdatePacth
	{
		NetworkStream stream;
		FileStream filestream;
		public UpdatePacth()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
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
			catch (Exception f)
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
				catch (Exception f)
				{
					Console.Write(lg.ServerSocket_UpdatePatch_Error + f.ToString() + "\r\n");
				}
			}

	}
}
