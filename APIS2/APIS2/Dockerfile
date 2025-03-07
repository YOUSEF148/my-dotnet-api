
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 10000  


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["APIS2.csproj", "./"]
RUN dotnet restore "./APIS2.csproj"
COPY . .
RUN dotnet build -c Release -o /app/build


FROM build AS publish
RUN dotnet publish -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

CMD ["dotnet", "APIS2.dll", "--urls=http://0.0.0.0:${PORT:-10000}"]
