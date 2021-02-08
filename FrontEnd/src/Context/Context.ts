import React, { useContext } from 'react'

export type AuthContext = [
    string | undefined,
    (token: string) => void,
    string | undefined,
    (role: string) => void
]

export const Context = React.createContext<AuthContext>([
    undefined,
    () => {},
    undefined,
    () => {},
])
export const useAuthContext = () => useContext<AuthContext>(Context)
