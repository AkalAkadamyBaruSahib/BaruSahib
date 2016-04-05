using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Purchase_AddVendorInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblUser.Text = Session["EmailId"].ToString();
            }
            BindActiveVendorsDetails();
            BindListMaterialTypes();
            if (Request.QueryString["VIdEdit"] != null)
            {
                //GetPurchaseSourceInfoToUpdate(Request.QueryString["PSIdEdit"].ToString());
                getVendorDetails(Request.QueryString["VIdEdit"].ToString());
                btnEdit.Visible = true; ;
                btnSave.Visible = false;
                pnlActiveVendor.Visible = true;
            }
            if (Request.QueryString["VIdDel"] != null)
            {
                DeleteVendorInfo(Request.QueryString["VIdDel"].ToString());
            }

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        DataSet dsExist = new DataSet();
        dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct VendorContactNo from VendorInfo where VendorContactNo='" + txtPhone.Text + "'");
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Contact Number Already Exist.');", true);
        }
        else
        {
            VendorInfo vendorinfo = new VendorInfo();
            vendorinfo.VendorName = txtVendorName.Text;
            vendorinfo.VendorAddress = txtAddress.Text;
            vendorinfo.VendorContactNo = txtPhone.Text;
            vendorinfo.CreatedOn = DateTime.Now;
            vendorinfo.ModifyBy = lblUser.Text;
            vendorinfo.ModifyOn = DateTime.Now;
            vendorinfo.Active = true;
            VendorMaterialRelation vendormaterialrelation = null;
            vendorinfo.VendorMaterialRelation = new List<VendorMaterialRelation>();
            foreach (ListItem item in lstMaterials.Items)
            {
                if (item.Selected)
                {
                    vendormaterialrelation = new VendorMaterialRelation();
                    vendormaterialrelation.MatType = Convert.ToInt32(drpMaterialTypes.SelectedValue);
                    vendormaterialrelation.MatID = Convert.ToInt32(item.Value);
                    vendormaterialrelation.CreatedOn = DateTime.Now;
                    vendormaterialrelation.ModifyOn = DateTime.Now;
                    vendorinfo.VendorMaterialRelation.Add(vendormaterialrelation);
                }
            }
            PurchaseRepository repo = new PurchaseRepository(new AkalAcademy.DataContext());
            if (Request.QueryString["VIdEdit"] == null)
            {
                repo.AddNewVendorInformation(vendorinfo);
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Vendor Information Create Successfully.');", true);
            BindActiveVendorsDetails();
            ClearData();
        }
    }

    protected void BindActiveVendorsDetails()
    {
        DataSet dsVDDetails = new DataSet();
        dsVDDetails = DAL.DalAccessUtility.GetDataInDataSet("select ID,VendorName,VendorContactNo,VendorAddress,Active from VendorInfo where Active=1");
        divVendorDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='20%'>Vendor Name</th>";
        ZoneInfo += "<th width='20%'>Address</th>";
        ZoneInfo += "<th width='20%'>Contact No</th>";
        ZoneInfo += "<th width='20%'>Status</th>";
        ZoneInfo += "<th width='20%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsVDDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='20%'>" + dsVDDetails.Tables[0].Rows[i]["VendorName"].ToString() + "</td>";
            ZoneInfo += "<td width='20%'>" + dsVDDetails.Tables[0].Rows[i]["VendorAddress"].ToString() + "</td>";
            ZoneInfo += "<td width='20%'>" + dsVDDetails.Tables[0].Rows[i]["VendorContactNo"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='20%'>";
            if (dsVDDetails.Tables[0].Rows[i]["Active"].ToString() == "True")
            {
                ZoneInfo += "<span class='label label-success' style='font-size: 15.998px;' title='Vendor Active'>Active</span>";
            }
            else
            {
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;' title='Vendor Inactive'>InActive</span>";
            }
            ZoneInfo += "<td class='center' width='20%'>";

            ZoneInfo += "<a class='btn btn-info' href='Purchase_AddVendorInfo.aspx?VIdEdit=" + dsVDDetails.Tables[0].Rows[i]["ID"].ToString() + "'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i> Edit";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-danger' href='Purchase_AddVendorInfo.aspx?VIdDel=" + dsVDDetails.Tables[0].Rows[i]["ID"].ToString() + "'>";
            ZoneInfo += "<i class='icon-trash icon-white'></i> Delete";
            ZoneInfo += "</a>";

            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";

        divVendorDetails.InnerHtml = ZoneInfo.ToString();
    }

    protected void DeleteVendorInfo(string vid)
    {
        PurchaseControler purchaseController = new PurchaseControler();
        purchaseController.DeleteVendorInfo(Convert.ToInt32(vid));
        BindActiveVendorsDetails();
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Vendor Inforamation  Delete Successfully.');", true);
    }

    private void getVendorDetails(string vid)
    {
        DataSet dsGetVendorDetail = new DataSet();
        dsGetVendorDetail = DAL.DalAccessUtility.GetDataInDataSet("select ID,VendorName,VendorContactNo,VendorAddress,Active,CreatedOn,ModifyOn,ModifyBy from VendorInfo where ID = '" + vid + "' order by ID asc");
        if (dsGetVendorDetail.Tables[0].Rows.Count > 0)
        {
            txtVendorName.Text = dsGetVendorDetail.Tables[0].Rows[0]["VendorName"].ToString();
            txtAddress.Text = dsGetVendorDetail.Tables[0].Rows[0]["VendorAddress"].ToString();
            txtPhone.Text = dsGetVendorDetail.Tables[0].Rows[0]["VendorContactNo"].ToString();
            bool ShowCkBoxValue = Convert.ToBoolean(dsGetVendorDetail.Tables[0].Rows[0]["Active"].ToString());
            chkInactive.Checked = ShowCkBoxValue;
        }

        DataSet dsGetMaterialDetail = new DataSet();
        dsGetMaterialDetail = DAL.DalAccessUtility.GetDataInDataSet("select VendorID,MatID,MatType from VendorMaterialRelation where VendorID = '" + vid + "'");
        //drpMaterialTypes.ClearSelection();
        drpMaterialTypes.SelectedValue = dsGetMaterialDetail.Tables[0].Rows[0]["MatType"].ToString();
        BindMaterialName();
        lstMaterials.ClearSelection();
        foreach (ListItem item in lstMaterials.Items)
        {
            if (dsGetMaterialDetail.Tables[0].Select("MatID=" + item.Value).Count() > 0)
            {
                item.Selected = true;

            }
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string VNameId = Request.QueryString["VIdEdit"];
        DAL.DalAccessUtility.ExecuteNonQuery("exec USP_NewVendorInfoProc '" + txtVendorName.Text + "','" + txtAddress.Text + "','" + txtPhone.Text + "','" + lblUser.Text + "','2','" + VNameId + "','" + chkInactive.Checked + "'");
        
            DAL.DalAccessUtility.ExecuteNonQuery("Delete from VendorMaterialRelation where Vendorid ='" + VNameId + "'");
            foreach (ListItem item in lstMaterials.Items)
            {
                if (item.Selected)
                {
                    DAL.DalAccessUtility.ExecuteNonQuery("insert into VendorMaterialRelation(VendorID,MatID,CreatedOn,ModifyOn,MatType) values('" + VNameId + "','" + Convert.ToInt32(item.Value) + "',GETDATE(),GETDATE(),'" + drpMaterialTypes.SelectedValue + "')");
                }
            }
        
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Vendor Infomation Edit Successfully.');", true);
        BindActiveVendorsDetails();
        ClearData();
        btnEdit.Visible = false;
        btnSave.Visible = true;
        pnlActiveVendor.Visible = false;
    }

    protected void btnCl_Click(object sender, EventArgs e)
    {
        ClearData();
    }

    private void ClearData()
    {
        txtPhone.Text = "";
        txtVendorName.Text = "";
        txtAddress.Text = "";
        chkInactive.Checked = false;
        lstMaterials.Items.Clear();
    }

    protected void chkShowAllVendor_CheckedChanged(object sender, EventArgs e)
    {
        if (chkShowAllVendor.Checked == true)
        {
            BindAllVendor();
        }
        else
        {
            BindActiveVendorsDetails();
        }
    }

    private void BindAllVendor()
    {
        DataSet dsVDDetails = new DataSet();
        dsVDDetails = DAL.DalAccessUtility.GetDataInDataSet("select ID,VendorName,VendorContactNo,VendorAddress,Active from VendorInfo");
        divVendorDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='20%'>Vendor Name</th>";
        ZoneInfo += "<th width='20%'>Address</th>";
        ZoneInfo += "<th width='20%'>Contact No</th>";
        ZoneInfo += "<th width='20%'>Status</th>";
        ZoneInfo += "<th width='20%'>Actions</th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsVDDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='20%'>" + dsVDDetails.Tables[0].Rows[i]["VendorName"].ToString() + "</td>";
            ZoneInfo += "<td width='20%'>" + dsVDDetails.Tables[0].Rows[i]["VendorAddress"].ToString() + "</td>";
            ZoneInfo += "<td width='20%'>" + dsVDDetails.Tables[0].Rows[i]["VendorContactNo"].ToString() + "</td>";
            ZoneInfo += "<td class='center' width='20%'>";
            if (dsVDDetails.Tables[0].Rows[i]["Active"].ToString() == "True")
            {
                ZoneInfo += "<span class='label label-success' style='font-size: 15.998px;' title='Vendor Active'>Active</span>";
            }
            else
            {
                ZoneInfo += "<span class='label label-important' style='font-size: 15.998px;' title='Vendor Inactive'>InActive</span>";
            }
            ZoneInfo += "<td class='center' width='20%'>";

            ZoneInfo += "<a class='btn btn-info' href='Purchase_AddVendorInfo.aspx?VIdEdit=" + dsVDDetails.Tables[0].Rows[i]["ID"].ToString() + "'>";
            ZoneInfo += "<i class='icon-edit icon-white'></i> Edit";
            ZoneInfo += "</a>&nbsp;";
            ZoneInfo += "<a class='btn btn-danger' href='Purchase_AddVendorInfo.aspx?VIdDel=" + dsVDDetails.Tables[0].Rows[i]["ID"].ToString() + "'>";
            ZoneInfo += "<i class='icon-trash icon-white'></i> Delete";
            ZoneInfo += "</a>";

            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";

        divVendorDetails.InnerHtml = ZoneInfo.ToString();
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

        drpMaterialTypes.DataSource = dsMatType;
        drpMaterialTypes.DataValueField = "MatTypeId";
        drpMaterialTypes.DataTextField = "MatTypeName";
        drpMaterialTypes.DataBind();
    }

    protected void lstMaterialTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMaterialName();
    }

    public void BindMaterialName()
    {
        DataSet dsMat = new DataSet();
        dsMat = DAL.DalAccessUtility.GetDataInDataSet("select MatId,MatName from Material where Active=1 and MatTypeId IN ('" + drpMaterialTypes.SelectedValue + "') order by MatName");
        lstMaterials.DataSource = dsMat;
        lstMaterials.DataValueField = "MatId";
        lstMaterials.DataTextField = "MatName";
        lstMaterials.DataBind();
    }

   
    
}