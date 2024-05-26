import React, { useEffect, useState } from 'react'
import { useFetching } from '../Hooks/useFetching'
import { Link } from 'react-router-dom'
import orderStatuses from '../Data/OrderStatuses.json'
import OrderService from '../Services/OrderService'

export default function OrdersPage() {

    const testOrder = {
        id: 1,
        time: new Date(),
        acceptTime: new Date(),
        loadAddress: {
            name: 'Vileyka'
        },
        deliverAddress: { name: 'Molodechno' },
        payloads: [{
            id: 1,
            length: 12,
            width: 12,
            height: 12,
            weight: 12,
            description: 'Макароны кароч',
            payloadType: {
                name: 'Item',
                id: 1
            }
        }],
        client: {
            name: 'Максим'
        },
        clientId: 1,
        orderStatus: { id: 1, name: 'processing' },
        operator: {
            secondName: 'Костень',
            firstName: 'Костя',
            middleName: 'Костевич',
            employDate: '12-12-2003',
            fireDate: null
        }
    }
    const [ordersData, setOrdersData] = useState([
        testOrder
    ])

    const [fetch, loading, error] = useFetching(async (type) => {
        switch (type) {
            case "get":
                {
                    const res = await OrderService.GetAll()

                    let orders = res.data
                    orders.forEach(ord => {
                        ord.time = new Date(ord.time)
                        if (ord.acceptTime) {
                            ord.acceptTime = new Date(ord.acceptTime)
                        }
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
        <div className="container-fluid mt-5 p-5">
            <div className='mt-5'>
                <div className="row justify-content-center">
                    <div className="col-12 content-head">
                        <div className="mbr-section-head mb-5">
                            <h4 className="mbr-section-title mbr-fonts-style align-center mb-0 display-2">
                                <strong>Заказы</strong>
                            </h4>
                        </div>
                    </div>
                </div>
                {
                    error &&
                    <div className="border border-danger border rounded-4 p-2 px-4 mt-2">
                        <span className="text-danger text-center h3">{typeof error === 'string' ? error : toString(error)}</span>
                    </div>
                }
                <div className="row">

                    {
                        ordersData.map(orderData =>
                            <div className="item features-without-image col-4 item-mb active border rounded rounded-4" key={orderData.id}>
                                <div className="item-wrapper">
                                    <div className="item-head">
                                        <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                            <strong>Заказ №{orderData.id}</strong>
                                        </h6>
                                        <h5 className="item-title mbr-fonts-style mb-0 display-7">
                                            <strong>Время оформления:</strong> {orderData.time.toLocaleString()}
                                        </h5>
                                        <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                            <strong>Адрес загрузки:</strong> {orderData.loadAddress?.name}
                                        </h6>
                                        <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                            <strong>Адрес доставки:</strong> {orderData.deliverAddress?.name}
                                        </h6>
                                        <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                            <strong>Клиент:</strong> {orderData.client?.name}
                                        </h6>
                                        <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                            <strong>Статус:</strong> {orderStatuses.find(v => v.id == orderData.orderStatus?.id)?.name}
                                        </h6>
                                    </div>

                                    <div className="mbr-section-btn item-footer">
                                        <Link to={"/order/" + orderData.id} className="btn item-btn btn-primary display-7">Подробнее</Link>
                                    </div>
                                </div>
                            </div>
                        )
                    }

                </div>
            </div>
        </div>
    )
}
