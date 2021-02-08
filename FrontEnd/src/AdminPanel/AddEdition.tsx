import { Typography } from 'antd'
import React, { FunctionComponent, useEffect, useState } from 'react'
import { Formik } from 'formik'
import { Input as AntdInput, Col, Row } from 'antd'
import { Form, Input, SubmitButton, Select, DatePicker } from 'formik-antd'
import { UserOutlined } from '@ant-design/icons'
import { RouteComponentProps, withRouter } from 'react-router-dom'
import { routePaths } from '../Routing/routePaths'
import { httpClient } from '../core/ApiClient'
import { Context } from '../Context/Context'
import { addUser } from '../Endpoints/Endpoints'
import { useAuthContext } from '../Context/Context'
import LoginForm from './LoginForm'
import {
    getDistricts,
    getProjects,
    postEdition,
    getPendingProjects,
} from '../Endpoints/Endpoints'

const { Option } = Select
const { TextArea } = Input
export const AddEdition: FunctionComponent = (props) => {
    const [token] = useAuthContext()

    const [districts, setDistricts] = useState([])
    const [projects, setProjects] = useState([])

    useEffect(() => {
        const fetchDistricts = async () => {
            const tempDistricts = await getDistricts()
            setDistricts(tempDistricts)
        }
        const fetchProjects = async () => {
            const tempProjects = await getPendingProjects()
            setProjects(tempProjects)
        }
        fetchDistricts()
        fetchProjects()
    }, [JSON.stringify(districts)])

    return (
        <>
            <Typography.Title>Dodaj edycję</Typography.Title>
            <Formik
                onSubmit={async (
                    { description, endDate, districtId, projects },
                    actions
                ) => {
                    console.log(description, endDate, districtId, projects)
                    postEdition(
                        description,
                        endDate,
                        districtId,
                        projects,
                        token
                    )
                }}
                initialValues={{
                    description: '',
                    endDate: new Date(),
                    districtId: 0,
                    projects: [],
                }}
            >
                <Form>
                    <AntdInput.Group
                        style={{ display: 'flex', justifyContent: 'center' }}
                    >
                        <Col>
                            <Row>
                                <TextArea
                                    name="description"
                                    bordered
                                    style={{ width: 300, margin: 10 }}
                                    placeholder="Opis"
                                    autoSize={{ minRows: 2, maxRows: 6 }}
                                />
                            </Row>
                            <Row>
                                <DatePicker
                                    showTime
                                    showToday={false}
                                    name="endDate"
                                    picker="date"
                                    placeholder="Data zakończenia"
                                    style={{ width: 300, margin: 10 }}
                                />
                            </Row>
                            <Row>
                                <Select
                                    name="districtId"
                                    style={{ width: 300, margin: 10 }}
                                >
                                    {districts?.map(({ id, name }) => (
                                        <Option key={id} value={id}>
                                            {name}
                                        </Option>
                                    ))}
                                </Select>
                            </Row>
                            <Row>
                                <Select
                                    mode="multiple"
                                    name="projects"
                                    style={{ width: 300, margin: 10 }}
                                >
                                    {projects.map(({ id, name }) => (
                                        <Option key={id} value={id}>
                                            {name}
                                        </Option>
                                    ))}
                                </Select>
                            </Row>
                            <Row justify="center">
                                <SubmitButton style={{ margin: 10 }}>
                                    Dodaj edycję
                                </SubmitButton>
                            </Row>
                        </Col>
                    </AntdInput.Group>
                </Form>
            </Formik>
        </>
    )
}
