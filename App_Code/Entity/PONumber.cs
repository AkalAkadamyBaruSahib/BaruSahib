using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PONumber
/// </summary>
public class PONumber
{
	public PONumber()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    [Key()]

    public int ID { get; set; }

    public string PurchaseOrderNumber { get; set; }

    public List<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
}