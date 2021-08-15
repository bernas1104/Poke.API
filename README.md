# Poke.API

## Migrations
To create a new database migration, add your entities to the project (or change
any of the existing ones), create the mappings and run the following command:

```bash
dotnet ef --startup-project src/Poke.API migrations -p src/Poke.Infra/ add <MIGRATION_NAME> --context EntityContext
```

This will create your new migrations. To update the database, run the following:

```bash
dotnet ef database -p src/Poke.API/ update --context EntityContext
```
