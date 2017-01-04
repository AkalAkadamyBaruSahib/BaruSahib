using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DieselPetrolPrice
/// </summary>
public class DieselPetrolPrice
{
    public DieselPetrolPrice()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [Key()]
    public int ID { get; set; }

    public int? AcaID { get; set; }

    public decimal? DieselPrice { get; set; }

    public decimal? PetrolPrice { get; set; }

    public DateTime? CreatedOn { get; set; }
}