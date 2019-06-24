import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Reservation } from '../models/reservation';

@Injectable({ providedIn: 'root' })
export class ReservationService {
	constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

	getReservation(id: number) {
		return this.http.get<Reservation>(`${this.baseUrl}api/HouseRental/GetReservation/${id}`);
	}

	createReservation(reservation: Reservation) {
		return this.http.post<Reservation>(`${this.baseUrl}api/HouseRental/AddReservation/`, reservation);
	}
}
