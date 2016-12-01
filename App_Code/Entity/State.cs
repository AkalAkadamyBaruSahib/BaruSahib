using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for State
/// </summary>
public class State
{
    public State()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [Key()]
    public int StateId { get; set; }

    public int? CountryId { get; set; }

    public string StateName { get; set; }

    public int? Active { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? ModifyOn { get; set; }

    public string ModifyBy { get; set; }
}