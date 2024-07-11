import { BrowserRouter, Routes, Route } from 'react-router-dom';
import SignIn from './pages/authentication/SignIn';
import SignUp from './pages/authentication/SignUp';
import RestorePassword from './pages/authentication/RestorePassword';
import ForgotPassword from './pages/authentication/ForgotPassword';
import Header from './components/Header';
import Home from './pages/Home/Index';
import Informaction from './pages/account/Information';
import Settings from './pages/account/settings';
import LogOut from './pages/authentication/LogOut';

function App() {
  return (
    <BrowserRouter>
      <Header/>
      <Routes>
        <Route path='/' element={ <Home/> } />
        <Route path='authentication/sign-in' element={ <SignIn/> } />
        <Route path='authentication/sign-up' element={ <SignUp/> } />
        <Route path='authentication/forgot-password' element={ <ForgotPassword/> } />
        <Route path='authentication/restore-password' element={ <RestorePassword/> } />
        <Route path='account/information' element={ <Informaction/> } />
        <Route path='account/settings' element={ <Settings/> } />
        <Route path='account/log-out' element={ <LogOut /> } />
      </Routes>
    </BrowserRouter>
  );
}

export default App;