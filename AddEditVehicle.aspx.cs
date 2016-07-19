using AkalAcademy;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddVehicle : System.Web.UI.Page
{
    private static int UserTypeID = -1;
    private int VehicleID = -1;
    private int InchargeID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserTypeID"] != null)
        {
            UserTypeID = int.Parse(Session["UserTypeID"].ToString());
        }
        if (Session["InchargeID"] != null)
        {
            InchargeID = int.Parse(Session["InchargeID"].ToString());
           
        }
        if (Request.QueryString["VehicleID"] != null)
        {
            VehicleID = int.Parse(Request.QueryString["VehicleID"].ToString());
        }
        if (!Page.IsPostBack)
        {
            //txtFileNumber.Attributes.Add("readonly", "true");
            BindOilSlab();
            Model();
            BindMake();
            BindZone();
            bindTransportType();
            bindNormsList();
            BindDriver();
            BindConductor();
            if (VehicleID > 0)
            {
                LoadVehicleData();
            }
            //bindDocumentGrid();
        }
    }

    public void BindDriver()
    {
        DataSet dsDriver = new DataSet();
        dsDriver = DAL.DalAccessUtility.GetDataInDataSet("select ID,Name from  VehicleEmployee VE where EmployeeType = 1 order by  VE.Name asc");
        ddlDriverName.DataSource = dsDriver;
        ddlDriverName.DataValueField = "ID";
        ddlDriverName.DataTextField = "Name";
        ddlDriverName.DataBind();
        ddlDriverName.Items.Insert(0, new ListItem("--Select One--", "0"));

    }
    public void BindConductor()
    {
        DataSet dsConductor = new DataSet();
        dsConductor = DAL.DalAccessUtility.GetDataInDataSet("select ID, Name from  VehicleEmployee VE where EmployeeType = 2 order by  VE.Name asc");
        ddlConductorName.DataSource = dsConductor;
        ddlConductorName.DataValueField = "ID";
        ddlConductorName.DataTextField = "Name";
        ddlConductorName.DataBind();
        ddlConductorName.Items.Insert(0, new ListItem("--Select One--", "0"));

    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAcademy();
    }

    protected void BindAcademy()
    {
        UsersRepository usersRepository = new UsersRepository(new DataContext());
        List<Academy> AcaList = new List<Academy>();
        AcaList = usersRepository.GetAcademyByZoneID(int.Parse(ddlZone.SelectedValue), 2);



        //DataSet dsAca = new DataSet();
        //dsAca = DAL.DalAccessUtility.GetDataInDataSet("select AcaId,AcaName from Academy where Active=1 and ZoneId='" + ddlZone.SelectedValue + "'");
        ddlAcademy.DataSource = AcaList;
        ddlAcademy.DataValueField = "AcaId";
        ddlAcademy.DataTextField = "AcaName";
        ddlAcademy.DataBind();
        ddlAcademy.Items.Insert(0, new ListItem("--Select Academy--", "0"));
        ddlAcademy.SelectedIndex = 0;
    }

    protected void BindZone()
    {
        DataSet dsZone = new DataSet();
        UsersRepository usersRepository = new UsersRepository(new DataContext());
        List<Zone> ZoneList = new List<Zone>();
        if (UserTypeID == 13)
        {
            ZoneList = usersRepository.GetZoneByModuleID(2);
            // dsZone = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId,ZoneName  from Zone where Active=1");
        }
        else
        {
            ZoneList = usersRepository.GetZoneByInchargeID(InchargeID);
            //dsZone = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_GetZoneByUserID] '" + Session["InchargeID"] + "'");
        }
        ddlZone.DataSource = ZoneList;
        ddlZone.DataValueField = "ZoneId";
        ddlZone.DataTextField = "ZoneName";
        ddlZone.DataBind();
        ddlZone.Items.Insert(0, "Select Zone");
        ddlZone.SelectedIndex = 0;
    }

    private void SetNorms()
    {
        DataSet dsNorms = new DataSet();

        dsNorms = DAL.DalAccessUtility.GetDataInDataSet("select VNR.*,N.NormType from VechilesNormsRelation VNR INNER JOIN NORMS N ON N.ID=VNR.NormID where VehicleID=" + VehicleID);

        if (dsNorms != null && dsNorms.Tables[0] != null && dsNorms.Tables[0].Rows.Count > 0)
        {
            foreach (ListItem item in chkNorms.Items)
            {
                var foundId = dsNorms.Tables[0].Select("NormID= '" + item.Value + "'");
                if (foundId.Length > 0)
                {
                    item.Selected = true;
                }
            }
        }
    }


    private void GetNorms()
    {
        string selectedItems = String.Join(",", chkNorms.Items.OfType<ListItem>().Where(r => r.Selected).Select(r => r.Value));
    }

    private void bindNormsList()
    {
        DataSet dsNorms = new DataSet();

        dsNorms = DAL.DalAccessUtility.GetDataInDataSet("select * from Norms order by NormType asc");

        chkNorms.DataSource = dsNorms;
        chkNorms.DataValueField = "ID";
        chkNorms.DataTextField = "NormType";
        chkNorms.DataBind();

        if (VehicleID > 0)
        {
            SetNorms();
        }
    }

    private void bindTransportType()
    {
        DataSet dsZone = new DataSet();

        dsZone = DAL.DalAccessUtility.GetDataInDataSet("select * from TransportTypes");

        ddlTransportType.DataSource = dsZone;
        ddlTransportType.DataValueField = "ID";
        ddlTransportType.DataTextField = "Type";
        ddlTransportType.DataBind();
        ddlTransportType.Items.Insert(0, "Select Type");
        ddlTransportType.SelectedIndex = 0;
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Button bts = e.CommandSource as Button;
        Response.Write(bts.Parent.Parent.GetType().ToString());
        if (e.CommandName.ToLower() != "upload")
        {
            return;
        }
    }

    private void LoadVehicleData()
    {
        TransportUserRepository repo = new TransportUserRepository(new DataContext());
        Vehicles vehicle = repo.GetVehicleByVehicleID(VehicleID);

        if (!String.IsNullOrEmpty(vehicle.ZoneID.ToString()))
        {
            ddlZone.ClearSelection();
            ddlZone.Items.FindByValue(vehicle.ZoneID.ToString()).Selected = true;
        }

        BindAcademy();
        if (!String.IsNullOrEmpty(vehicle.AcademyID.ToString()))
        {
            ddlAcademy.ClearSelection();
            ddlAcademy.Items.FindByValue(vehicle.AcademyID.ToString()).Selected = true;
        }

        ddlTransportType.ClearSelection();
        ddlTransportType.Items.FindByValue(vehicle.TypeID.ToString()).Selected = true;

        if (!String.IsNullOrEmpty(vehicle.DriverID.ToString()))
        {
            ddlDriverName.ClearSelection();
            ddlDriverName.Items.FindByValue(vehicle.DriverID.ToString()).Selected = true;
        }

        if (!String.IsNullOrEmpty(vehicle.ConductorID.ToString()))
        {
            ddlConductorName.ClearSelection();
            ddlConductorName.Items.FindByValue(vehicle.ConductorID.ToString()).Selected = true;
        }

        //string VehicleNumber = txtVehicleNo1.Text + "-" + txtVehicleNo2.Text + "-" + txtVehicleNo3.Text + "-" + txtVehicleNo4.Text;
        string[] VehicleNumber = vehicle.Number.Split('-');
        if (VehicleNumber.Length == 4)
        {
            txtVehicleNo1.Text = VehicleNumber[0];
            txtVehicleNo2.Text = VehicleNumber[1];
            txtVehicleNo3.Text = VehicleNumber[2];
            txtVehicleNo4.Text = VehicleNumber[3];
        }
        else
        {
            txtVehicleNo1.Text = VehicleNumber[0];
            txtVehicleNo2.Text = VehicleNumber[1];
            txtVehicleNo4.Text = VehicleNumber[2];
        }
 
        txtVehicleNo1.Enabled = true;
        txtVehicleNo2.Enabled = true;
        txtVehicleNo3.Enabled = true;
        txtVehicleNo4.Enabled = true;
      
        ddlAcademy.Enabled = true;
        ddlZone.Enabled = true;

        OwnerName.Text = vehicle.OwnerName;
        txtOwnerNo.Text = vehicle.OwnerNumber;
        txtSitting.Text = vehicle.Sitter.ToString();
        lblHeading.Text = vehicle.Number;
        chkTemp.Checked = Convert.ToBoolean(vehicle.IsTemporary);
       
        txtFileNumber.Text = vehicle.FileNumber == string.Empty ? "Contract-" + vehicle.Number : vehicle.FileNumber;
        txtEngineNumber.Text = vehicle.EngineNumber;
        txtChassisNumber.Text = vehicle.ChassisNumber;
        ddlMake.ClearSelection();
        ddlMake.SelectedValue = vehicle.Make;
        ddlModel.ClearSelection();
        ddlModel.SelectedValue = vehicle.Model;
        chkWrittenContract.Checked = vehicle.WrittenContract.ToString() != string.Empty ? Convert.ToBoolean(vehicle.WrittenContract.ToString()) : false;

        ddlPeriodOfContract.ClearSelection();
        ddlPeriodOfContract.SelectedValue = vehicle.PeriodOfContract.ToString();

        txtContractDieselRate.Text = vehicle.ContractDieselRate.ToString();
        GetCurrentOilPrice();

        if (vehicle.OilSlab!=null)
        {
            ddlOilSlab.ClearSelection();
            ddlOilSlab.SelectedValue = vehicle.OilSlab.ToString();
        }

        ddlFrontLeft.ClearSelection();
        ddlFrontLeft.SelectedValue = vehicle.FrontLeftTyreCondition;

        ddlFrontRight.ClearSelection();
        ddlFrontRight.SelectedValue = vehicle.FrontRightTyreCondition;

        ddlNumberOfTyres.ClearSelection();
        ddlNumberOfTyres.SelectedValue = vehicle.NumberOfTypres.ToString();



        if (vehicle.NumberOfTypres.ToString() == "4")
        {
            trRear.Visible = true;
            ddlRearLeft.ClearSelection();
            ddlRearLeft.SelectedValue = vehicle.RearLeftTyreCondition.ToString();

            ddlRearRight.ClearSelection();
            ddlRearRight.SelectedValue = vehicle.RearRightTyreCondition.ToString();
        }
        else if (vehicle.NumberOfTypres.ToString() == "6")
        {
            trRear.Visible = true;
            trRear2.Visible = true;

            ddlRearLeft.ClearSelection();
            ddlRearLeft.SelectedValue = vehicle.RearLeftTyreCondition.ToString();

            ddlRearRight.ClearSelection();
            ddlRearRight.SelectedValue = vehicle.RearRightTyreCondition.ToString();

            ddlRearLeft2.ClearSelection();
            ddlRearLeft2.SelectedValue = vehicle.RearLeftTyre2Condition.ToString();

            ddlRearRight2.ClearSelection();
            ddlRearRight2.SelectedValue = vehicle.RearRightTyre2Condition.ToString();
        }

        txtKm.Text = vehicle.KMPerDay.ToString();
        DisableControl();
    }

    

    protected void btnSaveChanges_Click(object sender, EventArgs e)
    {
        string NormsId = String.Join(",", chkNorms.Items.OfType<ListItem>().Where(r => r.Selected).Select(r => r.Value));

        string VehicleNumber = txtVehicleNo1.Text.Trim().ToUpper() + "-" + txtVehicleNo2.Text.Trim().ToUpper() + "-" + txtVehicleNo3.Text.Trim().ToUpper() + "-" + txtVehicleNo4.Text.Trim().ToUpper();

        if (txtVehicleNo3.Text == string.Empty)
        { VehicleNumber = txtVehicleNo1.Text.Trim().ToUpper() + "-" + txtVehicleNo2.Text.Trim().ToUpper() + "-" + txtVehicleNo4.Text.Trim().ToUpper(); }

        string rearL = null;
        string rearR = null; string rearL2 = null; string rearR2 = null;

        if (ddlNumberOfTyres.SelectedValue == "4")
        {
            rearL = ddlRearLeft.SelectedValue;
            rearR = ddlRearRight.SelectedValue;
        }
        else if (ddlNumberOfTyres.SelectedValue == "6")
        {
            rearL = ddlRearLeft.SelectedValue;
            rearR = ddlRearRight.SelectedValue;
            rearL2 = ddlRearLeft2.SelectedValue;
            rearR2 = ddlRearRight2.SelectedValue;
        }

        if (txtContractDieselRate.Text == "")
        {
            txtContractDieselRate.Text = "0.00";
        }
        //if (VehicleNumber.ToUpper().Contains('T') && !chkTemp.Checked)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Is vehicle on temporary registration.?');", true);
        //}
        string fileName = "Contract_" + VehicleNumber;
        try
        {
            DataSet isVehicleExists = DAL.DalAccessUtility.GetDataInDataSet("select * from vehicles where number ='" + VehicleNumber + "'");

            if (isVehicleExists.Tables[0].Rows.Count == 0 || VehicleID > -1)
            {
                
                long rowaffected = DAL.DalAccessUtility.ExecuteNonQuery("exec [USP_InsertUpdateVehicle] " + VehicleID + "," + ddlZone.SelectedValue + "," + ddlAcademy.SelectedValue + "," + ddlTransportType.SelectedValue + ",'" + VehicleNumber + "','" + OwnerName.Text + "','" + txtOwnerNo.Text + "','" + txtSitting.Text + "','" + NormsId + "',1," + chkTemp.Checked + ",'" + fileName + "','" + txtEngineNumber.Text.ToUpper() + "','" + txtChassisNumber.Text.ToUpper() + "','" + ddlMake.SelectedValue + "','" + ddlModel.SelectedValue + "'," + chkWrittenContract.Checked + ",'" + ddlPeriodOfContract.SelectedValue + "','" + txtContractDieselRate.Text + "','" + ddlOilSlab.SelectedValue + "','" + ddlFrontRight.SelectedValue + "','" + ddlFrontLeft.SelectedValue + "','" + rearR + "','" + rearL + "','" + txtKm.Text + "','" + rearR2 + "','" + rearL2 + "'," + ddlNumberOfTyres.SelectedValue + "," + ddlConductorName.SelectedValue + "," + ddlDriverName.SelectedValue);

                if (VehicleID == -1)
                {
                    DataSet ds = DAL.DalAccessUtility.GetDataInDataSet("select top 1 Id from Vehicles order by ID desc");
                    VehicleID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }

                //if (rowaffected > 0)
                //{
                //    if (SaveDocuments())
                //    {
                Response.Redirect("Transport_VehicleDetails.aspx");
                //    }
                //}
            }
            //Response.Redirect("Transport_VehicleDetails.aspx");
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Vehicle Number Already Exists');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(" + ex.Message + ");", true);
            return;
        }
    }

    private void BindMake()
    {
        DataTable dtMake = DAL.DalAccessUtility.GetDataInDataSet("Select * from VehicleMake order by name asc").Tables[0];
        ddlMake.DataTextField = "Name";
        ddlMake.DataValueField = "ID";
        ddlMake.DataSource = dtMake;
        ddlMake.DataBind();
        ddlMake.Items.Insert(0, new ListItem("--Select Make--", "0"));
    }

    private void Model()
    {
        int currentYear = DateTime.Now.Year;
        for (int i = 1995; i <= currentYear; i++)
        {
            ddlModel.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
    }

    private void BindOilSlab()
    {
        DataTable dtMake = DAL.DalAccessUtility.GetDataInDataSet("Select * from OilSlab order by value asc").Tables[0];
        ddlOilSlab.DataTextField = "DisplayValue";
        ddlOilSlab.DataValueField = "Value";
        ddlOilSlab.DataSource = dtMake;
        ddlOilSlab.DataBind();
        ddlOilSlab.Items.Insert(0, new ListItem("--Select Oil Increase Slab--", "0"));
    }

    private void GetCurrentOilPrice()
    {
        DataTable dtPrice = DAL.DalAccessUtility.GetDataInDataSet("Select * from DieselPetrolPrice WHERE AcaID=" + ddlAcademy.SelectedValue + " order by createdOn desc").Tables[0];
        if (dtPrice.Rows.Count > 0)
        {
            if (VehicleID == -1)
            {
                txtContractDieselRate.Text = dtPrice.Rows[0]["DieselPrice"].ToString();
                txtContractDieselRate.Enabled = false;
            }
            else
            {
                lblCurrentOilRate.Text = dtPrice.Rows[0]["DieselPrice"].ToString();
            }
        }
    }

    protected void ddlAcademy_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCurrentOilPrice();
    }
    protected void ddlTransportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisableControl();
    }
    protected void ddlNumberOfTyres_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNumberOfTyres.SelectedValue == "2")
        {
            trfront.Visible = true;
            trRear.Visible = false;
            trRear2.Visible = false;
        }
        else if (ddlNumberOfTyres.SelectedValue == "4")
        {
            trfront.Visible = true;
            trRear.Visible = true;
            trRear2.Visible = false;
        }
        else
        {
            trfront.Visible = true;
            trRear.Visible = true;
            trRear2.Visible = true;
        }
    }

    public void DisableControl()
    {
        if (ddlTransportType.SelectedValue == "1")
        {
            chkWrittenContract.Enabled = false;
            ddlPeriodOfContract.Enabled = false;
            ddlOilSlab.Enabled = false;
            txtKm.Enabled = false;
            dNorms.Visible = true;

        }
        else if (ddlTransportType.SelectedValue == "6" || ddlTransportType.SelectedValue == "7" || ddlTransportType.SelectedValue == "8" || ddlTransportType.SelectedValue == "9")
        {
            dNorms.Visible = false;
        }
        else if (ddlTransportType.SelectedValue == "5")
        {
            chkWrittenContract.Enabled = false;
            ddlPeriodOfContract.Enabled = false;
            txtContractDieselRate.Enabled = false;
            ddlOilSlab.Enabled = false;
            txtKm.Enabled = false;
            dNorms.Visible = false;
            ddlDriverName.Enabled = false;
            ddlConductorName.Enabled = false;
            txtOwnerNo.Enabled = false;
            OwnerName.Enabled = false;
            OwnerName.Text = "Trust";
            txtSitting.Text = "2";
            txtSitting.Enabled = false;
        }
        else if (ddlTransportType.SelectedValue == "3")
        {
            chkWrittenContract.Enabled = false;
            ddlPeriodOfContract.Enabled = false;
            txtContractDieselRate.Enabled = false;
            ddlOilSlab.Enabled = false;
            txtKm.Enabled = false;
            dNorms.Visible = false;

        }
        else
        {
            chkWrittenContract.Enabled = true;
            ddlPeriodOfContract.Enabled = true;
            ddlOilSlab.Enabled = true;
            txtKm.Enabled = true;
            dNorms.Visible = true;
        }
    
    }

}