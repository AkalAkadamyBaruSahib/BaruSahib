﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AkalAcademy;
using System.Data.Entity;
using System.Collections;
using System.Data;
using System.Web.UI;


/// <summary>
/// Summary description for VisitorUserRepository
/// </summary>
public class VisitorUserRepository
{
    private DataContext _context;

    public VisitorUserRepository(DataContext context)
    {
        _context = context;
    }

    public void AddNewVisitor(Visitors visitors)
    {
        Hashtable param = new Hashtable();
        param.Add("Name", visitors.Name);
        param.Add("TotalNoOfPerson", visitors.TotalNoOfPerson);
        param.Add("PurposeOfVisit", visitors.PurposeOfVisit);
        param.Add("VehicleNo", visitors.VehicleNo);
        param.Add("Identification", visitors.Identification);
        param.Add("IdentificationPath", visitors.IdentificationPath);
        param.Add("NoOfDaysToStay", visitors.NoOfDaysToStay);
        param.Add("ContactNumber", visitors.ContactNumber);
        param.Add("CreatedOn", visitors.CreatedOn);
        param.Add("VisitorAddress", visitors.VisitorAddress);
        param.Add("BuildingID", visitors.BuildingID);
        param.Add("VisitorTypeID", visitors.VisitorTypeID);
        param.Add("CreatedBy", visitors.CreatedBy);
        param.Add("ModifyBy", visitors.ModifyBy);
        param.Add("ModifyOn", visitors.ModifyOn);
        param.Add("VisitorsPhoto", visitors.VisitorsPhoto);
        param.Add("VisitorsAuthorityLetter", visitors.VisitorsAuthorityLetter);
        param.Add("TimePeriodTo", visitors.TimePeriodTo);
        param.Add("TimePeriodFrom", visitors.TimePeriodFrom);
        param.Add("RoomRent", visitors.RoomRent);
        param.Add("ElectricityBill", visitors.ElectricityBill);
        param.Add("State", visitors.State);
        param.Add("Country", visitors.Country);
        param.Add("City", visitors.City);
        param.Add("IsActive", visitors.IsActive);
        param.Add("VisitorReference", visitors.VisitorReference);


        int visitorID = DAL.DalAccessUtility.GetDataInScaler("procSaveVisitors", param);
        foreach (VisitorRoomNumbers vs in visitors.VisitorRoomNumbers)
        {
            param = new Hashtable();
            param.Add("RoomNumberID", vs.RoomNumberID);
            param.Add("VisitorID", visitorID);
            param.Add("CreatedOn", vs.CreatedOn);
            DAL.DalAccessUtility.GetDataInScaler("procSaveVisitorsRoom", param);
        }
    }

