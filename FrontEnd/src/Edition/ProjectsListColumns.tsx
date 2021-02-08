import { Button } from 'antd'
import { ColumnsType, TableProps } from 'antd/lib/table'
import React from 'react'
import { Link } from 'react-router-dom'
import { getProjectURL } from '../Routing/routePaths'
import { ProjectsColumnsData } from './EditionMainPage'

export const columns: ColumnsType<ProjectsColumnsData> = [
    {
        title: 'Nazwa',
        dataIndex: 'name',
        align: 'center' as 'center',
    },
    {
        align: 'center' as 'center',
        title: '',
        render: (project) => {
            return (
                <Link to={getProjectURL(project.id)}>
                    <Button type="primary"> Przeglądaj </Button>
                </Link>
            )
        },
    },
]
