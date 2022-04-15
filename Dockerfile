FROM mcr.microsoft.com/dotnet/aspnet:6.0
ENV TZ=Europe/Amsterdam
WORKDIR /srv
COPY . .
ENTRYPOINT ["dotnet", "WalletProxy.dll"]