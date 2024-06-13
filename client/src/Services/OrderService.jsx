import api from "../http/index"

export default class OrderService {
    static async GetById(id) {
        return await api.get('/orders/' + id)
    }
    static async GetAll() {
        return await api.get('/orders')
    }
    static async GetAllMy() {
        return await api.get('/orders/my')
    }
    static async Create(orderData, customerId) {
        if (customerId) {
            return await api.post('/orders', orderData, { params: { customerId } })
        }
        else {
            return await api.post('/orders', orderData)
        }
    }
    static async Delete(id) {
        return await api.delete('/orders/' + id)
    }
    static async Update(id, orderData) {
        return await api.put('/orders/' + id, orderData)
    }
    static async UpdatePayloadList(id, payloadsData) {
        return await api.patch('/orders/' + id, payloadsData)
    }
    static async SetStatus(id, status) {
        return await api.put('/orders/' + id + '/status', {}, { params: { status } })
    }
    static async GetReviews(id) {
        return await api.get('/orders/' + id + '/reviews')
    }
    static async CreateReview(id, reviewData) {
        return await api.post('/orders/' + id + '/reviews', reviewData)
    }
}