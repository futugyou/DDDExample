#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
ARG NET_IMAGE=10.0
FROM mcr.microsoft.com/dotnet/runtime:${NET_IMAGE} AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:${NET_IMAGE} AS build
WORKDIR /src
COPY ["src/Example.Domain.UnitTest/Example.Domain.UnitTest.csproj", "src/Example.Domain.UnitTest/"]
COPY ["src/Example.Domain/Example.Domain.csproj", "src/Example.Domain/"]
COPY ["src/Example.Domain.Core/Example.Domain.Core.csproj", "src/Example.Domain.Core/"]
COPY ["Directory.Build.props", "./"]
COPY ["Directory.Packages.props", "./"]
RUN dotnet restore "src/Example.Domain.UnitTest/Example.Domain.UnitTest.csproj"
COPY . .
WORKDIR "/src/src/Example.Domain.UnitTest"
# RUN dotnet build "Example.Domain.UnitTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore "Example.Domain.UnitTest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Example.Domain.UnitTest.dll"]