import { Component, ChangeDetectionStrategy, inject } from '@angular/core';
import { JobCategoriesService, JobCategoryDto } from '../../api';
import { NzTableModule } from 'ng-zorro-antd/table';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-job-categories',
  templateUrl: './job-categories.html',
  styleUrl: './job-categories.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [AsyncPipe, NzTableModule],
})
export class JobCategoriesComponent {
  private jobCategoriesService = inject(JobCategoriesService);

  jobCategories$: Observable<JobCategoryDto[]> = this.jobCategoriesService.apiJobcategoriesGet();
}
