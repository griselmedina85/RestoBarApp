import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {
  private isLoading = new Subject<boolean>();

  isLoading$ = this.isLoading.asObservable();

  show(): void {
    return this.isLoading.next(true);
  }

  hidden(): void {
    return this.isLoading.next(false);
  }
}
