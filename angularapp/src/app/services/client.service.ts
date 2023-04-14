import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Client } from '../shared/models/clientclass.models';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

// acá poner la varibal environment -.-
apiUrl = `${environment.apiUrl}/Client/`;

constructor(private http: HttpClient) { }

createClient(client: Client): Observable<Client> {
  console.log(client);
  return this.http.post<Client>(this.apiUrl, client);
}

getAllClient() {
  return this.http.get<Client[]>(this.apiUrl);
}

getClient (id: number): Observable<Client> {
  return this.http.get<Client>(`${this.apiUrl}${id}`);
}

updateClient(client: Client){
  console.log(client);
  return this.http.put<Client>(`${this.apiUrl}${client.personId}`, Client )
}

deleteClient(id: number) {
  return this.http.delete<Client>(`${this.apiUrl}${id}`);
}



}
