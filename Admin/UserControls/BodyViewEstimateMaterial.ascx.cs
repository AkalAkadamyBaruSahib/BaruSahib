using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UserControls_BodyViewEstimateMaterial : System.Web.UI.UserControl
{
    public int ModuleID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ModuleID"] != null)
        {
            ModuleID = int.Parse(Session["ModuleID"].ToString());
        }
        if (Session["EmailId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblUser.Text = Session["EmailId"].ToString();
        }

        ViewState["chklabel"] = null;
        if (!IsPostBack)
        {
            if (Request.QueryString["EstId"] != null)
            {
                getZoneAcaName(Request.QueryString["EstId"].ToString());
                getMaterialDetails(Request.QueryString["IsLocal"].ToString(), Request.QueryString["EstId"].ToString());
                if (ModuleID == 4)
                {
                    gvWorkShopMaterial.Visible = true;
                }
                else
                {
                    gvMaterailDetailForPurchase.Visible = true;
                }
            }
        }
    }
    private void getMaterialDetails(string psid, string id)
    {
        int UserTypeID = Convert.ToInt16(Session["UserTypeID"].ToString());
        string UserID = Session["InchargeID"].ToString();
        DataSet dsAcaDetails = new DataSet();

        if (UserTypeID == (int)(TypeEnum.UserType.CONSTRUCTION))
        {
            dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_EstimateMaterialViewForEmp_ByEmployeeID] '" + id + "'," + psid + ", " + UserID);
        }
        else if (UserTypeID == (int)(TypeEnum.UserType.WORKSHOPEMPLOYEE))
        {
            dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_EstimateMaterialViewForWorkshopEmployeeID] '" + id + "'," + psid + ", " + UserID);
        }
        else
        {
            dsAcaDetails = DAL.DalAccessUtility.GetDataInDataSet("exec [USP_EstimateMaterialViewForPurchase_ByEmployeeID] '" + id + "'," + psid + ", " + UserID);
        }
        if (dsAcaDetails.Tables[0].Rows.Count == 0)
        {
            if (UserTypeID == (int)(TypeEnum.UserType.CONSTRUCTION))
            {
                Response.Redirect("Emp_MaterialDepatchStatus.aspx");
                return;
            }
            else if (UserTypeID == (int)(TypeEnum.UserType.WORKSHOPEMPLOYEE))
            {
                Response.Redirect("Worksho_MaterialToBeDispatch.aspx");
                return;
            }
            else
            {
                Response.Redirect("Purchase_MaterialToBeDispatch.aspx");
                return;
            }
        }
        if (ModuleID == 4)
        {
            gvWorkShopMaterial.DataSource = dsAcaDetails;
            gvWorkShopMaterial.DataBind();
        }
        else
        {
            gvMaterailDetailForPurchase.DataSource = dsAcaDetails;
            gvMaterailDetailForPurchase.DataBind();
        }
    }
    private void getZoneAcaName(string id)
    {
        DataSet dsDetails = new DataSet();
        dsDetails = DAL.DalAccessUtility.GetDataInDataSet(" exec USP_ZoneAndAcademnyNameByEstId '" + id + "'");
        lblAcademy.Text = dsDetails.Tables[0].Rows[0]["AcaName"].ToString();
        lblZoneName.Text = dsDetails.Tables[0].Rows[0]["ZoneName"].ToString();
        DataSet dsZoneId = new DataSet();
        dsZoneId = DAL.DalAccessUtility.GetDataInDataSet("select ZoneId from Zone where ZoneName='" + lblZoneName.Text + "'");
        Session["Zone"] = dsZoneId.Tables[0].Rows[0]["ZoneId"].ToString();
        lblEstId.Text = dsDetails.Tables[0].Rows[0]["EstId"].ToString();
    }

    protected void btnDispatch_Click(object sender, EventArgs e)
    {
        GridViewRow gv = (GridViewRow)((Button)sender).NamingContainer;
        TextBox txtRate = (TextBox)gv.FindControl("txtRate");
        TextBox txtRemark = (TextBox)gv.FindControl("txtRemark");
        TextBox txtPurchaseQty = (TextBox)gv.FindControl("txtPurchaseQty");
        TextBox txtInStore = (TextBox)gv.FindControl("txtInStore");
        HiddenField txtEstID = (HiddenField)gv.FindControl("txtEstID");
        HiddenField txtUnitID = (HiddenField)gv.FindControl("txtUnitID");

        HiddenField txtMatID = (HiddenField)gv.FindControl("txtMatID");

        if (txtRate.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please insert the rate before dispatch.');", true);
        }
        else
        {
            DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set DispatchBy='" + lblUser.Text + "',DispatchDate=GETDATE(),Rate='" + txtRate.Text + "', Remark='" + txtRemark.Text + "',DispatchStatus=1 where EstId='" + txtEstID.Value + "' and MatId='" + txtMatID.Value + "' and UnitId='" + txtUnitID.Value + "'");
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material Dispatch Successfully.');", true);
        }
    }
    protected void gvMaterailDetailForPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int UserTypeID = Convert.ToInt16(Session["UserTypeID"].ToString());
        try
        {
            if (e.CommandName == "DispatchDate")
            {
                decimal Qty;

                GridViewRow gvRow = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                Button DisDate = (Button)e.CommandSource;
                string Sno = DisDate.CommandArgument;

                Label lbl = (Label)gvRow.FindControl("txtDispatchDate");
                lbl.Text = System.DateTime.Now.ToString();
                // TextBox rmk = (TextBox)gvRow.FindControl("txtRemark");
                HiddenField txtMatID = (HiddenField)gvRow.FindControl("txtMatID");
                HiddenField hdnMatTypeID = (HiddenField)gvRow.FindControl("hdnMatTypeID");
                TextBox txtRate = (TextBox)gvRow.FindControl("txtRate");
                TextBox txtPurchaseQty = (TextBox)gvRow.FindControl("txtPurchaseQty");
                HiddenField hdnPurchaseQty = (HiddenField)gvRow.FindControl("hdnPurchaseQty");
                HiddenField hdnRate = (HiddenField)gvRow.FindControl("hdnRate");
                CheckBox chkDirectPurchase = (CheckBox)gvRow.FindControl("chkDirectPurchase");
                HiddenField hdnMRP = (HiddenField)gvRow.FindControl("hdnMRP");
                HiddenField hdnVat = (HiddenField)gvRow.FindControl("hdnVat");
                HiddenField hdnDiscount = (HiddenField)gvRow.FindControl("hdnDiscount");
                if (UserTypeID == (int)(TypeEnum.UserType.PURCHASEEMPLOYEE))
                {
                    chkDirectPurchase.Visible = true;
                }
                else
                {
                    chkDirectPurchase.Visible = false;
                }


                lbl.Attributes.Remove("style");
                lbl.Attributes.Add("style", "display:blockp;");
                DisDate.Attributes.Add("style", "display:none;");
                Qty = Convert.ToDecimal(gvRow.Cells[3].Text);  // Est Required Quantity 
                var ExtraQty = (Convert.ToDecimal(Qty) * 5 / 100) + Convert.ToDecimal(Qty);
                var LessExtraQty = Convert.ToDecimal(Qty) - (Convert.ToDecimal(Qty) * 5 / 100);
                if (Convert.ToDecimal(txtRate.Text) > Convert.ToDecimal(hdnRate.Value) || Convert.ToDecimal(txtRate.Text) < Convert.ToDecimal(hdnRate.Value))
                {
                    var PurchasedQty = Convert.ToDecimal(txtPurchaseQty.Text) + Convert.ToDecimal(hdnPurchaseQty.Value);
                    var estCost = Convert.ToDecimal(txtRate.Text) * PurchasedQty;
                    var ExtraRate = (Convert.ToDecimal(hdnRate.Value) * 10 / 100) + Convert.ToDecimal(hdnRate.Value);
                    if (Convert.ToInt16(hdnMatTypeID.Value) == (int)TypeEnum.MatTypeID.RATION) // Ration Create Enum
                    {
                        if (PurchasedQty > ExtraQty)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Purchase Qty can not greater than 5% extra.');", true);
                        }
                        else
                        {
                            if ((PurchasedQty >= Qty && PurchasedQty <= ExtraQty) || (PurchasedQty <= Qty && PurchasedQty >= LessExtraQty))
                            {
                                DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set rate=" + txtRate.Text + ",Amount=" + estCost + ",DispatchDate=GETDATE(),remarkByPurchase='" + string.Empty + "',DispatchStatus='1',DispatchOn=getdate(),DispatchBy='" + lblUser.Text + "',PurchaseQty ='" + PurchasedQty + "',DirectPurchase ='" + chkDirectPurchase.Checked + "',VendorID ='" + hdnVandorID.Value + "',MRP ='" + hdnMRP.Value + "',Discount ='" + hdnDiscount.Value + "',Vat ='" + hdnVat.Value + "'  where Sno='" + Sno + "'");
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material has been dispatch.')", true);
                            }
                            else
                            {
                                DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set rate=" + txtRate.Text + ",Amount=" + estCost + ",DispatchDate=GETDATE(),remarkByPurchase='" + string.Empty + "',DispatchStatus='0',DispatchOn=getdate(),DispatchBy='" + lblUser.Text + "',PurchaseQty = '" + PurchasedQty + "',DirectPurchase ='" + chkDirectPurchase.Checked + "',VendorID ='" + hdnVandorID.Value + "',MRP ='" + hdnMRP.Value + "',Discount ='" + hdnDiscount.Value + "',Vat ='" + hdnVat.Value + "'  where Sno='" + Sno + "'");
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Purchase quantity has been updated.')", true);
                            }
                        }
                        DAL.DalAccessUtility.ExecuteNonQuery("update Material set MatCost=" + txtRate.Text + " where MatID='" + txtMatID.Value + "'");
                    }
                    else if (Convert.ToInt16(hdnMatTypeID.Value) == (int)TypeEnum.MatTypeID.VEGETABLESANDFRUITS)
                    {
                        DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set rate=" + txtRate.Text + ",Amount=" + estCost + ",DispatchDate=GETDATE(),remarkByPurchase='" + string.Empty + "',DispatchStatus='1',DispatchOn=getdate(),DispatchBy='" + lblUser.Text + "',PurchaseQty ='" + PurchasedQty + "',DirectPurchase ='" + chkDirectPurchase.Checked + "',VendorID ='" + hdnVandorID.Value + "',MRP ='" + hdnMRP.Value + "',Discount ='" + hdnDiscount.Value + "',Vat ='" + hdnVat.Value + "' where Sno='" + Sno + "'");
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material has been dispatch.')", true);
                        DAL.DalAccessUtility.ExecuteNonQuery("update Material set MatCost=" + txtRate.Text + " where MatID='" + txtMatID.Value + "'");
                    }
                    else if (Convert.ToInt16(txtMatID.Value) == (int)TypeEnum.MatID.ANGLEPATTI460MM || Convert.ToInt16(txtMatID.Value) == (int)TypeEnum.MatID.SARIA)
                    {
                        if (Convert.ToDecimal(txtRate.Text) <= ExtraRate)
                        {
                            DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set rate=" + txtRate.Text + ",Amount=" + estCost + ",DispatchDate=GETDATE(),remarkByPurchase='" + string.Empty + "',DispatchStatus='1',DispatchOn=getdate(),DispatchBy='" + lblUser.Text + "',PurchaseQty ='" + PurchasedQty + "',DirectPurchase ='" + chkDirectPurchase.Checked + "',VendorID ='" + hdnVandorID.Value + "',MRP ='" + hdnMRP.Value + "',Discount ='" + hdnDiscount.Value + "',Vat ='" + hdnVat.Value + "' where Sno='" + Sno + "'");
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material has been dispatch.')", true);
                            DAL.DalAccessUtility.ExecuteNonQuery("update Material set MatCost=" + txtRate.Text + " where MatID='" + txtMatID.Value + "'");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup(" + hdnMatTypeID.Value + "," + txtMatID.Value + ");", true);
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup(" + hdnMatTypeID.Value + "," + txtMatID.Value + ");", true);
                    }
                }

                else
                {
                    var PurchaseQty = Convert.ToDecimal(txtPurchaseQty.Text) + Convert.ToDecimal(hdnPurchaseQty.Value);   // Purchasing Quantity 
                    var TotalSteelWoodTileQuantity = (Convert.ToDecimal(Qty) * 10 / 100) + Convert.ToDecimal(Qty);  // 10% of Est Required Quantity(Steel,Wood,Tiles)
                    var TotalLessSteelWoodTileQuantit = Convert.ToDecimal(Qty) - (Convert.ToDecimal(Qty) * 10 / 100);

                    if (UserTypeID == (int)(TypeEnum.UserType.PURCHASEEMPLOYEE)) // For Mohali Purchaser
                    {
                        DAL.DalAccessUtility.ExecuteNonQuery("update Material set MatCost=" + txtRate.Text + " where MatID='" + txtMatID.Value + "'");
                    }
                    else
                    {
                        DAL.DalAccessUtility.ExecuteNonQuery("update Material set LocalRate=" + txtRate.Text + " where MatID='" + txtMatID.Value + "'");
                    }

                    if (Convert.ToInt16(hdnMatTypeID.Value) == (int)TypeEnum.MatTypeID.VEGETABLESANDFRUITS) // vegitable
                    {
                        if (UserTypeID == (int)(TypeEnum.UserType.PURCHASEEMPLOYEE))
                        {
                            var estimateCost = Convert.ToDecimal(txtRate.Text) * PurchaseQty;

                            DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set rate=" + txtRate.Text + ",Amount=" + estimateCost + ",DispatchDate=GETDATE(),remarkByPurchase='" + string.Empty + "',DispatchStatus='1',DispatchOn=getdate(),DispatchBy='" + lblUser.Text + "',PurchaseQty ='" + PurchaseQty + "',DirectPurchase ='" + chkDirectPurchase.Checked + "',VendorID ='" + hdnVandorID.Value + "',MRP ='" + hdnMRP.Value + "',Discount ='" + hdnDiscount.Value + "',Vat ='" + hdnVat.Value + "' where Sno='" + Sno + "'");
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material has been dispatch.')", true);
                        }
                    }
                    else if (Convert.ToInt16(hdnMatTypeID.Value) == (int)TypeEnum.MatTypeID.TILES || Convert.ToInt16(hdnMatTypeID.Value) == (int)TypeEnum.MatTypeID.WOODMATERIAL || Convert.ToInt16(hdnMatTypeID.Value) == (int)TypeEnum.MatTypeID.BHUILDINGMATERIAL) // Tiles,Wood,Steel
                    {
                        if (PurchaseQty > TotalSteelWoodTileQuantity)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Purchase Qty can not greater than 10% extra.');", true);
                        }
                        else
                        {
                            var estimateCost = Convert.ToDecimal(txtRate.Text) * PurchaseQty;
                            if ((PurchaseQty >= Qty && PurchaseQty <= TotalSteelWoodTileQuantity) || (PurchaseQty <= Qty && PurchaseQty >= TotalLessSteelWoodTileQuantit))
                            {
                                DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set rate=" + txtRate.Text + ", Amount=" + estimateCost + ", DispatchDate=GETDATE(),remarkByPurchase='" + string.Empty + "',DispatchStatus='1',DispatchOn=getdate(),DispatchBy='" + lblUser.Text + "',PurchaseQty ='" + PurchaseQty + "',DirectPurchase ='" + chkDirectPurchase.Checked + "',VendorID ='" + hdnVandorID.Value + "',MRP ='" + hdnMRP.Value + "',Discount ='" + hdnDiscount.Value + "',Vat ='" + hdnVat.Value + "'  where Sno='" + Sno + "'");
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material has been dispatch.')", true);
                            }
                            else
                            {
                                DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set rate=" + txtRate.Text + ",Amount=" + estimateCost + ",DispatchDate=GETDATE(),remarkByPurchase='" + string.Empty + "',DispatchStatus='0',DispatchOn=getdate(),DispatchBy='" + lblUser.Text + "',PurchaseQty = '" + PurchaseQty + "',DirectPurchase ='" + chkDirectPurchase.Checked + "',VendorID ='" + hdnVandorID.Value + "',MRP ='" + hdnMRP.Value + "',Discount ='" + hdnDiscount.Value + "',Vat ='" + hdnVat.Value + "'  where Sno='" + Sno + "'");
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Purchase quantity has been updated.')", true);
                            }
                        }
                    }
                    else
                    {
                        var estimateCost = Convert.ToDecimal(txtRate.Text) * PurchaseQty;
                        if (PurchaseQty > ExtraQty)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Purchase Qty can not greater than 5% extra.');", true);
                        }
                        else
                        {
                            if ((PurchaseQty >= Qty && PurchaseQty <= ExtraQty) || (PurchaseQty <= Qty && PurchaseQty >= LessExtraQty))
                            {
                                DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set rate=" + txtRate.Text + ",Amount=" + estimateCost + ",DispatchDate=GETDATE(),remarkByPurchase='" + string.Empty + "',DispatchStatus='1',DispatchOn=getdate(),DispatchBy='" + lblUser.Text + "',PurchaseQty ='" + PurchaseQty + "',DirectPurchase ='" + chkDirectPurchase.Checked + "',VendorID ='" + hdnVandorID.Value + "',MRP ='" + hdnMRP.Value + "',Discount ='" + hdnDiscount.Value + "',Vat ='" + hdnVat.Value + "'  where Sno='" + Sno + "'");
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material has been dispatch.')", true);
                            }
                            else
                            {
                                DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set rate=" + txtRate.Text + ",Amount=" + estimateCost + ",DispatchDate=GETDATE(),remarkByPurchase='" + string.Empty + "',DispatchStatus='0',DispatchOn=getdate(),DispatchBy='" + lblUser.Text + "',PurchaseQty = '" + PurchaseQty + "',DirectPurchase ='" + chkDirectPurchase.Checked + "',VendorID ='" + hdnVandorID.Value + "',MRP ='" + hdnMRP.Value + "',Discount ='" + hdnDiscount.Value + "',Vat ='" + hdnVat.Value + "'  where Sno='" + Sno + "'");
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Purchase quantity has been updated.')", true);
                            }
                        }
                    }
                }
                //DataSet dsTatDate = DAL.DalAccessUtility.GetDataInDataSet("select DispatchDate,EstId from EstimateAndMaterialOthersRelations where Sno='" + Sno + "'");
                //lbl.Text = dsTatDate.Tables[0].Rows[0]["DispatchDate"].ToString();
                getMaterialDetails(Request.QueryString["IsLocal"].ToString(), Request.QueryString["EstId"].ToString());
            }

            if (e.CommandName == "Tatitive")
            {
                string Material, Unit;
                GridViewRow row = ((GridViewRow)((Button)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                Material = row.Cells[1].Text;
                Unit = row.Cells[2].Text;
                Button ttive = (Button)e.CommandSource;
                string Sno = ttive.CommandArgument;
                Label lbl = (Label)row.FindControl("txtDispatchDate");
                Label lblTat = (Label)row.FindControl("lblTatDate");
                TextBox tatDate = (TextBox)row.FindControl("txtTatDate");
                TextBox rmk = (TextBox)row.FindControl("txtRemark");

                DataSet dsMatid = new DataSet();
                dsMatid = DAL.DalAccessUtility.GetDataInDataSet("select MatId from Material where MatName='" + Material + "'");
                DataSet dsUnitid = new DataSet();
                dsUnitid = DAL.DalAccessUtility.GetDataInDataSet("select UnitId from Unit where UnitName='" + Unit + "'");
                if (tatDate.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please insert Tantitive Date.');", true);
                }
                else
                {
                    DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set TantiveDate='" + tatDate.Text + "' where EstId='" + lblEstId.Text + "' and MatId='" + dsMatid.Tables[0].Rows[0]["MatId"].ToString() + "' and UnitId='" + dsUnitid.Tables[0].Rows[0]["UnitId"].ToString() + "' and Sno='" + Sno + "'");
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Tantitive Date successfully submit for " + Material + ".');", true);
                }
                DataSet dsTatDate = DAL.DalAccessUtility.GetDataInDataSet("select TantiveDate,EstId,PSId from EstimateAndMaterialOthersRelations where Sno='" + Sno + "'");
                lblTat.Text = dsTatDate.Tables[0].Rows[0]["TantiveDate"].ToString();
                getMaterialDetails(dsTatDate.Tables[0].Rows[0]["PSId"].ToString(), dsTatDate.Tables[0].Rows[0]["EstId"].ToString());
            }
        }
        catch (Exception)
        {
            //  throw;
        }
    }
    protected void gvMaterailDetailForPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int UserTypeID = Convert.ToInt16(Session["UserTypeID"].ToString());
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkDirectPurchase = e.Row.FindControl("chkDirectPurchase") as CheckBox;
            if (UserTypeID == (int)(TypeEnum.UserType.PURCHASEEMPLOYEE))
            {
                chkDirectPurchase.Visible = true;
            }

            Button btnDispatch = e.Row.FindControl("btnDispatch") as Button;
            HiddenField hdnVendorName = e.Row.FindControl("hdnVendorName") as HiddenField;
            TextBox txtRate = e.Row.FindControl("txtRate") as TextBox;
            TextBox txtPurchaseQty = e.Row.FindControl("txtPurchaseQty") as TextBox;
            HiddenField hdnMatTypeID = e.Row.FindControl("hdnMatTypeID") as HiddenField;

            btnDispatch.OnClientClick = "return OnClientClick(" + hdnMatTypeID.ClientID + ");";
        }
    }
    protected void btnDispatchWorkshop_Click(object sender, EventArgs e)
    {
        decimal Qty;
        int UserID = Convert.ToInt32(Session["InchargeID"].ToString());
        GridViewRow gv = (GridViewRow)((Button)sender).NamingContainer;
        TextBox txtDispatchQty = (TextBox)gv.FindControl("txtDispatchQty");
        HiddenField hdnEstID = (HiddenField)gv.FindControl("hdnEstID");
        HiddenField hdnSno = (HiddenField)gv.FindControl("hdnSno");
        HiddenField hdnMatID = (HiddenField)gv.FindControl("hdnMatID");
        HiddenField hdnDispatchQty = (HiddenField)gv.FindControl("hdnDispatchQty");
        HiddenField hdnInStoreQty = (HiddenField)gv.FindControl("hdnInStoreQty");
        HiddenField hdnQty = (HiddenField)gv.FindControl("hdnQty");
        if (txtDispatchQty.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please insert the Qty before dispatch.');", true);
        }
        else
        {
            var DispatchQty = Convert.ToDecimal(txtDispatchQty.Text);
            var LeftStoreQty = Convert.ToDecimal(hdnInStoreQty.Value) - Convert.ToDecimal(txtDispatchQty.Text);
            var TotalDispatchQty = Convert.ToDecimal(txtDispatchQty.Text) + Convert.ToDecimal(hdnDispatchQty.Value);


            if (hdnInStoreQty.Value == "0" || hdnInStoreQty.Value == "0.00")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Updtae the InStoreQty before dispatch.');", true);
            }
            else if ((Convert.ToDecimal(DispatchQty) > Convert.ToDecimal(hdnInStoreQty.Value)))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Dispatch Qty can not greater than InStore Qty .');", true);
            }
            else if ((Convert.ToDecimal(TotalDispatchQty) > Convert.ToDecimal(hdnQty.Value)))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Dispatch Qty can not greater than Required Qty.');", true);
            }
            else
            {
                WorkshopDispatchMaterial workshopDispatchMaterial = new WorkshopDispatchMaterial();
                workshopDispatchMaterial.DispatchQty = Convert.ToInt32(txtDispatchQty.Text);
                workshopDispatchMaterial.EstID = Convert.ToInt32(hdnEstID.Value);
                workshopDispatchMaterial.EMRID = Convert.ToInt32(hdnSno.Value);
                workshopDispatchMaterial.DispatchOn = DateTime.Now;
                workshopDispatchMaterial.DispatchBy = UserID;
                WorkshopRepository repo = new WorkshopRepository(new AkalAcademy.DataContext());
                if (workshopDispatchMaterial.ID == 0)
                {
                    repo.AddDispatchWorkshopMaterial(workshopDispatchMaterial);
                }
                if ((Convert.ToDecimal(TotalDispatchQty) == Convert.ToDecimal(hdnQty.Value)))
                {
                    DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set DispatchDate=GETDATE(),remarkByPurchase='" + string.Empty + "',DispatchStatus='1',DispatchOn=getdate(),DispatchBy='" + lblUser.Text + "' where Sno='" + hdnSno.Value + "'");
                }
                else
                {
                    DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set DispatchDate=GETDATE(),remarkByPurchase='" + string.Empty + "',DispatchStatus='0',DispatchOn=getdate(),DispatchBy='" + lblUser.Text + "' where Sno='" + hdnSno.Value + "'");

                }

                DAL.DalAccessUtility.ExecuteNonQuery("Update WorkshopStoreMaterial set InStoreQty='" + LeftStoreQty + "' where MatID='" + hdnMatID.Value + "'");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material Dispatch Successfully.')", true);
                getMaterialDetails(Request.QueryString["IsLocal"].ToString(), Request.QueryString["EstId"].ToString());
            }
        }
    }

    protected void gvWorkShopMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int groupName=0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //RequiredFieldValidator reqtxtDispatchQty = e.Row.FindControl("reqtxtDispatchQty") as RequiredFieldValidator;
            //reqtxtDispatchQty.ValidationGroup = groupName.ToString();
            //Button objAdd = e.Row.FindControl("btnDispatchWorkshop") as Button;
            //objAdd.ValidationGroup = groupName.ToString();
            //TextBox objtxtAddress = e.Row.FindControl("txtDispatchQty") as TextBox;
            //objtxtAddress.ValidationGroup = groupName.ToString();
            //groupName++;
        }
    }
}