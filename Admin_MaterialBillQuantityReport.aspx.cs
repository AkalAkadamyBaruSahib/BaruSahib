using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;

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
        var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Bills Report");

        DataTable dsDes = new DataTable();
        DataTable dsRate = new DataTable();

        int WAID = Convert.ToInt16(ddlNameOfWork.SelectedValue);
        int AcaID = Convert.ToInt16(ddlAcademy.SelectedValue);

        DataTable dtMaterials = GetBillReportByNameOFWork();

        DataTable dsEstimateCost = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct SUM(distinct ER.Amount) As EstimateCost FROM Estimate E Inner JOIn EstimateAndMaterialOthersRelations ER on ER.EstId=E.EstId WHERE E.WAId='" + ddlNameOfWork.SelectedValue + "'and E.AcaId ='" + ddlAcademy.SelectedValue + "' and ER.PSID ='" + (int)TypeEnum.PurchaseSourceID.Local + "' and E.IsApproved=1").Tables[0];

        DataTable dsPurchaseCost = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct SUM(SE.Amount) AS PurchaseCost FROM SubmitBillByUser S INNER JOIN SubmitBillByUserAndMaterialOthersRelation SE ON S.SubBillId = SE.SubBillId WHERE S.WAId='" + ddlNameOfWork.SelectedValue + "' AND ISNULL(S.FirstVarifyStatus,-1) != 0 AND ISNULL(S.SecondVarifyStatus,-1) != 0  and S.AcaId='" + ddlAcademy.SelectedValue + "'").Tables[0];


        dsDes = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct M.MatName,M.MatId,WA.WorkAllotName,A.AcaName From EstimateAndMaterialOthersRelations EM  INNER JOIN Estimate ES ON ES.EstId=EM.EstId INNER JOIN Academy A ON A.AcaId=ES.AcaId  INNER JOIN WorkAllot WA ON WA.WAId=ES.WAId INNER JOIN Material M ON EM.MatId = M.MatId  WHERE WA.WAId='" + ddlNameOfWork.SelectedValue + "'and ES.AcaId ='" + ddlAcademy.SelectedValue + "' and EM.PSID ='" + (int)TypeEnum.PurchaseSourceID.Local + "' and ES.IsApproved=1").Tables[0];

        ws.Cell("D1").Value = "TOTAL LOCAL ESTIMATE COST";
        var rngTable = ws.Range("D1:F1");
        ws.Cell("E1").Value = dsEstimateCost.Rows[0]["EstimateCost"].ToString();
        ws.Cell("D2").Value = "TOTAL BILL SUBMITTED:-";
        var rngTable1 = ws.Range("D2:F2");
        if (dsPurchaseCost.Rows[0]["PurchaseCost"].ToString() == string.Empty)
        {
            ws.Cell("E2").Value = 0;
        }
        else
        {
            ws.Cell("E2").Value = dsPurchaseCost.Rows[0]["PurchaseCost"].ToString();
        }


        ws.Cell("D3").Value = "BALANCE COST:-";
        ws.Cell("A3").Value = "NAME OF ACADEMY:-";
        if (dsDes.Rows.Count > 0 && dsDes != null)
        {
            ws.Cell("B3").Value = dsDes.Rows[0]["AcaName"].ToString();
        }
        var rngTable2 = ws.Range("D3:F3");

        decimal balanceCost = 0;
        if (dsPurchaseCost.Rows[0]["PurchaseCost"].ToString() == string.Empty)
        {
            if (dsEstimateCost.Rows[0]["EstimateCost"].ToString() == string.Empty)
            {
                balanceCost = 0;
            }
            else
            {
                balanceCost = Convert.ToDecimal(dsEstimateCost.Rows[0]["EstimateCost"].ToString());
            }
        }
        else
        {
            balanceCost = Convert.ToDecimal(dsEstimateCost.Rows[0]["EstimateCost"].ToString()) - Convert.ToDecimal(dsPurchaseCost.Rows[0]["PurchaseCost"].ToString());
        }
        ws.Cell("E3").Value = balanceCost.ToString();
        ws.Cell("A4").Value = "NAME OF WORK:-";
        if (dsDes.Rows.Count > 0 && dsDes != null)
        {
            ws.Cell("B4").Value = dsDes.Rows[0]["WorkAllotName"].ToString();
        }
        var rngTableEstimate = ws.Range("A6:C6");
        rngTable.IsMerged();
        var rngTable11 = ws.Range("A3:A3");
        var rngTable12 = ws.Range("A4:A4");

        ws.Cell("A6").Value = "";
        ws.Cell("B6").Value = "ESTIMATES";
        ws.Cell("C6").Value = "";
        var rngTable5 = ws.Range("A6:A6");
        var rngTable6 = ws.Range("B6:B6");
        var rngTable4 = ws.Range("C6:C6");
        ws.Cell("A7").Value = "EST NO.";
        ws.Cell("B7").Value = "ESTIMATE SUBHEAD";
        ws.Cell("C7").Value = "COST";
        var rngTable7 = ws.Range("A7:A7");
        var rngTable8 = ws.Range("B7:B7");
        var rngTable9 = ws.Range("C7:C7");
        int rowNum = 8;
        DataTable dsEstimate = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct Es.EstId,Es.SubEstimate,Es.EstmateCost From EstimateAndMaterialOthersRelations EM  INNER JOIN Estimate ES ON ES.EstId=EM.EstId  WHERE  ES.WAId='" + ddlNameOfWork.SelectedValue + "'and ES.AcaId ='" + ddlAcademy.SelectedValue + "' and EM.PSID ='" + (int)TypeEnum.PurchaseSourceID.Local + "' and ES.IsApproved=1").Tables[0];
        for (int x = 0; x < dsEstimate.Rows.Count; x++)
        {
            ws.Cell("A" + rowNum).Value = dsEstimate.Rows[x]["EstId"].ToString();
            ws.Cell("B" + rowNum).Value = dsEstimate.Rows[x]["SubEstimate"].ToString();
            ws.Cell("C" + rowNum).Value = dsEstimate.Rows[x]["EstmateCost"].ToString();
            rowNum = rowNum + 1;
        }
        rowNum = rowNum + 2;
        int col = 1;
        ws.Cell("D" + rowNum).Value = "DETAILS";
        foreach (DataColumn dtcol in dtMaterials.Columns)
        {
            var rngTable13 = ws.Range(getAlphabeticCharacter(col) + rowNum);
            rngTable13.FirstCell().Style.Font.SetBold().Fill.SetBackgroundColor(XLColor.CornflowerBlue).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.Bold = true;
            col += 1;
        }
        rowNum = rowNum + 1;
        int tcol = 1;
        foreach (DataColumn dtcol in dtMaterials.Columns)
        {
            ws.Cell(getAlphabeticCharacter(tcol) + rowNum).Value = dtcol.ColumnName;
            var rngTable10 = ws.Range(getAlphabeticCharacter(tcol) + rowNum);
            rngTable10.FirstCell().Style.Font.SetBold().Fill.SetBackgroundColor(XLColor.Aqua).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.Bold = true;
            tcol += 1;
        }

        rowNum = rowNum + 1;
        for (int i = 0; i < dtMaterials.Rows.Count; i++)
        {

            int dataCol = 1;
            for (int j = 1; j <= dtMaterials.Columns.Count; j++)
            {
                ws.Cell(getAlphabeticCharacter(dataCol) + rowNum).Value = dtMaterials.Rows[i][(j - 1)].ToString();
                dataCol += 1;
            }
            rowNum += 1;
        }
        rngTable.FirstCell().Style.Font.SetBold().Fill.SetBackgroundColor(XLColor.AntiqueWhite).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        rngTable1.FirstCell().Style.Font.SetBold().Fill.SetBackgroundColor(XLColor.Aquamarine).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        rngTable2.FirstCell().Style.Font.SetBold().Fill.SetBackgroundColor(XLColor.Apricot).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        rngTable4.FirstCell().Style.Font.SetBold().Fill.SetBackgroundColor(XLColor.CornflowerBlue).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        rngTable5.FirstCell().Style.Font.SetBold().Fill.SetBackgroundColor(XLColor.CornflowerBlue).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        rngTable6.FirstCell().Style.Font.SetBold().Fill.SetBackgroundColor(XLColor.CornflowerBlue).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        rngTable7.FirstCell().Style.Font.SetBold().Fill.SetBackgroundColor(XLColor.Aqua).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.Bold = true;
        rngTable8.FirstCell().Style.Font.SetBold().Fill.SetBackgroundColor(XLColor.Aqua).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.Bold = true;
        rngTable9.FirstCell().Style.Font.SetBold().Fill.SetBackgroundColor(XLColor.Aqua).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.Bold = true;
        rngTable11.FirstCell().Style.Font.SetBold().Fill.SetBackgroundColor(XLColor.Aquamarine).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.Bold = true;
        rngTable12.FirstCell().Style.Font.SetBold().Fill.SetBackgroundColor(XLColor.AntiqueWhite).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.Bold = true;

        ws.Columns().AdjustToContents();

        string FileName = "MaterialReport" + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".xlsx";

        string FilePath = Server.MapPath("EstFile") + "\\" + FileName;
        wb.SaveAs(@FilePath);

        Response.Clear();
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", FileName));
        Response.ContentType = "application/octet-stream";
        Response.WriteFile(@FilePath);
        Response.End();
    } 
    
    private string getAlphabeticCharacter(int num)
    {
        string character = string.Empty;

        switch (num)
        {
            case 1:
                character = "A";
                break;
            case 2:
                character = "B";
                break;
            case 3:
                character = "C";
                break;
            case 4:
                character = "D";
                break;
            case 5:
                character = "E";
                break;
            case 6:
                character = "F";
                break;
            case 7:
                character = "G";
                break;
            case 8:
                character = "H";
                break;
            case 9:
                character = "I";
                break;
            case 10:
                character = "J";
                break;
            case 11:
                character = "K";
                break;
            case 12:
                character = "L";
                break;
            case 13:
                character = "M";
                break;
            case 14:
                character = "N";
                break;
            case 15:
                character = "O";
                break;
            case 16:
                character = "P";
                break;
            case 17:
                character = "Q";
                break;
            case 18:
                character = "R";
                break;
            case 19:
                character = "S";
                break;
            case 20:
                character = "T";
                break;
            case 21:
                character = "U";
                break;
            case 22:
                character = "V";
                break;
            case 23:
                character = "W";
                break;
            case 24:
                character = "X";
                break;
            case 25:
                character = "Y";
                break;
            case 26:
                character = "Z";
                break;
            default:
                break;
        }

        return character;
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