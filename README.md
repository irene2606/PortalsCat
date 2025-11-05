# 🪄 Portal’s Cat

**Autora:** Irene Paniagua Rodríguez  
**Asignatura:** Sistemas Multimedia  
**Tipo de proyecto:** Prototipo de videojuego 2D de aventura y exploración  
**Plataforma:** PC (Windows 64 bits)  
**Motor:** Unity  
**Género:** Aventura / Exploración / Pixel Art  

---

## Descripción general

**Portal’s Cat** es un pequeño prototipo de videojuego en 2D de aventura y exploración.  
Cuenta la historia de un **mago** y su **gato**, quien se adentra en un **portal mágico** que lo transporta a diferentes mundos.  
El jugador controla al mago, que deberá **viajar entre mundos** para rescatar a su gato antes de que **el tiempo se agote y el portal se cierre**.

El juego combina elementos de **exploración, combate, diálogo y puntuación**, con un estilo visual **Pixel Art** y una atmósfera mágica y ligera.

---

## Mecánicas principales

- Dos tipos de mundos:
  - **Mundo Pacífico**: el jugador puede explorar, hablar con NPCs y realizar pequeñas tareas.  
  - **Mundo Hostil**: aparecen enemigos que atacan al jugador; puede enfrentarse a ellos con magia.

- El **gato aparece en una posición aleatoria** en cada partida, fomentando la exploración.  
- Sistema de **puntuación por tiempo restante**.  
- **Animaciones completas** para todos los personajes.  
- **Sonido de fondo** y efectos básicos.  
- **Pantalla de niveles** con dos niveles funcionales por tipo de mundo.

---

## Elementos implementados

✅ Dos niveles funcionales (mundo pacífico y mundo hostil)  
✅ Gato con aparición aleatoria  
✅ Un tipo de enemigo funcional  
✅ Sistema de combate y puntos  
✅ Diálogos con NPCs  
✅ Sonido ambiental  
✅ Animaciones completas  
✅ Interfaz con temporizador y puntuación  

---
## Explicaciones y observaciones

- **Pequeño bug al iniciar el nivel:**  
  En ocasiones poco frecuentes, el nivel puede no iniciarse correctamente.  
  Si ocurre, acércate al portal y pulsa la tecla **E**. Se abrirá un menú con la opción **“Reiniciar”**; al seleccionarla, el juego se restablece y funciona correctamente.

- **Interacción con el portal:**  
  Se puede interactuar con el portal pulsando **E**, tanto para cambiar de mundo como para acceder a opciones.

- **Niveles y generación de mapas:**  
  El juego tiene **dos niveles**.  
  En principio, los mapas iban a generarse de forma **procedimental** (aleatoria), pero en esta versión se utilizan mapas fijos distintos para cada nivel.

- **Sprites modificados:**  
  Algunos sprites fueron **ligeramente editados o recoloreados** para adaptarlos al estilo del juego. Todos los recursos externos están correctamente acreditados en el apartado correspondiente.


---

## Elementos no implementados (aún)

🚫 Misiones completas de los NPCs (solo diálogos básicos)  
🚫 Efectos de sonido para ataques y acciones  
🚫 Sistema de energía del jugador  
🚫 Tienda para cambiar la apariencia del gato  
🚫 Radar del mundo hostil  
🚫 Puntos adicionales por derrotar enemigos  
🚫 Generación aleatoria completa de mapas  

---

## Dificultades y aprendizajes

**Dificultades principales:**
- Sincronizar las animaciones del inicio con los diálogos y eventos.
- Encontrar documentación específica para ciertas mecánicas en Unity.

**Partes más sencillas:**
- Diseño de interfaz.
- Creación de animaciones simples.

**Cambios durante el desarrollo:**
- Se simplificó el sistema de puntuación (de una fórmula compleja a puntos basados en tiempo restante).

---

## Fuentes y recursos externos


