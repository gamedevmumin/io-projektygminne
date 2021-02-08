import { Typography } from 'antd'
import React, { FunctionComponent } from 'react'
import { Formik } from 'formik'
import { Input as AntdInput, Col, Row } from 'antd'
import { Form, Input, SubmitButton, Select } from 'formik-antd'
import { UserOutlined } from '@ant-design/icons'
import { RouteComponentProps, withRouter } from 'react-router-dom'
import { routePaths } from '../Routing/routePaths'
import { httpClient } from '../core/ApiClient'
import { Context } from '../Context/Context'
import { addUser } from '../Endpoints/Endpoints'
import { useAuthContext } from '../Context/Context'
import LoginForm from './LoginForm'

const { Option } = Select

export const AddUser: FunctionComponent = (props) => {
    const [token] = useAuthContext()

    return (
        <>
            <Typography.Title>Dodaj użytkownika</Typography.Title>
            <Formik
                onSubmit={async ({ userName, password, role }, actions) => {
                    console.log(password)
                    await addUser(userName, password, role, token)
                }}
                initialValues={{ userName: '', password: '', role: 'Admin' }}
            >
                <Form>
                    <AntdInput.Group
                        style={{ display: 'flex', justifyContent: 'center' }}
                    >
                        <Col>
                            <Row>
                                <Input
                                    name="userName"
                                    bordered
                                    prefix={<UserOutlined />}
                                    style={{ width: 300, margin: 10 }}
                                    placeholder="Nazwa użytkownika"
                                />
                            </Row>
                            <Row>
                                <Input.Password
                                    name="password"
                                    placeholder="Hasło"
                                    style={{ width: 300, margin: 10 }}
                                />
                            </Row>
                            <Row>
                                <Select
                                    name="role"
                                    style={{ width: 300, margin: 10 }}
                                >
                                    <Option value={2}>Urzędnik</Option>
                                    <Option value={1}>Admin</Option>
                                </Select>
                            </Row>
                            <Row justify="center">
                                <SubmitButton style={{ margin: 10 }}>
                                    {' '}
                                    Dodaj użytkownika{' '}
                                </SubmitButton>
                            </Row>
                        </Col>
                    </AntdInput.Group>
                </Form>
            </Formik>
        </>
    )
}
