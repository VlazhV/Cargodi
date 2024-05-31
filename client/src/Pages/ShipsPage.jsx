import React, { useEffect, useState } from 'react'
import { useFetching } from '../Hooks/useFetching'
import { Link } from 'react-router-dom'
import OrderService from '../Services/OrderService'
import ShipService from '../Services/ShipService'
import carTypes from '../Data/CarTypes.json'
import orderStatuses from '../Data/OrderStatuses.json'
import trailerTypes from '../Data/TrailerTypes.json'

export default function ShipsPage() {

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

    const [shipsData, setShipsData] = useState([])

    const [fetch, loading, error] = useFetching(async (type) => {
        switch (type) {
            case "get":
                {
                    const res = await ShipService.GetAll()

                    let ships = res.data
                    ships.forEach(s => {
                        if (s.start) {
                            s.start = new Date(s.start)
                        }
                        if (s.finish) {
                            s.finish = new Date(s.finish)
                        }
                    })

                    setShipsData(ships)
                }
                break;
            case "create":
                {
                    const res = await ShipService.Create()
                    fetch("get")
                }
                break;
        }

    })

    const handleGenerateClick = (e) => {
        fetch("create")
    }

    useEffect(() => {
        //fetch("get")
        setShipsData([testShipData1])
    }, [])

    return (
        <div className="container-fluid mt-5 p-5">
            <div className='mt-5'>
                <div className="row justify-content-center">
                    <div className="col-12 content-head">
                        <div className="mbr-section-head mb-5">
                            <h4 className="mbr-section-title mbr-fonts-style align-center mb-0 display-2">
                                <strong>Рейсы</strong>
                            </h4>
                        </div>
                    </div>
                </div>
                <div className="justify-content-center d-flex flex-row mb-4">
                    <button className='btn btn-primary rounded-pill display-7' onClick={handleGenerateClick}>Сгенерировать рейсы</button>
                </div>
                {
                    error &&
                    <div className="border border-danger border rounded-4 p-2 px-4 mt-2">
                        <span className="text-danger text-center h3">{typeof error === 'string' ? error : toString(error)}</span>
                    </div>
                }
                <div className="row">

                    {
                        shipsData.map(shipData =>
                            <div className="item features-without-image col-4 item-mb active border rounded rounded-4" key={shipData.id}>
                                <div className="item-wrapper">
                                    <div className="item-head">
                                        <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                            <strong>Рейс №{shipData.id}</strong>
                                        </h6>
                                        <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                            <strong>Машина:</strong>
                                            <br />
                                            Машина №{shipData.car.id}; {shipData.car.mark}; {shipData.car.licenseNumber}; {carTypes.find(v => v.id == shipData.car.carType.id)?.name}
                                        </h6>
                                        <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                            <strong>Прицеп:</strong>
                                            <br />
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
                                        </h6>
                                        <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                            <strong>Отправная точка:</strong>
                                            <br />
                                            Автопарк №{shipData.autoparkStart.id}; {shipData.autoparkStart.address.name}
                                        </h6>
                                        <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                            <strong>Конечная точка:</strong>
                                            <br />
                                            Автопарк №{shipData.autoparkFinish.id}; {shipData.autoparkFinish.address.name}
                                        </h6>
                                        <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                            <strong>Оператор:</strong>
                                            <br />
                                            {shipData.operator.secondName} {shipData.operator.firstName} {shipData.operator.middleName}
                                        </h6>
                                        <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                            <strong>Время начала исполнения:</strong>
                                            <br />
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
                                        </h6>
                                        <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                            <strong>Время окончания исполнения:</strong>
                                            <br />
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
                                        </h6>
                                    </div>

                                    <div className="mbr-section-btn item-footer">
                                        <Link to={"/ship/" + shipData.id} className="btn item-btn btn-primary display-7">Подробнее</Link>
                                    </div>
                                </div>
                            </div>
                        )
                    }

                </div>
            </div>
            <div className="d-flex align-items-center text-warning m-4 rounded-pill px-4 py-2 display-4 fixed-bottom bg-dark" style={{ visibility: loading ? 'visible' : 'hidden' }}>
                <strong>Загрузка...</strong>
                <div className="spinner-border ms-auto" role="status" aria-hidden="true"></div>
            </div>
        </div>
    )
}
