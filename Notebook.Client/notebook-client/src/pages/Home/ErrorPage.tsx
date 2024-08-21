import { Col, Row } from "antd"
import { useLocation } from "react-router-dom"

type ErrorPageProps = {
    errorMessage? : string
}

const ErrorPage : React.FC<ErrorPageProps> = () => {
    const location = useLocation();
    const errorMessage = location.state?.errorMessage || 'An unexpected error occurred.'

    return(
        <Row style={{height: 'calc(100vh - 64px)'}} align={'middle'} justify={'center'}>
            <Col>
                <h1>{errorMessage}</h1>
            </Col>
        </Row>
    )
}

export default ErrorPage;