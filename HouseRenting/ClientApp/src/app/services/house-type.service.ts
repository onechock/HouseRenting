import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { HouseType } from '../models/house-type';

@Injectable({ providedIn: 'root' })
export class HouseTypeService {
	constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

	getHouseTypes() {
		return this.http.get<HouseType[]>(`${this.baseUrl}api/HouseRental/GetHouseTypes`);
	}

	getHouseType(id: number) {
		return this.http.get<HouseType>(`${this.baseUrl}api/HouseRental/GetHouseType/${id}`, {});
	}

	addHouseType(houseType: HouseType) {
		return this.http.post<HouseType>(`${this.baseUrl}api/HouseRental/AddHouseType/`, houseType);
	}
}
