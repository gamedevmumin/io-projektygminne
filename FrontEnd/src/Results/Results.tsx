import React, { FunctionComponent, ReactNode, useEffect, useState } from 'react'
import { withRouter, Link } from 'react-router-dom'
import { Table, Button } from 'antd'
import { columns } from './ResultsColumns'
import { getCurrentEdition, getAcceptedProjects } from '../Endpoints/Endpoints'
import { getDateString } from '../core/DateHelpers'
import 'antd/dist/antd.css'
import { getEditionURL } from '../Routing/routePaths'
import { getYear } from 'date-fns'

interface CurrentEditionColumnData {
    key: number
    name: string
    endDate: string
    browseButton: ReactNode
}

export const Results: FunctionComponent = () => {
    const [acceptedProjects, setAcceptedProjects] = useState([])

    useEffect(() => {
        ;(async () => {
            try {
                const projects = await getAcceptedProjects()
                const mappedProjects = projects.map((project: any) => ({
                    key: project.id,
                    edition: `${project.district} ${project.theEditionThisProjectWonId}`,
                    wonProject: project.name,
                }))
                setAcceptedProjects(mappedProjects)
            } catch (err) {
                console.log(err)
            }
        })()
    }, [])

    return (
        <>
            <h1>Aktualna edycja</h1>
            <Table
                dataSource={acceptedProjects}
                columns={columns}
                size="middle"
                pagination={false}
                locale={{
                    emptyText: 'Aktualnie żadna edycja nie jest w toku',
                }}
            />
        </>
    )
}

export default withRouter(Results)
