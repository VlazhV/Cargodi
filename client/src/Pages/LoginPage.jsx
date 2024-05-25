import React, { useContext, useState } from 'react';
import { AuthContext } from "../Contexts/AuthContext";
import AuthService from "../Services/AuthService";
import { useNavigate } from "react-router-dom";

function LoginPage(props) {
    const [credentials, setCredentials] = useState({
        userName: undefined,
        password: undefined,
    })

    const { loading, error, dispatch } = useContext(AuthContext)

    const navigate = useNavigate()

    const handleChange = (e) => {
        setCredentials(prev => ({ ...prev, [e.target.id]: e.target.value }))
    }

    const handleClick = async (e) => {
        e.preventDefault()
        dispatch({ type: "LOGIN_START" })
        try {
            const res = await AuthService.Login(credentials.userName, credentials.password)
            dispatch({ type: "LOGIN_SUCCESS", payload: res })
            navigate("/")
        } catch (err) {
            dispatch({ type: "LOGIN_FAILURE", payload: err.response ? err.response.data.ErrorMessage : err })
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
                                    <strong>Авторизация</strong>
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
                                        <input name="input" placeholder="Никнейм" type='text' data-form-field="input"
                                            className="form-control" id="userName" onChange={handleChange}></input>
                                    </div>

                                    <div className="col-12 form-group mb-3" data-for="textarea">
                                        <input name="input" placeholder="Пароль" type='password' data-form-field="input"
                                            className="form-control" id="password" onChange={handleChange}></input>
                                    </div>

                                    <div className="col-lg-12 col-md-12 col-sm-12 align-center mbr-section-btn">
                                        <button className="btn btn-primary display-7" onClick={handleClick}>Авторизация</button>
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
        </div >
    );
}

export default LoginPage;