<template>
	<div>
		<h5>Get an artist's average lyric count across all of their releases:</h5>
		<div>
			<input id="artist-average-input" v-model="artist"/>
			<div>
				<button id="submit-average-count" @click.prevent="getArtistAverageDetails">{{buttonText}}</button>
			</div>
		</div>
		<div v-if="showResult">
			<label>{{artist}} average lyric details: </label>
			<br/>
			<label>Average: {{average}}</label>
			<br/>
			<label>Min: {{min}}</label>
			<br/>
			<label>Max: {{max}}</label>
			<br/>
		</div>
		<div v-if="errorMsg">
			<label>{{errorMsg}}</label>
		</div>
	</div>
</template>

<script>
import service from '../services/lyric-service.js'

export default {
	data() {
		return {
			artist: '',
			average: 0,
			min: 0,
			max: 0,
			showResult: false,
			buttonText: 'Submit',
			errorMsg: ''
		}
	},

	methods: {
		getArtistAverageDetails() {
			if (this.artist.length === 0) {
				this.errorMsg = "You must provide an artist's name.";
				return;
			}

			this.buttonText = 'Loading...';

			service.getAverageLyricCount(this.artist)
				.then(response => {
					this.average = response.data.averageDetails.average;
					this.max = response.data.averageDetails.maxCount;
					this.min = response.data.averageDetails.minCount;

					this.showResult = true;
					this.buttonText = 'Submit';
				})
				.catch(error => {
					this.errorMsg = error
				});
		}
	}
}
</script>