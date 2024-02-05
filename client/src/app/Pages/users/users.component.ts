import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/User';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
})
export class UsersComponent implements OnInit {
  users: User[] = [];
  wasSearched = false
  searchstring: string = '';
  constructor(private userService: UserService) {
    this.getAll();
  }

  getAll() {
    this.userService.getUsers().subscribe({
      next: (users) => {
        this.users = users;
      },
    });
  }
  ngOnInit(): void {}
  search() {
    this.userService.searchForUsers(this.searchstring).subscribe({
      next: (users) => {
        this.users = users;
        this.wasSearched = true
      },
    });
  }
  reset() {
    this.searchstring = '';
    this.getAll();
    this.wasSearched = false
  }
}
