import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { NGXLogger } from 'ngx-logger';
import { Subscription } from 'rxjs';
import { Contact } from '../../models/contact.model';
import { ContactService } from '../../services/contact.service';
import { Alert } from '../../shared/alert';
import { AlertType } from '../../shared/alert-type';

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.scss']
})
export class ContactListComponent implements OnInit, OnDestroy {
  alert?: Alert;
  listContactsSubscription?: Subscription;
  contacts: Contact[] = [];

  constructor(
    private logger: NGXLogger,
    private contactService: ContactService
  ) { }

  ngOnInit(): void {
    const state = window.history.state;
    if (state && state.message) {
      this.alert = new Alert(state.message, state.type ?? AlertType.INFO, state.dismissible ?? false);
    }
    this.listContactsSubscription = this.listContacts();
  }

  ngOnDestroy(): void {
    this.listContactsSubscription?.unsubscribe();
  }

  listContacts(): Subscription {
    return this.contactService.listContacts().subscribe(
      (contacts) => this.contacts = contacts,
      (error: HttpErrorResponse) => {
        this.logger.error('ContactService.listContacts', error.error);
        this.alert = new Alert('Could not list contacts.', AlertType.DANGER);
      }
    );
  }

  onAlertClose(): void {
    this.alert = undefined;
  }
}
