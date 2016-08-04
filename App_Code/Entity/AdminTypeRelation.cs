using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SubAdminTypesRelation
/// </summary>
public class AdminTypeRelation
{
    public AdminTypeRelation()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [Key()]

    public int ID { get; set; }

    public int? UserID { get; set; }

    public int? SubAdminTypeID { get; set; }

}