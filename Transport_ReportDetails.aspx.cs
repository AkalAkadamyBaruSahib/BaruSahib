using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using excel = Microsoft.Office.Interop.Excel;

public partial class Transport_ReporteDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["report"] != null)
        {
            downloadExcel();
        }
        if (!Page.IsPostBack)
        {
            BindZones();
        }
        
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "UploadedData.xls"));
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

    protected DataTable GetPendingDocumentReport()
    {
        DataTable dataTable = EmptyDataTable();
        DataRow dr = null;
        int ZoneID = Convert.ToInt16(ddlZone.SelectedValue);
        List<VechilesDocumentRelation> getDocuments = new List<VechilesDocumentRelation>();

        List<Vehicles> getVehicles = new List<Vehicles>();

        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        getVehicles = repository.GetVehiclesByZoneID(ZoneID, true);

        UsersRepository user = new UsersRepository(new AkalAcademy.DataContext());
        Incharge incharge = new Incharge();

        foreach (Vehicles vehicle in getVehicles)
        {
            getDocuments = repository.GetVechilesDocumentRelationByVehicleID(vehicle.ID);
            incharge = user.GetUsersByAcademyID(vehicle.AcademyID, 14).FirstOrDefault();
            if (getDocuments.Count != 0)
            {
                if (!getDocuments.Exists(document => document.TransportDocumentID == 1))
                {
                    dr = dataTable.NewRow();
                    dr["VehicleNumber"] = vehicle.Number;
                    dr["ZoneName"] = vehicle.Zone.ZoneName;
                    dr["AcademyName"] = vehicle.Academy.AcaName;
                    dr["DocumentName"] = "Registration";
                    if (incharge != null)
                    {
                        dr["TransportManager"] = incharge.InName;
                        dr["MobileNumber"] = incharge.InMobile;
                    }
                    dataTable.Rows.Add(dr);
                }

                if (!getDocuments.Exists(document => document.TransportDocumentID == 2))
                {
                    dr = dataTable.NewRow();
                    dr["VehicleNumber"] = vehicle.Number;
                    dr["ZoneName"] = vehicle.Zone.ZoneName;
                    dr["AcademyName"] = vehicle.Academy.AcaName;
                    dr["DocumentName"] = "Pollution";
                    if (incharge != null)
                    {
                        dr["TransportManager"] = incharge.InName;
                        dr["MobileNumber"] = incharge.InMobile;
                    }
                    dataTable.Rows.Add(dr);
                }

                if (!getDocuments.Exists(document => document.TransportDocumentID == 3))
                {
                    dr = dataTable.NewRow();
                    dr["VehicleNumber"] = vehicle.Number;
                    dr["ZoneName"] = vehicle.Zone.ZoneName;
                    dr["AcademyName"] = vehicle.Academy.AcaName;
                    dr["DocumentName"] = "Permit";
                    if (incharge != null)
                    {
                        dr["TransportManager"] = incharge.InName;
                        dr["MobileNumber"] = incharge.InMobile;
                    }
                    dataTable.Rows.Add(dr);
                }

                if (!getDocuments.Exists(document => document.TransportDocumentID == 4))
                {
                    dr = dataTable.NewRow();
                    dr["VehicleNumber"] = vehicle.Number;
                    dr["ZoneName"] = vehicle.Zone.ZoneName;
                    dr["AcademyName"] = vehicle.Academy.AcaName;
                    dr["DocumentName"] = "Tax";
                    if (incharge != null)
                    {
                        dr["TransportManager"] = incharge.InName;
                        dr["MobileNumber"] = incharge.InMobile;
                    }
                    dataTable.Rows.Add(dr);
                }

                if (!getDocuments.Exists(document => document.TransportDocumentID == 5))
                {
                    dr = dataTable.NewRow();
                    dr["VehicleNumber"] = vehicle.Number;
                    dr["ZoneName"] = vehicle.Zone.ZoneName;
                    dr["AcademyName"] = vehicle.Academy.AcaName;
                    dr["DocumentName"] = "Passing";
                    if (incharge != null)
                    {
                        dr["TransportManager"] = incharge.InName;
                        dr["MobileNumber"] = incharge.InMobile;
                    }
                    dataTable.Rows.Add(dr);
                }

                if (!getDocuments.Exists(document => document.TransportDocumentID == 6))
                {
                    dr = dataTable.NewRow();
                    dr["VehicleNumber"] = vehicle.Number;
                    dr["ZoneName"] = vehicle.Zone.ZoneName;
                    dr["AcademyName"] = vehicle.Academy.AcaName;
                    dr["DocumentName"] = "Insurance";
                    if (incharge != null)
                    {
                        dr["TransportManager"] = incharge.InName;
                        dr["MobileNumber"] = incharge.InMobile;
                    }
                    dataTable.Rows.Add(dr);
                }
            }
            else
            {
                dr = dataTable.NewRow();
                dr["VehicleNumber"] = vehicle.Number;
                dr["ZoneName"] = vehicle.Zone.ZoneName;
                dr["AcademyName"] = vehicle.Academy.AcaName;
                dr["DocumentName"] = "Registration";
                if (incharge != null)
                {
                    dr["TransportManager"] = incharge.InName;
                    dr["MobileNumber"] = incharge.InMobile;
                }
                dataTable.Rows.Add(dr);

                dr = dataTable.NewRow();
                dr["VehicleNumber"] = vehicle.Number;
                dr["ZoneName"] = vehicle.Zone.ZoneName;
                dr["AcademyName"] = vehicle.Academy.AcaName;
                dr["DocumentName"] = "Pollution";
                if (incharge != null)
                {
                    dr["TransportManager"] = incharge.InName;
                    dr["MobileNumber"] = incharge.InMobile;
                }
                dataTable.Rows.Add(dr);

                dr = dataTable.NewRow();
                dr["VehicleNumber"] = vehicle.Number;
                dr["ZoneName"] = vehicle.Zone.ZoneName;
                dr["AcademyName"] = vehicle.Academy.AcaName;
                dr["DocumentName"] = "Permit";
                if (incharge != null)
                {
                    dr["TransportManager"] = incharge.InName;
                    dr["MobileNumber"] = incharge.InMobile;
                }
                dataTable.Rows.Add(dr);

                dr = dataTable.NewRow();
                dr["VehicleNumber"] = vehicle.Number;
                dr["ZoneName"] = vehicle.Zone.ZoneName;
                dr["AcademyName"] = vehicle.Academy.AcaName;
                dr["DocumentName"] = "Tax";
                if (incharge != null)
                {
                    dr["TransportManager"] = incharge.InName;
                    dr["MobileNumber"] = incharge.InMobile;
                }
                dataTable.Rows.Add(dr);

                dr = dataTable.NewRow();
                dr["VehicleNumber"] = vehicle.Number;
                dr["ZoneName"] = vehicle.Zone.ZoneName;
                dr["AcademyName"] = vehicle.Academy.AcaName;
                dr["DocumentName"] = "Passing";
                if (incharge != null)
                {
                    dr["TransportManager"] = incharge.InName;
                    dr["MobileNumber"] = incharge.InMobile;
                }
                dataTable.Rows.Add(dr);

                dr = dataTable.NewRow();
                dr["VehicleNumber"] = vehicle.Number;
                dr["ZoneName"] = vehicle.Zone.ZoneName;
                dr["AcademyName"] = vehicle.Academy.AcaName;
                dr["DocumentName"] = "Insurance";
                if (incharge != null)
                {
                    dr["TransportManager"] = incharge.InName;
                    dr["MobileNumber"] = incharge.InMobile;
                }
                dataTable.Rows.Add(dr);
            }
        }
        return dataTable;
    }

    private DataTable EmptyDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("VehicleNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("ZoneName", typeof(string)));
        dt.Columns.Add(new DataColumn("AcademyName", typeof(string)));
        dt.Columns.Add(new DataColumn("DocumentName", typeof(string)));
        dt.Columns.Add(new DataColumn("TransportManager", typeof(string)));
        dt.Columns.Add(new DataColumn("MobileNumber", typeof(string)));
        //dt.Columns.Add(new DataColumn("Col7", typeof(string)));
        //dt.Columns.Add(new DataColumn("Col8", typeof(string)));
        return dt;

    }

    protected DataTable BindDatatable()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        if (ddlReport.SelectedValue == "1")
        {
            ds = DAL.DalAccessUtility.GetDataInDataSet("exec GetdocumentInfo");
            dt = ds.Tables[0];
        }
        else if (ddlReport.SelectedValue == "2")
        {
            dt = GetPendingDocumentReport();
        }
        
        return dt;
    }

    private void downloadExcel()
    {
        //Response.Redirect(Server.MapPath("~/VehicleDoc/Maintenance_schedule.xlsx"));

        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Maintenance_schedule.xlsx"));
        Response.ContentType = "application/ms-excel";
        Response.WriteFile(Server.MapPath("~/VehicleDoc/Maintenance_schedule.xlsx"));
        Response.End();
    }

    private void BindZones()
    {
        UsersRepository users = new UsersRepository(new AkalAcademy.DataContext());
        List<Zone> zone = new List<Zone>();
        ddlZone.DataSource = users.GetZoneByModuleID(2);
        ddlZone.DataValueField = "ZoneID";
        ddlZone.DataTextField = "ZoneName";
        ddlZone.DataBind();

    }
}