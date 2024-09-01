<template>
  <main>
    <h1> Seja bem vindo(a) ao Alpha </h1>
    <h4> A maior loja de Produtos Fake do Brasil </h4>
    <Button class="btn-linx-color" as="router-link" label="clique para acessar a plataforma" to="/Produtos" raised rounded severity="warn" />
  </main>
</template>

<script>
export default {
  name: "Home",
  components: {
    Button
  }
}

import Button from 'primevue/button';
</script>

<style scoped>
main {
  height: 100vh;
  width: 100vw;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  gap: 1em;
}
</style>

 