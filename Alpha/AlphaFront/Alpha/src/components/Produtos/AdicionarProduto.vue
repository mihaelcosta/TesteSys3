<template>
  <div>
    <Button class="btn-linx-color" label="Novo Produto" severity="warn" icon="pi pi-plus" @click="exibePopupAdicionar" raised />
    <Dialog v-model:visible="visible" modal header="Novo Produto" :style="{ width: '40rem', padding: '1em' }">
      <div class="container-form">
        <FloatLabel :style="{ width: '100%' }">
          <label for="nomeProdutoInsercao">Nome</label>
          <InputText v-model="novoProduto.Title" fluid id="nomeProdutoInsercao" autocomplete="off" />
        </FloatLabel>
        <FloatLabel>
          <label for="precoInsercao">Preço</label>
          <InputNumber v-model="novoProduto.Price" inputId="precoInsercao" mode="currency" currency="BRL" locale="pt-BR" fluid showButtons :min="0" />
        </FloatLabel>
        <FloatLabel>
          <label for="codigoBarrasInsercao">Código de Barras</label>
          <InputText v-model="novoProduto.Description" id="codigoBarrasInsercao" autocomplete="off" fluid />
        </FloatLabel>
        <label for="uploadInsercao">Imagem</label>
        <FloatLabel>
          <FileUpload id="uploadInsercao" class="btn-linx-color" customUpload auto mode="basic" accept="image/*" :maxFileSize="1000000" @select="onFileSelect" chooseLabel="Escolher" />
          <img v-if="src" :src="src" alt="Image" :style="{ maxWidth: '200px', margin: '1em auto', maxHeight: '300px', display: 'flex' }" />
        </FloatLabel>
      </div>
      <div class="button-container-insercao">
        <Button type="button" label="Cancelar" severity="secondary" @click="visible = false"></Button>
        <Button type="button" label="Salvar" class="btn-linx-color" @click="adicionarProduto" :loading="loading"></Button>
      </div>
    </Dialog>
  </div>
</template>

<script>
export default {
  name: "AdicionarProduto",
  components: {
    Button,
    Dialog,
    InputText,
    FloatLabel,
    InputNumber,
    FileUpload,
    Toast
  },
  data() {
    return {
      visible: false,
      novoProduto: {},
      loading: false,
      src: null,
      selectedFile: null
    }
  },
  methods: {
    exibePopupAdicionar() {
      this.visible = true
      this.src = false
      this.novoProduto = {
        Title: '',
        Description: '',
        Image: [],
        Price: 0
      }
    },
    async adicionarProduto() {
      this.loading = true;
      try {
        const sucesso = await criarProdutoApi(this.novoProduto);
        if (sucesso) {
          this.$toast.add({ severity: 'success', summary: 'Produto Adicionado!', detail: 'O produto foi adicionado com sucesso.', life: 3150 });
          this.loading = false;
          this.visible = false;
          this.$emit('salvo');
        }
      } catch (error) {
        this.loading = false;
        this.$toast.add({ severity: 'error', summary: 'Erro!', detail: 'Erro ao adicionar produto.', life: 3150 });
      }
    },
    onFileSelect(event) {
      const file = event.files[0];
      const reader = new FileReader();

      reader.onload = (e) => {
        // Converte a imagem em byte array
        const arrayBuffer = e.target.result;
        const byteArray = new Uint8Array(arrayBuffer);
        this.novoProduto.Image = Array.from(byteArray);
      };

      reader.readAsArrayBuffer(file);

      const file2 = event.files[0];
      const reader2 = new FileReader();

      reader2.onload = async (e) => {
        this.src = e.target.result;
      };

      reader2.readAsDataURL(file2);

    }
  }
}





import Button from 'primevue/button'
import Dialog from 'primevue/dialog'
import InputText from 'primevue/inputtext'
import InputNumber from 'primevue/inputnumber'
import FloatLabel from 'primevue/floatlabel';
import FileUpload from 'primevue/fileupload';
import { criarProdutoApi } from '../../services/productService.js'
import Toast from 'primevue/toast'

</script>

<style scoped>
.button-container-insercao {
  display: flex;
  justify-content: end;
  gap: .5em;
  padding-top: 1em;
}

.container-form {
  display: flex;
  flex-direction: column;
  padding: 2em 0;
  gap: 2em;
}
</style>