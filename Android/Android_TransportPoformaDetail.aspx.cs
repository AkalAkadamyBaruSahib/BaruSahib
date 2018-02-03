using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Android_Android_TransportPoformaDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request["Action"] != null)
        {
            string action = Request["Action"].ToString();
            if (action == "VehiclesDetail")
            {
                if (Request["AcaID"] != null)
                {
                    GetVehiclesNumberByAcaID(Request["AcaID"].ToString());
                }
            }
            if (action == "LateArrivingReason")
            {
                GetLateArrivingReasonlist();
            }
            if (action == "TransportComplaintReason")
            {
                GetTransportComplaintReasonList();
            }
            if (action == "SaveDailyProforma")
            {
                SaveDailyProforma(Request["AcaId"].ToString(), Request["totalStuMor"].ToString(), Request["PresentStuMor"].ToString(), Request["totalStaff"].ToString(), Request["presentStaff"].ToString(),
                Request["supervisor"].ToString(), Request["fSecurityG"].ToString(), Request["sSecurityG"].ToString(), Request["teacher"].ToString(), Request["totalStuEve"].ToString(), Request["PresentStuEve"].ToString(), Request["UserID"].ToString());
            }
        }
    }
    public void GetVehiclesNumberByAcaID(string AcaID)
    {
        JArray mainArray = new JArray();

        DataTable VehiclesView = new DataTable();
        VehiclesView = DAL.DalAccessUtility.GetDataInDataSet("select distinct V.Number,V.ID from Vehicles V where V.AcademyID='" + AcaID + "' and V.isApproved=1 ").Tables[0];

        for (int i = 0; i < VehiclesView.Rows.Count; i++)
        {
            String number = VehiclesView.Rows[i]["Number"].ToString();
            String id = VehiclesView.Rows[i]["ID"].ToString();
            DataTable dsEmplyoee = new DataTable();
            JObject jObject = new JObject();
            jObject.Add("Vehicle_No", number);
            jObject.Add("ID", id);

            // dsEmplyoee = DAL.DalAccessUtility.GetDataInDataSet("select VE.Name,CASE WHEN VE.EmployeeType=1 then 'Driver' WHEN VE.EmployeeType=2 THEN 'Conductor' END AS EmployeeType,VE.isActive from  VehicleEmployee VE where VE.VehicleID='" + VehiclesView.Rows[i]["ID"].ToString() + "' ").Tables[0];
            dsEmplyoee = DAL.DalAccessUtility.GetDataInDataSet("select VE.Name,VE.EmployeeType from  VehicleEmployee VE where VE.VehicleID='" + VehiclesView.Rows[i]["ID"].ToString() + "' and VE.isActive=1 ").Tables[0];

            for (int j = 0; j < dsEmplyoee.Rows.Count; j++)
            {
                string Name = dsEmplyoee.Rows[j]["Name"].ToString();
                if (dsEmplyoee.Rows[j]["EmployeeType"].ToString() == "1")
                {
                    jObject.Add("Driver_Name", Name);
                }
                if (dsEmplyoee.Rows[j]["EmployeeType"].ToString() == "2")
                {
                    jObject.Add("Conductor_Name", Name);
                }

            }
            mainArray.Add(jObject);
        }

        string json = JsonConvert.SerializeObject(mainArray, Formatting.Indented);
        Response.ContentType = "application/json";
        Response.Write(json);
    }

    public void GetLateArrivingReasonlist()
    {
        DataTable dsLateArriving = new DataTable();
        dsLateArriving = DAL.DalAccessUtility.GetDataInDataSet("select * from [dbo].[Transport_LateArrivingReason]").Tables[0];
        string json = JsonConvert.SerializeObject(dsLateArriving, Formatting.Indented);
        Response.ContentType = "application/json";
        Response.Write(json);
    }

    public void GetTransportComplaintReasonList()
    {
        DataTable dsLateArriving = new DataTable();
        dsLateArriving = DAL.DalAccessUtility.GetDataInDataSet("select * from [dbo].[Transport_ComplaintReason]").Tables[0];
        string json = JsonConvert.SerializeObject(dsLateArriving, Formatting.Indented);
        Response.ContentType = "application/json";
        Response.Write(json);
    }

    public void SaveDailyProforma(string AcaId, string totalStuMor, string PresentStuMor, string totalStaff, string presentStaff, string supervisor, string fSecurityG, string sSecurityG, string teacher, string totalStuEve, string PresentStuEve,string UserID)
    {
        Android_TransportDailyProformaDetail est = new Android_TransportDailyProformaDetail();

        est.TProformaID = 0;
        est.AcaID = int.Parse(AcaId);
        est.Teacher = teacher;
        est.SecondSecurityGuard = sSecurityG;
        est.FirstSecurityGuard = fSecurityG;
        est.Supervisor = Convert.ToBoolean(supervisor);
        est.PresentStaff = int.Parse(presentStaff);
        est.TotalStaff = int.Parse(totalStaff);
        est.TotalStuMorning = int.Parse(totalStuMor);
        est.PresentStuMorning = int.Parse(PresentStuMor);
        est.TotalStuEvening = int.Parse(totalStuEve);
        est.PresentStuEvening = int.Parse(PresentStuEve);
        est.CreatedBy = int.Parse(UserID);
        est.CreatedOn = Utility.GetLocalDateTime(DateTime.UtcNow);
        Android_AbsentConductorArray absntCond = null;
        est.Android_AbsentConductorArray = new List<Android_AbsentConductorArray>();
        string responseString = Request["absCondArray"].ToString();
        List<Android_AbsentConductorArray> absentConductorArray = JsonConvert.DeserializeObject<List<Android_AbsentConductorArray>>(responseString);
        for (int i = 0; i < absentConductorArray.Count; i++)
        {
            absntCond = new Android_AbsentConductorArray();
            absntCond.TransportDailyProformaID = est.TProformaID;
            absntCond.VehicleID = absentConductorArray[i].VehicleID;
            absntCond.Reason = absentConductorArray[i].Reason;
            est.Android_AbsentConductorArray.Add(absntCond);
        }

        Android_TransportComplaintArray transportComplaint = null;
        est.Android_TransportComplaintArray = new List<Android_TransportComplaintArray>();
        string transportComplaintArrayString = Request["tComplaintArray"].ToString();
        List<Android_TransportComplaintArray> transportComplaintArray = JsonConvert.DeserializeObject<List<Android_TransportComplaintArray>>(transportComplaintArrayString);
        for (int i = 0; i < transportComplaintArray.Count; i++)
        {
            transportComplaint = new Android_TransportComplaintArray();
            transportComplaint.TransportDailyProformaID = est.TProformaID;
            transportComplaint.VehicleID = transportComplaintArray[i].VehicleID;
            transportComplaint.ComplaintID = transportComplaintArray[i].ComplaintID;
            transportComplaint.Solution = transportComplaintArray[i].Solution;
            transportComplaint.OtherComplaint = transportComplaintArray[i].OtherComplaint;

            est.Android_TransportComplaintArray.Add(transportComplaint);
        }

        Android_WithoutUniformDriverAndConductorArray withoutUniformConductor = null;
        est.Android_WithoutUniformDriverAndConductorArray = new List<Android_WithoutUniformDriverAndConductorArray>();
        string withoutUniformConductorString = Request["wuConductorArray"].ToString();
        List<Android_WithoutUniformDriverAndConductorArray> withoutUniformConductorArray = JsonConvert.DeserializeObject<List<Android_WithoutUniformDriverAndConductorArray>>(withoutUniformConductorString);
        for (int i = 0; i < withoutUniformConductorArray.Count; i++)
        {
            withoutUniformConductor = new Android_WithoutUniformDriverAndConductorArray();
            withoutUniformConductor.TransportDailyProformaID = est.TProformaID;
            withoutUniformConductor.VehicleID = withoutUniformConductorArray[i].VehicleID;
            withoutUniformConductor.Name = withoutUniformConductorArray[i].Name;
            withoutUniformConductor.EmployeeTypeId = 2;
            est.Android_WithoutUniformDriverAndConductorArray.Add(withoutUniformConductor);
        }

        Android_WithoutUniformDriverAndConductorArray withoutUniformDriver = null;
        string withoutUniformDriverArrayString = Request["wuDriverArray"].ToString();
        List<Android_WithoutUniformDriverAndConductorArray> withoutUniformDriverArray = JsonConvert.DeserializeObject<List<Android_WithoutUniformDriverAndConductorArray>>(withoutUniformDriverArrayString);
        for (int i = 0; i < withoutUniformDriverArray.Count; i++)
        {
            withoutUniformDriver = new Android_WithoutUniformDriverAndConductorArray();
            withoutUniformDriver.TransportDailyProformaID = est.TProformaID;
            withoutUniformDriver.VehicleID = withoutUniformDriverArray[i].VehicleID;
            withoutUniformDriver.Name = withoutUniformDriverArray[i].Name;
            withoutUniformDriver.EmployeeTypeId = 1;
            est.Android_WithoutUniformDriverAndConductorArray.Add(withoutUniformDriver);
        }

        LateArrivingVehiclesMorningAndEvening lateArrivingVehicles = null;
        est.LateArrivingVehiclesMorningAndEvening = new List<LateArrivingVehiclesMorningAndEvening>();
        string lateArrivingVehiclesString = Request["lateRowEArray"].ToString();
        List<lateRowModel> lateArrivingVehiclesStringArray = JsonConvert.DeserializeObject<List<lateRowModel>>(lateArrivingVehiclesString);
        for (int i = 0; i < lateArrivingVehiclesStringArray.Count; i++)
        {
            lateArrivingVehicles = new LateArrivingVehiclesMorningAndEvening();
            lateArrivingVehicles.TransportDailyProformaID = est.TProformaID;
            lateArrivingVehicles.VehicleID = lateArrivingVehiclesStringArray[i].VehicleID;
            lateArrivingVehicles.TimeOfArrival = lateArrivingVehiclesStringArray[i].TimeOfArrival;
            lateArrivingVehicles.ReasonID = lateArrivingVehiclesStringArray[i].ReasonID;
            lateArrivingVehicles.DayType = "E";
            lateArrivingVehicles.OtherReason = lateArrivingVehiclesStringArray[i].OtherReason;
            est.LateArrivingVehiclesMorningAndEvening.Add(lateArrivingVehicles);
        }

        LateArrivingVehiclesMorningAndEvening lateArrivingMVehicles = null;
        string lateArrivingVehiclesMString = Request["lateRowMArray"].ToString();
        List<lateRowModel> lateArrivingVehiclesStringMArray = JsonConvert.DeserializeObject<List<lateRowModel>>(lateArrivingVehiclesMString);
        for (int i = 0; i < lateArrivingVehiclesStringMArray.Count; i++)
        {
            lateArrivingMVehicles = new LateArrivingVehiclesMorningAndEvening();
            lateArrivingMVehicles.TransportDailyProformaID = est.TProformaID;
            lateArrivingMVehicles.VehicleID = lateArrivingVehiclesStringMArray[i].VehicleID;
            lateArrivingMVehicles.TimeOfArrival = lateArrivingVehiclesStringMArray[i].TimeOfArrival;
            lateArrivingMVehicles.ReasonID = lateArrivingVehiclesStringMArray[i].ReasonID;
            lateArrivingMVehicles.DayType = "M";
            lateArrivingMVehicles.OtherReason = lateArrivingVehiclesStringMArray[i].OtherReason;
       
            est.LateArrivingVehiclesMorningAndEvening.Add(lateArrivingMVehicles);
        }

        Android_AcademyVisitDetail academyVisitDetail = null;
        est.Android_AcademyVisitDetail = new List<Android_AcademyVisitDetail>();
        string academyVisitArray = Request["AcvRowArray"].ToString();
        List<Android_AcademyVisitDetail> academyVisitStringArray = JsonConvert.DeserializeObject<List<Android_AcademyVisitDetail>>(academyVisitArray);
        for (int i = 0; i < academyVisitStringArray.Count; i++)
        {
            academyVisitDetail = new Android_AcademyVisitDetail();
            academyVisitDetail.TransportDailyProformaID = est.TProformaID;
            academyVisitDetail.AcaCampusVisitTime = academyVisitStringArray[i].AcaCampusVisitTime;
            academyVisitDetail.AcaCampusVisitSolution = academyVisitStringArray[i].AcaCampusVisitSolution;
            academyVisitDetail.AcaCampusVisitComplaint = academyVisitStringArray[i].AcaCampusVisitComplaint;


            est.Android_AcademyVisitDetail.Add(academyVisitDetail);
        }


        TransportUserRepository repo = new TransportUserRepository(new AkalAcademy.DataContext());
        repo.SaveDailyproformaDetail(est);

      

        JObject jsonObject = new JObject();
        jsonObject["status"] = 1;
        string json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
        Response.ContentType = "application/json";
        Response.Write(json);

    }


}
