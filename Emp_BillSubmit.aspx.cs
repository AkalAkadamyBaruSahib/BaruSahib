using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Emp_BillSubmit : System.Web.UI.Page
{
    private int BillID = -1;
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
            if (Request.QueryString["AcaId"] != null)
            {
                BindNameOfWork(Request.QueryString["AcaId"].ToString());
            }

            SetInitialRowBiils();
            BindBillType();
            ddlNameOfWork.Visible = false;
            ddlBillType.Visible = false;
            ddlEsimate.Visible = false;
            trMatSelect.Visible = false;
            pnlEstimateDetails.Visible = false;

            if (Request.QueryString["BillID"] != null && Request.QueryString["AcaId"] != null)
            {
                BillID = int.Parse(Request.QueryString["BillID"]);
                ShowBillDetails(Request.QueryString["BillID"].ToString());
            }
           
        }
        if (Request.QueryString["BillID"] != null && Request.QueryString["AcaId"] != null)
        {
            BillID = int.Parse(Request.QueryString["BillID"]);
        }

    }
    private void SetInitialRowBiils()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("SerialNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
        dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
        dt.Columns.Add(new DataColumn("Unit", typeof(string)));
        dt.Columns.Add(new DataColumn("Rate", typeof(string)));
        dt.Columns.Add(new DataColumn("Amount", typeof(string)));
        dt.Columns.Add(new DataColumn("MaterialType", typeof(string)));
        dt.Columns.Add(new DataColumn("Material", typeof(string)));
        dt.Columns.Add(new DataColumn("StockEntryNo", typeof(string)));

        dr = dt.NewRow();
        dr["SerialNumber"] = 1;
        dr["ItemName"] = string.Empty;
        dr["Quantity"] = string.Empty;
        dr["Unit"] = string.Empty;
        dr["Rate"] = string.Empty;
        dr["Amount"] = string.Empty;
        dr["MaterialType"] = string.Empty;
        dr["Material"] = string.Empty;
        dr["StockEntryNo"] = string.Empty;

        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentAddItemTable"] = dt;

        gvAddItems.DataSource = dt;
        gvAddItems.DataBind();

    }
    private void AddNewRowToBillGrid()
    {
        int rowIndex = 0;

        if (ViewState["CurrentAddItemTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentAddItemTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values
                    Label box1 = (Label)gvAddItems.Rows[rowIndex].Cells[1].FindControl("lblSno");
                    TextBox box2 = (TextBox)gvAddItems.Rows[rowIndex].Cells[2].FindControl("txtItmName");
                    TextBox box3 = (TextBox)gvAddItems.Rows[rowIndex].Cells[3].FindControl("txtQty");
                    DropDownList box4 = (DropDownList)gvAddItems.Rows[rowIndex].Cells[4].FindControl("ddlUnit");
                    TextBox box5 = (TextBox)gvAddItems.Rows[rowIndex].Cells[5].FindControl("txtRate");
                    Label box6 = (Label)gvAddItems.Rows[rowIndex].Cells[6].FindControl("lblAmt");
                    DropDownList box7 = (DropDownList)gvAddItems.Rows[rowIndex].Cells[7].FindControl("ddlMatType");
                    DropDownList box8 = (DropDownList)gvAddItems.Rows[rowIndex].Cells[2].FindControl("ddlMat");
                    TextBox box9 = (TextBox)gvAddItems.Rows[rowIndex].Cells[3].FindControl("txtStocEntry");

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["SerialNumber"] = i + 1;
                    drCurrentRow["ItemName"] = box2.Text;
                    drCurrentRow["Quantity"] = box3.Text;
                    drCurrentRow["Unit"] = box4.SelectedValue;
                    drCurrentRow["Rate"] = box5.Text;
                    drCurrentRow["Amount"] = box6.Text;
                    drCurrentRow["MaterialType"] = box7.SelectedValue;
                    drCurrentRow["Material"] = box8.SelectedValue;
                    drCurrentRow["StockEntryNo"] = box9.Text;

                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentAddItemTable"] = dtCurrentTable;

                gvAddItems.DataSource = dtCurrentTable;
                gvAddItems.DataBind();


            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

        //Set Previous Data on Postbacks
        SetBillPreviousData();
    }
    private void SetBillPreviousData()
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
                    TextBox box2 = (TextBox)gvAddItems.Rows[rowIndex].Cells[2].FindControl("txtRemark");
                    TextBox box3 = (TextBox)gvAddItems.Rows[rowIndex].Cells[3].FindControl("txtQty");
                    DropDownList box4 = (DropDownList)gvAddItems.Rows[rowIndex].Cells[4].FindControl("ddlUnit");
                    TextBox box5 = (TextBox)gvAddItems.Rows[rowIndex].Cells[5].FindControl("txtRate");
                    Label box6 = (Label)gvAddItems.Rows[rowIndex].Cells[6].FindControl("lblAmt");
                    DropDownList box7 = (DropDownList)gvAddItems.Rows[rowIndex].Cells[7].FindControl("ddlMatType");
                    DropDownList box8 = (DropDownList)gvAddItems.Rows[rowIndex].Cells[2].FindControl("ddlMat");
                    TextBox box9 = (TextBox)gvAddItems.Rows[rowIndex].Cells[3].FindControl("txtStocEntry");

                    box1.Text = dt.Rows[i]["SerialNumber"].ToString();
                    box2.Text = dt.Rows[i]["ItemName"].ToString();
                    box3.Text = dt.Rows[i]["Quantity"].ToString();
                    box4.Text = dt.Rows[i]["Unit"].ToString();
                    box5.Text = dt.Rows[i]["Rate"].ToString();
                    box6.Text = dt.Rows[i]["Amount"].ToString();
                    box7.Text = dt.Rows[i]["MaterialType"].ToString();
                    box8.Text = dt.Rows[i]["Material"].ToString();
                    box9.Text = dt.Rows[i]["StockEntryNo"].ToString();

                    rowIndex++;
                }
                ViewState["CurrentEstimateTable"] = dt;
            }
        }
    }
    protected void BindNameOfWork(string id)
    {
        DataSet dsWa = new DataSet();
        dsWa = DAL.DalAccessUtility.GetDataInDataSet("select WAId,WorkAllotName from WorkAllot where AcaId ='" + id + "' and Active=1");
        ddlNameOfWork.DataSource = dsWa;
        ddlNameOfWork.DataValueField = "WAId";
        ddlNameOfWork.DataTextField = "WorkAllotName";
        ddlNameOfWork.DataBind();
        ddlNameOfWork.Items.Insert(0, "Select Name Of Work");
        ddlNameOfWork.SelectedIndex = 0;
    }
    protected void BindBillType()
    {
        DataSet dsBillType = new DataSet();
        dsBillType = DAL.DalAccessUtility.GetDataInDataSet("select BillTypeId,BillTypeName from BillType where Active=1");
        ddlBillType.DataSource = dsBillType;
        ddlBillType.DataValueField = "BillTypeId";
        ddlBillType.DataTextField = "BillTypeName";
        ddlBillType.DataBind();
        ddlBillType.Items.Insert(0, "Select Bill Type");
        ddlBillType.SelectedIndex = 0;
    }
    protected void BindEstimate()
    {
        string AcaId = Request.QueryString["AcaId"];
        DataSet dsAcademy = new DataSet();
        dsAcademy = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateNo '" + AcaId + "'");
        ddlEsimate.DataSource = dsAcademy;
        ddlEsimate.DataValueField = "EstId";
        ddlEsimate.DataTextField = "EstNo";
        ddlEsimate.DataBind();
        ddlEsimate.Items.Insert(0, "Select Estimate ");
        ddlEsimate.SelectedIndex = 0;
    }
    protected void ddlBillType1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBillType1.SelectedValue == "1")
        {
            //BindEstimate();
            //ddlEsimate.Visible = true;
            //lblChargeable.Visible = true;
            //ddlBillType.Visible = false;
            //pnlSanction.Visible = true;
            //pnlNonSanction.Visible = false;
            //btnAmtTotal.Visible = false;
            //btnTtlSanctioned.Visible = true;
            pnlEstimateDetails.Visible = false;

            btnAmtTotal.Visible = false;
            pnlNonSanction.Visible = false;
            lblNameWork.Visible = true;
            ddlNameOfWork.Visible = true;
        }
        else if (ddlBillType1.SelectedValue == "2")
        {
            txtBillDes.Text = "";
            txtBillDes.Enabled = true;
            lblChargeable.Visible = true;
            lblNameWork.Visible = true;
            ddlBillType.Visible = true;
            ddlEsimate.Visible = false;
            pnlSanction.Visible = false;
            pnlNonSanction.Visible = true;
            pnlEstimateDetails.Visible = true;
            trMatSelect.Visible = false;
            btnAmtTotal.Visible = true;
            btnTtlSanctioned.Visible = false;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Chargeable to.');", true);
        }
    }
    protected void BindMat()
    {
        DataSet dsAcademy = new DataSet();
        pnlEstimateDetails.Visible = false;
        //dsAcademy = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowMatAndUnit '" + ddlEsimate.SelectedValue + "'");
        //dsAcademy = DAL.DalAccessUtility.GetDataInDataSet("exec USP_getEstimateBalance '" + ddlEsimate.SelectedValue + "','1'");
        dsAcademy = DAL.DalAccessUtility.GetDataInDataSet("exec USP_getEstimateBalanceNew'" + ddlNameOfWork.SelectedValue + "','1'");
        if (dsAcademy.Tables[0].Rows.Count > 0)
        {
            //pnlEstimateDetails.Visible = true;
        }

        GridView1.DataSource = dsAcademy;
        GridView1.DataBind();
        ViewState["MaterialBalance"] = dsAcademy.Tables[0];
    }
    protected void ddlEsimate_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsDes = new DataSet();
        dsDes = DAL.DalAccessUtility.GetDataInDataSet("SELECT Estimate.SubEstimate, WorkAllot.WorkAllotName FROM Estimate INNER JOIN WorkAllot ON Estimate.WAId = WorkAllot.WAId where EstId='" + ddlEsimate.SelectedValue + "'");
        //txtBillDes.Enabled = false;
        //txtBillDes.Text = dsDes.Tables[0].Rows[0]["SubEstimate"].ToString();
        trMatSelect.Visible = true;
        lblNameOfWork.Visible = true;
        lblNameWork.Visible = true;
        lblNameOfWork.Text = dsDes.Tables[0].Rows[0]["WorkAllotName"].ToString();
        BindMat();
    }
    protected void gvAddItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlMateType = (DropDownList)e.Row.FindControl("ddlMatType");
            DataSet dsMatType = new DataSet();
            dsMatType = DAL.DalAccessUtility.GetDataInDataSet("select MatTypeId,MatTypeName from MaterialType where Active=1");
            ddlMateType.DataSource = dsMatType;
            ddlMateType.DataValueField = "MatTypeId";
            ddlMateType.DataTextField = "MatTypeName";
            ddlMateType.DataBind();
            ddlMateType.Items.Insert(0, "Material Type");
            ddlMateType.SelectedIndex = 0;
        }

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    DropDownList ddlUni = (DropDownList)e.Row.FindControl("ddlUnit");
        //    DataSet dsUnit = new DataSet();
        //    dsUnit = DAL.DalAccessUtility.GetDataInDataSet("SELECT UnitId,UnitName FROM Unit where Active=1");
        //    ddlUni.DataSource = dsUnit;
        //    ddlUni.DataValueField = "UnitId";
        //    ddlUni.DataTextField = "UnitName";
        //    ddlUni.DataBind();
        //    ddlUni.Items.Insert(0, "Unit");
        //    ddlUni.SelectedIndex = 0;
        //}
    }
    protected void ddlMatType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList ddlMateType = (DropDownList)row.FindControl("ddlMatType");
        DropDownList ddlMaterail = (DropDownList)row.FindControl("ddlMat");
        TextBox txtItemN = (TextBox)row.FindControl("txtItmName");
        //DropDownList ddlUni = (DropDownList)row.FindControl("ddlUnit");
        TextBox txtUni = (TextBox)row.FindControl("txtUnit");
        if (ddlMateType.SelectedItem.Text == "OTHERS")
        {
            ddlMaterail.Visible = false;
            txtItemN.Visible = true;
            // ddlUni.Visible = false;
            txtUni.Visible = true;
        }
        else
        {
            DataSet dsMat = new DataSet();
            dsMat = DAL.DalAccessUtility.GetDataInDataSet("select MatId,MatName from Material where Active=1 and MatTypeId='" + ddlMateType.SelectedValue + "'");
            ddlMaterail.DataSource = dsMat;
            ddlMaterail.DataValueField = "MatId";
            ddlMaterail.DataTextField = "MatName";
            ddlMaterail.DataBind();
            ddlMaterail.Items.Insert(0, "Material");
            ddlMaterail.SelectedIndex = 0;
        }
    }
    protected void ddlMat_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList ddlMaterail = (DropDownList)row.FindControl("ddlMat");
        TextBox txtItem = (TextBox)row.FindControl("txtItmName");
        Label lblUnit = (Label)row.FindControl("lblNonSUnit");
        DataSet dsUnit = DAL.DalAccessUtility.GetDataInDataSet("SELECT Unit.UnitName FROM Material INNER JOIN Unit ON Material.UnitId = Unit.UnitId where Material.MatId='" + ddlMaterail.SelectedValue + "'");
        lblUnit.Text = dsUnit.Tables[0].Rows[0]["UnitName"].ToString();
        if (ddlMaterail.SelectedItem.Text == "OTHERS")
        {
            ddlMaterail.Visible = false;
            txtItem.Visible = true;
        }
        else
        {
            ddlMaterail.Visible = true;
            txtItem.Visible = false;
        }
    }
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtQt = (TextBox)row.FindControl("txtQty");
        TextBox txtRa = (TextBox)row.FindControl("txtRate");
        Label lblAm = (Label)row.FindControl("lblAmt");
        //Label lblMId = (Label)row.FindControl("txtMatId");
        decimal qt = Convert.ToDecimal(txtQt.Text);
        decimal ra = Convert.ToDecimal(txtRa.Text);
        //decimal mi = Convert.ToDecimal(lblMId.Text);
        decimal am = qt * ra;
        lblAm.Text = am.ToString();
    }
    protected void ButtonAdd2_Click(object sender, EventArgs e)
    {
        AddNewRowToBillGrid();
    }
    protected void ddlBillType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBillType.SelectedValue == "1")
        {
            ddlNameOfWork.Visible = true;
        }
        else if (ddlBillType.SelectedValue == "4")
        {
            ddlNameOfWork.Visible = true;
        }
        else if (ddlBillType.SelectedValue == "5")
        {
            ddlNameOfWork.Visible = true;
        }
        else if (ddlBillType.SelectedValue == "6")
        {
            ddlNameOfWork.Visible = true;
        }
        else if (ddlBillType.SelectedValue == "7")
        {
            ddlNameOfWork.Visible = true;
        }
        else if (ddlBillType.SelectedValue == "8")
        {
            ddlNameOfWork.Visible = true;
        }
        else if (ddlBillType.SelectedValue == "9")
        {
            ddlNameOfWork.Visible = true;
        }
        else
        {
            //ddlNameOfWork.SelectedValue = "0";
        }
    }
    private void SetInitialRowEstimateDataLoad()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("SerialNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("GateEntryNo", typeof(string)));
        dt.Columns.Add(new DataColumn("StockEntryNo", typeof(string)));
        dt.Columns.Add(new DataColumn("MatId", typeof(string)));
        dt.Columns.Add(new DataColumn("MatName", typeof(string)));
        dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
        dt.Columns.Add(new DataColumn("UnitId", typeof(string)));
        dt.Columns.Add(new DataColumn("UnitName", typeof(string)));
        dt.Columns.Add(new DataColumn("Rate", typeof(string)));
        dt.Columns.Add(new DataColumn("Amount", typeof(string)));
        dt.Columns.Add(new DataColumn("Remark", typeof(string)));
        dt.Columns.Add(new DataColumn("EstQty", typeof(string)));
        dt.Columns.Add(new DataColumn("EstRate", typeof(string)));
        dt.Columns.Add(new DataColumn("BalQty", typeof(string)));


        dr = dt.NewRow();
        dr["SerialNumber"] = 1;
        dr["GateEntryNo"] = "";
        dr["StockEntryNo"] = "";
        dr["MatId"] = "";
        dr["MatName"] = "";
        dr["Quantity"] = "";
        dr["UnitId"] = "";
        dr["UnitName"] = "";
        dr["Rate"] = "";
        dr["Amount"] = "";
        dr["Remark"] = "";
        dr["EstQty"] = "";
        dr["EstRate"] = "";
        dr["BalQty"] = "";

        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentEstimateTable"] = dt;

        gvAddItems2.DataSource = dt;
        gvAddItems2.DataBind();

    }
    protected void btnShowData_Click(object sender, EventArgs e)
    {
        //string data = "";
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("MatId", typeof(string)));
        dt.Columns.Add(new DataColumn("MatName", typeof(string)));
        dt.Columns.Add(new DataColumn("UnitId", typeof(string)));
        dt.Columns.Add(new DataColumn("UnitName", typeof(string)));
        dt.Columns.Add(new DataColumn("EstQty", typeof(string)));
        dt.Columns.Add(new DataColumn("EstRate", typeof(string)));
        dt.Columns.Add(new DataColumn("BalQty", typeof(string)));
        foreach (GridViewRow row in GridView1.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                //CheckBox chkRow = (row.Cells[0].FindControl("chkCtrl") as CheckBox);
                CheckBox chkRow = (CheckBox)row.Cells[0].FindControl("chkCtrl");
                HiddenField hdnMatID = (HiddenField)row.Cells[0].FindControl("hdnMatID");
                HiddenField hdnUnitId = (HiddenField)row.Cells[0].FindControl("hdnUnitId");
                if (chkRow.Checked)
                {
                    dr = dt.NewRow();
                    dr["MatId"] = hdnMatID.Value;
                    dr["MatName"] = row.Cells[1].Text;
                    dr["UnitId"] = hdnUnitId.Value;
                    dr["UnitName"] = row.Cells[2].Text;
                    dr["EstQty"] = row.Cells[3].Text;
                    dr["EstRate"] = row.Cells[4].Text;
                    dr["BalQty"] = row.Cells[5].Text;
                    dt.Rows.Add(dr);
                    //data = data + dr["MatName"] + ",";
                }
                //dr["UnitId"] = row.Cells[3].Text;
                //dr["UnitName"] = row.Cells[4].Text;
            }

        }
        dt.AcceptChanges();
        //Session["Data"] = dt;
        //lblData.Text = "You Select " + data;
        SetInitialRowEstimateDataLoad();
        gvAddItems2.DataSource = dt;
        gvAddItems2.DataBind();
        trMatSelect.Visible = false;
        pnlEstimateDetails.Visible = true;
        // pnlSanction.Visible = false;
    }
    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtQt = (TextBox)row.FindControl("txtQty");
        //TextBox txtRa = (TextBox)row.FindControl("txtRate");
        HiddenField lblMId = (HiddenField)row.FindControl("txtMatId");
        decimal qt = Convert.ToDecimal(txtQt.Text);
        //decimal ra = Convert.ToDecimal(txtRa.Text);
        decimal mi = Convert.ToDecimal(lblMId.Value);
        DataSet dsValidation = new DataSet();
        //dsValidation = DAL.DalAccessUtility.GetDataInDataSet("exec USP_QtyAndRateValidation '" + lblMId.Text + "','" + ddlEsimate.SelectedValue + "','" + lblUser.Text + "',''");
        //dsValidation = DAL.DalAccessUtility.GetDataInDataSet("exec USP_QtyAndRateValidation_New '" + ddlEsimate.SelectedValue + "','" + lblMId.Text + "','1'");
        //decimal EstQty = Convert.ToDecimal(dsValidation.Tables[0].Rows[0]["EstBal"].ToString());

        DataTable dtMaterialBalance = ViewState["MaterialBalance"] as DataTable;
        var results = (from myRow in dtMaterialBalance.AsEnumerable()
                       where myRow.Field<int>("MatID") == int.Parse(lblMId.Value)
                       select myRow).ToList();


        decimal EstQty = Convert.ToDecimal(results[0]["EstBal"].ToString());

        if (EstQty < qt)
        {
            btnSave.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You are entering more than estimate Quantity.');", true);
            txtQt.Text = "";
        }
        else
        {
            btnSave.Enabled = true;
        }

    }
    protected void txtRateSan_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtQt = (TextBox)row.FindControl("txtQty");
        TextBox txtRa = (TextBox)row.FindControl("txtRateSan");
        Label lblAm = (Label)row.FindControl("lblAmtSan");
        HiddenField lblMId = (HiddenField)row.FindControl("txtMatId");
        decimal qt = Convert.ToDecimal(txtQt.Text);
        decimal ra = Convert.ToDecimal(txtRa.Text);
        decimal mi = Convert.ToDecimal(lblMId.Value);
        decimal am = qt * ra;
        lblAm.Text = am.ToString();
        DataSet dsValidation = new DataSet();
        //dsValidation = DAL.DalAccessUtility.GetDataInDataSet("exec USP_QtyAndRateValidation '" + lblMId.Text + "','" + ddlEsimate.SelectedValue + "','" + lblUser.Text + "',''");
        //dsValidation = DAL.DalAccessUtility.GetDataInDataSet("exec USP_QtyAndRateValidation_New '" + ddlEsimate.SelectedValue + "','" + lblMId.Text + "','1'");
        //decimal EstRate = Convert.ToDecimal(dsValidation.Tables[0].Rows[0]["Rate"].ToString());

        DataTable dtMaterialBalance = ViewState["MaterialBalance"] as DataTable;
        var results = (from myRow in dtMaterialBalance.AsEnumerable()
                       where myRow.Field<int>("MatID") == int.Parse(lblMId.Value)
                       select myRow).ToList();


        decimal EstRate = Convert.ToDecimal(results[0]["Rate"].ToString());


        if (ra > EstRate)
        {
            btnSave.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You are entering more than estimate Rate. ');", true);
            txtRa.Text = "";
        }
        else
        {
            btnSave.Enabled = true;
        }
    }
    protected void btnAmtTotal_Click(object sender, EventArgs e)
    {
        decimal TtlAmt = 0;
        int rowindex = 0;
        foreach (GridViewRow gvrow in gvAddItems.Rows)
        {
            Label lblam = (Label)gvAddItems.Rows[rowindex].FindControl("lblAmt");
            TtlAmt = TtlAmt + Convert.ToDecimal(lblam.Text);
            rowindex = rowindex + 1;
        }
        lblEstimateCost.Text = Convert.ToString(TtlAmt);
    }
    protected void btnTtlSanctioned_Click(object sender, EventArgs e)
    {
        decimal TtlAmt = 0;
        int rowindex = 0;
        foreach (GridViewRow gvrow in gvAddItems2.Rows)
        {
            Label lblam = (Label)gvAddItems2.Rows[rowindex].FindControl("lblAmtSan");
            TtlAmt = TtlAmt + Convert.ToDecimal(lblam.Text);
            rowindex = rowindex + 1;
        }
        lblEstimateCost.Text = Convert.ToString(TtlAmt);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnTtlSanctioned_Click(btnTtlSanctioned, new EventArgs());
        System.Threading.Thread.Sleep(1000);
        string AcaId = Request.QueryString["AcaId"];
        DataSet dsZ = new DataSet();
        dsZ = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId from Academy where AcaId='" + AcaId + "'");
        string ZoneId = dsZ.Tables[0].Rows[0]["ZoneId"].ToString();
        if (ddlBillType1.SelectedValue == "2")
        {
            //if (ddlBillType.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Chargable To.');", true);
            //}
            if (txtBillDate.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Bill Date.');", true);
            }
            else if (txtGateEntryNo.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Gate Entry no.');", true);
            }

            else if (txtAgencyName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Agency Name.');", true);
            }
            else if (lblEstimateCost.Text == "00.00")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please click on Total Amount Button');", true);
            }
            else
            {

                if (BillID > 0)
                {
                    DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewSubmitBillByUser " + BillID + ",'" + ddlBillType1.SelectedValue + "','" + txtBillDate.Text + "','" + txtGateEntryNo.Text + "','','" + txtAgencyName.Text + "','" + txtBillDes.Text + "','" + lblEstimateCost.Text + "','" + txtRemark.Text + "','2','1','" + lblUser.Text + "','" + ddlEsimate.SelectedValue + "','" + AcaId + "','" + ZoneId + "','','" + ddlBillType1.SelectedItem.Text + "','" + ddlNameOfWork.SelectedValue + "'");
                }
                else
                {
                    DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewSubmitBillByUser '','" + ddlBillType1.SelectedValue + "','" + txtBillDate.Text + "','" + txtGateEntryNo.Text + "','','" + txtAgencyName.Text + "','" + txtBillDes.Text + "','" + lblEstimateCost.Text + "','" + txtRemark.Text + "','1','1','" + lblUser.Text + "','" + ddlEsimate.SelectedValue + "','" + AcaId + "','" + ZoneId + "','','" + ddlBillType1.SelectedItem.Text + "','" + ddlNameOfWork.SelectedValue + "'");
                }

                string MaterialType, Material, Itm, Qty, Unit, Rate, Amount, StockEntryNo, Uni;
                int rowindex = 0;
                foreach (GridViewRow gvrow in gvAddItems.Rows)
                {
                    DropDownList ddlmt = (DropDownList)gvAddItems.Rows[rowindex].FindControl("ddlMatType");
                    MaterialType = ddlmt.SelectedValue;
                    DropDownList ddlma = (DropDownList)gvAddItems.Rows[rowindex].FindControl("ddlMat");
                    Material = ddlma.SelectedValue;
                    TextBox txtItem = (TextBox)gvAddItems.Rows[rowindex].FindControl("txtItmName");
                    Itm = txtItem.Text;
                    TextBox txtqt = (TextBox)gvAddItems.Rows[rowindex].FindControl("txtQty");
                    Qty = txtqt.Text;
                    //DropDownList ddlun = (DropDownList)gvAddItems.Rows[rowindex].FindControl("ddlUnit");
                    //Unit = ddlun.Text;
                    Label lblUn = (Label)gvAddItems.Rows[rowindex].FindControl("lblNonSUnit");
                    Unit = lblUn.Text;
                    TextBox txtUn = (TextBox)gvAddItems.Rows[rowindex].FindControl("txtUnit");
                    Uni = txtUn.Text;
                    TextBox txtra = (TextBox)gvAddItems.Rows[rowindex].FindControl("txtRate");
                    Rate = txtra.Text;
                    Label lblam = (Label)gvAddItems.Rows[rowindex].FindControl("lblAmt");
                    Amount = lblam.Text;
                    TextBox txtST = (TextBox)gvAddItems.Rows[rowindex].FindControl("txtStocEntry");
                    StockEntryNo = txtST.Text;
                    DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewMaterialAndUnitByUser '" + Uni + "','" + lblUser.Text + "','" + Itm + "','" + MaterialType + "'");
                    if (txtItem.Text != "")
                    {
                        DataSet dsMUT = DAL.DalAccessUtility.GetDataInDataSet("select MatId,MatTypeId,UnitId from Material where MatName='" + Itm + "'");

                        if (BillID > 0)
                        {
                            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewSubmitBillByUserAndMaterialOthersRelations '','','" + dsMUT.Tables[0].Rows[0]["MatTypeId"].ToString() + "','" + dsMUT.Tables[0].Rows[0]["MatId"].ToString() + "','" + Itm + "','" + Qty + "','" + dsMUT.Tables[0].Rows[0]["UnitId"].ToString() + "','" + Rate + "','" + Amount + "','" + lblUser.Text + "','2','1','','" + StockEntryNo + "',''");
                        }
                        else
                        {
                            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewSubmitBillByUserAndMaterialOthersRelations '','','" + dsMUT.Tables[0].Rows[0]["MatTypeId"].ToString() + "','" + dsMUT.Tables[0].Rows[0]["MatId"].ToString() + "','" + Itm + "','" + Qty + "','" + dsMUT.Tables[0].Rows[0]["UnitId"].ToString() + "','" + Rate + "','" + Amount + "','" + lblUser.Text + "','1','1','','" + StockEntryNo + "',''");
                        }
                    }
                    else
                    {
                        DataSet dsUnitId = DAL.DalAccessUtility.GetDataInDataSet("select UnitId from Unit where UnitName='" + Unit + "'");
                        if (BillID > 0)
                        {
                            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewSubmitBillByUserAndMaterialOthersRelations '','','" + MaterialType + "','" + Material + "','" + Itm + "','" + Qty + "','" + dsUnitId.Tables[0].Rows[0]["UnitId"].ToString() + "','" + Rate + "','" + Amount + "','" + lblUser.Text + "','2','1','','" + StockEntryNo + "',''");
                        }
                        else
                        {
                            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewSubmitBillByUserAndMaterialOthersRelations '','','" + MaterialType + "','" + Material + "','" + Itm + "','" + Qty + "','" + dsUnitId.Tables[0].Rows[0]["UnitId"].ToString() + "','" + Rate + "','" + Amount + "','" + lblUser.Text + "','1','1','','" + StockEntryNo + "',''");
                        }
                        
                    }
                    rowindex = rowindex + 1;
                }
                gvAddItems.DataSource = null;
                gvAddItems.DataBind();
                SetInitialRowBiils();

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Submitted Successfully.');", true);

            }
        }
        else if (ddlBillType1.SelectedValue == "1")
        {
            //if (ddlBillType.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Chargable To.');", true);
            //}
            if (txtBillDate.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Bill Date.');", true);
            }
            else if (txtBillDes.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Bill Description.');", true);
            }

            else if (txtGateEntryNo.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Gate Entry no.');", true);
            }

            else if (txtAgencyName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Agency Name.');", true);
            }
            else if (lblEstimateCost.Text == "00.00")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please click on Total Amount Button');", true);
            }
            else
            {
                //DataSet dsNameOfWork = DAL.DalAccessUtility.GetDataInDataSet("select WAId from WorkAllot where WorkAllotName ='" + lblNameOfWork.Text + "'");
                //DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewSubmitBillByUser '','" + ddlBillType1.SelectedValue + "','" + txtBillDate.Text + "','" + txtGateEntryNo.Text + "','','" + txtAgencyName.Text + "','" + txtBillDes.Text + "','" + lblEstimateCost.Text + "','" + txtRemark.Text + "','1','1','" + lblUser.Text + "','" + ddlEsimate.SelectedValue + "','" + AcaId + "','" + ZoneId + "','','" + ddlBillType1.SelectedItem.Text + "','" + dsNameOfWork.Tables[0].Rows[0]["WAId"].ToString() + "'");


                if (BillID > 0)
                {
                    DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewSubmitBillByUser " + BillID + ",'" + ddlBillType1.SelectedValue + "','" + txtBillDate.Text + "','" + txtGateEntryNo.Text + "','','" + txtAgencyName.Text + "','" + txtBillDes.Text + "','" + lblEstimateCost.Text + "','" + txtRemark.Text + "','2','1','" + lblUser.Text + "','" + -1 + "','" + AcaId + "','" + ZoneId + "','','" + ddlBillType1.SelectedItem.Text + "','" + ddlNameOfWork.SelectedValue + "'");
                }
                else
                {
                    DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewSubmitBillByUser '','" + ddlBillType1.SelectedValue + "','" + txtBillDate.Text + "','" + txtGateEntryNo.Text + "','','" + txtAgencyName.Text + "','" + txtBillDes.Text + "','" + lblEstimateCost.Text + "','" + txtRemark.Text + "','1','1','" + lblUser.Text + "','" + -1 + "','" + AcaId + "','" + ZoneId + "','','" + ddlBillType1.SelectedItem.Text + "','" + ddlNameOfWork.SelectedValue + "'");
                }


                string MatId, Material, Qty, UnitId, UnitName, Rate, Amount, StockEntryNo, EstQty1, EstRate1;
                int rowindex = 0;
                foreach (GridViewRow gvrow in gvAddItems2.Rows)
                {

                    HiddenField lblMatId = (HiddenField)gvAddItems2.Rows[rowindex].FindControl("txtMatId");
                    MatId = lblMatId.Value;
                    Label lblMatName = (Label)gvAddItems2.Rows[rowindex].FindControl("txtMatName");
                    Material = lblMatName.Text;

                    TextBox txtqt = (TextBox)gvAddItems2.Rows[rowindex].FindControl("txtQty");
                    Qty = txtqt.Text;
                    //UnitId = gvAddItems2.Rows[0].Cells[4].Text;
                    //UnitName = gvAddItems2.Rows[0].Cells[5].Text;
                    HiddenField lblUnitId = (HiddenField)gvAddItems2.Rows[rowindex].FindControl("txtUnitId");
                    UnitId = lblUnitId.Value;
                    Label lblUnitName = (Label)gvAddItems2.Rows[rowindex].FindControl("txtUnitName");
                    UnitName = lblUnitName.Text;
                    TextBox txtra = (TextBox)gvAddItems2.Rows[rowindex].FindControl("txtRateSan");
                    Rate = txtra.Text;
                    Label lblam = (Label)gvAddItems2.Rows[rowindex].FindControl("lblAmtSan");
                    Amount = lblam.Text;
                    TextBox txtST = (TextBox)gvAddItems2.Rows[rowindex].FindControl("txtStockEntry");
                    StockEntryNo = txtST.Text;
                    Label lblEstQty = (Label)gvAddItems2.Rows[rowindex].FindControl("lblEstQty");
                    EstQty1 = lblEstQty.Text;
                    Label lblEstRate = (Label)gvAddItems2.Rows[rowindex].FindControl("lblEstRate");
                    EstRate1 = lblam.Text;
                    HiddenField hdnSno = (HiddenField)gvrow.FindControl("hdnSno");
                    //DataSet dsQtyValidation = new DataSet();
                    //dsQtyValidation = DAL.DalAccessUtility.GetDataInDataSet("exec USP_QtyValidation '" + MatId + "','" + ddlEsimate.SelectedValue + "'");
                    //if (Convert.ToInt64(dsQtyValidation.Tables[0].Rows[0]["op"].ToString()) == 1)
                    //{
                    //   ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You are entering more than estimate Quantity.');", true);
                    //}
                    //DataSet dsValidation = new DataSet();
                    //dsValidation = DAL.DalAccessUtility.GetDataInDataSet("exec USP_QtyAndRateValidation '" + MatId + "','" + ddlEsimate.SelectedValue + "','" + lblUser.Text + "',''");
                    //dsValidation = DAL.DalAccessUtility.GetDataInDataSet("exec USP_QtyAndRateValidation_New '" + ddlEsimate.SelectedValue + "','" + MatId + "','1'");
                    //decimal EstQty = Convert.ToDecimal(dsValidation.Tables[0].Rows[0]["EstBal"].ToString());
                    decimal q = Convert.ToDecimal(Qty);
                    //decimal EstRate = Convert.ToDecimal(dsValidation.Tables[0].Rows[0]["Rate"].ToString());
                    decimal r = Convert.ToDecimal(Rate);
                    //if (EstQty < q)
                    //{
                    //   ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You are entering more than estimate Quantity.');", true);

                    //}
                    //else if (EstRate < r)
                    //{
                    //   ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You are entering more than estimate Rate.');", true);

                    //}

                    //else
                    //{

                    if (BillID > 0)
                    {
                        DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewSubmitBillByUserAndMaterialOthersRelations " + hdnSno.Value + "," + BillID + ",'','" + MatId + "','" + Material + "','" + Qty + "','" + UnitId + "','" + Rate + "','" + Amount + "','" + lblUser.Text + "','2','1','','" + StockEntryNo + "','" + ddlNameOfWork.SelectedValue + "'");
                    }
                    else
                    {
                        DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewSubmitBillByUserAndMaterialOthersRelations '','','','" + MatId + "','" + Material + "','" + Qty + "','" + UnitId + "','" + Rate + "','" + Amount + "','" + lblUser.Text + "','1','1','','" + StockEntryNo + "','" + ddlNameOfWork.SelectedValue + "'");
                    }
                    //}

                    rowindex = rowindex + 1;
                }
                gvAddItems2.DataSource = null;
                gvAddItems2.DataBind();
                SetInitialRowEstimateDataLoad();

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Submitted Successfully.');", true);
            }
        }
        else
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Chargeable to.');", true);
        }
        //select MAX(SubBillId)as NewBillId from SubmitBillByUser where CreatedBy='tani'
        DataSet dsBillId = DAL.DalAccessUtility.GetDataInDataSet("select MAX(SubBillId)as NewBillId from SubmitBillByUser where CreatedBy='" + lblUser.Text + "'");
        DataSet dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_MsgContent '" + dsBillId.Tables[0].Rows[0]["NewBillId"].ToString() + "'");
        string msg = "New " + dsBillDetails.Tables[0].Rows[0]["BillType"].ToString() + " bill for " + dsBillDetails.Tables[0].Rows[0]["AcaName"].ToString() + " academy issued by Mr. " + dsBillDetails.Tables[0].Rows[0]["InName"].ToString() + ",  Bill No. is " + dsBillId.Tables[0].Rows[0]["NewBillId"].ToString() + " ";
        //DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsg '" + lblUser.Text + "','" + dsCreatedBy.Tables[0].Rows[0]["CreatedBy"].ToString() + "','"+ Msg +"')";
        DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendMsgToAdmin '" + lblUser.Text + "','" + msg + "'");
        Response.Redirect("Emp_BillDetails.aspx?BillId=" + dsBillId.Tables[0].Rows[0]["NewBillId"].ToString() + "");

    }

    protected void ddlNameOfWork_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsDes = new DataSet();
        dsDes = DAL.DalAccessUtility.GetDataInDataSet("SELECT Estimate.SubEstimate, WorkAllot.WorkAllotName FROM Estimate INNER JOIN WorkAllot ON Estimate.WAId = WorkAllot.WAId where Estimate.WAId='" + ddlNameOfWork.SelectedValue + "'");
        //txtBillDes.Enabled = false;
        //txtBillDes.Text = dsDes.Tables[0].Rows[0]["SubEstimate"].ToString();
        //trMatSelect.Visible = true;
        //lblNameOfWork.Visible = true;
        //lblNameWork.Visible = true;
        //lblNameOfWork.Text = dsDes.Tables[0].Rows[0]["WorkAllotName"].ToString();
        trMatSelect.Visible = true;
        BindMat();
    }

    protected void ShowBillDetails(string ID)
    {
        DataSet dsBill = new DataSet();
        dsBill = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AdminBillViewByBillId_V2 '" + ID + "'");

        ddlBillType1.Items.FindByText(dsBill.Tables[0].Rows[0]["BillType"].ToString()).Selected = true;
        ddlBillType1_SelectedIndexChanged(ddlBillType1, new EventArgs());
        //lblBillType.Text = dsBill.Tables[0].Rows[0]["BillTypeName"].ToString();
        ddlNameOfWork.ClearSelection();
        ddlNameOfWork.Items.FindByValue(dsBill.Tables[0].Rows[0]["WAId"].ToString()).Selected = true;
        txtBillDes.Text = dsBill.Tables[0].Rows[0]["BillDescr"].ToString();
        txtAgencyName.Text = dsBill.Tables[0].Rows[0]["AgencyName"].ToString();
        txtBillDate.Text = dsBill.Tables[0].Rows[0]["BillDate"].ToString();
        txtGateEntryNo.Text = dsBill.Tables[0].Rows[0]["GateEntryNo"].ToString();

        GetAllMaterials(dsBill.Tables[0].Rows[0]["WAId"].ToString(), dsBill.Tables[2]);

    }

    private void GetAllMaterials(string WorkAllotID, DataTable dtBilledMaterials)
    {
        DataSet dsAcademy = new DataSet();
        pnlEstimateDetails.Visible = false;
        dsAcademy = DAL.DalAccessUtility.GetDataInDataSet("exec USP_getEstimateBalanceNew'" + WorkAllotID + "','1'");

        DataTable dt = new DataTable();
        DataRow dr = null;
        
        dt.Columns.Add(new DataColumn("MatId", typeof(string)));
        dt.Columns.Add(new DataColumn("MatName", typeof(string)));
        dt.Columns.Add(new DataColumn("UnitId", typeof(string)));
        dt.Columns.Add(new DataColumn("UnitName", typeof(string)));
        dt.Columns.Add(new DataColumn("EstQty", typeof(string)));
        dt.Columns.Add(new DataColumn("EstRate", typeof(string)));
        dt.Columns.Add(new DataColumn("BalQty", typeof(string)));


        for (int i = 0; i < dtBilledMaterials.Rows.Count; i++)
        {
            var results = (from myRow in dsAcademy.Tables[0].AsEnumerable()
                           where myRow.Field<int>("MatID") == int.Parse(dtBilledMaterials.Rows[i]["MatID"].ToString())
                           select myRow).ToList();

            dr = dt.NewRow();

            
            dr["MatId"] = results[0]["MatId"].ToString();
            dr["MatName"] = results[0]["MatName"].ToString();
            dr["UnitId"] = results[0]["UnitId"].ToString();
            dr["UnitName"] = results[0]["UnitName"].ToString();
            dr["EstQty"] = results[0]["Quantity"].ToString();
            dr["EstRate"] = results[0]["Rate"].ToString();
            dr["BalQty"] = results[0]["EstBal"].ToString();
            dt.Rows.Add(dr);
        }

        dt.AcceptChanges();
        //SetInitialRowEstimateDataLoad();
        gvAddItems2.DataSource = dt;
        gvAddItems2.DataBind();

        double balanceQuantity = 0;

        foreach (GridViewRow gvrow in gvAddItems2.Rows)
        {

            var results = (from myRow in dtBilledMaterials.AsEnumerable()
                           where myRow.Field<int>("MatID") == int.Parse(((HiddenField)gvrow.FindControl("txtMatId")).Value)
                           select myRow).ToList();

            balanceQuantity = double.Parse(((Label)gvrow.FindControl("lblBalQty")).Text);
            if (balanceQuantity > double.Parse(results[0]["Qty"].ToString()))
            {
                ((TextBox)gvrow.FindControl("txtQty")).Text = results[0]["Qty"].ToString();
            }
            ((TextBox)gvrow.FindControl("txtRateSan")).Text = results[0]["Rate"].ToString();
            ((TextBox)gvrow.FindControl("txtStockEntry")).Text = results[0]["StockEntryNo"].ToString();
            ((Label)gvrow.FindControl("lblAmtSan")).Text = results[0]["Amount"].ToString();
            ((HiddenField)gvrow.FindControl("hdnSno")).Value = results[0]["sno"].ToString();
        }

        pnlNonSanction.Visible = false;
        btnAmtTotal.Visible = false;
        trMatSelect.Visible = false;
        pnlEstimateDetails.Visible = true;

        ViewState["MaterialBalance"] = dsAcademy.Tables[0];
    }
}