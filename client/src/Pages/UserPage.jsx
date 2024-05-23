import React, { useEffect, useState } from 'react'
import { useFetching } from '../Hooks/useFetching'
import UserService from '../Services/UserService'
import userRoles from '../Data/UserRoles.json'
import { useNavigate, useParams } from 'react-router'

export default function UserPage() {

    const { userId } = useParams()

    const [userData, setUserData] = useState({
        id: undefined,
        userName: null,
        email: null,
        phoneNumber: null,
        role: null,
        roleName: null,
    })

    const navigate = useNavigate()

    const [fetch, loading, error] = useFetching(async (type) => {
        switch (type) {
            case "get":
                {
                    const res = await UserService.GetById(userId)
                    let tempUserData = res.data
                    let roleNameIt = userRoles.find(v => v.value == tempUserData.role)

                    tempUserData.roleName = roleNameIt ? roleNameIt.name : null

                    setUserData(res.data)
                }
                break;
            case "delete":
                {
                    const res = await UserService.DeleteUser(userId)
                    if (res) {
                        navigate(-1)
                    }
                }
                break;
        }

    })

    const handleDeleteClick = (e) => {
        e.preventDefault()
        fetch("delete")
    }

    useEffect(() => {
        fetch("get")
    }, [userId])

    return (
        <div className="container mt-5 py-5">

            <div className="row justify-content-center mt-5">
                <div className="col-12 col-md-12 col-lg">
                    <div className="text-wrapper align-left">
                        <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                            <strong>Пользователь:</strong> <span>{userData.userName}</span>
                        </h1>

                        <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                            <strong>Роль:</strong> <span>{userData.roleName}</span>
                        </h1>
                    </div>
                </div>
                <div className="col-12 col-md-12 col-lg">
                    <div className="text-wrapper align-left">
                        <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                            <strong>Почта:</strong> <span>{userData.email}</span>
                        </h1>
                        <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                            <strong>Номер телефона:</strong> <span>{userData.phoneNumber}</span>
                        </h1>
                    </div>
                </div>
            </div>

            <div className="btn btn-danger display-3" onClick={handleDeleteClick}>Удалить</div>

            {
                error &&
                <div className="border border-danger border rounded-4 p-2 px-4 mt-2">
                    <span className="text-danger text-center h3">{toString(error)}</span>
                </div>
            }
        </div >
    )
}
