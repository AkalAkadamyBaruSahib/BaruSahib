--sp_helptext  USP_NewEstimate '0','0','0','0','',84,'5','2514','','0.0','1','Singed Copy','EstFile/image2-4-6-2016-3-15-31-PM.JPG',True,'',False,False



ALTER PROCEDURE [dbo].[USP_NewEstimate]                          

(

    @ZoneId as int,                          

	@AcaId as int,                          

    @SubEstimateId as nvarchar(max),                          

	@TypeWorkId as int,                  

	@SanctionDate as datetime,                                  

    @CreatedBy as nvarchar(100),                          

    @type as int,                   

	@EstId as int,                                

    @Active as int ,                

    @EstmatedCost as decimal(18,2),              

    @WAId as int,            

    @FileName as nvarchar(100),            

    @FilePath as nvarchar(max),          

    @IsApproved as bit,      

	@ReviewComments varchar(max),      

	@IsRejected  bit,    

	@IsItemRejected bit

)

as                          



BEGIN     



IF(@type=1)   



 BEGIN      



 INSERT into Estimate(ZoneId,AcaId,SubEstimate,TypeWorkId,SanctionDate,Active,CreatedBy,CreatedOn,EstmateCost,WAId,FileNme,FilePath,IsApproved,IsRejected,IsItemRejected) VALUES(@ZoneId,@AcaId,upper(@SubEstimateId),@TypeWorkId,@SanctionDate,@Active,



 @CreatedBy,GETDATE(),@EstmatedCost,@WAId,@FileName,@FilePath,@IsApproved,0,@IsItemRejected)    



END                          



ELSE IF(@type=2)               



 BEGIN            



UPDATE Estimate SET ZoneId=@ZoneId,AcaId=@AcaId,SubEstimate=upper(@SubEstimateId),

TypeWorkId=@TypeWorkId,SanctionDate=@SanctionDate,Active=@Active,ModIFyBy=@CreatedBy,ModIFyOn=GETDATE(),EstmateCost=@EstmatedCost,WAId=@WAId,FileNme=@FileName,FilePath=@FilePath 

WHERE EstId=@EstId  



END            







ELSE IF (@type=3)      







 BEGIN                  



DELETE FROM Estimate WHERE EstId=@EstId       







END                   



ELSE IF (@type=4)                  



BEGIN                  



 UPDATE Estimate SET Active=@Active,ModIFyBy=@CreatedBy,ModIFyOn=GETDATE() WHERE EstId=@EstId 



END          



ELSE IF (@type=5)          



 BEGIN      



 IF(@FilePath='')      



 BEGIN      



  UPDATE Estimate SET IsApproved=@IsApproved,WAId=@WAID,IsItemRejected=@IsItemRejected,IsRejected = @IsRejected,ModifyOn = GETDATE(),SubEstimate=upper(@SubEstimateId),TypeWorkId=@TypeWorkId WHERE EstId=@EstId      



 END



 ELSE      



 BEGIN      



  UPDATE Estimate SET FileNme=@FileName,FilePath=@FilePath,

   IsApproved=@IsApproved,IsItemRejected=@IsItemRejected,IsRejected = @IsRejected , ModifyOn = GETDATE()



  WHERE EstId=@EstId      



 END      



  INSERT INTO EstimateStatus VALUES(@EstId,@CreatedBy,@ReviewComments,GETDATE())      



 END                       



END 




