import { Component, OnInit, OnDestroy } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  private authStatuSub: Subscription;

  constructor(public authService: AuthService) {}

  ngOnInit() {
      // this.authStatuSub = this.authService.getAuthStatus().subscribe(
      //     authStatus => {
      //         this.isLoading = false;
      //     }
      // );
  }

  onLogin(form: NgForm) {
      // if (form.invalid) {
      //     return;
      // }
      // this.authService.loginUser(form.value.email, form.value.password);
  }

  ngOnDestroy() {
      // this.authStatuSub.unsubscribe();
  }

}
