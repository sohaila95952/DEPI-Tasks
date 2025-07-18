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

 --6
 declare @batchsize int=3
 declare @updated int=0
 declare @total_needed_to_update int=0

 select @total_needed_to_update=quantity
 from production.stocks
 where quantity>5
 
 while (@updated<@total_needed_to_update)
 begin
 update top()
 end