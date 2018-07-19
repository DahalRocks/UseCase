using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// wash hex out from xml file
/// </summary>
public class HexFilter
{
	public HexFilter()
	{
		
	}

    //make file clear

    public static string MakeFileClear(string filePath)
    {
        string content;
        using (FileStream stream = File.OpenRead(filePath))
        {
            content = RemoveInvalidXmlChars(FileProcesser.StreamToString(stream));
        }
        return content;
    }
    
    //make file stream clear

    public static MemoryStream MakeFileStreamClear(FileStream stream)
    {
        string dirtyText = FileProcesser.StreamToString(stream);
        string clearText = RemoveInvalidXmlChars(dirtyText);
        return FileProcesser.StringToStream(clearText);
    
    }
    
    private static string RemoveInvalidXmlChars(string text)
    {
        string og = text.Replace("&#x0;.", string.Empty);
        og = og.Replace("&#x1;", string.Empty);
        og = og.Replace("&#x2;", string.Empty);
        og = og.Replace("&#x3;", string.Empty);
        og = og.Replace("&#x4;", string.Empty);
        og = og.Replace("&#x5;", string.Empty);
        og = og.Replace("&#x6;", string.Empty);
        og = og.Replace("&#x7;", string.Empty);
        og = og.Replace("&#xB;", string.Empty);
        return og;

    }
}