--1.1
select p.BusinessEntityID,FirstName,LastName, HireDate
from Person.Person p
join HumanResources.Employee e 
on e.BusinessEntityID=p.BusinessEntityID
where HireDate>'2012-01-01' 
order by HireDate desc

--1.2
select ProductID,Name,ListPrice,ProductNumber
from Production.Product
where ListPrice between 100 and 500
order by ListPrice

--1.3
select FirstName,LastName,city
from person.Address a
join Person.BusinessEntityAddress b 
on a.AddressID=b.AddressID
join Person.Person p
on p.BusinessEntityID=b.BusinessEntityID
where city='Seattle' or city ='Portland'

--1.4
select top (15) p.name,ListPrice,DiscontinuedDate,c.name
from Production.Product p
join Production.ProductCategory c 
on p.ProductID=c.ProductCategoryID
where DiscontinuedDate is null
order by ListPrice desc

--2.1
select ProductID,Color,name,ListPrice
from Production.Product
where color like 'Black' and name like '%Mountain%'

--2.2
select FirstName+' '+LastName as ful_name,BirthDate,DATEDIFF(year,BirthDate,GETDATE()) as age
from Person.Person p
join HumanResources.Employee e
on e.BusinessEntityID=p.BusinessEntityID

--2.3
select SalesOrderID, OrderDate,CustomerID, TotalDue
from Sales.SalesOrderHeader
where OrderDate between '2013-10-01' and '2014-01-01'

--2.4
--List products with a null weight but a non-null size, showing product ID, name, weight, size, and product number.
select ProductID,Name,Weight,Size,ProductNumber
from Production.Product
where Weight is null and Size is not null

--3.1 
--Count the number of products by category, ordered by count descending.
select c.name,count(ProductID) as numofproducts
from Production.Product p
join Production.ProductCategory c
on c.ProductCategoryID=p.ProductSubcategoryID
group  by ProductCategoryID,c.name
order by count(ProductID) desc

--3.2 
--Show the average list price by product subcategory, including only subcategories with more than five products.
select s.name,avg(ListPrice) as average_listprice
from Production.Product p
join Production.ProductSubcategory s
on s.ProductSubcategoryID=p.ProductSubcategoryID
group by ProductCategoryID,s.name
having count(s.ProductSubcategoryID)>5

--3.3
--List the top 10 customers by total order count, including customer name.
select top(10) FirstName,count(SalesOrderID) as count
from Sales.Customer c
join Person.Person p
on p.BusinessEntityID=c.PersonID
join Sales.SalesOrderHeader h 
on h.CustomerID=c.CustomerID
group by FirstName
order by count desc

--3.4 
--Show monthly sales totals for 2013, displaying the month name and total amount.
select DATENAME(MONTH,OrderDate) as MONTH,sum(TotalDue) as total_sales
from Sales.SalesOrderHeader 
where year(OrderDate)=2013
group by month(OrderDate),DATENAME(MONTH,OrderDate)
order by month(OrderDate)

--4.1
--Find all products launched in the same year as 'Mountain-100 Black, 42'. Show product ID, name, sell start date, and year.
select ProductID,Name,SellStartDate,year(SellStartDate) as year
from Production.Product
where year(SellStartDate)=(
select year(SellStartDate) from Production.Product where name like'%Mountain-100 Black, 42%'
)

--4.2
--Find employees who were hired on the same date as someone else. 
--Show employee names, shared hire date, and the count of employees hired that day.
select FirstName,HireDate,count(*) OVER (PARTITION BY e.HireDate) as count
from Person.Person p
join HumanResources.Employee e 
on e.BusinessEntityID=p.BusinessEntityID
where hiredate = (
select top 1 HireDate
from HumanResources.Employee
)
group by HireDate ,FirstName

--5.1
--Create a table named Sales.ProductReviews with columns for review ID, product ID, customer ID, rating, 
--review date, review text, verified purchase flag, and helpful votes.
--Include appropriate primary key, foreign keys, check constraints, defaults, and a unique constraint on product ID and customer ID.
create table Sales.ProductReviews (
review_ID int primary key,
product_ID int unique,
customer_ID int unique,
rating int check (rating between 1 and 5),
review_date date default getdate(),
review_text text,
verified_purchase bit,
helpful_votes  int,
foreign key(product_ID) references production.product(ProductID),
foreign key(customer_ID) references sales.Customer(CustomerID)
)

