using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Security_SendMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindZone();
            divsecurityemp.Visible = false;

        }
    }
    protected void BindAcademy()
    {
        DataSet AcaList = new DataSet();
        AcaList = DAL.DalAccessUtility.GetDataInDataSet("select AcaId,AcaName from Academy where ZoneId='" + ddlZone.SelectedValue + "'");
        ddlAcademy.DataSource = AcaList;
        ddlAcademy.DataValueField = "AcaId";
        ddlAcademy.DataTextField = "AcaName";
        ddlAcademy.DataBind();
        ddlAcademy.Items.Insert(0, new ListItem("--Select Academy--", "0"));
        ddlAcademy.SelectedIndex = 0;
    }
    protected void BindZone()
    {
        DataSet ZoneList = new DataSet();
        ZoneList = DAL.DalAccessUtility.GetDataInDataSet("select * from Zone where Active=1");
        ddlZone.DataSource = ZoneList;
        ddlZone.DataValueField = "ZoneId";
        ddlZone.DataTextField = "ZoneName";
        ddlZone.DataBind();
        ddlZone.Items.Insert(0, "Select Zone");
        ddlZone.SelectedIndex = 0;
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAcademy();
    }
    protected void BindSecurityEmployee()
    {
        DataSet EmpList = new DataSet();
        EmpList = DAL.DalAccessUtility.GetDataInDataSet("Select ID,MobileNo from SecurityEmployeeInfo where AcaID='" + ddlAcademy.SelectedValue + "'");
        for (int i = 0; i < EmpList.Tables[0].Rows.Count; i++)
        {
            txtRecipientNumber.Text += EmpList.Tables[0].Rows[i]["MobileNo"].ToString() + ",";
        }
        txtRecipientNumber.Text = txtRecipientNumber.Text.Substring(0, txtRecipientNumber.Text.Length - 1);
    }
    protected void ddlAcademy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSecurityEmployee();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string mobileNumber = txtRecipientNumber.Text;
        string senderId = "AKALCO";
        string userid = "AKALSEVA";
        string password = "c@120";
        string message = HttpUtility.UrlEncode(txtMessage.Text);

        //Prepare you post parameters
        StringBuilder sbPostData = new StringBuilder();
        sbPostData.AppendFormat("username={0}", userid);
        sbPostData.AppendFormat("&password={0}", password);
        sbPostData.AppendFormat("&to={0}", mobileNumber);
        sbPostData.AppendFormat("&from={0}", senderId);
        sbPostData.AppendFormat("&message={0}", message);

        try
        {
            string sendSMSUri = "http://www.sunshinesms.org/quicksms/api.php";
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(sbPostData.ToString());
            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/x-www-form-urlencoded";
            httpWReq.ContentLength = data.Length;
            using (Stream stream = httpWReq.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responseString = reader.ReadToEnd();
            reader.Close();
            response.Close();
            ClearTextBox();
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "<script>alert('Message Sent Successfully');</script>", false);
        }
        catch (SystemException ex)
        {
            ex.Message.ToString();
        }
    }

    private void ClearTextBox()
    {
        txtRecipientNumber.Visible = false;
        ddlZone.ClearSelection();
        ddlAcademy.ClearSelection();
        txtMessage.Text = "";
    }
}