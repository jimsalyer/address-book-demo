export class Contact {
  constructor(
    public contactId: number = 0,
    public firstName: string = '',
    public middleName: string = '',
    public lastName: string = '',
    public displayName: string = '',
    public streetAddress: string = '',
    public city: string = '',
    public state: string = '',
    public zip: string = '',
    public phoneNumber: string = '',
    public emailAddress: string = ''
  ) {}
}
