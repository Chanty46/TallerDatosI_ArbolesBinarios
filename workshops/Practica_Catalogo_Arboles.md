# Práctica: catálogo de árboles binarios

**Instituto Tecnológico de Costa Rica — Ingeniería en Computadores**  
**Modalidad:** parejas, hasta 13 parejas · **Duración:** dos clases de dos horas · **Evaluación:** formativa, sin nota  
**Entrega y exposición:** al finalizar la segunda clase · **Trabajo fuera de clase:** no requerido ni permitido para completar esta práctica  
**Lenguaje:** C# · **Integrantes:** Santiago Allon Quesada - Mathias Araya Gomez - Felipe Guerrero Cervantes 

## 1. Propósito

Construir y presentar un catálogo breve que permita reconocer, explicar y comparar BST, AVL, Red Black, Splay y Max Heap. Para cada estructura, usted completará una parte de su implementación, analizará sus reglas y representará gráficamente una operación.

Al finalizar, usted podrá:

- Explicar la estructura y las reglas de cada árbol.
- Relacionar una operación del código con cambios en enlaces, colores, alturas o índices.
- Comprobar resultados y reconocer casos extremos.
- Seleccionar una estructura según las operaciones de una aplicación.

## 2. Materiales y organización

Recibirá `PracticaArboles.cs`, que contiene el programa de consola, las estructuras y los ejemplos. Los métodos pendientes lanzan `NotImplementedException`: el programa muestra **PENDIENTE** y continúa con el siguiente ejemplo. Los resultados **OK** corresponden a comprobaciones concretas; no sustituyen la revisión completa de las reglas del árbol.

Complete los ocho métodos marcados TODO. Conserve sus firmas; puede añadir auxiliares privados. No utilice estructuras de biblioteca que sustituyan los árboles solicitados. El uso de `List<int>` en Heap está permitido: actúa como almacenamiento del arreglo, no implementa el heap.

En BST, AVL, Red Black y Splay se ignoran las claves duplicadas. Heap permite duplicados. En Red Black, `null` representa una hoja NIL negra. En AVL, la altura de un árbol vacío es 0 y la de una hoja es 1.

**Roles:** una persona programa y la otra revisa enlaces, reglas y resultados. Intercambie los roles con su compañero o compañera al pasar a otro árbol. Ambos deben poder explicar lo realizado.

**Ejecución:** en su entorno de C#, compile el archivo completo como programa de consola. Si utiliza el proyecto incluido y cuenta con el SDK .NET 8 o uno compatible, ejecute `dotnet run` desde esta carpeta. El proyecto desactiva las advertencias de nulabilidad para mantener el código de nodos sencillo.

## 3. Plan de trabajo

| Clase | Actividad | Minutos |
|---|---|---:|
| 1 | Leer instrucciones, ejecutar el programa y asignar el árbol de exposición | 10 |
| 1 | BST: eliminación y comprobación | 25 |
| 1 | AVL: rotaciones y comprobación | 25 |
| 1 | Heap: extracción y comprobación | 25 |
| 1 | Completar las tres primeras fichas, con sus conceptos y diagramas | 25 |
| 1 | Revisión de avances con el docente | 10 |
| 2 | Red Black: recoloreo, conceptos y diagrama | 25 |
| 2 | Splay: movimientos, conceptos y diagrama | 25 |
| 2 | Completar el catálogo y revisar el programa | 15 |
| 2 | Completar la comparación y justificar la selección de estructuras | 5 |
| 2 | Preparar la exposición y dejar lista la evidencia | 5 |
| 2 | Exposiciones de las parejas ante todo el grupo y cierre | 45 |

**Todo el trabajo se realiza durante las clases.** Al finalizar la primera clase, conserve sus archivos para retomarlos en la segunda; no debe completarlos en casa. Al minuto 75 de la segunda clase, el programa y el catálogo deben estar listos y comienza el bloque de exposiciones.

