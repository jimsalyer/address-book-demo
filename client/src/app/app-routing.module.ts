import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactEditComponent } from './components/contact-edit/contact-edit.component';
import { ContactListComponent } from './components/contact-list/contact-list.component';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'contacts', component: ContactListComponent, canActivate: [AuthGuard] },
  { path: 'contacts/new', component: ContactEditComponent },
  { path: 'contacts/:id', component: ContactEditComponent },
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
