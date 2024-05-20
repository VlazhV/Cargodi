import './App.css';

import { BrowserRouter, Route, Routes } from "react-router-dom"
import MainPage from './Pages/MainPage';
import Header from './Components/Header';
import HeaderLayout from './Components/HeaderLayout';
import RegisterPage from './Pages/RegisterPage';
import { AuthContextProvider } from './Contexts/AuthContext';
import LoginPage from './Pages/LoginPage';
import RegisterOtherPage from './Pages/RegisterOtherPage';
import ProfilePage from './Pages/ProfilePage';
import UsersPage from './Pages/UsersPage';
import UserPage from './Pages/UserPage';
import AutoparksPage from './Pages/AutoparksPage';
import { YMapComponentsProvider } from 'ymap3-components'

function App() {
  return (
    <div className="App">
      <YMapComponentsProvider apiKey='b18e0a22-f14a-4d61-8b5f-6da9260a1876' lang='ru-RU'>
        <AuthContextProvider>
          <BrowserRouter>
            <Routes>
              <Route element={<HeaderLayout />}>
                <Route path="*" element={<MainPage />} />
                <Route path="/register" element={<RegisterPage />} />
                <Route path="/registerOther" element={<RegisterOtherPage />} />
                <Route path="/login" element={<LoginPage />} />
                <Route path="/profile" element={<ProfilePage />} />
                <Route path="/users" element={<UsersPage />} />
                <Route path="/user/:userId" element={<UserPage />} />
                <Route path="/autoparks/" element={<AutoparksPage />} />
              </Route>
            </Routes>
          </BrowserRouter>
        </AuthContextProvider>
      </YMapComponentsProvider>
    </div>
  );
}

export default App;
