using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Emp_MaterialView : System.Web.UI.Page
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
                lblUser.Text = Session["EmailId"].ToString();
            }
            //if (Session["AcaId"] == null)
            //{
            //    Response.Redirect("Emp_Home.aspx");
            //}
            // else
            //{
            //    string a;
            //    a=Session["AcaId"].ToString();
            //}
            if (Request.QueryString["AcaId"] == null)
            {
                Response.Redirect("Emp_Home.aspx");
            }
            else
            {
                string a;
                a = Request.QueryString["AcaId"].ToString();
                Session["AcaId1"] = a.ToString();
            }
           
            
            getAcaDetails();
        }
    }
    private void getAcaDetails()
    {
        DataSet dsAcaDetails = new DataSet();
        dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("SELECT Material.MatId AS MaId, Material.Active AS Expr5, Material.MatName, MaterialType.MatTypeName FROM Material INNER JOIN MaterialType ON Material.MatTypeId = MaterialType.MatTypeId where Material.Active=1");
        divAcademyDetails.InnerHtml = string.Empty;
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
        ZoneInfo += "<th width='20%'>Material Code</th>";
        ZoneInfo += "<th width='30%'>Material Type</th>";
        ZoneInfo += "<th width='50%'>Material Name</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsAcaDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='20%'>" + dsAcaDetails.Tables[0].Rows[i]["MaId"].ToString() + "</td>";
            ZoneInfo += "<td width='30%'>" + dsAcaDetails.Tables[0].Rows[i]["MatTypeName"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='50%' align='center'>";

            ZoneInfo += "<a class='btn btn-info' href='Emp_StockRegister.aspx?MaId=" + dsAcaDetails.Tables[0].Rows[i]["MaId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i>" + dsAcaDetails.Tables[0].Rows[i]["MatName"].ToString() + "";
            ZoneInfo += "</a>  ";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        ZoneInfo += "</div>";
        ZoneInfo += "</div>";
        divAcademyDetails.InnerHtml = ZoneInfo.ToString();
        
    }
}