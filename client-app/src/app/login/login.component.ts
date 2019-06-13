import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/services/login.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public authToken = '';

  constructor(private loginService: LoginService) {
  }

  ngOnInit() {

  }

  onSubmitLogin(form: NgForm) {
      this.loginService.getAuthToken(form.value).subscribe(token => {
          this.authToken = token;   });
  }

}
