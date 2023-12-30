USE [BookShop2023]
GO
INSERT [dbo].[OrderStatusEnum] ([Key], [Value], [Description], [DisplayText]) VALUES (N'New', 1, N'Đơn hàng mới được tạo', N'Mới tạo')
INSERT [dbo].[OrderStatusEnum] ([Key], [Value], [Description], [DisplayText]) VALUES (N'Completed', 2, N'Khách hàng đã thanh toán', N'Đã thanh toán')
INSERT [dbo].[OrderStatusEnum] ([Key], [Value], [Description], [DisplayText]) VALUES (N'Cancelled', 3, N'Đơn hàng đã hủy', N'Đã hủy')
INSERT [dbo].[OrderStatusEnum] ([Key], [Value], [Description], [DisplayText]) VALUES (N'Shipping', 4, N'Đã thanh toán và đã đã giao', N'Đã giao')
GO
