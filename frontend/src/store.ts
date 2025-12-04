// src/store.ts
import { createSignal } from 'solid-js';

// Ao criar FORA de um componente, isso vira um "Estado Global"
// Qualquer arquivo que importar isso vai ver o MESMO valor.
export const [isLoggedIn, setIsLoggedIn] = createSignal(false);

// Vamos exportar também uma função helper pra facilitar
export const login = () => setIsLoggedIn(true);
export const logout = () => setIsLoggedIn(false);