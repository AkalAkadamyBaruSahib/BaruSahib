using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EstimateBucketDTO
/// </summary>
public class EstimateBucketDTO
{
    public int ID { get; set; }
    public MaterialType MaterialType { get; set; }
    public int? MatID { get; set; }
    public int? BucketID { get; set; }
    public string BucketName { get; set; }
    public string MatName { get; set; }
}