--6.1 
--Add a column named LastModifiedDate to the Production.Product table, with a default value of the current date and time.
alter table production.product
add LastModifiedDate datetime default getdate()

--6.2 
--Create a non-clustered index on the LastName column of the Person.Person table, including FirstName and MiddleName.
create nonclustered index idx_lastname 
on person.person(LastName)
include (FirstName,MiddleName)

--6.3
--Add a check constraint to the Production.Product table to ensure that ListPrice is greater than StandardCost.
alter table Production.Product 
add constraint CK_ListPrice_StandardCost 
check (ListPrice>=StandardCost)

--7.1 
--Insert three sample records into Sales.ProductReviews using existing product and customer IDs, with varied ratings and meaningful review text.
insert into Sales.ProductReviews (review_ID,product_ID,customer_ID,rating,review_text)
values
(1,1,1,1,'excellent') 
,(2,2,2,2,'vert good')
,(3,3,3,3,'good')

--7.2 
--Insert a new product category named 'Electronics' and a corresponding product subcategory named 'Smartphones' under Electronics.
insert into [Production].[ProductCategory]
(Name,rowguid,ModifiedDate)
values
('Electronics','6F9619FF-8B86-D011-B42D-00C04FC964FF',getdate());
select * from [Production].[ProductCategory];

insert into [Production].[ProductSubcategory]
([ProductCategoryID],[Name],[rowguid],[ModifiedDate])
values
(6,'Smartphones','123A45B6-8B86-D011-B42D-00C04FC964FF',getdate())

select * from [Production].[ProductSubcategory]

--7.3 
--Copy all discontinued products (where SellEndDate is not null) into a new table named Sales.DiscontinuedProducts.
select * 
into Sales.DiscontinuedProducts
from [Production].[Product]
where DiscontinuedDate is not null
--no such data
select * 
from [Production].[Product]
where DiscontinuedDate is not  null
--test new table
select * from Sales.DiscontinuedProducts;

--8.1 
--Update the ModifiedDate to the current date for all products where ListPrice is greater than $1000 and SellEndDate is null.
update [Production].[Product] 
set ModifiedDate=getdate()
where ListPrice>1000 
and
SellEndDate is null

--8.2 
--Increase the ListPrice by 15% for all products in the 'Bikes' category and update the ModifiedDate.
update [Production].[Product]
set
ListPrice=ListPrice+(0.15*ListPrice)
,
ModifiedDate=GETDATE()
where ProductSubcategoryID in (1,2,3);
--
select * from [Production].[ProductSubcategory];
select * from [Production].[ProductCategory];
--another answer using join
--Increase the ListPrice by 15% for all products in the 'Bikes' category and update the ModifiedDate.
update p
set
ListPrice=ListPrice+(0.15*ListPrice)
,
ModifiedDate=GETDATE()
from [Production].[Product] p
join [Production].[ProductSubcategory] s
on s.ProductSubcategoryID=p.ProductSubcategoryID
join [Production].[ProductCategory] c
on c.ProductCategoryID=s.ProductCategoryID
where c.name ='Bikes';

--8.3 
--Update the JobTitle to 'Senior' plus the existing job title for employees hired before January 1, 2010.
update [HumanResources].[Employee]
set JobTitle='Senior '+JobTitle
where HireDate<'2010-01-01';
--
select * from [HumanResources].[Employee];

--9.1 
--Delete all product reviews with a rating of 1 and helpful votes equal to 0.
delete from Sales.ProductReviews 
where rating=1
and helpful_votes=0

--9.2 
--Delete products that have never been ordered, using a NOT EXISTS condition with Sales.SalesOrderDetail.
delete from [Production].[Product]
where not exists (
select 1 from Sales.SalesOrderDetail d
join Production.Product p
on p.ProductID=d.ProductID
)

--9.3 
--Delete all purchase orders from vendors that are no longer active.
select * from [Purchasing].[Vendor];
-- cant be deleted because of a trigger
delete from [Purchasing].[Vendor]
where ActiveFlag=0;

--10.1 
--Calculate the total sales amount by year from 2011 to 2014, showing year, total sales, average order value, and order count.
select year(OrderDate) as year,sum(TotalDue) as total_sales,avg(TotalDue) as average_value,count(SalesOrderID) num_of_orders
from [Sales].[SalesOrderHeader]
where year(OrderDate)
between
2011 and 2014
group by year(OrderDate)
order by year(OrderDate)

