--1
select list_price,
case 
   when list_price <300 then 'economy'
   when list_price between 300 and 999 then 'Standard'
   when list_price between 1000 and 2499 then 'Premium'
   when list_price >2499 then 'Luxury'
   else 'invalid'
end as price_categories
from production.products
order by list_price;

--2
select order_status,
case 
when order_status=1 then'Order Received'
when order_status=2 then'In Preparation'
when order_status=3 then'Order Cancelled'
when order_status=4 then'Order Delivered'
else 'invalid'
end as status,
case
when order_status=1 and DATEDIFF(DAY,order_date,GETDATE())>5 then'URGENT'
when order_status=2 and DATEDIFF(DAY,order_date,GETDATE())>3 then'HIGH'
else 'normal'
end as proirity
from sales.orders
order by order_status;

--3
select s.staff_id , count(o.order_id)as numoforders,
case
when count(order_id) =0 then 'New Staff'
when count(order_id) between 1 and 10 then 'Junior Staff'
when count(order_id) between 11 and 25 then 'Senior Staff'
when count(order_id) >25 then 'Expert Staff'
else 'invalid'
end as categorizedstaff
from sales.orders o
left join sales.staffs s on s.staff_id=o.staff_id
group by s.staff_id;

--4
select first_name+' '+last_name as fullname,
ISNULL(phone,'Phone Not Available') as phone
,email,street,city,state,zip_code,
coalesce(phone,email,'No Contact Method') as preferred_contact
from sales.customers;

--5
select list_price,isnull(quantity,0) as quantity,
 isnull(list_price/nullif(quantity,0),0) price,
case 
when isnull(quantity,0)=0 then 'Out of Stock'
when quantity between 1 and 10 then'Low Stock'
when quantity >10 then'High stock'
else 'invalid'
end as status
from production.stocks s
join production.products p on p.product_id=s.product_id
join sales.orders o on o.store_id=s.store_id
where s.store_id=1;

--6
select coalesce(street,city,state,zip_code,'no address') as address,
coalesce(street,'no street')+' ,'
+coalesce(city,'no city')
+' ,'+
+coalesce(state,'no state')
+' ,'+
coalesce(zip_code,'no zip') as formatted_address
from sales.customers;

--7
with total_for_customer (customer_id ,total_price) as 
(
select customer_id,sum(list_price*quantity) as total
from sales.order_items oi 
join sales.orders o on o.order_id=oi.order_id
group by customer_id

)
select c.customer_id,
  c.first_name,
  c.last_name,
  c.city,
  c.state,
  t.total_price
  from total_for_customer t
join sales.customers c on c.customer_id=t.customer_id
where t.total_price>1500
order by t.total_price desc
;

--8 diff
WITH TotalRevenuePerCategory AS (
    SELECT 
        c.category_id,
        c.category_name,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_revenue
    FROM sales.order_items oi
    JOIN production.products p ON oi.product_id = p.product_id
    JOIN production.categories c ON p.category_id = c.category_id
    GROUP BY c.category_id, c.category_name
),
AverageOrderValuePerCategory AS (
    SELECT 
        category_id,
        AVG(order_total) AS avg_order_value
    FROM (
        SELECT 
            o.order_id,
            c.category_id,
            SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS order_total
        FROM sales.orders o
        JOIN sales.order_items oi ON o.order_id = oi.order_id
        JOIN production.products p ON oi.product_id = p.product_id
        JOIN production.categories c ON p.category_id = c.category_id
        GROUP BY o.order_id, c.category_id
    ) AS category_orders
    GROUP BY category_id
)

SELECT 
    tr.category_id,
    tr.category_name,
    tr.total_revenue,
    aov.avg_order_value,
    CASE 
        WHEN tr.total_revenue > 50000 THEN 'Excellent'
        WHEN tr.total_revenue > 20000 THEN 'Good'
        ELSE 'Needs Improvement'
    END AS performance_rating
FROM TotalRevenuePerCategory tr
JOIN AverageOrderValuePerCategory aov ON tr.category_id = aov.category_id
ORDER BY tr.total_revenue DESC;

--9
with monthsales as(
select 
FORMAT(o.order_date, 'yyyy-MM') AS year_month,
SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_sales
from sales.orders o
join sales.order_items oi
on o.order_id=oi.order_id
group by FORMAT(o.order_date, 'yyyy-MM')
--order by FORMAT(o.order_date, 'yyyy-MM')
),
monthcomparison as (
SELECT 
year_month,total_sales,
LAG(total_sales) OVER (ORDER BY year_month) AS previous_month_sales,
total_sales - LAG(total_sales) OVER (ORDER BY year_month) AS difference
FROM monthsales
)
SELECT 
year_month,total_sales,previous_month_sales,
    ROUND(
        CASE 
            WHEN previous_month_sales IS NULL OR previous_month_sales = 0 THEN NULL
            ELSE ((total_sales - previous_month_sales) / previous_month_sales) * 100
        END, 2
    ) AS growth_percentage
