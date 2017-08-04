using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Net;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;


public partial class PurchaseOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public class CookieAwareWebClient : WebClient
    {
        public CookieAwareWebClient()
        {
            CookieContainer = new CookieContainer();
        }
        public CookieContainer CookieContainer { get; private set; }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = (HttpWebRequest)base.GetWebRequest(address);
            request.CookieContainer = CookieContainer;
            return request;
        }
    }

    public void getHTML()
    {
        string htmlCode = string.Empty;
        Uri myuri = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
        string appPath = HttpContext.Current.Request.ApplicationPath;

        string pathQuery = myuri.PathAndQuery;
        string FolderPath = myuri.ToString().Replace(pathQuery, "");
        string hostName = FolderPath + appPath;

        using (var client = new CookieAwareWebClient())
        {
            htmlCode = client.DownloadString(hostName + "/PurchaseOrderTemplate.html");
        }

        htmlCode = htmlCode.Replace("[Vendor]", hdnVendorName.Value.ToUpper() + ",<br/>" + hdnVendorAddress.Value.ToUpper() + "" + hdnCity.Value.ToUpper() + ",<br/>" + hdnVendorContactNo.Value);
        if (drpPOFor.SelectedValue == "1")
        {
            if (hdnDeliveryID.Value == "119" && hdnBillingID.Value != "119")
            {
                htmlCode = htmlCode.Replace("[DELIVERYADDRESS]", "THE KALGIDHAR SOCIETY,<br/>" + hdnBillingAddres.Value);
                htmlCode = htmlCode.Replace("[ShipTo]", "THE KALGIDHAR SOCIETY,<br/>" + hdnTrustName.Value + ",<br/>" + hdnDeliveryAddress.Value);
            }
            else if (hdnBillingID.Value == "119" && hdnDeliveryID.Value != "119")
            {
                htmlCode = htmlCode.Replace("[ShipTo]", "THE KALGIDHAR SOCIETY,<br/>" + hdnDeliveryAddress.Value);
                htmlCode = htmlCode.Replace("[DELIVERYADDRESS]", "THE KALGIDHAR SOCIETY,<br/>" + hdnBillingName.Value + ",<br/>" + hdnBillingAddres.Value);
            }
            else if (hdnBillingID.Value == "119" && hdnDeliveryID.Value == "119")
            {
                htmlCode = htmlCode.Replace("[ShipTo]", "THE KALGIDHAR SOCIETY,<br/>" + hdnDeliveryAddress.Value);
                htmlCode = htmlCode.Replace("[DELIVERYADDRESS]", "THE KALGIDHAR SOCIETY,<br/>" + hdnBillingAddres.Value);
            }
            else
            {
                htmlCode = htmlCode.Replace("[ShipTo]", "THE KALGIDHAR SOCIETY,<br/>" + hdnTrustName.Value + ",<br/>" + hdnDeliveryAddress.Value);
                htmlCode = htmlCode.Replace("[DELIVERYADDRESS]", "THE KALGIDHAR SOCIETY,<br/>" + hdnBillingName.Value + ",<br/>" + hdnBillingAddres.Value);
            }
            htmlCode = htmlCode.Replace("[POheader]", "THE KALGIDHAR SOCIETY");
            htmlCode = htmlCode.Replace("[status]", "Society");
            htmlCode = htmlCode.Replace("[src]", Server.MapPath("img") + "/Logo_Society.png");
        }
        else if (drpPOFor.SelectedValue == "2")
       {
           if (hdnDeliveryID.Value == "119" && hdnBillingID.Value != "119")
           {
               htmlCode = htmlCode.Replace("[ShipTo]", "THE KALGIDHAR TRUST,<br/>"  + hdnDeliveryAddress.Value);
               htmlCode = htmlCode.Replace("[DELIVERYADDRESS]", "THE KALGIDHAR TRUST,<br/>" + hdnBillingName.Value + ",<br/>" + hdnBillingAddres.Value);
           }
           else if (hdnBillingID.Value == "119" && hdnDeliveryID.Value != "119")
           {
               htmlCode = htmlCode.Replace("[ShipTo]", "THE KALGIDHAR TRUST,<br/>"  + hdnDeliveryAddress.Value);
               htmlCode = htmlCode.Replace("[DELIVERYADDRESS]", "THE KALGIDHAR TRUST,<br/>" + hdnBillingName.Value + ",<br/>" + hdnBillingAddres.Value);
           }
           else if (hdnBillingID.Value == "119" && hdnDeliveryID.Value == "119")
           {
               htmlCode = htmlCode.Replace("[ShipTo]", "THE KALGIDHAR TRUST,<br/>" + hdnDeliveryAddress.Value);
               htmlCode = htmlCode.Replace("[DELIVERYADDRESS]", "THE KALGIDHAR TRUST,<br/>" + hdnBillingAddres.Value);
           }
           else
           {
               htmlCode = htmlCode.Replace("[ShipTo]", "THE KALGIDHAR TRUST,<br/>" + hdnTrustName.Value + ",<br/>" + hdnDeliveryAddress.Value);
               htmlCode = htmlCode.Replace("[DELIVERYADDRESS]", "THE KALGIDHAR TRUST,<br/>" + hdnBillingName.Value + ",<br/>" + hdnBillingAddres.Value);
           }
            htmlCode = htmlCode.Replace("[src]", Server.MapPath("img") + "/Logo_Small.png");
            htmlCode = htmlCode.Replace("[POheader]", "THE KALGIDHAR TRUST");
            htmlCode = htmlCode.Replace("[status]", "Trust");
        }
        else
        {
            if (hdnDeliveryID.Value == "119" && hdnBillingID.Value != "119")
            {
                htmlCode = htmlCode.Replace("[ShipTo]", "GURUDWARA BARUSAHIB,<br/>"  + hdnDeliveryAddress.Value);
                htmlCode = htmlCode.Replace("[DELIVERYADDRESS]", "GURUDWARA BARUSAHIB,<br/>" + hdnBillingName.Value + ",<br/>" + hdnBillingAddres.Value);
            }
            else if (hdnBillingID.Value == "119" && hdnDeliveryID.Value != "119")
            {
                htmlCode = htmlCode.Replace("[ShipTo]", "GURUDWARA BARUSAHIB,<br/>" + hdnTrustName.Value + ",<br/>" + hdnDeliveryAddress.Value);
                htmlCode = htmlCode.Replace("[DELIVERYADDRESS]", "GURUDWARA BARUSAHIB,<br/>"  + hdnBillingAddres.Value);
            }
            else if (hdnBillingID.Value == "119" && hdnDeliveryID.Value == "119")
            {
                htmlCode = htmlCode.Replace("[ShipTo]", "GURUDWARA BARUSAHIB,<br/>" + hdnDeliveryAddress.Value);
                htmlCode = htmlCode.Replace("[DELIVERYADDRESS]", "GURUDWARA BARUSAHIB,<br/>" + hdnBillingAddres.Value);
            }
            else
            {
                htmlCode = htmlCode.Replace("[ShipTo]", "GURUDWARA BARUSAHIB,<br/>" + hdnTrustName.Value + ",<br/>" + hdnDeliveryAddress.Value);
                htmlCode = htmlCode.Replace("[DELIVERYADDRESS]", "GURUDWARA BARUSAHIB,<br/>" + hdnBillingName.Value + ",<br/>" + hdnBillingAddres.Value);
            }
            htmlCode = htmlCode.Replace("[src]", Server.MapPath("img") + "/Logo_Small.png");
            htmlCode = htmlCode.Replace("[POheader]", "GURUDWARA BARUSAHIB");
            htmlCode = htmlCode.Replace("[status]", "Gurudwara Barusahib");
        }

        htmlCode = htmlCode.Replace("[ContactPerson]", txtcontact.Text);
       
        DateTime date = Convert.ToDateTime(txtDate.Text.Trim());
        string dateString = date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        htmlCode = htmlCode.Replace("[DATE]", dateString);
        htmlCode = htmlCode.Replace("[PaymentMode]", txtPaymentMode.Text);
        htmlCode = htmlCode.Replace("[Freight]", hdnFreight.Value);
        htmlCode = htmlCode.Replace("[FrieghtCartage]", txtFrieght.Text);
        htmlCode = htmlCode.Replace("[Loading]", txtLoading.Text);
        htmlCode = htmlCode.Replace("[CompletedBy]", txtCompleted.Text);
        htmlCode = htmlCode.Replace("[Grid]", getGrid());
        htmlCode = htmlCode.Replace("[EstimateNo]", hdnIndentNo.Value);
        htmlCode = htmlCode.Replace("[GST]", hdnVatStatus.Value);
        htmlCode = htmlCode.Replace("[RefernceNo]", txtRefernceNo.Text);
        htmlCode = htmlCode.Replace("[PO]", hdnPoNum.Value);
        htmlCode = htmlCode.Replace("[SubTotal]", hdnTotalPrice.Value);
        decimal grandSum = Convert.ToDecimal(txtFrieght.Text) + Convert.ToDecimal(txtLoading.Text);
        grandSum = grandSum + Convert.ToDecimal(hdnTotalPrice.Value);
        htmlCode = htmlCode.Replace("[GrandTotal]", grandSum.ToString());
        pnlHtml.InnerHtml = htmlCode;

        hdnIndentNo.Value = hdnIndentNo.Value.Replace(',', '_');

        var fileName = "PurchaseOrder_" + hdnPoNum.Value.Trim() + ".pdf";

        string pathToSave = Server.MapPath("Bills") + "//PurchaseOrder";

        Utility.GeneratePDF(htmlCode, fileName, pathToSave);
    }

    protected void btnpdf_Click(object sender, EventArgs e)
    {
        getHTML();
    }

    public string getGrid()
    {
        PurchaseRepository repo = new PurchaseRepository(new AkalAcademy.DataContext());
        PONumber ponum = new PONumber();
        PurchaseOrderDetail po = new PurchaseOrderDetail();
        DataTable dt = new DataTable();
        DataTable dtPONum = new DataTable();
        if (hdnItemsLength.Value != "")
        {
            dt = DAL.DalAccessUtility.GetDataInDataSet("Select M.MatName,M.MatCost,EMR.EstID,EMR.Sno,EMR.Qty,EMR.Rate,EMR.MatID,U.UnitName,M.GST  from EstimateAndMaterialOthersRelations EMR INNER JOIN Material M  on M.MatId = EMR.MatId INNER JOIN Unit U  on U.UnitId = EMR.UnitId where Sno in (" + hdnItemsLength.Value + ")").Tables[0];
        }
        else
        {
            dt = DAL.DalAccessUtility.GetDataInDataSet(" Select M.EstID,M.MatID,M.SnoID,Ma.MatName,Ma.MatCost,Ma.MRP,M.GST from PurchaseOrderDetail M  INNER JOIN [dbo].[PONumber] P ON P.ID = M.[PONumberID]   INNER JOIN Material Ma  on Ma.MatId = M.MatId where P.PurchaseOrderNumber ='" + txtPO.Text + "'").Tables[0];
        }
        DataTable dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct ID,PurchaseOrderNumber from PONumber where PurchaseOrderNumber='" + hdnUpdatePoID.Value + "'").Tables[0];
        if (dsExist.Rows.Count > 0)
        {
            DAL.DalAccessUtility.ExecuteNonQuery("Delete From PurchaseOrderDetail where PONumberID='" + dsExist.Rows[0]["ID"].ToString() + "'");
            hdnPoNum.Value = dsExist.Rows[0]["PurchaseOrderNumber"].ToString();
        }
        else
        {
            if (drpPOFor.SelectedValue == "1")
            {
                dtPONum = DAL.DalAccessUtility.GetDataInDataSet("Select PurchaseOrderNumber from PONumber where [PurchaseOrderNumber] like '%KS%' order by ID desc").Tables[0];
            }
            else if (drpPOFor.SelectedValue == "2")
            {
                dtPONum = DAL.DalAccessUtility.GetDataInDataSet("Select PurchaseOrderNumber from PONumber where [PurchaseOrderNumber] like '%KT%' order by ID desc").Tables[0];
            }
            else
            {
                dtPONum = DAL.DalAccessUtility.GetDataInDataSet("Select PurchaseOrderNumber from PONumber where [PurchaseOrderNumber] like '%GB%' order by ID desc").Tables[0];
            }
            int pono = 0;
            string newPONo = string.Empty;
            string[] tokens;
            if (dtPONum.Rows.Count > 0)
            {
                string d = dtPONum.Rows[0]["PurchaseOrderNumber"].ToString();
                if (drpPOFor.SelectedValue == "1")
                {
                    tokens = d.Split('S');
                    pono = Convert.ToInt32(tokens[1]) + 1;
                    newPONo = "KS" + pono;
                }
                else if (drpPOFor.SelectedValue == "2")
                {
                    tokens = d.Split('T');
                    pono = Convert.ToInt32(tokens[1]) + 1;
                    newPONo = "KT" + pono;
                }
                else
                {
                    tokens = d.Split('B');
                    pono = Convert.ToInt32(tokens[1]) + 1;
                    newPONo = "GB" + pono;
                }
            }
            else
            {
                if (drpPOFor.SelectedValue == "1")
                {
                    newPONo = "KS1";
                }
                else if (drpPOFor.SelectedValue == "2")
                {
                    newPONo = "KT1";
                }
                else
                {
                    newPONo = "GB1";
                }
            }
            ponum.PurchaseOrderNumber = newPONo.ToString();
            repo.SavePONumber(ponum);
            hdnPoNum.Value = newPONo.ToString();
            hdnPoID.Value = ponum.ID.ToString();
        }
        string MaterialInfo = string.Empty;
        MaterialInfo += "<table style='width:100%' border='1' cellspacing='0' cellpadding='0'>";
        MaterialInfo += "<thead>";
        MaterialInfo += "<tr>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;font-size: 14px;font-family: Arial;'>Sr.No</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;font-size: 14px;font-family: Arial;'>DESCRIPTION</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;font-size: 14px;font-family: Arial;'>DETAIL</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;font-size: 14px;font-family: Arial;'>UNIT</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;font-size: 14px;font-family: Arial;'>QTY</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;font-size: 14px;font-family: Arial;'>PRICE</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;font-size: 14px;font-family: Arial;'>GST</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;font-size: 14px;font-family: Arial;'>NET PRICE</th>";
        MaterialInfo += "<th style='width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;font-size: 14px;font-family: Arial;'>LINE TOTAL</th>";
        MaterialInfo += "</tr>";
        MaterialInfo += "</thead>";
        MaterialInfo += "<tbody>";
        hdnIndentNo.Value = string.Empty;
        hdnVatStatus.Value = string.Empty;
        decimal sum = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string vat = string.Empty;
            DataTable dtmatID = new DataTable();
            string description = Request.Form["txtdescription" + i];
            string qty = Request.Form["txtQty" + i];
            if (vat != "")
            {
                vat = Request.Form["txtvat" + i];
            }
            else
            {
                vat = Request.Form["drpGst" + i];
            }
            string mat = Request.Form["txtMatName" + i];
            string unit = Request.Form["txtUnitName" + i];
            string cost = Request.Form["txtMatCost" + i];
            string sno = Request.Form["txtSno" + i];
            string mrp = Request.Form["txtMRP" + i];
            string matid = Request.Form["txtMatID" + i];
            string discount = Request.Form["txtDiscount" + i];
            string aditionaldiscount = Request.Form["txtAdditionalDiscount" + i];
            decimal SubTotal = 0;
            if (hdnItemsLength.Value != "")
            {
                dtmatID = DAL.DalAccessUtility.GetDataInDataSet("Select GST,MatName,MatCost From Material Where MatID ='" + matid + "'").Tables[0];
            }

            MaterialInfo += "<tr>";
            MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + (i + 1) + "</td>";
            MaterialInfo += "<td style='width: 30px; text-align: center; vertical-align: middle;font-size: 12px;'>" + description + "</td>";
            if (mat != null)
            {
                MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;font-size: 12px;'>" + mat + "</td>";
            }
            else
            {
                if (hdnItemsLength.Value != "")
                {
                    MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;font-size: 12px;'>" + dtmatID.Rows[0]["MatName"].ToString() + "</td>";
                }
                else
                {
                    MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;font-size: 12px;'>" + dt.Rows[i]["MatName"].ToString() + "</td>";

                }
            }
            MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + unit + "</td>";
            MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + qty + "</td>";
            if (mrp != "0")
            {
                decimal discountTotal = Convert.ToDecimal(mrp) * (Convert.ToDecimal(discount) / 100);
                discountTotal = Convert.ToDecimal(mrp) - discountTotal;
                decimal TotaladitionlDisunt = discountTotal * (Convert.ToDecimal(aditionaldiscount) / 100);
                discountTotal = Convert.ToDecimal(discountTotal) - TotaladitionlDisunt;
                MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + discountTotal + "</td>";
                SubTotal = discountTotal;
            }
            else
            {
                decimal discountTotal = Convert.ToDecimal(dt.Rows[i]["MatCost"].ToString()) * (Convert.ToDecimal(discount) / 100);
                discountTotal = Convert.ToDecimal(dt.Rows[i]["MatCost"].ToString()) - discountTotal;
                decimal TotaladitionlDisunt = discountTotal * (Convert.ToDecimal(aditionaldiscount) / 100);
                discountTotal = Convert.ToDecimal(discountTotal) - TotaladitionlDisunt;
                MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + discountTotal + "</td>";
                SubTotal = discountTotal;
            }
            if (vat != null)
            {
                MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + vat + "</td>";
            }
            else
            {
                if (hdnItemsLength.Value != "")
                {
                    MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + dtmatID.Rows[0]["GST"].ToString() + "</td>";
                }
                else
                {
                    MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + dt.Rows[i]["GST"].ToString() + "</td>";
                }
            }

            SubTotal = Math.Round(SubTotal, 2);
            if (vat == "0")
            {
                if (mrp != "0")
                {
                    decimal discountTotal = Convert.ToDecimal(mrp) * (Convert.ToDecimal(discount) / 100);
                    discountTotal = Convert.ToDecimal(mrp) - discountTotal;
                    decimal TotaladitionlDisunt = discountTotal * (Convert.ToDecimal(aditionaldiscount) / 100);
                    discountTotal = Convert.ToDecimal(discountTotal) - TotaladitionlDisunt;
                    MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + discountTotal + "</td>";
                    SubTotal = discountTotal * Convert.ToDecimal(qty);
                }
                else
                {
                    decimal discountTotal = Convert.ToDecimal(dt.Rows[i]["MatCost"].ToString()) * (Convert.ToDecimal(discount) / 100);
                    discountTotal = Convert.ToDecimal(dt.Rows[i]["MatCost"].ToString()) - discountTotal;
                    decimal TotaladitionlDisunt = discountTotal * (Convert.ToDecimal(aditionaldiscount) / 100);
                    discountTotal = Convert.ToDecimal(discountTotal) - TotaladitionlDisunt;
                    MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + discountTotal + "</td>";
                    SubTotal = discountTotal * Convert.ToDecimal(qty);
                }
                sum += SubTotal;
                hdnTotalPrice.Value = Math.Round(sum, 2).ToString();
                MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + SubTotal + "</td>";
            }
            else
            {
                decimal totalAmount = 0;
                decimal toatlNetAmount = 0;

                if (hdnItemsLength.Value != "")
                {
                    decimal vatTotal = 0;
                    if (vat != null)
                    {
                        vatTotal = Convert.ToDecimal(SubTotal) * Convert.ToDecimal(vat) / 100;
                    }
                    else
                    {
                        vatTotal = Convert.ToDecimal(SubTotal) * Convert.ToDecimal(dtmatID.Rows[0]["GST"].ToString()) / 100;
                    }
                    totalAmount = Convert.ToDecimal(SubTotal) + Convert.ToDecimal(vatTotal);
                    toatlNetAmount = totalAmount * Convert.ToDecimal(qty);
                    totalAmount = Math.Round(totalAmount, 2);
                    toatlNetAmount = Math.Round(toatlNetAmount, 2);
                }
                else
                {
                    var vatTotal = Convert.ToDecimal(SubTotal) * Convert.ToDecimal(dt.Rows[i]["GST"].ToString()) / 100;
                    totalAmount = Convert.ToDecimal(SubTotal) + Convert.ToDecimal(vatTotal);
                    toatlNetAmount = totalAmount * Convert.ToDecimal(qty);
                    totalAmount = Math.Round(totalAmount, 2);
                    toatlNetAmount = Math.Round(toatlNetAmount, 2);

                }
                sum += toatlNetAmount;
                hdnTotalPrice.Value = Math.Round(sum, 2).ToString();
                MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + totalAmount + "</td>";
                MaterialInfo += "<td style='width: 10px; text-align: center; vertical-align: middle;'>" + toatlNetAmount + "</td>";
            }
            MaterialInfo += "</tr>";
            if (!hdnIndentNo.Value.Contains(dt.Rows[i]["EstID"].ToString()))
            {
                hdnIndentNo.Value += dt.Rows[i]["EstID"].ToString() + ",";
            }
            if (vat != null)
            {
                if (!hdnVatStatus.Value.Contains(vat))
                {
                    hdnVatStatus.Value += vat + "%" + ",";
                }
            }
            else
            {
                if (hdnItemsLength.Value != "")
                {
                    if (!hdnVatStatus.Value.Contains(dtmatID.Rows[0]["GST"].ToString()))
                    {
                        hdnVatStatus.Value += dtmatID.Rows[0]["GST"].ToString() + "%" + ",";
                    }
                }
                else
                {
                    if (!hdnVatStatus.Value.Contains(dt.Rows[i]["GST"].ToString()))
                    {
                        hdnVatStatus.Value += dt.Rows[i]["GST"].ToString() + "%" + ",";
                    }
                }
            }
            po.CreatedOn = Utility.GetLocalDateTime(System.DateTime.UtcNow);
            if (dsExist.Rows.Count > 0)
            {
                po.PONumberID = Convert.ToInt32(dsExist.Rows[0]["ID"].ToString());
            }
            else
            {
                po.PONumberID = Convert.ToInt32(hdnPoID.Value);
            }
            po.VendorID = Convert.ToInt32(hdnVendorID.Value);
            po.EstID = Convert.ToInt32(dt.Rows[i]["EstID"].ToString());
            if (hdnItemsLength.Value != "")
            {
                po.MatID = Convert.ToInt32(matid);
            }
            else
            {
                po.MatID = Convert.ToInt32(dt.Rows[i]["MatID"].ToString());
            }

            po.Qty = Convert.ToDecimal(qty);
            if (cost != null)
            {
                po.Rate = Convert.ToDecimal(cost);
            }
            else
            {
                if (hdnItemsLength.Value != "")
                {
                    po.Rate = Convert.ToDecimal(dtmatID.Rows[0]["MatCost"].ToString());
                }
                else
                {
                    po.Rate = Convert.ToDecimal(dt.Rows[i]["MatCost"].ToString());
                }
            }
            po.Description = Request.Form["txtdescription" + i];
            if (vat != null)
            {
                po.GST = Convert.ToDecimal(vat);
            }
            else
            {
                if (hdnItemsLength.Value != "")
                {
                    po.GST = Convert.ToDecimal(dtmatID.Rows[0]["GST"].ToString());
                }
                else
                {
                    po.GST = Convert.ToDecimal(dt.Rows[i]["GST"].ToString());
                }
            }
            po.UnitName = unit;
            po.FrieghtCharges = Convert.ToDecimal(txtFrieght.Text);
            po.LoadingCharges = Convert.ToDecimal(txtLoading.Text);
            if (sno != null)
            {
                po.SnoID = Convert.ToInt32(sno);
            }
            else
            {
                po.SnoID = Convert.ToInt32(dt.Rows[i]["SnoID"].ToString());
            }
            repo.AddNewPODetail(po);
        }
        MaterialInfo += "</tbody>";
        MaterialInfo += "</table>";

        if (hdnIndentNo.Value != "")
        {
            hdnIndentNo.Value = hdnIndentNo.Value.Substring(0, hdnIndentNo.Value.Length - 1);
        }

        return MaterialInfo;
    }

    protected void btnSaveExit_Click(object sender, EventArgs e)
    {
        getGrid();
    }
}