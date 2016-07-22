﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UserControls_BodyViewEstimateMaterial : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
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
            else
            {
                Response.Redirect("Purchase_MaterialToBeDispatch.aspx");
                return;
            }
        }
        gvMaterailDetailForPurchase.DataSource = dsAcaDetails;
        gvMaterailDetailForPurchase.DataBind();

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


                lbl.Attributes.Remove("style");
                lbl.Attributes.Add("style", "display:blockp;");
                DisDate.Attributes.Add("style", "display:none;");

                if (txtRate.Text == string.Empty)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter rate before dispatch.');", true);
                }
                else
                {

                    Qty = Convert.ToDecimal(gvRow.Cells[3].Text);  // Est Required Quantity 

                    var PurchaseQty = Convert.ToDecimal(txtPurchaseQty.Text) + Convert.ToDecimal(hdnPurchaseQty.Value);   // Purchasing Quantity 

                    var ExtraQty = (Convert.ToDecimal(Qty) * 5 / 100) + Convert.ToDecimal(Qty);  // 5% of Est Required Quantity
                    var TotalVegiAcceptableQuantity = (Convert.ToDecimal(Qty) * 30 / 100) + Convert.ToDecimal(Qty);  // 30% of Est Required Quantity(Vegetable)
                    var TotalLessVegiAcceptableQuantity = Convert.ToDecimal(Qty) - (Convert.ToDecimal(Qty) * 30 / 100);
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

                    if (Convert.ToInt16(hdnMatTypeID.Value) == 53) // vegitable
                    {
                        if (UserTypeID == (int)(TypeEnum.UserType.PURCHASEEMPLOYEE))
                        {
                            if (PurchaseQty > TotalVegiAcceptableQuantity)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Purchase Qty can not greater than 30% extra.');", true);
                            }
                            else
                            {
                                if ((PurchaseQty >= Qty && PurchaseQty <= TotalVegiAcceptableQuantity) || (PurchaseQty <= Qty && PurchaseQty >= TotalLessVegiAcceptableQuantity))
                                {
                                    DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set rate=" + txtRate.Text + ",DispatchDate=GETDATE(),remarkByPurchase='" + string.Empty + "',DispatchStatus='1',DispatchOn=getdate(),DispatchBy='" + lblUser.Text + "',PurchaseQty ='" + PurchaseQty + "' where Sno='" + Sno + "'");
                                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material has been dispatch.')", true);
                                }
                                else
                                {
                                    DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set rate=" + txtRate.Text + ",DispatchDate=GETDATE(),remarkByPurchase='" + string.Empty + "',DispatchStatus='0',DispatchOn=getdate(),DispatchBy='" + lblUser.Text + "',PurchaseQty = '" + PurchaseQty + "' where Sno='" + Sno + "'");
                                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Purchase quantity has been updated.')", true);
                                }
                            }
                        }
                    }
                    else if (Convert.ToInt16(hdnMatTypeID.Value) == 29 || Convert.ToInt16(hdnMatTypeID.Value) == 65 || Convert.ToInt16(hdnMatTypeID.Value) == 24) // Tiles,Wood,Steel
                    {
                        if (PurchaseQty > TotalSteelWoodTileQuantity)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Purchase Qty can not greater than 10% extra.');", true);
                        }
                        else
                        {
                            if ((PurchaseQty >= Qty && PurchaseQty <= TotalSteelWoodTileQuantity) || (PurchaseQty <= Qty && PurchaseQty >= TotalLessSteelWoodTileQuantit))
                            {
                                DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set rate=" + txtRate.Text + ",DispatchDate=GETDATE(),remarkByPurchase='" + string.Empty + "',DispatchStatus='1',DispatchOn=getdate(),DispatchBy='" + lblUser.Text + "',PurchaseQty ='" + PurchaseQty + "' where Sno='" + Sno + "'");
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material has been dispatch.')", true);
                            }
                            else
                            {
                                DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set rate=" + txtRate.Text + ",DispatchDate=GETDATE(),remarkByPurchase='" + string.Empty + "',DispatchStatus='0',DispatchOn=getdate(),DispatchBy='" + lblUser.Text + "',PurchaseQty = '" + PurchaseQty + "' where Sno='" + Sno + "'");
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Purchase quantity has been updated.')", true);
                            }
                        }
                    }
                    else
                    {
                        if (PurchaseQty > ExtraQty)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Purchase Qty can not greater than 5% extra.');", true);
                        }
                        else
                        {
                            if (PurchaseQty == Qty || (PurchaseQty > Qty && PurchaseQty <= ExtraQty))
                            {
                                DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set rate=" + txtRate.Text + ",DispatchDate=GETDATE(),remarkByPurchase='" + string.Empty + "',DispatchStatus='1',DispatchOn=getdate(),DispatchBy='" + lblUser.Text + "',PurchaseQty ='" + PurchaseQty + "' where Sno='" + Sno + "'");
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Material has been dispatch.')", true);
                            }
                            else
                            {
                                DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set rate=" + txtRate.Text + ",DispatchDate=GETDATE(),remarkByPurchase='" + string.Empty + "',DispatchStatus='0',DispatchOn=getdate(),DispatchBy='" + lblUser.Text + "',PurchaseQty = '" + PurchaseQty + "' where Sno='" + Sno + "'");
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
}