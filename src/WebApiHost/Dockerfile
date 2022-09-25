#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
ARG NET_IMAGE=7.0
FROM mcr.microsoft.com/dotnet/aspnet:${NET_IMAGE} AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:${NET_IMAGE} AS build
WORKDIR /src

COPY ["WebApiHost/WebApiHost.csproj", "WebApiHost/"]
COPY ["Example.Infrastruct.Data/Example.Infrastruct.Data.csproj", "Example.Infrastruct.Data/"]
COPY ["Example.Domain.Core/Example.Domain.Core.csproj", "Example.Domain.Core/"]
COPY ["Example.Domain/Example.Domain.csproj", "Example.Domain/"]
COPY ["Example.Application/Example.Application.csproj", "Example.Application/"]
COPY ["WebApiHost.UnitTest/WebApiHost.UnitTest.csproj", "WebApiHost.UnitTest/"]
COPY ["DDDExample.sln", "./"]
RUN dotnet restore "DDDExample.sln"

COPY . .
WORKDIR "/src/WebApiHost"
#RUN dotnet build "WebApiHost.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore "WebApiHost.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiHost.dll"]