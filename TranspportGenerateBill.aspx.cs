using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
        }
        int UserTypeID = int.Parse(Session["UserTypeID"].ToString());
        if (!Page.IsPostBack)
        {
            if (UserTypeID == (int)(TypeEnum.UserType.TRANSPORTADMIN))
            {
                BindAcademy();
            }
            else
            {
                BindAcademyByInchargeID();
            }
        }
    }

    public void BindAcademy()
    {
        DataSet allAcademy = new DataSet();
        allAcademy = DAL.DalAccessUtility.GetDataInDataSet("Select * from Academy Where Active=1");
        ddlAcademy.DataSource = allAcademy;
        ddlAcademy.DataValueField = "AcaId";
        ddlAcademy.DataTextField = "AcaName";
        ddlAcademy.DataBind();
        ddlAcademy.Items.Insert(0, new ListItem("--Select Academy--", "0"));
    }

    public void BindAcademyByInchargeID()
    {
        DataSet allzone = new DataSet();
        allzone = DAL.DalAccessUtility.GetDataInDataSet("select * from Academy A INNER JOIN AcademyAssignToEmployee AAE on A.AcaId = AAE.AcaId  where A.Active=1 and AAE.EmpId='" + InchargeID + "'");
        ddlAcademy.DataSource = allzone;
        ddlAcademy.DataValueField = "AcaId";
        ddlAcademy.DataTextField = "AcaName";
        ddlAcademy.DataBind();
        ddlAcademy.Items.Insert(0, new ListItem("--Select Academy--", "0"));
    }
    protected void ddlAcademy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVehicleByAcdemyId();
    }

    public void BindVehicleByAcdemyId()
    {
        DataSet dsZoneDetails = new DataSet();
        dsZoneDetails = DAL.DalAccessUtility.GetDataInDataSet("select ID,Number from Vehicles where AcademyID='" + ddlAcademy.SelectedValue + "' and IsApproved=1 and TypeID=2");
        repVehicle.DataSource = dsZoneDetails;
        repVehicle.DataBind();
    }

    protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)repVehicle.HeaderRow.FindControl("chkboxSelectAll");
        foreach (GridViewRow row in repVehicle.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkvehicle");
            if (ChkBoxHeader.Checked == true)
            {
                ChkBoxRows.Checked = true;
            }
            else
            {
                ChkBoxRows.Checked = false;
            }
        }
    }
    protected void btnGenerateBil_Click(object sender, EventArgs e)
    {
        BindDatatable();
    }

    private DataTable EmptyDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("VehicleNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("AcademyName", typeof(string)));
        dt.Columns.Add(new DataColumn("OwnerName", typeof(string)));
        dt.Columns.Add(new DataColumn("OwnerNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("DocumentName", typeof(string)));
        dt.Columns.Add(new DataColumn("NormsName", typeof(string)));
        dt.Columns.Add(new DataColumn("Fine", typeof(string)));
        dt.Columns.Add(new DataColumn("TransportManager", typeof(string)));
        dt.Columns.Add(new DataColumn("MobileNumber", typeof(string)));
        return dt;
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "FutureExpireDocumentReport.xls"));
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

    public DataTable BindDatatable()
    {
        ArrayList positions = new ArrayList();
        DataTable dataTable = EmptyDataTable();
        DataRow dr = null;
        foreach (GridViewRow row in repVehicle.Rows)
        {
            int TotalFine = 0;
            var MisingDocumentName = string.Empty;
            var MisingNormsName = string.Empty;
            DataTable getNorms = new DataTable();
            DataTable getIncharge = new DataTable();
            DataTable getDocument = new DataTable();
            CheckBox chkBok = row.FindControl("chkvehicle") as CheckBox;
            HiddenField hdnVechileId = row.FindControl("hdnVechileId") as HiddenField;
            Label lblvechile = (Label)row.FindControl("lblvechile");
            if (chkBok.Checked)
            {
                
                DataTable getVehicles = new DataTable();
                int Fine = 0;
                getVehicles = DAL.DalAccessUtility.GetDataInDataSet("Select V.*,A.AcaName from Vehicles V   INNER JOIN Academy A on V.AcademyID = A.AcaId  where V.ID =" + hdnVechileId.Value + "").Tables[0];
                if (getVehicles.Rows.Count > 0)
                {
                    getDocument = DAL.DalAccessUtility.GetDataInDataSet("Select * from  VechilesDocumentRelation V  where V.VehicleID =" + getVehicles.Rows[0]["ID"].ToString() + "").Tables[0];
                    if (getDocument.Rows.Count > 0)
                    {
                        if (getDocument.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Registration)).Count() == 0)
                        {
                            MisingDocumentName += "Registration" + ",";
                        }
                        if (getDocument.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Insurance)).Count() == 0)
                        {
                            MisingDocumentName += "Insurance" + ",";
                        }
                        if (getDocument.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Tax)).Count() == 0)
                        {
                            MisingDocumentName += "Tax" + ",";
                        }
                        if (getDocument.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Permit)).Count() == 0)
                        {
                            MisingDocumentName += "Permit" + ",";
                        }
                        if (getDocument.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Passing)).Count() == 0)
                        {
                            MisingDocumentName += "Passing" + ",";
                        }
                        if (getDocument.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Pollution)).Count() == 0)
                        {
                            MisingDocumentName += "Pollution" + ",";
                        }
                    }
                    else
                    {
                        MisingDocumentName = "Registration,Insurance,Tax,Permit,Passing,Pollution,";
                    }
                    getNorms = DAL.DalAccessUtility.GetDataInDataSet("Select VNR.* from  VechilesNormsRelation VNR INNER JOIN Vehicles V  on VNR.VehicleID = V.ID  where V.ID =" + hdnVechileId.Value + "").Tables[0];
                    if (getNorms.Rows.Count > 0)
                    {
                        if (getNorms.Select("NormID = " + (int)(TypeEnum.TransportNormsType.Camera)).Count() == 0)
                        {
                            MisingNormsName += "Camera" + ",";
                            Fine += 100;
                        }
                        if (getNorms.Select("NormID = " + (int)(TypeEnum.TransportNormsType.SpeedGoverner)).Count() == 0)
                        {
                            MisingNormsName += "SpeedGoverner" + ",";
                            Fine += 100;
                        }
                        if (getNorms.Select("NormID = " + (int)(TypeEnum.TransportNormsType.YellowColor)).Count() == 0)
                        {
                            MisingNormsName += "YellowColor" + ",";
                            Fine += 100;
                        }
                        if (getNorms.Select("NormID = " + (int)(TypeEnum.TransportNormsType.SchoolNamebothside)).Count() == 0)
                        {
                            MisingNormsName += "SchoolNamebothside" + ",";
                            Fine += 100;
                        }
                        if (getNorms.Select("NormID = " + (int)(TypeEnum.TransportNormsType.Uniform)).Count() == 0)
                        {
                            MisingNormsName += "Uniform" + ",";
                            Fine += 100;
                        }
                        if (getNorms.Select("NormID = " + (int)(TypeEnum.TransportNormsType.Fire)).Count() == 0)
                        {
                            MisingNormsName += "Fire" + ",";
                            Fine += 100;
                        }
                        if (getNorms.Select("NormID = " + (int)(TypeEnum.TransportNormsType.Grill)).Count() == 0)
                        {
                            MisingNormsName += "Grill" + ",";
                            Fine += 100;
                        }
                        if (getNorms.Select("NormID = " + (int)(TypeEnum.TransportNormsType.EmergencyWindows)).Count() == 0)
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
                    var AcaID = getVehicles.Rows[0]["AcademyID"].ToString();
                    
                    dr = dataTable.NewRow();
                    dr["VehicleNumber"] = getVehicles.Rows[0]["Number"].ToString();
                    dr["AcademyName"] = getVehicles.Rows[0]["AcaName"].ToString();
                    dr["OwnerName"] = getVehicles.Rows[0]["OwnerName"].ToString();
                    dr["OwnerNumber"] = getVehicles.Rows[0]["OwnerNumber"].ToString();
                    dr["DocumentName"] = MisingDocumentName;
                    dr["NormsName"] = MisingNormsName;
                    dr["Fine"] = TotalFine;
                    getIncharge = DAL.DalAccessUtility.GetDataInDataSet("select Inc.InchargeId,Inc.InName,Inc.InMobile from  AcademyAssignToEmployee AAE INNER JOIN Incharge Inc on Inc.InchargeId = AAE.EmpID where AAE.AcaId='" + getVehicles.Rows[0]["AcademyID"].ToString() + "' and Inc.UserTypeId =14").Tables[0];
                    if (getIncharge.Rows.Count > 0)
                    {
                        dr["TransportManager"] = getIncharge.Rows[0]["InName"].ToString();
                        dr["MobileNumber"] = getIncharge.Rows[0]["InMobile"].ToString();
                    }
                    dataTable.Rows.Add(dr);
                }
            }
        }
        return dataTable;
    }
}