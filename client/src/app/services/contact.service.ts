import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Contact } from '../models/contact.model';

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  private baseUrl = `${environment.apiUrl}/v1/contacts`;

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(private http: HttpClient) {}

  addContact(contact: Contact): Observable<Contact> {
    return this.http.post<Contact>(this.baseUrl, JSON.stringify(contact), {
      ...this.httpOptions,
    });
  }

  deleteContact(id: number): Observable<Contact> {
    return this.http.delete<Contact>(`${this.baseUrl}/${id}`);
  }

  getContact(id: number): Observable<Contact> {
    return this.http.get<Contact>(`${this.baseUrl}/${id}`);
  }

  listContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>(this.baseUrl);
  }

  updateContact(id: number, contact: Contact): Observable<Contact> {
    return this.http.put<Contact>(
      `${this.baseUrl}/${id}`,
      JSON.stringify(contact),
      { ...this.httpOptions }
    );
  }
}
