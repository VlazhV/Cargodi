import React, { useEffect, useState } from 'react'
import { useFetching } from '../Hooks/useFetching'
import UserService from '../Services/UserService'
import userRoles from '../Data/UserRoles.json'

export default function ProfilePage() {

    const [editing, setEditing] = useState(false)
    const [editingPassword, setEditingPassword] = useState(false)

    const [userData, setUserData] = useState({
        id: undefined,
        userName: null,
        email: null,
        phoneNumber: null,
        role: null,
        roleName: null,
        client: null,
        operator: null,
        driver: null
    })

    const [oldPassword, setOldPassword] = useState("")
    const [newPassword, setNewPassword] = useState("")

    const [fetch, loading, error] = useFetching(async (type) => {
        switch (type) {
            case "get":
                {
                    const res = await UserService.GetProfile()
                    let tempUserData = res.data
                    let roleNameIt = userRoles.find(v => v.value == tempUserData.role)

                    tempUserData.roleName = roleNameIt ? roleNameIt.name : null

                    if (tempUserData.operator) {
                        tempUserData.operator.employDate = new Date(tempUserData.operator.employDate)
                        if (tempUserData.operator.fireDate) {
                            tempUserData.operator.fireDate = new Date(tempUserData.operator.fireDate)
                        }
                    }

                    if (tempUserData.driver) {
                        tempUserData.driver.employDate = new Date(tempUserData.driver.employDate)
                        if (tempUserData.driver.fireDate) {
                            tempUserData.driver.fireDate = new Date(tempUserData.driver.fireDate)
                        }
                    }


                    setUserData(tempUserData)
                }
                break;
            case "update":
                {
                    const res = await UserService.UpdateProfile(userData.userName,
                        userData.email,
                        userData.phoneNumber)

                    let tempUserData = res.data
                    let roleNameIt = userRoles.find(v => v.value == tempUserData.role)

                    tempUserData.roleName = roleNameIt ? roleNameIt.name : null

                    if (tempUserData.operator) {
                        tempUserData.operator.employDate = new Date(tempUserData.operator.employDate)
                        if (tempUserData.operator.fireDate) {
                            tempUserData.operator.fireDate = new Date(tempUserData.operator.fireDate)
                        }
                    }

                    if (tempUserData.driver) {
                        tempUserData.driver.employDate = new Date(tempUserData.driver.employDate)
                        if (tempUserData.driver.fireDate) {
                            tempUserData.driver.fireDate = new Date(tempUserData.driver.fireDate)
                        }
                    }

                    setUserData(tempUserData)
                }
                break;
            case "updatePassword":
                {
                    const res = await UserService.UpdateProfilePassword(oldPassword,
                        newPassword)

                    setEditingPassword(false)
                    setNewPassword("")
                    setOldPassword("")
                }
                break;
        }

    })

    const handleChange = (e) => {
        setUserData(prev => ({ ...prev, [e.target.id]: e.target.value }))
    }

    const handleChangePassword = (e) => {
        if (e.target.id === "oldPassword") {
            setOldPassword(e.target.value)
        }
        else if (e.target.id === "newPassword") {
            setNewPassword(e.target.value)
        }
    }

    const handleConfirmChangePassword = (e) => {
        e.preventDefault()
        fetch("updatePassword")
    }

    const handleEditingPasswordClick = (e) => {
        e.preventDefault()
        setEditingPassword(true)
    }

    const handleEditingClick = (e) => {
        e.preventDefault()
        setEditing(true)
    }

    const handleCancelEditingClick = (e) => {
        e.preventDefault()
        setEditing(false)
        setEditingPassword(false)
        fetch("get")
    }

    const handleSaveEditingClick = (e) => {
        e.preventDefault()
        setEditing(false)
        fetch("update")
    }

    useEffect(() => {
        if (!editing)
            fetch("get")
    }, [])

    return (
        <div className="container mt-5 py-5">

            {!editingPassword &&
                <>


                    <div className="row justify-content-center mt-5">
                        <div className="col-12 col-md-12 col-lg">
                            <div className="text-wrapper align-left">
                                {!editing &&
                                    <>
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
                                    </>
                                }
                                {
                                    editing &&
                                    <>
                                        <h1 className="mb-4 input-group">
                                            <strong className='input-group-text display-7'>Пользователь:</strong>
                                            <input name="input" value={userData.userName} type='text'
                                                className="form-control mx-2" id="userName" onChange={handleChange}></input>
                                        </h1>
                                        <h1 className="mbr-section-title mbr-fonts-style mb-4 display-7">
                                            <strong>Роль:</strong> <span>{userData.roleName}</span>
                                        </h1>
                                        <h1 className="mb-4 input-group">
                                            <strong className='input-group-text display-7'>Почта:</strong>
                                            <input name="input" value={userData.email} type='email'
                                                className="form-control mx-2" id="email" onChange={handleChange}></input>
                                        </h1>
                                        <h1 className="mb-4 input-group">
                                            <strong className='input-group-text display-7'>Номер телефона:</strong>
                                            <input name="input" value={userData.phoneNumber} type='text'
                                                className="form-control mx-2" id="phoneNumber" onChange={handleChange}></input>
                                        </h1>
                                    </>
                                }
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
                        !editing &&
                        <>
                            <div className="btn btn-primary display-3" onClick={handleEditingClick}>Редактировать</div>
                            <div className="btn btn-primary display-3" onClick={handleEditingPasswordClick}>Сменить пароль</div>
                        </>
                    }
                    {
                        editing &&
                        <>
                            <div className="btn btn-primary display-3" onClick={handleSaveEditingClick}>Сохранить</div>
                            <div className="btn btn-primary display-3" onClick={handleCancelEditingClick}>Отмена</div>
                        </>
                    }
                </>
            }
            {editingPassword &&
                <>
                    <div className="row justify-content-center mt-5">
                        <h1 className="mb-4 input-group">
                            <strong className='input-group-text display-7'>Старый пароль:</strong>
                            <input name="input" value={oldPassword} type='password'
                                className="form-control mx-2" id="oldPassword" onChange={handleChangePassword}></input>
                        </h1>
                        <h1 className="mb-4 input-group">
                            <strong className='input-group-text display-7'>Новый пароль:</strong>
                            <input name="input" value={newPassword} type='password'
                                className="form-control mx-2" id="newPassword" onChange={handleChangePassword}></input>
                        </h1>
                    </div>

                    <div className="btn btn-primary display-3" onClick={handleConfirmChangePassword}>Сменить пароль</div>
                    <div className="btn btn-primary display-3" onClick={handleCancelEditingClick}>Отмена</div>
                </>
            }

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
