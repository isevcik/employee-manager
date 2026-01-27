import { Component, ChangeDetectionStrategy, inject } from '@angular/core';
import { EmployeesService, EmployeeGetDto } from '../../api';
import { NzTableModule } from 'ng-zorro-antd/table';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-employees',
  templateUrl: './employees.html',
  styleUrl: './employees.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [AsyncPipe, NzTableModule, RouterLink],
})
export class EmployeesComponent {
  private employeesService = inject(EmployeesService);

  employees$: Observable<EmployeeGetDto[]> = this.employeesService.apiEmployeesGet();
}
