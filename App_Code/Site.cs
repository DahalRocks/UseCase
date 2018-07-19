using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Xml.Linq;



/// <summary>
/// Summary description for Site
/// </summary>
public class Site
{
    public string site { get; set; }
     
    public string apipath { get; set; }

    public string username { get; set; }

    public string password { get; set; }

    public string format { get; set; }
   
    public Uri address { get; set; }
    
    public string category { get; set; }
   
    public string edit_token { get; set; }
    
    public string mainWiki { get; set; }

    private CookieContainer cookies;
    
    GoogleTranslate objGoogle = new GoogleTranslate();
    
    public string usecase_name = string.Empty;
    
    List<NameSpace> lstnsp;
    
    NameSpace objNameSpace;

    /// <summary>
    /// Constructor
    /// </summary>
    public Site(string username, string password,string format,NameSpace NameSpace)
    {
        
        this.username = username;
        this.password = password;
        this.cookies = new CookieContainer();
        this.format = format;

        this.objNameSpace=NameSpace;
    }
    
    public Site()
    {

        
    }

    /// <summary>
    /// Posts the data to the given url.
    /// </summary>
    public string postData(string url)
    {
        return this.postData(url, null,null);
    }
    /// <summary>
    /// Posts the data to the given url with the given post data.
    /// </summary>
    public string postData(string url, string postData,string action)
    {
        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        
            request.UserAgent = "Wikiversity bot course/1.0";
            if (action != "import")
                request.ContentType = "application/x-www-form-urlencoded";

            else
            {

                var boundary = "---------------------------7da24f2e50046";
                request.ContentType = "multipart/form-data; boundary=" + boundary;
            }

            if (this.cookies.Count == 0)
                request.CookieContainer = new CookieContainer();
            else
                request.CookieContainer = this.cookies;

            if (!string.IsNullOrEmpty(postData))
            {
                request.Method = "POST";
                byte[] postBytes = Encoding.UTF8.GetBytes(postData);
                request.ContentLength = postBytes.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(postBytes, 0, postBytes.Length);
                stream.Close();
            }

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            foreach (Cookie cookie in response.Cookies)
            {
                this.cookies.Add(cookie);
            }

            Stream respStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(respStream);
            string respStr = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return respStr;
        
        
        
        
    }
    /// <summary>
    /// Signing in and saving cookies.
    /// </summary>
    public void login(Uri address)
    {
        this.address = address;
        string postData = string.Format("lgname={0}&lgpassword={1}&format={2}", HttpUtility.UrlEncode(this.username), HttpUtility.UrlEncode(this.password),HttpUtility.UrlEncode(this.format));
        string result = this.postData(address + "action=login", postData,"login");
        if (result.Contains("result=\"Success\""))
            throw new Exception("Login failed. Check your username and password.");
        else
        {
             string token = GetTokenFromXML(result,"login");
             postData = string.Format("lgname={0}&lgpassword={1}&lgtoken={2}&format={3}", HttpUtility.UrlEncode(this.username), HttpUtility.UrlEncode(this.password), HttpUtility.UrlEncode(token), HttpUtility.UrlEncode(this.format));
             result = this.postData(address + "action=login", postData,"login");   
        }
        SetEditToken();
    }

    public string GetTokenFromXML(string xml,string action)
    {
        Stream stream = FileProcesser.StringToStream(xml);
        XDocument xdoc = XDocument.Load(stream);
        var t = "";
        if (action == "login")
        {
            t = xdoc.Descendants()
                            .Where(rc => rc.Name == "login")
                            .Where(n => n.Parent.Name == "api")
                            .Select(c => (string)c.Attribute("token")).Single();
            
        }
        else if (action == "edit")
        {
             t = xdoc.Descendants()
                            .Where(rc => rc.Name == "tokens")
                            .Where(n => n.Parent.Name == "api")
                            .Select(c => (string)c.Attribute("edittoken")).Single();
        
        }
        return t.ToString();
    
    }

