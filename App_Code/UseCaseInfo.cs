using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UseCaseInfo
/// </summary>
public class UseCaseInfo
{

    
    public string name { get; set; }
    
    public string description { get; set; }
    
    
    public UseCaseInfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public UseCaseInfo(string name, string description)
    {
        this.name = name;
        this.description = description;
    }
}