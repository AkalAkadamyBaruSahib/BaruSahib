using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Comments
/// </summary>
public class Comments
{
    public Comments()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [Key()]
    public int ID { get; set; }

    public int? PrimaryID { get; set; }

    public int? CommentType { get; set; }

    public string Comment { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }
}