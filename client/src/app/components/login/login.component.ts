import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { NGXLogger } from 'ngx-logger';
import { AlertType } from 'src/app/shared/alert-type';
import { Login } from '../../models/login.model';
import { Alert } from '../../shared/alert';
import { AuthService } from './../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  @ViewChild('loginForm') loginForm?: NgForm;

  alert?: Alert;
  model: Login = new Login();

  constructor(
    private logger: NGXLogger,
    private router: Router,
    private authService: AuthService
  ) {}

  ngOnInit(): void {}

  isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }

  onFormSubmit(): void {
    this.authService
      .login(this.model.emailAddress, this.model.password)
      .subscribe(
        () => {
          this.router.navigateByUrl('/');
        },
        (error: HttpErrorResponse) => {
          this.logger.error('AuthService.login', this.model, error.error);
          this.alert = new Alert(
            error.error.message ?? 'An unknown error occurred.',
            AlertType.DANGER
          );
        }
      );
  }
}
