# Build React with Node
FROM node:18 AS build
WORKDIR /app

COPY package*.json ./
RUN npm install
COPY . ./
RUN npm run build


FROM nginx:alpine
# Remove default Nginx welcome page
RUN rm -rf /usr/share/nginx/html/*

COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
