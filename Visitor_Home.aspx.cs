﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Visitor_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AutoGeneratedCompliantsReport();
    }
    public void SendAutoGeneratedRoomStatusReport()
    {
        string msg = "Attached are the Vacant and Booked Room List.";
        string FileName = string.Empty;
        DataTable RoomStatus = new DataTable();

        RoomStatus = DAL.DalAccessUtility.GetDataInDataSet("exec GetRoomStatusByBuilding'-1'").Tables[0];

        if (RoomStatus != null)
        {
            FileName = "RoomStatus" + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".xls";
            string FilePath = Server.MapPath("Bills") + "\\" + FileName;

            RoomStatus.TableName = FileName;
            RoomStatus.WriteXml(FilePath);

            string cc = "jagjit@barusahib.org";
            string to = "bhupinder@barusahib.org,harkrishan.s.sidhu@gmail.com";
            try
            {
                Utility.SendEmailUsingAttachments(FilePath, to, cc, msg, "Akal Software Room status Report.");
            }
            catch { }
            finally
            {

            }
        }
    }

    public void AutoGeneratedCompliantsReport()
    { 
        string FileName = string.Empty;
        AutogeneratedEmail emaildays = new AutogeneratedEmail();
        PurchaseRepository repo = new PurchaseRepository(new AkalAcademy.DataContext());
        emaildays = repo.GetReportDay((int)TypeEnum.AutoGenerateReportType.VisitorRoomStatus);
        string[] days = emaildays.DayOfMonth.Split(',');

        if ((DateTime.Now.Day == int.Parse(days[0]) || DateTime.Now.Day == int.Parse(days[1]) || DateTime.Now.Day == int.Parse(days[2]) || DateTime.Now.Day == int.Parse(days[3]) || DateTime.Now.Day == int.Parse(days[4]) || DateTime.Now.Day == int.Parse(days[5]) || DateTime.Now.Day == int.Parse(days[6]) || DateTime.Now.Day == int.Parse(days[7]) || DateTime.Now.Day == int.Parse(days[8]) || DateTime.Now.Day == int.Parse(days[9]) || DateTime.Now.Day == int.Parse(days[10]) || DateTime.Now.Day == int.Parse(days[11]) ||
            DateTime.Now.Day == int.Parse(days[12]) || DateTime.Now.Day == int.Parse(days[13]) || DateTime.Now.Day == int.Parse(days[14]) || DateTime.Now.Day == int.Parse(days[15]) || DateTime.Now.Day == int.Parse(days[16]) || DateTime.Now.Day == int.Parse(days[17]) || DateTime.Now.Day == int.Parse(days[18]) || DateTime.Now.Day == int.Parse(days[19]) || DateTime.Now.Day == int.Parse(days[20]) || DateTime.Now.Day == int.Parse(days[21]) || DateTime.Now.Day == int.Parse(days[22]) || DateTime.Now.Day == int.Parse(days[23]) ||
            DateTime.Now.Day == int.Parse(days[24]) || DateTime.Now.Day == int.Parse(days[25]) || DateTime.Now.Day == int.Parse(days[26]) || DateTime.Now.Day == int.Parse(days[27]) || DateTime.Now.Day == int.Parse(days[28]) || DateTime.Now.Day == int.Parse(days[29])) && emaildays.EmailSent == false)
        {
            emaildays.EmailSent = true;
            FileName = "RoomStatus" + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + ".xls";
            string FilePath = Server.MapPath("Bills") + "\\" + FileName;
            if (!System.IO.File.Exists(FilePath))
            {
                SendAutoGeneratedRoomStatusReport();
            }
            repo.UpdateAutogeneratedEmail(emaildays);
        }
        else if ((DateTime.Now.Day == int.Parse(days[0]) || DateTime.Now.Day == int.Parse(days[1]) || DateTime.Now.Day == int.Parse(days[2]) || DateTime.Now.Day == int.Parse(days[3]) || DateTime.Now.Day == int.Parse(days[4]) || DateTime.Now.Day == int.Parse(days[5]) || DateTime.Now.Day == int.Parse(days[6]) || DateTime.Now.Day == int.Parse(days[7]) || DateTime.Now.Day == int.Parse(days[8]) || DateTime.Now.Day == int.Parse(days[9]) || DateTime.Now.Day == int.Parse(days[10]) || DateTime.Now.Day == int.Parse(days[11]) ||
             DateTime.Now.Day == int.Parse(days[12]) || DateTime.Now.Day == int.Parse(days[13]) || DateTime.Now.Day == int.Parse(days[14]) || DateTime.Now.Day == int.Parse(days[15]) || DateTime.Now.Day == int.Parse(days[16]) || DateTime.Now.Day == int.Parse(days[17]) || DateTime.Now.Day == int.Parse(days[18]) || DateTime.Now.Day == int.Parse(days[19]) || DateTime.Now.Day == int.Parse(days[20]) || DateTime.Now.Day == int.Parse(days[21]) || DateTime.Now.Day == int.Parse(days[22]) || DateTime.Now.Day == int.Parse(days[23]) ||
             DateTime.Now.Day == int.Parse(days[24]) || DateTime.Now.Day == int.Parse(days[25]) || DateTime.Now.Day == int.Parse(days[26]) || DateTime.Now.Day == int.Parse(days[27]) || DateTime.Now.Day == int.Parse(days[28]) || DateTime.Now.Day == int.Parse(days[29])) && emaildays.EmailSent == true)
        {
            emaildays.EmailSent = false;
            repo.UpdateAutogeneratedEmail(emaildays);
        }
    }
}