
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