--10.2 
--For each customer, show customer ID, total orders, total amount, average order value, first order date, and last order date.
select c.CustomerID,count (SalesOrderID) as total_orders,sum(TotalDue) as total_amount,
avg(TotalDue) as average_value,min(OrderDate) as first_order_date,max(OrderDate) as last_order_date
from [Sales].[Customer] c
join [Sales].[SalesOrderHeader] o
on o.CustomerID=c.CustomerID
group by c.CustomerID

--10.3 
--List the top 20 products by total sales amount, including product name, category, total quantity sold, and total revenue.
select top (20) p.name,c.Name,sum(OrderQty) as total_quantity_sold
,sum(LineTotal) as total_revenue
from Sales.SalesOrderDetail o
join Production.Product p
on p.ProductID=o.ProductID
join [Production].[ProductSubcategory] s
on s.ProductSubcategoryID=p.ProductSubcategoryID
join [Production].[ProductCategory] c
on c.ProductCategoryID=s.ProductCategoryID
group by p.name,c.Name
order by total_revenue desc

--10.4 
--Show sales amount by month for 2013, displaying the month name, sales amount, and percentage of the yearly total.
select 
datename(month,OrderDate) as month_name,
sum(TotalDue) As sales_amount ,
(sum(TotalDue)*100)/
(select sum(TotalDue) from [Sales].[SalesOrderHeader]
where year(OrderDate)=2013)
 as percentage
from [Sales].[SalesOrderHeader] 
where year(OrderDate)=2013
group by datename(month,OrderDate),month(OrderDate)

--11.1 
--Show employees with their full name, age in years, years of service, hire date formatted as 'Mon DD, YYYY', and birth month name.
select 
FirstName+' '+isnull(MiddleName,'')+' '+LastName ,'no name'as full_name,
datediff(year,BirthDate,getdate()) as age_in_years,
datediff(year,HireDate,getdate()) as years_of_service,
format(HireDate,'MMM dd , yyyy ') as formatted_hiredate,
datename(month,BirthDate) as month
from Person.Person p
join HumanResources.Employee e
on e.BusinessEntityID=p.BusinessEntityID

--11.2 
--Format customer names as 'LAST, First M.' (with middle initial), extract the email domain, and apply proper case formatting.
select upper(LastName)+' , '+FirstName+' '+isnull(upper(left(MiddleName,1)),'') as formattedname,
SUBSTRING(EmailAddress,CHARINDEX('@',EmailAddress)+1,len(EmailAddress)) as email_domain
from Person.Person p
join Sales.Customer c
on c.PersonID=p.BusinessEntityID
join Person.EmailAddress e
on e.BusinessEntityID=p.BusinessEntityID

--11.3 
--For each product, show name, weight rounded to one decimal, weight in pounds (converted from grams), and price per pound.
select [Name],round([Weight],1) as weight,
cast(case
when WeightUnitMeasureCode='G' 
then Weight/ 453.592
when WeightUnitMeasureCode='LB'
then weight
end as decimal(10,2))
as weight_in_pounds
,
cast(ListPrice/
case
when WeightUnitMeasureCode='G' 
then Weight/ 453.592
when WeightUnitMeasureCode='LB'
then weight 
end as decimal(10,2))
as price_per_pound
from [Production].[Product]

--12.1
--List product name, category, subcategory, and vendor name for products that have been purchased from vendors.
select p.name product_name
,c.name category,
s.Name subcategory
,v.Name vendor
from Production.Product p
join Production.ProductSubcategory s
on s.ProductSubcategoryID=p.ProductSubcategoryID
join Production.ProductCategory c
on c.ProductCategoryID=s.ProductCategoryID
join Purchasing.ProductVendor pv
on pv.ProductID=p.ProductID
join Purchasing.Vendor v
on v.BusinessEntityID=pv.BusinessEntityID

--12.2 !!!
--Show order details including order ID, customer name, 
--salesperson name, territory name, product name, quantity, and line total.
with salesperson as (
select p.BusinessEntityID, p.FirstName from Person.Person p
join Sales.Customer c on c.CustomerID=p.BusinessEntityID
)
select o.SalesOrderID,p.FirstName customer,
terr.Name territory,pr.Name product,d.OrderQty,d.LineTotal,sp.FirstName as salesperson
from Sales.SalesOrderHeader o
join Sales.Customer c on c.CustomerID=o.CustomerID
--join Sales.SalesPerson s on s.BusinessEntityID=o.SalesPersonID
join Person.Person p on p.BusinessEntityID=c.CustomerID
join Sales.SalesOrderDetail d on d.SalesOrderID=o.SalesOrderID
join Production.Product pr on pr.ProductID=d.ProductID
join Sales.SalesTerritory terr on terr.TerritoryID=o.TerritoryID
join salesperson sp on  sp.BusinessEntityID=p.BusinessEntityID

