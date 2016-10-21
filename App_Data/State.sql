USE [Akal]
GO
/****** Object:  Table [dbo].[State]    Script Date: 10/21/2016 10:11:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [int] NULL,
	[StateName] [nvarchar](50) NULL,
	[Active] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifyOn] [datetime] NULL,
	[ModifyBy] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[State] ON 

INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (1, 15, N'PUNJAB', 1, CAST(N'2015-03-21 15:35:56.933' AS DateTime), N'ADMIN', NULL, NULL)
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (2, 15, N'HARYANA', 1, CAST(N'2015-04-02 09:48:56.087' AS DateTime), N'ADMIN', NULL, NULL)
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (3, 15, N'RAJASTHAN', 1, CAST(N'2015-04-03 12:28:05.027' AS DateTime), N'ADMIN', NULL, NULL)
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (4, 15, N'UP', 1, CAST(N'2015-04-24 16:13:58.940' AS DateTime), N'ADMIN', NULL, NULL)
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (5, 15, N'DELHI', 1, CAST(N'2015-07-22 14:00:20.040' AS DateTime), N'ADMIN', NULL, NULL)
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (6, 15, N'HIMACHAL PRADESH', 1, CAST(N'2015-07-22 14:21:10.903' AS DateTime), N'ADMIN', NULL, NULL)
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (7, 15, N'TAMIL NADU', 1, CAST(N'2015-07-22 14:21:28.470' AS DateTime), N'ADMIN', NULL, NULL)
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (8, 15, N'ANDHRA PRADESH', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (9, 15, N'ARUNACHAL PRADESH', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (10, 15, N'ASSAM', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (11, 15, N'BIHAR', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (12, 15, N'CHHATTISGARH', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (13, 15, N'GOA', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (14, 15, N'GUJARAT', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (15, 15, N'JAMMU & KASHMIR', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (16, 15, N'JHARKHAND', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (17, 15, N'KARNATAKA', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (18, 15, N'KERALA', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (19, 15, N'MADHYA PRADESH', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (20, 15, N'MAHARASHTRA', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (21, 15, N'MANIPUR', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (22, 15, N'MEGHALAYA', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (23, 15, N'MIZORAM', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (24, 15, N'NAGALAND', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (25, 15, N'ODISHA', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (26, 15, N'SIKKIM', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (27, 15, N'TRIPURA', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (28, 15, N'UTTARKHAND', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (29, 15, N'WEST BENGAL', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
INSERT [dbo].[State] ([StateId], [CountryId], [StateName], [Active], [CreatedOn], [CreatedBy], [ModifyOn], [ModifyBy]) VALUES (30, 15, N'CHANDIGARH', 1, CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1', CAST(N'2016-10-17 16:26:51.223' AS DateTime), N'1')
SET IDENTITY_INSERT [dbo].[State] OFF
ALTER TABLE [dbo].[State]  WITH CHECK ADD FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO
