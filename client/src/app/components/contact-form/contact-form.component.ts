import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Contact } from './../../models/contact';

@Component({
  selector: 'app-contact-form',
  templateUrl: './contact-form.component.html',
  styleUrls: ['./contact-form.component.scss']
})
export class ContactFormComponent implements OnInit {
  @Output() submitted = new EventEmitter<Contact>();

  form = new FormGroup({
    contactId: new FormControl(0),
    firstName: new FormControl(),
    middleName: new FormControl(),
    lastName: new FormControl(),
    displayName: new FormControl(),
    streetAddress: new FormControl(),
    city: new FormControl(),
    region: new FormControl(),
    postalCode: new FormControl(),
    country: new FormControl(),
    phoneNumber: new FormControl(),
    emailAddress: new FormControl()
  });

  constructor() { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    this.submitted.emit(this.form.value);
  }

  updateForm(contact: Contact): void {
    this.form.setValue(contact);
  }
}
