using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
public partial class Admin_ViewBillDetailsForApproval : System.Web.UI.Page
{
    public static int InchargeID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tdProceedRemark.Visible = false;
            btnAgain.Visible = false;
            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblUser.Text = Session["EmailId"].ToString();
                InchargeID = Convert.ToInt32(Session["InchargeID"].ToString());
            }
            if (Request.QueryString["SubBillId"] != null)
            {
                ShowBillDetails(Request.QueryString["SubBillId"].ToString());
            }
            if (Request.QueryString["SubBillIdU"] != null)
            {
                tdProceedRemark.Visible = true;
                btnAgain.Visible = true;
                btnSave.Visible = false;
                GetUserProRemark(Request.QueryString["SubBillIdU"].ToString());
                ShowBillDetails(Request.QueryString["SubBillIdU"].ToString());
            }
        }
    }
    protected void GetUserProRemark(string id)
    {
        DataSet dsProUserRemark = DAL.DalAccessUtility.GetDataInDataSet("select UserProRemark from BillProceedLog where UserProStatus=1 and BillId='"+ id +"'");
        lblProRemark.Text = dsProUserRemark.Tables[0].Rows[0]["UserProRemark"].ToString();
    }
    protected void ShowBillDetails(string ID)
    {
        DataSet dsBill = new DataSet();
        dsBill = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AdminBillViewByBillId_V2 '"+ ID +"'");
        lblBillNo.Text = dsBill.Tables[0].Rows[0]["SubBillId"].ToString();
        Session["EstId"] = dsBill.Tables[0].Rows[0]["EstId"].ToString();
        if (dsBill.Tables[0].Rows[0]["BillType"].ToString() == ((int)TypeEnum.BillType.Sanctioned).ToString())
        {
            lblChargeableTo.Text = "Sanctioned";
        }
        else
        {
            lblChargeableTo.Text = "Non Sanctioned";
        }
        lblAgencyName.Text = dsBill.Tables[0].Rows[0]["AgencyName"].ToString();
        lblBillDate.Text = dsBill.Tables[0].Rows[0]["BillDate"].ToString();
        lblGateEntry.Text = dsBill.Tables[0].Rows[0]["GateEntryNo"].ToString();
        lblZone.Text = dsBill.Tables[0].Rows[0]["ZoneName"].ToString();
        lblAca.Text = dsBill.Tables[0].Rows[0]["AcaName"].ToString();
        hdnWorkAllotID.Value = dsBill.Tables[0].Rows[0]["WAID"].ToString();
        lblAgencyBillNo.Text = dsBill.Tables[0].Rows[0]["AgencyBillNumber"].ToString();
        aAgencyBill.Text = GetFileName(dsBill.Tables[0].Rows[0]["AgencyBill"].ToString(), dsBill.Tables[0].Rows[0]["AgencyName"].ToString());
        if (dsBill.Tables[0].Rows[0]["FirstVarifyStatus"].ToString() == "1")
        {
            divFinalButtons.Visible = false;
            txtRemark.Text = "Verified Bill";
            txtRemark.Enabled = false;
        }
        divBillMaterialDetails.InnerHtml = string.Empty;
        string BillInfo = string.Empty;
        BillInfo += "<div class='box span12'>";
        BillInfo += "<div class='box-header well' data-original-title>";
        BillInfo += "<h2><i class='icon-user'></i> Bills Material Detail</h2>";
        BillInfo += "</div>";
        BillInfo += "<div class='box-content'>";
        BillInfo += "<table border='1' class='table table-striped table-bordered bootstrap-datatable datatable'>";
        BillInfo += "<tr>";
        BillInfo += "<th width='10%'>Stock No</th>";
        BillInfo += "<th width='20%'>Material</th>";
        BillInfo += "<th width='10%'>Quantity</th>";
        BillInfo += "<th width='10%'>Unit</th>";
        BillInfo += "<th width='10%'>Rate</th>";
        BillInfo += "<th width='10%'>Amount</th>";
        BillInfo += "<th width='30%'>Remarks</th>";
        BillInfo += "</tr>";
        for (int i = 0; i < dsBill.Tables[2].Rows.Count; i++)
        {
            BillInfo += "<tr>";
            if (dsBill.Tables[2].Rows[i]["StockEntryNo"].ToString() == "" || dsBill.Tables[2].Rows[i]["StockEntryNo"].ToString() == null)
            {
                BillInfo += "<td width='15%'><span class='label label-success'>No Data</span></td>";
            }
            else
            {
                BillInfo += "<td width='10%'>" + dsBill.Tables[2].Rows[i]["StockEntryNo"].ToString() + "</td>";
            }
            //BillInfo += "<td width='10%'>" + dsBill.Tables[2].Rows[i]["StockEntryNo"].ToString() + "</td>";
            BillInfo += "<td width='20%'><a href='#' onclick='GetMaterialDetails(" + dsBill.Tables[2].Rows[i]["MatID"].ToString() + ")'>" + dsBill.Tables[2].Rows[i]["MatName"].ToString() + "</a></td>";
            BillInfo += "<td width='10%'>" + dsBill.Tables[2].Rows[i]["Qty"].ToString() + "</td>";
            BillInfo += "<td width='10%'>" + dsBill.Tables[2].Rows[i]["UnitName"].ToString() + "</td>";
            BillInfo += "<td width='10%'>" + dsBill.Tables[2].Rows[i]["Rate"].ToString() + "</td>";
            BillInfo += "<td width='10%'>" + dsBill.Tables[2].Rows[i]["Amount"].ToString() + "</td>";
            if (dsBill.Tables[2].Rows[i]["Remark"].ToString() == "" || dsBill.Tables[2].Rows[i]["Remark"].ToString() == null)
            {
                BillInfo += "<td width='30%'><span class='label label-success'>No Data</span></td>";
            }
            else
            {
                BillInfo += "<td width='30%'>" + dsBill.Tables[2].Rows[i]["Remark"].ToString() + "</td>";
            }
            //BillInfo += "<td width='40%'>" + dsBill.Tables[2].Rows[i]["Remark"].ToString() + "</td>";
            BillInfo += "</tr>";
           
        }
        if (dsBill.Tables[0].Rows[0]["EstId"].ToString() == "0")
        {
            BillInfo += "<tr>";
            BillInfo += "<td colspan='7'></td>";
            BillInfo += "</tr>";
        }
        else
        {
            //BillInfo += "<tr>";
            //BillInfo += "<td colspan='7'><table><tr><td><a class='btn btn-setting btn-round' href='Admin_ParticularEstimateView.aspx?EstId=" + dsBill.Tables[0].Rows[0]["EstId"].ToString() + "'>View Estimate Details</a> </td><td> </td></tr></table></td>";
            ////<a class='btn btn-setting btn-round' href='#'>View Balance</a>
           
            //BillInfo += "</tr>";
        }
        
        BillInfo += "</table>";
        BillInfo += "</div>";
        
        divBillMaterialDetails.InnerHtml = BillInfo.ToString();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (txtRemark.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Varify Remark.');", true);
        }
        else
        {
            string id = Request.QueryString["SubBillId"];
            DAL.DalAccessUtility.ExecuteNonQuery("update SubmitBillByUser set FirstVarify='" + InchargeID + "' , FirstVarifyOn=GETDATE(),FirstVarifyRemark=upper('" + txtRemark.Text + "'),FirstVarifyStatus=1 where SubBillId='" + id + "'");
            //DataSet dsContent = DAL.DalAccessUtility.GetDataInDataSet("exec USP_MsgContent '" + id + "'");
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Varified Successfully.');", true);
            //DataSet dsMsgContent = DAL.DalAccessUtility.GetDataInDataSet("exec USP_MsgContent '" + id + "'");
            //string Msg = "" + dsMsgContent.Tables[0].Rows[0]["BillType"].ToString() + " Bill No. " + id + " is varified by ADMIN";
            //DataSet dsCreatedBy = DAL.DalAccessUtility.GetDataInDataSet("select CreatedBy from SubmitBillByUser where SubBillId='" + id + "'");
            //DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsg '" + lblUser.Text + "','" + dsCreatedBy.Tables[0].Rows[0]["CreatedBy"].ToString() + "','" + Msg + "'");
            Response.Redirect("BillStatus.aspx");
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (txtRemark.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Reject Remark.');", true);
        }
        else
        {
            string id = Request.QueryString["SubBillId"];
            DAL.DalAccessUtility.ExecuteNonQuery("update SubmitBillByUser set FirstVarify='" + InchargeID + "' , FirstVarifyOn=GETDATE(),FirstVarifyRemark=upper('" + txtRemark.Text + "'),FirstVarifyStatus=0 where SubBillId='" + id + "'");
            // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Varified Successfully.');", true);
            //  DataSet dsMsgContent = DAL.DalAccessUtility.GetDataInDataSet("exec USP_MsgContent '" + id + "'");
            // string Msg = "" + dsMsgContent.Tables[0].Rows[0]["BillType"].ToString() + " Bill No. " + id + " is Rejected by ADMIN";
            //DataSet dsCreatedBy = DAL.DalAccessUtility.GetDataInDataSet("select CreatedBy from SubmitBillByUser where SubBillId='" + id + "'");
            // DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsg '" + InchargeID + "','" + dsMsgContent.Tables[0].Rows[0]["CreatedBy"].ToString() + "','" + Msg + "'");
            Response.Redirect("BillStatus.aspx");
        }
    }
    protected void btnAgain_Click(object sender, EventArgs e)
    {
        if (txtRemark.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Again Varify Remark.');", true);
        }
        else
        {
            string id1 = Request.QueryString["SubBillIdU"];
            DAL.DalAccessUtility.ExecuteNonQuery("update SubmitBillByUser set FirstVarify='" + InchargeID + "' , FirstVarifyOn=GETDATE(),FirstVarifyRemark=upper('" + txtRemark.Text + "'),FirstVarifyStatus=1 where SubBillId='" + id1 + "'");
            DAL.DalAccessUtility.ExecuteNonQuery("update BillProceedLog set UserProDate = null,UserProStatus=null,UserProUser=null,UserProRemark=null where BillId='" + id1 + "'");
            //DataSet dsContent = DAL.DalAccessUtility.GetDataInDataSet("exec USP_MsgContent '" + id1 + "'");
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Varified Successfully.');", true);
            //DataSet dsMsgContent = DAL.DalAccessUtility.GetDataInDataSet("exec USP_MsgContent '" + id1 + "'");
            //string Msg = "Proceed " + dsMsgContent.Tables[0].Rows[0]["BillType"].ToString() + " Bill No. " + id1 + " is varified again by ADMIN";
            //DataSet dsCreatedBy = DAL.DalAccessUtility.GetDataInDataSet("select CreatedBy from SubmitBillByUser where SubBillId='" + id1 + "'");
            //DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsg '" + lblUser + "','" + dsCreatedBy.Tables[0].Rows[0]["CreatedBy"].ToString() + "','" + Msg + "'");
            Response.Redirect("BillStatus.aspx");
        }
    }
    protected void btnPDFDownload_Click(object sender, EventArgs e)
    {
        string folderPath = Server.MapPath("Bills/CivilBillInvoice");
        Utility.GeneratePDFCivilMaterialBill(Convert.ToInt32(Request.QueryString["SubBillId"].ToString()), folderPath);
    }
    private string GetFileName(string filepaths, string fileName)
    {
        string anchorLink = string.Empty;
        string[] filePath = filepaths.Split(';');
        int count = 0;
        foreach (string path in filePath)
        {
            count++;
            anchorLink += "<a href= 'Bills/" + path + "' target='_blank'>" + fileName + "_" + count + "</a> , ";
        }

        return anchorLink.Substring(0, anchorLink.Length - 3);

    }
}