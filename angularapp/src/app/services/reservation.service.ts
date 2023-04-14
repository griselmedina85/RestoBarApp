import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject,Observable } from 'rxjs';
import { Client } from '../shared/models/clientclass.models';
import { environment } from '../../environments/environment';
import { Reservation } from '../shared/models/reservation.models';

@Injectable({
  providedIn: 'root'
})

export class ReservationsService {
    private enableButtonSubject = new BehaviorSubject<boolean>(true);
    public enableButtonSubject$ = this.enableButtonSubject.asObservable();
    // acá poner la varibal environment -.-
    apiUrl = `${environment.apiUrl}/Reservation/`;

    constructor(private http: HttpClient) { }

    ButtonSubscription() {
        this.enableButtonSubject.subscribe();
      }
    
      ButtonOn() {
        this.enableButtonSubject.next(false) ;
      }
    
      ButtonOff() {
        this.enableButtonSubject.next(true) ;
      }
    
      ButtonUnsusciption() {
        this.enableButtonSubject.unsubscribe();
      }
         

    getReservations(): Observable<Reservation[]>{
        return this.http.get<Reservation[]>(this.apiUrl);
      }
      atenderReserv(reserv: Reservation){
        console.log(reserv);
        return this.http.put<Reservation>(`${this.apiUrl}attended/${reserv.reservationId}`, reserv )
      }
      finReserv(reserv: Reservation){
        console.log(reserv);
        return this.http.put<Reservation>(`${this.apiUrl}finish/${reserv.reservationId}`, reserv )
      }
      cancelReserv(reserv: Reservation){
        console.log(reserv);
        return this.http.put<Reservation>(`${this.apiUrl}cancel/${reserv.reservationId}`, reserv )
      }
}



