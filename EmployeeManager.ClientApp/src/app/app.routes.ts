import { Routes } from '@angular/router';
import { CountriesComponent } from './pages/countries/countries';
import { EmployeesComponent } from './pages/employees/employees';
import { JobCategoriesComponent } from './pages/job-categories/job-categories';

export const routes: Routes = [
  { path: 'employees', component: EmployeesComponent },
  { path: 'countries', component: CountriesComponent },
  { path: 'job-categories', component: JobCategoriesComponent },

  { path: '', redirectTo: '/employees', pathMatch: 'full' },
];
