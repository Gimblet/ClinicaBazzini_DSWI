# Como ejecutar el proyecto

Antes de ejecutar el proyecto por primera vez asegurarse de contar con una instancia local de SqlServer2022 corriendo.

El usuario 'sa' debe estar habilitado y con contraseña 'sql'. Si en caso no se encuentra este usuario habilitado, los parametros de conexión deben de ser modificados en el archivo appsettings.json dentro de cada módulo adentro de la solución.

En la carpeta raiz del proyecto se encuentran dos archivos sql:

 1. [ClinicaDB.sql](ClinicaDB.sql)
 2. [ClinicaSP.sql](ClinicaSP.sql)

Ejecutar ambos archivos en su orden respectivo para tener la base de datos lista.

# Requerimientos adicionales

- DotNet Sdk 8.0.xx
