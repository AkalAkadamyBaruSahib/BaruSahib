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

    //protected DataTable BindDatatable()
    //{
    //    DataTable dt = new DataTable();
    //    DataSet ds = new DataSet();
    //    dt = GenerateFile();
    //    return dt;

    //}

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

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        DataTable dsDes = new DataTable();
        DataTable dsRate = new DataTable();

        int WAID = Convert.ToInt16(ddlNameOfWork.SelectedValue);
        int AcaID = Convert.ToInt16(ddlAcademy.SelectedValue);

        int rowNum = 0;


        DataTable dtMaterials = GetBillReportByNameOFWork();

        DataTable dsEstimateCost = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct SUM(distinct ER.Amount) As EstimateCost FROM Estimate E Inner JOIn EstimateAndMaterialOthersRelations ER on ER.EstId=E.EstId WHERE E.WAId='" + ddlNameOfWork.SelectedValue + "'and E.AcaId ='" + ddlAcademy.SelectedValue + "' and ER.PSID ='" + (int)TypeEnum.PurchaseSourceID.Local + "' and E.IsApproved=1").Tables[0];

        DataTable dsPurchaseCost = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct SUM(SE.Amount) AS PurchaseCost FROM SubmitBillByUser S INNER JOIN SubmitBillByUserAndMaterialOthersRelation SE ON S.SubBillId = SE.SubBillId WHERE S.WAId='" + ddlNameOfWork.SelectedValue + "' AND ISNULL(S.FirstVarifyStatus,-1) != 0 AND ISNULL(S.SecondVarifyStatus,-1) != 0  and S.AcaId='" + ddlAcademy.SelectedValue + "'").Tables[0];


        dsDes = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct M.MatName,M.MatId,WA.WorkAllotName,A.AcaName From EstimateAndMaterialOthersRelations EM  INNER JOIN Estimate ES ON ES.EstId=EM.EstId INNER JOIN Academy A ON A.AcaId=ES.AcaId  INNER JOIN WorkAllot WA ON WA.WAId=ES.WAId INNER JOIN Material M ON EM.MatId = M.MatId  WHERE WA.WAId='" + ddlNameOfWork.SelectedValue + "'and ES.AcaId ='" + ddlAcademy.SelectedValue + "' and EM.PSID ='" + (int)TypeEnum.PurchaseSourceID.Local + "' and ES.IsApproved=1").Tables[0];
        CreateExcelDoc excell_app = new CreateExcelDoc();


        //creates the main header

        rowNum = rowNum + 1; //1
        excell_app.createHeaders(rowNum, 4, "TOTAL LOCAL ESTIMATE COST:-", "D1", "F1", 2, "Turquoise", true, 10, "n");
        excell_app.addData(rowNum, 7, dsEstimateCost.Rows[0]["EstimateCost"].ToString(), "G1", "G1", "");
        //creates subheaders
        rowNum = rowNum + 1; //2
        excell_app.createHeaders(rowNum, 4, "TOTAL BILL SUBMITTED:-", "D2", "F2", 0, "Turquoise", true, 10, "");
        if (dsPurchaseCost.Rows[0]["PurchaseCost"].ToString() == string.Empty)
        {
            excell_app.addData(rowNum, 7, "0", "G2", "G2", "");
        }
        else
        {
            excell_app.addData(rowNum, 7, dsPurchaseCost.Rows[0]["PurchaseCost"].ToString(), "G2", "G2", "");
        }

        rowNum = rowNum + 1; //3
        excell_app.createHeaders(rowNum, 4, "BALANCE COST:-", "D3", "F3", 0, "Turquoise", true, 10, "");
        excell_app.createHeaders(rowNum, 1, "NAME OF ACADEMY:-", "A3", "A3", 0, "Turquoise", true, 10, "");
        excell_app.addData(rowNum, 2, dsDes.Rows[0]["AcaName"].ToString(), "B3", "B3", "");
        decimal balanceCost = 0;
        if (dsPurchaseCost.Rows[0]["PurchaseCost"].ToString() == string.Empty)
        {
            balanceCost = Convert.ToDecimal(dsEstimateCost.Rows[0]["EstimateCost"].ToString());
        }
        else
        {
            balanceCost = Convert.ToDecimal(dsEstimateCost.Rows[0]["EstimateCost"].ToString()) - Convert.ToDecimal(dsPurchaseCost.Rows[0]["PurchaseCost"].ToString());
        }
        excell_app.addData(rowNum, 7, balanceCost.ToString(), "G3", "G3", "");

        rowNum = rowNum + 1; //4
        excell_app.createHeaders(rowNum, 1, "NAME OF WORK:-", "A4", "A4", 0, "Turquoise", true, 10, "");
        excell_app.addData(rowNum, 2, dsDes.Rows[0]["WorkAllotName"].ToString(), "B4", "B4", "");

        rowNum = rowNum + 1; //5
        excell_app.createHeaders(rowNum, 1, "", getAlphabeticCharacter(1) + rowNum, getAlphabeticCharacter(dtMaterials.Columns.Count) + rowNum, 2, "GAINSBORO", true, 10, "");

        rowNum = rowNum + 1; //6
        excell_app.createHeaders(rowNum, 1, "ESTIMATES", "A6", "C6", 0, "PeachPuff", true, 10, "");

        rowNum = rowNum + 1; //7
        excell_app.createHeaders(rowNum, 1, "EST NO.", "A7", "A7", 0, "YELLOW", true, 10, "");
        excell_app.createHeaders(rowNum, 2, "ESTIMATE SUBHEAD", "B7", "B7", 0, "YELLOW", true, 10, "");
        excell_app.createHeaders(rowNum, 3, "COST", "C7", "C7", 0, "YELLOW", true, 10, "");

        rowNum = rowNum + 1;

        DataTable dsEstimate = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct Es.EstId,Es.SubEstimate,Es.EstmateCost From EstimateAndMaterialOthersRelations EM  INNER JOIN Estimate ES ON ES.EstId=EM.EstId  WHERE  ES.WAId='" + ddlNameOfWork.SelectedValue + "'and ES.AcaId ='" + ddlAcademy.SelectedValue + "' and EM.PSID ='" + (int)TypeEnum.PurchaseSourceID.Local + "' and ES.IsApproved=1").Tables[0];
        for (int x = 0; x < dsEstimate.Rows.Count; x++)
        {
            excell_app.addData(rowNum, 1, dsEstimate.Rows[x]["EstId"].ToString(), "A" + rowNum, "A" + rowNum, "");
            excell_app.addData(rowNum, 2, dsEstimate.Rows[x]["SubEstimate"].ToString(), "B" + rowNum, "B" + rowNum, "");
            excell_app.addData(rowNum, 3, dsEstimate.Rows[x]["EstmateCost"].ToString(), "C" + rowNum, "C" + rowNum, "");
            rowNum = rowNum + 1;
        }

        excell_app.createHeaders(rowNum, 1, "", getAlphabeticCharacter(1) + rowNum, getAlphabeticCharacter(dtMaterials.Columns.Count) + rowNum, 2, "GAINSBORO", true, 10, "");

        rowNum = rowNum + 1;

        excell_app.createHeaders(rowNum, 1, "DETAILS", getAlphabeticCharacter(1) + rowNum, getAlphabeticCharacter(dtMaterials.Columns.Count) + rowNum, 2, "PeachPuff", true, 10, "");

        rowNum = rowNum + 1;

        int col = 1;
        foreach (DataColumn dtcol in dtMaterials.Columns)
        {
            excell_app.createHeaders(rowNum, col, dtcol.ColumnName, getAlphabeticCharacter(col) + rowNum, getAlphabeticCharacter(col) + rowNum, 0, "YELLOW", true, 10, "");
            col += 1;
        }

        rowNum = rowNum + 1;
        for (int i = 0; i < dtMaterials.Rows.Count; i++)
        {

            int dataCol = 1;
            for (int j = 1; j <= dtMaterials.Columns.Count; j++)
            {

                excell_app.createHeaders(rowNum, j, dtMaterials.Rows[i][(j - 1)].ToString(), getAlphabeticCharacter(dataCol) + rowNum, getAlphabeticCharacter(dataCol) + rowNum, 0, "WHITE", true, 10, "");
                dataCol += 1;
            }
            rowNum += 1;
        }
        excell_app.closeExcel();
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
}