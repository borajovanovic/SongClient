namespace SongService
{
    public class SongService
    {
        public IEnumerable<SongDto> GetTopFiveLatestListenedSongs(User user)
        {
            return user.UserSongs.Where(x => x.Followed && x.TimeOfListening.HasValue).OrderByDescending(x => x.TimeOfListening).Take(5)
                .Select(x => new SongDto { Name = x.Name });

        }

        public AlbumDto GetLastYearFavoriteAlbum(User user)
        {
             return user.Artists.SelectMany(x => x.Albums)
                .Select(x => new
                {
                    Album = x,
                    UserAlbumRating = (decimal)x.Songs
                    .Where(y => user.UserSongs
                    .Any(z => z.Followed && z.TimeOfListening.HasValue && z.TimeOfListening > DateTime.Now.AddYears(-1) && z.Id == y.Id)).Count() / (decimal)x.Songs.Count * 100
                })
                .OrderByDescending(x => x.UserAlbumRating).Where(x => x.UserAlbumRating > 0)
                .Select(x => new AlbumDto { Titile = x.Album.Title  }).FirstOrDefault();
        }
    }



}