--12.3 
--List employees with their sales territories, including employee name,
--job title, territory name, territory group, and sales year-to-date.
select p.FirstName,e.JobTitle,st.Name as territory,st.[Group],
st.[CostYTD]
from Sales.SalesPerson s
join [HumanResources].[Employee] e on e.BusinessEntityID=s.BusinessEntityID
join [Person].[Person] p on p.BusinessEntityID=e.BusinessEntityID
join [Sales].[SalesTerritory] st on st.TerritoryID=s.TerritoryID

--13.1 
--List all products with their total sales, including those never sold. 
--Show product name, category, total quantity sold (zero if never sold), and total revenue (zero if never sold).
select p.name productname,c.Name category,
isnull(d.OrderQty,0) OrderQty,
isnull(d.LineTotal,0) total_revenue
from Production.Product p
left join Production.ProductSubcategory s on s.ProductSubcategoryID=p.ProductSubcategoryID
left join Production.ProductCategory c on c.ProductCategoryID=s.ProductCategoryID
left join Sales.SalesOrderDetail d on d.ProductID=p.ProductID

--13.2 
--Show all sales territories with their assigned employees, including unassigned territories.
--Show territory name, employee name (null if unassigned), and sales year-to-date.
select t.Name,isnull(p.FirstName,'null'),t.CostYTD
from [Sales].[SalesTerritory] t
join [Sales].[SalesPerson] s on s.TerritoryID=t.TerritoryID
left join [HumanResources].[Employee] e on e.BusinessEntityID=s.BusinessEntityID
join Person.Person p on p.BusinessEntityID=e.BusinessEntityID

--13.3 
--Show the relationship between vendors and product categories, including vendors with no products and categories with no vendors.
select v.BusinessEntityID vendorid ,v.Name vendor, pr.ProductID ,c.Name category
from Purchasing.Vendor v
full join Purchasing.ProductVendor pv on v.BusinessEntityID=pv.BusinessEntityID
full join Production.Product pr on pv.ProductID=pr.ProductID 
full join Production.ProductSubcategory s on s.ProductSubcategoryID=pr.ProductSubcategoryID
full join [Production].[ProductCategory] c on c.ProductCategoryID=s.ProductCategoryID

--14.1 List products with above-average list price, showing product ID, name, list price, and price difference from the average.
select p.ProductID,p.Name product,
p.ListPrice,p.ListPrice-(
select avg(ListPrice)
from Production.Product
) as diff
from Production.Product p
where ListPrice >(
select avg(ListPrice)
from Production.Product
)

--14.2 
--List customers who bought products from the 'Mountain' category, showing customer name, total orders, and total amount spent.
select p.FirstName,count(h.SalesOrderID) as total_orders,sum(h.TotalDue) as total_amount_spent
from Sales.Customer c
join Person.Person p on c.PersonID =p.BusinessEntityID
join Sales.SalesOrderHeader h on h.CustomerID=c.CustomerID
join Sales.SalesOrderDetail d on d.SalesOrderID=h.SalesOrderID
join Production.Product pr on pr.ProductID=d.ProductID
join Production.ProductSubcategory s on s.ProductSubcategoryID=pr.ProductSubcategoryID
join Production.ProductCategory cat on cat.ProductCategoryID=s.ProductCategoryID
where s.name like '%Mountain%'
group by FirstName

--14.3 
--List products that have been ordered by more than 100 different customers, showing product name, category, and unique customer count.
select p.Name product,cat.Name category,count(distinct CustomerID) as custcount
from Sales.SalesOrderDetail d
join Sales.SalesOrderHeader h on h.SalesOrderID=d.SalesOrderID
join Production.Product p on p.ProductID=d.ProductID
join Production.ProductSubcategory s on s.ProductSubcategoryID=p.ProductSubcategoryID
join Production.ProductCategory cat on cat.ProductCategoryID=s.ProductCategoryID
group by p.Name ,cat.Name
having count(distinct CustomerID)>100

