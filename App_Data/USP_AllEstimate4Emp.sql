CREATE procedure [dbo].[USP_AllEstimate4Emp]    

(           

@Id as int,

@ModuleID as int         

)           

as           

begin           

SELECT     Estimate.EstId, Estimate.ZoneId,ISNULL(Estimate.IsApproved,1) AS IsApproved,ISNULL(Estimate.IsRejected,0) AS IsRejected,Z.ZoneName,A.AcaName,

           Estimate.SubEstimate, Estimate.SanctionDate, Estimate.EstmateCost, WorkAllot.WorkAllotName,ISNULL(Estimate.IsItemRejected,0) AS IsItemRejected

FROM       Estimate INNER JOIN           

           WorkAllot ON Estimate.WAId = WorkAllot.WAId    

           INNER JOIN Zone Z ON Z.ZoneID=Estimate.ZoneId  

           INNER JOIN Academy A ON A.AcaId=Estimate.AcaId

		   INNER JOIN AcademyAssignToEmployee  aae on aae.AcaId=a.AcaId

           WHERE aae.EmpId = @id  AND Estimate.ModuleID = @ModuleID AND Estimate.CreatedOn > DATEADD(day,-30,GETDATE())

           order by ISNULL(Estimate.ModifyOn,Estimate.CreatedOn) desc

END