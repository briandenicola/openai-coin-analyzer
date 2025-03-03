FROM mcr.microsoft.com/dotnet/sdk:9.0-noble AS builder

WORKDIR /src/
COPY . .
RUN dotnet restore 
RUN dotnet publish -c Release --nologo -o publish/linux -r linux-x64 --self-contained -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true

#Build runtime image
FROM mcr.microsoft.com/dotnet/runtime-deps:9.0-azurelinux3.0-distroless

WORKDIR /app
COPY --from=builder /src/publish/linux/api .
COPY appsettings.json .
EXPOSE 5501
ENTRYPOINT ["/app/api"]