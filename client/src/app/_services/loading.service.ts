import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {

  constructor() { }
  private loadingCount = 0;
  loadingSource = new Subject<boolean>()
  loading$ = this.loadingSource.asObservable();
  showLoading(): void {
    this.loadingCount++;
    this.loadingSource.next(true);
  }

  hideLoading(): void {
    this.loadingCount--;
    if (this.loadingCount <= 0) {
      this.loadingCount = 0;
      this.loadingSource.next(false);
    }
  }
}
