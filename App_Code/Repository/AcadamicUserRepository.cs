using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AkalAcademy;

/// <summary>
/// Summary description for AcadamicUserRepository
/// </summary>
public class AcadamicUserRepository
{
    private DataContext _context;

    public AcadamicUserRepository(DataContext context)
    {
        _context = context;
    }

    public void SaveComplaintTicket(ComplaintTickets ticket)
    {
        if (ticket.ID > 0)
        {
            ComplaintTickets Isticket = _context.ComplaintTickets.Where(x => x.ID == ticket.ID).FirstOrDefault();
            Isticket.CompletionDate = ticket.CompletionDate;
           // Isticket.Comments = ticket.Comments;
            Isticket.Status = ticket.Status;
            Isticket.IsApproved = ticket.IsApproved;
            Isticket.IsApprovedRequired = ticket.IsApprovedRequired;
            Isticket.Description = ticket.Description;
            if (Isticket.Status == "Completed")
            {
                Isticket.CompletionDate = DateTime.Now;
            }
            else
            {
                Isticket.TentativeDate = ticket.TentativeDate;
            }
            _context.ComplaintTickets.Add(Isticket);
            _context.Entry(Isticket).State = EntityState.Modified;
        }
        else
        {
            _context.ComplaintTickets.Add(ticket);
            _context.Entry(ticket).State = EntityState.Added;
        }
        int rowAffected = _context.SaveChanges();
    
    }

    public void SaveTicketFeedback(int ID, string feedback)
    {
        if (ID > 0)
        {
            ComplaintTickets Isticket = _context.ComplaintTickets.Where(x => x.ID == ID).FirstOrDefault();
            Isticket.Feedback = feedback;
            _context.ComplaintTickets.Add(Isticket);
            _context.Entry(Isticket).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }

    public void DeleteTicket(int ID)
    {
        ComplaintTickets Isticket = _context.ComplaintTickets.Where(x => x.ID == ID).FirstOrDefault();
        _context.Entry(Isticket).State = EntityState.Deleted;
        _context.SaveChanges();
    }

    public void SaveComments(Comments comnts)
    {
        _context.Entry(comnts).State = EntityState.Added;
        _context.SaveChanges();
    }
}