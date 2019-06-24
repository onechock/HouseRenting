import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

import { HouseTypeService } from '../services/house-type.service';
import { BaseDayFeeService } from '../services/base-day-fee.service';
import { HouseType } from '../models/house-type';
import { Router } from '@angular/router';
import { BaseDayFee } from '../models/base-day-fee';
import { AlertService } from '../services/alert.service';
import { first } from 'rxjs/operators';

@Component({
	selector: 'app-house-types',
	templateUrl: './house-types.component.html',
	styleUrls: ['./house-types.component.css']
})
export class HouseTypesComponent implements OnInit {
	public houseTypes: HouseType[];
	public baseDayFee: BaseDayFee;
	baseDayFeeForm: FormGroup;
	submitted: boolean;
	loading: boolean;

	constructor(
		private formBuilder: FormBuilder,
		private alertService: AlertService,
		private router: Router,
		private houseTypeService: HouseTypeService,
		private baseDayFeeService: BaseDayFeeService
	) { }

	get f() { return this.baseDayFeeForm.controls; }

	ngOnInit() {
		this.baseDayFeeService.getBaseDayFee().subscribe(baseDayFee => {
			this.baseDayFee = baseDayFee;

			this.baseDayFeeForm = this.formBuilder.group({
				id: 0,
				price: ['', [Validators.required, this.notANumberValidator]]
			});

			if (baseDayFee) {
				this.baseDayFeeForm.controls['id'].setValue(baseDayFee.id);
				this.baseDayFeeForm.controls['price'].setValue(baseDayFee.price);
				this.getHouseTypes()
			}
		}, err => {
			console.log(err)
		});


	}

	notANumberValidator(control: FormControl) {
		let number = control.value;

		if (isNaN(number)) {
			return {
				notANumber: true
			}
		}

		return null;
	}

	getHouseTypes() {
		this.houseTypeService.getHouseTypes()
			.subscribe(houseTypes => {
				this.houseTypes = houseTypes;
			}, err => {
				console.log(err)
			});
	}

	newHouseType() {
		this.router.navigateByUrl(`/admin-create-house-type`);
	}

	onSubmit() {
		this.submitted = true;

		if (this.baseDayFeeForm.invalid) {
			return;
		}

		this.loading = true;

		this.baseDayFeeService.createBaseDayFee(this.baseDayFeeForm.value)
			.pipe(first())
			.subscribe(
				data => {
					this.alertService.success('Base Day Fee is updated', true);
					this.getHouseTypes();
				},
				error => {
					this.alertService.error(error);
					this.loading = false;
				});
	}
}
