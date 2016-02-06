using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Purchase_BillStatus : System.Web.UI.Page
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
            DataSet dsJan = new DataSet();
            dsJan = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillCountMonthWise '1'");
            lblJanBillcount.Text = dsJan.Tables[0].Rows[0]["billCount"].ToString();
            DataSet dsfeb = new DataSet();
            dsfeb = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillCountMonthWise '2'");
            lblFeBillCount.Text = dsfeb.Tables[0].Rows[0]["billCount"].ToString();
            DataSet dsmar = new DataSet();
            dsmar = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillCountMonthWise '3'");
            lblMarchBillCount.Text = dsmar.Tables[0].Rows[0]["billCount"].ToString();
            DataSet dsapr = new DataSet();
            dsapr = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillCountMonthWise '4'");
            lblAprBillCount.Text = dsapr.Tables[0].Rows[0]["billCount"].ToString();
            DataSet dsMay = new DataSet();
            dsMay = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillCountMonthWise '5'");
            lblMayBillCount.Text = dsMay.Tables[0].Rows[0]["billCount"].ToString();
            DataSet dsJune = new DataSet();
            dsJune = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillCountMonthWise '6'");
            lblJuneBillCount.Text = dsJune.Tables[0].Rows[0]["billCount"].ToString();
            DataSet dsJuly = new DataSet();
            dsJuly = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillCountMonthWise '7'");
            lblJulyBillCount.Text = dsJuly.Tables[0].Rows[0]["billCount"].ToString();
            DataSet dsAug = new DataSet();
            dsAug = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillCountMonthWise '8'");
            lbAugBillCount.Text = dsAug.Tables[0].Rows[0]["billCount"].ToString();
            DataSet dsSep = new DataSet();
            dsSep = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillCountMonthWise '9'");
            lblSepBillCount.Text = dsSep.Tables[0].Rows[0]["billCount"].ToString();
            DataSet dsOct = new DataSet();
            dsOct = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillCountMonthWise '10'");
            lblOctBillCount.Text = dsOct.Tables[0].Rows[0]["billCount"].ToString();
            DataSet dsnov = new DataSet();
            dsnov = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillCountMonthWise '11'");
            lblNovBillCount.Text = dsnov.Tables[0].Rows[0]["billCount"].ToString();
            DataSet dsDEc = new DataSet();
            dsDEc = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillCountMonthWise '12'");
            lblDecBillCount.Text = dsDEc.Tables[0].Rows[0]["billCount"].ToString();
        }
    }
    private void getBillDetails()
    {
        pnlBillMonthDetails.Visible = true;
        DataSet dsBillDetails = new DataSet();
        dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillVarificationDEtailsByZoneWiseForPurchase ");
        divAcademyDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Current Month and Year Bill List</h2>";
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
        ZoneInfo += "<th style='display:none;'></th>";
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
            ZoneInfo += "<td style='display:none;'>1</td>";
            ZoneInfo += "<td width='10%'>" + dsBillDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='10%' align='center'>";
            ZoneInfo += "<a  href='Purchase_BillDetails.aspx?BillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</span></a>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["AgencyName"].ToString() + "</td>";
            ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["TotalAmount"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='13%' align='center'>";
            if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "1")
            {
                //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Verified</span></a>  ";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "0")
            {
                //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Rejected</span></a>  ";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "")
            {
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='13%' align='center'>";
            if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "1")
            {
                //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Verified</span></a>  ";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "0")
            {
                //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Rejected</span></a>  ";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "")
            {
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
            }
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='13%' align='center'>";
            if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
            {
                //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Verified</span></a>  ";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "0")
            {
                //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Rejected</span></a>  ";
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
            else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "0")
            {
                //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Rejected</span></a>  ";
            }
            else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1" )
            {
                ZoneInfo += "<a class='btn btn-setting btn-round' href='Purchase_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;' >Recive to Angency</span></a>";
                Session["billid"] = dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString();
                //ZoneInfo += "<asp:LinkButton runat='server' CssClass='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>";
            }
            else if ( dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "")
            {
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
               
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
    protected void lbJanBill_Click(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Year.');", true);
        }
        else
        {
            pnlBillMonthDetails.Visible = true;
            DataSet dsBillDetails = new DataSet();
            dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillStatusForMonthYearWise4Admin '" + ddlYear.SelectedItem.Text + "','1' ");
            divAcademyDetails.InnerHtml = string.Empty;
            string ZoneInfo = string.Empty;
            ZoneInfo += "<div class='box span12'>";
            ZoneInfo += "<div class='box-header well' data-original-title>";
            ZoneInfo += "<h2><i class='icon-user'></i> January Bill List</h2>";
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
            //ZoneInfo += "<th width='10%'>Zone</th>";
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
                ZoneInfo += "<a  href='Purchase_BillDetails.aspx?BillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</span></a>";
                ZoneInfo += "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["AgencyName"].ToString() + "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["TotalAmount"].ToString() + "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Rejected</span></a>  ";
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
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    ZoneInfo += "<a class='btn btn-setting btn-round' href='Purchase_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;' >Recive to Angency</span></a>";
                    Session["billid"] = dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString();
                    //ZoneInfo += "<asp:LinkButton runat='server' CssClass='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";

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
    }
    protected void lbNovBill_Click(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Year.');", true);
        }
        else
        {
            pnlBillMonthDetails.Visible = true;
            DataSet dsBillDetails = new DataSet();
            dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillStatusForMonthYearWise4Admin '" + ddlYear.SelectedItem.Text + "','11' ");
            divAcademyDetails.InnerHtml = string.Empty;
            string ZoneInfo = string.Empty;
            ZoneInfo += "<div class='box span12'>";
            ZoneInfo += "<div class='box-header well' data-original-title>";
            ZoneInfo += "<h2><i class='icon-user'></i> November Bill List</h2>";
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
            //ZoneInfo += "<th width='10%'>Zone</th>";
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
                ZoneInfo += "<a  href='Purchase_BillDetails.aspx?BillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</span></a>";
                ZoneInfo += "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["AgencyName"].ToString() + "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["TotalAmount"].ToString() + "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Rejected</span></a>  ";
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
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    ZoneInfo += "<a class='btn btn-setting btn-round' href='Purchase_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;' >Recive to Angency</span></a>";
                    Session["billid"] = dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString();
                    //ZoneInfo += "<asp:LinkButton runat='server' CssClass='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";

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
    }
    protected void lbMarchBill_Click(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Year.');", true);
        }
        else
        {
            pnlBillMonthDetails.Visible = true;
            DataSet dsBillDetails = new DataSet();
            dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillStatusForMonthYearWise4Admin '" + ddlYear.SelectedItem.Text + "','3' ");
            divAcademyDetails.InnerHtml = string.Empty;
            string ZoneInfo = string.Empty;
            ZoneInfo += "<div class='box span12'>";
            ZoneInfo += "<div class='box-header well' data-original-title>";
            ZoneInfo += "<h2><i class='icon-user'></i> March Bill List</h2>";
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
           // ZoneInfo += "<th width='10%'>Zone</th>";
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
                ZoneInfo += "<a  href='Purchase_BillDetails.aspx?BillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</span></a>";
                ZoneInfo += "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["AgencyName"].ToString() + "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["TotalAmount"].ToString() + "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Rejected</span></a>  ";
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
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    ZoneInfo += "<a class='btn btn-setting btn-round' href='Purchase_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;' >Recive to Angency</span></a>";
                    Session["billid"] = dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString();
                    //ZoneInfo += "<asp:LinkButton runat='server' CssClass='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";

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
    }
    protected void lbAprBill_Click(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Year.');", true);
        }
        else
        {
            pnlBillMonthDetails.Visible = true;
            DataSet dsBillDetails = new DataSet();
            dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillStatusForMonthYearWise4Admin '" + ddlYear.SelectedItem.Text + "','4' ");
            divAcademyDetails.InnerHtml = string.Empty;
            string ZoneInfo = string.Empty;
            ZoneInfo += "<div class='box span12'>";
            ZoneInfo += "<div class='box-header well' data-original-title>";
            ZoneInfo += "<h2><i class='icon-user'></i> April Bill List</h2>";
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
           // ZoneInfo += "<th width='10%'>Zone</th>";
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
                ZoneInfo += "<a  href='Purchase_BillDetails.aspx?BillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</span></a>";
                ZoneInfo += "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["AgencyName"].ToString() + "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["TotalAmount"].ToString() + "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Rejected</span></a>  ";
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
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    ZoneInfo += "<a class='btn btn-setting btn-round' href='Purchase_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;' >Recive to Angency</span></a>";
                    Session["billid"] = dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString();
                    //ZoneInfo += "<asp:LinkButton runat='server' CssClass='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";

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
    }
    protected void lbMayBill_Click(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Year.');", true);
        }
        else
        {
            pnlBillMonthDetails.Visible = true;
            DataSet dsBillDetails = new DataSet();
            dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillStatusForMonthYearWise4Admin '" + ddlYear.SelectedItem.Text + "',5 ");
            divAcademyDetails.InnerHtml = string.Empty;
            string ZoneInfo = string.Empty;
            ZoneInfo += "<div class='box span12'>";
            ZoneInfo += "<div class='box-header well' data-original-title>";
            ZoneInfo += "<h2><i class='icon-user'></i> May Bill List</h2>";
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
           // ZoneInfo += "<th width='10%'>Zone</th>";
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
                ZoneInfo += "<a  href='Purchase_BillDetails.aspx?BillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</span></a>";
                ZoneInfo += "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["AgencyName"].ToString() + "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["TotalAmount"].ToString() + "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Rejected</span></a>  ";
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
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    ZoneInfo += "<a class='btn btn-setting btn-round' href='Purchase_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;' >Recive to Angency</span></a>";
                    Session["billid"] = dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString();
                    //ZoneInfo += "<asp:LinkButton runat='server' CssClass='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";

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
    }
    protected void lbJuneBill_Click(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Year.');", true);
        }
        else
        {
            pnlBillMonthDetails.Visible = true;
            DataSet dsBillDetails = new DataSet();
            dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillStatusForMonthYearWise4Admin '" + ddlYear.SelectedItem.Text + "','6' ");
            divAcademyDetails.InnerHtml = string.Empty;
            string ZoneInfo = string.Empty;
            ZoneInfo += "<div class='box span12'>";
            ZoneInfo += "<div class='box-header well' data-original-title>";
            ZoneInfo += "<h2><i class='icon-user'></i> June Bill List</h2>";
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
            //ZoneInfo += "<th width='10%'>Zone</th>";
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
                ZoneInfo += "<a  href='Purchase_BillDetails.aspx?BillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</span></a>";
                ZoneInfo += "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["AgencyName"].ToString() + "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["TotalAmount"].ToString() + "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Rejected</span></a>  ";
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
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    ZoneInfo += "<a class='btn btn-setting btn-round' href='Purchase_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;' >Recive to Angency</span></a>";
                    Session["billid"] = dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString();
                    //ZoneInfo += "<asp:LinkButton runat='server' CssClass='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";

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
    }
    protected void lbJulyBill_Click(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Year.');", true);
        }
        else
        {
            pnlBillMonthDetails.Visible = true;
            DataSet dsBillDetails = new DataSet();
            dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillStatusForMonthYearWise4Admin '" + ddlYear.SelectedItem.Text + "','7' ");
            divAcademyDetails.InnerHtml = string.Empty;
            string ZoneInfo = string.Empty;
            ZoneInfo += "<div class='box span12'>";
            ZoneInfo += "<div class='box-header well' data-original-title>";
            ZoneInfo += "<h2><i class='icon-user'></i> July Bill List</h2>";
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
            //ZoneInfo += "<th width='10%'>Zone</th>";
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
                ZoneInfo += "<a  href='Purchase_BillDetails.aspx?BillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</span></a>";
                ZoneInfo += "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["AgencyName"].ToString() + "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["TotalAmount"].ToString() + "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Rejected</span></a>  ";
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
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    ZoneInfo += "<a class='btn btn-setting btn-round' href='Purchase_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;' >Recive to Angency</span></a>";
                    Session["billid"] = dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString();
                    //ZoneInfo += "<asp:LinkButton runat='server' CssClass='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";

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
    }
    protected void lbAugBill_Click(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Year.');", true);
        }
        else
        {
            pnlBillMonthDetails.Visible = true;
            DataSet dsBillDetails = new DataSet();
            dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillStatusForMonthYearWise4Admin '" + ddlYear.SelectedItem.Text + "','8' ");
            divAcademyDetails.InnerHtml = string.Empty;
            string ZoneInfo = string.Empty;
            ZoneInfo += "<div class='box span12'>";
            ZoneInfo += "<div class='box-header well' data-original-title>";
            ZoneInfo += "<h2><i class='icon-user'></i> August Bill List</h2>";
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
           // ZoneInfo += "<th width='10%'>Zone</th>";
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
                ZoneInfo += "<a  href='Purchase_BillDetails.aspx?BillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</span></a>";
                ZoneInfo += "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["AgencyName"].ToString() + "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["TotalAmount"].ToString() + "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Rejected</span></a>  ";
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
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    ZoneInfo += "<a class='btn btn-setting btn-round' href='Purchase_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;' >Recive to Angency</span></a>";
                    Session["billid"] = dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString();
                    //ZoneInfo += "<asp:LinkButton runat='server' CssClass='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";

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
    }
    protected void LBsPEbILL_Click(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Year.');", true);
        }
        else
        {
            pnlBillMonthDetails.Visible = true;
            DataSet dsBillDetails = new DataSet();
            dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillStatusForMonthYearWise4Admin '" + ddlYear.SelectedItem.Text + "','9' ");
            divAcademyDetails.InnerHtml = string.Empty;
            string ZoneInfo = string.Empty;
            ZoneInfo += "<div class='box span12'>";
            ZoneInfo += "<div class='box-header well' data-original-title>";
            ZoneInfo += "<h2><i class='icon-user'></i> September Bill List</h2>";
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
           // ZoneInfo += "<th width='10%'>Zone</th>";
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
                ZoneInfo += "<a  href='Purchase_BillDetails.aspx?BillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</span></a>";
                ZoneInfo += "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["AgencyName"].ToString() + "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["TotalAmount"].ToString() + "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Rejected</span></a>  ";
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
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    ZoneInfo += "<a class='btn btn-setting btn-round' href='Purchase_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;' >Recive to Angency</span></a>";
                    Session["billid"] = dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString();
                    //ZoneInfo += "<asp:LinkButton runat='server' CssClass='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";

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
    }
    protected void lbOctBill_Click(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Year.');", true);
        }
        else
        {
            pnlBillMonthDetails.Visible = true;
            DataSet dsBillDetails = new DataSet();
            dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillStatusForMonthYearWise4Admin '" + ddlYear.SelectedItem.Text + "','10' ");
            divAcademyDetails.InnerHtml = string.Empty;
            string ZoneInfo = string.Empty;
            ZoneInfo += "<div class='box span12'>";
            ZoneInfo += "<div class='box-header well' data-original-title>";
            ZoneInfo += "<h2><i class='icon-user'></i> October Bill List</h2>";
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
            //ZoneInfo += "<th width='10%'>Zone</th>";
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
                ZoneInfo += "<a  href='Purchase_BillDetails.aspx?BillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</span></a>";
                ZoneInfo += "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["AgencyName"].ToString() + "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["TotalAmount"].ToString() + "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Rejected</span></a>  ";
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
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    ZoneInfo += "<a class='btn btn-setting btn-round' href='Purchase_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;' >Recive to Angency</span></a>";
                    Session["billid"] = dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString();
                    //ZoneInfo += "<asp:LinkButton runat='server' CssClass='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";

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
    }
    protected void lbDecBill_Click(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Year.');", true);
        }
        else
        {
            pnlBillMonthDetails.Visible = true;
            DataSet dsBillDetails = new DataSet();
            dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillStatusForMonthYearWise4Admin '" + ddlYear.SelectedItem.Text + "','12' ");
            divAcademyDetails.InnerHtml = string.Empty;
            string ZoneInfo = string.Empty;
            ZoneInfo += "<div class='box span12'>";
            ZoneInfo += "<div class='box-header well' data-original-title>";
            ZoneInfo += "<h2><i class='icon-user'></i> December Bill List</h2>";
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
            //ZoneInfo += "<th width='10%'>Zone</th>";
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
                ZoneInfo += "<a  href='Purchase_BillDetails.aspx?BillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</span></a>";
                ZoneInfo += "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["AgencyName"].ToString() + "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["TotalAmount"].ToString() + "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Rejected</span></a>  ";
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
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    ZoneInfo += "<a class='btn btn-setting btn-round' href='Purchase_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;' >Recive to Angency</span></a>";
                    Session["billid"] = dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString();
                    //ZoneInfo += "<asp:LinkButton runat='server' CssClass='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";

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
    }
    protected void lbFebBill_Click(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Year.');", true);
        }
        else
        {
            pnlBillMonthDetails.Visible = true;
            DataSet dsBillDetails = new DataSet();
            dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillStatusForMonthYearWise4Admin '" + ddlYear.SelectedItem.Text + "','2' ");
            divAcademyDetails.InnerHtml = string.Empty;
            string ZoneInfo = string.Empty;
            ZoneInfo += "<div class='box span12'>";
            ZoneInfo += "<div class='box-header well' data-original-title>";
            ZoneInfo += "<h2><i class='icon-user'></i> Year and Month Bill List</h2>";
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
            //ZoneInfo += "<th width='10%'>Zone</th>";
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
                ZoneInfo += "<a  href='Purchase_BillDetails.aspx?BillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</span></a>";
                ZoneInfo += "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["AgencyName"].ToString() + "</td>";
                ZoneInfo += "<td width='13%'>" + dsBillDetails.Tables[0].Rows[i]["TotalAmount"].ToString() + "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important'  style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AuditVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["AuditRemark"].ToString() + "'>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Verified</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Rejected</span></a>  ";
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
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "0")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Red;' title='Rejected Date:" + dsBillDetails.Tables[0].Rows[i]["DateOfRecevi"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["RecevingRemark"].ToString() + " '>Rejected</span></a>  ";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    ZoneInfo += "<a class='btn btn-setting btn-round' href='Purchase_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;' >Recive to Angency</span></a>";
                    Session["billid"] = dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString();
                    //ZoneInfo += "<asp:LinkButton runat='server' CssClass='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;'>Pending</span>";

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
    }
    protected DataTable BindBillDetailsEx()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillDetails");
        dt = ds.Tables[0];
        return dt;
    }
    protected DataTable BindBillStatusEx()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillStatus");
        dt = ds.Tables[0];
        return dt;
    }
    protected void btnExeclStatus_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "BillStatus.xls"));
        Response.ContentType = "application/ms-excel";
        DataTable dt = BindBillStatusEx();
        string str = string.Empty;
        foreach (DataColumn dtcol in dt.Columns)
        {
            Response.Write(str + dtcol.ColumnName);
            str = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            str = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                Response.Write(str + Convert.ToString(dr[j]));
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }
    protected void btnExecl_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "BillDetails.xls"));
        Response.ContentType = "application/ms-excel";
        DataTable dt = BindBillDetailsEx();
        string str = string.Empty;
        foreach (DataColumn dtcol in dt.Columns)
        {
            Response.Write(str + dtcol.ColumnName);
            str = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            str = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                Response.Write(str + Convert.ToString(dr[j]));
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }
}