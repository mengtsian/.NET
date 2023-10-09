FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
# TODO: Copy only csproj
COPY . .

RUN dotnet restore Oplog.Api/Oplog.Api.csproj

# Copy everything else and build
# TODO: Copy rest of files
# COPY . ./
RUN dotnet publish Oplog.Api -c Release -o Oplog.Api/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/Oplog.Api/out .

RUN addgroup --gid 1001 radix-non-root-group
RUN adduser --uid 1001 --gid 1001 radix-non-root-user

USER 1001

EXPOSE 5000
ENTRYPOINT ["dotnet", "Oplog.Api.dll"]