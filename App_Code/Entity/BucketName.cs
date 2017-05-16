using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BucketName
/// </summary>
public class BucketName
{
    public BucketName()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [Key()]

    public int BucketID { get; set; }

    public string Name { get; set; }

    public int? CreatedBy { get; set; }

    public List<EstimateBucketMaterialRelation> EstimateBucketMaterialRelation { get; set; }
}