using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MaterialRowAndroid
/// </summary>
public class MaterialRowAndroid
{
	public MaterialRowAndroid()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int? MatTypeId { get; set; }

    public int? MatId { get; set; }

    public int? PSId { get; set; }

    public decimal? Qty { get; set; }

    public int? UnitId { get; set; }

    public decimal? Rate { get; set; }

    public decimal? Amount { get; set; }

    public string Remark { get; set; }
}