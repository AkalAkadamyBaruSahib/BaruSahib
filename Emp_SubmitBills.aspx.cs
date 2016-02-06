using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Emp_SubmitBills : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //pnlMaterial.Visible = false;
            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblUser.Text = Session["EmailId"].ToString();
            }
            //SetInitialRowBiils();
            BindAcademy();
            BindEstimate();
    //        SetInitialRowEstimate();
            divPopUpEstimateMaterial.Visible = false;
        }
    }
    private void SetInitialRowEstimate()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("SerialNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("GateEntryNo", typeof(string)));
        dt.Columns.Add(new DataColumn("StockEntryNo", typeof(string)));
        dt.Columns.Add(new DataColumn("Material", typeof(string)));
        dt.Columns.Add(new DataColumn("Material", typeof(string)));
        dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
        dt.Columns.Add(new DataColumn("Unit", typeof(string)));
        dt.Columns.Add(new DataColumn("Rate", typeof(string)));
        dt.Columns.Add(new DataColumn("Amount", typeof(string)));
        dt.Columns.Add(new DataColumn("Remark", typeof(string)));
        
        dr = dt.NewRow();
        dr["SerialNumber"] = 1;
        dr["GateEntryNo"] = "";
        dr["StockEntryNo"] = "";
        dr["Material"] = "";
        dr["Quantity"] = "";
        dr["Unit"] = "";
        dr["Rate"] = "";
        dr["Amount"] = "";
        dr["Remark"] = "";
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentEstimateTable"] = dt;

        gvAddItems.DataSource = dt;
        gvAddItems.DataBind();

    }
    private void AddNewRowToEstimateGrid()
    {
        int rowIndex = 0;

        if (ViewState["CurrentEstimateTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentEstimateTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    Label box1 = (Label)gvAddItems.Rows[rowIndex].Cells[1].FindControl("lblSno");
                    TextBox box2 = (TextBox)gvAddItems.Rows[rowIndex].Cells[4].FindControl("txtGateEntry");
                    TextBox box9 = (TextBox)gvAddItems.Rows[rowIndex].Cells[4].FindControl("txtStockEntry");
                    Label box3 = (Label)gvAddItems.Rows[rowIndex].Cells[3].FindControl("ddlMat");
                    TextBox box4 = (TextBox)gvAddItems.Rows[rowIndex].Cells[4].FindControl("txtQty");
                    DropDownList box5 = (DropDownList)gvAddItems.Rows[rowIndex].Cells[5].FindControl("ddlUnit");
                    TextBox box6 = (TextBox)gvAddItems.Rows[rowIndex].Cells[6].FindControl("txtRate");
                    Label box7 = (Label)gvAddItems.Rows[rowIndex].Cells[7].FindControl("lblAmt");
                    TextBox box8 = (TextBox)gvAddItems.Rows[rowIndex].Cells[8].FindControl("txtRemark");
                    


                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["SerialNumber"] = i + 1;
                    drCurrentRow["GateEntryNo"] = box2.Text;
                    drCurrentRow["StockEntryNo"] = box2.Text;
                    drCurrentRow["Material"] = box3.Text;
                    drCurrentRow["Quantity"] = box4.Text;
                    drCurrentRow["Unit"] = box5.SelectedValue;
                    drCurrentRow["Rate"] = box6.Text;
                    drCurrentRow["Amount"] = box7.Text;
                    drCurrentRow["Remark"] = box8.Text;


                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentEstimateTable"] = dtCurrentTable;

                gvAddItems.DataSource = dtCurrentTable;
                gvAddItems.DataBind();

            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetEstimatePreviousData();
    }
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtQt = (TextBox)row.FindControl("txtQty");
        TextBox txtRa = (TextBox)row.FindControl("txtRate");
        Label lblAm = (Label)row.FindControl("lblAmt");
        decimal qt = Convert.ToDecimal(txtQt.Text);
        decimal ra = Convert.ToDecimal(txtRa.Text);
        decimal am = qt * ra;
        lblAm.Text = am.ToString();

    }
    //protected void ButtonAdd2_Click(object sender, EventArgs e)
    //{

    //    AddNewRowToEstimateGrid();
    //}
    private void SetEstimatePreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentEstimateTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentEstimateTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Label box1 = (Label)gvAddItems.Rows[rowIndex].Cells[1].FindControl("lblSno");
                    TextBox box2 = (TextBox)gvAddItems.Rows[rowIndex].Cells[4].FindControl("txtGateEntry");
                    TextBox box9 = (TextBox)gvAddItems.Rows[rowIndex].Cells[4].FindControl("txtStockEntry");
                    Label box3 = (Label)gvAddItems.Rows[rowIndex].Cells[3].FindControl("ddlMat");
                    TextBox box4 = (TextBox)gvAddItems.Rows[rowIndex].Cells[4].FindControl("txtQty");
                    DropDownList box5 = (DropDownList)gvAddItems.Rows[rowIndex].Cells[5].FindControl("ddlUnit");
                    TextBox box6 = (TextBox)gvAddItems.Rows[rowIndex].Cells[6].FindControl("txtRate");
                    Label box7 = (Label)gvAddItems.Rows[rowIndex].Cells[7].FindControl("lblAmt");
                    TextBox box8 = (TextBox)gvAddItems.Rows[rowIndex].Cells[8].FindControl("txtRemark");

                    box1.Text = dt.Rows[i]["SerialNumber"].ToString();
                    box2.Text = dt.Rows[i]["GateEntryNo"].ToString();
                    box9.Text = dt.Rows[i]["StockEntryNo"].ToString();
                    box3.Text = dt.Rows[i]["Material"].ToString();
                    box4.Text = dt.Rows[i]["Quantity"].ToString();
                    box5.Text = dt.Rows[i]["Unit"].ToString();
                    box6.Text = dt.Rows[i]["Rate"].ToString();
                    box7.Text = dt.Rows[i]["Amount"].ToString();
                    box8.Text = dt.Rows[i]["Remark"].ToString();
                    //box9.Text = dt.Rows[i]["SourceType"].ToString();



                    rowIndex++;
                }
            }
        }
    }

    protected void BindAcademy()
    {
        DataSet dsAcademy = new DataSet();
        dsAcademy = DAL.DalAccessUtility.GetDataInDataSet(" exec USP_SubmitBillByUserAgainstEstimateForAcademyDetails '"+ lblUser.Text +"'");
        ddlAcademy.DataSource = dsAcademy;
        ddlAcademy.DataValueField = "AcaId";
        ddlAcademy.DataTextField = "AcaName";
        ddlAcademy.DataBind();
        ddlAcademy.Items.Insert(0, "Select Academy");
        ddlAcademy.SelectedIndex = 0;
    }
    protected void BindEstimate()
    {
        DataSet dsAcademy = new DataSet();
        dsAcademy = DAL.DalAccessUtility.GetDataInDataSet("select EstId,SubEstimate from Estimate where AcaId='"+ ddlAcademy.SelectedValue +"'");
        ddlEstimateNo.DataSource = dsAcademy;
        ddlEstimateNo.DataValueField = "EstId";
        ddlEstimateNo.DataTextField = "SubEstimate";
        ddlEstimateNo.DataBind();
        ddlEstimateNo.Items.Insert(0, "Select Estimate");
        ddlEstimateNo.SelectedIndex = 0;
    }
    protected void ddlEstimateNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsEstSub = new DataSet();
        dsEstSub = DAL.DalAccessUtility.GetDataInDataSet("select SubEstimate from Estimate where EstId='"+ ddlEstimateNo.SelectedValue +"'");
        lblEstSub.Text = dsEstSub.Tables[0].Rows[0]["SubEstimate"].ToString();
        
        //DataSet dsAcademy = new DataSet();
        //dsAcademy = DAL.DalAccessUtility.GetDataInDataSet("select MatId,MatName from Material where MatId in (select MatId from EstimateAndMaterialOthersRelations where EstId='"+ ddlEstimateNo.SelectedValue +"'  and PSId=1)");
        //chkMaterial.DataSource = dsAcademy;
        //chkMaterial.DataValueField = "MatId";
        //chkMaterial.DataTextField = "MatName";
        //chkMaterial.DataBind();
        //pnlMaterial.Visible=true;
        
    }
    protected void ddlAcademy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEstimate();
    }
    protected void btnJ_Click(object sender, EventArgs e)
    {
        gvAddItems.DataSource = (DataTable)Session["Data"];
        gvAddItems.DataBind();
    }
    protected void btnLoadItem_Click(object sender, EventArgs e)
    {
        gvAddItems.DataSource = (DataTable)Session["Data"];
        gvAddItems.DataBind();
    }
}