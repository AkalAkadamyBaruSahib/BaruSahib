using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AkalAcademy;

public partial class Transport_VechicleRouteMap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
     {
        if (!Page.IsPostBack)
        {
            if ((Request.QueryString["AcaIDEdit"] !=null) && (Request.QueryString["RouteNoEdit"] !=null))
            {
                getRouteDetails((Request.QueryString["AcaIDEdit"].ToString()), Request.QueryString["RouteNoEdit"].ToString());
                btnEdit.Visible = true;
                btnSave.Visible = false;
            }
            else
            {
                BindZones();
            }
           
        } BindRouteDetails();
    }

    private void BindZones()
    {
        UsersRepository usersRepository = new UsersRepository(new DataContext());
        List<Zone> ZoneList = new List<Zone>();
        ZoneList = usersRepository.GetZoneByModuleID(2);
        drpZoneName.DataSource = ZoneList;
        drpZoneName.DataValueField = "ZoneId";
        drpZoneName.DataTextField = "ZoneName";
        drpZoneName.DataBind();
        drpZoneName.Items.Insert(0, "Select Zone");
      
    }

    protected void drpZoneName_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAcademy();
    }

    protected void BindAcademy()
    {
        UsersRepository usersRepository = new UsersRepository(new DataContext());
        List<Academy> AcaList = new List<Academy>();
        AcaList = usersRepository.GetAcademyByZoneID(int.Parse(drpZoneName.SelectedValue), 2);
        drpAcaName.DataSource = AcaList;
        drpAcaName.DataValueField = "AcaId";
        drpAcaName.DataTextField = "AcaName";
        drpAcaName.DataBind();
        drpAcaName.Items.Insert(0, new ListItem("--Select Academy--", "0"));
       
    }

    protected void BindVechicleList()
    {
        DataSet dsMat = new DataSet();
        dsMat = DAL.DalAccessUtility.GetDataInDataSet("select ID,Number from Vehicles where IsApproved=1 and  AcademyID IN (" + drpAcaName.SelectedValue + ") order by Number");
        lstVehicles.DataSource = dsMat;
        lstVehicles.DataValueField = "ID";
        lstVehicles.DataTextField = "Number";
        lstVehicles.DataBind();

    }

    protected void drpAcaName_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVechicleList();
    }

    protected void btnSave_Click1(object sender, EventArgs e)
    {
        DataSet dsVExist = new DataSet();
        dsVExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct RouteNo,VehicleNumber,A.AcaName from VechiclesRouteMap VR inner join Academy A on A.acaid= VR.AcaID where VehicleNumber='" + lstVehicles.SelectedValue + "'");
        if (dsVExist.Tables[0].Rows.Count > 0)
        {
            if (dsVExist.Tables[0].Rows[0]["VehicleNumber"].ToString() != null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This vechicle already running on the Route No " + dsVExist.Tables[0].Rows[0]["RouteNo"].ToString() + " in Academy " + dsVExist.Tables[0].Rows[0]["AcaName"].ToString() + "');", true);
            }
        }
        else
        {
            VechiclesRouteMap vechiclesroutemap = new VechiclesRouteMap();
            foreach (ListItem item in lstVehicles.Items)
            {
                if (item.Selected)
                {
                    vechiclesroutemap.ZoneID = Convert.ToInt32(drpZoneName.SelectedValue);
                    vechiclesroutemap.AcaID = Convert.ToInt32(drpAcaName.SelectedValue);
                    vechiclesroutemap.RouteNo = txtRouteNo.Text;
                    vechiclesroutemap.VehicleNumber = item.Value;
                    TransportUserRepository repo = new TransportUserRepository(new AkalAcademy.DataContext());
                    repo.AddVechiclesRouteMap(vechiclesroutemap);
                }
              ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Vehicle Route Map  Create Successfully.');", true);
             
            }    ClearTextBox();
            BindRouteDetails();
        }
    }

    protected void BindRouteDetails()
    {
        string sql = "select distinct RouteNo,AcaName,ZoneName,A.AcaID " +
        "from VechiclesRouteMap VR " +
        "inner join Academy A on VR.AcaID = A.AcaId " +
        "inner join Zone Z on VR.ZoneID=Z.ZoneId " +
        "inner join Vehicles V on V.ID = VR.VehicleNumber";

        string VehicleNumberSql = "DECLARE @VehicleNumbers varchar(200) " +
        "SELECT @VehicleNumbers = COALESCE(@VehicleNumbers + ', ', '') + Number " +
        "FROM " +
        "VechiclesRouteMap VR " +
        "INNER JOIN Vehicles V on V.ID = VR.VehicleNumber " +
        "where VR.RouteNo=[RouteNo] AND VR.AcaID=[AcaID] " +
        "SELECT @VehicleNumbers AS Number ";

        string replaceQuery = string.Empty;
        DataSet dsRouteDetails = new DataSet();
        DataSet dsNumbers = new DataSet();
        dsRouteDetails = DAL.DalAccessUtility.GetDataInDataSet(sql);
        divRoutMapDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='20%'>Zone Name</th>";
        ZoneInfo += "<th width='20%'>Academy Name</th>";
        ZoneInfo += "<th width='20%'>Route Number No</th>";
        ZoneInfo += "<th width='20%'>Vechicle Numbers</th>";
        ZoneInfo += "<th width='20%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsRouteDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='20%'>" + dsRouteDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td>";
            ZoneInfo += "<td width='20%'>" + dsRouteDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td>";
            ZoneInfo += "<td width='20%'>" + dsRouteDetails.Tables[0].Rows[i]["RouteNo"].ToString() + "</td>";


            replaceQuery = VehicleNumberSql.Replace("[RouteNo]", dsRouteDetails.Tables[0].Rows[i]["RouteNo"].ToString());
            replaceQuery = replaceQuery.Replace("[AcaID]", dsRouteDetails.Tables[0].Rows[i]["AcaID"].ToString());

            dsNumbers = DAL.DalAccessUtility.GetDataInDataSet(replaceQuery);
            {
                ZoneInfo += "<td width='20%'>" + dsNumbers.Tables[0].Rows[0]["Number"].ToString() + "</td>";
            }

            ZoneInfo += "<td class='center' width='20%'>";
            ZoneInfo += "<a class='btn btn-info' href='Transport_VechicleRouteMap.aspx?AcaIDEdit=" + dsRouteDetails.Tables[0].Rows[i]["AcaID"].ToString() + " &RouteNoEdit=" + dsRouteDetails.Tables[0].Rows[i]["RouteNo"].ToString() +"'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i> Edit";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";

        divRoutMapDetails.InnerHtml = ZoneInfo.ToString();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string VAcad = Request.QueryString["AcaIDEdit"];
        string VRouteId = Request.QueryString["RouteNoEdit"];
        DataSet dsVExist = new DataSet();
        dsVExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct RouteNo,VehicleNumber,A.AcaName from VechiclesRouteMap VR inner join Academy A on A.acaid= VR.AcaID where VehicleNumber='" + lstVehicles.SelectedValue + "'");
        if (dsVExist.Tables[0].Rows.Count > 0)
        {
            if (dsVExist.Tables[0].Rows[0]["VehicleNumber"].ToString() != null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('This vechicle already running on the Route No " + dsVExist.Tables[0].Rows[0]["RouteNo"].ToString() + " in Academy " + dsVExist.Tables[0].Rows[0]["AcaName"].ToString() + "');", true);
            }
          
        }
        else
        {
            DAL.DalAccessUtility.ExecuteNonQuery("Delete from VechiclesRouteMap  where  AcaID=" + VAcad + " and RouteNo=" + VRouteId + "");
            foreach (ListItem item in lstVehicles.Items)
            {
                if (item.Selected)
                {
                    DAL.DalAccessUtility.ExecuteNonQuery("insert into VechiclesRouteMap(AcaID,Zoneid,RouteNo,VehicleNumber) values('" + drpAcaName.SelectedValue + "','" + drpZoneName.SelectedValue + "','" + txtRouteNo.Text + "','" + item.Value + "')");
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Vehicle Route Map  Update Successfully.');", true);
            ClearTextBox();
            btnSave.Visible = true;
            btnEdit.Visible = false;
        } 
         BindRouteDetails();
    }

    private void getRouteDetails(string vacaid, string vrouteid)
    {
        DataSet dsGetRouteDetail = new DataSet();
        dsGetRouteDetail = DAL.DalAccessUtility.GetDataInDataSet("SELECT  A.AcaId,VR.RouteNo,VR.VehicleNumber,VR.ZoneID FROM Academy A  inner join VechiclesRouteMap VR on A.AcaId= VR.AcaID  where A.AcaID = '" + vacaid + "' and VR.RouteNo ='" + vrouteid + "'");
        if (dsGetRouteDetail.Tables[0].Rows.Count > 0)
        {
            BindZones();
            drpZoneName.ClearSelection();
            drpZoneName.Items.FindByValue(dsGetRouteDetail.Tables[0].Rows[0]["ZoneID"].ToString()).Selected = true;
            BindAcademy();
            drpAcaName.ClearSelection();
            drpAcaName.Items.FindByValue(dsGetRouteDetail.Tables[0].Rows[0]["AcaId"].ToString()).Selected = true;
            txtRouteNo.Text = dsGetRouteDetail.Tables[0].Rows[0]["RouteNo"].ToString();
            BindVechicleList();
            foreach (ListItem item in lstVehicles.Items)
            {
                if (item.Value == dsGetRouteDetail.Tables[0].Rows[0]["VehicleNumber"].ToString())
                {
                    item.Selected = true;
                }
            }
            
            btnEdit.Visible = true;
            btnSave.Visible = false;

        }
    }

    public void ClearTextBox()
    {
        lstVehicles.Items.Clear();
        //drpZoneName.ClearSelection();
        //drpAcaName.ClearSelection();
        txtRouteNo.Text = "";
    
    }

}

       