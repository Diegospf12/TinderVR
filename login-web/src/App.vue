<script setup>
import { RouterLink, RouterView } from 'vue-router'
import HelloWorld from './components/HelloWorld.vue'
import axios from 'axios'
import { ref } from 'vue'

const url = 'https://maswuwwgqsiadeecdytp.supabase.co/rest/v1/users';

// Headers
const headers = {
  'apikey': 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1hc3d1d3dncXNpYWRlZWNkeXRwIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTg1Njk4MzYsImV4cCI6MjAzNDE0NTgzNn0.vIGnyUOJarJFZ-s4gPCdlbEdqWiGjx_hVWx53vGnd5s',
  'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im1hc3d1d3dncXNpYWRlZWNkeXRwIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTg1Njk4MzYsImV4cCI6MjAzNDE0NTgzNn0.vIGnyUOJarJFZ-s4gPCdlbEdqWiGjx_hVWx53vGnd5s',
  'Content-Type': 'application/json',
  'Prefer': 'return=minimal'
};

var logged = ref(true)
const user = ref(null)
const userValue = ref('')
const pass = ref(null)
const passValue = ref('')

//const postData = ref({ username: '', password: '', description:'' };

 async function send(event){
  user.value.focus()
  pass.value.focus()
  const data = {username:userValue.value,password:passValue.value, description:""}
  try {
    const response = await axios.post(url, data,  { headers });
    return response.data;
  } catch (error) {
   throw error;
 }
}
</script>

<template>
  <header>
    <img alt="Vue logo" class="logo" src="@/assets/logo.svg" width="125" height="125" />

    <div class="wrapper">
      <HelloWorld msg="You did it!" />

      <nav>
        <RouterLink to="/">Home</RouterLink>
        <RouterLink to="/about">About</RouterLink>
      </nav>
    </div>
  </header>
    <div class="Register" v-if="logged">
      <h2>Register to discover new friends</h2>
      <input  type="text" placeholder="Usuario"  v-model="userValue" ref="user" id="User"/>
      <input v-model="passValue" type="password" placeholder="Contraseña" ref="pass" id="Pass"/>
      <button @click="send, logged = !logged"> Enviar </button>
    </div>
    <div class="Logged" v-else>
    <h2>Welcome, User!</h2>
    <!-- Replace with your actual content for logged-in users -->
    <p>SHOW Images</p>
    <div class="buttons">
      <button @click="goToProfile">Profile</button>
      <button @click="goToSettings">Settings</button>
      <button @click="logged = !logged">Log Out</button>
    </div>
  </div>
  <RouterView />
</template>

<style scoped>

.Register {
  max-width: 400px; /* Adjust the maximum width as needed */
  margin: 0 auto; /* Center the form horizontally */
  padding: 20px;
  background-color: #f0f0f0; /* Light gray background */
  border: 1px solid #ccc; /* Border around the form */
  border-radius: 5px; /* Rounded corners */
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1); /* Drop shadow for a subtle effect */
}

.Register h2 {
  font-size: 1.5em; /* Larger font size for the heading */
  text-align: center; /* Center the heading text */
  margin-bottom: 20px; /* Space below the heading */
}

.Register input[type="text"],
.Register input[type="password"] {
  width: 100%; /* Full width input */
  padding: 10px; /* Padding inside the input */
  margin-bottom: 15px; /* Space below each input */
  border: 1px solid #ccc; /* Border around the input */
  border-radius: 3px; /* Rounded corners */
  box-sizing: border-box; /* Ensure padding and border are included in width */
}

.Register button {
  width: 100%; /* Full width button */
  padding: 10px; /* Padding inside the button */
  background-color: #4CAF50; /* Green background */
  color: white; /* White text color */
  border: none; /* No border */
  border-radius: 3px; /* Rounded corners */
  cursor: pointer; /* Pointer cursor on hover */
}

.Register button:hover {
  background-color: #45a049; /* Darker green on hover */
}

header {
  line-height: 1.5;
  max-height: 100vh;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

nav {
  width: 100%;
  font-size: 12px;
  text-align: center;
  margin-top: 2rem;
}

nav a.router-link-exact-active {
  color: var(--color-text);
}

nav a.router-link-exact-active:hover {
  background-color: transparent;
}

nav a {
  display: inline-block;
  padding: 0 1rem;
  border-left: 1px solid var(--color-border);
}

nav a:first-of-type {
  border: 0;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }

  nav {
    text-align: left;
    margin-left: -1rem;
    font-size: 1rem;

    padding: 1rem 0;
    margin-top: 1rem;
  }
}


.Logged {
  max-width: 600px;
  margin: 0 auto;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 8px;
  background-color: #f9f9f9;
  text-align: center;
}

.Logged h2 {
  font-size: 24px;
  margin-bottom: 10px;
}

.buttons {
  margin-top: 20px;
}

.buttons button {
  margin: 0 10px;
  padding: 10px 20px;
  border: none;
  border-radius: 5px;
  background-color: #007bff;
  color: white;
  font-size: 16px;
  cursor: pointer;
}

.buttons button:hover {
  background-color: #0056b3;
}
</style>
