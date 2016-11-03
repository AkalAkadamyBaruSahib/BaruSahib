using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VisitorRoomNumbers
/// </summary>
public class VisitorRoomNumbers
{
    public VisitorRoomNumbers()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    [Key()]
    public int ID { get; set; }

    public int? VisitorID { get; set; }

    public int? RoomNumberID { get; set; }

    public DateTime? CreatedOn { get; set; }

    [ForeignKey("RoomNumberID")]
    public RoomNumbers RoomNumbers { get; set; }

}

public class BuildingName
{
    public int ID { get; set; }

    public string Name { get; set; }
}

public class RoomNumbers
{
    public int ID { get; set; }

    public int BuildingID { get; set; }

    public string Number { get; set; }

    public int BuildingFloor { get; set; }

    public int? NumOfBed { get; set; }

    [ForeignKey("BuildingID")]
    public BuildingName BuildingName { get; set; }

    public bool? IsPermanent { get; set; }
}