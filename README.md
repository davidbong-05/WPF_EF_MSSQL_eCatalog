# WPF_EF_MSSQL_eCatalog

This is a simple eCatalog desktop application build using .NET WPF, Entity Framework and MSSQL. It fetch the data from a WooCommerce REST API and store it in a local database using RestSharp.

## Project Setup

Edit the following code in Context\ApplicationDbContext.cs to match your database connection string

```bash
optionsBuilder.UseSqlServer("Data Source=(localdb)\\local;Initial Catalog=davidECatalogDb;Trusted_Connection=True;");
```

Rename the app.config.example file to app.config and fill in the required information
