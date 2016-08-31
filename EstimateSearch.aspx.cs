using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EstimateSearch : System.Web.UI.Page
{
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
                hdnInchargeID.Value = Session["InchargeID"].ToString();
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetPurchaseMaterialDetail(Convert.ToInt32(txtEstID.Text));
    }

    private void GetPurchaseMaterialDetail(int estID)
    {
        string UserTypeID = Session["UserTypeID"].ToString();
        string UserID = Session["InchargeID"].ToString();
        DataTable dtapproved = new DataTable();

        List<Estimate> PurchaseRegister = new List<Estimate>();
        PurchaseRepository purchaseRepository = new PurchaseRepository(new AkalAcademy.DataContext());

        if (estID != null)
        {
            PurchaseRegister = purchaseRepository.EstimateDetailByEstId(estID,2);
        }

        divMaterialDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        if (PurchaseRegister.Count > 0)
        {
            int Sno = -1;
            ZoneInfo += "<div class='row-fluid sortable'>";
            ZoneInfo += "<div class='box span12'>";
            ZoneInfo += "<div class='box-header well' data-original-title>";
            ZoneInfo += "<h2><i class='icon-user'></i>Estimate Detail</h2>";
            ZoneInfo += "<div class='box-icon'>";
            ZoneInfo += "<a href='#' class='btn btn-minimize btn-round'><i class='icon-chevron-up'></i></a>";
            ZoneInfo += "<a href='#' class='btn btn-close btn-round'><i class='icon-remove'></i></a>";
            ZoneInfo += "</div>";
            ZoneInfo += "</div>";
            ZoneInfo += "<div class='box-content'>";
            ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
            ZoneInfo += "<tbody>";
            foreach (Estimate Est in PurchaseRegister)
            {
                ZoneInfo += "<tr>";
                ZoneInfo += "<td style='display:none;'>1</td>";
                ZoneInfo += "<td>";
                ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
                ZoneInfo += "<tr>";
                ZoneInfo += "<td width='20%'><b style='color:red;'>Estimate No:</b> " + Est.EstId + "<br/><b style='color:red;'>Estimate File:</b> " + GetFileName(Est.FilePath, Est.FileNme) + "</td>";
                ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Sanction Date:</b> " + Est.ModifyOn + "</td>";
                ZoneInfo += "<td class='center' width='25%'><b style='color:red;'>Sub Estimate:</b> " + Est.SubEstimate + "</td>";
                ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Academy:</b> " + Est.Academy.AcaName + "</td>";
                ZoneInfo += "<td class='center' width='20%'><b style='color:red;'>Zone:</b> " + Est.Zone.ZoneName + "</td>";
                ZoneInfo += "<td class='center' width='20%'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + Est.EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a>/<a href='Purchase_ViewEstMaterial.aspx?EstId=" + Est.EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Edit</span></a></td>";
                ZoneInfo += "</tr>";
                ZoneInfo += "</table>";
                ZoneInfo += "<table border='1' class='table table-striped table-bordered bootstrap-datatable datatable'>";
                ZoneInfo += "<tr style='color:Green;'>";
                ZoneInfo += "<th width='5%'><b>Sr. No.</b></th>";
                ZoneInfo += "<th width='20%'>Material Name</th>";
                ZoneInfo += "<th width='2%'>Unit</th>";
                ZoneInfo += "<th width='2%'>Quantity</th>";
                ZoneInfo += "<th width='5%'>Source Type</th>";
                ZoneInfo += "<th width='27%'>Purchase Officer</th>";
                ZoneInfo += "<th width='15%'>Purchase Date</th>";
                ZoneInfo += "<th width='20%'>Remark</th>";
                ZoneInfo += "<th width='0%'>Action</th>";

                ZoneInfo += "</tr>";

                int count = 0;
                if (Est.EstimateAndMaterialOthersRelations != null)
                {
                    foreach (EstimateAndMaterialOthersRelations material in Est.EstimateAndMaterialOthersRelations)
                    {
                        if (material.EstId > 0)
                        {

                            ZoneInfo += "<tr>";
                            ZoneInfo += "<td>" + (count + 1) + "</td>";
                            ZoneInfo += "<td>" + material.Material.MatName + "</td>";
                            ZoneInfo += "<td>" + material.Unit.UnitName + "</td>";
                            ZoneInfo += "<td>" + material.Qty + "</td>";
                            ZoneInfo += "<td>" + material.PurchaseSource.PSName + "</td>";
                            ZoneInfo += "<td class='left'>";
                            ZoneInfo += "<table>";
                            ZoneInfo += "<tr><td> <b>Name:</b> " + material.Incharge.InName + " </td></tr>";
                            if (material.EmployeeAssignDateTime == Convert.ToDateTime("1/1/1900 12:00:00 AM"))
                            {
                                ZoneInfo += "<tr><td><b>Assigned Date:</b> </td></tr>";
                            }
                            else
                            {
                                ZoneInfo += "<tr><td style='color:darkred;'><b>Assigned Date:</b> " + material.EmployeeAssignDateTime + "</td></tr>";
                            }
                            ZoneInfo += "</table>";
                            ZoneInfo += "</td>";
                        }


                        ZoneInfo += "<td>" + material.DispatchDate + "</td>";


                        ZoneInfo += "<td>" + material.remarkByPurchase + "</td>";

                        ZoneInfo += "<td width='30%'><a href='javascript: openModelPopUp(" + Est.EstId + "," + material.Sno + ");'><span class='label label-warning'  style='font-size: 15.998px;'>Reject Item</span></a></td>";


                        ZoneInfo += "</tr>";
                        count++;
                    }
                }

                ZoneInfo += "</tr>";
                ZoneInfo += "</table>";
                ZoneInfo += "</td>";
            }

            ZoneInfo += "</tbody>";
            ZoneInfo += "</table>";
            ZoneInfo += "</div>";
            ZoneInfo += "</div>";
            ZoneInfo += "</div>";
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Estimate Number Not Exit.');", true);
        }

        divMaterialDetails.InnerHtml = ZoneInfo.ToString();
    }
    private string GetFileName(string filepaths, string fileName)
    {
        string anchorLink = string.Empty;
        string[] filePath = filepaths.Split(',');
        int count = 0;
        foreach (string path in filePath)
        {
            count++;
            anchorLink += "<a href='" + path + "' target='_blank'>" + fileName + "_" + count + "</a> , ";
        }

        return anchorLink.Substring(0, anchorLink.Length - 3);
    }
}