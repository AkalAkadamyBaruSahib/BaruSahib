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
            if (Request.QueryString["VisitorsId"] != null)
            {
                visitorType = Request.QueryString["VisitorsId"].ToString();
                if (visitorType == "1")
                {
                    BindNewVisitor();

                }
                else
                {
                    BindPermanentEmp();
                }
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
        }
        dtTemp.Clear();
        dtTemp = repo.GetStateByCountryID(15);

        if (dtTemp != null && dtTemp.Rows.Count > 0)
        {
            drpState.DataSource = dtTemp;
            drpState.DataValueField = "StateID";
            drpState.DataTextField = "StateName";
            drpState.DataBind();
            drpState.Items.Insert(0, new ListItem("--Select State--", "0"));
        }
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
        lblTag.Text = "Add New Visitor Information";
        divfileUploadauthority.Visible = false;
        divddlelectricitybill.Visible = false;
        divddlroomservice.Visible = false;
        divddlntypeofvisitor.Visible = false;
    }

    public void BindPermanentEmp()
    {
        lblTag.Text = "Add Permanent Employee Information";
        divtxtvehicle.Visible = false;
        divtxtnoofperson.Visible = false;
        divddlpurpose.Visible = false;
        divdrpNumberOfDays.Visible = false;
        divVisitorRoomRent.Visible = false;
        ddlRoomRent.Visible = false;
        ddlpurpose.Visible = false;
        DataSet visitortype = new DataSet();

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
        string fileNameToSave = string.Empty;
        Visitors visitor = new Visitors();
        if (fileUploadIdentity.HasFile)
        {

            string FileEx = System.IO.Path.GetExtension(fileUploadIdentity.FileName);
            string visitorfilepath = Server.MapPath("~/VisitorProof/" + txtName.Text.Replace(" ", "_") + drpProofType.SelectedItem.Text + FileEx);
            fileUploadIdentity.PostedFile.SaveAs(visitorfilepath);
            visitor.IdentificationPath = "VisitorProof/" + txtName.Text.Replace(" ", "_") + drpProofType.SelectedItem.Text + FileEx;
        }
        else
        {
            visitor.IdentificationPath = string.Empty;
        }
        visitor.ID = hdnVisitorID.Value == "" ? 0 : Convert.ToInt16(hdnVisitorID.Value);
        if (fileUploadphoto.HasFile)
        {
            string PhotoFileEx = System.IO.Path.GetExtension(fileUploadphoto.FileName);
            string path = Server.MapPath("~/VisitorsPhoto/" + txtName.Text.Replace(" ", "_") + PhotoFileEx);
            fileUploadphoto.PostedFile.SaveAs(path);
            visitor.VisitorsPhoto = "VisitorsPhoto/" + txtName.Text.Replace(" ", "_") + PhotoFileEx;
        }
        else
        {
            visitor.VisitorsPhoto = "";
        }
        if (fileUploadauthority.HasFile)
        {
            string AuthorityFileEx = System.IO.Path.GetExtension(fileUploadauthority.FileName);
            string path = Server.MapPath("~/VisitorsAuthourityLetter/" + txtName.Text.Replace(" ", "_") + AuthorityFileEx);
            fileUploadauthority.PostedFile.SaveAs(path);
            visitor.VisitorsAuthorityLetter = "VisitorsAuthourityLetter/" + txtName.Text.Replace(" ", "_") + AuthorityFileEx;
        }
        else
        {
            visitor.VisitorsAuthorityLetter = "";
        }
        if (ddlRoomRent.SelectedValue != "0")
        {
            visitor.RoomRent = ddlRoomRent.SelectedValue;
        }
        else
        {
            visitor.RoomRent = "0";
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
            visitor.ElectricityBill = ddlelectricitybill.SelectedValue;
        }
        else
        {
            visitor.ElectricityBill = "0";
        }

        if (string.IsNullOrEmpty(txtfirstDate.Text))
        {
            visitor.TimePeriodFrom = System.DateTime.Now;
        }
        else
        { visitor.TimePeriodFrom = Convert.ToDateTime(txtfirstDate.Text); }
        if (string.IsNullOrEmpty(txtlastDate.Text))
        {
            visitor.TimePeriodTo = System.DateTime.Now;
        }
        else
        { visitor.TimePeriodTo = Convert.ToDateTime(txtlastDate.Text); }

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
        visitor.CreatedOn = DateTime.Now;
        visitor.CreatedBy = UserID;
        visitor.ModifyBy = UserID;
        visitor.ModifyOn = DateTime.Now;
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
        visitor.Identification = drpProofType.SelectedValue;


        if (ddlntypeofvisitor.SelectedValue == "0")
        {
            visitor.VisitorTypeID = 1;
        }
        else
        {
            visitor.VisitorTypeID = Convert.ToInt32(ddlntypeofvisitor.SelectedValue);
        }
        visitor.State = drpState.SelectedValue;
        visitor.Country = drpCountry.SelectedValue;
        visitor.City = drpCity.SelectedValue;
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
}