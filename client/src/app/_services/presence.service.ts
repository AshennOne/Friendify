import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PresenceService {
  hubUrl = environment.hubUrl;
  private hubConnection?: HubConnection;
  private userIsOnline: boolean = false;

  constructor(private toastr: ToastrService) {}

  createHubConnection() {
    var token = localStorage.getItem('token');
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'presence', {
        accessTokenFactory: () => token || '',
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start().catch((error) => console.log(error));

    this.hubConnection.on('UserIsOnline', (username) => {
      if (!this.userIsOnline) {
        this.toastr.info(username + ' has connected');
      }
      this.userIsOnline = true;
    });

    this.hubConnection.on('UserIsOffline', (username) => {
      setTimeout(() => {
        if (!this.userIsOnline) {
          this.toastr.info(username + ' has disconnected');
        }
      }, 5000);

      this.userIsOnline = false;
    });
  }

  stopHubConnection() {
    this.hubConnection?.stop().catch((error) => console.log(error));
  }

  isHubConnection() {
    return !!this.hubConnection;
  }
}
