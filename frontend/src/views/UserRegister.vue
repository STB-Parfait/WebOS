<script setup>

    import { ref, onMounted } from 'vue';

    const activeTab = ref('none');

    function toggleTab(tabName){
        if(activeTab.value===tabName){
            activeTab.value = 'none';
        } else{
            activeTab.value = tabName;
        }
    }

</script>

<template>
    <div class="back">
        <video autoplay muted loop poster="/public/placeholder.svg">
            <source src="/public/sky_loop.mp4" type="video/mp4"></source>
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

        <div v-if="activeTab=='login'" class="tab login">
            <input type="email" placeholder="email" /><br />
            <input type="password" placeholder="password" />
            <button>continue</button>
        </div>

        <div v-if="activeTab=='register'" class="tab register">
            <input type="email" placeholder="email" /><br />
            <input type="password" placeholder="password" /><br/>
            <input type="password" placeholder="confirm password"/>
            <button>continue</button>
        </div>
    </div>
</template>

<style>
.back{
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
  font-family: "WDXL Lubrifont SC", sans-serif;
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
  font-family: "WDXL Lubrifont SC", sans-serif;
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
    background: linear-gradient(to bottom, #f5f5f5, #d5d5d5);
    border: 1px solid #999;
}

.tab input {
    margin-bottom: 5px;
    width: 90%;
}

.tab button {
  margin-top: 5px;
}
</style>