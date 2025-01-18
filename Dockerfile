# Використовуємо офіційний образ .NET SDK для побудови
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Встановлюємо Git
RUN apt-get update && apt-get install -y git && apt-get clean

# Копіюємо всі файли до контейнера
COPY . .

# Відновлюємо залежності та збираємо проєкт
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Використовуємо Runtime-образ для запуску програми
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

# Відкриваємо порт
EXPOSE 5000

# Вказуємо команду запуску
ENTRYPOINT ["dotnet", "MyApp.dll"]
