import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
	selector: 'app-home',
	templateUrl: './home.component.html',
})
export class HomeComponent {
	public artist = '';
	public baseUrl = '';
	public artistAverage: ArtistAverage = {
		artistId: '',
		artistName: '',
		averageDetails: {
			average: 0,
			maxCount: 0,
			minCount: 0
		}
	};
	public btnText = 'Search';
	public showResults = false;

	constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
		this.baseUrl = baseUrl;
	}

	getArtistData() {
		this.btnText = 'Loading...';
		this.showResults = false;

		this.http.get<ArtistAverage>(this.baseUrl + 'api/Lyric/' + this.artist)
			.subscribe(response => {
				this.btnText = 'Search';
				this.showResults = true;

				this.artistAverage.artistId = response.artistId;
				this.artistAverage.artistName = response.artistName;
				this.artistAverage.averageDetails.average = response.averageDetails.average;
				this.artistAverage.averageDetails.minCount = response.averageDetails.minCount;
				this.artistAverage.averageDetails.maxCount = response.averageDetails.maxCount;
		});
	}
}
