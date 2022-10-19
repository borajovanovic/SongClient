namespace SongService
{
    public class UserBuilder
    {
        private int id;
        private string firstName;
        private string lastName;
        private string loginName;
        private string email;
        private List<ArtistBuilder> artistBuilder;
        private List<UserSongBuilder> userSongBuilders;

        public UserBuilder(int id, string firstName, string lastName, string email, string loginName)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.loginName = loginName;
            this.artistBuilder = new List<ArtistBuilder>();
            this.userSongBuilders = new List<UserSongBuilder>();
        }

        public UserBuilder WithArtis(ArtistBuilder artistBuilder)
        {
            this.artistBuilder.Add(artistBuilder);
            return this;
        }

        public UserBuilder WithUserSongs(UserSongBuilder userSongBuilder)
        {
            this.userSongBuilders.Add(userSongBuilder);
            return this;
        }

        public User Build()
        {
            return new User()
            {
                Id = this.id,
                FirstName = this.firstName,
                LastName = this.lastName,
                Email = this.email,
                LoginName = this.loginName,
                Artists = this.artistBuilder.Select(x => x.Build()).ToList(),
                UserSongs = this.userSongBuilders.Select(x => x.Build()).ToList()
            };
        }
    }


    public class ArtistBuilder
    {
        private string firstName;
        private string lastName;
        private List<AlbumBuilder> albumBuilders;

        public ArtistBuilder(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.albumBuilders = new List<AlbumBuilder>();
        }

        public ArtistBuilder WithAlbum(AlbumBuilder albumBuilder)
        {
            this.albumBuilders.Add(albumBuilder);
            return this;
        }

        public Artist Build()
        {
            return new Artist()
            {
                FirstName = this.firstName,
                LastName = this.lastName,
                Albums = albumBuilders.Select(x => x.Build()).ToList()
            };
        }
    }

    public class AlbumBuilder
    {
        private string title;
        private Genre genre;
        private List<SongBuilder> songBuilder;
        public AlbumBuilder(string title)
        {
            this.title = title;
            this.songBuilder = new List<SongBuilder>();
        }

        public AlbumBuilder WithGenre(Genre genre)
        {
            this.genre = genre;
            return this;
        }

        public AlbumBuilder WithSong(SongBuilder songBuilder)
        {
            this.songBuilder.Add(songBuilder);
            return this;
        }

        public Album Build()
        {
            return new Album()
            {
                Title = this.title,
                Genre = this.genre,
                Songs = this.songBuilder.Select(x => x.Build()).ToList()
            };
        }
    }


    public class SongBuilder
    {
        protected int id;
        protected string name;
        protected decimal length;
        protected int size;
        protected string artistName;

        public SongBuilder(int id, string name)
        {
            this.id = id;
            this.name = name;
            this.artistName = String.Empty;
        }
        public SongBuilder WithLength(decimal length)
        {
            this.length = length;
            return this;
        }

        public SongBuilder WithSize(int size)
        {
            this.size = size;
            return this;
        }

        public SongBuilder WithArtist(string artist)
        {
            this.artistName = artist;
            return this;
        }



        public Song Build()
        {
            return new Song()
            {
                Id = this.id,
                Name = this.name,
                Size = this.size,
                Length = this.length,
                ArtistName = this.artistName
            };

        }
    }

    public class UserSongBuilder
    {
        private DateTime? timeOfListening;
        private bool followed;
        protected int id;
        protected string name;
        protected decimal length;
        protected int size;
        protected string artistName;

        public UserSongBuilder(int id, string name)
        {
            this.id = id;
            this.name = name;
            this.artistName = String.Empty;
        }

        public UserSongBuilder IsFollowed(bool followed)
        {
            this.followed = followed;
            return this;
        }

        public UserSongBuilder WithTimeOfListening(DateTime timeOfListening)
        {
            this.timeOfListening = timeOfListening;
            return this;
        }

        public UserSongBuilder WithLength(decimal length)
        {
            this.length = length;
            return this;
        }

        public UserSongBuilder WithSize(int size)
        {
            this.size = size;
            return this;
        }

        public UserSongBuilder WithArtist(string artist)
        {
            this.artistName = artist;
            return this;
        }


        public new UserSong Build()
        {
            return new UserSong()
            {
                Id = this.id,
                Name = this.name,
                Size = this.size,
                Length = this.length,
                ArtistName = this.artistName,
                TimeOfListening = timeOfListening,
                Followed = this.followed
            };

        }

    }

}




