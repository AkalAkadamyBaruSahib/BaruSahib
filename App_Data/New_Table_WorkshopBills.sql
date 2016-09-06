USE [Akal]
GO
/****** Object:  Table [dbo].[WorkshopBills]    Script Date: 9/5/2016 1:37:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WorkshopBills](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BillNumber] [varchar](50) NULL,
	[BillPath] [varchar](100) NULL,
	[WSId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK_WorkshopBills] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[WorkshopBills]  WITH CHECK ADD  CONSTRAINT [FK_WorkshopBills_Academy] FOREIGN KEY([WSId])
REFERENCES [dbo].[Academy] ([AcaId])
GO
ALTER TABLE [dbo].[WorkshopBills] CHECK CONSTRAINT [FK_WorkshopBills_Academy]
GO
ALTER TABLE [dbo].[WorkshopBills]  WITH CHECK ADD  CONSTRAINT [FK_WorkshopBills_Incharge] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Incharge] ([InchargeId])
GO
ALTER TABLE [dbo].[WorkshopBills] CHECK CONSTRAINT [FK_WorkshopBills_Incharge]
GO
