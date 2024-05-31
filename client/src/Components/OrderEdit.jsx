import React, { useEffect, useState } from 'react'
import { useFetching } from '../Hooks/useFetching'
import OrderService from '../Services/OrderService'
import orderStatuses from '../Data/OrderStatuses.json'

export default function OrderEdit(props) {
    const id = props.id ? props.id : null
    const onSelectOrder = props.onSelectOrder ? props.onSelectOrder : (newOrderData) => { }
    const nullable = props.nullable

    const [ordersData, setOrdersData] = useState([])

    const handleSelectDriver = (e) => {
        e.preventDefault()
        let index = e.target.id
        if (index == -1) {
            onSelectOrder(null)
        }
        else {
            onSelectOrder({ ...(ordersData[index]) })
        }
    }

    const [fetch, loading, error] = useFetching(async (type) => {
        switch (type) {
            case "get":
                {
                    const res = await OrderService.GetAll()

                    let orders = res.data
                    orders.forEach(ord => {
                        ord.time = new Date(ord.time)
                    })

                    setOrdersData(orders)
                }
                break;
        }

    })

    useEffect(() => {
        fetch("get")
    }, [])

    return (
        <div className="modal fade" id={id} tabIndex="-1">
            <div className="modal-dialog modal-dialog-scrollable">
                <div className="modal-content">
                    <div className="modal-header">
                        <h1 className="modal-title fs-5">Заказы</h1>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div className="modal-body d-flex flex-column">
                        {
                            ordersData.map((orderData, index) => {
                                return <button className='btn btn-outline-primary' data-bs-dismiss="modal" key={orderData.id} id={index} onClick={handleSelectDriver}>
                                    Заказ №{orderData.id}
                                    <br />
                                    Время оформления: {orderData.time.toLocaleString()}
                                    <br />
                                    Адрес загрузки: {orderData.loadAddress?.name}
                                    <br />
                                    Адрес доставки: {orderData.deliverAddress?.name}
                                    <br />
                                    Клиент: {orderData.client?.name}
                                    <br />
                                    Статус: {orderStatuses.find(v => v.id == orderData.orderStatus?.id)?.name}
                                </button>
                            })
                        }
                        {
                            nullable &&
                            <button className='btn btn-outline-secondary' data-bs-dismiss="modal" id={-1} onClick={handleSelectDriver}>
                                Убрать заказ
                            </button>
                        }
                    </div>
                    <div className="modal-footer">
                        <div className='w-100 h-100 d-flex flex-row'>
                            <div className='h-100 col d-flex flex-column'>
                                <div className="d-flex align-items-center text-warning mb-2 rounded-pill px-4 py-2 display-4 bg-dark"
                                    style={{ visibility: loading ? 'visible' : 'hidden' }}>
                                    <strong>Загрузка...</strong>
                                    <div className="spinner-border ms-auto" role="status" aria-hidden="true"></div>
                                </div>
                                <div className="border border-danger border rounded-pill p-2 px-4 w-100"
                                    style={{ visibility: error ? 'visible' : 'hidden' }}>
                                    <span className="text-danger text-center h3">{typeof error === 'string' ? error : toString(error)}</span>
                                </div>
                            </div>
                            <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Закрыть</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}
