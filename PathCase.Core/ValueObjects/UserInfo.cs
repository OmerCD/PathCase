namespace PathCase.Core.ValueObjects
{
    public class UserInfo
    {
        public UserInfo(string userName, string connectionId)
        {
            UserName = userName;
            ConnectionId = connectionId;
        }

        public string UserName { get; set; }
        public string ConnectionId { get; set; }
        
        public override int GetHashCode()
        {
            return UserName.GetHashCode();
        }
    }
}