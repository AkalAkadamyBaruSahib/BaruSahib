using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_BillStatus : System.Web.UI.Page
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

            if (Session["billid"] != null)
            {
                getBillID(Session["billid"].ToString());
            }

            //else
            //{
            //    string a;
            //    a = Request.QueryString["AcaId"].ToString();
            //    Session["AcaId1"] = a.ToString();
            //}


            getBillDetails();
        }
    }
    private void getBillID(string id)
    {
        DataSet dsAgency = new DataSet();
        dsAgency = DAL.DalAccessUtility.GetDataInDataSet("select AgencyName from SubmitBillByUser where SubBillId='" + id + "'");
        if (dsAgency.Tables[0].Rows.Count > 0)
        {
            lblAgency.Text = dsAgency.Tables[0].Rows[0]["AgencyName"].ToString();

        }
        getBillDetails();
    }
    private void getBillDetails()
    {
        DataSet dsBillDetails = new DataSet();
        dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillVarificationDEtailsForAdminView ");
        divAcademyDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Material List</h2>";
        ZoneInfo += "<div class='box-icon'>";
        //ZoneInfo += "<a href='#' class='btn btn-setting btn-round'><i class='icon-cog'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        ZoneInfo += "<div class='box-content'>";
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='10%'>Academy</th>";
        ZoneInfo += "<th width='10%'>Bill No.</th>";
        ZoneInfo += "<th width='13%'>Agency Name</th>";
        ZoneInfo += "<th width='13%'>Amount</th>";
        ZoneInfo += "<th width='13%'>H/Q Activity</th>";
        ZoneInfo += "<th width='13%'>Audit Activity</th>";
        ZoneInfo += "<th width='13%'>Account Activity</th>";
        ZoneInfo += "<th width='13%'>Reciving</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsBillDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='10%'>" + dsBillDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='10%' align='center'>";
            ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</span></a>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["AgencyName"].ToString() + "</td>";
            ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["TotalAmount"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='13%' align='center'>";
            if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "1")
            {
                //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Verified</span></a>  ";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "")
            {
                ZoneInfo += "<a  href='#'><span class='label label-important' style='font-size: 15.998px;'>Pending</span></a>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='13%' align='center'>";
            if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "1")
            {
                //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Verified</span></a>  ";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "")
            {
                ZoneInfo += "<a  href='#' style='font-size: 15.998px;'><span class='label label-important' >Pending</span></a>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='13%' align='center'>";
            if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
            {
                //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " &#x0aPayment Mode: " + dsBillDetails.Tables[0].Rows[i]["PayModeName"].ToString() + "'>Verified</span></a>  ";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "")
            {
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='13%' align='center'>";
            if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "1")
            {
                //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Received to Agency</span></a>  ";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1" && dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "")
            {
                ZoneInfo += "<a class='btn btn-setting btn-round' href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;' >Recive to Angency</span></a>";
                Session["billid"] = dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString();
                //ZoneInfo += "<asp:LinkButton runat='server' CssClass='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        divAcademyDetails.InnerHtml = ZoneInfo.ToString();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        string BillId = Session["billid"].ToString();


        if (txtDateOfRec.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Date of Receiving.');", true);
        }
        if (txtRecipTNo.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Recipt No .');", true);
        }

        else
        {
            DAL.DalAccessUtility.ExecuteNonQuery("update SubmitBillByUser set ReciptNoByEmp='" + txtRecipTNo.Text + "',DateOfReceving='" + txtDateOfRec.Text + "',RecevingRemark='" + txtRemark.Text + "',RecevingStatus='1',RecevingBy='" + lblUser.Text + "' where SubBillId='" + BillId + "'");
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('City create successfully.');", true);
            getBillDetails();
            Clr();
        }

    }
    protected void Clr()
    {
        txtRecipTNo.Text = "";
        txtRemark.Text = "";
        txtDateOfRec.Text = "";
    }
}