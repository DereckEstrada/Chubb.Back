Para poder ejecutar el back primero deberemos ejecutar el scriptDatabase.sql el cual se encargara de crear la base de datos en nuestro entorno, junto con las tablas usadas, primaries key, foreigns key, store procedure.
El siguiente paso sera cambiar la Connection String que se encuentra en la carpeta API, en el appsetting.json
![image](https://github.com/user-attachments/assets/0585f69d-0cdc-4eda-8d1f-ec911a79fb45)
En server ubicaremos el nombre de nuestro servidor del SMSS, adcional tambien deberemos cambiar el USER ID y Password por sus respectivas credenciales. 
Con esto ya deberiamos poder consumir las APIS desde el front.

Si tenemos problemas con los CORS debemos de ir a la carpeta API, en el program.cs encontraremos la configuacion de los CORS
![image](https://github.com/user-attachments/assets/0f3e2cf7-d9d2-4811-93cf-7087faedef70)

Podremos configurar el acceso de nuestro front en la linea "policy.WithOrigins("http://localhost:4200")", deberemos cambiar o agregar un nuevo "http://localhost:[Aqui iria el puerto en el que se levanto el front]"
