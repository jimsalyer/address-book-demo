import { AlertType } from './alert-type';

export class Alert {
  constructor(
    public message: string,
    public type: AlertType = AlertType.INFO,
    public dismissible: boolean = false
  ) {}
}
