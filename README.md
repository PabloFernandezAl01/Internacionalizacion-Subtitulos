# Internacionalización-Subtitulos
Ampliación del proyecto original (Internacionalización). Soporte para localizar subtítulos.

# Pablo Fernández Álvarez

## Nota: 

Este proyecto es una ampliación del proyecto de Internacionalización. Por ello, he añadido, dentro de cada apartado, un sub-apartado refiriendose 
a la nueva funcionalidad de los subtítulos.

# Resumen

Crear una herramienta de internacionalización y localización, en Unity. La localización implica traducir los textos, diálogos y subtítulos al idioma de destino, así como adaptar los gráficos y elementos culturales, como nombres de personajes o referencias culturales, para asegurar que sean apropiados para el público local. Por otro lado, la internacionalización, que consiste en el diseño y desarrollo de código para que sea fácilmente adaptable un juego a diferentes mercados y culturas. La localización e internacionalización son importantes porque permiten a los desarrolladores de videojuegos llegar a audiencias más amplias y diversificadas.

Por un lado, la internacionalización se hará desde el editor de Unity durante el desarrollo del juego, por otro, la localización ocurrirá durante el tiempo de ejecución del juego mediante un manager global. Este sistema estará basado en un diccionario de claves valor. La herramienta estará integrada en el editor de Unity, desde donde se mostrará toda la información necesaria mediante una serie de ventanas emergentes.

Con este sistema se podrá localizar textos, subtítulos, imágenes y música. Se podrán generar claves nuevas, modificar existentes o eliminar las que no se utilicen.

## Subtítulos	

En cuanto a los subtítulos, la idea es crear un motor de subtítulos sencillo, el cuál sepa procesar ficheros con la información de los subtítulos y mostrarlos en pantalla.
Al igual que con los textos, las imágenes y la música, estos subtítulos serán localizabes.

Para entender más a fondo el objetivo de este motor de subtítulos:

El motor seguirá la norma española de subtitulado para sordos UNE-153010: 2012

- Aspecto visuales:
	+ La posición de los subtítulos debe ser estática y ubicarse en la parte inferior central de la pantalla. En caso de 
	haber efectos sonores se indicará en la parte superior derecha.
	+ No se deben mostrar más de dos o tres (en caso excepcionales) líneas al mismo tiempo. Además, las líneas deben contener
	como máximo de 37 caracteres.
	+ Los subtítulos deben tener un tamaño de tal forma que sean legibles a 2,5 metros de la pantalla.
	+ Por cuestiones de visibilidad, está permitido usar un caja para los subtítulos para crear contraste con el color del fondo.

- Aspectos temporales: 
	+ La velocidad en la que se muestran los subtítulos deberá aproximarse a 15 caracteres por segundo.
	+ La norma hace referencia a la sincronismo visual-auditivo. Para este proyecto no va a afectar ya que simplemente
	se subtitulan audios sin ningún elemento visual vinculado.

- Identificación de personajes:
	+ Se usarán distintos colores en el texto de los subtítulos para distinguir entre las voces de los personajes de
	producto audiovisual

- Efectos sonoros:
	+ Se mostrarán subtítulos para efectos sonoros siempre que no sean evidentes
	+ Se colocarán en la parte superior derecha de la pantalla, entre paréntesis y mayúscula inicial

- Información contextual y voces en off:
	+ Se colocarán en la parte inferior izquierda antes de los propios subtítulos
	+ Se mostrarán en mayúsculas y entre paréntesis

- Música y canciones:
	+ Se colocarán y mostrarán igual que los efectos sonoros
	+ Hay 3 formas de mostrarlos: Genero de la música, sensación que transmite o identificando la pieza musical.

- Criterios editoriales:
	+ No afectan al motor de subtitulado ya que el formato del texto debe estar adecuado a la norma.



# Motivación y objetivos

En clase hemos hablado de la internacionalización y la localización pero no hemos llegado a desarrollar nada sobre ello por lo que nos pareció interesante el desarrollo de esta herramienta. Los juegos que no se adaptan a las preferencias culturales y lingüísticas de los jugadores locales pueden no ser bien recibidos, lo que puede reducir la demanda del juego y afectar negativamente las ventas, por ello pensamos que estos son procesos críticos para la industria del videojuego y de ahí su importancia. 
Hemos decidido hacerla en Unity para aprovechar sus características en el desarrollo, como por ejemplo no tener que desarrollar nosotros una interfaz gráfica. Si bien es cierto que Unity tiene ya su propio sistema de localización bastante completo hemos preferido empezar el nuestro desde cero y centrarnos en lo que sería el desarrollo del sistema de localización en vez de hacer extensiones a uno ya existente. 

Una ventaja adicional que tendrá nuestro sistema con respecto al de Unity es que podremos elegir qué idiomas utilizar. Unity tiene por defecto una lista bastante amplia de idiomas pero está limitado ya que no se puede ampliar. Esto puede suponer un problema ya que por ejemplo Unity no distingue entre español y español latino. Esto para la localización e internacionalización puede suponer un problema pero también para los desarrolladores que quieran sacar sus juegos en consolas (de esto último no podemos entrar más en detalle debido a haber firmado un NDA).

Otra ventaja que creemos que tiene nuestro sistema con respecto al de Unity es que al poder elegir nosotros los idiomas que se utilicen también podemos generar valores que no tengan porqué ser un idioma como tal. Por ejemplo, podríamos desarrollar un idioma con una versión infantil donde todas las palabras malsonantes sean sustituidas por algo más suave por ejemplo generando los idiomas ‘Spanish’ y ‘Spanish_kids’.

