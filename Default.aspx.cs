using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    private static int? UserTypeID = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (txtUserName.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Login Id');", true);
        }
        else if (txtPwd.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Password');", true);
        }
        else
        {
            UsersRepository repo = new UsersRepository(new AkalAcademy.DataContext());
            Incharge inchrge = new Incharge();
            //inchrge = repo.GetLoginUserDetail(txtUserName.Text, txtPwd.Text);

            DataSet dsExit = DAL.DalAccessUtility.GetDataInDataSet("select EmailId,Pwd from Login where Active=1 AND EmailId='" + txtUserName.Text + "'");
            if ((dsExit != null && dsExit.Tables[0] != null && dsExit.Tables[0].Rows.Count > 0) && (dsExit.Tables[0].Rows[0]["EmailId"].ToString() == txtUserName.Text && dsExit.Tables[0].Rows[0]["Pwd"].ToString() == txtPwd.Text))
            {
                DataSet ds = new DataSet();
                ds = DAL.DalAccessUtility.GetDataInDataSet("exec USP_Login '" + txtUserName.Text + "','" + txtPwd.Text + "','" + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString() + "'");
                if (ds.Tables.Count > 0)
                {
                    Session["InchargeID"] = ds.Tables[0].Rows[0]["InchargeID"].ToString();
                    Session["UserTypeID"] = ds.Tables[0].Rows[0]["UserTypeId"].ToString();
                    Session["UserName"] = ds.Tables[0].Rows[0]["InName"].ToString();
                    Session["EmailId"] = ds.Tables[0].Rows[0]["EmailId"].ToString();
                    Session["InName"] = ds.Tables[0].Rows[0]["InName"].ToString();
                    Session["ModuleID"] = ds.Tables[0].Rows[0]["ModuleID"].ToString();
                    UserTypeID = Convert.ToInt16(Session["UserTypeID"].ToString());

                    if (Convert.ToInt16(ds.Tables[0].Rows[0]["UserTypeId"].ToString()) == 1 || UserTypeID == 21)
                    {
                        Response.Redirect("AdminHome.aspx");
                    }
                    else if (Convert.ToInt16(ds.Tables[0].Rows[0]["UserTypeId"].ToString()) == 2)
                    {
                        Response.Redirect("Emp_Home.aspx");
                    }
                    else if (Convert.ToInt16(ds.Tables[0].Rows[0]["UserTypeId"].ToString()) == 6)
                    {
                        Response.Redirect("Workshop_Home.aspx");
                    }
                    else if (Convert.ToInt16(ds.Tables[0].Rows[0]["UserTypeId"].ToString()) == 4 || Convert.ToInt16(ds.Tables[0].Rows[0]["UserTypeId"].ToString()) == 12 || Convert.ToInt16(ds.Tables[0].Rows[0]["UserTypeId"].ToString()) == 23)
                    {
                        Response.Redirect("Purchase_Home.aspx");
                    }
                    else if (Convert.ToInt16(ds.Tables[0].Rows[0]["UserTypeId"].ToString()) == 3)
                    {
                        Response.Redirect("AuditHome.aspx");
                    }
                    else if (Convert.ToInt16(ds.Tables[0].Rows[0]["UserTypeId"].ToString()) == 5)
                    {
                        Response.Redirect("Account_Home.aspx");
                    }
                    else if (Convert.ToInt16(ds.Tables[0].Rows[0]["UserTypeId"].ToString()) == 7)
                    {
                        Response.Redirect("ArchHome.aspx");
                    }
                    else if (Convert.ToInt16(ds.Tables[0].Rows[0]["UserTypeId"].ToString()) == 10)
                    {
                        Response.Redirect("AcademicUserHome.aspx");
                    }
                    else if (Convert.ToInt16(ds.Tables[0].Rows[0]["UserTypeId"].ToString()) == 9)
                    {
                        Response.Redirect("Store_Materials.aspx");
                    }
                    else if (Convert.ToInt16(ds.Tables[0].Rows[0]["UserTypeId"].ToString()) == 13)
                    {
                        Response.Redirect("TransportHome.aspx");
                    }
                    else if (Convert.ToInt16(ds.Tables[0].Rows[0]["UserTypeId"].ToString()) == 14)
                    {
                        Response.Redirect("TransportHome.aspx");
                    }
                    else if (Convert.ToInt16(ds.Tables[0].Rows[0]["UserTypeId"].ToString()) == 15 || Session["UserTypeID"].ToString() == "16" || Session["UserTypeID"].ToString() == "17"
                        || Session["UserTypeID"].ToString() == "18" || Session["UserTypeID"].ToString() == "19" || Session["UserTypeID"].ToString() == "20")
                    {
                        Response.Redirect("TransportHome.aspx");
                    }
                    else if (UserTypeID == 22)
                    {
                        Response.Redirect("Visitor_Home.aspx");
                    }
                    else if (UserTypeID == 24)
                    {
                        Response.Redirect("Security_Home.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Valid Login Details');", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Valid Login Details');", true);
            }
        }
    }
}