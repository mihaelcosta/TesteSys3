<template>
  <main>
    <div class="title-container">
      <h1> Produtos </h1>
      <AdicionarProduto @salvo="atualizaLista" />
    </div>
    <Card>
      <template #content>
        <Filtro @filtrar="filtrar" />
      </template>
    </Card>
    <Card :style="{ flex: 1 }">
      <template #content>
        <ListaProdutos ref="listaRef" />
      </template>
    </Card>
  </main>
</template>

<script>
export default {
  name: "Produtos",
  components: {
    Card,
    Filtro,
    AdicionarProduto,
    ListaProdutos
  },
  data() {
    return {
      products: []
    }
  },
  methods: {
    filtrar(filtroNome, filtroCodigo) {
      const lista = this.$refs.listaRef;
      if (lista) {
        lista.realizaFiltro(filtroNome, filtroCodigo);
      }
    },
    atualizaLista() {
      const lista = this.$refs.listaRef;
      if (lista) {
        lista.atualizar();
      }
    }
  }
}

import Card from 'primevue/card';
import Filtro from '../components/Produtos/Filtro.vue'
import AdicionarProduto from '../components/Produtos/AdicionarProduto.vue'
import ListaProdutos from '../components/Produtos/ListaProdutos.vue'

</script>

<style scoped>
main {
  display: flex;
  flex-direction: column;
  gap: 1em;
  min-height: 100vh;
}

.title-container {
  display: flex;
  justify-content: space-between;
}
</style>