    public List<VisitorsDTO> GetVisitorInformation(DateTime from, DateTime to)
    {
        List<Visitors> visitors = _context.Visitors.Where(v => v.IsActive == true).ToList();


        DataSet visitortype = new DataSet();
        List<VisitorsDTO> dto = new List<VisitorsDTO>();

        VisitorsDTO visitDTO = null;
        foreach (Visitors v in visitors)
        {
            visitDTO = new VisitorsDTO();

            visitDTO.ID = v.ID;
            visitDTO.Name = v.Name;
            visitDTO.TotalNoOfPerson = v.TotalNoOfPerson;
            visitDTO.PurposeOfVisit = v.PurposeOfVisit;
            visitDTO.VehicleNo = v.VehicleNo;
            visitDTO.Identification = v.Identification;
            visitDTO.IdentificationPath = v.IdentificationPath;
            visitDTO.NoOfDaysToStay = v.NoOfDaysToStay;
            visitDTO.ContactNumber = v.ContactNumber;
            visitDTO.CreatedOn = v.CreatedOn.ToString();
            visitDTO.VisitorAddress = v.VisitorAddress;
            visitDTO.BuildingID = v.BuildingID;
            visitDTO.VisitorTypeID = v.VisitorTypeID;
            visitDTO.CreatedBy = v.CreatedBy;
            visitDTO.ModifyBy = v.ModifyBy;
            visitDTO.ModifyOn = v.ModifyOn.ToString();
            visitDTO.ElectricityBill = v.ElectricityBill;
            visitDTO.TimePeriodFrom = v.TimePeriodFrom.ToString();
            visitDTO.TimePeriodTo = v.TimePeriodTo.ToString();
            visitDTO.VisitorsPhoto = v.VisitorsPhoto;
            visitDTO.VisitorsAuthorityLetter = v.VisitorsAuthorityLetter;
            visitDTO.State = v.State;
            visitDTO.Country = v.Country;
            visitDTO.City = v.City;
            visitDTO.IsActive = v.IsActive;
            visitDTO.VisitorReference = v.VisitorReference;


            visitortype = DAL.DalAccessUtility.GetDataInDataSet("exec [GetRoomNumbersWithBuildingName] " + v.ID);

            if (visitortype != null && visitortype.Tables != null && visitortype.Tables.Count > 0
                && visitortype.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < visitortype.Tables[0].Rows.Count; i++)
                {
                    if (visitDTO.BuildingName == null || !visitDTO.BuildingName.Contains(visitortype.Tables[0].Rows[i]["Name"].ToString()))
                    {
                        visitDTO.BuildingName += visitortype.Tables[0].Rows[i]["Name"].ToString() + ",";
                    }
                    visitDTO.RoomNumbers += visitortype.Tables[0].Rows[i]["Number"].ToString() + ",";
                }

                string room = visitDTO.RoomNumbers.Substring(0, visitDTO.RoomNumbers.Length - 1);
                string buildingname = visitDTO.BuildingName.Substring(0, visitDTO.BuildingName.Length - 1);
                visitDTO.RoomNumbers = room;
                visitDTO.BuildingName = buildingname;
            }
            dto.Add(visitDTO);
        }

