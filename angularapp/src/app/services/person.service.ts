import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Person } from '../shared/models/personclass.models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})


export class PersonService {

  // acá poner la varibal environment -.-
  apiUrl = `${environment.apiUrl}/Person/`;

  constructor(private http: HttpClient) { }

  createPerson(person: Person): Observable<Person> {
    console.log(person);
    return this.http.post<Person>(this.apiUrl, person);
  }

  getAllPerson(): Observable<Person[]> {
    return this.http.get<Person[]>(this.apiUrl);
  }

  updatePerson(person: Person){
    console.log(person);
    return this.http.put<Person>(`${this.apiUrl}${person.personId}`, person )
  }

  deletePerson(id: number) {
    return this.http.delete<Person>(`${this.apiUrl}${id}`);
  }

}
