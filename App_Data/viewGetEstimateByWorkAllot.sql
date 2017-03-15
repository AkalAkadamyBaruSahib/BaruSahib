CREATE proc viewGetEstimateByWorkAllot 







@WaId INT







AS







BEGIN



----DECLARE @WaId INT=210

--IF OBJECT_ID('tempdb..#yourtable') IS NOT NULL

--BEGIN

--  --DROP TABLE ##CLIENTS_KEYWORD

--drop table #yourtable

--END

--  /*Then it exists*/

--CREATE table #yourtable

--    ([Value] DECIMAL(16,2), [ColumnName] int)

--;



--INSERT INTO #yourtable

--    ([Value],[ColumnName])



SELECT   sum(qty), e.EstId  from [dbo].[EstimateAndMaterialOthersRelations] ER

INNER JOIN Estimate E on E.EstId=ER.EstId



WHERE WAId=@WaId group by e.EstId















--DECLARE @cols AS NVARCHAR(MAX),







--    @query  AS NVARCHAR(MAX)















--select @cols = STUFF((SELECT ',' + QUOTENAME(ColumnName) 







--                    from #yourtable







--                    group by ColumnName







                   







--            FOR XML PATH(''), TYPE







--            ).value('.', 'NVARCHAR(MAX)') 







--        ,1,1,'')















--set @query = N'SELECT ' + @cols + N' from 







--             (







--                select value, ColumnName







--                from #yourtable







--            ) x







--            pivot 







--            (







--                max(value)







--                for ColumnName in (' + @cols + N')







--            ) p '















--exec sp_executesql @query;















END