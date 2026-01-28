import { Routes } from '@angular/router';
import { CountriesComponent } from './pages/countries/countries';
import { EmployeesComponent } from './pages/employees/employees';
import { JobCategoriesComponent } from './pages/job-categories/job-categories';
import { EmployeeEditComponent } from './pages/employee-edit/employee-edit';
import { EmployeeDetailComponent } from './pages/employee-detail/employee-detail';

export const routes: Routes = [
  { path: 'employees', component: EmployeesComponent },
  { path: 'employees/:employeeId/detail', component: EmployeeDetailComponent },
  { path: 'employees/:employeeId/edit', component: EmployeeEditComponent },
  { path: 'countries', component: CountriesComponent },
  { path: 'job-categories', component: JobCategoriesComponent },

  { path: '', redirectTo: '/employees', pathMatch: 'full' },
];
