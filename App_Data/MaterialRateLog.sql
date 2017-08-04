USE [Akal]
GO
/****** Object:  Table [dbo].[MaterialRateLog]    Script Date: 8/2/2017 10:36:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialRateLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MatID] [int] NULL,
	[OldRate] [decimal](18, 2) NULL,
	[NewRate] [decimal](18, 2) NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_MaterialRateLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[MaterialRateLog]  WITH CHECK ADD  CONSTRAINT [FK_MaterialRateLog_Material] FOREIGN KEY([MatID])
REFERENCES [dbo].[Material] ([MatId])
GO
ALTER TABLE [dbo].[MaterialRateLog] CHECK CONSTRAINT [FK_MaterialRateLog_Material]
GO
