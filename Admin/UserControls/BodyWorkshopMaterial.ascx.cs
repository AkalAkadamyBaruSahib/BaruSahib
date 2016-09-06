using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UserControls_BodyWorkshopMaterial : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblUser.Text = Session["InchargeID"].ToString();
                hdnInchargeID.Value = Session["InchargeID"].ToString();
                hdnUserType.Value = Session["UserTypeID"].ToString();
            }

            BindWorkshop();
        }
    }

    public void BindWorkshop()
    {
        int UserID = Convert.ToInt32(Session["InchargeID"].ToString());
        int UserType = Convert.ToInt32(Session["UserTypeID"].ToString());
        DataSet dsWorkshop = new DataSet();
        dsWorkshop = DAL.DalAccessUtility.GetDataInDataSet("Select A.AcaId,A.AcaName from Academy A INNER JOIN AcademyAssignToEmployee AAE on A.AcaId=AAE.AcaId Where  AAE.EmpId='" + UserID + "'");
        if (UserType == (int)(TypeEnum.UserType.WORKSHOPADMIN))
        {
            ddlworkshop.DataSource = dsWorkshop;
            ddlworkshop.DataValueField = "AcaId";
            ddlworkshop.DataTextField = "AcaName";
            ddlworkshop.DataBind();
            ddlworkshop.Items.Insert(0, new System.Web.UI.WebControls.ListItem("ALL Workshop Material", "0"));
        }
        else
        {
            divDrpWork.Visible = false;
            hdnAcaID.Value = dsWorkshop.Tables[0].Rows[0]["AcaId"].ToString();
        }
    }
}