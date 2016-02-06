using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Account_BillVerify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmailId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblUser.Text = Session["EmailId"].ToString();
        }
        if (!IsPostBack)
        {
            if (Request.QueryString["AcaId"] != null)
            {
                getBillDetails(Request.QueryString["AcaId"].ToString());
            }
        }
        //if (!IsPostBack)
        //{
        //    if (Request.QueryString["SubBillId"] != null)
        //    {
        //        getBillDetailsByBillId(Request.QueryString["SubBillId"].ToString());
        //    }
        //}
    }

    private void getBillDetails(string id)
    {
        DataSet dsAcaDetails = new DataSet();
        dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BillDetailByAcaId '" + id + "'");
        string BillId, Agency, Amount, Remark, PayMode, PayDetails;
        if (dsAcaDetails.Tables[0].Rows.Count > 0)
        {
            int rowindex = 0;
            lblAcaName.Text = dsAcaDetails.Tables[0].Rows[0]["AcaName"].ToString();
            foreach (GridViewRow gvrow in gvBillDetails.Rows)
            {

                Label lblBillId = (Label)gvBillDetails.Rows[rowindex].FindControl("lblBillId");
                BillId = lblBillId.Text;
                //BillId = gvrow.Cells[0].Text;
                Label lblAgency = (Label)gvBillDetails.Rows[rowindex].FindControl("lblAgencyName");
                Agency = lblAgency.Text;
                // Agency = gvrow.Cells[1].Text;
                Label lblAmt = (Label)gvBillDetails.Rows[rowindex].FindControl("lblAmount");
                Amount = lblAmt.Text;
                //Amount = gvrow.Cells[2].Text;
                DropDownList ddlPm = (DropDownList)gvBillDetails.Rows[rowindex].FindControl("ddlPayMode");
                PayMode = ddlPm.SelectedValue;
                TextBox txtPayDet = (TextBox)gvBillDetails.Rows[rowindex].FindControl("txtPaymentDetails");
                PayDetails = txtPayDet.Text;
                TextBox txtRek = (TextBox)gvBillDetails.Rows[rowindex].FindControl("txtRemark");
                Remark = txtRek.Text;
                //DAL.DalAccessUtility.ExecuteNonQuery("exec USP_DispatchByPurchase '" + TantiveDate + "','" + DisDate + "','" + Remark + "','" + lblUser.Text + "','" + dsMatid.Tables[0].Rows[0]["MatId"].ToString() + "','" + dsUnitid.Tables[0].Rows[0]["UnitId"].ToString() + "','" + id + "'");
                rowindex = rowindex + 1;
            }
            gvBillDetails.DataSource = dsAcaDetails;
            gvBillDetails.DataBind();
        }
        else
        {
            pnlBillDetails.Visible = false;
            lblBillDetails.Visible = true;
        }
    }
    protected void gvBillDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlPm = (DropDownList)e.Row.FindControl("ddlPayMode");
            DataSet dsPm = new DataSet();
            dsPm = DAL.DalAccessUtility.GetDataInDataSet("select PayModeId,PayModeName from PaymentMode where Active=1");
            ddlPm.DataSource = dsPm;
            ddlPm.DataValueField = "PayModeId";
            ddlPm.DataTextField = "PayModeName";
            ddlPm.DataBind();
            ddlPm.Items.Insert(0, "SELECT MODE");
            ddlPm.SelectedIndex = 0;
        }
    }
    protected void btnPay_Click1(object sender, EventArgs e)
    {
        
        GridViewRow gvrow = (GridViewRow)((Button)sender).NamingContainer;
        string BillId = gvrow.Cells[0].Text;
        string Agency = gvrow.Cells[1].Text;
        string Amount = gvrow.Cells[2].Text;
        DropDownList ddlPm = (DropDownList)gvrow.Cells[3].FindControl("ddlPayMode");
        string PayMode = ddlPm.SelectedValue;
        TextBox txtPayDet = (TextBox)gvrow.Cells[4].FindControl("txtPaymentDetails");
        string PayDetails = txtPayDet.Text;
        TextBox txtRek = (TextBox)gvrow.Cells[5].FindControl("txtRemark");
        string Remark = txtRek.Text;
        if (PayMode == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Mode of Payment.');", true);
        }
        else if (PayDetails == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Payment Details.');", true);
        }
        else if (Remark == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter Remark of this Payment.');", true);
        }
        else
        {
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_PaymentSubmit '','" + BillId + "','" + PayMode + "','" + PayDetails + "','" + Remark + "',1,'" + lblUser.Text + "',1,1");
            getBillDetails(Request.QueryString["AcaId"].ToString());
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Varify Successfully.');", true);
            //getBillDetailsByBillId(Request.QueryString["SubBillId"].ToString());
        }
    }
    protected void btnRej_Click(object sender, EventArgs e)
    {
        GridViewRow gvrow = (GridViewRow)((Button)sender).NamingContainer;
        string BillId = gvrow.Cells[0].Text;
        TextBox txtRek = (TextBox)gvrow.Cells[5].FindControl("txtRemark");
        string Remark = txtRek.Text;
        DropDownList ddlPm = (DropDownList)gvrow.Cells[3].FindControl("ddlPayMode");
        string PayMode = ddlPm.SelectedValue;
        TextBox txtPayDet = (TextBox)gvrow.Cells[4].FindControl("txtPaymentDetails");
        string PayDetails = txtPayDet.Text;
        ddlPm.Enabled = true;
        txtPayDet.Enabled = true;
        if (Remark == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Enter Remark for Rejection.');", true);
        }
        else
        {
            DAL.DalAccessUtility.ExecuteNonQuery("update SubmitBillByUser set PaymentStatus=0, ThirdVarifyBy='" + lblUser.Text + "',ThirdVarifyRemark=upper('"+ Remark +"'),ThirdVarifyOn=GETDATE() where SubBillId='" + BillId + "' ");
            getBillDetails(Request.QueryString["AcaId"].ToString());
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Reject Successfully.');", true);
        }
    }
}