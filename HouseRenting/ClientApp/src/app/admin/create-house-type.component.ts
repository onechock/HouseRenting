import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { first } from 'rxjs/operators';

import { HouseTypeService } from '../services/house-type.service';
import { AlertService } from '../services/alert.service';

@Component({
	selector: 'app-add-house-type',
	templateUrl: './create-house-type.component.html',
	styleUrls: ['./create-house-type.component.css']
})

export class CreateHouseTypeComponent implements OnInit {
	createForm: FormGroup;
	loading = false;
	submitted = false;

	constructor(
		private formBuilder: FormBuilder,
		private alertService: AlertService,
		private router: Router,
		private houseTypeService: HouseTypeService
	) {
		
	}

	get f() { return this.createForm.controls; }

	ngOnInit() {
		this.createForm = this.formBuilder.group({
			id: 0,
			name: ['', [Validators.required]],
			multiplyPriceBy: ['', [Validators.required, this.notANumberValidator]],
			price: 0
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

	onSubmit() {
		this.submitted = true;

		if (this.createForm.invalid) {
			return;
		}

		this.loading = true;

		this.houseTypeService.addHouseType(this.createForm.value)
			.pipe(first())
			.subscribe(
				data => {
					this.alertService.success('New house type created', true);
					this.router.navigateByUrl(`admin`);
				},
				error => {
					this.alertService.error(error);
					this.loading = false;
				});
	}
}
