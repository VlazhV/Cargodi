import React, { useEffect, useState } from 'react'
import { useFetching } from '../Hooks/useFetching'
import UserService from '../Services/UserService'
import userRoles from '../Data/UserRoles.json'
import { useNavigate, useParams } from 'react-router'
import CarService from '../Services/CarService'
import carTypes from '../Data/CarTypes.json'
import AutoparkEdit from '../Components/AutoparkEdit'
import OrderService from '../Services/OrderService'
import orderStatuses from '../Data/OrderStatuses.json'
import AddressEdit from '../Components/AddressEdit'

export default function OrderPage() {

    const { orderId } = useParams()

    const [editing, setEditing] = useState(false)

    const [orderData, setOrderData] = useState({
        id: null,
        time: null,
        acceptTime: null,
        loadAddress: '',
        deliverAddress: '',
        payloads: [],
        client: {
            name: ''
        },
        clientId: null,
        orderStatus: 'processing',
        operator: null
    })

    const testOrder = {
        id: 1,
        time: '01-01-2024',
        acceptTime: '01-01-2024',
        loadAddress: 'Vileyka',
        deliverAddress: 'Molodechno',
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
        orderStatus: 'processing',
        operator: {
            secondName: 'Костень',
            firstName: 'Костя',
            middleName: 'Костевич',
            employDate: '12-12-2003',
            fireDate: null
        }
    }

    const navigate = useNavigate()

    const [fetch, loading, error] = useFetching(async (type, data) => {
        switch (type) {
            case "get":
                {
                    const res = await OrderService.GetById(orderId)
                    setOrderData(res.data)
                }
                break;
            case "delete":
                {
                    const res = await CarService.Delete(orderId)
                    if (res) {
                        navigate(-1)
                    }
                }
                break;
            case "update":
                {
                    const res = await CarService.Update(orderId, orderData)
                    setOrderData(res.data)
                    setEditing(false)
                }
                break;
        }

    })

    const handleDeleteClick = (e) => {
        e.preventDefault()
        fetch("delete")
    }

    const handleEditingStart = (e) => {
        e.preventDefault()
        setEditing(true)
    }

    const handleEditingCancel = (e) => {
        e.preventDefault()
        setEditing(false)
        fetch("get")
    }

    const handleEditingSave = (e) => {
        e.preventDefault()
        fetch("update")
    }

    const handleOrderChange = (e) => {
        e.preventDefault()
        setOrderData(prev => ({ ...prev, [e.target.id]: e.target.value }))
    }

    useEffect(() => {
        // fetch("get")
        setOrderData(testOrder)
    }, [orderId])

    return (
        <div className="container mt-5 py-5">

            {
                !editing &&
                <div className="row justify-content-center mt-5">
                    <div className="col-12 col-md-12 col-lg">
                        <div className="text-wrapper align-left">
                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-4">
                                <strong>Заказ №{orderData.id}</strong>
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Время оформления:</strong> <span>{orderData.time}</span>
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Адрес загрузки:</strong> <span>{orderData.loadAddress}</span>
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Адрес доставки:</strong> <span>{orderData.deliverAddress}</span>
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Клиент:</strong> <span>{orderData.client.name}</span>
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Статус:</strong> <span>{orderStatuses.find(v => v.value == orderData.orderStatus).name}</span>
                            </h1>
                        </div>
                    </div>
                    <div className="col-12 col-md-12 col-lg">
                        <div className="text-wrapper align-left">
                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Оператор:</strong> <span>{orderData.operator
                                    ? orderData.operator.secondName + ' ' + orderData.operator.firstName
                                    + ' ' + orderData.operator.middleName : '-'}</span>
                            </h1>
                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Время подтверждения:</strong> <span>{orderData.acceptTime ? orderData.acceptTime : '-'}</span>
                            </h1>
                        </div>
                    </div>
                </div>
            }

            {
                editing &&
                <div className="row justify-content-center mt-5">
                    <h1 className="mbr-section-title mbr-fonts-style mb-4 display-4">
                        <strong>Заказ №{orderData.id}</strong>
                    </h1>
                    <div className='d-flex flex-row w-100 m-3'>
                        <AddressEdit name='Адрес загрузки' />
                        <AddressEdit name='Адрес доставки' />
                    </div>
                    <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                        <strong>Время оформления:</strong> <span>{orderData.time}</span>
                    </h1>

                    <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                        <strong>Клиент:</strong> <span>{orderData.client.name}</span>
                    </h1>

                    <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                        <strong>Статус:</strong> <span>{orderStatuses.find(v => v.value == orderData.orderStatus).name}</span>
                    </h1>

                    <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                        <strong>Оператор:</strong> <span>{orderData.operator
                            ? orderData.operator.secondName + ' ' + orderData.operator.firstName
                            + ' ' + orderData.operator.middleName : '-'}</span>
                    </h1>

                    <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                        <strong>Время подтверждения:</strong> <span>{orderData.acceptTime ? orderData.acceptTime : '-'}</span>
                    </h1>

                </div>
            }

            {
                !editing &&
                <>
                    <div className="btn btn-primary display-3" onClick={handleEditingStart}>Изменить</div>
                    <div className="btn btn-danger display-3" onClick={handleDeleteClick}>Удалить</div>
                </>
            }
            {
                editing &&
                <>
                    <div className="btn btn-primary display-3" onClick={handleEditingSave}>Сохранить</div>
                    <div className="btn btn-secondary display-3" onClick={handleEditingCancel}>Отмена</div>
                </>}

            {
                error &&
                <div className="border border-danger border rounded-4 p-2 px-4 mt-2">
                    <span className="text-danger text-center h3">{typeof error === 'string' ? error : toString(error)}</span>
                </div>
            }
            <div className='d-flex flex-column w-100 align-items-center mt-3'>
                <h2><strong>Грузы</strong></h2>
                <div className='row d-flex flex-row w-100'>
                    {
                        orderData.payloads.map(payloadData => (
                            <div></div>
                        ))
                    }
                </div>
            </div>
        </div >
    )
}
