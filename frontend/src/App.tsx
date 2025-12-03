import { createSignal, createMemo, Show } from 'solid-js'
import './App.css'
import placeholderLogo from './assets/placeholder.svg'
import backgroundLoop from './assets/sky_loop.mp4'

function App() {

  const [activeTab, setActiveTab] = createSignal('none');
  const [usernameInput, setUsernameInput] = createSignal('');
  const [mainPass, setMainPass] = createSignal('');
  const [confirmPass, setConfirmPass] = createSignal('');
  const [registerEmail, setRegisterEmail] = createSignal('');
  const [emailStatus, setEmailStatus] = createSignal('hidden');

  const mainPassStatus = createMemo(() => {
    const len = mainPass().length;
    if(len===0) return 'hidden';
    if(len>8&&len<32) return 'valid';
    return 'invalid';
  });

  const confirmPassStatus = createMemo(() => {
    if (confirmPass().length === 0) return 'hidden';
    if(confirmPass() === mainPass() && mainPassStatus() === 'valid') {
      return 'valid';
    }
    return 'invalid';
  });

  function toggleTab(tabName: string) {
    if (activeTab() === tabName) {
      setActiveTab('none');
    } else {
      setActiveTab(tabName);
    }
  }

  async function checkEmail() {
    const email = registerEmail();

    if(email.length === 0){
      setEmailStatus('hidden');
      return;
    }

    try{
      const response = await fetch('http://localhost:5020/api/users/checkemail', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email: email }),
      });

      if (!response.ok) throw new Error('Net error');

      const data = await response.json();
      setEmailStatus(data.isAvaliable ? 'valid' : 'invalid');
    } catch(err){
      console.error(err);
      setEmailStatus('invalid');
    }
  }

  async function handleRegister() {
    if (
      emailStatus() === 'valid' &&
      mainPassStatus() === 'valid' &&
      confirmPassStatus() === 'valid'
    ) {
      try{
        const response = await fetch('http://localhost:5020/api/users', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            username: usernameInput(),
            email: registerEmail(),
            password: mainPass(),
          }),
        });

        if (!response.ok){
          throw new Error('Net error');
        } else{
          alert('New user registered!');
          setUsernameInput('');
          setRegisterEmail('');
          setMainPass('');
          setConfirmPass('');
          setEmailStatus('hidden');
          setActiveTab('login');
        }
      } catch(err){
        console.error(err);
        setEmailStatus('invalid');
      }
    }
  }

  return(
    <div>
      <div class="back">
        <video autoplay muted loop poster="/placeholder.svg">
          <source src={backgroundLoop} type="video/mp4" />
        </video>
      </div>
      <div class="container">
        <div class="card">
        <img src={placeholderLogo} alt="placeholder.svg" />
        <h1>WebOS</h1>
        <p>V. 0.0.3</p>
      </div>
      <div class="tabs">
        <button class="tab-btn" onClick={() => toggleTab('login')}>login</button>
        <button class="tab-btn" onClick={() => toggleTab('register')}>register</button>
      </div>
      <Show when={activeTab() === 'login'}>
        <div class="tab login">
        <div class="wrapper">
          <input type="email" placeholder="email" /><br />
        </div>
        <div class="wrapper">
          <input type="password" placeholder="password" />
        </div>
          <button>continue</button>
        </div>
      </Show>
      <Show when={activeTab() === 'register'}>
        <div class="tab register">
        <div class="wrapper">
          <input type="text" placeholder="username"
          value={usernameInput()}
          onInput={(e) => setUsernameInput(e.currentTarget.value)}
          />
        </div>
        <div class="wrapper">
          <input type="email" placeholder="email"
          value={registerEmail()}
          onInput={(e) => setRegisterEmail(e.currentTarget.value)}
          onBlur={checkEmail}
          />
          <div class="statusDot"
          classList={{
            hidden: emailStatus() === 'hidden',
            valid: emailStatus() === 'valid',
            invalid: emailStatus() === 'invalid',
          }}
          title={emailStatus() === 'invalid' ? 'This email is already in use' : ''}
          ></div>
        </div>
        <div class="wrapper">
          <input type="password" placeholder="password"
          value={mainPass()}
          onInput={(e) => setMainPass(e.currentTarget.value)}
          />
          <div class="statusDot"
          classList={{
            hidden: mainPassStatus() === 'hidden',
            valid: mainPassStatus() === 'valid',
            invalid: mainPassStatus() === 'invalid',
          }}
          title={mainPassStatus() === 'invalid' ? 'Password must vary between 8 and 32 chars' : ''}
          ></div>
        </div>
        <div class="wrapper">
          <input type="password" placeholder="confirm password"
          value={confirmPass()}
          onInput={(e) => setConfirmPass(e.currentTarget.value)}
          />
          <div class="statusDot"
          classList={{
            hidden: confirmPassStatus() === 'hidden',
            valid: confirmPassStatus() === 'valid',
            invalid: confirmPassStatus() === 'invalid',
          }}
          title={confirmPassStatus() === 'invalid' ? 'Passwords do not match' : ''}
          ></div>
        </div>
          <button onClick={handleRegister}>continue</button>
        </div> 
      </Show> 
      </div>
    </div>
  )
}

export default App
