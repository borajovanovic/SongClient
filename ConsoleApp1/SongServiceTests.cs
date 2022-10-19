using NUnit.Framework;

namespace SongService
{
    [TestFixture]
    public class SongServiceTests
    {
        private User user;
        private SongService songService;

        [SetUp]
        public void SetUp()
        {
            this.user = new UserBuilder(1, "Borislav", "Jovanovic", "BoraJovanovic90@gmail.com", "borajovanovic")
                .WithArtis(new ArtistBuilder("The Weekend", String.Empty)
                .WithAlbum(new AlbumBuilder("After Hour").WithGenre(Genre.Pop)
                .WithSong(new SongBuilder(1, "Blinding Lights"))
                .WithSong(new SongBuilder(2, "Save Your Tears"))
                .WithSong(new SongBuilder(3, "In Your Eyes"))
                .WithSong(new SongBuilder(4, "After Hours"))))
                .WithArtis(new ArtistBuilder("Sia", "Kate")
                .WithAlbum(new AlbumBuilder("This Is Acting").WithGenre(Genre.Pop)
                .WithSong(new SongBuilder(5, "Move Your Body"))
                .WithSong(new SongBuilder(6, "Unstoppable"))
                .WithSong(new SongBuilder(7, "Cheap Thrills"))
                .WithSong(new SongBuilder(8, "The Greatest")))
                .WithAlbum(new AlbumBuilder("1000 forms of fear").WithGenre(Genre.Pop)
                .WithSong(new SongBuilder(9, "chandelier"))
                .WithSong(new SongBuilder(10, "Elastic Heart"))
                .WithSong(new SongBuilder(11, "Big Girls Cry"))))
                .WithArtis(new ArtistBuilder("Zack", "Hamsey"))
                .WithUserSongs(new UserSongBuilder(1, "Blinding Lights").WithArtist("The Weekend").WithTimeOfListening(DateTime.Now.AddDays(-1)).IsFollowed(true))
                .WithUserSongs(new UserSongBuilder(2, "Save Your Tears").WithArtist("The Weekend").WithTimeOfListening(DateTime.Now.AddDays(-2)))
                .WithUserSongs(new UserSongBuilder(5, "Move Your Body").WithArtist("Sia").WithTimeOfListening(DateTime.Now.AddDays(-2)).IsFollowed(true))
                .WithUserSongs(new UserSongBuilder(6, "Unstoppable").WithArtist("Sia").WithTimeOfListening(DateTime.Now.AddDays(-3)).IsFollowed(true))
                .WithUserSongs(new UserSongBuilder(9, "chandelier").WithArtist("Sia").WithTimeOfListening(DateTime.Now.AddDays(-4)).IsFollowed(true))
                .WithUserSongs(new UserSongBuilder(10, "Big Girls Cry").WithArtist("Sia").WithTimeOfListening(DateTime.Now.AddDays(-6)).IsFollowed(true))
                .WithUserSongs(new UserSongBuilder(7, "Cheap Thrills").WithArtist("Sia").WithTimeOfListening(DateTime.Now.AddDays(-8)).IsFollowed(true))
                .WithUserSongs(new UserSongBuilder(12, "The Way").WithArtist("Zack Hamsey").WithTimeOfListening(DateTime.Now.AddDays(-10)).IsFollowed(true))
                .WithUserSongs(new UserSongBuilder(13, "Ronin").WithArtist("Zack Hamsey").WithTimeOfListening(DateTime.Now.AddDays(-11)).IsFollowed(true))
                .Build();

        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.songService = new SongService();
        }


        [Test]
        public void GetTopFiveLatestListenedSongs_UserHasMoreThenFiveSongs_ReturneFiveSongsInCorrectOrder()
        {
            List<SongDto> actualSongs = this.songService.GetTopFiveLatestListenedSongs(this.user).ToList();

            Assert.AreEqual("Blinding Lights", actualSongs[0].Name);
            Assert.AreEqual("Move Your Body", actualSongs[1].Name);
            Assert.AreEqual("Unstoppable", actualSongs[2].Name);
            Assert.AreEqual("chandelier", actualSongs[3].Name);
            Assert.AreEqual("Big Girls Cry", actualSongs[4].Name);
        }

        [Test]
        public void GetTopFiveLatestListenedSongs_UserHasLessThenFiveSongs_ReturneSongsInCorrectOrder()
        {
            this.user.UserSongs = this.user.UserSongs.Take(4).ToList();

            List<SongDto> actualSongs = this.songService.GetTopFiveLatestListenedSongs(this.user).ToList();

            Assert.AreEqual("Blinding Lights", actualSongs[0].Name);
            Assert.AreEqual("Move Your Body", actualSongs[1].Name);
            Assert.AreEqual("Unstoppable", actualSongs[2].Name);

        }

        [Test]
        public void GetTopFiveLatestListenedSongs_UserHasNoSongs_ReturnEmptyList()
        {
            this.user.UserSongs.Clear();

            List<SongDto> actualSongs = this.songService.GetTopFiveLatestListenedSongs(this.user).ToList();
            Assert.IsFalse(actualSongs.Any());
        }


        [Test]
        public void GetTopFiveLatestListenedSongs_UserHasNoFollowedOrListenedSongs_ReturnEmptyList()
        {
            this.user.UserSongs.ForEach(x => { x.Followed = false; x.TimeOfListening = null; });
            List<SongDto> actualSongs = this.songService.GetTopFiveLatestListenedSongs(this.user).ToList();
            Assert.IsFalse(actualSongs.Any());
        }


        [Test]
        public void GetLastYearFavoriteAlbum_UserHasNoFollowedArtist_ReturnNull()
        {
            this.user.Artists.Clear();
            Assert.IsNull(this.songService.GetLastYearFavoriteAlbum(this.user));
        }

        [Test]
        public void GetLastYearFavoriteAlbum_UserHasUserSongs_ReturnNull()
        {
            this.user.UserSongs.Clear();
            Assert.IsNull(this.songService.GetLastYearFavoriteAlbum(this.user));
        }

        [Test]
        public void GetLastYearFavoriteAlbum_UserArtistsHasNoAlbums_ReturnNull()
        {
            this.user.Artists.ForEach(x => x.Albums.Clear());
            Assert.IsNull(this.songService.GetLastYearFavoriteAlbum(this.user));
        }
        [Test]
        public void GetLastYearFavoriteAlbum_UserHasNoFollowedOrListenedSongs_ReturnNull()
        {
            this.user.UserSongs.ForEach(x => { x.Followed = false; x.TimeOfListening = null; });
            Assert.IsNull(this.songService.GetLastYearFavoriteAlbum(this.user));
        }

        [Test]
        public void GetLastYearFavoriteAlbum_UserHasNoListenedSongsInLastYear_ReturnNull()
        {
            this.user.UserSongs.ForEach(x => { x.TimeOfListening = DateTime.Now.AddYears(-2); });
            Assert.IsNull(this.songService.GetLastYearFavoriteAlbum(this.user));
        }

        [Test]
        public void GetLastYearFavoriteAlbum_GetAlbumWithMostFollowedSongCompareToTotalAlbumSongs_Success()
        {
            AlbumDto albumDto = this.songService.GetLastYearFavoriteAlbum(this.user);
            Assert.AreEqual("This Is Acting", albumDto.Titile);

            this.user.Artists.RemoveAll(x => x.FirstName == "Sia");
            albumDto = this.songService.GetLastYearFavoriteAlbum(this.user);

            Assert.AreEqual("After Hour", albumDto.Titile);
        }


    }

}




