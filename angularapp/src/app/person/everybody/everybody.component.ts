import { Component, OnDestroy, OnInit } from '@angular/core';
import { PersonService } from 'src/app/services/person.service';
import { ClientService } from 'src/app/services/client.service';
import { Person } from 'src/app/shared/models/personclass.models';
import { Client } from 'src/app/shared/models/clientclass.models';
import { EverybodyService } from '../shared/everybody.service';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-everybody',
  templateUrl: './everybody.component.html',
  styleUrls: ['./everybody.component.css'],
})
export class EverybodyComponent implements OnInit, OnDestroy {
  users: Person[] = [];
  selectedUser!: Person;

  constructor(
    private readonly personService: PersonService,
    private readonly clientService: ClientService,
    private everybodyService: EverybodyService
  ) {}

  ngOnInit(): void {
    this.everybodyService.ButtonSubscription();
    this.RefreshView();
  }

  ngOnDestroy(): void {
    this.everybodyService.ButtonUnsusciption();
  }

  SelectedUser(user: Person): void {
    this.selectedUser = Person.clonePerson(user);
  }

  NewPerson() {
    const person = Person.createBlank();
    this.selectedUser = person;
    this.everybodyService.ButtonOn();
  }

  RefreshView() {
    this.personService.getAllPerson().subscribe((resp) => {
      this.users = resp;
    });
  }

  Save(valores: { form: FormGroup; id: number }): void {
    const isClient: boolean = valores.form.controls['isClient'].value;
    const isUser: boolean = valores.form.controls['isUser'].value;

    // let person = Person.createBlank();

    //   person.personName = valores.form.value.personName!;
    //   person.personLastName = valores.form.value.personLastName!;
    //   person.identityNumber = valores.form.value.identityNumber!;
    //   person.gender = valores.form.value.gender!;
    //   person.phone = valores.form.value.phone!;
    //   person.birthday = valores.form.value.birthday!;
    //   person.email = valores.form.value.email!;
    //   person.personId = valores.id;

    // let person = new Person(
    //   valores.id, valores.form.value.personLastName,
    //   valores.form.value.personName,
    //   valores.form.value.identityNumber,
    //   valores.form.value.birthday,
    //   valores.form.value.email,
    //   valores.form.value.phone,
    //   valores.form.value.gender);

    let person = Person.createPerson(valores);

    if (person.personId === 0) {
      this.personService.createPerson(person).subscribe({
        next: () => {
          this.nextS();
        },
        error: (error) => {
          this.errorS(error);
        },
      });
    } else {
      this.personService.updatePerson(person).subscribe({
        next: () => {
          this.nextS();
          this.AddClient(person);
        },
        error: (error) => {
          this.errorS(error);
        },
      });
      this.AddClient(person);
    }
  }

  Delete(personId: number): void {
    this.personService.deletePerson(personId).subscribe({
      next: () => {
        this.nextS();
      },
      error: (error) => {
        this.errorS(error);
      },
    });
  }

  nextS(): void {
    // this.loading = false;
    this.RefreshView();
    alert('Operación realizada con éxito');
  }

  errorS(error: any): void {
    // this.loading = false;
    alert(`Ocurrió un error ${error}`);
  }

  AddClient(person: Person) {
    this.clientService.getClient(person.personId).subscribe((x) => {
      // let cliente = Client.createBlank();
      // cliente.email = person.email;
      // cliente.personLastName = person.personLastName;
      // cliente.personName = person.personName;
      // cliente.personId = person.personId;

      let cliente = Client.createClient(person);

      if (x == null) {
        // nuevo cliente
        this.clientService.createClient(cliente).subscribe({
          next: () => {
            this.nextS();
          },
          error: (error) => {
            this.errorS(error);
          },
        });
      } else {
        //actualiza cliente
      }
    });
  }
}