Escriba respuestas breves directamente en las fichas mientras resuelve cada ejercicio. No prepare un informe adicional ni una presentación de diapositivas. Incluya únicamente los diagramas obligatorios indicados a continuación; los casos complementarios se utilizan solo si queda tiempo antes de las exposiciones.

## 4. Ejercicio 1 — BST: eliminar sin perder el orden

### Programación

Complete `BST.Remove(Node n, int value)` (**TODO 1**). Debe devolver la nueva raíz del subárbol y contemplar:

1. Árbol vacío o valor inexistente.
2. Nodo hoja.
3. Nodo con un hijo.
4. Nodo con dos hijos: utilice el **sucesor**, obtenido con `Minimum(n.Right)`.

La llamada pública ya reasigna `Root`. Recuerde que una llamada recursiva también puede cambiar la raíz de un subárbol.

### Conceptos

- Enuncie la propiedad de orden para **todos los valores de cada subárbol**, no únicamente para los hijos inmediatos.
- ¿Por qué no basta con borrar directamente un nodo que tiene hijos?
- ¿Qué ventaja tiene usar el sucesor en el caso de dos hijos?
- ¿Qué forma puede adoptar el árbol si se insertan claves en orden ascendente? ¿Cómo afecta una búsqueda?

### Diagramas y comprobación

Inserte, en este orden: **50, 30, 70, 20, 40, 60, 80, 35**.

Dibuje el árbol inicial y el resultado de eliminar **50**, utilizando el sucesor. Marque el nodo sustituto y los enlaces modificados. Para eliminar **20** y **40**, identifique por escrito el caso correspondiente y compruébelo con el programa; no necesita dibujar estos dos casos. Las tres eliminaciones son **independientes** y parten del árbol inicial.

Ejecute los ejemplos de BST. El programa comprueba el recorrido inorder esperado y, al eliminar 50, la nueva raíz. Revise también eliminar 999, eliminar en vacío y eliminar la única raíz.

**Evidencia para la ficha:** antes y después de eliminar 50, clasificación de los tres casos y una línea con el resultado de una ejecución.

## 5. Ejercicio 2 — AVL: rotar y conservar subárboles

### Programación

Complete `AVL.RotateLeft` y `AVL.RotateRight` (**TODO 2 y 3**). Usted debe:

- Reasignar los enlaces sin perder el subárbol intermedio.
- Actualizar la altura de la antigua raíz y después la de la nueva raíz.
- Devolver la nueva raíz del subárbol.

La inserción, el cálculo de alturas y la elección del caso de rebalanceo ya están implementados.

### Conceptos

- Defina el factor de balance con la convención del código.
- ¿Qué factores están permitidos en un AVL?
- Explique cuándo se requiere una rotación simple y cuándo una doble.
- ¿Por qué importa el orden en que se actualizan las alturas?

### Diagramas y comprobación

| Caso | Secuencia de inserción |
|---|---|
| LL | 30, 20, 10 |
| RR | 10, 20, 30 |
| LR | 30, 10, 20 |
| RL | 10, 30, 20 |

Identifique por escrito las rotaciones de los cuatro casos. Dibuje únicamente el caso **LR** en sus tres etapas: antes, después de la primera rotación y después de la segunda. Anote alturas y factores de balance de los nodos involucrados. El diagrama del caso LL es complementario.

Ejecute los cuatro casos. Todos deben conservar el inorder **10 20 30** y terminar con raíz **20**, de altura **2**.

Ejecute además **30, 20, 40, 10, 25, 5**: localice dónde queda 25 y explique por qué ese subárbol no debe perderse.

**Evidencia para la ficha:** las tres etapas de la rotación doble LR y el resultado del caso que conserva el subárbol intermedio.

## 6. Ejercicio 3 — Max Heap: restaurar después de extraer

### Programación

Complete `MaxHeap.SiftDown(int i)` (**TODO 7**) y `MaxHeap.IsValid()` (**TODO 8**).

