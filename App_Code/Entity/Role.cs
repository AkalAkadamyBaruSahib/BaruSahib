using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Role
/// </summary>
public class Role
{
	public Role()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    [Key()]
    public int ID { get; set; }

    public string RoleName { get; set; }
}