USE [BookShop2023]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 12/13/2023 3:09:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[ID] [int] NOT NULL,
	[Username] [varchar](50) NULL,
	[Password] [varchar](255) NULL,
	[Role] [nchar](10) NULL,
	[Name] [nvarchar](255) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 12/13/2023 3:09:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](100) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 12/13/2023 3:09:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Tel] [text] NULL,
	[Address] [nvarchar](255) NULL,
	[Email] [text] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 12/13/2023 3:09:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] NOT NULL,
	[CatID] [int] NULL,
	[Name] [text] NOT NULL,
	[Price] [text] NULL,
	[Quantity] [int] NULL,
	[ImagePath] [text] NULL,
	[Author] [nvarchar](255) NULL,
	[PublishedYear] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchase]    Script Date: 12/13/2023 3:09:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase](
	[ID] [int] NOT NULL,
	[CustomerID] [int] NOT NULL,
	[FinalTotal] [float] NOT NULL,
	[CreatedAt] [date] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Purchase] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseDetail]    Script Date: 12/13/2023 3:09:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseDetail](
	[ID] [int] NOT NULL,
	[PurchaseID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Price] [float] NULL,
	[Quantity] [int] NULL,
	[Total] [int] NULL,
 CONSTRAINT [PK_PurchaseDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseStatusEnum]    Script Date: 12/13/2023 3:09:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseStatusEnum](
	[Key] [nvarchar](50) NOT NULL,
	[Value] [int] NOT NULL,
	[Description] [nvarchar](255) NULL,
	[DisplayText] [nvarchar](255) NULL,
 CONSTRAINT [PK_PurchaseStatusEnum] PRIMARY KEY CLUSTERED 
(
	[Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CatID])
REFERENCES [dbo].[Category] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Purchase] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[Purchase] CHECK CONSTRAINT [FK_Customer_Purchase]
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_PurchaseStausEnum] FOREIGN KEY([Status])
REFERENCES [dbo].[PurchaseStatusEnum] ([Value])
GO
ALTER TABLE [dbo].[Purchase] CHECK CONSTRAINT [FK_Purchase_PurchaseStausEnum]
GO
ALTER TABLE [dbo].[PurchaseDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseDetail_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[PurchaseDetail] CHECK CONSTRAINT [FK_PurchaseDetail_Product]
GO
ALTER TABLE [dbo].[PurchaseDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseDetail_Purchase] FOREIGN KEY([PurchaseID])
REFERENCES [dbo].[Purchase] ([ID])
GO
ALTER TABLE [dbo].[PurchaseDetail] CHECK CONSTRAINT [FK_PurchaseDetail_Purchase]
GO
