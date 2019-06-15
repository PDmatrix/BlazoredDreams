FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

RUN mkdir /output

# Copy project and publish

COPY . /app

WORKDIR /app
RUN dotnet publish --configuration Release --output /output

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime

WORKDIR /app

COPY --from=build-env /output .

ENTRYPOINT ["dotnet", "BlazoredDreams.API.dll"]
