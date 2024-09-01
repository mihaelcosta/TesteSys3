<template>
  <div class="container-filtro">
    <InputText id="nomeProdutoFiltro" v-model="filtroNome" placeholder="Nome" />
    <InputText id="codigoBarrasFiltro" v-model="filtroCodigoBarras" placeholder="Código de Barras" />

    <Button type="button" label="Filtrar" class="btn-linx-color" @click="avisaFiltro"></Button>
  </div>
</template>

<script>
export default {
  name: "Filtro",
  components: {
    InputText,
    Button
  },
  data() {
    return {
      filtroNome: null,
      filtroCodigoBarras: null
    }
  },
  methods: {
    avisaFiltro() {
      this.$emit('filtrar', this.filtroNome, this.filtroCodigoBarras);
    }
  }
}
import InputText from 'primevue/inputtext'
import Button from 'primevue/button'
</script>

<style scoped>
.container-filtro {
  display: flex;
  gap: 1em;
  justify-content: end;
}

#codigoBarrasFiltro {
  width: 300px;
}
</style>