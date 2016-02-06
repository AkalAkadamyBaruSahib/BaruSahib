using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Transport_VehicleDetails : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    DataRow dr;
    private bool IsApproved = true;
    private int InchargeID = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InchargeID"] != null)
        {
            InchargeID = int.Parse(Session["InchargeID"].ToString());
        }

        if (Session["EmailId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblUser.Text = Session["EmailId"].ToString();
        }
        if (!Page.IsPostBack)
        {
            BindAcademy();

            getVehicleDetails(true, -1);
        }
        if (Request.QueryString["AcaId"] != null)
        {
            getVehicleDetails(true, int.Parse(Request.QueryString["AcaId"].ToString()));
            //btnExcel2.Visible = false;
            //btnExecl.Visible = false;
        }
        if (Request.QueryString["EstId"] != null)
        {
            GetPrint(Request.QueryString["EstId"].ToString());

        }

    }
    protected void GetPrint(string id)
    {
        DataSet dsValue = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateWithMaterialForEmp_V2 '" + id + "'");
        string EstInfo = string.Empty;
        EstInfo += "<div style='width:100%;font-family:Calibri;'>";
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
        //EstInfo += "<br />XXXXXXXXXXX";
        EstInfo += "</div>";
        EstInfo += "</td>";
        EstInfo += "</tr>";
        EstInfo += "</table>";
        EstInfo += "<br /><br />";
        EstInfo += "<div style='font-size:12px; margin-top:15px; font-weight:bold; width:100%;'>" + dsValue.Tables[0].Rows[0]["WorkAllotName"].ToString() + "</div>";
        EstInfo += "<table style='width:100%; margin-top:20px;'>";
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
        EstInfo += "<table style='width:100%; margin-top:15px;' border='1'>";
        EstInfo += "<thead>";
        EstInfo += "<tr>";
        EstInfo += "<th><b>Material</b></th>";
        EstInfo += "<th><b>Source Type</b></th>";
        EstInfo += "<th><b>Qty</b></th>";
        EstInfo += "<th><b>Unit</b></th>";
        EstInfo += "<th><b>Rate</b></th>";
        EstInfo += "<th style='width:152px;'><b>Amount</b></th>";
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
        EstInfo += "<div style='margin-top:50px; width:100%; text-align:center;'>&copy; The Kalgidhar Society All Rights Reserved</div>";
        EstInfo += "</div>";

        dt.Columns.Add("HtmlContent");
        dr = dt.NewRow();
        dr["HtmlContent"] = EstInfo;
        dt.Rows.Add(dr);
        pnlPdf.InnerHtml = dt.Rows[0][0].ToString();

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Estimate_" + dsValue.Tables[0].Rows[0]["EstId"].ToString() + ".pdf");
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

    private void GetEstimateDetailsByClick(string id)
    {
        DataSet dsEstimateDetails = new DataSet();
        //dsEstimateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateDetailsByEmpAndZone  '" + ID + "','"+ lblUser.Text +"'");
        dsEstimateDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateViewByAcaId '" + id + "'");
        var dtapproved = (from mytable in dsEstimateDetails.Tables[0].AsEnumerable()
                          where mytable.Field<bool>("IsApproved") == true
                          select mytable).CopyToDataTable();
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
        ZoneInfo += "<th width='10%'>Estmate No.</th>";
        ZoneInfo += "<th width='20%'>Estmate For</th>";
        //ZoneInfo += "<th width='15%'>Sanction Date</th>";
        ZoneInfo += "<th width='30%'>Details of Sactioned Estimates</th>";
        ZoneInfo += "<th width='15%'>Estimate Cost</th>";
        ZoneInfo += "<th width='40%'><table width='100%'><tr><th colspan='3' align='center'>Commulative Total Expenditure as On</th></tr><tr><th width='17%'>Bill Id</th><th width='50%'>Bill Date</th><th width='33%'>Bill Amount</th></tr></table></th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dtapproved.Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            ZoneInfo += "<td width='5%'>" + dtapproved.Rows[i]["EstId"].ToString() + "</td>";
            ZoneInfo += "<td width='20%'><table><tr><td><b>Zone</b>: " + dtapproved.Rows[i]["ZoneName"].ToString() + "</td></tr><tr><td><b>Academy</b>: " + dtapproved.Rows[i]["AcaName"].ToString() + "</td></tr><tr><td><b>Estimate File</b>: <a href='" + dtapproved.Rows[i]["FilePath"].ToString() + "' target='_blank'>" + dtapproved.Rows[i]["FileNme"].ToString() + "</a></td></tr></table></td>";
            //ZoneInfo += "<td class='center' width='15%'>" + dtapproved.Rows[i]["dt"].ToString() + "</td>";
            ZoneInfo += "<td class='center'width='30%'><table>";
            ZoneInfo += "<tr><td><b>Sub Head:</b> <a href='Admin_ParticularEstimateView.aspx?EstId=" + dtapproved.Rows[i]["EstId"].ToString() + "'>" + dtapproved.Rows[i]["SubEstimate"].ToString() + "</a></td></tr>";
            ZoneInfo += "<tr><td><b>Work Name:</b> " + dtapproved.Rows[i]["WorkAllotName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Sanction Date:</b> " + dtapproved.Rows[i]["dt"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><a class='btn btn-danger' href='Admin_EstimateView.aspx?EstId=" + dtapproved.Rows[i]["EstId"].ToString() + "'><span  style='font-size: 15.998px;'><i class='icon-edit icon-white'></i>Edit Estimate</span></a>   <a href='Admin_EstimateView.aspx?EstId=" + dtapproved.Rows[i]["EstId"].ToString() + "'><span class='label label-info'  style='font-size: 15.998px;'>Print Estimate</span></a></td></tr>";
            //ZoneInfo += "<tr><td><a class='btn btn-danger' href='Admin_EstimateEdit.aspx?EstId=" + dtapproved.Rows[i]["EstId"].ToString() + "'><span  style='font-size: 15.998px;'><i class='icon-edit icon-white'></i>Edit Estimate</span></a>   </td></tr>";
            ZoneInfo += "</table></td>";
            //ZoneInfo += "<td class='center'width='15%'>";
            //ZoneInfo += "" + dtapproved.Rows[i]["EstmateCost"].ToString() + "";
            //ZoneInfo += "</td>";
            DataSet dsBal = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateBalAmt '" + dtapproved.Rows[i]["EstId"].ToString() + "'");
            ZoneInfo += "<td width='20%'><table><tr><td> " + dtapproved.Rows[i]["EstmateCost"].ToString() + "</td></tr><tr><td><b>Bal</b>: " + dsBal.Tables[0].Rows[0]["BalAmt"].ToString() + "</td></tr></table></td>";
            ZoneInfo += "<td width='40%'><table width='100%'>";
            DataSet dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("SELECT SubBillId,CONVERT (VARCHAR(20),BillDate,107) AS bdATE,TotalAmount FROM SubmitBillByUser WHERE EstId='" + dtapproved.Rows[i]["EstId"].ToString() + "'");
            DataSet dsTa = DAL.DalAccessUtility.GetDataInDataSet("exec USP_TotalEstimateCostAfterBillSubmit '" + dtapproved.Rows[i]["EstId"].ToString() + "'");
            for (int j = 0; j < dsBillDetails.Tables[0].Rows.Count; j++)
            {
                ZoneInfo += "<tr><td width='17%'><a href='Admin_BillDetailsAfterApproval.aspx?SubBillId=" + dsBillDetails.Tables[0].Rows[j]["SubBillId"].ToString() + "'>" + dsBillDetails.Tables[0].Rows[j]["SubBillId"].ToString() + "</a></td><td width='50%'>" + dsBillDetails.Tables[0].Rows[j]["bdATE"].ToString() + "</td><td width='33%'>" + dsBillDetails.Tables[0].Rows[j]["TotalAmount"].ToString() + "</td></tr>";
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

    protected DataTable BindDatatable()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ExcelEstimate");
        dt = ds.Tables[0];
        return dt;
    }
    protected void btnExecl_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Estimate.xls"));
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

    private void getVehicleDetails(bool isApproved, int AcaID)
    {
        int UserTypeID = int.Parse(Session["UserTypeID"].ToString());
        DataSet dsVehicleDetails = new DataSet();
        dsVehicleDetails = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_GetVehiclesDetails]");
        System.Data.EnumerableRowCollection<System.Data.DataRow> dtApproved = null;
        DataTable dtapproved = new DataTable();
        int InchargeID = int.Parse(Session["InchargeID"].ToString());

        if (UserTypeID == 13)
        {
            if (AcaID > 0)
            {
                dtApproved = (from mytable in dsVehicleDetails.Tables[0].AsEnumerable()
                              where mytable.Field<int>("AcademyID") == AcaID
                              select mytable);
            }
            else
            {
                dtApproved = (from mytable in dsVehicleDetails.Tables[0].AsEnumerable()
                              where mytable.Field<bool>("IsApproved") == isApproved
                              select mytable);
            }
            if (dtApproved.Count() > 0)
            {
                dtapproved = dtApproved.CopyToDataTable();
            }
        }
        else if (UserTypeID == 14)
        {
            if (AcaID > 0)
            {
                dtApproved = (from mytable in dsVehicleDetails.Tables[0].AsEnumerable()
                              where mytable.Field<int>("AcademyID") == AcaID &&
                              mytable.Field<int>("InchargeID") == InchargeID
                              select mytable);
            }
            else
            {
                dtApproved = (from mytable in dsVehicleDetails.Tables[0].AsEnumerable()
                              where mytable.Field<bool>("IsApproved") == isApproved &&
                              mytable.Field<int>("InchargeID") == InchargeID
                              select mytable);

            }
            if (dtApproved.Count() > 0)
            {
                dtapproved = dtApproved.CopyToDataTable();
            }
        }
        else
        {
            TransportUserRepository transport = new TransportUserRepository(new AkalAcademy.DataContext());
            dtapproved = transport.GetVehiclesByInchargeID(InchargeID).Tables[0];
        }

        divEstimateDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Vehicles List</h2>";
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
        ZoneInfo += "<th width='25%'>Vehicle No.</th>";
        ZoneInfo += "<th width='30%'>Vehicle For</th>";
        //ZoneInfo += "<th width='15%'>Sanction Date</th>";
        ZoneInfo += "<th width='20%'>Details of Vehicle</th>";
        ZoneInfo += "<th width='5%'>Norms Completed (out of 16)</th>";

        //ZoneInfo += "<th width='40%'><table width='100%'><tr><th colspan='3' align='center'Vehicle Documents</th></tr><tr><th width='17%'>Insurance</th><th width='50%'>Polution</th><th width='33%'>Permit</th></tr></table></th>";
        ZoneInfo += "<th width='5%'>Document Completed (out of 6)</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dtapproved.Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            ZoneInfo += "<td width='25%'><table><tr><td><a class='btn btn-danger' href='AddEditVehicle.aspx?VehicleID=" + dtapproved.Rows[i]["ID"].ToString() + "'><span  style='font-size: 15.998px;'><i class='icon-edit icon-white'></i>" + dtapproved.Rows[i]["Number"].ToString() + "</span></a></td></tr><tr><td><b>Driver Name:</b> " + dtapproved.Rows[i]["DriverName"].ToString() + "</td></tr><tr><td><b>Conductor Name:</b> " + dtapproved.Rows[i]["ConducterName"].ToString() + "</td></tr><tr><td><b>Driver Number:</b> " + dtapproved.Rows[i]["DriverNumber"].ToString() + "</td></tr><tr><td><b>Conductor Number:</b> " + dtapproved.Rows[i]["ConducterNumber"].ToString() + "</td></tr></table></td>";
            ZoneInfo += "<td width='30%'><table><tr><td><b>Zone</b>: " + dtapproved.Rows[i]["ZoneName"].ToString() + "</td></tr><tr><td><b>Academy</b>: " + dtapproved.Rows[i]["AcaName"].ToString() + "</td></tr><tr><td><b>Transport Manager</b>: " + dtapproved.Rows[i]["InName"].ToString() + "</td></tr><tr><td><b>Transport Manager Number</b>: " + dtapproved.Rows[i]["TransportManagerNumber"].ToString() + "</td></tr></table>";
            //ZoneInfo += "<td class='center' width='15%'>" + dtapproved.Rows[i]["dt"].ToString() + "</td>";
            ZoneInfo += "<td class='center'width='20%'><table>";
            ZoneInfo += "<tr><td><b>Vehicle Type:</b>" + dtapproved.Rows[i]["Type"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Sitter:</b> " + dtapproved.Rows[i]["Sitter"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Owner Name:</b> " + dtapproved.Rows[i]["OwnerName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Owner Number:</b> " + dtapproved.Rows[i]["OwnerNumber"].ToString() + "</td></tr>";
            //ZoneInfo += "<tr><td><a class='btn btn-danger' href='Admin_EstimateEdit.aspx?EstId=" + dtapproved.Rows[i]["EstId"].ToString() + "'><span  style='font-size: 15.998px;'><i class='icon-edit icon-white'></i>Edit Estimate</span></a>   <a href='Admin_EstimateView.aspx?EstId=" + dtapproved.Rows[i]["EstId"].ToString() + "'><span class='label label-info'  style='font-size: 15.998px;'>Print Estimate</span></a></td></tr>";
            ZoneInfo += "</table></td>";


            ZoneInfo += "<td width='5%'>" + dtapproved.Rows[i]["Norms"].ToString() + "</td>";
            ZoneInfo += "<td width='5%'>" + dtapproved.Rows[i]["DocumentCount"].ToString() + "</td>";
            //ZoneInfo += "<td width='40%'><table width='100%'>";
            //ZoneInfo += "<tr><td width='17%'>" + dtapproved.Rows[i]["InsuranceEndDate"].ToString() + "</td><td width='50%'>" + dtapproved.Rows[i]["Pollution"].ToString() + "</td><td width='33%'>" + dtapproved.Rows[i]["PermitDate"].ToString() + "</td></tr>";
            //ZoneInfo += "</table></td>";
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
    protected DataTable BindDatatable2()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateWithMaterial");
        dt = ds.Tables[0];
        return dt;
    }
    protected void btnExcel2_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Estimate.xls"));
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
        string acaID = ddlAcademy.SelectedIndex == -1 ? "-1" : ddlAcademy.SelectedValue;
        if (((Button)sender).Text == "View Non Approved Vehicle(s)")
        {
            IsApproved = false;
            getVehicleDetails(false, int.Parse(acaID));
            ((Button)sender).Text = "View Approved Vehicle(s)";
        }
        else
        {
            IsApproved = true;
            ((Button)sender).Text = "View Non Approved Vehicle(s)";
            getVehicleDetails(true, int.Parse(acaID));
        }
    }

    protected void ddlAcademy_SelectedIndexChanged(object sender, EventArgs e)
    {
        int acaID = int.Parse(ddlAcademy.SelectedValue);
        getVehicleDetails(IsApproved, acaID);

    }

    private void BindAcademy()
    {
        UsersRepository users = new UsersRepository(new AkalAcademy.DataContext());
        List<Academy> acaList = new List<Academy>();
        if (Session["UserTypeID"].ToString() == "13")
        {
            acaList = users.GetAllAcademy(2);
        }
        else
        {
            acaList = users.GetAcademyByInchargeID(InchargeID);
        }

        ddlAcademy.DataSource = acaList;
        ddlAcademy.DataTextField = "AcaName";
        ddlAcademy.DataValueField = "AcaID";
        ddlAcademy.DataBind();
        ddlAcademy.Items.Insert(0, (new System.Web.UI.WebControls.ListItem("-- All Academy --", "0")));
        ddlAcademy.SelectedIndex = 0;

    }
}