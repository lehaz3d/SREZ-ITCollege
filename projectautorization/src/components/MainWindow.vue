<template>
    <div class="other container-authorization">
        <form @submit.prevent>
            <img src="../components/img/man.png" height="300"/>
            <h4>Добро пожаловать</h4>
            <label>{{user.firstName}}</label>
            <label>{{user.lastName}}</label>
            <label>{{user.middleName}}</label> 
            <input type="submit" style="margin-top: 60px" class="button-reg" value="Выход" @click="$router.push('/')"/>           
        </form>
    </div>  
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      user: Object
    };
  },
  
  async mounted() {
    const selectUser = (
          await axios.get(
            `https://192.168.1.2/api/Users/GetUserId/${this.$route.params.userId}`
          )
        ).data;
        this.user = selectUser;
  }
};
</script>

