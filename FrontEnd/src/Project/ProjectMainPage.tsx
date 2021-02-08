import { Typography } from 'antd'
import React, { FunctionComponent, useEffect, useState } from 'react'
import { RouteComponentProps, withRouter } from 'react-router-dom'
import { projectDataStubFactory } from './projectDataStub'
import ProjectDetails from './ProjectDetails/ProjectDetails'
import { MainImage, Wrapper } from './ProjectMainPage.components'
import ProjectVoteForm from './ProjectVoteForm/ProjectVoteForm'
import { getProject } from '../Endpoints/Endpoints'
import { idText } from 'typescript'

export const ProjectMainPage: FunctionComponent<RouteComponentProps<{
    id?: string
}>> = ({
    history,
    match: {
        params: { id },
    },
}) => {
    //const project = useMemo(async () => await getProject(id), [id])

    const [project, setProject] = useState<any>()

    useEffect(() => {
        const fetchProject = async () => {
            const tempProject = await getProject(id)
            setProject(tempProject)
        }
        fetchProject()
    }, [id])

    if (!id) {
        history.goBack()
        return null
    }

    return project ? (
        <Wrapper>
            <Typography.Title>
                {project.district} - {project.name}
            </Typography.Title>
            <MainImage src={project.mainPhoto} />
            <ProjectDetails project={project} />
            <Typography.Title level={3}>
                Podoba ci się ten pomysł? Zagłosuj!
            </Typography.Title>
            <ProjectVoteForm id={project.id} />
        </Wrapper>
    ) : null
}

export default withRouter(ProjectMainPage)
