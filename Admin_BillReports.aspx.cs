using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_BillReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindZone();
        }
    }

    protected void BindZone()
    {
        DataSet dsZone = new DataSet();

        dsZone = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId,ZoneName  from Zone where Active=1");

        ddlZone.DataSource = dsZone;
        ddlZone.DataValueField = "ZoneId";
        ddlZone.DataTextField = "ZoneName";
        ddlZone.DataBind();
        ddlZone.Items.Insert(0, "Select Zone");
        ddlZone.SelectedIndex = 0;
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAcademy();
    }

    protected void BindAcademy()
    {
        DataSet dsAca = new DataSet();
        dsAca = DAL.DalAccessUtility.GetDataInDataSet("select AcaId,AcaName from Academy where Active=1 and ZoneId='" + ddlZone.SelectedValue + "'");
        ddlAcademy.DataSource = dsAca;
        ddlAcademy.DataValueField = "AcaId";
        ddlAcademy.DataTextField = "AcaName";
        ddlAcademy.DataBind();
        ddlAcademy.Items.Insert(0, "Select Academy");
        ddlAcademy.SelectedIndex = 0;
    }

    protected DataTable BindDatatable()
    {
        DataTable dt = new DataTable();
        dt = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_GetApprocedBillDetails] " + ddlAcademy.SelectedValue + "'" + txtfirstDate.Text + "','" + txtlastDate.Text + "'").Tables[0];
        return dt;
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        MaterialDetailByWorkAllotIDInPDF();
    }

    protected void MaterialDetailByWorkAllotIDInPDF()
    {
        string[] columnname = new string[] { "SubBillId", "BillNo", "BillDate", "AgencyName", "BillType", "TotalAmount" };

        string[] columnWidths = new string[] { "10", "10", "20", "20", "20", "20" };

        DataTable dsBills = new DataTable();

        string pdfhtml = string.Empty;

        dsBills = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_GetApprocedBillDetails] " + ddlAcademy.SelectedValue + ", '" + txtfirstDate.Text + "','" + txtlastDate.Text + "'").Tables[0];

        decimal totalAmount = 0;
       
        if (dsBills != null && dsBills.Rows.Count > 0)
        {
            pdfhtml = Utility.getPDFHTML(6, columnname, dsBills.Rows.Count, "Approved Bill Details of " + ddlAcademy.SelectedItem.Text, columnWidths, true);
            string pattern = string.Empty;
            string replace = string.Empty;
            for (int i = 0; i < dsBills.Rows.Count; i++)
            {
                replace = dsBills.Rows[i]["SubBillId"].ToString();
                pattern = columnname[0].Substring(0, 4) + i + columnname[0].Substring(4);
                pdfhtml = Regex.Replace(pdfhtml, pattern, replace);

                replace = dsBills.Rows[i]["VendorBillNumber"].ToString();
                pattern = columnname[1].Substring(0, 4) + i + columnname[1].Substring(4);
                pdfhtml = Regex.Replace(pdfhtml, pattern, replace);

                replace = dsBills.Rows[i]["BillDate"].ToString();
                pattern = columnname[2].Substring(0, 4) + i + columnname[2].Substring(4);
                pdfhtml = Regex.Replace(pdfhtml, pattern, replace);

                replace = dsBills.Rows[i]["AgencyName"].ToString();
                pattern = columnname[3].Substring(0, 4) + i + columnname[3].Substring(4);
                pdfhtml = Regex.Replace(pdfhtml, pattern, replace);


                replace = dsBills.Rows[i]["BillType"].ToString();
                pattern = columnname[4].Substring(0, 4) + i + columnname[4].Substring(4);
                pdfhtml = Regex.Replace(pdfhtml, pattern, replace);

                replace = dsBills.Rows[i]["TotalAmount"].ToString();
                pattern = columnname[5].Substring(0, 4) + i + columnname[5].Substring(4);
                pdfhtml = Regex.Replace(pdfhtml, pattern, replace);


                if (!string.IsNullOrEmpty(dsBills.Rows[i]["TotalAmount"].ToString()))
                {
                    totalAmount += Convert.ToDecimal(dsBills.Rows[i]["TotalAmount"].ToString());
                }
            }

            pdfhtml = pdfhtml.Replace("[Total]", totalAmount.ToString());
        }

        string fileName = "Material_Details_By_WorlAllot_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".pdf";
        Utility.GeneratePDF(pdfhtml, fileName, string.Empty);
    }
}