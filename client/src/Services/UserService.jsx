import api from "../http/index"

export default class UserService {
    static async GetAll() {
        return await api.get('/users')
    }
    static async GetById(id) {
        return await api.get('/users/' + id)
    }
    static async GetProfile() {
        return await api.get('/profile')
    }
    static async UpdateProfile(userName, email, phoneNumber) {
        return await api.put('/profile', { userName, email, phoneNumber })
    }
    static async UpdateProfilePassword(oldPassword, newPassword) {
        return await api.put('/profile/password', { oldPassword, newPassword })
    }
    static async DeleteUser(id) {
        return await api.delete('/users/' + id)
    }
}