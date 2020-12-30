# -------------------------------------------------------------------------- #
# -------------------------<[ Build Docker Script ]>------------------------ #
# -------------------------------------------------------------------------- #

# get base sdk image
FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build

# set current working directory
WORKDIR /app

# copy image in
COPY Fakebook.Profile.Domain/*.csproj Fakebook.Profile.Domain/
COPY Fakebook.Profile.DataAccess/*.csproj Fakebook.Profile.DataAccess/
COPY Fakebook.Profile.RestApi/*.csproj Fakebook.Profile.RestApi/
COPY *.sln ./
RUN dotnet restore

# copy the solutions and projects after restore
COPY . ./

# publish to parent folder
RUN dotnet publish -c Release -o ../publish

# ------------------------------------------------------------------------- #
# -----------------------<[ Runtime Docker Script ]>----------------------- #
# ------------------------------------------------------------------------- #

# get aspnet runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine as runtime

# set cwd to /app
WORKDIR /app

# copy the items in
COPY --from=build /app/publish ./

# configure it with the command it will use to start containers
CMD [ "dotnet", "/app/Fakebook.Profile.RestApi.dll" ]
