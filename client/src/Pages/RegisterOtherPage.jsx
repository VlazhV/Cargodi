import React, { useContext, useEffect, useState } from 'react';
import { AuthContext } from "../Contexts/AuthContext";
import AuthService from "../Services/AuthService";
import { useNavigate } from "react-router-dom";
import userRoles from '../Data/UserRoles.json';
import AutoparkEdit from '../Components/AutoparkEdit';
import { useFetching } from '../Hooks/useFetching';

function RegisterOtherPage(props) {
    const [credentials, setCredentials] = useState({
        userName: '',
        role: 'admin',
        phoneNumber: '',
        email: '',
        client: {
            name: '',
        },
        driver: {
            license: '',
            secondName: '',
            firstName: '',
            middleName: '',
            autoparkId: null,
            actualAutoparkId: null
        },
        operator: {
            secondName: '',
            firstName: '',
            middleName: '',
            autoparkId: null,
        }
    })

    const checkCredentials = () => {
        const c1 = credentials.email && credentials.userName && credentials.phoneNumber && credentials.role;

        if (!c1) {
            return false;
        }

        const c2 = credentials.client && !credentials.driver && !credentials.operator;

        if (c2) {
            if (credentials.client.name) {
                return true;
            }
        }

        const c3 = !credentials.client && credentials.driver && !credentials.operator;

        if (c3) {
            if (credentials.driver.actualAutoparkId &&
                credentials.driver.autoparkId &&
                credentials.driver.firstName &&
                credentials.driver.license &&
                credentials.driver.middleName &&
                credentials.driver.secondName
            ) {
                return true;
            }
        }

        const c4 = !credentials.client && !credentials.driver && credentials.operator;

        if (c4) {
            if (credentials.operator.autoparkId &&
                credentials.operator.firstName &&
                credentials.operator.middleName &&
                credentials.operator.secondName
            ) {
                return true;
            }
        }

        return false;
    }

    const [fetch, loading, error] = useFetching(async (type) => {
        switch (type) {
            case 'register':
                {
                    const res = await AuthService.Register(credentials)
                    navigate("/")
                }
                break;
        }
    })

    const navigate = useNavigate()

    const handleChange = (e) => {
        setCredentials(prev => ({ ...prev, [e.target.id]: e.target.value }))
    }

    const handleClientChange = (e) => {
        setCredentials(prev => ({ ...prev, client: { ...prev.client, [e.target.id]: e.target.value } }))
    }

    const handleDriverChange = (e) => {
        setCredentials(prev => ({ ...prev, driver: { ...prev.driver, [e.target.id]: e.target.value } }))
    }

    const handleOperatorChange = (e) => {
        setCredentials(prev => ({ ...prev, operator: { ...prev.operator, [e.target.id]: e.target.value } }))
    }

    const changeRole = (role) => {
        switch (role) {
            case "manager":
            case "admin":
                credentials.client = null
                credentials.driver = null
                credentials.operator = {
                    secondName: '',
                    firstName: '',
                    middleName: '',
                    autoparkId: null,
                }
                break;
            case "driver":
                credentials.client = null
                credentials.driver = {
                    license: '',
                    secondName: '',
                    firstName: '',
                    middleName: '',
                    autoparkId: null,
                    actualAutoparkId: null
                }
                credentials.operator = null
                break;
            case "client":
                credentials.client = {
                    name: '',
                }
                credentials.driver = null
                credentials.operator = null
                break;
        }
        setCredentials(prev => ({ ...prev }))
    }


    const handleChangeRole = (e) => {
        changeRole(e.target.value)
        handleChange(e)
    }

    const handleOperatorAutoparkChange = (newAutoparkData) => {
        setCredentials(prev => ({ ...prev, operator: { ...prev.operator, autoparkId: newAutoparkData.id } }))
    }

    const handleDriverAutoparkChange = (newAutoparkData) => {
        setCredentials(prev => ({ ...prev, driver: { ...prev.driver, autoparkId: newAutoparkData.id } }))
    }

    const handleActualAutoparkChange = (newAutoparkData) => {
        setCredentials(prev => ({ ...prev, driver: { ...prev.driver, actualAutoparkId: newAutoparkData.id } }))
    }

    const handleClick = async (e) => {
        e.preventDefault()
        if (checkCredentials()) {
            fetch('register')
        }
    }

    useEffect(() => {
        changeRole('admin')
    }, [])

    const switchRole = (role) => {
        switch (role) {
            case "manager":
            case "admin":
                return (
                    <div>

                        <div className="col-12 form-group mb-3" data-for="textarea">
                            <input name="input" placeholder="Фамилия" type="text" data-form-field="input"
                                className="form-control" id="secondName" onChange={handleOperatorChange}></input>
                        </div>

                        <div className="col-12 form-group mb-3" data-for="textarea">
                            <input name="input" placeholder="Имя" type="text" data-form-field="input"
                                className="form-control" id="firstName" onChange={handleOperatorChange}></input>
                        </div>

                        <div className="col-12 form-group mb-3" data-for="textarea">
                            <input name="input" placeholder="Отчество" type="text" data-form-field="input"
                                className="form-control" id="middleName" onChange={handleOperatorChange}></input>
                        </div>

                        <div>
                            <h4>
                                <strong>Приписка:</strong> <span>Автопарк №{credentials.operator.autoparkId}</span>
                            </h4>
                        </div>

                        <button type="button" className="btn btn-primary display-3"
                            data-bs-toggle="modal" data-bs-target="#operatorAutoparkId">Назначить приписку</button>



                        <AutoparkEdit id="operatorAutoparkId" onSelectAutopark={handleOperatorAutoparkChange} />
                    </div>
                )
            case "driver":
                return (

                    <div>
                        <div className="col-12 form-group mb-3" data-for="textarea">
                            <input name="input" placeholder="Водительское удостоверение" type="text" data-form-field="input"
                                className="form-control" id="license" onChange={handleDriverChange}></input>
                        </div>

                        <div className="col-12 form-group mb-3" data-for="textarea">
                            <input name="input" placeholder="Фамилия" type="text" data-form-field="input"
                                className="form-control" id="secondName" onChange={handleDriverChange}></input>
                        </div>

                        <div className="col-12 form-group mb-3" data-for="textarea">
                            <input name="input" placeholder="Имя" type="text" data-form-field="input"
                                className="form-control" id="firstName" onChange={handleDriverChange}></input>
                        </div>

                        <div className="col-12 form-group mb-3" data-for="textarea">
                            <input name="input" placeholder="Отчество" type="text" data-form-field="input"
                                className="form-control" id="middleName" onChange={handleDriverChange}></input>
                        </div>

                        <div>
                            <h4>
                                <strong>Приписка:</strong> <span>Автопарк №{credentials.driver.autoparkId}</span>
                            </h4>
                        </div>
                        <div>
                            <h4>
                                <strong>Текущий автопарк:</strong> <span>Автопарк №{credentials.driver.actualAutoparkId}</span>
                            </h4>
                        </div>

                        <button type="button" className="btn btn-primary display-3"
                            data-bs-toggle="modal" data-bs-target="#driverAutoparkId">Назначить приписку</button>
                        <button type="button" className="btn btn-primary display-3"
                            data-bs-toggle="modal" data-bs-target="#driverActualAutoparkId">Назначить текущий автопарк</button>

                        <AutoparkEdit id="driverAutoparkId" onSelectAutopark={handleDriverAutoparkChange} />
                        <AutoparkEdit id="driverActualAutoparkId" onSelectAutopark={handleActualAutoparkChange} />
                    </div>
                )
            case "client":
                return (
                    <div className="col-12 form-group mb-3" data-for="textarea">
                        <input name="input" placeholder="Имя" type="text" data-form-field="input"
                            className="form-control" id="name" onChange={handleClientChange}></input>
                    </div>
                )
        }
    }


    return (
        <div>
            <section data-bs-version="5.1" className="form5 cid-ud3KesHTYC" id="contact-form-2-ud3KesHTYC">


                <div className="container">
                    <div className="row justify-content-center">
                        <div className="col-12 content-head">
                            <div className="mbr-section-head mb-5 mt-5">
                                <h3 className="mbr-section-title mbr-fonts-style align-center mb-0 display-2">
                                    <strong>Регистрация стороннего пользователя</strong>
                                </h3>

                            </div>
                        </div>
                    </div>
                    <div className="row justify-content-center">
                        <div className="col-lg-8 mx-auto mbr-form" data-form-type="formoid">
                            <form className="mbr-form form-with-styler"
                                data-form-title="Form Name"><input type="hidden" name="email" data-form-email="true"
                                    value="Ky1MAY5B1zYpUkuQChRmHcefA594xSl1d4+lCVG/6tGeUasfRT25pdpYagbLk3yLlZsKdIhi2GKQ3VFtT+pZ5GAPn22ntoC/7vSCC672JeZx+w/sbr+Bn4RWWFA3Hsux" />
                                <div className="dragArea row">
                                    <div className="col-12 form-group mb-3" data-for="textarea">
                                        <input name="input" placeholder="Почта" type="email" data-form-field="input"
                                            className="form-control" id="email" onChange={handleChange}></input>
                                    </div>

                                    <div className="col-12 form-group mb-3" data-for="textarea">
                                        <input name="input" placeholder="Никнейм" type='text' data-form-field="input"
                                            className="form-control" id="userName" onChange={handleChange}></input>
                                    </div>

                                    <div className="col-12 form-group mb-3" data-for="input">
                                        <input name="input" placeholder="Номер телефона" data-form-field="input"
                                            className="form-control" id="phoneNumber" onChange={handleChange}></input>
                                    </div>

                                    <div className="col-12 form-group mb-3" data-for="input">

                                        <select name="select" className='form-select display-7 p-3' onChange={handleChangeRole} id="role">
                                            {
                                                userRoles.map(role => <option value={role.value} key={role.value}>{role.name}</option>)
                                            }
                                        </select>
                                    </div>

                                    <div>
                                        {
                                            switchRole(credentials.role)
                                        }
                                    </div>

                                    <div className="col-lg-12 col-md-12 col-sm-12 align-center mbr-section-btn">
                                        <button className="btn btn-primary display-7" onClick={handleClick}>Зарегестрировать</button>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </section>

            {error &&
                <div className="border border-danger border rounded-4 p-2 px-4 mt-2">
                    <span className="text-danger text-center h3">{typeof error === 'string' ? error : toString(error)}</span>
                </div>}
            <div className="d-flex align-items-center text-warning m-4 rounded-pill px-4 py-2 display-4 fixed-bottom bg-dark" style={{ visibility: loading ? 'visible' : 'hidden' }}>
                <strong>Загрузка...</strong>
                <div className="spinner-border ms-auto" role="status" aria-hidden="true"></div>
            </div>
        </div >
    );
}

export default RegisterOtherPage;