using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Account_RejectBills : System.Web.UI.Page
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
            if (Request.QueryString["SubBillId"] != null)
            {
                ProceedBill4Varification(Request.QueryString["SubBillId"].ToString());
            }
            pnlBillMonthDetails.Visible = true;
            DataSet dsBillDetails = new DataSet();
            dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillStatusRejectedBills4Account ");
            divAcademyDetails.InnerHtml = string.Empty;
            string ZoneInfo = string.Empty;
            ZoneInfo += "<div class='box span12'>";
            ZoneInfo += "<div class='box-header well' data-original-title>";
            ZoneInfo += "<h2><i class='icon-user'></i> Rejected Bill Status</h2>";
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
            ZoneInfo += "<th width='10%'>Zone</th>";
            ZoneInfo += "<th width='10%'>Academy</th>";
            ZoneInfo += "<th width='10%'>Bill No.</th>";
            ZoneInfo += "<th width='13%'>Agency Name</th>";
            ZoneInfo += "<th width='13%'>Amount</th>";
            ZoneInfo += "<th width='13%'>H/Q-Admin</th>";
            ZoneInfo += "<th width='13%'>Audit</th>";
            ZoneInfo += "<th width='13%'>Account</th>";
            ZoneInfo += "<th width='13%'>Reciving</th>";
            ZoneInfo += "<th width='13%'>Action</th>";
            ZoneInfo += "</tr>";
            ZoneInfo += "</thead>";
            ZoneInfo += "<tbody>";
            for (int i = 0; i < dsBillDetails.Tables[0].Rows.Count; i++)
            {
                ZoneInfo += "<tr>";
                ZoneInfo += "<td width='10%'>" + dsBillDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td>";
                ZoneInfo += "<td width='10%'>" + dsBillDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td>";
                ZoneInfo += "<td class='center' width='10%' align='center'>";
                ZoneInfo += "<a  href='Audit_ViewBillDetailsForApproval.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "</span></a>";
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
                    ZoneInfo += "<a  href='Admin_ViewBillDetailsForApproval.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Pending</span></a>";
                }
                else if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "0")
                {
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["HQVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["HQRemark"].ToString() + "'>Rejected</span></a>  ";
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
                    ZoneInfo += "<a  href='#' style='font-size: 15.998px;'><span class='label label-important' style='font-size: 15.998px;'>Pending</span></a>";
                }
                ZoneInfo += "</td>";
                ZoneInfo += "<td class='center' width='13%' align='center'>";
                if (dsBillDetails.Tables[0].Rows[i]["AccStatus"].ToString() == "1")
                {
                    //ZoneInfo += "<a  href='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>";
                    //ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " &#x0aPayment Mode: " + dsBillDetails.Tables[0].Rows[i]["PayModeName"].ToString() + "'>Verified</span></a>  ";
                    ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;background-color: Green;' title='Varified Date:" + dsBillDetails.Tables[0].Rows[i]["AccVarifyDate"].ToString() + "&#x0aRemark: " + dsBillDetails.Tables[0].Rows[i]["ThirdVarifyRemark"].ToString() + " '>Verified</span></a>  ";
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
                else if (dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "")
                {
                    ZoneInfo += "<a  href='#'><span class='label label-important' style='font-size: 15.998px;' >Pending</span></a>";
                    Session["billid"] = dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString();
                    //ZoneInfo += "<asp:LinkButton runat='server' CssClass='btn btn-setting btn-round' PostBackUrl='Emp_BillStatus.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Recive to Angency</span></asp:LinkButton>";
                }
                ZoneInfo += "</td>";
                if (dsBillDetails.Tables[0].Rows[i]["HQStatus"].ToString() == "0" || dsBillDetails.Tables[0].Rows[i]["AuditStatus"].ToString() == "0" || dsBillDetails.Tables[0].Rows[i]["RecevingStatus"].ToString() == "0")
                {
                    ZoneInfo += "<td width='13%'><a class='btn btn-warning' data-rel='tooltip' title='Proceed bill to again verify.' href='Account_RejectBills.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[i]["SubBillId"].ToString() + "'>Proceed</a></td>";
                }
                else
                {
                    ZoneInfo += "<td width='13%'>Self Rejected</td>";
                }
                ZoneInfo += "</tr>";
            }
            ZoneInfo += "</tbody>";
            ZoneInfo += "</table>";
            ZoneInfo += "</div>";
            ZoneInfo += "</div>";
            divAcademyDetails.InnerHtml = ZoneInfo.ToString();
        }
    }
    protected void ProceedBill4Varification(string id)
    {
        DataSet dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_MsgContent '" + id + "'");
        string msg = "Please varified " + dsBillDetails.Tables[0].Rows[0]["BillType"].ToString() + " bill, Bill No.:" + id + " for " + dsBillDetails.Tables[0].Rows[0]["AcaName"].ToString() + " and bill issued by Mr. " + dsBillDetails.Tables[0].Rows[0]["InName"].ToString() + " ";
        DataSet dsUserName = DAL.DalAccessUtility.GetDataInDataSet("select InName from Incharge where LoginId='" + lblUser.Text + "'");
        string msgForAgain = "Please varified again " + dsBillDetails.Tables[0].Rows[0]["BillType"].ToString() + " bill, Bill No.:" + id + " for " + dsBillDetails.Tables[0].Rows[0]["AcaName"].ToString() + " and bill issued by Mr. " + dsBillDetails.Tables[0].Rows[0]["InName"].ToString() + " Proceed by Mr. " + dsUserName.Tables[0].Rows[0]["InName"].ToString() + "";
        DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsgToAdmin '" + lblUser.Text + "','" + msg + "'");
        DataSet dsVariDetails = DAL.DalAccessUtility.GetDataInDataSet("select FirstVarify,FirstVarifyStatus,SeccondVarify,SecondVarifyStatus,ThirdVarifyBy,PaymentStatus,RecevingBy,RecevingStatus from SubmitBillByUser where SubBillId='" + id + "'");
        if (dsVariDetails.Tables[0].Rows[0]["FirstVarifyStatus"].ToString() == "0")
        {
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsg '" + lblUser.Text + "', '" + dsVariDetails.Tables[0].Rows[0]["FirstVarify"].ToString() + "','" + msgForAgain + "'");
        }
        else if (dsVariDetails.Tables[0].Rows[0]["SecondVarifyStatus"].ToString() == "0")
        {
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsg '" + lblUser.Text + "', '" + dsVariDetails.Tables[0].Rows[0]["SeccondVarify"].ToString() + "','" + msgForAgain + "'");
        }
        else if (dsVariDetails.Tables[0].Rows[0]["PaymentStatus"].ToString() == "0")
        {
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsg '" + lblUser.Text + "', '" + dsVariDetails.Tables[0].Rows[0]["ThirdVarifyBy"].ToString() + "','" + msgForAgain + "'");
        }
        else if (dsVariDetails.Tables[0].Rows[0]["RecevingStatus"].ToString() == "0")
        {
            //string msgForRec = "<a href='Emp_BillDetails.aspx?BillId=" + id + "'>Please varified again " + dsBillDetails.Tables[0].Rows[0]["BillType"].ToString() + " bill, Bill No.:" + id + " for " + dsBillDetails.Tables[0].Rows[0]["AcaName"].ToString() + " and bill issued by Mr. " + dsBillDetails.Tables[0].Rows[0]["InName"].ToString() + " Proceed by Mr. " + dsUserName.Tables[0].Rows[0]["InName"].ToString() + "";
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsg '" + lblUser.Text + "', '" + dsVariDetails.Tables[0].Rows[0]["RecevingBy"].ToString() + "','" + msgForAgain + "'");
        }
        DAL.DalAccessUtility.ExecuteNonQuery("exec USP_ProceedBill '" + id + "','1','" + lblUser.Text + "','5'");
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Proceed to Again Verification.');", true);
    }
}
