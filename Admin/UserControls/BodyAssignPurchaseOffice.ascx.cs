using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Admin_UserControls_BodyAssignPurchaseOffice : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int UserTypeID = Convert.ToInt16(Session["UserTypeID"].ToString());
        if (Session["EmailId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblUser.Text = Session["EmailId"].ToString();
        }

        ViewState["chklabel"] = null;
        if (!IsPostBack)
        {
            BindDropdown();
            if (Request.QueryString["EstId"] != null)
            {
                getZoneAcaName(Request.QueryString["EstId"].ToString());
                getMaterialDetails(Request.QueryString["EstId"].ToString());
            }
        }
    }

    private void BindDropdown()
    {
        int UserTypeID = Convert.ToInt16(Session["UserTypeID"].ToString());
        DataSet dsAcaDetails = new DataSet();
        if (UserTypeID == (int)(TypeEnum.UserType.PURCHASE) || UserTypeID == (int)(TypeEnum.UserType.PURCHASECOMMITTEE))
        {
            dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("select * from incharge where usertypeid IN (12,0) and Active=1");
        }
        ddlEmployee.DataSource = dsAcaDetails.Tables[0];
        ddlEmployee.DataTextField = "InName";
        ddlEmployee.DataValueField = "InchargeID";
        ddlEmployee.DataBind();
        ddlEmployee.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Purchaser--", "-1"));
    }

    private void getMaterialDetails(string id)
    {
        int UserTypeID = Convert.ToInt16(Session["UserTypeID"].ToString());
        DataSet dsAcaDetails = new DataSet();
        if (UserTypeID == (int)(TypeEnum.UserType.PURCHASE) || UserTypeID == (int)(TypeEnum.UserType.PURCHASECOMMITTEE))
        {
            dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateMaterialViewForPurchase '" + id + "','2' ");
        }
        else
        {
            dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateMaterialViewForPurchase '" + id + "','3' ");
        }
        string Material, Qty, Unit;
        int rowindex = 0;
        foreach (GridViewRow gvrow in gvMaterailDetailForPurchase.Rows)
        {
            Material = gvrow.Cells[1].Text;
            DataSet dsMatid = new DataSet();
            dsMatid = DAL.DalAccessUtility.GetDataInDataSet("select MatId from Material where MatName='" + Material + "'");
            Unit = gvrow.Cells[2].Text;
            DataSet dsUnitid = new DataSet();
            dsUnitid = DAL.DalAccessUtility.GetDataInDataSet("select UnitId from Unit where UnitName='" + Unit + "'");
            Qty = gvrow.Cells[2].Text;
            rowindex = rowindex + 1;
        }
        gvMaterailDetailForPurchase.DataSource = dsAcaDetails;
        gvMaterailDetailForPurchase.DataBind();
    }
    private void getZoneAcaName(string id)
    {
        DataSet dsDetails = new DataSet();
        dsDetails = DAL.DalAccessUtility.GetDataInDataSet(" exec USP_ZoneAndAcademnyNameByEstId '" + id + "'");
        lblAcademy.Text = dsDetails.Tables[0].Rows[0]["AcaName"].ToString();
        lblZoneName.Text = dsDetails.Tables[0].Rows[0]["ZoneName"].ToString();
        DataSet dsZoneId = new DataSet();
        dsZoneId = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId from Zone where ZoneName='" + lblZoneName.Text + "'");
        Session["Zone"] = dsZoneId.Tables[0].Rows[0]["ZoneId"].ToString();
        lblEstId.Text = dsDetails.Tables[0].Rows[0]["EstId"].ToString();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        int isCheck = 0;
        string smsTo = string.Empty;
        foreach (GridViewRow row in gvMaterailDetailForPurchase.Rows)
        {
            CheckBox chkBok = row.FindControl("chkBok") as CheckBox;
            HiddenField hdnSno = row.FindControl("hdnSno") as HiddenField;
            TextBox txtRemark = (TextBox)row.FindControl("txtRemark");

            if (chkBok.Checked)
            {
                isCheck++;
                try
                {
                    DAL.DalAccessUtility.ExecuteNonQuery("INSERT INTO InchargeEmployee values(" + Session["InchargeID"].ToString() + "," + ddlEmployee.SelectedValue + ") ");
                    DAL.DalAccessUtility.ExecuteNonQuery("UPDATE EstimateAndMaterialOthersRelations SET PurchaseEmpID=" + ddlEmployee.SelectedValue + ", EmployeeAssignDateTime=GETDATE() where Sno=" + hdnSno.Value);
                    smsTo = ddlEmployee.SelectedValue;
                    SendSMSToAssignEmployee(smsTo);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + ex.Message + "');", true);
                }
            }

        }

        getMaterialDetails(Request.QueryString["EstId"].ToString());
        if (isCheck == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please check the checkbox against the material');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material has been assigned to purchase officer');", true);
        }


    }

    private void SendSMSToAssignEmployee(string UserID)
    {
        string smsTo = string.Empty;

        InchargeController conrtoller = new InchargeController();
        List<Incharge> incharges = conrtoller.GetUsersByUserType(12);
        if (UserID != null && UserID != "")
        {
            smsTo = incharges.Where(p => p.InchargeId == int.Parse(UserID)).Select(i => i.InMobile).FirstOrDefault();
        }

        Utility.SendSMS(smsTo, "New Material has been assigned to you for purchase. Please login to www.akalsewa.org for more details");
    }
}