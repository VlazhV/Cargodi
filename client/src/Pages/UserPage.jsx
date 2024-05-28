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
                        <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                            <strong>Почта:</strong> <span>{userData.email}</span>
                        </h1>
                        <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                            <strong>Номер телефона:</strong> <span>{userData.phoneNumber}</span>
                        </h1>
                    </div>
                </div>
                <div className="col-12 col-md-12 col-lg">
                    <div className="text-wrapper align-left">
                        {
                            userData.client &&
                            <>
                                <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                    <strong>Имя:</strong> <span>{userData.client.name}</span>
                                </h1>
                            </>
                        }
                        {
                            userData.operator &&
                            <>
                                <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                    <strong>Фамилия:</strong> <span>{userData.operator.secondName}</span>
                                </h1>

                                <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                    <strong>Имя:</strong> <span>{userData.operator.firstName}</span>
                                </h1>

                                <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                    <strong>Отчество:</strong> <span>{userData.operator.middleName}</span>
                                </h1>

                                <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                    <strong>Время нанятия:</strong> <span>{userData.operator.employDate?.toLocaleString()}</span>
                                </h1>

                                <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                    <strong>Время увольнения:</strong> <span>{userData.operator.fireDate ? userData.operator.fireDate.toLocaleString() : '-'}</span>
                                </h1>
                            </>
                        }
                        {
                            userData.driver &&
                            <>
                                <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                    <strong>Вод. удостоверение:</strong> <span>{userData.driver.license}</span>
                                </h1>

                                <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                    <strong>Фамилия:</strong> <span>{userData.driver.secondName}</span>
                                </h1>

                                <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                    <strong>Имя:</strong> <span>{userData.driver.firstName}</span>
                                </h1>

                                <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                    <strong>Отчество:</strong> <span>{userData.driver.middleName}</span>
                                </h1>

                                <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                    <strong>Время нанятия:</strong> <span>{userData.driver.employDate?.toLocaleString()}</span>
                                </h1>

                                <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                    <strong>Время увольнения:</strong> <span>{userData.driver.fireDate ? userData.operator.fireDate.toLocaleString() : '-'}</span>
                                </h1>
                            </>
                        }
                    </div>
                </div>
            </div>

            {
                error &&
                <div className="border border-danger border rounded-4 p-2 px-4 mt-2">
                    <span className="text-danger text-center h3">{typeof error === 'string' ? error : toString(error)}</span>
                </div>
            }
            <div className="d-flex align-items-center text-warning m-4 rounded-pill px-4 py-2 display-4 fixed-bottom bg-dark" style={{ visibility: loading ? 'visible' : 'hidden' }}>
                <strong>Загрузка...</strong>
                <div className="spinner-border ms-auto" role="status" aria-hidden="true"></div>
            </div>
        </div >
    )
}
