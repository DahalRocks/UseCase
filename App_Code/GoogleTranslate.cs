using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using Google.API.Translate;
using Translator;

/// <summary>
/// Summary description for GoogleTranslate
/// </summary>
public class GoogleTranslate
{
	public GoogleTranslate()
	{
		
	}
    private CookieContainer cookies;

    /// <summary>
    /// Posts the data to the given url.
    /// </summary>
    public string postData(Uri url)
    {
        return this.postData(url, null);
    }

    /// <summary>
    /// Posts the data to the given url with the given post data.
    /// </summary>
    public string postData(Uri url, string postData)
    {
        HttpWebRequest request = WebRequest.Create(url) as System.Net.HttpWebRequest;
        this.cookies = new CookieContainer();
        request.UserAgent = "just-plate-491 bot course/1.0";
        request.ContentType = "X-HTTP-Method-Override";
        //X-HTTP-Method-Override
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
    public void Translate()
    {
        Uri address = new Uri("https://www.googleapis.com/language/translate/v2?");
        string postData = string.Format("key={0}&target={1}&source={2}&q={3}",
                          HttpUtility.UrlEncode("AIzaSyBpEpy4jJrWftH8dmK7CKcFyRsNAKAF1bk"),
                          HttpUtility.UrlEncode("en"),
                          HttpUtility.UrlEncode("no"),
                          HttpUtility.UrlEncode("dette er dhruba Dahal"));
        string result = this.postData(address,postData);
        
    }

   
    //public void TranslateText( )
    //  {
    //      string text = "Navn";
    //      TranslateClient client = new TranslateClient("http://smartgrid.hiof.no/wiki/index.php/Bekrefte/avkrefte_h%C3%B8y/lav_spenning");
    //      string translated = client.Translate(text, Language.Norwegian, Language.English);
    //      Console.WriteLine(translated);
    //  }

    public string TranslateText(string input)
    {
        string languagePair = "no|en";
        string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);
        
        WebClient webClient = new WebClient();
        webClient.Encoding = System.Text.Encoding.UTF8;
        string result = webClient.DownloadString(url);
        result = result.Substring(result.IndexOf("<span title=\"") + "<span title=\"".Length);
        result = result.Substring(result.IndexOf(">") + 1);
        result = result.Substring(0, result.IndexOf("</span>"));
        return result.Trim();
    }

    public string MicrosoftTranslate(string input)
    {
        string clientID = "translatedhruba_123";
        string clientSecret = "sJnnJgpG12Rysf7KxjeBpY5ulaCKSdaJYot7adFzXWY=";
        String strTranslatorAccessURI =
              "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
        String strRequestDetails =
              string.Format("grant_type=client_credentials&client_id={0}&client_secret={1} &scope=http://api.microsofttranslator.com", HttpUtility.UrlEncode(clientID),
                  HttpUtility.UrlEncode(clientSecret));

        System.Net.WebRequest webRequest = System.Net.WebRequest.Create(strTranslatorAccessURI);
        webRequest.ContentType = "application/x-www-form-urlencoded";
        webRequest.Method = "POST";
        byte[] bytes = System.Text.Encoding.ASCII.GetBytes(strRequestDetails);
        webRequest.ContentLength = bytes.Length;
        
        string translatedtext = "";
        try
        {

            using (System.IO.Stream outputStream = webRequest.GetRequestStream())
            {
                outputStream.Write(bytes, 0, bytes.Length);
            }
            System.Net.WebResponse webResponse = webRequest.GetResponse();
            System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new
            System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(AdmAccessToken));

            AdmAccessToken token =
                (AdmAccessToken)serializer.ReadObject(webResponse.GetResponseStream());
            string headerValue = "Bearer " + token.access_token;

            string txtToTranslate = input;
            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" +
                 System.Web.HttpUtility.UrlEncode(txtToTranslate) + "&from=no&to=en";
            System.Net.WebRequest translationWebRequest = System.Net.WebRequest.Create(uri);
            translationWebRequest.Headers.Add("Authorization", headerValue);
            System.Net.WebResponse response = null;
            response = translationWebRequest.GetResponse();
            System.IO.Stream stream = response.GetResponseStream();
            System.Text.Encoding encode = System.Text.Encoding.GetEncoding("utf-8");

            System.IO.StreamReader translatedStream = new System.IO.StreamReader(stream, encode);
            System.Xml.XmlDocument xTranslation = new System.Xml.XmlDocument();
            xTranslation.LoadXml(translatedStream.ReadToEnd());
            translatedtext= xTranslation.InnerText;
            
        }
        catch (Exception ex)
        {
            ExceptionHandling.errorMessage = ex.ToString();
             
        }
        return translatedtext;
    }
}