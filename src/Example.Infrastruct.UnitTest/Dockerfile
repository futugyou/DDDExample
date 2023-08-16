#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
ARG NET_IMAGE=8.0-preview
FROM mcr.microsoft.com/dotnet/runtime:${NET_IMAGE} AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:${NET_IMAGE} AS build
WORKDIR /src
COPY ["src/Example.Infrastruct.UnitTest/Example.Infrastruct.UnitTest.csproj", "src/Example.Infrastruct.UnitTest/"]
COPY ["src/Example.Infrastruct/Example.Infrastruct.csproj", "src/Example.Infrastruct/"]
COPY ["src/Example.Domain/Example.Domain.csproj", "src/Example.Domain/"]
COPY ["src/Example.Domain.Core/Example.Domain.Core.csproj", "src/Example.Domain.Core/"]
RUN dotnet restore "src/Example.Infrastruct.UnitTest/Example.Infrastruct.UnitTest.csproj"
COPY . .
WORKDIR "/src/src/Example.Infrastruct.UnitTest"
# RUN dotnet build "Example.Infrastruct.UnitTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore "Example.Infrastruct.UnitTest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Example.Infrastruct.UnitTest.dll"]