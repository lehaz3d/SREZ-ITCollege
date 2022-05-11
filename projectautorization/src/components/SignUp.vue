<template>
<div class="other container-authorization">
        <div></div>
        <form @submit.prevent class="login-form content-align">
            <h4>Регистрация</h4>
            <label for="input-fio">ФИО (через пробел)</label>
            <input v-model="fio" type = "text" id="input-fio" class="login-form__input" required>
            <label for="input-Email">Email</label>
            <input type = "email" v-model="email" id="input-Email" class="login-form__input" required>
            <label for="input-password">Пароль</label>
            <div>
                <input v-model="password" type = "password" id="input-password" class="login-form__input" required>
            </div>
            <label for="input-password-again">Подтверждение пароля</label>
            <div>
                <input v-model="repeatpassword" type = "password" id="input-password-again" class="login-form__input" required>
            </div>                     
            <button type ="submit" @click="createNewUser">Зарегистрироваться</button>
        </form>
        <footer class="footer">
            <span>Уже есть аккаунт? <a @click="$router.push({ name: 'login'})">Войти</a></span>
        </footer>
    </div> 
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      fio: "",
      email: "",
      password: "",
      repeatpassword: ""
    };
  },

  methods: {  
    async createNewUser() {
      try {
        if (this.repeatpassword != this.password) {
          alert("Пароли не совпадают")
          return;
        }
        else {
          var addUser = (
          await axios.post(
            `https://192.168.1.2/api/Users/Registration/${this.fio}/${this.email}/${this.password}`));
        }
        if (addUser.status === 200) {
          alert("Пользователь создан")
          this.$router.push({ name: 'login'})
        }
      } catch (ex) {
        alert("Ошибка создания пользователя")
        console.log(ex)
      }
    },
  },
};
</script>
