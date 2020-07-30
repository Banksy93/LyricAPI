interface ArtistAverage {
	artistId?: string;
	artistName?: string;
	averageDetails?: AverageDetails;
}

interface AverageDetails {
	average?: number;
	maxCount?: number;
	minCount?: number;
}
