using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 1.1	Navn på Use Case
/// </summary>
public class DescriptionFirstTable
{
	public DescriptionFirstTable()
	{
		
	}
    public string ID { get; set; }

    public string Areadomain { get; set; }

    public string Name { get; set; }

    public DescriptionFirstTable(string ID,string Areadomain,string Name)
    {
        this.ID = ID;
        this.Areadomain = Areadomain;
        this.Name=Name;
    }
}