FROM monthcomparison
ORDER BY year_month; 

--10
select * from (
select
c.category_id,c.category_name,p.product_id,p.list_price ,
    ROW_NUMBER() over(partition by c.category_id order by p.list_price  DESC) as ROW_NUM,
	Rank() over(partition by c.category_id order by p.list_price  DESC) as Rank,
    DENSE_RANK () over(partition by c.category_id order by p.list_price  DESC) as DENSE_RANK
    from production.products p 
    JOIN production.categories c ON p.category_id = c.category_id )
	as rankedproducts
    WHERE ROW_NUM<=3
	ORDER BY category_id, list_price DESC;

--11
WITH CustomerTotals AS (
    SELECT 
    c.customer_id,SUM(oi.list_price * oi.quantity * (1 - oi.discount)) AS total_for_customer
    FROM sales.customers c
    JOIN sales.orders o ON c.customer_id = o.customer_id
    JOIN sales.order_items oi ON o.order_id = oi.order_id
    GROUP BY c.customer_id
)

SELECT 
    customer_id,total_for_customer,
    RANK() OVER (ORDER BY total_for_customer DESC) AS rank,
	case
	when (select RANK() OVER (ORDER BY total_for_customer DESC))=1 then 'vip'
	else 'none'
	end
FROM CustomerTotals;

--12

WITH total_revenue AS (
 SELECT S.store_id,SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_revenue
 from SALES.ORDERS O
 JOIN SALES.STORES S ON O.store_id=S.store_id
 JOIN sales.order_items OI ON OI.order_id=O.order_id
 GROUP BY S.store_id
 ),  
 ORDERS_COUNT_PER_STORE AS (
 SELECT S.store_id,COUNT(O.order_id) AS ORDERSCOUNT
 from SALES.ORDERS O  JOIN SALES.STORES S ON O.store_id=S.store_id
 GROUP BY S.store_id
 )
 SELECT S.store_id, RANK()OVER(ORDER BY total_revenue ) AS RANKtotal_revenue ,
 RANK()OVER(ORDER BY ORDERSCOUNT ) AS RANKORDERSCOUNT,
 PERCENT_RANK() OVER(ORDER BY total_revenue ) AS P_RANK_TOTAL,
 PERCENT_RANK()OVER(ORDER BY ORDERSCOUNT ) AS P_RANK_COUNT
 FROM total_revenue TR
 JOIN ORDERS_COUNT_PER_STORE OS 
 ON TR.store_id=OS.store_id
 JOIN sales.stores S ON S.store_id=TR.store_id
 ;

 --13
 --SELECT DISTINCT brand_name FROM production.brands ;
 SELECT * FROM
 (
 SELECT P.product_id ,
 B.brand_name,C.category_name
 FROM production.products P
 JOIN production.brands B ON B.brand_id=P.brand_id
 JOIN production.categories C ON C.category_id=P.category_id
 ) MAIN
 PIVOT (
 COUNT(product_id)
 FOR brand_name
 IN ([Adidas], [Gap], [Nike], [Theory])
 ) AS PIV

 --14
SELECT * ,
ISNULL([January], 0) + ISNULL([February], 0) + ISNULL([March], 0) +
       ISNULL([April], 0) + ISNULL([May], 0) + ISNULL([June], 0) +
       ISNULL([July], 0) + ISNULL([August], 0) + ISNULL([September], 0) +
       ISNULL([October], 0) + ISNULL([November], 0) + ISNULL([December], 0)
       AS Total_Annual_Sales
FROM(
 select 
DATENAME(MONTH,order_date) AS year_month,O.store_id,
SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_sales
from sales.orders o
join sales.order_items oi on o.order_id=oi.order_id
JOIN sales.stores S ON S.store_id=O.store_id
group by DATENAME(MONTH,order_date)
,O.store_id
) MAIN
PIVOT (
SUM(total_sales) 
FOR year_month
IN ( [January], [February], [March], [April], [May], [June], 
    [July], [August], [September], [October], [November], [December])
) PIV 
ORDER BY store_id

