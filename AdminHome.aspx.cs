using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class AdminHome : System.Web.UI.Page
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
            //BindDepartment();
            //BindEmployee();
            //if (Request.QueryString["ZoneId"] != null)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Incharge Location assign Successfully.');", true);
            //}
            if (Session["EmpId"] != null)
            {
                getZoneID(Session["EmpId"].ToString());
            }
        }
       
    }
    private void getZoneID(string id)
    {
        //DataSet dsZoneName = new DataSet();
        //dsZoneName = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EmpDetails '" + id + "'");
        //if (dsZoneName.Tables[0].Rows.Count > 0)
        //{
        //    lblZone.Text = dsZoneName.Tables[0].Rows[0]["InName"].ToString();
        //    lblDept.Text = dsZoneName.Tables[0].Rows[0]["department"].ToString();
        //    lblDesig.Text = dsZoneName.Tables[0].Rows[0]["Designation"].ToString();
        //    lblMob.Text = dsZoneName.Tables[0].Rows[0]["InMobile"].ToString();

        //}
        //BindZoneDetails();
    }
    //protected void BindDepartment()
    //{
    //    DataSet dsDept = new DataSet();
    //    dsDept = DAL.DalAccessUtility.GetDataInDataSet("select DepId,department from Department where Active=1");
    //    ddlDept.DataSource = dsDept;
    //    ddlDept.DataValueField = "DepId";
    //    ddlDept.DataTextField = "department";
    //    ddlDept.DataBind();
    //    ddlDept.Items.Insert(0, "Select");
    //    ddlDept.SelectedIndex = 0;
    //}
    //protected void BindEmployee()
    //{
    //    DataSet dsDept = new DataSet();
    //    dsDept = DAL.DalAccessUtility.GetDataInDataSet("select InchargeId,InName from Incharge where DepId='" + ddlDept.SelectedValue + "'");
    //    ddlEmpl.DataSource = dsDept;
    //    ddlEmpl.DataValueField = "InchargeId";
    //    ddlEmpl.DataTextField = "InName";
    //    ddlEmpl.DataBind();
    //    ddlEmpl.Items.Insert(0, "Select Incharge");
    //    ddlEmpl.SelectedIndex = 0;
    //}
    protected void BindZoneDetails()
    {
        DataSet dsZoneDetails = new DataSet();
        dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet(" exec USP_ShowZoneDetails_ByUser '" + lblUser.Text + "'");
        
        divZone.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-bordered table-striped table-condensed'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='10%'>Zone Code</th>";
        ZoneInfo += "<th width='20%'>Zone Name</th>";
        ZoneInfo += "<th width='35%'>Location</th>";
        ZoneInfo += "<th width='25%'>Zone Assigned To</th>";
        ZoneInfo += "<th width='10%'>Total Nos. of Academy</th>";                                
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsZoneDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='10%' class='center'>" + dsZoneDetails.Tables[0].Rows[i]["ZoId"].ToString() + "</td>";
            //Session["ZoneId"] = dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString();
            ZoneInfo += "<td width='20%' class='center'><a href='Admin_AcademiesDetails.aspx?ZoneId=" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'>" + dsZoneDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</a></td>";
            ZoneInfo += "<td width='35%' class='center'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr><td> <b>State:</b> " + dsZoneDetails.Tables[0].Rows[i]["StateName"].ToString() + " </td></tr>";
            ZoneInfo += "<tr><td> <b>City:</b> " + dsZoneDetails.Tables[0].Rows[i]["CityName"].ToString() + "(" + dsZoneDetails.Tables[0].Rows[i]["Pincode"].ToString() + ")</td></tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='25%'>";
            ZoneInfo += "<table>";
            DataSet dseEmp = new DataSet();
            dseEmp = DAL.DalAccessUtility.GetDataInDataSet("exec USP_LocationEmployee '" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'");
            if (dseEmp.Tables[0].Rows.Count>0)
            {
                for (int j = 0; j < dseEmp.Tables[0].Rows.Count; j++)
                {
                    ZoneInfo += "<tr><td><span  title=' Mobile: " + dseEmp.Tables[0].Rows[j]["InMobile"].ToString() + " \n Department: " + dseEmp.Tables[0].Rows[j]["department"].ToString() + " \n Designation: " + dseEmp.Tables[0].Rows[j]["Designation"].ToString() + "\n'  href='#'> " + dseEmp.Tables[0].Rows[j]["InName"].ToString() + " </span></td></tr>";
                    //class='btn btn-setting btn-round'
                }
            }
            else
            {
                //ZoneInfo += "<a class='btn btn-setting btn-round'  href='AdminHome.aspx?ZoneId=" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Please Allot Incharge</span></a>";
                //Session["ZoneId"] = dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString();
                ZoneInfo += "<a href='Admin_LocationAssignToEmployee.aspx?ZoneIdLoc=" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'><span class='label label-important' style='font-size: 15.998px;'>Please Allot Incharge</span></a>";
                //DataSet dsZoneName = new DataSet();
                //dsZoneName = DAL.DalAccessUtility.GetDataInDataSet("select ZoneName from zone where ZoneId='" + Session["ZoneId"].ToString() + "'");
                //lblZone.Text = dsZoneName.Tables[0].Rows[i]["ZoneName"].ToString();
            }
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            //Session["ZoneId"] = dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString();
            //DataSet dsZoneName = new DataSet();
            //dsZoneName = DAL.DalAccessUtility.GetDataInDataSet("select ZoneName from zone where ZoneId='" + Session["ZoneId"].ToString() + "'");
            //lblZone.Text = dsZoneName.Tables[0].Rows[0]["ZoneName"].ToString();

            DataSet dsAcaCount = new DataSet();
            dsAcaCount = DAL.DalAccessUtility.GetDataInDataSet("select COUNT(*) as Coun from Academy where ZoneId='" + dsZoneDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'");
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