using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Purchase_EstimateView : System.Web.UI.Page
{
    DataTable dt = new DataTable();

    DataRow dr;

    int InchargeID = -1;

    int UserType = -1;

    public int ModuleID = -1;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["ModuleID"] != null)
        {
            ModuleID = int.Parse(Session["ModuleID"].ToString());
        }
        if (Session["EmailId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblUser.Text = Session["EmailId"].ToString();
            InchargeID = int.Parse(Session["InchargeID"].ToString());
            UserType = int.Parse(Session["UserTypeID"].ToString());
        }
        if (!Page.IsPostBack)
        {
            getEstimateDetails(0);
        }
        
        if (Request.QueryString["AcaId"] != null)
        {
            GetEstimateDetailsByClick(Request.QueryString["AcaId"].ToString());
            divExcel.Visible = false;
        }
        if (Request.QueryString["EstId"] != null)
        {
            GetPrint(Request.QueryString["EstId"].ToString());
        }
       
    }

    protected void GetPrint(string id)
    {
        DataSet dsValue = new DataSet();
        if (UserType == (int)TypeEnum.UserType.PURCHASE || UserType == (int)TypeEnum.UserType.PURCHASECOMMITTEE)
        {
            dsValue = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateDetailByPurchaseAdmin " + id + "," + InchargeID);
        }
        else
        {
            dsValue = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_EstimateDetailByEmp] " + id + "," + InchargeID);
        }
        string EstInfo = string.Empty;
        decimal GrandTotal = 0;
        EstInfo += "<div style='width:100%; margin:20px; font-family:Calibri;'>";
        EstInfo += "<table style='width:100%;' border='0' >";
        EstInfo += "<tr>";
        EstInfo += "<td style='padding:0px; text-align:left; width:30%' valign='top'>";
        EstInfo += "<img src='http://akalsewa.org/BaruSahib/img/Logo_Small.png'/>";
        EstInfo += "</td>";
        EstInfo += "<td style='text-align: right;'>";
        EstInfo += "<br /><br />";
        EstInfo += "<div style='font-style:italic; text-align: right; float: right; margin-right: 10px;'>";
        EstInfo += "Baru Shahib,";
        EstInfo += "<br />Dist: Sirmaur";
        EstInfo += "<br />Himachal Pradesh-173001";
        EstInfo += "<br />XXXXXXXXXXX";
        EstInfo += "</div>";
        EstInfo += "</td>";
        EstInfo += "</tr>";
        EstInfo += "</table>";
        EstInfo += "<br /><br />";
        EstInfo += "<div style='font-size:30px; margin-top:20px; font-weight:bold; width:100%;'>" + dsValue.Tables[0].Rows[0]["WorkAllotName"].ToString() + "</div>";
        EstInfo += "<table style='width:100%; margin-top:20px;' border='0'>";
        EstInfo += "<tr>";
        EstInfo += "<td style='padding:0px; text-align:left;font-size:15px' valign='top'>";
        EstInfo += "Estimate No: <b> " + dsValue.Tables[0].Rows[0]["EstId"].ToString() + "</b><br />";
        EstInfo += "Zone: <b>" + dsValue.Tables[0].Rows[0]["ZoneName"].ToString() + "</b><br />";
        EstInfo += "Estimate Title:<b> " + dsValue.Tables[0].Rows[0]["SubEstimate"].ToString() + "</b><br />";
        EstInfo += "Sanction Date:<b> " + dsValue.Tables[0].Rows[0]["SanctionDate"].ToString() + "</b>";
        EstInfo += "</td>";
        EstInfo += "<td style='text-align: right; float: right; margin-right: 20px;'>";
        EstInfo += "Academy:<b> " + dsValue.Tables[0].Rows[0]["AcaName"].ToString() + "</b><br />";
        EstInfo += "Type of Work:<b> " + dsValue.Tables[0].Rows[0]["TypeWorkName"].ToString() + "</b><br />";
        EstInfo += "Estimate Cost: <b> " + dsValue.Tables[0].Rows[0]["EstmateCost"].ToString() + "</b>";
        EstInfo += "</td>";
        EstInfo += "</tr>";
        EstInfo += "</table>";
        EstInfo += "<br /><br />";
        EstInfo += "<div style='width:100%; font-size:20px; font-weight:bold; text-align:center;'>Estimate Purchase Details Assigned to " + lblUser.Text + "</div>";
        EstInfo += "<br />";
        EstInfo += "<table style='width:100%; margin-top:20px;font-size:13px;border-color:black' border='1'>";
        EstInfo += "<thead>";
        EstInfo += "<tr>";
        EstInfo += "<th>Sr.No</th>";
        EstInfo += "<th>Material Name</th>";
        EstInfo += "<th>Source Type</th>";
        EstInfo += "<th>EstQty</th>";
        EstInfo += "<th>PurchaseQty</th>";
        EstInfo += "<th>Rate</th>";
        EstInfo += "<th style='width:152px;'>Amount</th>";
        EstInfo += "</tr>";
        EstInfo += "</thead>";
        EstInfo += "<tbody>";
        for (int i = 0; i < dsValue.Tables[1].Rows.Count; i++)
        {

            EstInfo += "<tr>";
            EstInfo += "<td>" + (i + 1) + "</td>";
            EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["MatName"].ToString() + "(" + dsValue.Tables[1].Rows[i]["UnitName"].ToString() + ")</td>";
            EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["PSName"].ToString() + "</td>";
            EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["EstQty"].ToString() + "</td>";
            EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["PurchaseQty"].ToString() + "</td>";
            EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["Rate"].ToString() + "</td>";
            var totalAmount = Convert.ToDecimal(dsValue.Tables[1].Rows[i]["PurchaseQty"].ToString()) * Convert.ToDecimal(dsValue.Tables[1].Rows[i]["Rate"].ToString());
            EstInfo += "<td style='width:152px;'>" + totalAmount + "</td>";
            EstInfo += "</tr>";
            GrandTotal += totalAmount;

        }
        EstInfo += "<tr>";
        EstInfo += "<td></td><td></td><td></td><td></td><td><b>Total</b></td>";
        EstInfo += "<td style='width:152px; font-weight:bold;'>" + GrandTotal + "</td>";
        EstInfo += "</tr>";
        EstInfo += "</tbody>";
        EstInfo += "<tr>";
        EstInfo += "</table>";
        EstInfo += "<br />";
        EstInfo += "<div style='margin-top:50px; width:100%; text-align:center;'>&copy; The Kalgidhar Socity All Rights Reserved</div>";
        EstInfo += "</div>";

        dt.Columns.Add("HtmlContent");
        dr = dt.NewRow();
        dr["HtmlContent"] = EstInfo;
        dt.Rows.Add(dr);
        pnlPdf.InnerHtml = dt.Rows[0][0].ToString();

        //Response.ContentType = "application/pdf";
        //Response.AddHeader("content-disposition", "attachment;filename=Estimate_" + dsValue.Tables[0].Rows[0]["EstId"].ToString() + ".pdf");
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        //pnlPdf.RenderControl(hw);
        //StringReader sr = new StringReader(sw.ToString());
        //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 50f, 10f);
        //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //pdfDoc.Open();
        //htmlparser.Parse(sr);
        //pdfDoc.Close();
        //Response.Write(pdfDoc);
        //Response.End();

        Utility.GeneratePDF(pnlPdf.InnerHtml, dsValue.Tables[0].Rows[0]["EstId"].ToString() + ".pdf", string.Empty);
    }

    private void GetEstimateDetailsByClick(string id)
    {
        DataSet dsEstimateDetails = new DataSet();
        //dsEstimateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateDetailsByEmpAndZone  '" + ID + "','"+ lblUser.Text +"'");
        dsEstimateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateViewByAcaId '" + id + "'");
        divEstimateDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Estimate List</h2>";
        ZoneInfo += "<div class='box-icon'>";
        //ZoneInfo += "<a href='#' class='btn btn-setting btn-round'><i class='icon-cog'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        ZoneInfo += "<div class='box-content'>";
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th style='display:none;'></th>";
        ZoneInfo += "<th width='5%'>Est No.</th>";
        ZoneInfo += "<th width='20%'>Estmate For</th>";
        //ZoneInfo += "<th width='15%'>Sanction Date</th>";
        ZoneInfo += "<th width='30%'>Details of Sactioned Estimates</th>";
        ZoneInfo += "<th width='15%'>Estimate Cost</th>";
        ZoneInfo += "<th width='40%'><table width='100%'><tr><th colspan='3' align='center'>Commulative Total Expenditure as On</th></tr><tr><th width='33%'>Bill Id</th><th width='34%'>Bill Date</th><th width='33%'>Bill Amount</th></tr></table></th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsEstimateDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            ZoneInfo += "<td width='5%'>" + dsEstimateDetails.Tables[0].Rows[i]["EstId"].ToString() + "</td>";
            ZoneInfo += "<td width='20%'><table><tr><td><b>Zone</b>: " + dsEstimateDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td></tr><tr><td><b>Academy</b>: " + dsEstimateDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td></tr></table></td>";
            ZoneInfo += "<td class='center'width='30%'><table>";
            ZoneInfo += "<tr><td><b>Sub Head:</b> <a href='Purchase_ParticularEstView.aspx?EstId=" + dsEstimateDetails.Tables[0].Rows[i]["EstId"].ToString() + "'>" + dsEstimateDetails.Tables[0].Rows[i]["SubEstimate"].ToString() + "</a></td></tr>";
            ZoneInfo += "<tr><td><b>Work Name:</b> " + dsEstimateDetails.Tables[0].Rows[i]["WorkAllotName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Sanction Date:</b> " + dsEstimateDetails.Tables[0].Rows[i]["dt"].ToString() + "</td></tr>";
            ZoneInfo += "</table></td>";
            ZoneInfo += "<td width='20%'><table><tr><td> " + dsEstimateDetails.Tables[0].Rows[i]["EstmateCost"].ToString() + "</td></tr><tr><td><b>Bal</b>:0</td> </tr> <tr><td><a href='Purchase_EstimateView.aspx?EstId=" + dsEstimateDetails.Tables[0].Rows[i]["EstId"].ToString() + "'><span class='label label-info'  style='font-size: 15.998px;'>Print Estimate</span></a></td> </tr></table></td>";
            ZoneInfo += "<td width='40%'><table width='100%'>";
            DataSet dsBillDetails = new DataSet();
            DataSet dsTa = new DataSet();
            if (dsBillDetails.Tables.Count > 0)
            {
                for (int j = 0; j < dsBillDetails.Tables[0].Rows.Count; j++)
                {
                    ZoneInfo += "<tr><td width='33%'><a href='Purchase_BillDetails.aspx?BillId=" + dsBillDetails.Tables[0].Rows[j]["SubBillId"].ToString() + "'>" + dsBillDetails.Tables[0].Rows[j]["SubBillId"].ToString() + "</a></td><td width='34%'>" + dsBillDetails.Tables[0].Rows[j]["bdATE"].ToString() + "</td><td width='33%'>" + dsBillDetails.Tables[0].Rows[j]["TotalAmount"].ToString() + "</td></tr>";
                }
            }
            if (dsTa.Tables.Count > 0)
            {
                if (dsTa.Tables[0].Rows[0]["Ta"].ToString() != "" && dsTa.Tables[1].Rows[0]["BDate"].ToString() != "")
                {
                    ZoneInfo += "<td></td><td><b>Total Amount</b></td><td>" + dsTa.Tables[0].Rows[0]["Ta"].ToString() + "</td>";
                }
            }
            else
            {
                ZoneInfo += "<td colspan='2'><span class='label label-important' style='font-size: 15.998px;'>No Bill Submit</span></td>";
            }
            ZoneInfo += "</table></td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divEstimateDetails.InnerHtml = ZoneInfo.ToString();
    }

    private void getEstimateDetails(int EstID)
    {
        DataSet dsEstimateDetails = new DataSet();
        //dsEstimateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateDetailsByEmpAndZone  '" + ID + "','"+ lblUser.Text +"'");
        if (UserType == (int)TypeEnum.UserType.PURCHASE || UserType == (int)TypeEnum.UserType.PURCHASECOMMITTEE)
        {
            dsEstimateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AllEstimateView");
        }
        else
        {
            dsEstimateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_AllEstimateViewByEmployeeID]" + InchargeID);
        }

        System.Data.EnumerableRowCollection<System.Data.DataRow> dtApproved = null;

        if (EstID > 0)
        {

            dtApproved = (from mytable in dsEstimateDetails.Tables[0].AsEnumerable()
                          where mytable.Field<int>("EstID") == EstID
                          select mytable);
        }
        else
        {
            if (UserType == (int)TypeEnum.UserType.PURCHASE || UserType == (int)TypeEnum.UserType.PURCHASECOMMITTEE)
            {

                dtApproved = (from mytable in dsEstimateDetails.Tables[0].AsEnumerable()
                              where mytable.Field<DateTime>("CreatedOn") >= DateTime.Now.AddDays(-15)
                              select mytable);
            }
            else
            {
                dtApproved = (from mytable in dsEstimateDetails.Tables[0].AsEnumerable()
                              where mytable.Field<DateTime>("CreatedOn") >= DateTime.Now.AddDays(-30)
                              select mytable);
            }
        }

        DataTable dtapproved = new DataTable();
        if (dtApproved.Count() > 0)
        {
            dtapproved = dtApproved.CopyToDataTable();
        }

        divEstimateDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Estimate List</h2>";
        ZoneInfo += "<div class='box-icon'>";
        //ZoneInfo += "<a href='#' class='btn btn-setting btn-round'><i class='icon-cog'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        ZoneInfo += "<div class='box-content'>";
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th style='display:none;'></th>";
        ZoneInfo += "<th width='5%'>Est No.</th>";
        ZoneInfo += "<th width='20%'>Estmate For</th>";
        //ZoneInfo += "<th width='15%'>Sanction Date</th>";
        ZoneInfo += "<th width='30%'>Details of Sactioned Estimates</th>";
        ZoneInfo += "<th width='15%'>Estimate Cost</th>";
        ZoneInfo += "<th width='40%'><table width='100%'><tr><th colspan='3' align='center'>Commulative Total Expenditure as On</th></tr><tr><th width='33%'>Bill Id</th><th width='34%'>Bill Date</th><th width='33%'>Bill Amount</th></tr></table></th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dtapproved.Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            ZoneInfo += "<td width='5%'>" + dtapproved.Rows[i]["EstId"].ToString() + "</td>";
            ZoneInfo += "<td width='20%'><table><tr><td><b>Zone</b>: " + dtapproved.Rows[i]["ZoneName"].ToString() + "</td></tr><tr><td><b>Academy</b>: " + dtapproved.Rows[i]["AcaName"].ToString() + "</td></tr><tr><td><b>Estimate File</b>: " + GetFileName(dtapproved.Rows[i]["FilePath"].ToString(), dtapproved.Rows[i]["FileNme"].ToString()) + "</td></tr></table></td>";
            ZoneInfo += "<td class='center'width='30%'><table>";
            ZoneInfo += "<tr><td><b>Sub Head:</b> <a href='Purchase_ParticularEstView.aspx?EstId=" + dtapproved.Rows[i]["EstId"].ToString() + "'>" + dtapproved.Rows[i]["SubEstimate"].ToString() + "</a></td></tr>";
            ZoneInfo += "<tr><td><b>Work Name:</b> " + dtapproved.Rows[i]["WorkAllotName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Sanction Date:</b> " + dtapproved.Rows[i]["dt"].ToString() + "</td></tr>";
            ZoneInfo += "</table></td>";
            ZoneInfo += "<td width='20%'><table><tr><td> " + dtapproved.Rows[i]["EstmateCost"].ToString() + "</td></tr><tr><td><b>Bal</b>:0</td> </tr> <tr><td><a href='Purchase_EstimateView.aspx?EstId=" + dtapproved.Rows[i]["EstId"].ToString() + "'><span class='label label-info'  style='font-size: 15.998px;'>Print Estimate</span></a></td> </tr></table></td>";
            ZoneInfo += "<td width='40%'><table width='100%'>";
            DataSet dsBillDetails = new DataSet();
            DataSet dsTa = new DataSet();
            if (dsBillDetails.Tables.Count > 0)
            {
                for (int j = 0; j < dsBillDetails.Tables[0].Rows.Count; j++)
                {
                    ZoneInfo += "<tr><td width='33%'><a href='Purchase_BillDetails.aspx?BillId=" + dsBillDetails.Tables[0].Rows[j]["SubBillId"].ToString() + "'>" + dsBillDetails.Tables[0].Rows[j]["SubBillId"].ToString() + "</a></td><td width='34%'>" + dsBillDetails.Tables[0].Rows[j]["bdATE"].ToString() + "</td><td width='33%'>" + dsBillDetails.Tables[0].Rows[j]["TotalAmount"].ToString() + "</td></tr>";
                }
            }
            if (dsTa.Tables.Count > 0)
            {
                if (dsTa.Tables[0].Rows[0]["Ta"].ToString() != "" && dsTa.Tables[1].Rows[0]["BDate"].ToString() != "")
                {
                    ZoneInfo += "<td></td><td><b>Total Amount</b></td><td>" + dsTa.Tables[0].Rows[0]["Ta"].ToString() + "</td>";

                }
            }
            else
            {
                ZoneInfo += "<td colspan='2'><span class='label label-important' style='font-size: 15.998px;'>No Bill Submit</span></td>";
            }
            ZoneInfo += "</table></td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divEstimateDetails.InnerHtml = ZoneInfo.ToString();
    }

    private string GetFileName(string filepaths, string fileName)
    {
        string anchorLink = string.Empty;
        string[] filePath = filepaths.Split(',');
        int count = 0;
        foreach (string path in filePath)
        {
            count++;
            anchorLink += "<a href='" + path + "' target='_blank'>" + fileName + "_" + count + "</a> , ";
        }

        return anchorLink.Substring(0, anchorLink.Length - 3);

    }

    protected DataTable BindDatatable()
    {
        DataTable dt = new DataTable();
        dt = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ExcelEstimateByPurchaser'" + InchargeID + "','" + UserType + "'").Tables[0];
        return dt;
    }

    protected DataTable BindDatatable2()
    {
        DataTable dt = new DataTable();
        dt = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateWithMaterialByPurchaser'" + InchargeID + "','" + UserType + "'").Tables[0];
        return dt;
    }

    protected void btnExecl_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "EstimateStatement.xls"));
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

    protected void btnExcel1_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "EstimateStatementMaterial.xls"));
        Response.ContentType = "application/ms-excel";
        DataTable dt = BindDatatable2();
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

    protected void btnEstSearch_Click(object sender, EventArgs e)
    {
        if (txtEstid.Text != string.Empty)
        {
            getEstimateDetails(Convert.ToInt16(txtEstid.Text));
        }
    }
}