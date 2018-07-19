using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml.Linq;

/// <summary>
/// Use-case xml document parser
/// </summary>
public class XMLParser
{

   GoogleTranslate objGoogle = new GoogleTranslate();
   public string file { get; set; }
   public string testwikioriginal { get; set; }
   public string testwikienglish { get; set; }
   NameSpace obj;
   public XMLParser()
	{
		
	}
   public XMLParser(string file,NameSpace obj)
    {
        this.obj = obj; 
        this.file = file;
        GetUseCaseName();
        GetDescription();
        TestFunction();
    }
   
    
   public DescriptionFirstTable firsttable{get;set;}
   public DescriptionSecondTable secondtable { get; set; }
   public DescriptionThirdTable thirdtable { get; set; }
   public DescriptionFourthTable fourthtable { get; set; }
   public DescriptionFifthTable fifthtable { get; set; }
   public List<DescriptionSecondTable> lstTable { get; set; }
    
   public TechnicalDetailFirstTable techfirsttable { get; set; }
   public TechnicalDetailSecondTable techsecondtable { get; set; }
   public TechnicalDetailThirdTable techthirdtable { get; set; }
   public TechnicalDetailFourthTable techfourthtable { get; set; }
   public List<TechnicalDetailFirstTable> lstTechTable { get; set; }
   public List<TechnicalDetailSecondTable> lstTechSecondTable { get; set; }
   public List<TechnicalDetailThirdTable> lstTechThirdTable { get; set; }
  
   public StudyFirstTable studyfirsttable { get; set; }
   public StudySecondTable studysecondtable { get; set; }
   public List<StudyFirstTable> lstStudyTable { get; set; }
   public List<StudySecondTable> lstStudySecondTable { get; set; }
   public List<StudySecondTable> lstStudyThirdTable { get; set; }

   public InfoFirstTable infofirsttable { get; set; }
   public List<InfoFirstTable> lstInfoTable { get; set; }

   public TechnicalRequirement technicalreqtable { get; set; }
   public TechnicalRequirementSecondPart technicalreqtablesecond { get; set; }
   public List<TechnicalRequirement> lstTechnicalReq { get; set; }
   public List<TechnicalRequirementSecondPart> lstTechnicalReqsecond { get; set; }


   public TermsAndDefinitions termdefinitiontable { get; set; }
   public List<TermsAndDefinitions> lstTermDefinition { get; set; }
  
   
   public string usecasename { get; set; }

   string content = "NAVN";
    
    public void GetUseCaseName()
    {

        string fullcontent = "";
        //string clearFile = HexFilter.MakeFileClear(Server.MapPath("~/XMLDocument/prexml.xml"));
        string clearFile = file;
        Stream s = FileProcesser.StringToStream(clearFile);
        XDocument xml = XDocument.Load(s);

        var T = xml.Descendants()
              .Where(rc => rc.Name == "item" && rc.Ancestors("sections").Any())
              .Where(rc => rc.Name == "item" && rc.Ancestors("section").Any())
              .Where(rc => rc.Name == "item" && rc.Ancestors("body").Any())
              .Where(n => n.Parent.Name == "paragraphs")

              .Select(c => c).ToList();


        bool suspect = false;

        foreach (var lst in T)
        {

            if (suspect)
            {

                XDocument xmlC = XDocument.Load(FileProcesser.StringToStream(lst.ToString()));
                var TC = xmlC.Descendants()
                        .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                        .Where(n => n.Parent.Name == "item")
                        .Select(c => (string)c).ToList();
                foreach (var lstS in TC)
                {
                    fullcontent += lstS.ToString() + "";
                }

                suspect = false;
            }
            string look = lst.ToString();

            if (look.Contains(content))
            {
                suspect = true;

            }


        }
        this.usecasename=fullcontent;
    }


   /* public string GetIntroduction(string content)
    {

        string fullcontent = "";
        string clearFile = HexFilter.MakeFileClear(Server.MapPath("~/XMLDocument/prexml.xml"));
        Stream s = FileProcesser.StringToStream(clearFile);
        XDocument xml = XDocument.Load(s);

        var T = xml.Descendants()
              .Where(rc => rc.Name == "item" && rc.Ancestors("sections").Any())
              .Where(rc => rc.Name == "item" && rc.Ancestors("section").Any())
              .Where(rc => rc.Name == "item" && rc.Ancestors("body").Any())
              .Where(n => n.Parent.Name == "paragraphs")

              .Select(c => c).ToList();


        bool suspect = false;

        foreach (var lst in T)
        {

            if (suspect)
            {

                XDocument xmlC = XDocument.Load(FileProcesser.StringToStream(lst.ToString()));
                var TC = xmlC.Descendants()
                        .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                        .Where(n => n.Parent.Name == "item")
                        .Select(c => (string)c).ToList();
                foreach (var lstS in TC)
                {
                    fullcontent += lstS.ToString() + "";
                }

                suspect = false;
            }
            string look = lst.ToString();
            string[] typical = look.Split('>');
            for (int i = 0; i < typical.Length; i++)
            {
                string text = typical[i].ToString();
                if (text.Contains(content))
                {
                    suspect = true;

                    XDocument xmlC = XDocument.Load(FileProcesser.StringToStream(lst.ToString()));
                    var TC = xmlC.Descendants()
                            .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                            .Where(n => n.Parent.Name == "item")
                            .Select(c => (string)c).ToList();
                    foreach (var lstS in TC)
                    {
                        fullcontent += lstS.ToString() + "";
                    }

                }

            }
        }
        return fullcontent;
    }*/


