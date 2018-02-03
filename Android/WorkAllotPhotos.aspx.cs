using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class Android_WorkAllotPhotos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["Action"] != null)
        {
            string action = Request["Action"].ToString();
            if (action == "AcaName")
            {
                if (Request["UserId"] != null)
                {
                    GetAcademyByUserName(Request["UserId"].ToString());
                }
            }
            else if (action == "WorkAllotName")
            {
                if (Request["AcaId"] != null)
                {
                    GetWorkAllotNameByAcaID(Request["AcaId"].ToString());
                }
            }
            else if (action == "Login")
            {
                if (Request["UserName"] != null && Request["Password"] != null)
                {
                    GetLoginDetail(Request["UserName"].ToString(), Request["Password"].ToString());
                }

            }
            else if (action == "DrawingType")
            {
                GetAllDrawingTypeName();
            }
            else if (action == "ApprovedDrawing")
            {
                if (Request["DrawingTypeID"] != null && Request["AcaID"] != null && Request["UserID"] != null && Request["UserTypeID"] != null)
                {
                    GetApprovedDrawingDetail(Request["DrawingTypeID"].ToString(), Request["AcaID"].ToString(), Request["UserID"].ToString(), Request["UserTypeID"].ToString());
                }
            }
            else if (action == "NonpprovedDrawing")
            {
                if (Request["UserID"] != null && Request["UserTypeID"] != null)
                {
                    GetNonApprovedDrawingDetail(Request["UserID"].ToString(), Request["UserTypeID"].ToString());
                }
            }
            else if (action == "ApprovedTheDrawing")
            {
                if (Request["DwgId"] != null && Request["UserTypeID"] != null)
                {
                    ApprovedDrawing(Request["DwgId"].ToString(), Request["UserTypeID"].ToString());
                }
            }
            else if (action == "DeleteDrawing")
            {
                if (Request["DwgId"] != null && Request["UserTypeID"] != null)
                {
                    DeleteDrawingbyDrawingID(Request["DwgId"].ToString(), Request["UserTypeID"].ToString());
                }
            }
        }
    }

    private void ApprovedDrawing(string dwgID ,string userTypeID)
    {
        JObject jsonObject = new JObject();
        if (userTypeID == "1")
        {
            DAL.DalAccessUtility.ExecuteNonQuery("update drawing set IsApproved=1 where DwgId=" + dwgID);
            jsonObject["status"] = 1;
        }
        else
        {
            jsonObject["status"] = 0;
        }
        string json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
        Response.ContentType = "application/json";
        Response.Write(json);
    }

    private void DeleteDrawingbyDrawingID(string dwgID, string userTypeID)
    {
        JObject jsonObject = new JObject();
        if (userTypeID == "1")
        {
            DAL.DalAccessUtility.GetDataInDataSet("Delete From Drawing where DwgID=" + dwgID);
            jsonObject["status"] = 1;
        }
        else
        {
            jsonObject["status"] = 0;
        }
        string json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
        Response.ContentType = "application/json";
        Response.Write(json);
    }

    public void GetAcademyByUserName(string userID)
    {
        DataTable dsAca = new DataTable();
        dsAca = DAL.DalAccessUtility.GetDataInDataSet("select A.AcaId,A.AcaName from Academy A INNER JOIN AcademyAssignToEmployee AAE ON A.AcaID = AAE.AcaId where A.Active=1 and AAE.EmpID='" + userID + "'  Order by AcaName").Tables[0];
        string json = JsonConvert.SerializeObject(dsAca, Formatting.Indented);
        Response.ContentType = "application/json";
        Response.Write(json);
    }
    public void GetWorkAllotNameByAcaID(string acaID)
    {
        List<WorkAllot> WorkAllotView = new List<WorkAllot>();
        PurchaseControler controler = new PurchaseControler();
        WorkAllotView = controler.GetWorkAllotByAcademyID(int.Parse(acaID));
        string json = JsonConvert.SerializeObject(WorkAllotView, Formatting.Indented);
        Response.ContentType = "application/json";
        Response.Write(json);
    }
    public void GetLoginDetail(string user, string password)
    {
        UsersRepository repo = new UsersRepository(new AkalAcademy.DataContext());
        Incharge inchrge = new Incharge();
        inchrge = repo.GetLoginUserDetail(user.Trim(), password.Trim());
        if (inchrge == null)
        {
            JObject jsonObject = new JObject();
            jsonObject["error"] = 1;
            string json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
            Response.ContentType = "application/json";
            Response.Write(json);
        }
        else
        {
            string json = JsonConvert.SerializeObject(inchrge, Formatting.Indented);
            Response.ContentType = "application/json";
            Response.Write(json);
        }

    }

    public void GetAllDrawingTypeName()
    {
        DataTable dsDrawing = new DataTable();
        dsDrawing = DAL.DalAccessUtility.GetDataInDataSet("SELECT DwTypeId,DwTypeName FROM DrawingType where Active=1").Tables[0];
        string json = JsonConvert.SerializeObject(dsDrawing, Formatting.Indented);
        Response.ContentType = "application/json";
        Response.Write(json);
    }

    public void GetApprovedDrawingDetail(string DrawingTypeID, string AcaID,string UserID, string UserTypeID)
    {
        DataTable dtDrawingDetails = new DataTable();
        if (UserTypeID == "1")
        {
            dtDrawingDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ApprovedDrwaingShowForAdminByAcaIDANDDrawingType'" + Convert.ToInt32(AcaID) + "','" + true + "'," + Convert.ToInt32(DrawingTypeID)).Tables[0];
            if (dtDrawingDetails.Rows.Count == 0)
            {
                JObject jsonObject = new JObject();
                jsonObject["error"] = 1;
                string json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                Response.ContentType = "application/json";
                Response.Write(json);
            }
            else
            {
                string json = JsonConvert.SerializeObject(dtDrawingDetails, Formatting.Indented);
                Response.ContentType = "application/json";
                Response.Write(json);
            }
        }
        else if (UserTypeID == "2")
        {
            dtDrawingDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_DrwaingShowForConstructionByAcaID'" + Convert.ToInt32(AcaID) + "','" + true + "','" + Convert.ToInt32(UserID) + "','" + Convert.ToInt32(DrawingTypeID)+"'").Tables[0];
            if (dtDrawingDetails.Rows.Count == 0)
            {
                JObject jsonObject = new JObject();
                jsonObject["error"] = 1;
                string json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                Response.ContentType = "application/json";
                Response.Write(json);
            }
            else
            {
                string json = JsonConvert.SerializeObject(dtDrawingDetails, Formatting.Indented);
                Response.ContentType = "application/json";
                Response.Write(json);
            }
        }
        else
        {
            dtDrawingDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_DrwaingShowForArchByAcaID'" + Convert.ToInt32(AcaID) + "','" + true + "','" + Convert.ToInt32(UserID) + "'," + Convert.ToInt32(DrawingTypeID)).Tables[0];
            if (dtDrawingDetails.Rows.Count == 0)
            {
                JObject jsonObject = new JObject();
                jsonObject["error"] = 1;
                string json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                Response.ContentType = "application/json";
                Response.Write(json);
            }
            else
            {
                string json = JsonConvert.SerializeObject(dtDrawingDetails, Formatting.Indented);
                Response.ContentType = "application/json";
                Response.Write(json);
            }
        
        }

    }

    public void GetNonApprovedDrawingDetail(string UserID,string UserTypeID)
    {
        DataTable dtNonApprovedDetails = new DataTable();
        if (UserTypeID == "1")
        {
            dtNonApprovedDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_DrwaingShowForAdminInAnroid'" + false + "'").Tables[0];
            if (dtNonApprovedDetails.Rows.Count == 0)
            {
                JObject jsonObject = new JObject();
                jsonObject["error"] = 1;
                string json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                Response.ContentType = "application/json";
                Response.Write(json);
            }
            else
            {
                string json = JsonConvert.SerializeObject(dtNonApprovedDetails, Formatting.Indented);
                Response.ContentType = "application/json";
                Response.Write(json);
            }
        }
        else if (UserTypeID == "7")
        {
            dtNonApprovedDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_DrwaingShowForArchByID'" + false + "','" + UserID + "'").Tables[0];
            if (dtNonApprovedDetails.Rows.Count == 0)
            {
                JObject jsonObject = new JObject();
                jsonObject["error"] = 1;
                string json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                Response.ContentType = "application/json";
                Response.Write(json);
            }
            else
            {
                string json = JsonConvert.SerializeObject(dtNonApprovedDetails, Formatting.Indented);
                Response.ContentType = "application/json";
                Response.Write(json);
            }
        }
        else {
            JObject jsonObject = new JObject();
            jsonObject["error"] = 1;
            string json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
            Response.ContentType = "application/json";
            Response.Write(json);
        }
    }
}