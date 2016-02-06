using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Account_BillForPayment : System.Web.UI.Page
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

            getBillDetails();
        }
    }
    private void getBillDetails()
    {
        DataSet dsAcaDetails = new DataSet();
        dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BiilAcademyCount");
        divBillsDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Bills Detail</h2>";
        ZoneInfo += "<div class='box-icon'>";
        ZoneInfo += "<a href='#' class='btn btn-setting btn-round'><i class='icon-cog'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        ZoneInfo += "<div class='box-content'>";
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='33%'>Academy Name</th>";
        ZoneInfo += "<th width='34%'>Bills for Payment</th>";
       // ZoneInfo += "<th width='33%'></th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        ZoneInfo += "<tr>";
        if (dsAcaDetails.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsAcaDetails.Tables[0].Rows.Count; i++)
            {
                ZoneInfo += "<td width='33%'><a  href='Account_BillVerify.aspx?AcaId=" + dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'>" + dsAcaDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</a></td>";
                DataSet dsBillCount = DAL.DalAccessUtility.GetDataInDataSet("select count(AcaId)as BillCount from SubmitBillByUser where   AcaId='" + dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'  ");
                //Session["AcaId"] = dsBillCount.Tables[0].Rows[0]["BillCount"].ToString();
                ZoneInfo += "<td class='center' width='34%'> Bills(" + dsBillCount.Tables[0].Rows[0]["BillCount"].ToString() + ")</td>";
            }
        }
        else
        {
            ZoneInfo += "<td class='center' width='33%'  style='color:red;'> Biils are not Varified by Admim and Audit</td><td></td>";
        }
        
        //ZoneInfo += "<td class='center' width='34%'> Bills(" + Session["AcaId"].ToString() + ")</td>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        divBillsDetails.InnerHtml = ZoneInfo.ToString();
    }
}