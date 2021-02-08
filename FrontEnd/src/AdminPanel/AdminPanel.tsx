import { Button } from 'antd'
import React, { FunctionComponent } from 'react'
import { Link, withRouter } from 'react-router-dom'
import { useAuthContext } from '../Context/Context'
import { routePaths } from '../Routing/routePaths'
import LoginForm from './LoginForm'
import { ContentWrapper } from '../core/components/ContentWrapper'
import './styles/AdminPanel.css'

export const AdminPanel: FunctionComponent = () => {
    const [token, , role] = useAuthContext()
    if (!token) {
        return <LoginForm />
    }
    return (
        <ContentWrapper className="admin-panel">
            <h3>Panel Admina</h3>
            <Link className="admin-panel__link" to={routePaths.ADD_PROJECT}>
                <Button type="primary">Dodaj projekt</Button>
            </Link>
            <Link className="admin-panel__link" to={routePaths.EDITIONS_ADMIN}>
                <Button type="primary">Edycje</Button>
            </Link>
            <Link className="admin-panel__link" to={routePaths.ADD_EDITION}>
                <Button type="primary">Dodaj edycję</Button>
            </Link>
            {role === 'Admin' && (
                <Link className="admin-panel__link" to={routePaths.ADD_USER}>
                    <Button type="primary">Rejestruj użytkownika</Button>
                </Link>
            )}
        </ContentWrapper>
    )
}

export default withRouter(AdminPanel)
