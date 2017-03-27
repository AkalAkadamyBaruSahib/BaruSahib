using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MaterialBillQuantutyReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindZone();
        }
    }
    protected void BindZone()
    {
        DataTable dsZone = new DataTable();

        dsZone = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId,ZoneName  from Zone where Active=1").Tables[0];
        if (dsZone != null)
        {
            ddlZone.DataSource = dsZone;
            ddlZone.DataValueField = "ZoneId";
            ddlZone.DataTextField = "ZoneName";
            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("--Select Zone--", "0"));
            ddlZone.SelectedIndex = 0;
        }
    }

    protected void BindAcademy()
    {
        DataTable dsAca = new DataTable();
        dsAca = DAL.DalAccessUtility.GetDataInDataSet("select AcaId,AcaName from Academy where Active=1 and ZoneId='" + ddlZone.SelectedValue + "'").Tables[0];
        if (dsAca != null)
        {
            ddlAcademy.DataSource = dsAca;
            ddlAcademy.DataValueField = "AcaId";
            ddlAcademy.DataTextField = "AcaName";
            ddlAcademy.DataBind();
            ddlAcademy.Items.Insert(0, new ListItem("--Select Academy--", "0"));
            ddlAcademy.SelectedIndex = 0;
        }
    }

    protected void BindNameOfWork()
    {
        DataTable dsWa = new DataTable();
        dsWa = DAL.DalAccessUtility.GetDataInDataSet("select WAId,WorkAllotName from WorkAllot where AcaId ='" + ddlAcademy.SelectedValue + "' and Active=1").Tables[0];
        if (dsWa != null && dsWa.Rows.Count > 0)
        {
            ddlNameOfWork.DataSource = dsWa;
            ddlNameOfWork.DataValueField = "WAId";
            ddlNameOfWork.DataTextField = "WorkAllotName";
            ddlNameOfWork.DataBind();
            ddlNameOfWork.Items.Insert(0, new ListItem("--Select Name Of Work--", "0"));
            ddlNameOfWork.SelectedIndex = 0;
        }
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAcademy();
    }

    protected void ddlAcademy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindNameOfWork();
    }

    protected DataTable BindDatatable()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        dt = GetBillReportByNameOFWork();
        return dt;

    }


    protected void btnDownload_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "BillQuantityReportByNameOFWork(" + ddlNameOfWork.SelectedItem + ").xls"));
        Response.ContentType = "application/ms-excel";
        DataTable dt = BindDatatable();
        string str = string.Empty;
        foreach (DataColumn dtcol in dt.Columns)
        {
            Response.Write(str + dtcol.ColumnName);
            str = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            str = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                Response.Write(str + Convert.ToString(dr[j]));
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }

    private DataTable BillDetailDataTable(int waID)
    {
        DataTable dt = new DataTable();
        AddColumns(ref dt, "Sr.No");
        AddColumns(ref dt, "Name Of Material");
        AddColumns(ref dt, "Estimate Rate");
        DataTable dtEstimateColumns = DAL.DalAccessUtility.GetDataInDataSet("exec [viewGetEstimateByWorkAllot] " + waID).Tables[0];

        for (int i = 0; i < dtEstimateColumns.Rows.Count; i++)
        {
            AddColumns(ref dt, "Est No. " + dtEstimateColumns.Rows[i]["EstID"].ToString());
        }

        DataTable dtBillsColumns = DAL.DalAccessUtility.GetDataInDataSet("exec [viewGetBillIDsByWorkAllot] " + waID).Tables[0];
        for (int i = 0; i < dtBillsColumns.Rows.Count; i++)
        {
            AddColumns(ref dt, "Bill No. " + dtBillsColumns.Rows[i]["SubBillID"].ToString());
        }

        AddColumns(ref dt, "Balance Quantity");

        return dt;
    }

    private void AddColumns(ref DataTable dt, string columnName)
    {
        dt.Columns.Add(new DataColumn(columnName, typeof(string)));
    }

    protected DataTable GetBillReportByNameOFWork()
    {

        DataRow dr = null;

        DataTable dsDes = new DataTable();

        DataTable dsRate = new DataTable();

        int WAID = Convert.ToInt16(ddlNameOfWork.SelectedValue);
        int AcaID = Convert.ToInt16(ddlAcademy.SelectedValue);


        dsDes = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct M.MatName,M.MatId From EstimateAndMaterialOthersRelations EM  INNER JOIN Estimate ES ON ES.EstId=EM.EstId  INNER JOIN WorkAllot WA ON WA.WAId=ES.WAId INNER JOIN Material M ON EM.MatId = M.MatId  WHERE WA.WAId='" + ddlNameOfWork.SelectedValue + "'and ES.AcaId ='" + ddlAcademy.SelectedValue + "' and EM.PSID ='" + (int)TypeEnum.PurchaseSourceID.Local + "' and ES.IsApproved=1").Tables[0];

        DataTable dataTable = BillDetailDataTable(WAID);

        DataTable dtEstimateQtyDetail = DAL.DalAccessUtility.GetDataInDataSet("exec [GetEstimateQtyDetailByWorkAllot] '" + ddlNameOfWork.SelectedValue + "','" + ddlAcademy.SelectedValue + "','" + (int)TypeEnum.PurchaseSourceID.Local + "'").Tables[0];

        DataTable dtSubmitBillQtyDetail = DAL.DalAccessUtility.GetDataInDataSet("exec [GetSubmitBillQtyDetailByWorkAllot] '" + ddlNameOfWork.SelectedValue + "','" + ddlAcademy.SelectedValue + "','" + (int)TypeEnum.PurchaseSourceID.Local + "'").Tables[0];

        if (dsDes != null)
        {
            for (int i = 0; i < dsDes.Rows.Count; i++)
            {
                dsRate = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct Max(EM.Rate) As Rate From EstimateAndMaterialOthersRelations EM  INNER JOIN Estimate ES ON ES.EstId=EM.EstId  WHERE ES.WAId='" + ddlNameOfWork.SelectedValue + "'and ES.AcaId ='" + ddlAcademy.SelectedValue + "' and EM.PSID ='" + (int)TypeEnum.PurchaseSourceID.Local + "' and ES.IsApproved=1 and EM.MatId='" + dsDes.Rows[i]["MatId"].ToString() + "'").Tables[0];

                decimal BillQty = 0;
                decimal EstimateQty = 0;
                dr = dataTable.NewRow();
                dr[0] = i + 1;
                dr["Name Of Material"] = dsDes.Rows[i]["MatName"].ToString();
                dr["Estimate Rate"] = dsRate.Rows[0]["Rate"].ToString();

                for (int j = 0; j < dtEstimateQtyDetail.Rows.Count; j++)
                {
                    if (dsDes.Rows[i]["MatId"].ToString() == dtEstimateQtyDetail.Rows[j]["MatId"].ToString())
                    {
                        dr["Est No. " + dtEstimateQtyDetail.Rows[j]["EstId"].ToString()] = dtEstimateQtyDetail.Rows[j]["Qty"].ToString() == string.Empty ? "0" : dtEstimateQtyDetail.Rows[j]["Qty"].ToString();
                        EstimateQty += Convert.ToDecimal(dtEstimateQtyDetail.Rows[j]["Qty"].ToString());
                    }
                }

                for (int k = 0; k < dtSubmitBillQtyDetail.Rows.Count; k++)
                {
                    if (dsDes.Rows[i]["MatId"].ToString() == dtSubmitBillQtyDetail.Rows[k]["MatId"].ToString())
                    {
                        dr["Bill No. " + dtSubmitBillQtyDetail.Rows[k]["SubBillId"].ToString()] = dtSubmitBillQtyDetail.Rows[k]["Qty"].ToString() == string.Empty ? "0" : dtSubmitBillQtyDetail.Rows[k]["Qty"].ToString();
                        BillQty += Convert.ToDecimal(dtSubmitBillQtyDetail.Rows[k]["Qty"].ToString());
                    }
                }

                dr["Balance Quantity"] = Convert.ToDecimal(EstimateQty) - Convert.ToDecimal(BillQty);

                dataTable.Rows.Add(dr);
            }
        }
        return dataTable;
    }

    private void GenerateFile()
    {

        CreateExcelDoc excell_app = new CreateExcelDoc();
        //creates the main header
        excell_app.createHeaders(1, 4, "TOTAL LOCAL ESTIMATE COST", "D1", "F1", 2, "YELLOW", true, 10, "n");
        //creates subheaders
        excell_app.createHeaders(2, 4, "TOTAL BILL SUBMITTED", "D2", "F2", 0, "GRAY", true, 10, "");
        excell_app.createHeaders(3, 4, "BALANCE COST", "D3", "F3", 0, "GRAY", true, 10, "");
        excell_app.createHeaders(3, 1, "NAME OF ACADEMY", "A3", "A3", 0, "GRAY", true, 10, "");
        excell_app.createHeaders(4, 1, "NAME OF WORK", "A4", "A4", 0, "GRAY", true, 10, "");
        excell_app.createHeaders(5, 1, "", "A5", "K5", 2, "GAINSBORO", true, 10, "");
        excell_app.createHeaders(6, 1, "ESTIMATES", "C6", "C6", 0, "GRAY", true, 10, "");
        excell_app.createHeaders(7, 1, "EST NO.1 Total", "A7", "A7", 0, "GRAY", true, 10, "");
        excell_app.createHeaders(7, 2, "ESTIMATE SUBHEAD", "B7", "B7", 0, "GRAY", true, 10, "");
        excell_app.createHeaders(7, 3, "COST", "C7", "C7", 0, "GRAY", true, 10, "");
        excell_app.createHeaders(8, 1, "", "A5", "K5", 2, "GAINSBORO", true, 10, "");
        excell_app.createHeaders(9, 1, "DETAILS", "A9", "K9", 0, "GRAY", true, 10, "");

        excell_app.createHeaders(10, 1, "NAME OF MATERIAL", "A10", "A10", 0, "GRAY", true, 10, "");
        excell_app.createHeaders(10, 2, "ESTIMATE RATE", "B10", "B10", 0, "GRAY", true, 10, "");
        excell_app.createHeaders(10, 3, "EST NO.1", "C10", "C10", 0, "GRAY", true, 10, "");
        excell_app.createHeaders(10, 4, "EST NO.2", "D10", "D10", 0, "GRAY", true, 10, "");
        excell_app.createHeaders(10, 5, "EST NO.3", "E10", "E10", 0, "GRAY", true, 10, "");
        excell_app.createHeaders(10, 6, "EST NO.4", "F10", "F10", 0, "GRAY", true, 10, "");
        excell_app.createHeaders(10, 7, "BILL NO.1", "G10", "G10", 0, "GRAY", true, 10, "");
        excell_app.createHeaders(10, 8, "BILL NO.2", "H10", "H10", 0, "GRAY", true, 10, "");
        excell_app.createHeaders(10, 9, "BILL NO.3", "I10", "I10", 0, "GRAY", true, 10, "");
        excell_app.createHeaders(10, 10, "BILL NO.4", "J10", "J10", 0, "GRAY", true, 10, "");
        excell_app.createHeaders(10, 11, "BALANCE QTY", "K11", "K11", 0, "GRAY", true, 10, "");

        excell_app.createHeaders(3, 4, "Initial Total", "D6", "D6", 0, "GRAY", true, 10, "");
        //add Data to cells
        excell_app.addData(5, 2, "114287", "B7", "B7", "#,##0");
        excell_app.addData(7, 3, "", "C7", "C7", "");
        excell_app.addData(7, 4, "129121", "D7", "D7", "#,##0");
        //add percentage row
        excell_app.addData(8, 2, "", "B8", "B8", "");
        excell_app.addData(8, 3, "=B7/D7", "C8", "C8", "0.0%");
        excell_app.addData(8, 4, "", "D8", "D8", "");
        //add empty divider
        excell_app.createHeaders(9, 2, "", "B9", "D9", 2, "GAINSBORO", true, 10, "");

    }
}