import './App.css';

import { BrowserRouter } from "react-router-dom"
import { AuthContext, AuthContextProvider } from './Contexts/AuthContext';
import { YMapComponentsProvider } from 'ymap3-components'
import { useContext } from 'react';
import CargodiRoutes from './Components/CargodiRoutes';

function App() {
  const { user } = useContext(AuthContext)

  console.log(user)

  return (
    <div className="App">
      <YMapComponentsProvider apiKey='b18e0a22-f14a-4d61-8b5f-6da9260a1876' lang='ru-RU'>
        <AuthContextProvider>
          <BrowserRouter>
            <CargodiRoutes />
          </BrowserRouter>
        </AuthContextProvider>
      </YMapComponentsProvider>
    </div>
  );
}

export default App;
