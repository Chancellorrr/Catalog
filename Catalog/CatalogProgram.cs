using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog
{
    class CatalogProgram
    {
        private readonly string SONG_PATH = Directory.GetCurrentDirectory() + "/songs.json";
        private readonly string MOVIE_PATH = Directory.GetCurrentDirectory() + "movies.json";
        public void Start()
        {
            Console.WriteLine("Welcome User");
            Console.WriteLine("Type usage to view instructions ");
            while (true)
            {
                Console.Write(">");

                string[] response = Console.ReadLine()?.Split(' ');

                switch (response[0].ToLower())
                {
                    case ("usage"):
                        Console.WriteLine("To add a movie 'AddMovie NAME CATEGORY SIZE DIRECTOR MAINACTOR LOCATION(optional)' \n" +
                            "To add a song 'AddSong NAME CATEGORY SIZE SINGER LENGTHINSECONDS LOCATION(optional) \n" +
                            "To view movies 'ViewMovies'\n" +
                            "To view songs 'ViewSongs' \n" +
                            "To play a song 'play'");
                        break;

                    case ("addmovie"):
                        AddMovie(response);
                        break;

                    case ("viewmovies"):
                        ViewMovies(MOVIE_PATH);
                        break;

                    case ("addsong"):
                        AddSong(response);
                        break;

                    case ("viewsongs"):
                        ViewSongs();
                        break;

                    case ("play"):
                        PlaySong();
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }

        }

        private void PlaySong()
        {
            if (JsonConvert.DeserializeObject<List<Song>>(File.ReadAllText(SONG_PATH)) == null)
            {
                Console.WriteLine("No songs available");
                return;
            }

            ViewSongs();
            Console.WriteLine("Enter ID");
            try
            {
                var id = Guid.Parse(Console.ReadLine());
                var movieList = JsonConvert.DeserializeObject<List<Song>>(File.ReadAllText(SONG_PATH)) ?? new List<Song>();

                var song = movieList.FirstOrDefault(a => a.Id == id);
                if (song.Location == null)
                {
                    Console.WriteLine($"No location data found for {song.Name} ");
                }
                else
                {
                    song.Play();
                    Console.WriteLine($"Playing song at {song.Location}");

                }
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured. Check the id entered");
            }

        }

        private void ViewSongs()
        {
            CreateFile(SONG_PATH);
            var songList = JsonConvert.DeserializeObject<List<Song>>(File.ReadAllText(SONG_PATH)) ?? new List<Song>();
            Console.WriteLine($"{songList.Count} song(s) is/are in the catalog");
            foreach (var m in songList)
            {
                Console.WriteLine($"{m.Id} - {m.Name} - {m.Category}");
            }
        }

        private void AddSong(string[] response)
        {
            try
            {
                Song song = new Song()
                {
                    Id = Guid.NewGuid(),
                    Name = response[1],
                    Category = response[2],
                    Size = int.Parse(response[3]),
                    Singer = response[4],
                    LengthInSeconds = int.Parse(response[5]),
                };
                if (response.Count() > 6) song.Location = response[6];

                CreateFile(SONG_PATH);
                var songList = JsonConvert.DeserializeObject<List<Song>>(File.ReadAllText(SONG_PATH)) ?? new List<Song>();
                songList.Add(song);
                var jsonData = JsonConvert.SerializeObject(songList);
                File.WriteAllText(SONG_PATH, jsonData);

                Console.WriteLine("Added song successfully");

            }
            catch (Exception)
            {

                Console.WriteLine("Movie addition failed. Invalid format. Type usage to view instructions");
            }
        }

        private static void CreateFile(string path)
        {
            if (!File.Exists(path))
            {
                File.CreateText(path).Close();
            }
        }

        private void ViewMovies(string filePath)
        {
            CreateFile(MOVIE_PATH);
            var movieList = JsonConvert.DeserializeObject<List<Movie>>(File.ReadAllText(MOVIE_PATH)) ?? new List<Movie>();
            Console.WriteLine($"{movieList.Count} movies is/are in the catalog");
            foreach (var m in movieList)
            {
                Console.WriteLine($"{m.Name} - {m.Category}");
            }
        }

        public void AddMovie(string[] response)
        {
            try
            {
                Movie movie = new Movie()
                {
                    Id = Guid.NewGuid(),
                    Name = response[1],
                    Category = response[2],
                    Size = int.Parse(response[3]),
                    Director = response[4],
                    MainActor = response[5]
                };
                if (response.Count() > 6) movie.Location = response[6];

                WriteMovie(MOVIE_PATH, movie);

                Console.WriteLine("Added movie successfully");

            }
            catch (Exception)
            {
                Console.WriteLine("Movie addition failed. Invalid format. Type usage to view instructions");

            }
        }

        public void WriteMovie(string fileName, Movie movie)
        {
            CreateFile(MOVIE_PATH);
            var movieList = JsonConvert.DeserializeObject<List<Movie>>(File.ReadAllText(MOVIE_PATH)) ?? new List<Movie>();
            movieList.Add(movie);
            var jsonData = JsonConvert.SerializeObject(movieList);
            File.WriteAllText(MOVIE_PATH, jsonData);
        }
    }
}
