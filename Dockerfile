# Використовуємо офіційний .NET SDK для побудови
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копіюємо все до контейнера
COPY . .

# Відновлюємо залежності та збираємо проєкт
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Використовуємо Runtime-образ для запуску програми
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Вказуємо порт (наприклад, 5000)
EXPOSE 5000

# Вказуємо команду для запуску
ENTRYPOINT ["dotnet", "MyApp.dll"]
