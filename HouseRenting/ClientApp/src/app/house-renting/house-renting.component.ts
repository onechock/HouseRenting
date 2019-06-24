import { Component, OnInit } from '@angular/core';
import { HouseTypeService } from '../services/house-type.service';
import { HouseType } from '../models/house-type';
import { Router } from '@angular/router';

@Component({
  selector: 'app-house-renting',
  templateUrl: './house-renting.component.html',
  styleUrls: ['./house-renting.component.css']
})
export class HouseRentingComponent implements OnInit {
	public houseTypes: HouseType[];

	constructor(
		private router: Router,
		private houseTypeService: HouseTypeService
	) { }

	ngOnInit() {
		this.houseTypeService.getHouseTypes()
			.subscribe(houseTypes => {
				this.houseTypes = houseTypes;
			}, err => {
				console.log(err)
			});
	}

	chooseHouseType(houseType: Number) {
		this.router.navigateByUrl(`/create-reservation/${houseType}`);
	}
}
