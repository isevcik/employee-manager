import { Component, ChangeDetectionStrategy, inject } from '@angular/core';
import { CountriesService, CountryDto } from '../../api';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzTableModule } from 'ng-zorro-antd/table';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-countries',
  templateUrl: './countries.html',
  styleUrl: './countries.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [AsyncPipe, NzTableModule],
})
export class CountriesComponent {
  private countriesService = inject(CountriesService);

  countries$: Observable<CountryDto[]> = this.countriesService.apiCountriesGet();
}
