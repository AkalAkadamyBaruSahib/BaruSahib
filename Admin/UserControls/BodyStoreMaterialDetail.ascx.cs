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

public partial class Admin_UserControls_BodyStoreMaterialDetail : System.Web.UI.UserControl
{
    DataTable dt = new DataTable();
    DataRow dr;
    public string IsReceived
    {
        set
        {
            ViewState["_IsReceived"] = value;
        }
        get
        {
            if (ViewState["_IsReceived"] == null)
            {
                ViewState["_IsReceived"] = "0";
            }
            return (string)ViewState["_IsReceived"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Material"] != null)
        {
            IsReceived = Request.QueryString["Material"].ToString();
            if (IsReceived == "1")
            {
                hdnIsReceived.Value = "1";
            }
            else
            {
                hdnIsReceived.Value = "2";
            }
        }
        if (!Page.IsPostBack)
        {
            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                hdnInchargeID.Value = Session["InchargeID"].ToString();
            }

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetStoreMaterialDetail(Convert.ToInt32(txtEstID.Text));
    }

    private void GetStoreMaterialDetail(int estID)
    {
        var columnName = hdnIsReceived.Value == "1" ? "Received" : "Dispatch";
        string UserTypeID = Session["UserTypeID"].ToString();
        string UserID = Session["InchargeID"].ToString();
        DataTable dtapproved = new DataTable();

        List<Estimate> storeRegister = new List<Estimate>();
        StoreRepository storeRepository = new StoreRepository(new AkalAcademy.DataContext());

        if (estID != null)
        {
            storeRegister = storeRepository.GetStockMaterialInfo(estID);
        }

        divMaterialDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;

        int Sno = -1;
        ZoneInfo += "<div class='row-fluid sortable'>";
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i>Material Detail</h2>";
        ZoneInfo += "<div class='box-icon'>";
        ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        ZoneInfo += "<div class='box-content'>";
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<tbody>";
        foreach (Estimate register in storeRegister)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            ZoneInfo += "<td>";
            ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td><b style='color:red;'>Estimate No:</b> " + estID + "</td>";
            ZoneInfo += "<td class='center'><b style='color:red;'>Sanction Date:</b> " + register.ModifyOn + "</td>";
            ZoneInfo += "<td class='center'><b style='color:red;'>Sub Estimate:</b> " + register.SubEstimate + "</td>";
            ZoneInfo += "<td class='center'><b style='color:red;'>Academy:</b> " + register.Academy.AcaName + "</td>";

            ZoneInfo += "<td class='center'><table><tr><td class='center'><b style='color:red;'>Upload Bill:</b><br/><a onclick='OpenUploadbill(" + estID + ");' href='#'><span class='label label-warning'  style='font-size: 15.998px;'>Upload Bill</span></a></td><td class='center'><b style='color:red;'>View Bill:</b><br/><a onclick='OpenViewbill(" + estID + ");' href='#'><span class='label label-warning'  style='font-size: 15.998px;'>View Bill</span></a></td></tr></table><td>";
            ZoneInfo += "</tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "<table id='tblmatDetail' border='1' class='table table-striped table-bordered bootstrap-datatable datatable'>";
            ZoneInfo += "<tr style='color:Green;'>";
            ZoneInfo += "<th width='5%'><b>SNo.</b></th>";
            ZoneInfo += "<th width='200px'>Material Name</th>";
            ZoneInfo += "<th>Required Quantity</th>";
            ZoneInfo += "<th>Purchase Quantity</th>";
            ZoneInfo += "<th>Pending Quantity</th>";
            ZoneInfo += "<th>In Store</th>";
            ZoneInfo += "<th>Dispatch Quantity</th>";
            ZoneInfo += "<th>Received On</th>";
            ZoneInfo += "<th>" + columnName + "Quantity</th>";
            if (hdnIsReceived.Value == "1")
            {
                ZoneInfo += "<th>Bill Number</th>";
            }

            DataTable dsMatDetails = new DataTable();

            dsMatDetails = storeRepository.GetStockMaterialInfo(estID, '2').Tables[0];
            decimal RemainingQty = -1;
            decimal PendingQty = -1;
            decimal TotalPurchaseQty = -1;
            for (int j = 0; j < dsMatDetails.Rows.Count; j++)
            {
                ZoneInfo += "<tr>";
                ZoneInfo += "<td><input type='hidden' value='" + dsMatDetails.Rows[j]["Sno"].ToString() + "' id='hdnEMRId" + j + "' />" + (j + 1) + "</td>";
                ZoneInfo += "<td>" + dsMatDetails.Rows[j]["MatName"].ToString() + "<br/><b>Unit:-</b> " + dsMatDetails.Rows[j]["UnitName"].ToString() + "</td>";
                // Required Quantity
                ZoneInfo += "<td>" + dsMatDetails.Rows[j]["Qty"].ToString() + "</td>";

                ZoneInfo += "<td><input type='hidden' value='" + dsMatDetails.Rows[j]["PurchaseQty"].ToString() + "' id='hdnPurchaseQty" + j + "' />" + dsMatDetails.Rows[j]["PurchaseQty"].ToString() + "</td>";// Purchase Qty
                TotalPurchaseQty = Convert.ToDecimal(dsMatDetails.Rows[j]["PurchaseQty"].ToString());

                // Pending Quantity

                PendingQty = Convert.ToDecimal(dsMatDetails.Rows[j]["Qty"].ToString()) - Convert.ToDecimal(dsMatDetails.Rows[j]["PurchaseQty"].ToString());
                ZoneInfo += "<td>" + PendingQty + "</td>";

                //In Store Quantity
                if (dsMatDetails.Rows[j]["DispatchQuantity"].ToString() == "")
                {
                    ZoneInfo += "<td><input type='hidden' value='" + dsMatDetails.Rows[j]["InStoreQuantity"].ToString() + "' id='hdnlblStoreQuantity" + j + "' /><label id='lblInStoreQty" + j + "'>" + dsMatDetails.Rows[j]["InStoreQuantity"].ToString() + "</label></td>";
                }
                else
                {
                    RemainingQty = Convert.ToDecimal(dsMatDetails.Rows[j]["InStoreQuantity"].ToString()) - Convert.ToDecimal(dsMatDetails.Rows[j]["DispatchQuantity"].ToString());
                    ZoneInfo += "<td><input type='hidden' value='" + RemainingQty + "' id='hdnlblRemainingQty" + j + "' /><label id='lblRemainingQty" + j + "'>" + RemainingQty + "</label></td>";
                }
               
                // Dispatch Quantity
            
                    ZoneInfo += "<td><input type='hidden' value='" + dsMatDetails.Rows[j]["DispatchQuantity"].ToString() + "' id='hdnlblDispatchQuantity" + j + "' /><label id='lblDispatchQuantity" + j + "'>" + dsMatDetails.Rows[j]["DispatchQuantity"].ToString() + "</label></td>";
               
                // Received On Quantity
                ZoneInfo += "<td>" + dsMatDetails.Rows[j]["ReceivedOn"].ToString() + "</td>";

                //Received\Dispatch Quantity
                if (hdnIsReceived.Value == "1")
                {
                    //ZoneInfo += "<td><input type='hidden' value='" + dsMatDetails.Rows[j]["DispatchQuantity"].ToString() + "' id='hdnlblDispatchQuantity" + j + "' /><label id='lblDispatchQuantity" + j + "'>" + dsMatDetails.Rows[j]["DispatchQuantity"].ToString() + "</label></td>";
                    ZoneInfo += "<td><input type='hidden' value='" + dsMatDetails.Rows[j]["InStoreQuantity"].ToString() + "' id='hdnInStoreQuantity" + j + "' /><input style='width:100px;' value='0' type='text' id='txtInStoreQuantity" + j + "' name='InStoreQuantity'></td>";
                }
                else
                {
                    //ZoneInfo += "<td><input type='hidden' value='" + dsMatDetails.Rows[j]["InStoreQuantity"].ToString() + "' id='hdnlblInStoreQuantity" + j + "' /><label id='lblStoreQty" + j + "'>" + dsMatDetails.Rows[j]["InStoreQuantity"].ToString() + "</label></td>";
                    ZoneInfo += "<td><input type='hidden' value='" + dsMatDetails.Rows[j]["DispatchQuantity"].ToString() + "' id='hdnDispatchQuantity" + j + "' /><input style='width:100px;' value='0'  id='txDispatchQuantity" + j + "' name='txDispatchQuantity'></td>";
                }
                if (hdnIsReceived.Value == "1")
                {
                    //  ZoneInfo += "<td><input type='hidden' value='" + dsMatDetails.Rows[j]["BillPath"].ToString() + "' id='hdnBillNo" + j + "' /><select style='width:120px;' id='drpBillNo" + j + "'><option value='0'>-Select Bill No--</option></select></td>";
                    ZoneInfo += "<td><input type='hidden' value='0' id='hdnBillNo" + j + "' /><select style='width:120px;' id='drpBillNo" + j + "'><option value='0'>-Select Bill No--</option></select></td>";
                }
              

                ZoneInfo += "</tr>";
            }
            ZoneInfo += "</table>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr>";
            if (hdnIsReceived.Value == "1")
            {
                ZoneInfo += "<td><input type='button' class='btn btn-primary'  value='Received Material' id='btnReceivedMaterial'/></td>";
            }
            else
            {
                ZoneInfo += "<td><input type='button' class='btn btn-primary'   value='Dispatch Material' id='btnDispatchMaterial'/></td>";
            }
            ZoneInfo += "</tr>";

            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";

        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";


        divMaterialDetails.InnerHtml = ZoneInfo.ToString();
        bindBillDropdown();
    }

    private void bindBillDropdown()
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "BindBillNoByEstID(" + txtEstID.Text + ");", true);
    }
}
