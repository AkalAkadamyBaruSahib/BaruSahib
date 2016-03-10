using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Purchase_MaterialSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        getSearchMaterialDetail();
    }

    private void getSearchMaterialDetail()
    {
        DataSet dsMaterialDetails = new DataSet();
        dsMaterialDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_PurchaseMaterialSearch '" + txtMatName.Text + "'");
        divMaterialDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Material List</h2>";
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
        ZoneInfo += "<th width='10%'>Estimate No</th>";
        ZoneInfo += "<th width='10%'>Mateial Name</th>";
        ZoneInfo += "<th width='10%'>Company Rate</th>";
        ZoneInfo += "<th width='13%'>Vendor Name</th>";
        ZoneInfo += "<th width='13%'>Date</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsMaterialDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='10%'>" + dsMaterialDetails.Tables[0].Rows[i]["EstId"].ToString() + "</td>";
            ZoneInfo += "<td width='10%'>" + dsMaterialDetails.Tables[0].Rows[i]["EmployeeName"].ToString() + "</td>";
            ZoneInfo += "<td width='10%'>" + dsMaterialDetails.Tables[0].Rows[i]["MatName"].ToString() + "</td>";
            ZoneInfo += "<td width='10%'>" + dsMaterialDetails.Tables[0].Rows[i]["Rate"].ToString() + "</td>";
            ZoneInfo += "<td width='10%'>" + dsMaterialDetails.Tables[0].Rows[i]["DispatchDate"].ToString() + "</td>";

            ZoneInfo += "<td class='center' width='13%' align='center'>";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        divMaterialDetails.InnerHtml = ZoneInfo.ToString();
    }
}