    public void SetEditToken()
    {
        Uri address = this.address;
        string postData = string.Format("lgname={0}&lgpassword={1}&format={2}", HttpUtility.UrlEncode(this.username), HttpUtility.UrlEncode(this.password), HttpUtility.UrlEncode(this.format));
        string result = this.postData(address + "action=tokens", postData,"edit");
        string token = GetTokenFromXML(result,"edit");
        this.edit_token=token;
    }

    public List<NameSpace> GetNameSpace()
    {

        
        string postDataNameSpace = string.Format("meta={0}&siprop={1}&format={2}",
                                        HttpUtility.UrlEncode("siteinfo"),
                                        HttpUtility.UrlEncode("namespaces"),
                                        HttpUtility.UrlEncode("xml"));

        string namespaces = this.postData(address + "action=query", postDataNameSpace, "namespaces");

        XDocument xmlC = XDocument.Load(FileProcesser.StringToStream(namespaces.ToString()));
        var lstNameSpace = xmlC.Descendants()
                    .Where(rc => rc.Name == "ns" && rc.Ancestors("query").Any())
                    .Where(n => n.Parent.Name == "namespaces")
                    .Select(c =>(string) c).ToList();


        lstnsp = new List<NameSpace>();

        foreach (var lst in lstNameSpace)
        {
            NameSpace obj=new NameSpace();
            obj.namespaces = lst;
            lstnsp.Add(obj);
        
        }
        return lstnsp;
    
    
    
    }

    public void GetMainPageTableOfContent(XMLParser obj)
    {

        string postDataTextContent = string.Format("prop={0}&page={1}&format={2}",
                                        HttpUtility.UrlEncode("wikitext"),
                                        HttpUtility.UrlEncode("Main Page"),
                                        HttpUtility.UrlEncode("xml"));
        string textWikiContent = this.postData(address + "action=parse", postDataTextContent, "namespaces");
        
        
        
        string postDataParseWikiText = string.Format("text={0}&format={1}",
                                        HttpUtility.UrlEncode(textWikiContent),
                                        HttpUtility.UrlEncode("xml"));
        string parsedWikiContent = this.postData(address + "action=parse", postDataParseWikiText, "namespaces");

        XDocument contentTree;
        try
        {
            contentTree = XDocument.Load(FileProcesser.StringToStream(parsedWikiContent.ToString()));
            var pageNameLst = contentTree.Descendants()
                    .Where(rc => rc.Name == "pl" && rc.Ancestors("parse").Any())
                    .Where(n => n.Parent.Name == "links")
                    .Select(c => (string)c).ToList();

            pageNameLst.Add(this.objNameSpace.namespaces + ":" + obj.usecasename);
            string[]uniqueCategoryList= CreateUniquePageCategoryList(pageNameLst.ToArray());
            AddNewPageLinkToTableOfContent(pageNameLst.ToArray(), uniqueCategoryList);
        }
        catch (ArgumentException ex)
        {

            ExceptionHandling.errorMessage = ex.Message+" "+"Wiki sections of Main Page is  making this exception";
            return;
        }
    }

    public string[] CreateUniquePageCategoryList(string[]pageArray)
    {
        
       string[] newUniqueArray=new string[pageArray.Length];
        
       for (int t = 0; t < pageArray.Length; t++)
        {
            string[] splited = pageArray[t].Split(':');
            newUniqueArray[t] = splited[0].ToString();
            
        }

        for(int i=0;i<newUniqueArray.Length;i++)
        {
            for(int j=i+1;j<newUniqueArray.Length;j++)
            {
                if (newUniqueArray[i].ToString().ToLower() == newUniqueArray[j].ToString().ToLower())
                newUniqueArray[j] = string.Empty;
            }
            
        }
        
        return newUniqueArray;
    
    }

