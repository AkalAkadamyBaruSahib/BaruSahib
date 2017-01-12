CREATE PROCEDURE [dbo].[USP_ShowWorkAllotDetails_ByUser]                    

(                  

@User as int,

@PSID as INT                  

)                  

as                    

begin                

SELECT  Zone.ZoneName,

 Academy.AcaName,

 Academy.AcaId, 

 WorkAllot.WAId, 

 WorkAllot.WorkAllotName, 

 WorkAllot.Active,

 WorkAllot.CreateOn, 

 WorkAllot.CreatedBy, 

 WorkAllot.ModifyOn, 

 WorkAllot.ModifyBy, 

 WorkAllot.ImageFilePath,

 dbo.fnEstimateCostByWorkAllot(WorkAllot.WAId,1) AS EstimateCost,

 dbo.fnPurchasedCostByWorkAllot(WorkAllot.WAId) AS BillCost



FROM         Zone INNER JOIN  

                      WorkAllot ON Zone.ZoneId = WorkAllot.ZoneId INNER JOIN  

                      Academy ON WorkAllot.AcaId = Academy.AcaId where WorkAllot.CreatedBy=3   

					  order by   WorkAllot.WAId desc       







end












