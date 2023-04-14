import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EverybodyService {
  private enableButtonSubject = new BehaviorSubject<boolean>(true);
  public enableButtonSubject$ = this.enableButtonSubject.asObservable();

  constructor() { }

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





}
