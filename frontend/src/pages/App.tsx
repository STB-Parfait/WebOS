// src/App.tsx
import { Show, onMount } from 'solid-js';
import AuthPage from './AuthPage';
import Dashboard from './Dashboard';
import { isLoggedIn, login } from '../store'; 

function App() {
    onMount(async () => {
        try {
            const response = await fetch('http://localhost:5020/api/users/me', { credentials: 'include' });
            // Se o backend confirmar que tá logado, atualizamos a store global
            if (response.ok) login(); 
        } catch(e) {}
    });

    return (
        <div class="app-shell">
            <Show when={isLoggedIn()} 
            fallback={
                <AuthPage />
            }
            >
              <Dashboard />
            </Show>
        </div>
    );
}

export default App;