using Dapper.FluentMap.Mapping;

namespace LibraryAPI.Models {
    public enum RoleType {
        Patron,
        Librarian,
        Administrator
    }

    public class UserRole {
        public int RoleId {
            get; set;
        }

        public string? Name {
            get; set;
        }
        public UserRole() {
        }

        public UserRole(int roldId, string name) {
            this.RoleId = roldId;
            this.Name = name;
        }

        public UserRole(RoleType roleType, string name) {
            this.RoleId = (int)roleType;
            this.Name = name;
        }
    }

    internal class UserRoleMap : EntityMap<UserRole> {
        internal UserRoleMap() {
            Map(entity => entity.RoleId).ToColumn("role_id");
            Map(entity => entity.Name).ToColumn("name");
        }
    }
}
