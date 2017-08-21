using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StudentWiseTransportDetail : System.Web.UI.Page
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dsExist = new DataTable();
        StudentDetailInTransport student = new StudentDetailInTransport();
        student.AdmissionNumber = Convert.ToInt32(txtAdmissionNumber.Text);
        student.Class = txtClass.Text;
        student.StudentName = txtStudentName.Text;
        student.FatherName = txtFatherName.Text;
        student.ContactNo = txtContactNumber.Text;
        student.NameOfVillage = txtNameOfVillage.Text;
        student.AcaID = Convert.ToInt32(hdnAcaID.Value);
        student.CreatedBy =Convert.ToInt32(hdnInchargeID.Value);

        TransportUserRepository repo = new TransportUserRepository(new AkalAcademy.DataContext());
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("Select ID from StudentDetailInTransport  Where AdmissionNumber=" + txtAdmissionNumber.Text).Tables[0];
        if (dsExist.Rows.Count > 0)
        {
            student.ID = Convert.ToInt32(dsExist.Rows[0]["ID"].ToString());
            repo.UpdateStudentsInfo(student);
        }
        else
        {
            repo.AddNewStudents(student);
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "<script>alert('Record Saved Successfully');</script>", false);
        Clear();
    }
    public void Clear()
    {
        txtNameOfVillage.Text = "";
        txtAdmissionNumber.Text = "";
        txtClass.Text = "";
        txtContactNumber.Text = "";
        txtFatherName.Text = "";
        txtStudentName.Text = "";
        drpAcademy.ClearSelection();
        drpPassenger.SelectedIndex = 0;
        drpViewPassenger.SelectedIndex = 0;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnStaffSave_Click(object sender, EventArgs e)
    {
        StaffDetailInTransport staff = new StaffDetailInTransport();
        staff.ID = hdnStaffID.Value == "" ? 0 : Convert.ToInt16(hdnStaffID.Value);
        staff.StaffType = Convert.ToInt32(drpStaffType.SelectedValue);
        staff.StaffName = txtStafftName.Text;
        staff.FatherName = txtStaffFatherName.Text;
        staff.CreatedBy = Convert.ToInt32(hdnInchargeID.Value);
        hdnStaffID.Value = "";
       TransportUserRepository repo = new TransportUserRepository(new AkalAcademy.DataContext());
        if (staff.ID == 0)
        {
            repo.AddNewStaffDetail(staff);
        }
        else
        {
            repo.UpdateStaffDetail(staff);
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "<script>alert('Record Saved Successfully');</script>", false);
        StaffClear();
       
    }
    protected void btnStaffClear_Click(object sender, EventArgs e)
    {
        StaffClear();
    }
    
    public void StaffClear()
    {
        drpStaffType.ClearSelection();
        txtStaffFatherName.Text = "";
        txtStafftName.Text = "";
        drpPassenger.SelectedIndex = 0;
        drpViewPassenger.SelectedIndex = 0;
    }
}