    //public void GetDescription()
    //{
        
    //    string clearFile = file;
    //    int count = 1;
    //    Stream s = FileProcesser.StringToStream(clearFile);
    //    XDocument xml = XDocument.Load(s);
    //    var T = xml.Descendants()
    //          .Where(rc => rc.Name == "item" && rc.Ancestors("sections").Any())
    //          .Where(rc => rc.Name == "item" && rc.Ancestors("section").Any())
    //          .Where(rc => rc.Name == "item" && rc.Ancestors("body").Any())
    //          .Where(n => n.Parent.Name == "paragraphs")
    //          .Select(c => c).ToList();

    //    foreach (var lst in T)
    //    {
    //         XDocument xmlC = XDocument.Load(FileProcesser.StringToStream(lst.ToString()));
             
    //         string look = lst.ToString();
            

    //        if (look.Contains("table") )
    //        {
                
    //            if (count == 1)
    //            {
    //                ParseFirstTable(xmlC);
                
    //            }

    //            else if (count == 2)
    //            {
    //                ParseSecondTable(xmlC);
                
    //            }

    //            else if (count == 3)
    //            {
    //                ParseThirdTable(xmlC);
                
    //            }
               
                
    //           else if (count == 4)
    //            {
    //                ParseFourthTable(xmlC);

    //            }

    //            else if (count == 5)
    //            {
    //                ParseFifthTable(xmlC);

    //            }

    //            else if (count == 7)
    //            {
    //                ParseTechFirstTable(xmlC);

    //            }
    //            else if (count == 8)
    //            {
    //                ParseTechSecondTable(xmlC);

    //            }

    //            else if (count == 9)
    //            {
    //                ParseTechThirdTable(xmlC);

    //            }
    //            else if (count == 10)
    //            {
    //                ParseTechFourthTable(xmlC);

    //            }
    //            else if (count == 11)
    //            {
    //                ParseStudyFirstTable(xmlC);

    //            }

    //            else if (count == 12)
    //            {
    //                ParseStudySecondTable(xmlC);

    //            }
    //            else if (count == 13)
    //            {
    //                ParseStudyThirdTable(xmlC);

    //            }

    //            else if (count == 14)
    //            {
    //                ParseInfoFirstTable(xmlC);

    //            }
    //            else if (count == 15)
    //            {
    //                ParseTechnicalRequirementTable(xmlC);

    //            }
    //            else if (count == 16)
    //            {
    //                ParseTermsDefinitionsTable(xmlC);

    //            }
    //            count++;

    //        }
           


    //    }
       
    //}



    public void GetDescription()
    {

        string clearFile = file;
        int count = 1;
        Stream s = FileProcesser.StringToStream(clearFile);
        XDocument xml = XDocument.Load(s);
        var T = xml.Descendants()
              .Where(rc => rc.Name == "item" && rc.Ancestors("sections").Any())
              .Where(rc => rc.Name == "item" && rc.Ancestors("section").Any())
              .Where(rc => rc.Name == "item" && rc.Ancestors("body").Any())
              .Where(n => n.Parent.Name == "paragraphs")
              .Select(c => c).ToList();

        foreach (var lst in T)
        {
            XDocument xmlC = XDocument.Load(FileProcesser.StringToStream(lst.ToString()));

            string look = lst.ToString();


            if (look.Contains("table"))
            {

                if (count == 1)
                {
                    ParseFirstTable(xmlC);

                }

                else if (count == 2)
                {
                    ParseSecondTable(xmlC);

                }

                else if (count == 3)
                {
                    ParseThirdTable(xmlC);

                }


                else if (count == 4)
                {
                    ParseFourthTable(xmlC);

                }

                else if (count == 5)
                {
                    ParseFifthTable(xmlC);

                }

                else if (count == 7)
                {
                    ParseTechFirstTable(xmlC);

                }
                else if (count == 8)
                {
                    ParseTechSecondTable(xmlC);

                }

                else if (count == 9)
                {
                    ParseTechThirdTable(xmlC);

                }
                else if (count == 10)
                {
                    ParseTechFourthTable(xmlC);

                }
                else if (count == 11)
                {
                    ParseStudyFirstTable(xmlC);

                }

                else if (count == 12)
                {
                    ParseStudySecondTable(xmlC);

                }
                //else if (count == 13)
                //{
                //    ParseStudyThirdTable(xmlC);

                //}

                else if (count == 13)
                {
                    ParseInfoFirstTable(xmlC);

                }
                else if (count == 14)
                {
                    ParseTechnicalRequirementTable(xmlC);

                }
                else if (count == 15)
                {
                    ParseTermsDefinitionsTable(xmlC);

                }
                count++;

            }



        }

    }

