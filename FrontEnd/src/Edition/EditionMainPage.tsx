import { RouteComponentProps, withRouter, Link } from 'react-router-dom'
import React, { useEffect, useState, FunctionComponent } from 'react'
import { getCurrentEditionProjects } from '../Endpoints/Endpoints'
import { Table, Button, Result } from 'antd'
import { columns } from './ProjectsListColumns'
import { routePaths } from '../Routing/routePaths'
import { Newsletter } from '../Newsletter'
export interface ProjectsColumnsData {
    key: number
    name: string
    description: string
}

export const EditionMainPageComponent: FunctionComponent<RouteComponentProps<{
    id?: string
}>> = ({
    match: {
        params: { id },
    },
}) => {
    const [projectsColumnsData, setProjectsColumnsData] = useState<
        Array<ProjectsColumnsData>
    >()
    const [isEditionInProgress, setIsEditionInProgress] = useState<
        boolean | undefined
    >()

    useEffect(() => {
        if (id) {
            const fetchProjects = async () => {
                try {
                    const projects = await getCurrentEditionProjects()
                    const projectsColumnsDataTemp = projects?.participants.map(
                        ({ project }) => ({
                            key: project.id,
                            id: project.id,
                            name: project.name,
                            description: project.description,
                        })
                    )
                    setProjectsColumnsData(projectsColumnsDataTemp)
                    setIsEditionInProgress(true)
                } catch {
                    setIsEditionInProgress(false)
                }
            }
            fetchProjects()
        }
    }, [id])

    return (
        <div>
            <h1>Aktualna Edycja</h1>
            {isEditionInProgress && (
                <Table
                    dataSource={projectsColumnsData}
                    expandable={{
                        expandedRowRender: (record) => (
                            <p>{record.description}</p>
                        ),
                    }}
                    columns={columns}
                    size="middle"
                />
            )}
            {isEditionInProgress === false && (
                <Result
                    status="warning"
                    title="Brak aktywnej edycji!"
                    extra={<Newsletter />}
                />
            )}
        </div>
    )
}

export default withRouter(EditionMainPageComponent)
