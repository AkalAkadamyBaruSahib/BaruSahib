using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Admin_UserControls_BodyAssignLocation : System.Web.UI.UserControl
{
    public int ModuleID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ModuleID"] != null)
        {
            ModuleID = int.Parse(Session["ModuleID"].ToString());
        }
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
            BindDepartment();
            BindZone();
            BindZoneGrid();
            BindSerachDdl();
            divLocation.Visible = false;
            divLocationAssignFromAdminHome.Visible = false;
            tdAcaDemy.Visible = false;
            if (Request.QueryString["ZoneId"] != null)
            {
                divAllot.Visible = false;
                divLocation.Visible = true;
                divLocationAssignFromAdminHome.Visible = false;
              //  GetAllotmentDtails(Request.QueryString["ZoneId"].ToString());

            }
            if (Request.QueryString["ZoneIdLoc"] != null)
            {
                divAllot.Visible = false;
                divLocation.Visible = false;
                divLocationAssignFromAdminHome.Visible = true;
               // GetDetailFromAdminHomePage(Request.QueryString["ZoneIdLoc"].ToString());
              //  BindAcademyFromHome(Request.QueryString["ZoneIdLoc"].ToString());
            }
        }
    }

     protected void BindZoneGrid()
    {
        DataSet dsZone = new DataSet();
        dsZone = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct Z.ZoneID,Z.ZoneName FROM [TransportZoneAcademyRelation] TR Inner Join Zone Z  on Z.ZoneID=TR.ZoneID order by ZoneName ASC");
        GridZone.DataSource = dsZone;
        GridZone.DataBind();
    }

    protected void BindDepartment()
    {
        DataSet dsDept = new DataSet();
        dsDept = DAL.DalAccessUtility.GetDataInDataSet("select DepId,department from Department where Active=1 and ModuleID=" + ModuleID + " order by department asc");
        ddlDept.DataSource = dsDept;
        ddlDept.DataValueField = "DepId";
        ddlDept.DataTextField = "department";
        ddlDept.DataBind();
        ddlDept.Items.Insert(0, "Select");
        ddlDept.SelectedIndex = 0;
    }

    protected void BindSerachDdl()
    {
        DataSet dsUt = new DataSet();
        if (Session["UserTypeID"].ToString() == ((int)TypeEnum.UserType.ADMIN).ToString())
        {
            dsUt = DAL.DalAccessUtility.GetDataInDataSet("select UserTypeId,UserTypeName from UserType where ModuleID=1 order by UserTypeName ASC");
        }
        else if (Session["UserTypeID"].ToString() == ((int)TypeEnum.UserType.TRANSPORTADMIN).ToString())
        {
            dsUt = DAL.DalAccessUtility.GetDataInDataSet("select UserTypeId,UserTypeName from UserType where ModuleID=2 order by UserTypeName ASC");
        }
        else if (Session["UserTypeID"].ToString() == ((int)TypeEnum.UserType.WORKSHOPADMIN).ToString())
        {
            dsUt = DAL.DalAccessUtility.GetDataInDataSet("select UserTypeId,UserTypeName from UserType where ModuleID=4 order by UserTypeName ASC");
        }
        else
        {
            dsUt = DAL.DalAccessUtility.GetDataInDataSet("select UserTypeId,UserTypeName from UserType where ModuleID=3 order by UserTypeName ASC");
        }

        ddlSerchEmp.DataSource = dsUt;
        ddlSerchEmp.DataValueField = "UserTypeId";
        ddlSerchEmp.DataTextField = "UserTypeName";
        ddlSerchEmp.DataBind();
        ddlSerchEmp.Items.Insert(0, "Select");
        ddlSerchEmp.SelectedIndex = 0;

        ddlUserTpe4Assign.DataSource = dsUt;
        ddlUserTpe4Assign.DataValueField = "UserTypeId";
        ddlUserTpe4Assign.DataTextField = "UserTypeName";
        ddlUserTpe4Assign.DataBind();
        ddlUserTpe4Assign.Items.Insert(0, "Select");
        ddlUserTpe4Assign.SelectedIndex = 0;
    }

    protected void BindEmployee()
    {
        DataSet dsDept = new DataSet();
        dsDept = DAL.DalAccessUtility.GetDataInDataSet("select InchargeId,InName from Incharge where DepId='" + ddlDept.SelectedValue + "' order by InName ASC");
        ddlEmpl.DataSource = dsDept;
        ddlEmpl.DataValueField = "InchargeId";
        ddlEmpl.DataTextField = "InName";
        ddlEmpl.DataBind();
        ddlEmpl.Items.Insert(0, "Select Incharge");
        ddlEmpl.SelectedIndex = 0;
    }

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if(ddlDept.SelectedItem.Text=)
        pnlEmplyee.Visible = true;
        GridZone.Visible = false;
        lblAssignLocation.Visible = false;
        lblDesignation.Visible = false;
        btnAddAcademy.Visible = false;
        pnlAllZone.Visible = false;
        pnlSingleSelect.Visible = false;
        chkAllZone.Checked = false;
        GridAcademy.Visible = false;
        BindEmployee();
    }

    protected void BindZone()
    {
        List<Zone> dsZone = new List<Zone>();
        UsersRepository users = new UsersRepository(new AkalAcademy.DataContext());

        dsZone = users.GetZoneByModuleID(ModuleID);
        ddlZone.DataSource = dsZone;
        ddlZone.DataValueField = "ZoneId";
        ddlZone.DataTextField = "ZoneName";
        ddlZone.DataBind();
        ddlZone.Items.Insert(0, "Select Zone");
        ddlZone.SelectedIndex = 0;
    }

    protected void ddlEmpl_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlZone.ClearSelection();
        chkAllZone.Checked = false;
        btnAddAcademy.Visible = true;
        ddlZone.Enabled = true;
        DataSet dsval = new DataSet();
        dsval = DAL.DalAccessUtility.GetDataInDataSet("select UserTypeId from Incharge where InName='" + ddlEmpl.SelectedItem.Text + "'");
        int userTypeID = Convert.ToInt32(dsval.Tables[0].Rows[0]["UserTypeId"].ToString());
        if (userTypeID == ((int)TypeEnum.UserType.CONSTRUCTION) || userTypeID == ((int)TypeEnum.UserType.FrontDesk) || userTypeID == ((int)TypeEnum.UserType.CONSTRUCTIONSUBADMIN) || userTypeID == ((int)TypeEnum.UserType.TRANSPORTZONEINCHARGE))
        {
            pnlSingleSelect.Visible = true;
            lblDesignation.Visible = true;
            btnAddAcademy.Visible = false;
            pnlAllZone.Visible = false;
            GridAcademy.Visible = false;
        }

        else if (userTypeID == ((int)TypeEnum.UserType.ACADEMIC) || userTypeID == ((int)TypeEnum.UserType.AUDIT)  || userTypeID == ((int)TypeEnum.UserType.TRANSPORTMANAGER) || userTypeID == ((int)TypeEnum.UserType.INSURANCECOORDINATOR)
            ||  userTypeID == ((int)TypeEnum.UserType.SECURITYSUPERVISOR)  || userTypeID == ((int)TypeEnum.UserType.TRANSPORTTRAINEE)  || userTypeID == ((int)TypeEnum.UserType.WORKSHOPADMIN)
            || userTypeID == ((int)TypeEnum.UserType.WORKSHOPEMPLOYEE) || userTypeID == ((int)TypeEnum.UserType.COMPLAINT) || userTypeID == ((int)TypeEnum.UserType.ELECTRICAL) || userTypeID == ((int)TypeEnum.UserType.CIVILTRANSPORTSUPERVISOR) || userTypeID == ((int)TypeEnum.UserType.CLUSTERHEAD)) 
        {
            BindZoneGridOnSelectedEmp();
            BindBtnAcademyClickGrid();
            GridZone.Visible = true;
            lblAssignLocation.Visible = true;
            lblDesignation.Visible = true;
            btnAddAcademy.Visible = true;
            pnlAllZone.Visible = false;
            pnlSingleSelect.Visible = false;
        }
        else if (userTypeID == ((int)TypeEnum.UserType.TRANSPORTADMIN) || userTypeID == ((int)TypeEnum.UserType.ADMIN) || userTypeID == ((int)TypeEnum.UserType.ARCHITECTURAL)
            || userTypeID == ((int)TypeEnum.UserType.PURCHASEEMPLOYEE) || userTypeID == ((int)TypeEnum.UserType.PURCHASE) || userTypeID == ((int)TypeEnum.UserType.SECURITY) || userTypeID == ((int)TypeEnum.UserType.PURCHASECOMMITTEE))
        {
            lblDesignation.Visible = true;
            btnAddAcademy.Visible = false;
            GridZone.Visible = false;
            lblAssignLocation.Visible = false;
            pnlSingleSelect.Visible = false;
            pnlAllZone.Visible = true;
            GridAcademy.Visible = false;
        }
        else
        {
            pnlEmplyee.Visible = true;
            GridZone.Visible = false;
            lblAssignLocation.Visible = false;
            lblDesignation.Visible = false;
            btnAddAcademy.Visible = false;
            pnlAllZone.Visible = false;
            pnlSingleSelect.Visible = false;
            chkAllZone.Checked = false;
            GridAcademy.Visible = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (chkAllZone.Visible == true)
        {
            if (chkAllZone.Checked)
            {
                AssignAllAcademy();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Incharge Location assign Successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(' Please Checked The Check Box.');", true);
            }
        }
        else
        {
            DataSet dsval = new DataSet();
            DataSet dsDelA = DAL.DalAccessUtility.GetDataInDataSet("delete AcademyAssignToEmployee where EmpId='" + ddlEmpl.SelectedValue + "'");
            dsval = DAL.DalAccessUtility.GetDataInDataSet("select UserTypeId from Incharge where InName='" + ddlEmpl.SelectedItem.Text + "'");
            if (dsval.Tables[0].Rows[0]["UserTypeId"].ToString() == ((int)TypeEnum.UserType.CONSTRUCTION).ToString() || dsval.Tables[0].Rows[0]["UserTypeId"].ToString() == ((int)TypeEnum.UserType.PURCHASEEMPLOYEE).ToString() || dsval.Tables[0].Rows[0]["UserTypeId"].ToString() == ((int)TypeEnum.UserType.PURCHASE).ToString() || dsval.Tables[0].Rows[0]["UserTypeId"].ToString() == ((int)TypeEnum.UserType.STORE).ToString())
            {
                SaveAllAcademyInZone();
            }
            else
            {
                SaveSelectedAcademyInZones();
            }
        }
    }

    private void SaveSelectedAcademyInZones()
    {

        foreach (GridViewRow row in GridAcademy.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = row.FindControl("chkCtrl") as CheckBox;
                if (chkRow.Checked)
                {
                    HiddenField hdnAcaId = row.FindControl("hdnAcaId") as HiddenField;
                    HiddenField hdnZoneID = row.FindControl("hdnZoneID") as HiddenField;
                    DAL.DalAccessUtility.ExecuteNonQuery("insert into AcademyAssignToEmployee(ZoneId,AcaId,EmpId,Active,CreatedBy,CreatedOn) values ('" + hdnZoneID.Value + "','" + hdnAcaId.Value + "','" + ddlEmpl.SelectedValue + "','1','" + lblUser.Text + "','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "') ");
                }
            }
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Incharge Location assign Successfully.');", true);
    }

    private void SaveAllAcademyInZone()
    {
        DataSet dsA = new DataSet();
        dsA = DAL.DalAccessUtility.GetDataInDataSet("select AcaId from Academy where ZoneId='" + ddlZone.SelectedValue + "'");
        foreach (DataRow drAi in dsA.Tables[0].Rows)
        {
            string Ai = string.Empty;
            Ai = Ai + "," + drAi["AcaId"].ToString();
            string[] Ai0 = Ai.Split(',');
            foreach (string Ai1 in Ai.Split(','))
            {
                DataSet dsA1 = new DataSet();

                DAL.DalAccessUtility.ExecuteNonQuery("insert into AcademyAssignToEmployee(ZoneId,AcaId,EmpId,Active,CreatedBy,CreatedOn) values ('" + ddlZone.SelectedValue + "','" + Ai1 + "','" + ddlEmpl.SelectedValue + "','1','" + lblUser.Text + "','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "') ");
                DAL.DalAccessUtility.ExecuteNonQuery("delete AcademyAssignToEmployee where AcaId=0");
            }
        }
        DataSet dsLoginId = DAL.DalAccessUtility.GetDataInDataSet("select LoginId from Incharge where InName='" + ddlEmpl.SelectedItem.Text + "'");
        DAL.DalAccessUtility.ExecuteNonQuery("insert into StockRegister(CreatedBy) values('" + dsLoginId.Tables[0].Rows[0]["LoginId"].ToString() + "')");
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Incharge Location assign Successfully.');", true);
    }

    protected void BindInchargeDetails()
    {
        DataSet dsLoDetails = new DataSet();
        dsLoDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EmpAssignToLocation '" + lblUser.Text + "'," + ddlSerchEmp.SelectedValue);
        divLocationAssign.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='30%'>Incharge Details</th>";
        ZoneInfo += "<th width='30%'>Location Details</th>";
        // ZoneInfo += "<th width='20%'>Status</th>";
        //ZoneInfo += "<th width='20%'>Change Location</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsLoDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";

            ZoneInfo += "<td class='center' width='30%'><table><tr><td><b>Name</b> :" + dsLoDetails.Tables[0].Rows[i]["InName"].ToString() + "(" + dsLoDetails.Tables[0].Rows[i]["InMobile"].ToString() + ")</td>";
            ZoneInfo += "<tr><td><b>Department</b> : " + dsLoDetails.Tables[0].Rows[i]["department"].ToString() + "</td></tr>";
            ZoneInfo += "<tr><td><b>Designation</b> : " + dsLoDetails.Tables[0].Rows[i]["Designation"].ToString() + "</td></tr></table></td>";
            ZoneInfo += "<td class='center' width='30%'><table><tr><td><b>Zone</b> :" + dsLoDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td>";
            ZoneInfo += "<tr>";
            if (dsLoDetails.Tables[0].Rows[0]["ChangeLocationStatus"].ToString() == "1")
            {
                ZoneInfo += "<td><span class='label label-success' title='Location Chnages' style='font-size: 15.998px;'>Location Changed</span></td>";
            }
            else if (dsLoDetails.Tables[0].Rows[0]["ChangeLocationStatus"].ToString() == null)
            {
                ZoneInfo += "<td></td>";
            }
            ZoneInfo += "</tr></table></td>";
            //ZoneInfo += "<td class='center' width='20%'>";
            //if (dsLoDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
            //{
            //    ZoneInfo += "<span class='label label-success' title='On Location' style='font-size: 15.998px;'>On Location </span>";
            //}
            //else
            //{
            //    ZoneInfo += "<span class='label label-important' title='Not Assign Location' style='font-size: 15.998px;'>Not Assign Location</span>";
            //}
            //ZoneInfo += "</td>";
            //ZoneInfo += "<td class='center' width='20%'><a class='btn btn-info' href='Admin_LocationAssignToEmployee.aspx?AAEId=" + dsLoDetails.Tables[0].Rows[i]["AAEId"].ToString() + "'><i class='icon-edit icon-white'></i>Change Location </a></td>";
            // ZoneInfo += "<td class='center' width='20%'><a class='btn btn-info' href='Admin_LocationAssignToEmployee.aspx?ZoneId=" + dsLoDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'><i class='icon-edit icon-white'></i>Change Location </a></td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        divLocationAssign.InnerHtml = ZoneInfo.ToString();
    }

    protected void btnAddAcademy_Click(object sender, EventArgs e)
    {
        BindBtnAcademyClickGrid();
    }

    protected void btnChnageLoc_Click(object sender, EventArgs e)
    {
        //if (ddlLocatio.SelectedIndex == 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select New Zone to Change Location.');", true);
        //}
        //else
        //{
        //    DataSet dsExist = new DataSet();
        //    dsExist = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ExistingAssginLocation '" + ddlLocatio.SelectedValue + "'");
        //    if (dsExist.Tables[0].Rows.Count > 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Already Assigned location to " + dsExist.Tables[0].Rows[0]["InName"].ToString() + ".');", true);
        //    }
        //    else
        //    {
        //        DataSet dsval = new DataSet();
        //        dsval = DAL.DalAccessUtility.GetDataInDataSet("select UserTypeId from Incharge where InchargeId='" + Session["EmpId"].ToString() + "'");
        //        if (dsval.Tables[0].Rows[0]["UserTypeId"].ToString() == "2")
        //        {
        //            DataSet dsA = new DataSet();
        //            dsA = DAL.DalAccessUtility.GetDataInDataSet("select AcaId from Academy where ZoneId='" + ddlLocatio.SelectedValue + "'");
        //            DAL.DalAccessUtility.ExecuteNonQuery("update AcademyAssignToEmployee set Active=0, ChangeLocationStatus=0 where EmpId='" + Session["EmpId"].ToString() + "' ");
        //            //string Ai = string.Empty;
        //            foreach (DataRow drAi in dsA.Tables[0].Rows)
        //            {
        //                string Ai = string.Empty;
        //                Ai = Ai + "," + drAi["AcaId"].ToString();
        //                string[] Ai0 = Ai.Split(',');
        //                foreach (string Ai1 in Ai.Split(','))
        //                {
        //                    //DAL.DalAccessUtility.ExecuteNonQuery("insert into AcademyAssignToEmployee (AcaId,EmpId,Active,CreatedBy,CreatedOn,ZoneId,ChangeLocationStatus,ChangeLocationOn,LastLocation) values ('" + ddlZone.SelectedValue + "','" + Ai1 + "','" + ddlEmpl.SelectedValue + "','1','" + lblUser.Text + "','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "') ");
        //                    DAL.DalAccessUtility.ExecuteNonQuery("exec USP_ChangeLocation '" + ddlLocatio.SelectedValue + "','" + Ai1 + "','" + Session["EmpId"].ToString() + "','" + lblUser.Text + "'");
        //                    DAL.DalAccessUtility.ExecuteNonQuery("delete AcademyAssignToEmployee where AcaId=0");
        //                }
        //            }
        //            DataSet dsLoginId = DAL.DalAccessUtility.GetDataInDataSet("select LoginId from Incharge where InchargeId='" + Session["EmpId"].ToString() + "'");
        //            DAL.DalAccessUtility.ExecuteNonQuery("insert into StockRegister(CreatedBy) values('" + dsLoginId.Tables[0].Rows[0]["LoginId"].ToString() + "')");
        //            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Incharge Location chnage Successfully.');", true);
        //            BindInchargeDetails();
        //        }
        //    }
        //}
    }

    protected void ddlSerchEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  Session["UserTypeId"] = ddlSerchEmp.SelectedValue;
        if (ddlSerchEmp.SelectedValue == "2" || ddlSerchEmp.SelectedValue == "14" || ddlSerchEmp.SelectedValue == "15" || ddlSerchEmp.SelectedValue == "16"
            || ddlSerchEmp.SelectedValue == "17" || ddlSerchEmp.SelectedValue == "18" || ddlSerchEmp.SelectedValue == "19" || ddlSerchEmp.SelectedValue == "20")
        {
            DataSet dsLoDetails = new DataSet();
            dsLoDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EmpAssignToLocation '" + lblUser.Text + "','" + ddlSerchEmp.SelectedValue + "'");
            divLocationAssign.InnerHtml = string.Empty;
            string ZoneInfo = string.Empty;
            ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
            ZoneInfo += "<thead>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<th width='30%'>Incharge Details</th>";
            ZoneInfo += "<th width='30%'>Location Details</th>";
            //  ZoneInfo += "<th width='20%'>Status</th>";
            // ZoneInfo += "<th width='20%'>Change Location</th>";
            ZoneInfo += "</tr>";
            ZoneInfo += "</thead>";
            ZoneInfo += "<tbody>";
            for (int i = 0; i < dsLoDetails.Tables[0].Rows.Count; i++)
            {
                ZoneInfo += "<tr>";

                ZoneInfo += "<td class='center' width='30%'><table><tr><td><b>Name</b> :" + dsLoDetails.Tables[0].Rows[i]["InName"].ToString() + "(" + dsLoDetails.Tables[0].Rows[i]["InMobile"].ToString() + ")</td>";
                ZoneInfo += "<tr><td><b>Department</b> : " + dsLoDetails.Tables[0].Rows[i]["department"].ToString() + "</td></tr>";
                ZoneInfo += "<tr><td><b>Designation</b> : " + dsLoDetails.Tables[0].Rows[i]["Designation"].ToString() + "</td></tr></table></td>";
                ZoneInfo += "<td class='center' width='30%'><table><tr><td><b>Zone</b> :" + dsLoDetails.Tables[0].Rows[i]["ZoneName"].ToString() + "</td>";
                ZoneInfo += "<tr>";
                if (dsLoDetails.Tables[0].Rows[0]["ChangeLocationStatus"].ToString() == "1")
                {
                    ZoneInfo += "<td><span class='label label-success' title='Location Chnages' style='font-size: 15.998px;'>Location Changed</span></td>";
                }
                else if (dsLoDetails.Tables[0].Rows[0]["ChangeLocationStatus"].ToString() == null)
                {
                    ZoneInfo += "<td></td>";
                }
                ZoneInfo += "</tr></table></td>";
                //ZoneInfo += "<td class='center' width='20%'>";
                //if (dsLoDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
                //{
                //    ZoneInfo += "<span class='label label-success' title='On Location' style='font-size: 15.998px;'>On Location </span>";
                //}
                //else
                //{
                //    ZoneInfo += "<span class='label label-important' title='Not Assign Location' style='font-size: 15.998px;'>Not Assign Location</span>";
                //}
                //ZoneInfo += "</td>";
                //ZoneInfo += "<td class='center' width='20%'><a class='btn btn-info' href='Admin_LocationAssignToEmployee.aspx?AAEId=" + dsLoDetails.Tables[0].Rows[i]["AAEId"].ToString() + "'><i class='icon-edit icon-white'></i>Change Location </a></td>";
                //Session["EmpId"] = dsLoDetails.Tables[0].Rows[i]["InchargeId"].ToString();
                //  ZoneInfo += "<td class='center' width='20%'><a class='btn btn-info' href='Admin_LocationAssignToEmployee.aspx?ZoneId=" + dsLoDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'><i class='icon-edit icon-white'></i>Change Location </a></td>";
                ZoneInfo += "</tr>";
            }
            ZoneInfo += "</tbody>";
            ZoneInfo += "</table>";
            divLocationAssign.InnerHtml = ZoneInfo.ToString();
        }
        else if (ddlSerchEmp.SelectedValue == "3")
        {
            DataSet dsLoDetails = new DataSet();
            dsLoDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EmpAssignToLocation4Audit '" + lblUser.Text + "','3'");
            divLocationAssign.InnerHtml = string.Empty;
            string ZoneInfo = string.Empty;
            ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
            ZoneInfo += "<thead>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<th width='30%'>Incharge Details</th>";
            ZoneInfo += "<th width='30%'>Location Details</th>";
            //ZoneInfo += "<th width='20%'>Status</th>";
            //ZoneInfo += "<th width='20%'>Change Location</th>";
            ZoneInfo += "</tr>";
            ZoneInfo += "</thead>";
            ZoneInfo += "<tbody>";
            for (int i = 0; i < dsLoDetails.Tables[0].Rows.Count; i++)
            {
                ZoneInfo += "<tr>";

                ZoneInfo += "<td class='center' width='30%'><table><tr><td><b>Name</b> :" + dsLoDetails.Tables[0].Rows[i]["InName"].ToString() + "(" + dsLoDetails.Tables[0].Rows[i]["InMobile"].ToString() + ")</td>";
                ZoneInfo += "<tr><td><b>Department</b> : " + dsLoDetails.Tables[0].Rows[i]["department"].ToString() + "</td></tr>";
                ZoneInfo += "<tr><td><b>Designation</b> : " + dsLoDetails.Tables[0].Rows[i]["Designation"].ToString() + "</td></tr></table></td>";
                DataSet dsAuLoc = DAL.DalAccessUtility.GetDataInDataSet("exec USP_AuditUserLocation '" + dsLoDetails.Tables[0].Rows[i]["EmpId"].ToString() + "'");
                ZoneInfo += "<td class='center' width='30%'><table><tr><td><b>Zone</b> :" + dsAuLoc.Tables[0].Rows[0]["Result"].ToString() + "</td>";
                ZoneInfo += "<tr>";
                if (dsLoDetails.Tables[0].Rows[0]["ChangeLocationStatus"].ToString() == "1")
                {
                    ZoneInfo += "<td><span class='label label-success' title='Location Chnages' style='font-size: 15.998px;'>Location Changed</span></td>";
                }
                else if (dsLoDetails.Tables[0].Rows[0]["ChangeLocationStatus"].ToString() == null)
                {
                    ZoneInfo += "<td></td>";
                }
                ZoneInfo += "</tr></table></td>";
                //ZoneInfo += "<td class='center' width='20%'>";
                //if (dsLoDetails.Tables[0].Rows[i]["Active"].ToString() == "1")
                //{
                //    ZoneInfo += "<span class='label label-success' title='On Location' style='font-size: 15.998px;'>On Location </span>";
                //}
                //else
                //{
                //    ZoneInfo += "<span class='label label-important' title='Not Assign Location' style='font-size: 15.998px;'>Not Assign Location</span>";
                //}
                //ZoneInfo += "</td>";
                //ZoneInfo += "<td class='center' width='20%'><a class='btn btn-info' href='Admin_LocationAssignToEmployee.aspx?ZoneId=" + dsLoDetails.Tables[0].Rows[i]["ZoneId"].ToString() + "'><i class='icon-edit icon-white'></i>Change Location </a></td>";
                ZoneInfo += "<td class='center' width='20%'><a class='btn btn-info' href='#'><i class='icon-edit icon-white'></i>Change Location </a></td>";
                ZoneInfo += "</tr>";
            }
            ZoneInfo += "</tbody>";
            ZoneInfo += "</table>";
            divLocationAssign.InnerHtml = ZoneInfo.ToString();
        }
    }

    protected void chkCtrl_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkbox = (CheckBox)sender;
        GridViewRow Grow = (GridViewRow)chkbox.NamingContainer;
        DataSet dsExist = null;
        HiddenField hdnAcaId = Grow.FindControl("hdnAcaId") as HiddenField;
        // string aid = Grow.Cells[1].Text;
        if (Session["UserTypeID"].ToString() == "1")
        {
            dsExist = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct Incharge.InName FROM AcademyAssignToEmployee INNER JOIN Incharge ON AcademyAssignToEmployee.EmpId = Incharge.InchargeId where Incharge.InchargeID=" + ddlEmpl.SelectedValue + " and AcaId='" + hdnAcaId.Value + "'");

            if (dsExist.Tables[0].Rows.Count > 0)
            {
                if (dsExist.Tables[0].Rows[0]["InName"].ToString() != null)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('You can't Assign, This Academy already alloted to Mr. " + dsExist.Tables[0].Rows[0]["InName"].ToString() + " Audit Officer');", true);
                    chkbox.Enabled = false;
                    chkbox.Checked = false;
                }

            }
        }

    }

    //protected void GridAcademy_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        //CheckBox chkbox = (CheckBox)sender;
    //        //GridViewRow Grow = (GridViewRow)chkbox.NamingContainer;
    //        //string aid = Grow.Cells[1].Text; 
    //        DataSet dsExist = DAL.DalAccessUtility.GetDataInDataSet("SELECT distinct Incharge.InName FROM AcademyAssignToEmployee INNER JOIN Incharge ON AcademyAssignToEmployee.EmpId = Incharge.InchargeId where Incharge.UserTypeId=3 ");
    //        if (dsExist.Tables[0].Rows.Count > 0)
    //        {
    //            if (dsExist.Tables[0].Rows[0]["InName"].ToString() != null)
    //            {
    //                e.Row.BackColor = Color.FromName("#D80000");
    //            }
    //            else
    //            {
    //                e.Row.BackColor = Color.FromName("#F0F0F0");
    //            }
    //        }
    //        else
    //        {

    //        }
    //    }
    //}

    protected void ddlUserTpe4Assign_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataSet dsDept = new DataSet();
        //dsDept = DAL.DalAccessUtility.GetDataInDataSet("select InchargeId,InName from Incharge where UserTypeId='" + ddlUserTpe4Assign.SelectedValue + "' order by InName ASC");
        //ddlEmp4Assign.DataSource = dsDept;
        //ddlEmp4Assign.DataValueField = "InchargeId";
        //ddlEmp4Assign.DataTextField = "InName";
        //ddlEmp4Assign.DataBind();
        //ddlEmp4Assign.Items.Insert(0, "Select Incharge");
        //ddlEmp4Assign.SelectedIndex = 0;
        //tdAcaDemy.Visible = true;
        //tdEmpl.Visible = true;
    }

    protected void ddlEmp4Assign_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataSet dsLo = DAL.DalAccessUtility.GetDataInDataSet("select distinct ZoneId from AcademyAssignToEmployee where EmpId='" + ddlEmp4Assign.SelectedValue + "' and Active=1");
        //if (dsLo.Tables[0].Rows.Count > 0)
        //{
        //    DataSet dsZoneName = DAL.DalAccessUtility.GetDataInDataSet("SELECT DISTINCT STUFF(( select DISTINCT ' , ' +Zone.ZoneName FROM AcademyAssignToEmployee INNER JOIN Zone ON AcademyAssignToEmployee.ZoneId = Zone.ZoneId where AcademyAssignToEmployee.Active=1 and EmpId='" + ddlEmp4Assign.SelectedValue + "' for xml path('')),1,1,'') as Result from AcademyAssignToEmployee");
        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + ddlEmp4Assign.SelectedItem.Text + " is a incharge of " + dsZoneName.Tables[0].Rows[0]["Result"].ToString() + ".');", true);
        //}
        //pnlSelectAcademy.Visible = false;
    }

    protected void btnAssignFrmHomePage_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(1000);
        //string zid = Request.QueryString["ZoneIdLoc"].ToString();
        //DataSet dsExist = new DataSet();
        //dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct * from AcademyAssignToEmployee where EmpId='" + ddlEmp4Assign.SelectedValue + "' and ZoneId='" + zid + "'");
        //if (dsExist.Tables[0].Rows.Count > 0)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Already Assigned location to this Incharge.');", true);
        //}
        //else
        //{
        //    DataSet dsval = new DataSet();
        //    dsval = DAL.DalAccessUtility.GetDataInDataSet("select UserTypeId from Incharge where InName='" + ddlEmp4Assign.SelectedItem.Text + "'");
        //    if (dsval.Tables[0].Rows[0]["UserTypeId"].ToString() == "2")
        //    {


        //        DataSet dsA = new DataSet();
        //        dsA = DAL.DalAccessUtility.GetDataInDataSet("select AcaId from Academy where ZoneId='" + zid + "'");
        //        //string Ai = string.Empty;
        //        foreach (DataRow drAi in dsA.Tables[0].Rows)
        //        {
        //            string Ai = string.Empty;
        //            Ai = Ai + "," + drAi["AcaId"].ToString();
        //            string[] Ai0 = Ai.Split(',');
        //            foreach (string Ai1 in Ai.Split(','))
        //            {
        //                DAL.DalAccessUtility.ExecuteNonQuery("insert into AcademyAssignToEmployee(ZoneId,AcaId,EmpId,Active,CreatedBy,CreatedOn) values ('" + zid + "','" + Ai1 + "','" + ddlEmp4Assign.SelectedValue + "','1','" + lblUser.Text + "','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "') ");
        //                DAL.DalAccessUtility.ExecuteNonQuery("delete AcademyAssignToEmployee where AcaId=0");
        //            }
        //        }
        //        DataSet dsLoginId = DAL.DalAccessUtility.GetDataInDataSet("select LoginId from Incharge where InName='" + ddlEmp4Assign.SelectedItem.Text + "'");
        //        DAL.DalAccessUtility.ExecuteNonQuery("insert into StockRegister(CreatedBy) values('" + dsLoginId.Tables[0].Rows[0]["LoginId"].ToString() + "')");
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Incharge Location assign Successfully.');", true);
        //        BindInchargeDetails();
        //    }
        //    else
        //    {

        //        //string data = "";
        //        //DataTable dt = new DataTable();
        //        //DataRow dr = null;
        //        //dt.Columns.Add(new DataColumn("AcaId", typeof(string)));
        //        //dt.Columns.Add(new DataColumn("AcaName", typeof(string)));
        //        //foreach (GridViewRow row in grdAcaFromHome.Rows)
        //        //{
        //        //    if (row.RowType == DataControlRowType.DataRow)
        //        //    {
        //        //        CheckBox chkRow = (row.Cells[0].FindControl("chkCtrlHome") as CheckBox);
        //        //        if (chkRow.Checked)
        //        //        {
        //        //            dr = dt.NewRow();
        //        //            dr["AcaId"] = row.Cells[1].Text;
        //        //            dr["AcaName"] = row.Cells[2].Text;
        //        //            dt.Rows.Add(dr);
        //        //            data = data + dr["AcaId"] + ",";
        //        //        }
        //        //    }

        //        //}
        //        //dt.AcceptChanges();
        //        //string Aid = data;
        //        //string output = Aid.Remove(Aid.Length - 1, 1);
        //        //string[] values = output.Split(',');
        //        //foreach (string da in output.Split(','))
        //        //{

        //        //    DataSet dsZoneId = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId from Academy where AcaId='" + da + "'");
        //        //    DAL.DalAccessUtility.ExecuteNonQuery("insert into AcademyAssignToEmployee(ZoneId,AcaId,EmpId,Active,CreatedBy,CreatedOn) values ('" + zid + "','" + da + "','" + ddlEmp4Assign.SelectedValue + "','1','" + lblUser.Text + "','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "') ");
        //        //}
        //        ////DAL.DalAccessUtility.ExecuteNonQuery("insert into AcademyAssignToEmployee(ZoneId,AcaId,EmpId,Active,CreatedBy,CreatedOn) values ('" + ddlZone.SelectedValue + "','','" + ddlEmpl.SelectedValue + "','1','"+ lblUser.Text +"','"+ System.DateTime.Now.ToString("yyyy-MM-dd")   +"') ");
        //        //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Incharge Location assign Successfully.');", true);
        //        //BindInchargeDetails();

        //        //}
        //    }
        //}
    }

    private void AssignAllAcademy()
    {
        int InchargeID = int.Parse(ddlEmpl.SelectedValue);
        AdminController adminController = new AdminController();
        adminController.AssignAllAcademiesToUser(InchargeID);

    }

    private void BindBtnAcademyClickGrid()
    {
        GridAcademy.Visible = true;
        string data = "";
        foreach (GridViewRow row in GridZone.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkCtrl") as CheckBox);
                if (chkRow.Checked)
                {
                    HiddenField hdnZoneID = row.FindControl("hdnZoneID") as HiddenField;
                    data += hdnZoneID.Value + ",";

                }
            }
        }

        DataSet dsAca = new DataSet();
        if (data.Length > 1)
        {
            string output = data.Substring(0, data.Length - 1);

            if (ModuleID == 1)
            {
                dsAca = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId,AcaId,AcaName from Academy where ZoneId in (" + output + ") order by AcaName ASC");
            }
            else
            {
                dsAca = DAL.DalAccessUtility.GetDataInDataSet("SELECT A.ZoneId,A.AcaId,A.AcaName FROM [TransportZoneAcademyRelation] TR Inner Join Academy A  on A.AcaID=TR.TransportAcaID WHERE TR.ZoneId in (" + output + ") order by A.AcaName ASC");
            }
            GridAcademy.DataSource = dsAca;
            GridAcademy.DataBind();

            SelectAcademy(output);
        }
    }

    private void SelectAcademy(string output)
    {
        DataSet dsAcaEmp = new DataSet();
        dsAcaEmp = DAL.DalAccessUtility.GetDataInDataSet("select distinct Ase.AcaId ,A.AcaName from Academy A inner join AcademyAssignToEmployee Ase on A.AcaId = Ase.AcaId where Ase.ZoneId in (" + output + ") and Ase.EmpId='" + ddlEmpl.SelectedValue + "'");

        if (dsAcaEmp.Tables.Count > 0)
        {
            for (int i = 0; i < dsAcaEmp.Tables[0].Rows.Count; i++)
            {
                foreach (GridViewRow items in GridAcademy.Rows)
                {
                    CheckBox chkCtrl = items.FindControl("chkCtrl") as CheckBox;
                    HiddenField hdnAcaId = items.FindControl("hdnAcaId") as HiddenField;
                    if (dsAcaEmp.Tables[0].Rows[i]["AcaID"].ToString() == hdnAcaId.Value)
                    {
                        chkCtrl.Checked = true;
                    }
                }
            }
        }
    }

    private void BindZoneGridOnSelectedEmp()
    {
        foreach (GridViewRow item in GridZone.Rows)
        {
            CheckBox chkCtrl = item.FindControl("chkCtrl") as CheckBox;
            chkCtrl.Checked = false;

        }
        DataSet dsLo = new DataSet();
        dsLo = DAL.DalAccessUtility.GetDataInDataSet("select distinct ZoneId from AcademyAssignToEmployee where EmpId='" + ddlEmpl.SelectedValue + "' and Active=1");

        if (dsLo.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsLo.Tables[0].Rows.Count; i++)
            {
                foreach (GridViewRow item in GridZone.Rows)
                {
                    CheckBox chkCtrl = item.FindControl("chkCtrl") as CheckBox;
                    HiddenField hdnZoneID = item.FindControl("hdnZoneID") as HiddenField;
                    if (dsLo.Tables[0].Rows[i]["ZoneID"].ToString() == hdnZoneID.Value)
                    {
                        chkCtrl.Checked = true;
                        //chkCtrl.Enabled = false;
                    }
                }
            }
        }
    }

    private void ClearData()
    {
        ddlDept.ClearSelection();
        ddlEmpl.ClearSelection();
        chkAllZone.Checked = false;
        ddlZone.ClearSelection();
        foreach (GridViewRow item in GridZone.Rows)
        {
            CheckBox chkCtrl = item.FindControl("chkCtrl") as CheckBox;
            chkCtrl.Checked = false;
        }
        foreach (GridViewRow item in GridAcademy.Rows)
        {
            CheckBox chkCtrl = item.FindControl("chkCtrl") as CheckBox;
            chkCtrl.Checked = false;
        }
    }

    protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)GridAcademy.HeaderRow.FindControl("chkboxSelectAll");
        foreach (GridViewRow row in GridAcademy.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkCtrl");
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

    #region CommentedExtraCode
    protected void BindAcademyFromHome(string id)
    {
        //DataSet dsAca = new DataSet();
        //dsAca = DAL.DalAccessUtility.GetDataInDataSet("select AcaId,AcaName from Academy where ZoneId = '" + id + "'");
        //grdAcaFromHome.DataSource = dsAca;
        //grdAcaFromHome.DataBind();
    }
    protected void GetDetailFromAdminHomePage(string id)
    {
        //DataSet dsZOneName = DAL.DalAccessUtility.GetDataInDataSet("select ZoneName from Zone where ZoneId='" + id + "'");
        //lblZoneName.Text = dsZOneName.Tables[0].Rows[0]["ZoneName"].ToString();
        //tdAcaDemy.Visible = false;
        //tdEmpl.Visible = false;
    }
    protected void GetAllotmentDtails(string id)
    {
        //DataSet dsAllotLoDetails = new DataSet();
        //dsAllotLoDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EmpAssignToLocationDetailByZoneId '" + id + "', '" + Session["UserTypeId"].ToString() + "','" + Session["EmpId"].ToString() + "'");
        //lblEmp.Text = dsAllotLoDetails.Tables[0].Rows[0]["InName"].ToString();
        //lblCrtLocation.Text = dsAllotLoDetails.Tables[0].Rows[0]["ZoneName"].ToString();
        //lblDept.Text = dsAllotLoDetails.Tables[0].Rows[0]["department"].ToString();
        //lblDegis.Text = dsAllotLoDetails.Tables[0].Rows[0]["Designation"].ToString();
        ////Session["EmpId"] = dsAllotLoDetails.Tables[0].Rows[0]["InchargeId"].ToString();
        ////Bind Zone

        //DataSet dsZone = new DataSet();
        //dsZone = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId,ZoneName from Zone where Active=1 and ZoneId<> '" + id + "'");
        //ddlLocatio.DataSource = dsZone;
        //ddlLocatio.DataValueField = "ZoneId";
        //ddlLocatio.DataTextField = "ZoneName";
        //ddlLocatio.DataBind();
        //ddlLocatio.Items.Insert(0, "Select Zone");
        //ddlLocatio.SelectedIndex = 0;
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataSet dsZo = DAL.DalAccessUtility.GetDataInDataSet("select distinct EmpId from AcademyAssignToEmployee where ZoneId='" + ddlZone.SelectedValue + "' and Active=1");
        //if (dsZo.Tables[0].Rows.Count > 0)
        //{
        //    DataSet dsDepId = DAL.DalAccessUtility.GetDataInDataSet("select DepId,UserTypeId from Incharge where InchargeId='" + dsZo.Tables[0].Rows[0]["EmpId"].ToString() + "'");
        //    if (dsDepId.Tables[0].Rows[0]["DepId"].ToString() == ddlDept.SelectedValue)
        //    {
        //        if (dsDepId.Tables[0].Rows[0]["UserTypeId"].ToString() == "2")
        //        {
        //            DataSet dsEmpName = DAL.DalAccessUtility.GetDataInDataSet("select InName from Incharge where InchargeId='" + dsZo.Tables[0].Rows[0]["EmpId"].ToString() + "'");
        //            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + dsEmpName.Tables[0].Rows[0]["InName"].ToString() + " already assigned to " + ddlZone.SelectedItem.Text + ", Please Assign another Zone.');", true);
        //        }
        //    }
        //}

    }
    protected void btnAddAca_Click(object sender, EventArgs e)
    {


    }
    #endregion
    protected void btnCreateIncharge_Click(object sender, EventArgs e)
    {
        if (Session["UserTypeID"].ToString() == ((int)TypeEnum.UserType.TRANSPORTADMIN).ToString())
        {
            Response.Redirect("Transport_NewEmployee.aspx");
        }
        else if (Session["UserTypeID"].ToString() == ((int)TypeEnum.UserType.WORKSHOPADMIN).ToString())
        {
            Response.Redirect("AkalWorkshop_Incharge.aspx");
        }
        else
        {
            Response.Redirect("Admin_Incharge.aspx");
        }
    }
}