    public void TestFunction()
    {
        string clearFile = file;
        
        Stream s = FileProcesser.StringToStream(clearFile);
        XDocument xml = XDocument.Load(s);
        var T = xml.Descendants()
              .Where(rc => rc.Name == "item" && rc.Ancestors("sections").Any())
              .Where(rc => rc.Name == "item" && rc.Ancestors("section").Any())
              .Where(rc => rc.Name == "item" && rc.Ancestors("body").Any())
              .Where(n => n.Parent.Name == "paragraphs")
              .Select(c => c).ToList();
        foreach (var lst in T)
        {
            XDocument xmlC = XDocument.Load(FileProcesser.StringToStream(lst.ToString()));

            string look = lst.ToString();

            string tableHeading = string.Empty;
            if (look.Contains("table"))
            {
                XNode XNODE=lst.PreviousNode;
                
                if (XNODE != null)
                {
                    var heading = (XNODE as XElement).Descendants()
                                .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                                  .Where(n => n.Parent.Name == "item")
                                  .Select(c => (string)c).ToList();
               
                
                    foreach(var h in heading)
                    {
                        tableHeading += h;
                
                    }
                }
                this.testwikioriginal += ParseTestFunction(xmlC, tableHeading, "original");
                if(obj.englishversion)
                this.testwikienglish += ParseTestFunction(xmlC, tableHeading, "english");
            }
        }
        if (obj.englishversion)
        this.testwikioriginal+="[["+objGoogle.MicrosoftTranslate(obj.namespaces+":"+usecasename)+"]]";
    
    }
   
    //................................description tables.........................................//

    public string ParseTestFunction(XDocument xdoc,string tableHeading,string version)
    {

        
         string  general_description = string.Empty;
            
        if (tableHeading.Length > 0)
        {
            if (version != "original")
                general_description += "==" + objGoogle.MicrosoftTranslate(tableHeading) + "==" + "\n";
               // general_description += "==" + tableHeading + "==" + "/n" ;
            else
                general_description += "==" + tableHeading + "==" + "\n"; 
        }


        general_description += "{| class=\"wikitable\"" + "\n";
        var rows = xdoc.Descendants()
                    .Where(rc => rc.Name == "row" && rc.Ancestors("item").Any())
                    .Where(n => n.Parent.Name == "rows")
                    .Select(c => c).ToList();
        int rowcount = 1;
        foreach (var lstrows in rows)
        {

            general_description += "|-" + "\n";
            if (rowcount == 1)
                general_description +="!";
            else
                general_description +="|";
            
            var cols = lstrows.Descendants()
                    .Where(rc => rc.Name == "cell" && rc.Ancestors("row").Any())
                    .Where(n => n.Parent.Name == "cells")
                    .Select(c => c).ToList();
            foreach (var lstcols in cols)
            {

                var text = lstcols.Descendants()
                    .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                    .Where(n => n.Parent.Name == "item")
                    .Select(c => (string)c).ToList();
                string parText = "";
                foreach (var lsttext in text)
                {
                    
                    parText += lsttext;
                    
                }
                
                if (version != "original")
                    general_description += objGoogle.MicrosoftTranslate(parText);
                    //general_description += parText;
                else
                    general_description += parText;
                
               
                if (cols.Count > 1)
                {
                    if (rowcount == 1)
                        general_description += "!!";
                    else
                        general_description +="||";
                }
                
                
            }
            general_description += "\n";
            rowcount++;
        }
        general_description += "|}" + "\n";
        

        return general_description;

    }

    public void ParseFirstTable(XDocument xdoc)
    {
        var TC = xdoc.Descendants()
                        .Where(rc => rc.Name == "cell" && rc.Ancestors("rows").Any())
                        .Where(rc => rc.Name == "cell" && rc.Ancestors("row").Any())
                        .Where(n => n.Parent.Name == "cells")
                        .Select(c => c).ToList();
        int count = 1;
        firsttable = new DescriptionFirstTable();
        foreach (var lstS in TC)
        {
            if (count == 4)
            {
                firsttable.ID = lstS.Value;
            }
            else if (count == 5)
            {

                firsttable.Areadomain = lstS.Value;
            }
            else if (count == 6)
            {

                var TC6 = lstS.Descendants()
                        .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                        .Where(n => n.Parent.Name == "item")
                        .Select(c => (string)c).ToList();
                foreach (var lst6 in TC6)
                {
                    firsttable.Name = lst6.ToString();
                }

            }
            count++;
        }

    }


