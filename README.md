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

