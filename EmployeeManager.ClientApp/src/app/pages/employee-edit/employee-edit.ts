import { Component, ChangeDetectionStrategy, inject, effect, input } from '@angular/core';
import { EmployeesService, EmployeeGetDto } from '../../api';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.html',
  styleUrl: './employee-edit.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [ReactiveFormsModule, NzFormModule, NzInputModule, NzButtonModule],
})
export class EmployeeEditComponent {
  private employeesService = inject(EmployeesService);
  private fb = inject(FormBuilder);

  employeeId = input.required<string>();

  validateForm = this.fb.group({
    firstName: ['', [Validators.required]],
    middleName: [''],
    lastName: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
    phoneNumber: [''],
    birthDate: ['', [Validators.required]],
    gender: ['', [Validators.required]],
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
