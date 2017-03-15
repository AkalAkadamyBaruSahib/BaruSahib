using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AkalAcademy;
using System.Data;
/// <summary>
/// Summary description for UsersRepository
/// </summary>
public class UsersRepository
{
    private DataContext _context;



    public UsersRepository(DataContext context)
    {
        _context = context;
    }

    public List<Incharge> GetUsers()
    {
        return _context.Incharge.ToList();
    }

    public List<Incharge> GetUsersByUserType(int UserTypeID)
    {
        return _context.Incharge.Where(x => x.UserTypeId == UserTypeID).ToList();
    }

    public Incharge GetInchargeByUserTypeAndAcaID(int userTypeID, int acaID)
    {
        var incharges = (from aae in _context.AcademyAssignToEmployee
                         join x in _context.Incharge on aae.EmpId equals x.InchargeId
                         where x.UserTypeId == userTypeID && aae.AcaId == acaID
                         // where x.UserTypeId.ToString().Contains(userTypeID.ToString()) && aae.AcaId == acaID
                         select new
                         {
                             InchargeId = x.InchargeId,
                             InMobile = x.InMobile,
                             InName = x.InName,
                             LoginId = x.LoginId,
                             UserTypeId = x.UserTypeId

                         }).AsEnumerable().Select(x => new Incharge
                         {
                             InchargeId = x.InchargeId,
                             InMobile = x.InMobile,
                             InName = x.InName,
                             LoginId = x.LoginId,
                             UserTypeId = x.UserTypeId
                         }).FirstOrDefault();

        return incharges;
    }


    public System.Data.DataTable GetInchargeByInchargeID(int ID)
    {
        System.Data.DataSet dsInchargeDetails = new System.Data.DataSet();
        dsInchargeDetails = DAL.DalAccessUtility.GetDataInDataSet("SELECT *,ase.ZoneId,ase.AcaId FROM INCHARGE  inc inner join AcademyAssignToEmployee ASE on ASE.EmpId=inc.InchargeId where  inc.InchargeId=" + ID);
        return dsInchargeDetails.Tables[0];
    }

    public List<string> GetUsersByUserTypeAndAcademic(int UserTypeID, int acaID)
    {
        var result = (from aae in _context.AcademyAssignToEmployee
                      join x in _context.Incharge on aae.EmpId equals x.InchargeId
                      where x.UserTypeId == UserTypeID && aae.AcaId == acaID
                      select x.InMobile).ToList();
        return result;
        //.Where(x => x.UserTypeId == UserTypeID);
    }

    public IList<Incharge> GetUsersByAcademyID(int AcaID, int UserTypeID)
    {

        var Incharges = (from aae in _context.AcademyAssignToEmployee
                         join x in _context.Incharge on aae.EmpId equals x.InchargeId
                         where x.UserTypeId == UserTypeID && aae.AcaId == AcaID
                         select new
                         {
                             InchargeId = x.InchargeId,
                             InMobile = x.InMobile,
                             InName = x.InName

                         }).AsEnumerable().Select(x => new Incharge
                         {
                             InchargeId = x.InchargeId,
                             InMobile = x.InMobile,
                             InName = x.InName
                         }).ToList();


        return Incharges;

    }


    public List<Zone> GetZoneByModuleID(int ModuleID)
    {
        DataTable dtZones = new DataTable();
        List<Zone> ZoneList = new List<Zone>();

        if (ModuleID == 1)
        {
            ZoneList = _context.Zone.ToList();
        }
        else
        {
            var zoneli = (from TZ in _context.TransportZoneAcademyRelation
                          join z in _context.Zone on TZ.ZoneId equals z.ZoneId
                          select new { z.ZoneId, z.ZoneName }).Distinct().ToList().OrderByDescending(x => x.ZoneName).Reverse();


            Zone zone = null;
            foreach (var item in zoneli)
            {
                zone = new Zone();
                zone.ZoneId = item.ZoneId;
                zone.ZoneName = item.ZoneName;
                ZoneList.Add(zone);
            }

        }
        return ZoneList;
    }

    public List<Zone> GetZoneByInchargeID(int InchargeID)
    {
        DataTable dtZones = new DataTable();
        List<Zone> ZoneList = new List<Zone>();


        var zoneli = (from AE in _context.AcademyAssignToEmployee
                      join z in _context.Zone on AE.ZoneId equals z.ZoneId
                      where AE.EmpId == InchargeID
                      select new { z.ZoneId, z.ZoneName }).Distinct().ToList().OrderByDescending(x => x.ZoneName).Reverse();

        Zone zone = null;
        foreach (var item in zoneli)
        {
            zone = new Zone();
            zone.ZoneId = item.ZoneId;
            zone.ZoneName = item.ZoneName;
            ZoneList.Add(zone);
        }


        return ZoneList;
    }

