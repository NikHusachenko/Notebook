import { BrowserRouter, Routes, Route } from 'react-router-dom';
import SignIn from './pages/authentication/SignIn';
import SignUp from './pages/authentication/SignUp';
import RestorePassword from './pages/authentication/RestorePassword';
import ForgotPassword from './pages/authentication/ForgotPassword';
import Header from './components/Header';
import Home from './pages/Home/Index';
import Informaction from './pages/account/Information';
import Settings from './pages/account/Settings';
import LogOut from './pages/authentication/LogOut';
import Blog from './pages/blog/Blog';
import NotFound from './pages/Home/NotFound';
import AuthenticationProtect from './pages/Home/AuthenticationProtect';
import ErrorPage from './pages/Home/ErrorPage';

function App() {
  return (
    <BrowserRouter>
      <Header/>
      <Routes>
        <Route path='/authentication/sign-in' element={ <SignIn/> } />
        <Route path='/authentication/sign-up' element={ <SignUp/> } />
        <Route path='/authentication/forgot-password' element={ <ForgotPassword/> } />
        <Route path='/authentication/restore-password' element={ <RestorePassword/> } />
        <Route path='/error' element={ <ErrorPage errorMessage='' /> } />
        
        <Route element={<AuthenticationProtect/>}>
          <Route path='/' element={ <Home/> } />
          <Route path='/account/information' element={ <Informaction/> } />
          <Route path='/account/settings' element={ <Settings/> } />
          <Route path='/blog/:id' element={ <Blog /> } />
          <Route path='/account/log-out' element={ <LogOut /> } />
          <Route path='*' element={ <NotFound/> } />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;