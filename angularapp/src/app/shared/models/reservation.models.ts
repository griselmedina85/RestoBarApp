export class Reservation {
  private constructor(
    public reservationId: number = 0,
    public numberDiners: number = 0,
    public date: Date = new Date(),
    public time: string = '',
    public attended: false,
    public finishedMeal: false,
    public cancelation: false,
    public reasonCancelation: string = '',
    public restaurantId: number = 0,
    public clientId: number = 0,
    public personName: string = '',
    public personLastName: string = '',
    public tableId: number = 0
  ){  }


}
