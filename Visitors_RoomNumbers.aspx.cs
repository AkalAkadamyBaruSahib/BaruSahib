using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Visitors_RoomNumbers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBuildingName();
        }
    }

    protected void BindBuildingName()
    {
        DataTable dtBuilding = new DataTable();

        dtBuilding = DAL.DalAccessUtility.GetDataInDataSet("select * from dbo.BuildingName").Tables[0];

        if (dtBuilding != null && dtBuilding.Rows.Count > 0)
        {
            drpBuildingName.DataSource = dtBuilding;
            drpBuildingName.DataValueField = "ID";
            drpBuildingName.DataTextField = "Name";
            drpBuildingName.DataBind();
            drpBuildingName.Items.Insert(0, new ListItem("--Select Building Name--", "0"));
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        RoomNumbers roomNumber = new RoomNumbers();
        DataTable BuildingExit = new DataTable();
        BuildingExit = DAL.DalAccessUtility.GetDataInDataSet("select * from RoomNumbers where BuildingID =" + drpBuildingName.SelectedValue + " and BuildingFloor =" + drpBuildingFloor.SelectedValue + " and Number ='" + txtRoomNumber.Text + "'").Tables[0];
        if (BuildingExit.Rows.Count > 0 && BuildingExit != null)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "<script>alert('Room Number Already Exits');</script>", false);
        }
        else
        {
            roomNumber.BuildingID = int.Parse(drpBuildingName.SelectedValue);
            roomNumber.Number = txtRoomNumber.Text;
            roomNumber.BuildingFloor = int.Parse(drpBuildingFloor.SelectedValue);
            roomNumber.NumOfBed = int.Parse(txtNoOfBed.Text);
            if (chkIsPermant.Checked)
            {
                roomNumber.IsPermanent = true;
            }
            else
            {
                roomNumber.IsPermanent = false;
            }
            VisitorUserRepository repo = new VisitorUserRepository(new AkalAcademy.DataContext());
            if (roomNumber.ID == 0)
            {
                repo.AddNewRooms(roomNumber);
            }
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "<script>alert('Record Saved Successfully');</script>", false);
        }
        Clear();
    }

    protected void Clear()
    {
        drpBuildingName.ClearSelection();
        txtRoomNumber.Text = "";
        drpBuildingFloor.ClearSelection();
        txtNoOfBed.Text = "";
        chkIsPermant.Checked = false;
    }
    protected void btnClr_Click(object sender, EventArgs e)
    {
        Clear();
    }
}