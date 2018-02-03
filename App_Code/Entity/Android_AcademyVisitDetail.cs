using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Android_AcademyVisitDetail
/// </summary>
public class Android_AcademyVisitDetail
{
	public Android_AcademyVisitDetail()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    [Key()]

    public int ID { get; set; }

    public int? TransportDailyProformaID { get; set; }

    public DateTime AcaCampusVisitTime { get; set; }

    public string AcaCampusVisitSolution { get; set; }

    public string AcaCampusVisitComplaint { get; set; }
}