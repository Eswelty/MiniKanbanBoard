# Mini Kanban Board

Веб-приложение для управления задачами и пользователями
с использованием Kanban-доски.

## Технологии

- Backend: C#, ASP.NET Core Web API, .NET 8
- Frontend: Angular, TypeScript, HTML, CSS
- Database: PostgreSQL 16
- ORM: Entity Framework Core
- API Documentation: Swagger
- Version Control: Git

## Функциональность

- CRUD пользователей
- CRUD задач
- Назначение исполнителей
- Kanban-доска с тремя статусами:
  To Do, In Progress, Done
- Изменение статуса задач
- Фильтрация задач по исполнителю
- Валидация входных данных

## Архитектура

Backend реализован с использованием
трёхслойной архитектуры:

Controller → Service → Repository → PostgreSQL

База данных разработана по подходу SQL First.
Модели и DbContext сгенерированы с помощью
Entity Framework Core Scaffolding.

## Структура проекта

- backend/ — серверная часть
- frontend/ — клиентская часть
- database/ — SQL-скрипты
  - 01_schema.sql — структура базы данных
  - 02_seed.sql — тестовые данные
  - 03_joins.sql — примеры четырёх видов JOIN

## Инструкция по запуску

### 1. База данных

Создать в PostgreSQL базу данных mini_kanban.

Выполнить в ней скрипты:
1. database/01_schema.sql
2. database/02_seed.sql

Скрипт 03_joins.sql содержит демонстрацию
INNER, LEFT, RIGHT и FULL OUTER JOIN.

### 2. Backend

Из корневой папки проекта выполнить:

```powershell
cd backend/Kanban.Api
dotnet restore
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:KanbanDb" "Host=localhost;Port=5432;Database=mini_kanban;Username=postgres;Password=YOUR_PASSWORD"
dotnet dev-certs https --trust
dotnet run --launch-profile https
```

Вместо YOUR_PASSWORD необходимо указать
свой пароль PostgreSQL.

Swagger:
https://localhost:7222/swagger

### 3. Frontend

В отдельном терминале из корневой папки:

```powershell
cd frontend
npm ci
npm start
```

Адрес приложения:
http://localhost:4200

## API

| HTTP Method | Endpoint | Operation |
|---|---|---|
| GET | /api/users | Get all users |
| GET | /api/users/{id} | Get user |
| POST | /api/users | Create user |
| PUT | /api/users/{id} | Update user |
| DELETE | /api/users/{id} | Delete user |
| GET | /api/tasks | Get all tasks |
| GET | /api/tasks/{id} | Get task |
| POST | /api/tasks | Create task |
| PUT | /api/tasks/{id} | Update task |
| DELETE | /api/tasks/{id} | Delete task |

Фильтрация задач по исполнителю:

GET /api/tasks?assigneeId=1
