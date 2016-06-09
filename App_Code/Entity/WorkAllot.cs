using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WorkAllot
/// </summary>
public class WorkAllot
{
	public WorkAllot()
	{
		//
		// TODO: Add constructor logic here
		//
	}
   [Key()]
   
        public int WAId { get; set; }

        public int? ZoneId { get; set; }

        public int? AcaId { get; set; }

        public string WorkAllotName { get; set; }

        public int? Active { get; set; }

        public DateTime? CreateOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifyOn { get; set; }

        public string ModifyBy { get; set; }

        public string ImageFileName { get; set; }

        public string ImageFilePath { get; set; }

        public int? ShiftedStatus { get; set; }

 

}