FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "Template-WebAPI/Template-WebAPI.csproj"
COPY . .
WORKDIR "/src/Template-WebAPI"

RUN dotnet build "Template-WebAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Template-WebAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app

COPY --from=publish /app .
RUN mkdir -p Resources/File
ENTRYPOINT ["dotnet", "Template-WebAPI.dll"]