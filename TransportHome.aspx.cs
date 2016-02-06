using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class TransportHome : System.Web.UI.Page
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

            BindZoneDetails();
        }
    }
   
    protected void BindZoneDetails()
    {
        int UsrTypeID = int.Parse(Session["UserTypeID"].ToString());

        DataSet dsZoneDetails = new DataSet();
        if (UsrTypeID == 13)
        {
            dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowZoneDetails_ByUser '" + lblUser.Text + "'");
        }
        else
        {
            dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_ZoneAndUserDetails] '" + lblUser.Text + "'");
        }

        divZone.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-bordered table-striped table-condensed'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='10%'>Zone Code</th>";
        ZoneInfo += "<th width='20%'>Zone Name</th>";
        ZoneInfo += "<th width='35%'>Location</th>";
        //ZoneInfo += "<th width='25%'>Zone Assigned To</th>";
        ZoneInfo += "<th width='10%'>Total Nos. of Academy</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsZoneDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='10%' class='center'>" + dsZoneDetails.Tables[0].Rows[i]["ZoId"].ToString() + "</td>";
            //Session["ZoneId"] = dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString();
            ZoneInfo += "<td width='20%' class='center'><a href='Transport_ZoneDetails.aspx?ZoneId=" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'>" + dsZoneDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</a></td>";
            ZoneInfo += "<td width='35%' class='center'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td> <b>State:</b> " + dsZoneDetails.Tables[0].Rows[i]["StateName"].ToString() + " </td></tr>";
            ZoneInfo += "<tr><td> <b>City:</b> " + dsZoneDetails.Tables[0].Rows[i]["CityName"].ToString() + "(" + dsZoneDetails.Tables[0].Rows[i]["Pincode"].ToString() + ")</td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            DataSet dsAcaCount = new DataSet();
            
            dsAcaCount = DAL.DalAccessUtility.GetDataInDataSet("select COUNT(*) as Coun from [TransportZoneAcademyRelation] where ZoneId='" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'");
            ZoneInfo += "<td width='10%' class='center'>" + dsAcaCount.Tables[0].Rows[0]["Coun"].ToString() + "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divZone.InnerHtml = ZoneInfo.ToString();
        //lblZone.Text = dsZoneDetails.Tables[0].Rows[0]["ZoneName"].ToString();
    }


    //protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindEmployee();
    //}
    //protected void ddlEmpl_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DataSet dsDesig = new DataSet();
    //    dsDesig = DAL.DalAccessUtility.GetDataInDataSet("SELECT Designation.Designation FROM Incharge INNER JOIN Designation ON Incharge.DesigId = Designation.DesgId where InchargeId='" + ddlEmpl.SelectedValue + "'");
    //    lblDesignation.Visible = true;
    //    lblDesignation.Text = dsDesig.Tables[0].Rows[0]["Designation"].ToString();
    //}
    //protected void btnSave_Click(object sender, EventArgs e)
    //{

    //    string zoId = Session["ZoneId"].ToString();
    //     DataSet dsExist = new DataSet();
    //     dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct ZoneId,AcaId from AcademyAssignToEmployee where EmpId='" + ddlEmpl.SelectedValue +"' and ZoneId='"+ zoId +"'");
    //    if (dsExist.Tables[0].Rows.Count > 0)
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Incharge already assigned to Zone');", true);
    //    }
    //    else
    //    {
    //        if (ddlDept.SelectedIndex == 0)
    //        {
    //            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Department.');", true);
    //        }
    //        if (ddlEmpl.SelectedIndex == 0)
    //        {
    //            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Employee.');", true);
    //        }

    //        else
    //        {
    //            DAL.DalAccessUtility.ExecuteNonQuery("insert into AcademyAssignToEmployee(ZoneId,AcaId,EmpId,Active,CreatedBy,CreatedOn) values ('" + zoId + "','','" + ddlEmpl.SelectedValue + "','1','" + lblUser.Text + "','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "') ");
    //            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Incharge Location assign Successfully.');", true);
    //            BindZoneDetails();

    //        }
    //    }
    //}
}