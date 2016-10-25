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
            BindFutureAllZones();
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
        else if (ddlReport.SelectedValue == "3")
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "SummaryReport(" + ddlALLZone.SelectedItem + ").xls"));
        }
        else
        {
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "FutureExpireReport(" + ddlFutureZone.SelectedItem + ").xls"));
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
        dtVehicleSummary.Columns.Add("RouteMap", typeof(System.Int32));
        dtVehicleSummary.Columns.Add("DL", typeof(System.Int32));
        dtVehicleSummary.Columns.Add("Camera", typeof(System.Int32));
        dtVehicleSummary.Columns.Add("FemaleConductor", typeof(System.Int32));
        dtVehicleSummary.Columns.Add("SpeedGoverner", typeof(System.Int32));
        dtVehicleSummary.Columns.Add("GPS", typeof(System.Int32));
        dtVehicleSummary.Columns.Add("YellowColor", typeof(System.Int32));
        dtVehicleSummary.Columns.Add("MaleConductor", typeof(System.Int32));
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
            VehicleEmployee getDL = new VehicleEmployee();
            DataTable getNorms = new DataTable();
            int RC = 0;
            int Insurance = 0;
            int Permit = 0;
            int Passing = 0;
            int Pollution = 0;
            int RouteMap = 0;
            int Tax = 0;
            int WrittenContract = 0;
            int DL = 0;
            int Camera = 0;
            int FemaleConductor=0;
            int SpeedGoverner = 0;
            int GPS = 0;
            int MaleConductor = 0;
            int YellowColor =0;
            DateTime expiryDate;

            foreach (Vehicles v in vehicles)
            {

                //getDocuments = DAL.DalAccessUtility.GetDataInDataSet("select * from [dbo].[VechilesDocumentRelation] where VehicleID in (select id from Vehicles where AcademyID=" + v.AcademyID + " AND ID=" + v.ID + ")").Tables[0];

                getDocuments = DAL.DalAccessUtility.GetDataInDataSet("select * from [dbo].[VechilesDocumentRelation] where VehicleID = " + v.ID).Tables[0];

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


                if (v.TypeID == (int)(TypeEnum.TransportType.Trust) || v.TypeID == (int)(TypeEnum.TransportType.Contractual) || v.TypeID == (int)(TypeEnum.TransportType.MaterialMovementVehicle) || v.TypeID == (int)(TypeEnum.TransportType.DailyWages) || v.TypeID == (int)(TypeEnum.TransportType.Ambulance) || v.TypeID == (int)(TypeEnum.TransportType.CivilEquipmentVehicle))
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


                if (v.TypeID == (int)(TypeEnum.TransportType.Trust) || v.TypeID == (int)(TypeEnum.TransportType.Contractual) || v.TypeID == (int)(TypeEnum.TransportType.MaterialMovementVehicle) || v.TypeID == (int)(TypeEnum.TransportType.DailyWages) || v.TypeID == (int)(TypeEnum.TransportType.Ambulance) || v.TypeID == (int)(TypeEnum.TransportType.CivilEquipmentVehicle))
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

                if (v.TypeID == (int)(TypeEnum.TransportType.Trust) || v.TypeID == (int)(TypeEnum.TransportType.Contractual) || v.TypeID == (int)(TypeEnum.TransportType.MaterialMovementVehicle) || v.TypeID == (int)(TypeEnum.TransportType.DailyWages) || v.TypeID == (int)(TypeEnum.TransportType.Ambulance) || v.TypeID == (int)(TypeEnum.TransportType.CivilEquipmentVehicle))
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


                if (v.TypeID == (int)(TypeEnum.TransportType.Contractual))
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

                if (v.TypeID == (int)(TypeEnum.TransportType.Contractual) || v.TypeID == (int)(TypeEnum.TransportType.Trust))
                {
                    if (getDocuments.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.RouteMap)).Count() == 0)
                    {
                        RouteMap += 1;
                    }
                }

                
                if (v.TypeID != (int)(TypeEnum.TransportType.Twowheeler))
                {
                    getDL = trepository.GetEmployeeByVehicleID(v.ID, (int)TypeEnum.TransportVehicleEmployeeType.Driver);
                    if (getDL != null)
                    {
                        if (getDL.DLType != (int)TypeEnum.TransportDLType.HMV &&
                            getDL.DLType != (int)TypeEnum.TransportDLType.HTV &&
                            getDL.DLType != (int)TypeEnum.TransportDLType.PSVBUS &&
                            getDL.DLType != (int)TypeEnum.TransportDLType.TRANS &&
                            getDL.DLType != (int)TypeEnum.TransportDLType.CHASSIS)
                        {
                            DL += 1;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(getDL.DLValidity))
                            {
                                expiryDate = Convert.ToDateTime(getDL.DLValidity);
                                if (expiryDate <= DateTime.Now)
                                {
                                    DL += 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        DL += 1;
                    }
                }

                getNorms = DAL.DalAccessUtility.GetDataInDataSet("select NormID,VehicleID from [dbo].[VechilesNormsRelation] where VehicleID =" + v.ID).Tables[0];
                if (v.TypeID == (int)(TypeEnum.TransportType.Trust) || v.TypeID == (int)(TypeEnum.TransportType.Contractual) || v.TypeID == (int)(TypeEnum.TransportType.DailyWages) || v.TypeID == (int)(TypeEnum.TransportType.Ambulance))
                {
                    if (getNorms.Select("NormID = " + (int)(TypeEnum.TransportNormsType.Camera)).Count() == 0)
                    {
                        Camera += 1;
                    }

                    if (getNorms.Select("NormID = " + (int)(TypeEnum.TransportNormsType.FemaleConductor)).Count() == 0)
                    {
                        FemaleConductor += 1;
                    }

                    if (getNorms.Select("NormID = " + (int)(TypeEnum.TransportNormsType.SpeedGoverner)).Count() == 0)
                    {
                        SpeedGoverner += 1;
                    }

                    if (getNorms.Select("NormID = " + (int)(TypeEnum.TransportNormsType.GPS)).Count() == 0)
                    {
                        GPS += 1;
                    }

                    if (getNorms.Select("NormID = " + (int)(TypeEnum.TransportNormsType.YellowColor)).Count() == 0)
                    {
                        YellowColor += 1;
                    }
                }
                if (v.TypeID == (int)(TypeEnum.TransportType.Trust) || v.TypeID == (int)(TypeEnum.TransportType.Contractual) || v.TypeID == (int)(TypeEnum.TransportType.DailyWages))
                {
                    if (v.Sitter > 17)
                    {
                        if ((v.ConductorID == null) || (v.ConductorID == 0))
                        {
                            MaleConductor += 1;
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
            dr["RouteMap"] = RouteMap;
            dr["DL"] = DL;
            dr["Camera"] = Camera;
            dr["FemaleConductor"] = FemaleConductor;
            dr["SpeedGoverner"] = SpeedGoverner;
            dr["GPS"] = GPS;
            dr["MaleConductor"] = MaleConductor;
            dr["YellowColor"] = YellowColor;
        
            dr["Total"] = (RC + Insurance + Permit + Tax + Passing + Pollution + WrittenContract + DL + Camera + FemaleConductor + SpeedGoverner + GPS + MaleConductor + YellowColor + RouteMap);

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
            dr["RouteMap"] = Convert.ToInt32(dtVehicleSummary.Compute("SUM(RouteMap)", string.Empty));
            dr["DL"] = Convert.ToInt32(dtVehicleSummary.Compute("SUM(DL)", string.Empty));
            dr["Camera"] = Convert.ToInt32(dtVehicleSummary.Compute("SUM(Camera)", string.Empty));
            dr["FemaleConductor"] = Convert.ToInt32(dtVehicleSummary.Compute("SUM(FemaleConductor)", string.Empty));
            dr["SpeedGoverner"] = Convert.ToInt32(dtVehicleSummary.Compute("SUM(SpeedGoverner)", string.Empty));
            dr["GPS"] = Convert.ToInt32(dtVehicleSummary.Compute("SUM(GPS)", string.Empty));
            dr["YellowColor"] = Convert.ToInt32(dtVehicleSummary.Compute("SUM(YellowColor)", string.Empty));
            dr["MaleConductor"] = Convert.ToInt32(dtVehicleSummary.Compute("SUM(MaleConductor)", string.Empty));
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
        List<VechilesNormsRelation> getNorms = new List<VechilesNormsRelation>();
        VehicleEmployee getDL = new VehicleEmployee();

        List<Vehicles> getVehicles = new List<Vehicles>();

        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        getVehicles = repository.GetVehiclesByZoneID(ZoneID, true);

        UsersRepository user = new UsersRepository(new AkalAcademy.DataContext());
        Incharge incharge = new Incharge();


        foreach (Vehicles vehicle in getVehicles)
        {
            getDocuments = repository.GetVechilesDocumentRelationByVehicleID(vehicle.ID);
            getDL = repository.GetEmployeeByVehicleID(vehicle.ID, (int)TypeEnum.TransportVehicleEmployeeType.Driver);
            getNorms = repository.GetVechilesNormsRelationByVehicleID(vehicle.ID);
            incharge = user.GetUsersByAcademyID(vehicle.AcademyID, 14).FirstOrDefault();
            var PendingDocumentName = string.Empty;
            var PendingNormsName = string.Empty;
            var PendingDL = string.Empty;
            var PendingConductor = string.Empty;
            int count = 0;
            if (vehicle.TypeID == (int)(TypeEnum.TransportType.Trust) || vehicle.TypeID == (int)(TypeEnum.TransportType.Contractual) || vehicle.TypeID == (int)(TypeEnum.TransportType.DailyWages) || vehicle.TypeID == (int)(TypeEnum.TransportType.Ambulance))
            {
                if (vehicle.Sitter > 17)
                {
                    if ((vehicle.ConductorID == null) || (vehicle.ConductorID == 0))
                    {
                        PendingConductor = "Pending";

                    }
                    else
                    {
                        PendingConductor = "";
                    }
                }
                else
                {
                    PendingConductor = "";
                }
            }
            else
            {
                PendingConductor = "";
            }

            if (getDocuments.Count != 0)
            {
                if (!getDocuments.Exists(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Registration))
                {
                    PendingDocumentName += "Registration" + ",";
                    count += 1;
                }
                else
                {
                    var row = getDocuments.Where(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Registration).FirstOrDefault();
                    if (row != null && row.DocumentEndDate <= DateTime.Now)
                    {
                        PendingDocumentName += "Registration" + ",";
                        count += 1;
                    }
                }

                if (!getDocuments.Exists(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Pollution))
                {
                    PendingDocumentName += "Pollution" + ",";
                    count += 1;
                }
                else
                {
                    var row2 = getDocuments.Where(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Pollution).FirstOrDefault();
                    if (row2 != null && row2.DocumentEndDate <= DateTime.Now)
                    {
                        PendingDocumentName += "Pollution" + ",";
                        count += 1;
                    }
                }

                if (vehicle.TypeID == (int)(TypeEnum.TransportType.Trust) || vehicle.TypeID == (int)(TypeEnum.TransportType.Contractual) || vehicle.TypeID == (int)(TypeEnum.TransportType.MaterialMovementVehicle) || vehicle.TypeID == (int)(TypeEnum.TransportType.DailyWages) || vehicle.TypeID == (int)(TypeEnum.TransportType.Ambulance) || vehicle.TypeID == (int)(TypeEnum.TransportType.CivilEquipmentVehicle))
                {
                    if (!getDocuments.Exists(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Permit))
                    {
                        PendingDocumentName += "Permit" + ",";
                        count += 1;
                    }
                    else
                    {
                        var row3 = getDocuments.Where(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Permit).FirstOrDefault();
                        if (row3 != null && row3.DocumentEndDate <= DateTime.Now)
                        {
                            PendingDocumentName += "Permit" + ",";
                            count += 1;
                        }
                    }
                }

                if (vehicle.TypeID == (int)(TypeEnum.TransportType.Trust) || vehicle.TypeID == (int)(TypeEnum.TransportType.Contractual) || vehicle.TypeID == (int)(TypeEnum.TransportType.MaterialMovementVehicle) || vehicle.TypeID == (int)(TypeEnum.TransportType.DailyWages) || vehicle.TypeID == (int)(TypeEnum.TransportType.Ambulance) || vehicle.TypeID == (int)(TypeEnum.TransportType.CivilEquipmentVehicle))
                {
                    if (!getDocuments.Exists(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Tax))
                    {
                        PendingDocumentName += "Tax" + ",";
                        count += 1;
                    }
                    else
                    {
                        var row4 = getDocuments.Where(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Tax).FirstOrDefault();
                        if (row4 != null && row4.DocumentEndDate <= DateTime.Now)
                        {
                            PendingDocumentName += "Tax" + ",";
                            count += 1;
                        }
                    }
                }

                if (vehicle.TypeID == (int)(TypeEnum.TransportType.Trust) || vehicle.TypeID == (int)(TypeEnum.TransportType.Contractual) || vehicle.TypeID == (int)(TypeEnum.TransportType.MaterialMovementVehicle) || vehicle.TypeID == (int)(TypeEnum.TransportType.DailyWages) || vehicle.TypeID == (int)(TypeEnum.TransportType.Ambulance) || vehicle.TypeID == (int)(TypeEnum.TransportType.CivilEquipmentVehicle))
                {
                    if (!getDocuments.Exists(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Passing))
                    {
                        PendingDocumentName += "Passing" + ",";
                        count += 1;
                    }
                    else
                    {
                        var row5 = getDocuments.Where(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Passing).FirstOrDefault();
                        if (row5 != null && row5.DocumentEndDate <= DateTime.Now)
                        {
                            PendingDocumentName += "Passing" + ",";
                            count += 1;
                        }
                    }
                }


                if (!getDocuments.Exists(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Insurance))
                {
                    PendingDocumentName += "Insurance" + ",";
                    count += 1;
                }
                else
                {
                    var row6 = getDocuments.Where(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Insurance).FirstOrDefault();
                    if (row6 != null && row6.DocumentEndDate <= DateTime.Now)
                    {
                        PendingDocumentName += "Insurance" + ",";
                        count += 1;
                    }
                }

                if (vehicle.TypeID == (int)(TypeEnum.TransportType.Contractual))
                {
                    if (!getDocuments.Exists(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.WrittenContract))
                    {
                        PendingDocumentName += "WrittenContract" + ",";
                        count += 1;
                    }
                    else
                    {
                        var row7 = getDocuments.Where(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.WrittenContract).FirstOrDefault();
                        if (row7 != null && row7.DocumentEndDate <= DateTime.Now)
                        {
                            PendingDocumentName += "WrittenContract" + ",";
                            count += 1;
                        }
                    }
                }

                if (vehicle.TypeID == (int)(TypeEnum.TransportType.Contractual) || vehicle.TypeID == (int)(TypeEnum.TransportType.Trust))
                {
                    if (!getDocuments.Exists(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.RouteMap))
                    {
                        PendingDocumentName += "RouteMap" + ",";
                        count += 1;
                    }
                }
            }
            else
            {
                if (vehicle.TypeID == (int)(TypeEnum.TransportType.MaterialMovementVehicle) || vehicle.TypeID == (int)(TypeEnum.TransportType.DailyWages) || vehicle.TypeID == (int)(TypeEnum.TransportType.Ambulance) || vehicle.TypeID == (int)(TypeEnum.TransportType.CivilEquipmentVehicle))
                {
                    PendingDocumentName = "Registration,Pollution,Permit,Tax,Passing,Insurance,";
                    count = 6;
                }
                else if (vehicle.TypeID == (int)(TypeEnum.TransportType.Contractual))
                {
                    PendingDocumentName = "Registration,Pollution,Permit,Tax,Passing,Insurance,WrittenContract,RouteMap,";
                    count = 8;
                }
                else if (vehicle.TypeID == (int)(TypeEnum.TransportType.Trust))
                {
                    PendingDocumentName = "Registration,Pollution,Permit,Tax,Passing,Insurance,RouteMap,";
                    count = 7;
                }
                else
                {
                    PendingDocumentName = "Registration,Pollution,Insurance,";
                    count = 3;
                }
            }

            if (getDL!= null)
            {
                if (vehicle.TypeID != (int)(TypeEnum.TransportType.Twowheeler))
                {
                    if (getDL.DLType == Convert.ToInt32(TypeEnum.TransportDLType.HMV) ||
                        getDL.DLType == Convert.ToInt32(TypeEnum.TransportDLType.CHASSIS) ||
                        getDL.DLType == Convert.ToInt32(TypeEnum.TransportDLType.TRANS) ||
                        getDL.DLType == Convert.ToInt32(TypeEnum.TransportDLType.PSVBUS) ||
                        getDL.DLType == Convert.ToInt32(TypeEnum.TransportDLType.HTV))
                    {
                        if (!string.IsNullOrEmpty(getDL.DLValidity))
                        {
                            var row8 = Convert.ToDateTime(getDL.DLValidity) <= DateTime.Now;
                            if (row8 != null && (row8))
                            {
                                PendingDL = "Pending";
                            }
                            else
                            {
                                PendingDL = "";
                            }
                        }
                    }
                    else
                    {
                        PendingDL = "Pending";
                    }
                }
                else
                {
                    PendingDL = "";
                }
            }
            else
            {
                if (vehicle.TypeID != (int)(TypeEnum.TransportType.Twowheeler))
                {
                    PendingDL = "Pending";
                }
                else
                {
                    PendingDL = "";
                }
            }
            if (getNorms.Count != 0)
            {
                if (vehicle.TypeID == (int)(TypeEnum.TransportType.Trust) || vehicle.TypeID == (int)(TypeEnum.TransportType.Contractual) || vehicle.TypeID == (int)(TypeEnum.TransportType.DailyWages))
                {
                    if ((!getNorms.Exists(norm => norm.NormID == (int)(TypeEnum.TransportNormsType.Camera))))
                    {
                        PendingNormsName += "Camera" + ",";
                    }
                    if ((!getNorms.Exists(norm => norm.NormID == (int)(TypeEnum.TransportNormsType.FemaleConductor))))
                    {
                        PendingNormsName += "Female Conductor" + ",";
                    }
                    if ((!getNorms.Exists(norm => norm.NormID == (int)(TypeEnum.TransportNormsType.GPS))))
                    {
                        PendingNormsName += "GPS" + ",";
                    }
                    if ((!getNorms.Exists(norm => norm.NormID == (int)(TypeEnum.TransportNormsType.SpeedGoverner))))
                    {
                        PendingNormsName += "Speed Governer" + ",";
                    }
                    if ((!getNorms.Exists(norm => norm.NormID == (int)(TypeEnum.TransportNormsType.YellowColor))))
                    {
                        PendingNormsName += "Yellow Color" + ",";
                    }
                }
            }
            else
            {
                if (vehicle.TypeID == (int)(TypeEnum.TransportType.Trust) || vehicle.TypeID == (int)(TypeEnum.TransportType.Contractual) || vehicle.TypeID == (int)(TypeEnum.TransportType.DailyWages))
                {
                    PendingNormsName = "Camera,Female Conductor,GPS,Speed Governer,Yellow Color,";
                }
                else
                {
                    PendingNormsName = "";
                }
            }

            if (PendingDocumentName.Length > 0)
            {
                PendingDocumentName = PendingDocumentName.Substring(0, PendingDocumentName.Length - 1);
            }
            if (PendingNormsName.Length > 0)
            {
                PendingNormsName = PendingNormsName.Substring(0, PendingNormsName.Length - 1);
            }
            dr = dataTable.NewRow();
            dr["VehicleNumber"] = vehicle.Number;
            dr["VehicleType"] = vehicle.TransportTypes.Type;
            dr["ZoneName"] = vehicle.Zone.ZoneName;
            dr["AcademyName"] = vehicle.Academy.AcaName;
            dr["DocumentName"] = PendingDocumentName;
            dr["TotalPendingDocument"] = count;
            dr["HeavyDrivingLicence"] = PendingDL;
            dr["NormsName"] = PendingNormsName;
            dr["Conductor"] = PendingConductor;
            if (incharge != null)
            {
                dr["TransportManager"] = incharge.InName;
                dr["MobileNumber"] = incharge.InMobile;
            }
            dataTable.Rows.Add(dr);
        }
        return dataTable;
    }

    private DataTable EmptyDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("VehicleNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("VehicleType", typeof(string)));
        dt.Columns.Add(new DataColumn("ZoneName", typeof(string)));
        dt.Columns.Add(new DataColumn("AcademyName", typeof(string)));
        dt.Columns.Add(new DataColumn("DocumentName", typeof(string)));
        dt.Columns.Add(new DataColumn("TotalPendingDocument", typeof(string)));
        dt.Columns.Add(new DataColumn("HeavyDrivingLicence", typeof(string)));
        dt.Columns.Add(new DataColumn("NormsName", typeof(string)));
        dt.Columns.Add(new DataColumn("Conductor", typeof(string)));
        dt.Columns.Add(new DataColumn("TransportManager", typeof(string)));
        dt.Columns.Add(new DataColumn("MobileNumber", typeof(string)));

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
        else if (ddlReport.SelectedValue == "3")
        {
            dt = GetTransportSummaryReport();
        }
        else
        {
            dt = GetTransportFutureExpireReport();
        }
        return dt;
    }

    protected DataTable GetTransportFutureExpireReport()
    {
        DataTable dataTable = FutureExpireDataTable();

        DataRow dr = null;
        int ZoneID = Convert.ToInt16(ddlFutureZone.SelectedValue);
        List<VechilesDocumentRelation> getDocuments = new List<VechilesDocumentRelation>();
        List<VechilesNormsRelation> getNorms = new List<VechilesNormsRelation>();
        List<VehicleEmployee> getDL = new List<VehicleEmployee>();

        List<Vehicles> getVehicles = new List<Vehicles>();

        TransportUserRepository repository = new TransportUserRepository(new AkalAcademy.DataContext());
        getVehicles = repository.GetVehiclesByZoneID(ZoneID, true);

        UsersRepository user = new UsersRepository(new AkalAcademy.DataContext());
        Incharge incharge = new Incharge();


        foreach (Vehicles vehicle in getVehicles)
        {
            getDocuments = repository.GetVechilesDocumentRelationByVehicleID(vehicle.ID);
            incharge = user.GetUsersByAcademyID(vehicle.AcademyID, 14).FirstOrDefault();
            var PendingDocumentName = string.Empty;
            if (getDocuments.Count != 0)
            {
                var row = getDocuments.Where(document => document.TransportDocumentID == 1).FirstOrDefault();
                if (row != null && row.DocumentEndDate <= DateTime.Now.AddDays(Convert.ToInt32(rbFutureExpire.SelectedValue)) && row.DocumentEndDate >= DateTime.Now)
                {
                    PendingDocumentName += "Registration" + ",";
                }
                var row2 = getDocuments.Where(document => document.TransportDocumentID == 2).FirstOrDefault();
                if (row2 != null && row2.DocumentEndDate <= DateTime.Now.AddDays(Convert.ToInt32(rbFutureExpire.SelectedValue)) && row2.DocumentEndDate >= DateTime.Now)
                {
                    PendingDocumentName += "Pollution" + ",";
                }

                if (vehicle.TypeID == (int)(TypeEnum.TransportType.Trust) || vehicle.TypeID == (int)(TypeEnum.TransportType.Contractual) || vehicle.TypeID == (int)(TypeEnum.TransportType.MaterialMovementVehicle) || vehicle.TypeID == (int)(TypeEnum.TransportType.DailyWages) || vehicle.TypeID == (int)(TypeEnum.TransportType.Ambulance) || vehicle.TypeID == (int)(TypeEnum.TransportType.CivilEquipmentVehicle))
                {
                    var row3 = getDocuments.Where(document => document.TransportDocumentID == 3).FirstOrDefault();
                    if (row3 != null && row3.DocumentEndDate <= DateTime.Now.AddDays(Convert.ToInt32(rbFutureExpire.SelectedValue)) && row3.DocumentEndDate >= DateTime.Now)
                    {
                        PendingDocumentName += "Permit" + ",";
                    }
                }

                if (vehicle.TypeID == (int)(TypeEnum.TransportType.Trust) || vehicle.TypeID == (int)(TypeEnum.TransportType.Contractual) || vehicle.TypeID == (int)(TypeEnum.TransportType.MaterialMovementVehicle) || vehicle.TypeID == (int)(TypeEnum.TransportType.DailyWages) || vehicle.TypeID == (int)(TypeEnum.TransportType.Ambulance) || vehicle.TypeID == (int)(TypeEnum.TransportType.CivilEquipmentVehicle))
                {
                    var row4 = getDocuments.Where(document => document.TransportDocumentID == 4).FirstOrDefault();
                    if (row4 != null && row4.DocumentEndDate <= DateTime.Now.AddDays(Convert.ToInt32(rbFutureExpire.SelectedValue)) && row4.DocumentEndDate >= DateTime.Now)
                    {
                        PendingDocumentName += "Tax" + ",";
                    }
                }

                if (vehicle.TypeID == (int)(TypeEnum.TransportType.Trust) || vehicle.TypeID == (int)(TypeEnum.TransportType.Contractual) || vehicle.TypeID == (int)(TypeEnum.TransportType.MaterialMovementVehicle) || vehicle.TypeID == (int)(TypeEnum.TransportType.DailyWages) || vehicle.TypeID == (int)(TypeEnum.TransportType.Ambulance) || vehicle.TypeID == (int)(TypeEnum.TransportType.CivilEquipmentVehicle))
                {

                    var row5 = getDocuments.Where(document => document.TransportDocumentID == 5).FirstOrDefault();
                    if (row5 != null && row5.DocumentEndDate <= DateTime.Now.AddDays(Convert.ToInt32(rbFutureExpire.SelectedValue)) && row5.DocumentEndDate >= DateTime.Now)
                    {
                        PendingDocumentName += "Passing" + ",";
                    }
                }

                var row6 = getDocuments.Where(document => document.TransportDocumentID == 6).FirstOrDefault();
                if (row6 != null && row6.DocumentEndDate <= DateTime.Now.AddDays(Convert.ToInt32(rbFutureExpire.SelectedValue)) && row6.DocumentEndDate >= DateTime.Now)
                {
                    PendingDocumentName += "Insurance" + ",";
                }

                if (PendingDocumentName.Length > 0)
                {
                    PendingDocumentName = PendingDocumentName.Substring(0, PendingDocumentName.Length - 1);
                }
            }
            else
            {

                if (vehicle.TypeID == (int)(TypeEnum.TransportType.Trust) || vehicle.TypeID == (int)(TypeEnum.TransportType.Contractual) || vehicle.TypeID == (int)(TypeEnum.TransportType.MaterialMovementVehicle) || vehicle.TypeID == (int)(TypeEnum.TransportType.DailyWages) || vehicle.TypeID == (int)(TypeEnum.TransportType.Ambulance) || vehicle.TypeID == (int)(TypeEnum.TransportType.CivilEquipmentVehicle))
                {
                    PendingDocumentName = "Registration,Pollution,Permit,Tax,Passing,Insurance,";
                }
                else
                {
                    PendingDocumentName = "Registration,Pollution,Insurance,";
                }
            }

            if (PendingDocumentName.Length > 0)
            {
                PendingDocumentName = PendingDocumentName.Substring(0, PendingDocumentName.Length - 1);
            }
            dr = dataTable.NewRow();
            dr["VehicleNumber"] = vehicle.Number;
            dr["VehicleType"] = vehicle.TransportTypes.Type;
            dr["ZoneName"] = vehicle.Zone.ZoneName;
            dr["AcademyName"] = vehicle.Academy.AcaName;
            dr["DocumentName"] = PendingDocumentName;
            if (incharge != null)
            {
                dr["TransportManager"] = incharge.InName;
                dr["MobileNumber"] = incharge.InMobile;
            }
            dataTable.Rows.Add(dr);
        }
        return dataTable;
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
        ddlZone.Items.Insert(0, new ListItem("--Select Zone--", "0"));
    }

    private void BindAllZones()
    {
        DataSet allzone = new DataSet();
        allzone = DAL.DalAccessUtility.GetDataInDataSet("select * from dbo.Zone");
        ddlALLZone.DataSource = allzone;
        ddlALLZone.DataValueField = "ZoneID";
        ddlALLZone.DataTextField = "ZoneName";
        ddlALLZone.DataBind();
        ddlALLZone.Items.Insert(0, new ListItem("--Select Zone--", "0"));
    }

    private void BindFutureAllZones()
    {
        DataSet allzone = new DataSet();
        allzone = DAL.DalAccessUtility.GetDataInDataSet("select * from dbo.Zone");
        ddlFutureZone.DataSource = allzone;
        ddlFutureZone.DataValueField = "ZoneID";
        ddlFutureZone.DataTextField = "ZoneName";
        ddlFutureZone.DataBind();
        ddlFutureZone.Items.Insert(0, new ListItem("--Select Zone--", "0"));
    }

    private DataTable FutureExpireDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("VehicleNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("VehicleType", typeof(string)));
        dt.Columns.Add(new DataColumn("ZoneName", typeof(string)));
        dt.Columns.Add(new DataColumn("AcademyName", typeof(string)));
        dt.Columns.Add(new DataColumn("DocumentName", typeof(string)));
        dt.Columns.Add(new DataColumn("TransportManager", typeof(string)));
        dt.Columns.Add(new DataColumn("MobileNumber", typeof(string)));

        return dt;
    }
}