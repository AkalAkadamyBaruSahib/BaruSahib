using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RateUpload : System.Web.UI.Page
{
    public static int UserTypeID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            
            if (Session["UserTypeID"] != null)
            {
                UserTypeID = int.Parse(Session["UserTypeID"].ToString());
            }
            BindListMaterialTypes();
        }
    }
    
    private void BindListMaterialName()
    {
        DataSet dsMat = new DataSet();
        dsMat = DAL.DalAccessUtility.GetDataInDataSet("select MatId,MatName from Material where Active=1 and MatTypeId IN (" + lstMaterialTypes.SelectedValue + ") order by MatName");
        lstMaterials.DataSource = dsMat;
        lstMaterials.DataValueField = "MatId";
        lstMaterials.DataTextField = "MatName";
        lstMaterials.DataBind();

    }

    protected void lstMaterialTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindListMaterialName();
    }

    private void FirstGridViewRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Col1", typeof(string)));
        dt.Columns.Add(new DataColumn("Col2", typeof(string)));
        dt.Columns.Add(new DataColumn("Col3", typeof(string)));
        dt.Columns.Add(new DataColumn("Col4", typeof(string)));
        dt.Columns.Add(new DataColumn("Col5", typeof(string)));

        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Col1"] = string.Empty;
        dr["Col2"] = string.Empty;
        dr["Col3"] = string.Empty;
        dr["Col4"] = string.Empty;
        dr["Col5"] = string.Empty;
        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;
        grvMaterialDetails.DataSource = dt;
        grvMaterialDetails.DataBind();
    }

    private void AddNewRow()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList box2 = (DropDownList)grvMaterialDetails.Rows[rowIndex].Cells[1].FindControl("ddlMatType");
                    DropDownList box3 = (DropDownList)grvMaterialDetails.Rows[rowIndex].Cells[2].FindControl("ddlMat");
                    DropDownList box4 = (DropDownList)grvMaterialDetails.Rows[rowIndex].Cells[3].FindControl("ddlUnit");
                    TextBox box5 = (TextBox)grvMaterialDetails.Rows[rowIndex].Cells[4].FindControl("txtRate");
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Col1"] = box2.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = box3.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col3"] = box4.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col4"] = box5.Text;
                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;

                grvMaterialDetails.DataSource = dtCurrentTable;
                grvMaterialDetails.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData();
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < grvMaterialDetails.Rows.Count; i++)
                {
                    DropDownList box2 = (DropDownList)grvMaterialDetails.Rows[rowIndex].Cells[1].FindControl("ddlMatType");
                    DropDownList box3 = (DropDownList)grvMaterialDetails.Rows[rowIndex].Cells[2].FindControl("ddlMat");
                    DropDownList box4 = (DropDownList)grvMaterialDetails.Rows[rowIndex].Cells[3].FindControl("ddlUnit");
                    TextBox box5 = (TextBox)grvMaterialDetails.Rows[rowIndex].Cells[4].FindControl("txtRate");
                  

                    box2.SelectedValue = dt.Rows[i]["Col1"].ToString();
                     BindMaterialTypes(box2, box3);
                    box3.SelectedIndex = box3.Items.IndexOf(box3.Items.FindByValue(dt.Rows[i]["Col2"].ToString().Trim()));
                    BindUnit(box3, box4);
                    box4.SelectedIndex = box4.Items.IndexOf(box4.Items.FindByValue(dt.Rows[i]["Col3"].ToString().Trim()));
                    box5.Text = dt.Rows[i]["Col4"].ToString();
                    rowIndex++;
                }
                ViewState["CurrentTable"] = dt;
            }
        }
    }

    public void BindMaterialTypes(DropDownList MatType, DropDownList Materrial)
    {

        DataSet dsMat = new DataSet();

        if (ViewState["MatTypeID"] == null || ViewState["MatTypeID"].ToString() != MatType.SelectedValue)
        {
            ViewState["MatTypeID"] = MatType.SelectedValue;
            Session["dsMat"] = dsMat = DAL.DalAccessUtility.GetDataInDataSet("select MatId,MatName from Material where Active=1 and MatTypeId='" + MatType.SelectedValue + "'");
        }
        else
        {
            dsMat = Session["dsMat"] as DataSet;
        }

        Materrial.DataSource = dsMat;
        Materrial.DataValueField = "MatId";
        Materrial.DataTextField = "MatName";
        Materrial.DataBind();
        Materrial.Items.Insert(0, "Material");
        Materrial.SelectedIndex = 0;
    }

    private void getGridData()
    {
        int rowIndex = 0;
        DataTable dt = new DataTable();
        dt.Columns.Add("RowNumber");
        dt.Columns.Add("Col1");
        dt.Columns.Add("Col2");
        dt.Columns.Add("Col3");
        dt.Columns.Add("Col4");

        for (int i = 0; i < grvMaterialDetails.Rows.Count; i++)
        {
            DropDownList box2 = (DropDownList)grvMaterialDetails.Rows[rowIndex].Cells[2].FindControl("ddlMatType");
            DropDownList box3 = (DropDownList)grvMaterialDetails.Rows[rowIndex].Cells[3].FindControl("ddlMat");
            DropDownList box4 = (DropDownList)grvMaterialDetails.Rows[rowIndex].Cells[5].FindControl("ddlUnit");
            TextBox box5 = (TextBox)grvMaterialDetails.Rows[rowIndex].Cells[6].FindControl("txtRate");


            DataRow dr = null;
            dr = dt.NewRow();
            dr["RowNumber"] = rowIndex + 1;
            dr["Col1"] = box2.SelectedIndex;
            dr["Col2"] = box3.SelectedIndex;
            dr["Col3"] = box4.SelectedIndex;
            dr["Col4"] = box5.Text;
            dt.Rows.Add(dr);
            rowIndex++;
        }
        ViewState["CurrentTable"] = dt;
        dt = null;
    }

    protected void grvMaterialDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlMateType = (DropDownList)e.Row.FindControl("ddlMatType");
            DataSet dsMatType = new DataSet();

            dsMatType = ViewState["dsMatType"] as DataSet;
            if (dsMatType == null)
            {
                dsMatType = DAL.DalAccessUtility.GetDataInDataSet("select MatTypeId,MatTypeName from MaterialType where Active=1 and MatTypeName<>'OTHERS'");
                ViewState["dsMatType"] = dsMatType;
            }
            ddlMateType.DataSource = dsMatType;
            ddlMateType.DataValueField = "MatTypeId";
            ddlMateType.DataTextField = "MatTypeName";
            ddlMateType.DataBind();
            ddlMateType.Items.Insert(0, "Material Type");
            ddlMateType.SelectedIndex = 0;
        }

    }

    protected void ddlMatType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList ddlMateType = (DropDownList)row.FindControl("ddlMatType");
        DropDownList ddlMaterail = (DropDownList)row.FindControl("ddlMat");
        BindMaterialTypes(ddlMateType, ddlMaterail);
    }

    protected void ddlMat_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList ddlMaterail = (DropDownList)row.FindControl("ddlMat");
        DropDownList ddlUnit = (DropDownList)row.FindControl("ddlUnit");
        TextBox Rate = (TextBox)row.FindControl("txtRate");

        DataSet dsMatRate = new DataSet();
        dsMatRate = DAL.DalAccessUtility.GetDataInDataSet("Select MatCost from Material where MatId ='" + ddlMaterail.SelectedValue + "'");
        Rate.Text = dsMatRate.Tables[0].Rows[0]["MatCost"].ToString();
        BindUnit(ddlMaterail, ddlUnit);
   }

    public void BindUnit(DropDownList Materrial, DropDownList Unit)
    {
        DataSet dsMat = new DataSet();
        dsMat = DAL.DalAccessUtility.GetDataInDataSet("select distinct U.UnitName,U.UnitId from Unit U inner join  Material M  on M.UnitId = U.UnitId");
        Unit.DataSource = dsMat;
        Unit.DataValueField = "UnitId";
        Unit.DataTextField = "UnitName";
        Unit.DataBind();
    }

    private void SetDefaultMaterialTypes()
    {
        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        DropDownList ddlMatType = null;
        DropDownList ddlMat = null;
        DropDownList ddlUnit = null;
        string values = String.Join(", ", lstMaterials.Items.Cast<ListItem>()
                                                 .Where(i => i.Selected)
                                                 .Select(i => i.Value));
        string[] IDs = values.Split(',');
        int length = grvMaterialDetails.Rows.Count == 1 ? IDs.Length - 1 : IDs.Length;
        int indexValue = grvMaterialDetails.Rows.Count;

        for (int i = 0; i < IDs.Length; i++)
        {
            if (grvMaterialDetails.Rows.Count == 0)
            {
                FirstGridViewRow();
            }
            else
            {
                AddNewRow();
            }

            ddlMatType = new DropDownList();
            ddlMat = new DropDownList();
            ddlMatType = (DropDownList)grvMaterialDetails.Rows[indexValue].Cells[2].FindControl("ddlMatType");
            ddlMat = (DropDownList)grvMaterialDetails.Rows[indexValue].Cells[3].FindControl("ddlMat");
            ddlUnit = (DropDownList)grvMaterialDetails.Rows[indexValue].Cells[4].FindControl("ddlUnit");

            DataSet dsMat = new DataSet();
            dsMat = DAL.DalAccessUtility.GetDataInDataSet("select mt.MatTypeId from Material m inner join MaterialType mt on mt.MatTypeId = m.MatTypeId where m.MatId = " + IDs[i] + "");

            ddlMatType.ClearSelection();
            ddlMatType.Items.FindByValue(dsMat.Tables[0].Rows[0]["MatTypeId"].ToString()).Selected = true;
            ddlMatType_SelectedIndexChanged(ddlMatType, new EventArgs());

            ddlMat.ClearSelection();
            ddlMat.Items.FindByValue(IDs[i].Trim().ToString()).Selected = true;
            ddlMat_SelectedIndexChanged(ddlMat, new EventArgs());

            ddlUnit.ClearSelection();
            DataSet dsUMat = new DataSet();
            dsUMat = DAL.DalAccessUtility.GetDataInDataSet("select Matid,UnitId from Material where Matid = '" + ddlMat.SelectedValue + "'");
            ddlUnit.ClearSelection();
            ddlUnit.Items.FindByValue(dsUMat.Tables[0].Rows[0]["UnitId"].ToString()).Selected = true;

            indexValue++;
        }
    }

    protected void btnloadMaterials_Click(object sender, EventArgs e)
    {
        SetDefaultMaterialTypes();
        btnsave.Visible = true;
    }

    private void BindListMaterialTypes()
    {
        DataSet dsMatType = new DataSet();
        dsMatType = ViewState["dsMatType"] as DataSet;
        if (dsMatType == null)
        {
            dsMatType = DAL.DalAccessUtility.GetDataInDataSet("select MatTypeId,MatTypeName from MaterialType where Active=1 and MatTypeName<>'OTHERS' order by MatTypeName");
            ViewState["dsMatType"] = dsMatType;
        }

        lstMaterialTypes.DataSource = dsMatType;
        lstMaterialTypes.DataValueField = "MatTypeId";
        lstMaterialTypes.DataTextField = "MatTypeName";
        lstMaterialTypes.DataBind();
    }

    public bool ValidateMaterials()
    {
        bool sflag = true;
        foreach (GridViewRow row in grvMaterialDetails.Rows)
        {
            DropDownList ddlMatType = (DropDownList)row.FindControl("ddlMatType");
            DropDownList ddlMat = (DropDownList)row.FindControl("ddlMat");
            DropDownList ddlUnit = (DropDownList)row.FindControl("ddlUnit");
            TextBox txtRate = (TextBox)row.FindControl("txtRate");

            if (ddlMatType.SelectedValue == "Material Type" || ddlMat.SelectedValue == "Material" || txtRate.Text == "" || ddlUnit.SelectedValue == "")
            {
                sflag = false;
                break;
            }
        }
        return sflag;
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

        Material material = null;
        MaterialNonApprovedRate materialnonapprovedrate = null;
        List<Material> materials = new List<Material>();

        string MaterialType, Material, Unit, Rate;
        string InchargeID = Session["InchargeID"].ToString();

        foreach (GridViewRow gvrow in grvMaterialDetails.Rows)
        {
            material = new Material();

            DropDownList ddlmt = (DropDownList)gvrow.FindControl("ddlMatType");
            DropDownList ddlma = (DropDownList)gvrow.FindControl("ddlMat");
            DropDownList ddlunit = (DropDownList)gvrow.FindControl("ddlUnit");
            TextBox txtra = (TextBox)gvrow.FindControl("txtRate");

            Unit = ddlunit.SelectedValue;
            Material = ddlma.SelectedValue;
            MaterialType = ddlmt.SelectedValue;

            Rate = txtra.Text;

            materialnonapprovedrate = new MaterialNonApprovedRate();
            Material = ddlma.SelectedValue;
            Rate = txtra.Text;
            materialnonapprovedrate.MatID = int.Parse(Material);
            materialnonapprovedrate.Rate = decimal.Parse(Rate);
            materialnonapprovedrate.CreatedBy = InchargeID;
            materialnonapprovedrate.CreatedOn = DateTime.Now;
            DAL.DalAccessUtility.ExecuteNonQuery("Update Material set IsRateApproved= 0 where MatId = '" + Material + "'");
            ConstructionUserRepository repo = new ConstructionUserRepository(new AkalAcademy.DataContext());
            repo.SaveMaterial(materialnonapprovedrate);
         }
       
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Rate Upload Successfully.');", true);

        grvMaterialDetails.DataSource = null;
        grvMaterialDetails.DataBind();
        btnsave.Visible = false;
    }

    protected void grvMaterialDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowData();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable"] = dt;
                grvMaterialDetails.DataSource = dt;
                grvMaterialDetails.DataBind();

                for (int i = 0; i < grvMaterialDetails.Rows.Count - 1; i++)
                {
                    grvMaterialDetails.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousData();
            }
        }
    }

    private void SetRowData()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList box2 = (DropDownList)grvMaterialDetails.Rows[rowIndex].Cells[2].FindControl("ddlMatType");
                    DropDownList box3 = (DropDownList)grvMaterialDetails.Rows[rowIndex].Cells[3].FindControl("ddlMat");
                    DropDownList box4 = (DropDownList)grvMaterialDetails.Rows[rowIndex].Cells[4].FindControl("ddlUnit");
                    TextBox box5 = (TextBox)grvMaterialDetails.Rows[rowIndex].Cells[5].FindControl("txtRate");
                  drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Col1"] = box2.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col2"] = box3.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col3"] = box4.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col4"] = box5.Text;
                    rowIndex++;
                }
                ViewState["CurrentTable"] = dtCurrentTable;
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
      
    }
}
