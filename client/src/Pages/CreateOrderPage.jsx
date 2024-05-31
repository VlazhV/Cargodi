import React, { useState } from 'react'
import { useFetching } from '../Hooks/useFetching';
import { Link, useNavigate } from 'react-router-dom';
import OrderService from '../Services/OrderService'
import AddressEdit from '../Components/AddressEdit';
import payloadTypes from '../Data/PayloadTypes.json'
import AddressTwoEdit from '../Components/AddressTwoEdit';

export default function CreateOrderPage() {
    const navigate = useNavigate()

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

    const [newOrderData, setNewOrderData] = useState({
        loadAddress: {
            name: '',
            latitude: null,
            longitude: null,
            isWest: false,
            isNorth: false
        },
        deliverAddress: {
            name: '',
            latitude: null,
            longitude: null,
            isWest: false,
            isNorth: false
        },
        payloads: []
    })

    const [fetch, loading, error] = useFetching(async (type) => {
        switch (type) {
            case "create":
                {
                    const res = await OrderService.Create(newOrderData)
                    navigate(-1)
                }
                break;
        }

    })

    const handleLoadAddressChange = (newAddress) => {
        setNewOrderData(oldValue => { return { ...oldValue, loadAddress: newAddress } })
    }

    const handleDeliverAddressChange = (newAddress) => {
        setNewOrderData(oldValue => { return { ...oldValue, deliverAddress: newAddress } })
    }

    const handleAddPayload = (e) => {
        e.preventDefault()
        let orderNewPayloads = { ...newOrderData }
        orderNewPayloads.payloads = [...newOrderData.payloads, emptyPayload]
        setNewOrderData(orderNewPayloads)
    }

    const handleCreateOrder = (e) => {
        e.preventDefault()
        fetch('create')
    }

    return (
        <div className="container-fluid mt-5 p-5">
            <div className='mt-5'>
                <div className="row justify-content-center">
                    <div className="col-12 content-head">
                        <div className="mbr-section-head mb-5">
                            <h4 className="mbr-section-title mbr-fonts-style align-center mb-0 display-2">
                                <strong>Оформление заказа</strong>
                            </h4>
                        </div>
                    </div>
                </div>
                <div className="justify-content-center d-flex flex-row">
                    <div className='border rounded mx-2 p-2'>
                        <AddressTwoEdit addressFrom={newOrderData.loadAddress}
                            addressTo={newOrderData.deliverAddress}
                            onAddressFromChange={handleLoadAddressChange}
                            onAddressToChange={handleLoadAddressChange}
                            name='Адреса загрузки и выгрузки' />
                    </div>
                </div>
                <div>
                    <div className="mbr-section-head mb-5">
                        <h4 className="mbr-section-title mbr-fonts-style align-center mb-0 display-5">
                            <strong>Грузы</strong>
                        </h4>
                    </div>
                    <div className='row d-flex flex-row'>
                        {newOrderData.payloads.map((payloadData, index) => {

                            const handleChangePayload = (e) => {
                                let editNewOrderData = { ...newOrderData }
                                editNewOrderData.payloads = [...newOrderData.payloads]
                                if (e.target.type == 'number') {
                                    editNewOrderData.payloads[index] = { ...newOrderData.payloads[index], [e.target.id]: Number(e.target.value) }
                                }
                                else {
                                    editNewOrderData.payloads[index] = { ...newOrderData.payloads[index], [e.target.id]: e.target.value }
                                }
                                setNewOrderData(editNewOrderData)
                            }

                            const handleChangePayloadType = (e) => {
                                let editNewOrderData = { ...newOrderData }
                                editNewOrderData.payloads = [...newOrderData.payloads]
                                let newPayloadType = { id: Number(e.target.value), name: payloadTypes.find(v => v.id == e.target.value).value }
                                editNewOrderData.payloads[index] = { ...newOrderData.payloads[index], payloadType: newPayloadType }
                                setNewOrderData(editNewOrderData)
                            }

                            const handleRemovePayload = (e) => {
                                let editNewOrderData = { ...newOrderData }
                                editNewOrderData.payloads = [...newOrderData.payloads]
                                editNewOrderData.payloads.splice(index, 1)
                                setNewOrderData(editNewOrderData)
                            }

                            return <div className='col-4 d-flex flex-column border' key={index}>
                                <h4 className="mbr-section-title mbr-fonts-style align-center mb-2">
                                    <strong>Груз №{index + 1}</strong>
                                </h4>
                                <div className='btn btn-outline-secondary' onClick={handleRemovePayload}>
                                    -
                                </div>
                                <div className="col-12 form-group mb-3" data-for="textarea">
                                    <input name="input" placeholder="Длина" type="number" data-form-field="input"
                                        className="form-control" id="length" onChange={handleChangePayload}></input>
                                </div>
                                <div className="col-12 form-group mb-3" data-for="textarea">
                                    <input name="input" placeholder="Ширина" type="number" data-form-field="input"
                                        className="form-control" id="width" onChange={handleChangePayload}></input>
                                </div>
                                <div className="col-12 form-group mb-3" data-for="textarea">
                                    <input name="input" placeholder="Высота" type="number" data-form-field="input"
                                        className="form-control" id="height" onChange={handleChangePayload}></input>
                                </div>
                                <div className="col-12 form-group mb-3" data-for="textarea">
                                    <input name="input" placeholder="Вес" type="number" data-form-field="input"
                                        className="form-control" id="weight" onChange={handleChangePayload}></input>
                                </div>
                                <select name="select" className='form-select display-7 p-3 mb-3' onChange={handleChangePayloadType} id="payloadType">
                                    {
                                        payloadTypes.map(pType => <option value={pType.id} key={pType.id}>{pType.name}</option>)
                                    }
                                </select>
                                <div className="col-12 form-group mb-3" data-for="textarea">
                                    <textarea name="input" placeholder="Описание" type='text'
                                        className="form-control" id="description" onChange={handleChangePayload}></textarea>
                                </div>
                            </div>
                        }
                        )}
                        <div className='btn btn-outline-secondary col-1' onClick={handleAddPayload}>
                            +
                        </div>
                    </div>
                </div>
                <div className="col-lg-12 col-md-12 col-sm-12 align-center mbr-section-btn">
                    <button className="btn btn-primary display-7" onClick={handleCreateOrder} >Оформить заказ</button>
                </div>
                {
                    error &&
                    <div className="border border-danger border rounded-4 p-2 px-4 mt-2">
                        <span className="text-danger text-center h3">{typeof error === 'string' ? error : toString(error)}</span>
                    </div>
                }
            </div>
            <div className="d-flex align-items-center text-warning m-4 rounded-pill px-4 py-2 display-4 fixed-bottom bg-dark" style={{ visibility: loading ? 'visible' : 'hidden' }}>
                <strong>Загрузка...</strong>
                <div className="spinner-border ms-auto" role="status" aria-hidden="true"></div>
            </div>
        </div>)
}
