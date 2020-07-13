# Average Lyric Calculator Web App

A simple .NET Core web application which can calculate the average number of lyrics for an artist across all of their releases.  It uses the MusicBrainz.Core nuget package as an easy way of retrieving these details and connects to the lyrics.ovh API for getting each song's lyrics.

# To Run
* Clone the repository using git clone https://github.com/Banksy93/LyricAPI.git.
* Open in Visual Studio and set the startup project as Lyric.Web
* Hit F5 (or ctrl + f5) to run the application.
* Enter and artists name and hit submit (Note: For speed, use an artist who only has a small number of releases. For example, Lewis Capaldi or Billie Eilish. It will take a long time to process bands such as Metallica and Queen, more on that further down).
* Upon completion of the request, you will get the average lyrics in the artists songs, the maximum number of lyrics in a song, and the minimum number of lyrics in a song.
* To run tests, click the Test toolbar and select Run All Tests. (Or if using Resharper in VS2019, Extensions -> Resharper -> Unit Tests -> Run All Tests From Solution)

# Performance Enhancement Suggestions
The reason this application can be slow is down to the fact some artists have a lot of releases and the lyric calculator needs to process each one of their songs so here are a few possible performance enhancements:
* After each search has completed, store the result in a database so we can check if the artist has been searched for before, and if so, pull the data from there instead.
* Alternatively, cache previous search results and use the similar logic as above for retrieving already searched artists.

# A few ideas for new features
* Compare 2 artists - this would involve creating a new controller method and calling the existing logic for each artist passed up and then display their results
* Detailed list of averages based on each of the artists release's - display these results in a table with sorting capabilities. This would involve adding a new model called AlbumDetails which includes the Album name and the existing AverageDetails model. This would be a new list property on ArtistAverage model.
* Further to the above point, display charts based on the above results using a charts plugin.
* Error logging for debugging and for meaningful front end error messages

# Additional Info on the API Project
Initially I created this using a .NET Core Web API with VueJs front end template. The reason behind switching to an MVC front end was because I couldn't get it running on another machine after cloning it to test. I spent a bit of time trying to solve this but for now I just switched to an MVC/JQuery front end. 
The code for the original API/VueJs front end can be found on the Features/ApiImplementation branch. I will likely come back to this at a later date and add some of the extra features and performance enhancements mentioned above as I found it a fun little project.
