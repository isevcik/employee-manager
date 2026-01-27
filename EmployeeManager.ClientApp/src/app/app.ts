import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';

@Component({
  selector: 'app-root',
  imports: [RouterModule, RouterOutlet, NzLayoutModule, NzMenuModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App { }
