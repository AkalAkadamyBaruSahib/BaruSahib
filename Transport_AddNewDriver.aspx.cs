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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            BindYear();
            Fillmonth();
            BindDLType();
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
        DataSet TranspotDlType = new DataSet();
        TranspotDlType = DAL.DalAccessUtility.GetDataInDataSet("select * from dbo.DLTypes");
        drpDlType.DataSource = TranspotDlType;
        drpDlType.DataValueField = "ID";
        drpDlType.DataTextField = "Name";
        drpDlType.DataBind();
        drpDlType.Items.Insert(0, new ListItem("--Select One--", "0"));
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
        //=== Hide update button and show save button.
        //$("#btnSave").show();
        //$("#btnUpdate").hide();
    }
  
}