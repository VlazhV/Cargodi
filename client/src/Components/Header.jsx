import React, { useContext } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { AuthContext } from '../Contexts/AuthContext'

export default function Header() {
    const { user, dispatch } = useContext(AuthContext)

    const navigate = useNavigate()

    const handleLogoutClick = (e) => {
        dispatch({ type: "LOGOUT" })
        navigate('/')
    }

    return (
        <section className="menu menu2 cid-ud3KesCX8K">

            <nav className="navbar navbar-dropdown navbar-fixed-top navbar-expand-lg">
                <div className="container">
                    <div className="navbar-brand">

                        <span className="navbar-caption-wrap"><Link className="navbar-caption text-black display-4"
                            to={"/"}>CARGODI</Link></span>
                    </div>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-bs-toggle="collapse"
                        data-target="#navbarSupportedContent" data-bs-target="#navbarSupportedContent"
                        aria-controls="navbarNavAltMarkup" aria-expanded="false" aria-label="Toggle navigation">
                        <div className="hamburger">
                            <span></span>
                            <span></span>
                            <span></span>
                            <span></span>
                        </div>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul className="navbar-nav nav-dropdown" data-app-modern-menu="true">

                            {
                                user && user.client &&
                                <>
                                    <li className="nav-item">
                                        <Link className="nav-link link text-black display-4" aria-expanded="false" to={"/orders/create"}>Сделать заказ</Link>
                                    </li>
                                    <li className="nav-item">
                                        <Link className="nav-link link text-black display-4" aria-expanded="false" to={"/orders/my"}>Мои заказы</Link>
                                    </li>
                                </>
                            }
                            {
                                user && user.operator &&
                                <>
                                    <li className="nav-item">
                                        <Link className="nav-link link text-black display-4" aria-expanded="false" to={"/registerOther"}>Регистрация стороннего</Link>
                                    </li>
                                    <li className="nav-item">
                                        <Link className="nav-link link text-black display-4" aria-expanded="false" to={"/users"}>Пользователи</Link>
                                    </li>
                                    <li className="nav-item">
                                        <Link className="nav-link link text-black display-4" aria-expanded="false" to={"/autoparks"}>Автопарки</Link>
                                    </li>
                                    <li className="nav-item">
                                        <Link className="nav-link link text-black display-4" aria-expanded="false" to={"/orders/operator"}>Заказы</Link>
                                    </li>
                                </>
                            }

                            {
                                !user &&
                                <>
                                    <li className="nav-item">
                                        <Link className="nav-link link text-black display-4" aria-expanded="false" to={"/register"}>Регистрация</Link>
                                    </li>
                                    <li className="nav-item">
                                        <Link className="nav-link link text-black display-4" aria-expanded="false" to={"/login"}>Авторизация</Link>
                                    </li>
                                </>
                            }
                        </ul>

                        {
                            user &&
                            <>
                                <div className="navbar-buttons mbr-section-btn d-flex flex-row">
                                    <div className='input-group'>
                                        <button className="btn btn-warning" onClick={handleLogoutClick}>Выйти</button>
                                        <Link to={"/profile"} className="btn btn-primary">Профиль</Link>
                                    </div>
                                </div>

                            </>
                        }

                    </div>
                </div>
            </nav>
        </section>
    )
}
