import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
	selector: 'app-compare-component',
	templateUrl: './compare.component.html'
})
export class CompareComponent {
	public baseUrl = '';
	public btnText = 'Search';
	public artistResults: ArtistAverage[] = [];
	public showResults = false;

	constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
		this.baseUrl = baseUrl;
	}
}
