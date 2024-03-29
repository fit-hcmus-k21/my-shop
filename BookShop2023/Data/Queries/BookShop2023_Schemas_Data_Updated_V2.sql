USE [BookShop2023]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 12/29/2023 1:38:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Role] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 12/29/2023 1:38:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 12/29/2023 1:38:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[PhoneNumber] [varchar](11) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 12/29/2023 1:38:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[ID] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
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
/****** Object:  Table [dbo].[Orders]    Script Date: 12/29/2023 1:38:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[ID] [int] NOT NULL,
	[FinalTotal] [float] NOT NULL,
	[CreatedAt] [date] NULL,
	[Status] [int] NULL,
	[VoucherID] [int] NULL,
	[CustomerID] [int] NOT NULL,
 CONSTRAINT [PK_Purchase] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStatusEnum]    Script Date: 12/29/2023 1:38:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatusEnum](
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
/****** Object:  Table [dbo].[Product]    Script Date: 12/29/2023 1:38:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CatID] [int] NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Quantity] [int] NULL,
	[ImagePath] [nvarchar](255) NULL,
	[Author] [nvarchar](255) NULL,
	[PublishedYear] [int] NULL,
	[PurchasePrice] [money] NULL,
	[SellingPrice] [money] NULL,
	[UploadDate] [datetime] NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Voucher]    Script Date: 12/29/2023 1:38:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voucher](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[PercentOff] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([ID], [Username], [Password], [Name], [Role]) VALUES (2, N'thanhngan22', N'admin', N'Bùi Thị Thanh Ngân', N'admin')
INSERT [dbo].[Account] ([ID], [Username], [Password], [Name], [Role]) VALUES (3, N'chihiro', N'staff', N'Chihiro', N'staff')
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([ID], [Name]) VALUES (1, N'Tâm Lý Học')
INSERT [dbo].[Category] ([ID], [Name]) VALUES (2, N'Giáo Dục')
INSERT [dbo].[Category] ([ID], [Name]) VALUES (3, N'Kỹ Năng Sống')
INSERT [dbo].[Category] ([ID], [Name]) VALUES (4, N'Khoa Học - Công Nghệ')
INSERT [dbo].[Category] ([ID], [Name]) VALUES (5, N'Kinh Tế - Kinh Doanh')
INSERT [dbo].[Category] ([ID], [Name]) VALUES (6, N'Văn Học')
INSERT [dbo].[Category] ([ID], [Name]) VALUES (7, N'Luật')
INSERT [dbo].[Category] ([ID], [Name]) VALUES (8, N'Lịch Sử')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
INSERT [dbo].[OrderStatusEnum] ([Key], [Value], [Description], [DisplayText]) VALUES (N'New', 1, N'Đơn hàng mới được tạo', N'Mới tạo')
INSERT [dbo].[OrderStatusEnum] ([Key], [Value], [Description], [DisplayText]) VALUES (N'Completed', 2, N'Khách hàng đã thanh toán', N'Đã thanh toán')
INSERT [dbo].[OrderStatusEnum] ([Key], [Value], [Description], [DisplayText]) VALUES (N'Cancelled', 3, N'Đơn hàng đã hủy', N'Đã hủy')
INSERT [dbo].[OrderStatusEnum] ([Key], [Value], [Description], [DisplayText]) VALUES (N'Shipping', 4, N'Đã thanh toán và đã đã giao', N'Đã giao')
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (1, 1, N'Tâm Lý Học Đời Sống', 11, N'Images\53b091da-e4ad-404b-ac5b-f844199fcfd1.jpg', N'Sigmund Freud', 2020, 125000.0000, 179000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (2, 1, N'Tâm Lý Học Nỗi Đau', 22, N'Images\eadae6fe-32e9-4e07-95bf-68ca889d4f0f.jpg', N'Patrick Wall', 2019, 179000.0000, 204000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (3, 1, N'Tâm Lý Học Tội Phạm', 6, N'Images\a500c5d4-4375-4f73-b471-866c0f3efdee.jpg', N'Hans Gross', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (4, 1, N'Tâm Lý Học Ứng Dụng', 7, N'Images\35d71c64-66e3-4c16-93cb-99b5048a271e.jpg', N'Patrick King', 2017, 79000.0000, 104000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (6, 1, N'Cân Bằng Cảm Xúc Cả Lúc Bão Giông', 13, N'Images\24883e29-ce00-46f2-a73f-f7036e1b8ea9.jpg', N'Anonymous', 2018, 69000.0000, 94000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (7, 1, N'Người Nhạy Cảm - Món Quà Hay Lời Nguyền', 21, N'Images\fe5e09bd-9864-46a0-8783-16b3323f4864.jpg', N'Anonymous', 2021, 69000.0000, 94000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (8, 1, N'Chữa Lành Bằng Tâm Thức (Tái bản năm 2021)', 17, N'Images\54747dc9-a28c-4b7b-a863-2efa3e99162a.jpg', N'Newton Kondaveti', 2020, 139000.0000, 164000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (9, 1, N'Lo Âu Xã Hội', 19, N'Images\c11adc2a-3317-4d44-a52d-0a4f41a1b3ca.jpg', N'David Shanley', 2019, 159000.0000, 184000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (10, 1, N'Giải Mã Từ Điển Cảm Xúc', 53, N'Images\556bcd06-556b-4bfa-8af6-602bb45b94c5.jpg', N'Kim Eana', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (11, 1, N'Tâm Lý Học - Tất Cả Những Điều Cần Biết Để Thông Thạo Bộ Môn Này', 16, N'Images\ca4804a8-2dcd-41b9-8dec-1dc2b3270cf0.jpg', N'Alan Porter', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (12, 1, N'Tâm Lý Học Tội Phạm - Phác Họa Chân Dung Kẻ Phạm Tội', 28, N'Images\a0aa94cf-7f62-4a1f-948a-dd8c85b27d97.jpg', N'Diệp Hồng Vũ', 2015, 69000.0000, 94000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (13, 2, N'Mĩ Thuật Lớp 1', 100, N'Images\8b73576a-b2b5-4c85-94e9-e35ed555e5ce.jpg', N'Nhà xuất bản Việt Nam', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (14, 2, N'Bổ trợ và Nâng cao Toán 7', 69, N'Images\1145f0c4-7bd5-41fb-accf-741f120415ef.jpg', N'Nhà xuất bản Việt Nam', 2019, 179000.0000, 204000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (15, 2, N'Bài tập Tiếng Anh 9', 73, N'Images\4f6b0232-b104-4dc1-9533-f8b3c5f7e077.jpg', N'Nhà xuất bản Việt Nam', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (16, 2, N'Câu Chuyện Giáo Dục - Tài Trí', 22, N'Images\ffbfe3ca-d0bc-4af6-895a-dcc5d9cade74.jpg', N'Hải Nam', 2017, 79000.0000, 104000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (17, 2, N'Câu Chuyện Giáo Dục - Nhân Hậu', 91, N'Images\268c326a-6c6a-4ef8-ac18-17d7f612bd75.jpg', N'Hải Nam', 2016, 85000.0000, 110000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (18, 2, N'Câu Chuyện Giáo Dục - Kiên Trì', 16, N'Images\8d5d99e8-d364-4eae-859c-5aba99b832c3.jpg', N'Hải Nam', 2018, 69000.0000, 94000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (19, 2, N'Câu Chuyện Giáo Dục - Dũng Cảm', 27, N'Images\3ad0a7f8-d43d-4365-b802-c420bd84555c.jpg', N'Hải Nam', 2021, 69000.0000, 94000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (20, 2, N'Câu Chuyện Giáo Dục - Đạo Đức', 85, N'Images\925b8218-c70e-43b7-9c55-5da99a9094f7.jpg', N'Hải Nam', 2020, 139000.0000, 164000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (21, 2, N'Con Đường Hạnh Phúc', 35, N'Images\3ff920fd-3355-4ebb-8684-c7d3f83441cf.jpg', N'Thiền sư Gelong Thubten', 2019, 159000.0000, 184000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (22, 2, N'Khi Bạn Đang Mơ Thì Người Khác Đang Nỗ Lực', 96, N'Images\24e1a00c-5d3a-42cd-85e6-f637e89aee0c.jpg', N'Vĩ Nhân', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (23, 2, N'Làm Ít Được Nhiều', 45, N'Images\2c3a5905-2266-4488-baf6-bd3b012f9773.jpg', N'Chin-Ning Chu', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (24, 2, N'Bài Học Vô Giá Từ Những Điều Bình Dị ( Tái Bản )', 37, N'Images\2511a959-f616-45d1-982d-847103480c5c.jpg', N'Francis Xavier', 2015, 69000.0000, 94000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (25, 3, N'Đắc Nhân Tâm', 100, N'Images\1f115172-3205-4f63-8db3-293444939057.jpg', N'Dale Camegie', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (26, 3, N'Đánh Thức Con Người Bên Trong Bạn', 69, N'Images\7f80c68a-2559-48f1-9d18-f5c7cf458cf8.jpg', N'Tony Robbins', 2019, 179000.0000, 204000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (27, 3, N'Mindset', 73, N'Images\e70f37eb-d3c2-4728-911d-4064629bbb5f.jpg', N'Carol S.Dweck', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (28, 3, N'Tư Duy Phản Biện', 22, N'Images\2615b516-d2ef-460b-919e-c75cbff52a7d.jpg', N'Richard Paul and Linda Elder', 2017, 79000.0000, 104000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (29, 3, N'Tôi Tài Giỏi Bạn Cũng Thế', 91, N'Images\7ad38770-0041-4873-a323-a67ec8d9ee9b.jpg', N'Anonymous', 2016, 85000.0000, 110000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (30, 3, N'Học Khôn Ngoan Mà Không Gian Nan', 16, N'Images\db37c307-ee04-4b80-bbc9-4272616f7319.jpg', N'Kevin Pauth', 2018, 69000.0000, 94000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (31, 3, N'Tư Duy Sâu', 27, N'Images\092ae371-6877-428e-9330-4689df64bdf9.jpg', N'Diệp Tu', 2021, 69000.0000, 94000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (32, 3, N'Rèn Luyện Tư Duy Phàn Biện', 85, N'Images\7305e0fe-8afa-47d2-8d30-760e35fbebc0.jpg', N'Albert RutherFold', 2020, 139000.0000, 164000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (33, 3, N'Đi Tìm Lẽ Sống', 35, N'Images\11e060e3-b0fa-4839-97cb-8a3fd361cba9.jpg', N'Victor E.Frankl', 2019, 159000.0000, 184000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (34, 3, N'Hiểu Về Trái Tim', 96, N'Images\7d6c0346-34ae-4731-b914-55b26ac556dd.jpg', N'Minh Niệm', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (35, 3, N'What I wish I knew when I was 20', 45, N'Images\12352d7c-7acd-411b-a9a9-957c4e2c2387.jpg', N'Tina Seelig ', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (36, 3, N'Chuyện Con Mèo Dạy Hải Âu Bay', 37, N'Images\f32abe71-5cbe-4556-b542-33a304edd0cc.jpg', N'Luis Sepul Veda', 2015, 69000.0000, 94000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (37, 4, N'How Psychology Works', 100, N'Images\1b89cbb7-d04d-4d13-ad0a-98ecf894bc53.jpg', N'Jo Hemmings', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (38, 4, N'Object Oriented Programming', 69, N'Images\8af45a6f-df49-47bd-9d90-8ef5a50e81bc.jpg', N'HCMUS', 2019, 179000.0000, 204000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (39, 4, N'Why We Sleep ?', 73, N'Images\631efec0-f378-4887-99f7-1838447c6250.jpg', N'Matthew Walker', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (40, 4, N'Python for Data Analysis ', 22, N'Images\3be904af-99a1-40b1-aa91-63cd78e81abf.jpg', N'Bephein John', 2022, 79000.0000, 104000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (41, 4, N'How to improve your memory for study', 91, N'Images\f3c3d47c-3888-46a4-a0d4-a3dffbd8afec.jpg', N'Jonathan HanCock', 2016, 85000.0000, 110000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (42, 4, N'Business Analysis ', 8, N'Images\70e514f3-ad90-49f1-9317-6fbeb164a71f.jpg', N'Dummies', 2021, 69000.0000, 94000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (43, 4, N'12 Xu Hướng Công Nghệ Trong Thời Đại 4.0', 11, N'Images\ce712e11-d7e0-43e4-9343-83727cd876e7.jpg', N'Kevin Kelly', 2021, 69000.0000, 94000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (44, 4, N'Khoa Học Khám Phá - Dữ Liệu Lớn', 8, N'Images\9b76f502-f883-47d6-bded-1be93bdb77df.jpg', N'Nhiều Tác Giả', 2021, 139000.0000, 164000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (45, 4, N'Công Nghiệp Tương Lai', 7, N'Images\24af1be9-05e4-4485-97a7-34bcd0c5359e.jpg', N'Alec Ross', 2022, 159000.0000, 184000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (46, 4, N'Deep Learning - Cuộc Cách Mạng Học Sâu', 4, N'Images\1e276a75-e1c3-4e1d-914e-c11bde43a8e2.jpg', N'Terrence J.Sejnowski', 2023, 99000.0000, 124000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (47, 4, N'Nghệ Thuật Ẩn Mình', 45, N'Images\48e8e9bf-8c6c-4f07-94ed-3cfdb83a7ccf.jpg', N'Kelvin Mitnick', 2021, 89000.0000, 114000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (48, 4, N'Gián Điệp Mạng', 2, N'Images\b8c3cfb5-de8e-428d-9fa8-3514b5471dfe.jpg', N'Clifford Stoll', 2021, 69000.0000, 94000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (49, 4, N'Hacker Lược Sử', 4, N'Images\65c944d8-4d7b-4b08-b4ff-c19daaa2ddda.jpg', N'Steven Levy', 2022, 99000.0000, 124000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (50, 4, N'LIFE 3.0 - Loài Người Trong Kỷ Nguyên Trí Tuệ Nhân Tạo', 3, N'Images\3692abb8-caf2-4864-8f82-1c1493dd0655.jpg', N'Max Tegmark', 2023, 179000.0000, 204000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (51, 5, N'Nhà Lãnh Đạo Tương Lai', 73, N'Images\6dc869a7-7af7-417f-9172-4f30d923dce6.jpg', N'Jacob Morgan', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (52, 5, N'Nghệ Thuật Viết Quảng Cáo', 22, N'Images\c9313c97-3f1f-4da6-bef7-295bb1e4b468.jpg', N'Victor O. Schwab', 2017, 79000.0000, 104000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (53, 5, N'Những Sát Thủ Hàng Loạt Trong Giới Tài Chính', 91, N'Images\d9dd747b-b4dc-457a-9a36-912b5350108d.jpg', N'Bruce KellyTom Ajamie', 2016, 85000.0000, 110000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (54, 5, N'Đừng Để Tiền Ngủ Yên Trong Túi (Tái bản năm 2021)', 16, N'Images\9fa68676-56f1-4a68-860e-512b0c141ceb.jpg', N'Tương Lâm', 2018, 69000.0000, 94000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (55, 5, N'Donald Trump - Chiến Lược Đầu Tư Bất Động Sản (Tái bản năm 2021)', 27, N'Images\eeae3d96-9453-45ac-838a-d239a5715109.jpg', N'George H. RossAndrew James McLean', 2021, 69000.0000, 94000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (56, 5, N'Những Doanh Nhân Vĩ Đại - Bill Marriott - Những Quyết Định Lịch Sử Làm Nên Đế Chế Khách Sạn Thành Công Nhất Thế Giới', 85, N'Images\3927690c-1285-42c1-b8c3-4dec6b65aa33.jpg', N'Dale Van Atta', 2020, 139000.0000, 164000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (57, 5, N'Con Đường Tơ Lụa Mới - Hiện Tại và Tương Lai Của Thế Giới', 35, N'Images\f86e930c-84be-4043-a7bf-6328269830d6.jpg', N'Peter Frankopan', 2019, 159000.0000, 184000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (58, 5, N'Nghệ Thuật Quản Lý Nhân Sự (Tái bản năm 2021)', 96, N'Images\7aaf3b3a-cc2b-4047-a8cf-f43c0a662b6d.jpg', N'Lê Tiến Thành', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (59, 5, N'Người Giải Mã Thị Trường Tài Chính', 45, N'Images\cc066be9-aa0b-4732-b2d8-ab44ab007b14.jpg', N'Gregory Zuckerman', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (60, 5, N'Giàu Từ Chứng Khoán (Tái bản năm 2021)', 37, N'Images\6f0de74d-d258-4dfa-b49e-ed0eb87b005b.jpg', N'John Boik', 2015, 69000.0000, 94000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (61, 5, N'Chiến Lược Đầu Tư Chứng Khoán (Tái bản năm 2021)', 100, N'Images\d428f83d-8d1f-44d3-b6ff-34055e632be3.jpg', N'David BrownKassandra Bentley', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (62, 5, N'Đằng Sau Một Quyết Định Lớn', 69, N'Images\91bb751b-ee91-45f0-ba9b-23c8939d33f8.jpg', N'Joseph L. Badaracco Jr.', 2019, 179000.0000, 204000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (63, 6, N'Nhật Ký Tarot', 73, N'Images\70467389-918c-42b4-95ed-573c9f611030.jpg', N'Brigit Esselmont', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (64, 6, N'Tác Phẩm Kinh Điển Minh Họa Mới - Tiếng Gọi Của Hoang Dã', 22, N'Images\0ad62488-2810-479d-b588-494cf145109c.jpg', N'Jack London', 2017, 79000.0000, 104000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (65, 6, N'Điệp Vụ Gibson Vaughn - Giải Thoát', 91, N'Images\b0127982-71a3-4c93-b1aa-84a2b65f8e1d.jpg', N'Matthew Fitzsimmons', 2016, 85000.0000, 110000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (66, 6, N'Bức Tranh Dorian Gray (Tái bản năm 2021)', 16, N'Images\eb603b6d-f1c6-47d2-a6d0-3d352eba3abc.jpg', N'Oscar Wilde', 2018, 69000.0000, 94000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (67, 6, N'Lại Đây, Ôm Cái Nào!', 27, N'Images\b48073a0-96c9-49e7-b540-ef4eb1cdae4e.jpg', N'Tả Đồng', 2021, 69000.0000, 94000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (68, 6, N'Chóng Mặt', 85, N'Images\da8000df-3f08-4067-af53-32cf0da0e256.jpg', N'W. G. Sebald', 2020, 139000.0000, 164000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (69, 6, N'Sinh Năm 1972 - Khát Vọng Sống (Tự truyện Nguyễn Cảnh Bình)', 35, N'Images\73aaa46f-12ab-4a63-ba05-45ec9aac5d69.jpg', N'Nguyễn Cảnh Bình', 2019, 159000.0000, 184000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (70, 6, N'Về Nguyễn Huy Thiệp', 96, N'Images\b424ca40-9dd9-444b-9ea3-2c69a127edc4.jpg', N'Nhiều Tác Giả', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (71, 6, N'Hứa Với Con, Ba Nhé', 45, N'Images\05a755f2-5a2a-413e-abd3-c9b1cfe74c97.jpg', N'Joe Biden', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (72, 6, N'Sự Thật Ta Nắm Giữ - Một Hành Trình Xuyên Nước Mỹ', 37, N'Images\2417791b-420e-4549-b562-9986e0001a22.jpg', N'Joe Biden', 2015, 69000.0000, 94000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (73, 6, N'Nơi Ánh Sáng Chiếu Soi - Hành Trình Xây Dựng Gia Đình, Khám Phá Chính Mình', 100, N'Images\63ff09a2-e1c1-41f0-ace3-62bab8213ef8.jpg', N'Jill Biden', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (74, 6, N'Tuổi Trẻ Mà Tôi Đã Sống', 69, N'Images\e232b16e-9dcc-433f-bef7-a180fbf745f4.jpg', N'Nguyễn Ngọc Sơn', 2019, 179000.0000, 204000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (75, 7, N'Luật An Toàn Thông Tin Mạng', 4, N'Images\e5031b15-e917-499a-805b-392c9f802540.jpg', N'Luật Pháp Việt Nam', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (76, 8, N'Phạm Xuân Ẩn - Điệp Viên Hoàn Hảo X6', 3, N'Images\c9fc0bd6-2e13-42c5-be98-c491fd35f1ec.jpg', N'Larry Berman', 2017, 79000.0000, 104000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (77, 2, N'Học Khôn Ngoan Mà Không Gian Nan', 5, N'Images\6bc08252-e085-4f1b-9e09-1af00eda389a.jpg', N'muaban.net', 2023, 75000.0000, 125000.0000, CAST(N'2023-12-26T00:00:00.000' AS DateTime), N'')
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseDetail_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_PurchaseDetail_Product]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseDetail_Purchase] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([ID])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_PurchaseDetail_Purchase]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Voucher] FOREIGN KEY([VoucherID])
REFERENCES [dbo].[Voucher] ([ID])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Voucher]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_PurchaseStausEnum] FOREIGN KEY([Status])
REFERENCES [dbo].[OrderStatusEnum] ([Value])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Purchase_PurchaseStausEnum]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CatID])
REFERENCES [dbo].[Category] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
