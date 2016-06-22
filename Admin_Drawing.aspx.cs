using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

public partial class Admin_Drawing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (Session["EmailId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                hdnInchargeID.Value = Session["InchargeID"].ToString();
                lblUser.Text = Session["EmailId"].ToString();
            }
           
        }
    }
     
    //private void SendNotificationToEmp()
    //{
    //    DataSet dsdrawingID = DAL.DalAccessUtility.GetDataInDataSet("select top 1 * from Drawing order by DwgId desc");

    //    string msg = "New Drawing has been uploaded by " + lblUser.Text;

    //    try
    //    {
    //        DAL.DalAccessUtility.ExecuteNonQuery("exec USP_SendDrawingMsgToUserType '" + lblUser.Text + "','2','" + msg + "','" + dsdrawingID.Tables[0].Rows[0]["AcaId"].ToString() + "'");
    //    }
    //    catch (Exception ex)
    //    { }
    //}

    //private void SendSMSToConstructionUsers(int AcaID, string drawingName)
    //{
    //    const int USERTYPE = 2;
    //    string smsTo = string.Empty;
    //    InchargeController conrtoller = new InchargeController();
    //    List<string> incharges = conrtoller.GetUsersByUserTypeAndAcademic(USERTYPE, AcaID);
    //    foreach (string inchargeNumber in incharges)
    //    {
    //        smsTo += inchargeNumber + ",";
    //    }

    //    string adminNumber = System.Configuration.ConfigurationManager.AppSettings["AdminToSendDrawingSMS"].ToString();
    //    if (btnSave.Visible==true)
    //    {

    //        {
    //            smsTo += adminNumber + ",";
    //        }
    //        smsTo = smsTo.Substring(0, smsTo.Length - 1);
    //        Utility.SendSMS(smsTo, "Drawing of " + ddlAcademy.SelectedItem.Text + " has been uploaded to www.Akalsewa.org.");
    //    }
    //    else if(btnEdit.Visible == true)
    //    {
    //        {
    //            smsTo += adminNumber + ",";
    //        }
    //        smsTo = smsTo.Substring(0, smsTo.Length - 1);
    //        Utility.SendSMS(smsTo, "Drawing of " + ddlAcademy.SelectedItem.Text + " has been Edited and Uploaded to www.Akalsewa.org.");
    //    }
    //}

  

}