import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';

type NewType = Router;

@Injectable({
  providedIn: 'root',
})
export class AntiAuthGuard implements CanActivate {
  constructor(private toastr: ToastrService, private router: Router) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    var token = localStorage.getItem('token');
    if (token == null && token == undefined) return true;
    else {
      this.router.navigateByUrl('/main');
      return false;
    }
  }
}