--14.4 For each customer, show their order count and their rank among all customers.
with order_count as (
select o.CustomerID,count(SalesOrderID) as ordercount
from Sales.SalesOrderHeader o
group by CustomerID
)
select CustomerID,ordercount,
rank()over(order by ordercount) as rank
from order_count

--15.1 
--Create a view named vw_ProductCatalog with product ID, name, product number, category, 
--subcategory, list price, standard cost, profit margin percentage, inventory level, and status (active/discontinued).
create view 
vw_ProductCatalog as
select p.ProductID,p.Name product_name,
c.Name category_name,s.Name Subcategory_name,p.ListPrice
,p.StandardCost,
(ListPrice-StandardCost)/ListPrice*100
profit_margin_percentage
,sum(i.Quantity) as inventory_level
,case 
when SellEndDate is null then 'active'
else 'discontinued'
end as status
from Production.Product p
join Production.ProductInventory i on i.ProductID=p.ProductID
join Production.ProductSubcategory s on s.ProductSubcategoryID=p.ProductSubcategoryID
join Production.ProductCategory c on c.ProductCategoryID=s.ProductCategoryID
group by p.ProductID,p.Name,
c.Name,s.Name,p.ListPrice
,p.StandardCost,p.SellEndDate;

--15.2!!
--Create a view named vw_SalesAnalysis with year, month, territory, total sales, order count, average order value, and top product name.
create view vw_SalesAnalysis as
with data as(
select 
year(OrderDate) orderyear,month(OrderDate) ordermonth,t.name,
o.[SalesOrderID] ,o.[TotalDue],p.name product
from Sales.SalesOrderHeader o
join Sales.SalesTerritory t on t.TerritoryID=o.TerritoryID
join Sales.SalesOrderDetail d on d.SalesOrderID=o.SalesOrderID
join Production.Product p on p.ProductLine=d.ProductID
),
with aggr as(
select 
orderyear,ordermonth,
sum(TotalDue) as total_sales,
count(SalesOrderID) as ordercount,
avg(TotalDue) as avg_ordervalue

from data
)

--15.3       !!cant get manager name
--Create a view named vw_EmployeeDirectory with full name, job title, department, manager name, hire date, years of service, email, and phone.
create view vw_EmployeeDirectory
as 
select
p.FirstName+' '+isnull(p.MiddleName,'')+' '+p.LastName as full_name,
e.JobTitle,dep.Name department,
e.HireDate,
datediff(YEAR,HireDate,GETDATE()) as years_of_service,
ad.EmailAddress,
ph.PhoneNumber
from Person.Person p
join HumanResources.Employee e on p.BusinessEntityID=e.BusinessEntityID
join Person.EmailAddress ad on ad.BusinessEntityID = p.BusinessEntityID
join Person.PersonPhone ph on ph.BusinessEntityID=p.BusinessEntityID
join HumanResources.EmployeeDepartmentHistory d on d.BusinessEntityID=p.BusinessEntityID
join HumanResources.Department dep on dep.DepartmentID=d.DepartmentID

--15.4 Write three different queries using the views you created, demonstrating practical business scenarios.
select *
from vw_ProductCatalog
where status='active'

select * 
from vw_EmployeeDirectory
where JobTitle like '%senior%'

--16.1 
--Classify products by price as 'Premium' (greater than $500), 'Standard' ($100 to $500),
--or 'Budget' (less than $100), and show the count and average price for each category.
with categories as (
select p.ProductID, c.ProductCategoryID,sum(ListPrice) as totalforcategory,
avg(ListPrice) as avgforcategory
from Production.Product p
join Production.ProductSubcategory s on s.ProductSubcategoryID=p.ProductSubcategoryID
join Production.ProductCategory c on  c.ProductCategoryID=s.ProductSubcategoryID
group by c.ProductCategoryID,p.ProductID )
select p.ProductID,ListPrice,
case
when ListPrice>500 then 'Premium'
when ListPrice between 100 and 500 then 'Standard'
when ListPrice<100 then 'Budget'
end as Classification,
c.ProductCategoryID,totalforcategory,avgforcategory
from Production.Product p
join categories c on p.ProductID=c.ProductID

