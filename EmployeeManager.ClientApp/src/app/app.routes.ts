import { Routes } from '@angular/router';
import { EmployeesComponent } from './employees/employees';
import { CountriesComponent } from './countries/countries';
import { JobCategoriesComponent } from './job-categories/job-categories';

export const routes: Routes = [
  { path: 'employees', component: EmployeesComponent },
  { path: 'countries', component: CountriesComponent },
  { path: 'job-categories', component: JobCategoriesComponent }
];
