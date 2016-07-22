﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_UserControls_BodyMaterials : System.Web.UI.UserControl
{
    public TextBox SettxtRate
    {
        get
        {
            return txtRate;
        }
    }
    public static int UserTypeID = -1;
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
                UserTypeID = int.Parse(Session["UserTypeID"].ToString());
            }

            if (UserTypeID == 10)
            {
                divAddNew.Visible = false;
            }
            BindMatType();
            BindUnit();
            if (Request.QueryString["MatId"] != null)
            {
                getMatDetails(Request.QueryString["MatId"].ToString());
                btnEdit.Visible = true;
                btnSave.Visible = false;
                ddlMatType.Enabled = true;

            }
            else if (Request.QueryString["MatIdIA"] != null)
            {
                DeactiveMat(Request.QueryString["MatIdIA"].ToString());
            }
            else if (Request.QueryString["MatIdA"] != null)
            {
                ActiveMat(Request.QueryString["MatIdA"].ToString());
            }
            
        }
    }
    
    protected DataTable BindDatatable()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ExcelMaterial");
        dt = ds.Tables[0];
        return dt;
    }
    protected void btnExecl_Click(object sender, EventArgs e)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Material.xls"));
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

    protected void BindUnit()
    {
        DataSet dsMatType = new DataSet();
        if (Session["BindUnit"] == null)
        {
            dsMatType = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowUnitDetails");
            Session["BindUnit"] = dsMatType;
        }
        else
        {
            dsMatType = Session["BindUnit"] as DataSet;
        }
        ddlUnit.DataSource = dsMatType;
        ddlUnit.DataValueField = "UnitId";
        ddlUnit.DataTextField = "UnitName";
        ddlUnit.DataBind();
        ddlUnit.Items.Insert(0, "Select Unit");
        ddlUnit.SelectedIndex = 0;
    }
    protected void BindMatType()
    {
        DataSet dsMatType = new DataSet();
        if (ModuleID == 2)
        {
            dsMatType = DAL.DalAccessUtility.GetDataInDataSet("Select MatTypeId,MatTypeName from MaterialType Where MatTypeId = 49");
        }
        else
        {
            if (Session["dsMatType"] == null)
            {
                dsMatType = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowMatTypeDetails '" + lblUser.Text + "'");
                Session["dsMatType"] = dsMatType;
            }
            else
            {
                dsMatType = Session["dsMatType"] as DataSet;
            }
        }
        
        ddlMatType.DataSource = dsMatType;
        ddlMatType.DataValueField = "MatTypeId";
        ddlMatType.DataTextField = "MatTypeName";
        ddlMatType.DataBind();
        ddlMatType.Items.Insert(0, "Select Material Type");
        ddlMatType.SelectedIndex = 0;


        ddlMatTypegrid.DataSource = dsMatType;
        ddlMatTypegrid.DataValueField = "MatTypeId";
        ddlMatTypegrid.DataTextField = "MatTypeName";
        ddlMatTypegrid.DataBind();
        ddlMatTypegrid.Items.Insert(0, "Select Material Type");
        ddlMatTypegrid.SelectedIndex = 0;
    }
    private string GetPageName()
    {
        string path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo info = new System.IO.FileInfo(path);
        return info.Name; 
    }

    protected void BindMatDetails(bool IsRefresh, int MatID)
    {
        DataSet dsMatDetails = new DataSet();

        if (Session["dsMatDetails"] == null)
        {
            dsMatDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowMatrialDetails_ByUser '" + lblUser.Text + "'");
            Session["dsMatDetails"] = dsMatDetails;
        }
        else
        {
            dsMatDetails = Session["dsMatDetails"] as DataSet;
        }

        var MatDetails = (from mytable in dsMatDetails.Tables[0].AsEnumerable()
                          where mytable.Field<int>("MatTypeId") == MatID
                          select mytable).CopyToDataTable();

        divMatDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th>Material Type</th>";
        ZoneInfo += "<th>Material Name</th>";
        ZoneInfo += "<th>Material Cost</th>";
        ZoneInfo += "<th>Status</th>";
        ZoneInfo += "<th>Image of Material</th>";
        if (UserTypeID != 2 && UserTypeID != 10 && UserTypeID != 3)
        {
            ZoneInfo += "<th>Actions</th>";
        }
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < MatDetails.Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td>" + MatDetails.Rows[i]["MatTypeName"].ToString() + "</td>";
            ZoneInfo += "<td>" + MatDetails.Rows[i]["MatName"].ToString() + "(" + MatDetails.Rows[i]["UnitName"].ToString() + ")</td>";
            if (MatDetails.Rows[i]["MatCost"].ToString() != "")
            {
                ZoneInfo += "<td>" + string.Format("{0:0.00}", Convert.ToDouble(MatDetails.Rows[i]["MatCost"].ToString())) + "</td>";
            }
            else
            {
                ZoneInfo += "<td>0.00</td>";
            }
            ZoneInfo += "<td class='center'>";
            if (MatDetails.Rows[i]["Active"].ToString() == "1")
            {
                ZoneInfo += "<span class='label label-success' title='Active' style='font-size: 15.998px;'>Active</span>";
            }
            else
            {
                ZoneInfo += "<span class='label label-important' title='Inactive' style='font-size: 15.998px;'>InActive</span>";
            }
            if (MatDetails.Rows[i]["ChangeMatTypeStatus"].ToString() == "0")
            {
                ZoneInfo += "&nbsp;&nbsp;<a class='btn btn-info' href='" + GetPageName() + "?MatIdC=" + MatDetails.Rows[i]["MatId"].ToString() + "'>Change Material Type</a>&nbsp;";
            }
            ZoneInfo += "</td>";

            ZoneInfo += "<td width='25%'><ul class='thumbnails gallery><li id='image-1' class='thumbnail'>";
            ZoneInfo += "<a  style='background:url(" + MatDetails.Rows[i]["ImageUrl"].ToString() + ")'  href='" + MatDetails.Rows[i]["ImageUrl"].ToString() + "'>";
            ZoneInfo += "<img class='grayscale' width='75Px' height='75PX' src='" + MatDetails.Rows[i]["ImageUrl"].ToString() + "' ></a></li></ul></td>";
            if (UserTypeID != 2 && UserTypeID != 10 && UserTypeID != 3)
            {
                ZoneInfo += "<td class='center'>";
                ZoneInfo += "<a class='btn btn-success' href='" + GetPageName() + "?MatIdA=" + MatDetails.Rows[i]["MatId"].ToString() + "'>";
                ZoneInfo += "<i class='icon-zoom-in icon-white'></i> Active";
                ZoneInfo += "</a>&nbsp;";
                ZoneInfo += "<a class='btn btn-info' href='" + GetPageName() + "?MatId=" + MatDetails.Rows[i]["MatId"].ToString() + "'>";
                ZoneInfo += "<i class='icon-edit icon-white'></i> Edit";
                ZoneInfo += "</a>&nbsp;";
                ZoneInfo += "<a class='btn btn-danger' href='" + GetPageName() + "?MatIdIA=" + MatDetails.Rows[i]["MatId"].ToString() + "'>";
                ZoneInfo += "<i class='icon-trash icon-white'></i> Inactive";
                ZoneInfo += "</a>";
                ZoneInfo += "</td>";
            }
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";

        divMatDetails.InnerHtml = ZoneInfo.ToString();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //string zoId = Session["ZoneId"].ToString();
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct MatTypeId,MatName from Material where MatTypeId='" + ddlMatType.SelectedValue + "' and MatName='" + txtMat.Text + "'");
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material with Material Type already assigned to Zone');", true);
        }
        else
        {
            if (txtMat.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Material name.');", true);
            }
            if (ddlMatType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Material Type.');", true);
            }
            if (ddlUnit.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Unit.');", true);
            }
            if (txtRate.Visible && txtRate.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Rate of Material.');", true);
            }
            if (txtRate.Visible && !IsNumeric(txtRate.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Rate in correct format like 12.00 OR 12.');", true);
            }
            else
            {
                string fileNameToSave = string.Empty;

                if (fuMaterialpic.HasFile)
                {
                    string filename = System.IO.Path.GetFileName(fuMaterialpic.FileName);
                    string FileEx = System.IO.Path.GetExtension(fuMaterialpic.FileName);

                    fileNameToSave = "MaterialPics/" + System.Text.RegularExpressions.Regex.Replace(filename + "-" + System.DateTime.Now.ToString(), @"[^0-9a-zA-Z]+", "-").Replace(' ', '-').ToString() + FileEx;
                    fuMaterialpic.SaveAs(Server.MapPath(fileNameToSave));
                }

                double MaterialCost = txtRate.Visible == false ? 0.00 : Convert.ToDouble(txtRate.Text);
                double LocalCost = 0.00;
                string ddl = ddlMatType.SelectedValue;
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewMatProc '" + txtMat.Text + "','" + MaterialCost + "','" + ddl + "','" + lblUser.Text + "','1','','1','" + ddlUnit.SelectedValue + "','" + fileNameToSave + "','" + LocalCost + "'");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material create successfully.');", true);
                Session["dsMatDetails"] = null;
                BindMatDetails(true, int.Parse(ddlMatType.SelectedValue));
                Clr();
            }
        }
    }

    private bool IsNumeric(string value)
    {
        //System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[0-9]+$");
        System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("[-+]?([0-9]*\\.[0-9]+|[0-9]+)");
        return r.IsMatch(value);
    }

    protected void Clr()
    {
        txtRate.Text = "";
        txtMat.Text = "";
        ddlMatType.SelectedIndex = 0;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        DataSet dsExist = new DataSet();
        if (txtMat.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Material name.');", true);
        }
        if (ddlMatType.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Material Type.');", true);
        }
        if (ddlUnit.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Unit.');", true);
        }
        else
        {
            string fileNameToSave = string.Empty;

            if (fuMaterialpic.HasFile)
            {
                string filename = System.IO.Path.GetFileName(fuMaterialpic.FileName);
                string FileEx = System.IO.Path.GetExtension(fuMaterialpic.FileName);

                fileNameToSave = "MaterialPics/" + System.Text.RegularExpressions.Regex.Replace(filename + "-" + System.DateTime.Now.ToString(), @"[^0-9a-zA-Z]+", "-").Replace(' ', '-').ToString() + FileEx;
                fuMaterialpic.SaveAs(Server.MapPath(fileNameToSave));
            }


            string MatId = Request.QueryString["MatId"];
            double MaterialCost = txtRate.Visible == false ? 0.00 : Convert.ToDouble(txtRate.Text);
            string ddl = ddlMatType.SelectedValue;
            DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewMatProc '" + txtMat.Text + "','" + MaterialCost + "','" + ddlMatType.SelectedValue + "','" + lblUser.Text + "','2','" + MatId + "','1','" + ddlUnit.SelectedValue + "','" + fileNameToSave + "'");
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material edit successfully.');", true);
            Session["dsMatDetails"] = null;
            BindMatDetails(true, int.Parse(ddlMatType.SelectedValue));
            Clr();
            btnEdit.Visible = false;
            btnSave.Visible = true;
        }

    }

    private void UpdateSessionDataTable(string Action, string MatID, double MaterialCost)
    {
        DataSet dsMatDetails = Session["dsMatDetails"] as DataSet;
        DataRow dr = dsMatDetails.Tables[0].AsEnumerable().Where(x => (x.Field<int>("MatId")).Equals(MatID)).FirstOrDefault();
        if (Action == "Edit")
        {
            dr[0] = ddlMatType.SelectedItem.Text;
            dr[1] = MatID;
            dr[2] = txtMat.Text;
            dr[3] = MaterialCost;
            dr[7] = ddlUnit.SelectedItem.Text;
        }
        else if (Action == "Active")
        {
            dr[4] = 1;
        }
        else if (Action == "InActive")
        {
            dr[4] = 0;
        }

        Session["dsMatDetails"] = dsMatDetails;
    }
    protected void btnCl_Click(object sender, EventArgs e)
    {
        Clr();
    }
    private void getMatDetails(string ID)
    {
        DataSet dsCouDetails = new DataSet();
        dsCouDetails = DAL.DalAccessUtility.GetDataInDataSet("exec USP_ShowMatrialDetails_ByID '" + ID + "'");
        if (dsCouDetails.Tables[0].Rows.Count > 0)
        {
            txtMat.Text = dsCouDetails.Tables[0].Rows[0]["MatName"].ToString();
            txtRate.Text = dsCouDetails.Tables[0].Rows[0]["MatCost"].ToString();
            ddlMatType.SelectedIndex = ddlMatType.Items.IndexOf(ddlMatType.Items.FindByValue(dsCouDetails.Tables[0].Rows[0]["MatTypeId"].ToString().Trim()));
            BindUnit();
            ddlUnit.SelectedIndex = ddlUnit.Items.IndexOf(ddlUnit.Items.FindByValue(dsCouDetails.Tables[0].Rows[0]["UnitId"].ToString().Trim()));
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please active Material Status before edit..');", true);
        }
    }
    protected void DeactiveMat(string ID)
    {
        DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewMatProc '','00.00','','" + lblUser.Text + "','4','" + ID + "','0','',''");
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material deactive successfully.');", true);

    }
    protected void ActiveMat(string ID)
    {
        DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewMatProc '','00.00','','" + lblUser.Text + "','4','" + ID + "','1','',''");
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material active successfully.');", true);

    }


    protected void ddlMatType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMatDetails(true, int.Parse(ddlMatTypegrid.SelectedValue));
    }
}