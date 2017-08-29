ALTER Procedure [dbo].[procSaveVisitors]  



(  



 @Name varchar(50)  



,@TotalNoOfMen int 



,@TotalNoOfWomen int



,@TotalNoOfChildren int 



,@PurposeOfVisit varchar(50)  



,@VehicleNo varchar(50)  



,@Identification varchar(50)  



,@IdentificationPath varchar(50)  



,@NoOfDaysToStay int



,@ContactNumber varchar(50)  



,@CreatedOn datetime  



,@VisitorAddress varchar(200)  



,@BuildingID int 



,@VisitorTypeID int 



,@CreatedBy int  



,@ModifyBy int  



,@ModifyOn datetime  



,@VisitorsPhoto varchar(50)



,@VisitorsAuthorityLetter varchar(50)



,@TimePeriodTo datetime 



,@TimePeriodFrom datetime 



,@RoomRent int  



,@ElectricityBill int 



,@State int



,@Country int



,@City int



,@IsActive bit



,@VisitorReference varchar(20)



,@RoomRentType int



,@AdmissionNumber varchar(50)

,@PurposeOfVisitRemarks varchar(150)



)  



AS  



BEGIN  



INSERT INTO [dbo].[Visitors]  



           ([Name]  



         ,[TotalNoOfMen]



         ,[TotalNoOfWomen]



         ,[TotalNoOfChildren]



         ,[PurposeOfVisit]  



         ,[VehicleNo]  



         ,[Identification]  



         ,[IdentificationPath]  



    	 ,[NoOfDaysToStay]



         ,[ContactNumber]  



         ,[CreatedOn]  



         ,[VisitorAddress]  



         ,[BuildingID] 



         ,[VisitorTypeID] 



         ,[CreatedBy]  



         ,[ModifyBy]  



         ,[ModifyOn] 



         ,[VisitorsPhoto]  



         ,[VisitorsAuthorityLetter] 



         ,[TimePeriodTo] 



         ,[TimePeriodFrom]  



         ,[RoomRent]  



         ,[ElectricityBill]



	     ,[State]



         ,[Country]



         ,[City]



    	   ,[IsActive]



         ,[VisitorReference]



	    ,[RoomRentType]



		,[AdmissionNumber]
		
		
		,[PurposeOfVisitRemarks])



  VALUES  



          (@Name  



          ,@TotalNoOfMen



          ,@TotalNoOfWomen



          ,@TotalNoOfChildren



          ,@PurposeOfVisit  



          ,@VehicleNo  



          ,@Identification  



          ,@IdentificationPath 



	      ,@NoOfDaysToStay 



          ,@ContactNumber  



          ,@CreatedOn  



          ,@VisitorAddress  



          ,@BuildingID 



	      ,@VisitorTypeID 



          ,@CreatedBy  



          ,@ModifyBy  



          ,@ModifyOn



	      ,@VisitorsPhoto  



          ,@VisitorsAuthorityLetter 



		  ,@TimePeriodTo 



          ,@TimePeriodFrom  



          ,@RoomRent  



          ,@ElectricityBill



	      ,@State 



          ,@Country 



          ,@City



          ,@IsActive



          ,@VisitorReference



		   ,@RoomRentType



		   ,@AdmissionNumber
		   
		   
		   ,@PurposeOfVisitRemarks)











   return    SCOPE_IDENTITY()



END