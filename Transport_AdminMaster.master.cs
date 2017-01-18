using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;

public partial class Transport_AdminMaster : System.Web.UI.MasterPage
{
    public int AdminType = -1;
    public int UserType = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmailId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblUserName.Text = Session["InName"].ToString();
            if (Session["AdminType"] != null)
            {
                AdminType = Convert.ToInt16(Session["AdminType"].ToString());
            }
            UserType = Convert.ToInt16(Session["UserTypeID"].ToString());
        }


        if (UserType != (int)TypeEnum.UserType.TRANSPORTADMIN)
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
        else
        {
            liEstimateiewForEmp.Visible = false;
            liBills.Visible = true;
            showUnApprovedEstimateCount();
        }
        

        if (AdminType == (int)TypeEnum.SubAdminName.TransportMaintenance)
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

 
        }
        else if(AdminType == (int)TypeEnum.SubAdminName.TransportVehicleMaintenance)
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

        }
        
     
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
}
