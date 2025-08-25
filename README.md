1- El primer paso para ejecutar correctamente el proyecto de .Net es crear la base de datos. 
para mayor facilidad se deja un script al mismo nivel de este Readme para ser ejecutado, el cual crea la base de datos y las tablas necesarias para el proyecto.

2- El connectionString a la base de datos es configurado con las opciones default de postgresql, es muy probable que sea necesario cambiar la contraseña dada por la creada en la instalacion en la maquina local donde se ejecutara el proyecto.

Decisiones Tecnicas.

Aunque faltan muchisimas cosas por hacer en este proyecto, se trato de implementar lo mejor y mas posible buenas practicas en el.
-Se utiliza patron de diseño inyeccion de dependencias para los servicios.
-Se utilizan DTOs para la entrega de datos.
-Se utiliza AutoMapper para el mapeo de entidades y DTOs.
-Se agrego patrones de diseño como Repository, Unit of Work y singleton para darle una capa de abstraccion mas al manejo de los datos con EntityFramework.
-Se utilizan Enums y Types para evitar la quema de codigo.
-Se separan responsabilidades y se invierten dependencias seguiendo principios SOLID.
-Se creo un sistema de pagos para las deudas en la cual se marcan las deudas como pagadas solo cuando se paga el total del monto.
-La edicion de deudas solo permite cambiar la descripcion dado que al realizar los pagos se altera el monto total de la deuda.

Se trato de usar las mejores practicas aun sabiendo que faltan temas de seguridad, autenticacion, roles, JWT, claims, try y catch, pruebas unitarias.

Soy consciente de que faltan puntos extras como un endpoint para exportar Deudas con JSON y los Test Unitarios, siento que la prueba es bastante larga si se quiere tener todos los puntos cubiertos.

Trate de cubrir lo que mas pude en los 2 dias que tenia para realizarla.

------------------------------------------------------------------------------------------------------------------

Fase 2: Preguntas de Arquitectura y Experiencia

Microservicios:
Si el sistema creciera y necesitará pasar de monolito a microservicios, ¿cómo dividirías los
servicios y qué consideraciones de comunicación implementarías?

Para este caso podemos crear y dividir en varios microservicios, por ejemplo
-Microservicio de Seguridad y Autenticacion.
-Microservicio para la carga de deudas por medio de JSON.
-Microservicio de notificaciones.
-Microservicio para el registro de Logs de la aplicacion.
-Microservicio API Gateway.
-Implementacion de JWT para la seguridad y correcta comunicacion entre APIs.

-------------------------------------------------------------------------------------------------------------------

Optimización en la nube:
Supongamos que tu aplicación corre en AWS. ¿Qué servicios usarías y porque para:

● Autenticación segura.
● Base de datos.
● Cache y escalabilidad.
● Balanceo de carga.

Lastimosamente no tengo experiencia con AWS pero si con Azure y puedo hacer una comparativa de lo que usaria en Azure en lugar de AWS.

● Autenticación segura:
Azure AD B2C: Ofrece registro, login  y emisión de tokens JWT.
Se puede integrar con API Management y App Services.

Uso de Indetidad administrada para el tema de la seguridad de credenciales e integracion con otros servicios de azure como Key Vault, azure storage y Databases

● Base de datos
Azure SQL Database: Base relacional completamente administrada en complemento con SQL server management.
Azure Storage para manejo de archivos, blobs.

● Cache y escalabilidad
Azure Cache para Redis: Para caching de datos y sesiones.
App Service Plan con escalado automático: Para aplicaciones web sin preocuparte de la infraestructura.

● Balanceo de carga

Azure Application Gateway: Balanceo a nivel de HTTP.
Ventaja: Maneja SSL offloading y enrutamiento basado en URL.

---------------------------------------------------------------------------------------------------------------------------

Buenas prácticas de seguridad:
Menciona al menos 3 prácticas clave que aplicarías para garantizar seguridad en backend,
frontend y despliegue en la nube.

Para el backEnd es necesario el uso de JWT, claims, Roles, Auth, encriptacion de datos,evitar SQL injection, idempotencia para evitar multiples llamados.
Para el frontend con angular el uso de Guards, interceptors, JWT, localStorage,  validacion de formularios, encriptacion de datos.

En la nube HTTPS/TLS para toda comunicacion, Monitoreo constante y alertas, logs, identidades administradas, keyVaults.

----------------------------------------------------------------------------------------------------------------------------

Despliegue:
Si tuvieras que desplegar esta app en producción, ¿qué pipeline CI/CD diseñarías para
asegurar calidad, testeo y despliegue continuo?

los pasos de los pipelines serian:

-CheckOut de repositorio desde Git.
-Instalacion de depencias (Angular npm install - .NET dotnet restore)
-Build del proyecto. (Angular: ng build --prod - .NET: dotnet build --configuration Release)
-Ejecucion de pruebas Unitarias. (Angular: ng test - .NET: dotnet test)
-Analisis de codigo con sonarQube.
- Aprobacion Manual de cambios bajo PRs.
-Despligue de BackEnd y FrontEnd en produccion
-Monitorización post-despliegue, Verificar errores, métricas de uso y logs.
-Alertas ante fallos.