*** DB backup and Scripts files provided for Sample DB. Can use either one to create/restore Sample DB

Script file provided: Scripts.sql
- If using Script file, execute the queries mentioned in script file 
- Uncomment insert queries to insert sample data in created tables.


Sample DB backup provided: JobsDB.bak
- If using DB backup, please follow below steps to restore DB:
1. In SQL Server Management Studio, connect to required server
2. Right click on 'Databases' folder -> click on 'Restore Database' option
3. In Restore Database window, in 'Source' section select 'Device' and navigate to DB backup file and select it
4. In 'Destination' section, you can change the DB name in 'Database' field
5. Select Restore Plan
6. Click on OK


*** To connect application to created/restored DB
- Change the connectionString value in all App.config and Web.config files (all projects in solution, including testing projects), change it to required server and database details.

- Changing the below values in connectionString:
data source = to required server name
initial catalog = to required database name
integrated security = True //if the server is connected using Windows Auth

Sample connection string for reference:
<connectionStrings>
    <add name="JobsEntities" connectionString="metadata=res://*/JobsModel.csdl|res://*/JobsModel.ssdl|res://*/JobsModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=TestServer\SQLEXPRESS;initial catalog=JobsDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
  </connectionStrings>
