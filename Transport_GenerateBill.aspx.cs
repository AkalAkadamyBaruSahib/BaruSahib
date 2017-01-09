using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TranspportGenerateBill : System.Web.UI.Page
{
    private int InchargeID = -1;
    public static int UserTypeID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InchargeID"] != null)
        {
            InchargeID = int.Parse(Session["InchargeID"].ToString());
            UserTypeID = int.Parse(Session["UserTypeID"].ToString());
        }

        if (!Page.IsPostBack)
        {
            BindAcademy();
        }
    }

    private DataTable GenerateEBill()
    {
        ArrayList positions = new ArrayList();
        DataTable dataTable = EmptyDataTable();
        DataRow dr = null;
        foreach (GridViewRow row in repVehicle.Rows)
        {
            decimal TotalBillAmount = 0;
            int TotalFine = 0;
            decimal totalRunningKMOnSchoolRoute = 0;
            bool CompanyOwned = false;
            var MisingDocumentName = string.Empty;
            var MisingNormsName = string.Empty;
            Vehicles getVehicleInfo = new Vehicles();
            List<VechilesDocumentRelation> getDocuments = new List<VechilesDocumentRelation>();
            List<VechilesNormsRelation> getNorms = new List<VechilesNormsRelation>();
            Incharge getIncharge = new Incharge();
            UsersRepository user = new UsersRepository(new AkalAcademy.DataContext());
            TransportUserRepository repo = new TransportUserRepository(new AkalAcademy.DataContext());
            CheckBox chkBok = row.FindControl("chkvehicle") as CheckBox;
            HiddenField hdnVechileId = row.FindControl("hdnVechileId") as HiddenField;
            Label lblvechile = (Label)row.FindControl("lblvechile");
            bool flag = true;
            if (chkBok.Checked)
            {
                int Fine = 0;
                getVehicleInfo = repo.GetVehicleByVehicleID(Convert.ToInt32(hdnVechileId.Value));

                if (getVehicleInfo != null)
                {
                    CompanyOwned = Convert.ToBoolean(getVehicleInfo.IsCompanyOwned);
                    getDocuments = repo.GetVechilesDocumentRelationByVehicleID(getVehicleInfo.ID);
                    if (getDocuments.Count != 0)
                    {
                        if (!getDocuments.Exists(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Registration))
                        {
                            MisingDocumentName += "Registration" + ",";
                            flag = false;
                        }
                        else
                        {
                            var expiryDocument1 = getDocuments.Where(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Registration).FirstOrDefault();
                            if (expiryDocument1 != null && expiryDocument1.DocumentEndDate <= DateTime.Now)
                            {
                                MisingDocumentName += "Registration" + ",";
                                flag = false;
                            }
                        }

                        if (!getDocuments.Exists(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Pollution))
                        {
                            MisingDocumentName += "Pollution" + ",";
                            flag = false;
                        }
                        else
                        {
                            var expiryDocument2 = getDocuments.Where(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Pollution).FirstOrDefault();
                            if (expiryDocument2 != null && expiryDocument2.DocumentEndDate <= DateTime.Now)
                            {
                                MisingDocumentName += "Pollution" + ",";
                                flag = false;
                            }
                        }

                        if (!getDocuments.Exists(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Permit))
                        {
                            MisingDocumentName += "Permit" + ",";
                            flag = false;
                        }
                        else
                        {
                            var expiryDocument3 = getDocuments.Where(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Permit).FirstOrDefault();
                            if (expiryDocument3 != null && expiryDocument3.DocumentEndDate <= DateTime.Now)
                            {
                                MisingDocumentName += "Permit" + ",";
                                flag = false;
                            }
                        }

                        if (!getDocuments.Exists(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Tax))
                        {
                            MisingDocumentName += "Tax" + ",";
                            flag = false;
                        }
                        else
                        {
                            var expiryDocument4 = getDocuments.Where(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Tax).FirstOrDefault();
                            if (expiryDocument4 != null && expiryDocument4.DocumentEndDate <= DateTime.Now)
                            {
                                MisingDocumentName += "Tax" + ",";
                                flag = false;
                            }
                        }

                        if (!getDocuments.Exists(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Passing))
                        {
                            MisingDocumentName += "Passing" + ",";
                            flag = false;
                        }
                        else
                        {
                            var expiryDocument5 = getDocuments.Where(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Passing).FirstOrDefault();
                            if (expiryDocument5 != null && expiryDocument5.DocumentEndDate <= DateTime.Now)
                            {
                                MisingDocumentName += "Passing" + ",";
                                flag = false;
                            }
                        }

                        if (!getDocuments.Exists(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Insurance))
                        {
                            MisingDocumentName += "Insurance" + ",";
                            flag = false;
                        }
                        else
                        {
                            var expiryDocument6 = getDocuments.Where(document => document.TransportDocumentID == (int)TypeEnum.TransportDocumentType.Insurance).FirstOrDefault();
                            if (expiryDocument6 != null && expiryDocument6.DocumentEndDate <= DateTime.Now)
                            {
                                MisingDocumentName += "Insurance" + ",";
                                flag = false;
                            }
                        }
                    }
                    else
                    {
                        MisingDocumentName = "Registration,Insurance,Tax,Permit,Passing,Pollution,";
                        flag = false;
                    }
                    getNorms = repo.GetVechilesNormsRelationByVehicleID(getVehicleInfo.ID);
                    if (getNorms.Count != 0)
                    {
                        if ((!getNorms.Exists(norm => norm.NormID == (int)(TypeEnum.TransportNormsType.Camera))))
                        {
                            MisingNormsName += "Camera" + ",";
                            Fine += 100;
                        }
                        if ((!getNorms.Exists(norm => norm.NormID == (int)(TypeEnum.TransportNormsType.SpeedGoverner))))
                        {
                            MisingNormsName += "SpeedGoverner" + ",";
                            Fine += 100;
                        }
                        if ((!getNorms.Exists(norm => norm.NormID == (int)(TypeEnum.TransportNormsType.YellowColor))))
                        {
                            MisingNormsName += "YellowColor" + ",";
                            Fine += 100;
                        }
                        if ((!getNorms.Exists(norm => norm.NormID == (int)(TypeEnum.TransportNormsType.SchoolNamebothside))))
                        {
                            MisingNormsName += "SchoolNamebothside" + ",";
                            Fine += 100;
                        }
                        if ((!getNorms.Exists(norm => norm.NormID == (int)(TypeEnum.TransportNormsType.Uniform))))
                        {
                            MisingNormsName += "Uniform" + ",";
                            Fine += 100;
                        }
                        if ((!getNorms.Exists(norm => norm.NormID == (int)(TypeEnum.TransportNormsType.Fire))))
                        {
                            MisingNormsName += "Fire" + ",";
                            Fine += 100;
                        }
                        if ((!getNorms.Exists(norm => norm.NormID == (int)(TypeEnum.TransportNormsType.Grill))))
                        {
                            MisingNormsName += "Grill" + ",";
                            Fine += 100;
                        }
                        if ((!getNorms.Exists(norm => norm.NormID == (int)(TypeEnum.TransportNormsType.EmergencyWindows))))
                        {
                            MisingNormsName += "EmergencyWindows" + ",";
                            Fine += 100;
                        }
                    }
                    else
                    {
                        MisingNormsName = "Camera,SpeedGoverner,YellowColor,SchoolNamebothside,Uniform,Fire,Grill,EmergencyWindows,";
                        Fine = 800;
                    }
                    if (MisingDocumentName.Length > 0)
                    {
                        MisingDocumentName = MisingDocumentName.Substring(0, MisingDocumentName.Length - 1);
                    }
                    if (MisingNormsName.Length > 0)
                    {
                        MisingNormsName = MisingNormsName.Substring(0, MisingNormsName.Length - 1);
                    }
                    TotalFine = Fine;
                    var AcaID = getVehicleInfo.AcademyID;

                    if (flag == true)
                    {

                        Vehicle_Calculated_data vehicle_Calculated_data = GetVehicleTotalRunningKM(getVehicleInfo.Number);
                        totalRunningKMOnSchoolRoute = vehicle_Calculated_data.total_KM;

                        TotalBillAmount = generateBill(getVehicleInfo, vehicle_Calculated_data);

                        dr = dataTable.NewRow();
                        dr["VehicleNumber"] = getVehicleInfo.Number;
                        dr["AcademyName"] = getVehicleInfo.Academy.AcaName;
                        dr["OwnerName"] = getVehicleInfo.OwnerName;
                        dr["OwnerNumber"] = getVehicleInfo.OwnerNumber;
                        //dr["DocumentName"] = MisingDocumentName;
                        dr["NormsName"] = MisingNormsName;
                        dr["Fine"] = TotalFine;
                        if (CompanyOwned == true)
                        {
                            dr["TDS"] = "2%";
                        }
                        else
                        {
                            dr["TDS"] = "1%";
                        }
                        dr["TotalPayAmount"] = (TotalBillAmount - TotalFine);
                        dr["TotalKmRunning"] = totalRunningKMOnSchoolRoute;
                        getIncharge = user.GetUsersByAcademyID(getVehicleInfo.AcademyID, 14).FirstOrDefault();
                        if (getIncharge != null)
                        {
                            dr["TransportManager"] = getIncharge.InName;
                            dr["MobileNumber"] = getIncharge.InMobile;
                        }
                        dataTable.Rows.Add(dr);
                    }
                }
            }
        }
        return dataTable;
    }

    private Vehicle_Calculated_data GetVehicleTotalRunningKM(string vehicleNumber)
    {
        vehicleNumber = vehicleNumber.Replace("-", " ");
        //var apiData = Utility.getDataFromAPI("2016-11-01", "2016-11-30", vehicleNumber);
        DateTime sd = DateTime.ParseExact(txtStartDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);

        string strtDate = sd.ToString("yyyy-MM-dd");

        DateTime ed = DateTime.ParseExact(txtEndDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);

        string endDate = ed.ToString("yyyy-MM-dd");

        var apiData = Utility.getDataFromAPI(strtDate, endDate, vehicleNumber);
        Vehicle_Calculated_data vehicle_Calculated_data = new Vehicle_Calculated_data();
        decimal totalKM = 0;
        foreach (vehicle_attendance_detail data in apiData[0].vehicle_attendance_detail)
        {
            if (data.attendance == 1)
            {
                vehicle_Calculated_data.total_KM += (data.route_start_km - data.route_end_km);
                vehicle_Calculated_data.vehicleAttendance += data.attendance;
            }
        }
        return vehicle_Calculated_data;
    }

    public void BindAcademy()
    {
        UsersRepository users = new UsersRepository(new AkalAcademy.DataContext());
        List<Academy> acaList = new List<Academy>();
        if (UserTypeID == ((int)TypeEnum.UserType.TRANSPORTADMIN))
        {
            acaList = users.GetAllAcademy(2);
        }
        else
        {
            acaList = users.GetAcademyByInchargeID(InchargeID);
        }
        if (acaList != null && acaList.Count > 0)
        {
            ddlAcademy.DataSource = acaList;
            ddlAcademy.DataTextField = "AcaName";
            ddlAcademy.DataValueField = "AcaID";
            ddlAcademy.DataBind();
            ddlAcademy.Items.Insert(0, new ListItem("--Select One--", "0"));
        }
    }

    protected void ddlAcademy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVehicleByAcdemyId();
    }

    public void BindVehicleByAcdemyId()
    {
        DataTable dsZoneDetails = new DataTable();
        List<VehiclesDTO> mt = new List<VehiclesDTO>();
        TransportUserRepository repo = new TransportUserRepository(new AkalAcademy.DataContext());
        mt = repo.GetContracturalVehiclesByAcaID(Convert.ToInt32(ddlAcademy.SelectedValue), true);
        if (dsZoneDetails != null)
        {
            repVehicle.DataSource = mt;
            repVehicle.DataBind();
        }
    }

    private DataTable EmptyDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("VehicleNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("AcademyName", typeof(string)));
        dt.Columns.Add(new DataColumn("OwnerName", typeof(string)));
        dt.Columns.Add(new DataColumn("OwnerNumber", typeof(string)));
        //dt.Columns.Add(new DataColumn("DocumentName", typeof(string)));
        dt.Columns.Add(new DataColumn("NormsName", typeof(string)));
        dt.Columns.Add(new DataColumn("TransportManager", typeof(string)));
        dt.Columns.Add(new DataColumn("MobileNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Fine", typeof(string)));
        dt.Columns.Add(new DataColumn("TDS", typeof(string)));
        dt.Columns.Add(new DataColumn("TotalKmRunning", typeof(string)));
        dt.Columns.Add(new DataColumn("TotalPayAmount", typeof(string)));

        return dt;
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "VehicleAutoGenerateBillReport.xls"));
        Response.ContentType = "application/ms-excel";
        DataTable dt = GenerateEBill();
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
    private decimal generateBill(Vehicles getVehicleInfo, Vehicle_Calculated_data vehicle_Calculated_data)
    {
        DieselPetrolPrice getCurrentDieselRate = new DieselPetrolPrice();
        TransportUserRepository repo = new TransportUserRepository(new AkalAcademy.DataContext());

        decimal billAmount = 0;
        getCurrentDieselRate = repo.GetCurrentDieselRateByAcaID(Convert.ToInt32(getVehicleInfo.AcademyID));

        if (getCurrentDieselRate != null)
        {

            decimal vehicleAverage = Convert.ToDecimal(getVehicleInfo.VehicleAverage);

            decimal currentDieselRate = Convert.ToDecimal(getCurrentDieselRate.DieselPrice);

            decimal contractRate = Convert.ToInt32(getVehicleInfo.VehicleContractRate);

            decimal contractDieselRate = Convert.ToInt32(getVehicleInfo.ContractDieselRate);

            decimal contractKM = Convert.ToInt32(getVehicleInfo.KMPerDay);

            //Vehicle_Calculated_data vehicle_Calculated_data = GetVehicleTotalRunningKM(getVehicleInfo.Number);

            decimal totalRunningKMOnSchoolRoute = vehicle_Calculated_data.total_KM;


            decimal totalRunningKMAccordingToContract = 0;

            int numOfWorkingDays = vehicle_Calculated_data.vehicleAttendance;

            decimal extraRunningKM = 0;

            bool IsDieselRateIncreaseMore = false;

            decimal dieselRatebySevenOilSlab = (contractDieselRate * 7) / 100;   // 7% of diesel rate 
            decimal dieselRatebyForteenOilSlab = (contractDieselRate * 14) / 100; // 14% of diesel rate 
            decimal dieselRatebyTwentyOneOilSlab = (contractDieselRate * 21) / 100; // 21% of diesel rate 

            decimal dieselRateInSeven = contractDieselRate + dieselRatebySevenOilSlab;
            decimal dieselRateInForteen = contractDieselRate + dieselRatebyForteenOilSlab;
            decimal dieselRateInTwentyOne = contractDieselRate + dieselRatebyTwentyOneOilSlab;

            decimal extraKMAmount = 0;
            decimal extraKMAndOilSlabAmount = 0;

            IsDieselRateIncreaseMore = currentDieselRate >= dieselRateInSeven ? true : false;

            // Total Km Should run in working days according to contract
            totalRunningKMAccordingToContract = (contractKM * numOfWorkingDays);

            // Step1:- Get Total Running KM of bus : totalRunningKMOnSchoolRoute 

            if (!IsDieselRateIncreaseMore)
            {
                if (totalRunningKMOnSchoolRoute == totalRunningKMAccordingToContract)
                {
                    extraRunningKM = totalRunningKMOnSchoolRoute;
                    billAmount = contractRate;
                }
                else if (totalRunningKMOnSchoolRoute > totalRunningKMAccordingToContract)
                {
                    // Step2:- if total running KM is 
                    extraRunningKM = totalRunningKMOnSchoolRoute - totalRunningKMAccordingToContract;
                    extraKMAmount = (extraRunningKM / vehicleAverage) * currentDieselRate;
                    billAmount = contractRate + extraKMAmount;
                }
                else
                {
                    extraRunningKM = totalRunningKMAccordingToContract - totalRunningKMOnSchoolRoute;
                    extraKMAmount = (extraRunningKM / vehicleAverage) * currentDieselRate;
                    billAmount = contractRate - extraKMAmount;
                }
                // billAmount: Total amount according to KMrunning
            }
            else
            {
                if (totalRunningKMOnSchoolRoute > totalRunningKMAccordingToContract)
                {

                    extraRunningKM = totalRunningKMOnSchoolRoute - totalRunningKMAccordingToContract;
                    extraKMAndOilSlabAmount = (extraRunningKM / vehicleAverage) * currentDieselRate;

                    if (currentDieselRate <= dieselRateInSeven)
                    {
                        extraKMAmount = (totalRunningKMAccordingToContract / vehicleAverage) * dieselRatebySevenOilSlab;
                        billAmount = contractRate + extraKMAmount + extraKMAndOilSlabAmount;
                    }
                    else if (currentDieselRate > dieselRateInSeven && currentDieselRate <= dieselRateInForteen)
                    {
                        extraKMAmount = (totalRunningKMAccordingToContract / vehicleAverage) * dieselRatebyForteenOilSlab;
                        billAmount = contractRate + extraKMAmount + extraKMAndOilSlabAmount;
                    }
                    else
                    {
                        extraKMAmount = (totalRunningKMAccordingToContract / vehicleAverage) * dieselRatebyTwentyOneOilSlab;
                        billAmount = contractRate + extraKMAmount + extraKMAndOilSlabAmount;
                    }
                }
                else
                {
                    if (currentDieselRate <= dieselRateInSeven)
                    {
                        extraKMAmount = (totalRunningKMAccordingToContract / vehicleAverage) * dieselRatebySevenOilSlab;
                        billAmount = contractRate + extraKMAmount;
                    }
                    else if (currentDieselRate > dieselRateInSeven && currentDieselRate <= dieselRateInForteen)
                    {
                        extraKMAmount = (totalRunningKMAccordingToContract / vehicleAverage) * dieselRatebyForteenOilSlab;
                        billAmount = contractRate + extraKMAmount;
                    }
                    else
                    {
                        extraKMAmount = (totalRunningKMAccordingToContract / vehicleAverage) * dieselRatebyTwentyOneOilSlab;
                        billAmount = contractRate + extraKMAmount;
                    }
                }
            }
        }

        bool CompanyOwned = Convert.ToBoolean(getVehicleInfo.IsCompanyOwned);

        decimal tdsAmount = 0;

        if (CompanyOwned == true)
        {
            tdsAmount = (billAmount * 2) / 100;
            billAmount = billAmount + tdsAmount;
        }
        else
        {
            tdsAmount = (billAmount * 1) / 100;
            billAmount = billAmount + tdsAmount;
        }
        return billAmount;
    }
}