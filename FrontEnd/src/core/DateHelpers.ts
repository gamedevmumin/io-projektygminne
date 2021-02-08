import { format } from 'date-fns'

export const getDateString = (date: Date) => format(date, 'dd-MM-yyyy')