`ExtractMax` ya elimina la raíz, coloca en ella el último elemento y llama a `SiftDown`. Su método debe comparar ambos hijos, elegir el mayor y continuar únicamente cuando se necesita un intercambio. Contemple el caso con un solo hijo izquierdo.

`IsValid` debe comprobar que cada padre es mayor o igual que sus hijos existentes. Un heap vacío o de un elemento es válido.

### Conceptos

- ¿Qué significa que un árbol sea completo?
- ¿Qué diferencia hay entre la propiedad de orden del Max Heap y la del BST?
- Con índices desde 0, escriba las fórmulas del padre y de ambos hijos. Indique la restricción para calcular el padre de la raíz.
- ¿Se puede elegir un único subárbol para buscar cualquier clave, como en BST? Justifique.

### Diagramas y comprobación

Inserte **50, 30, 70, 20, 40, 60, 80**. Dibuje el heap obtenido y escriba el arreglo con sus índices.

Represente la primera extracción en tres etapas: estado inicial, sustitución de la raíz por el último elemento y estado final después de `SiftDown`. Señale los intercambios y mantenga correspondencia entre árbol y arreglo.

Ejecute los ejemplos. Las extracciones deben producir **80 70 60 50 40 30 20**. Compruebe también el caso **9, 9, 4**, que incluye duplicados y una extracción que deja un solo hijo.

**Evidencia para la ficha:** árbol y arreglo de la primera extracción, fórmulas de índices y resultado de las extracciones.

## 7. Ejercicio 4 — Red Black: reparar mediante colores

### Programación

Complete `RedBlackTree.RepairRedUncle(RBNode n)` (**TODO 4**). Se llama únicamente cuando el nodo tiene **padre rojo y tío rojo**.

Localice padre, abuelo y tío; realice el recoloreo y devuelva el nodo desde el cual debe continuar la reparación. `FixInsert` ya contiene los demás casos y garantiza que la raíz termine negra. No implemente eliminación en Red Black.

### Conceptos

- Escriba las reglas de un árbol Red Black, incluyendo las hojas NIL.
- ¿Por qué un nodo nuevo se inserta inicialmente rojo?
- ¿Por qué el recoloreo puede trasladar el problema hacia el abuelo?
- ¿Por qué Red Black no exige el mismo factor de balance que AVL?

### Diagramas y comprobación

Inserte **10, 5, 15** y agregue **1**. Dibuje el estado justo después de enlazar 1, antes de reparar, y el estado después de completar la reparación. Use **R** para rojo y **B** para negro. Incluya las hojas NIL en este diagrama.

Identifique la regla incumplida en cada estado hipotético:

| Estado | Descripción |
|---|---|
| A | Raíz 10(B), hijo izquierdo 5(R), hijo derecho 15(R); 5 tiene hijo izquierdo 1(R). Los demás enlaces son NIL. |
| B | Raíz 10(B), hijo izquierdo 5(B), hijo derecho 15(B); 5 tiene hijo izquierdo 1(B). Los demás enlaces son NIL. |

Para B, compare dos caminos desde la raíz hasta NIL y anote cuántos nodos negros tiene cada uno, incluyendo NIL y usando la misma convención en ambos caminos.

Ejecute el caso de padre y tío rojos. Después, ejecute **10, 5, 15, 1, 6, 12, 18, 0, -1, -2** y revise manualmente las reglas en el resultado. El recoloreo local no basta para demostrar que todo el árbol es válido.

**Evidencia para la ficha:** diagrama de recoloreo, reglas y explicación breve de las infracciones A y B.

## 8. Ejercicio 5 — Splay: acercar el elemento accedido

### Programación

Complete `SplayTree.ZigZig(Node n, bool left)` y `SplayTree.ZigZag(Node n, bool left)` (**TODO 5 y 6**). Utilice las rotaciones proporcionadas y devuelva la nueva raíz local.

- En `ZigZig`, `left=true` representa izquierda-izquierda y `false` derecha-derecha.
- En `ZigZag`, `left=true` representa izquierda-derecha y `false` derecha-izquierda.

