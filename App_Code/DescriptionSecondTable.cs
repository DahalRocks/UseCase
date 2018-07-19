using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 1.2	Versjonshåndtering
/// </summary>
public  class DescriptionSecondTable
{
	public DescriptionSecondTable()
	{
		
	}
    public string Version { get; set; }

    public string Date { get; set; }

    public string AuthorNameCommitte { get; set; }
    
    public string Changes{get;set;}

    public string UseCaseStatus { get; set; }
}