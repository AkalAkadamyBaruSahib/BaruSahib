using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;

public partial class Visitors_Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindBuildingName();
            BindCountry();
        }
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
       // BindDatatable();
        Response.ClearContent();
        Response.Buffer = true;
        if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.AccordingDate).ToString())
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "VisitorReportByDate" + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".xls"));
        }
        else if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.ViewVacantRoomList).ToString())
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ReportOnEmptyRoomList" + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".xls"));
        }
        else if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.VisitorsReportByPlaces).ToString())
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "VisitorsReportByPlace" + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".xls"));
        }
        else if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.ViewBookedRoomList).ToString())
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ReportBookedRoomList" + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".xls"));
        }
        else if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.RoomStatus).ToString())
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "RoomStatus" + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".xls"));
        }
        else if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.PermanentRoomDetailReport).ToString())
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "PermanentRoomDetails" + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".xls"));
        }
        else if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.DailyVisitorStatusReport).ToString())
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "DailyVisitorDetails" + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".xls"));
        }
        else if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.RoomSummaryReport).ToString())
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "RoomSummaryReport" + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".xls"));
        }

        Response.ContentType = "application/ms-excel";
        DataTable dt = BindDatatable();
        string str = string.Empty;
        foreach (DataColumn dtcol in dt.Columns)
        {
            Response.Write(str + dtcol.ColumnName);
            str = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            str = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                Response.Write(str + Convert.ToString(dr[j]));
                str = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }

    protected DataTable BindDatatable()
    {
        DataTable dt = new DataTable();
        string from = string.Empty;
        string FileName = string.Empty;
        from = txtCheckInDate.Text == string.Empty ? DateTime.MinValue.ToShortDateString() : txtCheckInDate.Text;
        if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.AccordingDate).ToString())
        {
            dt = DAL.DalAccessUtility.GetDataInDataSet("exec [GetVisitorsReports] '" + txtCheckInDate.Text + "','" + txtCheckOutDate.Text + "'").Tables[0];
        }
        else if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.ViewVacantRoomList).ToString())
        {
            dt = DAL.DalAccessUtility.GetDataInDataSet("exec [GetVacantRoomListByBuilding] '" + drpBuildingName.SelectedValue + "'").Tables[0];
        }
        else if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.VisitorsReportByPlaces).ToString())
        {
            dt = DAL.DalAccessUtility.GetDataInDataSet(getSqlString()).Tables[0];
        }
        else if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.ViewBookedRoomList).ToString())
        {
            dt = DAL.DalAccessUtility.GetDataInDataSet("exec [GetBookedRoomListByBuilding] '" + drpBuildingName.SelectedValue + "'").Tables[0];
        }
        else if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.RoomStatus).ToString())
        {
            dt = DAL.DalAccessUtility.GetDataInDataSet("exec [GetRoomStatusByBuilding] '" + drpBuildingName.SelectedValue + "'").Tables[0];
        }
        else if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.PermanentRoomDetailReport).ToString())
        {
            dt = DAL.DalAccessUtility.GetDataInDataSet("exec [GetPermanentRoomReport] '" + drpBuildingName.SelectedValue + "'").Tables[0];
        }
        else if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.DailyVisitorStatusReport).ToString())
        {
            dt = DAL.DalAccessUtility.GetDataInDataSet("exec [GetDailyWiseVisitorsReports] '" + txtCheckInDate.Text + "'").Tables[0];
        }
        else if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.RoomSummaryReport).ToString())
        {
            dt = GetRoomsSummaryReport();
        }
        return dt;
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
            drpBuildingName.Items.Insert(0, new ListItem("All Building", "-1"));
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
            drpCountry.Items.Insert(0, new ListItem("All Country", "-1"));
        }
        dtTemp.Clear();
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
            drpCity.Items.Insert(0, new ListItem("--Select City--", "-1"));
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
            drpState.Items.Insert(0, new ListItem("--Select State--", "-1"));
        }
    }
    protected void drpFilterData_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.AccordingDate).ToString())
        {
            divCheckInDate.Visible = true;
            divEmptyRooms.Visible = false;
            divPlaces.Visible = false;
            txtCheckOutDate.Visible = true;
            lblCheckOut.Visible = true;
        }
        else if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.RoomStatus).ToString() || drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.ViewVacantRoomList).ToString() || drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.ViewBookedRoomList).ToString() || drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.PermanentRoomDetailReport).ToString())
        {
            divCheckInDate.Visible = false;
            divEmptyRooms.Visible = true;
            divPlaces.Visible = false;
        }
        else if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.VisitorsReportByPlaces).ToString())
        {
            divCheckInDate.Visible = false;
            divEmptyRooms.Visible = false;
            divPlaces.Visible = true;
        }
        else if (drpFilterData.SelectedValue == ((int)TypeEnum.VisitorReportTypes.DailyVisitorStatusReport).ToString())
        {
            divCheckInDate.Visible = true;
            divEmptyRooms.Visible = false;
            divPlaces.Visible = false;
            txtCheckOutDate.Visible = false;
            lblCheckOut.Visible = false;
        }
        else
        {
            divCheckInDate.Visible = false;
            divEmptyRooms.Visible = false;
            divPlaces.Visible = false;
        }
    }

    private string getSqlString()
    {
        string sql = string.Empty;
        sql = "SELECT distinct V.Name AS [Visitor Name], VT.VisitorType,V.ContactNumber,V.VisitorAddress,CO.CountryName,S.StateName,C.CityName,V.PurposeOfVisit,V.RoomRent AS LangerSewa,V.AdmissionNumber, " +
            "V.VisitorReference,V.VehicleNo,V.Identification,V.TimePeriodFrom As CheckIn,V.TimePeriodTo As CheckOut FROM Visitors V  INNER JOIN VisitorType VT ON VT.ID= V.VisitorTypeID " +
            "INNER JOIN Country CO ON CO.CountryId = V.Country	INNER JOIN State S ON S.StateId = V.State INNER JOIN City C ON C.CityId = V.City WHERE V.VisitorTypeID=" + (int)TypeEnum.VisitoryType.Visitor;

        if (drpCountry.SelectedValue != "-1")
        {
            sql += " AND V.Country=" + drpCountry.SelectedValue;
        }

        if (drpState.SelectedValue != "-1")
        {
            sql += " AND V.State=" + drpState.SelectedValue;
        }

        if (drpCity.SelectedValue != "-1")
        {
            sql += " AND V.City=" + drpCity.SelectedValue;
        }

        return sql;
    }


    public DataTable GetRoomsSummaryReport()
    {
        DataTable dtRoomsSummary = new DataTable();
        dtRoomsSummary.Columns.Add("Name_Of_Building");
        dtRoomsSummary.Columns.Add("Total_Rooms", typeof(System.Int32));
        dtRoomsSummary.Columns.Add("Permanent_Rooms", typeof(System.Int32));
        dtRoomsSummary.Columns.Add("Temporary_Rooms", typeof(System.Int32));
        dtRoomsSummary.Columns.Add("Booked_Temporary_Rooms", typeof(System.Int32));
        dtRoomsSummary.Columns.Add("Booked_Permanent_Rooms", typeof(System.Int32)); 
        dtRoomsSummary.Columns.Add("Total_Vacant_Rooms", typeof(System.Int32));
        dtRoomsSummary.Columns.Add("Remarks", typeof(System.String));
    
        DataRow dr;
        VisitorUserRepository repository = new VisitorUserRepository(new AkalAcademy.DataContext());
        List<BuildingName> building = repository.GetBuildingNameList();
        DataTable dtTotalNumberOfRooms;
        int sumOfRooms = 0;
        string numbers = string.Empty;
        foreach (BuildingName aca in building)
        {
            numbers = string.Empty;
            dr = dtRoomsSummary.NewRow();
            dr["Name_Of_Building"] = aca.Name;
            dtTotalNumberOfRooms = DAL.DalAccessUtility.GetDataInDataSet("select count(id) as count from RoomNumbers where  BuildingID = " + aca.ID).Tables[0];
            if (dtTotalNumberOfRooms.Rows.Count > 0)
            {
                dr["Total_Rooms"] = dtTotalNumberOfRooms.Rows[0]["count"].ToString();
                sumOfRooms += Convert.ToInt32(dtTotalNumberOfRooms.Rows[0]["count"].ToString());
            }

            dr["Permanent_Rooms"] = repository.GetPermanentRoomList(aca.ID);
            dr["Temporary_Rooms"] = repository.GetTemporaryRoomList(aca.ID);
            dr["Booked_Temporary_Rooms"] = repository.GetBookedTemporaryRoomListCount(aca.ID);
            dr["Booked_Permanent_Rooms"] = repository.GetBookedPermanentRoomListCount(aca.ID); 
            dr["Total_Vacant_Rooms"] = repository.GetAvailableRoomListCount(aca.ID);
            dr["Remarks"] = string.Empty;

            dtRoomsSummary.Rows.Add(dr);

        }

        if (dtRoomsSummary.Rows.Count > 0)
        {
            dr = dtRoomsSummary.NewRow();

            dr["Name_Of_Building"] = "Total";
            dr["Total_Rooms"] = Convert.ToInt32(dtRoomsSummary.Compute("SUM(Total_Rooms)", string.Empty));
            dr["Permanent_Rooms"] = Convert.ToInt32(dtRoomsSummary.Compute("SUM(Permanent_Rooms)", string.Empty));
            dr["Temporary_Rooms"] = Convert.ToInt32(dtRoomsSummary.Compute("SUM(Temporary_Rooms)", string.Empty));
            dr["Booked_Temporary_Rooms"] = Convert.ToInt32(dtRoomsSummary.Compute("SUM(Booked_Temporary_Rooms)", string.Empty));
            dr["Booked_Permanent_Rooms"] = Convert.ToInt32(dtRoomsSummary.Compute("SUM(Booked_Permanent_Rooms)", string.Empty));
            dr["Total_Vacant_Rooms"] = Convert.ToInt32(dtRoomsSummary.Compute("SUM(Total_Vacant_Rooms)", string.Empty));
            dtRoomsSummary.Rows.Add(dr);
        }
        return dtRoomsSummary;
    }
}