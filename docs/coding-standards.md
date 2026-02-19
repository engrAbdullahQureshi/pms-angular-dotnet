# Coding Standards

## Backend (.NET)
- Controllers: PascalCase (e.g., ProjectsController)
- Services: PascalCase (e.g., ProjectService)
- Interfaces: prefix I (e.g., IProjectService)
- DTOs:
  - Requests: CreateProjectRequest
  - Responses: ProjectDto
- Async methods end with Async
- Use DTOs in controllers (do not expose entities directly)

## Frontend (Angular)
- Feature modules in /features (auth, projects, tasks, dashboard)
- Components: kebab-case files (project-list.component.ts)
- Services: *.service.ts
- Observables use `$` suffix (projects$)