    public void ParseSecondTable(XDocument xdoc) 
    {
        lstTable = new List<DescriptionSecondTable>();
        secondtable = new DescriptionSecondTable();

        var rows = xdoc.Descendants()
                    .Where(rc => rc.Name == "row" && rc.Ancestors("item").Any())
                    .Where(n => n.Parent.Name == "rows")
                    .Select(c => c).ToList();
        int rowcount = 1;
        foreach (var lstrows in rows)
        {
            var cols = lstrows.Descendants()
                    .Where(rc => rc.Name == "cell" && rc.Ancestors("row").Any())
                    .Where(n => n.Parent.Name == "cells")
                    .Select(c => c).ToList();
            int colcount = 1;
            foreach (var lstcols in cols)
            {

                var text = lstcols.Descendants()
                    .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                    .Where(n => n.Parent.Name == "item")
                    .Select(c => (string)c).ToList();

                foreach (var lsttext in text)
                {
                    if (colcount == 1)
                    {
                        secondtable.Version += lsttext + "";

                    }
                    else if (colcount == 2)
                    {
                        secondtable.Date += lsttext + "";

                    }
                    else if (colcount == 3)
                    {
                        secondtable.AuthorNameCommitte += lsttext + "";

                    }
                    else if (colcount == 4)
                    {
                        secondtable.Changes += lsttext + "";

                    }
                    else if (colcount == 5)
                    {
                        secondtable.UseCaseStatus += lsttext + "";

                    }
                }
                colcount++;
            }
            lstTable.Add(secondtable);
            secondtable = new DescriptionSecondTable();
            rowcount++;
        }


    }
   

    public void ParseThirdTable(XDocument xdoc)
    {
        
        thirdtable = new DescriptionThirdTable();
        var rows = xdoc.Descendants()
                    .Where(rc => rc.Name == "row" && rc.Ancestors("item").Any())
                    .Where(n => n.Parent.Name == "rows")
                    .Select(c => c).ToList();
        int rowcount = 1;
        foreach (var lstrows in rows)
        {
            var cols = lstrows.Descendants()
                    .Where(rc => rc.Name == "cell" && rc.Ancestors("row").Any())
                    .Where(n => n.Parent.Name == "cells")
                    .Select(c => c).ToList();
            int colcount = 1;
            foreach (var lstcols in cols)
            {
               
                var text=lstcols.Descendants()
                    .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                    .Where(n => n.Parent.Name == "item")
                    .Select(c =>(string) c).ToList();
                    
                foreach(var lsttext in text){
                    if (rowcount == 2 && colcount==2)
                    {
                        thirdtable.Area += lsttext + "";
                
                    }
                    else if (rowcount == 3 && colcount == 2)
                    {
                        thirdtable.Goal += lsttext + "";
                    
                    }
                    else if (rowcount == 4 && colcount == 2)
                    {
                        thirdtable.RelatedBusinessCase += lsttext + "";

                    }
                }
                colcount++;
            }
            rowcount++;
        }

    }


    public void ParseFourthTable(XDocument xdoc)
    {

        fourthtable = new DescriptionFourthTable();
        var rows = xdoc.Descendants()
                    .Where(rc => rc.Name == "row" && rc.Ancestors("item").Any())
                    .Where(n => n.Parent.Name == "rows")
                    .Select(c => c).ToList();
        int rowcount = 1;
        foreach (var lstrows in rows)
        {
            var cols = lstrows.Descendants()
                    .Where(rc => rc.Name == "cell" && rc.Ancestors("row").Any())
                    .Where(n => n.Parent.Name == "cells")
                    .Select(c => c).ToList();
            int colcount = 1;
            foreach (var lstcols in cols)
            {

                var text = lstcols.Descendants()
                    .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                    .Where(n => n.Parent.Name == "item")
                    .Select(c => (string)c).ToList();

                foreach (var lsttext in text)
                {
                    if (rowcount == 3)
                    {
                        fourthtable.ShortDescription += lsttext + "";

                    }
                    else if (rowcount == 5)
                    {
                        fourthtable.CompleteDescription += lsttext + "";

                    }
                    
                }
                colcount++;
            }
            rowcount++;
        }

    }


