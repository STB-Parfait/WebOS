/* @refresh reload */
import { render } from 'solid-js/web'
import './index.css'
import AuthPage from './pages/AuthPage.tsx'

const root = document.getElementById('root')

render(() => <AuthPage />, root!)
