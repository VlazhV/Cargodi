import React, { useEffect, useState } from 'react'
import UserService from '../Services/UserService'
import { useFetching } from '../Hooks/useFetching'
import { Link } from 'react-router-dom'

export default function UsersPage() {

    const [usersData, setUsersData] = useState([])

    const [fetch, loading, error] = useFetching(async (type) => {
        switch (type) {
            case "get":
                {
                    const res = await UserService.GetAll()
                    setUsersData(res.data)
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
                                <strong>Пользователи</strong>
                            </h4>
                        </div>
                    </div>
                </div>
                <div className="row">

                    {
                        usersData.map(userData =>
                            <div className="item features-without-image col-12 col-md-6 col-lg-3 item-mb active">
                                <div className="item-wrapper">
                                    <div className="item-head">
                                        <h5 className="item-title mbr-fonts-style mb-0 display-5">
                                            <strong>{userData.userName}</strong>
                                        </h5>
                                        <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                            <strong>{userData.email}</strong>
                                        </h6>
                                        <h6 className="item-subtitle mbr-fonts-style mt-0 mb-0 display-7">
                                            <strong>{userData.phoneNumber}</strong>
                                        </h6>
                                    </div>

                                    <div className="mbr-section-btn item-footer">
                                        <Link to={"/user/" + userData.id} className="btn item-btn btn-primary display-7">Открыть профиль</Link>
                                    </div>
                                </div>
                            </div>
                        )
                    }

                </div>
            </div>

            {
                error &&
                <div className="border border-danger border rounded-4 p-2 px-4 mt-2">
                    <span className="text-danger text-center h3">{typeof error === 'string' ? error : toString(error)}</span>
                </div>
            }
        </div>
    )
}
