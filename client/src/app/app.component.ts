import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'client';
  hideNavbar = true
  constructor(private router:Router){}
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
  }
  
}
