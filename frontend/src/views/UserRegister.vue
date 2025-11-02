<script setup>
import { ref, computed } from 'vue'

const activeTab = ref('none')

function toggleTab(tabName) {
  if (activeTab.value === tabName) {
    activeTab.value = 'none'
  } else {
    activeTab.value = tabName
  }
}

const mainPass = ref('')

const mainPassStatus = computed(() => {
  if (mainPass.value.length === 0) {
    return 'hidden'
  } else if (mainPass.value.length >= 8 && mainPass.value.length <= 32) {
    return 'valid'
  } else {
    return 'invalid'
  }
})

const registerEmail = ref('')
const emailStatus = ref('hidden')

async function checkEmail() {
  const email = registerEmail.value

  if (email.length === 0) {
    emailStatus.value = 'hidden'
    return
  }

  emailStatus.value = 'loading'

  try {
    const response = await fetch('http://localhost:3000/api/users/checkemail', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email: email }),
    })

    if (!response.ok) {
      throw new Error('Erro na rede')
    }

    const data = await response.json()

    emailStatus.value = data.isAvaliable ? 'valid' : 'invalid'
  } catch (err) {
    console.error(err)
    emailStatus.value = 'invalid'
  }
}

const confirmPass = ref('')

const confirmPassStatus = computed(() => {
  if (confirmPass.value === mainPass.value && mainPassStatus.value === 'valid') {
    return 'valid'
  } else if (confirmPass.value.length === 0) {
    return 'hidden'
  } else {
    return 'invalid'
  }
})
</script>

<template>
  <div class="back">
    <video autoplay muted loop poster="/placeholder.svg">
      <source src="/sky_loop.mp4" type="video/mp4" />
    </video>
  </div>

  <div class="container">
    <div class="card">
      <img src="/placeholder.svg" alt="placeholder.svg" />
      <h1>WebOS</h1>
      <p>V. 0.0.1</p>
    </div>

    <div class="tabs">
      <button class="tab-btn" @click="toggleTab('login')">login</button>
      <button class="tab-btn" @click="toggleTab('register')">register</button>
    </div>

    <div v-if="activeTab == 'login'" class="tab login">
      <input type="email" placeholder="email" /><br />
      <input type="password" placeholder="password" />
      <button>continue</button>
    </div>

    <div v-if="activeTab == 'register'" class="tab register">
      <div class="wrapper">
        <input type="text" placeholder="username" />
      </div>
      <div class="wrapper">
        <input type="email" placeholder="email" v-model="registerEmail" @blur="checkEmail" />
        <div
          class="statusDot"
          :class="{
            hidden: emailStatus === 'hidden',
            valid: emailStatus === 'valid',
            invalid: emailStatus === 'invalid',
          }"
        ></div>
      </div>

      <div class="wrapper">
        <input type="password" placeholder="password" v-model="mainPass" />
        <div
          class="statusDot"
          :class="{
            hidden: mainPassStatus === 'hidden',
            invalid: mainPassStatus === 'invalid',
            valid: mainPassStatus === 'valid',
          }"
          :title="
            mainPassStatus === 'invalid' ? 'The password must have between 8 and 32 digits' : ''
          "
        ></div>
        <br />
      </div>

      <div class="wrapper">
        <input type="password" placeholder="confirm password" v-model="confirmPass" />
        <div
          class="statusDot"
          :class="{
            hidden: confirmPassStatus === 'hidden',
            invalid: confirmPassStatus === 'invalid',
            valid: confirmPassStatus === 'valid',
          }"
          :title="confirmPassStatus === 'invalid' ? 'Make sure bolth passwords are equal' : ''"
        ></div>
      </div>

      <button>continue</button>
    </div>
  </div>
</template>

<style>
.wrapper {
  display: flex;
  align-items: center;
  margin-bottom: 5px;
}
.statusDot {
  display: inline-block;
  height: 10px;
  width: 10px;
  border-radius: 50%;
  background-color: gold;
  opacity: 1;
  transition:
    background-color 0.3s ease,
    opacity 0.3s ease;
}
.statusDot.hidden {
  opacity: 0;
  pointer-events: none;
}
.statusDot.valid {
  opacity: 1;
  background-color: green;
}
.statusDot.invalid {
  background-color: red;
}
.back {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  overflow: hidden;
  z-index: -1;
  pointer-events: none;
}
.container {
  width: fit-content;
  position: relative;
  font-family: 'WDXL Lubrifont SC', sans-serif;
}

.card {
  width: 261.883px;
  padding: 15px;
  background: linear-gradient(to bottom, #f5f5f5, #d5d5d5);
  border: 1px solid #999;
}

.card img {
  width: 100px;
  height: 100px;
}

h1,
p {
  margin: 5px 0;
}

.tabs {
  display: flex;
  justify-content: space-between;
  margin-top: 8px;
}

.tab-btn {
  flex: 1;
  background: linear-gradient(to bottom, #f5f5f5, #d5d5d5);
  border: 1px solid #999;
  font-family: 'WDXL Lubrifont SC', sans-serif;
  font-size: 14px;
  text-transform: lowercase;
  cursor: pointer;
  transition: 0.2s;
  padding: 5px 0;
}

.tab-btn:hover {
  background: linear-gradient(to bottom, #ffffff, #cfcfcf);
}

.tab-btn:active {
  background: linear-gradient(to bottom, #d0d0d0, #aaaaaa);
}

.tab {
  margin-top: 10px;
  padding: 15px;
  padding-right: 0px;
  background: linear-gradient(to bottom, #f5f5f5, #d5d5d5);
  border: 1px solid #999;
}

.tab input {
  margin-right: 5px;
  width: 90%;
}

.tab button {
  margin-top: 5px;
}
</style>
