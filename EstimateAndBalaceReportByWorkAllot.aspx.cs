﻿using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EstimateAndBalaceReportByWorkAllot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindZone();
        }
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Bills Report");
        string FileName = string.Empty;
        FileName = "EstimateAndBalanceCostReport.xlsx";
        dt = GetDispatchAndPendingReport();
        int rowNum = 1;
        int col = 1;

        ws.Cell("D" + rowNum).Value = "ESTIMATE AND BALANCE COST  REPORT";
        foreach (DataColumn dtcol in dt.Columns)
        {
            var rngTable1 = ws.Range(getAlphabeticCharacter(col) + rowNum);
            rngTable1.FirstCell().Style.Font.SetBold().Fill.SetBackgroundColor(XLColor.CornflowerBlue).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.Bold = true;
            col += 1;
        }
        rowNum = rowNum + 1;
        int tcol = 1;
        foreach (DataColumn dtcol in dt.Columns)
        {
            ws.Cell(getAlphabeticCharacter(tcol) + rowNum).Value = dtcol.ColumnName;
            var rngTable2 = ws.Range(getAlphabeticCharacter(tcol) + rowNum);
            rngTable2.FirstCell().Style.Font.SetBold().Fill.SetBackgroundColor(XLColor.Aqua).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.Bold = true;
            tcol += 1;
        }

        rowNum = rowNum + 1;
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            int dataCol = 1;
            for (int j = 1; j <= dt.Columns.Count; j++)
            {
                if (dt.Rows[i][(j - 1)].ToString() != "")
                {
                    ws.Cell(getAlphabeticCharacter(dataCol) + rowNum).Value = dt.Rows[i][(j - 1)].ToString();
                }
                else
                {
                    ws.Cell(getAlphabeticCharacter(dataCol) + rowNum).Value = 0;
                }
                dataCol += 1;
            }
            rowNum += 1;
        }
        ws.Columns().AdjustToContents();

        string FilePath = Server.MapPath("EstFile") + "\\" + FileName;
        wb.SaveAs(@FilePath);

        Response.Clear();
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", FileName));
        Response.ContentType = "application/octet-stream";
        Response.WriteFile(@FilePath);
        Response.End();
    }

    protected DataTable GetDispatchAndPendingReport()
    {
        DataRow dr = null;

        DataTable dataTable = BillDetailDataTable();

        DataTable dtMaterialColumns = DAL.DalAccessUtility.GetDataInDataSet("select AcaName,AcaID from academy where Zoneid='"+ddlZone.SelectedValue+"'").Tables[0];
        if (dtMaterialColumns != null)
        {
            for (int i = 0; i < dtMaterialColumns.Rows.Count; i++)
            {
              
                DataTable dsTotalQty = new DataTable();
                dr = dataTable.NewRow();
                dr["ACADEMY_NAME"] = dtMaterialColumns.Rows[i]["AcaName"].ToString();
                if (dtMaterialColumns.Rows.Count == i + 1)
                {
                    dr["ACADEMY_NAME"] = "TOTAL COST";

                }

                DataTable dtWorkAllotColumns = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct WorkAllotName FROM WorkAllot WHERE  Zoneid='" + ddlZone.SelectedValue + "' and Active=1").Tables[0];
                for (int j = 0; j < dtWorkAllotColumns.Rows.Count; j++)
                {

                    DataTable dsEstimateCost = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct SUM(ER.Rate * ER.Qty) As EstimateCost FROM Estimate E Inner JOIn EstimateAndMaterialOthersRelations ER on ER.EstId=E.EstId Inner join Workallot W on W.waid=E.WAId WHERE W.Workallotname='" + dtWorkAllotColumns.Rows[j]["WorkAllotName"].ToString() + "' and ER.PSID ='" + (int)TypeEnum.PurchaseSourceID.Local + "' and E.IsApproved=1 and E.acaid='" + dtMaterialColumns.Rows[i]["AcaID"].ToString() + "'").Tables[0];

                    DataTable dsPurchaseCost = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct SUM(SE.Amount) AS PurchaseCost FROM SubmitBillByUser S INNER JOIN SubmitBillByUserAndMaterialOthersRelation SE ON S.SubBillId = SE.SubBillId Inner join Workallot W on W.waid=S.WAId  WHERE W.Workallotname='" + dtWorkAllotColumns.Rows[j]["WorkAllotName"].ToString() + "' AND ISNULL(S.FirstVarifyStatus,-1) != 0 AND ISNULL(S.SecondVarifyStatus,-1) != 0 and S.acaid='" + dtMaterialColumns.Rows[i]["AcaID"].ToString() + "'").Tables[0];

                    decimal Totalcost = 0;
                    if (dsEstimateCost.Rows.Count > 0 && dsEstimateCost != null)
                    {
                        if (dsEstimateCost.Rows[0]["EstimateCost"].ToString() == "" && dsPurchaseCost.Rows[0]["PurchaseCost"].ToString() == "")
                        {
                            dr[dtWorkAllotColumns.Rows[j]["WorkAllotName"].ToString()] = "Estimate:  0.00 ,Balance:  0.00";
                        }
                        else if (dsEstimateCost.Rows[0]["EstimateCost"].ToString() == "" && dsPurchaseCost.Rows[0]["PurchaseCost"].ToString() != "")
                        {
                            Totalcost = Convert.ToDecimal(0.00) - Convert.ToDecimal(dsPurchaseCost.Rows[0]["PurchaseCost"].ToString());
                            dr[dtWorkAllotColumns.Rows[j]["WorkAllotName"].ToString()] = "Estimate:  0.00 ,Balance: " + Totalcost;
                        }
                        else if (dsEstimateCost.Rows[0]["EstimateCost"].ToString() != "" && dsPurchaseCost.Rows[0]["PurchaseCost"].ToString() == "")
                        {
                            dr[dtWorkAllotColumns.Rows[j]["WorkAllotName"].ToString()] = "Estimate: " + dsEstimateCost.Rows[0]["EstimateCost"].ToString() + "   ,Balance:" + dsEstimateCost.Rows[0]["EstimateCost"].ToString();
                        }
                        else
                        {
                            Totalcost = Convert.ToDecimal(dsEstimateCost.Rows[0]["EstimateCost"].ToString()) - Convert.ToDecimal(dsPurchaseCost.Rows[0]["PurchaseCost"].ToString());
                            dr[dtWorkAllotColumns.Rows[j]["WorkAllotName"].ToString()] = "Estimate: " + dsEstimateCost.Rows[0]["EstimateCost"].ToString() + "   ,Balance: " + Totalcost;
                        }
                        DataTable EstimateTotalCost = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct SUM(ER.Rate * ER.Qty) As EstimateCost FROM Estimate E Inner JOIn EstimateAndMaterialOthersRelations ER on ER.EstId=E.EstId INNER JOIN WorkAllot W ON W.WAId =E.WAId WHERE W.Workallotname='" + dtWorkAllotColumns.Rows[j]["WorkAllotName"].ToString() + "' and ER.PSID ='" + (int)TypeEnum.PurchaseSourceID.Local + "' and E.IsApproved=1 and E.Zoneid='" + ddlZone.SelectedValue + "'").Tables[0];

                        DataTable dsPurchaseTotalCost = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct SUM(SE.Amount) AS PurchaseCost FROM SubmitBillByUser S INNER JOIN SubmitBillByUserAndMaterialOthersRelation SE ON S.SubBillId = SE.SubBillId INNER JOIN Workallot W on W.waid=S.WAId  WHERE W.Workallotname='" + dtWorkAllotColumns.Rows[j]["WorkAllotName"].ToString() + "' AND ISNULL(S.FirstVarifyStatus,-1) != 0 AND ISNULL(S.SecondVarifyStatus,-1) != 0 and S.Zoneid='" + ddlZone.SelectedValue + "'").Tables[0];

                        decimal TotalPurchaseCost = 0;
                        if (dtMaterialColumns.Rows.Count == i + 1)
                        {
                            if (EstimateTotalCost.Rows[0]["EstimateCost"].ToString() == "" && dsPurchaseTotalCost.Rows[0]["PurchaseCost"].ToString() == "")
                            {
                                dr[dtWorkAllotColumns.Rows[j]["WorkAllotName"].ToString()] = "Estimate Cost:  0.00 ,Balance Cost:  0.00";
                            }
                            else if (EstimateTotalCost.Rows[0]["EstimateCost"].ToString() == "" && dsPurchaseTotalCost.Rows[0]["PurchaseCost"].ToString() != "")
                            {
                                TotalPurchaseCost = Convert.ToDecimal(0.00) - Convert.ToDecimal(dsPurchaseTotalCost.Rows[0]["PurchaseCost"].ToString());

                                dr[dtWorkAllotColumns.Rows[j]["WorkAllotName"].ToString()] = "Estimate Cost:  0.00 ,Balance Cost: " + TotalPurchaseCost;
                            }
                            else if (EstimateTotalCost.Rows[0]["EstimateCost"].ToString() != "" && dsPurchaseTotalCost.Rows[0]["PurchaseCost"].ToString() == "")
                            {
                                dr[dtWorkAllotColumns.Rows[j]["WorkAllotName"].ToString()] = "Estimate Cost: " + EstimateTotalCost.Rows[0]["EstimateCost"].ToString() + "   ,Balance Cost:" + EstimateTotalCost.Rows[0]["EstimateCost"].ToString();
                            }
                            else
                            {
                                TotalPurchaseCost = Convert.ToDecimal(EstimateTotalCost.Rows[0]["EstimateCost"].ToString()) - Convert.ToDecimal(dsPurchaseTotalCost.Rows[0]["PurchaseCost"].ToString());

                                dr[dtWorkAllotColumns.Rows[j]["WorkAllotName"].ToString()] = "Estimate Cost: " + EstimateTotalCost.Rows[0]["EstimateCost"].ToString() + "   ,Balance Cost: " + TotalPurchaseCost;
                            }
                        }
                    }
                }
                dataTable.Rows.Add(dr);
            }
        }
        return dataTable;

    }

    private DataTable BillDetailDataTable()
    {
        DataTable dt = new DataTable();
        AddColumns(ref dt, "ACADEMY_NAME");
      
        DataTable dtMaterialColumns = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct WorkAllotName FROM WorkAllot WHERE  Zoneid='" + ddlZone.SelectedValue + "' and Active=1").Tables[0];

        for (int i = 0; i < dtMaterialColumns.Rows.Count; i++)
        {
            AddColumns(ref dt, dtMaterialColumns.Rows[i]["WorkAllotName"].ToString());
           
        }
        
        return dt;
    }
    
    private void AddColumns(ref DataTable dt, string columnName)
    {
        dt.Columns.Add(new DataColumn(columnName, typeof(string)));
    }
   
    protected void BindZone()
    {
        DataTable dsZone = new DataTable();

        dsZone = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId,ZoneName  from Zone where Active=1 Order by ZoneName asc").Tables[0];
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
            case 989:
                character = "ALA";
                break;
            case 990:
                character = "ALB";
                break;
            case 991:
                character = "ALC";
                break;
            case 992:
                character = "ALD";
                break;
            case 993:
                character = "ALE";
                break;
            case 994:
                character = "ALF";
                break;
            case 995:
                character = "ALG";
                break;
            case 996:
                character = "ALH";
                break;
            case 997:
                character = "ALI";
                break;
            case 998:
                character = "ALJ";
                break;
            case 999:
                character = "ALK";
                break;
            case 1000:
                character = "ALL";
                break;
            case 1001:
                character = "ALM";
                break;
            case 1002:
                character = "ALN";
                break;
            case 1003:
                character = "ALO";
                break;
            case 1004:
                character = "ALP";
                break;
            case 1005:
                character = "ALQ";
                break;
            case 1006:
                character = "ALR";
                break;
            case 1007:
                character = "ALS";
                break;
            case 1008:
                character = "ALT";
                break;
            case 1009:
                character = "ALU";
                break;
            case 1010:
                character = "ALV";
                break;
            case 1011:
                character = "ALW";
                break;
            case 1012:
                character = "ALX";
                break;
            case 1013:
                character = "ALY";
                break;
            case 1014:
                character = "ALZ";
                break;
            case 1015:
                character = "AMA";
                break;
            case 1016:
                character = "AMB";
                break;
            case 1017:
                character = "AMC";
                break;
            case 1018:
                character = "AMD";
                break;
            case 1019:
                character = "AME";
                break;
            case 1020:
                character = "AMF";
                break;
            case 1021:
                character = "AMG";
                break;
            case 1022:
                character = "AMH";
                break;
            case 1023:
                character = "AMI";
                break;
            case 1024:
                character = "AMJ";
                break;
            case 1025:
                character = "AMK";
                break;
            case 1026:
                character = "AML";
                break;
            case 1027:
                character = "AMM";
                break;
            case 1028:
                character = "AMN";
                break;
            case 1029:
                character = "AMO";
                break;
            case 1030:
                character = "AMP";
                break;
            case 1031:
                character = "AMQ";
                break;
            case 1032:
                character = "AMR";
                break;
            case 1033:
                character = "AMS";
                break;
            case 1034:
                character = "AMT";
                break;
            case 1035:
                character = "AMU";
                break;
            case 1036:
                character = "AMV";
                break;
            case 1037:
                character = "AMW";
                break;
            case 1038:
                character = "AMX";
                break;
            case 1039:
                character = "AMY";
                break;
            case 1040:
                character = "AMZ";
                break;
            case 1041:
                character = "ANA";
                break;
            case 1042:
                character = "ANB";
                break;
            case 1043:
                character = "ANC";
                break;
            case 1044:
                character = "AND";
                break;
            case 1045:
                character = "ANE";
                break;
            case 1046:
                character = "ANF";
                break;
            case 1047:
                character = "ANG";
                break;
            case 1048:
                character = "ANH";
                break;
            case 1049:
                character = "ANI";
                break;
            case 1050:
                character = "ANJ";
                break;
            case 1051:
                character = "ANK";
                break;
            case 1052:
                character = "ANL";
                break;
            case 1053:
                character = "ANM";
                break;
            case 1054:
                character = "ANN";
                break;
            case 1055:
                character = "ANO";
                break;
            case 1056:
                character = "ANP";
                break;
            case 1057:
                character = "ANQ";
                break;
            case 1058:
                character = "ANR";
                break;
            case 1059:
                character = "ANS";
                break;
            case 1060:
                character = "ANT";
                break;
            case 1061:
                character = "ANU";
                break;
            case 1062:
                character = "ANV";
                break;
            case 1063:
                character = "ANW";
                break;
            case 1064:
                character = "ANX";
                break;
            case 1065:
                character = "ANY";
                break;
            case 1066:
                character = "ANZ";
                break;
            case 1067:
                character = "AOA";
                break;
            case 1068:
                character = "AOB";
                break;
            case 1069:
                character = "AOC";
                break;
            case 1070:
                character = "AOD";
                break;
            case 1071:
                character = "AOE";
                break;
            case 1072:
                character = "AOF";
                break;
            case 1073:
                character = "AOG";
                break;
            case 1074:
                character = "AOH";
                break;
            case 1075:
                character = "AOI";
                break;
            case 1076:
                character = "AOJ";
                break;
            case 1077:
                character = "AOK";
                break;
            case 1078:
                character = "AOL";
                break;
            case 1079:
                character = "AOM";
                break;
            case 1080:
                character = "AON";
                break;
            case 1081:
                character = "AOO";
                break;
            case 1082:
                character = "AOP";
                break;
            case 1083:
                character = "AOQ";
                break;
            case 1084:
                character = "AOR";
                break;
            case 1085:
                character = "AOS";
                break;
            case 1086:
                character = "AOT";
                break;
            case 1087:
                character = "AOU";
                break;
            case 1088:
                character = "AOV";
                break;
            case 1089:
                character = "AOW";
                break;
            case 1090:
                character = "AOX";
                break;
            case 1091:
                character = "AOY";
                break;
            case 1092:
                character = "AOZ";
                break;
            case 1093:
                character = "APA";
                break;
            case 1094:
                character = "APB";
                break;
            case 1095:
                character = "APC";
                break;
            case 1096:
                character = "APD";
                break;
            case 1097:
                character = "APE";
                break;
            case 1098:
                character = "APF";
                break;
            case 1099:
                character = "APG";
                break;
            case 1100:
                character = "APH";
                break;
            case 1101:
                character = "API";
                break;
            case 1102:
                character = "APJ";
                break;
            case 1103:
                character = "APK";
                break;
            case 1104:
                character = "APL";
                break;
            case 1105:
                character = "APM";
                break;
            case 1106:
                character = "APN";
                break;
            case 1107:
                character = "APO";
                break;
            case 1108:
                character = "APP";
                break;
            case 1109:
                character = "APQ";
                break;
            case 1110:
                character = "APR";
                break;
            case 1111:
                character = "APS";
                break;
            case 1112:
                character = "APT";
                break;
            case 1113:
                character = "APU";
                break;
            case 1114:
                character = "APV";
                break;
            case 1115:
                character = "APW";
                break;
            case 1116:
                character = "APX";
                break;
            case 1117:
                character = "APY";
                break;
            case 1118:
                character = "APZ";
                break;
            case 1119:
                character = "AQA";
                break;
            case 1120:
                character = "AQB";
                break;
            case 1121:
                character = "AQC";
                break;
            case 1122:
                character = "AQD";
                break;
            case 1123:
                character = "AQE";
                break;
            case 1124:
                character = "AQF";
                break;
            case 1125:
                character = "AQG";
                break;
            case 1126:
                character = "AQH";
                break;
            case 1127:
                character = "AQI";
                break;
            case 1128:
                character = "AQJ";
                break;
            case 1129:
                character = "AQK";
                break;
            case 1130:
                character = "AQL";
                break;
            case 1131:
                character = "AQM";
                break;
            case 1132:
                character = "AQN";
                break;
            case 1133:
                character = "AQO";
                break;
            case 1134:
                character = "AQP";
                break;
            case 1135:
                character = "AQQ";
                break;
            case 1136:
                character = "AQR";
                break;
            case 1137:
                character = "AQS";
                break;
            case 1138:
                character = "AQT";
                break;
            case 1139:
                character = "AQU";
                break;
            case 1140:
                character = "AQV";
                break;
            case 1141:
                character = "AQW";
                break;
            case 1142:
                character = "AQX";
                break;
            case 1143:
                character = "AQY";
                break;
            case 1144:
                character = "AQZ";
                break;
            case 1145:
                character = "ARA";
                break;
            case 1146:
                character = "ARB";
                break;
            case 1147:
                character = "ARC";
                break;
            case 1148:
                character = "ARD";
                break;
            case 1149:
                character = "ARE";
                break;
            case 1150:
                character = "ARF";
                break;
            case 1151:
                character = "ARG";
                break;
            case 1152:
                character = "ARH";
                break;
            case 1153:
                character = "ARI";
                break;
            case 1154:
                character = "ARJ";
                break;
            case 1155:
                character = "ARK";
                break;
            case 1156:
                character = "ARL";
                break;
            case 1157:
                character = "ARM";
                break;
            case 1158:
                character = "ARN";
                break;
            case 1159:
                character = "ARO";
                break;
            case 1160:
                character = "ARP";
                break;
            case 1161:
                character = "ARQ";
                break;
            case 1162:
                character = "ARR";
                break;
            case 1163:
                character = "ARS";
                break;
            case 1164:
                character = "ART";
                break;
            case 1165:
                character = "ARU";
                break;
            case 1166:
                character = "ARV";
                break;
            case 1167:
                character = "ARW";
                break;
            case 1168:
                character = "ARX";
                break;
            case 1169:
                character = "ARY";
                break;
            case 1170:
                character = "ARZ";
                break;
            case 1171:
                character = "ASA";
                break;
            case 1172:
                character = "ASB";
                break;
            case 1173:
                character = "ASC";
                break;
            case 1174:
                character = "ASD";
                break;
            case 1175:
                character = "ASE";
                break;
            case 1176:
                character = "ASF";
                break;
            case 1177:
                character = "ASG";
                break;
            case 1178:
                character = "ASH";
                break;
            case 1179:
                character = "ASI";
                break;
            case 1180:
                character = "ASJ";
                break;
            case 1181:
                character = "ASK";
                break;
            case 1182:
                character = "ASL";
                break;
            case 1183:
                character = "ASM";
                break;
            case 1184:
                character = "ASN";
                break;
            case 1185:
                character = "ASO";
                break;
            case 1186:
                character = "ASP";
                break;
            case 1187:
                character = "ASQ";
                break;
            case 1188:
                character = "ASR";
                break;
            case 1189:
                character = "ASS";
                break;
            case 1190:
                character = "AST";
                break;
            case 1191:
                character = "ASU";
                break;
            case 1192:
                character = "ASV";
                break;
            case 1193:
                character = "ASW";
                break;
            case 1194:
                character = "ASX";
                break;
            case 1195:
                character = "ASY";
                break;
            case 1196:
                character = "ASZ";
                break;
            case 1197:
                character = "ATA";
                break;
            case 1198:
                character = "ATB";
                break;
            case 1199:
                character = "ATC";
                break;
            case 1200:
                character = "ATD";
                break;
            case 1201:
                character = "ATE";
                break;
            case 1202:
                character = "ATF";
                break;
            case 1203:
                character = "ATG";
                break;
            case 1204:
                character = "ATH";
                break;
            case 1205:
                character = "ATI";
                break;
            case 1206:
                character = "ATJ";
                break;
            case 1207:
                character = "ATK";
                break;
            case 1208:
                character = "ATL";
                break;
            case 1209:
                character = "ATM";
                break;
            case 1210:
                character = "ATN";
                break;
            case 1211:
                character = "ATO";
                break;
            case 1212:
                character = "ATP";
                break;
            case 1213:
                character = "ATQ";
                break;
            case 1214:
                character = "ATR";
                break;
            case 1215:
                character = "ATS";
                break;
            case 1216:
                character = "ATT";
                break;
            case 1217:
                character = "ATU";
                break;
            case 1218:
                character = "ATV";
                break;
            case 1219:
                character = "ATW";
                break;
            case 1220:
                character = "ATX";
                break;
            case 1221:
                character = "ATY";
                break;
            case 1222:
                character = "ATZ";
                break;
            case 1223:
                character = "AUA";
                break;
            case 1224:
                character = "AUB";
                break;
            case 1225:
                character = "AUC";
                break;
            case 1226:
                character = "AUD";
                break;
            case 1227:
                character = "AUE";
                break;
            case 1228:
                character = "AUF";
                break;
            case 1229:
                character = "AUG";
                break;
            case 1230:
                character = "AUH";
                break;
            case 1231:
                character = "AUI";
                break;
            case 1232:
                character = "AUJ";
                break;
            case 1233:
                character = "AUK";
                break;
            case 1234:
                character = "AUL";
                break;
            case 1235:
                character = "AUM";
                break;
            case 1236:
                character = "AUN";
                break;
            case 1237:
                character = "AUO";
                break;
            case 1238:
                character = "AUP";
                break;
            case 1239:
                character = "AUQ";
                break;
            case 1240:
                character = "AUR";
                break;
            case 1241:
                character = "AUS";
                break;
            case 1242:
                character = "AUT";
                break;
            case 1243:
                character = "AUU";
                break;
            case 1244:
                character = "AUV";
                break;
            case 1245:
                character = "AUW";
                break;
            case 1246:
                character = "AUX";
                break;
            case 1247:
                character = "AUY";
                break;
            case 1248:
                character = "AUZ";
                break;
            case 1249:
                character = "AVA";
                break;
            case 1250:
                character = "AVB";
                break;
            case 1251:
                character = "AVC";
                break;
            case 1252:
                character = "AVD";
                break;
            case 1253:
                character = "AVE";
                break;
            case 1254:
                character = "AVF";
                break;
            case 1255:
                character = "AVG";
                break;
            case 1256:
                character = "AVH";
                break;
            case 1257:
                character = "AVI";
                break;
            case 1258:
                character = "AVJ";
                break;
            case 1259:
                character = "AVK";
                break;
            case 1260:
                character = "AVL";
                break;
            case 1261:
                character = "AVM";
                break;
            case 1262:
                character = "AVN";
                break;
            case 1263:
                character = "AVO";
                break;
            case 1264:
                character = "AVP";
                break;
            case 1265:
                character = "AVQ";
                break;
            case 1266:
                character = "AVR";
                break;
            case 1267:
                character = "AVS";
                break;
            case 1268:
                character = "AVT";
                break;
            case 1269:
                character = "AVU";
                break;
            case 1270:
                character = "AVV";
                break;
            case 1271:
                character = "AVW";
                break;
            case 1272:
                character = "AVX";
                break;
            case 1273:
                character = "AVY";
                break;
            case 1274:
                character = "AVZ";
                break;
            case 1275:
                character = "AWA";
                break;
            case 1276:
                character = "AWB";
                break;
            case 1277:
                character = "AWC";
                break;
            case 1278:
                character = "AWD";
                break;
            case 1279:
                character = "AWE";
                break;
            case 1280:
                character = "AWF";
                break;
            case 1281:
                character = "AWG";
                break;
            case 1282:
                character = "AWH";
                break;
            case 1283:
                character = "AWI";
                break;
            case 1284:
                character = "AWJ";
                break;
            case 1285:
                character = "AWK";
                break;
            case 1286:
                character = "AWL";
                break;
            case 1287:
                character = "AWM";
                break;
            case 1288:
                character = "AWN";
                break;
            case 1289:
                character = "AWO";
                break;
            case 1290:
                character = "AWP";
                break;
            case 1291:
                character = "AWQ";
                break;
            case 1292:
                character = "AWR";
                break;
            case 1293:
                character = "AWS";
                break;
            case 1294:
                character = "AWT";
                break;
            case 1295:
                character = "AWU";
                break;
            case 1296:
                character = "AWV";
                break;
            case 1297:
                character = "AWW";
                break;
            case 1298:
                character = "AWX";
                break;
            case 1299:
                character = "AWY";
                break;
            case 1300:
                character = "AWZ";
                break;
            case 1301:
                character = "AXA";
                break;
            case 1302:
                character = "AXB";
                break;
            case 1303:
                character = "AXC";
                break;
            case 1304:
                character = "AXD";
                break;
            case 1305:
                character = "AXE";
                break;
            case 1306:
                character = "AXF";
                break;
            case 1307:
                character = "AXG";
                break;
            case 1308:
                character = "AXH";
                break;
            case 1309:
                character = "AXI";
                break;
            case 1310:
                character = "AXJ";
                break;
            case 1311:
                character = "AXK";
                break;
            case 1312:
                character = "AXL";
                break;
            case 1313:
                character = "AXM";
                break;
            case 1314:
                character = "AXN";
                break;
            case 1315:
                character = "AXO";
                break;
            case 1316:
                character = "AXP";
                break;
            case 1317:
                character = "AXQ";
                break;
            case 1318:
                character = "AXR";
                break;
            case 1319:
                character = "AXS";
                break;
            case 1320:
                character = "AXT";
                break;
            case 1321:
                character = "AXU";
                break;
            case 1322:
                character = "AXV";
                break;
            case 1323:
                character = "AXW";
                break;
            case 1324:
                character = "AXX";
                break;
            case 1325:
                character = "AXY";
                break;
            case 1326:
                character = "AXZ";
                break;
            case 1327:
                character = "AYA";
                break;
            case 1328:
                character = "AYB";
                break;
            case 1329:
                character = "AYC";
                break;
            case 1330:
                character = "AYD";
                break;
            case 1331:
                character = "AYE";
                break;
            case 1332:
                character = "AYF";
                break;
            case 1333:
                character = "AYG";
                break;
            case 1334:
                character = "AYH";
                break;
            case 1335:
                character = "AYI";
                break;
            case 1336:
                character = "AYJ";
                break;
            case 1337:
                character = "AYK";
                break;
            case 1338:
                character = "AYL";
                break;
            case 1339:
                character = "AYM";
                break;
            case 1340:
                character = "AYN";
                break;
            case 1341:
                character = "AYO";
                break;
            case 1342:
                character = "AYP";
                break;
            case 1343:
                character = "AYQ";
                break;
            case 1344:
                character = "AYR";
                break;
            case 1345:
                character = "AYS";
                break;
            case 1346:
                character = "AYT";
                break;
            case 1347:
                character = "AYU";
                break;
            case 1348:
                character = "AYV";
                break;
            case 1349:
                character = "AYW";
                break;
            case 1350:
                character = "AYX";
                break;
            case 1351:
                character = "AYY";
                break;
            case 1352:
                character = "AYZ";
                break;
            case 1353:
                character = "AZA";
                break;
            case 1354:
                character = "AZB";
                break;
            case 1355:
                character = "AZC";
                break;
            case 1356:
                character = "AZD";
                break;
            case 1357:
                character = "AZE";
                break;
            case 1358:
                character = "AZF";
                break;
            case 1359:
                character = "AZG";
                break;
            case 1360:
                character = "AZH";
                break;
            case 1361:
                character = "AZI";
                break;
            case 1362:
                character = "AZJ";
                break;
            case 1363:
                character = "AZK";
                break;
            case 1364:
                character = "AZL";
                break;
            case 1365:
                character = "AZM";
                break;
            case 1366:
                character = "AZN";
                break;
            case 1367:
                character = "AZO";
                break;
            case 1368:
                character = "AZP";
                break;
            case 1369:
                character = "AZQ";
                break;
            case 1370:
                character = "AZR";
                break;
            case 1371:
                character = "AZS";
                break;
            case 1372:
                character = "AZT";
                break;
            case 1373:
                character = "AZU";
                break;
            case 1374:
                character = "AZV";
                break;
            case 1375:
                character = "AZW";
                break;
            case 1376:
                character = "AZX";
                break;
            case 1377:
                character = "AZY";
                break;
            case 1378:
                character = "AZZ";
                break;
            case 1379:
                character = "BAA";
                break;
            case 1380:
                character = "BAB";
                break;
            case 1381:
                character = "BAC";
                break;
            case 1382:
                character = "BAD";
                break;
            case 1383:
                character = "BAE";
                break;
            case 1384:
                character = "BAF";
                break;
            case 1385:
                character = "BAG";
                break;
            case 1386:
                character = "BAH";
                break;
            case 1387:
                character = "BAI";
                break;
            case 1388:
                character = "BAJ";
                break;
            case 1389:
                character = "BAK";
                break;
            case 1390:
                character = "BAL";
                break;
            case 1391:
                character = "BAM";
                break;
            case 1392:
                character = "BAN";
                break;
            case 1393:
                character = "BAO";
                break;
            case 1394:
                character = "BAP";
                break;
            case 1395:
                character = "BAQ";
                break;
            case 1396:
                character = "BAR";
                break;
            case 1397:
                character = "BAS";
                break;
            case 1398:
                character = "BAT";
                break;
            case 1399:
                character = "BAU";
                break;
            case 1400:
                character = "BAV";
                break;
            case 1401:
                character = "BAW";
                break;
            case 1402:
                character = "BAX";
                break;
            case 1403:
                character = "BAY";
                break;
            case 1404:
                character = "BAZ";
                break;
            case 1405:
                character = "BBA";
                break;
            case 1406:
                character = "BBB";
                break;
            case 1407:
                character = "BBC";
                break;
            case 1408:
                character = "BBD";
                break;
            case 1409:
                character = "BBE";
                break;
            case 1410:
                character = "BBF";
                break;
            case 1411:
                character = "BBG";
                break;
            case 1412:
                character = "BBH";
                break;
            case 1413:
                character = "BBI";
                break;
            case 1414:
                character = "BBJ";
                break;
            case 1415:
                character = "BBK";
                break;
            case 1416:
                character = "BBL";
                break;
            case 1417:
                character = "BBM";
                break;
            case 1418:
                character = "BBN";
                break;
            case 1419:
                character = "BBO";
                break;
            case 1420:
                character = "BBP";
                break;
            case 1421:
                character = "BBQ";
                break;
            case 1422:
                character = "BBR";
                break;
            case 1423:
                character = "BBS";
                break;
            case 1424:
                character = "BBT";
                break;
            case 1425:
                character = "BBU";
                break;
            case 1426:
                character = "BBV";
                break;
            case 1427:
                character = "BBW";
                break;
            case 1428:
                character = "BBX";
                break;
            case 1429:
                character = "BBY";
                break;
            case 1430:
                character = "BBZ";
                break;
            case 1431:
                character = "BCA";
                break;
            case 1432:
                character = "BCB";
                break;
            case 1433:
                character = "BCC";
                break;
            case 1434:
                character = "BCD";
                break;
            case 1435:
                character = "BCE";
                break;
            case 1436:
                character = "BCF";
                break;
            case 1437:
                character = "BCG";
                break;
            case 1438:
                character = "BCH";
                break;
            case 1439:
                character = "BCI";
                break;
            case 1440:
                character = "BCJ";
                break;
            case 1441:
                character = "BCK";
                break;
            case 1442:
                character = "BCL";
                break;
            case 1443:
                character = "BCM";
                break;
            case 1444:
                character = "BCN";
                break;
            case 1445:
                character = "BCO";
                break;
            case 1446:
                character = "BCP";
                break;
            case 1447:
                character = "BCQ";
                break;
            case 1448:
                character = "BCR";
                break;
            case 1449:
                character = "BCS";
                break;
            case 1450:
                character = "BCT";
                break;
            case 1451:
                character = "BCU";
                break;
            case 1452:
                character = "BCV";
                break;
            case 1453:
                character = "BCW";
                break;
            case 1454:
                character = "BCX";
                break;
            case 1455:
                character = "BCY";
                break;
            case 1456:
                character = "BCZ";
                break;
            case 1457:
                character = "BDA";
                break;
            case 1458:
                character = "BDB";
                break;
            case 1459:
                character = "BDC";
                break;
            case 1460:
                character = "BDD";
                break;
            case 1461:
                character = "BDE";
                break;
            case 1462:
                character = "BDF";
                break;
            case 1463:
                character = "BDG";
                break;
            case 1464:
                character = "BDH";
                break;
            case 1465:
                character = "BDI";
                break;
            case 1466:
                character = "BDJ";
                break;
            case 1467:
                character = "BDK";
                break;
            case 1468:
                character = "BDL";
                break;
            case 1469:
                character = "BDM";
                break;
            case 1470:
                character = "BDN";
                break;
            case 1471:
                character = "BDO";
                break;
            case 1472:
                character = "BDP";
                break;
            case 1473:
                character = "BDQ";
                break;
            case 1474:
                character = "BDR";
                break;
            case 1475:
                character = "BDS";
                break;
            case 1476:
                character = "BDT";
                break;
            case 1477:
                character = "BDU";
                break;
            case 1478:
                character = "BDV";
                break;
            case 1479:
                character = "BDW";
                break;
            case 1480:
                character = "BDX";
                break;
            case 1481:
                character = "BDY";
                break;
            case 1482:
                character = "BDZ";
                break;
            case 1483:
                character = "BEA";
                break;
            case 1484:
                character = "BEB";
                break;
            case 1485:
                character = "BEC";
                break;
            case 1486:
                character = "BED";
                break;
            case 1487:
                character = "BEE";
                break;
            case 1488:
                character = "BEF";
                break;
            case 1489:
                character = "BEG";
                break;
            case 1490:
                character = "BEH";
                break;
            case 1491:
                character = "BEI";
                break;
            case 1492:
                character = "BEJ";
                break;
            case 1493:
                character = "BEK";
                break;
            case 1494:
                character = "BEL";
                break;
            case 1495:
                character = "BEM";
                break;
            case 1496:
                character = "BEN";
                break;
            case 1497:
                character = "BEO";
                break;
            case 1498:
                character = "BEP";
                break;
            case 1499:
                character = "BEQ";
                break;
            case 1500:
                character = "BER";
                break;
            case 1501:
                character = "BES";
                break;
            case 1502:
                character = "BET";
                break;
            case 1503:
                character = "BEU";
                break;
            case 1504:
                character = "BEV";
                break;
            case 1505:
                character = "BEW";
                break;
            case 1506:
                character = "BEX";
                break;
            case 1507:
                character = "BEY";
                break;
            case 1508:
                character = "BEZ";
                break;
            case 1509:
                character = "BFA";
                break;
            case 1510:
                character = "BFB";
                break;
            case 1511:
                character = "BFC";
                break;
            case 1512:
                character = "BFD";
                break;
            case 1513:
                character = "BFE";
                break;
            case 1514:
                character = "BFF";
                break;
            case 1515:
                character = "BFG";
                break;
            case 1516:
                character = "BFH";
                break;
            case 1517:
                character = "BFI";
                break;
            case 1518:
                character = "BFJ";
                break;
            case 1519:
                character = "BFK";
                break;
            case 1520:
                character = "BFL";
                break;
            case 1521:
                character = "BFM";
                break;
            case 1522:
                character = "BFN";
                break;
            case 1523:
                character = "BFO";
                break;
            case 1524:
                character = "BFP";
                break;
            case 1525:
                character = "BFQ";
                break;
            case 1526:
                character = "BFR";
                break;
            case 1527:
                character = "BFS";
                break;
            case 1528:
                character = "BFT";
                break;
            case 1529:
                character = "BFU";
                break;
            case 1530:
                character = "BFV";
                break;
            case 1531:
                character = "BFW";
                break;
            case 1532:
                character = "BFX";
                break;
            case 1533:
                character = "BFY";
                break;
            case 1534:
                character = "BFZ";
                break;
            case 1535:
                character = "BGA";
                break;
            case 1536:
                character = "BGB";
                break;
            case 1537:
                character = "BGC";
                break;
            case 1538:
                character = "BGD";
                break;
            case 1539:
                character = "BGE";
                break;
            case 1540:
                character = "BGF";
                break;
            case 1541:
                character = "BGG";
                break;
            case 1542:
                character = "BGH";
                break;
            case 1543:
                character = "BGI";
                break;
            case 1544:
                character = "BGJ";
                break;
            case 1545:
                character = "BGK";
                break;
            case 1546:
                character = "BGL";
                break;
            case 1547:
                character = "BGM";
                break;
            case 1548:
                character = "BGN";
                break;
            case 1549:
                character = "BGO";
                break;
            case 1550:
                character = "BGP";
                break;
            case 1551:
                character = "BGQ";
                break;
            case 1552:
                character = "BGR";
                break;
            case 1553:
                character = "BGS";
                break;
            case 1554:
                character = "BGT";
                break;
            case 1555:
                character = "BGU";
                break;
            case 1556:
                character = "BGV";
                break;
            case 1557:
                character = "BGW";
                break;
            case 1558:
                character = "BGX";
                break;
            case 1559:
                character = "BGY";
                break;
            case 1560:
                character = "BGZ";
                break;
            case 1561:
                character = "BHA";
                break;
            case 1562:
                character = "BHB";
                break;
            case 1563:
                character = "BHC";
                break;
            case 1564:
                character = "BHD";
                break;
            case 1565:
                character = "BHE";
                break;
            case 1566:
                character = "BHF";
                break;
            case 1567:
                character = "BHG";
                break;
            case 1568:
                character = "BHH";
                break;
            case 1569:
                character = "BHI";
                break;
            case 1570:
                character = "BHJ";
                break;
            case 1571:
                character = "BHK";
                break;
            case 1572:
                character = "BHL";
                break;
            case 1573:
                character = "BHM";
                break;
            case 1574:
                character = "BHN";
                break;
            case 1575:
                character = "BHO";
                break;
            case 1576:
                character = "BHP";
                break;
            case 1577:
                character = "BHQ";
                break;
            case 1578:
                character = "BHR";
                break;
            case 1579:
                character = "BHS";
                break;
            case 1580:
                character = "BHT";
                break;
            case 1581:
                character = "BHU";
                break;
            case 1582:
                character = "BHV";
                break;
            case 1583:
                character = "BHW";
                break;
            case 1584:
                character = "BHX";
                break;
            case 1585:
                character = "BHY";
                break;
            case 1586:
                character = "BHZ";
                break;
            default:
                break;
        }
        return character;
    }
}