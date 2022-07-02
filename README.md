# Test_CS
A project created on WinForms for working with a DB, connecting to it and creating a simple query, as well as for displaying the received data in a form.

The composition of the archive:
+ database (MS SQL Server) with one table;
+ full project with source code.

#### Connecting a database to a project
For the correct connection of the database to the project, it is necessary to specify the server name of the local SQL service (Data Source) in the .config file.
```c#
connectionString="Data Source=DESKTOP-7V0R4GH\SQLEXPRESS;Initial Catalog=ShipmentDB;Integrated Security=True"
```
#### Working with the application
When the application starts, a form with a datagrid appears, the cells of which are automatically filled with data from the DB.
The user specifies the columns for which he wants to get totals using the checklist on the right side of the form, ticking them.
After that, user presses the "Group" button and the data is summarized by the specified columns, and in the same datagrid, instead of the original data, the totals are displayed.
The user can refuse the selected grouping by clicking "Return original data", after which the original data from the DB will be returned to the datagrid.
