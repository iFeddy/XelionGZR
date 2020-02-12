# XelionZR

## Table of Contents

- [Acerca de](#about)
- [Uso](#getting_started)

## Acerca de <a name = "about"></a>

Xelion es un servidor privado de Fiesta Online. Las Zonas del servidor necesitan interactuar con un servidor html para obtener datos. XelionZR se encarga de crear esa respuesta y darsela a las Zonas para que funcionen. Esta version es la misma de GamigoZR pero Open Source para que sea transparente.

## Instalación <a name = "getting_started"></a>

Para instalar este servicio, lo primero que debes hacer es compilarlo con VisualStudio 2019. Despues dentro del Exe se crea un batch con el siguiente comando:

- SC CREATE "_XelionZR" binpath="XelionZR.exe"

Para borrarlo simplemente usar el comando
 
- SC DELETE _XelionZR
