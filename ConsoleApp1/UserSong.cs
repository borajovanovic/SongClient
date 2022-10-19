namespace SongService
{
    public class UserSong : Song
    {

        public DateTime? TimeOfListening { get; set; }
        public bool Followed { get; set; }

    }
}




