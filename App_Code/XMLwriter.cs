using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spire.Doc;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;

/// <summary>
/// writes xml file
/// </summary>
public class XMLwriter
{
	
    public XMLwriter()
	{
	    
	}

    public static string CreateXml(List<DescriptionSecondTable> lst,string usecasename)
    {
        string xml = "";
        xml += @"<mediawiki><page>";
        xml += "<title>" + usecasename + "</title>";
        xml += "<text xml:space=\"preserve\">";
        xml += "dhruba be carefully for exporting file ok";
        xml += "</text>";

        xml += "</page></mediawiki>";

        return xml;
    
    }

    

}