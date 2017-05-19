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

    public int MatTypeID { get; set; }
    public int MatID { get; set; }
    public int VendorID { get; set; }

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
                lblUser.Text = Session["EmailId"].ToString();
            }

            BindListMaterialTypes();
            if (Request.QueryString["MatTypeID"] != null && Request.QueryString["MatID"] != null)
            {
                MatTypeID = Convert.ToInt32(Request.QueryString["MatTypeID"].ToString());
                MatID = Convert.ToInt32(Request.QueryString["MatID"].ToString());
                lstMaterialTypes.SelectedValue = MatTypeID.ToString();
                lstMaterialTypes_SelectedIndexChanged(lstMaterialTypes, new EventArgs());
                lstMaterials.SelectedValue = MatID.ToString();

                btnloadMaterials_Click(btnloadMaterials, new EventArgs());


            }
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

    private void FirstGridViewRow(String[] MAtID)
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Col1", typeof(string)));
        dt.Columns.Add(new DataColumn("Col2", typeof(string)));
        dt.Columns.Add(new DataColumn("Col3", typeof(string)));
        dt.Columns.Add(new DataColumn("Col4", typeof(string)));
        dt.Columns.Add(new DataColumn("Col5", typeof(string)));
        dt.Columns.Add(new DataColumn("Col6", typeof(string)));
        dt.Columns.Add(new DataColumn("Col7", typeof(string)));
        dt.Columns.Add(new DataColumn("Col8", typeof(string)));
        dt.Columns.Add(new DataColumn("Col9", typeof(string)));
        dt.Columns.Add(new DataColumn("Col10", typeof(string)));

        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Col1"] = string.Empty;
        dr["Col2"] = string.Empty;
        dr["Col3"] = string.Empty;
        dr["Col4"] = string.Empty;
        dr["Col5"] = string.Empty;
        dr["Col6"] = string.Empty;
        dr["Col7"] = string.Empty;
        dr["Col8"] = string.Empty;
        dr["Col9"] = string.Empty;
        dr["Col10"] = string.Empty;
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
                    Label box2 = (Label)grvMaterialDetails.Rows[rowIndex].Cells[1].FindControl("lblMaterialType");
                    Label box3 = (Label)grvMaterialDetails.Rows[rowIndex].Cells[2].FindControl("lblMaterial");
                    DropDownList box4 = (DropDownList)grvMaterialDetails.Rows[rowIndex].Cells[3].FindControl("ddlUnit");
                    Label box5 = (Label)grvMaterialDetails.Rows[rowIndex].Cells[4].FindControl("lblCurrentRate");
                    DropDownList box6 = (DropDownList)grvMaterialDetails.Rows[rowIndex].Cells[5].FindControl("drpVendorName");
                    HiddenField box7 = (HiddenField)grvMaterialDetails.Rows[rowIndex].Cells[1].FindControl("hdnMatType");
                    Label box8 = (Label)grvMaterialDetails.Rows[rowIndex].Cells[7].FindControl("hdnMatID");
                    TextBox box9 = (TextBox)grvMaterialDetails.Rows[rowIndex].Cells[8].FindControl("txtMRP");
                    TextBox box10 = (TextBox)grvMaterialDetails.Rows[rowIndex].Cells[9].FindControl("txtDiscount");
                    TextBox box11 = (TextBox)grvMaterialDetails.Rows[rowIndex].Cells[10].FindControl("txtVat");
                    Label box12 = (Label)grvMaterialDetails.Rows[rowIndex].Cells[11].FindControl("txtNetRate");

                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Col1"] = box2.Text;
                    dtCurrentTable.Rows[i - 1]["Col2"] = box3.Text;
                    dtCurrentTable.Rows[i - 1]["Col3"] = box4.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col4"] = box5.Text;
                    dtCurrentTable.Rows[i - 1]["Col5"] = box6.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col6"] = box8.Text;
                    dtCurrentTable.Rows[i - 1]["Col7"] = box9.Text;
                    dtCurrentTable.Rows[i - 1]["Col8"] = box10.Text;
                    dtCurrentTable.Rows[i - 1]["Col9"] = box11.Text;
                    dtCurrentTable.Rows[i - 1]["Col10"] = box12.Text;

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
                    Label box2 = (Label)grvMaterialDetails.Rows[rowIndex].Cells[1].FindControl("lblMaterialType");
                    Label box3 = (Label)grvMaterialDetails.Rows[rowIndex].Cells[2].FindControl("lblMaterial");
                    DropDownList box4 = (DropDownList)grvMaterialDetails.Rows[rowIndex].Cells[3].FindControl("ddlUnit");
                    Label box5 = (Label)grvMaterialDetails.Rows[rowIndex].Cells[4].FindControl("lblCurrentRate");
                    DropDownList box6 = (DropDownList)grvMaterialDetails.Rows[rowIndex].Cells[5].FindControl("drpVendorName");
                    Label box7 = (Label)grvMaterialDetails.Rows[rowIndex].Cells[7].FindControl("hdnMatID");
                    TextBox box9 = (TextBox)grvMaterialDetails.Rows[rowIndex].Cells[8].FindControl("txtMRP");
                    TextBox box10 = (TextBox)grvMaterialDetails.Rows[rowIndex].Cells[9].FindControl("txtDiscount");
                    TextBox box11 = (TextBox)grvMaterialDetails.Rows[rowIndex].Cells[10].FindControl("txtVat");
                    Label box12 = (Label)grvMaterialDetails.Rows[rowIndex].Cells[11].FindControl("txtNetRate");

                    box2.Text = dt.Rows[i]["Col1"].ToString();
                    box3.Text = dt.Rows[i]["Col2"].ToString().Trim();
                    BindUnit(box3, box4);
                    box4.SelectedIndex = box4.Items.IndexOf(box4.Items.FindByValue(dt.Rows[i]["Col3"].ToString().Trim()));
                    box5.Text = dt.Rows[i]["Col4"].ToString();
                    box6.SelectedValue = dt.Rows[i]["Col5"].ToString();
                    box7.Text = dt.Rows[i]["Col6"].ToString();
                    box9.Text = dt.Rows[i]["Col7"].ToString();
                    box10.Text = dt.Rows[i]["Col8"].ToString();
                    box11.Text = dt.Rows[i]["Col9"].ToString();
                    box12.Text = dt.Rows[i]["Col10"].ToString();
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
            Label box2 = (Label)grvMaterialDetails.Rows[rowIndex].Cells[2].FindControl("lblMaterialType");
            Label box3 = (Label)grvMaterialDetails.Rows[rowIndex].Cells[3].FindControl("lblMaterial");
            DropDownList box4 = (DropDownList)grvMaterialDetails.Rows[rowIndex].Cells[5].FindControl("ddlUnit");
            Label box5 = (Label)grvMaterialDetails.Rows[rowIndex].Cells[6].FindControl("lblCurrentRate");
            TextBox box6 = (TextBox)grvMaterialDetails.Rows[rowIndex].Cells[7].FindControl("txtRate");


            DataRow dr = null;
            dr = dt.NewRow();
            dr["RowNumber"] = rowIndex + 1;
            dr["Col1"] = box2.Text;
            dr["Col2"] = box3.Text;
            dr["Col3"] = box4.SelectedValue;
            dr["Col4"] = box5.Text;
            dr["Col5"] = box6.Text;
            dt.Rows.Add(dr);
            rowIndex++;
        }
        ViewState["CurrentTable"] = dt;
        dt = null;
    }

    private void SetDefaultMaterialTypes()
    {
        Label ddlMatType = null;
        Label ddlMat = null;
        HiddenField hdnMatType = null;
        Label hdnMatID = null;
        DropDownList ddlUnit = null;
        Label lblCurrentRate = null;
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
                FirstGridViewRow(IDs);
            }
            else
            {
                AddNewRow();
            }

            ddlMatType = new Label();
            ddlMat = new Label();
            ddlMatType = (Label)grvMaterialDetails.Rows[indexValue].Cells[2].FindControl("lblMaterialType");
            ddlMat = (Label)grvMaterialDetails.Rows[indexValue].Cells[3].FindControl("lblMaterial");
            ddlUnit = (DropDownList)grvMaterialDetails.Rows[indexValue].Cells[4].FindControl("ddlUnit");
            lblCurrentRate = (Label)grvMaterialDetails.Rows[indexValue].Cells[5].FindControl("lblCurrentRate");
            hdnMatType = (HiddenField)grvMaterialDetails.Rows[indexValue].Cells[2].FindControl("hdnMatType");
            hdnMatID = (Label)grvMaterialDetails.Rows[indexValue].Cells[7].FindControl("hdnMatID");

            DataSet dsMat = new DataSet();
            dsMat = DAL.DalAccessUtility.GetDataInDataSet("select mt.MatTypeName,m.MatName,mt.MatTypeId,UnitId,m.MatCost,M.MatID from Material m inner join MaterialType mt on mt.MatTypeId = m.MatTypeId where m.MatId = " + IDs[i] + "");

            ddlMatType.Text = dsMat.Tables[0].Rows[0]["MatTypeName"].ToString();
            ddlMat.Text = dsMat.Tables[0].Rows[0]["MatName"].ToString();
            lblCurrentRate.Text = dsMat.Tables[0].Rows[0]["MatCost"].ToString();
            hdnMatType.Value = dsMat.Tables[0].Rows[0]["MatTypeId"].ToString();
            hdnMatID.Text = dsMat.Tables[0].Rows[0]["MatID"].ToString();

            ddlUnit.ClearSelection();
            ddlUnit.ClearSelection();
            ddlUnit.Items.FindByValue(dsMat.Tables[0].Rows[0]["UnitId"].ToString()).Selected = true;

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
            dsMatType = DAL.DalAccessUtility.GetDataInDataSet("select MatTypeId,MatTypeName from MaterialType where Active=1 order by MatTypeName");
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
            TextBox txtRate = (TextBox)row.FindControl("txtRate");

            if (txtRate.Text == "")
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

        string MaterialType, Material, Unit, Rate, hdnMaterial, MRP, Discount, Vat, NetRate;
        string InchargeID = Session["InchargeID"].ToString();

        int cnt = 0;
        foreach (GridViewRow gvrow in grvMaterialDetails.Rows)
        {
            material = new Material();

            Label ddlmt = (Label)gvrow.FindControl("lblMaterialType");
            Label ddlma = (Label)gvrow.FindControl("lblMaterial");
            DropDownList ddlunit = (DropDownList)gvrow.FindControl("ddlUnit");
            Label lblCurrentRate = (Label)gvrow.FindControl("lblCurrentRate");
            Label hdnMatID = (Label)gvrow.FindControl("hdnMatID");
            TextBox txtMRP = (TextBox)gvrow.FindControl("txtMRP");
            TextBox txtDiscount = (TextBox)gvrow.FindControl("txtDiscount");
            TextBox txtVat = (TextBox)gvrow.FindControl("txtVat");
            Label txtNetRate = (Label)gvrow.FindControl("txtNetRate");
            DropDownList drpVendorName = (DropDownList)gvrow.FindControl("drpVendorName");

            Unit = ddlunit.SelectedValue;
            Material = ddlma.Text;
            MaterialType = ddlmt.Text;
            hdnMaterial = hdnMatID.Text;
            MRP = txtMRP.Text;
            Discount = txtDiscount.Text;
            Vat = txtVat.Text;
            NetRate = txtNetRate.Text;

            decimal DiscountRate = Convert.ToDecimal(MRP) - (decimal.Parse(MRP) * decimal.Parse(Discount) / 100);
            decimal RateAfterDiscountvat = Convert.ToDecimal(DiscountRate) + (Convert.ToDecimal(DiscountRate) * Convert.ToDecimal(Vat) / 100);

          

            materialnonapprovedrate = new MaterialNonApprovedRate();
            Material = ddlma.Text;

            if (RateAfterDiscountvat != 0)
            {
                materialnonapprovedrate.MatID = int.Parse(hdnMaterial);
                materialnonapprovedrate.CreatedBy = InchargeID;
                materialnonapprovedrate.CreatedOn = Utility.GetLocalDateTime(DateTime.UtcNow);
                materialnonapprovedrate.MRP = decimal.Parse(MRP);
                materialnonapprovedrate.Discount = decimal.Parse(Discount);
                materialnonapprovedrate.Vat = decimal.Parse(Vat);
                materialnonapprovedrate.NetRate = RateAfterDiscountvat;
                materialnonapprovedrate.VendorID = int.Parse(drpVendorName.SelectedValue);
                DAL.DalAccessUtility.ExecuteNonQuery("Update Material set UnitID='" + Unit + "', IsRateApproved= 0 where MatID = '" + hdnMaterial + "'");
                ConstructionUserRepository repo = new ConstructionUserRepository(new AkalAcademy.DataContext());
                repo.SaveMaterial(materialnonapprovedrate);

                SendEmailToPurchaseCommitee(Material, lblCurrentRate.Text, RateAfterDiscountvat, lblUser.Text, MaterialType, MRP, Discount, Vat);
            }
            cnt = cnt + 1;
        }

        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Rate Upload Successfully.');", true);

        grvMaterialDetails.DataSource = null;
        grvMaterialDetails.DataBind();
        btnsave.Visible = false;
    }

    public void SendEmailToPurchaseCommitee(string Material, string oldRate, decimal Rate, string UserName, string MaterialTypeID, string MRP, string Discount, string Vat)
    {

        string MsgInfo = string.Empty;
        MsgInfo += "<table style='width:100%;'>";
        MsgInfo += "<tr>";
        MsgInfo += "<td style='padding:0px; text-align:left; width:50%' valign='top'>";
        MsgInfo += "<img src='http://akalsewa.org/img/logoakalnew.png' style='width:100%;' />";
        MsgInfo += "</td>";
        MsgInfo += "<td style='text-align: right; width:40%;'>";
        MsgInfo += "<br /><br />";
        MsgInfo += "<div style='font-style:italic; text-align: right;'>";
        MsgInfo += "Baru Shahib,";
        MsgInfo += "<br />Dist: Sirmaur";
        MsgInfo += "<br />Himachal Pradesh-173001";
        MsgInfo += "</td>";
        MsgInfo += "</tr>";
        MsgInfo += "<tr>";
        MsgInfo += "<td colspan='2' style='height:50px'>";
        MsgInfo += "Please approved New rate for Materials. <a href='http://akalsewa.org/'>Akal Sewa</a>";
        MsgInfo += "</td>";
        MsgInfo += "</tr>";
        MsgInfo += "</table>";
        MsgInfo += "<table border='1' style='width:50%' cellspacing='0' cellpadding='0'>";
        MsgInfo += "<tbody>";
        MsgInfo += "<tr>";
        MsgInfo += "<td>";
        MsgInfo += "<b>Requested By:</b>";
        MsgInfo += "</td>";
        MsgInfo += "<td>";
        MsgInfo += UserName;
        MsgInfo += "</td>";
        MsgInfo += "</tr>";
        MsgInfo += "<tr>";
        MsgInfo += "<td>";
        MsgInfo += "<b>Material Name:</b>";
        MsgInfo += "</td>";
        MsgInfo += "<td>";
        MsgInfo += Material;
        MsgInfo += "</td>";
        MsgInfo += "</tr>";
        MsgInfo += "<tr>";
        MsgInfo += "<td>";
        MsgInfo += "<b>Old Rate:</b>";
        MsgInfo += "</td>";
        MsgInfo += "<td>";
        MsgInfo += "Rs. " + oldRate;
        MsgInfo += "</td>";
        MsgInfo += "</tr>";
        MsgInfo += "<tr>";
        MsgInfo += "<td>";
        MsgInfo += "<b>MRP:</b>";
        MsgInfo += "</td>";
        MsgInfo += "<td>";
        MsgInfo += "Rs. " + MRP;
        MsgInfo += "</td>";
        MsgInfo += "</tr>";
        MsgInfo += "<tr>";
        MsgInfo += "<td>";
        MsgInfo += "<b>Discount:</b>";
        MsgInfo += "</td>";
        MsgInfo += "<td>";
        MsgInfo += Discount + "%";
        MsgInfo += "</td>";
        MsgInfo += "</tr>";
        MsgInfo += "<tr>";
        MsgInfo += "<td>";
        MsgInfo += "<b>Vat:</b>";
        MsgInfo += "</td>";
        MsgInfo += "<td>";
        MsgInfo += Vat + "%";
        MsgInfo += "</td>";
        MsgInfo += "</tr>";
        MsgInfo += "<tr>";
        MsgInfo += "<td>";
        MsgInfo += "<b>Net Rate:</b>";
        MsgInfo += "</td>";
        MsgInfo += "<td>";
        MsgInfo += "Rs. " + Rate;
        MsgInfo += "</td>";
        MsgInfo += "</tr>";
       

        MsgInfo += "</tbody>";

        MsgInfo += "</table>";

        string FileName = string.Empty;
        string to = "dshah@barusahib.org";
        string cc = string.Empty;
        if (MaterialTypeID == ((int)TypeEnum.MatTypeID.TRANSPORTMATERIAL).ToString()) // Transport Material
        {
             cc = "bhupinder@barusahib.org,akaltransport@barusahib.org";
        }
        else if (MaterialTypeID == ((int)TypeEnum.MatTypeID.ELECTRICALMATERIAL).ToString() || MaterialTypeID == ((int)TypeEnum.MatTypeID.MOTORSANDPUMPS).ToString() || MaterialTypeID == ((int)TypeEnum.MatTypeID.EXTERNALELECTRICALWORK).ToString()) // Electrical Material
        {
              cc = "bhupinder@barusahib.org,electricals@barusahib.org";
        }
        else // Construction Material
        {
              cc = "bhupinder@barusahib.org,akalconstruction@barusahib.org";
        }

        try
        {
            // Utility.SendEmailWithoutAttachments(to, cc, MsgInfo, "New Rate Approval Request.");
        }
        catch { }
        finally
        {

        }
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
                    Label box2 = (Label)grvMaterialDetails.Rows[rowIndex].Cells[2].FindControl("lblMaterialType");
                    Label box3 = (Label)grvMaterialDetails.Rows[rowIndex].Cells[3].FindControl("lblMaterial");
                    DropDownList box4 = (DropDownList)grvMaterialDetails.Rows[rowIndex].Cells[4].FindControl("ddlUnit");
                    Label box5 = (Label)grvMaterialDetails.Rows[rowIndex].Cells[5].FindControl("lblCurrentRate");
                    TextBox box6 = (TextBox)grvMaterialDetails.Rows[rowIndex].Cells[6].FindControl("txtRate");
                    Label box7 = (Label)grvMaterialDetails.Rows[rowIndex].Cells[7].FindControl("hdnMatID");
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = i + 1;

                    dtCurrentTable.Rows[i - 1]["Col1"] = box2.Text;
                    dtCurrentTable.Rows[i - 1]["Col2"] = box3.Text;
                    dtCurrentTable.Rows[i - 1]["Col3"] = box4.SelectedValue;
                    dtCurrentTable.Rows[i - 1]["Col4"] = box5.Text;
                    dtCurrentTable.Rows[i - 1]["Col5"] = box6.Text;
                    dtCurrentTable.Rows[i - 1]["Col6"] = box7.Text;
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

    protected void grvMaterialDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlUnit = (DropDownList)e.Row.FindControl("ddlUnit");
            DropDownList drpVendorName = (DropDownList)e.Row.FindControl("drpVendorName");

            DataTable dsUnit = DAL.DalAccessUtility.GetDataInDataSet("select UnitId,UnitName from Unit where Active=1").Tables[0];
            if (dsUnit != null && dsUnit.Rows.Count > 0)
            {
                ddlUnit.DataSource = dsUnit;
                ddlUnit.DataValueField = "UnitId";
                ddlUnit.DataTextField = "UnitName";
                ddlUnit.DataBind();
            }
            DataTable dsVendor = DAL.DalAccessUtility.GetDataInDataSet("select ID,VendorName from VendorInfo where Active=1 order by VendorName asc").Tables[0];
            if (dsVendor != null && dsVendor.Rows.Count > 0)
            {
                drpVendorName.DataSource = dsVendor;
                drpVendorName.DataValueField = "ID";
                drpVendorName.DataTextField = "VendorName";
                drpVendorName.DataBind();
                drpVendorName.Items.Insert(0, new ListItem("--Select Vendor--", "0"));
            }
        }
    }

    public void BindUnit(Label Material, DropDownList Unit)
    {
        DataSet dsMat = new DataSet();
        dsMat = DAL.DalAccessUtility.GetDataInDataSet("select distinct U.UnitName,U.UnitId from Unit U inner join  Material M  on M.UnitId = U.UnitId");
        Unit.DataSource = dsMat;
        Unit.DataValueField = "UnitId";
        Unit.DataTextField = "UnitName";
        Unit.DataBind();
    }

    protected void txtVat_TextChanged(object sender, EventArgs e)
    {
        GridViewRow gvrow = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtMRP = (TextBox)gvrow.FindControl("txtMRP");
        TextBox txtDiscount = (TextBox)gvrow.FindControl("txtDiscount");
        TextBox txtVat = (TextBox)gvrow.FindControl("txtVat");
        Label txtNetRate = (Label)gvrow.FindControl("txtNetRate");

        decimal DiscountRate = Convert.ToDecimal(txtMRP.Text) - (decimal.Parse(txtMRP.Text) * decimal.Parse(txtDiscount.Text) / 100);
        decimal RateAfterDiscountvat = Convert.ToDecimal(DiscountRate) + (Convert.ToDecimal(DiscountRate) * Convert.ToDecimal(txtVat.Text) / 100);
        txtNetRate.Text = RateAfterDiscountvat.ToString();
    }


}
