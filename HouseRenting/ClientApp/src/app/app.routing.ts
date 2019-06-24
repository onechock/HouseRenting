import { Routes, RouterModule } from '@angular/router';

import { HouseRentingComponent } from './house-renting/house-renting.component';
import { CreateReservationComponent } from './reservation/create-reservation.component';
import { ReservationComponent } from './reservation/reservation.component';
import { CreateHouseTypeComponent } from './admin/create-house-type.component';
import { HouseTypesComponent } from './admin/house-types.component';

const appRoutes: Routes = [
	{ path: '', component: HouseRentingComponent },
	{ path: 'create-reservation/:id', component: CreateReservationComponent },
	{ path: 'get-reservation/:id', component: ReservationComponent },
	{ path: 'admin-create-house-type', component: CreateHouseTypeComponent },
	{ path: 'admin', component: HouseTypesComponent },
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);