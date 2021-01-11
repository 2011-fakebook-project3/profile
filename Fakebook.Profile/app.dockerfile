#################- Build and Publish -#####################

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

COPY . ./

RUN dotnet publish Fakebook.Profile.RestApi -c Release -o ../publish

#################- Package Assemblies -###################

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS runtime

WORKDIR /app

COPY --from=build /app/publish ./

CMD dotnet Fakebook.Profile.RestApi.dll
