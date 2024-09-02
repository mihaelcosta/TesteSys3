<template>
  <Toast />
  <DataTable :value="products" dataKey="id" scrollable scrollHeight="100%" tableStyle="min-width: 50rem; height: 100%">
    <template #empty> Nenhum produto encontrado! </template>
    <template #loading> Carregando os Dados. </template>
    <Column sortable field="nomeProduto" header="Nome">
      <template #body="slotProps">
        {{ slotProps.data.title }}
      </template>
    </Column>
    <Column header="imagem">
      <template #body="slotProps">
        <img :src="slotProps.data.image" class="w-24 rounded" />
      </template>
    </Column>
    <Column sortable field="preco" header="Preço">
      <template #body="slotProps">
        {{ formatCurrency(slotProps.data.price) }}
      </template>
    </Column>
    <Column sortable field="codigoBarras" header="Código de Barras">
      <template #body="slotProps">
        {{ slotProps.data.description }}
      </template>
    </Column>
    <Column style="min-width: 6rem">
      <template #body="slotProps">
        <ConfirmPopup id="confirm" aria-label="popup"></ConfirmPopup>
        <Button icon="pi pi-pencil" outlined rounded :style="{ marginRight: '1em' }" @click="editaProduto(slotProps.data)" />
        <Button icon="pi pi-trash" outlined rounded severity="danger" @click="deletaProdutoPopup($event, slotProps.data)" />
      </template>
    </Column>
    <template #footer>
      <div class="card">
        <Paginator @page="onPageChange" :rows="rows" :totalRecords="total" template="FirstPageLink PrevPageLink CurrentPageReport NextPageLink LastPageLink" currentPageReportTemplate="Mostrando {first} ao {last} de {totalRecords}" />
      </div>
    </template>
  </DataTable>
  <EditarProduto ref="editaProduto" @salvo="atualizar"></EditarProduto>
</template>

<script>
export default {
  name: "ListaProdutos",
  components: {
    DataTable,
    Column,
    ColumnGroup,
    Row,
    Rating,
    Tag,
    Button,
    InputText,
    ConfirmPopup,
    EditarProduto,
    Paginator,
    Toast
  },
  data() {
    return {
      products: [],
      first: 0,
      total: 0,
      rows: 15

    }
  },
  methods: {
    formatCurrency(value) {
      return value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
    },
    async realizaFiltro(filtroNome, filtroCodigo) {
      try {
        const data = await realizaFiltroApi(filtroNome, filtroCodigo, this.rows);
        this.products = data.produtos;
        this.total = data.totalItems;
      } catch (error) {
        console.error('Erro ao aplicar filtro:', error);
        this.$toast.add({ severity: 'error', summary: 'Erro!', detail: 'Erro ao aplicar filtro. Tente novamente.', life: 3150 });
      }
    },
    deletaProdutoPopup(event, data) {
      this.$confirm.require({
        target: event.currentTarget,
        message: 'Deseja excluir esse produto?',
        icon: 'pi pi-info-circle',
        rejectProps: {
          label: 'Cancelar',
          severity: 'secondary',
          outlined: true
        },
        acceptProps: {
          label: 'Excluir',
          severity: 'danger'
        },
        accept: () => {
          this.excluirProduto(data.id)
        }
      });
    },
    async excluirProduto(id) {
      try {
        const sucesso = await excluirProdutoApi(id);
        if (sucesso) {
          this.atualizar()
          this.$toast.add({ severity: 'success', summary: 'Excluído!', detail: 'Produto excluído com sucesso!', life: 3150 });
        } else {
          this.$toast.add({ severity: 'error', summary: 'Erro!', detail: 'Erro ao excluir o produto. Tente novamente.', life: 3150 });
        }
      } catch (error) {
        this.$toast.add({ severity: 'error', summary: 'Erro!', detail: 'Ocorreu um erro ao tentar excluir o produto.', life: 3150 });
      }
    },

    editaProduto(data) {
      const lista = this.$refs.editaProduto;
      if (lista) {
        lista.abreEditaProduto(data);
      }
    },
    async getDados() {
      try {
        const data = await getDadosApi(this.first, this.rows);
        this.products = data.produtos;
        this.total = data.totalItems;
      } catch (error) {
        console.error('Erro ao obter dados:', error);
        this.$toast.add({ severity: 'error', summary: 'Erro!', detail: 'Erro ao coletar dados!', life: 3150 });
        // Aqui você pode adicionar lógica para mostrar mensagens de erro ao usuário
      }
    },
    atualizar() {
      this.getDados()
    },
    onPageChange(event) {
      this.first = event.page * event.rows;
      this.rows = event.rows;
      this.getDados();
    }
  },
  mounted() {
    this.getDados()
  }
}

import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import ColumnGroup from 'primevue/columngroup';
import Button from 'primevue/button';
import Tag from 'primevue/tag';
import Row from 'primevue/row';
import Rating from 'primevue/rating';
import InputText from 'primevue/inputtext';
import ConfirmPopup from 'primevue/confirmpopup';
import EditarProduto from '../Produtos/EditarProduto.vue'
import Paginator from 'primevue/paginator'
import { getDadosApi, realizaFiltroApi, excluirProdutoApi } from '../../services/productService.js'
import Toast from 'primevue/toast'
</script>

<style scoped>
.w-24 {
  width: 6rem;
}

.rounded {
  border-radius: .25rem;
}
</style>