using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Unit
/// </summary>
public class Unit
{
	public Unit()
	{
		//
		// TODO: Add constructor logic here
		//
	}
        [Key()]

        public int UnitId { get; set; }

        public string UnitName { get; set; }

        public int? Active { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? ModifyOn { get; set; }

        public string ModifyBy { get; set; }

        public string LastCreatedBy { get; set; }

  
}