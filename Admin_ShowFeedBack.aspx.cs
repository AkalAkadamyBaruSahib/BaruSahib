using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Admin_ShowFeedBack : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetFeedBackDetails();
        }
    }
    protected void GetFeedBackDetails()
    {
        DataSet dsCouDetails = new DataSet();
        dsCouDetails = DAL.DalAccessUtility.GetDataInDataSet("select UserName,UserEmail,FBMsg,IsAkalEmp,MobNo from Feedback where Active=1 order by CreatedOn desc");
        divCouDetails.InnerHtml = string.Empty;
        string ZoneInfo = string.Empty;
        ZoneInfo += "<table class='table table-striped table-bordered bootstrap-datatable datatable'>";
        ZoneInfo += "<thead>";
        ZoneInfo += "<tr>";
        ZoneInfo += "<th width='40%'>Feedback Message</th>";
        ZoneInfo += "<th width='60%'><table><tr><td colspan='4' width='140%' align='center'>Details</td></tr><tr><td width='30%'>Name</td><td width='35%'>Email</td><td width='30%'>Mobile</td><td width='45%'>Akal Employee</td></tr></table></th>";
        ZoneInfo += "</tr>";
        ZoneInfo += "</thead>";
        ZoneInfo += "<tbody>";
        for (int i = 0; i < dsCouDetails.Tables[0].Rows.Count; i++)
        {
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='40%'>" + dsCouDetails.Tables[0].Rows[i]["FBMsg"].ToString() + "</td>";
            ZoneInfo += "<td width='60%'>";
            ZoneInfo += "<table>";
            ZoneInfo += "<tr>";
            ZoneInfo += "<td width='30%'>" + dsCouDetails.Tables[0].Rows[i]["UserName"].ToString() + "</td><td width='35%'>" + dsCouDetails.Tables[0].Rows[i]["UserEmail"].ToString() + "</td><td width='35%'>" + dsCouDetails.Tables[0].Rows[i]["MobNo"].ToString() + "</td>";
            if (dsCouDetails.Tables[0].Rows[i]["IsAkalEmp"].ToString() == "1")
            {
                ZoneInfo += "<td width='30%'>Yes</td>";
            }
            else
            {
                ZoneInfo += "<td width='30%'>No</td>";
            }
            ZoneInfo += "</tr>";
            ZoneInfo += "</table>";
            ZoneInfo += "</td>";
            ZoneInfo += "</tr>";
        }
        ZoneInfo += "</tbody>";
        ZoneInfo += "</table>";

        divCouDetails.InnerHtml = ZoneInfo.ToString();
    }
}