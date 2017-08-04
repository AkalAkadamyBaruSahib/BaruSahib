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
                if (dtMaterials.Rows[i][(j - 1)].ToString() != "")
                {
                    ws.Cell(getAlphabeticCharacter(dataCol) + rowNum).Value = dtMaterials.Rows[i][(j - 1)].ToString();
                }
                else
                {
                    ws.Cell(getAlphabeticCharacter(dataCol) + rowNum).Value = 0;
                }
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
            case 27:
                character = "AA";
                break;
            case 28:
                character = "AB";
                break;
            case 29:
                character = "AC";
                break;
            case 30:
                character = "AD";
                break;
            case 31:
                character = "AE";
                break;
            case 32:
                character = "AF";
                break;
            case 33:
                character = "AG";
                break;
            case 34:
                character = "AH";
                break;
            case 35:
                character = "AI";
                break;
            case 36:
                character = "AJ";
                break;
            case 37:
                character = "AK";
                break;
            case 38:
                character = "AL";
                break;
            case 39:
                character = "AM";
                break;
            case 40:
                character = "AN";
                break;
            case 41:
                character = "AO";
                break;
            case 42:
                character = "AP";
                break;
            case 43:
                character = "AQ";
                break;
            case 44:
                character = "AR";
                break;
            case 45:
                character = "AS";
                break;
            case 46:
                character = "AT";
                break;
            case 47:
                character = "AU";
                break;
            case 48:
                character = "AV";
                break;
            case 49:
                character = "AW";
                break;
            case 50:
                character = "AX";
                break;
            case 51:
                character = "AY";
                break;
            case 52:
                character = "AZ";
                break;
            case 53:
                character = "BA";
                break;
            case 54:
                character = "BB";
                break;
            case 55:
                character = "BC";
                break;
            case 56:
                character = "BD";
                break;
            case 57:
                character = "BE";
                break;
            case 58:
                character = "BF";
                break;
            case 59:
                character = "BG";
                break;
            case 60:
                character = "BH";
                break;
            case 61:
                character = "BI";
                break;
            case 62:
                character = "BJ";
                break;
            case 63:
                character = "BK";
                break;
            case 64:
                character = "BL";
                break;
            case 65:
                character = "BM";
                break;
            case 66:
                character = "BN";
                break;
            case 67:
                character = "BO";
                break;
            case 68:
                character = "BP";
                break;
            case 69:
                character = "BQ";
                break;
            case 70:
                character = "BR";
                break;
            case 71:
                character = "BS";
                break;
            case 72:
                character = "BT";
                break;
            case 73:
                character = "BU";
                break;
            case 74:
                character = "BV";
                break;
            case 75:
                character = "BW";
                break;
            case 76:
                character = "BX";
                break;
            case 77:
                character = "BY";
                break;
            case 78:
                character = "BZ";
                break;
            case 79:
                character = "CA";
                break;
            case 80:
                character = "CB";
                break;
            case 81:
                character = "CC";
                break;
            case 82:
                character = "CD";
                break;
            case 83:
                character = "CE";
                break;
            case 84:
                character = "CF";
                break;
            case 85:
                character = "CG";
                break;
            case 86:
                character = "CH";
                break;
            case 87:
                character = "CI";
                break;
            case 88:
                character = "CJ";
                break;
            case 89:
                character = "CK";
                break;
            case 90:
                character = "CL";
                break;
            case 91:
                character = "CM";
                break;
            case 92:
                character = "CN";
                break;
            case 93:
                character = "CO";
                break;
            case 94:
                character = "CP";
                break;
            case 95:
                character = "CQ";
                break;
            case 96:
                character = "CR";
                break;
            case 97:
                character = "CS";
                break;
            case 98:
                character = "CT";
                break;
            case 99:
                character = "CU";
                break;
            case 100:
                character = "CV";
                break;
            case 101:
                character = "CW";
                break;
            case 102:
                character = "CX";
                break;
            case 103:
                character = "CY";
                break;
            case 104:
                character = "CZ";
                break;
            case 105:
                character = "DA";
                break;
            case 106:
                character = "DB";
                break;
            case 107:
                character = "DC";
                break;
            case 108:
                character = "DD";
                break;
            case 109:
                character = "DE";
                break;
            case 110:
                character = "DF";
                break;
            case 111:
                character = "DG";
                break;
            case 112:
                character = "DH";
                break;
            case 113:
                character = "DI";
                break;
            case 114:
                character = "DJ";
                break;
            case 115:
                character = "DK";
                break;
            case 116:
                character = "DL";
                break;
            case 117:
                character = "DM";
                break;
            case 118:
                character = "DN";
                break;
            case 119:
                character = "DO";
                break;
            case 120:
                character = "DP";
                break;
            case 121:
                character = "DQ";
                break;
            case 122:
                character = "DR";
                break;
            case 123:
                character = "DS";
                break;
            case 124:
                character = "DT";
                break;
            case 125:
                character = "DU";
                break;
            case 126:
                character = "DV";
                break;
            case 127:
                character = "DW";
                break;
            case 128:
                character = "DX";
                break;
            case 129:
                character = "DY";
                break;
            case 130:
                character = "DZ";
                break;
            case 131:
                character = "EA";
                break;
            case 132:
                character = "EB";
                break;
            case 133:
                character = "EC";
                break;
            case 134:
                character = "ED";
                break;
            case 135:
                character = "EE";
                break;
            case 136:
                character = "EF";
                break;
            case 137:
                character = "EG";
                break;
            case 138:
                character = "EH";
                break;
            case 139:
                character = "EI";
                break;
            case 140:
                character = "EJ";
                break;
            case 141:
                character = "EK";
                break;
            case 142:
                character = "EL";
                break;
            case 143:
                character = "EM";
                break;
            case 144:
                character = "EN";
                break;
            case 145:
                character = "EO";
                break;
            case 146:
                character = "EP";
                break;
            case 147:
                character = "EQ";
                break;
            case 148:
                character = "ER";
                break;
            case 149:
                character = "ES";
                break;
            case 150:
                character = "ET";
                break;
            case 151:
                character = "EU";
                break;
            case 152:
                character = "EV";
                break;
            case 153:
                character = "EW";
                break;
            case 154:
                character = "EX";
                break;
            case 155:
                character = "EY";
                break;
            case 156:
                character = "EZ";
                break;
            case 157:
                character = "FA";
                break;
            case 158:
                character = "FB";
                break;
            case 159:
                character = "FC";
                break;
            case 160:
                character = "FD";
                break;
            case 161:
                character = "FE";
                break;
            case 162:
                character = "FF";
                break;
            case 163:
                character = "FG";
                break;
            case 164:
                character = "FH";
                break;
            case 165:
                character = "FI";
                break;
            case 166:
                character = "FJ";
                break;
            case 167:
                character = "FK";
                break;
            case 168:
                character = "FL";
                break;
            case 169:
                character = "FM";
                break;
            case 170:
                character = "FN";
                break;
            case 171:
                character = "FO";
                break;
            case 172:
                character = "FP";
                break;
            case 173:
                character = "FQ";
                break;
            case 174:
                character = "FR";
                break;
            case 175:
                character = "FS";
                break;
            case 176:
                character = "FT";
                break;
            case 177:
                character = "FU";
                break;
            case 178:
                character = "FV";
                break;
            case 179:
                character = "FW";
                break;
            case 180:
                character = "FX";
                break;
            case 181:
                character = "FY";
                break;
            case 182:
                character = "FZ";
                break;
            case 183:
                character = "GA";
                break;
            case 184:
                character = "GB";
                break;
            case 185:
                character = "GC";
                break;
            case 186:
                character = "GD";
                break;
            case 187:
                character = "GE";
                break;
            case 188:
                character = "GF";
                break;
            case 189:
                character = "GG";
                break;
            case 190:
                character = "GH";
                break;
            case 191:
                character = "GI";
                break;
            case 192:
                character = "GJ";
                break;
            case 193:
                character = "GK";
                break;
            case 194:
                character = "GL";
                break;
            case 195:
                character = "GM";
                break;
            case 196:
                character = "GN";
                break;
            case 197:
                character = "GO";
                break;
            case 198:
                character = "GP";
                break;
            case 199:
                character = "GQ";
                break;
            case 200:
                character = "GR";
                break;
            case 201:
                character = "GS";
                break;
            case 202:
                character = "GT";
                break;
            case 203:
                character = "GU";
                break;
            case 204:
                character = "GV";
                break;
            case 205:
                character = "GW";
                break;
            case 206:
                character = "GX";
                break;
            case 207:
                character = "GY";
                break;
            case 208:
                character = "GZ";
                break;
            case 209:
                character = "HA";
                break;
            case 210:
                character = "HB";
                break;
            case 211:
                character = "HC";
                break;
            case 212:
                character = "HD";
                break;
            case 213:
                character = "HE";
                break;
            case 214:
                character = "HF";
                break;
            case 215:
                character = "HG";
                break;
            case 216:
                character = "HH";
                break;
            case 217:
                character = "HI";
                break;
            case 218:
                character = "HJ";
                break;
            case 219:
                character = "HK";
                break;
            case 220:
                character = "HL";
                break;
            case 221:
                character = "HM";
                break;
            case 222:
                character = "HN";
                break;
            case 223:
                character = "HO";
                break;
            case 224:
                character = "HP";
                break;
            case 225:
                character = "HQ";
                break;
            case 226:
                character = "HR";
                break;
            case 227:
                character = "HS";
                break;
            case 228:
                character = "HT";
                break;
            case 229:
                character = "HU";
                break;
            case 230:
                character = "HV";
                break;
            case 231:
                character = "HW";
                break;
            case 232:
                character = "HX";
                break;
            case 233:
                character = "HY";
                break;
            case 234:
                character = "HZ";
                break;
            case 235:
                character = "IA";
                break;
            case 236:
                character = "IB";
                break;
            case 237:
                character = "IC";
                break;
            case 238:
                character = "ID";
                break;
            case 239:
                character = "IE";
                break;
            case 240:
                character = "IF";
                break;
            case 241:
                character = "IG";
                break;
            case 242:
                character = "IH";
                break;
            case 243:
                character = "II";
                break;
            case 244:
                character = "IJ";
                break;
            case 245:
                character = "IK";
                break;
            case 246:
                character = "IL";
                break;
            case 247:
                character = "IM";
                break;
            case 248:
                character = "IN";
                break;
            case 249:
                character = "IO";
                break;
            case 250:
                character = "IP";
                break;
            case 251:
                character = "IQ";
                break;
            case 252:
                character = "IR";
                break;
            case 253:
                character = "IS";
                break;
            case 254:
                character = "IT";
                break;
            case 255:
                character = "IU";
                break;
            case 256:
                character = "IV";
                break;
            case 257:
                character = "IW";
                break;
            case 258:
                character = "IX";
                break;
            case 259:
                character = "IY";
                break;
             case 260:
                character = "IZ";
                break;
            case 261:
                character = "JA";
                break;
            case 262:
                character = "JB";
                break;
            case 263:
                character = "JC";
                break;
            case 264:
                character = "JD";
                break;
            case 265:
                character = "JE";
                break;
            case 266:
                character = "JF";
                break;
            case 267:
                character = "JG";
                break;
            case 268:
                character = "JH";
                break;
            case 269:
                character = "JI";
                break;
            case 270:
                character = "JJ";
                break;
            case 271:
                character = "JK";
                break;
            case 272:
                character = "JL";
                break;
            case 273:
                character = "JM";
                break;
            case 274:
                character = "JN";
                break;
            case 275:
                character = "JO";
                break;
            case 276:
                character = "JP";
                break;
            case 277:
                character = "JQ";
                break;
            case 278:
                character = "JR";
                break;
            case 279:
                character = "JS";
                break;
            case 280:
                character = "JT";
                break;
            case 281:
                character = "JU";
                break;
            case 282:
                character = "JV";
                break;
            case 283:
                character = "JW";
                break;
            case 284:
                character = "JX";
                break;
            case 285:
                character = "JY";
                break;
            case 286:
                character = "JZ";
                break;
            case 287:
                character = "KA";
                break;
            case 288:
                character = "KB";
                break;
            case 289:
                character = "KC";
                break;
            case 290:
                character = "KD";
                break;
            case 291:
                character = "KE";
                break;
            case 292:
                character = "KF";
                break;
            case 293:
                character = "KG";
                break;
            case 294:
                character = "KH";
                break;
            case 295:
                character = "KI";
                break;
            case 296:
                character = "KJ";
                break;
            case 297:
                character = "KK";
                break;
            case 298:
                character = "KL";
                break;
            case 299:
                character = "KM";
                break;
            case 300:
                character = "KN";
                break;
            case 301:
                character = "KO";
                break;
            case 302:
                character = "KP";
                break;
            case 303:
                character = "KQ";
                break;
            case 304:
                character = "KR";
                break;
            case 305:
                character = "KS";
                break;
            case 306:
                character = "KT";
                break;
            case 307:
                character = "KU";
                break;
            case 308:
                character = "KV";
                break;
            case 309:
                character = "KW";
                break;
            case 310:
                character = "KX";
                break;
            case 311:
                character = "KY";
                break;
            case 312:
                character = "KZ";
                break;
            case 313:
                character = "LA";
                break;
            case 314:
                character = "LB";
                break;
            case 315:
                character = "LC";
                break;
            case 316:
                character = "LD";
                break;
            case 317:
                character = "LE";
                break;
            case 318:
                character = "LF";
                break;
            case 319:
                character = "LG";
                break;
            case 320:
                character = "LH";
                break;
            case 321:
                character = "LI";
                break;
            case 322:
                character = "LJ";
                break;
            case 323:
                character = "LK";
                break;
            case 324:
                character = "LL";
                break;
            case 325:
                character = "LM";
                break;
            case 326:
                character = "LN";
                break;
            case 327:
                character = "LO";
                break;
            case 328:
                character = "LP";
                break;
            case 329:
                character = "LQ";
                break;
            case 330:
                character = "LR";
                break;
            case 331:
                character = "LS";
                break;
            case 332:
                character = "LT";
                break;
            case 333:
                character = "LU";
                break;
            case 334:
                character = "LV";
                break;
            case 335:
                character = "LW";
                break;
            case 336:
                character = "LX";
                break;
            case 337:
                character = "LY";
                break;
            case 338:
                character = "LZ";
                break;
            case 339:
                character = "MA";
                break;
            case 340:
                character = "MB";
                break;
            case 341:
                character = "MC";
                break;
            case 342:
                character = "MD";
                break;
            case 343:
                character = "ME";
                break;
            case 344:
                character = "MF";
                break;
            case 345:
                character = "MG";
                break;
            case 346:
                character = "MH";
                break;
            case 347:
                character = "MI";
                break;
            case 348:
                character = "MJ";
                break;
            case 349:
                character = "MK";
                break;
            case 350:
                character = "ML";
                break;
            case 351:
                character = "MM";
                break;
            case 352:
                character = "MN";
                break;
            case 353:
                character = "MO";
                break;
            case 354:
                character = "MP";
                break;
            case 355:
                character = "MQ";
                break;
            case 356:
                character = "MR";
                break;
            case 357:
                character = "MS";
                break;
            case 358:
                character = "MT";
                break;
            case 359:
                character = "MU";
                break;
            case 360:
                character = "MV";
                break;
            case 361:
                character = "MW";
                break;
            case 362:
                character = "MX";
                break;
            case 363:
                character = "MY";
                break;
            case 364:
                character = "MZ";
                break;
            case 365:
                character = "NA";
                break;
            case 366:
                character = "NB";
                break;
            case 367:
                character = "NC";
                break;
            case 368:
                character = "ND";
                break;
            case 369:
                character = "NE";
                break;
            case 370:
                character = "NF";
                break;
            case 371:
                character = "NG";
                break;
            case 372:
                character = "NH";
                break;
            case 373:
                character = "NI";
                break;
            case 374:
                character = "NJ";
                break;
            case 375:
                character = "NK";
                break;
            case 376:
                character = "NL";
                break;
            case 377:
                character = "NM";
                break;
            case 378:
                character = "NN";
                break;
            case 379:
                character = "NO";
                break;
            case 380:
                character = "NP";
                break;
            case 381:
                character = "NQ";
                break;
            case 382:
                character = "NR";
                break;
            case 383:
                character = "NS";
                break;
            case 384:
                character = "NT";
                break;
            case 385:
                character = "NU";
                break;
            case 386:
                character = "NV";
                break;
            case 387:
                character = "NW";
                break;
            case 388:
                character = "NX";
                break;
            case 389:
                character = "NY";
                break;
            case 390:
                character = "NZ";
                break;
            case 391:
                character = "OA";
                break;
            case 392:
                character = "OB";
                break;
            case 393:
                character = "OC";
                break;
            case 394:
                character = "OD";
                break;
            case 395:
                character = "OE";
                break;
            case 396:
                character = "OF";
                break;
            case 397:
                character = "OG";
                break;
            case 398:
                character = "OH";
                break;
            case 399:
                character = "OI";
                break;
            case 400:
                character = "OJ";
                break;
            case 401:
                character = "OK";
                break;
            case 402:
                character = "OL";
                break;
            case 403:
                character = "OM";
                break;
            case 404:
                character = "ON";
                break;
            case 405:
                character = "OO";
                break;
            case 406:
                character = "OP";
                break;
            case 407:
                character = "OQ";
                break;
            case 408:
                character = "OR";
                break;
            case 409:
                character = "OS";
                break;
            case 410:
                character = "OT";
                break;
            case 411:
                character = "OU";
                break;
            case 412:
                character = "OV";
                break;
            case 413:
                character = "OW";
                break;
            case 414:
                character = "OX";
                break;
            case 415:
                character = "OY";
                break;
            case 416:
                character = "OZ";
                break;
            case 417:
                character = "PA";
                break;
            case 418:
                character = "PB";
                break;
            case 419:
                character = "PC";
                break;
            case 420:
                character = "PD";
                break;
            case 421:
                character = "PE";
                break;
            case 422:
                character = "PF";
                break;
            case 423:
                character = "PG";
                break;
            case 424:
                character = "PH";
                break;
            case 425:
                character = "PI";
                break;
            case 426:
                character = "PJ";
                break;
            case 427:
                character = "PK";
                break;
            case 428:
                character = "PL";
                break;
            case 429:
                character = "PM";
                break;
            case 430:
                character = "PN";
                break;
            case 431:
                character = "PO";
                break;
            case 432:
                character = "PP";
                break;
            case 433:
                character = "PQ";
                break;
            case 434:
                character = "PR";
                break;
            case 435:
                character = "PS";
                break;
            case 436:
                character = "PT";
                break;
            case 437:
                character = "PU";
                break;
            case 438:
                character = "PV";
                break;
            case 439:
                character = "PW";
                break;
            case 440:
                character = "PX";
                break;
            case 441:
                character = "PY";
                break;
            case 442:
                character = "PZ";
                break;
            case 443:
                character = "QA";
                break;
            case 444:
                character = "QB";
                break;
            case 445:
                character = "QC";
                break;
            case 446:
                character = "QD";
                break;
            case 447:
                character = "QE";
                break;
            case 448:
                character = "QF";
                break;
            case 449:
                character = "QG";
                break;
            case 450:
                character = "QH";
                break;
            case 451:
                character = "QI";
                break;
            case 452:
                character = "QJ";
                break;
            case 453:
                character = "QK";
                break;
            case 454:
                character = "QL";
                break;
            case 455:
                character = "QM";
                break;
            case 456:
                character = "QN";
                break;
            case 457:
                character = "QO";
                break;
            case 458:
                character = "QP";
                break;
            case 459:
                character = "QQ";
                break;
            case 460:
                character = "QR";
                break;
            case 461:
                character = "QS";
                break;
            case 462:
                character = "QT";
                break;
            case 463:
                character = "QU";
                break;
            case 464:
                character = "QV";
                break;
            case 465:
                character = "QW";
                break;
            case 466:
                character = "QX";
                break;
            case 467:
                character = "QY";
                break;
            case 468:
                character = "QZ";
                break;
            case 469:
                character = "RA";
                break;
            case 470:
                character = "RB";
                break;
            case 471:
                character = "RC";
                break;
            case 472:
                character = "RD";
                break;
            case 473:
                character = "RE";
                break;
            case 474:
                character = "RF";
                break;
            case 475:
                character = "RG";
                break;
            case 476:
                character = "RH";
                break;
            case 477:
                character = "RI";
                break;
            case 478:
                character = "RJ";
                break;
            case 479:
                character = "RK";
                break;
            case 480:
                character = "RL";
                break;
            case 481:
                character = "RM";
                break;
            case 482:
                character = "RN";
                break;
            case 483:
                character = "RO";
                break;
            case 484:
                character = "RP";
                break;
            case 485:
                character = "RQ";
                break;
            case 486:
                character = "RR";
                break;
            case 487:
                character = "RS";
                break;
            case 488:
                character = "RT";
                break;
            case 489:
                character = "RU";
                break;
            case 490:
                character = "RV";
                break;
            case 491:
                character = "RW";
                break;
            case 492:
                character = "RX";
                break;
            case 493:
                character = "RY";
                break;
            case 494:
                character = "RZ";
                break;
            case 495:
                character = "SA";
                break;
            case 496:
                character = "SB";
                break;
            case 497:
                character = "SC";
                break;
            case 498:
                character = "SD";
                break;
            case 499:
                character = "SE";
                break;
            case 500:
                character = "SF";
                break;
            case 501:
                character = "SG";
                break;
            case 502:
                character = "SH";
                break;
            case 503:
                character = "SI";
                break;
            case 504:
                character = "SJ";
                break;
            case 505:
                character = "SK";
                break;
            case 506:
                character = "SL";
                break;
            case 507:
                character = "SM";
                break;
            case 508:
                character = "SN";
                break;
            case 509:
                character = "SO";
                break;
            case 510:
                character = "SP";
                break;
            case 511:
                character = "SQ";
                break;
            case 512:
                character = "SR";
                break;
            case 513:
                character = "SS";
                break;
            case 514:
                character = "ST";
                break;
            case 515:
                character = "SU";
                break;
            case 516:
                character = "SV";
                break;
            case 517:
                character = "SW";
                break;
            case 518:
                character = "SX";
                break;
            case 519:
                character = "SY";
                break;
            case 520:
                character = "SZ";
                break;
            case 521:
                character = "TA";
                break;
            case 522:
                character = "TB";
                break;
            case 523:
                character = "TC";
                break;
            case 524:
                character = "TD";
                break;
            case 525:
                character = "TE";
                break;
            case 526:
                character = "TF";
                break;
            case 527:
                character = "TG";
                break;
            case 528:
                character = "TH";
                break;
            case 529:
                character = "TI";
                break;
            case 530:
                character = "TJ";
                break;
            case 531:
                character = "TK";
                break;
            case 532:
                character = "TL";
                break;
            case 533:
                character = "TM";
                break;
            case 534:
                character = "TN";
                break;
            case 535:
                character = "TO";
                break;
            case 536:
                character = "TP";
                break;
            case 537:
                character = "TQ";
                break;
            case 538:
                character = "TR";
                break;
            case 539:
                character = "TS";
                break;
            case 540:
                character = "TT";
                break;
            case 541:
                character = "TU";
                break;
            case 542:
                character = "TV";
                break;
            case 543:
                character = "TW";
                break;
            case 544:
                character = "TX";
                break;
            case 545:
                character = "TY";
                break;
            case 546:
                character = "TZ";
                break;
            case 547:
                character = "UA";
                break;
            case 548:
                character = "UB";
                break;
            case 549:
                character = "UC";
                break;
            case 550:
                character = "UD";
                break;
            case 551:
                character = "UE";
                break;
            case 552:
                character = "UF";
                break;
            case 553:
                character = "UG";
                break;
            case 554:
                character = "UH";
                break;
            case 555:
                character = "UI";
                break;
            case 556:
                character = "UJ";
                break;
            case 557:
                character = "UK";
                break;
            case 558:
                character = "UL";
                break;
            case 559:
                character = "UM";
                break;
            case 560:
                character = "UN";
                break;
            case 561:
                character = "UO";
                break;
            case 562:
                character = "UP";
                break;
            case 563:
                character = "UQ";
                break;
            case 564:
                character = "UR";
                break;
            case 565:
                character = "US";
                break;
            case 566:
                character = "UT";
                break;
            case 567:
                character = "UU";
                break;
            case 568:
                character = "UV";
                break;
            case 569:
                character = "UW";
                break;
            case 570:
                character = "UX";
                break;
            case 571:
                character = "UY";
                break;
            case 572:
                character = "UZ";
                break;
            case 573:
                character = "VA";
                break;
            case 574:
                character = "VB";
                break;
            case 575:
                character = "VC";
                break;
            case 576:
                character = "VD";
                break;
            case 577:
                character = "VE";
                break;
            case 578:
                character = "VF";
                break;
            case 579:
                character = "VG";
                break;
            case 580:
                character = "VH";
                break;
            case 581:
                character = "VI";
                break;
            case 582:
                character = "VJ";
                break;
            case 583:
                character = "VK";
                break;
            case 584:
                character = "VL";
                break;
            case 585:
                character = "VM";
                break;
            case 586:
                character = "VN";
                break;
            case 587:
                character = "VO";
                break;
            case 588:
                character = "VP";
                break;
            case 589:
                character = "VQ";
                break;
            case 590:
                character = "VR";
                break;
            case 591:
                character = "VS";
                break;
            case 592:
                character = "VT";
                break;
            case 593:
                character = "VU";
                break;
            case 594:
                character = "VV";
                break;
            case 595:
                character = "VW";
                break;
            case 596:
                character = "VX";
                break;
            case 597:
                character = "VY";
                break;
            case 598:
                character = "VZ";
                break;
            case 599:
                character = "WA";
                break;
            case 600:
                character = "WB";
                break;
            case 601:
                character = "WC";
                break;
            case 602:
                character = "WD";
                break;
            case 603:
                character = "WE";
                break;
            case 604:
                character = "WF";
                break;
            case 605:
                character = "WG";
                break;
            case 606:
                character = "WH";
                break;
            case 607:
                character = "WI";
                break;
            case 608:
                character = "WJ";
                break;
            case 609:
                character = "WK";
                break;
            case 610:
                character = "WL";
                break;
            case 611:
                character = "WM";
                break;
            case 612:
                character = "WN";
                break;
            case 613:
                character = "WO";
                break;
            case 614:
                character = "WP";
                break;
            case 615:
                character = "WQ";
                break;
            case 616:
                character = "WR";
                break;
            case 617:
                character = "WS";
                break;
            case 618:
                character = "WT";
                break;
            case 619:
                character = "WU";
                break;
            case 620:
                character = "WV";
                break;
            case 621:
                character = "WW";
                break;
            case 622:
                character = "WX";
                break;
            case 623:
                character = "WY";
                break;
            case 624:
                character = "WZ";
                break;
            case 625:
                character = "XA";
                break;
            case 626:
                character = "XB";
                break;
            case 627:
                character = "XC";
                break;
            case 628:
                character = "XD";
                break;
            case 629:
                character = "XE";
                break;
            case 630:
                character = "XF";
                break;
            case 631:
                character = "XG";
                break;
            case 632:
                character = "XH";
                break;
            case 633:
                character = "XI";
                break;
            case 634:
                character = "XJ";
                break;
            case 635:
                character = "XK";
                break;
            case 636:
                character = "XL";
                break;
            case 637:
                character = "XM";
                break;
            case 638:
                character = "XN";
                break;
            case 639:
                character = "XO";
                break;
            case 640:
                character = "XP";
                break;
            case 641:
                character = "XQ";
                break;
            case 642:
                character = "XR";
                break;
            case 643:
                character = "XS";
                break;
            case 644:
                character = "XT";
                break;
            case 645:
                character = "XU";
                break;
            case 646:
                character = "XV";
                break;
            case 647:
                character = "XW";
                break;
            case 648:
                character = "XX";
                break;
            case 649:
                character = "XY";
                break;
            case 650:
                character = "XZ";
                break;
            case 651:
                character = "YA";
                break;
            case 652:
                character = "YB";
                break;
            case 653:
                character = "YC";
                break;
            case 654:
                character = "YD";
                break;
            case 655:
                character = "YE";
                break;
            case 656:
                character = "YF";
                break;
            case 657:
                character = "YG";
                break;
            case 658:
                character = "YH";
                break;
            case 659:
                character = "YI";
                break;
            case 660:
                character = "YJ";
                break;
            case 661:
                character = "YK";
                break;
            case 662:
                character = "YL";
                break;
            case 663:
                character = "YM";
                break;
            case 664:
                character = "YN";
                break;
            case 665:
                character = "YO";
                break;
            case 666:
                character = "YP";
                break;
            case 667:
                character = "YQ";
                break;
            case 668:
                character = "YR";
                break;
            case 669:
                character = "YS";
                break;
            case 670:
                character = "YT";
                break;
            case 671:
                character = "YU";
                break;
            case 672:
                character = "YV";
                break;
            case 673:
                character = "YW";
                break;
            case 674:
                character = "YX";
                break;
            case 675:
                character = "YY";
                break;
            case 676:
                character = "YZ";
                break;
            case 677:
                character = "ZA";
                break;
            case 678:
                character = "ZB";
                break;
            case 679:
                character = "ZC";
                break;
            case 680:
                character = "ZD";
                break;
            case 681:
                character = "ZE";
                break;
            case 682:
                character = "ZF";
                break;
            case 683:
                character = "ZG";
                break;
            case 684:
                character = "ZH";
                break;
            case 685:
                character = "ZI";
                break;
            case 686:
                character = "ZJ";
                break;
            case 687:
                character = "ZK";
                break;
            case 688:
                character = "ZL";
                break;
            case 689:
                character = "ZM";
                break;
            case 690:
                character = "ZN";
                break;
            case 691:
                character = "ZO";
                break;
            case 692:
                character = "ZP";
                break;
            case 693:
                character = "ZQ";
                break;
            case 694:
                character = "ZR";
                break;
            case 695:
                character = "ZS";
                break;
            case 696:
                character = "ZT";
                break;
            case 697:
                character = "ZU";
                break;
            case 698:
                character = "ZV";
                break;
            case 699:
                character = "ZW";
                break;
            case 700:
                character = "ZX";
                break;
            case 701:
                character = "ZY";
                break;
            case 702:
                character = "ZZ";
                break;
            case 703:
                character = "AAA";
                break;
            case 704:
                character = "AAB";
                break;
            case 705:
                character = "AAC";
                break;
            case 706:
                character = "AAD";
                break;
            case 707:
                character = "AAE";
                break;
            case 708:
                character = "AAF";
                break;
            case 709:
                character = "AAG";
                break;
            case 710:
                character = "AAH";
                break;
            case 711:
                character = "AAI";
                break;
            case 712:
                character = "AAJ";
                break;
            case 713:
                character = "AAK";
                break;
            case 714:
                character = "AAL";
                break;
            case 715:
                character = "AAM";
                break;
            case 716:
                character = "AAN";
                break;
            case 717:
                character = "AAO";
                break;
            case 718:
                character = "AAP";
                break;
            case 719:
                character = "AAQ";
                break;
            case 720:
                character = "AAR";
                break;
            case 721:
                character = "AAS";
                break;
            case 722:
                character = "AAT";
                break;
            case 723:
                character = "AAU";
                break;
            case 724:
                character = "AAV";
                break;
            case 725:
                character = "AAW";
                break;
            case 726:
                character = "AAX";
                break;
            case 727:
                character = "AAY";
                break;
            case 728:
                character = "AAZ";
                break;
            case 729:
                character = "ABA";
                break;
            case 730:
                character = "ABB";
                break;
            case 731:
                character = "ABC";
                break;
            case 732:
                character = "ABD";
                break;
            case 733:
                character = "ABE";
                break;
            case 734:
                character = "ABF";
                break;
            case 735:
                character = "ABG";
                break;
            case 736:
                character = "ABH";
                break;
            case 737:
                character = "ABI";
                break;
            case 738:
                character = "ABJ";
                break;
            case 739:
                character = "ABK";
                break;
            case 740:
                character = "ABL";
                break;
            case 741:
                character = "ABM";
                break;
            case 742:
                character = "ABN";
                break;
            case 743:
                character = "ABO";
                break;
            case 744:
                character = "ABP";
                break;
            case 745:
                character = "ABQ";
                break;
            case 746:
                character = "ABR";
                break;
            case 747:
                character = "ABS";
                break;
            case 748:
                character = "ABT";
                break;
            case 749:
                character = "ABU";
                break;
            case 750:
                character = "ABV";
                break;
            case 751:
                character = "ABW";
                break;
            case 752:
                character = "ABX";
                break;
            case 753:
                character = "ABY";
                break;
            case 754:
                character = "ABZ";
                break;
            case 755:
                character = "ACA";
                break;
            case 756:
                character = "ACB";
                break;
            case 757:
                character = "ACC";
                break;
            case 758:
                character = "ACD";
                break;
            case 759:
                character = "ACE";
                break;
            case 760:
                character = "ACF";
                break;
            case 761:
                character = "ACG";
                break;
            case 762:
                character = "ACH";
                break;
            case 763:
                character = "ACI";
                break;
            case 764:
                character = "ACJ";
                break;
            case 765:
                character = "ACK";
                break;
            case 766:
                character = "ACL";
                break;
            case 767:
                character = "ACM";
                break;
            case 768:
                character = "ACN";
                break;
            case 769:
                character = "ACO";
                break;
            case 770:
                character = "ACP";
                break;
            case 771:
                character = "ACQ";
                break;
            case 772:
                character = "ACR";
                break;
            case 773:
                character = "ACS";
                break;
            case 774:
                character = "ACT";
                break;
            case 775:
                character = "ACU";
                break;
            case 776:
                character = "ACV";
                break;
            case 777:
                character = "ACW";
                break;
            case 778:
                character = "ACX";
                break;
            case 779:
                character = "ACY";
                break;
            case 780:
                character = "ACZ";
                break;
            case 781:
                character = "ADA";
                break;
            case 782:
                character = "ADB";
                break;
            case 783:
                character = "ADC";
                break;
            case 784:
                character = "ADD";
                break;
            case 785:
                character = "ADE";
                break;
            case 786:
                character = "ADF";
                break;
            case 787:
                character = "ADG";
                break;
            case 788:
                character = "ADH";
                break;
            case 789:
                character = "ADI";
                break;
            case 790:
                character = "ADJ";
                break;
            case 791:
                character = "ADK";
                break;
            case 792:
                character = "ADL";
                break;
            case 793:
                character = "ADM";
                break;
            case 794:
                character = "ADN";
                break;
            case 795:
                character = "ADO";
                break;
            case 796:
                character = "ADP";
                break;
            case 797:
                character = "ADQ";
                break;
            case 798:
                character = "ADR";
                break;
            case 799:
                character = "ADS";
                break;
            case 800:
                character = "ADT";
                break;
            case 801:
                character = "ADU";
                break;
            case 802:
                character = "ADV";
                break;
            case 803:
                character = "ADW";
                break;
            case 804:
                character = "ADX";
                break;
            case 805:
                character = "ADY";
                break;
            case 806:
                character = "ADZ";
                break;
            case 807:
                character = "AEA";
                break;
            case 808:
                character = "AEB";
                break;
            case 809:
                character = "AEC";
                break;
            case 810:
                character = "AED";
                break;
            case 811:
                character = "AEE";
                break;
            case 812:
                character = "AEF";
                break;
            case 813:
                character = "AEG";
                break;
            case 814:
                character = "AEH";
                break;
            case 815:
                character = "AEI";
                break;
            case 816:
                character = "AEJ";
                break;
            case 817:
                character = "AEK";
                break;
            case 818:
                character = "AEL";
                break;
            case 819:
                character = "AEM";
                break;
            case 820:
                character = "AEN";
                break;
            case 821:
                character = "AEO";
                break;
            case 822:
                character = "AEP";
                break;
            case 823:
                character = "AEQ";
                break;
            case 824:
                character = "AER";
                break;
            case 825:
                character = "AES";
                break;
            case 826:
                character = "AET";
                break;
            case 827:
                character = "AEU";
                break;
            case 828:
                character = "AEV";
                break;
            case 829:
                character = "AEW";
                break;
            case 830:
                character = "AEX";
                break;
            case 831:
                character = "AEY";
                break;
            case 832:
                character = "AEZ";
                break;
            case 833:
                character = "AFA";
                break;
            case 834:
                character = "AFB";
                break;
            case 835:
                character = "AFC";
                break;
            case 836:
                character = "AFD";
                break;
            case 837:
                character = "AFE";
                break;
            case 838:
                character = "AFF";
                break;
            case 839:
                character = "AFG";
                break;
            case 840:
                character = "AFH";
                break;
            case 841:
                character = "AFI";
                break;
            case 842:
                character = "AFJ";
                break;
            case 843:
                character = "AFK";
                break;
            case 844:
                character = "AFL";
                break;
            case 845:
                character = "AFM";
                break;
            case 846:
                character = "AFN";
                break;
            case 847:
                character = "AFO";
                break;
            case 848:
                character = "AFP";
                break;
            case 849:
                character = "AFQ";
                break;
            case 850:
                character = "AFR";
                break;
            case 851:
                character = "AFS";
                break;
            case 852:
                character = "AFT";
                break;
            case 853:
                character = "AFU";
                break;
            case 854:
                character = "AFV";
                break;
            case 855:
                character = "AFW";
                break;
            case 856:
                character = "AFX";
                break;
            case 857:
                character = "AFY";
                break;
            case 858:
                character = "AFZ";
                break;
            case 859:
                character = "AGA";
                break;
            case 860:
                character = "AGB";
                break;
            case 861:
                character = "AGC";
                break;
            case 862:
                character = "AGD";
                break;
            case 863:
                character = "AGE";
                break;
            case 864:
                character = "AGF";
                break;
            case 865:
                character = "AGG";
                break;
            case 866:
                character = "AGH";
                break;
            case 867:
                character = "AGI";
                break;
            case 868:
                character = "AGJ";
                break;
            case 869:
                character = "AGK";
                break;
            case 870:
                character = "AGL";
                break;
            case 871:
                character = "AGM";
                break;
            case 872:
                character = "AGN";
                break;
            case 873:
                character = "AGO";
                break;
            case 874:
                character = "AGP";
                break;
            case 875:
                character = "AGQ";
                break;
            case 876:
                character = "AGR";
                break;
            case 877:
                character = "AGS";
                break;
            case 878:
                character = "AGT";
                break;
            case 879:
                character = "AGU";
                break;
            case 880:
                character = "AGV";
                break;
            case 881:
                character = "AGW";
                break;
            case 882:
                character = "AGX";
                break;
            case 883:
                character = "AGY";
                break;
            case 884:
                character = "AGZ";
                break;
            case 885:
                character = "AHA";
                break;
            case 886:
                character = "AHB";
                break;
            case 887:
                character = "AHC";
                break;
            case 888:
                character = "AHD";
                break;
            case 889:
                character = "AHE";
                break;
            case 890:
                character = "AHF";
                break;
            case 891:
                character = "AHG";
                break;
            case 892:
                character = "AHH";
                break;
            case 893:
                character = "AHI";
                break;
            case 894:
                character = "AHJ";
                break;
            case 895:
                character = "AHK";
                break;
            case 896:
                character = "AHL";
                break;
            case 897:
                character = "AHM";
                break;
            case 898:
                character = "AHN";
                break;
            case 899:
                character = "AHO";
                break;
            case 900:
                character = "AHP";
                break;
            case 901:
                character = "AHQ";
                break;
            case 902:
                character = "AHR";
                break;
            case 903:
                character = "AHS";
                break;
            case 904:
                character = "AHT";
                break;
            case 905:
                character = "AHU";
                break;
            case 906:
                character = "AHV";
                break;
            case 907:
                character = "AHW";
                break;
            case 908:
                character = "AHX";
                break;
            case 909:
                character = "AHY";
                break;
            case 910:
                character = "AHZ";
                break;
            case 911:
                character = "AIA";
                break;
            case 912:
                character = "AIB";
                break;
            case 913:
                character = "AIC";
                break;
            case 914:
                character = "AID";
                break;
            case 915:
                character = "AIE";
                break;
            case 916:
                character = "AIF";
                break;
            case 917:
                character = "AIG";
                break;
            case 918:
                character = "AIH";
                break;
            case 919:
                character = "AII";
                break;
            case 920:
                character = "AIJ";
                break;
            case 921:
                character = "AIK";
                break;
            case 922:
                character = "AIL";
                break;
            case 923:
                character = "AIM";
                break;
            case 924:
                character = "AIN";
                break;
            case 925:
                character = "AIO";
                break;
            case 926:
                character = "AIP";
                break;
            case 927:
                character = "AIQ";
                break;
            case 928:
                character = "AIR";
                break;
            case 929:
                character = "AIS";
                break;
            case 930:
                character = "AIT";
                break;
            case 931:
                character = "AIU";
                break;
            case 932:
                character = "AIV";
                break;
            case 933:
                character = "AIW";
                break;
            case 934:
                character = "AIX";
                break;
            case 935:
                character = "AIY";
                break;
            case 936:
                character = "AIZ";
                break;
            case 937:
                character = "AJA";
                break;
            case 938:
                character = "AJB";
                break;
            case 939:
                character = "AJC";
                break;
            case 940:
                character = "AJD";
                break;
            case 941:
                character = "AJE";
                break;
            case 942:
                character = "AJF";
                break;
            case 943:
                character = "AJG";
                break;
            case 944:
                character = "AJH";
                break;
            case 945:
                character = "AJI";
                break;
            case 946:
                character = "AJJ";
                break;
            case 947:
                character = "AJK";
                break;
            case 948:
                character = "AJL";
                break;
            case 949:
                character = "AJM";
                break;
            case 950:
                character = "AJN";
                break;
            case 951:
                character = "AJO";
                break;
            case 952:
                character = "AJP";
                break;
            case 953:
                character = "AJQ";
                break;
            case 954:
                character = "AJR";
                break;
            case 955:
                character = "AJS";
                break;
            case 956:
                character = "AJT";
                break;
            case 957:
                character = "AJU";
                break;
            case 958:
                character = "AJV";
                break;
            case 959:
                character = "AJW";
                break;
            case 960:
                character = "AJX";
                break;
            case 961:
                character = "AJY";
                break;
            case 962:
                character = "AJZ";
                break;
            case 963:
                character = "AKA";
                break;
            case 964:
                character = "AKB";
                break;
            case 965:
                character = "AKC";
                break;
            case 966:
                character = "AKD";
                break;
            case 967:
                character = "AKE";
                break;
            case 968:
                character = "AKF";
                break;
            case 969:
                character = "AKG";
                break;
            case 970:
                character = "AKH";
                break;
            case 971:
                character = "AKI";
                break;
            case 972:
                character = "AKJ";
                break;
            case 973:
                character = "AKK";
                break;
            case 974:
                character = "AKL";
                break;
            case 975:
                character = "AKM";
                break;
            case 976:
                character = "AKN";
                break;
            case 977:
                character = "AKO";
                break;
            case 978:
                character = "AKP";
                break;
            case 979:
                character = "AKQ";
                break;
            case 980:
                character = "AKR";
                break;
            case 981:
                character = "AKS";
                break;
            case 982:
                character = "AKT";
                break;
            case 983:
                character = "AKU";
                break;
            case 984:
                character = "AKV";
                break;
            case 985:
                character = "AKW";
                break;
            case 986:
                character = "AKX";
                break;
            case 987:
                character = "AKY";
                break;
            case 988:
                character = "AKZ";
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
        AddColumns(ref dt, "Average Rate");
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
        DataTable dsAverageRate = new DataTable();

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
                decimal AverageRate = 0;
                dsRate = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct Max(EM.Rate) As Rate From EstimateAndMaterialOthersRelations EM  INNER JOIN Estimate ES ON ES.EstId=EM.EstId  WHERE ES.WAId='" + ddlNameOfWork.SelectedValue + "'and ES.AcaId ='" + ddlAcademy.SelectedValue + "' and EM.PSID ='" + (int)TypeEnum.PurchaseSourceID.Local + "' and ES.IsApproved=1 and EM.MatId='" + dsDes.Rows[i]["MatId"].ToString() + "'").Tables[0];
                dsAverageRate = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct SUM(EM.Rate) As Rate,COUNT(*) AS TotalBill From [dbo].[SubmitBillByUserAndMaterialOthersRelation] EM  INNER JOIN [dbo].[SubmitBillByUser] ES ON ES.SubBillId=EM.SubBillId  WHERE ES.WAId='" + ddlNameOfWork.SelectedValue + "'and ES.AcaId ='" + ddlAcademy.SelectedValue + "' and EM.MatId='" + dsDes.Rows[i]["MatId"].ToString() + "' and ISNULL(ES.FirstVarifyStatus,1)=1 ").Tables[0];
                if (dsAverageRate.Rows[0]["Rate"].ToString() != "")
                {
                    AverageRate = Convert.ToDecimal(dsAverageRate.Rows[0]["Rate"].ToString());
                }

                decimal BillQty = 0;
                decimal EstimateQty = 0;
                dr = dataTable.NewRow();
                dr[0] = i + 1;
                dr["Name Of Material"] = dsDes.Rows[i]["MatName"].ToString();
                dr["Estimate Rate"] = dsRate.Rows[0]["Rate"].ToString();
                if (dsAverageRate.Rows[0]["Rate"].ToString() != "")
                {
                    dr["Average Rate"] = AverageRate / Convert.ToInt32(dsAverageRate.Rows[0]["TotalBill"].ToString());
                }
                else
                {
                    dr["Average Rate"] = 0;
                }

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