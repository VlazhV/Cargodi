import React, { useContext } from 'react'
import { Route, Routes } from 'react-router'
import { AuthContext } from '../Contexts/AuthContext'
import AutoparkPage from '../Pages/AutoparkPage';
import CarPage from '../Pages/CarPage';
import TrailerPage from '../Pages/TrailerPage';
import CreateOrderPage from '../Pages/CreateOrderPage';
import OrdersPage from '../Pages/OrdersPage';
import OrderPage from '../Pages/OrderPage';
import MyOrdersPage from '../Pages/MyOrdersPage';
import LoginPage from '../Pages/LoginPage';
import RegisterOtherPage from '../Pages/RegisterOtherPage';
import ProfilePage from '../Pages/ProfilePage';
import UsersPage from '../Pages/UsersPage';
import UserPage from '../Pages/UserPage';
import AutoparksPage from '../Pages/AutoparksPage';
import MainPage from '../Pages/MainPage';
import HeaderLayout from '../Components/HeaderLayout';
import RegisterPage from '../Pages/RegisterPage';
import ShipsPage from '../Pages/ShipsPage';
import ShipPage from '../Pages/ShipPage';

export default function CargodiRoutes() {
    const { user } = useContext(AuthContext)

    return (
        <Routes>
            <Route element={<HeaderLayout />}>
                <Route path="*" element={<MainPage />} />
                {
                    user && user.client &&
                    <>
                        <Route path="/orders/my" element={<MyOrdersPage />} />
                        <Route path="/orders/create" element={<CreateOrderPage />} />
                    </>
                }
                {
                    user && user.operator &&
                    <>
                        <Route path="/registerOther" element={<RegisterOtherPage />} />
                        <Route path="/users" element={<UsersPage />} />
                        <Route path="/autoparks/" element={<AutoparksPage />} />
                        <Route path="/autopark/:parkId" element={<AutoparkPage />} />
                        <Route path="/car/:carId" element={<CarPage />} />
                        <Route path="/trailer/:trailerId" element={<TrailerPage />} />
                        <Route path="/orders/operator" element={<OrdersPage />} />
                        <Route path="/user/:userId" element={<UserPage />} />
                        <Route path="/ships/operator" element={<ShipsPage />} />
                    </>
                }
                {
                    user && (user.operator || user.driver) &&
                    <>
                        <Route path="/ship/:shipId" element={<ShipPage />} />
                    </>
                }
                {
                    user &&
                    <>
                        <Route path="/profile" element={<ProfilePage />} />
                        <Route path="/order/:orderId" element={<OrderPage />} />
                    </>
                }
                {
                    !user &&
                    <>
                        <Route path="/register" element={<RegisterPage />} />
                        <Route path="/login" element={<LoginPage />} />
                    </>
                }
            </Route>
        </Routes>
    )
}
