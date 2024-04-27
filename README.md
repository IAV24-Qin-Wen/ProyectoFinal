# ProyectoFinal 

## Autores
- Nanxi Qin ([NanxiQin](https://github.com/NanxiQin))
- Jianuo Wen Hu ([Jjianuo](https://github.com/Jjianuo))

## Propuesta
<p align="justify">
Este es el proyecto final de la asignatura de Inteligencia Artificial para Videojuegos del Grado en Desarrollo de Videojuegos de la UCM.
</p>

- [Alive by night-time]
<p align="justify">
Este proyecto consiste en una competicion entre dos bandos, los supervivientes y el asesino. 
  Los supervivientes deben intentar escapar de la mansión.
  El asesino debe intentar matar a los supervivientes antes de que consigan escapar 

El juego cuenta con los siguientes comportamientos:
</p>
<p align="justify">  
    
- Los supervivientes deben salir de la mansión, para esto deberán reparar una cantidad N de generadores repartidos por el mapa. Es controlado por un árbol de comportamiento complejo.
- Los supervivientes pueden comunicarse entre ellos para enviar información relevante.
- Los generadores tardan un tiempo T en ser reparados, los supervivientes no pueden moverse mientras estan reparando un generador.
- Una vez reparados todos los generadores o cuando sólo queda un superviviente, las puertas pueden ser desbloqueadas
- Para desbloquear una puerta, los supervivientes tardan un tiempo T, los supervivientes no pueden moverse mientras desbloquean la puerta.
- El asesino patrulla y ataca a cualquier superviviente en su cercanía.
- Cuando el asesino da X número de golpes a un superviviente, el superviviente es teletransportado a uno de los ganchos.
- Los supervivientes atrapados en un gancho no pueden moverse hasta que les rescata otro superviviente.
- Los supervivientes mueren tras estar un tiempo X enganchados, o tras ser enganchados X veces.
- El asesino tiene un mapa de influencia que va variando según cambia el estado del juego (Número y disposición de generadores, estado de los supervivientes, ganchos...)
- (Opcionales)
- Los armarios permiten a los supervivientes ocultarse del asesino.
- El asesino puede abrir armarios para encontrar a un superviviente, en cuyo caso son dañados
</p>

## Punto de partida
Se parte de un proyecto base de **John Lemon's Haunted Jaunt: 3D Beginner** [https://learn.unity.com/project/john-lemon-s-haunted-jaunt-3d-beginner].

El proyecto sólo se ha cogido para la parte estética del juego, los scripts han sido todos implementados por los estudiantes.
Se han usado también otros assets encontrados por internet.

## Diseño de la solución

Los índices mostrados a continuación son relativos al apartado del enunciado al que hacen referencia.

A.Mundo virtual:

B.Supervivientes:

C.Generadores:

C.Asesino:

E.Ganchos:

### Diseño del nivel


## Pruebas y métricas
### Pruebas
A.Mundo virtual:

B.Supervivientes:

D.Generadores:

C.Asesino:

E.Ganchos:

- [Vídeo con la batería de pruebas](https://youtu.be/)

____________________________________________________________________________________________________________________________________________________________________
### Métricas

_Las métricas se han realizado con todos los scripts activados, con movimiento de jugador con teclado en el ejecutable._

Ordenador de la FDI (Procesador 12th Gen Intel Core i7-12700, 32GB RAM, RTX 2060)

| Tamaño del mapa | Supervivientes | FPS |
| ------------- | ------------- | ------------- |


Ordenador de miembro 1 (Procesador 13th Gen Intel Core i5-13600K, 32GB RAM, RX 7800XT)

| Tamaño del mapa | Supervivientes | FPS |
| ------------- | ------------- | ------------- |


Ordenador de miembro 2 (AMD Ryzen 7 4800H, 16GB RAM)

| Tamaño del mapa | Supervivientes | FPS |
| ------------- | ------------- | ------------- |

## Producción
<p align="justify">
Para el correcto desarrollo de la práctica el reparto de tareas y el seguimiento de las mismas se realizará utilizando como herramienta las issues de github desde la pestaña de Projects.
El enlace desde el que por tanto se podrá hacer el seguimiento de la evolución del proyecto así como la organización del mismo es el siguiente: 
</p>

- [https://github.com/orgs/IAV24-Qin-Wen/projects/1](https://github.com/orgs/IAV24-Qin-Wen/projects/1)
<p align="justify">
Además, obsérvese que dentro de la pestaña de project las distintas tareas tienen asignadas distintas labels y descripciones para poder comprender con mayor facilidad la tarea concreta a la que hacen referencia.
</p>

## Licencia
<p align="justify">
Nanxi Qin y Jianuo Wen, autores de la documentación, código y recursos de este trabajo, concedemos permiso permanente a los profesores de la Facultad de Informática de la Universidad Complutense de Madrid para utilizar nuestro material, con sus comentarios y evaluaciones, con fines educativos o de investigación; ya sea para obtener datos agregados de forma anónima como para utilizarlo total o parcialmente reconociendo expresamente nuestra autoría.
</p>
<p align="justify">
Una vez superada con éxito la asignatura se prevee publicar todo en abierto (la documentación con licencia Creative Commons Attribution 4.0 International (CC BY 4.0) y el código con licencia GNU Lesser General Public License 3.0).
</p>

## Referencias

Los recursos de terceros utilizados son de uso público.

- *AI for Games*, Ian Millington.
- UnityLearn(https://learn.unity.com/project/john-lemon-s-haunted-jaunt-3d-beginner).

