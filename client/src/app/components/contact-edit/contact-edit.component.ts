import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Contact } from './../../models/contact';
import { ContactService } from './../../services/contact.service';

@Component({
  selector: 'app-contact-edit',
  templateUrl: './contact-edit.component.html',
  styleUrls: ['./contact-edit.component.scss']
})
export class ContactEditComponent implements OnInit {
  contact = new Contact();
  form: FormGroup;
  id = 0;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private service: ContactService
  ) {
    this.form = this.formBuilder.group({
      contactId: [0, [Validators.required, Validators.pattern(/^\d+$/)]],
      firstName: ['', [Validators.required, Validators.maxLength(255)]],
      middleName: ['', Validators.maxLength(255)],
      lastName: ['', [Validators.required, Validators.maxLength(255)]],
      displayName: ['', [Validators.required, Validators.maxLength(255)]],
      streetAddress: ['', [Validators.required, Validators.maxLength(255)]],
      city: ['', [Validators.required, Validators.maxLength(255)]],
      region: ['', [Validators.required, Validators.maxLength(255)]],
      postalCode: ['', [Validators.required, Validators.maxLength(255)]],
      country: ['', [Validators.required, Validators.maxLength(255)]],
      phoneNumber: ['', Validators.maxLength(255)],
      emailAddress: ['', [Validators.email, Validators.maxLength(255)]]
    });
  }

  ngOnInit(): void {
    this.id = +(this.route.snapshot.params.id ?? 0);
    if (this.id > 0) {
      this.service.getContact(this.id).subscribe(
        (contact: Contact) => {
          this.contact = contact;
          this.form.setValue(contact);
        },
        () => this.router.navigate(['/contacts'])
      );
    }
  }

  onReset(): void {
    this.form.reset(this.contact);
  }

  onSubmit(): void {
    if (this.id > 0) {
      this.service.updateContact(this.id, this.form.value).subscribe(
        () => this.router.navigate(['/contacts'])
      );
    } else {
      this.service.addContact(this.form.value).subscribe(
        () => this.router.navigate(['/contacts'])
      );
    }
  }
}