Lo que esperamos conseguir con este sistema de internacionalización es una herramienta robusta con la cual se puedan localizar juegos en Unity de una forma sencilla para los desarrolladores. Sin tener que manejar por nuestra cuenta ficheros adicionales y con una interfaz entendible y cómoda. Para ello utilizaremos las herramientas que nos proporciona Unity para extender su editor y poder crear nuestras propias ventanas a través de las cuales poder generar la información necesaria para la internacionalización.

Las ventanas que se generen estarán separadas por funcionalidad. Por lo que las ventanas que queremos que haya serán:
 - Ventana general: desde esta ventana se establecen los idiomas del juego y cuál será el idioma por defecto.
 - Ventanas de clave-valores: desde estas ventanas se establecen las claves y valores del juego. Se pueden generar nuevos, eliminar las que haya, o editar las claves para cada idioma. Habrá una ventana para textos, una para imágenes, otra para sonidos y otra para subtítulos. 
 
Los objetivos específicos del proyecto incluyeron: 
 - Desarrollar una herramienta que permita la gestión de diferentes idiomas y sus respectivas traducciones. 
 - Implementar esta herramienta en la plataforma Unity para permitir la integración de las traducciones en videojuegos. 
 - Crear una interfaz de usuario intuitiva que permita a los desarrolladores gestionar fácilmente los diferentes idiomas y sus traducciones.
 - Desarrollar una arquitectura modular y escalable que permita la integración de nuevas funcionalidades en el futuro y que ayude al rendimiento del videojuego.

 ## Subtítulos

Los subtítulos cumplen varias funciones importantes en los videojuegos:

- En relación con la internacionalización: Son una herramienta para localizar gameplay a través de la traducción de texto. Esto tarea resulta más sencilla
que crear archivos de audio adaptados a cada región, con lo que ello supone (contratar actores de doblaje por cada región, por ejemplo). De esta forma, 
el usuario puede entender el gameplay con los archivos de audio originales, es decir, aquellos creados en la región donde se desarrolló el videojuego, y
por lo tanto, en el idioma usado en dicha región. Es verdad que el idioma de los audios no tiene porqué coincidir con el idioma de la región donde 
se desarrolla un videojuego, pero, por lo general, esto es lo que sucede. Esto también se utiliza en películas, series y demás productos audiovisuales.

- Flexibilidad de volumen: Debido a que los subtítulos proporcionan una segunda vía de comunicación, la principal vía (audio) no es imprescindible. 
De esta forma, el usuario puede ajustar el reducir volumen del audio y seguir entendiendo el gameplay. Esto puede ayudar si el jugador se encuentra en un 
entorno ruidoso o no dispone de un dispositivo de reproducción de audio ( hay muchas clases de jugadores :-P ).

- Ayuda a usuarios con problemas auditivos: Al proporcionar texto sincronizado con el diálogo y otros elementos de sonido en el juego, los subtítulos 
permiten que las personas con dificultades para escuchar o con pérdida auditiva puedan seguir y comprender la historia, las conversaciones y los efectos de sonido.


# Detalles de diseño/implementación

He suprimido los detalles de implementación y diseño de la internacionalización.
Los subtítulos funcionan como un tipo más de elemento a localizar (texto, subtítulos, audio, imágenes)

## Subtítulos


<!-- # Resultados obtenidos

Hemos creado una escena de ejemplo donde se ve el resultado de todo lo implementado anteriormente.

## Paquete en el editor

Dentro del editor se puede observar el apartado Localization, desde el que se puede controlar los distintos aspectos de la herramienta: las claves, los distintos idiomas,  algunos extras como la moneda, y el apartado Scene Utility para poder cambiar el idioma  de toda la escena de forma fácil.

![IMG1](./readme/editor.png)

Para usar la herramienta será necesario acoplar un componente que implemente la clase Localizable al objeto en cuestión (Ej: un texto), para así poder indicar qué claves queremos que siga.

![IMG2](./readme/component.png)

## KeyCreators
Todos los KeyCreators tendrán un aspecto similar al de la imagen. Arriba encontramos los distintos idiomas que tengamos preparados en nuestra herramienta, y abajo tanto las claves como los valores del asset que queramos modificar (texto, sprite, audio, fuente…). Además podemos tanto crear como eliminar claves a nuestro antojo,

![KEYCREATOR](./readme/keyCreator.png)

## Escena de prueba
Hemos preparado una escena de prueba donde se ve el funcionamiento de la herramienta.

Aquí un video de su funcionamiento:
https://drive.google.com/file/d/1_1upkUEl4Izr0-WY_2NRMupvfiDILCtu/view?usp=sharing 

![ESCENAPRUEBA](./readme/escenaPrueba.png)

# Conclusiones

Hemos conseguido realizar lo que teníamos pensado, de una forma bastante completa. Hemos podido incluso añadir cosas extra como el apartado Scene Utilities para poder editar la escena cómodamente y exportación e importación a CSV. Viendo el sistema de  Unity y el nuestro pensamos que no se queda atrás y hemos hecho un buen trabajo.

# Instalación

Para facilitar la instalación de esta herramienta se ha creado un package de Unity, estos son los pasos para la instalación:

1- Descargar el package del repositorio de Github, en el apartado de Releases.

![STEP](./readme/step.png)

2- Dentro del proyecto en el que queremos utilizar la herramienta, hacer clic derecho en Assets (o buscarlo en el menú de arriba) y seleccionar la opción:
“Import Package -> Custom Package”.

![STEP1](./readme/step1.png)

3- Buscar la ubicación de la descarga del Package y “Abrir”.

![STEP2](./readme/step2.png)

4- Seleccionar todo y “Import”

![STEP3](./readme/step3.png)

5- Si la instalación ha sido correcta, debería aparecer una opción en la barra del programa con todas las opciones de la herramienta.

![STEP4](./readme/step4.png) -->
