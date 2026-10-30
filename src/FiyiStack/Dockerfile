FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ["FiyiStack.csproj", "./"]

RUN dotnet restore "FiyiStack.csproj"

COPY . .

RUN dotnet publish "FiyiStack.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

EXPOSE 8080

COPY --from=build /app/publish .

USER root
RUN apt-get update && apt-get install -y --no-install-recommends curl && \
    rm -rf /var/lib/apt/lists/* && \
    mkdir -p /home/app/.aspnet/DataProtection-Keys && \
    chown -R app:app /home/app/.aspnet /app

USER app

HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
    CMD curl --fail http://localhost:8080/api/health || exit 1

ARG GIT_SHA=dev
ENV APP_VERSION=$GIT_SHA

ENTRYPOINT ["dotnet", "FiyiStack.dll"]
