import React, { useContext, useEffect, useState } from 'react'
import { useFetching } from '../Hooks/useFetching'
import { useNavigate, useParams } from 'react-router'
import AddressEdit from '../Components/AddressEdit'
import payloadTypes from '../Data/PayloadTypes.json'
import { AuthContext } from '../Contexts/AuthContext'
import ShipService from '../Services/ShipService'
import carTypes from '../Data/CarTypes.json'
import orderStatuses from '../Data/OrderStatuses.json'
import trailerTypes from '../Data/TrailerTypes.json'
import { Link } from 'react-router-dom'
import CarEdit from '../Components/CarEdit'
import TrailerEdit from '../Components/TrailerEdit'
import AutoparkEdit from '../Components/AutoparkEdit'
import DriverEdit from '../Components/DriverEdit'
import OrderEdit from '../Components/OrderEdit'
import MapRoutes from '../Components/MapRoutes'
import AutoparkMarker from '../Components/AutoparkMarker'

export default function ShipPage() {
    const { user } = useContext(AuthContext)

    const { shipId } = useParams()

    const [editing, setEditing] = useState(false)

    const [updateShipData, setUpdateShipData] = useState({
        id: shipId,
        driverIds: [],
        carId: null,
        trailerId: null,
        autoparkStartId: null,
        autoparkFinishId: null,
        stops: []
    })

    const [shipData, setShipData] = useState({
        id: null,
        drivers: [],
        car: {
            id: null,
            licenseNumber: null,
            mark: null,
            range: null,
            carrying: null,
            tankVolume: null,
            capacityLength: null,
            capacityWidth: null,
            capacityHeight: null,
            carType:
            {
                id: 1,
                name: "Truck"
            },
            autoparkId: null,
            actualAutoparkId: null
        },
        trailer: null,
        autoparkStart: {
            id: null,
            address: {
                id: null,
                name: null,
                latitude: null,
                longitude: null,
                isWest: false,
                isNorth: false
            },
            capacity: null
        },
        autoparkFinish: {
            id: null,
            address: {
                id: null,
                name: null,
                latitude: null,
                longitude: null,
                isWest: false,
                isNorth: false
            },
            capacity: null
        },
        operator: {
            secondName: null,
            firstName: null,
            middleName: null,
            employDate: null,
            fireDate: null
        },
        start: null, //?
        finish: null, //?
        stops: [],
    })

    const resetUpdateShipData = () => {
        let tempUpdateShipData = {
            id: shipId,
            driverIds: [],
            carId: null,
            trailerId: null,
            autoparkStartId: null,
            autoparkFinishId: null,
            stops: []
        }
        tempUpdateShipData.driverIds = shipData.drivers.map(dr => dr?.id)
        tempUpdateShipData.carId = shipData.car?.id
        tempUpdateShipData.trailerId = shipData.trailer?.id
        tempUpdateShipData.autoparkStartId = shipData.autoparkStart?.id
        tempUpdateShipData.autoparkFinishId = shipData.autoparkFinish?.id
        tempUpdateShipData.stops = shipData.stops.map(st => ({ number: st?.number, orderId: st?.order?.id }))
        setUpdateShipData(tempUpdateShipData)
    }

    const testShipData1 = {
        id: 1,
        drivers: [
            {
                id: 1,
                license: 'ABCABCA',
                secondName: 'sName',
                firstName: 'fName',
                middleName: 'mName',
                employDate: new Date(),
                fireDate: null
            }
        ],
        car: {
            id: 1,
            licenseNumber: 'aASDA',
            mark: "Volva",
            range: 100,
            carrying: 1000,
            tankVolume: 100,
            capacityLength: 10000,
            capacityWidth: 2000,
            capacityHeight: 2000,
            carType:
            {
                id: 1,
                name: "Truck"
            },
            autoparkId: 1,
            actualAutoparkId: 1
        },
        trailer: {
            id: 1,
            licenseNumber: 'ASDAS',
            capacityLength: 10000,
            capacityWidth: 2000,
            capacityHeight: 2000,
            carrying: 100000,
            autoparkId: 1,
            actualAutoparkId: 1,
            trailerType: {
                id: 1,
                name: "Cistem"
            }
        }, //?
        autoparkStart: {
            id: 1,
            address: {
                id: 1,
                name: 'Minsk',
                latitude: 17.5,
                longitude: 17.5,
                isWest: false,
                isNorth: true
            },
            capacity: 100
        },
        autoparkFinish: {
            id: 2,
            address: {
                id: 2,
                name: 'Vileyka',
                latitude: 15.5,
                longitude: 15.5,
                isWest: false,
                isNorth: true
            },
            capacity: 100
        },
        operator: {
            secondName: 'Михалёв',
            firstName: 'Михаил',
            middleName: 'Михалыч',
            employDate: new Date(),
            fireDate: null
        },
        start: new Date(), //?
        finish: null, //?
        stops: [
            {
                id: 1,
                number: 1,
                order: {
                    id: 1,
                    time: new Date(),
                    acceptTime: new Date(),
                    loadAddress: {
                        id: 1,
                        name: 'Minsk',
                        latitude: 17.5,
                        longitude: 17.5,
                        isWest: false,
                        isNorth: true
                    },
                    deliverAddress: {
                        id: 2,
                        name: 'Vileyka',
                        latitude: 15.5,
                        longitude: 15.5,
                        isWest: false,
                        isNorth: true
                    },
                    orderStatus: {
                        id: 2,
                        name: "accepted"
                    }
                },
                address: {
                    id: 3,
                    name: 'Molodechno',
                    latitude: 16.5,
                    longitude: 16.5,
                    isWest: false,
                    isNorth: true
                }
            }
        ],
    }

    const navigate = useNavigate()

    const [fetch, loading, error] = useFetching(async (type, data) => {
        switch (type) {
            case "get":
                {
                    const res = await ShipService.GetById(shipId)

                    let ship = res.data
                    if (ship.start) {
                        ship.start = new Date(ship.start)
                    }
                    if (ship.finish) {
                        ship.finish = new Date(ship.finish)
                    }
                    ship.stops.sort((stopLeft, stopRight) => stopLeft.number - stopRight.number)

                    setShipData(ship)
                }
                break;
            case "delete":
                {
                    const res = await ShipService.Delete(shipId)
                    if (res) {
                        navigate(-1)
                    }
                }
                break;
            case "update":
                {
                    const res = await ShipService.Update(shipId, updateShipData)
                    fetch("get")
                    setEditing(false)
                }
                break;
            case "mark":
                {
                    const res = await ShipService.Mark(shipId)
                    fetch("get")

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
        resetUpdateShipData()
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

    const onCarChange = (newCarData) => {
        setShipData(prev => ({ ...prev, car: newCarData }))
        setUpdateShipData(prev => ({ ...prev, carId: newCarData.id }))
    }

    const onTrailerChange = (newTrailerData) => {
        setShipData(prev => ({ ...prev, trailer: newTrailerData }))
        setUpdateShipData(prev => ({ ...prev, trailerId: newTrailerData ? newTrailerData.id : null }))
    }

    const onStartAutoparkChange = (newAutoparkData) => {
        setShipData(prev => ({ ...prev, autoparkStart: newAutoparkData }))
        setUpdateShipData(prev => ({ ...prev, autoparkStartId: newAutoparkData.id }))
    }

    const onFinishAutoparkChange = (newAutoparkData) => {
        setShipData(prev => ({ ...prev, autoparkFinish: newAutoparkData }))
        setUpdateShipData(prev => ({ ...prev, autoparkFinishId: newAutoparkData.id }))
    }

    const handleRemoveDriver = (e) => {
        let index = Number(e.target.id)
        let drivers = [...(shipData.drivers)]
        drivers.splice(index, 1)
        setShipData(prev => ({ ...prev, drivers: drivers }))
        setUpdateShipData(prev => ({ ...prev, driverIds: drivers.map(dr => dr?.id) }))
    }

    const onAddDriver = (newDriverData) => {
        let drivers = [...(shipData.drivers)]
        drivers.push(newDriverData)
        setShipData(prev => ({ ...prev, drivers: drivers }))
        setUpdateShipData(prev => ({ ...prev, driverIds: drivers.map(dr => dr?.id) }))
    }

    const handleRemoveStop = (e) => {
        let index = Number(e.target.id)
        let stops = [...(shipData.stops)]
        stops.splice(index, 1)
        stops.forEach((st, stIndex) => st.number = stIndex + 1)
        setShipData(prev => ({ ...prev, stops: stops }))
        setUpdateShipData(prev => ({ ...prev, stops: stops.map(st => ({ number: st?.number, orderId: st?.order?.id })) }))
    }

    const onAddStop = (newOrderData) => {
        let stops = [...(shipData.stops)]
        stops.push({ order: newOrderData, id: null, number: 0 })
        stops.forEach((st, ordIndex) => st.number = ordIndex + 1)
        setShipData(prev => ({ ...prev, stops: stops }))
        setUpdateShipData(prev => ({ ...prev, stops: stops.map(st => ({ number: st?.number, orderId: st?.order?.id })) }))
    }

    const handleStopToLeft = (e) => {
        let index = Number(e.target.id)
        let stops = [...(shipData.stops)]
        if (index + 1 > 1) {
            let temp = stops[index]
            stops[index] = stops[index - 1]
            stops[index - 1] = temp
        }
        stops.forEach((st, ordIndex) => st.number = ordIndex + 1)
        setShipData(prev => ({ ...prev, stops: stops }))
        setUpdateShipData(prev => ({ ...prev, stops: stops.map(st => ({ number: st?.number, orderId: st?.order?.id })) }))
    }

    const handleStopToRight = (e) => {
        let index = Number(e.target.id)
        let stops = [...(shipData.stops)]
        if (index + 1 < stops.length) {
            let temp = stops[index]
            stops[index] = stops[index + 1]
            stops[index + 1] = temp
        }
        stops.forEach((st, ordIndex) => st.number = ordIndex + 1)
        setShipData(prev => ({ ...prev, stops: stops }))
        setUpdateShipData(prev => ({ ...prev, stops: stops.map(st => ({ number: st?.number, orderId: st?.order?.id })) }))
    }

    const handleMarkClick = (e) => {
        fetch('mark')
    }

    useEffect(() => {
        fetch("get")
        //setShipData(testShipData1)
    }, [shipId])

    const [showList, setShowList] = useState('showDrivers')

    const handleShowListChange = (e) => {
        setShowList(e.target.id)
    }

    const switchRenderList = () => {
        switch (showList) {
            case 'showDrivers':
                return !editing ? <div className="row w-100">
                    {
                        shipData.drivers.map((driverData, index) =>
                            <div className="item features-without-image col-4 item-mb active border rounded" key={driverData.id}>
                                <div className="item-wrapper">
                                    <div className="item-head">
                                        <h6 className="item-title mbr-fonts-style mb-0 display-7">
                                            <strong>Водитель №{driverData.id}</strong>
                                        </h6>
                                        <h6 className="item-title mbr-fonts-style mb-0 display-7">
                                            <strong>ФИО: {driverData.secondName} {driverData.firstName} {driverData.middleName}</strong>
                                        </h6>
                                        <h5 className="item-subtitle mbr-fonts-style mt-0 mb-1 ">
                                            Вод. удостоверение: {driverData.license}
                                        </h5>
                                        <h5 className="item-subtitle mbr-fonts-style mt-0 mb-1 ">
                                            Время нанятия: {driverData.employDate.toLocaleString()}
                                        </h5>
                                    </div>

                                </div>
                            </div>
                        )
                    }
                </div>
                    :
                    <div className="row w-100">
                        {
                            shipData.drivers.map((driverData, index) =>
                                <div className="item features-without-image col-4 item-mb active border rounded" key={driverData.id}>
                                    <div className="item-wrapper">
                                        <div className="item-head">
                                            <button className='btn btn-outline-secondary w-100' id={index} onClick={handleRemoveDriver}>
                                                -
                                            </button>
                                            <h6 className="item-title mbr-fonts-style mb-0 display-7">
                                                <strong>Водитель №{driverData.id}</strong>
                                            </h6>
                                            <h6 className="item-title mbr-fonts-style mb-0 display-7">
                                                <strong>ФИО: {driverData.secondName} {driverData.firstName} {driverData.middleName}</strong>
                                            </h6>
                                            <h5 className="item-subtitle mbr-fonts-style mt-0 mb-1 ">
                                                Вод. удостоверение: {driverData.license}
                                            </h5>
                                            <h5 className="item-subtitle mbr-fonts-style mt-0 mb-1 ">
                                                Время нанятия: {driverData.employDate.toLocaleString()}
                                            </h5>
                                        </div>

                                    </div>
                                </div>
                            )
                        }
                        <button className="btn btn-outline-secondary col-4"
                            data-bs-toggle="modal" data-bs-target="#driverId">
                            +
                        </button>
                        <DriverEdit id='driverId' onSelectDriver={onAddDriver} />
                    </div>
            case 'showStops':
                return !editing ? <div className="row w-100">
                    {
                        shipData.stops.map(stopData =>
                            <div className="item features-without-image col-5 item-mb active border rounded" key={stopData.id}>
                                <div className="item-wrapper">
                                    <div className="item-head">
                                        <h6 className="item-title mbr-fonts-style mb-0 display-7">
                                            <strong>Остановка №{stopData.number}</strong>
                                        </h6>
                                        <h5 className="item-subtitle mbr-fonts-style mt-0 mb-1 ">
                                            Адрес: {stopData.address.name}
                                        </h5>
                                        <div className="item-wrapper border p-4 my-4">
                                            <div className="item-head">
                                                <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                                    <strong>Заказ №{stopData.order.id}</strong>
                                                </h6>

                                                <h5 className="item-title mbr-fonts-style mb-0 display-7">
                                                    <strong>Время оформления:</strong> {stopData.order.time.toLocaleString()}
                                                </h5>
                                                <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                                    <strong>Адрес загрузки:</strong> {stopData.order.loadAddress?.name}
                                                </h6>
                                                <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                                    <strong>Адрес доставки:</strong> {stopData.order.deliverAddress?.name}
                                                </h6>
                                                <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                                    <strong>Клиент:</strong> {stopData.order.client?.name}
                                                </h6>
                                                <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                                    <strong>Статус:</strong> {orderStatuses.find(v => v.id == stopData.order.orderStatus?.id)?.name}
                                                </h6>
                                            </div>

                                            <div className="mbr-section-btn item-footer">
                                                <Link to={"/order/" + stopData.order.id} className="btn item-btn btn-primary display-7">Подробнее</Link>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </div>
                        )
                    }
                </div>
                    :
                    <div className="row w-100">
                        {
                            shipData.stops.map((stopData, index) =>
                                <div className="item features-without-image col-5 item-mb active border rounded" key={stopData.id}>
                                    <div className="item-wrapper">
                                        <div className="item-head">
                                            <div className='d-flex flex-column align-items-center'>
                                                <button className='btn btn-outline-secondary w-100' id={index} onClick={handleRemoveStop}>
                                                    -
                                                </button>
                                                <div className='d-flex flex-row'>
                                                    <button className='btn btn-outline-secondary w-100' id={index} onClick={handleStopToLeft}>
                                                        {'<'}
                                                    </button>
                                                    <button className='btn btn-outline-secondary w-100' id={index} onClick={handleStopToRight}>
                                                        {'>'}
                                                    </button>
                                                </div>
                                            </div>

                                            <h6 className="item-title mbr-fonts-style mb-0 display-7">
                                                <strong>Остановка №{stopData.number ? stopData.number : '?'}</strong>
                                            </h6>
                                            <h5 className="item-subtitle mbr-fonts-style mt-0 mb-1 ">
                                                Адрес: {stopData.address ? stopData.address.name : '?'}
                                            </h5>
                                            <div className="item-wrapper border p-4 my-4">
                                                <div className="item-head">
                                                    <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                                        <strong>Заказ №{stopData.order.id}</strong>
                                                    </h6>
                                                    <h5 className="item-title mbr-fonts-style mb-0 display-7">
                                                        <strong>Время оформления:</strong> {stopData.order.time.toLocaleString()}
                                                    </h5>
                                                    <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                                        <strong>Адрес загрузки:</strong> {stopData.order.loadAddress?.name}
                                                    </h6>
                                                    <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                                        <strong>Адрес доставки:</strong> {stopData.order.deliverAddress?.name}
                                                    </h6>
                                                    <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                                        <strong>Клиент:</strong> {stopData.order.client?.name}
                                                    </h6>
                                                    <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                                        <strong>Статус:</strong> {orderStatuses.find(v => v.id == stopData.order.orderStatus?.id)?.name}
                                                    </h6>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            )
                        }
                        <button className="btn btn-outline-secondary col-4"
                            data-bs-toggle="modal" data-bs-target="#orderId">
                            +
                        </button>
                        <OrderEdit id='orderId' onSelectOrder={onAddStop} />
                    </div>
            case 'showMap':
                {
                    return <div className='d-flex justify-content-center w-100'>
                        <MapRoutes stops={shipData.stops}>
                            {
                                shipData.autoparkStart &&
                                <AutoparkMarker autoparkData={shipData.autoparkStart} />
                            }
                            {
                                shipData.autoparkFinish &&
                                shipData.autoparkFinish != shipData.autoparkStart?.id &&
                                <AutoparkMarker autoparkData={shipData.autoparkFinish} />
                            }
                        </MapRoutes>
                    </div>
                }

        }
    }

    return (
        <div className="container mt-5 py-5">
            {
                !editing &&
                <div className="row justify-content-center mt-5">
                    <div className="col-12 col-md-12 col-lg">
                        <div className="text-wrapper align-left">
                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-4">
                                <strong>Рейс №{shipData.id}</strong>
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Машина: </strong>
                                <span>Машина №{shipData.car.id}; {shipData.car.mark}; {shipData.car.licenseNumber}; {carTypes.find(v => v.id == shipData.car.carType.id)?.name}</span>
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Прицеп: </strong>
                                {
                                    shipData.trailer ?
                                        <>
                                            Прицеп №{shipData.trailer.id}; {shipData.trailer.licenseNumber}; {trailerTypes.find(v => v.id == shipData.trailer.trailerType.id)?.name}
                                        </>
                                        :
                                        <>
                                            -
                                        </>
                                }
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Отправная точка: </strong>
                                Автопарк №{shipData.autoparkStart.id}; {shipData.autoparkStart.address.name}
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Конечная точка: </strong>
                                Автопарк №{shipData.autoparkFinish.id}; {shipData.autoparkFinish.address.name}
                            </h1>

                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Оператор: </strong>
                                {shipData.operator.secondName} {shipData.operator.firstName} {shipData.operator.middleName}
                            </h1>
                        </div>
                    </div>
                    <div className="col-12 col-md-12 col-lg">
                        <div className="text-wrapper align-left">
                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Время начала исполнения: </strong>
                                {
                                    shipData.start ?
                                        <>
                                            {shipData.start.toLocaleString()}
                                        </>
                                        :
                                        <>
                                            -
                                        </>
                                }
                            </h1>
                            <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                <strong>Время окончания исполнения: </strong>
                                {
                                    shipData.finish ?
                                        <>
                                            {shipData.finish.toLocaleString()}
                                        </>
                                        :
                                        <>
                                            -
                                        </>
                                }
                            </h1>
                        </div>
                    </div>
                </div>
            }

            {
                editing &&
                <div className="row justify-content-center mt-5">
                    <h1 className="mbr-section-title mbr-fonts-style display-4">
                        <strong>Рейс №{shipData.id}</strong>
                    </h1>

                    <h1 className="mbr-section-title mbr-fonts-style display-7">
                        <button className='btn btn-primary rounded-pill display-7'
                            data-bs-toggle="modal" data-bs-target="#carId">
                            <strong>
                                Машина:
                                <span> Машина №
                                    {shipData.car.id}; {shipData.car.mark}; {shipData.car.licenseNumber}; {carTypes.find(v => v.id == shipData.car.carType.id)?.name}
                                </span>
                            </strong>
                        </button>
                    </h1>
                    <CarEdit onSelectCar={onCarChange} id='carId' />

                    <h1 className="mbr-section-title mbr-fonts-style display-7">
                        <button className='btn btn-primary rounded-pill display-7'
                            data-bs-toggle="modal" data-bs-target="#trailerId">
                            <strong>
                                Прицеп:
                                {
                                    shipData.trailer ?
                                        <span> Прицеп №
                                            {shipData.trailer.id}; {shipData.trailer.licenseNumber}; {trailerTypes.find(v => v.id == shipData.trailer.trailerType.id)?.name}
                                        </span>
                                        :
                                        <>-</>
                                }

                            </strong>
                        </button>
                    </h1>
                    <TrailerEdit onSelectTrailer={onTrailerChange} nullable={true} id='trailerId' />

                    <h1 className="mbr-section-title mbr-fonts-style display-7">
                        <button className='btn btn-primary rounded-pill display-7'
                            data-bs-toggle="modal" data-bs-target="#autoparkStartId">
                            <strong>
                                Отправная точка:
                                <span> Автопарк №{shipData.autoparkStart.id}; {shipData.autoparkStart.address.name}</span>
                            </strong>
                        </button>
                    </h1>
                    <AutoparkEdit onSelectAutopark={onStartAutoparkChange} id='autoparkStartId' />

                    <h1 className="mbr-section-title mbr-fonts-style display-7">
                        <button className='btn btn-primary rounded-pill display-7'
                            data-bs-toggle="modal" data-bs-target="#autoparkFinishId">
                            <strong>
                                Конечная точка:
                                <span> Автопарк №{shipData.autoparkFinish.id}; {shipData.autoparkFinish.address.name}</span>
                            </strong>
                        </button>
                    </h1>
                    <AutoparkEdit onSelectAutopark={onFinishAutoparkChange} id='autoparkFinishId' />
                </div>
            }

            {
                !editing &&
                <>
                    {
                        user && user.operator &&
                        <>
                            <div className="btn btn-primary display-3" onClick={handleEditingStart}>Изменить</div>
                            <div className="btn btn-danger display-3" onClick={handleDeleteClick}>Удалить</div>
                        </>
                    }
                    {
                        user && (user.driver || user.operator) && (!shipData.start || !shipData.end) &&
                        <button onClick={handleMarkClick} className="btn btn-success display-3">{!shipData.start ?
                            'Принять к исполнению'
                            :
                            'Окончить исполение'}</button>
                    }

                </>
            }
            {
                editing &&
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

            <div className="btn-group d-flex flex-row justify-content-center mb-3 w-100">
                <input type="radio" className="btn-check"
                    name="btnradio" id="showDrivers"
                    autoComplete="off" onClick={handleShowListChange} defaultChecked={true} />
                <label className="btn btn-outline-dark" htmlFor="showDrivers">Водители</label>
                <input type="radio" className="btn-check"
                    name="btnradio" id="showStops"
                    autoComplete="off" onClick={handleShowListChange} />
                <label className="btn btn-outline-dark" htmlFor="showStops">Заказы</label>
                <input type="radio" className="btn-check"
                    name="btnradio" id="showMap"
                    autoComplete="off" onClick={handleShowListChange} />
                <label className="btn btn-outline-dark" htmlFor="showMap">Карта</label>

            </div>

            {
                switchRenderList()
            }

            <div className="d-flex align-items-center text-warning m-4 rounded-pill px-4 py-2 display-4 fixed-bottom bg-dark" style={{ visibility: loading ? 'visible' : 'hidden' }}>
                <strong>Загрузка...</strong>
                <div className="spinner-border ms-auto" role="status" aria-hidden="true"></div>
            </div>
        </div >
    )
}
