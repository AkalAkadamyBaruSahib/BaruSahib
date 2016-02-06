using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Example_InVoice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["BillId"] != null)
            {
                GetBillDetails(Request.QueryString["BillId"].ToString());
            }
        }
    }
    protected void GetBillDetails(string id)
    {
        DataSet dsBill = new DataSet();
        dsBill = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AdminBillViewByBillId_V2 '" + id + "'");
        lblTranId.Text = dsBill.Tables[0].Rows[0]["SubBillId"].ToString();
        lblZone.Text = dsBill.Tables[0].Rows[0]["ZoneName"].ToString();
        lblAca.Text = dsBill.Tables[0].Rows[0]["AcaName"].ToString();
        lblCreatedOn.Text = dsBill.Tables[0].Rows[0]["BillDate"].ToString();
        lblAgency.Text = dsBill.Tables[0].Rows[0]["AgencyName"].ToString();
        lblDesc.Text = dsBill.Tables[0].Rows[0]["BillDescr"].ToString();
        divBillDetails.InnerHtml = string.Empty;
        string BillInfo = string.Empty;
         BillInfo += "<table>";
         BillInfo += "<tr>";
         BillInfo += "<td><strong>Items</strong></td>";
         BillInfo += "<td><strong>Qty</strong></td>";
         BillInfo += "<td><strong>Unit Price</strong></td>";
         BillInfo += "<td><strong>Amount</strong></td>";
         BillInfo += "</tr>";
         for (int i = 0; i < dsBill.Tables[2].Rows.Count; i++)
         {
             BillInfo += "<tr class='odd'>";
             BillInfo += "<td>" + dsBill.Tables[2].Rows[i]["MatName"].ToString() + "</td>";
             BillInfo += "<td>" + dsBill.Tables[2].Rows[i]["Qty"].ToString() + "</td>";
             BillInfo += "<td>" + dsBill.Tables[2].Rows[i]["Rate"].ToString() + "</td>";
             BillInfo += "<td>" + dsBill.Tables[2].Rows[i]["Amount"].ToString() + "</td>";
             BillInfo += "</tr>";
             BillInfo += "<tr>";
         }
         BillInfo += "<td>&nbsp;</td>";
         BillInfo += "<td>&nbsp;</td>";
         BillInfo += "<td><strong>Total</strong></td>";
         BillInfo += "<td><strong>" + dsBill.Tables[0].Rows[0]["TotalAmount"].ToString() +"</strong></td>";
         BillInfo += "</tr>";
         BillInfo += "</table>";
        divBillDetails.InnerHtml = BillInfo.ToString();
    }


}