using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;
using System.Data;

public partial class Transport_AdminMaster : System.Web.UI.MasterPage
{
    public int AdminType = -1;
    public int UserType = -1;
    private int InchargeID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmailId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblUserName.Text = Session["InName"].ToString();
            UserType = Convert.ToInt16(Session["UserTypeID"].ToString());
            InchargeID = int.Parse(Session["InchargeID"].ToString());
        }
        if (UserType == (int)TypeEnum.UserType.TRANSPORTADMIN || UserType == (int)TypeEnum.UserType.CLUSTERHEAD)
        {
            liEstimateiewForEmp.Visible = false;
            liBills.Visible = true;
            liComplaints.Visible = true;
            showUnApprovedEstimateCount();
        }
        else
        {
            liDesignation.Visible = false;
            liDepartment.Visible = false;
            liCreateEditEmployee.Visible = false;
            liLocationAssign.Visible = false;
            liEstimateNewEstimate.Visible = false;
            liCreateMaterial.Visible = false;
            liContractRate.Visible = false;
            liBills.Visible = false;
        }
        ShowLinks();
    }
    private void showUnApprovedEstimateCount()
    {
        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        if (!spnViewEstimate.InnerText.Contains('('))
        {
            spnViewEstimate.InnerText = spnViewEstimate.InnerText + " (" + repository.GetUnApprovedEstimates((int)TypeEnum.Module.Transport) + ")";
        }
    }

    protected void lbLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("~/Default.aspx");
    }

    private void ShowLinks()
    {
        List<UserRole> role = new List<UserRole>();
        TransportUserRepository repo = new TransportUserRepository(new AkalAcademy.DataContext());
        role = repo.GetUserRoleByInchargeID(InchargeID);
        if (role != null && role.Count!=0)
        {
            var userTransportMaintRole = role.Where(document => document.RoleID == (int)TypeEnum.UserRole.TransportMaintenance).FirstOrDefault();
            if (userTransportMaintRole != null)
            {
                liEmployee.Visible = false;
                liVehicles.Visible = false;
                lireport.Visible = false;
                liDiesel.Visible = false;
                liEstimate.Visible = true;
                liContractRate.Visible = false;
                liDesignation.Visible = false;
                liDepartment.Visible = false;
                liCreateEditEmployee.Visible = false;
                liLocationAssign.Visible = false;
                liCreateMaterial.Visible = false;
                liContractRate.Visible = false;
                liEstimateiewForEmp.Visible = false;
                liMaintenance.Visible = true;
                liEstimateNewEstimate.Visible = true;
                liComplaints.Visible = false;
            }
            var userTransportVehicleRole = role.Where(document => document.RoleID == (int)TypeEnum.UserRole.TransportVehicleMaintenance).FirstOrDefault();
            if (userTransportVehicleRole != null)
            {
                liEmployee.Visible = true;
                liVehicles.Visible = true;
                lireport.Visible = true;
                liMaintenance.Visible = false;
                liDiesel.Visible = false;
                liEstimate.Visible = false;
                liContractRate.Visible = false;
                liDesignation.Visible = false;
                liDepartment.Visible = false;
                liCreateEditEmployee.Visible = false;
                liEstimateNewEstimate.Visible = false;
                liCreateMaterial.Visible = false;
                liContractRate.Visible = false;
                liComplaints.Visible = false;

            }
            var userComplaintRole = role.Where(document => document.RoleID == (int)TypeEnum.UserRole.Complaint).FirstOrDefault();
            if (userComplaintRole != null)
            {
                liComplaints.Visible = true;
            }
        }
    }
}