import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ArtistAverage } from './../types/types';

@Component({
	selector: 'app-compare-component',
	templateUrl: './compare.component.html'
})
export class CompareComponent {
	public baseUrl = '';
	public btnText = 'Search';
	public artistResults: ArtistAverage[] = [];
	public showResults = false;
	public artistOne = '';
	public artistTwo = '';

	constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
		this.baseUrl = baseUrl;
	}

	compareArtistData() {
		this.btnText = 'Loading...';
		this.showResults = false;

		this.http.get<ArtistAverage[]>(this.baseUrl + 'api/Lyric/' + this.artistOne + '/' + this.artistTwo)
			.subscribe(response => {
				this.btnText = 'Search';

				for (let i = 0; i < response.length; i++) {
					const artistData: ArtistAverage = {
						artistId: response[i].artistId,
						artistName: response[i].artistName,
						averageDetails: response[i].averageDetails
					};

					this.artistResults.push(artistData);
				}

				this.showResults = true;
			});
	}
}
