import { RouteComponentProps, withRouter, Link, useHistory } from 'react-router-dom'
import React, { useEffect, useState, FunctionComponent, useContext } from 'react'
import { AuthContext, Context, useAuthContext } from '../Context/Context'


export const AuthedWrapper: FunctionComponent = ({
     children
}) => {
    const [token] = useAuthContext();
    const history = useHistory();

    if(!token){
        history.push("/");
        return  null;
    }

    return <>{children}</>;
}

export default  AuthedWrapper;