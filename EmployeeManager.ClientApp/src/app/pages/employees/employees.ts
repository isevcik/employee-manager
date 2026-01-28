import { Component, ChangeDetectionStrategy, inject, signal } from '@angular/core';
import { EmployeesService, EmployeeGetDto } from '../../api';
import { NzTableModule } from 'ng-zorro-antd/table';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { RouterLink } from "@angular/router";
import { tap, finalize, delay } from 'rxjs/operators';
import { NzDividerComponent } from 'ng-zorro-antd/divider';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.html',
  styleUrl: './employees.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [AsyncPipe, NzTableModule, RouterLink, NzDividerComponent],
})
export class EmployeesComponent {
  private employeesService = inject(EmployeesService);

  loading = signal(true);

  employees$: Observable<EmployeeGetDto[]> = this.employeesService.apiEmployeesGet().pipe(
    delay(1000),
    finalize(() => this.loading.set(false))
  );
}
