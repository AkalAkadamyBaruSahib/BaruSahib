using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Admin_FAQ : System.Web.UI.Page
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
            BindFAQs();
            if (Request.QueryString["FqaId"] != null)
            {
                getFaqDetails(Request.QueryString["FqaId"].ToString());
                btnEdit.Visible = true;
                btnSave.Visible = false;
               

            }
            if (Request.QueryString["FqaIdIA"] != null)
            {
                DeactiveFAQ(Request.QueryString["FqaIdIA"].ToString());
            }
            if (Request.QueryString["FqaIdA"] != null)
            {
                ActiveFAQ(Request.QueryString["FqaIdA"].ToString());
            }
        }
    }
    protected void DeactiveFAQ(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewFAQ '" + ID + "','','','0','" + lblUser.Text + "','4'");
        BindFAQs();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Question deactive successfully.');", true);

    }
    protected void ActiveFAQ(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewFAQ '" + ID + "','','','1','" + lblUser.Text + "','4'");
        BindFAQs();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Question active successfully.');", true);
        
    }
    private void getFaqDetails(string ID)
    {
        DataSet dsCouDetails = new DataSet();
        dsCouDetails = DAL.DalAccessUtility.GetDataInDataSet("select FqaId,Question,Answer from FAQ where FqaId='" + ID + "' and Active=1");
        if (dsCouDetails.Tables[0].Rows.Count > 0)
        {
            txtAns.Text = dsCouDetails.Tables[0].Rows[0]["Answer"].ToString();
            txtQues.Text = dsCouDetails.Tables[0].Rows[0]["Question"].ToString();

        }
        BindFAQs();
    }
    protected void BindFAQs()
    {
        DataSet dsZoneDetails = new DataSet();
        dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet("select FqaId,Question,Answer,Active from FAQ where CreatedBy='" + lblUser.Text +"'");
        divZoneDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='70%'>FAQs</th>";
        ZoneInfo += "<th width='30%'>Edit</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsZoneDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='70%'>";
            ZoneInfo += "<div class='row-fluid'>";
			ZoneInfo += "<div class='span12'>";
            ZoneInfo += "<h3>Q: " + dsZoneDetails.Tables[0].Rows[i]["Question"].ToString() + "";
            if (dsZoneDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "(<b><span style='color:green;'>Active</span></b>)";
            }
            else
            {
                ZoneInfo += "(<span style='color:red;'>Inactive</span>)";
            }
            ZoneInfo += "</h3>";
			ZoneInfo += "<div class='row-fluid'>";
			ZoneInfo += "<div class='span6'>";
			ZoneInfo += "<blockquote>";
            ZoneInfo += "<p>A: " + dsZoneDetails.Tables[0].Rows[i]["Answer"].ToString() + "</p>";
			ZoneInfo += "</blockquote>";
			ZoneInfo += "</div>";
			ZoneInfo += "</div>";
			ZoneInfo += "</div>";
			ZoneInfo += "</div>";
            ZoneInfo += "Active";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='30%'>";
            ZoneInfo += "<a class='btn btn-success' href='Admin_FAQ.aspx?FqaIdA=" + dsZoneDetails.Tables[0].Rows[i]["FqaId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-zoom-in icon-white'></i> Active";
            ZoneInfo += "</a>";
            ZoneInfo += "<a class='btn btn-info' href='Admin_FAQ.aspx?FqaId=" + dsZoneDetails.Tables[0].Rows[i]["FqaId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i> Edit ";
            ZoneInfo += "</a>";
            ZoneInfo += "<a class='btn btn-danger' href='Admin_FAQ.aspx?FqaIdIA=" + dsZoneDetails.Tables[0].Rows[i]["FqaId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-trash icon-white'></i> Inactive";
            ZoneInfo += "</a>";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";

        divZoneDetails.InnerHtml = ZoneInfo.ToString();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        if (txtAns.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Answer.');", true);
        }
        else if (txtQues.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Question.');", true);
        }
        else
        {
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewFAQ '','" + txtQues.Text + "','" + txtAns.Text + "',1,'"+ lblUser.Text +"',1");
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('FAQ create successfully.');", true);
        }
        txtQues.Text = "";
        txtAns.Text = "";
        BindFAQs();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        string Fid = Request.QueryString["FqaId"];
        if (txtAns.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Answer.');", true);
        }
        else if (txtQues.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Question.');", true);
        }
        else
        {
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewFAQ '"+ Fid +"','" + txtQues.Text + "','" + txtAns.Text + "',1,'" + lblUser.Text + "',2");
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('FAQ create successfully.');", true);
        }
        txtQues.Text = "";
        txtAns.Text = "";
        BindFAQs();
    }
    protected void btnCl_Click(object sender, EventArgs e)
    {
        txtQues.Text = "";
        txtAns.Text = "";
    }
}