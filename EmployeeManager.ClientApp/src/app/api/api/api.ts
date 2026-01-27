export * from './countries.service';
import { CountriesService } from './countries.service';
export * from './employees.service';
import { EmployeesService } from './employees.service';
export * from './jobCategories.service';
import { JobCategoriesService } from './jobCategories.service';
export const APIS = [CountriesService, EmployeesService, JobCategoriesService];
