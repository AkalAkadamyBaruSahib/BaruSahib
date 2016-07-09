using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Login
/// </summary>
public class Login
{
    public Login()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    [Key()]

    public int Sno { get; set; }

    public string EmailId { get; set; }

    public string Pwd { get; set; }

    public string LastLoginIP { get; set; }

    public DateTime? LastLogindate { get; set; }

    public int? Active { get; set; }

    public int? UserTypeId { get; set; }

    public DateTime? ChangePwdOn { get; set; }


}