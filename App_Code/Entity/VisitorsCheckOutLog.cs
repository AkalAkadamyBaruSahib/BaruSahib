using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VisitorsCheckOutLog
/// </summary>
public class VisitorsCheckOutLog
{
    public VisitorsCheckOutLog()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public int ID { get; set; }

    public int? VisitorID { get; set; }

    public int? RoomNumberID { get; set; }

    public DateTime? CreatedOn { get; set; }
}