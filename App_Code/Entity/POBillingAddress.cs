using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for POBillingAddress
/// </summary>
public class POBillingAddress
{
    public POBillingAddress()
    {
    }

    [Key()]
    public int ID { get; set; }

    public string TrustName { get; set; }

    public string Address { get; set; }

    public string State { get; set; }

    public string City { get; set; }

    public string PhoneNumber { get; set; }

    public string Zipcode { get; set; }
}