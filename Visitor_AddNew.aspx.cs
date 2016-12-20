using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Visitor_AddNew : System.Web.UI.Page
{
    protected static int UserID = -1;

    public string visitorType
    {
        set
        {
            ViewState["_visitortype"] = value;
        }
        get
        {
            if (ViewState["_visitortype"] == null)
            {
                ViewState["_visitortype"] = "0";
            }
            return (string)ViewState["_visitortype"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["InchargeID"] != null)
            {
                UserID = Convert.ToInt16(Session["InchargeID"].ToString());
            }
            if (Request.QueryString["VisitorType"] != null)
            {
                visitorType = Request.QueryString["VisitorType"].ToString();
                if (visitorType == "1")
                {
                    BindNewVisitor();

                }
                else
                {
                    BindPermanentEmp();
                }

                hdnVisitorType.Value = visitorType;
            }
            BindVisitorType();
            // ddlntypeofvisitor.Items.FindByValue("1").Selected = true;
            BindCountry();
            if (Request.QueryString["VisitorID"] != null)
            {
                int visitor = int.Parse(Request.QueryString["VisitorID"]);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "LoadVisitorByVisitorID(" + visitor + ");", true);
            }
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

            ddlPrmntCountry.DataSource = dtTemp;
            ddlPrmntCountry.DataValueField = "CountryID";
            ddlPrmntCountry.DataTextField = "CountryName";
            ddlPrmntCountry.DataBind();
            ddlPrmntCountry.Items.Insert(0, new ListItem("--Select Country--", "0"));

        }
        dtTemp.Clear();
    }

    private void BindVisitorType()
    {
        DataTable visitortype = new DataTable();
        visitortype = DAL.DalAccessUtility.GetDataInDataSet("select * from dbo.VisitorType").Tables[0];
        if (visitortype != null && visitortype.Rows.Count > 0)
        {
            ddlntypeofvisitor.DataSource = visitortype;
            ddlntypeofvisitor.DataValueField = "ID";
            ddlntypeofvisitor.DataTextField = "VisitorType";
            ddlntypeofvisitor.DataBind();
            ddlntypeofvisitor.Items.Insert(0, new ListItem("--Select--", "0"));

            ddlntypeofvisitor.Items.RemoveAt(1);
        }
    }

    private void BindNewVisitor()
    {
        lblTag.Text = "Add New Visitor Information ||";
        divVisitorInfo.Visible = true;
        divPrmanent.Visible = false;
    }

    public void BindPermanentEmp()
    {
        lblTag.Text = "Add Permanent Employee Information";
        divPrmanent.Visible = true;
        divVisitorInfo.Visible = false;
    }

    public void Clear()
    {
        txtName.Text = "";
        txtnoofperson.Text = "";
        ddlpurpose.ClearSelection();
        txtvehicle.Text = "";
        drpNumberOfDays.ClearSelection();
        drpProofType.ClearSelection();
        ddlntypeofvisitor.ClearSelection();
        txtAddress.Text = "";
        txtReference.Text = "";
        txtContactNumber.Text = "";
        ddlroomservice.ClearSelection();
        ddlRoomRent.ClearSelection();
        ddlelectricitybill.ClearSelection();
        txtfirstDate.Text = "";
        txtlastDate.Text = "";
        drpState.ClearSelection();
        drpCountry.ClearSelection();
        drpCity.ClearSelection();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (hdnbookedSeats.Value == string.Empty)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please assign room to visitor.')", true);
        }
        else
        {
            string fileNameToSave = string.Empty;
            Visitors visitor = new Visitors();
            if (hdnVisitorType.Value == "1")
            {
                if (fileUploadIdentity.HasFile)
                {

                    string FileEx = System.IO.Path.GetExtension(fileUploadIdentity.FileName);
                    string visitorfilepath = Server.MapPath("~/VisitorProof/" + txtName.Text + "_" + txtContactNumber.Text + FileEx);
                    fileUploadIdentity.PostedFile.SaveAs(visitorfilepath);
                    visitor.IdentificationPath = "VisitorProof/" + txtName.Text + "_" + txtContactNumber.Text + FileEx;
                }
                else
                {
                    visitor.IdentificationPath = string.Empty;
                }
                visitor.ID = hdnVisitorID.Value == "" ? 0 : Convert.ToInt16(hdnVisitorID.Value);
                if (fileUploadphoto.HasFile)
                {
                    string PhotoFileEx = System.IO.Path.GetExtension(fileUploadphoto.FileName);
                    string path = Server.MapPath("~/VisitorsPhoto/" + txtName.Text + "_" + txtContactNumber.Text + PhotoFileEx);
                    fileUploadphoto.PostedFile.SaveAs(path);
                    visitor.VisitorsPhoto = "VisitorsPhoto/" + txtName.Text + "_" + txtContactNumber.Text + PhotoFileEx;
                }
                else
                {
                    visitor.VisitorsPhoto = "";
                }
                if (fileUploadauthority.HasFile)
                {
                    string AuthorityFileEx = System.IO.Path.GetExtension(fileUploadauthority.FileName);
                    string path = Server.MapPath("~/VisitorsAuthourityLetter/" + txtName.Text + "_" + txtContactNumber.Text + AuthorityFileEx);
                    fileUploadauthority.PostedFile.SaveAs(path);
                    visitor.VisitorsAuthorityLetter = "VisitorsAuthourityLetter/" + txtName.Text + "_" + txtContactNumber.Text + AuthorityFileEx;
                }
                else
                {
                    visitor.VisitorsAuthorityLetter = "";
                }
                if (ddlRoomRent.SelectedValue != "-1")
                {
                    visitor.RoomRent = int.Parse(ddlRoomRent.SelectedValue);
                }
                else
                {
                    visitor.RoomRent = 0;
                }
                if (string.IsNullOrEmpty(txtfirstDate.Text))
                {
                    visitor.TimePeriodFrom = Utility.GetLocalDateTime(System.DateTime.UtcNow);
                }
                else
                {
                    DateTime serverTime = Convert.ToDateTime(txtfirstDate.Text);
                    DateTime utcTime = serverTime.ToUniversalTime();

                    TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);
                    visitor.TimePeriodFrom = localTime;
                }
                if (string.IsNullOrEmpty(txtlastDate.Text))
                {
                    visitor.TimePeriodTo = Utility.GetLocalDateTime(System.DateTime.UtcNow);
                }
                else
                {
                    visitor.TimePeriodTo = Convert.ToDateTime(txtlastDate.Text);
                }

                visitor.Name = txtName.Text;
                if (ddlpurpose.SelectedValue != "0")
                {
                    visitor.PurposeOfVisit = ddlpurpose.SelectedValue;
                }
                else
                {
                    visitor.PurposeOfVisit = "0";
                }
                visitor.VisitorAddress = txtAddress.Text;
                visitor.ContactNumber = txtContactNumber.Text;
                if (txtvehicle.Text == "")
                {
                    visitor.VehicleNo = "";

                }
                else
                {
                    visitor.VehicleNo = txtvehicle.Text;
                }
                visitor.BuildingID = Convert.ToInt16(hdnBuildingID.Value);
                visitor.CreatedOn = Utility.GetLocalDateTime(System.DateTime.UtcNow);
                visitor.CreatedBy = UserID;
                visitor.ModifyBy = UserID;
                visitor.ModifyOn = Utility.GetLocalDateTime(System.DateTime.UtcNow);
                if (txtnoofperson.Text == "")
                {
                    visitor.TotalNoOfMen = 0;
                }
                else
                {
                    visitor.TotalNoOfMen = Convert.ToInt32(txtnoofperson.Text);

                }

                if (txtNoOfFemale.Text == "")
                {
                    visitor.TotalNoOfWomen = 0;
                }
                else
                {
                    visitor.TotalNoOfWomen = Convert.ToInt32(txtNoOfFemale.Text);

                }

                if (txtNoOfChildren.Text == "")
                {
                    visitor.TotalNoOfChildren = 0;
                }
                else
                {
                    visitor.TotalNoOfChildren = Convert.ToInt32(txtNoOfChildren.Text);

                }
                if (drpNumberOfDays.SelectedValue != null)
                {
                    visitor.NoOfDaysToStay = Convert.ToInt16(drpNumberOfDays.SelectedValue);
                }
                else
                {
                    visitor.NoOfDaysToStay = null;
                }
                visitor.VisitorTypeID = 1;
              
               
                visitor.Identification = drpProofType.SelectedValue;

                visitor.State = int.Parse(drpState.SelectedValue);
                visitor.Country = int.Parse(drpCountry.SelectedValue);
                visitor.City = int.Parse(drpCity.SelectedValue);
                visitor.IsActive = true;
                visitor.AdmissionNumber = txtAdmissionNo.Text;
                visitor.VisitorReference = txtReference.Text;


                VisitorRoomNumbers roomNumber = null;
                visitor.VisitorRoomNumbers = new List<VisitorRoomNumbers>();
                foreach (string str in hdnbookedSeats.Value.Split(','))
                {
                    roomNumber = new VisitorRoomNumbers();
                    roomNumber.RoomNumberID = int.Parse(str);
                    roomNumber.CreatedOn = DateTime.Now;
                    visitor.VisitorRoomNumbers.Add(roomNumber);
                }
            }
            else
            {
                if (fileUploadPrmntProof.HasFile)
                {

                    string FileEx = System.IO.Path.GetExtension(fileUploadPrmntProof.FileName);
                    string visitorfilepath = Server.MapPath("~/VisitorProof/" + txtPrmntName.Text + "_" + txtPrmntContactNo.Text + FileEx);
                    fileUploadPrmntProof.PostedFile.SaveAs(visitorfilepath);
                    visitor.IdentificationPath = "VisitorProof/" + txtPrmntName.Text + "_" + txtPrmntContactNo.Text + FileEx;
                }
                else
                {
                    visitor.IdentificationPath = string.Empty;
                }
                visitor.ID = hdnVisitorID.Value == "" ? 0 : Convert.ToInt16(hdnVisitorID.Value);
                if (fileUploadPrmntPhoto.HasFile)
                {
                    string PhotoFileEx = System.IO.Path.GetExtension(fileUploadPrmntPhoto.FileName);
                    string path = Server.MapPath("~/VisitorsPhoto/" + txtPrmntName.Text + "_" + txtPrmntContactNo.Text + PhotoFileEx);
                    fileUploadPrmntPhoto.PostedFile.SaveAs(path);
                    visitor.VisitorsPhoto = "VisitorsPhoto/" + txtPrmntName.Text + "_" + txtPrmntContactNo.Text + PhotoFileEx;
                }
                else
                {
                    visitor.VisitorsPhoto = "";
                }
                if (fileUploadPrmntAuthority.HasFile)
                {
                    string AuthorityFileEx = System.IO.Path.GetExtension(fileUploadPrmntAuthority.FileName);
                    string path = Server.MapPath("~/VisitorsAuthourityLetter/" + txtPrmntName.Text + "_" + txtPrmntContactNo.Text + AuthorityFileEx);
                    fileUploadPrmntAuthority.PostedFile.SaveAs(path);
                    visitor.VisitorsAuthorityLetter = "VisitorsAuthourityLetter/" + txtPrmntName.Text + "_" + txtPrmntContactNo.Text + AuthorityFileEx;
                }
                else
                {
                    visitor.VisitorsAuthorityLetter = "";
                }

                if (ddlroomservice.SelectedValue != "0")
                {
                    visitor.RoomRentType = Convert.ToInt32(ddlroomservice.SelectedValue);
                }
                else
                {
                    visitor.RoomRentType = (int)TypeEnum.RoomRentType.VisitorSelfPaid;
                }
                if (ddlelectricitybill.SelectedValue != "0")
                {
                    visitor.ElectricityBill = int.Parse(ddlelectricitybill.SelectedValue);
                }
                else
                {
                    visitor.ElectricityBill = 0;
                }

                if (string.IsNullOrEmpty(txtprmntFrom.Text))
                {
                    visitor.TimePeriodFrom = Utility.GetLocalDateTime(System.DateTime.UtcNow);
                }
                else
                {
                    DateTime serverTime = Convert.ToDateTime(txtprmntFrom.Text);
                    DateTime utcTime = serverTime.ToUniversalTime();

                    TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);
                    visitor.TimePeriodFrom = localTime;
                }
                if (string.IsNullOrEmpty(txtprmntTo.Text))
                {
                    visitor.TimePeriodTo = Utility.GetLocalDateTime(System.DateTime.UtcNow);
                }
                else
                {
                    visitor.TimePeriodTo = Convert.ToDateTime(txtprmntTo.Text);
                }
                visitor.Name = txtPrmntName.Text;
                visitor.VisitorAddress = txtPrmntAddress.Text;
                visitor.ContactNumber = txtPrmntContactNo.Text;

                visitor.BuildingID = Convert.ToInt16(hdnBuildingID.Value);
                visitor.CreatedOn = Utility.GetLocalDateTime(System.DateTime.UtcNow);
                visitor.CreatedBy = UserID;
                visitor.ModifyBy = UserID;
                visitor.ModifyOn = Utility.GetLocalDateTime(System.DateTime.UtcNow);


                visitor.Identification = ddlPrmntIdntity.SelectedValue;


                if (ddlntypeofvisitor.SelectedValue == "0")
                {
                    visitor.VisitorTypeID = 1;
                }
                else
                {
                    visitor.VisitorTypeID = Convert.ToInt32(ddlntypeofvisitor.SelectedValue);
                }
                visitor.State = int.Parse(ddlPrmntState.SelectedValue);
                visitor.Country = int.Parse(ddlPrmntCountry.SelectedValue);
                visitor.City = int.Parse(ddlPrmntCity.SelectedValue);
                visitor.IsActive = true;

                VisitorRoomNumbers roomNumber = null;
                visitor.VisitorRoomNumbers = new List<VisitorRoomNumbers>();
                foreach (string str in hdnbookedSeats.Value.Split(','))
                {
                    roomNumber = new VisitorRoomNumbers();
                    roomNumber.RoomNumberID = int.Parse(str);
                    roomNumber.CreatedOn = DateTime.Now;
                    visitor.VisitorRoomNumbers.Add(roomNumber);
                }
            }
            hdnVisitorID.Value = "";
            hdnbookedSeats.Value = "";
            VisitorUserRepository repo = new VisitorUserRepository(new AkalAcademy.DataContext());
            if (visitor.ID == 0)
            {
                repo.AddNewVisitor(visitor);
            }
            else
            {

                repo.UpdateVisitor(visitor);
            }
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "<script>alert('Record Saved Successfully');</script>", false);
            Response.Redirect("ViewVisitors.aspx");
            Clear();
        }
    }

    private void getBookedSeatNumbers()
    {
        hdnbookedSeats.Value = string.Empty;
    }

    protected void ddltypeofvisitor_SelectedIndexChanged(object sender, EventArgs e)
    {

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

    protected void ddlPrmntCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtTemp = new DataTable();
        UsersRepository repo = new UsersRepository(new AkalAcademy.DataContext());
        int countryID = int.Parse(ddlPrmntCountry.SelectedValue);
        dtTemp = repo.GetStateByCountryID(countryID);
        if (dtTemp != null && dtTemp.Rows.Count > 0)
        {
            ddlPrmntState.DataSource = dtTemp;
            ddlPrmntState.DataValueField = "StateID";
            ddlPrmntState.DataTextField = "StateName";
            ddlPrmntState.DataBind();
            ddlPrmntState.Items.Insert(0, new ListItem("--Select State--", "0"));
        }
    }

    protected void ddlPrmntState_SelectedIndexChanged(object sender, EventArgs e)
    {
        int stateID = int.Parse(ddlPrmntState.SelectedValue);
        DataTable dtTemp = new DataTable();
        UsersRepository repo = new UsersRepository(new AkalAcademy.DataContext());
        dtTemp = repo.GetCityByStateID(stateID);
        if (dtTemp != null && dtTemp.Rows.Count > 0)
        {
            ddlPrmntCity.DataSource = dtTemp;
            ddlPrmntCity.DataValueField = "CityID";
            ddlPrmntCity.DataTextField = "CityName";
            ddlPrmntCity.DataBind();
            ddlPrmntCity.Items.Insert(0, new ListItem("--Select City--", "0"));
        }
    }
}