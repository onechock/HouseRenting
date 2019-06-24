import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';


import { HouseType } from '../models/house-type';
import { HouseTypeService } from '../services/house-type.service';
import { ReservationService } from '../services/reservation.service';
import { AlertService } from '../services/alert.service';

declare var $: any;

@Component({
	selector: 'app-add-reservation',
	templateUrl: './create-reservation.component.html',
	styleUrls: ['./create-reservation.component.css']
})
export class CreateReservationComponent implements OnInit {
	createForm: FormGroup;
	currentHouseType: HouseType;
	loading = false;
	submitted = false;

	constructor(
		private formBuilder: FormBuilder,
		private alertService: AlertService,
		private route: ActivatedRoute,
		private router: Router,
		private houseTypeService: HouseTypeService,
		private reservationService: ReservationService
	) {
		
	}

	get f() { return this.createForm.controls; }

	ngOnInit() {

		const id: number = parseInt(this.route.snapshot.paramMap.get('id')); 

		this.houseTypeService.getHouseType(id)
			.subscribe(houseType => {
				this.currentHouseType = houseType;
				this.createForm = this.formBuilder.group({
					id: 0,
					email: ['', [Validators.required, Validators.email]],
					date: ['', [Validators.required]],
					houseTypeName: this.currentHouseType.name,
					houseType: this.currentHouseType.id,
					price: this.currentHouseType.price,
				});
				
			}, err => {
				console.log(err)
			});
	}

	onSubmit() {
		this.submitted = true;

		this.createForm.controls['date'].setValue($('.datepicker').val());
		this.createForm.controls['date'].updateValueAndValidity();

		if (this.createForm.invalid) {
			return;
		}

		this.loading = true;

		this.reservationService.createReservation(this.createForm.value)
			.pipe(first())
			.subscribe(
				data => {
					this.alertService.success('Reservation created', true);
					this.router.navigateByUrl(`/get-reservation/${data.id}`);
				},
				error => {
					this.alertService.error(error);
					this.loading = false;
				});
	}
}
