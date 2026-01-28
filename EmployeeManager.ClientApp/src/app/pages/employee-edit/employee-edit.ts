import { Component, ChangeDetectionStrategy, inject, effect, input } from '@angular/core';
import { EmployeesService, EmployeeGetDto, JobCategoriesService, JobCategoryDto, CountriesService, CountryDto } from '../../api';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.html',
  styleUrl: './employee-edit.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [ReactiveFormsModule, NzFormModule, NzInputModule, NzButtonModule, NzRadioModule, NzSelectModule, NzDividerModule, CommonModule],
})
export class EmployeeEditComponent {
  private employeesService = inject(EmployeesService);
  private jobCategoriesService = inject(JobCategoriesService);
  private countriesService = inject(CountriesService);
  private fb = inject(FormBuilder);

  employeeId = input.required<string>();
  jobCategories$: Observable<JobCategoryDto[]> = this.jobCategoriesService.apiJobcategoriesGet();
  countries$: Observable<CountryDto[]> = this.countriesService.apiCountriesGet();
  employees$: Observable<EmployeeGetDto[]> = this.employeesService.apiEmployeesGet();

  validateForm = this.fb.group({
    firstName: ['', [Validators.required]],
    middleName: [''],
    lastName: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
    phoneNumber: [''],
    birthDate: ['', [Validators.required]],
    gender: ['', [Validators.required]],
    jobCategories: [[] as number[]],
    salary: [null as number | null],
    superiorId: [null as number | null],
    street: [''],
    zipCode: [''],
    city: [''],
    countryId: [null as number | null],
    joinedDate: ['', [Validators.required]],
    exitedDate: [''],
  });

  constructor() {
    effect(() => {
      this.employeesService.apiEmployeesIdGet(Number(this.employeeId())).subscribe(employee => {
        this.validateForm.patchValue({
          firstName: employee.firstName || '',
          middleName: employee.middleName || '',
          lastName: employee.lastName || '',
          email: employee.email || '',
          phoneNumber: employee.phoneNumber || '',
          birthDate: employee.birthDate || '',
          gender: employee.gender || '',
          jobCategories: employee.jobCategories?.map(jc => jc.id).filter((id): id is number => id !== undefined) || [],
          salary: employee.salary?.amount as number | null || null,
          superiorId: employee.superiorId as number | null || null,
          street: employee.address?.street || '',
          zipCode: employee.address?.zipCode || '',
          city: employee.address?.city || '',
          countryId: employee.address?.countryId as number | null || null,
          joinedDate: employee.joinedDate || '',
          exitedDate: employee.exitedDate || '',
        });
      });
    });
  }

  submitForm(): void {
    if (this.validateForm.valid) {
      console.log('Form submitted:', this.validateForm.value);
    } else {
      Object.values(this.validateForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
    }
  }
}
