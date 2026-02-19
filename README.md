# Project Management System (PMS)

A full-stack web-based Project Management System for small-to-medium teams (5–50 users).
Built for portfolio and learning using Angular + ASP.NET Core Web API + SQL database.

## Tech Stack
- Frontend: Angular
- Backend: ASP.NET Core Web API
- Database: MySQL (EF Core)
- Authentication: JWT + Role-Based Access Control (RBAC)

## MVP Features
- Authentication (Register/Login) with JWT
- RBAC roles: Admin, Manager, Member
- Project Management (CRUD + members)
- Task Management (CRUD + assignment + status workflow)
- Task comments
- Dashboard summary

## Architecture (High Level)
Angular UI → Web API (Controllers) → Services → Repositories → Database

## Project Structure
- /frontend: Angular app
- /backend: .NET solution (API + Application + Domain + Infrastructure)
- /docs: SRS, API notes, diagrams, screenshots

## How to Run (Development)
### Backend
1. Navigate to `/backend`
2. Configure DB connection string
3. Run migrations
4. Start API

### Frontend
1. Navigate to `/frontend`
2. Install dependencies
3. Run Angular app
