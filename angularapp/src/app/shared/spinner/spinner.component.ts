import { Component, OnInit } from '@angular/core';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.css']
})
export class SpinnerComponent implements OnInit {
  loading$ = this.loaderService.isLoading$;

  constructor(private loaderService: LoaderService) { }

  ngOnInit(): void {
  }

}
