import { Contact } from './../../models/contact';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-contact-form',
  templateUrl: './contact-form.component.html',
  styleUrls: ['./contact-form.component.scss']
})
export class ContactFormComponent implements OnInit {
  @Input() contact = new Contact();
  @Output() submitted = new EventEmitter<Contact>();

  form = new FormGroup({
    firstName: new FormControl(this.contact.firstName),
    middleName: new FormControl(this.contact.middleName),
    lastName: new FormControl(this.contact.lastName),
    displayName: new FormControl(this.contact.displayName),
    streetAddress: new FormControl(this.contact.streetAddress),
    city: new FormControl(this.contact.city),
    region: new FormControl(this.contact.region),
    postalCode: new FormControl(this.contact.postalCode),
    country: new FormControl(this.contact.country),
    phoneNumber: new FormControl(this.contact.phoneNumber),
    emailAddress: new FormControl(this.contact.emailAddress)
  });

  constructor() { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    this.submitted.emit(this.form.value);
  }
}
