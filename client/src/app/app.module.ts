import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { JwtModule } from '@auth0/angular-jwt';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoggerModule, NgxLoggerLevel } from 'ngx-logger';
import { environment } from './../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ContactEditComponent } from './components/contact-edit/contact-edit.component';
import { ContactListComponent } from './components/contact-list/contact-list.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { NavbarComponent } from './components/navbar/navbar.component';

@NgModule({
  declarations: [
    AppComponent,
    ContactListComponent,
    NavbarComponent,
    HomeComponent,
    ContactEditComponent,
    LoginComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    FormsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        allowedDomains: [new URL(environment.apiUrl).host],
        disallowedRoutes: [
          `${environment.apiUrl}/v1/users/authenticate`,
          `${environment.apiUrl}/v1/users/register`
        ],
        tokenGetter: () => localStorage.getItem('accessToken')
      }
    }),
    LoggerModule.forRoot({
      level: environment.production ? NgxLoggerLevel.WARN : NgxLoggerLevel.DEBUG
    }),
    NgbModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
