FROM mcr.microsoft.com/dotnet/runtime:5.0-focal AS base
WORKDIR /app
#RUN mkdir /app/video/ && cp /home/lamborge/Projects/bottg/video/shmil.mp4 /app/video/

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app && mkdir /app/video/  /app/ascii/ /app/audio
COPY ./video/shmil.mp4 /app/video/
COPY ./ascii/ /app/ascii/
COPY ./audio/ /app/audio/
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["bot_tg_sharp.csproj", "./"]
RUN dotnet restore "bot_tg_sharp.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "bot_tg_sharp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "bot_tg_sharp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "bot_tg_sharp.dll"]