        return dto;
    }

    public List<VisitorsDTO> GetVisitorInformationByVisitorTypeID(int visitorTypeID)
    {
        List<Visitors> visitors = _context.Visitors.Where(v => v.VisitorTypeID == visitorTypeID)
          .ToList();


        DataSet visitortype = new DataSet();
        List<VisitorsDTO> dto = new List<VisitorsDTO>();

        VisitorsDTO visitDTO = null;
        foreach (Visitors v in visitors)
        {
            visitDTO = new VisitorsDTO();

            visitDTO.ID = v.ID;
            visitDTO.Name = v.Name;
            visitDTO.TotalNoOfPerson = v.TotalNoOfPerson;
            visitDTO.PurposeOfVisit = v.PurposeOfVisit;
            visitDTO.VehicleNo = v.VehicleNo;
            visitDTO.Identification = v.Identification;
            visitDTO.IdentificationPath = v.IdentificationPath;
            visitDTO.NoOfDaysToStay = v.NoOfDaysToStay;
            visitDTO.ContactNumber = v.ContactNumber;
            visitDTO.CreatedOn = v.CreatedOn.ToString();
            visitDTO.VisitorAddress = v.VisitorAddress;
            visitDTO.BuildingID = v.BuildingID;
            visitDTO.VisitorTypeID = v.VisitorTypeID;
            visitDTO.CreatedBy = v.CreatedBy;
            visitDTO.ModifyBy = v.ModifyBy;
            visitDTO.ModifyOn = v.ModifyOn.ToString();
            visitDTO.RoomRent = v.RoomRent;
            visitDTO.ElectricityBill = v.ElectricityBill;
            visitDTO.TimePeriodFrom = v.TimePeriodFrom.ToString();
            visitDTO.TimePeriodTo = v.TimePeriodTo.ToString();
            visitDTO.VisitorsPhoto = v.VisitorsPhoto;
            visitDTO.VisitorsAuthorityLetter = v.VisitorsAuthorityLetter;
            visitDTO.State = v.State;
            visitDTO.Country = v.Country;
            visitDTO.City = v.City;
            visitDTO.IsActive = v.IsActive;
            visitDTO.VisitorReference = v.VisitorReference;
            visitortype = DAL.DalAccessUtility.GetDataInDataSet("exec [GetRoomNumbersWithBuildingName] " + v.ID);

            if (visitortype != null && visitortype.Tables != null && visitortype.Tables.Count > 0
                && visitortype.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < visitortype.Tables[0].Rows.Count; i++)
                {
                    visitDTO.BuildingName = visitortype.Tables[0].Rows[i]["Name"].ToString();
                    visitDTO.RoomNumbers += visitortype.Tables[0].Rows[i]["Number"].ToString() + ",";
                }
                string room = visitDTO.RoomNumbers.Substring(0, visitDTO.RoomNumbers.Length - 1);
                visitDTO.RoomNumbers = room;
            }
            dto.Add(visitDTO);
        }

        return dto;
    }

    public List<BuildingName> GetBuildingNameList()
    {
        return _context.BuildingName.ToList();
    }

    public List<RoomNumbers> GetAvailableRoomList(int BuildingID)
    {
        return _context.RoomNumbers.Where(r => (!_context.VisitorRoomNumbers.Select(vr => vr.RoomNumberID).Contains(r.ID)) && (r.BuildingID == BuildingID)).ToList();
    }

    public string GetBookedRoomList(int BuildingID)
    {
        List<RoomNumbers> rm = new List<RoomNumbers>();
        string roomnumbers = string.Empty;

        rm = _context.RoomNumbers.Where(r => (_context.VisitorRoomNumbers.Select(vr => vr.RoomNumberID).Contains(r.ID)) && (r.BuildingID == BuildingID)).ToList();

        roomnumbers = string.Join(",", rm.Select(r => r.ID));
        return roomnumbers;
    }

    public IList<RoomNumbers> GetBookedRoomsByVisitorID(int VisitorID)
    {
        string roomnumbers = string.Empty;

        var result = (from vr in _context.VisitorRoomNumbers
                      join r in _context.RoomNumbers on vr.RoomNumberID equals r.ID
                      where vr.VisitorID == VisitorID
                      select new
                      {
                          ID = r.ID,
                          Number = r.Number
                      }).AsEnumerable().Select(x => new RoomNumbers
                      {
                          ID = x.ID,
                          Number = x.Number
                      }).ToList();


        return result;
    }

    public List<RoomNumbers> GetRoomList(int BuildingID)
    {
        return _context.RoomNumbers.Where(r => r.BuildingID == BuildingID).ToList();
    }

    public void CheckOutVisitor(int VisitorID)
    {

        _context.VisitorRoomNumbers.RemoveRange(_context.VisitorRoomNumbers.Where(v => v.VisitorID == VisitorID));

        Visitors visitor = _context.Visitors.Where(v => v.ID == VisitorID)
                            .Include(r => r.VisitorRoomNumbers).FirstOrDefault();

        visitor.VisitorRoomNumbers = null;
        visitor.IsActive = false;
        _context.Entry(visitor).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public VisitorsDTO GetVisitorInfoByVisitorId(int VisitorID)
    {
        Visitors visitor = _context.Visitors.Where(v => v.ID == VisitorID)
            .Include(r => r.VisitorRoomNumbers)
            .FirstOrDefault();

        VisitorsDTO dto = new VisitorsDTO();
        dto.ID = visitor.ID;
        dto.Name = visitor.Name;
        dto.TotalNoOfPerson = visitor.TotalNoOfPerson;
        dto.PurposeOfVisit = visitor.PurposeOfVisit;
        dto.VehicleNo = visitor.VehicleNo;
        dto.Identification = visitor.Identification;
        if (visitor.IdentificationPath != "")
        {
            dto.IdentificationPath = visitor.IdentificationPath;
        }
        dto.NoOfDaysToStay = visitor.NoOfDaysToStay;
        dto.ContactNumber = visitor.ContactNumber;
        dto.CreatedOn = visitor.CreatedOn.ToString();
        dto.VisitorAddress = visitor.VisitorAddress;
        dto.BuildingID = visitor.BuildingID;
        dto.VisitorTypeID = visitor.VisitorTypeID;
        dto.CreatedBy = visitor.CreatedBy;
        dto.ModifyBy = visitor.ModifyBy;
        dto.ModifyOn = visitor.ModifyOn.ToString();
        if (visitor.VisitorsPhoto != "")
        {
            dto.VisitorsPhoto = visitor.VisitorsPhoto;
        }

        if (visitor.VisitorsAuthorityLetter != "")
        {
            dto.VisitorsAuthorityLetter = visitor.VisitorsAuthorityLetter;
        }
        dto.TimePeriodTo = visitor.TimePeriodTo.Value.ToShortDateString();
        dto.TimePeriodFrom = visitor.TimePeriodFrom.Value.ToShortDateString();
        dto.RoomRent = visitor.RoomRent;
        dto.ElectricityBill = visitor.ElectricityBill;
        dto.State = visitor.State;
        dto.Country = visitor.Country;
        dto.City = visitor.City;
        dto.IsActive = visitor.IsActive;
        dto.VisitorReference = visitor.VisitorReference;

        return dto;
        //   newVisitor

        //declate all properties
    }

    public void UpdateVisitor(Visitors visitor)
    {
        _context.VisitorRoomNumbers.RemoveRange(_context.VisitorRoomNumbers.Where(v => v.VisitorID == visitor.ID));


        Visitors newVisitor = _context.Visitors.Where(v => v.ID == visitor.ID)
            .Include(r => r.VisitorRoomNumbers)
            .FirstOrDefault();

        newVisitor.Name = visitor.Name;
        newVisitor.TotalNoOfPerson = visitor.TotalNoOfPerson;
        newVisitor.PurposeOfVisit = visitor.PurposeOfVisit;
        newVisitor.VehicleNo = visitor.VehicleNo;
        newVisitor.Identification = visitor.Identification;
        if (visitor.IdentificationPath != "")
        {
            newVisitor.IdentificationPath = visitor.IdentificationPath;
        }
        newVisitor.NoOfDaysToStay = visitor.NoOfDaysToStay;
        newVisitor.ContactNumber = visitor.ContactNumber;
        newVisitor.CreatedOn = visitor.CreatedOn;
        newVisitor.VisitorAddress = visitor.VisitorAddress;
        newVisitor.BuildingID = visitor.BuildingID;
        newVisitor.VisitorTypeID = visitor.VisitorTypeID;
        newVisitor.CreatedBy = visitor.CreatedBy;
        newVisitor.ModifyBy = visitor.ModifyBy;
        newVisitor.ModifyOn = visitor.ModifyOn;
        if (visitor.VisitorsPhoto != "")
        {
            newVisitor.VisitorsPhoto = visitor.VisitorsPhoto;
        }

        if (visitor.VisitorsAuthorityLetter != "")
        {
            newVisitor.VisitorsAuthorityLetter = visitor.VisitorsAuthorityLetter;
        }
        newVisitor.TimePeriodTo = visitor.TimePeriodTo;
        newVisitor.TimePeriodFrom = visitor.TimePeriodFrom;
        newVisitor.RoomRent = visitor.RoomRent;
        newVisitor.ElectricityBill = visitor.ElectricityBill;
        newVisitor.State = visitor.State;
        newVisitor.Country = visitor.Country;
        newVisitor.City = visitor.City;
        newVisitor.IsActive = visitor.IsActive;
        newVisitor.VisitorReference = visitor.VisitorReference;

        //   newVisitor

        //declate all properties

        newVisitor.VisitorRoomNumbers = new List<VisitorRoomNumbers>();
        VisitorRoomNumbers visitorRoom;
        foreach (VisitorRoomNumbers rm in visitor.VisitorRoomNumbers)
        {
            visitorRoom = new VisitorRoomNumbers();
            visitorRoom.VisitorID = newVisitor.ID;
            visitorRoom.RoomNumberID = rm.RoomNumberID;
            visitorRoom.CreatedOn = DateTime.Now;

            newVisitor.VisitorRoomNumbers.Add(visitorRoom);
        }


        _context.Entry(newVisitor).State = EntityState.Modified;
        _context.SaveChanges();
    }

    
}