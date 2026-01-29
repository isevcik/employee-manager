import { Component, ChangeDetectionStrategy, inject, signal } from '@angular/core';
import { EmployeesService, EmployeeListDto } from '../../api';
import { NzTableModule } from 'ng-zorro-antd/table';
import { Observable, Subject } from 'rxjs';
import { AsyncPipe, DatePipe } from '@angular/common';
import { RouterLink } from "@angular/router";
import { finalize, delay, switchMap, startWith } from 'rxjs/operators';
import { NzDividerComponent } from 'ng-zorro-antd/divider';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.html',
  styleUrl: './employees.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [AsyncPipe, DatePipe, NzTableModule, RouterLink, NzDividerComponent, NzPopconfirmModule],
})
export class EmployeesComponent {
  private employeesService = inject(EmployeesService);

  loading = signal(true);
  private refreshTrigger$ = new Subject<void>();

  employees$: Observable<EmployeeListDto[]> = this.refreshTrigger$.pipe(
    startWith(undefined),
    switchMap(() => this.employeesService.apiEmployeesGet().pipe(
      finalize(() => this.loading.set(false))
    ))
  );

  deleteEmployee(id: number): void {
    this.loading.set(true);
    this.employeesService.apiEmployeesIdDelete(id as number).subscribe({
      next: () => {
        this.refreshTrigger$.next();
      },
      error: (error) => {
        console.error('Error deleting employee:', error);
        this.loading.set(false);
      }
    });
  }
}
