import React from 'react'
import { Link } from 'react-router-dom'

export default function Header() {
    return (
        <section data-bs-version="5.1" className="menu menu2 cid-ud3KesCX8K" id="menu-5-ud3KesCX8K">

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

                            <li className="nav-item">
                                <Link className="nav-link link text-black display-4" aria-expanded="false" to={"/register"}>Регистрация</Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link link text-black display-4" aria-expanded="false" to={"/registerOther"}>Регистрация стороннего</Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link link text-black display-4" aria-expanded="false" to={"/login"}>Авторизация</Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link link text-black display-4" aria-expanded="false" to={"/register"}>Заказы</Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link link text-black display-4" aria-expanded="false" to={"/users"}>Пользователи</Link>
                            </li>
                            <li className="nav-item">
                                <Link className="nav-link link text-black display-4" aria-expanded="false" to={"/autoparks"}>Автопарки</Link>
                            </li>


                        </ul>

                        <div className="navbar-buttons mbr-section-btn"><Link to={"/profile"} className="btn btn-primary display-4"
                        >Профиль</Link></div>
                    </div>
                </div>
            </nav>
        </section>
    )
}