    public void ParseFifthTable(XDocument xdoc)
    {

        fifthtable = new DescriptionFifthTable();
        var rows = xdoc.Descendants()
                    .Where(rc => rc.Name == "row" && rc.Ancestors("item").Any())
                    .Where(n => n.Parent.Name == "rows")
                    .Select(c => c).ToList();
        int rowcount = 1;
        foreach (var lstrows in rows)
        {
            var cols = lstrows.Descendants()
                    .Where(rc => rc.Name == "cell" && rc.Ancestors("row").Any())
                    .Where(n => n.Parent.Name == "cells")
                    .Select(c => c).ToList();
            int colcount = 1;
            foreach (var lstcols in cols)
            {

                var text = lstcols.Descendants()
                    .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                    .Where(n => n.Parent.Name == "item")
                    .Select(c => (string)c).ToList();

                foreach (var lsttext in text)
                {
                    if (rowcount == 2)
                    {
                        fifthtable.AnyComments += lsttext + "";

                    }

                }
                colcount++;
            }
            rowcount++;
        }

    }

  


    //.................................technical description tables....................................//
    public void ParseTechFirstTable(XDocument xdoc)
    {

        techfirsttable = new TechnicalDetailFirstTable();
        lstTechTable = new List<TechnicalDetailFirstTable>();
        var rows = xdoc.Descendants()
                    .Where(rc => rc.Name == "row" && rc.Ancestors("item").Any())
                    .Where(n => n.Parent.Name == "rows")
                    .Select(c => c).ToList();
        int rowcount = 1;

        foreach (var lstrows in rows)
        {
            if (rowcount > 1)
            {
                var cols = lstrows.Descendants()
                        .Where(rc => rc.Name == "cell" && rc.Ancestors("row").Any())
                        .Where(n => n.Parent.Name == "cells")
                        .Select(c => c).ToList();
                int colcount = 1;
                foreach (var lstcols in cols)
                {

                    var text = lstcols.Descendants()
                        .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                        .Where(n => n.Parent.Name == "item")
                        .Select(c => (string)c).ToList();

                    foreach (var lsttext in text)
                    {
                        if (colcount == 1)
                        {
                            techfirsttable.PlayerName += lsttext + "";

                        }
                        else if (colcount == 2)
                        {
                            techfirsttable.PerformerType += lsttext + "";

                        }
                        else if (colcount == 3)
                        {
                            techfirsttable.PerformerDescription += lsttext + "";

                        }
                        else if (colcount == 4)
                        {
                            techfirsttable.AdditionalInfo += lsttext + "";

                        }

                    }
                    colcount++;
                }
                lstTechTable.Add(techfirsttable);
                techfirsttable = new TechnicalDetailFirstTable();
            }
            rowcount++;
        }

    }

    public void ParseTechSecondTable(XDocument xdoc)
    {

        techsecondtable = new TechnicalDetailSecondTable();
        lstTechSecondTable = new List<TechnicalDetailSecondTable>();
        var rows = xdoc.Descendants()
                    .Where(rc => rc.Name == "row" && rc.Ancestors("item").Any())
                    .Where(n => n.Parent.Name == "rows")
                    .Select(c => c).ToList();
        int rowcount = 1;
        foreach (var lstrows in rows)
        {
            if (rowcount > 1)
            {
                var cols = lstrows.Descendants()
                        .Where(rc => rc.Name == "cell" && rc.Ancestors("row").Any())
                        .Where(n => n.Parent.Name == "cells")
                        .Select(c => c).ToList();
                int colcount = 1;
                foreach (var lstcols in cols)
                {

                    var text = lstcols.Descendants()
                        .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                        .Where(n => n.Parent.Name == "item")
                        .Select(c => (string)c).ToList();

                    foreach (var lsttext in text)
                    {
                        if (colcount == 1)
                        {
                            techsecondtable.Actor += lsttext + "";

                        }
                        else if (colcount == 2)
                        {
                            techsecondtable.TriggeringEvent += lsttext + "";

                        }
                        else if (colcount == 3)
                        {
                            techsecondtable.StartConditions += lsttext + "";

                        }
                        else if (colcount == 4)
                        {
                            techsecondtable.Assumptions += lsttext + "";

                        }

                    }
                    colcount++;
                }
                lstTechSecondTable.Add(techsecondtable);
                techsecondtable = new TechnicalDetailSecondTable();
            }
            rowcount++;
        }

    }


