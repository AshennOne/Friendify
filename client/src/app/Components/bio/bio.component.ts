import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/User';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-bio',
  templateUrl: './bio.component.html',
  styleUrls: ['./bio.component.css']
})
export class BioComponent implements OnChanges {
@Input() bio = ""
tempbio = ""
isBeingEdited = false
@Input() isCurrentUser = false;
  constructor(private userService:UserService,private toastr:ToastrService) { }
  ngOnChanges(changes: SimpleChanges): void {
    if(changes['bio'])
    this.tempbio = this.bio;
  }

 
editBio(){
  this.isBeingEdited = !this.isBeingEdited
}
cancel(){
  this.tempbio = this.bio
  this.isBeingEdited = false
}
save(){
  if(this.bio != this.tempbio){
    this.bio = this.tempbio
    this.isBeingEdited = false
    var editedUser:User = {
      bio: this.tempbio
    } as User
    this.userService.editUser(editedUser).subscribe({
      next:()=>{
        this.toastr.success("Your bio has been updated")
      }
    })
  }
 
}
}
