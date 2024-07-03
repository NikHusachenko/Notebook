import './App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import SignIn from './pages/authentication/SignIn';
import SignUp from './pages/authentication/SignUp';
import RestorePassword from './pages/authentication/RestorePassword';
import ForgotPassword from './pages/authentication/ForgotPassword';
import Layout from './Layout';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<Layout/>} />
        <Route path='sign-in' element={<SignIn/>} />
        <Route path='sign-up' element={<SignUp/>} />
        <Route path='forgot-password' element={<ForgotPassword/>} />
        <Route path='restore-password' element={<RestorePassword/>} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;