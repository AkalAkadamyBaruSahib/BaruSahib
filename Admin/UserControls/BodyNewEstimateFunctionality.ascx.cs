using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BodyNewEstimateFunctionality : System.Web.UI.UserControl
{
    public static int UserTypeID = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblUser.Text = Session["EmailId"].ToString();
                hdnInchargeID.Value = Session["InchargeID"].ToString();
                hdnIsAdmin.Value = Session["UserTypeID"].ToString();
                hdnModule.Value = Session["ModuleID"].ToString();
            }
            BindTypeOfWork();
        }
    }


    public void BindTypeOfWork()
    {
        DataTable dsZone = new DataTable();
        dsZone = DAL.DalAccessUtility.GetDataInDataSet("select TypeWorkId,TypeWorkName from TypeOfWork where Active=1").Tables[0];
        if (dsZone.Rows.Count > 0 || dsZone != null)
        {
            ddlTypeOfWork.DataSource = dsZone;
            ddlTypeOfWork.DataValueField = "TypeWorkId";
            ddlTypeOfWork.DataTextField = "TypeWorkName";
            ddlTypeOfWork.DataBind();
            ddlTypeOfWork.Items.Insert(0, new ListItem("Select Type of Work", ""));
            ddlTypeOfWork.SelectedIndex = 0;
        }
    }

}