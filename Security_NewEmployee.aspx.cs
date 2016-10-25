using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AkalAcademy;

public partial class Security_NewEmployee : System.Web.UI.Page
{
    private int EmployeeID = -1;
    public static int ModuleID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["EmployeeID"] != null)
        {
            EmployeeID = int.Parse(Request.QueryString["EmployeeID"].ToString());
        }
        if (!IsPostBack)
        {

            if (Session["ModuleID"] != null)
            {
                ModuleID = int.Parse(Session["ModuleID"].ToString());
            }
            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblUser.Text = Session["EmailId"].ToString();
            }

            BindDesignation();
            BindDepartment();
            BindZone();

            if (EmployeeID > 0)
            {
                LoadEmployeeData();
            }
        }
    }

    protected void BindAcademy()
    {
        DataTable AcaList = new DataTable();
        AcaList = DAL.DalAccessUtility.GetDataInDataSet("select AcaId,AcaName from Academy where ZoneId=" + drpZone.SelectedValue).Tables[0];
        if (AcaList != null && AcaList.Rows.Count > 0)
        {
            drpAcademy.DataSource = AcaList;
            drpAcademy.DataValueField = "AcaId";
            drpAcademy.DataTextField = "AcaName";
            drpAcademy.DataBind();
            drpAcademy.Items.Insert(0, new ListItem("--Select Academy--", "0"));
            drpAcademy.SelectedIndex = 0;
        }
    }
    protected void BindZone()
    {
        int InchargeID = int.Parse(Session["InchargeID"].ToString());
        PurchaseRepository repo = new PurchaseRepository(new DataContext());
        List<Zone> zones = repo.GetZoneByInchargeID(InchargeID);
        drpZone.DataSource = zones;
        drpZone.DataValueField = "ZoneId";
        drpZone.DataTextField = "ZoneName";
        drpZone.DataBind();
        drpZone.Items.Insert(0, new ListItem("--Select Zone--", "0"));
        drpZone.SelectedIndex = 0;
    }
 
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string fileNameToSave = string.Empty;
        SecurityEmployeeInfo securityemp = new SecurityEmployeeInfo();
        if (fileUploadAppointment.HasFile)
        {
            string FileEx = System.IO.Path.GetExtension(fileUploadAppointment.FileName);
            string Securityfilepath = Server.MapPath("~/SecurityAppointmentLetter/" + txtName.Text.Replace(" ", "_") + txtMobileNo.Text + FileEx);
            fileUploadAppointment.PostedFile.SaveAs(Securityfilepath);
            securityemp.AppointmentLetter = "SecurityAppointmentLetter/" + txtName.Text.Replace(" ", "_") + txtMobileNo.Text + FileEx;
        }
        else
        {
            securityemp.AppointmentLetter = string.Empty;
        }
        securityemp.ID = hdnsecurityEmployeeID.Value == "" ? 0 : Convert.ToInt16(hdnsecurityEmployeeID.Value);
        if (fileUploadphoto.HasFile)
        {
            string PhotoFileEx = System.IO.Path.GetExtension(fileUploadphoto.FileName);
            string path = Server.MapPath("~/SecurityEmployeePhoto/" + txtName.Text.Replace(" ", "_") + txtMobileNo.Text + PhotoFileEx);
            fileUploadphoto.PostedFile.SaveAs(path);
            securityemp.Photo = "SecurityEmployeePhoto/" + txtName.Text.Replace(" ", "_") + txtMobileNo.Text + PhotoFileEx;
        }
        else
        {
            securityemp.Photo = "";
        }

        if (fileUploadExperience.HasFile)
        {
            string ExperienceFileEx = System.IO.Path.GetExtension(fileUploadExperience.FileName);
            string path = Server.MapPath("~/SecurityExperienceLetter/" + txtName.Text.Replace(" ", "_") + txtMobileNo.Text + ExperienceFileEx);
            fileUploadExperience.PostedFile.SaveAs(path);
            securityemp.ExperienceLetter = "SecurityExperienceLetter/" + txtName.Text.Replace(" ", "_") + txtMobileNo.Text + ExperienceFileEx;
        }
        else
        {
            securityemp.ExperienceLetter = "";
        }
        if (fileUploadFamilyRationCard.HasFile)
        {
            string RationCardFileEx = System.IO.Path.GetExtension(fileUploadFamilyRationCard.FileName);
            string path = Server.MapPath("~/SecurityFamilyRationCard/" + txtName.Text.Replace(" ", "_") + txtMobileNo.Text + RationCardFileEx);
            fileUploadFamilyRationCard.PostedFile.SaveAs(path);
            securityemp.FamilyRationCard = "SecurityFamilyRationCard/" + txtName.Text.Replace(" ", "_") + txtMobileNo.Text + RationCardFileEx;
        }
        else
        {
            securityemp.FamilyRationCard = "";
        }
        if (fileUploadPCC.HasFile)
        {
            string PCCFileEx = System.IO.Path.GetExtension(fileUploadPCC.FileName);
            string path = Server.MapPath("~/SecurityPCC/" + txtName.Text.Replace(" ", "_") + txtMobileNo.Text + PCCFileEx);
            fileUploadPCC.PostedFile.SaveAs(path);
            securityemp.PCC = "SecurityPCC/" + txtName.Text.Replace(" ", "_") + txtMobileNo.Text + PCCFileEx;
        }
        else
        {
            securityemp.PCC = "";
        }
        if (fileUploadQualification.HasFile)
        {
            string QualificationFileEx = System.IO.Path.GetExtension(fileUploadQualification.FileName);
            string path = Server.MapPath("~/SecurityQualificationLetter/" + txtName.Text.Replace(" ", "_") + txtMobileNo.Text + QualificationFileEx);
            fileUploadQualification.PostedFile.SaveAs(path);
            securityemp.QualificationLetter = "SecurityQualificationLetter/" + txtName.Text.Replace(" ", "_") + txtMobileNo.Text + QualificationFileEx;
        }
        else
        {
            securityemp.QualificationLetter = "";
        }
        if (drpZone.SelectedValue != "0")
        {
            securityemp.ZoneID = Convert.ToInt32(drpZone.SelectedValue);
        }
        else
        {
            securityemp.ZoneID = 0;
        }
        if (drpAcademy.SelectedValue != "0")
        {
            securityemp.AcaID = Convert.ToInt32(drpAcademy.SelectedValue);
        }
        else
        {
            securityemp.AcaID = 0;
        }
        if (ddlDesig.SelectedValue != "0")
        {
            securityemp.DesigID = Convert.ToInt32(ddlDesig.SelectedValue);
        }
        else
        {
            securityemp.DesigID = 0;
        }
      
        if (ddlEducation.SelectedValue != null)
        {
            securityemp.Education = ddlEducation.SelectedValue;
        }
        else
        {
            securityemp.Education = null;
        }
        securityemp.Name = txtName.Text;
        securityemp.Address = txtAddress.Text;
        securityemp.MobileNo = txtMobileNo.Text;
        securityemp.Salary = txtSalary.Text;
        securityemp.Deduction = Convert.ToInt32(txtCutting.Text);
        securityemp.CreatedOn = DateTime.Now;
        securityemp.ModifyOn = DateTime.Now;
        securityemp.IsApproved = true;
        if (txtDateofAppraisal.Text != null && txtDateofAppraisal.Text != "")
        {
            securityemp.DateOfAppraisal = Convert.ToDateTime(txtDateofAppraisal.Text);
        }

        securityemp.DOJ = Convert.ToDateTime(txtDateofJoining.Text);
        securityemp.LastAppraisal = txtLastAppraisal.Text;

         SecurityRepository repo = new SecurityRepository(new AkalAcademy.DataContext());
        if (hdnsecurityEmployeeID.Value == "0" || hdnsecurityEmployeeID.Value =="")
        {
            repo.AddNewSecurityEmp(securityemp);
        }
        else
        {
            repo.UpdateSecurityEmp(securityemp);
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "<script>alert('Record Saved Successfully');</script>", false);
        Response.Redirect("Security_EmployeeDetail.aspx");
        Clr();
    }
    protected void Clr()
    {
        txtName.Text = "";
        txtSalary.Text = "";
        txtCutting.Text = "";
        txtAddress.Text = "";
        txtMobileNo.Text = "";
        txtName.Text = "";
        txtLastAppraisal.Text = "";
        txtDateofJoining.Text = "";
        txtDateofAppraisal.Text = "";
        ddlDesig.SelectedIndex = 0;
        drpZone.SelectedIndex = 0;
        drpAcademy.ClearSelection();
        ddlEducation.ClearSelection();
    }
    protected void BindDesignation()
    {
        DataTable dsDesgis = new DataTable();
        dsDesgis = DAL.DalAccessUtility.GetDataInDataSet("select DesgId,Designation from Designation where Active=1 and ModuleID=" + ModuleID + " order by Designation asc").Tables[0];
        if (dsDesgis != null && dsDesgis.Rows.Count > 0)
        {
            ddlDesig.DataSource = dsDesgis;
            ddlDesig.DataValueField = "DesgId";
            ddlDesig.DataTextField = "Designation";
            ddlDesig.DataBind();
            ddlDesig.Items.Insert(0, new ListItem("--Select Designation--", "0"));
            ddlDesig.SelectedIndex = 0;
        }
    }
    protected void BindDepartment()
    {
     
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Clr();
    }
   
    public void LoadEmployeeData()
    {
        SecurityEmployeeInfoDTO emp = new SecurityEmployeeInfoDTO();
        SecurityRepository repository = new SecurityRepository(new AkalAcademy.DataContext());
        emp = repository.GetSecurityEmployeeInfoToUpdate(EmployeeID);
        hdnsecurityEmployeeID.Value = emp.ID.ToString();
        txtAddress.Text = emp.Address;
        txtName.Text = emp.Name;
        txtMobileNo.Text = emp.MobileNo;
        txtSalary.Text = emp.Salary;
        txtCutting.Text = emp.Deduction;
        ddlEducation.SelectedValue = emp.Education.ToString();
        ddlDesig.SelectedValue = emp.DesigID.ToString();
        drpZone.SelectedValue = emp.ZoneID.ToString();
        BindAcademy();
        drpAcademy.SelectedValue = emp.AcaID.ToString();
        txtDateofJoining.Text = emp.DOJ;
        txtDateofAppraisal.Text = emp.DateOfAppraisal.ToString();
        txtLastAppraisal.Text = emp.LastAppraisal.ToString();
        afileUploadAppointment.Visible = true;
        afileUploadExperience.Visible = true;
        afileUploadQualification.Visible = true;
        afileUploadPCC.Visible = true;
        afileUploadFamilyRationCard.Visible = true;
        afileUploadphoto.Visible = true;
        afileUploadAppointment.HRef = emp.AppointmentLetter;
        afileUploadExperience.HRef = emp.ExperienceLetter;
        afileUploadQualification.HRef = emp.QualificationLetter;
        afileUploadPCC.HRef = emp.PCC;
        afileUploadFamilyRationCard.HRef = emp.FamilyRationCard;
        afileUploadphoto.HRef = emp.Photo;

    }
    protected void drpZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAcademy();
    }
}