using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_Academy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblUser.Text = Session["EmailId"].ToString();
            }
            lblHeading.Text = "Step 1 - New Academy";
            divShiftAcademy.Visible = false;
            divShitAUdit.Visible = false;
            //divAssignLocation.Visible = false;
            frmAssignment.Visible = false;
            BindAcademyDetails();
            //BindUserType();
            BindZone();
            BindStatusType();
            if (Request.QueryString["AcaId"] != null)
            {
                
                getAcaDetails(Request.QueryString["AcaId"].ToString());
                btnEdit.Visible = true;
                btnSave.Visible = false;
                ddlZone.Enabled = false;
                txtZone.Enabled = false;
                //ddlStatusType.Enabled = false;

            }
            if (Request.QueryString["AcaIdS"] != null)
            {
                //BindShiftedZone(Request.QueryString["AcaIdS"].ToString());
                GetAcaShiftedToAnotherZone(Request.QueryString["AcaIdS"].ToString());
                divNewAcademy.Visible = false;
                divShiftAcademy.Visible = true;
                divShitAUdit.Visible = false;
                
            }
            if (Request.QueryString["AcaIdA"] != null)
            {
                //BindShiftedZone(Request.QueryString["AcaIdS"].ToString());
                GetAcaShiftedToAnotherAuditUser(Request.QueryString["AcaIdA"].ToString());
                divNewAcademy.Visible = false;
                divShiftAcademy.Visible = false;
                divShitAUdit.Visible = true;
            }
            if (Request.QueryString["AcaIdAE"] != null)
            {
                //divNewAcademy.Visible = false;
                //divAssignLocation.Visible = true;
                GetAcaAssignEmpl(Request.QueryString["AcaIdAE"].ToString());
                lblHeading.Text = "Step 2 - Assign Employee";
                frmNewAca.Visible = false;
                frmAssignment.Visible = true;
                BindUserType();

            }
            if (Request.QueryString["AcaIdIA"] != null)
            {
                DeactiveAca(Request.QueryString["AcaIdIA"].ToString());
            }
            if (Request.QueryString["AcaIdIdA"] != null)
            {
                ActiveAca(Request.QueryString["AcaIdIdA"].ToString());
            }
        }
    }
    protected void BindUserType()
    {
        DataSet dsUsType = new DataSet();
        dsUsType = DAL.DalAccessUtility.GetDataInDataSet("select UserTypeId,UserTypeName from UserType where Active=1 and UserTypeName<>'ADMIN' and UsertYpeId in (2,3)");
        ddlUserType.DataSource = dsUsType;
        ddlUserType.DataValueField = "UserTypeId";
        ddlUserType.DataTextField = "UserTypeName";
        ddlUserType.DataBind();
        ddlUserType.Items.Insert(0, "Select User Type");
        ddlUserType.SelectedIndex = 0;
    }
    protected void BindEmployee()
    {
        DataSet dsEmp = new DataSet();
        dsEmp = DAL.DalAccessUtility.GetDataInDataSet("select InchargeId,InName from Incharge where UserTypeId='" + ddlUserType.SelectedValue + "'");
        if (dsEmp.Tables[0].Rows.Count > 0)
        {
            ddlEmployee.DataSource = dsEmp;
            ddlEmployee.DataValueField = "InchargeId";
            ddlEmployee.DataTextField = "InName";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, "Select Employee");
            ddlEmployee.SelectedIndex = 0;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('No Employee created of this User Type.');", true);
        }
    }
    protected void DeactiveAca(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewAcademy '" + ID + "','','','0','" + lblUser.Text + "','4'");
        BindAcademyDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Academy deactive successfully.');", true);

    }
    protected void ActiveAca(string ID)
    {
        DAL.DalAccessUtility.GetDataInDataSet("exec USP_NewAcademy '" + ID + "','','','1','" + lblUser.Text + "','4'");
        BindAcademyDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Academy active successfully.');", true);

    }
    private void getAcaDetails(string ID)
    {
        DataSet dsCouDetails = new DataSet();
        dsCouDetails = DAL.DalAccessUtility.GetDataInDataSet("select AcaName,StatusTypeId,ZoneId from Academy where AcaId='" + ID + "'");
        if (dsCouDetails.Tables[0].Rows.Count > 0)
        {
            txtZone.Text = dsCouDetails.Tables[0].Rows[0]["AcaName"].ToString();
            BindStatusType();
            ddlStatusType.SelectedIndex = ddlStatusType.Items.IndexOf(ddlStatusType.Items.FindByValue(dsCouDetails.Tables[0].Rows[0]["StatusTypeId"].ToString().Trim()));
            BindZone();
            ddlZone.SelectedIndex = ddlZone.Items.IndexOf(ddlZone.Items.FindByValue(dsCouDetails.Tables[0].Rows[0]["ZoneId"].ToString().Trim()));
        }
        BindAcademyDetails();
    }
    private void GetAcaShiftedToAnotherZone(string id)
    {
        DataSet dsShiftAca = DAL.DalAccessUtility.GetDataInDataSet("SELECT Academy.ZoneId, Academy.AcaName, Zone.ZoneName FROM Academy INNER JOIN Zone ON Academy.ZoneId = Zone.ZoneId where AcaId='"+ id +"'");
        if (dsShiftAca.Tables[0].Rows.Count > 0)
        {
            lblAcaName.Text = dsShiftAca.Tables[0].Rows[0]["AcaName"].ToString();
            lblPresentZone.Text = dsShiftAca.Tables[0].Rows[0]["ZoneName"].ToString();
            BindShiftedZone(dsShiftAca.Tables[0].Rows[0]["ZoneId"].ToString());
        }
    }
    protected DataTable BindDatatable()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ExcelAcademy");
        dt = ds.Tables[0];
        return dt;
    }
    protected void btnExecl_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Zone.xls"));
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
    private void GetAcaAssignEmpl(string id)
    {
        DataSet dsAcaEmp = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ExistingEmpDetails '" + id + "'");
        lblAEAcademy.Text = dsAcaEmp.Tables[0].Rows[0]["AcaName"].ToString();
        if (dsAcaEmp.Tables[1].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Academy Already Assign to " + dsAcaEmp.Tables[1].Rows[0]["InName"].ToString() + "(" + dsAcaEmp.Tables[1].Rows[0]["InMobile"].ToString() + ").');", true);
            ddlUserType.Enabled = false;
            ddlEmployee.Enabled = false;
        }
    }
    private void GetAcaShiftedToAnotherAuditUser(string id)
    {
        DataSet dsShiftAcaAu = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AuditInchargeDetail '"+ id +"'");
        DataSet dsAc = DAL.DalAccessUtility.GetDataInDataSet("select AcaName from Academy where AcaId='"+ id +"'");
        lblAuditAcaName.Text = dsAc.Tables[0].Rows[0]["AcaName"].ToString();
        if (dsShiftAcaAu.Tables[0].Rows.Count > 0)
        {
            
            lblAuditUser.Text = dsShiftAcaAu.Tables[0].Rows[0]["InName"].ToString();
            lblMobNo.Text = dsShiftAcaAu.Tables[0].Rows[0]["InMobile"].ToString();
            BindShiftedAcademy(dsShiftAcaAu.Tables[0].Rows[0]["AcaId"].ToString());
        }
    }
    protected void BindZone()
    {
        DataSet dsZone = new DataSet();
        dsZone = DAL.DalAccessUtility.GetDataInDataSet(" exec  USP_ShowZoneDetails");
        ddlZone.DataSource = dsZone;
        ddlZone.DataValueField = "ZoneId";
        ddlZone.DataTextField = "ZoneName";
        ddlZone.DataBind();
        ddlZone.Items.Insert(0, "Select Zone");
        ddlZone.SelectedIndex = 0;
    }
    protected void BindShiftedZone( string zi)
    {
        //string zi = Request.QueryString["AcaIdS"].ToString();
        DataSet dsZone = new DataSet();
        dsZone = DAL.DalAccessUtility.GetDataInDataSet(" select ZoneId,ZoneName from Zone where ZoneId<>'"+ zi +"'");
        ddlShiftedZone.DataSource = dsZone;
        ddlShiftedZone.DataValueField = "ZoneId";
        ddlShiftedZone.DataTextField = "ZoneName";
        ddlShiftedZone.DataBind();
        ddlShiftedZone.Items.Insert(0, "Select Zone");
        ddlShiftedZone.SelectedIndex = 0;
    }
    protected void BindShiftedAcademy(string ai)
    {
        //string zi = Request.QueryString["AcaIdS"].ToString();
        DataSet dsZone = new DataSet();
        dsZone = DAL.DalAccessUtility.GetDataInDataSet(" select AcaId,AcaName from Academy where AcaId<>'" + ai + "'");
        ddlAuditAca.DataSource = dsZone;
        ddlAuditAca.DataValueField = "AcaId";
        ddlAuditAca.DataTextField = "AcaName";
        ddlAuditAca.DataBind();
        ddlAuditAca.Items.Insert(0, "Select Academy");
        ddlAuditAca.SelectedIndex = 0;
    }
    protected void BindStatusType()
    {
        DataSet dsST = new DataSet();
        dsST = DAL.DalAccessUtility.GetDataInDataSet(" exec  USP_ShowStatusTypeDetails");
        ddlStatusType.DataSource = dsST;
        ddlStatusType.DataValueField = "StatusTypeId";
        ddlStatusType.DataTextField = "StatusTypeName";
        ddlStatusType.DataBind();
        ddlStatusType.Items.Insert(0, "Select Status Type");
        ddlStatusType.SelectedIndex = 0;
    }
    protected void BindAcademyDetails()
    {
        DataSet dsAcaDetails = new DataSet();
        dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowAcademyDetails_ByUser '" + lblUser.Text + "'");
        divAcademyDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th style='display:none;'></th>";
        ZoneInfo += "<th width='30%'>Academy Details</th>";
        ZoneInfo += "<th width='25%'>Zone Details</th>";
        ZoneInfo += "<th width='20%'>Status and Image</th>";
        ZoneInfo += "<th width='25%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsAcaDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td style='display:none;'>1</td>";
            ZoneInfo += "<td width='30%'><table><tr><td><b>Academy Code </b> :" + dsAcaDetails.Tables[0].Rows[i]["AcId"].ToString() + "</td>";

            ZoneInfo += "<tr><td><b>Academy Name</b> : " + dsAcaDetails.Tables[0].Rows[i]["AcaName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><a class='btn btn-success' href='Admin_Academy.aspx?AcaIdAE=" + dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'><i class='icon-zoom-in icon-white'></i> Assign Employee</a></td></tr></table></td>";
            ZoneInfo += "<td width='25%'><table><tr><td><b>Zone Code </b> : " + dsAcaDetails.Tables[0].Rows[i]["ZoId"].ToString() + "</td>";
            ZoneInfo += "<tr><td><b>Zone Name</b> : " + dsAcaDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>State</b> : " + dsAcaDetails.Tables[0].Rows[i]["StateName"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>City</b> : " + dsAcaDetails.Tables[0].Rows[i]["CityName"].ToString() + "(" + dsAcaDetails.Tables[0].Rows[i]["Pincode"].ToString() + ")</td></tr></table></td>";
            //ZoneInfo += "<tr><td><b>Pincode</b> : " + dsAcaDetails.Tables[0].Rows[i]["Pincode"].ToString() + "</td></tr></table></td>";
            ZoneInfo += "<td class='center' width='20%'>";
            ZoneInfo += "<span class='label label-success' title='" + dsAcaDetails.Tables[0].Rows[i]["StatusTypeName"].ToString() + "' style='font-size: 15.998px;'>" + dsAcaDetails.Tables[0].Rows[i]["StatusTypeName"].ToString() + "</span>";
           //if (dsAcaDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
           // {
               
           // } 
            //else
            //{
            //    ZoneInfo += "<span class='label label-important' title='Inactive'>InActive</span>";
            //}
            ZoneInfo += "</td>";
            ZoneInfo += "<td class='center' width='25%'>";
            //ZoneInfo += "<a class='btn btn-success' href='Admin_Academy.aspx?AcaIdIdA=" + dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'>";
            //ZoneInfo += "<i class='icon-zoom-in icon-white'></i> Active";
            //ZoneInfo += "</a>";
            ZoneInfo += "<table><tr><td><a class='btn btn-info' href='Admin_Academy.aspx?AcaId=" + dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i> Change Academy Status";
            ZoneInfo += "</a></td></tr>";
            ZoneInfo += "<tr><td><a class='btn btn-info' href='Admin_Academy.aspx?AcaIdS=" + dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'><i class='icon-edit icon-white'></i> Shift Academy to another Zone</a></td></tr>";
            ZoneInfo += "<tr><td><a class='btn btn-info' href='Admin_Academy.aspx?AcaIdA=" + dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'><i class='icon-edit icon-white'></i> Shift Academy to Audit User</a></td></tr>";
            //ZoneInfo += "<a class='btn btn-danger' href='Admin_Academy.aspx?AcaIdIA=" + dsAcaDetails.Tables[0].Rows[i]["AcaId"].ToString() + "'>";
            //ZoneInfo += "<i class='icon-trash icon-white'></i> Inactive";
            //ZoneInfo += "</a>";
            ZoneInfo += "</table></td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";

        divAcademyDetails.InnerHtml = ZoneInfo.ToString();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct AcaName from Academy where AcaName='" + txtZone.Text + "'");
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Academy Already Exist.');", true);
        }
        else
        {
            if (txtZone.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter academy name.');", true);
            }

            else if (ddlZone.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select zone.');", true);
            }
            else if (ddlStatusType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Status .');", true);
            }
            else
            {
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewAcademy '','" + txtZone.Text + "','" + ddlZone.SelectedValue + "','1','" + lblUser.Text + "','1','" + ddlStatusType.SelectedValue + "'");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Academy Create Successfully.');", true);
                BindAcademyDetails();
                txtZone.Text = "";
                ddlZone.SelectedIndex = 0;
            }
        }
        //divAssignLocation.Visible = true;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        string Acaid = Request.QueryString["AcaId"];
        if (txtZone.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter academy name.');", true);
        }
        else if (ddlStatusType.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Status .');", true);
        }
        else if (ddlZone.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select zone.');", true);
        }
        else
        {
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewAcademy '" + Acaid + "','" + txtZone.Text + "','" + ddlZone.SelectedValue + "','1','" + lblUser.Text + "','2','" + ddlStatusType.SelectedValue + "'");
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Academy edit Successfully.');", true);
            BindAcademyDetails();
            txtZone.Text = "";
            ddlZone.SelectedIndex = 0;
        }
    }
    protected void btnCl_Click(object sender, EventArgs e)
    {
        txtZone.Text = "";
        ddlZone.SelectedIndex = 0;
    }
    protected void btnShift_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        string aid = Request.QueryString["AcaIdS"].ToString();
        DAL.DalAccessUtility.ExecuteNonQuery("exec USP_ShiftAcademyToAnotherZone '"+ aid +"','"+ lblUser.Text +"','"+ ddlShiftedZone.SelectedValue +"'");
        BindAcademyDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Academy successfully shifed to "+ ddlShiftedZone.SelectedItem.Text +".');", true);
    }
    protected void ddlAuditAca_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dsExistAudit = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowExistingAuditEmp '"+ lblAuditAcaName.Text +"'");
        if (dsExistAudit.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Already assign to "+ dsExistAudit.Tables[0].Rows[0]["InName"].ToString() +" Audit Officer.');", true);
        }
    }
    
    protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployee();
    }
}