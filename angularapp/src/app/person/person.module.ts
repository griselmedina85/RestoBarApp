import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';


// component
import { PersonRoutingModule } from './person-routing.module';
import { UserComponent } from './user/user.component';

// component extras
import { DatepickerComponent } from '../shared/datepicker/datepicker.component';
import { EverybodyComponent } from './everybody/everybody.component';
import { EverybodyListComponent } from './shared/everybody-list/everybody-list.component';
import { EverybodyFormComponent } from './shared/everybody-form/everybody-form.component';
import { FilterPipe } from './shared/filter.pipe';


@NgModule({
  declarations: [
    UserComponent,
    EverybodyComponent,
    EverybodyListComponent,
    EverybodyFormComponent,
    FilterPipe,
  ],
  imports: [
    CommonModule,
    PersonRoutingModule,
    DatepickerComponent,
    ReactiveFormsModule
  ]
})
export class PersonModule { }
