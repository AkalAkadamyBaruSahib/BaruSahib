using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProformaMaterialDetail
/// </summary>
public class ProformaMaterialDetail
{
    public ProformaMaterialDetail()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    [Key()]
    public int ID { get; set; }

    public int? ProformaID { get; set; }

    public int? MatID { get; set; }

    public int? UnitID { get; set; }

    public decimal? Qty { get; set; }

    public decimal? Rate { get; set; }

    public decimal? Amount { get; set; }

}