FROM mcr.microsoft.com/dotnet/core/sdk:latest AS build
WORKDIR /app
EXPOSE 80

# copy csproj and restore as distinct layers
COPY *.props .
COPY *.sln .

COPY Contracts.DAL.App/*.csproj ./Contracts.DAL.App/
COPY DAL.App.DTO/*.csproj ./DAL.App.DTO/
COPY DAL.App.EF/*.csproj ./DAL.App.EF/

COPY Domain.App/*.csproj ./Domain.App/

COPY PublicApi.DTO.v1/*.csproj ./PublicApi.DTO.v1/
COPY Extensions/*.csproj ./Extensions/

COPY WebApp/*.csproj ./WebApp/

RUN dotnet restore


# copy everything else and build app
COPY Contracts.DAL.App/. ./Contracts.DAL.App/
COPY DAL.App.DTO/. ./DAL.App.DTO/
COPY DAL.App.EF/. ./DAL.App.EF/

COPY Domain.App/. ./Domain.App/

COPY PublicApi.DTO.v1/. ./PublicApi.DTO.v1/
COPY Extensions/. ./Extensions/

COPY WebApp/. ./WebApp/

WORKDIR /app/WebApp
RUN dotnet publish -c Release -o out



FROM mcr.microsoft.com/dotnet/core/aspnet:latest AS runtime
WORKDIR /app
EXPOSE 80
ENV ConnectionStrings:MySqlConnection="Server=alpha.akaver.com,1533;User Id=student;Password=Student.Bad.password.0;Database=magord-exam2019s;MultipleActiveResultSets=true"
COPY --from=build /app/WebApp/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]
