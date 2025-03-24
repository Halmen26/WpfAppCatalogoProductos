# WpfAppCatalogProduct

## Crear contenedor de SQL Server
```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Interfaces-2425" -p 1433:1433 --name sqlserver_container -d mcr.microsoft.com/mssql/server:2022-latest 
```