--16.2 
--Classify employees by years of service as 'Veteran' (10+ years), 'Experienced' (5-10 years), 'Regular' (2-5 years),
--or 'New' (less than 2 years), and show salary statistics for each group.
select datediff(YEAR,HireDate,GETDATE()) as years_of_service ,
case
when datediff(YEAR,HireDate,GETDATE()) >10 then 'Veteran'
when datediff(YEAR,HireDate,GETDATE()) between 5 and 10 then 'Experienced'
when datediff(YEAR,HireDate,GETDATE()) between 2 and 4 then 'Regular'
else 'New' 
end as Classification,
count(*) employee_count,
avg(p.Rate) avg_salary_per_hour,
max(p.Rate) max_salary_per_hour,
min(p.Rate) min_salary_per_hour
from HumanResources.Employee e
join [HumanResources].[EmployeePayHistory] p on p.BusinessEntityID=e.BusinessEntityID
group by datediff(YEAR,HireDate,GETDATE()) ,
case
when datediff(YEAR,HireDate,GETDATE()) >10 then 'Veteran'
when datediff(YEAR,HireDate,GETDATE()) between 5 and 10 then 'Experienced'
when datediff(YEAR,HireDate,GETDATE()) between 2 and 4 then 'Regular'
else 'New' 
end 

--16.3 
--Classify orders by size as 'Large' (greater than $5000), 'Medium' ($1000 to $5000), or 'Small' (less than $1000), and show the percentage distribution.
with order_Classification 
as
(
select h.TotalDue,
case
when TotalDue>5000 then 'Large'
when TotalDue between 1000 and 5000 then 'Medium'
else 'Small'
end as size
from Sales.SalesOrderHeader h
)
select size ,
count(*) orders_count,
(100.0*count(*))/sum(count(*) )over()
as distribution
from order_Classification o
group by size

--17.1 
--Show products with name, weight (display 'Not Specified' if null), size (display 'Standard' if null), and color (display 'Natural' if null).
select 
name,
isnull(cast (Weight as varchar),'Not Specified') as weight
,isnull(Size,'Standard') as size
,isnull(Color,'Natural') as color
from Production.Product 

--17.2 For each customer, display the best available contact method, prioritizing email address, then phone, then address line.
select 
coalesce(e.EmailAddress,ph.PhoneNumber,a.AddressLine1,a.AddressLine2)
as best_available_contact_method
from Person.Person p
join Sales.Customer c on c.PersonID=p.BusinessEntityID
join Person.EmailAddress e on e.BusinessEntityID=p.BusinessEntityID
join Person.PersonPhone ph on ph.BusinessEntityID=p.BusinessEntityID
join Person.BusinessEntityAddress ad on ad.BusinessEntityID=p.BusinessEntityID
join Person.Address a on a.AddressID=ad.AddressID

--17.3 
--Find products where weight is null but size is not null, and also find products where both weight and size are null.
--Discuss the impact on inventory management.
select Weight,size,
case 
when Weight is null and size is not null then 'weight missing only'
when Weight is null and size is null then 'weight and size missing'
end as  Weight_size_status
from Production.Product
where Weight is null

--18.1 !!!!
--Create a recursive query to show the complete employee hierarchy, including employee name, manager name, hierarchy level, and path.


--18.2 Create a query to compare year-over-year sales for each product, showing product, sales for 2013, sales for 2014, growth percentage, and growth category.
with sales2013 as(
select d.ProductID ,year(orderdate) y2013,sum(d.LineTotal) sales2013
from sales.SalesOrderDetail d
join Sales.SalesOrderHeader o on o.SalesOrderID=d.SalesOrderID 
where year(orderdate)=2013
group by d.ProductID,year(orderdate)
),
sales2014 as(
select d.ProductID ,year(orderdate) y2014,sum(d.LineTotal) sales2014
from sales.SalesOrderDetail d
join Sales.SalesOrderHeader o on o.SalesOrderID=d.SalesOrderID 
where year(orderdate)=2014
group by d.ProductID,year(orderdate)
)
 select s1.ProductID,y2013,sales2013,y2014,sales2014,
 (sales2014-sales2013)*100/sales2013 as growth_percentage,
 case 
 when sales2014>sales2013 then 'increased'
  when sales2014<sales2013 then 'decreased'
  else 'no change' 
 end as growth_category
 from  sales2013 s1
 join sales2014 s2 on s1.ProductID=s2.ProductID

