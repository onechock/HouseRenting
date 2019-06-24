import { Component, OnInit } from '@angular/core';
import { ReservationService } from '../services/reservation.service';
import { Reservation } from '../models/reservation';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-reservation',
  templateUrl: './reservation.component.html',
  styleUrls: ['./reservation.component.css']
})
export class ReservationComponent implements OnInit {
	reservation: Reservation;

	constructor(
		private route: ActivatedRoute,
		private reservationService: ReservationService
	) { }

	ngOnInit() {
		const id: number = parseInt(this.route.snapshot.paramMap.get('id'));

		this.reservationService.getReservation(id)
			.subscribe(reservation => {
				this.reservation = reservation;
			}, err => {
				console.log(err)
			});
  }

}
