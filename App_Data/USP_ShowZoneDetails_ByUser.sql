CREATE PROCEDURE [dbo].[USP_ShowZoneDetails_ByUser]      
(      
@User as nvarchar(50)      
)      
AS       
BEGIN      
SELECT Zone.ZoId,Country.CountryName, State.StateName, City.CityName, Zone.ZoneName, Zone.ZoneId, Zone.Active, Zone.CreatedOn, Zone.CreatedBy, Zone.ModifyOn,       
                      Zone.ModifyBy, Zone.Pincode, (select count(1) from academy aA where aA.ZoneID=Zone.ZoneID) as Coun   
FROM         Zone INNER JOIN      
                      Country ON Zone.CountryId = Country.CountryId INNER JOIN      
                      State ON Zone.StateId = State.StateId 
					  INNER JOIN City ON Zone.CityId = City.CityId 
					  ORDER BY Zone.ZoneName ASC
END
