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

public partial class Admin_UserControls_EstimateViewAcademyWise : System.Web.UI.UserControl
{
    DataTable dt = new DataTable();
    DataRow dr;
    public int ModuleID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ModuleID"] != null)
        {
            ModuleID = int.Parse(Session["ModuleID"].ToString());
        }
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
            if (Request.QueryString["EstId"] != null)
            {
                GetPrint(Request.QueryString["EstId"].ToString());

            }
            BindAcademy();
            ShowAllEstimateDetails(true);
        }
    }

    private void ShowAllEstimateDetails(bool IsApproved)
    {
        DataSet dsEstimateDetails = new DataSet();

        //dsEstimateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateDetailsByEmpAndZone  '" + ID + "','"+ lblUser.Text +"'");

        if (ddlAcademy.SelectedIndex > 0)
        {
            dsEstimateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EmpEstimateAcaWise '" + lblUser.Text + "' ,'" + ddlAcademy.SelectedValue + "','" + ModuleID + "'");
        }
        else
        {
            dsEstimateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AllEstimate4Emp '" + lblUser.Text + "','" + ModuleID + "'");
        }

        var dtApproved = (from mytable in dsEstimateDetails.Tables[0].AsEnumerable()
                          where mytable.Field<bool>("IsApproved") == IsApproved && (mytable.Field<bool>("IsRejected") == !IsApproved) && (mytable.Field<bool>("IsItemRejected") == false)
                          select mytable);
        if (!IsApproved)
        {
            dtApproved = (from mytable in dsEstimateDetails.Tables[0].AsEnumerable()
                          where mytable.Field<bool>("IsApproved") == IsApproved || (mytable.Field<bool>("IsRejected") == !IsApproved) || (mytable.Field<bool>("IsItemRejected") == true)
                          select mytable);
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
        ZoneInfo += "<h2><i class='icon-user'></i> All Estimate List</h2>";
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
        ZoneInfo += "<th width='15%'>Estmate No.</th>";
        ZoneInfo += "<th width='15%'>Estimate For</th>";
        ZoneInfo += "<th width='25%'>Details of Sactioned Estimates</th>";
        ZoneInfo += "<th width='15%'>Estimate Cost</th>";
        ZoneInfo += "<th width='35%'><table width='100%'><tr><th colspan='2' align='center'>Review Comments</th></tr><tr><th width='33%'>Comments</th><th width='34%'>Reviewed On</th></tr></table></th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dtapproved.Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            if (IsApproved || (!Convert.ToBoolean(dtapproved.Rows[i]["IsApproved"].ToString()) && !Convert.ToBoolean(dtapproved.Rows[i]["IsRejected"].ToString())))
            {
                ZoneInfo += "<td width='10%'><span class='btn btn-primary'>" + dtapproved.Rows[i]["EstId"].ToString() + "</span></td>";
            }
            else
                if (dtapproved.Rows[i]["IsApproved"].ToString() == "True" && dtapproved.Rows[i]["IsItemRejected"].ToString() == "True")
                {
                    if (ModuleID == 2)
                    {
                        ZoneInfo += "<td width='20%'><table><tr><td><a class='btn btn-primary' href='Transport_EstimateEdit.aspx?IsRejected=1&EstID=" + dtapproved.Rows[i]["EstId"].ToString() + "'>Edit Estimate No. " + dtapproved.Rows[i]["EstId"].ToString() + "</a></td></tr><tr><td style='color:red'><b>Rejected</b></td></tr></table></td>";
                    }
                    else
                    {
                        ZoneInfo += "<td width='20%'><table><tr><td><a class='btn btn-primary' href='Emp_EditEstimate.aspx?IsRejected=1&EstID=" + dtapproved.Rows[i]["EstId"].ToString() + "'>Edit Estimate No. " + dtapproved.Rows[i]["EstId"].ToString() + "</a></td></tr><tr><td style='color:red'><b>Rejected</b></td></tr></table></td>";
                    }
                }
                else
                {
                    if (ModuleID == 2)
                    {
                        ZoneInfo += "<td width='20%'><a class='btn btn-primary' href='Transport_EstimateEdit.aspx?EstID=" + dtapproved.Rows[i]["EstId"].ToString() + "'>Edit Estimate No. " + dtapproved.Rows[i]["EstId"].ToString() + "</a> </td>";
                    }
                    else
                    {
                        ZoneInfo += "<td width='20%'><a class='btn btn-primary' href='Emp_EditEstimate.aspx?EstID=" + dtapproved.Rows[i]["EstId"].ToString() + "'>Edit Estimate No. " + dtapproved.Rows[i]["EstId"].ToString() + "</a> </td>";
                    }
                }

            ZoneInfo += "<td width='20%'><table><tr><td><b>Zone</b>: " + dtapproved.Rows[i]["ZoneName"].ToString() + "</td></tr><tr><td><b>Academy</b>: " + dtapproved.Rows[i]["AcaName"].ToString() + "</td></tr></table></td>";
            ZoneInfo += "<td class='center'width='25%'><table>";
            if (ModuleID == (int)TypeEnum.Module.Transport)
            {
                ZoneInfo += "<tr><td><b>Sub Head:</b><a href='Transport_ParticularEstimateView.aspx?EstId=" + dtapproved.Rows[i]["EstId"].ToString() + "'>" + dtapproved.Rows[i]["SubEstimate"].ToString() + "</a></td></tr>";
            }
            else if (ModuleID == (int)TypeEnum.Module.Workshop)
            {
                ZoneInfo += "<tr><td><b>Sub Head:</b><a href='WorkshopEmployee_ParticularEstimateView.aspx?EstId=" + dtapproved.Rows[i]["EstId"].ToString() + "'>" + dtapproved.Rows[i]["SubEstimate"].ToString() + "</a></td></tr>";
            }
            else
            {
                ZoneInfo += "<tr><td><b>Sub Head:</b><a href='Emp_ParticularEstimateView.aspx?EstId=" + dtapproved.Rows[i]["EstId"].ToString() + "'>" + dtapproved.Rows[i]["SubEstimate"].ToString() + "</a></td></tr>";
            }
            ZoneInfo += "<tr><td><b>Work Name:</b> " + dtapproved.Rows[i]["WorkAllotName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Sanction Date:</b>" + dtapproved.Rows[i]["SanctionDate"].ToString() + "</td></tr>";

            ZoneInfo += "</table></td>";
            DataSet dsBal = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateBalAmt '" + dtapproved.Rows[i]["EstId"].ToString() + "'");

            ZoneInfo += "<td width='20%'><table><tr><td> " + dtapproved.Rows[i]["EstmateCost"].ToString() + "</td></tr><tr><td><b>Bal</b>: " + dsBal.Tables[0].Rows[0]["BalAmt"].ToString() + "</td></tr></table></td>";

            ZoneInfo += "<td width='35%'><table width='100%'>";
            DataSet dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("Select * FROM EstimateStatus WHERE EstId='" + dtapproved.Rows[i]["EstId"].ToString() + "'");
            for (int j = 0; j < dsBillDetails.Tables[0].Rows.Count; j++)
            {
                ZoneInfo += "<tr><td width='33%'>" + (j + 1) + ". " + dsBillDetails.Tables[0].Rows[j]["ReviewComments"].ToString() + "</td><td width='34%'>" + dsBillDetails.Tables[0].Rows[j]["CreatedOn"].ToString() + "</td></tr>";
            }
            ZoneInfo += "</table></td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divEstimateDetails.InnerHtml = ZoneInfo.ToString();
    }

    protected void GetPrint(string id)
    {
        DataSet dsValue = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateWithMaterialForEmp_V2 '" + id + "'");
        string EstInfo = string.Empty;
        EstInfo += "<div style='width:100%; font-family:Calibri;'>";
        EstInfo += "<table style='width:100%;'>";
        EstInfo += "<tr>";
        EstInfo += "<td style='padding:0px; text-align:left; width:50%' valign='top'>";
        EstInfo += "<img src='http://barusahib.org/wp-content/uploads/2013/06/Logo.png' style='width:100%;' />";
        EstInfo += "</td>";
        EstInfo += "<td style='text-align: right; width:40%;'>";
        // EstInfo += "<br /><br />";
        EstInfo += "<div style='font-style:italic; text-align: right;font-family:Calibri;'>";
        EstInfo += "Baru Shahib,";
        EstInfo += "<br />Dist: Sirmaur";
        EstInfo += "<br />Himachal Pradesh-173001";
        //EstInfo += "<br />XXXXXXXXXXX";
        EstInfo += "</div>";
        EstInfo += "</td>";
        EstInfo += "</tr>";
        EstInfo += "</table>";
        EstInfo += "<br /><br />";
        EstInfo += "<div style='font-size:12px; margin-top:15px; font-weight:bold; width:100%;font-family:Calibri;'>" + dsValue.Tables[0].Rows[0]["WorkAllotName"].ToString() + "</div>";
        EstInfo += "<table style='font-size:12px;width:100%; margin-top:20px;font-family:Calibri;'>";
        EstInfo += "<tr>";
        EstInfo += "<td style='padding:0px; text-align:left;' valign='top'>";
        EstInfo += "<p>";
        EstInfo += "Estimate No: " + dsValue.Tables[0].Rows[0]["EstId"].ToString() + "<br />";
        EstInfo += "Zone:" + dsValue.Tables[0].Rows[0]["ZoneName"].ToString() + "<br />";
        EstInfo += "Estimate Title: " + dsValue.Tables[0].Rows[0]["SubEstimate"].ToString() + "<br />";
        EstInfo += "Sanction Date: " + dsValue.Tables[0].Rows[0]["SanctionDate"].ToString();
        EstInfo += "</p>";
        EstInfo += "</td>";
        EstInfo += "<td style='text-align: right;'>";
        EstInfo += "<p style='text-align: right;'>";
        EstInfo += "Academy: " + dsValue.Tables[0].Rows[0]["AcaName"].ToString() + "<br />";
        EstInfo += "Type of Work: " + dsValue.Tables[0].Rows[0]["TypeWorkName"].ToString() + "<br />";
        EstInfo += "Estimate Cost: " + dsValue.Tables[0].Rows[0]["EstmateCost"].ToString();
        EstInfo += "</p>";
        EstInfo += "</td>";
        EstInfo += "</tr>";
        EstInfo += "</table>";
        EstInfo += "<br /><br />";
        EstInfo += "<div style='width:100%; font-size:15px; font-weight:bold; text-align:center;'>Estimate Particular Details</div>";
        EstInfo += "<br />";
        EstInfo += "<table style='width:100%; margin-top:20px;' border='1'>";
        EstInfo += "<thead>";
        EstInfo += "<tr>";
        EstInfo += "<th><b>Material</b></th>";
        EstInfo += "<th><b>Source Type</b></th>";
        EstInfo += "<th><b>Qty</b></th>";
        EstInfo += "<th style='width:50px;'><b>Unit</b></th>";
        EstInfo += "<th><b>Rate</b></th>";
        EstInfo += "<th style='width:150px;'><b>Amount</b></th>";
        EstInfo += "</tr>";
        EstInfo += "</thead>";
        EstInfo += "<tbody>";
        for (int i = 0; i < dsValue.Tables[1].Rows.Count; i++)
        {

            EstInfo += "<tr>";
            EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["MatName"].ToString() + "</td>";
            EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["PSName"].ToString() + "</td>";
            EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["Qty"].ToString() + "</td>";
            EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["UnitName"].ToString() + "</td>";
            EstInfo += "<td>" + dsValue.Tables[1].Rows[i]["Rate"].ToString() + "</td>";
            EstInfo += "<td style='width:152px;'>" + dsValue.Tables[1].Rows[i]["Amount"].ToString() + "</td>";
            EstInfo += "</tr>";

        }
        EstInfo += "<tr>";
        EstInfo += "<td></td><td></td><td></td><td></td><td><b>Total</b></td>";
        EstInfo += "<td style='width:152px; font-weight:bold;'>" + dsValue.Tables[2].Rows[0]["Ttl"].ToString() + "</td>";
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

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=PdfReport.pdf");
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

    public void CreateTemplate(string watermarkText, string targetFileName)
    {
        var document = new Document();
        var pdfWriter = PdfWriter.GetInstance(document, new FileStream(targetFileName, FileMode.Create));
        var font = new Font(Font.TIMES_ROMAN, 60, Font.NORMAL, Color.LIGHT_GRAY);
        document.Open();
        ColumnText.ShowTextAligned(pdfWriter.DirectContent, Element.ALIGN_CENTER, new Phrase(watermarkText, font), 300, 400, 45);
        document.Close();
    }

    public void AddTextWatermark(string sourceFilePath, string watermarkTemplatePath, string targetFilePath)
    {
        var pdfReaderSource = new PdfReader(sourceFilePath);
        var pdfStamper = new PdfStamper(pdfReaderSource, new FileStream(targetFilePath, FileMode.Create));
        var pdfReaderTemplate = new PdfReader(watermarkTemplatePath);
        var page = pdfStamper.GetImportedPage(pdfReaderTemplate, 1);

        for (var i = 0; i < pdfReaderSource.NumberOfPages; i++)
        {
            var content = pdfStamper.GetUnderContent(i + 1);
            content.AddTemplate(page, 0, 0);
        }

        pdfStamper.Close();
        pdfReaderTemplate.Close();
    }

    public void AddImageWatermark(string sourceFilePath, string watermarkImagePath, string targetFilePath)
    {
        var pdfReader = new PdfReader(sourceFilePath);
        var pdfStamper = new PdfStamper(pdfReader, new FileStream(targetFilePath, FileMode.Create));
        var image = iTextSharp.text.Image.GetInstance(watermarkImagePath);
        image.SetAbsolutePosition(200, 400);

        for (var i = 0; i < pdfReader.NumberOfPages; i++)
        {
            var content = pdfStamper.GetUnderContent(i + 1);
            content.AddImage(image);
        }

        pdfStamper.Close();
    }

    protected void BindAcademy()
    {
        DataSet dsAca = new DataSet();
        dsAca = DAL.DalAccessUtility.GetDataInDataSet("exec USP_BindAcademyForEmpEstimateAcademyWise '" + lblUser.Text + "'");
        ddlAcademy.DataSource = dsAca;
        ddlAcademy.DataValueField = "AcaId";
        ddlAcademy.DataTextField = "AcaName";
        ddlAcademy.DataBind();
        ddlAcademy.Items.Insert(0, "Select Academy");
        ddlAcademy.SelectedIndex = 0;
    }

    private void getEstimateDetails()
    {
        DataSet dsEstimateDetails = new DataSet();
        //dsEstimateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateDetailsByEmpAndZone  '" + ID + "','"+ lblUser.Text +"'");
        dsEstimateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EmpEstimateAcaWise '" + lblUser.Text + "' ,'" + ddlAcademy.SelectedValue + "'");
        divEstimateDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> " + ddlAcademy.SelectedItem.Text + "'s Estimate List</h2>";
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
        ZoneInfo += "<th width='10%'>Estmate No.</th>";
        ZoneInfo += "<th width='15%'>Estimate For</th>";
        ZoneInfo += "<th width='25%'>Details of Sactioned Estimates</th>";
        ZoneInfo += "<th width='15%'>Estimate Cost</th>";
        ZoneInfo += "<th width='35%'><table width='100%'><tr><th colspan='2' align='center' >Commulative Total Expenditure as On</th></tr><tr><th>Date</th><th>Cost</th></tr></table></th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsEstimateDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            ZoneInfo += "<td width='10%'>" + dsEstimateDetails.Tables[0].Rows[i]["EstId"].ToString() + "</td>";
            ZoneInfo += "<td width='20%'><table><tr><td><b>Zone</b>: " + dsEstimateDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td></tr><tr><td><b>Academy</b>: " + dsEstimateDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td></tr></table></td>";
            ZoneInfo += "<td class='center'width='25%'><table>";
            if (ModuleID == 2)
            {
                ZoneInfo += "<tr><td><b>Sub Head:</b><a href='Transport_ParticularEstimateView.aspx?EstId=" + dsEstimateDetails.Tables[0].Rows[i]["EstId"].ToString() + "'>" + dsEstimateDetails.Tables[0].Rows[i]["SubEstimate"].ToString() + "</a></td></tr>";
            }
            else if (ModuleID == 4)
            {
                ZoneInfo += "<tr><td><b>Sub Head:</b><a href='WorkshopEmployee_ParticularEstimateView.aspx?EstId=" + dsEstimateDetails.Tables[0].Rows[i]["EstId"].ToString() + "'>" + dsEstimateDetails.Tables[0].Rows[i]["SubEstimate"].ToString() + "</a></td></tr>";
            }
            else
            {
                ZoneInfo += "<tr><td><b>Sub Head:</b><a href='Emp_ParticularEstimateView.aspx?EstId=" + dsEstimateDetails.Tables[0].Rows[i]["EstId"].ToString() + "'>" + dsEstimateDetails.Tables[0].Rows[i]["SubEstimate"].ToString() + "</a></td></tr>";
            }
            ZoneInfo += "<tr><td><b>Work Name:</b> " + dsEstimateDetails.Tables[0].Rows[i]["WorkAllotName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Sanction Date:</b>" + dsEstimateDetails.Tables[0].Rows[i]["SanctionDate"].ToString() + "</td></tr>";
            ZoneInfo += "</table></td>";
            DataSet dsBal = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateBalAmt '" + dsEstimateDetails.Tables[0].Rows[i]["EstId"].ToString() + "'");
            ZoneInfo += "<td width='20%'><table><tr><td> " + dsEstimateDetails.Tables[0].Rows[i]["EstmateCost"].ToString() + "</td></tr><tr><td><b>Bal</b>: " + dsBal.Tables[0].Rows[0]["BalAmt"].ToString() + "</td></tr></table></td>";
            //ZoneInfo += "<td width='35%'><table width='100%'><tr><td>--</td><td>--</td></tr></table></td>";
            ZoneInfo += "<td width='35%'><table width='100%'>";
            DataSet dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("SELECT SubBillId,CONVERT (VARCHAR(20),BillDate,107) AS bdATE,TotalAmount FROM SubmitBillByUser WHERE EstId='" + dsEstimateDetails.Tables[0].Rows[i]["EstId"].ToString() + "'");
            DataSet dsTa = DAL.DalAccessUtility.GetDataInDataSet("exec USP_TotalEstimateCostAfterBillSubmit '" + dsEstimateDetails.Tables[0].Rows[i]["EstId"].ToString() + "'");
            for (int j = 0; j < dsBillDetails.Tables[0].Rows.Count; j++)
            {
                ZoneInfo += "<tr><td width='33%'><a href='Emp_BillDetails.aspx?BillId=" + dsBillDetails.Tables[0].Rows[j]["SubBillId"].ToString() + "'>" + dsBillDetails.Tables[0].Rows[j]["SubBillId"].ToString() + "</a></td><td width='34%'>" + dsBillDetails.Tables[0].Rows[j]["bdATE"].ToString() + "</td><td width='33%'>" + dsBillDetails.Tables[0].Rows[j]["TotalAmount"].ToString() + "</td></tr>";
                //if (dsTa.Tables[0].Rows.Count > 0 && dsTa.Tables[1].Rows.Count > 0)

            }
            if (dsTa.Tables[0].Rows[0]["Ta"].ToString() != "" && dsTa.Tables[1].Rows[0]["BDate"].ToString() != "")
            {
                ZoneInfo += "<td></td><td><b>Total Amount</b></td><td>" + dsTa.Tables[0].Rows[0]["Ta"].ToString() + "</td>";

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
        //ZoneInfo += "</div>";
        //ZoneInfo += "</div>";
        divEstimateDetails.InnerHtml = ZoneInfo.ToString();
    }

    protected void ddlAcademy_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowAllEstimateDetails(true);
        //getEstimateDetails();
    }

    protected DataTable BindDatatable()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ExcelEstimateForAllUsers '" + lblUser.Text + "','" + ModuleID + "'");
        dt = ds.Tables[0];
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

    protected DataTable BindDatatable2()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateWithMaterial4AllUser '" + lblUser.Text + "','" + ModuleID + "'");
        dt = ds.Tables[0];
        return dt;
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

    protected void btnNonApproved_Click(object sender, EventArgs e)
    {
        if (((Button)sender).Text == "View Rejected\\Non Approved Estimates")
        {
            ShowAllEstimateDetails(false);
            ((Button)sender).Text = "View Approved Estimates";
        }
        else
        {
            ((Button)sender).Text = "View Rejected\\Non Approved Estimates";
            ShowAllEstimateDetails(true);
        }
    }
}