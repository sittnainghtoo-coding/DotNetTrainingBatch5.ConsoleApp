
oracle 
------

only select commit data from database so we don't have to worry about performance issues

Select * from tbl_blog with (nolock)

comit data / uncommit data

insert into


update tbl_blog


1- ko ko1
2- ko ko2
3- ko ko3 - ko ko 6   <---- in this state koko 6 is uncommit stage so with nolock we only select koko3
4- ko ko4
5- ko ko5


efcore database first (manual ,auto) / code first

efcore database first (manual ,auto) / code first


---------------------


when you want to database first you have to use scaffold command

dotnet ef dbcontext scaffold "Server=.;Database=DotNetTrainingBatch5;User Id=sa;Password=12345;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -f

			                                                                                                                                                                                     -t mean table you want tbl1,2 3 and then add -t tbl1,tbl2,tbl3 / -f mean by force
																																																 
dotnet ef dbcontext scaffold "Server=.;Database=DotNetTrainingBatch5;User Id=sa;Password=12345;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -t Tbl_TodoList -f


----------------------

Request Model
Response Model
Dto

we must split resquest model and response model to avoid unnessary request data.

---------------------


ado.net /dapper => custom service
-----------------------------------



