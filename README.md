# _Prueba Técnica - API CRUD de Usuarios con .NET y MySQL_

## Descripción
La API implementa un CRUD básico de la entidad Usuarios, la cual contiene campos como ID, Nombre, Email y Password. Se la desarrolló empleando tecnologías como Entity Framework, MySQL, patrón de diseño Repository y contenedores Docker. También, se manejan de manera segura las contraseñas, utilizando hashing y salt.

## Características Principales
# • CRUD de Usuarios:
La API implementa todas las operaciones básicas (Create, Read, Update, Delete) para la entidad Usuarios. Además, se implementaron métodos de hashing y salt que aseguran que las contraseñas queden protegidas, almacenando únicamente éstos datos y no el password en texto plano.

# • Patrón de diseño Repository:
El proyecto sigue el patrón de diseño Repository, el cual separa la lógica de acceso a datos del resto de la aplicación.

# • Dockerización del Proyecto:
El proyecto está completamente dockerizado, facilitando el despliegue en contenedores.

# • Configuración de Variables de Entorno:

Se implementó el uso de variables de entorno para la conexión a la base de datos en el __entorno local__, asegurando que las credenciales sensibles no estén hardcodeadas. 
Además, se definió un **fallback** hacia el archivo `application.Development.json`, permitiendo así el funcionamiento en el entorno de __Docker__.
