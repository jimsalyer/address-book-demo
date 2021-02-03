import { NgForm } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NGXLogger } from 'ngx-logger';
import { Contact } from '../../models/contact.model';
import { ContactService } from '../../services/contact.service';
import { Alert } from '../../shared/alert';
import { AlertType } from '../../shared/alert-type';
import { Region } from '../../models/region.model';
import { RegionService } from './../../services/region.service';

@Component({
  selector: 'app-contact-edit',
  templateUrl: './contact-edit.component.html',
  styleUrls: ['./contact-edit.component.scss'],
})
export class ContactEditComponent implements OnInit {
  @ViewChild('contactForm') contactForm?: NgForm;
  @ViewChild('deleteModalContent') deleteModalContent?: any;

  alert?: Alert;
  model = new Contact();
  regions: Region[] = [];

  constructor(
    private logger: NGXLogger,
    private modalService: NgbModal,
    private route: ActivatedRoute,
    private router: Router,
    private contactService: ContactService,
    private regionService: RegionService
  ) {}

  ngOnInit(): void {
    this.regions = this.regionService.getRegions();

    const id = +(this.route.snapshot.params.id ?? 0);
    if (id > 0) {
      this.contactService.getContact(id).subscribe(
        (contact: Contact) => {
          this.model = contact;
        },
        (error: HttpErrorResponse) => {
          this.logger.error('ContactService.getContact', id, error.error);
          this.router.navigateByUrl('/contacts', {
            state: new Alert(
              `A contact with ID ${id} could not be found.`,
              AlertType.DANGER
            ),
          });
        }
      );
    }
  }

  onDelete(): void {
    if (this.model.contactId > 0) {
      this.modalService.open(this.deleteModalContent).result.then(
        () => {
          this.contactService.deleteContact(this.model.contactId).subscribe(
            () =>
              this.router.navigateByUrl('/contacts', {
                state: new Alert(
                  `${this.model.displayName} has been deleted.`,
                  AlertType.SUCCESS,
                  true
                ),
              }),
            (error: HttpErrorResponse) =>
              this.handleError(
                'ContactService.deleteContact',
                'Could not delete contact.',
                this.model.contactId,
                error.error
              )
          );
        },
        () => {}
      );
    } else {
      this.handleError(
        'ContactEditComponent.onDelete',
        'There is no existing contact to delete.'
      );
    }
  }

  onAlertClose(): void {
    this.alert = undefined;
  }

  onFormSubmit(): void {
    if (this.model.contactId > 0) {
      this.contactService
        .updateContact(this.model.contactId, this.model)
        .subscribe(
          () =>
            this.router.navigateByUrl('/contacts', {
              state: new Alert(
                `${this.model.displayName} has been updated.`,
                AlertType.SUCCESS,
                true
              ),
            }),
          (error: HttpErrorResponse) =>
            this.handleError(
              'ContactService.updateContact',
              'Could not update contact.',
              this.model,
              error.error
            )
        );
    } else {
      this.contactService.addContact(this.model).subscribe(
        () =>
          this.router.navigateByUrl('/contacts', {
            state: new Alert(
              `${this.model.displayName} has been added.`,
              AlertType.SUCCESS,
              true
            ),
          }),
        (error: HttpErrorResponse) =>
          this.handleError(
            'ContactService.addContact',
            'Could not add contact.',
            this.model,
            error.error
          )
      );
    }
  }

  resetForm(): void {
    if (this.model.contactId > 0) {
      window.location.reload();
    } else {
      this.contactForm?.resetForm(new Contact());
    }
  }

  private handleError(
    logMessage: string,
    displayMessage: string,
    ...data: any[]
  ): void {
    this.logger.error(logMessage, data);
    this.alert = new Alert(displayMessage, AlertType.DANGER);
  }
}
