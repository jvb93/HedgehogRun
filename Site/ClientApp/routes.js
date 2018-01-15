import Dashboard from 'components/dashboard'
//import FetchData from 'components/fetch-data'
import HomePage from 'components/home-page'
import Records from 'components/records'

export const routes = [
    { path: '/', component: Dashboard, display: 'Home', style: 'glyphicon glyphicon-home' },
    { path: '/records', component: Records, display: 'Records', style: 'glyphicon glyphicon-star' },
   // { path: '/counter', component: CounterExample, display: 'Counter', style: 'glyphicon glyphicon-education' },
 //   { path: '/fetch-data', component: FetchData, display: 'Fetch data', style: 'glyphicon glyphicon-th-list' }
]
