ALTER PROCEDURE uspSaveVehicleDocuments  
(  
 @ID INT,  
 @VehicleID INT,  
 @DocumentTypeID INT,  
 @uploadedFile VARCHAR(500),  
 @EndDate DATETIME,
 @IsApproved bit  
)  
AS  
BEGIN  
IF EXISTS(SELECT * FROM VechilesDocumentRelation WHERE VehicleID=@VehicleID AND TransportDocumentID=@DocumentTypeID)  
BEGIN  
 Update VechilesDocumentRelation SET [Path]=@uploadedFile, DocumentEndDate=@EndDate,CreatedOn = GetDate(),IsApproved=@IsApproved WHERE ID=@ID  
END  



ELSE  



BEGIN  



 Insert Into VechilesDocumentRelation Values(@VehicleID ,@DocumentTypeID, @uploadedFile ,@EndDate,GetDate(),@IsApproved)  



END  



END