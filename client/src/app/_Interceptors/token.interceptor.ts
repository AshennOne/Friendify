import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, finalize } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { LoadingService } from '../_services/loading.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(
    private toastr: ToastrService,
    private router: Router,
    private loadingService: LoadingService
  ) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const myToken = localStorage.getItem('token');
    if (myToken) {
      request = request.clone({
        setHeaders: { Authorization: 'Bearer ' + myToken },
      });
    }

    this.loadingService.showLoading();

    return next.handle(request).pipe(
      catchError((err: any) => {
        if (err instanceof HttpErrorResponse) {
          if (err.status === 401) {
            localStorage.clear();
            this.toastr.warning('Token has expired, log in again');
            this.router.navigateByUrl('');
          }
        }
        throw err; 
      }),
      finalize(() => {
        this.loadingService.hideLoading(); 
      })
    );
  }
}
