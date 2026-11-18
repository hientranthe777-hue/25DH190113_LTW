USE [MyStore];
GO

-- 1. Bổ sung các cột phương thức giao hàng, thanh toán và địa chỉ vào bảng Order
IF COL_LENGTH('dbo.Order', 'PaymentMethod') IS NULL
    ALTER TABLE [dbo].[Order] ADD [PaymentMethod] NVARCHAR(MAX) NULL;

IF COL_LENGTH('dbo.Order', 'DeliveryMethod') IS NULL
    ALTER TABLE [dbo].[Order] ADD [DeliveryMethod] NVARCHAR(MAX) NULL;

IF COL_LENGTH('dbo.Order', 'ShippingMethod') IS NULL
    ALTER TABLE [dbo].[Order] ADD [ShippingMethod] NVARCHAR(MAX) NULL;

IF COL_LENGTH('dbo.Order', 'ShippingAddress') IS NULL
    ALTER TABLE [dbo].[Order] ADD [ShippingAddress] NVARCHAR(MAX) NULL;
GO

-- 2. Bổ sung cột tự động tính thành tiền TotalPrice = Quantity * UnitPrice vào bảng OrderDetail
IF COL_LENGTH('dbo.OrderDetail', 'TotalPrice') IS NULL
    ALTER TABLE [dbo].[OrderDetail] ADD [TotalPrice] AS ([Quantity] * [UnitPrice]) PERSISTED;
GO