CREATE procedure [dbo].[USP_ExcelEstimate]  

(

@ModuleID as int                   

) 

AS  



BEGIN  



SELECT     Estimate.EstId AS ESTIMATE_ID, Zone.ZoneName AS ZONE_NAME, Academy.AcaName AS ACADEMY_NAME,Estimate.SubEstimate AS ESTIMATE_DESCRIPTION,CONVERT( NVARCHAR(20), Estimate.SanctionDate,107) AS SANCTION_DATE, Estimate.EstmateCost AS ESTIMATED_COST, 






 TypeOfWork.TypeWorkName AS WORKTYPE, WorkAllot.WorkAllotName AS WORKALLOT_NAME  



FROM         Estimate INNER JOIN  



                      Zone ON Estimate.ZoneId = Zone.ZoneId INNER JOIN  



                      Academy ON Estimate.AcaId = Academy.AcaId INNER JOIN  



                      TypeOfWork ON Estimate.TypeWorkId = TypeOfWork.TypeWorkId INNER JOIN  



                      WorkAllot ON Estimate.WAId = WorkAllot.WAId

					  

					  WHERE  Estimate.ModuleID = @ModuleID 

					  

					    order by Estimate.EstId desc



END
