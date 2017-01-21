using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RateApproved : System.Web.UI.Page
{
    private int InchargeID { get; set; }
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
                InchargeID = Convert.ToInt32(Session["InchargeID"].ToString());
            }
            BindNonApprovedRateMaterial();
        }
    }

    protected void BindNonApprovedRateMaterial()
    {
        DataSet dsMat = new DataSet();
        dsMat = DAL.DalAccessUtility.GetDataInDataSet("Select M.MatId,MT.MatTypeName,M.MatName,M.MatCost,U.UnitName,MN.Rate,Inc.InName from Material M INNER JOIN MaterialNonApprovedRate MN on M.MatId = MN.MatID INNER JOIN MaterialType MT on M.MatTypeId = MT.MatTypeId INNER JOIN Unit U On M.UnitId = U.UnitId INNER JOIN Incharge Inc On MN.CreatedBy = Inc.InchargeId  where M.IsRateApproved = 0");
        grvNonApprovedRateDetails.DataSource = dsMat;
        grvNonApprovedRateDetails.DataBind();
    }
    protected void btn_Approved_Click(object sender, EventArgs e)
    {

        MaterialRateApproved materialrateapproved = new MaterialRateApproved();
        GridViewRow gr = (GridViewRow)((DataControlFieldCell)((Button)sender).Parent).Parent;

        Button btnapproved = (Button)gr.FindControl("btn_Approved");
        Label txtRate = (Label)gr.FindControl("txtRate");
        TextBox txtNewRate = (TextBox)gr.FindControl("txtNewRate");

        string approvedid = btnapproved.CommandArgument.ToString();

        DAL.DalAccessUtility.ExecuteNonQuery("Update Material set MatCost='" + txtNewRate.Text + "',IsRateApproved = 1 where MatId = '" + approvedid + "'");

        materialrateapproved.MatID = Convert.ToInt32(approvedid);
        materialrateapproved.ApprovedBy = InchargeID;
        materialrateapproved.ApprovedOn = DateTime.Now;

        ConstructionUserRepository repo = new ConstructionUserRepository(new AkalAcademy.DataContext());
        repo.SaveApprovedMaterial(materialrateapproved);

        BindNonApprovedRateMaterial();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "<script>alert('Rate Approved Successfully');</script>", false);
    }
}