--15
SELECT * FROM (
SELECT S.store_id,S.store_name,order_id
,CASE
WHEN O.order_status=1 THEN 'Pending'
WHEN O.order_status=2 THEN 'Processing'
WHEN O.order_status=3 THEN 'Completed'
WHEN O.order_status=4 THEN 'Rejected'
END AS STATUS
FROM sales.orders o
JOIN sales.stores S ON S.store_id=O.store_id
GROUP BY O.order_status,S.store_id,order_id,S.store_name )
MAIN
PIVOT (
COUNT(order_id)
FOR STATUS IN ([Pending],[Processing],[Completed],[Rejected])
) PIV
ORDER BY store_id

--16
--SELECT YEAR(order_date) FROM sales.orders
SELECT * FROM (
SELECT B.brand_name ,YEAR(order_date) AS YEARS,
SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_sales
FROM production.brands B
JOIN production.products P
ON P.brand_id=B.brand_id
JOIN sales.order_items OI
ON OI.product_id=P.product_id
JOIN SALES.ORDERS O ON O.order_id=OI.order_id
GROUP BY YEAR(order_date),B.brand_name ) MAIN
PIVOT (
SUM (total_sales)
FOR YEARS
IN ([2022],[2023],[2024])
) PIV

--17
SELECT quantity,product_id,
CASE 
WHEN quantity>0 THEN 'In-stock'
ELSE 'NONE'
END AS STATUS
FROM production.stocks
WHERE quantity>0
UNION
SELECT quantity,product_id,
CASE 
WHEN quantity=0 OR quantity IS NULL THEN 'Out-of-stock'
ELSE 'NONE'
END AS STATUS
FROM production.stocks
WHERE quantity=0 OR quantity IS NULL
UNION
SELECT quantity,P.product_id ,
'Discontinued' AS STATUS
FROM production.products P
LEFT JOIN production.stocks S ON S.product_id =P.product_id
WHERE S.product_id  IS NULL

--SELECT COUNT(*) 
--FROM production.products P
--LEFT JOIN production.stocks S ON S.product_id = P.product_id
--WHERE S.product_id IS NULL;

--18                     -> مفيش منتجات موجوده في 1 ومش موجوده في 2
SELECT customer_id ,YEAR(order_date) AS YEAR,COUNT(o.ORDER_ID) AS NUMOFORDERS
,SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_spent
FROM sales.orders o
join sales.order_items oi on o.order_id=oi.order_id
WHERE customer_id IN
(
SELECT C.customer_id
FROM sales.customers C
JOIN sales.orders O ON O.customer_id=C.customer_id
WHERE YEAR(order_date)=2023

INTERSECT

SELECT C.customer_id
FROM sales.customers C
JOIN sales.orders O ON O.customer_id=C.customer_id
WHERE YEAR(order_date)=2024 
)  
AND YEAR(order_date) IN (2023, 2024) 
GROUP BY  customer_id ,YEAR(order_date) 
ORDER BY customer_id, YEAR(order_date);

--19
select DISTINCT product_id,'Available in all 3 stores' AS status
from production.stocks
where store_id=1
intersect 
select DISTINCT product_id,'Available in all 3 stores' AS status
from production.stocks
where store_id=2
intersect 
select DISTINCT product_id,'Available in all 3 stores' AS status
from production.stocks
where store_id=3
union
SELECT DISTINCT product_id, 'Only in store 1' AS status
FROM production.stocks
WHERE store_id = 1
except
select DISTINCT product_id, 'Only in store 1' AS status
from production.stocks
where  store_id=2

--20
(SELECT C.customer_id, 'retained customers' as status
FROM sales.customers C
JOIN sales.orders O ON O.customer_id=C.customer_id
WHERE YEAR(order_date)=2023

INTERSECT

SELECT C.customer_id,'retained customers' as status
FROM sales.customers C
JOIN sales.orders O ON O.customer_id=C.customer_id
WHERE YEAR(order_date)=2024 
)
union (
SELECT C.customer_id ,'lost customers' as status
FROM sales.customers C
JOIN sales.orders O ON O.customer_id=C.customer_id
WHERE YEAR(order_date)=2023

except

SELECT C.customer_id ,'lost customers' as status
FROM sales.customers C
JOIN sales.orders O ON O.customer_id=C.customer_id
WHERE YEAR(order_date)=2024 

)
union (
SELECT C.customer_id ,'new customers' as status
FROM sales.customers C
JOIN sales.orders O ON O.customer_id=C.customer_id
WHERE YEAR(order_date)=2024

except

SELECT C.customer_id ,'new customers' as status
FROM sales.customers C
JOIN sales.orders O ON O.customer_id=C.customer_id
WHERE YEAR(order_date)=2023 
)