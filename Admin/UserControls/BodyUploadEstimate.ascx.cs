using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;

public partial class Admin_UserControls_BodyUploadEstimate : System.Web.UI.UserControl
{

    //public HtmlTableRow trEstimateNo { get; set; }
    //public HtmlTableRow trZone { get; set; }
    public static int UserTypeID = -1;
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

            if (Session["UserTypeID"] != null)
            {
                UserTypeID = int.Parse(Session["UserTypeID"].ToString());
            }

            //if (UserTypeID == 2)
            //{
            //    trEstimateNo.Visible = false;
            //    trZone.Visible = false;
            //}


            //SetInitialRowEstimate();
            BindListMaterialTypes();
            //FirstGridViewRow();
            BindZone();
            BindTypeOfWork();
        }
    }
    private void FirstGridViewRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Col1", typeof(string)));
        dt.Columns.Add(new DataColumn("Col2", typeof(string)));
        dt.Columns.Add(new DataColumn("Col3", typeof(string)));
        dt.Columns.Add(new DataColumn("Col4", typeof(string)));
        dt.Columns.Add(new DataColumn("Col5", typeof(string)));
        dt.Columns.Add(new DataColumn("Col6", typeof(string)));
        dt.Columns.Add(new DataColumn("Col7", typeof(string)));
        dt.Columns.Add(new DataColumn("Col8", typeof(string)));
        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Col1"] = string.Empty;
        dr["Col2"] = string.Empty;
        dr["Col3"] = string.Empty;
        dr["Col4"] = string.Empty;
        dr["Col5"] = string.Empty;
        dr["Col6"] = string.Empty;
        dr["Col7"] = string.Empty;
        dr["Col8"] = string.Empty;
        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;


        grvStudentDetails.DataSource = dt;
        grvStudentDetails.DataBind();

        Button btnAdd = (Button)grvStudentDetails.FooterRow.Cells[5].FindControl("ButtonAdd");
        Page.Form.DefaultFocus = btnAdd.ClientID;


    }
    private void AddNewRow()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList box2 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("ddlMatType");
                    DropDownList box3 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("ddlMat");
                    TextBox box4 = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("txtQty");
                    Label box5 = (Label)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("lblUnit");
                    TextBox box6 = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[6].FindControl("txtRate");
                    Label box7 = (Label)grvStudentDetails.Rows[rowIndex].Cells[7].FindControl("lblAmt");
                    TextBox box8 = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[8].FindControl("txtRemark");
                    DropDownList box9 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[9].FindControl("ddlSourceType");
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Col1"] = box2.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = box3.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col3"] = box4.Text;
                    //dtCurrentTable.Rows[i - 1]["Col4"] = box5.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col4"] = box5.Text;
                    dtCurrentTable.Rows[i - 1]["Col5"] = box6.Text;
                    dtCurrentTable.Rows[i - 1]["Col6"] = box7.Text;
                    dtCurrentTable.Rows[i - 1]["Col7"] = box8.Text;
                    dtCurrentTable.Rows[i - 1]["Col8"] = box9.SelectedValue;
                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;

                grvStudentDetails.DataSource = dtCurrentTable;
                grvStudentDetails.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData();
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < grvStudentDetails.Rows.Count; i++)
                {
                    DropDownList box2 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("ddlMatType");
                    DropDownList box3 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("ddlMat");
                    TextBox box4 = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("txtQty");
                    Label box5 = (Label)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("lblUnit");
                    TextBox box6 = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[6].FindControl("txtRate");
                    Label box7 = (Label)grvStudentDetails.Rows[rowIndex].Cells[7].FindControl("lblAmt");
                    TextBox box8 = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[8].FindControl("txtRemark");
                    DropDownList box9 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[9].FindControl("ddlSourceType");

                    box2.SelectedValue = dt.Rows[i]["Col1"].ToString();
                    //BindMaterial
                    BindMaterialTypes(box2, box3);
                    //End Bind Material
                    box3.SelectedIndex = box3.Items.IndexOf(box3.Items.FindByValue(dt.Rows[i]["Col2"].ToString().Trim()));
                    box4.Text = dt.Rows[i]["Col3"].ToString();
                    box5.Text = dt.Rows[i]["Col4"].ToString();
                    box6.Text = dt.Rows[i]["Col5"].ToString();
                    box7.Text = dt.Rows[i]["Col6"].ToString();
                    box8.Text = dt.Rows[i]["Col7"].ToString();
                    box9.SelectedValue = dt.Rows[i]["Col8"].ToString();
                    rowIndex++;
                }
                ViewState["CurrentTable"] = dt;
            }
        }
    }

    public void BindMaterialTypes(DropDownList MatType, DropDownList Materrial)
    {

        DataSet dsMat = new DataSet();

        if (ViewState["MatTypeID"] == null || ViewState["MatTypeID"].ToString() != MatType.SelectedValue)
        {
            ViewState["MatTypeID"] = MatType.SelectedValue;
            Session["dsMat"] = dsMat = DAL.DalAccessUtility.GetDataInDataSet("select MatId,MatName from Material where Active=1 and MatTypeId='" + MatType.SelectedValue + "'");
        }
        else
        {
            dsMat = Session["dsMat"] as DataSet;
        }

        Materrial.DataSource = dsMat;
        Materrial.DataValueField = "MatId";
        Materrial.DataTextField = "MatName";
        Materrial.DataBind();
        Materrial.Items.Insert(0, "Material");
        Materrial.SelectedIndex = 0;
    }
    protected void grvStudentDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowData();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable"] = dt;
                grvStudentDetails.DataSource = dt;
                grvStudentDetails.DataBind();

                for (int i = 0; i < grvStudentDetails.Rows.Count - 1; i++)
                {
                    grvStudentDetails.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousData();
            }
        }
    }
    private void SetRowData()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList box2 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("ddlMatType");
                    DropDownList box3 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("ddlMat");
                    TextBox box4 = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("txtQty");
                    Label box5 = (Label)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("lblUnit");
                    TextBox box6 = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[6].FindControl("txtRate");
                    Label box7 = (Label)grvStudentDetails.Rows[rowIndex].Cells[7].FindControl("lblAmt");
                    TextBox box8 = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[8].FindControl("txtRemark");
                    DropDownList box9 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[9].FindControl("ddlSourceType");
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Col1"] = box2.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = box3.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col3"] = box4.Text;
                    dtCurrentTable.Rows[i - 1]["Col4"] = box5.Text;
                    dtCurrentTable.Rows[i - 1]["Col5"] = box6.Text;
                    dtCurrentTable.Rows[i - 1]["Col6"] = box7.Text;
                    dtCurrentTable.Rows[i - 1]["Col7"] = box8.Text;
                    dtCurrentTable.Rows[i - 1]["Col8"] = box9.SelectedValue;
                    rowIndex++;
                }
                ViewState["CurrentTable"] = dtCurrentTable;
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //SetPreviousData();
    }
    protected void BindTypeOfWork()
    {
        DataSet dsZone = new DataSet();
        dsZone = DAL.DalAccessUtility.GetDataInDataSet("select TypeWorkId,TypeWorkName from TypeOfWork where Active=1");
        ddlTypeOfWork.DataSource = dsZone;
        ddlTypeOfWork.DataValueField = "TypeWorkId";
        ddlTypeOfWork.DataTextField = "TypeWorkName";
        ddlTypeOfWork.DataBind();
        ddlTypeOfWork.Items.Insert(0, "Select Type Of Work");
        ddlTypeOfWork.SelectedIndex = 0;
    }
    protected void BindZone()
    {
        DataSet dsZone = new DataSet();
        if (UserTypeID == 1)
        {
            dsZone = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId,ZoneName  from Zone where Active=1");
        }
        else
        {
            dsZone = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_GetZoneByUserID] '" + Session["InchargeID"] + "'");
        }
        ddlZone.DataSource = dsZone;
        ddlZone.DataValueField = "ZoneId";
        ddlZone.DataTextField = "ZoneName";
        ddlZone.DataBind();
        ddlZone.Items.Insert(0, "Select Zone");
        ddlZone.SelectedIndex = 0;
    }
    protected void BindWork()
    {

        DataSet dsWork = new DataSet();
        dsWork = DAL.DalAccessUtility.GetDataInDataSet("select WAId,WorkAllotName from WorkAllot where AcaId='" + ddlAcademy.SelectedValue + "' and ZoneId='" + ddlZone.SelectedValue + "' and active=1");
        ddlWorkAllot.DataSource = dsWork;
        ddlWorkAllot.DataValueField = "WAId";
        ddlWorkAllot.DataTextField = "WorkAllotName";
        ddlWorkAllot.DataBind();
        ddlWorkAllot.Items.Insert(0, "Select Work Allot");
        ddlWorkAllot.SelectedIndex = 0;
    }
    protected void BindAcademy()
    {
        DataSet dsAca = new DataSet();
        dsAca = DAL.DalAccessUtility.GetDataInDataSet("select AcaId,AcaName from Academy where Active=1 and ZoneId='" + ddlZone.SelectedValue + "'");
        ddlAcademy.DataSource = dsAca;
        ddlAcademy.DataValueField = "AcaId";
        ddlAcademy.DataTextField = "AcaName";
        ddlAcademy.DataBind();
        ddlAcademy.Items.Insert(0, "Select Academy");
        ddlAcademy.SelectedIndex = 0;
    }
    private void getGridData()
    {
        int rowIndex = 0;
        DataTable dt = new DataTable();
        dt.Columns.Add("RowNumber");
        dt.Columns.Add("Col1");
        dt.Columns.Add("Col2");
        dt.Columns.Add("Col3");
        dt.Columns.Add("Col4");
        dt.Columns.Add("Col5");
        dt.Columns.Add("Col6");
        dt.Columns.Add("Col7");
        dt.Columns.Add("Col8");
        for (int i = 0; i < grvStudentDetails.Rows.Count; i++)
        {
            DropDownList box2 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("ddlMatType");
            DropDownList box3 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("ddlMat");
            TextBox box4 = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("txtQty");
            //DropDownList box5 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("ddlUnit");
            Label box5 = (Label)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("lblUnit");
            TextBox box6 = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[6].FindControl("txtRate");
            Label box7 = (Label)grvStudentDetails.Rows[rowIndex].Cells[7].FindControl("lblAmt");
            TextBox box8 = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[8].FindControl("txtRemark");
            DropDownList box9 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[9].FindControl("ddlSourceType");

            DataRow dr = null;
            dr = dt.NewRow();
            dr["RowNumber"] = rowIndex + 1;
            dr["Col1"] = box2.SelectedIndex;
            dr["Col2"] = box3.SelectedIndex;
            dr["Col3"] = box4.Text;
            dr["Col4"] = box5.Text;
            dr["Col5"] = box6.Text;
            dr["Col6"] = box7.Text;
            dr["Col7"] = box8.Text;
            dr["Col8"] = box9.SelectedIndex;
            dt.Rows.Add(dr);
            rowIndex++;
        }
        ViewState["CurrentTable"] = dt;
        dt = null;
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        //SetPreviousData();
        getGridData();
        AddNewRow();
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAcademy();
        DataSet dsZCode = new DataSet();
        dsZCode = DAL.DalAccessUtility.GetDataInDataSet("select ZoId from Zone where ZoneId='" + ddlZone.SelectedValue + "'");
        lblZoneCode.Text = dsZCode.Tables[0].Rows[0]["ZoId"].ToString();
    }
    protected void ddlAcademy_SelectedIndexChanged(object sender, EventArgs e)
    {
        tdWorkAllot.Visible = true;
        BindWork();
        DataSet dsACode = new DataSet();
        dsACode = DAL.DalAccessUtility.GetDataInDataSet("select AcId from Academy where Active=1 and ZoneId='" + ddlZone.SelectedValue + "'");
        lblAcaCode.Text = dsACode.Tables[0].Rows[0]["AcId"].ToString();
    }
    protected void grvStudentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlMateType = (DropDownList)e.Row.FindControl("ddlMatType");
            DataSet dsMatType = new DataSet();

            dsMatType = ViewState["dsMatType"] as DataSet;
            if (dsMatType == null)
            {
                dsMatType = DAL.DalAccessUtility.GetDataInDataSet("select MatTypeId,MatTypeName from MaterialType where Active=1 and MatTypeName<>'OTHERS'");
                ViewState["dsMatType"] = dsMatType;
            }
            ddlMateType.DataSource = dsMatType;
            ddlMateType.DataValueField = "MatTypeId";
            ddlMateType.DataTextField = "MatTypeName";
            ddlMateType.DataBind();
            ddlMateType.Items.Insert(0, "Material Type");
            ddlMateType.SelectedIndex = 0;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlSourceType = (DropDownList)e.Row.FindControl("ddlSourceType");
            DataSet dsSourcType = new DataSet();
            dsSourcType = ViewState["dsSourcType"] as DataSet;
            if (dsSourcType == null)
            {
                dsSourcType = DAL.DalAccessUtility.GetDataInDataSet("select PSId,PSName from PurchaseSource where Active=1");
                ViewState["dsSourcType"] = dsSourcType;
            }

            ddlSourceType.DataSource = dsSourcType;
            ddlSourceType.DataValueField = "PSId";
            ddlSourceType.DataTextField = "PSName";
            ddlSourceType.DataBind();
            ddlSourceType.Items.Insert(0, "Source Type");
            ddlSourceType.SelectedIndex = 0;
        }
    }
    protected void ddlMatType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList ddlMateType = (DropDownList)row.FindControl("ddlMatType");
        DropDownList ddlMaterail = (DropDownList)row.FindControl("ddlMat");
        BindMaterialTypes(ddlMateType, ddlMaterail);
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
    protected void btnSubEstimate_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(1000);
        Estimate estimate = new Estimate();
        EstimateAndMaterialOthersRelations estimateRelation = null;
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct SubEstimate,ZoneId,AcaId from Estimate where SubEstimate='" + txtSubEstimate.Text + "' and ZoneId='" + ddlZone.SelectedValue + "' and AcaId='" + ddlAcademy.SelectedValue + "'");
        if (dsExist.Tables[0].Rows.Count > 1)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Same Sub Estimate's name already assigned to selected Zone and Academy');", true);
        }
        else
        {
            if (ddlZone.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Zone.');", true);
            }
            if (ddlAcademy.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Academy.');", true);
            }
            if (ddlTypeOfWork.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Type of Work.');", true);
            }
            if (txtSubEstimate.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Sanction Date.');", true);
            }
            //if (txtSanctionDate.Text == "")
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Sub Estimate/ Title.');", true);
            //}
            if (txtFileName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter File Name.');", true);
            }
            if (!ValidateMaterials())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter the material details carefully.');", true);
            }
            else
            {
                string fileNameToSave = string.Empty;
                foreach (HttpPostedFile postedFile in fuFile.PostedFiles)
                {
                    string fileDwgname = System.IO.Path.GetFileName(postedFile.FileName);
                    string fileDwgPath = System.IO.Path.GetFileName(postedFile.FileName);
                    string FileDwgEx = System.IO.Path.GetExtension(postedFile.FileName);
                    String FDwgNam = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName);
                    Int64 i = 0;
                    fileDwgPath = "~/EstFile/" + fileDwgname;
                    string dwgfilepath = "EstFile/" + Regex.Replace(FDwgNam + "-" + System.DateTime.Now.ToString(), @"[^0-9a-zA-Z]+", "-").Replace(' ', '-').ToString() + FileDwgEx;
                    postedFile.SaveAs(Server.MapPath(dwgfilepath));
                    dwgfilepath = dwgfilepath + ",";

                    fileNameToSave += dwgfilepath;
                }

                fileNameToSave = fileNameToSave.Substring(0, fileNameToSave.Length - 1);
                bool isApproved = Session["UserTypeID"] != null && Session["UserTypeID"].ToString() == "1" ? true : false;
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + UserTypeID + "');", true);

                estimate.ZoneId = int.Parse(ddlZone.SelectedValue);
                estimate.AcaId = int.Parse(ddlAcademy.SelectedValue);
                estimate.SubEstimate = txtSubEstimate.Text;
                estimate.TypeWorkId = int.Parse(ddlTypeOfWork.SelectedValue);
                estimate.CreatedOn = DateTime.Now;
                estimate.ModifyOn = DateTime.Now;
                estimate.CreatedBy = int.Parse(Session["InchargeID"].ToString());
                estimate.Active = 1;
                estimate.EstmateCost = Convert.ToDecimal(lblTtlAmtAfterGrid.Text);
                estimate.WAId = int.Parse(ddlWorkAllot.SelectedValue);
                estimate.FileNme = txtFileName.Text;
                estimate.FilePath = fileNameToSave;
                estimate.IsApproved = isApproved;
                estimate.IsRejected = false;
                estimate.EstimateAndMaterialOthersRelations = new List<EstimateAndMaterialOthersRelations>();

                string MaterialType, Material, SourceType, Qty, Unit, Rate, Amount, Remark;
                int rowindex = 0;
                bool isPurchase = false;
                bool isWorkshop = false;
                foreach (GridViewRow gvrow in grvStudentDetails.Rows)
                {
                    estimateRelation = new EstimateAndMaterialOthersRelations();
                    DropDownList ddlmt = (DropDownList)grvStudentDetails.Rows[rowindex].FindControl("ddlMatType");
                    MaterialType = ddlmt.SelectedValue;
                    DropDownList ddlma = (DropDownList)grvStudentDetails.Rows[rowindex].FindControl("ddlMat");
                    Material = ddlma.SelectedValue;
                    TextBox txtqt = (TextBox)grvStudentDetails.Rows[rowindex].FindControl("txtQty");
                    Qty = txtqt.Text;
                    //DropDownList ddlun = (DropDownList)grvStudentDetails.Rows[rowindex].FindControl("ddlUnit");

                    Label ddlun = (Label)grvStudentDetails.Rows[rowindex].FindControl("lblUnit");
                    DataSet dsUnitId = DAL.DalAccessUtility.GetDataInDataSet("select UnitId from unit where UnitName='" + ddlun.Text + "'");
                    Unit = dsUnitId.Tables[0].Rows[0]["UnitId"].ToString();
                    TextBox txtra = (TextBox)grvStudentDetails.Rows[rowindex].FindControl("txtRate");
                    Rate = txtra.Text;
                    Label lblam = (Label)grvStudentDetails.Rows[rowindex].FindControl("lblAmt");
                    if (Rate == "")
                    {
                        Rate = "0";
                    }
                    Amount = (Convert.ToDecimal(Qty) * Convert.ToDecimal(Rate)).ToString();  //lblam.Text;
                    TextBox txtre = (TextBox)grvStudentDetails.Rows[rowindex].FindControl("txtRemark");
                    Remark = txtre.Text;
                    DropDownList ddlSo = (DropDownList)grvStudentDetails.Rows[rowindex].FindControl("ddlSourceType");
                    SourceType = ddlSo.SelectedValue;
                    if (SourceType == "2")
                    {
                        isPurchase = true;
                    }
                    if (SourceType == "3")
                    {
                        isWorkshop = true;
                    }
                    estimateRelation.EstId = estimate.EstId;
                    estimateRelation.MatId = int.Parse(Material);
                    estimateRelation.MatTypeId = int.Parse(MaterialType);
                    estimateRelation.PSId = int.Parse(SourceType);
                    estimateRelation.Qty = decimal.Parse(Qty);
                    estimateRelation.UnitId = int.Parse(Unit);
                    estimateRelation.Rate = decimal.Parse(Rate);
                    estimateRelation.Amount = decimal.Parse(Amount);
                    estimateRelation.Remark = Remark;
                    estimateRelation.CreatedBy = int.Parse(Session["InchargeID"].ToString());
                    estimateRelation.CreatedOn = DateTime.Now;
                    estimateRelation.ModifyOn = DateTime.Now;
                    estimateRelation.Active = 1;
                    estimateRelation.IsApproved = true;
                    estimate.EstimateAndMaterialOthersRelations.Add(estimateRelation);

                    rowindex = rowindex + 1;
                }

                ConstructionUserRepository repo = new ConstructionUserRepository(new AkalAcademy.DataContext());
                repo.SaveEstimate(estimate);

                grvStudentDetails.DataSource = null;
                grvStudentDetails.DataBind();

                if (Session["UserTypeID"] != null && Session["UserTypeID"].ToString() == "1")
                {
                    SendSMS(ddlAcademy.SelectedValue, isPurchase, isWorkshop, isApproved);
                }
                else
                {
                    SendSMS(ddlAcademy.SelectedValue, isPurchase, isWorkshop, isApproved);
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Estimate Create Successfully.');", true);
                DataSet dsEst = DAL.DalAccessUtility.GetDataInDataSet("select max(EstId) as ei from Estimate");

                if (Session["UserTypeID"] != null && Session["UserTypeID"].ToString() == "1")
                {
                    Response.Redirect("Admin_ParticularEstimateView.aspx?EstId=" + dsEst.Tables[0].Rows[0]["ei"].ToString() + "");
                }
                else
                {
                    Response.Redirect("Emp_ParticularEstimateView.aspx?EstId=" + dsEst.Tables[0].Rows[0]["ei"].ToString() + "");
                }
            }
        }

    }

    private void SendSMS(string AcaID, bool IsPurchase, bool IsWorkshop, bool IsApproved)
    {
        const int CONST_USERTYPE = 2;
        const int PURC_USERTYPE = 4;
        const int WORKSHOP_USERTYPE = 6;

        string smsTo = string.Empty;
        string adminNumber = System.Configuration.ConfigurationManager.AppSettings["AdminToSendDrawingSMS"].ToString();
        if (IsApproved)
        {
            InchargeController conrtoller = new InchargeController();
            List<string> incharges = conrtoller.GetUsersByUserTypeAndAcademic(CONST_USERTYPE, int.Parse(AcaID));
            foreach (string inchargeNumber in incharges)
            {
                smsTo += inchargeNumber + ",";
            }


            if (adminNumber != string.Empty)
            {
                smsTo += adminNumber + ",";
            }
            List<Incharge> InchargeInfo = conrtoller.GetUsersByUserType(PURC_USERTYPE);

            // Purchase 
            if (IsPurchase)
            {
                foreach (Incharge inchargeNumber in InchargeInfo)
                {
                    smsTo += inchargeNumber.InMobile + ",";
                }
            }

            if (IsWorkshop)
            {
                InchargeInfo = conrtoller.GetUsersByUserType(WORKSHOP_USERTYPE);
                foreach (Incharge inchargeNumber in InchargeInfo)
                {
                    smsTo += inchargeNumber.InMobile + ",";
                }
            }
            smsTo = smsTo.Substring(0, smsTo.Length - 1);
            Utility.SendSMS(smsTo, "New Estimate for " + ddlAcademy.SelectedItem.Text + " has been uploaded to www.Akalsewa.org.");
        }
        else
        {
            Utility.SendSMS(adminNumber, "New Non Approved Estimate for " + ddlAcademy.SelectedItem.Text + " has been uploaded to www.Akalsewa.org.");
        }
    }

    protected void btnAmtTotal_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(1000);
        decimal TtlAmt = 0;
        foreach (GridViewRow gvrow in grvStudentDetails.Rows)
        {
            TextBox txtQty = (TextBox)gvrow.FindControl("txtQty");
            TextBox txtRate = (TextBox)gvrow.FindControl("txtRate");

            Label lblam = (Label)gvrow.FindControl("lblAmt");
            if (txtRate.Text == "")
            {
                txtRate.Text = "0.00";
            }
            lblam.Text = (Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtRate.Text)).ToString();
            TtlAmt += Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtRate.Text);
        }
        lblEstimateCost.Text = Convert.ToString(TtlAmt);
        lblTtlAmtAfterGrid.Text = Convert.ToString(TtlAmt);
    }
    protected void ddlWorkAllot_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblWorkNameReflect.Visible = true;
        lblWorkNameReflect.Text = ddlWorkAllot.SelectedItem.Text;
    }
    protected void ddlMat_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList ddlMaterail = (DropDownList)row.FindControl("ddlMat");
        Label lblUni = (Label)row.FindControl("lblUnit");
        DataSet dsMat = new DataSet();
        dsMat = DAL.DalAccessUtility.GetDataInDataSet("exec USP_UnitName  '" + ddlMaterail.SelectedValue + "'");
        lblUni.Text = dsMat.Tables[0].Rows[0]["UnitName"].ToString();
    }

    protected void lstMaterialTypes_SelectedIndexChanged(object sender, EventArgs e)
    {

        string values = String.Join(", ", lstMaterialTypes.Items.Cast<ListItem>()
                                                  .Where(i => i.Selected)
                                                  .Select(i => i.Value));

        DataSet dsMat = new DataSet();
        dsMat = DAL.DalAccessUtility.GetDataInDataSet("select MatId,MatName from Material where Active=1 and MatTypeId IN (" + values + ") order by MatName");
        lstMaterials.DataSource = dsMat;
        lstMaterials.DataValueField = "MatId";
        lstMaterials.DataTextField = "MatName";
        lstMaterials.DataBind();


    }

    private void SetDefaultMaterialTypes()
    {
        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        DropDownList ddlMatType = null;
        DropDownList ddlMat = null;

        string values = String.Join(", ", lstMaterials.Items.Cast<ListItem>()
                                                 .Where(i => i.Selected)
                                                 .Select(i => i.Value));
        string[] IDs = values.Split(',');
        int length = grvStudentDetails.Rows.Count == 1 ? IDs.Length - 1 : IDs.Length;

        //getGridData();

        int indexValue = grvStudentDetails.Rows.Count;

        for (int i = 0; i < IDs.Length; i++)
        {
            if (grvStudentDetails.Rows.Count == 0)
            {
                FirstGridViewRow();
                trCost.Visible = true;
                trTotal.Visible = true;
            }
            else
            {
                AddNewRow();
            }

            ddlMatType = new DropDownList();
            ddlMat = new DropDownList();
            ddlMatType = (DropDownList)grvStudentDetails.Rows[indexValue].Cells[2].FindControl("ddlMatType");
            ddlMat = (DropDownList)grvStudentDetails.Rows[indexValue].Cells[3].FindControl("ddlMat");
            DataSet dsMat = new DataSet();
            dsMat = DAL.DalAccessUtility.GetDataInDataSet("select mt.MatTypeId from Material m inner join MaterialType mt on mt.MatTypeId = m.MatTypeId where m.MatId = " + IDs[i] + "");

            ddlMatType.ClearSelection();
            ddlMatType.Items.FindByValue(dsMat.Tables[0].Rows[0]["MatTypeId"].ToString()).Selected = true;
            ddlMatType_SelectedIndexChanged(ddlMatType, new EventArgs());

            ddlMat.ClearSelection();
            ddlMat.Items.FindByValue(IDs[i].Trim().ToString()).Selected = true;
            ddlMat_SelectedIndexChanged(ddlMat, new EventArgs());
            indexValue++;
        }
    }

    protected void btnloadMaterials_Click(object sender, EventArgs e)
    {
        SetDefaultMaterialTypes();
    }

    private void BindListMaterialTypes()
    {
        DataSet dsMatType = new DataSet();
        dsMatType = ViewState["dsMatType"] as DataSet;
        if (dsMatType == null)
        {
            dsMatType = DAL.DalAccessUtility.GetDataInDataSet("select MatTypeId,MatTypeName from MaterialType where Active=1 and MatTypeName<>'OTHERS' order by MatTypeName");
            ViewState["dsMatType"] = dsMatType;
        }

        lstMaterialTypes.DataSource = dsMatType;
        lstMaterialTypes.DataValueField = "MatTypeId";
        lstMaterialTypes.DataTextField = "MatTypeName";
        lstMaterialTypes.DataBind();
    }
    protected void ddlSourceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList ddlST = (DropDownList)row.FindControl("ddlSourceType");
        DropDownList ddlMat = (DropDownList)row.FindControl("ddlMat");
        TextBox txtRate = (TextBox)row.FindControl("txtRate");

        if (ddlST.SelectedValue == "2")
        {
            DataSet dsMat = new DataSet();
            dsMat = DAL.DalAccessUtility.GetDataInDataSet("select MatCost from Material where MatId = " + ddlMat.SelectedValue + "");
            if (dsMat != null && dsMat.Tables[0] != null && dsMat.Tables[0].Rows[0]["MatCost"].ToString() != "")
            {
                txtRate.Text = dsMat.Tables[0].Rows[0]["MatCost"].ToString();
            }
        }
        else
        {
            txtRate.Text = string.Empty;
        }
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtRate = (TextBox)row.FindControl("txtRate");
        if (txtRate.Text != "")
        {
            txtRate_TextChanged(txtRate, new EventArgs());
        }
    }

    public bool ValidateMaterials()
    {
        bool sflag = true;
        foreach (GridViewRow row in grvStudentDetails.Rows)
        {
            DropDownList ddlMatType = (DropDownList)row.FindControl("ddlMatType");
            DropDownList ddlMat = (DropDownList)row.FindControl("ddlMat");
            TextBox txtQty = (TextBox)row.FindControl("txtQty");
            Label lblUnit = (Label)row.FindControl("lblUnit");
            TextBox txtRate = (TextBox)row.FindControl("txtRate");
            Label lblAmt = (Label)row.FindControl("lblAmt");
            DropDownList ddlSourceType = (DropDownList)row.FindControl("ddlSourceType");


            if (ddlMatType.SelectedValue == "Material Type" || ddlMat.SelectedValue == "Material" || txtQty.Text == "" || txtRate.Text == ""
                || ddlSourceType.SelectedValue == "Source Type")
            {
                sflag = false;
                break;
            }

        }
        return sflag;
    }
}