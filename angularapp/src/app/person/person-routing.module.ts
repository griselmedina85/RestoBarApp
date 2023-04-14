import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { UserComponent } from './user/user.component';

import { EverybodyComponent } from './everybody/everybody.component';

const routes: Routes = [
  { path: '', component: EverybodyComponent},
  { path: 'user', component: UserComponent},
  // { path: 'person', component: PersonComponent},
  // { path: 'client', component: ClientComponent},
  // { path: 'employee', component: EmployeeComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PersonRoutingModule { }
