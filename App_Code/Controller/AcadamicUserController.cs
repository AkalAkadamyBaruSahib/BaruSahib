using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AkalAcademy;

/// <summary>
/// Summary description for AcadamicUserController
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class AcadamicUserController : System.Web.Services.WebService {

    public AcadamicUserController () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string SaveComplaintTicket(ComplaintTickets complaintTickets, int isAdd, int InchageID, int UserTypeID)
    {
        UsersRepository usersRepository = new UsersRepository(new DataContext());
        List<Incharge> Inchage = usersRepository.GetUsersByUserType(2);
        string ErrorMsg = string.Empty;

        if (complaintTickets.ID == 0)
        {
            complaintTickets.AssignedTo = Inchage[0].InchargeId;
            complaintTickets.CreatedOn = DateTime.Now;
            complaintTickets.Status = "Assigned";
            ErrorMsg = "Complaint has been created and assigned to sewa daar.";
        }

        if (complaintTickets.ID > 0)
        {
            ErrorMsg = "Complaint has been updated.";
        }

        if (Inchage[0].UserTypeId == 2 && complaintTickets.ID > 0 && complaintTickets.TentativeDate > DateTime.Now.AddDays(7))
        {
            complaintTickets.IsApprovedRequired = true;
            complaintTickets.IsApproved = false;
            ErrorMsg = "Completion Date can not more then a week, Email has been sent to Admin for approval.";
        }

        System.Data.DataSet dsDegisDetails = new System.Data.DataSet();
        dsDegisDetails = DAL.DalAccessUtility.GetDataInDataSet("SELECT DISTINCT AcademyAssignToEmployee.ZoneId FROM AcademyAssignToEmployee INNER JOIN Incharge ON AcademyAssignToEmployee.EmpId = Incharge.InchargeId where AcademyAssignToEmployee.Active=1 and  Incharge.InchargeID='" + InchageID + "'");

        complaintTickets.ZoneID = int.Parse(dsDegisDetails.Tables[0].Rows[0]["ZoneID"].ToString());
        AcadamicUserRepository acadamicUserRepository = new AcadamicUserRepository(new DataContext());
        acadamicUserRepository.SaveComplaintTicket(complaintTickets);


        ComplaintTicketsSMS(InchageID, UserTypeID, complaintTickets.CreatedBy);

        return ErrorMsg;
    }

    [WebMethod(EnableSession = true)]
    public void ComplaintTicketsSMS(int InchargeID, int usertTypeID, int createdBy)
    {
        // string userType = Session["UserTypeID"].ToString();

        string smsTo = string.Empty;
        UsersRepository User = new UsersRepository(new DataContext());

        if (usertTypeID == 2)
        {
            var usrss = User.GetInchargeByInchargeID(createdBy);

            Utility.SendSMS(usrss.Rows[0]["InMobile"].ToString(), "Your complaint ticket has been updated, Please login to akalsewa.org to view the status.");
        }
        else
        {
            var usrss = User.GetInchargeByInchargeID(InchargeID);
            var ConstructionUser = User.GetUsersByUserTypeAndAcademic(2, int.Parse(usrss.Rows[0]["AcaID"].ToString()));
            foreach (string inchargeNumber in ConstructionUser)
            {
                smsTo += inchargeNumber + ",";
            }

            Utility.SendSMS(smsTo, "New Complaint has been created, Please login to akalsewa.org for more information.");
        }

    }

    [WebMethod]
    public List<DTO.Ticket> GetComplaintTickets(string loginID)
    {
        AcadamicUserRepository acadamicUserRepository = new AcadamicUserRepository(new DataContext());
      //  acadamicUserRepository.SaveComplaintTicket();

        System.Data.DataSet dsDegisDetails = new System.Data.DataSet();
        dsDegisDetails = DAL.DalAccessUtility.GetDataInDataSet("select ct.SeverityDays,ct.Severity,ct.FeedBack,ct.TentativeDate, ac.AcaName, z.ZoneName, ct.ID,ct.Description,ct.CreatedBy,CONVERT(VARCHAR(19),ct.CreatedOn) AS CreatedOn,(select InName from Incharge where InchargeId= ct.AssignedTo) AS AssignedTo,ISNULL(CONVERT(VARCHAR(19),ct.CompletionDate),'') AS ModifyOn ,ct.Comments, ct.status, ct.Image, ct.ComplaintType from ComplaintTickets ct INNER JOIN AcademyAssignToEmployee aae on aae.EmpId=ct.CreatedBy INNER JOIN Academy ac on ac.AcaId=aae.AcaId INNER JOIN Zone z on z.ZoneId=aae.ZoneId WHERE ct.ZoneID IN (SELECT DISTINCT AcademyAssignToEmployee.ZoneId FROM AcademyAssignToEmployee INNER JOIN Incharge ON AcademyAssignToEmployee.EmpId = Incharge.InchargeId where AcademyAssignToEmployee.Active=1 and  Incharge.LoginId='" + loginID + "') order by CreatedOn desc");
        List<DTO.Ticket> tickets = new List<DTO.Ticket>();
        DTO.Ticket ticket = null;
        for (int i = 0; i < dsDegisDetails.Tables[0].Rows.Count; i++)
        {
            ticket = new DTO.Ticket();
            ticket.ID = int.Parse(dsDegisDetails.Tables[0].Rows[i]["ID"].ToString());
            ticket.Description = dsDegisDetails.Tables[0].Rows[i]["Description"].ToString();
            ticket.CreatedBy = dsDegisDetails.Tables[0].Rows[i]["CreatedBy"].ToString();
            ticket.CreatedOn = Convert.ToDateTime(dsDegisDetails.Tables[0].Rows[i]["CreatedOn"].ToString()).ToString();
            ticket.AssignedTo = dsDegisDetails.Tables[0].Rows[i]["AssignedTo"].ToString();
            if (!string.IsNullOrEmpty(dsDegisDetails.Tables[0].Rows[i]["ModifyOn"].ToString()))
            {
                ticket.CompletionDate = String.Format(String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(dsDegisDetails.Tables[0].Rows[i]["ModifyOn"]))); //dsDegisDetails.Tables[0].Rows[i]["ModifyOn"].ToString();
            }
            ticket.Comments = dsDegisDetails.Tables[0].Rows[i]["Comments"].ToString();
            ticket.ComplaintType = dsDegisDetails.Tables[0].Rows[i]["ComplaintType"].ToString();
            ticket.Status = "<span style='color:green'><b>" + dsDegisDetails.Tables[0].Rows[i]["Status"].ToString() + "</b></span>";
            ticket.Zone = dsDegisDetails.Tables[0].Rows[i]["ZoneName"].ToString();
            ticket.Academy = dsDegisDetails.Tables[0].Rows[i]["AcaName"].ToString();
            ticket.Feedback = dsDegisDetails.Tables[0].Rows[i]["Feedback"].ToString();
            if (!string.IsNullOrEmpty(dsDegisDetails.Tables[0].Rows[i]["TentativeDate"].ToString()))
            {
                if (DateTime.Now > Convert.ToDateTime(dsDegisDetails.Tables[0].Rows[i]["TentativeDate"].ToString()))
                {
                    ticket.Status = "<span style='color:red'><b>" + dsDegisDetails.Tables[0].Rows[i]["Status"].ToString() + "</b></span>";
                }
                ticket.TentativeDate = String.Format(String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(dsDegisDetails.Tables[0].Rows[i]["TentativeDate"]))); //dsDegisDetails.Tables[0].Rows[i]["ModifyOn"].ToString();
            }
            ticket.Severity = dsDegisDetails.Tables[0].Rows[i]["Severity"].ToString();
            ticket.SeverityDays = dsDegisDetails.Tables[0].Rows[i]["SeverityDays"].ToString();

            if (DateTime.Now.AddDays(2) >= Convert.ToDateTime(dsDegisDetails.Tables[0].Rows[i]["CreatedOn"].ToString()) &&
            string.IsNullOrEmpty(dsDegisDetails.Tables[0].Rows[i]["TentativeDate"].ToString()) &&
                dsDegisDetails.Tables[0].Rows[i]["Status"].ToString()=="Assigned")
            {
                ticket.Status = "<span style='color:orange'><b>" + dsDegisDetails.Tables[0].Rows[i]["Status"].ToString() + "</b></span>";
            }
            tickets.Add(ticket);
        }

        return tickets;
        //'';
    }

    [WebMethod]
    public DTO.Ticket GetComplaintTicketsByID(int ID)
    {
        AcadamicUserRepository acadamicUserRepository = new AcadamicUserRepository(new DataContext());
        //  acadamicUserRepository.SaveComplaintTicket();

        System.Data.DataSet dsDegisDetails = new System.Data.DataSet();
        dsDegisDetails = DAL.DalAccessUtility.GetDataInDataSet("select ct.SeverityDays,ct.Severity,ct.FeedBack,ct.TentativeDate, ac.AcaName, z.ZoneName, ct.ID,ct.Description,ct.CreatedBy,CONVERT(VARCHAR(19),ct.CreatedOn) AS CreatedOn,(select InName from Incharge where InchargeId= ct.AssignedTo) AS AssignedTo,ISNULL(CONVERT(VARCHAR(19),ct.CompletionDate),'') AS ModifyOn ,ct.Comments, ct.status, ct.Image, ct.ComplaintType from ComplaintTickets ct INNER JOIN AcademyAssignToEmployee aae on aae.EmpId=ct.CreatedBy INNER JOIN Academy ac on ac.AcaId=aae.AcaId INNER JOIN Zone z on z.ZoneId=aae.ZoneId where ct.ID=" + ID + " order by CreatedOn desc");
        DTO.Ticket ticket = null;
        ticket = new DTO.Ticket();
        ticket.ID = int.Parse(dsDegisDetails.Tables[0].Rows[0]["ID"].ToString());
        ticket.Description = dsDegisDetails.Tables[0].Rows[0]["Description"].ToString();
        ticket.CreatedBy = dsDegisDetails.Tables[0].Rows[0]["CreatedBy"].ToString();
        ticket.CreatedOn = Convert.ToDateTime(dsDegisDetails.Tables[0].Rows[0]["CreatedOn"].ToString()).ToString();
        ticket.AssignedTo = dsDegisDetails.Tables[0].Rows[0]["AssignedTo"].ToString();
        ticket.CompletionDate = dsDegisDetails.Tables[0].Rows[0]["ModifyOn"].ToString();
        ticket.Comments = dsDegisDetails.Tables[0].Rows[0]["Comments"].ToString();
        ticket.ComplaintType = dsDegisDetails.Tables[0].Rows[0]["ComplaintType"].ToString();
        ticket.Status = dsDegisDetails.Tables[0].Rows[0]["Status"].ToString();

        if (DateTime.Now.AddDays(2)>=Convert.ToDateTime(dsDegisDetails.Tables[0].Rows[0]["CreatedOn"].ToString()) &&
            string.IsNullOrEmpty(dsDegisDetails.Tables[0].Rows[0]["TentativeDate"].ToString()))
        {
            ticket.Status = "<span forcolor='orange'>" + dsDegisDetails.Tables[0].Rows[0]["Status"].ToString() + "</span>";
        }

        

        ticket.Zone = dsDegisDetails.Tables[0].Rows[0]["ZoneName"].ToString();
        ticket.Academy = dsDegisDetails.Tables[0].Rows[0]["AcaName"].ToString();
        ticket.Feedback = dsDegisDetails.Tables[0].Rows[0]["Feedback"].ToString();
        if (!string.IsNullOrEmpty(dsDegisDetails.Tables[0].Rows[0]["TentativeDate"].ToString()))
        {
            ticket.TentativeDate = String.Format(String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(dsDegisDetails.Tables[0].Rows[0]["TentativeDate"]))); //dsDegisDetails.Tables[0].Rows[i]["ModifyOn"].ToString();
        }
        ticket.Severity = dsDegisDetails.Tables[0].Rows[0]["Severity"].ToString();
        ticket.SeverityDays = dsDegisDetails.Tables[0].Rows[0]["SeverityDays"].ToString();

        return ticket;
        //'';
    }

    [WebMethod]
    public void SaveTicketFeedback(int ID, string feedback)
    {
        AcadamicUserRepository acadamicUserRepository = new AcadamicUserRepository(new DataContext());
        acadamicUserRepository.SaveTicketFeedback(ID, feedback);
    }


    [WebMethod]
    public void DeleteTicket(int ID)
    {
        AcadamicUserRepository acadamicUserRepository = new AcadamicUserRepository(new DataContext());
        acadamicUserRepository.DeleteTicket(ID);
        //'';
    }


    

}
