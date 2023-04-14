import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { Person } from 'src/app/shared/models/personclass.models';
import { EverybodyService } from '../everybody.service';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-everybody-list',
  templateUrl: './everybody-list.component.html',
  styleUrls: ['./everybody-list.component.css'],
})
export class EverybodyListComponent implements OnInit {
  @Input() vm!: Person[];
  @Output() selected = new EventEmitter<Person>();

  filter = new FormControl('', { nonNullable: true });
  selectedid!: number;

  constructor(private everybodyService: EverybodyService) {  }

  ngOnInit(): void {
    this.everybodyService.enableButtonSubject$.subscribe((val) =>
      console.log(val)
    );
  }

  select(user: Person) {
    this.selectedid = user.personId;
    this.selected.emit(user);
    this.everybodyService.ButtonOff();
    console.log('select');
  }

  editClient(user: Person, event: any): void {
    console.log(user);
    event.stopPropagation();
    this.selected.emit(user);
    this.everybodyService.ButtonOn();
    console.log('edit');
  }
}

