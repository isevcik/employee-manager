import { Component, ChangeDetectionStrategy, inject, effect, input, signal } from '@angular/core';
import { EmployeesService, EmployeeGetDto, JobCategoriesService, JobCategoryDto, CountriesService, CountryDto } from '../../api';
import { NzDescriptionsModule } from 'ng-zorro-antd/descriptions';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { Observable } from 'rxjs';
import { CommonModule, DatePipe } from '@angular/common';

@Component({
    selector: 'app-employee-detail',
    templateUrl: './employee-detail.html',
    styleUrl: './employee-detail.css',
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [NzDescriptionsModule, NzDividerModule, NzTagModule, CommonModule, DatePipe],
})
export class EmployeeDetailComponent {
    private employeesService = inject(EmployeesService);
    private jobCategoriesService = inject(JobCategoriesService);
    private countriesService = inject(CountriesService);

    employeeId = input.required<string>();
    employee = signal<EmployeeGetDto | null>(null);
    jobCategories = signal<JobCategoryDto[]>([]);
    countries = signal<CountryDto[]>([]);
    employees = signal<EmployeeGetDto[]>([]);

    constructor() {
        this.jobCategoriesService.apiJobcategoriesGet().subscribe(jc => this.jobCategories.set(jc));
        this.countriesService.apiCountriesGet().subscribe(c => this.countries.set(c));
        this.employeesService.apiEmployeesGet().subscribe(e => this.employees.set(e));

        effect(() => {
            this.employeesService.apiEmployeesIdGet(Number(this.employeeId())).subscribe(employee => {
                this.employee.set(employee);
            });
        });
    }

    getCountryName(countryId: number | undefined): string {
        if (!countryId) return '-';
        const country = this.countries().find(c => c.id === countryId);
        return country?.name || '-';
    }

    getSuperiorName(superiorId: number | undefined): string {
        if (!superiorId) return '-';
        const superior = this.employees().find(e => e.id === superiorId);
        if (!superior) return '-';
        return `${superior.firstName || ''} ${superior.lastName || ''}`.trim();
    }
}
