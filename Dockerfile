# Use the official .NET 8 SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set working directory
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY MemberService.Api/*.csproj ./MemberService.Api/
COPY MemberService.Tests/*.csproj ./MemberService.Tests/
RUN dotnet restore ./MemberService.Api/MemberService.Api.csproj

# Copy everything and build
COPY . .
RUN dotnet publish MemberService.Api/MemberService.Api.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose default port
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "MemberService.Api.dll"]