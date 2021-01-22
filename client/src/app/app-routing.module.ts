import { ContactUpdateComponent } from './components/contact-update/contact-update.component';
import { ContactAddComponent } from './components/contact-add/contact-add.component';
import { ContactListComponent } from './components/contact-list/contact-list.component';
import { HomeComponent } from './components/home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'home' },
  { path: 'home', component: HomeComponent },
  { path: 'contacts', component: ContactListComponent },
  { path: 'contacts/new', component: ContactAddComponent },
  { path: 'contacts/:id', component: ContactUpdateComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
