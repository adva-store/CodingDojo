import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TimeProviderService {

  constructor() { }

  private getCurrentTime(): Date
  {
    return new Date();
  }

  public getCurrentMinutes(): number
  {
    return this.getCurrentTime().getMinutes()
  }

  public getCurrentHours(): number
  {
    return this.getCurrentTime().getHours();
  }
}
