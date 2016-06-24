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
        ArrayList positions = new ArrayList();
        DataTable dataTable = EmptyDataTable();
        DataRow dr = null;
        foreach (GridViewRow row in repVehicle.Rows)
        {
            int TotalFine = 0;
            CheckBox chkBok = row.FindControl("chkvehicle") as CheckBox;
            HiddenField hdnVechileId = row.FindControl("hdnVechileId") as HiddenField;
            Label lblvechile = (Label)row.FindControl("lblvechile");
            if (chkBok.Checked)
            {
                DataTable getVehicles = new DataTable();
                int Fine = 0;
                getVehicles = DAL.DalAccessUtility.GetDataInDataSet("Select V.*,VDR.*,VNR.* from Vehicles V INNER JOIN VechilesDocumentRelation VDR on V.ID = VDR.VehicleID  INNER JOIN VechilesNormsRelation VNR on VNR.VehicleID = V.ID where V.ID =" + hdnVechileId.Value + "").Tables[0];
                dr = dataTable.NewRow();
                dr["VehicleNumber"] = getVehicles.Rows[0]["Number"].ToString();
                dr["OwnerName"] = getVehicles.Rows[0]["OwnerName"].ToString();
                dr["OwnerNumber"] = getVehicles.Rows[0]["OwnerNumber"].ToString();
                if (getVehicles.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Registration)).Count() == 0)
                {
                    dr["DocumentName"] = "Registration"; 
                }
                if (getVehicles.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Insurance)).Count() == 0)
                { 
                    dr["DocumentName"] = "Insurance";
                }
                if (getVehicles.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Tax)).Count() == 0)
                { 
                    dr["DocumentName"] = "Tax";
                }
                if (getVehicles.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Permit)).Count() == 0)
                { 
                    dr["DocumentName"] = "Permit"; 
                }
                if (getVehicles.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Passing)).Count() == 0)
                { 
                    dr["DocumentName"] = "Passing";
                }
                if (getVehicles.Select("TransportDocumentID = " + (int)(TypeEnum.TransportDocumentType.Pollution)).Count() == 0)
                {
                    dr["DocumentName"] = "Pollution";
                }
                if (getVehicles.Select("NormID = " + (int)(TypeEnum.TransportNormsType.Camera)).Count() == 0)
                {
                    dr["NormsName"] = "Camera";
                    Fine += 100;
                }
                if (getVehicles.Select("NormID = " + (int)(TypeEnum.TransportNormsType.SpeedGoverner)).Count() == 0)
                {
                    dr["NormsName"] = "SpeedGoverner";
                    Fine += 100;
                }
                if (getVehicles.Select("NormID = " + (int)(TypeEnum.TransportNormsType.YellowColor)).Count() == 0)
                {
                    dr["NormsName"] = "YellowColor";
                    Fine += 100;
                }
                if (getVehicles.Select("NormID = " + (int)(TypeEnum.TransportNormsType.SchoolNamebothside)).Count() == 0)
                {
                    dr["NormsName"] = "SchoolNamebothside";
                    Fine += 100;
                }
                if (getVehicles.Select("NormID = " + (int)(TypeEnum.TransportNormsType.Uniform)).Count() == 0)
                {
                    dr["NormsName"] = "Uniform";
                    Fine += 100;
                }
                if (getVehicles.Select("NormID = " + (int)(TypeEnum.TransportNormsType.Fire)).Count() == 0)
                {
                    dr["NormsName"] = "Fire";
                    Fine += 100;
                }
                if (getVehicles.Select("NormID = " + (int)(TypeEnum.TransportNormsType.Grill)).Count() == 0)
                {
                    dr["NormsName"] = "Grill";
                    Fine += 100;
                }
                if (getVehicles.Select("NormID = " + (int)(TypeEnum.TransportNormsType.EmergencyWindows)).Count() == 0)
                {
                    dr["NormsName"] = "EmergencyWindows";
                    Fine += 100;
                }
                TotalFine = Fine;
                dr["Fine"] = TotalFine;
                dataTable.Rows.Add(dr);
            }
        }
    }

    private DataTable EmptyDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("VehicleNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("OwnerName", typeof(string)));
        dt.Columns.Add(new DataColumn("OwnerNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("DocumentName", typeof(string)));
        dt.Columns.Add(new DataColumn("NormsName", typeof(string)));
        dt.Columns.Add(new DataColumn("Fine", typeof(string)));
        return dt;
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        //Response.ClearContent();
        //Response.Buffer = true;
        //Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "FutureExpireDocumentReport.xls"));
        //Response.ContentType = "application/ms-excel";
        ////DataTable dt = BindDatatable();
        //string str = string.Empty;
        //foreach (DataColumn dtcol in dt.Columns)
        //{
        //    Response.Write(str + dtcol.ColumnName);
        //    str = "\t";
        //}
        //Response.Write("\n");
        //foreach (DataRow dr in dt.Rows)
        //{
        //    str = "";
        //    for (int j = 0; j < dt.Columns.Count; j++)
        //    {
        //        Response.Write(str + Convert.ToString(dr[j]));
        //        str = "\t";
        //    }
        //    Response.Write("\n");
        //}
        //Response.End();

    }

    public void BindDatatable()
    {
    
    
    }

 
}