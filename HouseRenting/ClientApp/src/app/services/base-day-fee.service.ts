import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { BaseDayFee } from '../models/base-day-fee';

@Injectable({ providedIn: 'root' })
export class BaseDayFeeService {
	constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

	getBaseDayFee() {
		return this.http.get<BaseDayFee>(`${this.baseUrl}api/HouseRental/GetBaseDayFee/`);
	}

	createBaseDayFee(baseDayFee: BaseDayFee) {
		return this.http.post<BaseDayFee>(`${this.baseUrl}api/HouseRental/AddUpdateBaseDayFee/`, baseDayFee);
	}
}
