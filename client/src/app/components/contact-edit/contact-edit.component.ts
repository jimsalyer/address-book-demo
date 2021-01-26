import { RegionService } from './../../services/region.service';
import { Region } from './../../models/region';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NGXLogger } from 'ngx-logger';
import { Contact } from '../../models/contact';
import { ContactService } from '../../services/contact.service';
import { Alert } from '../../shared/alert';
import { AlertType } from '../../shared/alert-type';

@Component({
  selector: 'app-contact-edit',
  templateUrl: './contact-edit.component.html',
  styleUrls: ['./contact-edit.component.scss']
})
export class ContactEditComponent implements OnInit {
  @ViewChild('deleteModalContent') deleteModalContent?: any;

  alert?: Alert;
  contact?: Contact;
  form: FormGroup;
  id = 0;
  regions: Region[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private logger: NGXLogger,
    private modalService: NgbModal,
    private route: ActivatedRoute,
    private router: Router,
    private contactService: ContactService,
    private regionService: RegionService
  ) {
    this.form = this.formBuilder.group({
      contactId: [0, [Validators.required, Validators.pattern(/^\d+$/)]],
      firstName: ['', [Validators.required, Validators.maxLength(255)]],
      middleName: ['', Validators.maxLength(255)],
      lastName: ['', [Validators.required, Validators.maxLength(255)]],
      displayName: ['', [Validators.required, Validators.maxLength(255)]],
      streetAddress: ['', [Validators.required, Validators.maxLength(255)]],
      city: ['', [Validators.required, Validators.maxLength(255)]],
      state: ['', [Validators.required, Validators.maxLength(2)]],
      zip: ['', [Validators.required, Validators.maxLength(10)]],
      country: ['', [Validators.required, Validators.maxLength(255)]],
      phoneNumber: ['', Validators.required, Validators.maxLength(255)],
      emailAddress: ['', [Validators.required, Validators.email, Validators.maxLength(255)]]
    });
  }

  ngOnInit(): void {
    this.regions = this.regionService.getRegions();

    this.id = +(this.route.snapshot.params.id ?? 0);
    if (this.id > 0) {
      this.contactService.getContact(this.id).subscribe(
        (contact: Contact) => {
          this.contact = contact;
          this.form?.setValue(contact);
        },
        (error: HttpErrorResponse) => {
          this.logger.error('ContactService.getContact', this.id, error.error);
          this.router.navigateByUrl('/contacts', {
            state: new Alert('Could not get contact.', AlertType.DANGER)
          });
        }
      );
    }
  }

  get firstName(): AbstractControl {
    return this.form.controls.firstName;
  }

  get middleName(): AbstractControl {
    return this.form.controls.middleName;
  }

  get lastName(): AbstractControl {
    return this.form.controls.lastName;
  }

  get displayName(): AbstractControl {
    return this.form.controls.displayName;
  }

  get streetAddress(): AbstractControl {
    return this.form.controls.streetAddress;
  }

  get city(): AbstractControl {
    return this.form.controls.city;
  }

  get state(): AbstractControl {
    return this.form.controls.state;
  }

  get zip(): AbstractControl {
    return this.form.controls.zip;
  }

  get phoneNumber(): AbstractControl {
    return this.form.controls.phoneNumber;
  }

  get emailAddress(): AbstractControl {
    return this.form.controls.emailAddress;
  }

  onDelete(): void {
    this.modalService.open(this.deleteModalContent).result.then(
      () => {
        this.contactService.deleteContact(this.id).subscribe(
          () => this.router.navigateByUrl('/contacts', {
            state: new Alert('The contact has been deleted.', AlertType.SUCCESS)
          }),
          (error: HttpErrorResponse) => this.handleError(
            'ContactService.deleteContact',
            'Could not delete contact.',
            this.id,
            error.error
          )
        );
      },
      () => {}
    );
  }

  onAlertClose(): void {
    this.alert = undefined;
  }

  onFormReset(): void {
    this.alert = undefined;
    this.form.reset(this.contact);
  }

  onFormSubmit(): void {
    const formValue = this.form.value;
    if (this.id > 0) {
      this.contactService.updateContact(this.id, formValue).subscribe(
        () => this.router.navigateByUrl('/contacts', {
          state: new Alert('The contact has been updated.', AlertType.SUCCESS)
        }),
        (error: HttpErrorResponse) => this.handleError(
          'ContactService.updateContact',
          'Could not updated contact.',
          this.id,
          formValue,
          error.error
        )
      );
    } else {
      this.contactService.addContact(formValue).subscribe(
        () => this.router.navigateByUrl('/contacts', {
          state: new Alert('The contact has been added.', AlertType.SUCCESS)
        }),
        (error: HttpErrorResponse) => this.handleError(
          'ContactService.addContact',
          'Could not add contact.',
          formValue,
          error.error
        )
      );
    }
  }

  private handleError(logMessage: string, displayMessage: string, ...data: any[]): void {
    this.logger.error(logMessage, data);
    this.alert = new Alert(displayMessage, AlertType.DANGER);
  }
}
