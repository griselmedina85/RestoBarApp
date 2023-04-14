import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
// import { AppComponent } from './app.component';
// import { PersonComponent } from './person/person/person.component';
// import { ClientComponent } from './person/client/client.component';
// import { EmployeeComponent } from './person/employee/employee.component';
// import { UserComponent } from './person/user/user.component';
// import { PersonModule } from './person/person.module';
import { LoginComponent } from './login/login.component';
import { ReservationComponent } from './reservation/reservation.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  /*{ path: 'shareForm', component: FormPersonaABMComponent}*/

    {
      path: 'person',
      loadChildren: () => import('./person/person.module').then(m => m.PersonModule)
    },

    { path: 'reservation', component: ReservationComponent },


  ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
