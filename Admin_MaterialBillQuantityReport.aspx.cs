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
        
        //CreateExcelDoc excell_app = new CreateExcelDoc();
        ////creates the main header
        //excell_app.createHeaders(5, 2, "Total of Products", "B5", "D5", 2, "YELLOW", true, 10, "n");
        ////creates subheaders
        //excell_app.createHeaders(6, 2, "Sold Product", "B6", "B6", 0, "GRAY", true, 10, "");
        //excell_app.createHeaders(6, 3, "", "C6", "C6", 0, "GRAY", true, 10, "");
        //excell_app.createHeaders(6, 4, "Initial Total", "D6", "D6", 0, "GRAY", true, 10, "");
        ////add Data to cells
        //excell_app.addData(7, 2, "114287", "B7", "B7", "#,##0");
        //excell_app.addData(7, 3, "", "C7", "C7", "");
        //excell_app.addData(7, 4, "129121", "D7", "D7", "#,##0");
        ////add percentage row
        //excell_app.addData(8, 2, "", "B8", "B8", "");
        //excell_app.addData(8, 3, "=B7/D7", "C8", "C8", "0.0%");
        //excell_app.addData(8, 4, "", "D8", "D8", "");
        ////add empty divider
        //excell_app.createHeaders(9, 2, "", "B9", "D9", 2, "GAINSBORO", true, 10, "");


        DataRow dr = null;

        DataTable dsDes = new DataTable();

        int WAID = Convert.ToInt16(ddlNameOfWork.SelectedValue);
        int AcaID = Convert.ToInt16(ddlAcademy.SelectedValue);


        dsDes = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct ER.Qty, M.MatName,Max(ER.Rate) As Rate,ER.MatId  from EstimateAndMaterialOthersRelations ER INNER JOIN Estimate E on E.EstId=ER.EstId INNER JOIN Material M on M.MatId = ER.MatId WHERE E.WAId='" + ddlNameOfWork.SelectedValue + "'and E.AcaId ='" + ddlAcademy.SelectedValue + "' and ER.PSID ='" + (int)TypeEnum.PurchaseSourceID.Local + "' and E.IsApproved=1 group by ER.Rate,ER.Qty,M.MatName,ER.MatId").Tables[0];

        DataTable dataTable = BillDetailDataTable(WAID);

        DataTable dtEstimateQtyDetail = DAL.DalAccessUtility.GetDataInDataSet("exec [GetEstimateQtyDetailByWorkAllot] '" + ddlNameOfWork.SelectedValue + "','" + ddlAcademy.SelectedValue + "','" + (int)TypeEnum.PurchaseSourceID.Local + "'").Tables[0];

        DataTable dtSubmitBillQtyDetail = DAL.DalAccessUtility.GetDataInDataSet("exec [GetSubmitBillQtyDetailByWorkAllot] '" + ddlNameOfWork.SelectedValue + "','" + ddlAcademy.SelectedValue + "','" + (int)TypeEnum.PurchaseSourceID.Local + "'").Tables[0];

        if (dsDes != null)
        {
            for (int i = 0; i < dsDes.Rows.Count; i++)
            {
                decimal BillQty = 0;
                decimal EstimateQty = 0;
                dr = dataTable.NewRow();
                dr[0] = i + 1;
                dr["Name Of Material"] = dsDes.Rows[i]["MatName"].ToString();
                dr["Estimate Rate"] = dsDes.Rows[i]["Rate"].ToString();

                for (int j = 0; j < dtEstimateQtyDetail.Rows.Count; j++)
                {
                    if (dsDes.Rows[i]["MatId"].ToString() == dtEstimateQtyDetail.Rows[j]["MatId"].ToString())
                    {
                        dr["Est No. " + dtEstimateQtyDetail.Rows[j]["EstId"].ToString()] = dtEstimateQtyDetail.Rows[j]["Qty"].ToString();
                        EstimateQty += Convert.ToDecimal(dtEstimateQtyDetail.Rows[j]["Qty"].ToString());
                    }
                }

                for (int k = 0; k < dtSubmitBillQtyDetail.Rows.Count; k++)
                {
                    if (dsDes.Rows[i]["MatId"].ToString() == dtSubmitBillQtyDetail.Rows[k]["MatId"].ToString())
                    {
                        dr["Bill No. " + dtSubmitBillQtyDetail.Rows[k]["SubBillId"].ToString()] = dtSubmitBillQtyDetail.Rows[k]["Qty"].ToString();
                        BillQty += Convert.ToDecimal(dtSubmitBillQtyDetail.Rows[k]["Qty"].ToString());
                    }
                }

                dr["Balance Quantity"] = Convert.ToDecimal(EstimateQty) - Convert.ToDecimal(BillQty);

                dataTable.Rows.Add(dr);
            }
        }
        return dataTable;
    }
}