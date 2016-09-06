ALTER procedure [dbo].[USP_ShowMatrialDetails_ByID]            

(          

@Id as nvarchar(50)          

)          

as            

begin            

SELECT     MatId, MatName, MatCost, Active, CreatedOn, CreatedBy, UnitId, MatTypeId,AkalWorkshopRate  

FROM         Material where MatId=@Id  and Active=1  

end