Los métodos reciben una configuración en la que existen los dos enlaces necesarios. La búsqueda recursiva y el caso zig ya están implementados. Las alturas de `Node` no se utilizan en Splay.

### Conceptos

- ¿Qué sucede con un elemento encontrado después de la búsqueda?
- Diferencie zig, zig-zig y zig-zag.
- ¿Por qué Splay puede convenir cuando ciertas claves se consultan repetidamente?
- ¿Garantiza altura logarítmica después de cada acceso? Diferencie el costo de una operación del costo amortizado.

### Diagramas y comprobación

Identifique las rotaciones necesarias para estos dos estados iniciales:

1. Raíz **30**, hijo izquierdo **20**, nieto izquierdo **10**. Buscar **10**: zig-zig.
2. Raíz **30**, hijo izquierdo **10**, hijo derecho de 10 igual a **20**. Buscar **20**: zig-zag.

Dibuje únicamente el segundo caso, **zig-zag**: muestre el estado inicial, el resultado de la primera rotación y el resultado de la segunda. Marque el nodo accedido. El diagrama de zig-zig es complementario.

Ejecute los cuatro casos del programa, incluidos sus simétricos. El nodo encontrado debe quedar en la raíz y el inorder debe seguir siendo **10 20 30**. El programa repite el acceso y luego busca 999. Explique por qué una búsqueda fallida puede cambiar la raíz en esta implementación y qué propiedad debe conservarse.

**Evidencia para la ficha:** las tres etapas de zig-zag, identificación de las rotaciones de zig-zig y una explicación del acceso repetido.

## 9. Catálogo final

Prepare **cinco fichas breves**, una por árbol, de aproximadamente media página cada una; puede usar hasta una página si el diagrama lo requiere. Responda los conceptos con frases cortas o viñetas. Complete las cinco fichas aunque su exposición se centre en un solo árbol. Se permiten diagramas digitales o fotografías legibles de dibujos. Evite portada independiente, introducción extensa y capturas de toda la consola.

Copie esta estructura para cada árbol:

### [Nombre del árbol]

**Estructura y reglas:** describa cómo almacena los datos, su propiedad de orden y las restricciones que cumple.

**Balance o reorganización:** indique qué mecanismo usa o si no garantiza balance.

**Operación trabajada:** nombre del método completado y explicación breve de lo que cambia.

**Diagrama:** incorpore la evidencia solicitada en el ejercicio.

**Complejidad:** indique búsqueda, inserción y eliminación; distinga peor caso y amortizado cuando corresponda. Para Heap, separe consulta del máximo, extracción del máximo y búsqueda de una clave arbitraria. No atribuya a Heap la búsqueda ordenada de BST. Para Splay, considere las operaciones habituales que realizan splay; el código de esta práctica no incluye eliminación.

**Uso, ventaja y desventaja:** un ejemplo concreto y una frase para cada aspecto.

**Comprobación:** datos utilizados, resultado esperado, resultado obtenido y propiedad revisada.

## 10. Comparación y selección

Complete una tabla de cierre:

| Árbol | Estructura de almacenamiento | Propiedad de orden | Regla de balance o reorganización | Uso concreto | Ventaja | Desventaja |
|---|---|---|---|---|---|---|
| BST | | | | | | |
| AVL | | | | | | |
| Red Black | | | | | | |
| Splay | | | | | | |
| Max Heap | | | | | | |

Para cada situación, elija una estructura y justifique con una frase. Puede haber varias respuestas defendibles, pero usted debe relacionarlas con las operaciones requeridas.