    public void ParseTechThirdTable(XDocument xdoc)
    {

        techthirdtable = new TechnicalDetailThirdTable();
        lstTechThirdTable = new List<TechnicalDetailThirdTable>();
        var rows = xdoc.Descendants()
                    .Where(rc => rc.Name == "row" && rc.Ancestors("item").Any())
                    .Where(n => n.Parent.Name == "rows")
                    .Select(c => c).ToList();
        int rowcount = 1;
        foreach (var lstrows in rows)
        {
            if (rowcount > 1)
            {
                var cols = lstrows.Descendants()
                        .Where(rc => rc.Name == "cell" && rc.Ancestors("row").Any())
                        .Where(n => n.Parent.Name == "cells")
                        .Select(c => c).ToList();
                int colcount = 1;
                foreach (var lstcols in cols)
                {

                    var text = lstcols.Descendants()
                        .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                        .Where(n => n.Parent.Name == "item")
                        .Select(c => (string)c).ToList();

                    foreach (var lsttext in text)
                    {
                        if (colcount == 1)
                        {
                            techthirdtable.No += lsttext + "";

                        }
                        else if (colcount == 2)
                        {
                            techthirdtable.ReferenceType += lsttext + "";

                        }
                        else if (colcount == 3)
                        {
                            techthirdtable.Reference += lsttext + "";

                        }
                        else if (colcount == 4)
                        {
                            techthirdtable.Status += lsttext + "";

                        }
                        else if (colcount == 5)
                        {
                            techthirdtable.Implications += lsttext + "";

                        }
                        else if (colcount == 6)
                        {
                            techthirdtable.Organization += lsttext + "";

                        }
                        else if (colcount == 7)
                        {
                            techthirdtable.Link += lsttext + "";

                        }

                    }
                    colcount++;
                }
                lstTechThirdTable.Add(techthirdtable);
                techthirdtable = new TechnicalDetailThirdTable();
            }
            rowcount++;
        }

    }


    public void ParseTechFourthTable(XDocument xdoc)
    {

        techfourthtable = new TechnicalDetailFourthTable();
        var rows = xdoc.Descendants()
                    .Where(rc => rc.Name == "row" && rc.Ancestors("item").Any())
                    .Where(n => n.Parent.Name == "rows")
                    .Select(c => c).ToList();
        int rowcount = 1;
        foreach (var lstrows in rows)
        {
            if (rowcount > 1)
            {
                var cols = lstrows.Descendants()
                        .Where(rc => rc.Name == "cell" && rc.Ancestors("row").Any())
                        .Where(n => n.Parent.Name == "cells")
                        .Select(c => c).ToList();
                int colcount = 1;
                foreach (var lstcols in cols)
                {

                    var text = lstcols.Descendants()
                        .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                        .Where(n => n.Parent.Name == "item")
                        .Select(c => (string)c).ToList();

                    foreach (var lsttext in text)
                    {
                        if (rowcount == 3)
                        {
                            techfourthtable.RelationToOther += lsttext + "";

                        }
                        else if (rowcount == 5)
                        {
                            techfourthtable.Level += lsttext + "";

                        }
                        else if (rowcount == 7)
                        {
                            techfourthtable.Priorities += lsttext + "";

                        }
                        else if (rowcount == 9)
                        {
                            techfourthtable.Interest += lsttext + "";

                        }
                        else if (rowcount == 11)
                        {
                            techfourthtable.Orientation += lsttext + "";

                        }
                        else if (rowcount == 13)
                        {
                            techfourthtable.KeyWords += lsttext + "";

                        }
                        

                    }
                    colcount++;
                }
                
            }
            rowcount++;
        }

    }



    //............................. use case study step by step......................................//
    public void ParseStudyFirstTable(XDocument xdoc)
    {

        studyfirsttable = new StudyFirstTable();
        lstStudyTable = new List<StudyFirstTable>();
        var rows = xdoc.Descendants()
                    .Where(rc => rc.Name == "row" && rc.Ancestors("item").Any())
                    .Where(n => n.Parent.Name == "rows")
                    .Select(c => c).ToList();
        int rowcount = 1;
        foreach (var lstrows in rows)

        {
            if (rowcount > 1)
            {

                var cols = lstrows.Descendants()
                        .Where(rc => rc.Name == "cell" && rc.Ancestors("row").Any())
                        .Where(n => n.Parent.Name == "cells")
                        .Select(c => c).ToList();
                int colcount = 1;
                foreach (var lstcols in cols)
                {

                    var text = lstcols.Descendants()
                        .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                        .Where(n => n.Parent.Name == "item")
                        .Select(c => (string)c).ToList();

                    foreach (var lsttext in text)
                    {
                        if (colcount == 1)
                        {
                            studyfirsttable.No += lsttext + "";

                        }
                        else if (colcount == 2)
                        {
                            studyfirsttable.Notification += lsttext + "";

                        }
                        else if (colcount == 3)
                        {
                            studyfirsttable.PrimaryActor += lsttext + "";

                        }
                        else if (colcount == 4)
                        {
                            studyfirsttable.TriggeringEvent += lsttext + "";

                        }
                        else if (colcount == 5)
                        {
                            studyfirsttable.StartCondition += lsttext + "";

                        }
                        else if (colcount == 6)
                        {
                            studyfirsttable.FinalTerms += lsttext + "";

                        }

                    }
                    colcount++;
                }
                lstStudyTable.Add(studyfirsttable);
                studyfirsttable = new StudyFirstTable();
            }
            rowcount++;
        }

    }


