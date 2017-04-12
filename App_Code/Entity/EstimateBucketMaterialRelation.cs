using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EstimateBucket
/// </summary>
public class EstimateBucketMaterialRelation
{
    public EstimateBucketMaterialRelation()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [Key()]
    public int ID { get; set; }

    public int? MatID { get; set; }

    public int? InchargeID { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? BucketID { get; set; }

    public int? MatTypeID { get; set; }

}