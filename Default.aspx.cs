using System;
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
      // Utility.SendEmailUsingAttachments(@"D:\GITAkalSewa\Source\Repos\BaruSahib\EstFile\6808_22112016.xlsx", "itmohali@barusahib.org", "test", "15 Days pending report");
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
           
            // DataSet dsExit = DAL.DalAccessUtility.GetDataInDataSet("select EmailId,Pwd from Login where Active=1 AND EmailId='" + txtUserName.Text + "'");
            if (inchrge != null && inchrge.InchargeId > 0)
            {


                AdminTypeRelation subadmin = repo.GetAdminType(inchrge.InchargeId);

                Session["InchargeID"] = inchrge.InchargeId;
                Session["UserTypeID"] = inchrge.UserTypeId;
                Session["UserName"] = inchrge.InName;
                Session["EmailId"] = inchrge.LoginId;
                Session["InName"] = inchrge.InName;
                Session["ModuleID"] = inchrge.ModuleID;
                

                if (subadmin != null)
                {
                    switch ((int)subadmin.SubAdminTypeID)
                    {
                        case (int)TypeEnum.SubAdminName.Barusahib:
                            Session["AdminType"] = (int)TypeEnum.SubAdminName.Barusahib;
                            break;
                        case (int)TypeEnum.SubAdminName.Electrical:
                            Session["AdminType"] = (int)TypeEnum.SubAdminName.Electrical;
                            break;
                        case (int)TypeEnum.SubAdminName.Transport:
                            Session["AdminType"] = (int)TypeEnum.SubAdminName.Transport;
                            break;
                        case (int)TypeEnum.SubAdminName.Construction:
                            Session["AdminType"] = (int)TypeEnum.SubAdminName.Construction;
                            break;
                        case (int)TypeEnum.SubAdminName.TransportMaintenance:
                            Session["AdminType"] = (int)TypeEnum.SubAdminName.TransportMaintenance;
                            break;
                        case (int)TypeEnum.SubAdminName.TransportVehicleMaintenance:
                            Session["AdminType"] = (int)TypeEnum.SubAdminName.TransportVehicleMaintenance;
                            break;
                        default:
                            break;
                    }
                }

                if (inchrge.UserTypeId == (int)TypeEnum.UserType.ADMIN)
                {
                    Response.Redirect("Admin_Dashboard.aspx");
                }
                else if (inchrge.UserTypeId == (int)TypeEnum.UserType.CONSTRUCTION || inchrge.UserTypeId == (int)TypeEnum.UserType.COMPLAINT || inchrge.UserTypeId == (int)TypeEnum.UserType.ELECTRICAL)
                {
                    Response.Redirect("Emp_Home.aspx");
                }
                else if (inchrge.UserTypeId == (int)TypeEnum.UserType.WORKSHOPADMIN || inchrge.UserTypeId == (int)TypeEnum.UserType.WORKSHOPEMPLOYEE)
                {
                   Response.Redirect("Workshop_Home.aspx");
                }
                else if (inchrge.UserTypeId == (int)TypeEnum.UserType.PURCHASE || inchrge.UserTypeId == (int)TypeEnum.UserType.PURCHASECOMMITTEE || inchrge.UserTypeId == (int)TypeEnum.UserType.PURCHASEEMPLOYEE)
                {
                    Response.Redirect("Purchase_Home.aspx");
                }

                else if (inchrge.UserTypeId == (int)TypeEnum.UserType.AUDIT)
                {
                    Response.Redirect("AuditHome.aspx");
                }
                else if (inchrge.UserTypeId == (int)TypeEnum.UserType.ACCOUNT)
                {
                    Response.Redirect("Account_Home.aspx");
                }
                else if (inchrge.UserTypeId == (int)TypeEnum.UserType.ARCHITECTURAL)
                {
                    Response.Redirect("ArchHome.aspx");
                }
                else if (inchrge.UserTypeId == (int)TypeEnum.UserType.ACADEMIC)
                {
                    Response.Redirect("AcademicUserHome.aspx");
                }
                else if (inchrge.UserTypeId == (int)TypeEnum.UserType.STORE)
                {
                    Response.Redirect("StoreHome.aspx");
                    Response.Redirect("Store_Materials.aspx");
                }
                else if (inchrge.UserTypeId == (int)TypeEnum.UserType.TRANSPORTADMIN || inchrge.UserTypeId == (int)TypeEnum.UserType.TRANSPORTMANAGER || inchrge.UserTypeId == (int)TypeEnum.UserType.INSURANCECOORDINATOR || inchrge.UserTypeId == (int)TypeEnum.UserType.TRANSPORTTRAINEE
                         || inchrge.UserTypeId == (int)TypeEnum.UserType.TRANSPORTZONEINCHARGE || inchrge.UserTypeId == (int)TypeEnum.UserType.BACKOFFICEHQ || inchrge.UserTypeId == (int)TypeEnum.UserType.CLUSTERHEAD)
                {
                    Response.Redirect("TransportHome.aspx");
                }
                else if (inchrge.UserTypeId == (int)TypeEnum.UserType.FrontDesk || inchrge.UserTypeId == (int)TypeEnum.UserType.RECEPTIONADMIN)
                {
                    Response.Redirect("Visitor_Home.aspx");
                }
                else if (inchrge.UserTypeId == (int)TypeEnum.UserType.SECURITY)
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
    protected void btnForgetPassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChangePassword.aspx");
    }
}