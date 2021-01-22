import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Contact } from '../../models/contact';
import { ContactService } from '../../services/contact.service';

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.scss']
})
export class ContactListComponent implements OnInit, OnDestroy {
  listContactsSubscription: Subscription = Subscription.EMPTY;
  contacts: Contact[] = [];

  constructor(
    private contactService: ContactService
  ) { }

  ngOnInit(): void {
    this.listContactsSubscription = this.listContacts();
  }

  ngOnDestroy(): void {
    this.listContactsSubscription.unsubscribe();
  }

  listContacts(): Subscription {
    return this.contactService.listContacts().subscribe(
      (contacts) => {
        this.contacts = contacts;
      }
    );
  }
}
