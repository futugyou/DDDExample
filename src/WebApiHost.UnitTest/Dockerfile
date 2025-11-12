#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
ARG NET_IMAGE=10.0
FROM mcr.microsoft.com/dotnet/runtime:${NET_IMAGE} AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:${NET_IMAGE} AS build
WORKDIR /src
COPY ["src/WebApiHost.UnitTest/WebApiHost.UnitTest.csproj", "src/WebApiHost.UnitTest/"]
COPY ["src/WebApiHost/WebApiHost.csproj", "src/WebApiHost/"]
COPY ["src/Example.Application/Example.Application.csproj", "src/Example.Application/"]
COPY ["src/Example.Domain/Example.Domain.csproj", "src/Example.Domain/"]
COPY ["src/Example.Domain.Core/Example.Domain.Core.csproj", "src/Example.Domain.Core/"]
COPY ["src/Example.Infrastruct/Example.Infrastruct.csproj", "src/Example.Infrastruct/"]
COPY ["Directory.Build.props", "./"]
COPY ["Directory.Packages.props", "./"]
RUN dotnet restore "src/WebApiHost.UnitTest/WebApiHost.UnitTest.csproj"
COPY . .
WORKDIR "/src/src/WebApiHost.UnitTest"
# RUN dotnet build "WebApiHost.UnitTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore "WebApiHost.UnitTest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiHost.UnitTest.dll"]