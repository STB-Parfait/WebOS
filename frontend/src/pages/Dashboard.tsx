import type { Component } from 'solid-js';
import { logout } from '../store';

const Dashboard: Component = () => {

  async function handleLogout() {
      await fetch('http://localhost:5020/api/users/logout', { method: 'POST', credentials: 'include' });
      logout();
  }

  return (
    <div>
        <h1>Dashboard</h1>
        <button onClick={handleLogout}>Sair</button>
    </div>
  );
};

export default Dashboard;