import { Typography } from 'antd'
import React, { FunctionComponent, useEffect, useState } from 'react'
import { Formik } from 'formik'
import { Input as AntdInput, Col, Row, Button } from 'antd'
import { Form, Input, SubmitButton, Select, DatePicker } from 'formik-antd'
import { UserOutlined } from '@ant-design/icons'
import { RouteComponentProps, withRouter } from 'react-router-dom'
import { routePaths } from '../Routing/routePaths'
import { httpClient } from '../core/ApiClient'
import { Context } from '../Context/Context'
import { useAuthContext } from '../Context/Context'
import LoginForm from './LoginForm'
import {
    getEditionDrafts,
    startEdition,
    removeEditionDraft,
} from '../Endpoints/Endpoints'

const { Option } = Select

export const Editions: FunctionComponent = (props) => {
    const [token] = useAuthContext()

    const [editions, setEditions] = useState([])
    const [edition, setEdition] = useState(0)

    useEffect(() => {
        const fetchEditions = async () => {
            const tempEditions = await getEditionDrafts()
            setEditions(tempEditions)
        }

        fetchEditions()
    }, [JSON.stringify(editions)])

    return (
        <>
            <Typography.Title>Zarządzaj edycjami</Typography.Title>
            {editions?.length === 0 ? (
                <h4>Nie masz jeszcze żadnych edycji</h4>
            ) : (
                <Formik
                    onSubmit={async ({}, actions) => {}}
                    initialValues={{
                        editionDraft: 1,
                    }}
                >
                    {(props) => (
                        <Form>
                            <AntdInput.Group
                                style={{
                                    display: 'flex',
                                    justifyContent: 'center',
                                }}
                            >
                                <Col>
                                    <Row>
                                        <Select
                                            name="editionDraft"
                                            style={{ width: 300, margin: 10 }}
                                        >
                                            {editions?.map(
                                                ({ id, district }) => (
                                                    <Option key={id} value={id}>
                                                        {district} {id}
                                                    </Option>
                                                )
                                            )}
                                        </Select>
                                    </Row>
                                    <Row justify="center">
                                        <Button
                                            onClick={() =>
                                                removeEditionDraft(
                                                    props.values.editionDraft,
                                                    token
                                                )
                                            }
                                            type="primary"
                                            danger
                                            style={{ margin: 10 }}
                                        >
                                            Usuń
                                        </Button>
                                        <Button
                                            onClick={() =>
                                                startEdition(
                                                    props.values.editionDraft,
                                                    token
                                                )
                                            }
                                            type="primary"
                                            style={{ margin: 10 }}
                                        >
                                            Rozpocznij
                                        </Button>
                                    </Row>
                                </Col>
                            </AntdInput.Group>
                        </Form>
                    )}
                </Formik>
            )}
        </>
    )
}
