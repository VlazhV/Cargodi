import api from "../http/index"

export default class AuthService {
    static async Login(userName, password) {
        const tokenData = await api.post('/login', { userName, password })
        localStorage.setItem('accessToken', tokenData.data.accessToken)
        return tokenData
    }
    static async Register(userDto) {
        const userData = await api.post('/register', userDto)
        return userData
    }
    static async SignUp(userName, password, email, phoneNumber, client) {
        const tokenData = await api.post('/sign-up', { userName, password, email, phoneNumber, client })
        localStorage.setItem('accessToken', tokenData.data.accessToken)
        return tokenData
    }
    static async Logout() {
        localStorage.removeItem('accessToken')
    }
}