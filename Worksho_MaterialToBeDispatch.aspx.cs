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

public partial class Worksho_MaterialToBeDispatch : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    DataRow dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmailId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblUser.Text = Session["EmailId"].ToString();
        }

        getEstimateDetails();
        if (Request.QueryString["EstId"] != null)
        {
            GetPrint(Request.QueryString["EstId"].ToString());

        }
    }
    protected void GetPrint(string id)
    {
        DataSet dsValue = DAL.DalAccessUtility.GetDataInDataSet("exec USP_MaterialDepatchStatusBill '" + id + "'");
        string EstInfo = string.Empty;
        EstInfo += "<div style='width:100%; margin:20px; font-family:Calibri;'>";
        EstInfo += "<table style='width:100%;'>";
        EstInfo += "<tr>";
        EstInfo += "<td style='padding:0px; text-align:left; width:50%' valign='top'>";
        EstInfo += "<img src='http://barusahib.org/wp-content/uploads/2013/06/Logo.png' style='width:100%;' />";
        EstInfo += "</td>";
        EstInfo += "<td style='text-align: right; width:40%;'>";
        EstInfo += "<br /><br />";
        EstInfo += "<div style='font-style:italic; text-align: right;'>";
        EstInfo += "Baru Shahib,";
        EstInfo += "<br />Dist: Sirmaur";
        EstInfo += "<br />Himachal Pradesh-173001";
        EstInfo += "<br />XXXXXXXXXXX";
        EstInfo += "</div>";
        EstInfo += "</td>";
        EstInfo += "</tr>";
        EstInfo += "</table>";
        EstInfo += "<br /><br />";
        EstInfo += "<div style='font-size:15px; margin-top:20px; font-weight:bold; width:100%;'>" + dsValue.Tables[0].Rows[0]["SubEstimate"].ToString() + "</div>";
        EstInfo += "<table style='width:100%; margin-top:20px;'>";
        EstInfo += "<tr>";
        EstInfo += "<td style='padding:0px; text-align:left;' valign='top'>";
        EstInfo += "<p>";
        EstInfo += "Estimate No: " + dsValue.Tables[0].Rows[0]["EstId"].ToString() + "<br />";
        EstInfo += "Academy: " + dsValue.Tables[0].Rows[0]["AcaName"].ToString() + "<br />";
        EstInfo += "Sanction Date: " + dsValue.Tables[0].Rows[0]["SanctionDate"].ToString();
        EstInfo += "</p>";
        EstInfo += "</td>";

        EstInfo += "</tr>";
        EstInfo += "</table>";
        EstInfo += "<br /><br />";
        EstInfo += "<div style='width:100%; font-size:15px; font-weight:bold; text-align:center;'>Material Dispatch Details</div>";
        EstInfo += "<br />";
        EstInfo += "<table style='width:100%; margin-top:20px;' border='1'>";
        EstInfo += "<thead>";
        EstInfo += "<tr>";
        EstInfo += "<th>Material</th>";
        EstInfo += "<th>Source Type</th>";
        EstInfo += "<th>Qty</th>";
        EstInfo += "<th>Unit</th>";
        EstInfo += "<th>Rate</th>";
        // EstInfo += "<th style='width:152px;'>Amount</th>";
        EstInfo += "<th>Tantitive Date</th>";
        EstInfo += "<th>Dispatch Date</th>";
        EstInfo += "<th>Remark</th>";
        EstInfo += "</tr>";
        EstInfo += "</thead>";
        EstInfo += "<tbody>";
        DataSet dsMatDetails = new DataSet();
        dsMatDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateMaterialViewForPurchase_V1 '" + dsValue.Tables[0].Rows[0]["EstId"].ToString() + "','3' "); 
        for (int i = 0; i < dsMatDetails.Tables[0].Rows.Count; i++)
        {
            //if (i != dsMatDetails.Tables[0].Rows.Count - 1)
            //{
            EstInfo += "<tr>";
            EstInfo += "<td>" + dsMatDetails.Tables[0].Rows[i]["MatName"].ToString() + "</td>";
            EstInfo += "<td>" + dsMatDetails.Tables[0].Rows[i]["PSName"].ToString() + "</td>";
            EstInfo += "<td>" + dsMatDetails.Tables[0].Rows[i]["Qty"].ToString() + "</td>";
            EstInfo += "<td>" + dsMatDetails.Tables[0].Rows[i]["UnitName"].ToString() + "</td>";
            EstInfo += "<td>" + dsMatDetails.Tables[0].Rows[i]["Rate"].ToString() + "</td>";
            //EstInfo += "<td style='width:152px;'>" + dsMatDetails.Tables[1].Rows[i]["Amount"].ToString() + "</td>";
            if (dsMatDetails.Tables[0].Rows[i]["TantiveDate"].ToString() == null)
            {
                EstInfo += "<td width='15%'  style='color:darkred;'>Not Mantioned</td>";
            }
            else
            {
                EstInfo += "<td width='15%'>" + dsMatDetails.Tables[0].Rows[i]["TantiveDate"].ToString() + "</td>";
            }
            if (dsMatDetails.Tables[0].Rows[i]["DispatchDate"].ToString() == null)
            {
                EstInfo += "<td width='15%'  style='color:darkred;'>Not Mantioned</td>";
            }
            else
            {
                EstInfo += "<td width='15%'>" + dsMatDetails.Tables[0].Rows[i]["DispatchDate"].ToString() + "</td>";
            }
            if (dsMatDetails.Tables[0].Rows[i]["remarkByPurchase"].ToString() == null)
            {
                EstInfo += "<td width='30%' style='color:darkred;'>Not Mantioned</td>";
            }
            else
            {
                EstInfo += "<td width='30%'>" + dsMatDetails.Tables[0].Rows[i]["remarkByPurchase"].ToString() + "</td>";
            }
            EstInfo += "</tr>";
            //}
            //else
            //{
            //    //EstInfo += "<tr>";
            //    //EstInfo += "<td></td><td></td><td></td><td></td><td><b>Total</b></td>";
            //    //EstInfo += "<td style='width:152px; font-weight:bold;'>" + dsValue.Tables[1].Rows[i]["Amount"].ToString() + "</td>";
            //    //EstInfo += "</tr>";
            //}
        }
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

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=MaterialDispatch_" + dsValue.Tables[0].Rows[0]["EstId"].ToString() + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        pnlPdf.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 0f, 10f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }
    private void getEstimateDetails()
    {
        DataSet dsAcaDetails = new DataSet();
        dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateViewForWorkshop ");
        divEstimateDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='row-fluid sortable'>";
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Material Dispatch Details</h2>";
        ZoneInfo += "<div class='box-icon'>";
        ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        ZoneInfo += "<div class='box-content'>";
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th style='display:none;'></th>";
        ZoneInfo += "<th style='display:none;'></th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsAcaDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            ZoneInfo += "<td>";
            ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='25%'><b style='color:red;'>Estimate No:</b> " + dsAcaDetails.Tables[0].Rows[i]["EstId"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Sanction Date:</b> " + dsAcaDetails.Tables[0].Rows[i]["SanctionDate"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='35%'><b style='color:red;'>Sub Estimate:</b> " + dsAcaDetails.Tables[0].Rows[i]["SubEstimate"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Academy:</b> " + dsAcaDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td>";
            //ZoneInfo += "<td class='center' width='25%' align='center'>";
            //if (dsAcaDetails.Tables[0].Rows[i]["DispatchStatus"].ToString() == "1")
            //{
            //    ZoneInfo += "<span class='label label-success'  style='font-size: 15.998px;'>Dispatched</span>";
            //}
            //else
            //{
            //    ZoneInfo += "<a class='btn btn-info' href='Purchase_ViewEstMaterial.aspx?EstId=" + dsAcaDetails.Tables[0].Rows[i]["EstId"].ToString() + "'>";
            //    ZoneInfo += "<i class='icon-edit icon-white'></i>Edit</a>";
            //}
            //ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='20%'><a href='Worksho_MaterialToBeDispatch.aspx?EstId=" + dsAcaDetails.Tables[0].Rows[i]["EstId"].ToString() + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a></td>";
            ZoneInfo += "</tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
            ZoneInfo += "<tr style='color:Green;'>";
            ZoneInfo += "<th width='10%'>Material Name</th>";
            ZoneInfo += "<th width='5%'>Unit</th>";
            ZoneInfo += "<th width='5%'>Quantity</th>";
            ZoneInfo += "<th width='20%'>Source Type</th>";
            ZoneInfo += "<th width='15%'>Tantitive Date</th>";
            ZoneInfo += "<th width='15%'>Dispatch Date</th>";
            ZoneInfo += "<th width='30%'>Remark</th>";
            ZoneInfo += "<th width='30%'>Status&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>";
            ZoneInfo += "</tr>";
            DataSet dsMatDetails = new DataSet();
            //dsMatDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateMaterialViewForPurchaseForUser '" + dsAcaDetails.Tables[0].Rows[i]["EstId"].ToString() + "' ");
            dsMatDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateMaterialViewForPurchase_V1 '" + dsAcaDetails.Tables[0].Rows[i]["EstId"].ToString() + "','3' ");
            for (int j = 0; j < dsMatDetails.Tables[0].Rows.Count; j++)
            {
                ZoneInfo += "<tr>";
                ZoneInfo += "<td width='10%'>" + dsMatDetails.Tables[0].Rows[j]["MatName"].ToString() + "</td>";
                ZoneInfo += "<td width='5%'>" + dsMatDetails.Tables[0].Rows[j]["UnitName"].ToString() + "</td>";
                ZoneInfo += "<td width='5%'>" + dsMatDetails.Tables[0].Rows[j]["Qty"].ToString() + "</td>";
                ZoneInfo += "<td width='20%'>" + dsMatDetails.Tables[0].Rows[j]["PSName"].ToString() + "</td>";
                if (dsMatDetails.Tables[0].Rows[j]["TantiveDate"].ToString() == null)
                {
                    ZoneInfo += "<td width='15%'  style='color:darkred;'>Not Mantioned</td>";
                }
                else
                {
                    ZoneInfo += "<td width='15%'>" + dsMatDetails.Tables[0].Rows[j]["TantiveDate"].ToString() + "</td>";
                }
                if (dsMatDetails.Tables[0].Rows[j]["DispatchDate"].ToString() == null)
                {
                    ZoneInfo += "<td width='15%'  style='color:darkred;'>Not Mantioned</td>";
                }
                else
                {
                    ZoneInfo += "<td width='15%'>" + dsMatDetails.Tables[0].Rows[j]["DispatchDate"].ToString() + "</td>";
                }
                if (dsMatDetails.Tables[0].Rows[j]["remarkByPurchase"].ToString() == null)
                {
                    ZoneInfo += "<td width='30%' style='color:darkred;'>Not Mantioned</td>";
                }
                else
                {
                    ZoneInfo += "<td width='30%'>" + dsMatDetails.Tables[0].Rows[j]["remarkByPurchase"].ToString() + "</td>";
                }
                if (dsMatDetails.Tables[0].Rows[j]["DispatchStatus"].ToString() == "1")
                {
                    ZoneInfo += "<td width='0%'><span class='label label-success'  style='font-size: 15.998px;'>Dispatched</span></td>";
                }
                else
                {
                    ZoneInfo += "<td width='30%'><a class='btn btn-info' href='Workshop_ViewEstMaterial.aspx?EstId=" + dsAcaDetails.Tables[0].Rows[i]["EstId"].ToString() + "'><i class='icon-edit icon-white'></i>Edit</a></td>";

                }
                ZoneInfo += "</tr>";
            }
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";


        divEstimateDetails.InnerHtml = ZoneInfo.ToString();
    }
    protected DataTable BindDatatable()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_DispatchExcel4PurchaseAndWorkShop '3'");
        dt = ds.Tables[0];
        return dt;
    }
    protected void btnExecl_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Dispatch.xls"));
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
}