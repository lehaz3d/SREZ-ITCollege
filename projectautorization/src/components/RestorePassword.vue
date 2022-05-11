<template>
<div class="other container-authorization">
        <div></div>
        <img src="../components/img/lock.png" height="300" width="300"/>
        <form class="login-form content-align" @submit.prevent="restore">
                    <h4>Восстановление пароля</h4>
                    <label>Email</label>
                    <input type="email" class="login-form__input" v-model="email" />
                    <input type="submit" class="login-form__input" value="Восстановить пароль"
                    />
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
        };
    },
    methods: {
        async restore() {            
            try {
                let selectEmailUser = await axios.post(
                    `https://192.168.1.2/api/Users/RestorePassword/${this.email}`
                );
                if (selectEmailUser.data === -1) {
                    alert("Такого email не существует");
                    return;
                }
                this.$router.push(`/change`);
            } catch (ex) {
                console.log(ex);
            }
        },
    },
};
</script>