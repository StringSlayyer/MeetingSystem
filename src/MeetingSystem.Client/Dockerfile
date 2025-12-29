# -----------------------------------------------------------------------------
# Stage 1: Build
# -----------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# 1. COPY ONLY THE PROJECTS WE NEED
# Left Side (Host): Must have "src/" because your real files are in the src folder.
# Right Side (Container): We remove "src/" to make the container clean.
COPY ["src/MeetingSystem.Client/MeetingSystem.Client.csproj", "MeetingSystem.Client/"]
COPY ["src/MeetingSystem.Contracts/MeetingSystem.Contracts.csproj", "MeetingSystem.Contracts/"]
COPY ["src/SharedKernel/SharedKernel.csproj", "SharedKernel/"]


# 2. RESTORE
# Now we restore using the clean internal paths (no more /src/src nonsense)
RUN dotnet restore "MeetingSystem.Client/MeetingSystem.Client.csproj"

# 3. COPY SOURCE CODE
# Instead of "COPY . .", we copy only the two folders we actually need.
COPY ["src/MeetingSystem.Client/", "MeetingSystem.Client/"]
COPY ["src/MeetingSystem.Contracts/", "MeetingSystem.Contracts/"]
COPY ["src/SharedKernel/", "SharedKernel/"]

# 4. BUILD
WORKDIR "/app/MeetingSystem.Client"
RUN dotnet build "MeetingSystem.Client.csproj" -c Release -o /app/build

# -----------------------------------------------------------------------------
# Stage 2: Publish
# -----------------------------------------------------------------------------
FROM build AS publish
RUN dotnet publish "MeetingSystem.Client.csproj" -c Release -o /app/publish

# -----------------------------------------------------------------------------
# Stage 3: Final (Nginx)
# -----------------------------------------------------------------------------
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# Copy the published static files
COPY --from=publish /app/publish/wwwroot .

# Copy Nginx config (Using the specific source path)
COPY src/MeetingSystem.Client/nginx.conf /etc/nginx/nginx.conf

EXPOSE 80