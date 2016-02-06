using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Example_EditEstimateaspx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
            //if (Request.QueryString["EstId"] != null)
            //{
            //    GetEstimateDetails(Request.QueryString["EstId"].ToString());
            //    getEstimateWithParticularDetails(Request.QueryString["EstId"].ToString());
            //}
            
                BindGrid();
           
        }
    }

    protected void BindGrid()
    {
        DataSet ds = new DataSet();
        ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_EDITEstimate '" + 32 + "'");
        if (ds.Tables[0].Rows.Count > 0)
        {
            gvDetails.DataSource = ds;
            gvDetails.DataBind();
        }
        else
        {
            Response.Write("no data");
        }
    }
    protected void gvDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvDetails.EditIndex = e.NewEditIndex;
        BindGrid();
        
        DropDownList ddlMateType = ((DropDownList)gvDetails.Rows[e.NewEditIndex].Cells[3].FindControl("ddlMatTId"));
        DataSet dsMatType = new DataSet();
        dsMatType = DAL.DalAccessUtility.GetDataInDataSet("select MatTypeId,MatTypeName from MaterialType where Active=1");
        ddlMateType.DataSource = dsMatType;
        ddlMateType.DataValueField = "MatTypeId";
        ddlMateType.DataTextField = "MatTypeName";
        ddlMateType.DataBind();
        ddlMateType.Items.Insert(0, "Material Type");
        ddlMateType.SelectedIndex = 0;
        //Label lbMatT = gvDetails.Rows[e.NewEditIndex].FindControl("lblMat") as Label;
        //string matT = gvDetails.Rows[e.NewEditIndex].Cells[3].Text;
        //DataSet dsMatId = DAL.DalAccessUtility.GetDataInDataSet("select MatTypeId from MaterialType where MatTypeName='"+ matT +"'");
        //ddlMateType.SelectedIndex = ddlMateType.Items.IndexOf(ddlMateType.Items.FindByValue(dsMatType.Tables[0].Rows[0]["MatTypeName"].ToString().Trim()));
       
        
        
        DropDownList ddlSourceType = ((DropDownList)gvDetails.Rows[e.NewEditIndex].Cells[8].FindControl("ddlPs"));
        DataSet dsSourcType = new DataSet();
        dsSourcType = DAL.DalAccessUtility.GetDataInDataSet("select PSId,PSName from PurchaseSource where Active=1");
        ddlSourceType.DataSource = dsSourcType;
        ddlSourceType.DataValueField = "PSId";
        ddlSourceType.DataTextField = "PSName";
        ddlSourceType.DataBind();
        ddlSourceType.Items.Insert(0, "Source Type");
        ddlSourceType.SelectedIndex = 0;
        
    }

    protected void ddlMatTId_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList ddlMateType = (DropDownList)row.FindControl("ddlMatTId");
        DropDownList ddlMaterail = (DropDownList)row.FindControl("ddlMatId");
        DataSet dsMat = new DataSet();
        dsMat = DAL.DalAccessUtility.GetDataInDataSet("select MatId,MatName from Material where Active=1 and MatTypeId='" + ddlMateType.SelectedValue + "'");
        ddlMaterail.DataSource = dsMat;
        ddlMaterail.DataValueField = "MatId";
        ddlMaterail.DataTextField = "MatName";
        ddlMaterail.DataBind();
        ddlMaterail.Items.Insert(0, "Material");
        ddlMaterail.SelectedIndex = 0;
    }
    protected void gvDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvDetails.EditIndex = -1;
        BindGrid();
    }
    protected void gvDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int Sno = Convert.ToInt32(gvDetails.DataKeys[e.RowIndex].Value.ToString());
        DropDownList dlMatT = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlMatTId");
        DropDownList dlMat = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlMatId");

        TextBox txQty = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtQty");
        TextBox txRate = (TextBox)gvDetails.Rows[e.RowIndex].FindControl("txtRate");
        DropDownList dlSt = (DropDownList)gvDetails.Rows[e.RowIndex].FindControl("ddlPs");
        Label lbUnit = (Label)gvDetails.Rows[e.RowIndex].FindControl("lblUnitEdit");
        DataSet dsUnitId = DAL.DalAccessUtility.GetDataInDataSet("select UnitId from Unit where UnitName='"+ lbUnit.Text +"'");
        int uId = Convert.ToInt32(dsUnitId.Tables[0].Rows[0]["UnitId"].ToString());
        Label lbAmt = (Label)gvDetails.Rows[e.RowIndex].FindControl("txtAmtEdit");
        DAL.DalAccessUtility.ExecuteNonQuery("update EstimateAndMaterialOthersRelations set MatTypeId='" + dlMatT.SelectedValue +"',MatId='"+ dlMat.SelectedValue +"',Qty='"+ txQty.Text +"',Rate='"+ txRate.Text +"',PSId='"+ dlSt.SelectedValue +"',UnitId='"+ uId +"',Amount='"+ lbAmt.Text +"' where Sno='"+ Sno +"'"); //,ModifyBy='',ModifyOn=''
        gvDetails.EditIndex = -1;
        BindGrid();
    }
    protected void ddlMatId_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((DropDownList)sender).Parent.Parent;
        Label UnitName = (Label)row.FindControl("lblUnitEdit");
        DropDownList ddlMaterail = (DropDownList)row.FindControl("ddlMatId");
        DataSet dsUName = DAL.DalAccessUtility.GetDataInDataSet("SELECT Unit.UnitName FROM Material INNER JOIN Unit ON Material.UnitId = Unit.UnitId where Material.MatId='"+ ddlMaterail.SelectedValue + "'");
        UnitName.Text = dsUName.Tables[0].Rows[0]["UnitName"].ToString();
    }
    protected void txtRate_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox txtQt = (TextBox)row.FindControl("txtQty");
        TextBox txtRa = (TextBox)row.FindControl("txtRate");
        Label lblAm = (Label)row.FindControl("txtAmtEdit");
        decimal qt = Convert.ToDecimal(txtQt.Text);
        decimal ra = Convert.ToDecimal(txtRa.Text);
        decimal am = qt * ra;
        lblAm.Text = am.ToString();
    }
}