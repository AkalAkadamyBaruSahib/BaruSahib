USE [Akal]
GO
/****** Object:  Table [dbo].[SittingTyreRelation]    Script Date: 6/5/2017 1:46:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SittingTyreRelation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SittingCapacity] [int] NULL,
	[NumOfTyre] [int] NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[SittingTyreRelation] ON 

INSERT [dbo].[SittingTyreRelation] ([ID], [SittingCapacity], [NumOfTyre]) VALUES (1, 8, 5)
INSERT [dbo].[SittingTyreRelation] ([ID], [SittingCapacity], [NumOfTyre]) VALUES (2, 14, 5)
INSERT [dbo].[SittingTyreRelation] ([ID], [SittingCapacity], [NumOfTyre]) VALUES (3, 17, 5)
INSERT [dbo].[SittingTyreRelation] ([ID], [SittingCapacity], [NumOfTyre]) VALUES (4, 25, 7)
INSERT [dbo].[SittingTyreRelation] ([ID], [SittingCapacity], [NumOfTyre]) VALUES (5, 27, 7)
INSERT [dbo].[SittingTyreRelation] ([ID], [SittingCapacity], [NumOfTyre]) VALUES (6, 32, 7)
INSERT [dbo].[SittingTyreRelation] ([ID], [SittingCapacity], [NumOfTyre]) VALUES (7, 36, 7)
INSERT [dbo].[SittingTyreRelation] ([ID], [SittingCapacity], [NumOfTyre]) VALUES (8, 42, 7)
INSERT [dbo].[SittingTyreRelation] ([ID], [SittingCapacity], [NumOfTyre]) VALUES (9, 52, 7)
SET IDENTITY_INSERT [dbo].[SittingTyreRelation] OFF