    public void ParseStudySecondTable(XDocument xdoc)
    {

        studysecondtable = new StudySecondTable();
        lstStudySecondTable = new List<StudySecondTable>();
        var rows = xdoc.Descendants()
                    .Where(rc => rc.Name == "row" && rc.Ancestors("item").Any())
                    .Where(n => n.Parent.Name == "rows")
                    .Select(c => c).ToList();
        int rowcount = 1;
        foreach (var lstrows in rows)
        {
            if (rowcount > 2)
            {

                var cols = lstrows.Descendants()
                        .Where(rc => rc.Name == "cell" && rc.Ancestors("row").Any())
                        .Where(n => n.Parent.Name == "cells")
                        .Select(c => c).ToList();
                int colcount = 1;
                foreach (var lstcols in cols)
                {

                    var text = lstcols.Descendants()
                        .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                        .Where(n => n.Parent.Name == "item")
                        .Select(c => (string)c).ToList();

                    foreach (var lsttext in text)
                    {
                        if (colcount == 1)
                        {
                            studysecondtable.StepRanked += lsttext + "";

                        }
                        else if (colcount == 2)
                        {
                            studysecondtable.HendElse += lsttext + "";

                        }
                        else if (colcount == 3)
                        {
                            studysecondtable.Activity += lsttext + "";

                        }
                        else if (colcount == 4)
                        {
                            studysecondtable.ProcessDescription += lsttext + "";

                        }
                        else if (colcount == 5)
                        {
                            studysecondtable.Service += lsttext + "";

                        }
                        else if (colcount == 6)
                        {
                            studysecondtable.InformationCreates += lsttext + "";

                        }
                        else if (colcount == 7)
                        {
                            studysecondtable.InformationReceiver += lsttext + "";

                        }
                        else if (colcount == 8)
                        {
                            studysecondtable.InformationExchanged += lsttext + "";

                        }
                        else if (colcount == 9)
                        {
                            studysecondtable.TechnicalRequirements += lsttext + "";

                        }

                    }
                    colcount++;
                }
                lstStudySecondTable.Add(studysecondtable);
                studysecondtable = new StudySecondTable();
            }
            rowcount++;
        }

    }



    public void ParseStudyThirdTable(XDocument xdoc)
    {

        studysecondtable = new StudySecondTable();
        lstStudyThirdTable = new List<StudySecondTable>();
        var rows = xdoc.Descendants()
                    .Where(rc => rc.Name == "row" && rc.Ancestors("item").Any())
                    .Where(n => n.Parent.Name == "rows")
                    .Select(c => c).ToList();
        int rowcount = 1;
        foreach (var lstrows in rows)
        {
            if (rowcount > 2)
            {

                var cols = lstrows.Descendants()
                        .Where(rc => rc.Name == "cell" && rc.Ancestors("row").Any())
                        .Where(n => n.Parent.Name == "cells")
                        .Select(c => c).ToList();
                int colcount = 1;
                foreach (var lstcols in cols)
                {

                    var text = lstcols.Descendants()
                        .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                        .Where(n => n.Parent.Name == "item")
                        .Select(c => (string)c).ToList();

                    foreach (var lsttext in text)
                    {
                        if (colcount == 1)
                        {
                            studysecondtable.StepRanked += lsttext + "";

                        }
                        else if (colcount == 2)
                        {
                            studysecondtable.HendElse += lsttext + "";

                        }
                        else if (colcount == 3)
                        {
                            studysecondtable.Activity += lsttext + "";

                        }
                        else if (colcount == 4)
                        {
                            studysecondtable.ProcessDescription += lsttext + "";

                        }
                        else if (colcount == 5)
                        {
                            studysecondtable.Service += lsttext + "";

                        }
                        else if (colcount == 6)
                        {
                            studysecondtable.InformationCreates += lsttext + "";

                        }
                        else if (colcount == 7)
                        {
                            studysecondtable.InformationReceiver += lsttext + "";

                        }
                        else if (colcount == 8)
                        {
                            studysecondtable.InformationExchanged += lsttext + "";

                        }
                        else if (colcount == 9)
                        {
                            studysecondtable.TechnicalRequirements += lsttext + "";

                        }

                    }
                    colcount++;
                }
                lstStudyThirdTable.Add(studysecondtable);
                studysecondtable = new StudySecondTable();
            }
            rowcount++;
        }

    }


