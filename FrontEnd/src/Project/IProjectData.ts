export interface IProjectData {
    id: number
    name: string
    district: { name: string }
    pricePln: number
    mainPhoto: string
    description: string
    cancelledAt: Date | null
    estimatedTimeInDays: number
}
