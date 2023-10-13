import { ChangeDetectorRef, Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { ngxLoadingAnimationTypes } from 'ngx-loading';
import { LoadingService } from './_services/loading.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'client';
  hideNavbar = true
  isLoading = false;
  public ngxLoadingAnimationTypes = ngxLoadingAnimationTypes;
  public primaryColour = '#ffffff';
  public secondaryColour = '#ccc';
  constructor(private router:Router,private loadingService:LoadingService,private changeDetector: ChangeDetectorRef){}
  ngOnInit() {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        if (event.url === '/' || event.url === '/forgetpassword' || event.url === '/confirmemail') { 
          this.hideNavbar = true;
        } else {
          this.hideNavbar = false;
        }
      }
    });
    this.loadingService.loading$.subscribe({
      next: (bool)=> {
        this.isLoading = bool;
      }
    })
  }
  ngAfterContentChecked(): void {
    this.changeDetector.detectChanges();
  }
  
}
