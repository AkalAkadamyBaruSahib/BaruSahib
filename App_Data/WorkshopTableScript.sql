USE [Akal]
GO
/****** Object:  Table [dbo].[WorkshopDispatchMaterial]    Script Date: 8/26/2016 1:11:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkshopDispatchMaterial](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EstID] [int] NULL,
	[EMRID] [int] NULL,
	[DispatchQty] [int] NULL,
	[DispatchBy] [int] NULL,
	[DispatchOn] [datetime] NULL,
 CONSTRAINT [PK_WorkshopDispatchMaterial] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WorkshopStoreMaterial]    Script Date: 8/26/2016 1:11:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkshopStoreMaterial](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MatID] [int] NULL,
	[AcaID] [int] NULL,
	[InStoreQty] [decimal](16, 2) NULL,
	[ModifyBy] [int] NULL,
	[ModifyOn] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_WorkshopStoreMaterial] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WorkshopDispatchMaterial]  WITH CHECK ADD  CONSTRAINT [FK_WorkshopDispatchMaterial_Estimate] FOREIGN KEY([EstID])
REFERENCES [dbo].[Estimate] ([EstId])
GO
ALTER TABLE [dbo].[WorkshopDispatchMaterial] CHECK CONSTRAINT [FK_WorkshopDispatchMaterial_Estimate]
GO
ALTER TABLE [dbo].[WorkshopDispatchMaterial]  WITH CHECK ADD  CONSTRAINT [FK_WorkshopDispatchMaterial_EstimateAndMaterialOthersRelations] FOREIGN KEY([EMRID])
REFERENCES [dbo].[EstimateAndMaterialOthersRelations] ([Sno])
GO
ALTER TABLE [dbo].[WorkshopDispatchMaterial] CHECK CONSTRAINT [FK_WorkshopDispatchMaterial_EstimateAndMaterialOthersRelations]
GO
ALTER TABLE [dbo].[WorkshopStoreMaterial]  WITH CHECK ADD  CONSTRAINT [FK_WorkshopStoreMaterial_Academy] FOREIGN KEY([AcaID])
REFERENCES [dbo].[Academy] ([AcaId])
GO
ALTER TABLE [dbo].[WorkshopStoreMaterial] CHECK CONSTRAINT [FK_WorkshopStoreMaterial_Academy]
GO
ALTER TABLE [dbo].[WorkshopStoreMaterial]  WITH CHECK ADD  CONSTRAINT [FK_WorkshopStoreMaterial_Material] FOREIGN KEY([MatID])
REFERENCES [dbo].[Material] ([MatId])
GO
ALTER TABLE [dbo].[WorkshopStoreMaterial] CHECK CONSTRAINT [FK_WorkshopStoreMaterial_Material]
GO
