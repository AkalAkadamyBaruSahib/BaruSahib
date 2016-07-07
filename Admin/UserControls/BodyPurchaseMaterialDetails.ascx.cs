using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Admin_UserControls_BodyPurchaseMaterialDetails : System.Web.UI.UserControl
{
    DataTable dt = new DataTable();
    DataRow dr;

    private int _islocal = -1;

    public int PurchaseSource
    {
        get
        {
            return _islocal;
        }
        set
        {
            _islocal = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       PurchaseSource = Request.QueryString["IsLocal"] != null ? 1 : 2;

        if (!Page.IsPostBack)
        {
                    
            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblUser.Text = Session["InchargeID"].ToString();
            }


            if (Request.QueryString["EstId"] != null)
            {
                GetPrint(Request.QueryString["EstId"].ToString());
            }
            else
            {
                BindAcademy();
                getPurchaseMaterialsDetailsDetails(-1, PurchaseSource);

            }
        }
    }

    protected void GetPrint(string id)
    {
        string HeaderText = "Tantative Date";
        string UserTypeID = Session["UserTypeID"].ToString();
        string UserID = Session["InchargeID"].ToString();
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
        //EstInfo += "<br />XXXXXXXXXXX";
        EstInfo += "</div>";
        EstInfo += "</td>";
        EstInfo += "</tr>";
        EstInfo += "</table>";
        EstInfo += "<br /><br />";
        EstInfo += "<div style='font-size:10px; margin-top:10px; font-weight:bold; width:100%;'>" + dsValue.Tables[0].Rows[0]["SubEstimate"].ToString() + "</div>";
        EstInfo += "<table style='width:100%; margin-top:20px;'>";
        EstInfo += "<tr>";
        EstInfo += "<td style='padding:0px; text-align:left;font-size:10px' valign='top'>";
        EstInfo += "<p>";
        EstInfo += "Estimate No: " + dsValue.Tables[0].Rows[0]["EstId"].ToString() + "<br />";
        EstInfo += "Academy: " + dsValue.Tables[0].Rows[0]["AcaName"].ToString() + "<br />";
        EstInfo += "Zone: " + dsValue.Tables[0].Rows[0]["ZoneName"].ToString() + "<br />";
        EstInfo += "Sanction Date: " + dsValue.Tables[0].Rows[0]["SanctionDate"].ToString();
        EstInfo += "</p>";
        EstInfo += "</td>";

        EstInfo += "</tr>";
        EstInfo += "</table>";
        EstInfo += "<br /><br />";
        EstInfo += "<div style='width:100%; font-size:15px; font-weight:bold; text-align:center;'>Material Dispatch Details</div>";
        EstInfo += "<br />";
        EstInfo += "<table style='width:100%; margin-top:20px;font-size:10px' border='1'>";
        EstInfo += "<thead>";
        EstInfo += "<tr>";
        EstInfo += "<th><b>Sr. No.</b></th>";
        EstInfo += "<th><b>Material</b></th>";
        EstInfo += "<th><b>Source Type</b></th>";
        EstInfo += "<th><b>Qty</b></th>";
        EstInfo += "<th><b>Unit</b></th>";
        EstInfo += "<th><b>Rate</b></th>";
        if (UserTypeID != "12")
        {
            EstInfo += "<th width='20%'><b>Purchase Officer</b></th>";
        }
        EstInfo += "<th><b>" + HeaderText + "</b></th>";
        EstInfo += "<th><b>Remark</b></th>";
        EstInfo += "</tr>";
        EstInfo += "</thead>";
        EstInfo += "<tbody>";
        DataSet dsMatDetails = new DataSet();
        if (UserTypeID == "4")
        {
            dsMatDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateMaterialViewForPurchase_V1 '" + dsValue.Tables[0].Rows[0]["EstId"].ToString() + "','" + PurchaseSource + "' ");
        }
        else if (UserTypeID == "12")
        {
            dsMatDetails = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_EstimateMaterialViewForPurchase_V1ByEmployeeID] '" + dsValue.Tables[0].Rows[0]["EstId"].ToString() + "','" + PurchaseSource + "', " + UserID);
        }
        else if (UserTypeID == "2" || UserTypeID == "1")
        {
            dsMatDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateMaterialViewForAdminUser '" + dsValue.Tables[0].Rows[0]["EstId"].ToString() + "','" + PurchaseSource + "'");
        }
        for (int i = 0; i < dsMatDetails.Tables[0].Rows.Count; i++)
        {
            //if (i != dsMatDetails.Tables[0].Rows.Count - 1)
            //{
            EstInfo += "<tr>";
            EstInfo += "<td font-size:10px'>" + i + 1 + "</td>";
            EstInfo += "<td style='padding:0px; text-align:left;font-size:10px'>" + dsMatDetails.Tables[0].Rows[i]["MatName"].ToString() + "</td>";
            EstInfo += "<td style='padding:0px; text-align:left;font-size:10px'>" + dsMatDetails.Tables[0].Rows[i]["PSName"].ToString() + "</td>";
            EstInfo += "<td style='padding:0px; text-align:left;font-size:10px'>" + dsMatDetails.Tables[0].Rows[i]["Qty"].ToString() + "</td>";
            EstInfo += "<td style='padding:0px; text-align:left;font-size:10px'>" + dsMatDetails.Tables[0].Rows[i]["UnitName"].ToString() + "</td>";
            EstInfo += "<td style='padding:0px; text-align:left;font-size:10px'>" + dsMatDetails.Tables[0].Rows[i]["Rate"].ToString() + "</td>";
            if (UserTypeID != "12")
            {
                EstInfo += "<td style='padding:0px; text-align:left;font-size:10px'>";
                EstInfo += "<table>";
                EstInfo += "<tr><td style='padding:0px; text-align:left;font-size:5px'> <b>Name:</b> " + dsMatDetails.Tables[0].Rows[i]["EmployeeName"].ToString() + " </td></tr>";
                if (dsMatDetails.Tables[0].Rows[i]["EmployeeAssignDateTime"].ToString() == "1/1/1900 12:00:00 AM")
                {
                    EstInfo += "<tr><td style='padding:0px; text-align:left;font-size:5px'><b>Assigned Date:</b> </td></tr>";
                }
                else
                {
                    EstInfo += "<tr><td style='padding:0px; text-align:left;font-size:5px'><b>Assigned Date:</b> " + dsMatDetails.Tables[0].Rows[i]["EmployeeAssignDateTime"].ToString() + "</td></tr>";
                }
                EstInfo += "</table>";
                EstInfo += "</td>";
            }

            if (dsMatDetails.Tables[0].Rows[i]["TantiveDate"].ToString() != string.Empty && dsMatDetails.Tables[0].Rows[i]["DispatchDate"].ToString() == "")
            {
                HeaderText = "Tantative Date";
                EstInfo += "<td style='padding:0px; text-align:left;font-size:10px'>" + dsMatDetails.Tables[0].Rows[i]["TantiveDate"].ToString() + "</td>";
            }
            else
            {
                HeaderText = "Purchase Date";
                EstInfo += "<td style='padding:0px; text-align:left;font-size:10px'>" + dsMatDetails.Tables[0].Rows[i]["DispatchDate"].ToString() + "</td>";
            }
            if (dsMatDetails.Tables[0].Rows[i]["remarkByPurchase"].ToString() == string.Empty)
            {
                EstInfo += "<td style='color:darkred;text-align:left;font-size:10px'> </td>";
            }
            else
            {
                EstInfo += "<td style='padding:0px; text-align:left;font-size:10px'>" + dsMatDetails.Tables[0].Rows[i]["remarkByPurchase"].ToString() + "</td>";
            }
            EstInfo += "</tr>";
        }
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
        Response.AddHeader("content-disposition", "attachment;filename=MaterialDispatch_" + dsValue.Tables[0].Rows[0]["EstId"].ToString() + ".pdf");
        //Response.AddHeader("content-disposition", "attachment;filename=MaterialDispatch_" + dsValue.Tables[0].Rows[0]["EstId"].ToString() + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        pnlPdf.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 0, 0, 0, 0);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }

    private void getPurchaseMaterialsDetailsDetails(int AcaID, int PSID)
    {
        string UserTypeID = Session["UserTypeID"].ToString();
        string UserID = Session["InchargeID"].ToString();

        DataSet dsAcaDetails = new DataSet();
        List<EstimateViewForPurchaser> PurchaseEstimateView = new List<EstimateViewForPurchaser>();
        PurchaseRepository purchaseRepo = new PurchaseRepository(new AkalAcademy.DataContext());
        

        System.Data.EnumerableRowCollection<System.Data.DataRow> dtApproved = null;

        if (AcaID > 0)
        {

            if (UserTypeID == "4")
            {
                PurchaseEstimateView = purchaseRepo.EstimateViewForPurchaseByAcaID(PSID,AcaID);
            }
            else if (UserTypeID == "12")
            {
                PurchaseEstimateView = purchaseRepo.EstimateViewForPurchaseByEmployeeIDByAcaID(Convert.ToInt32(UserID), PSID,AcaID);
            }
            else if (UserTypeID == "1")
            {
                if (PSID == 1)
                {
                    PurchaseEstimateView = purchaseRepo.MaterialDepatchStatusForAdminByAcaID(PSID,AcaID);
                }
                else
                {
                    PurchaseEstimateView = purchaseRepo.MaterialDepatchStatusForAdminByAcaID(PSID,AcaID);
                }
            }
            else if (UserTypeID == "2")
            {
                var UserId = lblUser.Text;
                PurchaseEstimateView = purchaseRepo.MaterialDepatchStatusByAcaID(Convert.ToInt32(UserId), PSID,AcaID);
            }
        }
        else
        {
            if (UserTypeID == "4")
            {
                //dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateViewForPurchase'" + PSID + "' ");
                PurchaseEstimateView = purchaseRepo.EstimateViewForPurchase(PSID);

            }
            else if (UserTypeID == "12")
            {
                PurchaseEstimateView = purchaseRepo.EstimateViewForPurchaseByEmployeeID(Convert.ToInt32(UserID), PSID);
                //dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_EstimateViewForPurchaseByEmployeeID] " + UserID + ",'" + PSID + "',1");
            }
            else if (UserTypeID == "1")
            {
                if (PSID == 1)
                {
                    PurchaseEstimateView = purchaseRepo.MaterialDepatchStatusForAdmin(PSID);
                    // dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_MaterialDepatchStatusForAdmin'" + PSID + "'");
                }
                else
                {
                    PurchaseEstimateView = purchaseRepo.MaterialDepatchStatusForAdmin(PSID);
                    //dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_MaterialDepatchStatusForAdmin'" + PSID + ",3' ");
                }
            }
            else if (UserTypeID == "2")
            {
                var UserId = lblUser.Text;
                PurchaseEstimateView = purchaseRepo.MaterialDepatchStatus(Convert.ToInt32(UserId), PSID);
                //  dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_MaterialDepatchStatus '" + lblUser.Text + "','" + PSID + "'");
            }
        }

       
        divEstimateDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        if (PurchaseEstimateView.Count > 0)
        {
            if (PurchaseEstimateView.Count > 0)
            {
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
                foreach (EstimateViewForPurchaser Est in PurchaseEstimateView)
                {
                    ZoneInfo += "<tr>";
                    ZoneInfo += "<td style='display:none;'>1</td>";
                    ZoneInfo += "<td>";
                    ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
                    ZoneInfo += "<tr>";
                    ZoneInfo += "<td width='20%'><b style='color:red;'>Estimate No:</b> " + Est.EstId + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Sanction Date:</b> " + Est.SanctionDate + "</td>";
                    ZoneInfo += "<td class='center' width='25%'><b style='color:red;'>Sub Estimate:</b> " + Est.SubEstimate + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Academy:</b> " + Est.AcaName + "</td>";
                    ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Zone:</b> " + Est.ZoneName + "</td>";
                    if (UserTypeID == "4")
                    {
                        ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est.EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a>/<a href='Purchase_ViewEstMaterial.aspx?EstId=" + Est.EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Edit</span></a></td>";
                    }
                    else if (UserTypeID == "12")
                    {
                        ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est.EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a>/<a href='PurchaseEmployee_ViewEstMaterial.aspx?EstId=" + Est.EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Edit</span></a></td>";
                    }
                    else
                    {
                        if (PurchaseSource == 1)
                        {
                            ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?IsLocal=1&EstId=" + Est.EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a></td>";
                        }
                        else
                        {
                            ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est.EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a></td>";
                        }
                    }
                    ZoneInfo += "</tr>";
                    ZoneInfo += "</table>";
                    ZoneInfo += "<table border='1' class='table table-striped table-bordered bootstrap-datatable datatable'>";
                    ZoneInfo += "<tr style='color:Green;'>";
                    ZoneInfo += "<th width='5%'><b>Sr. No.</b></th>";
                    ZoneInfo += "<th width='20%'>Material Name</th>";
                    ZoneInfo += "<th width='2%'>Unit</th>";
                    ZoneInfo += "<th width='2%'>Quantity</th>";
                    ZoneInfo += "<th width='5%'>Source Type</th>";
                    if (UserTypeID != "12")
                    {
                        ZoneInfo += "<th width='27%'>Purchase Officer</th>";
                    }
                    ZoneInfo += "<th width='15%'>Purchase Date</th>";
                    ZoneInfo += "<th width='20%'>Remark</th>";
                    if (UserTypeID == "4")
                    {
                        ZoneInfo += "<th width='0%'>Action</th>";
                    }
                    ZoneInfo += "</tr>";
                    DataSet dsMatDetails = new DataSet();
                    if (UserTypeID == "4")
                    {
                        dsMatDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateMaterialViewForPurchase_V1 '" + Est.EstId + "','" + PSID + "'");
                    }
                    else if (UserTypeID == "12")
                    {
                        dsMatDetails = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_EstimateMaterialViewForPurchase_V1ByEmployeeID] '" + Est.EstId + "','" + PSID + "', " + UserID);
                    }
                    else if (UserTypeID == "2" || UserTypeID == "1")
                    {
                        dsMatDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateMaterialViewForAdminUser '" + Est.EstId + "', '" + PSID + "'");
                    }

                    for (int j = 0; j < dsMatDetails.Tables[0].Rows.Count; j++)
                    {
                        ZoneInfo += "<tr>";
                        ZoneInfo += "<td>" + (j + 1) + "</td>";
                        ZoneInfo += "<td>" + dsMatDetails.Tables[0].Rows[j]["MatName"].ToString() + "</td>";
                        ZoneInfo += "<td>" + dsMatDetails.Tables[0].Rows[j]["UnitName"].ToString() + "</td>";
                        ZoneInfo += "<td>" + dsMatDetails.Tables[0].Rows[j]["Qty"].ToString() + "</td>";
                        ZoneInfo += "<td>" + dsMatDetails.Tables[0].Rows[j]["PSName"].ToString() + "</td>";
                        if (UserTypeID != "12")
                        {
                            ZoneInfo += "<td class='left'>";
                            ZoneInfo += "<table>";
                            ZoneInfo += "<tr><td> <b>Name:</b> " + dsMatDetails.Tables[0].Rows[j]["EmployeeName"].ToString() + " </td></tr>";
                            if (dsMatDetails.Tables[0].Rows[j]["EmployeeAssignDateTime"].ToString() == "1/1/1900 12:00:00 AM")
                            {
                                ZoneInfo += "<tr><td><b>Assigned Date:</b> </td></tr>";
                            }
                            else
                            {
                                ZoneInfo += "<tr><td style='color:darkred;'><b>Assigned Date:</b> " + dsMatDetails.Tables[0].Rows[j]["EmployeeAssignDateTime"].ToString() + "</td></tr>";
                            }
                            ZoneInfo += "</table>";
                            ZoneInfo += "</td>";
                        }


                        ZoneInfo += "<td>" + dsMatDetails.Tables[0].Rows[j]["DispatchDate"].ToString() + "</td>";


                        ZoneInfo += "<td>" + dsMatDetails.Tables[0].Rows[j]["remarkByPurchase"].ToString() + "</td>";

                        if (UserTypeID == "4")
                        {
                            ZoneInfo += "<td width='30%'><a href='javascript: openModelPopUp(" + Est.EstId + "," + dsMatDetails.Tables[0].Rows[j]["Sno"].ToString() + ");'><span class='label label-warning'  style='font-size: 15.998px;'>Reject Item</span></a></td>";
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
            }
        }
        divEstimateDetails.InnerHtml = ZoneInfo.ToString();
    }

    public DataTable BindDatatable()
    {
        string UserTypeID = Session["UserTypeID"].ToString();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        if (UserTypeID == "4")
        {
            ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_DispatchExcel4PurchaseAndWorkShop '2'");
        }
        else if (UserTypeID == "1")
        {
            ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_DispatchExcel");
        }
        else if (UserTypeID == "2")
        {
            ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_DispatchExcel");
        }

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
                Response.Write(str + Convert.ToString(dr[j]).Trim());
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }

    protected void ddlAcademy_SelectedIndexChanged(object sender, EventArgs e)
    {
        int acaID = int.Parse(ddlAcademy.SelectedValue);

        getPurchaseMaterialsDetailsDetails(acaID, PurchaseSource);
    }

    private void BindAcademy()
    {
        UsersRepository repo = new UsersRepository(new AkalAcademy.DataContext());
        int inchargeID = Convert.ToInt16(Session["InchargeID"].ToString());
        List<Academy> acaList = repo.GetAllAcademy(1);
        //List<Academy> acaList = repo.GetAcademyByInchargeID(inchargeID);
        ddlAcademy.DataSource = acaList;
        ddlAcademy.DataTextField = "AcaName";
        ddlAcademy.DataValueField = "AcaID";
        ddlAcademy.DataBind();
        ddlAcademy.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Academy--", "0"));
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PurchaseControler controler = new PurchaseControler();
        controler.RejectMaterialItemByID(int.Parse(hidEMRID.Value), int.Parse(hidEstID.Value));
        getPurchaseMaterialsDetailsDetails(-1,-1);
        DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set remarkByPurchase = '" + txtRemarks.Text + "' where estid = '" + hidEstID.Value + "' and sno ='" + hidEMRID.Value + "'");
        txtRemarks.Text = "";
    }

  
}