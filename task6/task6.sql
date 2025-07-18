--1
declare @SPENT int 
select @spent=sum(oi.quantity*oi.list_price*(1-oi.discount))
from sales.customers c
join sales.orders o on o.customer_id=c.customer_id
join sales.order_items oi on oi.order_id=o.order_id
where c.customer_id=1
select @spent as totalspent
if @SPENT >5000
begin
print 'vip'
end
else begin
print'regular customer'
 end;
 
 --2
 declare @THRESHOLD int = 1500
 declare @orders_count int 
 select @orders_count= count(*)
 from (
 select o.order_id
 from production.products p
 join sales.order_items oi on oi.product_id=p.product_id
 join sales.orders o on o.order_id=oi.order_id
 group by o.order_id
 having sum(oi.quantity*oi.list_price*(1-oi.discount))>@THRESHOLD
 ) filtered
 print concat('number of orders having THRESHOLD > ',@THRESHOLD,' is :', @orders_count);

 --3
declare @staff_ID int =2 
declare @year int =2024 
declare @total decimal(10,2)  

 select @total=sum(oi.quantity*oi.list_price*(1-oi.discount))
 from sales.staffs s 
 join sales.orders o on o.staff_id=s.staff_id
 join sales.order_items oi on oi.order_id=o.order_id
 where s.staff_id=@staff_ID and year(order_date)=@year
 print concat('the total sales for staff member ID ',@staff_ID,' in year ',@year,' is : ',@total);

 --4
 print 'my server name is : ' +cast (@@SERVERNAME as nvarchar(255)) +
 '
 my version is : ' +cast (@@VERSION as nvarchar(255))  +
 '
 and the number of rows affected by the last statement is : '+cast (@@ROWCOUNT as nvarchar(255))  ;

 --5
 declare @quantity int
 select @quantity=quantity
 from production.stocks
 where product_id=1 and store_id=1;
 if @quantity>20
 begin
 print ('Well stocked')
 end
 if @quantity >=10 and @quantity<=20
 begin
 print ('Moderate stock')
 end
 else
 begin
 print ('Low stock - reorder needed')
 end

 --6    no rows has <5
 declare @batchsize int=3
 declare @updated int=0
 declare @total_needed_to_update int=0

 select @total_needed_to_update=count(*)
 from production.stocks s
 where quantity<10
 
 while (@updated<@total_needed_to_update)
 begin
 update top(@batchsize) production.stocks
 set quantity += 10
 where quantity<10;
 set @updated +=@batchsize
 print ('updated '++cast (@updated as nvarchar(255)) +'out of '++cast (@total_needed_to_update as nvarchar(255)))
 end

 --7
 select c.customer_id,
 sum(oi.quantity*oi.list_price*(1-oi.discount)) as total_for_customer,
 case 
 when sum(oi.quantity*oi.list_price*(1-oi.discount)) <300 then 'Budget'
 when sum(oi.quantity*oi.list_price*(1-oi.discount)) between 300 and 800 then 'Mid-Range'
 when sum(oi.quantity*oi.list_price*(1-oi.discount)) between 801 and 2000 then 'Premium'
 when sum(oi.quantity*oi.list_price*(1-oi.discount)) >2000 then 'Luxury'
 end as status
 from 
 sales.customers c
 join sales.orders o on o.customer_id=c.customer_id
 join sales.order_items oi on oi.order_id=o.order_id
 group by c.customer_id

 --8
 declare @customerid int=5
 declare @order_count int =0
 if exists (select customer_id from sales.customers where customer_id= @customerid)
 begin
 select @order_count=count(order_id)
 from sales.orders 
 where customer_id=@customerid
 print ('Customer Exist, order count: '+cast(@order_count as varchar(255)))
 end
 else
 begin
 print 'this customer has no orders'
 end

 --9
 
 create function Calculate_Shipping(@total_For_order decimal(10,2))
 returns decimal(10,2)
 as
 begin
 declare @ShippingCost decimal(10,2)
 if @total_For_order>100 
 set @ShippingCost=0
 if @total_For_order between 50 and 99
 set @ShippingCost=5.99
 else
 set @ShippingCost=12.99
 return @ShippingCost
 end
 select dbo.Calculate_Shipping(55) as ShippingCost;

 --10
 create function GetProducts_ByPriceRange(@min int,@max int)
 returns table 
 as
 return
 (
 select *
 from production.products p
 where list_price>@min and list_price<@max
 )
select * from dbo.GetProducts_ByPriceRange(100,200)

--11
CREATE FUNCTION GetCustomerYearlySummary(@customer_id INT)
RETURNS @summary TABLE
(
 OrderYear INT,OrderCount INT,
 TotalAmount DECIMAL(10,2),
 AvgOrderValue DECIMAL(10,2)
)
AS
BEGIN
INSERT INTO @summary
SELECT
YEAR(o.order_date) as OrderYear,
COUNT(*) as OrderCount,
SUM(oi.quantity * oi.list_price * (1 - oi.discount)) as TotalAmount,
AVG(oi.quantity * oi.list_price * (1 - oi.discount)) as AvgOrderValue
FROM sales.orders o
JOIN sales.order_items oi ON o.order_id = oi.order_id
WHERE o.customer_id = @customer_id
GROUP BY YEAR(o.order_date);
RETURN;
END;
SELECT * FROM dbo.GetCustomerYearlySummary(1);

--12
create function Calculate_Bulk_Discount(@quantity int)
returns int
as
begin
declare @discount int
if @quantity in (1,2)
set @discount=0
else if @quantity in (3,4,5)
set @discount=5
else if @quantity in (6,7,8,9)
set @discount=10
else
set @discount=15
return @discount
end;
select dbo.Calculate_Bulk_Discount(4) as discount;

