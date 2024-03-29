USE [BookShop2023]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 12/25/2023 11:54:01 PM ******/
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
/****** Object:  Table [dbo].[Category]    Script Date: 12/25/2023 11:54:01 PM ******/
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
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 12/25/2023 11:54:01 PM ******/
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
/****** Object:  Table [dbo].[Orders]    Script Date: 12/25/2023 11:54:01 PM ******/
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
	[CustomerName] [nvarchar](100) NULL,
	[CustomerAddress] [nvarchar](255) NULL,
 CONSTRAINT [PK_Purchase] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStatusEnum]    Script Date: 12/25/2023 11:54:01 PM ******/
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
/****** Object:  Table [dbo].[Product]    Script Date: 12/25/2023 11:54:01 PM ******/
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
/****** Object:  Table [dbo].[Voucher]    Script Date: 12/25/2023 11:54:01 PM ******/
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

INSERT [dbo].[Category] ([ID], [Name]) VALUES (9, N'Tâm Lý Học')
INSERT [dbo].[Category] ([ID], [Name]) VALUES (10, N'Giáo Dục')
INSERT [dbo].[Category] ([ID], [Name]) VALUES (11, N'Kỹ Năng Sống')
INSERT [dbo].[Category] ([ID], [Name]) VALUES (12, N'Khoa Học - Công Nghệ')
INSERT [dbo].[Category] ([ID], [Name]) VALUES (13, N'Kinh Tế - Kinh Doanh')
INSERT [dbo].[Category] ([ID], [Name]) VALUES (14, N'Văn Học')
INSERT [dbo].[Category] ([ID], [Name]) VALUES (15, N'Luật')
INSERT [dbo].[Category] ([ID], [Name]) VALUES (16, N'Lịch Sử')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
INSERT [dbo].[OrderStatusEnum] ([Key], [Value], [Description], [DisplayText]) VALUES (N'New', 1, N'Đơn hàng mới được tạo', N'Mới tạo')
INSERT [dbo].[OrderStatusEnum] ([Key], [Value], [Description], [DisplayText]) VALUES (N'Completed', 2, N'Khách hàng đã thanh toán', N'Đã thanh toán')
INSERT [dbo].[OrderStatusEnum] ([Key], [Value], [Description], [DisplayText]) VALUES (N'Cancelled', 3, N'Đơn hàng đã hủy', N'Đã hủy')
INSERT [dbo].[OrderStatusEnum] ([Key], [Value], [Description], [DisplayText]) VALUES (N'Shipping', 4, N'Đã thanh toán và đã đã giao', N'Đã giao')
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (2, 9, N'Tâm Lý Học Đời Sống', 11, N'01.jpg', N'Sigmund Freud', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (3, 9, N'Tâm Lý Học Nỗi Đau', 22, N'02.jpg', N'Patrick Wall', 2019, 179000.0000, 204000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (4, 9, N'Tâm Lý Học Tội Phạm', 6, N'03.jpg', N'Hans Gross', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (5, 9, N'Tâm Lý Học Ứng Dụng', 7, N'04.jpg', N'Patrick King', 2017, 79000.0000, 104000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (6, 9, N'Tâm Lý Học Đại Cương', 9, N'05.jpg', N'Nguyễn Quang Uân', 2016, 85000.0000, 110000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (7, 9, N'Cân Bằng Cảm Xúc Cả Lúc Bão Giông', 13, N'06.jpg', N'Anonymous', 2018, 69000.0000, 94000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (8, 9, N'Người Nhạy Cảm - Món Quà Hay Lời Nguyền', 21, N'07.jpg', N'Anonymous', 2021, 69000.0000, 94000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (9, 9, N'Chữa Lành Bằng Tâm Thức (Tái bản năm 2021)', 17, N'08.jpg', N'Newton Kondaveti', 2020, 139000.0000, 164000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (10, 9, N'Lo Âu Xã Hội', 19, N'09.jpg', N'David Shanley', 2019, 159000.0000, 184000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (11, 9, N'Giải Mã Từ Điển Cảm Xúc', 53, N'10.jpg', N'Kim Eana', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (12, 9, N'Tâm Lý Học - Tất Cả Những Điều Cần Biết Để Thông Thạo Bộ Môn Này', 16, N'11.jpg', N'Alan Porter', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (13, 9, N'Tâm Lý Học Tội Phạm - Phác Họa Chân Dung Kẻ Phạm Tội', 28, N'12.jpg', N'Diệp Hồng Vũ', 2015, 69000.0000, 94000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (14, 10, N'Mĩ Thuật Lớp 1', 100, N'13.jpg', N'Nhà xuất bản Việt Nam', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (15, 10, N'Bổ trợ và Nâng cao Toán 7', 69, N'14.jpg', N'Nhà xuất bản Việt Nam', 2019, 179000.0000, 204000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (16, 10, N'Bài tập Tiếng Anh 9', 73, N'15.jpg', N'Nhà xuất bản Việt Nam', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (17, 10, N'Câu Chuyện Giáo Dục - Tài Trí', 22, N'16.jpg', N'Hải Nam', 2017, 79000.0000, 104000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (18, 10, N'Câu Chuyện Giáo Dục - Nhân Hậu', 91, N'17.jpg', N'Hải Nam', 2016, 85000.0000, 110000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (19, 10, N'Câu Chuyện Giáo Dục - Kiên Trì', 16, N'18.jpg', N'Hải Nam', 2018, 69000.0000, 94000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (20, 10, N'Câu Chuyện Giáo Dục - Dũng Cảm', 27, N'19.jpg', N'Hải Nam', 2021, 69000.0000, 94000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (21, 10, N'Câu Chuyện Giáo Dục - Đạo Đức', 85, N'20.jpg', N'Hải Nam', 2020, 139000.0000, 164000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (22, 10, N'Con Đường Hạnh Phúc', 35, N'21.jpg', N'Thiền sư Gelong Thubten', 2019, 159000.0000, 184000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (23, 10, N'Khi Bạn Đang Mơ Thì Người Khác Đang Nỗ Lực', 96, N'22.jpg', N'Vĩ Nhân', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (24, 10, N'Làm Ít Được Nhiều', 45, N'23.jpg', N'Chin-Ning Chu', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (25, 10, N'Bài Học Vô Giá Từ Những Điều Bình Dị ( Tái Bản )', 37, N'24.jpg', N'Francis Xavier', 2015, 69000.0000, 94000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (26, 11, N'Đắc Nhân Tâm', 100, N'25.jpg', N'Dale Camegie', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (27, 11, N'Đánh Thức Con Người Bên Trong Bạn', 69, N'26.jpg', N'Tony Robbins', 2019, 179000.0000, 204000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (28, 11, N'Mindset', 73, N'27.jpg', N'Carol S.Dweck', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (29, 11, N'Tư Duy Phản Biện', 22, N'28.jpg', N'Richard Paul and Linda Elder', 2017, 79000.0000, 104000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (30, 11, N'Tôi Tài Giỏi Bạn Cũng Thế', 91, N'29.jpg', N'Anonymous', 2016, 85000.0000, 110000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (31, 11, N'Học Khôn Ngoan Mà Không Gian Nan', 16, N'30.jpg', N'Kevin Pauth', 2018, 69000.0000, 94000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (32, 11, N'Tư Duy Sâu', 27, N'31.jpg', N'Diệp Tu', 2021, 69000.0000, 94000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (33, 11, N'Rèn Luyện Tư Duy Phàn Biện', 85, N'32.jpg', N'Albert RutherFold', 2020, 139000.0000, 164000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (34, 11, N'Đi Tìm Lẽ Sống', 35, N'33.jpg', N'Victor E.Frankl', 2019, 159000.0000, 184000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (35, 11, N'Hiểu Về Trái Tim', 96, N'34.jpg', N'Minh Niệm', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (36, 11, N'What I wish I knew when I was 20', 45, N'35.jpg', N'Tina Seelig ', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (37, 11, N'Chuyện Con Mèo Dạy Hải Âu Bay', 37, N'36.jpg', N'Luis Sepul Veda', 2015, 69000.0000, 94000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (38, 12, N'How Psychology Works', 100, N'37.jpg', N'Jo Hemmings', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (39, 12, N'Object Oriented Programming', 69, N'38.jpg', N'HCMUS', 2019, 179000.0000, 204000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (40, 12, N'Why We Sleep ?', 73, N'39.jpg', N'Matthew Walker', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (41, 12, N'Python for Data Analysis ', 22, N'40.jpg', N'Bephein John', 2022, 79000.0000, 104000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (42, 12, N'How to improve your memory for study', 91, N'41.jpg', N'Jonathan HanCock', 2016, 85000.0000, 110000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (43, 12, N'Business Analysis ', 8, N'42.jpg', N'Dummies', 2021, 69000.0000, 94000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (44, 12, N'12 Xu Hướng Công Nghệ Trong Thời Đại 4.0', 11, N'43.jpg', N'Kevin Kelly', 2021, 69000.0000, 94000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (45, 12, N'Khoa Học Khám Phá - Dữ Liệu Lớn', 8, N'44.jpg', N'Nhiều Tác Giả', 2021, 139000.0000, 164000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (46, 12, N'Công Nghiệp Tương Lai', 7, N'45.jpg', N'Alec Ross', 2022, 159000.0000, 184000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (47, 12, N'Deep Learning - Cuộc Cách Mạng Học Sâu', 4, N'46.jpg', N'Terrence J.Sejnowski', 2023, 99000.0000, 124000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (48, 12, N'Nghệ Thuật Ẩn Mình', 45, N'47.jpg', N'Kelvin Mitnick', 2021, 89000.0000, 114000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (49, 12, N'Gián Điệp Mạng', 2, N'48.jpg', N'Clifford Stoll', 2021, 69000.0000, 94000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (50, 12, N'Hacker Lược Sử', 4, N'49.jpg', N'Steven Levy', 2022, 99000.0000, 124000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (51, 12, N'LIFE 3.0 - Loài Người Trong Kỷ Nguyên Trí Tuệ Nhân Tạo', 3, N'50.jpg', N'Max Tegmark', 2023, 179000.0000, 204000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (52, 13, N'Nhà Lãnh Đạo Tương Lai', 73, N'51.jpg', N'Jacob Morgan', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (53, 13, N'Nghệ Thuật Viết Quảng Cáo', 22, N'52.jpg', N'Victor O. Schwab', 2017, 79000.0000, 104000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (54, 13, N'Những Sát Thủ Hàng Loạt Trong Giới Tài Chính', 91, N'53.jpg', N'Bruce KellyTom Ajamie', 2016, 85000.0000, 110000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (55, 13, N'Đừng Để Tiền Ngủ Yên Trong Túi (Tái bản năm 2021)', 16, N'54.jpg', N'Tương Lâm', 2018, 69000.0000, 94000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (56, 13, N'Donald Trump - Chiến Lược Đầu Tư Bất Động Sản (Tái bản năm 2021)', 27, N'55.jpg', N'George H. RossAndrew James McLean', 2021, 69000.0000, 94000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (57, 13, N'Những Doanh Nhân Vĩ Đại - Bill Marriott - Những Quyết Định Lịch Sử Làm Nên Đế Chế Khách Sạn Thành Công Nhất Thế Giới', 85, N'56.jpg', N'Dale Van Atta', 2020, 139000.0000, 164000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (58, 13, N'Con Đường Tơ Lụa Mới - Hiện Tại và Tương Lai Của Thế Giới', 35, N'57.jpg', N'Peter Frankopan', 2019, 159000.0000, 184000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (59, 13, N'Nghệ Thuật Quản Lý Nhân Sự (Tái bản năm 2021)', 96, N'58.jpg', N'Lê Tiến Thành', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (60, 13, N'Người Giải Mã Thị Trường Tài Chính', 45, N'59.jpg', N'Gregory Zuckerman', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (61, 13, N'Giàu Từ Chứng Khoán (Tái bản năm 2021)', 37, N'60.jpg', N'John Boik', 2015, 69000.0000, 94000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (62, 13, N'Chiến Lược Đầu Tư Chứng Khoán (Tái bản năm 2021)', 100, N'61.jpg', N'David BrownKassandra Bentley', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (63, 13, N'Đằng Sau Một Quyết Định Lớn', 69, N'62.jpg', N'Joseph L. Badaracco Jr.', 2019, 179000.0000, 204000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (64, 14, N'Nhật Ký Tarot', 73, N'63.jpg', N'Brigit Esselmont', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (65, 14, N'Tác Phẩm Kinh Điển Minh Họa Mới - Tiếng Gọi Của Hoang Dã', 22, N'64.jpg', N'Jack London', 2017, 79000.0000, 104000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (66, 14, N'Điệp Vụ Gibson Vaughn - Giải Thoát', 91, N'65.jpg', N'Matthew Fitzsimmons', 2016, 85000.0000, 110000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (67, 14, N'Bức Tranh Dorian Gray (Tái bản năm 2021)', 16, N'66.jpg', N'Oscar Wilde', 2018, 69000.0000, 94000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (68, 14, N'Lại Đây, Ôm Cái Nào!', 27, N'67.jpg', N'Tả Đồng', 2021, 69000.0000, 94000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (69, 14, N'Chóng Mặt', 85, N'68.jpg', N'W. G. Sebald', 2020, 139000.0000, 164000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (70, 14, N'Sinh Năm 1972 - Khát Vọng Sống (Tự truyện Nguyễn Cảnh Bình)', 35, N'69.jpg', N'Nguyễn Cảnh Bình', 2019, 159000.0000, 184000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (71, 14, N'Về Nguyễn Huy Thiệp', 96, N'70.jpg', N'Nhiều Tác Giả', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (72, 14, N'Hứa Với Con, Ba Nhé', 45, N'71.jpg', N'Joe Biden', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (73, 14, N'Sự Thật Ta Nắm Giữ - Một Hành Trình Xuyên Nước Mỹ', 37, N'72.jpg', N'Joe Biden', 2015, 69000.0000, 94000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (74, 14, N'Nơi Ánh Sáng Chiếu Soi - Hành Trình Xây Dựng Gia Đình, Khám Phá Chính Mình', 100, N'73.jpg', N'Jill Biden', 2020, 99000.0000, 124000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (75, 14, N'Tuổi Trẻ Mà Tôi Đã Sống', 69, N'74.jpg', N'Nguyễn Ngọc Sơn', 2019, 179000.0000, 204000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (76, 15, N'Luật An Toàn Thông Tin Mạng', 4, N'75.jpg', N'Luật Pháp Việt Nam', 2018, 89000.0000, 114000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[Product] ([ID], [CatID], [Name], [Quantity], [ImagePath], [Author], [PublishedYear], [PurchasePrice], [SellingPrice], [UploadDate], [Description]) VALUES (77, 16, N'Phạm Xuân Ẩn - Điệp Viên Hoàn Hảo X6', 3, N'76.jpg', N'Larry Berman', 2017, 79000.0000, 104000.0000, CAST(N'2023-12-16T00:00:00.000' AS DateTime), N'')
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
