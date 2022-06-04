# Picture store sample

## Initializing DB

Example uses a local SQL Server db, that can be initialized simply using dotnet ef

If you do not have already installed **install the dotnet-ef tool to interact with entity framework**.

```
dotnet tool install --global dotnet-ef
```

Once you have the tool you can initialize a local database with.

```
dotnet ef database update
```

After this you can use Sql Object browser inside Visual Studio or Azure Data Studio to interact with the database.