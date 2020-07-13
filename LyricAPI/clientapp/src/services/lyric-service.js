import axios from 'axios';

const baseURL = 'http://localhost:5000/api/Lyric';

export default {
	getAverageLyricCount(artist) {
		return axios.get(baseURL + '/' + artist);
	}
}