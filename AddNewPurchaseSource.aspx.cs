using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class AddNewPurchaseSource : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblUser.Text = Session["EmailId"].ToString();
            }
            if (Request.QueryString["PSIdEdit"] != null)
            {
                //GetPurchaseSourceInfoToUpdate(Request.QueryString["PSIdEdit"].ToString());
                getPSDetails(Request.QueryString["PSIdEdit"].ToString());
                btnEdit.Visible = true;
                btnSave.Visible = false;
            }
            if (Request.QueryString["PSIdDel"] != null)
            {
                DeactivePSName(Request.QueryString["PSIdDel"].ToString());
            }
            BindPSDetails();
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct PSName from purchasesource where PSName='" + txtPSName.Text + "'");
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Purchase Source Name Already Exist.');", true);
        }
        else if (txtPSName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter The Purchase Source Name');", true);
        }
        else
        {
            PurchaseSource purchasesource = new PurchaseSource();
            purchasesource.PSName = txtPSName.Text;
            purchasesource.CreatedOn = DateTime.Now;
            purchasesource.CreatedBy = "ADMIN";
            //purchasesource.ModifyBy = "Admin";
            //purchasesource.AssignToEmp = 
            purchasesource.ModifyOn = DateTime.Now;
            purchasesource.Active = 1;
            AdminRepository repo = new AdminRepository(new AkalAcademy.DataContext());
            if (purchasesource.PSId == 0)
            {
                repo.AddNewPurchaseSource(purchasesource);
            }
            //else {
            //    repo.UpdatePurchaseSource(purchasesource);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Purchase Source Name Create Successfully.');", true);
            BindPSDetails();
            txtPSName.Text = "";
        }
    }

    protected void BindPSDetails()
    {
        DataSet dsPSDetails = new DataSet();
        dsPSDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowPurchaseDetails_ByUser '" + lblUser.Text + "'");
        divPSNameDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='50%'>Purchase Source Name</th>";
        ZoneInfo += "<th width='50%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsPSDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='50%'>" + dsPSDetails.Tables[0].Rows[i]["PSName"].ToString() + "</td>";

            ZoneInfo += "<td class='center' width='50%'>";

            ZoneInfo += "<a class='btn btn-info' href='AddNewPurchaseSource.aspx?PSIdEdit=" + dsPSDetails.Tables[0].Rows[i]["PSId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i> Edit";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-danger' href='AddNewPurchaseSource.aspx?PSIdDel=" + dsPSDetails.Tables[0].Rows[i]["PSId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-trash icon-white'></i> Delete";
            ZoneInfo += "</a>";

            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";

        divPSNameDetails.InnerHtml = ZoneInfo.ToString();
    }

    protected void DeactivePSName(string psid)
    {
        AdminController adminController = new AdminController();
        adminController.DeletePSName(Convert.ToInt32(psid)); 
        BindPSDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Purchase Source Name  Delete Successfully.');", true);
    }

    private void getPSDetails(string psid)
    {
        DataSet dsGetPSDetail = new DataSet();
        dsGetPSDetail = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_ShowPurchaseDetailDetails_ByID] '" + psid + "'");
        if (dsGetPSDetail.Tables[0].Rows.Count > 0)
        {
            txtPSName.Text = dsGetPSDetail.Tables[0].Rows[0]["PSName"].ToString();
        }

        BindPSDetails();
    }

    protected void btnCl_Click(object sender, EventArgs e)
    {
        txtPSName.Text = "";
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        string PSNameId = Request.QueryString["PSIdEdit"];
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct PSName from purchasesource where PSName='" + txtPSName.Text + "'");
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Purchase Source Name Already Exist.');", true);
        }
        else if (txtPSName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter The Purchase Source Name');", true);
        }
        else
        {
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewPurchaseSourceProc '" + txtPSName.Text + "','" + lblUser.Text + "','2','" + PSNameId + "','1'");
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Purchase Source Name Edit Successfully.');", true);
            BindPSDetails();
            txtPSName.Text = "";
            btnEdit.Visible = false;
            btnSave.Visible = true;
        }
    }

   
}