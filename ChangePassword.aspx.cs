using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet dsExist = DAL.DalAccessUtility.GetDataInDataSet("select distinct EmailId from Login where EmailId='" + txtLoginId.Text + "'");
        DataSet dsOldPwd = DAL.DalAccessUtility.GetDataInDataSet("select UserPwd from Incharge where LoginId='" + txtLoginId.Text + "'");
        if (dsExist.Tables[0].Rows.Count > 0)
        {
            
            if (txtLoginId.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter Login ID.');", true);
            }
            else if (txtOldPwd.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter New Password.');", true);
            }
            else if (dsOldPwd.Tables[0].Rows[0]["UserPwd"].ToString()!=txtOldPwd.Text)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter correct old password.');", true);
            }
            else if (txtNewPwd.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please enter New Password.');", true);
            }
            
            else if (txtRePwd.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Re-Enter Password.');", true);
            }
            else
            {
                DAL.DalAccessUtility.ExecuteNonQuery("exec USP_ChangePassword '" + txtLoginId.Text + "', '" + txtRePwd.Text + "'");
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Thanks ! Password Change successfully.');", true);
                Response.Redirect("Default.aspx");
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Login ID does not exist, Please enter correct Login ID.');", true);
        }
    }
}