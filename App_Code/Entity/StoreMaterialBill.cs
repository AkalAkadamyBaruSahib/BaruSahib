using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StoreMaterialBill
/// </summary>
public class StoreMaterialBill
{
    [Key()]
    public int ID { get; set; }

    public int EstID { get; set; }

    public string BillName { get; set; }

    public string BillNo { get; set; }

    public string BillPath { get; set; }
}