    public List<Academy> GetAcademyByZoneID(int ZoneID, int ModuleID)
    {
        DataTable dtZones = new DataTable();
        List<Academy> AcademyList = new List<Academy>();


        if (ModuleID == 1)
        {
            var Acali = (from AE in _context.AcademyAssignToEmployee
                         join A in _context.Academy on AE.AcaId equals A.AcaID
                         where AE.ZoneId == ZoneID
                         select new { A.AcaID, A.AcaName }).Distinct().ToList().OrderByDescending(x => x.AcaName).Reverse();


            Academy academy = null;
            foreach (var item in Acali)
            {
                academy = new Academy();
                academy.AcaID = item.AcaID;
                academy.AcaName = item.AcaName;
                AcademyList.Add(academy);
            }
        }
        if (ModuleID == 2)
        {
            var Acali = (from AE in _context.TransportZoneAcademyRelation
                         join A in _context.Academy on AE.TransportAcaID equals A.AcaID
                         where AE.ZoneId == ZoneID
                         select new { A.AcaID, A.AcaName }).Distinct().ToList().OrderByDescending(x => x.AcaName).Reverse();

            Academy academy = null;
            foreach (var item in Acali)
            {
                academy = new Academy();
                academy.AcaID = item.AcaID;
                academy.AcaName = item.AcaName;
                AcademyList.Add(academy);
            }
        }

        return AcademyList;
    }

    public List<Academy> GetAcademyByInchargeID(int InchargeID)
    {
        DataTable dtZones = new DataTable();
        List<Academy> AcademyList = new List<Academy>();

        var Acali = (from AE in _context.AcademyAssignToEmployee
                     join A in _context.Academy on AE.AcaId equals A.AcaID
                     where AE.EmpId == InchargeID
                     select new { A.AcaID, A.AcaName }).Distinct().ToList().OrderByDescending(x => x.AcaName).Reverse();

        Academy academy = null;
        foreach (var item in Acali)
        {
            academy = new Academy();
            academy.AcaID = item.AcaID;
            academy.AcaName = item.AcaName;
            AcademyList.Add(academy);
        }

        return AcademyList;
    }

    public List<Academy> GetAllAcademy(int ModuleID)
    {
        DataTable dtZones = new DataTable();
        List<Academy> AcademyList = new List<Academy>();


        if (ModuleID == 1)
        {
            var Acali = (from AE in _context.AcademyAssignToEmployee
                         join A in _context.Academy on AE.AcaId equals A.AcaID
                         select new { A.AcaID, A.AcaName }).Distinct().ToList().OrderByDescending(x => x.AcaName).Reverse().DefaultIfEmpty();

            Academy academy = null;
            foreach (var item in Acali)
            {
                academy = new Academy();
                academy.AcaID = item.AcaID;
                academy.AcaName = item.AcaName;
                AcademyList.Add(academy);
            }
        }
        if (ModuleID == 2)
        {
            var Acali = (from AE in _context.TransportZoneAcademyRelation
                         join A in _context.Academy on AE.TransportAcaID equals A.AcaID
                         select new { A.AcaID, A.AcaName }).Distinct().ToList().OrderByDescending(x => x.AcaName).Reverse();

            Academy academy = null;
            foreach (var item in Acali)
            {
                academy = new Academy();
                academy.AcaID = item.AcaID;
                academy.AcaName = item.AcaName;
                AcademyList.Add(academy);
            }
        }

        return AcademyList;
    }

    public DataSet GetAcademyByUserNameAndZoneID(string UserName, int ModuleID, int ZoneID)
    {
        return DAL.DalAccessUtility.GetDataInDataSet("exec USP_AcademayWithZone '" + UserName + "','" + ZoneID + "'," + ModuleID);
    }

    public Incharge GetLoginUserDetail(string UserName, string password)
    {
        return _context.Incharge.Where(v => v.LoginId == UserName && v.UserPwd == password && v.Active == 1).FirstOrDefault();
    }

    public AdminTypeRelation GetAdminType(int UserID)
    {
        return _context.AdminTypeRelation.Where(x => x.UserID == UserID).FirstOrDefault();
    }

    public DataTable GetCountry()
    {
        return DAL.DalAccessUtility.GetDataInDataSet("Select CountryID,CountryName from Country").Tables[0];
    }

    public DataTable GetStateByCountryID(int countryID)
    {
        return DAL.DalAccessUtility.GetDataInDataSet("Select StateID,StateName from State where countryID=" + countryID + " order by statename asc").Tables[0];
    }

    public DataTable GetCityByStateID(int stateID)
    {
        return DAL.DalAccessUtility.GetDataInDataSet("Select CityID,CityName from City where StateID=" + stateID + " order by cityname asc").Tables[0];
    }
}
