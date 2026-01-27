import { Component, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.html',
  styleUrl: './employees.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EmployeesComponent {
}
