FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build

WORKDIR /app/src

# --------------------------------
COPY Fakebook.Profile.Domain/*.csproj Fakebook.Profile.Domain/
COPY Fakebook.Profile.DataAccess/*.csproj Fakebook.Profile.DataAccess/
COPY Fakebook.Profile.RestApi/*.csproj Fakebook.Profile.RestApi/
COPY Fakebook.Profile.UnitTests/*.csproj Fakebook.Profile.UnitTests/
COPY *.sln ./
RUN dotnet restore
# ---------------------------------
COPY .config ./
RUN dotnet tool restore
# ---------------------------------

COPY . ./

# generate SQL script from migrations
RUN dotnet ef migrations script -p Fakebook.Profile.DataAccess -s Fakebook.Profile.RestApi -o ../init-db.sql -i

FROM postgres:13.0 AS runtime

WORKDIR /docker-entrypoint-initdb.d

ENV POSTGRES_PASSWORD Pass@word

COPY --from=build /app/init-db.sql .
