import { Component, ChangeDetectionStrategy, inject, effect, input } from '@angular/core';
import { EmployeesService, EmployeeGetDto, JobCategoriesService, JobCategoryDto, CountriesService, CountryDto } from '../../api';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzMessageService } from 'ng-zorro-antd/message';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.html',
  styleUrl: './employee-edit.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [ReactiveFormsModule, NzFormModule, NzInputModule, NzButtonModule, NzRadioModule, NzSelectModule, NzDividerModule, NzDatePickerModule, CommonModule],
})
export class EmployeeEditComponent {
  private employeesService = inject(EmployeesService);
  private jobCategoriesService = inject(JobCategoriesService);
  private countriesService = inject(CountriesService);
  private fb = inject(FormBuilder);
  private message = inject(NzMessageService);

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
    birthDate: [null as Date | null, [Validators.required]],
    gender: ['', [Validators.required]],
    jobCategories: [[] as number[]],
    salary: [null as number | null],
    superiorId: [null as number | null],
    countryId: [null as number | null],
    address: this.fb.group({
      street: [''],
      zipCode: [''],
      city: [''],
      countryId: [null as number | null],
    }),
    joinedDate: [null as Date | null, [Validators.required]],
    exitedDate: [null as Date | null],
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
          birthDate: employee.birthDate ? new Date(employee.birthDate) : null,
          gender: employee.gender || '',
          jobCategories: employee.jobCategories?.map(jc => jc.id).filter((id): id is number => id !== undefined) || [],
          salary: employee.salary?.amount as number | null || null,
          superiorId: employee.superiorId as number | null || null,
          countryId: employee.address?.countryId as number | null || null,
          address: {
            street: employee.address?.street || '',
            zipCode: employee.address?.zipCode || '',
            city: employee.address?.city || '',
            countryId: employee.address?.countryId as number | null || null,
          },
          joinedDate: employee.joinedDate ? new Date(employee.joinedDate) : null,
          exitedDate: employee.exitedDate ? new Date(employee.exitedDate) : null,
        });
      });
    });
  }

  submitForm(): void {
    if (this.validateForm.valid) {
      const formValue = this.validateForm.value;
      const employeeUpdateDto = {
        id: Number(this.employeeId()),
        firstName: formValue.firstName || '',
        middleName: formValue.middleName || null,
        lastName: formValue.lastName || '',
        birthDate: formValue.birthDate?.toISOString() || '',
        gender: formValue.gender || '',
        countryId: formValue.countryId || null,
        address: {
          street: formValue.address?.street || '',
          zipCode: formValue.address?.zipCode || '',
          city: formValue.address?.city || '',
          countryId: formValue.address?.countryId || undefined,
        },
        email: formValue.email || '',
        phoneNumber: formValue.phoneNumber || null,
        joinedDate: formValue.joinedDate?.toISOString() || '',
        exitedDate: formValue.exitedDate?.toISOString() || null,
        superiorId: formValue.superiorId || null,
        salary: formValue.salary || 0,
        jobCategories: formValue.jobCategories?.map(id => ({ id, title: '' })) || [],
      };

      this.employeesService.apiEmployeesIdPut(Number(this.employeeId()), employeeUpdateDto).subscribe({
        next: () => {
          this.message.success('Employee updated successfully');
        },
        error: (error) => {
          console.error('Error updating employee:', error);
          this.message.error('Failed to update employee');
        }
      });
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
