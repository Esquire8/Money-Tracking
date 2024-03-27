Для работы с миграциями необходимо установить:
- nuget-пакет Microsoft.EntityFrameworkCore.Design
- dotnet ef `dotnet tool install --global dotnet-ef`

Команды:
- Создание миграции `dotnet ef migrations add <название миграции>`
- Применение/накатывание миграции на БД `dotnet ef database update`