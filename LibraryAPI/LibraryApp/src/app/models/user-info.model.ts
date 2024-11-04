export class UserInfo {
    userId: number;
    username: string;
    password: string;
    roleId: number;

    constructor (
        userId: number,
        username: string,
        password: string,
        roleId: number
    ) {
        this.userId = userId;
        this.username = username;
        this.password = password;
        this.roleId = roleId;
    }
}
