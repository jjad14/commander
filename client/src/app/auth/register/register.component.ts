import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs';

import { AuthService } from '../auth.service';
import { MustMatch } from '../../shared/helpers/must-match.validator';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit, OnDestroy {
  registerForm: FormGroup;

  submitted = false;

  constructor(public authService: AuthService, private fb: FormBuilder) {}

  ngOnInit() {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        userName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', Validators.required]
    }, {
        validator: MustMatch('password', 'confirmPassword')
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.registerForm.controls; }

  // passing form values to create user
  onSubmit() {
    this.submitted = true;

    if (this.registerForm.invalid) {
        return;
    }
      // this.authService.createUser(
      //     this.form.value.username,
      //     this.form.value.email,
      //     this.form.value.firstName,
      //     this.form.value.lastName,
      //     this.form.value.password
      // );
  }


  ngOnDestroy() {
      // this.authStatuSub.unsubscribe();
  }
}
