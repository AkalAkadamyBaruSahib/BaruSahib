CREATE procedure [dbo].[AutoGenerateAllPendingCompliantReport]

(

@date as datetime

)



AS



BEGIN

Select Z.ZoneName As Zone,A.AcaName As Academy,Inc.InName As CreatedBy,C.Description,C.CreatedOn,C.ComplaintType,C.Severity,C.Status from ComplaintTickets C

INNER JOIN Zone Z ON Z.ZoneId = C.ZoneID 

INNER JOIN Academy A ON A.AcaId = C.AcaID

INNER JOIN Incharge Inc ON Inc.InchargeId = C.CreatedBy

WHERE  C.Status='In Progress' and C.TentativeDate <  @date

END