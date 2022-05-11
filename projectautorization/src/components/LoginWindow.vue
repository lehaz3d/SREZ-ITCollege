<template>
  <body class="authorization" background-color="#304FFD">
    <div class="content">
      <div class="image">
        <img src=".\img\preview.png" />
      </div>
      <div class="container-authorization">
        <div></div>
        <form @submit.prevent class="login-form content-align">
          <h4>Авторизация</h4>
          <label>Email</label>
          <input type="email" id="tbEmail" v-model="email" class="login-form__input" required/>
          <div>
            <label>Пароль</label>
            <input type="password" id="tbPassword" v-model="password" class="login-form__input" required/>
          </div>
          <span><a @click="$router.push({ name: 'restore' })">Забыли пароль?</a></span>
          <button type="submit" @click="isLogged">Войти</button>
        </form>
        <footer class="footer">
          <label>Еще нет аккаунта?<a @click="$router.push({ name: 'sign' })">Зарегистрироваться</a></label>
        </footer>
      </div>
    </div>
  </body>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      ListUsers: [],
      email: "",
      password: "",
    };
  },

  methods: {
    async isLogged() {
      try {
        const findUser = (
          await axios.post(
            `https://192.168.1.2/api/Users/IsLogged/${this.email}/${this.password}`
          )
        ).data;
        if (findUser == 0) {
          alert("Неверные данные или же зарегистрируйтесь");
        } else {
          this.$router.push({ path: `/main/${findUser}`})      
        }
      } catch (ex) {
        console.log(ex);
      }
    },

    async getUsers() {
      this.ListUsers = (await axios.get("https://192.168.1.2/api/Users")).data;
    },
  },

  created() {
    this.getUsers();
  },
};
</script>