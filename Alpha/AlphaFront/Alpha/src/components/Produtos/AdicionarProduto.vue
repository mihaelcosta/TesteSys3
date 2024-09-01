<template>
  <div>
    <Button class="btn-linx-color" label="Novo Produto" severity="warn" icon="pi pi-plus" @click="exibePopupAdicionar" raised />
    <Dialog v-model:visible="visible" modal header="Novo Produto" :style="{ width: '40rem', padding: '1em' }">
      <div class="container-form">
        <FloatLabel :style="{ width: '100%' }">
          <label for="nomeProdutoInsercao">Nome</label>
          <InputText @input="this.validarCampos" v-model="novoProduto.Title" fluid id="nomeProdutoInsercao" autocomplete="off" />
        </FloatLabel>
        <FloatLabel>
          <label for="precoInsercao">Preço</label>
          <InputNumber @input="this.validarCampos" v-model="novoProduto.Price" inputId="precoInsercao" mode="currency" currency="BRL" locale="pt-BR" fluid showButtons :min="0" />
        </FloatLabel>
        <FloatLabel>
          <label for="codigoBarrasInsercao">Código de Barras</label>
          <InputText @input="this.validarCampos" v-model="novoProduto.Description" id="codigoBarrasInsercao" autocomplete="off" fluid />
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
      this.novoProduto = {
        Title: '',
        Description: '',
        Image: [],
        Price: 0
      }
    },
    async adicionarProduto() {
      try {
        const sucesso = await criarProdutoApi(this.novoProduto);
        if (sucesso) {
          this.loading = false;
          this.visible = false;
          this.$emit('salvo');
        }
      } catch (error) {
        this.loading = false;
        // Aqui você pode adicionar lógica para lidar com o erro, se necessário
      }
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

  },
  async salvarImagem() {
    this.loading = true
    if (!this.selectedFile) return;
    try {
      const response = uploadImagem(this.selectedFile)

      const result = await response.json();
      this.imageUrl = result.data.link;

      this.novoProduto.Image = this.imageUrl;
      await this.adicionarProduto();
    } catch (error) {
      console.error("Erro ao fazer upload da imagem:", error);
      this.loading = false
    }
  },
  validarCampos() {
    const regexCodigo = /^\d{13}$/;
    this.novoProduto.Description = regex.test(this.novoProduto.Description.replace(/\D/g, ''));

    const regexTitle = /^[a-zA-Z0-9 ]+$/;
    this.novoProduto.Title = regex.test(this.novoProduto.Title.replace(/\D/g, ''));

    const precoRegex = /^\d+(\.\d{1,2})?$/;


    this.novoProduto.Price = precoRegex.test(this.novoProduto.Price) && parseFloat(this.novoProduto.Price) > 0;


    const map = {
      '&': '&amp;',
      '<': '&lt;',
      '>': '&gt;',
      '"': '&quot;',
      "'": '&#039;'
    };

    this.novoProduto.Title.replace(/[&<>"']/g, function (m) { return map[m]; });
    this.novoProduto.Description.replace(/[&<>"']/g, function (m) { return map[m]; });
    this.novoProduto.Price.replace(/[&<>"']/g, function (m) { return map[m]; });
  }

}




import Button from 'primevue/button'
import Dialog from 'primevue/dialog'
import InputText from 'primevue/inputtext'
import InputNumber from 'primevue/inputnumber'
import FloatLabel from 'primevue/floatlabel';
import FileUpload from 'primevue/fileupload';
import uploadImagem from '../../services/imgbb'
import { criarProdutoApi } from '../../services/productService.js'

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