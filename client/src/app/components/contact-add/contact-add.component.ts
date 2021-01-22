import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Contact } from '../../models/contact';
import { ContactService } from '../../services/contact.service';

@Component({
  selector: 'app-contact-add',
  templateUrl: './contact-add.component.html',
  styleUrls: ['./contact-add.component.scss']
})
export class ContactAddComponent implements OnInit {
  constructor(
    private router: Router,
    private service: ContactService
  ) { }

  ngOnInit(): void {
  }

  onSubmit(contact: Contact): void {
    this.service.addContact(contact).subscribe(
      () => this.router.navigate(['/contacts'])
    );
  }
}