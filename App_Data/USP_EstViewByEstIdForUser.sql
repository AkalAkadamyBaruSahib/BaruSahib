ALTER procedure [dbo].[USP_EstViewByEstIdForUser]  

(  

@InchargeID as int,

@AcaId as int  

)  

as  

begin  

SELECT DISTINCT 

                      Estimate.EstId, Estimate.ZoneId, Estimate.SubEstimate, CONVERT(nvarchar(20), Estimate.ModifyOn, 107) AS SanctionDate,

					  Estimate.EstmateCost, Estimate.AcaId,WorkAllot.WorkAllotName, Estimate.FileNme, Estimate.FilePath

FROM                  Estimate INNER JOIN              

                     Academy ON Estimate.AcaId = Academy.AcaId

	                 INNER JOIN WorkAllot ON Estimate.WAId = WorkAllot.WAId

	                 INNER JOIN AcademyAssignToEmployee AAE ON AAE.AcaId = Academy.AcaId

WHERE                Estimate.IsActive =1 and AAE.EmpId=@InchargeID and Estimate.AcaId=@AcaId  

 



END
