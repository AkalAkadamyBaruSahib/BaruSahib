ALTER procedure [dbo].[USP_InsertUpdateVehicle]      

(          

 @VehicleID INT         

 ,@ZoneID INT        

 ,@AcaID INT        

 ,@TransportTypeID INT      

 ,@VehicleNo VARCHAR(200)        

 ,@OwnerName VARCHAR(200)     

 ,@OwnerNumber VARCHAR(200)    

 ,@Sitting INT    

 ,@Norms VARCHAR(200)     

 ,@IsApproved BIT    

 ,@IsTemporary BIT    

 ,@FileNumber  VARCHAR(200)    

 ,@EngineNumber VARCHAR(200)    

 ,@ChassisNumber VARCHAR(200)    

 ,@Make VARCHAR(200)    

 ,@Model VARCHAR(200)    

 ,@WrittenContract BIT    

 ,@PeriodOfContract INT    

 ,@ContractDieselRate VARCHAR(200)    

 ,@OilSlab VARCHAR(200)    

 ,@FrontRightTyreCondition VARCHAR(200)    

 ,@FrontLeftTyreCondition VARCHAR(200)    

 ,@RearRightTyreCondition VARCHAR(200)    

 ,@RearLeftTyreCondition VARCHAR(200)  

 ,@KMPerDay INT  

 ,@RearRightTyre2Condition VARCHAR(200)    

 ,@RearLeftTyre2Condition VARCHAR(200)

 ,@NumberOfTypres INT

 ,@ConductorID INT

 ,@DriverID INT

 ,@VehicleContractRate INT

 ,@VehicleAverage DECIMAL

)   

  

AS                      

BEGIN      

DECLARE @VecID INT          

IF(@VehicleID >0)          

BEGIN          

    

  UPDATE [dbo].[Vehicles] SET

     

  [TypeID] = @TransportTypeID      

 ,[Number] = @VehicleNo      

 ,[Sitter] = @Sitting      

 ,[OwnerName] = @OwnerName      

 ,[OwnerNumber] = @OwnerNumber      

 ,[IsApproved] = @IsApproved      

 ,[AcademyID] = @AcaID      

 ,[ZoneID] = @ZoneID      

 ,[ModifyOn] = getdate()    

 ,[IsTemporary] = @IsTemporary    

 ,[FileNumber] = @FileNumber  

 ,[EngineNumber] =@EngineNumber  

 ,[ChassisNumber] =@ChassisNumber  

 ,[Make] =@Make  

 ,[Model] =@Model  

 ,[WrittenContract] =@WrittenContract  

 ,[PeriodOfContract] =@PeriodOfContract  

 ,[ContractDieselRate] =@ContractDieselRate  

 ,[OilSlab] =@OilSlab  

 ,[FrontRightTyreCondition] =@FrontRightTyreCondition  

 ,[FrontLeftTyreCondition] =@FrontLeftTyreCondition  

 ,[RearRightTyreCondition] =@RearRightTyreCondition  

 ,[RearLeftTyreCondition] =@RearLeftTyreCondition  

 ,[KMPerDay]= @KMPerDay  

 ,[RearRightTyre2Condition]= @RearRightTyre2Condition  

 ,[RearLeftTyre2Condition]= @RearLeftTyre2Condition

 ,[NumberOfTypres]=@NumberOfTypres 

 ,[ConductorID]=@ConductorID

 ,[DriverID]=@DriverID

 ,[VehicleContractRate]=@VehicleContractRate 

 ,[VehicleAverage]=@VehicleAverage

   

 WHERE ID=@VehicleID      

    

        

    

DELETE FROM VechilesNormsRelation WHERE VehicleID=@VehicleID          



INSERT INTO dbo.VechilesNormsRelation (VehicleID,NormID) SELECT @VehicleID,item  FROM dbo.Split(@Norms,',')   

    

END          

ELSE          

BEGIN          

    

INSERT INTO [dbo].[Vehicles]          

 ([TypeID]          

 ,[Number]          

 ,[Sitter]          

 ,[OwnerName]          

 ,[OwnerNumber]          

 ,[IsApproved]          

 ,[AcademyID]          

 ,[ZoneID]    

 ,IsTemporary    

 ,FileNumber    

 ,EngineNumber    

 ,ChassisNumber    

 ,Make    

 ,Model    

 ,WrittenContract    

 ,PeriodOfContract    

 ,ContractDieselRate    

 ,OilSlab    

 ,FrontRightTyreCondition    

 ,FrontLeftTyreCondition    

 ,RearRightTyreCondition    

 ,RearLeftTyreCondition  

 ,KMPerDay

 ,RearRightTyre2Condition

 ,RearLeftTyre2Condition

 ,NumberOfTypres

 ,DriverID

 ,ConductorID
 
 ,VehicleContractRate 

 ,VehicleAverage)    

         

VALUES          

    

 (@TransportTypeID          

 ,@VehicleNo          

 ,@Sitting          

 ,@OwnerName          

 ,@OwnerNumber          

 ,@IsApproved          

 ,@AcaID          

 ,@ZoneID    

 ,@IsTemporary    

 ,@FileNumber    

 ,@EngineNumber    

 ,@ChassisNumber    

 ,@Make    

 ,@Model    

 ,@WrittenContract    

 ,@PeriodOfContract    

 ,@ContractDieselRate    

 ,@OilSlab    

 ,@FrontRightTyreCondition    

 ,@FrontLeftTyreCondition    

 ,@RearRightTyreCondition    

 ,@RearLeftTyreCondition  

 ,@KMPerDay

 ,@RearRightTyre2Condition

 ,@RearLeftTyre2Condition

 ,@NumberOfTypres

 ,@DriverID

 ,@ConductorID
 
 ,@VehicleContractRate 

 ,@VehicleAverage)          

    

          

SET @VecID= (SELECT @@IDENTITY)        

  

IF(@Norms!='')  

BEGIN  

 INSERT INTO dbo.VechilesNormsRelation (VehicleID,NormID) SELECT @VecID,item  FROM dbo.Split(@Norms,',')  

END  

    

END          

END 