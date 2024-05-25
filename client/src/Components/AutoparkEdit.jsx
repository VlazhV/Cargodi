import React, { useEffect, useState } from 'react'
import { useFetching } from '../Hooks/useFetching'
import AutoparkService from '../Services/AutoparkService'

export default function AutoparkEdit(props) {
    const id = props.id ? props.id : null
    const onSelectAutopark = props.onSelectAutopark ? props.onSelectAutopark : (autoparkId) => { }

    const handleSelectAutopark = (e) => {
        e.preventDefault()
        onSelectAutopark(e.target.id)
    }

    const [autoparksData, setAutoparksData] = useState([])

    const [fetch, loading, error] = useFetching(async (type) => {
        switch (type) {
            case "get":
                {
                    const res = await AutoparkService.GetAll()
                    setAutoparksData(res.data)
                }
                break;
        }

    })

    useEffect(() => {
        fetch("get")
    }, [])

    return (
        <div className="modal fade" id={id} tabIndex="-1">
            <div className="modal-dialog">
                <div className="modal-content">
                    <div className="modal-header">
                        <h1 className="modal-title fs-5">Автопарки</h1>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div className="modal-body">
                        {
                            autoparksData.map(autoparkData => {
                                return <button className='btn btn-outline-primary w-100' data-bs-dismiss="modal" key={autoparkData.id} id={autoparkData.id} onClick={handleSelectAutopark}>
                                    Автопарк №{autoparkData.id}
                                    <br />
                                    Адрес: {autoparkData.address.name}
                                </button>
                            })
                        }
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    )
}
