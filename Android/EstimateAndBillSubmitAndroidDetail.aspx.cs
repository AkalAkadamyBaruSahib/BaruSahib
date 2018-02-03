using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Android_EstimateAndBillSubmitAndroidDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["Action"] != null)
        {
            string action = Request["Action"].ToString();
            if (action == "ZoneName")
            {
                if (Request["UserId"] != null)
                {
                    GetZoneByUserName(Request["UserId"].ToString());
                }
            }
            else if (action == "AcaName")
            {
                if (Request["UserId"] != null && Request["ZoneId"] != null)
                {
                    GetAcademyByUserIDAndZoneID(Request["UserId"].ToString(), Request["ZoneId"].ToString());
                }
            }
            else if (action == "WorkAllotName")
            {
                if (Request["AcaId"] != null)
                {
                    GetWorkAllotNameByAcaID(Request["AcaId"].ToString());
                }
            }
            else if (action == "WorkType")
            {
                GetTypeOfWorkDetail();
            }
            else if (action == "SourceType")
            {
                GetSourceTypeName();
            }
            else if (action == "MaterialType")
            {
                GetAllMaterialType();
            }
            else if (action == "MaterialName")
            {
                if (Request["MatTypeID"] != null)
                {
                    GetAllMaterialNameByMatTypeID(Request["MatTypeID"].ToString());
                }
               
            }
            else if (action == "SaveEstimateDetail")
            {
                SaveEstimate(Request["ZoneId"].ToString(), Request["AcaId"].ToString(), Request["SubEstimate"].ToString(),
                    Request["TypeWorkId"].ToString(), Request["WAId"].ToString(), Request["FileNme"].ToString(),
                    Request["ModuleID"].ToString(), Request["CreatedBy"].ToString(), Request["UserTypeID"].ToString(), Request["StartDate"].ToString(), Request["EndDate"].ToString());
            }
            //else if (action == "AcademyAssignToEmp")
            //{
            //    if (Request["UserID"] != null)
            //    {
            //        GetAllAcademyAssignToEmploeeByUserID(Request["UserID"].ToString());
            //    }
            //}
            //else if (action == "SanctionedBill")
            //{
            //    if (Request["AcaId"] != null && Request["WorkAllotId"]!=null)
            //    {
            //        GetMaterialInSanctionedBill(Request["AcaId"].ToString(),Request["WorkAllotId"].ToString());
            //    }
            //}
            //else if (action == "AgencyDetail")
            //{
            //    GetAllAgencyName();
            //}
        }
    }

    public void GetZoneByUserName(string userID)
    {
        List<Zone> ZoneNameView = new List<Zone>();
        PurchaseRepository repo = new PurchaseRepository(new AkalAcademy.DataContext());
        ZoneNameView = repo.GetZoneByInchargeID(int.Parse(userID));
        string json = JsonConvert.SerializeObject(ZoneNameView, Formatting.Indented);
        Response.ContentType = "application/json";
        Response.Write(json);
    }

    public void GetAcademyByUserIDAndZoneID(string userID,string zoneID)
    {
        List<Academy> AcaNameView = new List<Academy>();
        PurchaseRepository repo = new PurchaseRepository(new AkalAcademy.DataContext());
        AcaNameView = repo.GetAcademybyZoneIDByEmpID(int.Parse(zoneID), int.Parse(userID));
        string json = JsonConvert.SerializeObject(AcaNameView, Formatting.Indented);
        Response.ContentType = "application/json";
        Response.Write(json);
    }

    public void GetWorkAllotNameByAcaID(string AcaID)
    {
        List<WorkAllot> WorkAllotView = new List<WorkAllot>();
        PurchaseRepository repo = new PurchaseRepository(new AkalAcademy.DataContext());
        WorkAllotView = repo.GetWorkAllotByAcademyID(int.Parse(AcaID));
        string json = JsonConvert.SerializeObject(WorkAllotView, Formatting.Indented);
        Response.ContentType = "application/json";
        Response.Write(json);
    }

    public void GetTypeOfWorkDetail()
    {
        DataTable dsWorkType = new DataTable();
        dsWorkType = DAL.DalAccessUtility.GetDataInDataSet("select TypeWorkId,TypeWorkName from TypeOfWork where Active=1").Tables[0];
        string json = JsonConvert.SerializeObject(dsWorkType, Formatting.Indented);
        Response.ContentType = "application/json";
        Response.Write(json);
    }

    public void GetSourceTypeName()
    {
        List<PurchaseSource> PurchaseSourceView = new List<PurchaseSource>();
        PurchaseRepository repo = new PurchaseRepository(new AkalAcademy.DataContext());
        PurchaseSourceView = repo.GetPurchaseSource();
        string json = JsonConvert.SerializeObject(PurchaseSourceView, Formatting.Indented);
       Response.ContentType = "application/json";
        Response.Write(json);
    }

    public void GetAllMaterialType()
    {
        List<MaterialType> MaterialTypeView = new List<MaterialType>();
        PurchaseRepository repo = new PurchaseRepository(new AkalAcademy.DataContext());
        MaterialTypeView = repo.GetActiveMaterialTypes();
        string json = JsonConvert.SerializeObject(MaterialTypeView, Formatting.Indented);
        Response.ContentType = "application/json";
        Response.Write(json);
    }

    public void GetAllMaterialNameByMatTypeID(string MatTypeID)
    {
        List<MaterialsDTO> MaterialNameView = new List<MaterialsDTO>();
        PurchaseRepository repo = new PurchaseRepository(new AkalAcademy.DataContext());
        MaterialNameView = repo.GetActiveMaterialsByMatTypeID(int.Parse(MatTypeID));
        string json = JsonConvert.SerializeObject(MaterialNameView, Formatting.Indented);
        Response.ContentType = "application/json";
        Response.Write(json);
    }

    public void SaveEstimate(string ZoneId, string AcaId, string SubEstimate, string TypeWorkId, string WAId, string FileNme, string ModuleID, string CreatedBy, string UserTypeID, string StartDate, string EndDate)
    {
       Estimate est = new Estimate();

       est.EstId = 0;
        est.ZoneId = int.Parse(ZoneId);
        est.AcaId = int.Parse(AcaId);
        est.SubEstimate = SubEstimate;
        est.TypeWorkId = int.Parse(TypeWorkId);
        est.Active = 1;
        est.WAId = int.Parse(WAId);
        est.FileNme = FileNme;
        est.CreatedBy = int.Parse(CreatedBy);
        est.ModifyBy = int.Parse(CreatedBy);
        est.FilePath = "";
        if (int.Parse(UserTypeID) == 1 || int.Parse(UserTypeID) == 13 || int.Parse(UserTypeID) == 6)
        {
            est.IsApproved = true;
        }
        else
        {
            est.IsApproved = false;
        }
        if (int.Parse(UserTypeID) == 1 || int.Parse(UserTypeID) == 13 || int.Parse(UserTypeID) == 6)
        {
            est.SanctionDate = Utility.GetLocalDateTime(DateTime.UtcNow);
        }
        est.CreatedOn = Utility.GetLocalDateTime(DateTime.UtcNow);

        est.IsRejected = false;
        est.IsActive = true;
        est.ModuleID = int.Parse(ModuleID);
        est.IsReceived = false;
        est.ReceivedBy = 0;
        est.StartDate = Convert.ToDateTime(StartDate);
        est.EndDate = Convert.ToDateTime(EndDate);
        decimal Amt = 0;
        EstimateAndMaterialOthersRelations estmat = null;
        est.EstimateAndMaterialOthersRelations = new List<EstimateAndMaterialOthersRelations>();
        string responseString = Request["MaterialRowArray"].ToString();
        List<MaterialRowAndroid> materialRowArray = JsonConvert.DeserializeObject<List<MaterialRowAndroid>>(responseString);
        for (int i = 0; i < materialRowArray.Count; i++)
        {
            estmat = new EstimateAndMaterialOthersRelations();
            estmat.EstId = est.EstId;
            estmat.MatId = materialRowArray[i].MatId;
            estmat.MatTypeId = materialRowArray[i].MatTypeId;
            estmat.PSId = materialRowArray[i].PSId;
            estmat.Qty = materialRowArray[i].Qty;
            estmat.Rate = materialRowArray[i].Rate;
            estmat.Remark = materialRowArray[i].Remark;
            estmat.UnitId = materialRowArray[i].UnitId;
            estmat.CreatedBy = int.Parse(CreatedBy);
            estmat.ModifyBy = int.Parse(CreatedBy);
            estmat.Active = 1;
            estmat.CreatedOn = Utility.GetLocalDateTime(DateTime.UtcNow);
            estmat.ModifyOn = Utility.GetLocalDateTime(DateTime.UtcNow);
            estmat.IsApproved = true;
            estmat.VendorId = 0;
            estmat.PurchaseQty = 0;
            estmat.PurchaseEmpID = 0;
            estmat.DispatchStatus = 0;
            estmat.DirectPurchase = false;
            estmat.MRP = 0;
            estmat.Discount = 0;
            estmat.Vat = 0;
            estmat.AdditionalDiscount = 0;
            estmat.GST = 0;
            Amt += Convert.ToDecimal(materialRowArray[i].Qty) * Convert.ToDecimal(materialRowArray[i].Rate);
            estmat.Amount = Convert.ToDecimal(materialRowArray[i].Qty) * Convert.ToDecimal(materialRowArray[i].Rate);
            est.EstimateAndMaterialOthersRelations.Add(estmat);
        }

        est.EstmateCost = Amt;
        PurchaseRepository repo = new PurchaseRepository(new AkalAcademy.DataContext());
        repo.SaveEstimateDetail(est);
     
        SaveImage(est.EstId);
               

        JObject jsonObject = new JObject();
        jsonObject["status"] = 1;
        string json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
        Response.ContentType = "application/json";
        Response.Write(json);
      
    }

    public void SaveImage(int estID)
    {
        DirectoryInfo dir = null;

        string databasePath = string.Empty;
        HttpFileCollection files = Request.Files;
        string path = "~/EstFile/";
        for (int i = 0; i < files.Count; i++)
        {
            HttpPostedFile file = files[i];

            if (file.ContentLength > 0)
            {
                string ext = System.IO.Path.GetExtension(file.FileName).Trim();

                string fileName = estID + "_Signcopy_" + (i + 1) + ext;
                file.SaveAs(Server.MapPath(path + fileName));
                databasePath += "EstFile/" + fileName + ",";
            }
        }
        databasePath = databasePath.Substring(0, databasePath.Length - 1);
        
        DAL.DalAccessUtility.ExecuteNonQuery("Update Estimate set FilePath='" + databasePath + "' where estid=" + estID);
      
        
    }
    
    //public void GetAllAcademyAssignToEmploeeByUserID(string userID)
    //{
    //    DataTable dsAca = new DataTable();
    //    dsAca = DAL.DalAccessUtility.GetDataInDataSet("select A.AcaId,A.AcaName from Academy A INNER JOIN AcademyAssignToEmployee AAE ON A.AcaID = AAE.AcaId where A.Active=1 and AAE.EmpID='" + userID + "'  Order by AcaName").Tables[0];
    //    string json = JsonConvert.SerializeObject(dsAca, Formatting.Indented);
    //    Response.ContentType = "application/json";
    //    Response.Write(json);
    //}

    //public void GetMaterialInSanctionedBill(string acaID, string waID)
    //{
    //    DataTable dsAca = new DataTable();
    //    dsAca = DAL.DalAccessUtility.GetDataInDataSet("exec USP_getEstimateBalanceNew'" + waID + "','1'").Tables[0];
    //    string json = JsonConvert.SerializeObject(dsAca, Formatting.Indented);
    //    Response.ContentType = "application/json";
    //    Response.Write(json);
    //}
    //public void GetAllAgencyName()
    //{
    //    List<VendorInfoDTO> VendorInfoDTOView = new List<VendorInfoDTO>();
    //    PurchaseRepository repo = new PurchaseRepository(new AkalAcademy.DataContext());
    //    VendorInfoDTOView = repo.GetActiveVendor();
    //    string json = JsonConvert.SerializeObject(VendorInfoDTOView, Formatting.Indented);
    //    Response.ContentType = "application/json";
    //    Response.Write(json);
    //}

}