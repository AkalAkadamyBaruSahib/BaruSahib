using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Account_BillDetails : System.Web.UI.Page
{
    public static int InchargeID = -1;
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
                InchargeID = Convert.ToInt32(Session["InchargeID"].ToString());
            }
            if (Request.QueryString["SubBillId"] != null)
            {
                ShowBillDetails(Request.QueryString["SubBillId"].ToString());
            }

            //DataSet dsAdminVerifyStatus = DAL.DalAccessUtility.GetDataInDataSet("exec USP_PayVarifyDetails '" + Request.QueryString["SubBillId"].ToString() + "'");
            //if (dsAdminVerifyStatus.Tables[0].Rows[0]["FirstVarifyStatus"].ToString() == "")
            //{
            //    lblRemark.Text = "ADMIN NOT VERIFY BILL";
            //    txtRemark.Enabled  = false;
            //    btnSave.Enabled = false;
            //    btnEdit.Enabled = false;
            //}

            //else if (dsAdminVerifyStatus.Tables[0].Rows[0]["FirstVarifyStatus"].ToString() == "0")
            //{
            //    lblRemark.Text = dsAdminVerifyStatus.Tables[0].Rows[0]["FirstVarifyRemark"].ToString();
            //    txtRemark.Enabled = false;
            //    btnSave.Enabled = false;
            //    btnEdit.Enabled = false;
            //}
            //else if (dsAdminVerifyStatus.Tables[0].Rows[0]["FirstVarifyStatus"].ToString() == "1" && dsAdminVerifyStatus.Tables[0].Rows[0]["SecondVarifyStatus"].ToString() == "0")
            //{
            //    lbl2ndRemark.Text = "Admin verify but Audit Department not verifity yet.";
            //    txtRemark.Enabled = false;
            //    btnSave.Enabled = false;
            //    btnEdit.Enabled = false;
            //}
            //else if (dsAdminVerifyStatus.Tables[0].Rows[0]["SecondVarifyStatus"].ToString() == "")
            //{
            //    lbl2ndRemark.Text = "AUDIT DEPARTMENT NOT VERIFY BILL";
            //    txtRemark.Enabled = false;
            //    btnSave.Enabled = false;
            //    btnEdit.Enabled = false;
            //}

            //else if (dsAdminVerifyStatus.Tables[0].Rows[0]["SecondVarifyStatus"].ToString() == "0")
            //{
            //    lbl2ndRemark.Text = dsAdminVerifyStatus.Tables[0].Rows[0]["SecondVarifyStatus"].ToString();
            //    txtRemark.Enabled = false;
            //    btnSave.Enabled = false;
            //    btnEdit.Enabled = false;
            //}

            //else
            //{

            //    txtRemark.Enabled = true;
            //    btnSave.Enabled = true;
            //    btnEdit.Enabled = true;
            //    lblRemark.Visible = false;

            //}
        }
    }

    protected void ShowBillDetails(string ID)
    {
        DataSet dsBill = new DataSet();
        dsBill = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AdminBillViewByBillId_V2 '" + ID + "'");
       
            lblBillNo.Text = dsBill.Tables[0].Rows[0]["SubBillId"].ToString();
            //lblBillType.Text = dsBill.Tables[0].Rows[0]["BillTypeName"].ToString();
            lblChargeableTo.Text = dsBill.Tables[0].Rows[0]["BillType"].ToString();
            lblBillDesc.Text = dsBill.Tables[0].Rows[0]["BillDescr"].ToString();
            lblAgencyName.Text = dsBill.Tables[0].Rows[0]["AgencyName"].ToString();
            lblBillDate.Text = dsBill.Tables[0].Rows[0]["BillDate"].ToString();
            lblGateEntry.Text = dsBill.Tables[0].Rows[0]["GateEntryNo"].ToString();
            lblZone.Text = dsBill.Tables[0].Rows[0]["ZoneName"].ToString();
            lblAca.Text = dsBill.Tables[0].Rows[0]["AcaName"].ToString();
            if (dsBill.Tables[0].Rows[0]["FirstVarifyStatus"].ToString() == "")
            {
                lblHqUser.Text = "Not Varified";
                lblHqAppDate.Text = "Not Varified";
                lblHqRemark.Text = "Not Varified";
                lblHqStatus.Text = "No Status";
            }
            else if (dsBill.Tables[0].Rows[0]["FirstVarifyStatus"].ToString() == "0")
            {
                DataTable ds1stVerfiName = new DataTable();
                ds1stVerfiName = DAL.DalAccessUtility.GetDataInDataSet("select InName from Incharge where InchargeID='" + dsBill.Tables[0].Rows[0]["FirstVarify"].ToString() + "'").Tables[0];
                if (ds1stVerfiName != null && ds1stVerfiName.Rows.Count > 0)
                {
                    lblHqUser.Text = ds1stVerfiName.Rows[0]["InName"].ToString();
                }
                lblHqAppDate.Text = dsBill.Tables[0].Rows[0]["FirstVarifyOn"].ToString();
                lblHqRemark.Text = dsBill.Tables[0].Rows[0]["FirstVarifyRemark"].ToString();
                lblHqStatus.Text = "REJECTED";
            }
            else
            {
                DataTable ds1stVerfiName = new DataTable();
                ds1stVerfiName = DAL.DalAccessUtility.GetDataInDataSet("select InName from Incharge where InchargeID='" + dsBill.Tables[0].Rows[0]["FirstVarify"].ToString() + "'").Tables[0];
                if (ds1stVerfiName != null && ds1stVerfiName.Rows.Count > 0)
                {
                    lblHqUser.Text = ds1stVerfiName.Rows[0]["InName"].ToString();
                }
                lblHqAppDate.Text = dsBill.Tables[0].Rows[0]["FirstVarifyOn"].ToString();
                lblHqRemark.Text = dsBill.Tables[0].Rows[0]["FirstVarifyRemark"].ToString();
                lblHqStatus.Text = "VERIFIED";
            }
            if (dsBill.Tables[0].Rows[0]["SecondVarifyStatus"].ToString() == "")
            {
                lbl2ndUser.Text = "Not Varified";
                lbl2ndAppOn.Text = "Not Varified";
                lbl2ndRemark.Text = "Not Varified";
                lblAuditStatus.Text = "No Status";
            }
            else if (dsBill.Tables[0].Rows[0]["SecondVarifyStatus"].ToString() == "0")
            {
                DataSet ds2ndVerfiName = DAL.DalAccessUtility.GetDataInDataSet("select InName from Incharge where InchargeID='" + dsBill.Tables[0].Rows[0]["SeccondVarify"].ToString() + "'");
                lbl2ndUser.Text = ds2ndVerfiName.Tables[0].Rows[0]["InName"].ToString();
                lbl2ndAppOn.Text = dsBill.Tables[0].Rows[0]["SeccondVarifyOn"].ToString();
                lbl2ndRemark.Text = dsBill.Tables[0].Rows[0]["SecondVarifyRemark"].ToString();
                lblAuditStatus.Text = "REJECTED";
            }
            else
            {
                DataSet ds2ndVerfiName = DAL.DalAccessUtility.GetDataInDataSet("select InName from Incharge where InchargeID='" + dsBill.Tables[0].Rows[0]["SeccondVarify"].ToString() + "'");
                lbl2ndUser.Text = ds2ndVerfiName.Tables[0].Rows[0]["InName"].ToString();
                lbl2ndAppOn.Text = dsBill.Tables[0].Rows[0]["SeccondVarifyOn"].ToString();
                lbl2ndRemark.Text = dsBill.Tables[0].Rows[0]["SecondVarifyRemark"].ToString();
                lblAuditStatus.Text = "VARIFIED";
            }
            if (dsBill.Tables[0].Rows[0]["PaymentStatus"].ToString() == "")
            {
                lbl3rdUser.Text = "Not Varified";
                lbl3rdAppOn.Text = "Not Varified";
                lblPayStatus.Text = "No Status";
            }
            else if (dsBill.Tables[0].Rows[0]["PaymentStatus"].ToString() == "0")
            {
                DataSet ds3ndVerfiName = DAL.DalAccessUtility.GetDataInDataSet("select InName from Incharge where InchargeID='" + dsBill.Tables[0].Rows[0]["ThirdVarifyBy"].ToString() + "'");
                lbl3rdUser.Text = ds3ndVerfiName.Tables[0].Rows[0]["InName"].ToString();
                lbl3rdAppOn.Text = dsBill.Tables[0].Rows[0]["ThirdVarifyOn"].ToString();
                lblPayStatus.Text = "REJECTED";
                if (dsBill.Tables[1].Rows.Count > 0)
                {
                    lblAccRemark.Text = dsBill.Tables[1].Rows[0]["Remark"].ToString();
                    lblPayMode.Text = dsBill.Tables[1].Rows[0]["PayModeName"].ToString();
                    lblPayDetails.Text = dsBill.Tables[1].Rows[0]["PayDetails"].ToString();
                }
                else
                {
                    lblAccRemark.Text = "Not Varified";
                    lblPayMode.Text = "Not Varified";
                    lblPayDetails.Text = "Not Varified";
                }
            }
            else
            {
                DataSet ds3ndVerfiName = DAL.DalAccessUtility.GetDataInDataSet("select InName from Incharge where InchargeID='" + dsBill.Tables[0].Rows[0]["ThirdVarifyBy"].ToString() + "'");
                lbl3rdUser.Text = ds3ndVerfiName.Tables[0].Rows[0]["InName"].ToString();
                lbl3rdAppOn.Text = dsBill.Tables[0].Rows[0]["ThirdVarifyOn"].ToString();
                lblPayStatus.Text = "VARIFIED";
                if (dsBill.Tables[1].Rows.Count > 0)
                {
                    lblAccRemark.Text = dsBill.Tables[1].Rows[0]["Remark"].ToString();
                    lblPayMode.Text = dsBill.Tables[1].Rows[0]["PayModeName"].ToString();
                    lblPayDetails.Text = dsBill.Tables[1].Rows[0]["PayDetails"].ToString();
                }
                else
                {
                    lblAccRemark.Text = "Not Varified";
                    lblPayMode.Text = "Not Varified";
                    lblPayDetails.Text = "Not Varified";
                }
                
            }

            if (dsBill.Tables[0].Rows[0]["RecevingStatus"].ToString() == "")
            {
                lblRecUser.Text = "Not Varified";
                lblRecAppOn.Text = "Not Varified";
                lblRecVocNo.Text = "Not Varified";
                lblRecRemark.Text = "Not Varified";
                lblRecStatus.Text = "No Status";
            }
            else if (dsBill.Tables[0].Rows[0]["RecevingStatus"].ToString() == "0")
            {
                DataSet ds4ndVerfiName = DAL.DalAccessUtility.GetDataInDataSet("select InName from Incharge where InchargeID='" + dsBill.Tables[0].Rows[0]["RecevingBy"].ToString() + "'");
                lblRecUser.Text = ds4ndVerfiName.Tables[0].Rows[0]["InName"].ToString();
                lblRecAppOn.Text = dsBill.Tables[0].Rows[0]["DateOfReceving"].ToString();
                lblRecVocNo.Text = dsBill.Tables[0].Rows[0]["ReciptNoByEmp"].ToString();
                lblRecRemark.Text = dsBill.Tables[0].Rows[0]["RecevingRemark"].ToString();
                lblRecStatus.Text = "REJECTED";
            }
            else
            {
                DataSet ds4ndVerfiName = DAL.DalAccessUtility.GetDataInDataSet("select InName from Incharge where InchargeID='" + dsBill.Tables[0].Rows[0]["RecevingBy"].ToString() + "'");
                lblRecUser.Text = ds4ndVerfiName.Tables[0].Rows[0]["InName"].ToString();
                lblRecAppOn.Text = dsBill.Tables[0].Rows[0]["DateOfReceving"].ToString();
                lblRecVocNo.Text = dsBill.Tables[0].Rows[0]["ReciptNoByEmp"].ToString();
                lblRecRemark.Text = dsBill.Tables[0].Rows[0]["RecevingRemark"].ToString();
                lblRecStatus.Text = "VARIFIED";
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
            BillInfo += "<th width='10%'>Material</th>";
            BillInfo += "<th width='10%'>Quantity</th>";
            BillInfo += "<th width='10%'>Unit</th>";
            BillInfo += "<th width='10%'>Rate</th>";
            BillInfo += "<th width='10%'>Amount</th>";
            BillInfo += "<th width='40%'>Remarks</th>";
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
                    BillInfo += "<td width='15%'>" + dsBill.Tables[2].Rows[i]["StockEntryNo"].ToString() + "</td>";
                }
                //BillInfo += "<td width='10%'>" + dsBill.Tables[2].Rows[i]["StockEntryNo"].ToString() + "</td>";
                BillInfo += "<td width='10%'>" + dsBill.Tables[2].Rows[i]["MatName"].ToString() + "</td>";
                BillInfo += "<td width='10%'>" + dsBill.Tables[2].Rows[i]["Qty"].ToString() + "</td>";
                BillInfo += "<td width='10%'>" + dsBill.Tables[2].Rows[i]["UnitName"].ToString() + "</td>";
                BillInfo += "<td width='10%'>" + dsBill.Tables[2].Rows[i]["Rate"].ToString() + "</td>";
                BillInfo += "<td width='10%'>" + dsBill.Tables[2].Rows[i]["Amount"].ToString() + "</td>";
                if (dsBill.Tables[2].Rows[i]["Remark"].ToString() == "" || dsBill.Tables[2].Rows[i]["Remark"].ToString() == null)
                {
                    BillInfo += "<td width='15%'><span class='label label-success'>No Data</span></td>";
                }
                else
                {
                    BillInfo += "<td width='15%'>" + dsBill.Tables[2].Rows[i]["Remark"].ToString() + "</td>";
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
                BillInfo += "<tr>";
                BillInfo += "<td colspan='7'><a href='Account_EstmateView.aspx?EstId=" + dsBill.Tables[0].Rows[0]["EstId"].ToString() + "'>View Details</a> </td>";
                BillInfo += "</tr>";
            }

            BillInfo += "</table>";
            BillInfo += "</div>";

            divBillMaterialDetails.InnerHtml = BillInfo.ToString();
       
       
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtRemark.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Remark.');", true);
        }
        else
        {
            string id = Request.QueryString["SubBillId"];
            DataSet ds1StVeri = DAL.DalAccessUtility.GetDataInDataSet("select FirstVarifyStatus from SubmitBillByUser where SubBillId='" + id + "'");
            if (ds1StVeri.Tables[0].Rows[0]["FirstVarifyStatus"].ToString() == "1")
            {
                btnEdit.Visible = true;
                btnSave.Visible = true;
                DAL.DalAccessUtility.ExecuteNonQuery("update SubmitBillByUser set ThirdVarifyBy='"+ InchargeID +"',ThirdVarifyOn=GETDATE(),ThirdVarifyRemark=upper('"+ txtRemark.Text +"'),PaymentStatus=1 where SubBillId='" + id + "'");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Varify Successfully.');", true);
                Response.Redirect("Account_BillStatus.aspx");
            }
            else
            {
                btnEdit.Visible = false;
                btnSave.Visible = false;
            }
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (txtRemark.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Remark.');", true);
        }
        else
        {
            string id = Request.QueryString["SubBillId"];
            DataSet ds1StVeri = DAL.DalAccessUtility.GetDataInDataSet("select FirstVarifyStatus from SubmitBillByUser where SubBillId='" + id + "'");
            if (ds1StVeri.Tables[0].Rows[0]["FirstVarifyStatus"].ToString() == "1")
            {
                btnEdit.Visible = true;
                btnSave.Visible = true;
                DAL.DalAccessUtility.ExecuteNonQuery("update SubmitBillByUser set ThirdVarifyBy='" + InchargeID + "',ThirdVarifyOn=GETDATE(),ThirdVarifyRemark=upper('" + txtRemark.Text + "'),PaymentStatus=0 where SubBillId='" + id + "'");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Reject Successfully.');", true);
                Response.Redirect("Account_BillStatus.aspx");
            }
            else
            {
                btnEdit.Visible = false;
                btnSave.Visible = false;
            }
        }
    }
}