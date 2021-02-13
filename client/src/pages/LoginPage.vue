<template>
    <div>
        <p>Hello Login</p>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'Vue-property-decorator'

@Component
export default class LoginPage extends Vue {
    created() {
        console.log('created Login')
    }
}
</script>