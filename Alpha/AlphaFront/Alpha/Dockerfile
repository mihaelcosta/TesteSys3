# Use a imagem base Node.js LTS com Alpine
FROM node:lts-alpine

# Define o ambiente como desenvolvimento
ENV NODE_ENV=development

# Define o diretório de trabalho
WORKDIR /usr/src/app

# Copia os arquivos de configuração do npm
COPY package.json package-lock.json* npm-shrinkwrap.json* ./

# Instala todas as dependências, incluindo as de desenvolvimento
RUN npm install --silent

# Copia o restante do código da aplicação
COPY . .

# Exposição da porta de desenvolvimento
EXPOSE 5173

# Define o usuário como 'node' para evitar problemas de permissões
RUN chown -R node /usr/src/app
USER node

# Comando para iniciar o Vite no modo de desenvolvimento
CMD ["npm", "run", "dev"]