using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transport_ViewDriverConductor : System.Web.UI.Page
{
    private int InchargeID = -1;
    private int UserTypeID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InchargeID"] != null)
        {
            InchargeID = int.Parse(Session["InchargeID"].ToString());
        }
        if (Session["UserTypeID"] != null)
        {
            UserTypeID = int.Parse(Session["UserTypeID"].ToString());
        }
        if (!Page.IsPostBack)
        {
            BindVehicleNumber();
        }
    }

    private void BindVehicleNumber()
    {
        DataTable TranspotVehicles = new DataTable();
        if (UserTypeID == 13)
        {
            TranspotVehicles = DAL.DalAccessUtility.GetDataInDataSet("select ID,Number from Vehicles Where IsApproved=1").Tables[0];
        }
        else
        {
            TranspotVehicles = DAL.DalAccessUtility.GetDataInDataSet("select V.ID, V.Number,AAE.EmpId,A.AcaName from Vehicles V INNER JOIN Academy A on V.AcademyID = A.AcaId INNER JOIN AcademyAssignToEmployee AAE on AAE.AcaID = A.AcaId  Where V.IsApproved=1 and AAE.EmpId='" + InchargeID + "'").Tables[0];
        }
        if (TranspotVehicles != null && TranspotVehicles.Rows.Count > 0)
        {
            ddlVehicleNumber.DataSource = TranspotVehicles;
            ddlVehicleNumber.DataValueField = "ID";
            ddlVehicleNumber.DataTextField = "Number";
            ddlVehicleNumber.DataBind();
            ddlVehicleNumber.Items.Insert(0, new ListItem("--Select One--", "0"));
        }
    }

}