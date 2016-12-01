ALTER PROCEDURE [dbo].[GetComplaintsTicketReports] --'1/1/0001 00:00:00:000','12/31/2015 00:00:00:000'



(                          



@userType int,



@inchargeID int,



@status varchar(50)



)                          



AS          



BEGIN     

IF(@userType=1)

BEGIN  

	SELECT z.ZoneName As Zone,

     ac.AcaName As Academy,

	ct.ComplaintType,

	ct.Description,

	ct.SeverityDays,

	ct.Severity,

	ct.Comments,

	ct.FeedBack,

	ct.TentativeDate,

	ct.status,

	CONVERT(VARCHAR(19),ct.CreatedOn) AS CreatedOn,

    (SELECT InName FROM Incharge WHERE InchargeId= ct.CreatedBy) AS CreatedBy,ISNULL(CONVERT(VARCHAR(19),ct.CompletionDate),'') AS ModifyOn

FROM ComplaintTickets ct LEFT OUTER JOIN Academy ac on ac.AcaId=ct.AcaID INNER JOIN Zone z on ct.ZoneID=z.ZoneId

WHERE ct.status=@status order by CreatedOn desc

END

ELSE

BEGIN 

SELECT z.ZoneName As Zone,

     ac.AcaName As Academy,

	ct.ComplaintType,

	ct.Description,

	ct.SeverityDays,

	ct.Severity,

	ct.Comments,

	ct.FeedBack,

	ct.TentativeDate,

	ct.status,

	CONVERT(VARCHAR(19),ct.CreatedOn) AS CreatedOn,

    (SELECT InName FROM Incharge WHERE InchargeId= ct.CreatedBy) AS CreatedBy,ISNULL(CONVERT(VARCHAR(19),ct.CompletionDate),'') AS ModifyOn

	

FROM ComplaintTickets ct LEFT OUTER JOIN Academy ac on ac.AcaId=ct.AcaID INNER JOIN Zone z on ct.ZoneID=z.ZoneId

WHERE ct.status=@status and ct.CreatedBy = @inchargeID order by CreatedOn desc

END

END