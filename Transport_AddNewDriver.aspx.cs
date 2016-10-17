using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Transport_AddNewDriver : System.Web.UI.Page
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
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            BindYear();
            Fillmonth();
            BindDLType();
            BindTransportType();
            BindVehicleNumber();
        }
    }

    private void BindYear()
    {
        for (int i = 0; i <= 30; i++)
        {
            ddlyear.Items.Add(i.ToString());
        }
    }

    private void Fillmonth()
    {

        for (int i = 0; i <= 12; i++)
        {
            ddlmonth.Items.Add(i.ToString());
        }
    }

    private void BindDLType()
    {
        DataTable TranspotDlType = new DataTable();
        TranspotDlType = DAL.DalAccessUtility.GetDataInDataSet("select * from dbo.DLTypes").Tables[0];
        if (TranspotDlType != null && TranspotDlType.Rows.Count > 0)
        {
            drpDlType.DataSource = TranspotDlType;
            drpDlType.DataValueField = "ID";
            drpDlType.DataTextField = "Name";
            drpDlType.DataBind();
            drpDlType.Items.Insert(0, new ListItem("--Select One--", "0"));
        }
    }

    private void BindTransportType()
    {
        DataTable TranspotType = new DataTable();
        TranspotType = DAL.DalAccessUtility.GetDataInDataSet("select * from TransportTypes").Tables[0];
        if (TranspotType != null && TranspotType.Rows.Count > 0)
        {
            drpTransportType.DataSource = TranspotType;
            drpTransportType.DataValueField = "ID";
            drpTransportType.DataTextField = "Type";
            drpTransportType.DataBind();
            drpTransportType.Items.Insert(0, new ListItem("--Select One--", "0"));
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
        if(TranspotVehicles != null && TranspotVehicles.Rows.Count > 0)
        {
        ddlVehicleNumber.DataSource = TranspotVehicles;
        ddlVehicleNumber.DataValueField = "ID";
        ddlVehicleNumber.DataTextField = "Number";
        ddlVehicleNumber.DataBind();
        ddlVehicleNumber.Items.Insert(0, new ListItem("--Select One--", "0"));
        }
    }

     public void ClearTextBox()
    {
        txtName.Text = "";
        txtFatherName.Text = "";
        txtDateOfBirth.Text = "";
        txtAddress.Text = "";
        txtDlNumber.Text = "";
        txtdlvalidity.Text = "";
        txtEmergencyContactNo.Text = "";
        txtDateOfJoin.Text = "";
        txtNameOfTheComp.Text = "";
        txtMobileNo.Text = "";
        ddlmonth.ClearSelection();
        ddlyear.ClearSelection();
        drpEmployeeType.ClearSelection();
        drpTransportType.ClearSelection();
        //=== Hide update button and show save button.
        //$("#btnSave").show();
        //$("#btnUpdate").hide();
    }
  
}