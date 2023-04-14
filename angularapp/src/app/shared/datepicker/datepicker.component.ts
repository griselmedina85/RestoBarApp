import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbAlertModule, NgbDatepickerModule, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { JsonPipe } from '@angular/common';

@Component({
  selector: 'app-datepicker',
  standalone: true,
  imports: [CommonModule, NgbDatepickerModule, NgbAlertModule, FormsModule, JsonPipe],
  templateUrl: './datepicker.component.html',
  styleUrls: ['./datepicker.component.css']
})
export class DatepickerComponent implements OnInit {
  @Input() mv!: Date;

  @Output() 
  clickedEvent: EventEmitter<Date> =  new EventEmitter<Date>();
  
  model!: NgbDateStruct;
  
  ngOnInit(): void {
    if (this.mv) {
      this.model =  {year: this.mv.getUTCFullYear(), month: this.mv.getUTCMonth(), day: this.mv.getUTCDay()};
    } else {
      this.model =  {year: 2000, month: 1, day: 1};
    }
  }

  onClicked(): void {
    let dateBirt = new Date();

    if (this.model) { // to Date
      dateBirt = new Date(this.model.year, this.model.month-1, this.model.day)
    }

    this.clickedEvent.emit(dateBirt);
  }


}


