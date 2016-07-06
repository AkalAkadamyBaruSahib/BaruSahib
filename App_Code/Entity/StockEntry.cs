using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StockEntry
/// </summary>
public class StockEntry
{
    public StockEntry()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    [Key()]

    public int ID { get; set; }

    public int? EMRID { get; set; }

    public DateTime? ReceivedOn { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? Rate { get; set; }

    public int? ReceivedBy { get; set; }

    public string BillPath { get; set; }
}