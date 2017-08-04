using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_BodyMaterialSearch : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string name = Request.Form["txtMaterial"];
        DataTable dsMaterialDetails = new DataTable();
        List<MaterialsDTO> MaterialView = new List<MaterialsDTO>();
        PurchaseRepository purchaseRepo = new PurchaseRepository(new AkalAcademy.DataContext());
        MaterialView = purchaseRepo.GetBindMaterialByMaterialName(name.Trim());
        divMaterialDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Material Detail</h2>";
        ZoneInfo += "<div class='box-icon'>";
        ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
        ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        ZoneInfo += "<div class='box-content'>";
        ZoneInfo += "<table class='table table-striped table-bordered'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='10%' style='color:#cc3300;'>Material Name</th>";
        ZoneInfo += "<th width='10%' style='color:#cc3300;'>Material Type</th>";
        ZoneInfo += "<th width='10%' style='color:#cc3300;'>Rate</th>";
        ZoneInfo += "<th width='13%' style='color:#cc3300;'>Unit Name</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        if (MaterialView.Count > 0)
        {
            foreach (MaterialsDTO Est in MaterialView)
            {
                ZoneInfo += "<tr>";
                ZoneInfo += "<td width='10%'>" + Est.MatName + "</td>";
                ZoneInfo += "<td width='10%'>" + Est.MaterialType.MatTypeName + "</td>";
                ZoneInfo += "<td width='10%'>" + Est.MatCost + "</td>";
                ZoneInfo += "<td width='10%'>" + Est.Unit.UnitName + "</td>";
                ZoneInfo += "</tr>";
            }
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        divMaterialDetails.InnerHtml = ZoneInfo.ToString();
    }
}