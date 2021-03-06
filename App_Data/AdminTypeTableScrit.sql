USE [Akal]
GO
/****** Object:  Table [dbo].[AdminType]    Script Date: 8/4/2016 5:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminType](
	[ID] [int] NOT NULL,
	[SubAdminName] [nvarchar](250) NULL,
 CONSTRAINT [PK_SubAdminTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AdminTypeRelation]    Script Date: 8/4/2016 5:25:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminTypeRelation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[SubAdminTypeID] [int] NULL,
 CONSTRAINT [PK_SubAdminTypesRelation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[AdminType] ([ID], [SubAdminName]) VALUES (1, N'Electrical')
INSERT [dbo].[AdminType] ([ID], [SubAdminName]) VALUES (2, N'Barusahib')
INSERT [dbo].[AdminType] ([ID], [SubAdminName]) VALUES (3, N'Transport')
INSERT [dbo].[AdminType] ([ID], [SubAdminName]) VALUES (4, N'Construction')
SET IDENTITY_INSERT [dbo].[AdminTypeRelation] ON 

INSERT [dbo].[AdminTypeRelation] ([ID], [UserID], [SubAdminTypeID]) VALUES (1, 84, 1)
INSERT [dbo].[AdminTypeRelation] ([ID], [UserID], [SubAdminTypeID]) VALUES (2, 78, 2)
INSERT [dbo].[AdminTypeRelation] ([ID], [UserID], [SubAdminTypeID]) VALUES (3, 98, 3)
INSERT [dbo].[AdminTypeRelation] ([ID], [UserID], [SubAdminTypeID]) VALUES (4, 3, 4)
SET IDENTITY_INSERT [dbo].[AdminTypeRelation] OFF
ALTER TABLE [dbo].[AdminTypeRelation]  WITH CHECK ADD  CONSTRAINT [FK_SubAdminTypesRelation_Incharge] FOREIGN KEY([UserID])
REFERENCES [dbo].[Incharge] ([InchargeId])
GO
ALTER TABLE [dbo].[AdminTypeRelation] CHECK CONSTRAINT [FK_SubAdminTypesRelation_Incharge]
GO
ALTER TABLE [dbo].[AdminTypeRelation]  WITH CHECK ADD  CONSTRAINT [FK_SubAdminTypesRelation_SubAdminTypes] FOREIGN KEY([SubAdminTypeID])
REFERENCES [dbo].[AdminType] ([ID])
GO
ALTER TABLE [dbo].[AdminTypeRelation] CHECK CONSTRAINT [FK_SubAdminTypesRelation_SubAdminTypes]
GO
