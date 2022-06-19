FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Cms.Presentation/Cms.Presentation.csproj", "Cms.Presentation/"]
COPY ["Cms.Common/Cms.Common.csproj", "Cms.Common/"]
COPY ["Cms.Services/Cms.Services.csproj", "Cms.Services/"]
COPY ["Cms.Data/Cms.Data.csproj", "Cms.Data/"]
RUN dotnet restore "Cms.Presentation/Cms.Presentation.csproj"
COPY . .
WORKDIR "/src/Cms.Presentation"
RUN dotnet build "Cms.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cms.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cms.Presentation.dll"]
