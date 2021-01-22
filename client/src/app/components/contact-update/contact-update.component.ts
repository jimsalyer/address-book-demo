import { ContactFormComponent } from './../contact-form/contact-form.component';
import { Contact } from './../../models/contact';
import { ContactService } from './../../services/contact.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-contact-update',
  templateUrl: './contact-update.component.html',
  styleUrls: ['./contact-update.component.scss']
})
export class ContactUpdateComponent implements OnInit {
  @ViewChild('form') form: ContactFormComponent | null = null;
  contact = new Contact();

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: ContactService
  ) { }

  ngOnInit(): void {
    const id = +(this.route.snapshot.paramMap.get('id') ?? 0);
    this.service.getContact(id).subscribe(
      (contact) => {
        this.contact = contact;
        this.form?.updateForm(contact);
      },
      () => this.router.navigate(['/contacts'])
    );
  }

  onSubmit(contact: Contact): void {
    this.service.updateContact(this.contact.contactId, contact).subscribe(
      () => this.router.navigate(['/contacts'])
    );
  }
}
