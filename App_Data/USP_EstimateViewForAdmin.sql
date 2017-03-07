ALTER procedure [dbo].[USP_EstimateViewForAdmin]           

(



@ModuleID as int,



@InchargeID as int                    



)                    



AS



BEGIN    



IF(@InchargeID=84)



BEGIN  



SELECT    DISTINCT Estimate.EstId,Academy.AcaId,Estimate.CreatedOn,Estimate.EstmateCost,ISNULL(Estimate.IsItemRejected,0) AS IsItemRejected,



          ISNULL(Estimate.IsApproved,1) AS IsApproved,ISNULL(Estimate.IsRejected,0) AS IsRejected, CONVERT(VARCHAR(20), Estimate.ModifyOn, 107) AS dt, 



          Estimate.SubEstimate, Zone.ZoneName, Academy.AcaName, WorkAllot.WorkAllotName, Estimate.FileNme, Estimate.FilePath,Estimate.ModifyOn



FROM      Estimate INNER JOIN              



          Zone ON Estimate.ZoneId = Zone.ZoneId 



		  INNER JOIN Academy ON Estimate.AcaId = Academy.AcaId



	      INNER JOIN WorkAllot ON Estimate.WAId = WorkAllot.WAId



	      INNER JOIN AcademyAssignToEmployee AAE ON AAE.AcaId = Academy.AcaId



		  INNER JOIN EstimateAndMaterialOthersRelations EMR on Estimate.EstId = EMR.EstId



WHERE     Estimate.IsActive =1  and AAE.EmpId=@InchargeID 



          and EMR.MatTypeId in(22,48,35,33,50)



order by  Estimate.ModifyOn desc    



END







ELSE IF(@InchargeID=32 or @InchargeID=111)



BEGIN   



SELECT    DISTINCT Estimate.EstId,Academy.AcaId,Estimate.CreatedOn,Estimate.EstmateCost,ISNULL(Estimate.IsItemRejected,0) AS IsItemRejected,



          ISNULL(Estimate.IsApproved,1) AS IsApproved,ISNULL(Estimate.IsRejected,0) AS IsRejected, CONVERT(VARCHAR(20), Estimate.ModifyOn, 107) AS dt, 



          Estimate.SubEstimate, Zone.ZoneName, Academy.AcaName, WorkAllot.WorkAllotName, Estimate.FileNme, Estimate.FilePath,Estimate.ModifyOn



FROM      Estimate INNER JOIN              



          Zone ON Estimate.ZoneId = Zone.ZoneId 



  		  INNER JOIN Academy ON Estimate.AcaId = Academy.AcaId



          INNER JOIN WorkAllot ON Estimate.WAId = WorkAllot.WAId



          INNER JOIN AcademyAssignToEmployee AAE ON AAE.AcaId = Academy.AcaId



       	  INNER JOIN EstimateAndMaterialOthersRelations EMR on Estimate.EstId = EMR.EstId



WHERE     Estimate.IsActive =1  and AAE.EmpId=@InchargeID 



          and EMR.MatTypeId in(49)



order by  Estimate.ModifyOn desc    



END



ELSE



BEGIN            



SELECT  DISTINCT Estimate.EstId,Academy.AcaId,Estimate.CreatedOn,Estimate.EstmateCost,ISNULL(Estimate.IsItemRejected,0) AS IsItemRejected,



        ISNULL(Estimate.IsApproved,1) AS IsApproved,ISNULL(Estimate.IsRejected,0) AS IsRejected, CONVERT(VARCHAR(20), Estimate.ModifyOn, 107) AS dt, 



        Estimate.SubEstimate, Zone.ZoneName, Academy.AcaName, WorkAllot.WorkAllotName, Estimate.FileNme, Estimate.FilePath,Estimate.ModifyOn



FROM    Estimate INNER JOIN              



        Zone ON Estimate.ZoneId = Zone.ZoneId 



        INNER JOIN Academy ON Estimate.AcaId = Academy.AcaId



	    INNER JOIN WorkAllot ON Estimate.WAId = WorkAllot.WAId



        INNER JOIN AcademyAssignToEmployee AAE ON AAE.AcaId = Academy.AcaId



		INNER JOIN EstimateAndMaterialOthersRelations EMR on Estimate.EstId = EMR.EstId



WHERE   Estimate.IsActive =1 and  Estimate.ModuleID = @ModuleID and AAE.EmpId=@InchargeID



        --AND Estimate.CreatedOn>=DATEADD(day,-30,getdate()) 



		and EMR.MatTypeId not in(22,48,35,49,50)



order by  Estimate.ModifyOn desc    



-- order by  Estimate.IsItemRejected desc   



END         



END 