### Tutoriales (YouTube)
- [Hundred Fires Games](https://youtube.com/playlist?list=PLLtCXwcEVtulmgxqM_cA8hjIWkSNMWuie&si=kazL7zVUPkwqM3MH)  
- [BravePixelG](https://youtube.com/playlist?list=PLy1Xj-4F5G_cytIH8by-bZ9TVj5qKMlZn&si=veeAtptMhxAv53mR)  
- [Tutorial adicional](https://youtu.be/ccY78OsPIN0?si=5ISNsdR7KvQk6B3m)

### Sprites y tilesets (itch.io y CraftPix)

- Corazones → [ELV Games](https://elvgames.itch.io/free-inventory-asset-pack)  
- Paneles UI → [GX310](https://gx310.itch.io/pxiel-art-ui-borders)  
- Elementos UI → [Mounir Tohami](https://mounirtohami.itch.io/pixel-art-gui-elements), [Sr. Toasty](https://srtoasty.itch.io/ui-assets-pack-2)  
- Teclado UI → [Dreammix](https://dreammix.itch.io/keyboard-keys-for-ui)  
- Fuente pixel → [vrtxrry](https://vrtxrry.itch.io/dungeonfont)  
- Personaje principal → [penzilla](https://penzilla.itch.io/hooded-protagonist)  
- NPCs → [Szadi Art](https://szadiart.itch.io/3-direction-npc-characters)  
- Terreno, casa y árboles → [Shubibubi](https://shubibubi.itch.io/cozy-farm), [CraftPix - arbustos](https://craftpix.net/freebies/free-top-down-bushes-pixel-art/)  
- Enemigo → [CraftPix - slimes](https://craftpix.net/freebies/free-slime-mobs-pixel-art-top-down-sprite-pack/)  
- Gato → [Elthen’s Pixel Art Shop](https://elthen.itch.io/2d-pixel-art-cat-sprites)

### Otras referencias

- Imagen inicial obtenida desde [Pinterest](https://es.pinterest.com/pin/914512268085485643/sent/?invite_code=eee5295e45eb4c888b1471959c7b64fd&sender=914512405491435639&sfo=1)  
  (original de Tumblr por **@lettersfromavalon**)  
- Edición y creación de sprites adicionales en **Procreate**.

---

## Conclusión

Este proyecto me permitió aprender los **fundamentos del desarrollo de videojuegos**, desde la programación básica en Unity hasta la creación de animaciones, interfaz y narrativa visual.  
Si pudiera repetirlo, **simplificaría algunos elementos** del diseño inicial para centrarme más en la calidad visual y las funcionalidades clave.

Aunque actualmente no planeo continuar el desarrollo, considero que **Portal’s Cat tiene potencial para expandirse** con nuevas misiones, efectos y sistemas de progreso.

---

## Trabajo futuro

- Añadir misiones completas para los NPCs.  
- Implementar los efectos de sonido para ataques.  
- Completar el radar en el mundo hostil.  
- Crear la tienda para cambiar el diseño del gato.  
- Añadir más niveles y enemigos.  

---

## Documentación adicional

- [Memoria del proyecto (PDF)](./GDD-VersionFinal_IrenePaniagua.pdf)
 
- [Presentación (PDF)](./Presentacion_PortalsCat.pdf)  

---

## Descarga y prueba

Puedes probar el juego descargando el ejecutable aquí:  
**[Descargar Portal’s Cat – Versión Final (.zip)](https://github.com/irene2606/PortalsCat/releases/latest)**   

---

## Aviso de derechos

> Este proyecto se publica únicamente con fines demostrativos.  
> El contenido no puede ser reutilizado, modificado ni distribuido.  
> Los sprites, tilesets y demás recursos visuales pertenecen a sus respectivos autores, citados en este documento.


---

© 2025 Irene Paniagua Rodríguez. Todos los derechos reservados.

