import { Col, Row } from "antd";
import Note from "../../components/Note";
import { useState } from "react";
import NewNote from "../../components/NewNote";

const Blog: React.FC = () => {

    const [isExpand, setExpand] = useState(false)
    const toggleExpand = () => { setExpand(!isExpand) }

    return (
        <Row>
            <Col span={6}></Col>
            <Col span={12}>
                <NewNote />
                <Note author="NikFaraday" content="Note content" releaseDate={new Date()} />
            </Col>
        </Row>
    )
}

export default Blog;