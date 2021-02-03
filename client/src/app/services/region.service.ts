import { Injectable } from '@angular/core';
import { Region } from '../models/region.model';

@Injectable({
  providedIn: 'root',
})
export class RegionService {
  regions: Region[] = [
    new Region('AL', 'Alabama'),
    new Region('AK', 'Alaska'),
    new Region('AS', 'American Samoa'),
    new Region('AZ', 'Arizona'),
    new Region('AR', 'Arkansas'),
    new Region('CA', 'California'),
    new Region('CO', 'Colorado'),
    new Region('CT', 'Connecticut'),
    new Region('DE', 'Delaware'),
    new Region('DC', 'District Of Columbia'),
    new Region('FM', 'Federated States Of Micronesia'),
    new Region('FL', 'Florida'),
    new Region('GA', 'Georgia'),
    new Region('GU', 'Guam'),
    new Region('HI', 'Hawaii'),
    new Region('ID', 'Idaho'),
    new Region('IL', 'Illinois'),
    new Region('IN', 'Indiana'),
    new Region('IA', 'Iowa'),
    new Region('KS', 'Kansas'),
    new Region('KY', 'Kentucky'),
    new Region('LA', 'Louisiana'),
    new Region('ME', 'Maine'),
    new Region('MH', 'Marshall Islands'),
    new Region('MD', 'Maryland'),
    new Region('MA', 'Massachusetts'),
    new Region('MI', 'Michigan'),
    new Region('MN', 'Minnesota'),
    new Region('MS', 'Mississippi'),
    new Region('MO', 'Missouri'),
    new Region('MT', 'Montana'),
    new Region('NE', 'Nebraska'),
    new Region('NV', 'Nevada'),
    new Region('NH', 'New Hampshire'),
    new Region('NJ', 'New Jersey'),
    new Region('NM', 'New Mexico'),
    new Region('NY', 'New York'),
    new Region('NC', 'North Carolina'),
    new Region('ND', 'North Dakota'),
    new Region('MP', 'Northern Mariana Islands'),
    new Region('OH', 'Ohio'),
    new Region('OK', 'Oklahoma'),
    new Region('OR', 'Oregon'),
    new Region('PW', 'Palau'),
    new Region('PA', 'Pennsylvania'),
    new Region('PR', 'Puerto Rico'),
    new Region('RI', 'Rhode Island'),
    new Region('SC', 'South Carolina'),
    new Region('SD', 'South Dakota'),
    new Region('TN', 'Tennessee'),
    new Region('TX', 'Texas'),
    new Region('UT', 'Utah'),
    new Region('VT', 'Vermont'),
    new Region('VI', 'Virgin Islands'),
    new Region('VA', 'Virginia'),
    new Region('WA', 'Washington'),
    new Region('WV', 'West Virginia'),
    new Region('WI', 'Wisconsin'),
    new Region('WY', 'Wyoming'),
  ];

  constructor() {}

  getRegions(): Region[] {
    return this.regions;
  }
}
