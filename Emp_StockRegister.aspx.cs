using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Emp_StockRegister : System.Web.UI.Page
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
        if (Session["AcaId1"] == null)
        {
            Response.Redirect("Emp_Home.aspx");
        }
        else
        {
            string a;
            a = Session["AcaId1"].ToString();
            DataSet dsAcaName = DAL.DalAccessUtility.GetDataInDataSet("select AcaName from academy where AcaId='"+ a +"'");
            lblAcaName.Text = dsAcaName.Tables[0].Rows[0]["AcaName"].ToString();
        }
        if (!IsPostBack)
        { 
            if (Request.QueryString["MaId"] != null)
            {
                getStockRegister(Request.QueryString["MaId"].ToString());
                string b;
                b = Request.QueryString["MaId"].ToString();
                DataSet dsMatName = DAL.DalAccessUtility.GetDataInDataSet("select MatName from Material where MatId='" + b + "'");
                lblMaterialName.Text = dsMatName.Tables[0].Rows[0]["MatName"].ToString();
            }
        }
       

    }

    protected void getStockRegister(string id)
    {
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("select SRId,DateOfEntry,Amount,ReciQty,IssuedQty,BalanceQty,Remark,Particluar,ReciptAndBillNo from StockRegister where CreatedBy='" + lblUser.Text + "' and MatId='"+ id +"'");
        //ds = DAL.DalAccessUtility.GetDataInDataSet("select SRId,ChallenBillNo,GateEntryNo,AgencyName,Amount,ReciQty,IssuedQty,BalanceQty,Remark from StockRegister ");
        if (ds.Tables.Count > 0)
        {
            gvdStockRegister.DataSource = ds;
            gvdStockRegister.DataBind(); 
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Previous Record');", true);
        }

    }
    protected void gvdStockRegister_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label txtAgencyName = (Label)gvdStockRegister.FooterRow.FindControl("txt_AgencyName");
        //txtAgencyName.Text = System.DateTime.Now.ToString();
        //txtAgencyName.Enabled = false;
        TextBox txtChallen = (TextBox)gvdStockRegister.FooterRow.FindControl("txt_ChallenNo");
        TextBox txtGateNo = (TextBox)gvdStockRegister.FooterRow.FindControl("txt_GateEntryNo");
        TextBox txtAmt = (TextBox)gvdStockRegister.FooterRow.FindControl("txt_Amount");
        TextBox txtRecQty = (TextBox)gvdStockRegister.FooterRow.FindControl("txt_ReciQty");
        TextBox txtIssQty = (TextBox)gvdStockRegister.FooterRow.FindControl("txt_IssuedQty");
        TextBox txtBalQty = (TextBox)gvdStockRegister.FooterRow.FindControl("txt_BalanceQty");
        TextBox txtRemark = (TextBox)gvdStockRegister.FooterRow.FindControl("txt_Remark");
        string a = Session["AcaId1"].ToString();
        string m = Request.QueryString["MaId"].ToString();
        DAL.DalAccessUtility.ExecuteNonQuery(" exec USP_StockRegisterEntery '" + txtAmt.Text + "','" + txtRecQty.Text + "','" + txtIssQty.Text + "','" + txtBalQty.Text + "','" + txtRemark.Text + "','1','" + lblUser.Text + "','" + Session["AcaId1"].ToString() + "','" + Request.QueryString["MaId"].ToString() + "','" + txtChallen.Text + "','" + txtGateNo.Text + "','" + txtAgencyName.Text + "'");
        getStockRegister(Request.QueryString["MaId"].ToString());
    }
}