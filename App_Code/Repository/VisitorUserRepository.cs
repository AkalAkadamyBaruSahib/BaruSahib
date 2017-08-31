using System;
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
        param.Add("TotalNoOfMen", visitors.TotalNoOfMen);
        param.Add("TotalNoOfWomen", visitors.TotalNoOfWomen);
        param.Add("TotalNoOfChildren", visitors.TotalNoOfChildren);
        param.Add("AdmissionNumber", visitors.AdmissionNumber);
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
        param.Add("RoomRentType", visitors.RoomRentType);
        param.Add("PurposeOfVisitRemarks", visitors.PurposeOfVisitRemarks);

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
            visitDTO.TotalNoOfMen = v.TotalNoOfMen;
            visitDTO.TotalNoOfWomen = v.TotalNoOfWomen;
            visitDTO.TotalNoOfChildren = v.TotalNoOfChildren;
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
            visitDTO.IsActive = Convert.ToBoolean(v.IsActive);
            visitDTO.VisitorReference = v.VisitorReference;
            visitDTO.RoomRentType = v.RoomRentType;
            visitDTO.AdmissionNumber = v.AdmissionNumber;
            visitDTO.PurposeOfVisitRemarks = v.PurposeOfVisitRemarks;


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
        List<Visitors> visitors = _context.Visitors.Where(v => v.VisitorTypeID == visitorTypeID && v.IsActive == true)
          .ToList();


        DataSet visitortype = new DataSet();
        List<VisitorsDTO> dto = new List<VisitorsDTO>();

        VisitorsDTO visitDTO = null;
        foreach (Visitors v in visitors)
        {
            visitDTO = new VisitorsDTO();

            visitDTO.ID = v.ID;
            visitDTO.Name = v.Name;
            visitDTO.TotalNoOfMen = v.TotalNoOfMen;
            visitDTO.TotalNoOfWomen = v.TotalNoOfWomen;
            visitDTO.TotalNoOfChildren = v.TotalNoOfChildren;
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
            visitDTO.IsActive = Convert.ToBoolean(v.IsActive);
            visitDTO.VisitorReference = v.VisitorReference;
            visitDTO.RoomRentType = v.RoomRentType;
            visitDTO.AdmissionNumber = v.AdmissionNumber;
            visitDTO.PurposeOfVisitRemarks = v.PurposeOfVisitRemarks;
                visitortype = DAL.DalAccessUtility.GetDataInDataSet("exec [GetRoomNumbersWithBuildingName] " + v.ID);

            if (visitortype != null && visitortype.Tables != null && visitortype.Tables.Count > 0
                && visitortype.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < visitortype.Tables[0].Rows.Count; i++)
                {
                    if (visitDTO.BuildingName == null || !visitDTO.BuildingName.Contains(visitortype.Tables[0].Rows[i]["Name"].ToString()))
                    {
                        visitDTO.BuildingName = visitortype.Tables[0].Rows[i]["Name"].ToString() + ",";
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

    public List<BuildingName> GetBuildingNameList()
    {
        return _context.BuildingName.ToList();
    }

    public List<RoomNumbers> GetAvailableRoomList(int BuildingID)
    {
        return _context.RoomNumbers.Where(r => (!_context.VisitorRoomNumbers.Select(vr => vr.RoomNumberID).Contains(r.ID)) && (r.BuildingID == BuildingID)).ToList();
    }

    public int GetPermanentRoomList(int BuildingID)
    {
        return _context.RoomNumbers.Where(r => r.BuildingID == BuildingID && r.IsPermanent == true).Count();
    }

    public int GetTemporaryRoomList(int BuildingID)
    {
        return _context.RoomNumbers.Where(r => r.BuildingID == BuildingID && r.IsPermanent == false).Count();
    }

    public int GetAvailableRoomListCount(int BuildingID)
    {
        return _context.RoomNumbers.Where(r => (!_context.VisitorRoomNumbers.Select(vr => vr.RoomNumberID).Contains(r.ID)) && (r.BuildingID == BuildingID)).Count();
    }

    public int GetBookedTemporaryRoomListCount(int BuildingID)
    {
        return _context.RoomNumbers.Where(r => (_context.VisitorRoomNumbers.Select(vr => vr.RoomNumberID).Contains(r.ID)) && (r.BuildingID == BuildingID && r.IsPermanent == false)).Count();
    }

    public int GetBookedPermanentRoomListCount(int BuildingID)
    {
        return _context.RoomNumbers.Where(r => (_context.VisitorRoomNumbers.Select(vr => vr.RoomNumberID).Contains(r.ID)) && (r.BuildingID == BuildingID && r.IsPermanent == true)).Count();
    }

    public string GetBookedRoomList(int BuildingID)
    {
        List<RoomNumbers> rm = new List<RoomNumbers>();
        string roomnumbers = string.Empty;

        rm = _context.RoomNumbers.Where(r => (_context.VisitorRoomNumbers.Select(vr => vr.RoomNumberID).Contains(r.ID)) && (r.BuildingID == BuildingID)).ToList();

        roomnumbers = string.Join(",", rm.Select(r => r.ID));
        return roomnumbers;
    }

    public List<RoomNumbers> GetBookedRoomsByVisitorID(int VisitorID)
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

    public List<RoomNumbers> GetRoomList(int BuildingID, int VisitorType)
    {
        List<RoomNumbers> roomList = new List<RoomNumbers>();

        if (VisitorType == (int)TypeEnum.VisitoryType.Visitor)
        {
            roomList = _context.RoomNumbers.Where(r => r.BuildingID == BuildingID && r.IsPermanent == false).ToList();
        }
        else
        {
            roomList = _context.RoomNumbers.Where(r => r.BuildingID == BuildingID).ToList();
        }

        return roomList;
    }

    public void CheckOutVisitor(int VisitorID)
    {
        List<VisitorRoomNumbers> visitorroom = _context.VisitorRoomNumbers.Where(v => v.VisitorID == VisitorID).ToList();

        VisitorsCheckOutLog checkoutLog = null;
        foreach (VisitorRoomNumbers v in visitorroom)
        {
            checkoutLog = new VisitorsCheckOutLog();
            checkoutLog.VisitorID = v.VisitorID;
            checkoutLog.RoomNumberID = v.RoomNumberID;
            checkoutLog.CreatedOn = v.CreatedOn;
            _context.Entry(checkoutLog).State = EntityState.Added;
            _context.SaveChanges();
        }

        _context.VisitorRoomNumbers.RemoveRange(_context.VisitorRoomNumbers.Where(v => v.VisitorID == VisitorID));

        Visitors visitor = _context.Visitors.Where(v => v.ID == VisitorID).Include(r => r.VisitorRoomNumbers).FirstOrDefault();

        visitor.VisitorRoomNumbers = null;
        visitor.IsActive = false;
        visitor.TimePeriodTo = Utility.GetLocalDateTime(DateTime.UtcNow);
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
        dto.TotalNoOfMen = visitor.TotalNoOfMen;
        dto.TotalNoOfWomen = visitor.TotalNoOfWomen;
        dto.TotalNoOfChildren = visitor.TotalNoOfChildren;
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
        dto.IsActive = Convert.ToBoolean(visitor.IsActive);
        dto.VisitorReference = visitor.VisitorReference;
        dto.RoomRentType = visitor.RoomRentType;
        dto.AdmissionNumber = visitor.AdmissionNumber;
        dto.PurposeOfVisitRemarks = visitor.PurposeOfVisitRemarks;
        return dto;
        //   newVisitor

        //declate all properties
    }

    public void UpdateVisitor(Visitors visitor, int Usertype)
    {
        _context.VisitorRoomNumbers.RemoveRange(_context.VisitorRoomNumbers.Where(v => v.VisitorID == visitor.ID));
        _context.SaveChanges();

        Visitors newVisitor = _context.Visitors.Where(v => v.ID == visitor.ID).Include(r => r.VisitorRoomNumbers).FirstOrDefault();
        if (Usertype == (int)TypeEnum.UserType.FrontDesk)
        {
            newVisitor.Name = visitor.Name;
            newVisitor.ContactNumber = visitor.ContactNumber;
            newVisitor.TimePeriodTo = visitor.TimePeriodTo;
            newVisitor.TimePeriodFrom = visitor.TimePeriodFrom;
            newVisitor.BuildingID = visitor.BuildingID;
            newVisitor.VisitorAddress = visitor.VisitorAddress;

            DataTable dsRoom = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct Name From BuildingName Where ID='" + visitor.BuildingID + "'").Tables[0];

            string MsgInfo = string.Empty;
            MsgInfo += "<table style='width:100%;'>";
            MsgInfo += "<tr>";
            MsgInfo += "<td style='padding:0px; text-align:left; width:50%' valign='top'>";
            MsgInfo += "<img src='http://akalsewa.org/img/logoakalnew.png' style='width:100%;' />";
            MsgInfo += "</td>";
            MsgInfo += "<td style='text-align: right; width:40%;'>";
            MsgInfo += "<br /><br />";
            MsgInfo += "<div style='font-style:italic; text-align: right;'>";
            MsgInfo += "Baru Shahib,";
            MsgInfo += "<br />Dist: Sirmaur";
            MsgInfo += "<br />Himachal Pradesh-173001";
            MsgInfo += "</td>";
            MsgInfo += "</tr>";
            MsgInfo += "</table>";
            MsgInfo += "<table border='1' style='width:100%'>";
            MsgInfo += "<tbody>";
            MsgInfo += "<tr>";
            MsgInfo += "<td>";
            MsgInfo += "<b>Visitor Name:</b>";
            MsgInfo += "</td>";
            MsgInfo += "<td>";
            MsgInfo += visitor.Name;
            MsgInfo += "</td>";
            MsgInfo += "</tr>";
            MsgInfo += "<tr>";
            MsgInfo += "<td>";
            MsgInfo += "<b>Visitor Contact Number:</b>";
            MsgInfo += "</td>";
            MsgInfo += "<td>";
            MsgInfo += visitor.ContactNumber;
            MsgInfo += "</td>";
            MsgInfo += "</tr>";
            MsgInfo += "<tr>";
            MsgInfo += "<td>";
            MsgInfo += "<b>Visitor Address:</b>";
            MsgInfo += "</td>";
            MsgInfo += "<td>";
            MsgInfo += visitor.VisitorAddress;
            MsgInfo += "</td>";
            MsgInfo += "</tr>";
            MsgInfo += "<tr>";
            MsgInfo += "<td>";
            MsgInfo += "<b>Building Name:</b>";
            MsgInfo += "</td>";
            MsgInfo += "<td>";
            MsgInfo += dsRoom.Rows[0]["Name"].ToString();
            MsgInfo += "</td>";
            MsgInfo += "</tr>";
            MsgInfo += "<tr>";
            MsgInfo += "<td>";
            MsgInfo += "<b>CheckIn Date:</b>";
            MsgInfo += "</td>";
            MsgInfo += "<td>";
            MsgInfo += visitor.CreatedOn;
            MsgInfo += "</td>";
            MsgInfo += "</tr>";
            MsgInfo += "<tr>";
            MsgInfo += "<td>";
            MsgInfo += "<b>CheckOut Date:</b>";
            MsgInfo += "</td>";
            MsgInfo += "<td>";
            MsgInfo += visitor.TimePeriodTo;
            MsgInfo += "</td>";
            MsgInfo += "</tr>";
            MsgInfo += "</tbody>";
            MsgInfo += "</table>";

            string FileName = string.Empty;
            //     string to = "bhupinder@barusahib.org";
            string cc = string.Empty;

            try
            {
                //    Utility.SendEmailWithoutAttachments(to, cc, MsgInfo, "New Changes are Updated In Visitor");
            }
            catch { }
            finally
            {

            }
        }
        else
        {
            newVisitor.Name = visitor.Name;
            newVisitor.TotalNoOfMen = visitor.TotalNoOfMen;
            newVisitor.TotalNoOfWomen = visitor.TotalNoOfWomen;
            newVisitor.TotalNoOfChildren = visitor.TotalNoOfChildren;
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

            newVisitor.RoomRent = visitor.RoomRent;
            newVisitor.ElectricityBill = visitor.ElectricityBill;
            newVisitor.State = visitor.State;
            newVisitor.Country = visitor.Country;
            newVisitor.City = visitor.City;
            newVisitor.IsActive = visitor.IsActive;
            newVisitor.VisitorReference = visitor.VisitorReference;
            newVisitor.RoomRentType = visitor.RoomRentType;

        }
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

    public List<Visitors> GetUnCheckOutVisitor(DateTime date, bool isActive)
    {

        List<Visitors> visitors = _context.Visitors.Where(v => v.TimePeriodTo < date && v.IsActive == isActive && v.VisitorTypeID == (int)TypeEnum.VisitoryType.Visitor)
                            .Include(r => r.VisitorRoomNumbers)
                            .Include(o => o.VisitorRoomNumbers.Select(emp => emp.RoomNumbers))
                            .Include(o => o.VisitorRoomNumbers.Select(emp => emp.RoomNumbers.BuildingName))
                            .ToList();

        return visitors;
    }


    public int GetUnCheckOutRoomCount(DateTime date, bool isActive,int VisitorTypeID )
    {
        var visitors = _context.Visitors.Where(v => v.TimePeriodTo < date && v.IsActive == isActive && v.VisitorTypeID == VisitorTypeID).Count();
        return visitors;
    }


    public List<City> GetCityByStateID(int stateID)
    {
        return _context.City.Where(x => x.StateId == stateID).OrderBy(x => x.CityName).ToList();
    }

    public void AddNewReceipt(VisitorReceipt vr)
    {
        _context.Entry(vr).State = EntityState.Added;
        _context.SaveChanges();
    }
    public void AddNewBuilding(BuildingName bName)
    {
        _context.Entry(bName).State = EntityState.Added;
        _context.SaveChanges();
    }

    public void AddNewRooms(RoomNumbers roomNumber)
    {
        _context.Entry(roomNumber).State = EntityState.Added;
        _context.SaveChanges();
    }

    public List<RoomNumbers> GetVisitorRoomDetail(int RoomID)
    {
        List<RoomNumbers> roomNumbers = _context.RoomNumbers.Include(a => a.BuildingName).ToList();
        return roomNumbers;
    }

    public RoomNumbers GetRoomInfoToUpdate(int RoomID)
    {
        RoomNumbers roomNumbers = _context.RoomNumbers.Where(v => v.ID == RoomID)
            .Include(e => e.BuildingName)
          .FirstOrDefault();
        RoomNumbers dto = new RoomNumbers();
        dto.ID = roomNumbers.ID;
        dto.NumOfBed = roomNumbers.NumOfBed;
        dto.Number = roomNumbers.Number;
        dto.BuildingID = roomNumbers.BuildingID;
        dto.BuildingFloor = roomNumbers.BuildingFloor;
        dto.IsPermanent = roomNumbers.IsPermanent;
        return dto;
    }

    public void UpdateVisitorRoomInfo(RoomNumbers RoomNumbers)
    {

        RoomNumbers newRoomNumbers = _context.RoomNumbers.Where(v => v.ID == RoomNumbers.ID).Include(r => r.BuildingName).FirstOrDefault();

        newRoomNumbers.BuildingID = RoomNumbers.BuildingID;
        newRoomNumbers.BuildingFloor = RoomNumbers.BuildingFloor;
        newRoomNumbers.Number = RoomNumbers.Number;
        newRoomNumbers.IsPermanent = RoomNumbers.IsPermanent;
        newRoomNumbers.NumOfBed = RoomNumbers.NumOfBed;
        _context.Entry(newRoomNumbers).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public List<State> GetStateByCountryID(int CountryID)
    {
        return _context.State.Where(x => x.CountryId == CountryID).OrderBy(x => x.StateName).ToList();
    }

    public StudentDetail GetVisitorInfoByAdminsnNumber(int AdmissionNumber)
    {
        StudentDetail visitor = _context.StudentDetail.Where(v => v.AdmissionNumber == AdmissionNumber).FirstOrDefault();
        return visitor;
    }

    public void AddNewStudents(StudentDetail student)
    {
        _context.Entry(student).State = EntityState.Added;
        _context.SaveChanges();
    }
    public void UpdateStudentsInfo(StudentDetail student)
    {
       StudentDetail stu = _context.StudentDetail.Where(v => v.ID == student.ID).FirstOrDefault();
        stu.ID = student.ID;
        stu.ContactNo = student.ContactNo;
        stu.Class = student.Class;
        stu.StudentName = student.StudentName;
        stu.CountryID = student.CountryID;
        stu.StateID = student.StateID;
        stu.CityID = student.CityID;
        stu.Address = student.Address;
        
        _context.Entry(stu).State = EntityState.Modified;
        _context.SaveChanges();
    }
}