--19.1 Rank products by sales within each category, showing product name, category, sales amount, rank, dense rank, and row number.
select p.name productname,
c.Name category,
sum(d.OrderQty*d.UnitPrice) salesamount,
rank() over(partition by c.Name order by sum (d.OrderQty*d.UnitPrice) desc ) as rank,
dense_rank() over(partition by c.Name order by sum (d.OrderQty*d.UnitPrice) desc ) as denserank,
row_number() over(partition by c.Name order by sum (d.OrderQty*d.UnitPrice) desc ) as row_number
from Production.Product p
join Production.ProductSubcategory s on s.ProductSubcategoryID=p.ProductSubcategoryID
join Production.ProductCategory c on c.ProductCategoryID=s.ProductCategoryID
join Sales.SalesOrderDetail d on d.ProductID=p.ProductID
group by p.Name,c.Name ;

--19.2 Show the running total of sales by month for 2013, displaying month, monthly sales, running total, and percentage of year-to-date.
with monthlysales as (
select month(OrderDate) month,sum(LineTotal) as monthly_sales
from Sales.SalesOrderHeader o
join Sales.SalesOrderDetail d on d.SalesOrderID=o.SalesOrderID
where YEAR(OrderDate)=2013 
group by month(OrderDate)
)
select monthly_sales,
monthly_sales,
sum(monthly_sales) over(order by month) running_total,
(sum(monthly_sales) over(order by month)*100)/sum(monthly_sales) over() as percentageYTD
from monthlysales
order by month

--19.3 Show the three-month moving average of sales for each territory, displaying territory, month, sales, and moving average.
with monthsales as (
select t.Name territory,month(OrderDate) month,year(OrderDate) year,sum(LineTotal) sales
from Sales.SalesOrderDetail d
join Sales.SalesOrderHeader o on o.SalesOrderID=d.SalesOrderID
join Sales.SalesTerritory t on t.TerritoryID=o.TerritoryID
group by t.Name,month(OrderDate),year(OrderDate)
)
select territory,month,year,sales,
 AVG(sales) OVER (
 PARTITION BY Territory
 ORDER BY Year, Month
 ROWS BETWEEN 2 PRECEDING AND CURRENT ROW
    ) moving_average
from monthsales
order by month,year

--19.4 Show month-over-month sales growth, displaying month, sales, previous month sales, growth amount, and growth percentage.
with monthly as (
select month(OrderDate) monthsales,year(OrderDate) syear,
sum(d.LineTotal) sales
from Sales.SalesOrderDetail d
join Sales.SalesOrderHeader o on o.SalesOrderID=d.SalesOrderID
group by month(OrderDate) ,year(OrderDate)
)
select monthsales,syear,sales,
lag(sales) over(order by monthsales) as PreviousMonthSales,
sales-lag(sales) over(order by monthsales) as growthamount,
sales-lag(sales) over(order by monthsales) *100 /lag(sales) over(order by monthsales) growth_percentage
from monthly 

--19.5 
--Divide customers into four quartiles based on total purchase amount, showing customer name, total purchases, quartile, and quartile average.
with total as(
select p.FirstName+' '+p.LastName as fullname,sum(o.TotalDue) total_purchases,
ntile(4) over (order by sum(o.TotalDue) desc) as quartile
from Person.Person p
join Sales.Customer c on c.PersonID=p.BusinessEntityID
join Sales.SalesOrderHeader o on o.CustomerID=c.CustomerID
group by p.FirstName+' '+p.LastName
)
select 
fullname,
total_purchases,
quartile,
avg(total_purchases) over(partition by quartile) as average
from total
ORDER BY quartile, total_purchases DESC;

--20.1 Create a pivot table showing product categories as rows and years (2011-2014) as columns, displaying sales amounts with totals.
with data as(
select year(OrderDate) year,c.Name category,sum(TotalDue) total_sales
from Sales.SalesOrderHeader o
join Sales.SalesOrderDetail d on o.SalesOrderID=d.SalesOrderID
join Production.Product p on p.ProductID=d.ProductID
join Production.ProductSubcategory s on s.ProductSubcategoryID=p.ProductSubcategoryID
join Production.ProductCategory c on c.ProductCategoryID=s.ProductCategoryID
where year(OrderDate) between 2011 and 2014
group by year(OrderDate),c.Name )
select category,
[2011] sales2011,[2012] sales2012,[2013] sales2013,[2014] sales2014,
[2011]+[2012]+[2013]+[2014] as totals

from data
pivot (
sum(total_sales)
for year IN ([2011], [2012], [2013], [2014])
)AS PivotTable