ALTER Procedure [dbo].[procSaveVisitors]  
(  
 @Name varchar(500)  
,@TotalNoOfMen int 
,@TotalNoOfWomen int
,@TotalNoOfChildren int 
,@PurposeOfVisit varchar(200)  
,@VehicleNo varchar(200)  
,@Identification varchar(200)  
,@IdentificationPath varchar(500)  
,@NoOfDaysToStay int
,@ContactNumber varchar(50)  
,@CreatedOn datetime  
,@VisitorAddress varchar(500)  
,@BuildingID int 
,@VisitorTypeID int 
,@CreatedBy int  
,@ModifyBy int  
,@ModifyOn datetime  
,@VisitorsPhoto varchar(500)
,@VisitorsAuthorityLetter varchar(500)
,@TimePeriodTo datetime 
,@TimePeriodFrom datetime 
,@RoomRent varchar(200)  
,@ElectricityBill varchar(200) 
,@State varchar(500)
,@Country varchar(500)
,@City varchar(500)
,@IsActive bit
,@VisitorReference varchar(500)
,@RoomRentType int
,@AdmissionNumber varchar(50)
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
		,[AdmissionNumber])
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
		   ,@AdmissionNumber)


   return    SCOPE_IDENTITY()
END