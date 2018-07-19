using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Threading;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {
      
    [System.Web.Services.WebMethod]
    public string GetText()
    {
        
        
        for (int i = 0; i < 10; i++)
        {
            Thread.Sleep(1000);
        }
        return "All finished!";
    }
    

    
}
