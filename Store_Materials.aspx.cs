using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class Store_Materials : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    DataRow dr;
    protected void Page_Load(object sender, EventArgs e)
    {
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
                getStoreDetails(-1);
            }
            // BindVendor();
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
        EstInfo += "<td style='padding:0px; text-align:left;font-size:7px' valign='top'>";
        EstInfo += "<p>";
        EstInfo += "Estimate No: " + dsValue.Tables[0].Rows[0]["EstId"].ToString() + "<br />";
        EstInfo += "Academy: " + dsValue.Tables[0].Rows[0]["AcaName"].ToString() + "<br />";
        EstInfo += "Sanction Date: " + dsValue.Tables[0].Rows[0]["SanctionDate"].ToString();
        EstInfo += "</p>";
        EstInfo += "</td>";

        EstInfo += "</tr>";
        EstInfo += "</table>";
        EstInfo += "<br /><br />";
        EstInfo += "<div style='width:100%; font-size:10px; font-weight:bold; text-align:center;'>Material Dispatch Details</div>";
        EstInfo += "<br />";
        EstInfo += "<table style='width:100%; margin-top:20px;font-size:5.5px' border='1'>";
        EstInfo += "<thead>";
        EstInfo += "<tr>";
        EstInfo += "<th><b>Material</b></th>";
        EstInfo += "<th><b>Source Type</b></th>";
        EstInfo += "<th><b>Qty</b></th>";
        if (UserTypeID != "12")
        {
            EstInfo += "<th width='20%'><b>Purchase Officer</b></th>";
        }
        EstInfo += "<th><b>Rate</b></th>";
        EstInfo += "<th><b>" + HeaderText + "</b></th>";
        EstInfo += "<th><b>Remark</b></th>";
        EstInfo += "</tr>";
        EstInfo += "</thead>";
        EstInfo += "<tbody>";
        DataSet dsMatDetails = new DataSet();
        if (UserTypeID == "4")
        {
            dsMatDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateMaterialViewForPurchase_V1 '" + dsValue.Tables[0].Rows[0]["EstId"].ToString() + "','2' ");
        }
        else if (UserTypeID == "12")
        {
            dsMatDetails = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_EstimateMaterialViewForPurchase_V1ByEmployeeID] '" + dsValue.Tables[0].Rows[0]["EstId"].ToString() + "','2', " + UserID);
        }
        else if (UserTypeID == "2" || UserTypeID == "1")
        {
            dsMatDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EstimateMaterialViewForAdminUser '" + dsValue.Tables[0].Rows[0]["EstId"].ToString() + "' ");
        }
        for (int i = 0; i < dsMatDetails.Tables[0].Rows.Count; i++)
        {
            //if (i != dsMatDetails.Tables[0].Rows.Count - 1)
            //{
            EstInfo += "<tr>";
            EstInfo += "<td style='padding:0px; text-align:left;font-size:5px'>" + dsMatDetails.Tables[0].Rows[i]["MatName"].ToString() + "</td>";
            EstInfo += "<td style='padding:0px; text-align:left;font-size:5px'>" + dsMatDetails.Tables[0].Rows[i]["PSName"].ToString() + "</td>";
            EstInfo += "<td style='padding:0px; text-align:left;font-size:5px'>" + dsMatDetails.Tables[0].Rows[i]["Qty"].ToString() + "</td>";
            if (UserTypeID != "12")
            {
                EstInfo += "<td style='padding:0px; text-align:left;font-size:5px'>";
                EstInfo += "<table>";
                EstInfo += "<tr><td style='padding:0px; text-align:left;font-size:5px'> <b>Name:</b> " + dsMatDetails.Tables[0].Rows[i]["EmployeeName"].ToString() + " </td></tr>";
                if (dsMatDetails.Tables[0].Rows[i]["EmployeeAssignDateTime"].ToString() == "01-01-1900 AM 12:00:00")
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
            EstInfo += "<td style='padding:0px; text-align:left;font-size:5px'>" + dsMatDetails.Tables[0].Rows[i]["Rate"].ToString() + "</td>";
            //EstInfo += "<td style='width:152px;'>" + dsMatDetails.Tables[1].Rows[i]["Amount"].ToString() + "</td>";
            //if (dsMatDetails.Tables[0].Rows[i]["TantiveDate"].ToString() == null)
            //{
            //    EstInfo += "<td width='15%'  style='color:darkred;text-align:left;font-size:10px'>Not Mantioned</td>";
            //}
            //else
            //{
            //    EstInfo += "<td width='15%' style='padding:0px; text-align:left;font-size:10px'>" + dsMatDetails.Tables[0].Rows[i]["TantiveDate"].ToString() + "</td>";
            //}
            //if (dsMatDetails.Tables[0].Rows[i]["DispatchDate"].ToString() == null)
            //{
            //    EstInfo += "<td width='15%'  style='color:darkred;text-align:left;font-size:10px'>Not Mantioned</td>";
            //}
            //else
            //{
            //    EstInfo += "<td width='15%' style='padding:0px; text-align:left;font-size:10px'>" + dsMatDetails.Tables[0].Rows[i]["DispatchDate"].ToString() + "</td>";
            //}
            //if (dsMatDetails.Tables[0].Rows[i]["remarkByPurchase"].ToString() == null)
            //{
            //    EstInfo += "<td width='30%' style='color:darkred;text-align:left;font-size:10px'>Not Mantioned</td>";
            //}
            //else
            //{
            //    EstInfo += "<td width='30%' style='padding:0px; text-align:left;font-size:10px'>" + dsMatDetails.Tables[0].Rows[i]["remarkByPurchase"].ToString() + "</td>";
            //}

            if (dsMatDetails.Tables[0].Rows[i]["TantiveDate"].ToString() != string.Empty && dsMatDetails.Tables[0].Rows[i]["DispatchDate"].ToString() == "")
            {
                HeaderText = "Tantative Date";
                EstInfo += "<td style='padding:0px; text-align:left;font-size:5px'>" + dsMatDetails.Tables[0].Rows[i]["TantiveDate"].ToString() + "</td>";
            }
            else
            {
                HeaderText = "Purchase Date";
                EstInfo += "<td style='padding:0px; text-align:left;font-size:5px'>" + dsMatDetails.Tables[0].Rows[i]["DispatchDate"].ToString() + "</td>";
            }
            if (dsMatDetails.Tables[0].Rows[i]["remarkByPurchase"].ToString() == string.Empty)
            {
                EstInfo += "<td style='color:darkred;text-align:left;font-size:5px'>Not Mantioned</td>";
            }
            else
            {
                EstInfo += "<td style='padding:0px; text-align:left;font-size:5px'>" + dsMatDetails.Tables[0].Rows[i]["remarkByPurchase"].ToString() + "</td>";
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

    private void getStoreDetails(int AcaID)
    {
        string UserTypeID = Session["UserTypeID"].ToString();
        string UserID = Session["InchargeID"].ToString();

        DataSet dsAcaDetails = new DataSet();

        dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_StckRegister]");
        System.Data.EnumerableRowCollection<System.Data.DataRow> dtApproved = null;

        if (AcaID > 0)
        {
            dtApproved = (from mytable in dsAcaDetails.Tables[0].AsEnumerable()
                          where mytable.Field<int>("AcaID") == AcaID
                          select mytable);
        }
        else
        {
            dtApproved = (from mytable in dsAcaDetails.Tables[0].AsEnumerable()
                          where mytable.Field<DateTime>("CreatedOn") >= DateTime.Now.AddDays(-15)
                          select mytable);
        }

        DataTable dtapproved = new DataTable();
        if (dtApproved.Count() > 0)
        {
            dtapproved = dtApproved.CopyToDataTable();
        }

        divEstimateDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;

        int Sno = -1;

        ZoneInfo += "<div class='row-fluid sortable'>";
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Stock Register</h2>";
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
        for (int i = 0; i < dtapproved.Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            ZoneInfo += "<td>";
            ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td><b style='color:red;'>Estimate No:</b> " + dtapproved.Rows[i]["EstId"].ToString() + "</td>";
            ZoneInfo += "<td class='center'><b style='color:red;'>Sanction Date:</b> " + dtapproved.Rows[i]["SanctionDate"].ToString() + "</td>";
            ZoneInfo += "<td class='center'><b style='color:red;'>Sub Estimate:</b> " + dtapproved.Rows[i]["SubEstimate"].ToString() + "</td>";
            ZoneInfo += "<td class='center'><b style='color:red;'>Academy:</b> " + dtapproved.Rows[i]["AcaName"].ToString() + "</td>";

            ZoneInfo += "<td class='center'><table><tr><td class='center'><b style='color:red;'>Upload Bill:</b><br/><a onclick='OpenUploadbill(" + dtapproved.Rows[i]["EstId"].ToString() + ");' href='#'><span class='label label-warning'  style='font-size: 15.998px;'>Upload Bill</span></a></td><td class='center'><b style='color:red;'>View Bill:</b><br/><a onclick='OpenViewbill(" + dtapproved.Rows[i]["EstId"].ToString() + ");' href='#'><span class='label label-warning'  style='font-size: 15.998px;'>View Bill</span></a></td></tr></table><td>";
            ZoneInfo += "</tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "<table border='1' class='table table-striped table-bordered bootstrap-datatable datatable'>";
            ZoneInfo += "<tr style='color:Green;'>";
            ZoneInfo += "<th width='5%'><b>SNo.</b></th>";
            ZoneInfo += "<th width='200px'>Material Name</th>";
            ZoneInfo += "<th>Required Quantity</th>";
            ZoneInfo += "<th>Purchase Quantity</th>";
            ZoneInfo += "<th>In Store Quantity</th>";
            ZoneInfo += "<th>Source Type</th>";
            ZoneInfo += "<th>Received On</th>";
            // ZoneInfo += "<th>Dispatch On</th>";
            ZoneInfo += "<th>Dispatch Status</th>";
            //ZoneInfo += "<th width='0%'>Status&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>";
            ZoneInfo += "</tr>";
            DataSet dsMatDetails = new DataSet();
            dsMatDetails = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_StockMaterialDetails] '" + dtapproved.Rows[i]["EstId"].ToString() + "','2' ");
            decimal InstoreQuantity = -1;
            decimal RemainingQty = -1;
            for (int j = 0; j < dsMatDetails.Tables[0].Rows.Count; j++)
            {
                ZoneInfo += "<tr>";
                ZoneInfo += "<td>" + (j + 1) + "</td>";
                ZoneInfo += "<td>" + dsMatDetails.Tables[0].Rows[j]["MatName"].ToString() + "<br/><b>Unit:-</b> " + dsMatDetails.Tables[0].Rows[j]["UnitName"].ToString() + "</td>";
                // Required Quantity
                ZoneInfo += "<td>" + dsMatDetails.Tables[0].Rows[j]["Qty"].ToString() + "</td>";

                ZoneInfo += "<td>" + dsMatDetails.Tables[0].Rows[j]["PurchaseQty"].ToString() + "</td>";// Purchase Qty 
                //In Store Quantity
                if (dsMatDetails.Tables[0].Rows[j]["DispatchQuantity"].ToString() == "")
                {
                    ZoneInfo += "<td>" + dsMatDetails.Tables[0].Rows[j]["InStoreQuantity"].ToString() + "</td>";
                }
                else
                {
                    RemainingQty = Convert.ToDecimal(dsMatDetails.Tables[0].Rows[j]["InStoreQuantity"].ToString()) - Convert.ToDecimal(dsMatDetails.Tables[0].Rows[j]["DispatchQuantity"].ToString());
                    ZoneInfo += "<td>" + RemainingQty + "</td>";
                }

                InstoreQuantity = Convert.ToDecimal(dsMatDetails.Tables[0].Rows[j]["InStoreQuantity"].ToString());
                ZoneInfo += "<td>" + dsMatDetails.Tables[0].Rows[j]["PSName"].ToString() + "</td>";

                ZoneInfo += "<td>" + dsMatDetails.Tables[0].Rows[j]["ReceivedOn"].ToString() + "</td>";
                Sno = int.Parse(dsMatDetails.Tables[0].Rows[j]["Sno"].ToString());
                string MatID = dsMatDetails.Tables[0].Rows[j]["MatID"].ToString();

                DataSet dsStoreBillDetails = new DataSet();
                dsStoreBillDetails = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_StoreBillDetails] '" + dtapproved.Rows[i]["EstId"].ToString() + "'," + Sno + " ");
                if (dsStoreBillDetails.Tables[0].Rows.Count > 0)
                {
                    string innertable = string.Empty;
                    for (int k = 0; k < dsStoreBillDetails.Tables[0].Rows.Count; k++)
                    {
                        if (dsStoreBillDetails.Tables[0].Rows[k]["BillPath"].ToString() != string.Empty)
                        {
                            innertable += "<tr><td><a href='" + dsStoreBillDetails.Tables[0].Rows[k]["BillPath"].ToString() + "' target='_blank'><span class='label label-warning'  style='font-size: 15.998px;'>Scan Bill Copy_" + (k + 1) + "</span></a></td></tr>";
                        }
                    }

                    if (InstoreQuantity <= 0)
                    {
                        ZoneInfo += "<td><a onclick='OpenReceivedMaterial(" + Sno + "," + dsMatDetails.Tables[0].Rows[j]["Qty"].ToString() + "," + MatID + ");' href='#'><span class='label label-warning'  style='font-size: 15.998px;'>Received Material</span></a></br><table>" + innertable + "</table></td>";
                    }
                    else
                    {
                        ZoneInfo += "<td><a onclick='OpenReceivedMaterial(" + Sno + "," + dsMatDetails.Tables[0].Rows[j]["Qty"].ToString() + "," + MatID + ");' href='#'><span class='label label-warning'  style='font-size: 15.998px;'>Received Material</span></a></br><a onclick='OpenDispatchMaterial(" + Sno + ");' href='#'><span class='label label-warning'  style='font-size: 15.998px;'>Dispatch Material</span></a></br><table>" + innertable + "</table></td>";
                    }
                }
                else
                {
                    //ZoneInfo += "<td><a onclick='OpenReceivedMaterial(" + Sno + "," + receivedQty + "," + Rate + ");' href='#'><span class='label label-warning'  style='font-size: 15.998px;'>Received Material</span></a>/<a onclick='OpenDispatchMaterial(" + Sno + ");' href='#'><span class='label label-warning'  style='font-size: 15.998px;'>Dispatch Material</span></a></td>";
                    ZoneInfo += "<td><a onclick='OpenReceivedMaterial(" + Sno + "," + dsMatDetails.Tables[0].Rows[j]["Qty"].ToString() + "," + MatID + ");' href='#'><span class='label label-warning'  style='font-size: 15.998px;'>Received Material</span></a></td>";
                }
                

                //ZoneInfo += "<td width='30%'></td>";
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

    public DataTable BindDatatable()
    {
        string UserTypeID = Session["UserTypeID"].ToString();
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        ds = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_DispatchExcel4Stock] '2'");

        dt = ds.Tables[0];
        return dt;
    }

    protected void btnExecl_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "StockRegister.xls"));
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


    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet dsPurchaseQty = DAL.DalAccessUtility.GetDataInDataSet("Select PurchaseQty from EstimateAndMaterialOthersRelations  where Sno = " + hdnEMRId.Value + "");
        var PurchaseQuantity = dsPurchaseQty.Tables[0].Rows[0]["PurchaseQty"].ToString();
        if (PurchaseQuantity != "0.00" && Convert.ToDecimal(txtReceivedQty.Text) <= Convert.ToDecimal(PurchaseQuantity))
        {
            if (hdnIsReceived.Value == "1")
            {
                Int64 i = 0;
                i = DAL.DalAccessUtility.ExecuteNonQuery("Insert Into StockEntry (EMRID,ReceivedOn,Quantity,ReceivedBy,BillPath) VALUES (" + hdnEMRId.Value + ",GETDATE()," + txtReceivedQty.Text + "," + Session["InchargeID"].ToString() + ",'" + txtLinkBillNo.Text + "')");
                DAL.DalAccessUtility.ExecuteNonQuery("Update  EstimateAndMaterialOthersRelations set VendorId = '" + hdnVendorID.Value + "' where Sno =" + hdnEMRId.Value + "");
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material has been received.');", true);
                }
            }
            else
            {
               
                DAL.DalAccessUtility.ExecuteNonQuery("Insert Into StockDispatchEntry (EMRID,DispatchOn,DispatchQuantity,DispatchBy) VALUES (" + hdnEMRId.Value + ",GETDATE()," + txtReceivedQty.Text + "," + Session["InchargeID"].ToString() + ")");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material has been Dispatch.');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Can not Received and Dispatch the Material without Purchase the Material');", true);
        }
        getStoreDetails(int.Parse(ddlAcademy.SelectedValue));
     
    }


    private void BindAcademy()
    {
        DataSet dsBillDetails = DAL.DalAccessUtility.GetDataInDataSet("Select * FROM Academy order by AcaName asc");
        ddlAcademy.DataSource = dsBillDetails.Tables[0];
        ddlAcademy.DataTextField = "AcaName";
        ddlAcademy.DataValueField = "AcaID";
        ddlAcademy.DataBind();
        ddlAcademy.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--All Academy--", "0"));

    }

    protected void ddlAcademy_SelectedIndexChanged(object sender, EventArgs e)
    {
        int acaID = int.Parse(ddlAcademy.SelectedValue);
        getStoreDetails(acaID);
    }
}
   
