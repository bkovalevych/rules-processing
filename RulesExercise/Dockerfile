#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["RulesExercise/RulesExercise.csproj", "RulesExercise/"]
COPY ["RulesExercise.Infrastructure/RulesExercise.Infrastructure.csproj", "RulesExercise.Infrastructure/"]
COPY ["RulesExercise.Application/RulesExercise.Application.csproj", "RulesExercise.Application/"]
COPY ["RulesExercise.Domain/RulesExercise.Domain.csproj", "RulesExercise.Domain/"]
RUN dotnet restore "RulesExercise/RulesExercise.csproj"
COPY . .
WORKDIR "/src/RulesExercise"
RUN dotnet build "RulesExercise.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RulesExercise.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RulesExercise.dll"]