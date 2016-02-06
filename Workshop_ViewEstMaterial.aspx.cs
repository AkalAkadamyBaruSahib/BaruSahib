using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Workshop_ViewEstMaterial : System.Web.UI.Page
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

        ViewState["chklabel"] = null;
        if (!IsPostBack)
        {
            if (Request.QueryString["EstId"] != null)
            {
                getZoneAcaName(Request.QueryString["EstId"].ToString());
                getMaterialDetails(Request.QueryString["EstId"].ToString());
            }
        }
    }
    private void getMaterialDetails(string id)
    {
        DataSet dsAcaDetails = new DataSet();
        dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateMaterialViewForPurchase '" + id + "','3' ");
        string Material, Qty, Unit, Remark, TantiveDate, DisDate;
        int rowindex = 0;
        foreach (GridViewRow gvrow in gvMaterailDetailForPurchase.Rows)
        {
            Material = gvrow.Cells[1].Text;
            DataSet dsMatid = new DataSet();
            dsMatid = DAL.DalAccessUtility.GetDataInDataSet("select MatId from Material where MatName='" + Material + "'");
            Unit = gvrow.Cells[2].Text;
            DataSet dsUnitid = new DataSet();
            dsUnitid = DAL.DalAccessUtility.GetDataInDataSet("select UnitId from Unit where UnitName='" + Unit + "'");
            Qty = gvrow.Cells[2].Text;
            Button btnDisDateDate = (Button)gvMaterailDetailForPurchase.Rows[rowindex].FindControl("btnDispatch");
            Label txtDisDateDate = (Label)gvMaterailDetailForPurchase.Rows[rowindex].FindControl("txtDispatchDate");
            Label lblTTDate = (Label)gvMaterailDetailForPurchase.Rows[rowindex].FindControl("lblTatDate");
            lblTTDate.Text = gvrow.Cells[4].Text;
            if (dsAcaDetails.Tables[0].Rows[0]["DispatchDate"].ToString() == "" || dsAcaDetails.Tables[0].Rows[0]["DispatchDate"].ToString() == null)
            {
                btnDisDateDate.Visible = true;
                txtDisDateDate.Visible = false;
            }
            else
            {
                btnDisDateDate.Visible = false;
                txtDisDateDate.Visible = true;
            }
            if (dsAcaDetails.Tables[0].Rows[0]["TantiveDate"].ToString() == "" || dsAcaDetails.Tables[0].Rows[0]["TantiveDate"].ToString() == null)
            {
                btnDisDateDate.Enabled = true;
            }
            else
            {
                btnDisDateDate.Enabled = false;
            }
            TextBox txtRek = (TextBox)gvMaterailDetailForPurchase.Rows[rowindex].FindControl("txtRemark");
            Remark = txtRek.Text;
            rowindex = rowindex + 1;
        }
        gvMaterailDetailForPurchase.DataSource = dsAcaDetails;
        gvMaterailDetailForPurchase.DataBind();
    }
    private void getZoneAcaName(string id)
    {
        DataSet dsDetails = new DataSet();
        dsDetails = DAL.DalAccessUtility.GetDataInDataSet(" exec USP_ZoneAndAcademnyNameByEstId '" + id + "'");
        lblAcademy.Text = dsDetails.Tables[0].Rows[0]["AcaName"].ToString();
        lblZoneName.Text = dsDetails.Tables[0].Rows[0]["ZoneName"].ToString();
        DataSet dsZoneId = new DataSet();
        dsZoneId = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId from Zone where ZoneName='" + lblZoneName.Text + "'");
        Session["Zone"] = dsZoneId.Tables[0].Rows[0]["ZoneId"].ToString();

        lblEstId.Text = dsDetails.Tables[0].Rows[0]["EstId"].ToString();
    }
    protected void btnDispatch_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        GridViewRow gv = (GridViewRow)((Button)sender).NamingContainer;
        TextBox txtTatDate = (TextBox)gv.Cells[4].FindControl("txtTatDate");
        TextBox txtRek = (TextBox)gv.Cells[6].FindControl("txtRemark");
        string EstId = gv.Cells[0].Text;
        string Material = gv.Cells[1].Text;
        DataSet dsMatid = new DataSet();
        dsMatid = DAL.DalAccessUtility.GetDataInDataSet("select MatId from Material where MatName='" + Material + "'");
        string Unit = gv.Cells[2].Text;
        DataSet dsUnitid = new DataSet();
        dsUnitid = DAL.DalAccessUtility.GetDataInDataSet("select  UnitId from Unit where UnitName='" + Unit + "'");
        if (txtTatDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please insert Tantitive Date before dispatch.');", true);
        }
        else
        {
            DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set DispatchBy='" + lblUser.Text + "',DispatchDate=GETDATE(),DispatchStatus=1 where EstId='" + lblEstId.Text + "' and MatId='" + dsMatid.Tables[0].Rows[0]["MatId"].ToString() + "' and UnitId='" + dsUnitid.Tables[0].Rows[0]["UnitId"].ToString() + "'");
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material Dispatch Successfully.');", true);
        }
    }
    protected void gvMaterailDetailForPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "DispatchDate")
            {
                GridViewRow gvRow = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int RowIndex = gvRow.RowIndex;
                Button DisDate = (Button)e.CommandSource;
                string Sno = DisDate.CommandArgument;
                Label lbl = (Label)gvRow.FindControl("txtDispatchDate");
                TextBox tatDate = (TextBox)gvRow.FindControl("txtTatDate");
                TextBox rmk = (TextBox)gvRow.FindControl("txtRemark");
                lbl.Attributes.Remove("style");
                lbl.Attributes.Add("style", "display:blockp;");
                DisDate.Attributes.Add("style", "display:none;");
                DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set DispatchDate='" + lbl.Text + "',remarkByPurchase='" + rmk.Text + "',DispatchStatus='1',DispatchOn=getdate(),DispatchBy='" + lblUser.Text + "' where Sno='" + Sno + "'");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Dispatch Date Updated Successfully.')", true);
                Response.Redirect("Worksho_MaterialToBeDispatch.aspx");
            }

            if (e.CommandName == "Tatitive")
            {
                string Material, Unit;
                GridViewRow row = ((GridViewRow)((Button)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                Material = row.Cells[1].Text;
                Unit = row.Cells[2].Text;
                Button ttive = (Button)e.CommandSource;
                string Sno = ttive.CommandArgument;
                Label lbl = (Label)row.FindControl("txtDispatchDate");
                Label lblTat = (Label)row.FindControl("lblTatDate");
                TextBox tatDate = (TextBox)row.FindControl("txtTatDate");
                TextBox rmk = (TextBox)row.FindControl("txtRemark");
                DataSet dsMatid = new DataSet();
                dsMatid = DAL.DalAccessUtility.GetDataInDataSet("select MatId from Material where MatName='" + Material + "'");
                DataSet dsUnitid = new DataSet();
                dsUnitid = DAL.DalAccessUtility.GetDataInDataSet("select UnitId from Unit where UnitName='" + Unit + "'");
                if (tatDate.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please insert Tantitive Date.');", true);
                }
                else
                {
                    DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set TantiveDate='" + tatDate.Text + "' where EstId='" + lblEstId.Text + "' and MatId='" + dsMatid.Tables[0].Rows[0]["MatId"].ToString() + "' and UnitId='" + dsUnitid.Tables[0].Rows[0]["UnitId"].ToString() + "' and Sno='" + Sno + "'");
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Tantitive Date successfully submit for " + Material + ".');", true);
                }
                DataSet dsTatDate = DAL.DalAccessUtility.GetDataInDataSet("select TantiveDate,EstId from EstimateAndMaterialOthersRelations where Sno='" + Sno + "'");
                lblTat.Text = dsTatDate.Tables[0].Rows[0]["TantiveDate"].ToString();
                getMaterialDetails(dsTatDate.Tables[0].Rows[0]["EstId"].ToString());
            }
        }
        catch (Exception)
        {
            //  throw;
        }
    }

    protected void gvMaterailDetailForPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button btnDis = (Button)e.Row.Cells[6].FindControl("btnDispatch");
            Button btnTat = (Button)e.Row.Cells[7].FindControl("btnOk");
            Label lbl = (Label)e.Row.Cells[6].FindControl("txtDispatchDate");
            TextBox txttt = (TextBox)e.Row.Cells[4].FindControl("txtTatDate");
            Label ttlbl = (Label)e.Row.Cells[4].FindControl("lblTatDate");
            if (ttlbl.Text != string.Empty && ttlbl.Text != null)
            {
                ttlbl.Visible = true;
                txttt.Visible = false;
                btnTat.Visible = false;
            }
            else
            {
                txttt.Visible = true;
                ttlbl.Visible = false;
                btnTat.Visible = true;
            }
            if (lbl.Text != string.Empty && lbl.Text != null)
            {
                lbl.Visible = true;
                btnDis.Visible = false;
            }
            else
            {
                btnDis.Visible = true;
                lbl.Visible = false;
            }
        }
    }
}