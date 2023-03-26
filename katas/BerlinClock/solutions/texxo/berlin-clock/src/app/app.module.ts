import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BerlinClockComponent } from './views/components/berlin-clock/berlin-clock.component';
import { TimeIntervalBlockComponent } from './views/components/time-interval-block/time-interval-block.component';

@NgModule({
  declarations: [
    AppComponent,
    BerlinClockComponent,
    TimeIntervalBlockComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
