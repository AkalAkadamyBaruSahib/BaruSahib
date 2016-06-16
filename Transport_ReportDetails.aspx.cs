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
            BindAllZones();
        }
        
   }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        if (ddlReport.SelectedValue == "1")
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "DailyDocumentReport.xls"));
        }
        else if (ddlReport.SelectedValue == "2")
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "PendingDocumentsReport(" + ddlZone.SelectedItem + ").xls"));
        }
        else
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "SummaryReport(" + ddlALLZone.SelectedItem + ").xls"));
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


    public DataTable GetTransportSummaryReport()
    {
        DataTable dtVehicleSummary = new DataTable();
        dtVehicleSummary.Columns.Add("AcademyName");
        dtVehicleSummary.Columns.Add("TotalNumberOfVehicles", typeof(System.Int32));
        dtVehicleSummary.Columns.Add("RC", typeof(System.Int32));
        dtVehicleSummary.Columns.Add("Insurance", typeof(System.Int32));
        dtVehicleSummary.Columns.Add("Permit", typeof(System.Int32));
        dtVehicleSummary.Columns.Add("Tax", typeof(System.Int32));
        dtVehicleSummary.Columns.Add("Passing", typeof(System.Int32));
        //dtVehicleSummary.Columns.Add("DLType");
        dtVehicleSummary.Columns.Add("WrittenContract", typeof(System.Int32));
        dtVehicleSummary.Columns.Add("Pollution", typeof(System.Int32));
         dtVehicleSummary.Columns.Add("Total", typeof(System.Int32));
        //dtVehicleSummary.Columns.Add("Norms");
        dtVehicleSummary.Columns.Add("TransportManager", typeof(System.String));
        //dtVehicleSummary.Columns.Add("VehicleDetails");

        DataRow dr;
        int zoneid = Convert.ToInt32(ddlALLZone.SelectedValue);
        UsersRepository repository = new UsersRepository(new AkalAcademy.DataContext());
        List<Academy> academies = repository.GetAcademyByZoneID(zoneid, 2);
        DataTable dtTotalNumberOfVehicles;
        int sumOfVehicles = 0;
        string numbers = string.Empty;
        foreach (Academy aca in academies)
        {
            numbers = string.Empty;
            dr = dtVehicleSummary.NewRow();
            dr["AcademyName"] = aca.AcaName;
            dtTotalNumberOfVehicles = DAL.DalAccessUtility.GetDataInDataSet("select count(id) as count from Vehicles where IsApproved=1 and  AcademyID = " + aca.AcaID).Tables[0];
            if (dtTotalNumberOfVehicles.Rows.Count > 0)
            {
                dr["TotalNumberOfVehicles"] = dtTotalNumberOfVehicles.Rows[0]["count"].ToString();
                sumOfVehicles += Convert.ToInt32(dtTotalNumberOfVehicles.Rows[0]["count"].ToString());
            }

            TransportUserRepository trepository = new TransportUserRepository(new AkalAcademy.DataContext());
            List<Vehicles> vehicles = trepository.GetAllVehiclesByAcademyID(aca.AcaID);

            DataTable dtTransportManagerName = new DataTable();

            DataTable getDocuments = new DataTable();
            int RC = 0;
            int Insurance = 0;
            int Permit = 0;
            int Passing = 0;
            int Pollution = 0;
            int Tax = 0;
            int WrittenContract = 0;
            DateTime expiryDate ;

            foreach (Vehicles v in vehicles)
            {

                getDocuments = DAL.DalAccessUtility.GetDataInDataSet("select * from [dbo].[VechilesDocumentRelation] where VehicleID in (select id from Vehicles where AcademyID=" + v.AcademyID + " AND ID=" + v.ID + ")").Tables[0];

                DataRow[] drrow = getDocuments.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Registration));

                if (getDocuments.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Registration)).Count() == 0)
                {
                    RC += 1;
                }
                else
                {
                    expiryDate = Convert.ToDateTime(drrow[0]["DocumentEndDate"]);
                    if (expiryDate <= DateTime.Now)
                    {
                        RC += 1;
                    }
                }

                DataRow[] drrowIn = getDocuments.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Insurance));
                if (getDocuments.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Insurance)).Count() == 0)
                {
                    Insurance += 1;
                }
                else
                {
                    expiryDate = Convert.ToDateTime(drrowIn[0]["DocumentEndDate"]);
                    if (expiryDate <= DateTime.Now)
                    {
                        Insurance += 1;
                    }
                }


                if (v.TypeID == 1 || v.TypeID == 2 || v.TypeID == 3 || v.TypeID == 4 || v.TypeID == 7 || v.TypeID == 9)
                {
                    DataRow[] drrowPa = getDocuments.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Passing));
                    if (getDocuments.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Passing)).Count() == 0)
                    {
                        Passing += 1;
                    }
                    else
                    {
                        expiryDate = Convert.ToDateTime(drrowPa[0]["DocumentEndDate"]);
                        if (expiryDate <= DateTime.Now)
                        {
                            Passing += 1;
                        }
                    }
                }


                if (v.TypeID == 1 || v.TypeID == 2 || v.TypeID == 3 || v.TypeID == 4 || v.TypeID == 7 || v.TypeID == 9)
                {
                    DataRow[] drrowPer = getDocuments.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Permit));
                    if (getDocuments.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Permit)).Count() == 0)
                    {
                        Permit += 1;
                    }
                    else
                    {
                        expiryDate = Convert.ToDateTime(drrowPer[0]["DocumentEndDate"]);
                        if (expiryDate <= DateTime.Now)
                        {
                            Permit += 1;
                        }
                    }
                }


                DataRow[] drrowPol = getDocuments.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Pollution));
                if (getDocuments.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Pollution)).Count() == 0)
                {
                    Pollution += 1;
                }
                else
                {
                    expiryDate = Convert.ToDateTime(drrowPol[0]["DocumentEndDate"]);
                    if (expiryDate <= DateTime.Now)
                    {
                        Pollution += 1;
                    }
                }

                if (v.TypeID == 1 || v.TypeID == 2 || v.TypeID == 3 || v.TypeID == 4 || v.TypeID == 7 || v.TypeID == 9)
                {
                    DataRow[] drrowTax = getDocuments.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Tax));
                    if (getDocuments.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Tax)).Count() == 0)
                    {
                        Tax += 1;
                    }
                    else
                    {
                        expiryDate = Convert.ToDateTime(drrowTax[0]["DocumentEndDate"]);
                        if (expiryDate <= DateTime.Now)
                        {
                            Tax += 1;
                        }
                    }
                }


                if (v.TypeID == 1 || v.TypeID == 2 || v.TypeID == 3 || v.TypeID == 4 || v.TypeID == 7 || v.TypeID == 9)
                {
                    DataRow[] drrowWritte = getDocuments.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.WrittenContract));
                    if (getDocuments.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.WrittenContract)).Count() == 0)
                    {
                        WrittenContract += 1;
                    }
                    else
                    {
                        expiryDate = Convert.ToDateTime(drrowWritte[0]["DocumentEndDate"]);
                        if (expiryDate <= DateTime.Now)
                        {
                            WrittenContract += 1;
                        }
                    }
                }
            }
                       


            dr["RC"] = RC;
            dr["Insurance"] = Insurance;
            dr["Permit"] = Permit;
            dr["Tax"] = Tax;
            dr["Passing"] = Passing;
            dr["Pollution"] = Pollution;
            dr["WrittenContract"] = WrittenContract;
            dr["Total"] = (RC + Insurance + Permit + Tax + Passing + Pollution + WrittenContract);

            dtTransportManagerName = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct INC.* FROM Vehicles V " +
            "INNER JOIN dbo.AcademyAssignToEmployee A ON A.AcaID=V.AcademyID " +
            "INNER JOIN Incharge INC ON INC.InchargeID=A.EMPID " +
            "INNER JOIN Academy Ac ON Ac.AcaId=A.AcaID WHERE INC.ModuleID=2  AND INC.UserTypeId=14 AND A.AcaID=" + aca.AcaID).Tables[0];

            if (dtTransportManagerName.Rows.Count > 0)
            {
                dr["TransportManager"] = String.Join(",", dtTransportManagerName.AsEnumerable().Select(x => x.Field<string>("InName").ToString()).ToArray());
            }
            else
            {
                dr["TransportManager"] = "No Transport Assign";
            }
            dtVehicleSummary.Rows.Add(dr);

        }

        if (dtVehicleSummary.Rows.Count > 0)
        {
            dr = dtVehicleSummary.NewRow();

            dr["AcademyName"] = "Total No. Of Vehicles";
            dr["TotalNumberOfVehicles"] = Convert.ToInt32(dtVehicleSummary.Compute("SUM(TotalNumberOfVehicles)", string.Empty));
            dr["RC"] = Convert.ToInt32(dtVehicleSummary.Compute("SUM(RC)", string.Empty));
            dr["Insurance"] = Convert.ToInt32(dtVehicleSummary.Compute("SUM(Insurance)", string.Empty));
            dr["Permit"] = Convert.ToInt32(dtVehicleSummary.Compute("SUM(Permit)", string.Empty));
            dr["Tax"] = Convert.ToInt32(dtVehicleSummary.Compute("SUM(Tax)", string.Empty));
            dr["Passing"] = Convert.ToInt32(dtVehicleSummary.Compute("SUM(Passing)", string.Empty));
            dr["WrittenContract"] = Convert.ToInt32(dtVehicleSummary.Compute("SUM(WrittenContract)", string.Empty));
            dr["Pollution"] = Convert.ToInt32(dtVehicleSummary.Compute("SUM(Pollution)", string.Empty));
          dr["Total"] = Convert.ToInt32(dtVehicleSummary.Compute("SUM(Total)", string.Empty));
            dtVehicleSummary.Rows.Add(dr);
        }
        return dtVehicleSummary;
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


                var row = getDocuments.Where(document => document.TransportDocumentID == 1).FirstOrDefault();
                if (row != null && row.DocumentEndDate <= DateTime.Now)
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


                var row2 = getDocuments.Where(document => document.TransportDocumentID == 2).FirstOrDefault();
                if (row2 != null && row2.DocumentEndDate <= DateTime.Now)
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

                if (vehicle.TypeID == 1 || vehicle.TypeID == 2 || vehicle.TypeID == 3 || vehicle.TypeID == 4 || vehicle.TypeID == 7 || vehicle.TypeID == 9)
                {
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
                }


                var row3 = getDocuments.Where(document => document.TransportDocumentID == 3).FirstOrDefault();
                if (vehicle.TypeID == 1 || vehicle.TypeID == 2 || vehicle.TypeID == 3 || vehicle.TypeID == 4 || vehicle.TypeID == 7 || vehicle.TypeID == 9)
                {
                    if (row3 != null && row3.DocumentEndDate <= DateTime.Now)
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
                }

                if (vehicle.TypeID == 1 || vehicle.TypeID == 2 || vehicle.TypeID == 3 || vehicle.TypeID == 4 || vehicle.TypeID == 7 || vehicle.TypeID == 9)
                {
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
                }


                var row4 = getDocuments.Where(document => document.TransportDocumentID == 4).FirstOrDefault();
                if (row4 != null && row4.DocumentEndDate <= DateTime.Now)
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


                if (vehicle.TypeID == 1 || vehicle.TypeID == 2 || vehicle.TypeID == 3 || vehicle.TypeID == 4 || vehicle.TypeID == 7 || vehicle.TypeID == 9)
                {
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
                }


                var row5 = getDocuments.Where(document => document.TransportDocumentID == 5).FirstOrDefault();
                if (vehicle.TypeID == 1 || vehicle.TypeID == 2 || vehicle.TypeID == 3 || vehicle.TypeID == 4 || vehicle.TypeID == 7 || vehicle.TypeID == 9)
                {
                    if (row5 != null && row5.DocumentEndDate <= DateTime.Now)
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


                var row6 = getDocuments.Where(document => document.TransportDocumentID == 6).FirstOrDefault();
                if (row6 != null && row6.DocumentEndDate <= DateTime.Now)
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

                if (!getDocuments.Exists(document => document.TransportDocumentID == 9))
                {
                    dr = dataTable.NewRow();
                    dr["VehicleNumber"] = vehicle.Number;
                    dr["ZoneName"] = vehicle.Zone.ZoneName;
                    dr["AcademyName"] = vehicle.Academy.AcaName;
                    dr["DocumentName"] = "Route Map";
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
                if (vehicle.TypeID == 1 || vehicle.TypeID == 2 || vehicle.TypeID == 3 || vehicle.TypeID == 4 || vehicle.TypeID == 7 || vehicle.TypeID == 9)
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


                if (vehicle.TypeID == 1 || vehicle.TypeID == 2 || vehicle.TypeID == 3 || vehicle.TypeID == 4 || vehicle.TypeID == 7 || vehicle.TypeID == 9)
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

                dr = dataTable.NewRow();
                dr["VehicleNumber"] = vehicle.Number;
                dr["ZoneName"] = vehicle.Zone.ZoneName;
                dr["AcademyName"] = vehicle.Academy.AcaName;
                dr["DocumentName"] = "Route Map";
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
            
            try
            {
                dt = ds.Tables[0].AsEnumerable().Where(x => x.Field<DateTime>("CreatedOnDate").Date >= Convert.ToDateTime(txtfirstDate.Text).Date &&
                    x.Field<DateTime>("CreatedOnDate").Date <= Convert.ToDateTime(txtlastDate.Text).Date).CopyToDataTable();
            }
            catch (Exception ex)
            {
                dt.Clear();
            }
        }

        else if (ddlReport.SelectedValue == "2")
        {
            dt = GetPendingDocumentReport();
        }
        else
        {
            dt = GetTransportSummaryReport();
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

    private void BindAllZones()
    {
        DataSet allzone = new DataSet();
        allzone = DAL.DalAccessUtility.GetDataInDataSet("select * from dbo.Zone");
        ddlALLZone.DataSource = allzone;
        ddlALLZone.DataValueField = "ZoneID";
        ddlALLZone.DataTextField = "ZoneName";
        ddlALLZone.DataBind();
        ddlALLZone.Items.Insert(0, new ListItem("--Select--", "0"));
   }
}