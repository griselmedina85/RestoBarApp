// reservation/reservation.component.ts

import { Component, EventEmitter, OnDestroy, OnInit, Output} from "@angular/core";
import { ReservationsService } from "../services/reservation.service";
import { Reservation } from "../shared/models/reservation.models";
import { FormControl } from '@angular/forms';

@Component({
  selector: "app-reservation",
  templateUrl: "reservation.component.html",
  styleUrls: ["reservation.component.css"],
})
export class ReservationComponent implements  OnInit, OnDestroy {
    reservations : Reservation[] = [];

    filtro = new FormControl('', { nonNullable: true });
    @Output() selected = new EventEmitter<Reservation>();


    constructor(public reservationService: ReservationsService) { }
    ngOnDestroy(): void {
        throw new Error("Method not implemented.");
    }

    ngOnInit() {
        // this.reservationService.getReservations().subscribe((data)=>{
        //     this.reservations = data;
        //     console.log(this.reservations);
        // });
        this.reservationService.ButtonSubscription();
        this.RefreshView();        
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
    RefreshView() {
        this.reservationService.getReservations().subscribe((data)=>{
                this.reservations = data;
                console.log(this.reservations);
        });
    }
    atenderReserva(reserv: Reservation, event: any): void {
        console.log(reserv);
        this.reservationService.atenderReserv(reserv).subscribe({
            next: () => {
              this.nextS();
            },
            error: (error) => {
              this.errorS(error);
            },
          });
    }
    finalizarReserva(reserv: Reservation, event: any): void {
      console.log(reserv);
      this.reservationService.finReserv(reserv).subscribe({
          next: () => {
            this.nextS();
          },
          error: (error) => {
            this.errorS(error);
          },
        });
  }

  cancelarReserva(reserv: Reservation, event: any): void {
    console.log(reserv);
    this.reservationService.cancelReserv(reserv).subscribe({
        next: () => {
          this.nextS();
        },
        error: (error) => {
          this.errorS(error);
        },
      });
  }
}