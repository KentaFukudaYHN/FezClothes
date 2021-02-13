<template>
    <div>
        Hello
        <!-- <router-view/> -->
    </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class App extends Vue{
    created() {
        console.log('App Created Loginコンポーネント遷移前')
        //this.$router.push({name: 'Login'})
    }
}
</script>