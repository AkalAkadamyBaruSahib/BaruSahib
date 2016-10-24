using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Security_EmployeeDetail : System.Web.UI.Page
{
    private bool IsApproved = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            getSecurityDetails(IsApproved);
            hdnInchargeID.Value = Session["InchargeID"].ToString();

            if (Request.QueryString["ActiveEmployeeID"] != null)
            {
                InActiveEmployee(Request.QueryString["ActiveEmployeeID"].ToString());
            }

            if (Request.QueryString["DeActiveEmployeeID"] != null)
            {
                ActiveEmployee(Request.QueryString["DeActiveEmployeeID"].ToString());
            }

        }

    }
    private void getSecurityDetails(bool IsApproved)
    {
        DataTable dsSecurityEmpDetails = new DataTable();
        List<SecurityEmployeeInfo> SecurityEmployee = new List<SecurityEmployeeInfo>();
        SecurityRepository SecurityRepo = new SecurityRepository(new AkalAcademy.DataContext());


        SecurityEmployee = SecurityRepo.SecurityEmployeeInfoView(IsApproved);

        string ZoneInfo = string.Empty;
        ZoneInfo += "<div class='box span12'>";
        ZoneInfo += "<div class='box-header well' data-original-title>";
        ZoneInfo += "<h2><i class='icon-user'></i> Employee List</h2>";
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
        ZoneInfo += "<th style='display:none;'></th>";
        ZoneInfo += "<th width='30%'>Employee Information</th>";
        ZoneInfo += "<th width='25%'>Assigned For</th>";
        ZoneInfo += "<th width='30%'>Zonal Security Supervisor</th>";
        ZoneInfo += "<th width='15%'>Salary</th>";
        if (IsApproved)
        {
            ZoneInfo += "<th width='15%'>Action</th>";
        }
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        if (SecurityEmployee.Count > 0)
        {
            foreach (SecurityEmployeeInfo Security in SecurityEmployee)
            {
                ZoneInfo += "<tr>";
                ZoneInfo += "<td style='display:none;'>1</td>";
                if (Security.IsApproved == true)
                {
                    ZoneInfo += "<td width='30%'><table><tr><td><li id='image-1' class='thumbnail'><a target='_blank' style='background:url(" + Security.Photo + ")'  href='" + Security.Photo + "'><img class='grayscale' width='75Px' height='75PX' src='" + Security.Photo + "' ></a></li></td></tr><tr><td><a class='btn btn-danger' href='Security_NewEmployee.aspx?EmployeeID=" + Security.ID + "'><span  style='font-size: 15.998px;'><i class='icon-edit icon-white'></i>" + Security.Name + "</span></a></td><td><a href='Security_EmployeeDetail.aspx?ActiveEmployeeID=" + Security.ID + "'><span class='label label-success' style='font-size: 15.998px;' title='Employee Active'>Active</span></a></td></tr></table></td>";
                }
                else
                {
                    ZoneInfo += "<td width='25%'><table><tr><td><li id='image-1' class='thumbnail'><a target='_blank' style='background:url(" + Security.Photo + ")'  href='" + Security.Photo + "'><img class='grayscale' width='75Px' height='75PX' src='" + Security.Photo + "' ></a></li></td></tr><tr><td><a class='btn btn-danger' href='Security_NewEmployee.aspx?EmployeeID=" + Security.ID + "'><span  style='font-size: 15.998px;'><i class='icon-edit icon-white'></i>" + Security.Name + "</span></a></td><td><a href='Security_EmployeeDetail.aspx?DeActiveEmployeeID=" + Security.ID + "'><span class='label label-warning' style='font-size: 15.998px;' title='Employee InActive'>InActive</span></a></td></tr></table></td>";
                }
                ZoneInfo += "<td width='30%'><table><tr><td><b>Zone</b>: " + Security.Zone.ZoneName + "</td></tr><tr><td><b>Academy</b>: " + Security.Academy.AcaName + "</td></tr><tr><td><b>Address:</b>" + Security.Address + "</td></tr><tr><td><b>MobileNo:</b> " + Security.MobileNo + "</td></tr></table>";
                ZoneInfo += "<td class='center'width='30%'><table>"; 
                DataTable dsINcharge = new DataTable();
                dsINcharge = DAL.DalAccessUtility.GetDataInDataSet("select distinct Inc.InName,Inc.InMobile from  AcademyAssignToEmployee AAE INNER JOIN Incharge Inc on Inc.InchargeId = AAE.EmpID where AAE.ZoneId='" + Security.ZoneID + "' and Inc.ModuleID = " + (int)TypeEnum.Module.Security + " and Inc.DesigId = 26").Tables[0];
                if (dsINcharge != null && dsINcharge.Rows.Count > 0)
                {
                    for (int j = 0; j < dsINcharge.Rows.Count; j++)
                    {
                        ZoneInfo += "<tr><td class='center'><b>Name:</b>" + dsINcharge.Rows[j]["InName"].ToString() + "</td>";
                        ZoneInfo += "<tr><td class='center'><b>MobileNo:</b> " + dsINcharge.Rows[j]["InMobile"].ToString() + "</td></tr>";
                    }
                }
                else
                {
                    ZoneInfo += "<td width='20%' class='center'>No Zonal Officer Assign</td>";
                }
             
              
                ZoneInfo += "</table></td>";
                ZoneInfo += "<td width='15%'>" + Convert.ToDecimal(Security.Salary).ToString("#,##0.00") + "</td>";
                if (IsApproved)
                {
                    ZoneInfo += "<td  width='15%' class='center'><table><tr><td><a onclick='OpenTransferEmployee(" + Security.ID + "," + Security.ZoneID + "," + Security.AcaID + ",&quot;" + Security.Name + "&quot;);'  href='#'><span class='label label-warning'  style='font-size: 15.998px;'>Transfer Employee</span></a></td></tr>";
                    ZoneInfo += "<tr><td class='center'><a onclick='OpenViewTransferLetter(" + Security.ID + ");' href='#'><span class='label label-warning'  style='font-size: 15.998px;'>View Transfer Letter</span></a></td></tr></table></td>";
                }
                ZoneInfo += "</tr>";
            }
            ZoneInfo += "</tbody>";
            ZoneInfo += "</table>";
        }
        divEmployeeDetails.InnerHtml = ZoneInfo.ToString();
    }

    protected void InActiveEmployee(string eid)
    {
        SecurityController securitycontroller = new SecurityController();
        securitycontroller.DeleteEmployeeInfo(Convert.ToInt32(eid));
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Employee Information  Delete Successfully.');", true);
        getSecurityDetails(true);
    }

    protected void ActiveEmployee(string eid)
    {
        SecurityController securitycontroller = new SecurityController();
        securitycontroller.ActiveEmployeeInfo(Convert.ToInt32(eid));
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Employee Information  Active Successfully.');", true);
        getSecurityDetails(true);
    }

    protected void btnNonApproved_Click(object sender, EventArgs e)
    {
        if (((Button)sender).Text == "View InActive Employee(s)")
        {
            IsApproved = false;
            getSecurityDetails(false);
            ((Button)sender).Text = "View Active Employee(s)";
        }
        else
        {
            IsApproved = true;
            ((Button)sender).Text = "View Active Employee(s)";
            getSecurityDetails(true);
            ((Button)sender).Text = "View InActive Employee(s)";
        }
    }
}