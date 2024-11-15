# SalsaTech

SalsaTech es un proyecto interactivo diseñado para celebrar la riqueza cultural caleña a través de la salsa. Los usuarios pueden personalizar la experiencia de baile seleccionando combinaciones de trajes para dos bailarines (una chica y un chico) mediante fotografías de tarjetas con diferentes combinaciones de colores. Este proyecto utiliza tecnologías avanzadas como TensorFlow Lite y Unity para clasificar las imágenes y reflejar los resultados en un entorno 3D.

## Descripción del Proyecto

SalsaTech combina el uso de un modelo de red neuronal convolucional (CNN) entrenado para clasificar imágenes de las tarjetas seleccionadas, con una experiencia inmersiva desarrollada en Unity. Según las elecciones del usuario:

- Se actualizan los trajes de los personajes 3D.
- Cambia la música para reflejar el estilo caleño.
- Varían las animaciones de baile y los entornos (skyboxes).

El proyecto incluye:

1. **Modelo de Clasificación**: Entrenado en Google Colab usando TensorFlow y adaptado a TensorFlow Lite para integración en Unity.
2. **Escenario en Unity**: Incluye personajes con diferentes texturas, música, skyboxes y código para realizar los cambios dinámicamente.
3. **Dataset**: Creado a partir de imágenes de las tarjetas utilizadas, dividido para entrenamiento y validación.

---

## Contenido del Repositorio

- `Assets/`: Recursos de Unity como modelos, texturas, scripts y configuraciones.
- `UserSettings/`: Configuraciones específicas del proyecto en Unity.
- `Packages/`: Dependencias y paquetes utilizados.
- `ProjectSettings/`: Ajustes globales del proyecto en Unity.
- `model_training.py`: Script de entrenamiento del modelo con TensorFlow.
- `Dataset/`: Carpeta con imágenes de entrenamiento y validación, además de plantillas para imprimir nuevas tarjetas y sugerir diseños.

---

## Cómo Desplegar el Proyecto

### Requisitos Previos
- Unity instalado en la versión **2022.3.42f1**.

### Pasos

1. **Configurar el Proyecto en Unity**:
   - Descarga el proyecto.
   - Sustituye las carpetas `Assets`, `UserSettings`, `Packages` y `ProjectSettings` en tu proyecto de Unity con las incluidas en el repositorio.

2. **Ejecutar el Proyecto**:
   - Abre el proyecto en Unity.
   - Ejecuta la escena principal.

3. **Clasificar Imágenes**:
   - Toma fotos de las tarjetas con las combinaciones deseadas.
   - Sube las imágenes al sistema.
   - Observa cómo los trajes, la música, las animaciones y los entornos cambian dinámicamente.

---

## Para Entrenar o Mejorar el Modelo

- Usa el script `CNN_Salsa_Tech.ipynb` en Google Colab o en un entorno local.
- El dataset está disponible en la carpeta [Dataset de SalsaTech](https://drive.google.com/drive/folders/1NaXrRZTim6-2Q_ugeYxqY_uJPXduiTRq?usp=sharing).
  - Contiene imágenes para entrenamiento y validación.
  - Incluye las tarjetas para descargar.

---

## Dataset y Recursos Adicionales

El dataset y los recursos para entrenamiento están disponibles en el siguiente enlace:

[Dataset de SalsaTech](https://drive.google.com/drive/folders/1NaXrRZTim6-2Q_ugeYxqY_uJPXduiTRq?usp=sharing)

Incluye:
- Imágenes de entrenamiento y validación.
- Tarjetas para descargar e imprimir.

---

## Disfruta SalsaTech

Explora la salsa caleña de manera interactiva y personalizada. Contribuye al desarrollo del proyecto con ideas para nuevos trajes o mejoras en el modelo. ¡Que lo disfrutes!
