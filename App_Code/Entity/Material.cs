using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Material
/// </summary>
public class Material
{
	public Material()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    [Key()]
    public int MatId { get; set; }

    public int? MatTypeId { get; set; }

    public string MatName { get; set; }

    public decimal? MatCost { get; set; }

    public int? Active { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? ModifyOn { get; set; }

    public string ModifyBy { get; set; }

    public int? UnitId { get; set; }

    public string LastCreateBy { get; set; }

    public int? ChangeMatTypeStatus { get; set; }

    public DateTime? ChangeMatTypeOn { get; set; }

    public string ImageUrl { get; set; }

    public bool? IsRateApproved { get; set; }
}