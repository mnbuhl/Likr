### Useful documentation:

Generate nuget package from Likr.Shared - Navigate to Likr.Shared folder and run command:

``` dotnet pack -p:PackageVersion=1.X.X --output ../../Packages ```

#### Docker commands:

Run: ```docker compose up -d```

Build & run:
``` docker compose up -d --build ```

Rebuild: ```docker compose up -d --build --force-recreate```

Stop: ```docker compose down```