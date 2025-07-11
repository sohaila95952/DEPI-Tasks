select count(*) as numofproducts from production.products;

select avg(list_price) as average_price,
min (list_price) as minimum_price,
MAX(list_price) as maximum_price
from production.products;

select count(*) as numofproducts,category_id from production.products group by category_id;

select count(*) as numoforders ,store_id from sales.orders group by store_id;

select top 10 upper(first_name) as firstupper, lower (last_name) as lastlower from sales.customers;

select top 10 product_name,len(product_name) as lengthofname from production.products;

select top 15 left(phone,3) as areacode from sales.customers;

select top 10 getdate() as currentdate , year(order_date) as year,month(order_date) as month from sales.orders;

select top 10 p.product_name,c.category_name from production.products p inner join production.categories c on p.category_id=c.category_id;

select top 10 c.first_name+' '+c.last_name as customername ,order_date from sales.customers c join sales.orders s on c.customer_id=s.customer_id;

select p.product_name,coalesce(b.brand_name,'No Brand') from production.products p left join production.brands b on p.brand_id=b.brand_id;

select product_name,list_price from production.products
where list_price> (select avg(list_price) from production.products);

select c.customer_id,c.first_name+' '+c.last_name as customername from sales.customers c 
where customer_id in (select customer_id from sales.orders);

select c.first_name+' '+c.last_name as customername ,
(select count(*) as totalorders from sales.orders o where c.customer_id=o.customer_id )
from sales.customers c;

create view easy_product_list as
select 
product_name,list_price, category_name
from production.products p join production.categories c on p.category_id=c.category_id;

select * from easy_product_list where list_price>100;

create view customer_info as
select customer_id,c.first_name+' '+c.last_name as customername,email,city+' ' +state as location 
from sales.customers c
select * from customer_info where location like '%CA';

select product_name,list_price from production.products where list_price between 50 and 200 order by list_price;

select state ,count(*) as numofcustomers from sales.customers group by state order by count(*) desc;

select p.product_name,c.category_id,category_name 
from production.products p join production.categories c on p.category_id=c.category_id 
where  p.list_price = (
    SELECT MAX(p2.list_price)
    FROM production.products p2
    WHERE p2.category_id = p.category_id
  );

select store_name,city ,count(o.order_id) as numoforders from sales.stores s join sales.orders o on s.store_id=o.store_id
group by store_name,city;
