CREATE procedure [dbo].[USP_ShowAllZoneDetails]  



as   



begin  



SELECT     Country.CountryName, State.StateName, City.CityName, Zone.ZoneName, Zone.ZoneId, Zone.Active, Zone.CreatedOn, Zone.CreatedBy, Zone.ModifyOn,   



                      Zone.ModifyBy, Zone.Pincode, Zone.ZoId,(select count(1) from academy aA where aA.ZoneID=Zone.ZoneID) as Coun  



FROM         Zone INNER JOIN  



                      Country ON Zone.CountryId = Country.CountryId INNER JOIN  



                      State ON Zone.StateId = State.StateId INNER JOIN  



                      City ON Zone.CityId = City.CityId 



end
