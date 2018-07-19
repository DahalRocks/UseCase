using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;

/// <summary>
/// changes data into different foramats
/// </summary>
public class FileProcesser
{
	public FileProcesser()
	{
		
	}
    //change string to stream
    public static MemoryStream StringToStream(string s)
    {
        MemoryStream stream = new MemoryStream();
        StreamWriter writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }


    //change stream to string
    public static string StreamToString(FileStream strm)
    {
        string content;
        using (StreamReader reader = new StreamReader(strm, Encoding.Unicode))
        {
            content = reader.ReadToEnd();
        }
        return content;
    }


    //change stream to byte array
    public static byte[] StreamToByte(Stream input)
    {
        byte[] buffer = new byte[16 * 1024];
        using (MemoryStream ms = new MemoryStream())
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }

            return ms.ToArray();
        }
    }
}