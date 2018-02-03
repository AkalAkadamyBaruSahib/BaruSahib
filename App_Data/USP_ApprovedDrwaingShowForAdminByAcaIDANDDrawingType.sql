CREATE procedure [dbo].[USP_ApprovedDrwaingShowForAdminByAcaIDANDDrawingType]            
(
@AcaId as int,
@IsApproved as bit,
@DrwingTypeID AS INT
)       
as            
begin            
IF(@AcaId>0 and @DrwingTypeID=0)
BEGIN
SELECT     DrawingType.DwTypeName, Drawing.DwgId, Drawing.DwgNo, Drawing.RevisionNo, Drawing.DwgFileName, Drawing.PdfFileName, Drawing.PdfFilePath,       
           Drawing.DwgFileName AS Expr1, Drawing.DwgFilePath, Drawing.Active, CONVERT(VARCHAR(20), Drawing.CreatedOn, 107) AS CreatedOn, Drawing.CreatedBy,       
           Drawing.DrawingName, Drawing.AcaId, Drawing.DwTypeId, Zone.ZoneName, Academy.AcaName,    
           Drawing.IsApproved ,   ISNULL(Drawing.CreatedOn,GETDATE()) AS [CreatedOnDate] ,inc.InName  
FROM       Drawing INNER JOIN      
           DrawingType ON Drawing.DwTypeId = DrawingType.DwTypeId INNER JOIN      
           Zone ON Drawing.ZoneId = Zone.ZoneId INNER JOIN      
           Academy ON Drawing.AcaId = Academy.AcaId     
    	   inner join  Incharge inc on inc.inchargeID=Drawing.CreatedBy 
		   WHERE Drawing.Active=1  and  Drawing.AcaId = @AcaId and  Drawing.IsApproved = @IsApproved 
		   Order by Drawing.CreatedOn desc    
		   END
ELSE IF(@AcaId>0 AND @DrwingTypeID>0)
BEGIN
SELECT     DrawingType.DwTypeName, Drawing.DwgId, Drawing.DwgNo, Drawing.RevisionNo, Drawing.DwgFileName, Drawing.PdfFileName, Drawing.PdfFilePath,       
           Drawing.DwgFileName AS Expr1, Drawing.DwgFilePath, Drawing.Active, CONVERT(VARCHAR(20), Drawing.CreatedOn, 107) AS CreatedOn, Drawing.CreatedBy,       
           Drawing.DrawingName, Drawing.AcaId, Drawing.DwTypeId, Zone.ZoneName, Academy.AcaName,    
           Drawing.IsApproved ,   ISNULL(Drawing.CreatedOn,GETDATE()) AS [CreatedOnDate] ,inc.InName  
FROM       Drawing INNER JOIN      
           DrawingType ON Drawing.DwTypeId = DrawingType.DwTypeId INNER JOIN      
           Zone ON Drawing.ZoneId = Zone.ZoneId INNER JOIN      
           Academy ON Drawing.AcaId = Academy.AcaId     
    	   inner join  Incharge inc on inc.inchargeID=Drawing.CreatedBy 
		   WHERE Drawing.Active=1  and  Drawing.AcaId = @AcaId and Drawing.DwTypeId = @DrwingTypeID and  Drawing.IsApproved = @IsApproved 
	       Order by Drawing.CreatedOn desc    
	       END
ELSE
BEGIN
SELECT     DrawingType.DwTypeName, Drawing.DwgId, Drawing.DwgNo, Drawing.RevisionNo, Drawing.DwgFileName, Drawing.PdfFileName, Drawing.PdfFilePath,       
           Drawing.DwgFileName AS Expr1, Drawing.DwgFilePath, Drawing.Active, CONVERT(VARCHAR(20), Drawing.CreatedOn, 107) AS CreatedOn, Drawing.CreatedBy,       
           Drawing.DrawingName, Drawing.AcaId, Drawing.DwTypeId, Zone.ZoneName, Academy.AcaName,    
           Drawing.IsApproved ,   ISNULL(Drawing.CreatedOn,GETDATE()) AS [CreatedOnDate] ,inc.InName  
FROM       Drawing INNER JOIN      
	       DrawingType ON Drawing.DwTypeId = DrawingType.DwTypeId INNER JOIN      
           Zone ON Drawing.ZoneId = Zone.ZoneId INNER JOIN      
           Academy ON Drawing.AcaId = Academy.AcaId     
        	inner join  Incharge inc on inc.inchargeID=Drawing.CreatedBy 
		  WHERE Drawing.Active=1  and  Drawing.DwTypeId = @DrwingTypeID and  Drawing.IsApproved = @IsApproved 
          Order by Drawing.CreatedOn desc 
END
end 