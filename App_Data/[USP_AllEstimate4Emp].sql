















CREATE procedure [dbo].[USP_AllEstimate4Emp] --'GURMOHAN'    















(           















@Id as nvarchar(50)  ,





@ModuleID as int         















)           















as           















begin           















SELECT     Estimate.EstId, Estimate.ZoneId,ISNULL(Estimate.IsApproved,1) AS IsApproved,ISNULL(Estimate.IsRejected,0) AS IsRejected,Z.ZoneName,A.AcaName, Estimate.SubEstimate, Estimate.SanctionDate, Estimate.EstmateCost, WorkAllot.WorkAllotName,ISNULL(Esti
mate.IsItemRejected,0) AS IsItemRejected







           
















FROM       Estimate INNER JOIN           















           WorkAllot ON Estimate.WAId = WorkAllot.WAId        















           INNER JOIN Zone Z ON Z.ZoneID=Estimate.ZoneId        















           INNER JOIN Academy A ON A.AcaId=Estimate.AcaId 















           where Estimate.AcaID IN (select distinct AcaID from AcademyAssignToEmployee where Active=1 and EmpId = (select InchargeId from Incharge where LoginId=@id))  and Estimate.ModuleID = @ModuleID      















           order by ISNULL(Estimate.ModifyOn,Estimate.CreatedOn) desc















END