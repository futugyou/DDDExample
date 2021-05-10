#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApiHost/WebApiHost.csproj", "WebApiHost/"]
COPY ["Example.Infrastruct.Data/Example.Infrastruct.Data.csproj", "Example.Infrastruct.Data/"]
COPY ["Example.Domain.Core/Example.Domain.Core.csproj", "Example.Domain.Core/"]
COPY ["Example.Domain/Example.Domain.csproj", "Example.Domain/"]
COPY ["Example.Application/Example.Application.csproj", "Example.Application/"]
RUN dotnet restore "WebApiHost/WebApiHost.csproj"
COPY . .
WORKDIR "/src/WebApiHost"
RUN dotnet build "WebApiHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApiHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiHost.dll"]