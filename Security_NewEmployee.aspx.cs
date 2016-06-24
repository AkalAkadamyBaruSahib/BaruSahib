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
            //BindZone();

            if (EmployeeID > 0)
            {
                LoadEmployeeData();
            }
        }
    }
    //protected void BindAcademy()
    //{
    //    DataSet AcaList = new DataSet();
    //    AcaList = DAL.DalAccessUtility.GetDataInDataSet("select AcaId,AcaName from Academy where ZoneId='" + ddlZone.SelectedValue + "'");
    //    ddlAcademy.DataSource = AcaList;
    //    ddlAcademy.DataValueField = "AcaId";
    //    ddlAcademy.DataTextField = "AcaName";
    //    ddlAcademy.DataBind();
    //    ddlAcademy.Items.Insert(0, new ListItem("--Select Academy--", "0"));
    //    ddlAcademy.SelectedIndex = 0;
    //}
    //protected void BindZone()
    //{
    //    DataSet ZoneList = new DataSet();
    //    ZoneList = DAL.DalAccessUtility.GetDataInDataSet("select * from Zone where Active=1");
    //    ddlZone.DataSource = ZoneList;
    //    ddlZone.DataValueField = "ZoneId";
    //    ddlZone.DataTextField = "ZoneName";
    //    ddlZone.DataBind();
    //    ddlZone.Items.Insert(0, "Select Zone");
    //    ddlZone.SelectedIndex = 0;
    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string fileNameToSave = string.Empty;
        SecurityEmployeeInfo securityemp = new SecurityEmployeeInfo();
        if (fileUploadAppointment.HasFile)
        {
            string FileEx = System.IO.Path.GetExtension(fileUploadAppointment.FileName);
            string Securityfilepath = Server.MapPath("~/SecurityAppointmentLetter/" + txtName.Text.Replace(" ", "_") + FileEx);
            fileUploadAppointment.PostedFile.SaveAs(Securityfilepath);
            securityemp.AppointmentLetter = "SecurityAppointmentLetter/" + txtName.Text.Replace(" ", "_") + FileEx;
        }
        else
        {
            securityemp.AppointmentLetter = string.Empty;
        }
        securityemp.ID = hdnsecurityEmployeeID.Value == "" ? 0 : Convert.ToInt16(hdnsecurityEmployeeID.Value);
        if (fileUploadphoto.HasFile)
        {
            string PhotoFileEx = System.IO.Path.GetExtension(fileUploadphoto.FileName);
            string path = Server.MapPath("~/SecurityEmployeePhoto/" + txtName.Text.Replace(" ", "_") + PhotoFileEx);
            fileUploadphoto.PostedFile.SaveAs(path);
            securityemp.Photo = "SecurityEmployeePhoto/" + txtName.Text.Replace(" ", "_") + PhotoFileEx;
        }
        else
        {
            securityemp.Photo = "";
        }

        if (fileUploadExperience.HasFile)
        {
            string ExperienceFileEx = System.IO.Path.GetExtension(fileUploadExperience.FileName);
            string path = Server.MapPath("~/SecurityExperienceLetter/" + txtName.Text.Replace(" ", "_") + ExperienceFileEx);
            fileUploadExperience.PostedFile.SaveAs(path);
            securityemp.ExperienceLetter = "SecurityExperienceLetter/" + txtName.Text.Replace(" ", "_") + ExperienceFileEx;
        }
        else
        {
            securityemp.ExperienceLetter = "";
        }
        if (fileUploadFamilyRationCard.HasFile)
        {
            string RationCardFileEx = System.IO.Path.GetExtension(fileUploadFamilyRationCard.FileName);
            string path = Server.MapPath("~/SecurityFamilyRationCard/" + txtName.Text.Replace(" ", "_") + RationCardFileEx);
            fileUploadFamilyRationCard.PostedFile.SaveAs(path);
            securityemp.FamilyRationCard = "SecurityFamilyRationCard/" + txtName.Text.Replace(" ", "_") + RationCardFileEx;
        }
        else
        {
            securityemp.FamilyRationCard = "";
        }
        if (fileUploadPCC.HasFile)
        {
            string PCCFileEx = System.IO.Path.GetExtension(fileUploadPCC.FileName);
            string path = Server.MapPath("~/SecurityPCC/" + txtName.Text.Replace(" ", "_") + PCCFileEx);
            fileUploadPCC.PostedFile.SaveAs(path);
            securityemp.PCC = "SecurityPCC/" + txtName.Text.Replace(" ", "_") + PCCFileEx;
        }
        else
        {
            securityemp.PCC = "";
        }
        if (fileUploadQualification.HasFile)
        {
            string QualificationFileEx = System.IO.Path.GetExtension(fileUploadQualification.FileName);
            string path = Server.MapPath("~/SecurityQualificationLetter/" + txtName.Text.Replace(" ", "_") + QualificationFileEx);
            fileUploadQualification.PostedFile.SaveAs(path);
            securityemp.QualificationLetter = "SecurityQualificationLetter/" + txtName.Text.Replace(" ", "_") + QualificationFileEx;
        }
        else
        {
            securityemp.QualificationLetter = "";
        }
        if (ddlZone.SelectedValue != null)
        {
            securityemp.ZoneID = Convert.ToInt32(ddlZone.SelectedValue);
        }
        else
        {
            securityemp.ZoneID = null;
        }
        if (ddlAcademy.SelectedValue != null)
        {
            securityemp.AcaID = Convert.ToInt32(ddlAcademy.SelectedValue);
        }
        else
        {
            securityemp.AcaID = null;
        }
        if (ddlDesig.SelectedValue != null)
        {
            securityemp.DesigID = Convert.ToInt32(ddlDesig.SelectedValue);
        }
        else
        {
            securityemp.DesigID = null;
        }
        if (ddlDept.SelectedValue != null)
        {
            securityemp.DeptID = Convert.ToInt32(ddlDept.SelectedValue);
        }
        else
        {
            securityemp.DeptID = null;
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
        securityemp.Cutting = txtCutting.Text;
        securityemp.CreatedOn = DateTime.Now;
        securityemp.ModifyOn = DateTime.Now;
        securityemp.IsApproved = true;
        hdnsecurityEmployeeID.Value = "";
        SecurityRepository repo = new SecurityRepository(new AkalAcademy.DataContext());
        if (securityemp.ID == 0)
        {
            repo.AddNewSecurityEmp(securityemp);
        }
        else
        {
            repo.UpdateSecurityEmp(securityemp);
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "<script>alert('Record Saved Successfully');</script>", false);
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
        ddlDept.SelectedIndex = 0;
        ddlDesig.SelectedIndex = 0;
        ddlZone.SelectedIndex = 0;
        ddlAcademy.ClearSelection();
        ddlEducation.ClearSelection();
    }
    protected void BindDesignation()
    {
        DataSet dsDesgis = new DataSet();
        dsDesgis = DAL.DalAccessUtility.GetDataInDataSet("select DesgId,Designation from Designation where Active=1 and ModuleID=" + ModuleID + " order by Designation asc");
        ddlDesig.DataSource = dsDesgis;
        ddlDesig.DataValueField = "DesgId";
        ddlDesig.DataTextField = "Designation";
        ddlDesig.DataBind();
        ddlDesig.Items.Insert(0, "--Select Designation--");
        ddlDesig.SelectedIndex = 0;
    }
    protected void BindDepartment()
    {
        DataSet dsDept = new DataSet();
        dsDept = DAL.DalAccessUtility.GetDataInDataSet("select DepId,department from Department where Active=1 and ModuleID=" + ModuleID + " order by department asc");
        ddlDept.DataSource = dsDept;
        ddlDept.DataValueField = "DepId";
        ddlDept.DataTextField = "department";
        ddlDept.DataBind();
        ddlDept.Items.Insert(0, "--Select Department--");
        ddlDept.SelectedIndex = 0;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Clr();
    }
    //protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindAcademy();
    //}
    public void LoadEmployeeData()
    {
        SecurityEmployeeInfoDTO emp = new SecurityEmployeeInfoDTO();
        SecurityRepository repository = new SecurityRepository(new AkalAcademy.DataContext());
        emp = repository.GetSecurityEmployeeInfoToUpdate(EmployeeID);
        txtAddress.Text = emp.Address;
        txtName.Text = emp.Name;
        txtMobileNo.Text = emp.MobileNo;
        txtSalary.Text = emp.Salary;
        txtCutting.Text = emp.Cutting;
        ddlEducation.SelectedValue = emp.Education.ToString();
        ddlDesig.SelectedValue = emp.DesigID.ToString();
        ddlDept.SelectedValue = emp.DeptID.ToString();
        ddlZone.SelectedValue = emp.ZoneID.ToString();
        ddlAcademy.SelectedValue = emp.AcaID.ToString();
    }
}