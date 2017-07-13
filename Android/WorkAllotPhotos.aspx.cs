using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class Android_WorkAllotPhotos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["UserId"] != null)
        {
            if (Request.QueryString["Action"] != null)
            {
                string action = Request.QueryString["Action"].ToString();
                if (action == "AcaName")
                {
                    GetAcademyByUserName(Request.QueryString["UserId"].ToString());
                }
                else if (action == "WorkAllotName")
                {
                    GetWorkAllotNameByAcaID(Request.QueryString["AcaId"].ToString());
                }
            }
        }
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
        WorkAllotView=controler.GetWorkAllotByAcademyID(int.Parse(acaID));
        string json = JsonConvert.SerializeObject(WorkAllotView, Formatting.Indented);
        Response.ContentType = "application/json";
        Response.Write(json);
    }

}