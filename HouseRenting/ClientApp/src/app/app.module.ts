/// <reference path = "app.routing.ts" />
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { routing } from './app.routing';

import { HouseRentingComponent } from './house-renting/house-renting.component';
import { ReservationComponent } from './reservation/reservation.component';
import { CreateReservationComponent } from './reservation/create-reservation.component';
import { CreateHouseTypeComponent } from './admin/create-house-type.component';
import { HouseTypesComponent } from './admin/house-types.component';

@NgModule({
	imports: [
		BrowserModule,
		ReactiveFormsModule,
		NgbModule,
		FormsModule,
		HttpClientModule,
		routing
	],
	declarations: [
		AppComponent,
		HouseRentingComponent,
		CreateHouseTypeComponent,
		HouseTypesComponent,
		ReservationComponent,
		CreateReservationComponent
	],
	bootstrap: [AppComponent]
})

export class AppModule {}