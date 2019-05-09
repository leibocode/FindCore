FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["FindCore.UserAPI/FindCore.UserAPI.csproj", "FindCore.UserAPI/"]
RUN dotnet restore "FindCore.UserAPI/FindCore.UserAPI.csproj"
COPY . .
WORKDIR "/src/FindCore.UserAPI"
RUN dotnet build "FindCore.UserAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FindCore.UserAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FindCore.UserAPI.dll"]