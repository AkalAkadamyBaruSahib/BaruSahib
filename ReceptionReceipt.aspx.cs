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


public partial class ReceptionReceipt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    
    {
        if (!IsPostBack)
        {

            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                hdnInchargeID.Value = Session["InchargeID"].ToString();
                lblCurrentDate.Text = DateTime.Now.ToShortDateString();
            }
        }
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

    protected void btnGenerateReceipt_Click(object sender, EventArgs e)
    {
        int UserId = Convert.ToInt32(Session["InchargeID"].ToString());
        VisitorReceipt vr = new VisitorReceipt();
        vr.ReceivedAmount = Convert.ToDecimal(txtSumofRupees.Text);
        vr.ReceivedFrom = txtReceived.Text;
        vr.PaymentMode = int.Parse(ddlCashType.SelectedValue);
        if (txtNumber.Text != "" && txtNumber.Text != null)
        {
            vr.ChequeDDNumber = txtNumber.Text;
        }
        vr.ReceiptPath = "Bills/VisitorReceipt/" + txtReceived.Text + ".pdf";
        vr.Createdby = UserId;
        vr.CreatedOn = DateTime.Now;
        VisitorUserRepository repo = new VisitorUserRepository(new AkalAcademy.DataContext());
        if (vr.ID == 0)
        {
            repo.AddNewReceipt(vr);
            hdnRecepitNumber.Value = vr.ID.ToString();
        }
        getHTML();
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
            htmlCode = client.DownloadString(hostName + "/ReceptionReceipt.html");
        }
        htmlCode = htmlCode.Replace("[No]", hdnRecepitNumber.Value);
        htmlCode = htmlCode.Replace("[CurntDate]", DateTime.Now.ToString("dd/MM/yyyy"));
        htmlCode = htmlCode.Replace("[Received]", txtReceived.Text);
        htmlCode = htmlCode.Replace("[Rupees]", txtSumofRupees.Text);
        htmlCode = htmlCode.Replace("[Cash]", txtNumber.Text == string.Empty ? "Cash" : txtNumber.Text);
        htmlCode = htmlCode.Replace("[Account]", "Kalgidhar Trust");
        htmlCode = htmlCode.Replace("[SignatureSuprevisor]", String.Empty);

        pnlHtml.InnerHtml = htmlCode;
        string folderPath = Server.MapPath("Bills/VisitorReceipt/");

        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + txtReceived.Text + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        BaseFont f_cb = BaseFont.CreateFont("c:\\windows\\fonts\\calibrib.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        pnlHtml.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 50, 50, 30, 30);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);

        Response.End();
       //Utility.GeneratePDF(htmlCode, (txtReceived.Text + ".pdf"), folderPath + "");
    }
}