    public void AddNewPageLinkToTableOfContent(string[] lst,string[]finalCategory)
    {
       
        StringBuilder wiki = new StringBuilder();
        for (int i = 0; i < finalCategory.Length; i++)
        {
            if (finalCategory[i].ToString() != "" || finalCategory[i].ToString() != string.Empty)
            {
                wiki.Append("==" + finalCategory[i].ToString() + "==" + "\n");
            }
            for (int j = 0; j < lst.Length; j++)
            {
                string[] page = lst[j].Split(':');
                if (finalCategory[i].ToString().ToLower() == page[0].ToString().ToLower())
                {
                    wiki.Append("\n"+"[[" + lst[j].ToString() + "]]"+"\n");
                }
            }
        }

        this.mainWiki = wiki.ToString();
    }

    public string Edit(XMLParser xdata)
    {

 
         string usecase_name = xdata.usecasename;
         Uri address = this.address;
         string postDataEditOriginal = string.Format("title={0}&section={1}&"+
                                        "summary={2}&text={3}&"+
                                        "token={4}&format={5}",
                                        HttpUtility.UrlEncode(this.objNameSpace.namespaces+":"+xdata.usecasename),
                                        HttpUtility.UrlEncode("new"),
                                        HttpUtility.UrlEncode(""),
                                        HttpUtility.UrlEncode(xdata.testwikioriginal),
                                        HttpUtility.UrlEncode(this.edit_token), HttpUtility.UrlEncode(this.format));
         string postDataDeleteOriginal = string.Format("title={0}&token={1}" ,
                                         HttpUtility.UrlEncode(this.objNameSpace.namespaces + ":" + xdata.usecasename),
                                         HttpUtility.UrlEncode(this.edit_token), HttpUtility.UrlEncode(this.format));




         string usecasename = objGoogle.MicrosoftTranslate(this.objNameSpace.namespaces + ":" + xdata.usecasename);
         string postDataMainPage = string.Format("title={0}&section={1}&" +
                                          "summary={2}&text={3}&" +
                                          "token={4}&format={5}",
                                          HttpUtility.UrlEncode("Main Page"),
                                          HttpUtility.UrlEncode("new"),
                                          HttpUtility.UrlEncode(""),
                                          HttpUtility.UrlEncode(this.mainWiki),
                                          HttpUtility.UrlEncode(this.edit_token), HttpUtility.UrlEncode(this.format));
         string postDataDeleteMainPage= string.Format("title={0}&token={1}",
                                          HttpUtility.UrlEncode("Main Page"),
                                          HttpUtility.UrlEncode(this.edit_token), HttpUtility.UrlEncode(this.format));



        string postDataEditEnglish = string.Format("title={0}&section={1}&" +
                                         "summary={2}&text={3}&" +
                                         "token={4}&format={5}",
                                         HttpUtility.UrlEncode(usecasename),
                                         HttpUtility.UrlEncode("new"),
                                         HttpUtility.UrlEncode(""),
                                         HttpUtility.UrlEncode(xdata.testwikienglish),
                                         HttpUtility.UrlEncode(this.edit_token), HttpUtility.UrlEncode(this.format));
         string postDataDeleteEnglish = string.Format("title={0}&token={1}",
                                         HttpUtility.UrlEncode(objGoogle.MicrosoftTranslate(this.objNameSpace.namespaces + ":" + usecasename)),
                                         HttpUtility.UrlEncode(this.edit_token), HttpUtility.UrlEncode(this.format));


        string deleteresultoriginal = this.postData(address + "action=delete", postDataDeleteOriginal, "delete");
        string result = this.postData(address + "action=edit", postDataEditOriginal, "edit");
        
        string deleteresultmainpage = this.postData(address + "action=delete", postDataDeleteMainPage, "delete");
        result = this.postData(address + "action=edit", postDataMainPage, "edit");
        
        if (this.objNameSpace.englishversion)
        {
            string deleteresultenglish = this.postData(address + "action=delete", postDataDeleteEnglish, "delete");
            result = this.postData(address + "action=edit", postDataEditEnglish, "edit");
        }
        
        return result;
    }

}