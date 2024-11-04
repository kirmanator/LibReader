namespace LibraryAPI.Dto {
    public class CredentialsDto {
        public string Username { get; }
        public string Password { get; set; }
        public CredentialsDto(string username, string password) {
            this.Username = username;
            this.Password = password;
        }
    }
}
