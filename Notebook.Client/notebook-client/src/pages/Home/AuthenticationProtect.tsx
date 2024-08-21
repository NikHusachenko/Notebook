import { Col, Row } from "antd";
import React, { useEffect, useState } from "react";
import { Outlet, useNavigate } from "react-router-dom";

async function checkAuthentication(): Promise<number> {
    try {
        let response = await fetch('https://localhost:7262/api/authentication/check-authentication');
        return response.status;
    } catch(ex) {
        console.log(ex)
        return 400;
    }
}

const AuthenticationProtect : React.FC = () => {
    const navigate = useNavigate();
    const [isAuth, setAuth] = useState<boolean | null>(null)

    useEffect(() => {
        const verifyAuthentication = async () => {
            const authStatusCode = await checkAuthentication() 
            
            if (authStatusCode >= 500 && authStatusCode < 600) {
                navigate('error', {
                    state: {
                        errorMessage: 'Serve not available'
                    }
                })
            } else {
                navigate('/authentication/sign-in')
            }
        }

        verifyAuthentication();
    }, [navigate])

    if (isAuth === null) {
        return(
            <Row style={{height: 'calc(100vh - 64px)'}} align={'middle'} justify={'center'}>
                <Col>
                    <div>
                        Loading...
                    </div>
                </Col>
            </Row>
        )
    }

    return(
        isAuth ? <Outlet/> : <></>
    )
};

export default AuthenticationProtect;
