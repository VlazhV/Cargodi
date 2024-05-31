import api from "../http/index"

export default class DriverService {
    static async GetAll(driverFilter) {
        return await api.get('/drivers', { params: { driverFilter } })
    }
}