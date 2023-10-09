# Oplog-API

## Cases
### SQL Database table - import .mdb files

1. Right click relevant SQL database < Task < Import Data
2. Data source: Microsoft Access (Microsoft Access Database Engine)
3. Destination (Azure SQL): Microsoft OLE DB Driver for SQL Server (Also choose "Windows Authentication"  in Properties)
	
Known issue: Importing tables can produce exceptions in some cases. Rows can contain wrong DateTime format values - these needs to be editet. Replace these incidents with NULL/empty values