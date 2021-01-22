import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { Contact } from '../models/contact';
import { retry, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  baseUrl = 'https://localhost:5001/api/v1/contacts';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  addContact(contact: Contact): Observable<Contact> {
    return this.http.post<Contact>(this.baseUrl, JSON.stringify(contact))
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  deleteContact(id: number): Observable<Contact> {
    return this.http.delete<Contact>(`${this.baseUrl}/${id}`)
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  getContact(id: number): Observable<Contact> {
    return this.http.get<Contact>(`${this.baseUrl}/${id}`)
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  listContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>(this.baseUrl)
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  updateContact(id: number, contact: Contact): Observable<Contact> {
    return this.http.put<Contact>(`${this.baseUrl}/{id}`, JSON.stringify(contact))
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  private handleError(error: any): Observable<never> {
    let errorMessage = '';
    if (error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(errorMessage);
  }
}
