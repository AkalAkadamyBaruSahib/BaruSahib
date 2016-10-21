using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for City
/// </summary>
public class City
{
	public City()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    [Key()]
    public int CityId { get; set; }

    public int? StateId { get; set; }

    public int? CountryId { get; set; }

    public string CityName { get; set; }

    public string Pincode { get; set; }

    public int? Active { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? ModifyOn { get; set; }

    public string ModifyBy { get; set; }


}