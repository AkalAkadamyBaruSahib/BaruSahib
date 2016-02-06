using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FirstGridViewRow();
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
                    DropDownList box5 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("ddlUnit");
                    TextBox box6 = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[6].FindControl("txtRate");
                    Label box7 = (Label)grvStudentDetails.Rows[rowIndex].Cells[7].FindControl("lblAmt");
                    TextBox box8 = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[8].FindControl("txtRemark");
                    DropDownList box9 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[9].FindControl("ddlSourceType");
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Col1"] = box2.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = box3.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col3"] = box4.Text;
                    dtCurrentTable.Rows[i - 1]["Col4"] = box5.SelectedValue;
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

                //TextBox txn = (TextBox)grvStudentDetails.Rows[1].Cells[1].FindControl("txtName");
                //txn.Focus();
                // txn.Focus;
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
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList box2 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[2].FindControl("ddlMatType");
                    DropDownList box3 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[3].FindControl("ddlMat");
                    TextBox box4 = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[4].FindControl("txtQty");
                    DropDownList box5 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("ddlUnit");
                    TextBox box6 = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[6].FindControl("txtRate");
                    Label box7 = (Label)grvStudentDetails.Rows[rowIndex].Cells[7].FindControl("lblAmt");
                    TextBox box8 = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[8].FindControl("txtRemark");
                    DropDownList box9 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[9].FindControl("ddlSourceType");

                    box2.SelectedValue = dt.Rows[i]["Col1"].ToString();
                    //box3.SelectedValue = dt.Rows[i]["Col2"].ToString();
                    //BindMaterial
                    DataSet dsMat = new DataSet();
                    dsMat = DAL.DalAccessUtility.GetDataInDataSet("select MatId,MatName from Material where Active=1 and MatTypeId='" + box2.SelectedValue + "'");
                    box3.DataSource = dsMat;
                    box3.DataValueField = "MatId";
                    box3.DataTextField = "MatName";
                    box3.DataBind();
                    box3.Items.Insert(0, "Material");
                    box3.SelectedIndex = 0;
                    //End Bind Material
                    box3.SelectedIndex = box3.Items.IndexOf(box3.Items.FindByValue(dt.Rows[i]["Col2"].ToString().Trim()));
                    box4.Text = dt.Rows[i]["Col3"].ToString();
                    box5.SelectedValue = dt.Rows[i]["Col4"].ToString();
                    box6.Text = dt.Rows[i]["Col5"].ToString();
                    box7.Text = dt.Rows[i]["Col6"].ToString();
                    box8.Text = dt.Rows[i]["Col7"].ToString();
                    box9.SelectedValue = dt.Rows[i]["Col8"].ToString();
                    rowIndex++;
                }
                
                //ViewState["CurrentTable"] = dt;
                grvStudentDetails.DataSource = dt;
                grvStudentDetails.DataBind();
            }
        }
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRow();
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
                //SetPreviousData();
            }
        }
        SetPreviousData();
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
                    DropDownList box5 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[5].FindControl("ddlUnit");
                    TextBox box6 = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[6].FindControl("txtRate");
                    Label box7 = (Label)grvStudentDetails.Rows[rowIndex].Cells[7].FindControl("lblAmt");
                    TextBox box8 = (TextBox)grvStudentDetails.Rows[rowIndex].Cells[8].FindControl("txtRemark");
                    DropDownList box9 = (DropDownList)grvStudentDetails.Rows[rowIndex].Cells[9].FindControl("ddlSourceType");
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Col1"] = box2.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = box3.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col3"] = box4.Text;
                    dtCurrentTable.Rows[i - 1]["Col4"] = box5.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col5"] = box6.Text;
                    dtCurrentTable.Rows[i - 1]["Col6"] = box7.Text;
                    dtCurrentTable.Rows[i - 1]["Col7"] = box8.Text;
                    dtCurrentTable.Rows[i - 1]["Col8"] = box9.SelectedValue;
                    rowIndex++;
                }
                //dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;
                //grvStudentDetails.DataSource = dtCurrentTable;
                //grvStudentDetails.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SetRowData();
            DataTable table = ViewState["CurrentTable"] as DataTable;

            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    string dlMType = row.ItemArray[1] as string;
                    string dlMat = row.ItemArray[2] as string;
                    string txQty = row.ItemArray[3] as string;
                    string dlUnit = row.ItemArray[4] as string;
                    string txRate = row.ItemArray[5] as string;
                    string txAmt = row.ItemArray[6] as string;
                    string txRemark = row.ItemArray[7] as string;
                    string dlStype = row.ItemArray[8] as string;


                    if (dlMType != null ||
                        dlMat != null ||
                        txQty != null ||
                        dlUnit != null ||
                        txRate != null ||
                        txAmt != null ||
                        txRemark != null ||
                        dlStype != null)
                    {
                        // Do whatever is needed with this data, 
                        // Possibily push it in database
                        // I am just printing on the page to demonstrate that it is working.
                        Response.Write(string.Format("{0} {1} {2} {3} {4} {5} {6} {7}<br/>", dlMType, dlMat, txQty, dlUnit, txRate, txAmt, txRemark, dlStype));
                    }

                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    protected void grvStudentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlSourceType = (DropDownList)e.Row.FindControl("ddlSourceType");
            DataSet dsSourcType = new DataSet();
            dsSourcType = DAL.DalAccessUtility.GetDataInDataSet("select PSId,PSName from PurchaseSource where Active=1");
            ddlSourceType.DataSource = dsSourcType;
            ddlSourceType.DataValueField = "PSId";
            ddlSourceType.DataTextField = "PSName";
            ddlSourceType.DataBind();
            ddlSourceType.Items.Insert(0, "Source Type");
            ddlSourceType.SelectedIndex = 0;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlUni = (DropDownList)e.Row.FindControl("ddlUnit");
            DataSet dsUnit = new DataSet();
            dsUnit = DAL.DalAccessUtility.GetDataInDataSet("SELECT UnitId,UnitName FROM Unit where Active=1");
            ddlUni.DataSource = dsUnit;
            ddlUni.DataValueField = "UnitId";
            ddlUni.DataTextField = "UnitName";
            ddlUni.DataBind();
            ddlUni.Items.Insert(0, "Unit");
            ddlUni.SelectedIndex = 0;
        }
    }
    protected void ddlMatType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList ddlMateType = (DropDownList)row.FindControl("ddlMatType");
        DropDownList ddlMaterail = (DropDownList)row.FindControl("ddlMat");
        DataSet dsMat = new DataSet();
        dsMat = DAL.DalAccessUtility.GetDataInDataSet("select MatId,MatName from Material where Active=1 and MatTypeId='" + ddlMateType.SelectedValue + "'");
        ddlMaterail.DataSource = dsMat;
        ddlMaterail.DataValueField = "MatId";
        ddlMaterail.DataTextField = "MatName";
        ddlMaterail.DataBind();
        ddlMaterail.Items.Insert(0, "Material");
        ddlMaterail.SelectedIndex = 0;
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
}
