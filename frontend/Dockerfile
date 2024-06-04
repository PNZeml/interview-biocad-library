FROM node:18-alpine3.18 AS builder
WORKDIR /opt/build

COPY package.json package-lock.json ./
RUN npm ci

COPY . .

ARG VITE_APPLICATION_API_URL="http://localhost:8080/api"
ENV VITE_APPLICATION_API_URL=$VITE_APPLICATION_API_URL

RUN npm run build

FROM nginx:1.25.4-alpine3.18 AS app
WORKDIR /usr/share/nginx/html

COPY --from=builder /opt/build/dist .
COPY ./nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 8081
CMD ["nginx", "-g", "daemon off;"]
