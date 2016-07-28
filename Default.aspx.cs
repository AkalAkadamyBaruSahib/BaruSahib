﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
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
            inchrge = repo.GetLoginUserDetail(txtUserName.Text.Trim(), txtPwd.Text.Trim());
            //DataSet dsExit = DAL.DalAccessUtility.GetDataInDataSet("select EmailId,Pwd from Login where Active=1 AND EmailId='" + txtUserName.Text + "'");
            if (inchrge != null && inchrge.InchargeId > 0)
            {
                Session["InchargeID"] = inchrge.InchargeId;
                Session["UserTypeID"] = inchrge.UserTypeId;
                Session["UserName"] = inchrge.InName;
                Session["EmailId"] = inchrge.LoginId;
                Session["InName"] = inchrge.InName;
                Session["ModuleID"] = inchrge.ModuleID;


                if (inchrge.UserTypeId == 1 || inchrge.UserTypeId == 21)
                {
                    Response.Redirect("AdminHome.aspx");
                }
                else if (inchrge.UserTypeId == 2)
                {
                    Response.Redirect("Emp_Home.aspx");
                }
                else if (inchrge.UserTypeId == 6)
                {
                    Response.Redirect("Workshop_Home.aspx");
                }
                else if (inchrge.UserTypeId == 4 || inchrge.UserTypeId == 12 || inchrge.UserTypeId == 23)
                {
                    Response.Redirect("Purchase_Home.aspx");
                }
                else if (inchrge.UserTypeId == 3)
                {
                    Response.Redirect("AuditHome.aspx");
                }
                else if (inchrge.UserTypeId == 5)
                {
                    Response.Redirect("Account_Home.aspx");
                }
                else if (inchrge.UserTypeId == 7)
                {
                    Response.Redirect("ArchHome.aspx");
                }
                else if (inchrge.UserTypeId == 10)
                {
                    Response.Redirect("AcademicUserHome.aspx");
                }
                else if (inchrge.UserTypeId == 9)
                {
                    Response.Redirect("Store_Materials.aspx");
                }
                else if (inchrge.UserTypeId == 13)
                {
                    Response.Redirect("TransportHome.aspx");
                }
                else if (inchrge.UserTypeId == 14)
                {
                    Response.Redirect("TransportHome.aspx");
                }
                else if (inchrge.UserTypeId == 15 || inchrge.UserTypeId == 16 || inchrge.UserTypeId == 17
                    || inchrge.UserTypeId == 18 || inchrge.UserTypeId == 19 || inchrge.UserTypeId == 20)
                {
                    Response.Redirect("TransportHome.aspx");
                }
                else if (inchrge.UserTypeId == 22)
                {
                    Response.Redirect("Visitor_Home.aspx");
                }
                else if (inchrge.UserTypeId == 24)
                {
                    Response.Redirect("Security_Home.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Valid Login Details');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Valid Login Details');", true);
            }
        }
    }
}