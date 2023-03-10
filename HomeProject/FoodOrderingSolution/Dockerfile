FROM mcr.microsoft.com/dotnet/core/sdk:latest AS build
WORKDIR /app
EXPOSE 80

# copy csproj and restore as distinct layers
COPY *.props .
COPY *.sln .

COPY BLL.App/*.csproj ./BLL.App/
COPY BLL.App.DTO/*.csproj ./BLL.App.DTO/
COPY Contracts.BLL.App/*.csproj ./Contracts.BLL.App/

COPY Contracts.DAL.App/*.csproj ./Contracts.DAL.App/
COPY DAL.App.DTO/*.csproj ./DAL.App.DTO/
COPY DAL.App.EF/*.csproj ./DAL.App.EF/

COPY Domain/Domain.App/*.csproj ./Domain/Domain.App/

COPY PublicApi.DTO.v1/*.csproj ./PublicApi.DTO.v1/
COPY Extensions/*.csproj ./Extensions/

COPY WebApp/*.csproj ./WebApp/

RUN dotnet restore


# copy everything else and build app
COPY BLL.App/. ./BLL.App/
COPY BLL.App.DTO/. ./BLL.App.DTO/
COPY Contracts.BLL.App/. ./Contracts.BLL.App/

COPY Contracts.DAL.App/. ./Contracts.DAL.App/
COPY DAL.App.DTO/. ./DAL.App.DTO/
COPY DAL.App.EF/. ./DAL.App.EF/

COPY Domain/Domain.App/. ./Domain/Domain.App/

COPY PublicApi.DTO.v1/. ./PublicApi.DTO.v1/
COPY Extensions/. ./Extensions/

COPY WebApp/. ./WebApp/

WORKDIR /app/WebApp
RUN dotnet publish -c Release -o out



FROM mcr.microsoft.com/dotnet/core/aspnet:latest AS runtime
WORKDIR /app
EXPOSE 80
ENV ConnectionStrings:MySqlConnection="server=alpha.akaver.com;database=student2018_magord_FoodOrderingDb;user=student2018;password=student2018"
COPY --from=build /app/WebApp/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]
