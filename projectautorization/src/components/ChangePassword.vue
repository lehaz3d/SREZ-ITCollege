<template>
<div class="other container-authorization">
        <div></div>
                <img src="../components/img/lock.png" height="300" width="300" />
                <h4>Изменение пароля</h4>
                <form class="login-form content-align" @submit.prevent="change">
                    <label>Email</label>
                    <input class="login-form__input" type="email" v-model="email" />
                    <label>Новый пароль</label>
                    <div>
                        <input type="password"  class="login-form__input" v-model="password" />
                    </div>
                    <label>Подтверждение пароля</label>
                    <div>
                        <input type="password" class="login-form__input" v-model="repeatPassword" />
                    </div>
                    <input type="submit" value="Изменить пароль"/>
                </form>
            <footer style="margin-top: 30px">
                <label> Обратно на окно </label>
                <a @click="$router.push('/')"><label>Авторизации</label></a>
            </footer>
    </div>           
</template>

<script>
import axios from "axios";
export default {
    data() {
        return {
            email: "",
            password: "",
            repeatPassword: "",
        };
    },

    methods: {
        async change() {
            try {
                if (this.password != this.repeatPassword) {
                    alert("Пароли не совпадают");
                    return;
                }
                let request = await axios.post(
                    `https://192.168.1.2/api/Users/RestorePassword/${this.email}`
                );
                if (request.data === -1) {
                    alert("Такого email не существует");
                    return;
                }
                request = await axios.post(
                    `https://192.168.1.2/api/Users/ChangePassword/${this.email}/${this.password}`
                );
                alert("Пароль успешно изменён");
                this.$router.push("/");
            } catch (ex) {
                console.log(ex);
            }
        },
    },
};
</script>