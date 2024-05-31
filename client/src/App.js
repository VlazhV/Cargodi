import './App.css';

import { BrowserRouter } from "react-router-dom"
import { AuthContextProvider } from './Contexts/AuthContext';
import CargodiRoutes from './Components/CargodiRoutes';
import { YMapsContextProvider } from './Contexts/YmapsContext';

function App() {

  return (
    <div className="App">
      <YMapsContextProvider>
        <AuthContextProvider>
          <BrowserRouter>
            <CargodiRoutes />
          </BrowserRouter>
        </AuthContextProvider>
      </YMapsContextProvider>
    </div >
  );
}

export default App;
