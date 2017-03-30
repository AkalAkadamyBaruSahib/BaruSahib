using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserRole
/// </summary>
public class UserRole
{
    public UserRole()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [Key()]
    public int ID { get; set; }

    public int? UserID { get; set; }

    public int? RoleID { get; set; }
}