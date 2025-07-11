select * from production.products where list_price > 1000;
select * from sales.customers where state='CA' or state= 'NY';
select * from sales.orders where year(order_date)=2023;
select * from sales.customers where email like '%gmail.com';
select * from sales.staffs where active=1;
select top 5 * from  production.products order by list_price desc;
select top 10 * from  sales.orders order by order_date desc;
select top 3 * from sales.customers order by last_name;
select * from sales.customers where phone is null;
select * from sales.staffs where manager_id is not null;
select count (*) as numproducts,category_id from production.products group by category_id;
select count (*) as numcustomers ,state from sales.customers group by state;
select avg(list_price) as average ,brand_id from production.products group by brand_id;
select count(*) numstaffs ,staff_id from sales.orders group by staff_id;
select customer_id , count (order_id) numoforders from sales.orders  group by customer_id having count(order_id)>2;
select * from production.products where list_price between 500 and 1500;
select * from sales.customers where city like 'S%';
select * from sales.orders where order_status =2 or  order_status =4;
select * from production.products where category_id in (1,2,3);
select * from sales.staffs where staff_id=1 or phone is null;