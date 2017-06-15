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
        if (Session["EmailId"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            lblUser.Text = Session["EmailId"].ToString();
            InchargeID = Convert.ToInt32(Session["InchargeID"].ToString());
        }
        if (!IsPostBack)
        {
            BindNonApprovedRateMaterial();
        }
    }

    protected void BindNonApprovedRateMaterial()
    {
        DataSet dsMat = new DataSet();
        if (InchargeID == (int)TypeEnum.PurchaseCommittee.FirstApproval)
        {
            dsMat = DAL.DalAccessUtility.GetDataInDataSet("Select M.MatId,MT.MatTypeName,M.MatName,M.MatCost,U.UnitName,Inc.InName,MN.CreatedOn,MN.MRP,MN.Discount,MN.Vat,MN.NetRate,V.VendorAddress,V.VendorName from Material M INNER JOIN MaterialNonApprovedRate MN on M.MatId = MN.MatID INNER JOIN MaterialType MT on M.MatTypeId = MT.MatTypeId INNER JOIN Unit U On M.UnitId = U.UnitId INNER JOIN Incharge Inc On MN.CreatedBy = Inc.InchargeId LEFT OUTER JOIN VendorInfo V On V.ID = MN.VendorID LEFT OUTER JOIN [dbo].[MaterialRateApproved] MR ON MR.MatID = MN.MatID WHERE MR.FirstApproval is null ");
        }
        else
        {
            dsMat = DAL.DalAccessUtility.GetDataInDataSet("Select M.MatId,MT.MatTypeName,M.MatName,M.MatCost,U.UnitName,Inc.InName,MN.CreatedOn,MN.MRP,MN.Discount,MN.Vat,MN.NetRate,V.VendorAddress,V.VendorName from Material M INNER JOIN MaterialNonApprovedRate MN on M.MatId = MN.MatID INNER JOIN MaterialType MT on M.MatTypeId = MT.MatTypeId INNER JOIN Unit U On M.UnitId = U.UnitId INNER JOIN Incharge Inc On MN.CreatedBy = Inc.InchargeId LEFT OUTER JOIN VendorInfo V On V.ID = MN.VendorID LEFT OUTER JOIN [dbo].[MaterialRateApproved] MR ON MR.MatID = MN.MatID WHERE MR.FirstApproval = '" + (int)TypeEnum.PurchaseCommittee.FirstApproval + "' and MR.SecondApproval is null");
        }
        grvNonApprovedRateDetails.DataSource = dsMat;
        grvNonApprovedRateDetails.DataBind();
    }
    protected void btn_Approved_Click(object sender, EventArgs e)
    {
        MaterialRateApproved materialrateapproved = new MaterialRateApproved();
        GridViewRow gr = (GridViewRow)((DataControlFieldCell)((Button)sender).Parent).Parent;

        Button btnapproved = (Button)gr.FindControl("btn_Approved");
        Label txtRate = (Label)gr.FindControl("txtRate");
        Label lblMRP = (Label)gr.FindControl("lblMRP");
        Label lblDiscount = (Label)gr.FindControl("lblDiscount");
        Label lblVat = (Label)gr.FindControl("lblVat");
        Label lblNewRate = (Label)gr.FindControl("lblNewRate");

        string approvedid = btnapproved.CommandArgument.ToString();
        lblMRP.Text = lblMRP.Text.Replace("%", "");
        lblDiscount.Text = lblDiscount.Text.Replace("%", "");
        lblVat.Text = lblVat.Text.Replace("%", "");

        ConstructionUserRepository repo = new ConstructionUserRepository(new AkalAcademy.DataContext());
        materialrateapproved.ApprovedOn = Utility.GetLocalDateTime(DateTime.UtcNow);

        if (InchargeID == (int)TypeEnum.PurchaseCommittee.FirstApproval)
        {
            DataTable dsMatID = DAL.DalAccessUtility.GetDataInDataSet("Select MR.ID from  MaterialNonApprovedRate MN LEFT OUTER JOIN [dbo].[MaterialRateApproved] MR ON MR.MatID = MN.MatID WHERE MR.FirstApproval  is null and MR.MatID ='" + approvedid + "'").Tables[0];
            materialrateapproved.ID = Convert.ToInt32(dsMatID.Rows[0]["ID"].ToString());
            materialrateapproved.FirstApproval = InchargeID;
            repo.UpdateApprovedMaterial(materialrateapproved, InchargeID);
        }
        else
        {
            DataTable dsMatID = DAL.DalAccessUtility.GetDataInDataSet("Select MR.ID,MN.CreatedBy,inc.LoginId from  MaterialNonApprovedRate MN LEFT OUTER JOIN [dbo].[MaterialRateApproved] MR ON MR.MatID = MN.MatID INNER JOIN Incharge Inc ON MN.CreatedBy = Inc.InchargeId WHERE MR.FirstApproval = '" + (int)TypeEnum.PurchaseCommittee.FirstApproval + "' and MR.SecondApproval is null and MR.MatID ='" + approvedid + "'").Tables[0];
            materialrateapproved.ID = Convert.ToInt32(dsMatID.Rows[0]["ID"].ToString());
            materialrateapproved.SecondApproval = InchargeID;
            repo.UpdateApprovedMaterial(materialrateapproved, InchargeID);

            DataTable dsMat = DAL.DalAccessUtility.GetDataInDataSet("Select MatName from Material where MatId = '" + approvedid + "'").Tables[0];
            DAL.DalAccessUtility.ExecuteNonQuery("Update Material set MatCost='" + lblNewRate.Text + "',MRP='" + lblMRP.Text + "',Discount='" + lblDiscount.Text + "',Vat='" + lblVat.Text + "',IsRateApproved = 1 where MatId = '" + approvedid + "'");
            SendAutoGeneratedApprovedRateEmail(dsMat.Rows[0]["MatName"].ToString(), lblNewRate.Text, dsMatID.Rows[0]["LoginId"].ToString());
            DAL.DalAccessUtility.ExecuteNonQuery("Delete from MaterialNonApprovedRate where MatId = '" + approvedid + "'");
        }

        BindNonApprovedRateMaterial();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "<script>alert('Rate Approved Successfully');</script>", false);
    }

    public void SendAutoGeneratedApprovedRateEmail(string Material, string NewRate,string loginID)
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
        MsgInfo += "New Rate for Materials Approved By Purchase Committee.Please click on <a href='http://akalsewa.org/'>Akal Sewa</a>";
        MsgInfo += "</td>";
        MsgInfo += "</tr>";
        MsgInfo += "</table>";
        MsgInfo += "<table border='1' style='width:50%' cellspacing='0' cellpadding='0'>";
        MsgInfo += "<tbody>";
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
        MsgInfo += "<b>New Rate:</b>";
        MsgInfo += "</td>";
        MsgInfo += "<td>";
        MsgInfo += "Rs. " + NewRate;
        MsgInfo += "</td>";
        MsgInfo += "</tr>";
        MsgInfo += "</tbody>";
        MsgInfo += "</table>";
        string FileName = string.Empty;
        string to = "purchase@barusahib.org";
        string cc = loginID;

        try
        {
          Utility.SendEmailWithoutAttachments(to, cc, MsgInfo, "New Rate Approved Successfully.");
        }
        catch { }
        finally
        {

        }

    }
  
    protected void btn_Rejected_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (GridViewRow)((DataControlFieldCell)((Button)sender).Parent).Parent;

        Button btnrejected = (Button)gr.FindControl("btn_Rejected");
        Label txtRate = (Label)gr.FindControl("txtRate");
        Label lblNewRate = (Label)gr.FindControl("lblNewRate");
        string rejectedid = btnrejected.CommandArgument.ToString();
        DataTable dsNonMat = DAL.DalAccessUtility.GetDataInDataSet("Select MN.CreatedBy,inc.LoginId,M.MatName FROM  MaterialNonApprovedRate MN INNER JOIN Incharge Inc ON MN.CreatedBy = Inc.InchargeId INNER JOIN Material M ON M.MatId = MN.MatID WHERE MN.MatId= '" + rejectedid + "'").Tables[0];

        SendAutoGeneratedRejectedRateEmail(dsNonMat.Rows[0]["MatName"].ToString(), lblNewRate.Text, dsNonMat.Rows[0]["LoginId"].ToString());
        DAL.DalAccessUtility.ExecuteNonQuery("Delete from MaterialNonApprovedRate where MatId = '" + rejectedid + "'");
        if (InchargeID == (int)TypeEnum.PurchaseCommittee.FirstApproval)
        {
            DAL.DalAccessUtility.ExecuteNonQuery("Delete from MaterialRateApproved where MatId = '" + rejectedid + "' and FirstApproval is null and SecondApproval is null");
        }
        else
        {
            DAL.DalAccessUtility.ExecuteNonQuery("Delete from MaterialRateApproved where MatId = '" + rejectedid + "' and SecondApproval is null");
        }
        BindNonApprovedRateMaterial();
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "<script>alert('Rate Rejected Successfully');</script>", false);
    }

    public void SendAutoGeneratedRejectedRateEmail(string Material, string RejectedRate, string loginID)
    {
        string MsgInfo = string.Empty;
        MsgInfo += "<table style='width:100%;'>";
        MsgInfo += "<tr><td style='padding:0px; text-align:left; width:50%' valign='top'>";
        MsgInfo += "<img src='http://akalsewa.org/img/logoakalnew.png' style='width:100%;' /></td>";
        MsgInfo += "<td style='text-align: right; width:40%;'><br /><br />";
        MsgInfo += "<div style='font-style:italic; text-align: right;'>";
        MsgInfo += "Baru Shahib,<br />Dist: Sirmaur<br />Himachal Pradesh-173001";
        MsgInfo += "</td></tr>";
        MsgInfo += "<tr><td colspan='2' style='height:50px'>New Rate for Materials Rejected By Purchase Committee.Please click on <a href='http://akalsewa.org/'>Akal Sewa</a></td></tr>";
        MsgInfo += "</table><table border='1' style='width:100%' cellspacing='0' cellpadding='0'><tbody><tr><td><b>Material Name:</b></td><td>"+Material+"</td></tr>";
        MsgInfo += "<tr><td><b>Rejected Rate:</b></td><td>"+RejectedRate+"</td></tr></tbody></table>";
        string FileName = string.Empty;
        string to = "purchase@barusahib.org";
        string cc = loginID;
        try
        {
          Utility.SendEmailWithoutAttachments(to, cc, MsgInfo, "Request for Approve Rate for Materials " + Material + " is Rejected.");
        }
        catch { }
        finally
        {

        }

    }
}