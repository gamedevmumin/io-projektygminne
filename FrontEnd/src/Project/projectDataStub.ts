import { IProjectData } from './IProjectData'

export const projectDataStubFactory: (id: number) => IProjectData = (id) => ({
    id,
    name: 'Oczyszczalnia ścieków',
    pricePln: 2222,
    cancelledAt: null,
    description: 'To będzie oczyszczalnia na miarę naszych potrzeb',
    district: { name: 'Brudna' },
    mainPhoto: 'https://cataas.com/cat',
    estimatedTimeInDays: 300,
})
