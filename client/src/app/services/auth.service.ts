import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { AuthToken } from '../models/auth-token.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = `${environment.apiUrl}/v1/users`;

  constructor(
    private http: HttpClient,
    private jwtHelperService: JwtHelperService
  ) { }

  isAuthenticated(): boolean {
    const accessToken = localStorage.getItem('accessToken') ?? undefined;
    return !this.jwtHelperService.isTokenExpired(accessToken);
  }

  login(emailAddress: string, password: string): Observable<AuthToken> {
    return this.http.post<AuthToken>(`${this.baseUrl}/authenticate`, { emailAddress, password })
      .pipe(tap((authToken) => {
        localStorage.setItem('accessToken', authToken.accessToken);
      }));
  }

  logout(): void {
    localStorage.removeItem('accessToken');
  }

  register(emailAddress: string, password: string, confirmPassword: string): Observable<boolean> {
    return this.http.post<boolean>(`${this.baseUrl}/register`, { emailAddress, password, confirmPassword })
      .pipe(tap(() => this.login(emailAddress, password)));
  }
}
