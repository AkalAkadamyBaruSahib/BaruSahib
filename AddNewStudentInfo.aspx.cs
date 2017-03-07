using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddNewStudentInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindCountry();
        }
    }
    private void BindCountry()
    {
        DataTable dtTemp = new DataTable();
        UsersRepository repo = new UsersRepository(new AkalAcademy.DataContext());
        dtTemp = repo.GetCountry();
        if (dtTemp != null && dtTemp.Rows.Count > 0)
        {
            drpCountry.DataSource = dtTemp;
            drpCountry.DataValueField = "CountryID";
            drpCountry.DataTextField = "CountryName";
            drpCountry.DataBind();
            drpCountry.Items.Insert(0, new ListItem("--Select Country--", "0"));

        }
    }
    protected void drpState_SelectedIndexChanged(object sender, EventArgs e)
    {
        int stateID = int.Parse(drpState.SelectedValue);
        DataTable dtTemp = new DataTable();
        UsersRepository repo = new UsersRepository(new AkalAcademy.DataContext());
        dtTemp = repo.GetCityByStateID(stateID);
        if (dtTemp != null && dtTemp.Rows.Count > 0)
        {
            drpCity.DataSource = dtTemp;
            drpCity.DataValueField = "CityID";
            drpCity.DataTextField = "CityName";
            drpCity.DataBind();
            drpCity.Items.Insert(0, new ListItem("--Select City--", "0"));
        }
    }

    protected void drpCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtTemp = new DataTable();
        UsersRepository repo = new UsersRepository(new AkalAcademy.DataContext());

        int countryID = int.Parse(drpCountry.SelectedValue);
        dtTemp = repo.GetStateByCountryID(countryID);

        if (dtTemp != null && dtTemp.Rows.Count > 0)
        {
            drpState.DataSource = dtTemp;
            drpState.DataValueField = "StateID";
            drpState.DataTextField = "StateName";
            drpState.DataBind();
            drpState.Items.Insert(0, new ListItem("--Select State--", "0"));
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet dsExist = new DataSet();
        StudentDetail student = new StudentDetail();
        student.AdmissionNumber = Convert.ToInt32(txtAdmissionNumber.Text);
        student.Class = txtClass.Text;
        student.StudentName = txtStudentName.Text;
        student.FatherName = txtFatherName.Text;
        student.ContactNo = txtContactNumber.Text;
        student.CountryID = Convert.ToInt32(drpCountry.SelectedValue);
        student.StateID = Convert.ToInt32(drpState.SelectedValue);
        student.CityID = Convert.ToInt32(drpCity.SelectedValue);
        student.Address = txtAddress.Text;
      
        VisitorUserRepository repo = new VisitorUserRepository(new AkalAcademy.DataContext());
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("Select ID from StudentDetail  Where AdmissionNumber=" + txtAdmissionNumber.Text);
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            student.ID = Convert.ToInt32(dsExist.Tables[0].Rows[0]["ID"].ToString());
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
        txtAddress.Text = "";
        txtAdmissionNumber.Text = "";
        txtClass.Text = "";
        txtContactNumber.Text = "";
        txtFatherName.Text = "";
        txtStudentName.Text = "";
        drpCity.ClearSelection();
        drpCountry.ClearSelection();
        drpState.ClearSelection();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Clear();
    }
}