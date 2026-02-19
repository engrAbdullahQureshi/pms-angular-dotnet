import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full',
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('./features/auth/auth-module').then((m) => m.AuthModule),
  },
  {
    path: 'projects',
    loadChildren: () =>
      import('./features/projects/projects-module').then((m) => m.ProjectsModule),
  },
  {
    path: 'tasks',
    loadChildren: () =>
      import('./features/tasks/tasks-module').then((m) => m.TasksModule),
  },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./features/dashboard/dashboard-module').then((m) => m.DashboardModule),
  },
  {
    path: '**',
    redirectTo: 'dashboard',
  },
];
