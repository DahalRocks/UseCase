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
using System.Net;
using Translator;
using System.Threading;
using System.Web.Compilation;

public partial class _Default : System.Web.UI.Page
{
    NameSpace obj;
    
    Site s;
    
    Uri address;
    
    public string name_spaces;
   
    protected void Page_Load(object sender, EventArgs e)
    {
         
        if (!Page.IsPostBack)
        {

            s = new Site("wiki", "smartgrid2013", "xml", obj);
            
            address = new Uri("http://smartgrid.hiof.no/wiki/api.php?");
            
            //Site s = new Site("Syntef", "dhruba1234", "xml", obj);
            
            //Uri address = new Uri("http://localhost:8080/mediawiki/api.php?");

            s.login(address);

            List<NameSpace>lst= s.GetNameSpace();
            
            lstBrowser.DataTextField = "namespaces";
            
            lstBrowser.DataValueField = "namespaces";
            
            lstBrowser.DataSource = lst;
            
            lstBrowser.DataBind();
            
            lblException.Text = ExceptionHandling.errorMessage;

        }
    }
   
    protected void btnOk_Click(object sender, EventArgs e)
    {
        Document document = new Document();
        
        lblResult.Text = string.Empty;
        
        obj = new NameSpace();
        
        obj.namespaces = lstBrowser.SelectedValue;
        
        if (FileUploadControl.HasFile)
        {

            string fileExt = System.IO.Path.GetExtension(FileUploadControl.FileName);      

            if (fileExt == ".docx")
            {
                //do what you want with this file
            }
            else
            {
                lblResult.Text = "Only .docx files are allowed!";
                
                return;
            }
            
            if ( lstBrowser.SelectedValue=="")
            {
                lblResult.Text = "Please select file and assign namespace to the page before submit";
                
                return;
            }
            else
            {

                string file = Path.GetFileName(FileUploadControl.FileName);
                
                FileUploadControl.SaveAs(Server.MapPath("~/DOCFile/") + file);
                
                document.LoadFromFile(Server.MapPath("~/DOCFile/" + file), FileFormat.Docx);
                
                document.SaveToFile(Server.MapPath("~/XMLDocument/prexml.xml"), FileFormat.Xml);

                if (chkEnglishVersion.Checked)
                {
                    obj.englishversion = true;

                }
                else
                {
                    obj.englishversion = false;
                
                }
                
                
                string clearfile = string.Empty;
                
                try
                {
                     clearfile = HexFilter.MakeFileClear(Server.MapPath("~/XMLDocument/prexml.xml"));
                }
                catch (Exception ex)
                {

                    ExceptionHandling.errorMessage = ex.ToString();
                    
                    lblException.Text = ex.ToString();
                    
                    return;
                
                }


                try
                {
                    XMLParser xdata = new XMLParser(clearfile,obj);
                    
                    s = new Site("wiki", "smartgrid2013", "xml", obj);
                    
                    address = new Uri("http://smartgrid.hiof.no/wiki/api.php?");
                    
                    //Site s = new Site("Syntef", "dhruba1234", "xml", obj);
                    
                    //Uri address = new Uri("http://localhost:8080/mediawiki/api.php?");
                   
                    s.login(address);
                    
                    s.GetMainPageTableOfContent(xdata);
                    
                    if (s.Edit(xdata).Contains("result=\"Success\""))
                    {
                        lblResult.Text = "Delivery succeed !";
                        
                        lblException.Text = string.Empty;
                        
                    }
                    else
                    {
                        lblResult.Text = "Delivery failed!";
                        
                        lblException.Text = "File may not be in standard use-case format!Please check!";
                    }
                    
                    ClearFolder(Server.MapPath("~/DOCFile/"));
                }
                catch (Exception ex)
                {
                    ExceptionHandling.errorMessage = ex.ToString();
                    
                    lblException.Text = ex.ToString();
                    
                    return;
                
                }
                
                lstBrowser.ClearSelection();
            
            }
            
        }
        else
        {
            lblResult.Text = "Please select file before submit";
            
            return;
        }
        
    }
    private void ClearFolder(string FolderName)
    {
        DirectoryInfo dir = new DirectoryInfo(FolderName);

        foreach (FileInfo fi in dir.GetFiles())
        {
            fi.IsReadOnly = false;
            
            fi.Delete();
        }

        foreach (DirectoryInfo di in dir.GetDirectories())
        {
            ClearFolder(di.FullName);
            
            di.Delete();
        }
    }
    
    
}
