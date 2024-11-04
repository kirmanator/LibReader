using Dapper.FluentMap.Mapping;

namespace LibraryAPI.Models {
    public class UserInfo {
        public int UserId {
            get; set;
        }
        public string Username {
            get; set;
        } = "";
        public string Password {
            get; set;
        } = "";
        public int RoleId {
            get; set;
        }
        public UserInfo() {
        }
        public UserInfo(int userId, string username, int roleId) {
            UserId = userId;
            Username = username;
            RoleId = roleId;
        }
    }

    internal class UserInfoMap : EntityMap<UserInfo> {
        internal UserInfoMap() {
            Map(entity => entity.UserId).ToColumn("user_id");
            Map(entity => entity.Username).ToColumn("username");
            Map(entity => entity.Password).ToColumn("password");
            Map(entity => entity.RoleId).ToColumn("role_id");
        }
    }
}
