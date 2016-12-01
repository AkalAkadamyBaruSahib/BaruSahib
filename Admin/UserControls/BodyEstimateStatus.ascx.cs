using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UserControls_BodyEstimateStatus : System.Web.UI.UserControl
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
                lblUser.Text = Session["InchargeID"].ToString();
            }
            if (Request.QueryString["EstimateID"] != null)
            {
                getReceivedMaterial(Request.QueryString["EstimateID"].ToString());
            }
            BindAcademy();
            getEstimateStatusDetails(-1);
            getClosedEstimateStatusDetails(-1);

        }
    }

    private void BindAcademy()
    {
        UsersRepository repo = new UsersRepository(new AkalAcademy.DataContext());
        int inchargeID = Convert.ToInt16(Session["InchargeID"].ToString());
        List<Academy> acaList = repo.GetAcademyByInchargeID(inchargeID);

        ddlAcademy.DataSource = acaList;
        ddlAcademy.DataTextField = "AcaName";
        ddlAcademy.DataValueField = "AcaID";
        ddlAcademy.DataBind();
        ddlAcademy.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Academy--", "0"));

        ddlClosedAcademies.DataSource = acaList;
        ddlClosedAcademies.DataTextField = "AcaName";
        ddlClosedAcademies.DataValueField = "AcaID";
        ddlClosedAcademies.DataBind();
        ddlClosedAcademies.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Academy--", "0"));
    }

    protected void ddlAcademy_SelectedIndexChanged(object sender, EventArgs e)
    {
        int acaID = int.Parse(ddlAcademy.SelectedValue);
        getEstimateStatusDetails(acaID);
    }

    protected void ddlClosedAcademies_SelectedIndexChanged(object sender, EventArgs e)
    {
        int acaID = int.Parse(ddlClosedAcademies.SelectedValue);
        getClosedEstimateStatusDetails(acaID);

        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", String.Format(System.Globalization.CultureInfo.InvariantCulture,
           "var _Page_tabToSelect = {0};", 1), true);

    }


    [WebMethod]
    public void getEstimateStatusDetails(int AcaID)
    {
        int UserTypeID = Convert.ToInt32(Session["UserTypeID"].ToString());
        int UserID = Convert.ToInt32(Session["InchargeID"].ToString());
        DataTable dtapproved = new DataTable();

        List<Estimate> storeRegister = new List<Estimate>();
        PurchaseRepository storeRepository = new PurchaseRepository(new AkalAcademy.DataContext());
        StoreRepository storeRepo = new StoreRepository(new AkalAcademy.DataContext());
        System.Data.EnumerableRowCollection<System.Data.DataRow> dtApproved = null;

        if (AcaID > 0)
        {
            if (UserTypeID == (int)TypeEnum.UserType.CONSTRUCTION)
            {
                storeRegister = storeRepository.MaterialReceivedStatusAcaID((int)TypeEnum.PurchaseSourceID.Mohali, UserID, AcaID);
            }
            else
            {
                storeRegister = storeRepository.MaterialReceivedStatusForAdminAcaID((int)TypeEnum.PurchaseSourceID.Mohali, UserID, AcaID);
            }
        }
        else
        {
            if (UserTypeID == (int)TypeEnum.UserType.CONSTRUCTION)
            {
                storeRegister = storeRepository.MaterialReceivedStatus((int)TypeEnum.PurchaseSourceID.Mohali, UserID);
            }
            else
            {
                storeRegister = storeRepository.MaterialReceivedStatusForAdmin((int)TypeEnum.PurchaseSourceID.Mohali, UserID);
            }
        }

        divEstimateDetails.InnerHtml = string.Empty;
        divCloseEstimateDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        string CloseEstimate = string.Empty;

        int estID = -1;

        ZoneInfo += "<div class='box-content'>";
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th style='display:none;'></th>";
        ZoneInfo += "<th style='display:none;'></th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";

        foreach (Estimate register in storeRegister)
        {
            if (register.IsReceived == false)
            {
                ZoneInfo += "<tr>";
                ZoneInfo += "<td style='display:none;'>1</td>";
                ZoneInfo += "<td>";
                ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
                ZoneInfo += "<tr>";
                ZoneInfo += "<td><b style='color:red;'>Estimate No:</b> " + register.EstId + "</td>";
                ZoneInfo += "<td class='center'><b style='color:red;'>Sanction Date:</b> " + register.ModifyOn + "</td>";
                ZoneInfo += "<td class='center'><b style='color:red;'>Sub Estimate:</b> " + register.SubEstimate + "</td>";
                ZoneInfo += "<td class='center'><b style='color:red;'>Academy:</b> " + register.Academy.AcaName + "</td>";
                ZoneInfo += "<td class='center'><table><tr><td>[CloseButton]</td><td><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + register.EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a></td></tr></table><td>";
                ZoneInfo += "</tr>";
                ZoneInfo += "</table>";
                ZoneInfo += "<table border='1' class='table table-striped table-bordered bootstrap-datatable datatable'>";
                ZoneInfo += "<tr style='color:Green;'>";
                ZoneInfo += "<th width='5%'><b>SNo.</b></th>";
                ZoneInfo += "<th width='200px'>Material Name</th>";
                ZoneInfo += "<th>Required Quantity</th>";
                ZoneInfo += "<th>Purchase Quantity</th>";
                ZoneInfo += "<th>Purchaser Officer</th>";
                ZoneInfo += "<th> Dispatch Quantity</th>";
                //ZoneInfo += "<th> Pending Quantity</th>";
                ZoneInfo += "<th>Dispatch Status</th>";
                ZoneInfo += "</tr>";

                DataTable dsMatDetails = new DataTable();
                estID = register.EstId;

                dsMatDetails = storeRepo.GetStockMaterialInfo(estID, (int)TypeEnum.PurchaseSourceID.Mohali).Tables[0];
                int ReceivedCount = 0;
                decimal TotalPurchaseQty = -1;
                for (int j = 0; j < dsMatDetails.Rows.Count; j++)
                {

                    ZoneInfo += "<tr>";
                    ZoneInfo += "<td>" + (j + 1) + "</td>";
                    ZoneInfo += "<td>" + dsMatDetails.Rows[j]["MatName"].ToString() + "<br/><b>Unit:-</b> " + dsMatDetails.Rows[j]["UnitName"].ToString() + "</td>";
                    // Required Quantity
                    ZoneInfo += "<td>" + dsMatDetails.Rows[j]["Qty"].ToString() + "</td>";

                    ZoneInfo += "<td>" + dsMatDetails.Rows[j]["PurchaseQty"].ToString() + "</td>";// Purchase Qty
                    TotalPurchaseQty = Convert.ToDecimal(dsMatDetails.Rows[j]["PurchaseQty"].ToString());
                    if (TotalPurchaseQty > 0)
                    {
                        ReceivedCount++;
                    }
                    //In Store Quantity
                    ZoneInfo += "<td class='left'>";
                    ZoneInfo += "<table>";
                    ZoneInfo += "<tr><td> <b>Name:</b> " + dsMatDetails.Rows[j]["InName"].ToString() + " </td></tr>";
                    ZoneInfo += "<tr><td> <b>Contact Number:</b> " + dsMatDetails.Rows[j]["InMobile"].ToString() + " </td></tr>";
                    if (string.IsNullOrEmpty(dsMatDetails.Rows[j]["EmployeeAssignDateTime"].ToString()))
                    {
                        ZoneInfo += "<tr><td><b>Assigned Date:</b> </td></tr>";
                    }
                    else
                    {
                        ZoneInfo += "<tr><td style='color:darkred;'><b>Assigned Date:</b> " + dsMatDetails.Rows[j]["EmployeeAssignDateTime"].ToString() + "</td></tr>";
                    }
                    ZoneInfo += "</table>";
                    ZoneInfo += "</td>";
                    //End 
                    ZoneInfo += "<td>" + dsMatDetails.Rows[j]["DispatchQuantity"].ToString() + "</td>";

                    if (Convert.ToDecimal(dsMatDetails.Rows[j]["Qty"].ToString()) > Convert.ToDecimal(dsMatDetails.Rows[j]["PurchaseQty"].ToString()))
                    {
                        ZoneInfo += "<td style='color:#cc3300;'>Pending</td>";
                    }
                    else if (dsMatDetails.Rows[j]["Qty"].ToString() == dsMatDetails.Rows[j]["PurchaseQty"].ToString() && dsMatDetails.Rows[j]["Qty"].ToString() == dsMatDetails.Rows[j]["DispatchQuantity"].ToString())
                    {
                        ZoneInfo += "<td style='color:#cc3300;'>Dispatch</td>";
                    }
                    else if (dsMatDetails.Rows[j]["Qty"].ToString() == dsMatDetails.Rows[j]["PurchaseQty"].ToString() && dsMatDetails.Rows[j]["Qty"].ToString() != dsMatDetails.Rows[j]["DispatchQuantity"].ToString())
                    {
                        ZoneInfo += "<td style='color:#cc3300;'>Partial Dispatch</td>";
                    }
                    ZoneInfo += "</tr>";
                }
                ZoneInfo += "</table>";
                ZoneInfo += "</td>";
                ZoneInfo += "</tr>";

                if (dsMatDetails.Rows.Count == ReceivedCount)
                {
                    ZoneInfo = ZoneInfo.Replace("[CloseButton]", "<a href='Emp_ReceivedEstimate.aspx?EstimateID=" + register.EstId + "'><span class='label label-warning' style='font-size: 15.998px;'>Close Estimate</span></a>");
                }
                else
                {
                    ZoneInfo = ZoneInfo.Replace("[CloseButton]", "");
                }
            }
        }

        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";
        ZoneInfo += "</div>";

        divEstimateDetails.InnerHtml = ZoneInfo.ToString();
    }

    private void getClosedEstimateStatusDetails(int AcaID)
    {
        int UserTypeID = Convert.ToInt32(Session["UserTypeID"].ToString());
        int UserID = Convert.ToInt32(Session["InchargeID"].ToString());
        DataTable dtapproved = new DataTable();

        List<Estimate> storeRegister = new List<Estimate>();
        PurchaseRepository storeRepository = new PurchaseRepository(new AkalAcademy.DataContext());
        StoreRepository storeRepo = new StoreRepository(new AkalAcademy.DataContext());
        System.Data.EnumerableRowCollection<System.Data.DataRow> dtApproved = null;

        if (AcaID > 0)
        {
            if (UserTypeID == (int)TypeEnum.UserType.CONSTRUCTION)
            {
                storeRegister = storeRepository.MaterialClosedReceivedStatusAcaID((int)TypeEnum.PurchaseSourceID.Mohali, UserID, AcaID);
            }
            else
            {
                storeRegister = storeRepository.MaterialClosedReceivedStatusForAdminAcaID((int)TypeEnum.PurchaseSourceID.Mohali, UserID, AcaID);
            }
        }
        else
        {
            if (UserTypeID == (int)TypeEnum.UserType.CONSTRUCTION)
            {
                storeRegister = storeRepository.MaterialClosedReceivedStatus((int)TypeEnum.PurchaseSourceID.Mohali, UserID);
            }
            else
            {
                storeRegister = storeRepository.MaterialClosedReceivedStatusForAdmin((int)TypeEnum.PurchaseSourceID.Mohali, UserID);
            }
        }

        string ZoneInfo = string.Empty;
        string CloseEstimate = string.Empty;

        int estID = -1;

        CloseEstimate += "<div class='box-content'>";
        CloseEstimate += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        CloseEstimate += "<thead>";
        CloseEstimate += "<tr>";
        CloseEstimate += "<th style='display:none;'></th>";
        CloseEstimate += "<th style='display:none;'></th>";
        CloseEstimate += "</tr>";
        CloseEstimate += "</thead>";
        CloseEstimate += "<tbody>";

        foreach (Estimate register in storeRegister)
        {
            if (register.IsReceived == true)
            {
                CloseEstimate += "<tr>";
                CloseEstimate += "<td style='display:none;'>1</td>";
                CloseEstimate += "<td>";
                CloseEstimate += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
                CloseEstimate += "<tr>";
                CloseEstimate += "<td><b style='color:red;'>Estimate No:</b> " + register.EstId + "</td>";
                CloseEstimate += "<td class='center'><b style='color:red;'>Sanction Date:</b> " + register.ModifyOn + "</td>";
                CloseEstimate += "<td class='center'><b style='color:red;'>Sub Estimate:</b> " + register.SubEstimate + "</td>";
                CloseEstimate += "<td class='center'><b style='color:red;'>Academy:</b> " + register.Academy.AcaName + "</td>";
                CloseEstimate += "<td class='center'><a href='Purchase_MaterialToBeDispatch.aspx?EstId=" + register.EstId + "'><span class='label label-warning'  style='font-size: 15.998px;'>Print</span></a><td>";

                CloseEstimate += "</tr>";
                CloseEstimate += "</table>";
                CloseEstimate += "<table border='1' class='table table-striped table-bordered bootstrap-datatable datatable'>";
                CloseEstimate += "<tr style='color:Green;'>";
                CloseEstimate += "<th width='5%'><b>SNo.</b></th>";
                CloseEstimate += "<th width='200px'>Material Name</th>";
                CloseEstimate += "<th>Required Quantity</th>";
                CloseEstimate += "<th>Purchase Quantity</th>";
                CloseEstimate += "<th>Purchaser Officer</th>";
                CloseEstimate += "<th> Dispatch Quantity</th>";
                CloseEstimate += "</tr>";

                DataTable dsMatDetails = new DataTable();
                estID = register.EstId;

                dsMatDetails = storeRepo.GetStockMaterialInfo(estID, (int)TypeEnum.PurchaseSourceID.Mohali).Tables[0];
                int ReceivedCount = 0;
                decimal TotalPurchaseQty = -1;
                for (int j = 0; j < dsMatDetails.Rows.Count; j++)
                {

                    CloseEstimate += "<tr>";
                    CloseEstimate += "<td>" + (j + 1) + "</td>";
                    CloseEstimate += "<td>" + dsMatDetails.Rows[j]["MatName"].ToString() + "<br/><b>Unit:-</b> " + dsMatDetails.Rows[j]["UnitName"].ToString() + "</td>";
                    // Required Quantity
                    CloseEstimate += "<td>" + dsMatDetails.Rows[j]["Qty"].ToString() + "</td>";

                    CloseEstimate += "<td>" + dsMatDetails.Rows[j]["PurchaseQty"].ToString() + "</td>";// Purchase Qty
                    TotalPurchaseQty = Convert.ToDecimal(dsMatDetails.Rows[j]["PurchaseQty"].ToString());
                    if (TotalPurchaseQty > 0)
                    {
                        ReceivedCount++;
                    }
                    //In Store Quantity
                    CloseEstimate += "<td class='left'>";
                    CloseEstimate += "<table>";
                    CloseEstimate += "<tr><td> <b>Name:</b> " + dsMatDetails.Rows[j]["InName"].ToString() + " </td></tr>";
                    CloseEstimate += "<tr><td> <b>Contact Number:</b> " + dsMatDetails.Rows[j]["InMobile"].ToString() + " </td></tr>";
                    if (string.IsNullOrEmpty(dsMatDetails.Rows[j]["EmployeeAssignDateTime"].ToString()))
                    {
                        CloseEstimate += "<tr><td><b>Assigned Date:</b> </td></tr>";
                    }
                    else
                    {
                        CloseEstimate += "<tr><td style='color:darkred;'><b>Assigned Date:</b> " + dsMatDetails.Rows[j]["EmployeeAssignDateTime"].ToString() + "</td></tr>";
                    }
                    CloseEstimate += "</table>";
                    CloseEstimate += "</td>";
                    //End 
                    CloseEstimate += "<td>" + dsMatDetails.Rows[j]["DispatchQuantity"].ToString() + "</td>";

                    CloseEstimate += "</tr>";
                }
                CloseEstimate += "</table>";
                CloseEstimate += "</td>";
                CloseEstimate += "</tr>";
            }
        }
            CloseEstimate += "</tbody>";
            CloseEstimate += "</table>";
            CloseEstimate += "</div>";
       

        divCloseEstimateDetails.InnerHtml = CloseEstimate.ToString();
    }
    private void getReceivedMaterial(string EstID)
    {  
        int InchargeID = Convert.ToInt32(Session["InchargeID"].ToString());
        PurchaseControler purchaseControler = new PurchaseControler();
        purchaseControler.ReceivedMaterial(Convert.ToInt32(EstID), InchargeID);
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "<script>alert('Estimate Closed Successfully');</script>", false);
        getEstimateStatusDetails(-1);
    }
  
}