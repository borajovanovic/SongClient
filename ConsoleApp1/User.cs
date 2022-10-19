namespace SongService
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string LoginName { get; set; }
        public List<Artist> Artists { get; set; }

        public List<UserSong> UserSongs { get; set; }

    }
}




