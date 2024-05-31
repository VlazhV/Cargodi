import React, { useContext, useEffect, useState } from 'react'
import { useFetching } from '../Hooks/useFetching'
import { useNavigate, useParams } from 'react-router'
import CarService from '../Services/CarService'
import OrderService from '../Services/OrderService'
import orderStatuses from '../Data/OrderStatuses.json'
import AddressEdit from '../Components/AddressEdit'
import payloadTypes from '../Data/PayloadTypes.json'
import { AuthContext } from '../Contexts/AuthContext'
import AddressTwoEdit from '../Components/AddressTwoEdit'

export default function OrderPage() {
    const { user } = useContext(AuthContext)

    const emptyPayload = {
        length: null,
        width: null,
        height: null,
        weight: null,

        description: '',

        payloadType: {
            id: payloadTypes[0].id,
            name: payloadTypes[0].value
        }
    }

    const { orderId } = useParams()

    const [editing, setEditing] = useState(false)
    const [editingPayloads, setEditingPayloads] = useState(false)

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

    const navigate = useNavigate()

    const [fetch, loading, error] = useFetching(async (type, data) => {
        switch (type) {
            case "get":
                {
                    const res = await OrderService.GetById(orderId)

                    let order = res.data
                    order.time = new Date(order.time)
                    if (order.acceptTime) {
                        order.acceptTime = new Date(order.acceptTime)
                    }

                    setOrderData(order)
                }
                break;
            case "delete":
                {
                    const res = await OrderService.Delete(orderId)
                    if (res) {
                        navigate(-1)
                    }
                }
                break;
            case "update":
                {
                    const res = await OrderService.Update(orderId, orderData)
                    fetch("get")
                    setEditing(false)
                }
                break;
            case "accept":
                {
                    const res = await OrderService.SetStatus(orderId, "accepted")
                    fetch("get")

                }
                break;
            case "decline":
                {
                    const res = await OrderService.SetStatus(orderId, "declined")
                    fetch("get")
                }
                break;
            case "updatePayloads":
                {
                    const res = await OrderService.UpdatePayloadList(orderId, orderData.payloads)
                    fetch("get")
                    setEditingPayloads(false)
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

    const handleAcceptClick = (e) => {
        e.preventDefault()
        fetch("accept")
    }

    const handleDeclineClick = (e) => {
        e.preventDefault()
        fetch("decline")
    }

    const handleOrderChange = (e) => {
        e.preventDefault()
        setOrderData(prev => ({ ...prev, [e.target.id]: e.target.value }))
    }

    const handleLoadAddressChange = (newAddress) => {
        setOrderData(prev => ({ ...prev, loadAddress: newAddress }))
    }

    const handleDeliverAddressChange = (newAddress) => {
        setOrderData(prev => ({ ...prev, deliverAddress: newAddress }))
    }

    const handleEditingPayloadsStart = (e) => {
        e.preventDefault()
        setEditingPayloads(true)
    }

    const handleEditingPayloadsCancel = (e) => {
        e.preventDefault()
        setEditingPayloads(false)
        fetch("get")
    }

    const handleEditingPayloadsSave = (e) => {
        e.preventDefault()
        fetch("updatePayloads")
    }

    const handleAddPayload = (e) => {
        e.preventDefault()
        let orderNewPayloads = { ...orderData }
        orderNewPayloads.payloads = [...orderData.payloads, emptyPayload]
        setOrderData(orderNewPayloads)
    }

    useEffect(() => {
        fetch("get")
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
                                <strong>Время оформления:</strong> <span>{orderData.time?.toLocaleString()}</span>
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Адрес загрузки:</strong> <span>{orderData.loadAddress?.name}</span>
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Адрес доставки:</strong> <span>{orderData.deliverAddress?.name}</span>
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Клиент:</strong> <span>{orderData.client?.name}</span>
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Статус:</strong> <span>{orderStatuses.find(v => v.id == orderData.orderStatus?.id)?.name}</span>
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
                                <strong>Время подтверждения:</strong> <span>{orderData.acceptTime ? orderData.acceptTime.toLocaleString() : '-'}</span>
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
                    <div className='d-flex flex-row w-100 m-4'>
                        <div className='mx-4'>
                            <AddressTwoEdit addressFrom={orderData.loadAddress}
                                addressTo={orderData.deliverAddress}
                                onAddressFromChange={handleLoadAddressChange}
                                onAddressToChange={handleLoadAddressChange}
                                name='Адреса загрузки и выгрузки' />
                        </div>
                    </div>
                    <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                        <strong>Время оформления:</strong> <span>{orderData.time?.toLocaleString()}</span>
                    </h1>

                    <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                        <strong>Клиент:</strong> <span>{orderData.client?.name}</span>
                    </h1>

                    <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                        <strong>Статус:</strong> <span>{orderStatuses.find(v => v.id == orderData.orderStatus?.id)?.name}</span>
                    </h1>

                    <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                        <strong>Оператор:</strong> <span>{orderData.operator
                            ? orderData.operator.secondName + ' ' + orderData.operator.firstName
                            + ' ' + orderData.operator.middleName : '-'}</span>
                    </h1>

                    <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                        <strong>Время подтверждения:</strong> <span>{orderData.acceptTime ? orderData.acceptTime.toLocaleString() : '-'}</span>
                    </h1>

                </div>
            }

            {
                !editing && !editingPayloads &&
                <>
                    <div className="btn btn-primary display-3" onClick={handleEditingStart}>Изменить</div>
                    {
                        user && user.operator &&
                        <div className="btn btn-danger display-3" onClick={handleDeleteClick}>Удалить</div>
                    }
                </>
            }
            {
                editing && !editingPayloads &&
                <>
                    <div className="btn btn-primary display-3" onClick={handleEditingSave}>Сохранить</div>
                    <div className="btn btn-secondary display-3" onClick={handleEditingCancel}>Отмена</div>
                </>
            }

            {
                error &&
                <div className="border border-danger border rounded-4 p-2 px-4 mt-2">
                    <span className="text-danger text-center h3">{typeof error === 'string' ? error : toString(error)}</span>
                </div>
            }
            <div className='d-flex flex-column w-100 align-items-center mt-3'>
                <h2><strong>Грузы (кол-во: {orderData.payloads.length})</strong></h2>
                {
                    !editingPayloads && !editing &&
                    <>
                        <div className="btn btn-primary display-3" onClick={handleEditingPayloadsStart}>Изменить грузы</div>
                    </>
                }
                {
                    editingPayloads &&
                    <div className='d-flex flex-row'>
                        <div className="btn btn-primary display-3" onClick={handleEditingPayloadsSave}>Сохранить грузы</div>
                        <div className="btn btn-secondary display-3" onClick={handleEditingPayloadsCancel}>Отмена</div>
                    </div>
                }
                {
                    !editingPayloads &&
                    <div className='row d-flex flex-row w-100'>
                        {
                            orderData.payloads.map((payloadData, index) => (
                                <div className='col-4 d-flex flex-column border' key={index}>
                                    <h4 className="mbr-section-title mbr-fonts-style align-center mb-2">
                                        <strong>Груз №{index + 1}</strong>
                                    </h4>

                                    <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                        <strong>Длинна (мм):</strong> <span>{payloadData.length}</span>
                                    </h1>
                                    <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                        <strong>Ширина (мм):</strong> <span>{payloadData.width}</span>
                                    </h1>
                                    <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                        <strong>Высота (мм):</strong> <span>{payloadData.height}</span>
                                    </h1>
                                    <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                        <strong>Вес (г):</strong> <span>{payloadData.weight}</span>
                                    </h1>
                                    <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                        <strong>Тип:</strong> <span>{payloadTypes.find(v => v.value == payloadData.payloadType?.name)?.name}</span>
                                    </h1>
                                    <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                        <strong>Описание:</strong>
                                        <p className='border rounded-pill p-4 mt-2'>{payloadData.description}</p>
                                    </h1>
                                </div>
                            ))
                        }
                    </div>
                }
                {
                    editingPayloads &&
                    <div className='row d-flex flex-row w-100'>
                        {orderData.payloads.map((payloadData, index) => {

                            const handleChangePayload = (e) => {
                                let editNewOrderData = { ...orderData }
                                editNewOrderData.payloads = [...orderData.payloads]
                                if (e.target.type == 'number') {
                                    editNewOrderData.payloads[index] = { ...orderData.payloads[index], [e.target.id]: Number(e.target.value) }
                                }
                                else {
                                    editNewOrderData.payloads[index] = { ...orderData.payloads[index], [e.target.id]: e.target.value }
                                }
                                setOrderData(editNewOrderData)
                            }

                            const handleChangePayloadType = (e) => {
                                let editNewOrderData = { ...orderData }
                                editNewOrderData.payloads = [...orderData.payloads]
                                let newPayloadType = { id: Number(e.target.value), name: payloadTypes.find(v => v.id == e.target.value).value }
                                editNewOrderData.payloads[index] = { ...orderData.payloads[index], payloadType: newPayloadType }
                                setOrderData(editNewOrderData)
                            }

                            const handleRemovePayload = (e) => {
                                let editNewOrderData = { ...orderData }
                                editNewOrderData.payloads = [...orderData.payloads]
                                editNewOrderData.payloads.splice(index, 1)
                                setOrderData(editNewOrderData)
                            }

                            return <div className='col-4 d-flex flex-column border' key={index}>
                                <h4 className="mbr-section-title mbr-fonts-style align-center mb-2">
                                    <strong>Груз №{index + 1}</strong>
                                </h4>
                                <div className='btn btn-outline-secondary' onClick={handleRemovePayload}>
                                    -
                                </div>
                                <div className="col-12 form-group mb-3" data-for="textarea">
                                    <input name="input" placeholder="Длинна" type="number" data-form-field="input"
                                        className="form-control" id="length" value={payloadData.length} onChange={handleChangePayload}></input>
                                </div>
                                <div className="col-12 form-group mb-3" data-for="textarea">
                                    <input name="input" placeholder="Ширина" type="number" data-form-field="input"
                                        className="form-control" id="width" value={payloadData.width} onChange={handleChangePayload}></input>
                                </div>
                                <div className="col-12 form-group mb-3" data-for="textarea">
                                    <input name="input" placeholder="Высота" type="number" data-form-field="input"
                                        className="form-control" id="height" value={payloadData.height} onChange={handleChangePayload}></input>
                                </div>
                                <div className="col-12 form-group mb-3" data-for="textarea">
                                    <input name="input" placeholder="Вес" type="number" data-form-field="input"
                                        className="form-control" id="weight" value={payloadData.weight} onChange={handleChangePayload}></input>
                                </div>
                                <select name="select" className='form-select display-7 p-3 mb-3' value={payloadData.payloadType?.id} onChange={handleChangePayloadType} id="payloadType">
                                    {
                                        payloadTypes.map(pType => <option value={pType.id} key={pType.id}>{pType.name}</option>)
                                    }
                                </select>
                                <div className="col-12 form-group mb-3" data-for="textarea">
                                    <textarea name="input" placeholder="Описание" type='text'
                                        className="form-control" id="description" value={payloadData.description} onChange={handleChangePayload}></textarea>
                                </div>
                            </div>
                        }
                        )}
                        <div className='btn btn-outline-secondary col-1' onClick={handleAddPayload}>
                            +
                        </div>
                    </div>
                }

            </div>

            {
                !editing && !editingPayloads && user && user.operator &&
                <div className='w-100 d-flex flex-row justify-content-center mt-4'>
                    {
                        orderData.orderStatus?.name === 'processing' &&
                        <div className="btn btn-success display-3" onClick={handleAcceptClick}>Принять</div>
                    }
                    {

                        (orderData.orderStatus?.name === 'processing' || orderData.orderStatus?.name === 'accepted') &&
                        <div className="btn btn-danger display-3" onClick={handleDeclineClick}>Отклонить</div>
                    }
                </div>
            }
            <div className="d-flex align-items-center text-warning m-4 rounded-pill px-4 py-2 display-4 fixed-bottom bg-dark" style={{ visibility: loading ? 'visible' : 'hidden' }}>
                <strong>Загрузка...</strong>
                <div className="spinner-border ms-auto" role="status" aria-hidden="true"></div>
            </div>
        </div >
    )
}