    //.............................information to be exchanged.........................................//
    public void ParseInfoFirstTable(XDocument xdoc)
    {

        infofirsttable = new InfoFirstTable();
        lstInfoTable = new List<InfoFirstTable>();
        var rows = xdoc.Descendants()
                    .Where(rc => rc.Name == "row" && rc.Ancestors("item").Any())
                    .Where(n => n.Parent.Name == "rows")
                    .Select(c => c).ToList();
        int rowcount = 1;
        foreach (var lstrows in rows)
        {
            if (rowcount > 2)
            {

                var cols = lstrows.Descendants()
                        .Where(rc => rc.Name == "cell" && rc.Ancestors("row").Any())
                        .Where(n => n.Parent.Name == "cells")
                        .Select(c => c).ToList();
                int colcount = 1;
                foreach (var lstcols in cols)
                {

                    var text = lstcols.Descendants()
                        .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                        .Where(n => n.Parent.Name == "item")
                        .Select(c => (string)c).ToList();

                    foreach (var lsttext in text)
                    {
                        if (colcount == 1)
                        {
                            infofirsttable.ID += lsttext + "";

                        }
                        else if (colcount == 2)
                        {
                            infofirsttable.Description += lsttext + "";

                        }
                        else if (colcount == 3)
                        {
                            infofirsttable.RequiredInfo += lsttext + "";

                        }
                        
                    }
                    colcount++;
                }
                lstInfoTable.Add(infofirsttable);
                infofirsttable = new InfoFirstTable();
            }
            rowcount++;
        }

    }

    //.............................technical requirements..............................................//
    public void ParseTechnicalRequirementTable(XDocument xdoc)
    {

        technicalreqtable = new TechnicalRequirement();
        lstTechnicalReq = new List<TechnicalRequirement>();
        technicalreqtablesecond = new TechnicalRequirementSecondPart();
        lstTechnicalReqsecond = new List<TechnicalRequirementSecondPart>();
        var rows = xdoc.Descendants()
                    .Where(rc => rc.Name == "row" && rc.Ancestors("item").Any())
                    .Where(n => n.Parent.Name == "rows")
                    .Select(c => c).ToList();
        int rowcount = 1;
        foreach (var lstrows in rows)
        {
            if (rowcount > 2)
            {

                var cols = lstrows.Descendants()
                        .Where(rc => rc.Name == "cell" && rc.Ancestors("row").Any())
                        .Where(n => n.Parent.Name == "cells")
                        .Select(c => c).ToList();
                int colcount = 1;
                foreach (var lstcols in cols)
                {

                    var text = lstcols.Descendants()
                        .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                        .Where(n => n.Parent.Name == "item")
                        .Select(c => (string)c).ToList();

                    foreach (var lsttext in text)
                    {
                        if ( colcount == 1)
                        {
                            technicalreqtable.CategoryReq += lsttext + "";

                        }
                        else if ( colcount == 2)
                        {
                            technicalreqtable.CategoryDescription += lsttext + "";

                        }
                        
                        if (rowcount > 4)
                        {
                            if (colcount == 1) { technicalreqtablesecond.RID += lsttext + ""; }
                            else if (colcount == 2) { technicalreqtablesecond.RequirementDescription += lsttext + ""; }
                        
                        
                        }

                    }
                    colcount++;
                }
                lstTechnicalReq.Add(technicalreqtable);
                lstTechnicalReqsecond.Add(technicalreqtablesecond);
                technicalreqtable = new TechnicalRequirement();
                technicalreqtablesecond = new TechnicalRequirementSecondPart();
            }
            rowcount++;
        }

    }


    //.............................terms and definitions...............................................//

    public void ParseTermsDefinitionsTable(XDocument xdoc)
    {

        termdefinitiontable = new TermsAndDefinitions();
        lstTermDefinition = new List<TermsAndDefinitions>();
        var rows = xdoc.Descendants()
                    .Where(rc => rc.Name == "row" && rc.Ancestors("item").Any())
                    .Where(n => n.Parent.Name == "rows")
                    .Select(c => c).ToList();
        int rowcount = 1;
        foreach (var lstrows in rows)
        {
            if (rowcount > 2)
            {

                var cols = lstrows.Descendants()
                        .Where(rc => rc.Name == "cell" && rc.Ancestors("row").Any())
                        .Where(n => n.Parent.Name == "cells")
                        .Select(c => c).ToList();
                int colcount = 1;
                foreach (var lstcols in cols)
                {

                    var text = lstcols.Descendants()
                        .Where(rc => rc.Name == "text" && rc.Ancestors("items").Any())
                        .Where(n => n.Parent.Name == "item")
                        .Select(c => (string)c).ToList();

                    foreach (var lsttext in text)
                    {
                        if (colcount == 1)
                        {
                            termdefinitiontable.Concept += lsttext + "";

                        }
                        else if (colcount == 2)
                        {
                            termdefinitiontable.Definitions += lsttext + "";

                        }

                    }
                    colcount++;
                }
                lstTermDefinition.Add(termdefinitiontable);
                termdefinitiontable = new TermsAndDefinitions();
            }
            rowcount++;
        }

    }









}