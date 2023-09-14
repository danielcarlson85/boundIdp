#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/IDP.Runtime/", "IDP.Runtime/"]
COPY ["src/IDP.Manager/", "IDP.Manager/"]
COPY ["src/IDP.Abstractions/", "IDP.Abstractions/"]
COPY ["src/IDP.EventBus", "IDP.EventBus"]

COPY ["NuGet.Config", "/"]
ARG PAT=cb4lwgwasb5yfiv7avemgzigjcjamynvw2nc4w5v7tszi3fddj2a
RUN sed -i "s|</configuration>|<packageSourceCredentials><BoundNuget><add key=\"Username\" value=\"PAT\" /><add key=\"ClearTextPassword\" value=\"${PAT}\" /></BoundNuget></packageSourceCredentials></configuration>|" /NuGet.Config
RUN dotnet restore "IDP.Runtime/IDP.Runtime.csproj" --configfile "/NuGet.Config"
#RUN dotnet restore "IDP.Runtime/IDP.Runtime.csproj"
COPY . .
WORKDIR "/src/IDP.Runtime"
RUN dotnet build "IDP.Runtime.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IDP.Runtime.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bound.IDP.Runtime.dll"]
