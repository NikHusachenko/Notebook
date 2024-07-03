import { Link } from "react-router-dom"

const Layout : React.FC = () => {
    return(
        <div>
            <nav>
                <ul>
                    <li><Link to={'/'}>Home</Link></li>
                    <li><Link to={'sign-in'}>Sign in</Link></li>
                    <li><Link to={'sign-up'}>Sign up</Link></li>
                </ul>
            </nav>
        </div>       
    )
}

export default Layout