export interface ArtistAverage {
	artistId?: string;
	artistName?: string;
	averageDetails?: AverageDetails;
}

export interface AverageDetails {
	average?: number;
	maxCount?: number;
	minCount?: number;
}