1. Un catálogo recibe códigos en orden ascendente. Después se realizan muchas búsquedas y pocas modificaciones. Se requiere un límite logarítmico de peor caso para buscar.
2. Un sistema mantiene claves ordenadas con inserciones y eliminaciones frecuentes. Compare AVL con Red Black: explique un criterio de elección sin afirmar que uno siempre es más rápido.
3. Una aplicación consulta repetidamente un pequeño grupo de claves, aunque almacena muchas otras. Se acepta un costo amortizado.
4. Un planificador necesita consultar y retirar repetidamente la tarea de mayor prioridad. No requiere búsquedas arbitrarias por clave.
5. Para enseñanza, se necesita mostrar cómo la forma de un árbol depende del orden de inserción, con la implementación más sencilla entre estos árboles de búsqueda.

## 11. Entrega y exposición al final de la segunda clase

Entregue el catálogo completo de los **cinco árboles** y `PracticaArboles.cs` con los ocho métodos completos, mediante el medio indicado por el docente. Ambos archivos deben estar listos al comenzar las exposiciones, en el **minuto 75 de la segunda clase**. No se requiere trabajo en casa.

### Árbol asignado para la exposición

Cada pareja expondrá **un árbol** ante todo el grupo. El docente asignará el tema al inicio de la primera clase para evitar repeticiones excesivas. Todas las parejas deben completar los cinco ejercicios y las cinco fichas.

| Parejas | Árbol que expondrán |
|---|---|
| 1, 6 y 11 | BST |
| 2, 7 y 12 | AVL |
| 3, 8 y 13 | Red Black |
| 4 y 9 | Splay |
| 5 y 10 | Max Heap |

Si hay menos de 13 parejas, utilice los números disponibles. El orden de exposición será el número de pareja.

### Duración y contenido

| Distribución de tiempo | Actividad |
|---|---|
| 0:00–1:00 | Explicar la regla principal del árbol y mostrar el diagrama de la operación. |
| 1:00–2:00 | Señalar el método completado y explicar un cambio relevante de enlaces, colores o índices. |
| 2:00–2:30 | Mostrar un resultado ya preparado del programa; indicar qué se esperaba y qué propiedad confirma. Mencionar un uso, una ventaja y una limitación en forma breve. |
| 2:30–3:00 | Finalizar y dar paso a la siguiente pareja. |

Utilice el catálogo y el programa como apoyo; **no prepare diapositivas adicionales**. Deje abierto el fragmento de código y la evidencia antes de su turno. No ejecute todos los ejemplos durante la exposición: muestre únicamente el resultado elegido, mediante una salida ya disponible o una captura breve incluida en la ficha.

Con 13 parejas, los turnos ocupan **39 minutos**. Los **seis minutos restantes** del bloque se reservan para incidencias, comentarios generales y cierre del docente. Las consultas extensas se atenderán durante el trabajo o al cierre, sin prolongar cada turno.

### Revisión formativa

La práctica y la exposición **no tienen nota**. El docente observará:

- Si el programa produce los resultados esperados y conserva las reglas de las estructuras.
- Si los diagramas corresponden a las operaciones explicadas.
- Si el catálogo contiene las cinco fichas y la comparación final.
- Si ambos integrantes participan y pueden explicar el código.
- Si la pareja comunica con claridad y respeta el tiempo asignado.

Antes de entregar, compruebe que los ocho TODO fueron sustituidos; que los recorridos, raíces y alturas coinciden con los resultados esperados; que las reglas de Red Black incluyen NIL; y que el árbol y el arreglo de Heap representan el mismo estado. Verifique también que el catálogo diferencia peor caso y costo amortizado.

## 12. Casos complementarios, si queda tiempo

- **BST:** elimine una raíz con un único hijo izquierdo y otra con un único hijo derecho.
- **AVL:** inserte del 1 al 10 y revise alturas y factores de balance en todos los nodos.
- **Red Black:** inserte del 1 al 10 y compruebe orden, enlaces de padre, ausencia de rojo-rojo y caminos negros.
- **Splay:** inserte 10, 20, 30, 40, 50; busque 20 dos veces y luego 40. Dibuje las raíces sucesivas.
- **Heap:** inserte -5, -1, -3 y compruebe las extracciones. Explique por qué el algoritmo también funciona con negativos.
