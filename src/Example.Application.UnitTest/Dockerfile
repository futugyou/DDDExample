#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
ARG NET_IMAGE=9.0
FROM mcr.microsoft.com/dotnet/runtime:${NET_IMAGE} AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:${NET_IMAGE} AS build
WORKDIR /src
COPY ["src/Example.Application.UnitTest/Example.Application.UnitTest.csproj", "src/Example.Application.UnitTest/"]
COPY ["src/Example.Application/Example.Application.csproj", "src/Example.Application/"]
COPY ["src/Example.Domain/Example.Domain.csproj", "src/Example.Domain/"]
COPY ["src/Example.Domain.Core/Example.Domain.Core.csproj", "src/Example.Domain.Core/"]
RUN dotnet restore "src/Example.Application.UnitTest/Example.Application.UnitTest.csproj"
COPY . .
WORKDIR "/src/src/Example.Application.UnitTest"
# RUN dotnet build "Example.Application.UnitTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore "Example.Application.UnitTest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Example.Application.UnitTest.dll"]