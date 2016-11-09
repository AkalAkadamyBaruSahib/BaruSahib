﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        Response.ClearContent();
        Response.Buffer = true;
        if (drpFilterData.SelectedValue == "1")
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "VisitorReportByDate.xls"));
        }
        else if (drpFilterData.SelectedValue == "2")
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "ReportOnEmptyRoomList.xls"));
        }
        else
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "VisitorsReportByPlace.xls"));
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
        from = txtCheckInDate.Text == string.Empty ? DateTime.MinValue.ToShortDateString() : txtCheckInDate.Text;
        if (drpFilterData.SelectedValue == "1")
        {
            dt = DAL.DalAccessUtility.GetDataInDataSet("exec GetVisitorsReports '" + txtCheckInDate.Text + "','" + txtCheckOutDate.Text + "'").Tables[0];
        }
        else if (drpFilterData.SelectedValue == "2")
        {
            dt = DAL.DalAccessUtility.GetDataInDataSet("exec GetVacantRoomListByBuilding '" + drpBuildingName.SelectedValue + "'").Tables[0];
        }
        else
        {
            dt = DAL.DalAccessUtility.GetDataInDataSet(getSqlString()).Tables[0];
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
        if (drpFilterData.SelectedValue == "1")
        {
            divCheckInDate.Visible = true;
            divEmptyRooms.Visible = false;
            divPlaces.Visible = false;
        }
        else if (drpFilterData.SelectedValue == "2")
        {
            divCheckInDate.Visible = false;
            divEmptyRooms.Visible = true;
            divPlaces.Visible = false;
        }
        else if (drpFilterData.SelectedValue == "3")
        {
            divCheckInDate.Visible = false;
            divEmptyRooms.Visible = false;
            divPlaces.Visible = true;
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
        sql = "SELECT distinct V.Name AS [Visitor Name], VT.VisitorType,V.ContactNumber,V.VisitorAddress,V.PurposeOfVisit,B.Name As BuildingName,RN.BuildingFloor AS BuildingFloor,RN.Number As RoomNumber,V.RoomRent,V.AdmissionNumber, " +
            "V.VisitorReference,V.VehicleNo,V.Identification,V.TimePeriodFrom As CheckIn,V.TimePeriodTo As CheckOut FROM Visitors V INNER jOIN VisitorRoomNumbers RM ON RM.VisitorID=V.ID INNER JOIN VisitorType VT ON VT.ID= V.VisitorTypeID " +
            "INNER JOIN BuildingName B ON B.ID= V.BuildingID INNER JOIN RoomNumbers RN ON RM.RoomNumberID = RN.ID WHERE V.VisitorTypeID=" + (int)TypeEnum.VisitoryType.Visitor;

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
}