FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /RobotApp

COPY /RobotApp/RobotApp.csproj .
RUN dotnet restore "RobotApp.csproj"

COPY /RobotApp .
RUN dotnet build "RobotApp.csproj" -c release -o /build

FROM build-env AS publish
RUN dotnet publish "RobotApp.csproj" -c Release -o /publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

COPY --from=publish /publish/wwwroot /usr/local/webapp/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf