using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class faq : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindFAQs();
        }
    }
    protected void BindFAQs()
    {
        DataSet dsZoneDetails = new DataSet();
        dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet("select FqaId,Question,Answer from FAQ where Active='1'");
        divFaqDetails.InnerHtml = string.Empty;
        string faqInfo = string.Empty;
        for (int i = 0; i < dsZoneDetails.Tables[0].Rows.Count; i++)
        {
            faqInfo += "<div class='span12'>";
            faqInfo += "<h3>" + dsZoneDetails.Tables[0].Rows[i]["Question"].ToString() + "</h3>";
            faqInfo += "<div class='row-fluid'>";
            faqInfo += "<div class='span6'>";
            faqInfo += "<blockquote>";
            faqInfo += "<p>" + dsZoneDetails.Tables[0].Rows[i]["Answer"].ToString() + "</p>";
            faqInfo += "</blockquote>";
            faqInfo += "</div>";
            faqInfo += "</div>";
            faqInfo += "</div>";   
        }
        divFaqDetails.InnerHtml = faqInfo.ToString();
    }
}