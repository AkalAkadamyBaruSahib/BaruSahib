USE [Akal]
GO
/****** Object:  Table [dbo].[VisitorReceipt]    Script Date: 10/21/2016 10:15:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[VisitorReceipt](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReceivedAmount] [money] NULL,
	[ReceivedFrom] [varchar](50) NULL,
	[PaymentMode] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[Createdby] [int] NULL,
	[ReceiptPath] [varchar](100) NULL,
	[ChequeDDNumber] [varchar](50) NULL,
 CONSTRAINT [PK_VisitorReceipt] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
