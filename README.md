

Веб-приложение на ASP.NET Core 8 (Blazor Server) для каталога, управления и покупки строительных материалов. 
Проект разработан в рамках курсовой работы и практических занятий по контейнеризации, работе с данными и современным веб-технологиям.

#  Технологии
- Framework: .NET 8, ASP.NET Core, Blazor Server
- База данных: Entity Framework Core (CodeFirst), SQLite
- Валидация: FluentValidation
- Контейнеризация: Docker (multi-stage build), Docker Compose
- Контроль версий: Git, GitHub

#  Основные функции
- Каталог товаров с фильтрацией по категориям
- Корзина с возможностью изменения количества и удаления товаров
- Регистрация и вход пользователей 
- Валидация форм на клиенте и сервере
-  Полная контейнеризация с сохранением данных (SQLite volume)
-  Healthcheck для мониторинга состояния контейнера

#  Требования
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Git](https://git-scm.com/)


# Запуск через github
- bash
# 1. Клонирование репозитория
git clone https://github.com/BelovAP/BuildingStore.Web.git
cd BuildingStore.Web
# 2. Восстановление зависимостей
dotnet restore
# 3. Применение миграций БД
dotnet ef database update
# 4. Запуск приложения
dotnet run


# Запуск через Docker
- bash
# 1. Убедитесь, что Docker Desktop запущен
# 2. Соберите и запустите контейнеры
docker compose up --build -d
# 3. Проверьте статус
docker compose ps

# Ссылки
GitHub репозиторий: https://github.com/BelovAP/BuildingStore.Web
Docker Hub образ: https://hub.docker.com/r/belovap/buildingstoreapi

# Автор
ФИО: Белов Артем Павлович
Группа: ББСО-01-24
Вариант: 2