--13
CREATE PROCEDURE sales.sp_GetCustomerOrderHistory
    @customer_id INT,
    @start_date DATE = NULL,
    @end_date DATE = NULL
AS
BEGIN
SELECT
o.order_id,
o.order_date,
o.order_status,
SUM(oi.quantity * oi.list_price * (1 - oi.discount)) as order_total
FROM sales.orders o
JOIN sales.order_items oi ON o.order_id = oi.order_id
WHERE o.customer_id = @customer_id
AND (@start_date IS NULL OR o.order_date >= @start_date)
AND (@end_date IS NULL OR o.order_date <= @end_date)
GROUP BY o.order_id, o.order_date, o.order_status
ORDER BY o.order_date DESC;
END;
exec sales.sp_GetCustomerOrderHistory 1

--14
create proc sp_Restock
@storeId int,
@productId int,
@fixStockQuntity int
as
begin
    if @fixStockQuntity <= 0
       begin
       print 'enter valid stock num'
       return
       end
    if not exists (select 1 from production.stocks where store_id = @storeId and product_id = @productId)
    begin 
        print 'product not exist in the store'
        return
    end
    if (select quantity from production.stocks where store_id = @storeId and product_id = @productId) < @fixStockQuntity
    begin
        print 'u cant fix this stock'
        return
    end
    update production.stocks
    set quantity = quantity - @fixStockQuntity
    where store_id = @storeId and product_id = @productId
end
exec sp_Restock 1, 1, 20
select * from production.stocks where store_id=1 AND product_id = 1


    --15 
	CREATE PROC sp_Process_New_Order 
    @CustomerID INT,
    @ProductID INT,
    @Quantity INT,
    @StoreID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @OrderID INT;
    DECLARE @ListPrice DECIMAL(10,2);
    DECLARE @Discount DECIMAL(4,2) = 0;
    DECLARE @StaffID INT;

    BEGIN TRY
        BEGIN TRANSACTION;
        SELECT TOP 1 @StaffID = staff_id 
        FROM sales.staffs 
        WHERE store_id = @StoreID;
        IF @StaffID IS NULL
        BEGIN
            RAISERROR('No staff found for this store', 16, 1);
            ROLLBACK;
            RETURN;
        END
        IF NOT EXISTS (
            SELECT 1 
            FROM production.stocks 
            WHERE product_id = @ProductID 
              AND store_id = @StoreID 
              AND quantity >= @Quantity
        )
        BEGIN
            RAISERROR('Not enough stock', 16, 1);
            ROLLBACK;
            RETURN;
        END
        INSERT INTO sales.orders (customer_id, order_date, required_date, store_id, order_status, staff_id)
        VALUES (@CustomerID, GETDATE(), DATEADD(DAY, 7, GETDATE()), @StoreID, 1, @StaffID);
        SET @OrderID = SCOPE_IDENTITY();
        INSERT INTO sales.order_items (order_id, item_id, product_id, quantity, list_price, discount)
        SELECT @OrderID, 1, @ProductID, @Quantity, list_price, 0
        FROM production.products
        WHERE product_id = @ProductID;
        UPDATE production.stocks
        SET quantity = quantity - @Quantity
        WHERE product_id = @ProductID AND store_id = @StoreID;

        COMMIT;
        PRINT 'Order added successfully.';
    END TRY
    BEGIN CATCH
        ROLLBACK;
        PRINT 'Something went wrong: ' + ERROR_MESSAGE();
    END CATCH
END;
EXEC sp_Process_New_Order 
    @CustomerID = 1,
    @ProductID = 101,
    @Quantity = 2,
    @StoreID = 1;

--16
   
    CREATE PROCEDURE sp_Search_Products1
    @ProductName VARCHAR(100) = NULL,
    @CategoryID INT = NULL,
    @MinPrice DECIMAL(10,2) = NULL,
    @MaxPrice DECIMAL(10,2) = NULL,
    @SortColumn VARCHAR(50) = NULL
AS
BEGIN
    DECLARE @SQL NVARCHAR(MAX);
    DECLARE @WhereClause NVARCHAR(MAX) = '';
    DECLARE @OrderClause NVARCHAR(MAX) = '';
    DECLARE @Params NVARCHAR(MAX);

    SET @SQL = 'SELECT product_id, product_name, category_id, list_price 
                FROM production.products WHERE 1=1';

    IF @CategoryID IS NOT NULL
        SET @WhereClause += ' AND category_id = @CategoryID';

    IF @MinPrice IS NOT NULL
        SET @WhereClause += ' AND list_price >= @MinPrice';

    IF @MaxPrice IS NOT NULL
        SET @WhereClause += ' AND list_price <= @MaxPrice';

    IF @ProductName IS NOT NULL
        SET @WhereClause += ' AND product_name LIKE ''%' + @ProductName + '%''';

    IF @SortColumn IS NOT NULL
        SET @OrderClause = ' ORDER BY ' + QUOTENAME(@SortColumn);

    SET @SQL += @WhereClause + @OrderClause;

    SET @Params = N'@CategoryID INT, @MinPrice DECIMAL(10,2), @MaxPrice DECIMAL(10,2)';

    EXEC sp_executesql @SQL, @Params,
        @CategoryID = @CategoryID,
        @MinPrice = @MinPrice,
        @MaxPrice = @MaxPrice;
END

    EXEC sp_Search_Products1
    @CategoryID = 1,
    @MinPrice = 100,
    @MaxPrice = 500,
    @ProductName = 'Purple Label Custom Fit French Cuff Shirt - White',
    @SortColumn = 'list_price';

	--i tried hardly to do my best but its too difficult