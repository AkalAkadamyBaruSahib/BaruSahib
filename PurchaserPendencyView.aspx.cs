using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PurchaserPendencyView : System.Web.UI.Page
{
    public int inchargeId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["inchargeId"] != null)
            {
                inchargeId = Convert.ToInt32(Request.QueryString["inchargeId"].ToString());
                PurchaserPendency(inchargeId);
            }
        }
    }
    public void PurchaserPendency(int inchargeId)
    {
        DataSet PendingEst = new DataSet();
        PendingEst = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_PurchaserPendencyView]'" + (int)TypeEnum.PurchaseSourceID.Mohali + "','" + inchargeId + "'");
        divPurchaserPendencyView.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        lblPurchaserName.Text = PendingEst.Tables[0].Rows[0]["PurchaserName"].ToString();
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='5%'><b>Sr.No.</b></th>";
        ZoneInfo += "<th width='20%'>EstID</th>";
        ZoneInfo += "<th width='2%'>Material Name</th>";
        ZoneInfo += "<th width='2%'>Quantity</th>";
        ZoneInfo += "<th width='5%'>Unit</th>";
        ZoneInfo += "<th width='5%'>Assigned Date</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";

        for (int i = 0; i < PendingEst.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td class='center' width='5%'>" + (i + 1) + "</td>";
            ZoneInfo += "<td class='center' width='10%'>" + PendingEst.Tables[0].Rows[i]["EstimateNumber"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='20%'>" + PendingEst.Tables[0].Rows[i]["MaterialName"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='10%'>" + PendingEst.Tables[0].Rows[i]["EstimateQuantity"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='5%'>" + PendingEst.Tables[0].Rows[i]["UnitName"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='20%'>" + PendingEst.Tables[0].Rows[i]["EmployeeAssignDateTime"].ToString() + "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
   
        divPurchaserPendencyView.InnerHtml = ZoneInfo.ToString();
    }

}