import React, { useEffect, useState } from 'react'
import { useFetching } from '../Hooks/useFetching'
import CarService from '../Services/CarService'
import carTypes from '../Data/CarTypes.json'
import ClientService from '../Services/ClientService'

export default function ClientEdit(props) {
    const id = props.id ? props.id : null
    const onSelectClient = props.onSelectClient ? props.onSelectClient : (newClientData) => { }

    const [clientsData, setClientsData] = useState([])

    const handleSelectClient = (e) => {
        e.preventDefault()
        let index = e.target.id
        onSelectClient({ ...(clientsData[index]) })
    }

    const [fetch, loading, error] = useFetching(async (type) => {
        switch (type) {
            case "get":
                {
                    const res = await ClientService.GetAll()
                    setClientsData(res.data)
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
                        <h1 className="modal-title fs-5">Клиенты</h1>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>

                    </div>
                    <div className="modal-body d-flex flex-column">
                        {
                            clientsData.map((clientData, index) => {
                                return <button className='btn btn-outline-primary' data-bs-dismiss="modal" key={clientData.id} id={index} onClick={handleSelectClient}>
                                    Клиент №{clientData.id}
                                    <br />
                                    Имя: {clientData.name}
                                    <br />
                                    {clientData.user.userName}; {clientData.user.email}; {clientData.user.phoneNumber}
                                </button>
                            })
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
