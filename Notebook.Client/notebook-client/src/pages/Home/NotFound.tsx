import { Col, Row } from "antd"
import { Link } from "react-router-dom"

const NotFound : React.FC = () => {
    return(
        <Row style={{height: 'calc(100vh - 64px)'}} align={'middle'} justify={'center'}>
            <Col>
                <Row>
                    <h1>Page not found</h1>
                </Row>
                <Row>
                    <Link to={'/'}>Go home</Link>
                </Row>
            </Col>
        </Row>
    )
}

export default NotFound